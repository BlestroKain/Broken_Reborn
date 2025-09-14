using Intersect.Enums;
using Intersect.Server.Core;
using Intersect.Server.Entities;
using Intersect.Server.Localization;
using Intersect.Utilities;
using Intersect.Combat;
using NCalc;
using Newtonsoft.Json;

namespace Intersect.Server.General;

public partial class Formulas
{
    private const string DefaultFormulaMagicDamage = "Random(((BaseDamage + (ScalingStat * ScaleFactor))) * CritMultiplier * .975, ((BaseDamage + (ScalingStat * ScaleFactor))) * CritMultiplier * 1.025) * (100 / (100 + V_MagicResist))";
    private const string DefaultFormulaPhysicalDamage = "Random(((BaseDamage + (ScalingStat * ScaleFactor))) * CritMultiplier * .975, ((BaseDamage + (ScalingStat * ScaleFactor))) * CritMultiplier * 1.025) * (100 / (100 + V_Defense))";
    private const string DefaultFormulaTrueDamage = "Random(((BaseDamage + (ScalingStat * ScaleFactor))) * CritMultiplier * .975, ((BaseDamage + (ScalingStat * ScaleFactor))) * CritMultiplier * 1.025)";

    private static readonly string FormulasFile = Path.Combine(ServerContext.ResourceDirectory, "formulas.json");
    private static Formulas? _formulas;

    public Formula ExpFormula = new("BaseExp * Power(Gain, Level)");

    public string MagicDamage = DefaultFormulaMagicDamage;
    public string PhysicalDamage = DefaultFormulaPhysicalDamage;
    public string TrueDamage = DefaultFormulaTrueDamage;

    public static void LoadFormulas()
    {
        Console.WriteLine("Loading formulae...");

        try
        {
            _formulas = new Formulas();
            if (File.Exists(FormulasFile))
            {
                _formulas = JsonConvert.DeserializeObject<Formulas>(File.ReadAllText(FormulasFile)) ?? _formulas;
            }

            File.WriteAllText(FormulasFile, JsonConvert.SerializeObject(_formulas, Formatting.Indented));

            Expression.CacheEnabled = false;
        }
        catch (Exception ex)
        {
            throw new Exception(Strings.Formulas.Missing, ex);
        }
    }

