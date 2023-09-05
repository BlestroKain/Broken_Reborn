using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using Intersect.Enums;
using Intersect.GameObjects.Conditions;
using Intersect.GameObjects.Events;
using Intersect.Models;
using Intersect.Utilities;

using Newtonsoft.Json;

namespace Intersect.GameObjects
{
    public partial class PetBase : DatabaseObject<PetBase>, IFolderable
    {
        [NotMapped]
        public ConditionLists FollowOwnerConditions = new ConditionLists();

        [NotMapped] public int[] MaxVital = new int[(int)Vital.VitalCount];

        [NotMapped] public int[] Stats = new int[(int)Enums.Stat.StatCount];

        [NotMapped] public int[] VitalRegen = new int[(int)Vital.VitalCount];

        [NotMapped]
        public List<SpellEffect> Immunities = new List<SpellEffect>();

        [JsonIgnore]
        [Column("Immunities")]
        public string ImmunitiesJson
        {
            get => JsonConvert.SerializeObject(Immunities);
            set
            {
                Immunities = JsonConvert.DeserializeObject<List<SpellEffect>>(value ?? "") ?? new List<SpellEffect>();
            }
        }
        public int Damage { get; set; }
        public DamageType DamageType { get; set; }
        public Stat ScalingStat { get; set; }
        public int Scaling { get; set; }
        public int CritChance { get; set; }
        public double CritMultiplier { get; set; }

        [Column("AttackAnimation")]
        public Guid AttackAnimationId { get; set; }
        [NotMapped]
        [JsonIgnore]
        public AnimationBase AttackAnimation
        {
            get => AnimationBase.Get(AttackAnimationId);
            set => AttackAnimationId = value?.Id ?? Guid.Empty;
        }

        [Column("DeathAnimation")]
        public Guid DeathAnimationId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public AnimationBase DeathAnimation
        {
            get => AnimationBase.Get(DeathAnimationId);
            set => DeathAnimationId = value?.Id ?? Guid.Empty;
        }

        [JsonConstructor]
        public PetBase(Guid id) : base(id)
        {
            Name = "New Pet";
        }

        //Parameterless constructor for EF
        public PetBase()
        {
            Name = "New Pet";
        }


        public long Experience { get; set; }

        public int Level { get; set; } = 1;

        //Vitals & Stats
        [Column("MaxVital")]
        [JsonIgnore]
        public string JsonMaxVital
        {
            get => DatabaseUtils.SaveIntArray(MaxVital, (int)Vital.VitalCount);
            set => DatabaseUtils.LoadIntArray(ref MaxVital, value, (int)Vital.VitalCount);
        }
        public string Sprite { get; set; } = "";

        [Column("Stats")]
        [JsonIgnore]
        public string JsonStat
        {
            get => DatabaseUtils.SaveIntArray(Stats, (int)Enums.Stat.StatCount);
            set => DatabaseUtils.LoadIntArray(ref Stats, value, (int)Enums.Stat.StatCount);
        }

        //Vital Regen %
        [JsonIgnore]
        [Column("VitalRegen")]
        public string RegenJson
        {
            get => DatabaseUtils.SaveIntArray(VitalRegen, (int)Vital.VitalCount);
            set => VitalRegen = DatabaseUtils.LoadIntArray(value, (int)Vital.VitalCount);
        }

        /// <inheritdoc />
        public string Folder { get; set; } = "";
        //Spells
        [JsonIgnore]
        [Column("Spells")]
        public string CraftsJson
        {
            get => JsonConvert.SerializeObject(Spells, Formatting.None);
            protected set => Spells = JsonConvert.DeserializeObject<DbList<SpellBase>>(value);
        }

        [NotMapped]
        public DbList<SpellBase> Spells { get; set; } = new DbList<SpellBase>();
        public int AttackSpeedValue { get; set; }
        public int AttackSpeedModifier { get; set; }

        public SpellBase GetRandomSpell(Random random)
        {
            if (Spells == null || Spells.Count == 0)
            {
                return null;
            }

            var spellIndex = random.Next(0, Spells.Count);
            var spellId = Spells[spellIndex];

            return SpellBase.Get(spellId);
        }
    }
}
