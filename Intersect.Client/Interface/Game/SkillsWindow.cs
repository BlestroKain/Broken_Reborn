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

namespace Intersect.Client.Interface.Game
{

    public class SkillsWindow
    {



        //Controls
        private WindowControl mSkillsWindow;

        private Label mSkillsName;

        Label mMiningLabel;
        Label mFarmingLabel;

        Label mFishingLabel;
        Label mWoodcutterLabel;
        Label mHunterLabel;

        Label mBlacksmithLabel;
        Label mCookingLabel;
        Label mAlchemyLabel;
        Label mTailorLabel;
        Label mJewerlyLabel;
        Label mCobblerLabel;

        //buttoms

        Button mMiningBtn;
        Button mFarmingBtn;

        Button mFishingBtn;
        Button mWoodcutterBtn;
        Button mHunterBtn;

        Button mSmithingBtn;
        Button mCookingBtn;
        Button mAlquemyBtn;
        Button mTailorBtn;
        Button mJewerlyBtn;
        Button mCobblerBtn;
        Button mRecoSkillsBtn;
        Button mCraftSkillsBtn;

        public int X;

        public int Y;


        //Init
        public SkillsWindow(Canvas gameCanvas)
        {

            mSkillsWindow = new WindowControl(gameCanvas, Strings.Skills.skill, false, "SkillsWindow");
            mSkillsWindow.DisableResizing();

            mSkillsName = new Label(mSkillsWindow, "SkillsNameLabel");
            mSkillsName.SetTextColor(Color.White, Label.ControlState.Normal);
            mSkillsWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            var RecoskillLabel = new Label(mSkillsWindow, "RecolectionSkillsLabel");
            RecoskillLabel.SetText(Strings.Skills.recoskill);
            RecoskillLabel.SetPosition(10, 10); // Set position to (10, 10)

            mFarmingLabel = new Label(mSkillsWindow, "mFarmingLabel");
            mFarmingLabel.SetPosition(10, 40);

            mMiningLabel = new Label(mSkillsWindow, "mMiningLabel");
            mMiningLabel.SetPosition(10, 70);

            mWoodcutterLabel = new Label(mSkillsWindow, "mWoodcutterLabel");
            mWoodcutterLabel.SetPosition(10, 100);

            mFishingLabel = new Label(mSkillsWindow, "mFishingLabel");
            mFishingLabel.SetPosition(10, 130);

            mHunterLabel = new Label(mSkillsWindow, "mHunterLabel");
            mHunterLabel.SetPosition(10, 160);
            var CraftskillLabel = new Label(mSkillsWindow, "CrafttingSkillsLabel");
            CraftskillLabel.SetText(Strings.Skills.craftskill);
            CraftskillLabel.SetPosition(10, 190); // Se establece la posición en (10, 280)

            mBlacksmithLabel = new Label(mSkillsWindow, "mBlacksmithLabel");
            mBlacksmithLabel.SetPosition(10, 220);

            mCookingLabel = new Label(mSkillsWindow, "mCookingLabel");
            mCookingLabel.SetPosition(10, 250);

            mAlchemyLabel = new Label(mSkillsWindow, "mAlchemyLabel");
            mAlchemyLabel.SetPosition(10, 280);

        }
        public void Update()
        {
            mFarmingLabel.SetText(Strings.Skills.skill0.ToString(Strings.Job.skill0, Globals.Me.FarmingLevel));
            mMiningLabel.SetText(Strings.Skills.skill1.ToString(Strings.Job.skill1, Globals.Me.MiningLevel));
            mWoodcutterLabel.SetText(Strings.Skills.skill2.ToString(Strings.Job.skill2, Globals.Me.WoodLevel));
            mFishingLabel.SetText(Strings.Skills.skill3.ToString(Strings.Job.skill3, Globals.Me.FishingLevel));
            mHunterLabel.SetText(Strings.Skills.skill4.ToString(Strings.Job.skill4, Globals.Me.HunterLevel));
            mBlacksmithLabel.SetText(Strings.Skills.skill5.ToString(Strings.Job.skill5, Globals.Me.BlacksmithLevel));
            mCookingLabel.SetText(Strings.Skills.skill6.ToString(Strings.Job.skill6, Globals.Me.CookingLevel));
            mAlchemyLabel.SetText(Strings.Skills.skill7.ToString(Strings.Job.skill7, Globals.Me.AlchemyLevel));

        }


        public void Show()
        {
            mSkillsWindow.IsHidden = false;
        }

        public bool IsVisible()
        {
            return !mSkillsWindow.IsHidden;
        }

        public void Hide()
        {
            mSkillsWindow.IsHidden = true;
        }


    }


}







