using System;
using Intersect.Client.Core;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Localization;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.General;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.Client.Entities;

namespace Intersect.Client.Interface.Game.Pet
{
    public partial class PetWindow
    {
        private WindowControl mPetWindow;

        private Label mPetName;
        private Label mPetLevel;

        private Label mPetGender;
        private Label mPetRarity;
        private Label mPetPersonality;

        private ImagePanel mHealthBarBackground;
        private ImagePanel mHealthBar;
        private Label mHealthLabel;

        private ImagePanel mBreedStatusBarBackground;
        private ImagePanel mBreedStatusBar;
        private Label mBreedStatusLabel;

        private ImagePanel mMoodBarBackground;
        private ImagePanel mMoodBar;
        private Label mMoodLabel;

        private ImagePanel mHungerBarBackground;
        private ImagePanel mHungerBar;
        private Label mHungerLabel;

        public PetWindow(Canvas gameCanvas)
        {
            mPetWindow = new WindowControl(gameCanvas, Strings.Pets.title, false, "PetWindow");
            mPetWindow.DisableResizing();

            mPetName = new Label(mPetWindow, "PetNameLabel");
            mPetName.SetTextColor(Color.White, Label.ControlState.Normal);

            mPetLevel = new Label(mPetWindow, "PetLevelLabel");
            mPetLevel.SetText(Strings.Pets.level);


            mPetGender = new Label(mPetWindow, "PetGenderLabel");
            mPetGender.SetText(Strings.Pets.gender);

            mPetRarity = new Label(mPetWindow, "PetRarityLabel");
            mPetRarity.SetText(Strings.Pets.rarity);

            mPetPersonality = new Label(mPetWindow, "PetPersonalityLabel");
            mPetPersonality.SetText(Strings.Pets.personality);

            mHealthBarBackground = new ImagePanel(mPetWindow, "HealthBarBackground");
            mHealthBarBackground.SetPosition(10, 100);
            mHealthBarBackground.SetSize(200, 16);

            mHealthBar = new ImagePanel(mHealthBarBackground, "HealthBar");
            mHealthBar.SetPosition(1, 1);
            mHealthBar.SetSize(198, 14);

            mHealthLabel = new Label(mPetWindow, "HealthLabel");
            mHealthLabel.SetText(Strings.Pets.health);
            mHealthLabel.SetPosition(10, 80);

            mBreedStatusBarBackground = new ImagePanel(mPetWindow, "BreedStatusBarBackground");
            mBreedStatusBarBackground.SetPosition(10, 150);
            mBreedStatusBarBackground.SetSize(200, 16);

            mBreedStatusBar = new ImagePanel(mBreedStatusBarBackground, "BreedStatusBar");
            mBreedStatusBar.SetPosition(1, 1);
            mBreedStatusBar.SetSize(198, 14);

            mBreedStatusLabel = new Label(mPetWindow, "BreedStatusLabel");
            mBreedStatusLabel.SetText(Strings.Pets.breedStatus);
            mBreedStatusLabel.SetPosition(10, 130);

            mMoodBarBackground = new ImagePanel(mPetWindow, "MoodBarBackground");
            mMoodBarBackground.SetPosition(10, 200);
            mMoodBarBackground.SetSize(200, 16);

            mMoodBar = new ImagePanel(mMoodBarBackground, "MoodBar");
            mMoodBar.SetPosition(1, 1);
            mMoodBar.SetSize(198, 14);

            mMoodLabel = new Label(mPetWindow, "MoodLabel");
            mMoodLabel.SetText(Strings.Pets.mood);
            mMoodLabel.SetPosition(10, 180);

            mHungerBarBackground = new ImagePanel(mPetWindow, "HungerBarBackground");
            mHungerBarBackground.SetPosition(10, 250);
            mHungerBarBackground.SetSize(200, 16);

            mHungerBar = new ImagePanel(mHungerBarBackground, "HungerBar");
            mHungerBar.SetPosition(1, 1);
            mHungerBar.SetSize(198, 14);

            mHungerLabel = new Label(mPetWindow, "HungerLabel");
            mHungerLabel.SetText(Strings.Pets.hunger);
            mHungerLabel.SetPosition(10, 230);

            mPetWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        public void Update()
        {
            if (mPetWindow.IsHidden)
            {
                return;
            }

            var pet = Globals.Me.Pet; // Assuming MyPet contains the current pet details
            if (pet == null)
            {
                return;
            }

            mPetName.SetText(pet.Name);
            mPetLevel.SetText(Strings.Pets.level.ToString(pet.Level));
            mPetGender.SetText(pet.Gender.ToString());
            mPetRarity.SetText(pet.Rarity.ToString());
            mPetPersonality.SetText(pet.Personality.ToString());

            UpdateBar(mHealthBar, pet.Health, pet.MaxHealth);
            UpdateBar(mBreedStatusBar, pet.BreedStatus, 100); // Assuming breed status is percentage based
            UpdateBar(mMoodBar, pet.Mood, 100); // Assuming mood is percentage based
            UpdateBar(mHungerBar, pet.Hunger, 100); // Assuming hunger is percentage based
        }

        private void UpdateBar(ImagePanel bar, int currentValue, int maxValue)
        {
            var width = (float)currentValue / maxValue * 198;
            bar.SetSize((int)width, 14);
            bar.SetTextureRect(0, 0, (int)width, 14);
        }

        public void Show()
        {
            mPetWindow.IsHidden = false;
        }

        public bool IsVisible()
        {
            return !mPetWindow.IsHidden;
        }

        public void Hide()
        {
            mPetWindow.IsHidden = true;
        }
    }
}
