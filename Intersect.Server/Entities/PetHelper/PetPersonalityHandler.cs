using System;
using Intersect.Enums;
using Intersect.Server.Entities;

namespace Intersect.Core.Pets
{
    public class PetPersonalityHandler
    {
        private Pet Pet { get; }

        public PetPersonalityHandler(Pet pet)
        {
            Pet = pet;
        }

        public void OnTick()
        {
            switch (Pet.Personality)
            {
                case PetPersonality.Joyful:
                    HandleJoyfulPersonality();
                    break;
                case PetPersonality.Serious:
                    HandleSeriousPersonality();
                    break;
                case PetPersonality.Playful:
                    HandlePlayfulPersonality();
                    break;
                case PetPersonality.Shy:
                    HandleShyPersonality();
                    break;
                case PetPersonality.Brave:
                    HandleBravePersonality();
                    break;
                case PetPersonality.Protective:
                    HandleProtectivePersonality();
                    break;
                case PetPersonality.Energetic:
                    HandleEnergeticPersonality();
                    break;
                case PetPersonality.Leader:
                    HandleLeaderPersonality();
                    break;
                case PetPersonality.Mysterious:
                    HandleMysteriousPersonality();
                    break;
                case PetPersonality.Wise:
                    HandleWisePersonality();
                    break;
                case PetPersonality.Fierce:
                    HandleFiercePersonality();
                    break;
                default:
                    HandleJoyfulPersonality();
                    break;
            }
        }

        private void HandleJoyfulPersonality()
        {
            // Lógica específica para la personalidad alegre
        }

        private void HandleSeriousPersonality()
        {
            // Lógica específica para la personalidad seria
        }

        private void HandlePlayfulPersonality()
        {
            // Lógica específica para la personalidad juguetona
        }

        private void HandleShyPersonality()
        {
            // Lógica específica para la personalidad tímida
        }

        private void HandleBravePersonality()
        {
            // Lógica específica para la personalidad valiente
        }

        private void HandleProtectivePersonality()
        {
            // Lógica específica para la personalidad protectora
        }

        private void HandleEnergeticPersonality()
        {
            // Lógica específica para la personalidad energética
        }

        private void HandleLeaderPersonality()
        {
            // Lógica específica para la personalidad líder
        }

        private void HandleMysteriousPersonality()
        {
            // Lógica específica para la personalidad misteriosa
        }

        private void HandleWisePersonality()
        {
            // Lógica específica para la personalidad sabia
        }

        private void HandleFiercePersonality()
        {
            // Lógica específica para la personalidad feroz
        }
    }

}