    /// <summary>
    /// Calculates the final damage applied to a <paramref name="victim"/> from an <paramref name="attacker"/>.
    /// </summary>
    /// <param name="baseDamage">Base damage value before scaling and bonuses.</param>
    /// <param name="damageType">Type of damage being inflicted.</param>
    /// <param name="scalingStat">Statistic used for scaling the damage.</param>
    /// <param name="scaling">Scaling percentage applied to <paramref name="scalingStat"/>.</param>
    /// <param name="critMultiplier">Critical hit multiplier.</param>
    /// <param name="attacker">Entity dealing the damage.</param>
    /// <param name="victim">Entity receiving the damage.</param>
    /// <param name="attackerLevel">Optional override for the attacker's level.</param>
    /// <param name="attackerElement">Elemental type of the attack.</param>
    /// <remarks>
    /// Final damage is calculated as
    /// <c>result * (1 + attackerElementBonus) * affinityMultiplier * (1 - defenderResistance)</c> where:
    /// <list type="bullet">
    /// <item><c>result</c> is the raw damage determined by the configured formula.</item>
    /// <item><c>attackerElementBonus</c> is the bonus damage from the attacker's element.</item>
    /// <item><c>affinityMultiplier</c> is the multiplier from the affinity between attacker and defender elements.</item>
    /// <item><c>defenderResistance</c> is the defender's resistance to the attacker's element.</item>
    /// </list>
    /// </remarks>
    public static long CalculateDamage(
        long baseDamage,
        DamageType damageType,
        Stat scalingStat,
        int scaling,
        double critMultiplier,
        Entity attacker,
        Entity victim,
        int? attackerLevel = null,
        ElementType attackerElement = ElementType.Neutral
    )
    {
        if (_formulas == null)
        {
            throw new InvalidOperationException("Formulas not yet initialized");
        }

        ArgumentNullException.ThrowIfNull(attacker, nameof(attacker));
        ArgumentNullException.ThrowIfNull(victim);

        if (attacker.Stat == null)
        {
            throw new ArgumentException(
                $@"{nameof(attacker)}.{nameof(attacker.Stat)} is null",
                nameof(attacker)
            );
        }

        if (victim.Stat == null)
        {
            throw new ArgumentException($@"{nameof(victim)}.{nameof(victim.Stat)} is null", nameof(victim));
        }

        var expressionString = damageType switch
        {
            DamageType.Physical => _formulas.PhysicalDamage,
            DamageType.Magic => _formulas.MagicDamage,
            DamageType.True => _formulas.TrueDamage,
            _ => _formulas.TrueDamage,
        };

        var expression = new Expression(expressionString);
        var negate = false;

        if (baseDamage < 0)
        {
            baseDamage = Math.Abs(baseDamage);
            negate = true;
        }

        if (expression.Parameters == null)
        {
            throw new ArgumentNullException(nameof(expression.Parameters));
        }

        try
        {
            expression.Parameters["BaseDamage"] = baseDamage;
            expression.Parameters["ScalingStat"] = attacker.Stat[(int)scalingStat].Value();
            expression.Parameters["ScaleFactor"] = scaling / 100f;
            expression.Parameters["CritMultiplier"] = critMultiplier;
            expression.Parameters["A_Attack"] = attacker.Stat[(int)Stat.Attack].Value();
            expression.Parameters["A_Defense"] = attacker.Stat[(int)Stat.Defense].Value();
            // Use agility in place of speed for combat calculations
            expression.Parameters["A_Speed"] = attacker.Stat[(int)Stat.Agility].Value();
            expression.Parameters["A_AbilityPwr"] = attacker.Stat[(int)Stat.Intelligence].Value();
            expression.Parameters["A_MagicResist"] = attacker.Stat[(int)Stat.Vitality].Value();
            expression.Parameters["A_Level"] = attackerLevel ?? attacker.Level;
            expression.Parameters["V_Attack"] = victim.Stat[(int)Stat.Attack].Value();
            expression.Parameters["V_Defense"] = victim.Stat[(int)Stat.Defense].Value();
            // Use agility in place of speed for combat calculations
            expression.Parameters["V_Speed"] = victim.Stat[(int)Stat.Agility].Value();
            expression.Parameters["V_AbilityPwr"] = victim.Stat[(int)Stat.Intelligence].Value();
            expression.Parameters["V_MagicResist"] = victim.Stat[(int)Stat.Vitality].Value();
            expression.Parameters["V_Level"] = victim.Level;

            expression.EvaluateFunction += delegate(string name, FunctionArgs args)
            {
                ArgumentNullException.ThrowIfNull(args);

                if (name == "Random")
                {
                    args.Result = Random(args);
                }
            };

            var result = Convert.ToDouble(expression.Evaluate());
            if (negate)
            {
                result = -result;
            }

            var attackerElementBonus = attacker.GetElementDamageBonus(attackerElement);
            var defenderResistance = victim.GetResistance(attackerElement);

            var defenderElement = ElementType.Neutral;
            if (victim is Player victimPlayer && victimPlayer.LastAttackingWeapon != null)
            {
                defenderElement = victimPlayer.LastAttackingWeapon.Element;
            }

            var affinityMultiplier = ElementalAffinity.GetMultiplier(attackerElement, defenderElement);

            // Final damage is calculated using the following factors:
            //   result:            damage determined by the configured formula
            //   attackerElementBonus: bonus damage provided by the attacker's elemental affinity
            //   affinityMultiplier:  multiplier based on the relation between attacker's and defender's elements
            //   defenderResistance:  damage reduction provided by the defender's elemental resistance
            // Formula: result * (1 + attackerElementBonus) * affinityMultiplier * (1 - defenderResistance)
            var finalDamage = result * (1 + attackerElementBonus) * affinityMultiplier * (1 - defenderResistance);

            return (long)Math.Round(finalDamage);
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to evaluate damage formula", ex);
        }
    }

    private static int Random(FunctionArgs args)
    {
        if (args.Parameters == null)
        {
            throw new ArgumentNullException(nameof(args.Parameters));
        }

        var parameters = args.EvaluateParameters() ??
                         throw new NullReferenceException($"{nameof(args.EvaluateParameters)}() returned null.");

        if (parameters.Length < 2)
        {
            throw new ArgumentException($"{nameof(Random)}() requires 2 numerical parameters.");
        }

        var min = (int)Math.Round(
            (double)(parameters[0] ?? throw new NullReferenceException("First parameter is null."))
        );

        var max = (int)Math.Round(
            (double)(parameters[1] ?? throw new NullReferenceException("Second parameter is null."))
        );

        return min >= max ? min : Randomization.Next(min, max + 1);
    }
}