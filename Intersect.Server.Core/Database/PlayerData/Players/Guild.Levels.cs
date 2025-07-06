using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;


using Intersect.Enums;
using Intersect.Server.Networking;
using Intersect.Framework.Core.GameObjects.Guild;


namespace Intersect.Server.Database.PlayerData.Players;

public partial class Guild
{
    // ===============================
    // Sistema de Nivel, XP y Mejoras
    // ===============================

    public int Level { get; set; } = 1;

    public long Experience { get; set; } = 0;

    public int GuildPoints { get; set; } = 0;

    public int SpentGuildPoints { get; set; } = 0;

    // Indica el archivo (o “ID”/“nombre”) para el fondo
    public string LogoBackground { get; set; } = string.Empty;

    // Colores RGB para el fondo
    public byte BackgroundR { get; set; } = 255;
    public byte BackgroundG { get; set; } = 255;
    public byte BackgroundB { get; set; } = 255;

    // Indica el archivo para el símbolo
    public string LogoSymbol { get; set; } = string.Empty;

    // Colores RGB para el símbolo
    public byte SymbolR { get; set; } = 255;
    public byte SymbolG { get; set; } = 255;
    public byte SymbolB { get; set; } = 255;
    public void SetLogo(
   string backgroundFile,
   byte bgR, byte bgG, byte bgB,
   string symbolFile,
   byte symR, byte symG, byte symB
 )
    {
        // Asignar propiedades
        LogoBackground = backgroundFile ?? string.Empty;
        BackgroundR = bgR;
        BackgroundG = bgG;
        BackgroundB = bgB;

        LogoSymbol = symbolFile ?? string.Empty;
        SymbolR = symR;
        SymbolG = symG;
        SymbolB = symB;

        // Guardar en DB
        Save(); // Usa tu método lock(mLock) { ... context.SaveChanges() }

        // Opcional: notificar a los miembros online que el “logo” cambió
        UpdateMemberList();
    }

    /// <summary>
    /// Añade experiencia al gremio y verifica si puede subir de nivel.
    /// </summary>

    [NotMapped]
    public Dictionary<GuildUpgradeType, int> GuildUpgrades = new();

    public string GuildUpgradesData
    {
        get => JsonConvert.SerializeObject(GuildUpgrades);
        set => GuildUpgrades = string.IsNullOrEmpty(value)
            ? new()
            : JsonConvert.DeserializeObject<Dictionary<GuildUpgradeType, int>>(value);
    }
    public static Dictionary<GuildUpgradeType, int> MaxUpgradeLevels { get; set; } = new()
{
    { GuildUpgradeType.ExtraMembers, 5 },
    { GuildUpgradeType.ExtraBankSlots, 5 },
    { GuildUpgradeType.BonusXp, 3 },
    { GuildUpgradeType.BonusDrop, 5 },
    { GuildUpgradeType.BonusJobXp, 3 } // NUEVO
};

    public static Dictionary<GuildUpgradeType, int[]> UpgradeCosts = new()
{
    { GuildUpgradeType.ExtraMembers, new[] {3, 5, 7, 9, 15} },
    { GuildUpgradeType.ExtraBankSlots, new[] {1, 2, 3, 4, 5} },
    { GuildUpgradeType.BonusXp, new[] {6, 11, 20} },
    { GuildUpgradeType.BonusDrop, new[] {4, 8, 12, 16, 20} },
    { GuildUpgradeType.BonusJobXp, new[] {5, 10, 15} } // NUEVO
};

    public long ExperienceToNextLevel => CalculateRequiredExperience(Level);

    /// <summary>
    /// Calcula la XP necesaria para subir al siguiente nivel usando los valores configurables.
    /// </summary>
    private long CalculateRequiredExperience(int level)
    {
        return (long)(Options.Instance.Guild.BaseXP * Math.Pow(level, Options.Instance.Guild.GrowthFactor));
    }

    /// <summary>
    /// Calcula la cantidad máxima de miembros en un gremio según su nivel.
    /// </summary>
    private int CalculateMaxMembers()
    {
        return Options.Instance.Guild.InitialMaxMembers; // +5 miembros por nivel
    }

    /// <summary>
    /// Añade experiencia al gremio y verifica si puede subir de nivel.
    /// </summary>
    public void AddExperience(long amount)
    {
        if (amount <= 0)
            return;

        Experience += amount;

        if (CheckLevelUp())
        {
            UpdateMemberList(); // Refrescar en clientes
        }

        Save(); // Guardar en DB
    }

    private bool CheckLevelUp()
    {
        var levelUps = 0;

        while (Experience >= CalculateRequiredExperience(Level + levelUps) &&
               CalculateRequiredExperience(Level + levelUps) > 0)
        {
            Experience -= CalculateRequiredExperience(Level + levelUps);
            levelUps++;
        }

        if (levelUps <= 0)
            return false;

        LevelUp(false, levelUps);
        return true;
    }

    public void LevelUp(bool resetExperience = false, int levels = 1)
    {
        if (levels <= 0)
            return;

        const int MaxLevel = 100;

        for (int i = 0; i < levels; i++)
        {
            if (Level >= MaxLevel)
            {
                Level = MaxLevel;
                Experience = 0;
                break;
            }

            Level++;

            if (resetExperience)
            {
                Experience = 0;
            }

            // Ganar puntos por nivel (1 por defecto, puedes ajustar)
            GuildPoints++;

            // Notificar a miembros online
            foreach (var member in FindOnlineMembers())
            {
                PacketSender.SendChatMsg(member, $"¡El gremio ha subido al nivel {Level}!", ChatMessageType.Guild);
            }
        }

        UpdateMemberList();
        Save();
    }

