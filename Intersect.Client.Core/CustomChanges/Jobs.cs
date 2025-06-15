using Intersect.Client.Networking;
using Intersect.Config;
using Intersect.Network.Packets.Server;


namespace Intersect.Client.Entities;

public partial class Player
{
    // Inicializa los diccionarios al crear el jugador
    public Dictionary<JobType, int> JobLevel { get; set; } = [];
    public Dictionary<JobType, long> JobExp { get; set; } = [];
    public Dictionary<JobType, long> JobExpToNextLevel { get; set; } = [];


    public void UpdateJobsFromPacket(Dictionary<JobType, JobData> jobData)
    {
        if (jobData == null)
        {
            PacketSender.SendChatMsg("Error: El paquete de datos de trabajos está vacío.", 5);
            return;
        }

        foreach (var job in jobData)
        {
            var jobType = job.Key;
            var jobDetails = job.Value;

            // Asegurar inicialización
            if (!JobLevel.ContainsKey(jobType))
            {
                JobLevel[jobType] = 1;
            }

            if (!JobExp.ContainsKey(jobType))
            {
                JobExp[jobType] = 0;
            }

            if (!JobExpToNextLevel.ContainsKey(jobType))
            {
                JobExpToNextLevel[jobType] = 100;
            }

            // Actualizar valores
            JobLevel[jobType] = jobDetails.Level;
            JobExp[jobType] = jobDetails.Experience;
            JobExpToNextLevel[jobType] = jobDetails.ExperienceToNextLevel;

            // Depuración en el cliente
            //  PacketSender.SendChatMsg($"Trabajo {jobType} actualizado: Nivel {jobDetails.Level}, Exp {jobDetails.Experience}/{jobDetails.ExperienceToNextLevel}", 5);
        }
    }


}
