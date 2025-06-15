using Intersect.Enums;

namespace Intersect.Config;

    /// <summary>
    /// Contains configurable options pertaining to the way Jobs are handled by the engine.
    /// </summary>
    public partial class JobOptions
    {
        /// <summary>
        /// The maximum level a job can achieve.
        /// </summary>
        public int MaxJobLevel { get; set; } = 100;

        /// <summary>
        public string RecipesId = "2d6cffdb-2611-40e2-9085-8256dec204a2";

        /// <summary>
        /// Growth rate for job experience required per level.
        /// </summary>
        public double ExpGrowthRate { get; set; } = 1.7;
        /// <summary>
        /// Determines the rate at which job-related bonuses are applied (e.g., resource yield).
        /// </summary>
        public double JobBonusRate { get; set; } = 1.2;
        /// <summary>
        /// A dictionary containing base experience configurations for each job type.
        /// </summary>
        public Dictionary<JobType, long> JobBaseExp { get; set; } = new Dictionary<JobType, long>
        {
            { JobType.Farming, 100 },
            { JobType.Mining, 120 },
            { JobType.Fishing, 110 },
            { JobType.Lumberjack, 90 },
            { JobType.Cooking, 150 },
            { JobType.Alchemy, 130 },
            { JobType.Crafting, 140 },
            { JobType.Smithing, 160 },
            { JobType.Hunter, 170 },
            { JobType.Jewerly, 180 },
            { JobType.Tanner, 140 },
            { JobType.Tailoring, 120 }
        };

    }

    /// <summary>
    /// Enum defining different types of jobs available in the game.
    /// </summary>
    public enum JobType
    {
        // Recolection skills
        None,
        Farming,
        Mining,
        Lumberjack,
        Fishing,
        Hunter,

        // Crafting skills
        Cooking,
        Smithing,
        Alchemy,
        Crafting,
        Jewerly,
        Tanner,
        Tailoring,

        // Count (Used for validation and iteration)
        JobCount
    }