    public void SetLevel(int level, bool resetExperience = false)
    {
        const int MaxLevel = 100;

        if (level < 1)
            return;

        Level = Math.Min(level, MaxLevel);

        if (resetExperience)
        {
            Experience = 0;
        }

        UpdateMemberList();
        Save();
    }
    public bool HasUpgrade(GuildUpgradeType upgrade)
    {
        return GuildUpgrades.TryGetValue(upgrade, out var level) && level > 0;
    }

    public int GetUpgradeLevel(GuildUpgradeType type)
    {
        if (!GuildUpgrades.ContainsKey(type))
        {
            GuildUpgrades[type] = 0;
        }
        return GuildUpgrades[type];
    }
    public bool ApplyUpgrade(GuildUpgradeType upgrade)
    {
        // Inicializar si no existe
        if (!GuildUpgrades.ContainsKey(upgrade))
        {
            GuildUpgrades[upgrade] = 0;
        }

        var currentLevel = GuildUpgrades[upgrade];

        // Verificación de nivel máximo
        if (!MaxUpgradeLevels.ContainsKey(upgrade))
        {
            Console.WriteLine($"[GuildUpgrade] Error: MaxUpgradeLevels no contiene la mejora {upgrade}.");
            return false;
        }

        var maxLevel = MaxUpgradeLevels[upgrade];
        if (currentLevel >= maxLevel)
        {
            Console.WriteLine($"[GuildUpgrade] Ya alcanzado el nivel máximo para {upgrade}.");
            return false;
        }

        // Verificación de costos
        if (!UpgradeCosts.TryGetValue(upgrade, out var costs))
        {
            Console.WriteLine($"[GuildUpgrade] Error: No hay costos definidos para {upgrade}.");
            return false;
        }

        if (currentLevel >= costs.Length)
        {
            Console.WriteLine($"[GuildUpgrade] Error: No hay costo definido para nivel {currentLevel} de {upgrade}.");
            return false;
        }
        var cost = costs[currentLevel];
        if (GuildPoints < cost)
        {
            Console.WriteLine($"[GuildUpgrade] No hay suficientes puntos. Requiere {cost}, disponibles {GuildPoints}.");
            return false;
        }

        // Aplicar mejora
        GuildUpgrades[upgrade] = currentLevel + 1;
        SpentGuildPoints += cost;
        GuildPoints -= cost;
        // Efecto inmediato si aplica

        switch (upgrade)
        {
            case GuildUpgradeType.ExtraBankSlots:
                ExpandBankSlots(BankSlotsCount + 10); // +10 por nivel
                foreach (var member in FindOnlineMembers())
                {
                    PacketSender.SendChatMsg(member, "¡El gremio ha mejorado los espacios del banco! +10 espacios nuevos.", ChatMessageType.Guild);
                }
                break;

            case GuildUpgradeType.ExtraMembers:
                foreach (var member in FindOnlineMembers())
                {
                    PacketSender.SendChatMsg(member, "¡El gremio puede aceptar más miembros! Límite aumentado.", ChatMessageType.Guild);
                }
                break;

            case GuildUpgradeType.BonusXp:
                foreach (var member in FindOnlineMembers())
                {
                    PacketSender.SendChatMsg(member, "¡El gremio ha mejorado su bonificación de experiencia!", ChatMessageType.Guild);
                }
                break;

            case GuildUpgradeType.BonusDrop:
                foreach (var member in FindOnlineMembers())
                {
                    PacketSender.SendChatMsg(member, "¡El gremio ha mejorado su bonificación de drop!", ChatMessageType.Guild);
                }
                break;
            case GuildUpgradeType.BonusJobXp:
                foreach (var member in FindOnlineMembers())
                {
                    PacketSender.SendChatMsg(member, "¡El gremio ha mejorado su bonificación de experiencia de oficios!", ChatMessageType.Guild);
                }
                break;

        }


        // Actualizar y guardar
        UpdateMemberList();
        Save();

        Console.WriteLine($"[GuildUpgrade] Mejora {upgrade} aplicada con éxito. Nuevo nivel: {GuildUpgrades[upgrade]}");

        return true;
    }
    public float GetJobXpBonusMultiplier()
    {
        var bonusLevel = GetUpgradeLevel(GuildUpgradeType.BonusJobXp);
        return 1f + (bonusLevel * 0.05f); // 5% por nivel
    }

    public float GetXpBonusMultiplier()
    {
        var bonusLevel = GetUpgradeLevel(GuildUpgradeType.BonusXp);
        return 1f + (bonusLevel * 0.05f); // 5% por nivel
    }
    public int GetMaxBankSlots()
    {
        var extra = GetUpgradeLevel(GuildUpgradeType.ExtraBankSlots) * 10;
        return Options.Instance.Guild.InitialBankSlots + extra;
    }
    public int GetMaxMembers()
    {
        var baseMembers = CalculateMaxMembers();
        var extra = GetUpgradeLevel(GuildUpgradeType.ExtraMembers) * 10;
        return baseMembers + extra;
    }
  

}
