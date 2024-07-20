using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Intersect.Enums
{
   public enum PetState
        {
            Idle,
            Following,
            Attacking
        }
    // Comportamientos de la mascota
    public enum PetBehavior
    {
        Passive,
        Defensive,
        Aggressive
    }
    public enum PetRarity
    {
        Common,
        Rare,
        Epic,
        Legendary
    }

    public enum PetPersonality
    {
        Joyful,
        Serious,
        Playful,
        Shy,
        Brave,
        Protective,
        Energetic,
        Leader,
        Mysterious,
        Wise,
        Fierce
    }

}
