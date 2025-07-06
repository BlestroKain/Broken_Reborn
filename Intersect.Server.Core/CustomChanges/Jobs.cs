using System.ComponentModel.DataAnnotations.Schema;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using Newtonsoft.Json;

namespace Intersect.Server.Entities
{
    public partial class Player : Entity
    {
        [Column("JobsData")]
        public string JobsJson
        {
            get => JsonConvert.SerializeObject(Jobs);

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    // PacketSender.SendChatMsg(this, "Advertencia: No se encontraron datos de trabajos. Inicializando por defecto.", ChatMessageType.Experience);
                    InitializeJobs();
                }
                else
                {
                    Jobs = JsonConvert.DeserializeObject<Dictionary<JobType, PlayerJob>>(value)
                           ?? new Dictionary<JobType, PlayerJob>();
                    InitializeJobs(); // Garantiza que los trabajos faltantes se inicialicen
                }
            }
        }

        [NotMapped]
        public Dictionary<JobType, PlayerJob> Jobs { get; set; } = new Dictionary<JobType, PlayerJob>();

        public void InitializeJobs()
        {
            // Recorremos el rango válido de JobType (del primero al último trabajo real)
            for (var jobIndex = (int)JobType.Farming; jobIndex < (int)JobType.JobCount; jobIndex++)
            {
                var jobType = (JobType)jobIndex;

                if (!Jobs.ContainsKey(jobType))
                {
                    Jobs[jobType] = new PlayerJob(jobType);
                    //  PacketSender.SendChatMsg(this, $"Trabajo inicializado: {jobType}", ChatMessageType.Notice);
                }
            }
        }

        public void GiveJobExperience(JobType jobType, long experience)
        {
            if (Jobs.TryGetValue(jobType, out var job))
            {
                job.AddExperience(experience, this);
            }
            else
            {
                //PacketSender.SendChatMsg(this, $"Error: El trabajo '{jobType}' no está inicializado.", ChatMessageType.Error);
            }
        }

        public void SetJobLevel(JobType jobType, int level, bool resetExperience = false)
        {
            if (Jobs.TryGetValue(jobType, out var job))
            {
                job.SetLevel(level, resetExperience, this);
            }
            else
            {
                PacketSender.SendChatMsg(this, $"Error: El trabajo '{jobType}' no está inicializado.", ChatMessageType.Error);
            }
        }

        public PlayerJob GetJob(JobType jobType) =>
            Jobs.TryGetValue(jobType, out var job) ? job : null;
    }

    public class PlayerJob
    {
        public JobType JobType { get; set; }
        public int JobLevel { get; set; } = 1;
        public long JobExp { get; set; } = 0;

        public PlayerJob(JobType jobType)
        {
            JobType = jobType;
        }

        public void AddExperience(long amount, Player player)
        {
            // Aplicar multiplicador del gremio si pertenece a uno
            if (player.Guild != null)
            {
                var multiplier = player.Guild.GetJobXpBonusMultiplier();
                amount = (long)(amount * multiplier);
            }

            // Aplicar multiplicador del gremio si pertenece a uno
            if (player.Guild != null)
            {
              //  var multiplier = player.Guild.GetJobXpBonusMultiplier();
                amount = (long)(amount/* * multiplier*/);
            }

            JobExp += amount;

            // Nivelar si se supera la experiencia necesaria para el siguiente nivel
            while (JobExp >= GetExperienceToNextLevel(JobLevel) && JobLevel < Options.Instance.JobOpts.MaxJobLevel)
            {
                JobExp -= GetExperienceToNextLevel(JobLevel);
                LevelUp(player);
            }

            PacketSender.SendJobSync(player); // Sincroniza los datos después de actualizar
        }


        public void SetLevel(int newLevel, bool resetExperience, Player player)
        {
            JobLevel = Math.Clamp(newLevel, 1, Options.Instance.JobOpts.MaxJobLevel);

            if (resetExperience)
            {
                JobExp = 0;
            }

            PacketSender.SendJobSync(player);
        }

        public long GetExperienceToNextLevel(int currentLevel)
        {
            if (!Options.Instance.JobOpts.JobBaseExp.TryGetValue(JobType, out var baseExp))
            {
                PacketSender.SendChatMsg(player: null, $"Error: No se estableció la experiencia base para el trabajo {JobType}.", ChatMessageType.Error);
                return -1;
            }

            return (long)(baseExp * Math.Pow(currentLevel, Options.Instance.JobOpts.ExpGrowthRate));
        }

        private void LevelUp(Player player)
        {
            JobLevel++;

            var levelUpMessage = Strings.Player.GetJobLevelUpMessage(JobType);
            PacketSender.SendChatMsg(player, string.Format(levelUpMessage, JobLevel), ChatMessageType.Experience);
            PacketSender.SendActionMsg(player, $"¡{JobType} Level UP!", CustomColors.Combat.JobLevelUp);
        }
    }
}
