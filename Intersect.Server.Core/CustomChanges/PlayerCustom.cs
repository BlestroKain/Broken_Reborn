using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Intersect.Server.Entities
{
    public partial class Player : Entity
    {
        /// <summary>
        /// Calcula los puntos de experiencia modificados en función de la diferencia de niveles entre el jugador y el enemigo.
        /// </summary>
        /// <param name="enemyLevel">El nivel del enemigo.</param>
        /// <param name="exp">Los puntos de experiencia base ganados.</param>
        /// <param name="playerLevel">El nivel del jugador (opcional, con un valor predeterminado de 0).</param>
        /// <returns>Los puntos de experiencia modificados.</returns>
        public long ExpModifiedByLevel(int enemyLevel, long baseExp, int playerLevel = 0)
        {
            // Calcula la diferencia de niveles entre el jugador y el enemigo.
            int levelDiff = playerLevel == 0 ? Level - enemyLevel : playerLevel - enemyLevel;
            // Inicializa el multiplicador de experiencia como 1.0 (sin modificación).
            float expMultiplier = 1.0f;
            // Aplica modificadores basados en la diferencia de niveles.
            if (levelDiff >= 4 && levelDiff < 6)
            {
                expMultiplier = 0.8f; // Reducción del 20%
            }
            else if (levelDiff >= 6 && levelDiff < 10)
            {
                expMultiplier = 0.6f; // Reducción del 40%
            }
            else if (levelDiff >= 10)
            {
                expMultiplier = 0.2f; // Reducción del 80%
            }
            // Calcula la experiencia modificada y la convierte a long.
            long modifiedExp = (long)(baseExp * expMultiplier);
            return modifiedExp;
        }
    }
}