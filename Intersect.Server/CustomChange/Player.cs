using Intersect.Enums;
using Intersect.Server.Localization;
using Intersect.Server.Networking;
using System;
using System.Collections.Generic;

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

        //////JOBS//////////
        ///
        #region Set Job Level
        public void SetFarmingLevel(int Farminglevel, bool resetExperience = false)
        {
            FarmingLevel = Math.Min(Options.MaxLevel, Farminglevel);
            if (resetExperience)
            {
                FarmingExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendFarmingExperience(this);
        }
        public void SetMiningLevel(int Mininglevel, bool resetExperience = false)
        {
            if (Mininglevel < 1)
            {
                return;
            }

            MiningLevel = Math.Min(Options.MaxLevel, Mininglevel);
            if (resetExperience)
            {
                MiningExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendMiningExperience(this);
        }
        public void SetFishingLevel(int Fishinglevel, bool resetExperience = false)
        {
            if (Fishinglevel < 1)
            {
                return;
            }

            FishingLevel = Math.Min(Options.MaxLevel, Fishinglevel);
            if (resetExperience)
            {
                FishingExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendFishingExperience(this);
        }
        public void SetWoodLevel(int Woodlevel, bool resetExperience = false)
        {
            if (Woodlevel < 1)
            {
                return;
            }

            WoodLevel = Math.Min(Options.MaxLevel, Woodlevel);
            if (resetExperience)
            {
                WoodExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendWoodExperience(this);
        }
        public void SetCookingLevel(int Cookinglevel, bool resetExperience = false)
        {
            if (Cookinglevel < 1)
            {
                return;
            }

            CookingLevel = Math.Min(Options.MaxLevel, Cookinglevel);
            if (resetExperience)
            {
                CookingExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendCookingExperience(this);
        }
        public void SetCraftingLevel(int Craftinglevel, bool resetExperience = false)
        {
            if (Craftinglevel < 1)
            {
                return;
            }

            CraftingLevel = Math.Min(Options.MaxLevel, Craftinglevel);
            if (resetExperience)
            {
                CraftingExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendCraftingExperience(this);
        }
        public void SetAlchemyLevel(int Alchemylevel, bool resetExperience = false)
        {
            if (Alchemylevel < 1)
            {
                return;
            }

            AlchemyLevel = Math.Min(Options.MaxLevel, Alchemylevel);
            if (resetExperience)
            {
                AlchemyExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendAlchemyExperience(this);
        }
        public void SetBlacksmithLevel(int Blacksmithlevel, bool resetExperience = false)
        {
            if (Blacksmithlevel < 1)
            {
                return;
            }

            BlacksmithLevel = Math.Min(Options.MaxLevel, Blacksmithlevel);
            if (resetExperience)
            {
                BlacksmithExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendBlacksmithExperience(this);
        }
        public void SetHuntingLevel(int Huntinglevel, bool resetExperience = false)
        {
            if (Huntinglevel < 1)
            {
                return;
            }

            HuntingLevel = Math.Min(Options.MaxLevel, Huntinglevel);
            if (resetExperience)
            {
                HuntingExp = 0;
            }

            PacketSender.SendEntityDataToProximity(this);
            PacketSender.SendHuntingExperience(this);
        }
        #endregion
        #region JobLevelUP
        public void FarmingLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (FarmingLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetFarmingLevel(FarmingLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Farminglevelup.ToString(FarmingLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.FarmingLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendFarmingExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void MiningLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (MiningLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetMiningLevel(MiningLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Mininglevelup.ToString(MiningLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.MiningLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendMiningExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void FishingLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (FishingLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetFishingLevel(FishingLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Fishinglevelup.ToString(FishingLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.FishingLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendFishingExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void WoodLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (WoodLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetWoodLevel(WoodLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Woodlevelup.ToString(WoodLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.WoodcuttingLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendWoodExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void HuntingLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (HuntingLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetHuntingLevel(HuntingLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Huntinglevelup.ToString(HuntingLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.HuntingLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendHuntingExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void AlchemyLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (AlchemyLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetAlchemyLevel(AlchemyLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Alchemylevelup.ToString(AlchemyLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.AlchemyLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendAlchemyExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void BlacksmithLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (BlacksmithLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetBlacksmithLevel(BlacksmithLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Blacksmithlevelup.ToString(BlacksmithLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.BlacksmithingLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendBlacksmithExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void CookingLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (CookingLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetCookingLevel(CookingLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Cookinglevelup.ToString(CookingLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.CookingLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendCookingExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        public void CraftingLevelUp(bool resetExperience = true, int levels = 1)
        {
            var messages = new List<string>();
            if (CraftingLevel < Options.MaxLevel)
            {
                for (var i = 0; i < levels; i++)
                {
                    SetCraftingLevel(CraftingLevel + 1, resetExperience);
                }
            }
            PacketSender.SendChatMsg(this, Strings.Player.Craftinglevelup.ToString(CraftingLevel), ChatMessageType.Experience, CustomColors.Combat.SkillLevelUp, Name);

            PacketSender.SendActionMsg(this, Strings.Combat.CraftingLevelUp, CustomColors.Combat.LevelUp);
            foreach (var message in messages)
            {
                PacketSender.SendChatMsg(this, message, ChatMessageType.Experience, CustomColors.Alerts.Info, Name);
            }
            PacketSender.SendCraftingExperience(this);
            PacketSender.SendEntityDataToProximity(this);

        }
        #endregion

        #region GiveJobExperience
        public void GiveFarmingExperience(long amount)
        {
            FarmingExp += (int)(amount);
            if (FarmingExp < 0)
            {
                FarmingExp = 0;
            }

            if (!CheckFarmingLevelUp())
            {
                PacketSender.SendFarmingExperience(this);
            }
        }
        public void GiveMiningExperience(long amount)
        {
            MiningExp += (int)(amount);
            if (MiningExp < 0)
            {
                MiningExp = 0;
            }

            if (!CheckMiningLevelUp())
            {
                PacketSender.SendMiningExperience(this);
            }
        }
        public void GiveFishingExperience(long amount)
        {
            FishingExp += (int)(amount);
            if (FishingExp < 0)
            {
                FishingExp = 0;
            }

            if (!CheckFishingLevelUp())
            {
                PacketSender.SendFishingExperience(this);
            }
        }
        public void GiveWoodExperience(long amount)
        {
            WoodExp += (int)(amount);
            if (WoodExp < 0)
            {
                WoodExp = 0;
            }

            if (!CheckWoodLevelUp())
            {
                PacketSender.SendWoodExperience(this);
            }
        }
        public void GiveHuntingExperience(long amount)
        {
            HuntingExp += (int)(amount);
            if (HuntingExp < 0)
            {
                HuntingExp = 0;
            }

            if (!CheckHuntingLevelUp())
            {
                PacketSender.SendHuntingExperience(this);
            }
        }
        public void GiveBlacksmithExperience(long amount)
        {
            BlacksmithExp += (int)(amount);
            if (BlacksmithExp < 0)
            {
                BlacksmithExp = 0;
            }

            if (!CheckBlacksmithLevelUp())
            {
                PacketSender.SendBlacksmithExperience(this);
            }
        }
        public void GiveCookingExperience(long amount)
        {
            CookingExp += (int)(amount);
            if (CookingExp < 0)
            {
                CookingExp = 0;
            }

            if (!CheckCookingLevelUp())
            {
                PacketSender.SendCookingExperience(this);
            }
        }
        public void GiveCraftingExperience(long amount)
        {
            CraftingExp += (int)(amount);
            if (CraftingExp < 0)
            {
                CraftingExp = 0;
            }

            if (!CheckCraftingLevelUp())
            {
                PacketSender.SendCraftingExperience(this);
            }
        }
        public void GiveAlchemyExperience(long amount)
        {
            AlchemyExp += (int)(amount);
            if (AlchemyExp < 0)
            {
                AlchemyExp = 0;
            }

            if (!CheckAlchemyLevelUp())
            {
                PacketSender.SendAlchemyExperience(this);
            }
        }
        #endregion

        #region CheckJobLevelUp 
        private bool CheckFarmingLevelUp()
        {
            var levelCount = 0;
            while (FarmingExp >= GetExperienceToFarmingNextLevel(FarmingLevel + levelCount) &&
                   GetExperienceToFarmingNextLevel(FarmingLevel + levelCount) > 0)
            {
                FarmingExp -= GetExperienceToFarmingNextLevel(FarmingLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            FarmingLevelUp(false, levelCount);

            return true;
        }
        private bool CheckMiningLevelUp()
        {
            var levelCount = 0;
            while (MiningExp >= GetExperienceToMiningNextLevel(MiningLevel + levelCount) &&
                   GetExperienceToMiningNextLevel(MiningLevel + levelCount) > 0)
            {
                MiningExp -= GetExperienceToMiningNextLevel(MiningLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            MiningLevelUp(false, levelCount);

            return true;
        }
        private bool CheckFishingLevelUp()
        {
            var levelCount = 0;
            while (FishingExp >= GetExperienceToFishingNextLevel(FishingLevel + levelCount) &&
                   GetExperienceToFishingNextLevel(FishingLevel + levelCount) > 0)
            {
                FishingExp -= GetExperienceToFishingNextLevel(FishingLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            FishingLevelUp(false, levelCount);

            return true;
        }
        private bool CheckWoodLevelUp()
        {
            var levelCount = 0;
            while (WoodExp >= GetExperienceToWoodNextLevel(WoodLevel + levelCount) &&
                   GetExperienceToWoodNextLevel(WoodLevel + levelCount) > 0)
            {
                WoodExp -= GetExperienceToWoodNextLevel(WoodLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            WoodLevelUp(false, levelCount);

            return true;
        }
        private bool CheckAlchemyLevelUp()
        {
            var levelCount = 0;
            while (AlchemyExp >= GetExperienceToAlchemyNextLevel(AlchemyLevel + levelCount) &&
                   GetExperienceToAlchemyNextLevel(AlchemyLevel + levelCount) > 0)
            {
                AlchemyExp -= GetExperienceToAlchemyNextLevel(AlchemyLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            AlchemyLevelUp(false, levelCount);

            return true;
        }
        private bool CheckCookingLevelUp()
        {
            var levelCount = 0;
            while (CookingExp >= GetExperienceToCookingNextLevel(CookingLevel + levelCount) &&
                   GetExperienceToCookingNextLevel(CookingLevel + levelCount) > 0)
            {
                CookingExp -= GetExperienceToCookingNextLevel(CookingLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            CookingLevelUp(false, levelCount);

            return true;
        }
        private bool CheckCraftingLevelUp()
        {
            var levelCount = 0;
            while (CraftingExp >= GetExperienceToCraftingNextLevel(CraftingLevel + levelCount) &&
                   GetExperienceToCraftingNextLevel(CraftingLevel + levelCount) > 0)
            {
                CraftingExp -= GetExperienceToCraftingNextLevel(CraftingLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            CraftingLevelUp(false, levelCount);

            return true;
        }
        private bool CheckBlacksmithLevelUp()
        {
            var levelCount = 0;
            while (BlacksmithExp >= GetExperienceToBlacksmithNextLevel(BlacksmithLevel + levelCount) &&
                   GetExperienceToBlacksmithNextLevel(BlacksmithLevel + levelCount) > 0)
            {
                BlacksmithExp -= GetExperienceToBlacksmithNextLevel(BlacksmithLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            BlacksmithLevelUp(false, levelCount);

            return true;
        }
        private bool CheckHuntingLevelUp()
        {
            var levelCount = 0;
            while (HuntingExp >= GetExperienceToHuntingNextLevel(HuntingLevel + levelCount) &&
                   GetExperienceToHuntingNextLevel(HuntingLevel + levelCount) > 0)
            {
                HuntingExp -= GetExperienceToHuntingNextLevel(HuntingLevel + levelCount);
                levelCount++;
            }

            if (levelCount <= 0)
            {
                return false;
            }

            HuntingLevelUp(false, levelCount);

            return true;
        }

        #endregion

        #region Get Exp To Job Next Level
        private long GetExperienceToFarmingNextLevel(int FarmingLevel)
        {
            if (FarmingLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseFarmingExp;
            var Gain = Options.GainBaseExponent;
            var level = FarmingLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToMiningNextLevel(int MiningLevel)
        {
            if (MiningLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseMiningExp;
            var Gain = Options.GainBaseExponent;
            var level = MiningLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToFishingNextLevel(int FishingLevel)
        {
            if (FishingLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseFishingExp;
            var Gain = Options.GainBaseExponent;
            var level = FishingLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToWoodNextLevel(int WoodLevel)
        {
            if (WoodLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseLumberjackExp;
            var Gain = Options.GainBaseExponent;
            var level = WoodLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToCookingNextLevel(int CookingLevel)
        {
            if (CookingLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseCookingExp;
            var Gain = Options.GainBaseExponent;
            var level = CookingLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToCraftingNextLevel(int CraftingLevel)
        {
            if (CraftingLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseCraftingExp;
            var Gain = Options.GainBaseExponent;
            var level = CraftingLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToHuntingNextLevel(int HuntingLevel)
        {
            if (HuntingLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseHuntingExp;
            var Gain = Options.GainBaseExponent;
            var level = HuntingLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToBlacksmithNextLevel(int BlacksmithLevel)
        {
            if (BlacksmithLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseBlacksmithExp;
            var Gain = Options.GainBaseExponent;
            var level = BlacksmithLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }
        private long GetExperienceToAlchemyNextLevel(int AlchemyLevel)
        {
            if (AlchemyLevel >= Options.MaxJobLevel)
            {
                return -1;
            }

            var skillBase = Options.BaseAlchemyExp;
            var Gain = Options.GainBaseExponent;
            var level = AlchemyLevel;


            return (long)Math.Floor(skillBase * (((Math.Pow(level, Gain)))));
        }

        #endregion
    }
}