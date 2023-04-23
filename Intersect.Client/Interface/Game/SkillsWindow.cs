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
using Intersect.Configuration;
using Intersect.Client.Entities;
using Intersect.Utilities;
using System.Drawing;
using Graphics = Intersect.Client.Core.Graphics;
using static Intersect.Client.Localization.Strings;

namespace Intersect.Client.Interface.Game
{

    public class SkillsWindow
    {

        //Controls
        private WindowControl mSkillsWindow;

        private Label mSkillsName;
        public ImagePanel EntityInfoPanel;
        public float CurExpWidth;

        //infopanel 
        Label JobNameLabel;
        public ImagePanel ExpBackground;
        public ImagePanel JobIcon;
        public ImagePanel ExpBar;
        Label ExpLabel;
        Label ExpTitle;
        RichLabel JobDescriptionLabel;
        Label JobLevelLabel;

        //Faming 
        public Button FarmingContainer;
        Label mFarmingLabel;
        public ImagePanel ExpFarmingBackground;
        public ImagePanel FarmingIcon;
        public ImagePanel ExpFarmingBar;
        Label ExpFarmingLbl;
        Label ExpFarmingTitle;

        // Mining
        public Button MiningContainer;
        Label mMiningLabel;
        public ImagePanel ExpMiningBackground;
        public ImagePanel MiningIcon;
        public ImagePanel ExpMiningBar;
        Label ExpMiningLbl;
        Label ExpMiningTitle;

        // Fishing
        public Button FishingContainer;
        Label mFishingLabel;
        public ImagePanel ExpFishingBackground;
        public ImagePanel FishingIcon;
        public ImagePanel ExpFishingBar;
        Label ExpFishingLbl;
        Label ExpFishingTitle;

        // Lumberjack
        public Button LumberjackContainer;
        Label mLumberjackLabel;
        public ImagePanel ExpLumberjackBackground;
        public ImagePanel LumberjackIcon;
        public ImagePanel ExpLumberjackBar;
        Label ExpLumberjackLbl;
        Label ExpLumberjackTitle;

        // Hunter
        public Button HuntingContainer;
        Label mHuntingLabel;
        public ImagePanel ExpHuntingBackground;
        public ImagePanel HuntingIcon;
        public ImagePanel ExpHuntingBar;
        Label ExpHuntingLbl;
        Label ExpHuntingTitle;

        // Blacksmith
        public Button BlacksmithContainer;
        Label mBlacksmithLabel;
        public ImagePanel ExpBlacksmithBackground;
        public ImagePanel BlacksmithIcon;
        public ImagePanel ExpBlacksmithBar;
        Label ExpBlacksmithLbl;
        Label ExpBlacksmithTitle;

        // Cooking
        public Button CookingContainer;
        Label mCookingLabel;
        public ImagePanel ExpCookingBackground;
        public ImagePanel CookingIcon;
        public ImagePanel ExpCookingBar;
        Label ExpCookingLbl;
        Label ExpCookingTitle;

        // Alchemy
        public Button AlchemyContainer;
        Label mAlchemyLabel;
        public ImagePanel ExpAlchemyBackground;
        public ImagePanel AlchemyIcon;
        public ImagePanel ExpAlchemyBar;
        Label ExpAlchemyLbl;
        Label ExpAlchemyTitle;

        //paneles
        public ImagePanel InfoPanel;
        public ImagePanel JobsPanel;
        //Experience Labels
        Label mFarmingExpLabel;
        Label mMiningExpLabel;
        Label mLumberjackExpLabel;
        Label mFishingExpLabel;
        Label mHunterExpLabel;
        Label mBlacksmithExpLabel;
        Label mCookingExpLabel;
        Label mAlchemyExpLabel;

        private Label mJobtDescTemplateLabel;
        public int X;

        public int Y;

        public float CurExpSize = -1;

        private long mLastUpdateTime;
        public float xtnl;

        public float Jobexp;

        //Init
        public SkillsWindow(Canvas gameCanvas)
        {


            mSkillsWindow = new WindowControl(gameCanvas, Strings.Skills.skill, false, "SkillsWindow");
            mSkillsWindow.DisableResizing();
            mSkillsWindow.SetSize(420, 400);

            mSkillsName = new Label(mSkillsWindow, "SkillsNameLabel");
            mSkillsName.SetTextColor(Color.White, Label.ControlState.Normal);

            /// <summary>
            /// This code creates an ImagePanel, Label, and ImagePanel objects for a Job Info Panel. It sets the size, position, text, and size of the objects. 
            /// </summary>
            #region InfoPanel
            InfoPanel = new ImagePanel(mSkillsWindow, "InfoPanel");
            InfoPanel.SetSize(250, 450);
            InfoPanel.SetPosition(205, 5);
            // Job Icon
            JobIcon = new ImagePanel(InfoPanel, "JobIcon");
            JobIcon.SetPosition(0, 0);
            JobIcon.SetSize(35, 35);

            // Job name and level
            JobNameLabel = new Label(InfoPanel, "JobNameLabel");
            JobNameLabel.SetPosition(40, 0);
           
          
            JobLevelLabel = new Label(InfoPanel, "JobLevelLabel");
            JobLevelLabel.SetPosition(160, 0);

            // Job description
            JobDescriptionLabel = new RichLabel(InfoPanel,"Jobdesc");
            JobDescriptionLabel.SetPosition(5, 50);
            JobDescriptionLabel.SetSize(200, 100);
            JobDescriptionLabel.ClearText();

            mJobtDescTemplateLabel = new Label(InfoPanel, "JobDescriptionTemplate");

            // Exp label
            ExpTitle = new Label(InfoPanel, "ExpTitle");
            ExpTitle.SetText(Strings.EntityBox.exp);
            ExpTitle.SetPosition(40, 20);

            // Exp bar
            ExpBackground = new ImagePanel(InfoPanel, "ExpBackground");
            ExpBackground.SetPosition(70, 20);
            ExpBackground.SetSize(110, 16);
            ExpBackground.IsHidden = true;

            ExpBar = new ImagePanel(InfoPanel, "ExpBar");
            ExpBar.SetPosition(71, 21);
            ExpBar.SetSize(100, 12);
            ExpBar.IsHidden = true;
            // Exp value
            ExpLabel = new Label(InfoPanel, "ExpLabel");
            ExpLabel.SetPosition(70, 20);
            ExpLabel.SetSize(180, 16);
            // Agregar el contenedor al conjunto de elementos hijos del mSkillsWindow
            mSkillsWindow.AddChild(InfoPanel);
            #endregion
            // Crear el panel de oficios
            JobsPanel = new ImagePanel(mSkillsWindow, "JobsPanel");
            JobsPanel.SetSize(200, 450);
            JobsPanel.SetPosition(5, 5);

            var jobContainerHeight = 40;
            var jobContainerWidth = 195;
            var jobIconSize = 40;

            #region Farming
            // Crear el contenedor para la habilidad de farming
            FarmingContainer = new Button(JobsPanel, "FarmingContainer");
            FarmingContainer.SetPosition(5, jobContainerHeight);
            FarmingContainer.SetSize(jobContainerWidth, jobContainerHeight);
            FarmingContainer.Clicked += FarmingBtn_Clicked;

            // Job Icon
            FarmingIcon = new ImagePanel(FarmingContainer, "FarmingIcon");
            FarmingIcon.SetPosition(0, 0);
            FarmingIcon.SetSize(jobIconSize, jobIconSize);

            // Job name and level
            mFarmingLabel = new Label(FarmingContainer, "FarmingNameLbl");
            mFarmingLabel.SetPosition(jobIconSize + 5, 0);

            // Exp label
            ExpFarmingTitle = new Label(FarmingContainer, "EXPFarmingTitle");
            ExpFarmingTitle.SetText(Strings.EntityBox.exp);
            ExpFarmingTitle.SetPosition(jobIconSize + 5, 20);

            // Exp bar
            ExpFarmingBackground = new ImagePanel(FarmingContainer, "EXPFarmingBackground");
            ExpFarmingBackground.SetPosition(jobIconSize + 35, 20);
            ExpFarmingBackground.SetSize(70, 16);

            ExpFarmingBar = new ImagePanel(FarmingContainer, "EXPFarmingBar");
            ExpFarmingBar.SetPosition(jobIconSize + 36, 21);
            ExpFarmingBar.SetSize(68, 14);

            // Exp value
            ExpFarmingLbl = new Label(FarmingContainer, "EXPFarmingLabel");
            ExpFarmingLbl.SetPosition(jobIconSize + 110, 20);
            ExpFarmingLbl.SetSize(40, 16);


            // Agregar el contenedor al conjunto de elementos hijos del JobsPanel
            JobsPanel.AddChild(FarmingContainer); 
            #endregion

            //////////////////////////////MINING/////////////////////////////////////////////////
            // Crear el contenedor para la habilidad de minería
            #region Mining
            MiningContainer = new Button(JobsPanel, "MiningContainer");
            MiningContainer.SetPosition(5, jobContainerHeight * 2);
            MiningContainer.SetSize(jobContainerWidth, jobContainerHeight);
            MiningContainer.Clicked += MiningBtn_Clicked;

            // Job Icon
            MiningIcon = new ImagePanel(MiningContainer, "MiningIcon");
            MiningIcon.SetPosition(0, 0);
            MiningIcon.SetSize(35, 35);

            // Job name and level
            mMiningLabel = new Label(MiningContainer, "MiningNameLbl");
            mMiningLabel.SetPosition(40, 0);

            // Exp label
            ExpMiningTitle = new Label(MiningContainer, "EXPMiningTitle");
            ExpMiningTitle.SetText(Strings.EntityBox.exp);
            ExpMiningTitle.SetPosition(40, 20);

            // Exp bar
            ExpMiningBackground = new ImagePanel(MiningContainer, "EXPMiningBackground");
            ExpMiningBackground.SetPosition(70, 20);
            ExpMiningBackground.SetSize(110, 16);

            ExpMiningBar = new ImagePanel(MiningContainer, "EXPMiningBar");
            ExpMiningBar.SetPosition(71, 21);
            ExpMiningBar.SetSize(100, 12);

            // Exp value
            ExpMiningLbl = new Label(MiningContainer, "EXPMiningLabel");
            ExpMiningLbl.SetPosition(70, 20);
            ExpMiningLbl.SetSize(180, 16);

            JobsPanel.AddChild(MiningContainer);

            // Agregar el contenedor al conjunto de elementos hijos del mSkillsWindow
            mSkillsWindow.AddChild(JobsPanel); 
            #endregion
            //////////////////////////////Lumberjack/////////////////////////////////////////////////

            #region Lumberjack
            LumberjackContainer = new Button(JobsPanel, "LumberjackContainer");
            LumberjackContainer.SetPosition(5, jobContainerHeight * 3);
            LumberjackContainer.SetSize(jobContainerWidth, jobContainerHeight);
            LumberjackContainer.Clicked += LumberjackBtn_Clicked;

            // Job Icon
            LumberjackIcon = new ImagePanel(LumberjackContainer, "LumberjackIcon");
            LumberjackIcon.SetPosition(0, 0);
            LumberjackIcon.SetSize(35, 35);

            // Job name and level
            mLumberjackLabel = new Label(LumberjackContainer, "LumberjackNameLbl");
            mLumberjackLabel.SetPosition(40, 0);

            // Exp label
            ExpLumberjackTitle = new Label(LumberjackContainer, "EXPLumberjackTitle");
            ExpLumberjackTitle.SetText(Strings.EntityBox.exp);
            ExpLumberjackTitle.SetPosition(40, 20);

            // Exp bar
            ExpLumberjackBackground = new ImagePanel(LumberjackContainer, "EXPLumberjackBackground");
            ExpLumberjackBackground.SetPosition(70, 20);
            ExpLumberjackBackground.SetSize(110, 16);

            ExpLumberjackBar = new ImagePanel(LumberjackContainer, "EXPLumberjackBar");
            ExpLumberjackBar.SetPosition(71, 21);
            ExpLumberjackBar.SetSize(100, 12);

            // Exp value
            ExpLumberjackLbl = new Label(LumberjackContainer, "EXPLumberjackLabel");
            ExpLumberjackLbl.SetPosition(70, 20);
            ExpLumberjackLbl.SetSize(180, 16); 
            #endregion

            //////////////////////////////FISHING/////////////////////////////////////////////////
            #region Fishing

            // Crear el contenedor para la habilidad de minería
            FishingContainer = new Button(JobsPanel, "FishingContainer");
            FishingContainer.SetPosition(5, jobContainerHeight * 4);
            FishingContainer.SetSize(jobContainerWidth, jobContainerHeight);
            FishingContainer.Clicked += FishingBtn_Clicked;

            // Job Icon
            FishingIcon = new ImagePanel(FishingContainer, "FishingIcon");
            FishingIcon.SetPosition(0, 0);
            FishingIcon.SetSize(35, 35);

            // Job name and level
            mFishingLabel = new Label(FishingContainer, "FishingNameLbl");
            mFishingLabel.SetPosition(40, 0);

            // Exp label
            ExpFishingTitle = new Label(FishingContainer, "EXPFishingTitle");
            ExpFishingTitle.SetText(Strings.EntityBox.exp);
            ExpFishingTitle.SetPosition(40, 20);

            // Exp bar
            ExpFishingBackground = new ImagePanel(FishingContainer, "EXPFishingBackground");
            ExpFishingBackground.SetPosition(70, 20);
            ExpFishingBackground.SetSize(110, 16);

            ExpFishingBar = new ImagePanel(FishingContainer, "EXPFishingBar");
            ExpFishingBar.SetPosition(71, 21);
            ExpFishingBar.SetSize(100, 12);

            // Exp value
            ExpFishingLbl = new Label(FishingContainer, "EXPFishingLabel");
            ExpFishingLbl.SetPosition(70, 20);
            ExpFishingLbl.SetSize(180, 16);

            JobsPanel.AddChild(FishingContainer);
            #endregion
            //////////////////////////////Hunter/////////////////////////////////////////////////
            #region Hunting
            HuntingContainer = new Button(JobsPanel, "HuntingContainer");
            HuntingContainer.SetPosition(5, jobContainerHeight * 5);
            HuntingContainer.SetSize(jobContainerWidth, jobContainerHeight);
            HuntingContainer.Clicked += HuntingBtn_Clicked;

            // Job Icon
            HuntingIcon = new ImagePanel(HuntingContainer, "HuntingIcon");
            HuntingIcon.SetPosition(0, 0);
            HuntingIcon.SetSize(35, 35);

            // Job name and level
            mHuntingLabel = new Label(HuntingContainer, "HuntingNameLbl");
            mHuntingLabel.SetPosition(40, 0);

            // Exp label
            ExpHuntingTitle = new Label(HuntingContainer, "EXPHuntingTitle");
            ExpHuntingTitle.SetText(Strings.EntityBox.exp);
            ExpHuntingTitle.SetPosition(40, 20);

            // Exp bar
            ExpHuntingBackground = new ImagePanel(HuntingContainer, "EXPHuntingBackground");
            ExpHuntingBackground.SetPosition(70, 20);
            ExpHuntingBackground.SetSize(110, 16);

            ExpHuntingBar = new ImagePanel(HuntingContainer, "EXPHuntingBar");
            ExpHuntingBar.SetPosition(71, 21);
            ExpHuntingBar.SetSize(100, 12);

            // Exp value
            ExpHuntingLbl = new Label(HuntingContainer, "EXPHuntingLabel");
            ExpHuntingLbl.SetPosition(70, 20);
            ExpHuntingLbl.SetSize(180, 16);

            JobsPanel.AddChild(HuntingContainer);
            #endregion

            //////////////////////////////Blacksmith/////////////////////////////////////////////////
            #region Blacksmith
            BlacksmithContainer = new Button(JobsPanel, "BlacksmithContainer");
            BlacksmithContainer.SetPosition(5, jobContainerHeight * 6);
            BlacksmithContainer.SetSize(jobContainerWidth, jobContainerHeight);
            BlacksmithContainer.Clicked += BlacksmithBtn_Clicked;

            // Job Icon
            BlacksmithIcon = new ImagePanel(BlacksmithContainer, "BlacksmithIcon");
            BlacksmithIcon.SetPosition(0, 0);
            BlacksmithIcon.SetSize(35, 35);

            // Job name and level
            mBlacksmithLabel = new Label(BlacksmithContainer, "BlacksmithNameLbl");
            mBlacksmithLabel.SetPosition(40, 0);

            // Exp label
            ExpBlacksmithTitle = new Label(BlacksmithContainer, "EXPBlacksmithTitle");
            ExpBlacksmithTitle.SetText(Strings.EntityBox.exp);
            ExpBlacksmithTitle.SetPosition(40, 20);

            // Exp bar
            ExpBlacksmithBackground = new ImagePanel(BlacksmithContainer, "EXPBlacksmithBackground");
            ExpBlacksmithBackground.SetPosition(70, 20);
            ExpBlacksmithBackground.SetSize(110, 16);

            ExpBlacksmithBar = new ImagePanel(BlacksmithContainer, "EXPBlacksmithBar");
            ExpBlacksmithBar.SetPosition(71, 21);
            ExpBlacksmithBar.SetSize(100, 12);

            // Exp value
            ExpBlacksmithLbl = new Label(BlacksmithContainer, "EXPBlacksmithLabel");
            ExpBlacksmithLbl.SetPosition(70, 20);
            ExpBlacksmithLbl.SetSize(180, 16);

            JobsPanel.AddChild(BlacksmithContainer); 
            #endregion
            #region Cooking
            CookingContainer = new Button(JobsPanel, "CookingContainer");
            CookingContainer.SetPosition(5, jobContainerHeight * 7);
            CookingContainer.SetSize(jobContainerWidth, jobContainerHeight);
            CookingContainer.Clicked += CookingBtn_Clicked;

            // Job Icon
            CookingIcon = new ImagePanel(CookingContainer, "CookingIcon");
            CookingIcon.SetPosition(0, 0);
            CookingIcon.SetSize(35, 35);

            // Job name and level
            mCookingLabel = new Label(CookingContainer, "CookingNameLbl");
            mCookingLabel.SetPosition(40, 0);

            // Exp label
            ExpCookingTitle = new Label(CookingContainer, "EXPCookingTitle");
            ExpCookingTitle.SetText(Strings.EntityBox.exp);
            ExpCookingTitle.SetPosition(40, 20);

            // Exp bar
            ExpCookingBackground = new ImagePanel(CookingContainer, "EXPCookingBackground");
            ExpCookingBackground.SetPosition(70, 20);
            ExpCookingBackground.SetSize(110, 16);

            ExpCookingBar = new ImagePanel(CookingContainer, "EXPCookingBar");
            ExpCookingBar.SetPosition(71, 21);
            ExpCookingBar.SetSize(100, 12);

            // Exp value
            ExpCookingLbl = new Label(CookingContainer, "EXPCookingLabel");
            ExpCookingLbl.SetPosition(70, 20);
            ExpCookingLbl.SetSize(180, 16);

            JobsPanel.AddChild(CookingContainer);
            #endregion
            #region Alchemy
            AlchemyContainer = new Button(JobsPanel, "AlchemyContainer");
            AlchemyContainer.SetPosition(5, jobContainerHeight * 8);
            AlchemyContainer.SetSize(jobContainerWidth, jobContainerHeight);
            AlchemyContainer.Clicked += AlchemyBtn_Clicked;

            // Job Icon
            AlchemyIcon = new ImagePanel(AlchemyContainer, "AlchemyIcon");
            AlchemyIcon.SetPosition(0, 0);
            AlchemyIcon.SetSize(35, 35);

            // Job name and level
            mAlchemyLabel = new Label(AlchemyContainer, "AlchemyNameLbl");
            mAlchemyLabel.SetPosition(40, 0);

            // Exp label
            ExpAlchemyTitle = new Label(AlchemyContainer, "EXPAlchemyTitle");
            ExpAlchemyTitle.SetText(Strings.EntityBox.exp);
            ExpAlchemyTitle.SetPosition(40, 20);

            // Exp bar
            ExpAlchemyBackground = new ImagePanel(AlchemyContainer, "EXPAlchemyBackground");
            ExpAlchemyBackground.SetPosition(70, 20);
            ExpAlchemyBackground.SetSize(110, 16);

            ExpAlchemyBar = new ImagePanel(AlchemyContainer, "EXPAlchemyBar");
            ExpAlchemyBar.SetPosition(71, 21);
            ExpAlchemyBar.SetSize(100, 12);

            // Exp value
            ExpAlchemyLbl = new Label(AlchemyContainer, "EXPAlchemyLabel");
            ExpAlchemyLbl.SetPosition(70, 20);
            ExpAlchemyLbl.SetSize(180, 16);

            JobsPanel.AddChild(AlchemyContainer); 
            #endregion


            mSkillsWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void FishingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
           ExpBackground.IsHidden=false;
            ExpBar.Show();
            UpdateFishingInfo();
        }

        private void LumberjackBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
            UpdateLumberjackInfo();
        }

      

        private void MiningBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
            UpdateMiningInfo();
        }



        private void FarmingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
            UpdateFarmingInfo();
        }

        private void AlchemyBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
            UpdateAlchemyInfo();
        }

        private void CookingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
            UpdateCookingInfo();
        }

        private void BlacksmithBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
            UpdateBlacksmithInfo();
        }

        private void HuntingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            JobDescriptionLabel.ClearText();
            UpdateHuntingInfo();
        }

     
        private void UpdateLumberjackInfo()
        {
            Jobexp = Globals.Me.WoodExperience;
            xtnl = Globals.Me.ExperienceToWoodNextLevel;
            JobNameLabel.Text = mLumberjackLabel.Text;
            JobIcon = new ImagePanel(LumberjackIcon);
           /* ExpTitle.SetText(ExpLumberjackTitle.Text);
            ExpLabel.SetText(ExpLumberjackLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.LumberjackDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.WoodLevel.ToString());
        }
        private void UpdateFarmingInfo()
        {
            Jobexp = Globals.Me.FarmingExperience;
            xtnl = Globals.Me.ExperienceToFarmingNextLevel;
            JobNameLabel.Text = mFarmingLabel.Text;
            JobIcon = new ImagePanel(FarmingIcon);
            /*ExpTitle.SetText(ExpFarmingTitle.Text);
            ExpLabel.SetText(ExpFarmingLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.FarmingDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.FarmingLevel.ToString());

        }

        private void UpdateMiningInfo()
        {
            Jobexp = Globals.Me.MiningExperience;
            xtnl = Globals.Me.ExperienceToMiningNextLevel;
            JobNameLabel.Text = mMiningLabel.Text;
            JobIcon = new ImagePanel(MiningIcon);
          /*  ExpTitle.SetText(ExpMiningTitle.Text);
            ExpLabel.SetText(ExpMiningLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.MiningDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.MiningLevel.ToString());
        }

        private void UpdateFishingInfo()
        {
            Jobexp = Globals.Me.FishingExperience;
            xtnl = Globals.Me.ExperienceToFishingNextLevel;
            JobNameLabel.Text = mFishingLabel.Text;
            JobIcon = new ImagePanel(FishingIcon);
          /*  ExpTitle.SetText(ExpFishingTitle.Text);
            ExpLabel.SetText(ExpFishingLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.FishingDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.FishingLevel.ToString());
        }

        private void UpdateHuntingInfo()
        {
            Jobexp = Globals.Me.HuntingExperience;
            xtnl = Globals.Me.ExperienceToHuntingNextLevel;
            JobNameLabel.Text = mHuntingLabel.Text;
            JobIcon = new ImagePanel(HuntingIcon);
          /*  ExpTitle.SetText(ExpHuntingTitle.Text);
            ExpLabel.SetText(ExpHuntingLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.HuntingDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.HunterLevel.ToString());
        }
        private void UpdateBlacksmithInfo()
        {
            Jobexp = Globals.Me.BlacksmithExperience;
            xtnl = Globals.Me.ExperienceToBlacksmithNextLevel;
            JobNameLabel.Text = mBlacksmithLabel.Text;
            JobIcon = new ImagePanel(BlacksmithIcon);
          /*  ExpTitle.SetText(ExpBlacksmithTitle.Text);
            ExpLabel.SetText(ExpBlacksmithLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.BlacksmithDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.BlacksmithLevel.ToString());
        }
        private void UpdateCookingInfo()
        {
            Jobexp = Globals.Me.CookingExperience;
            xtnl = Globals.Me.ExperienceToCookingNextLevel;
            JobNameLabel.Text = mCookingLabel.Text;
            JobIcon = new ImagePanel(CookingIcon);
          /*  ExpTitle.SetText(ExpCookingTitle.Text);
            ExpLabel.SetText(ExpCookingLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.CookingDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.CookingLevel.ToString());
        }
        private void UpdateAlchemyInfo()
        {
            Jobexp = Globals.Me.AlchemyExperience;
            xtnl = Globals.Me.ExperienceToAlchemyNextLevel;
            JobNameLabel.Text = mAlchemyLabel.Text;
            JobIcon = new ImagePanel(AlchemyIcon);
           /* ExpTitle.SetText(ExpAlchemyTitle.Text);
            ExpLabel.SetText(ExpAlchemyLbl.Text);*/
            JobDescriptionLabel.AddText(Strings.Job.AlchemyDesc, mJobtDescTemplateLabel);
            JobLevelLabel.SetText("Lv. " + Globals.Me.AlchemyLevel.ToString());
        }
        public void Update()
        {
            //Time since this window was last updated (for bar animations)
            var elapsedTime = (Timing.Global.Milliseconds - mLastUpdateTime) / 1000.0f;
            mFarmingLabel.SetText(Strings.Job.skill0);
            mMiningLabel.SetText(Strings.Job.skill1);
            mLumberjackLabel.SetText(Strings.Job.skill2);
            mFishingLabel.SetText(Strings.Job.skill3);
            mHuntingLabel.SetText(Strings.Job.skill4);
            mBlacksmithLabel.SetText(Strings.Job.skill5);
            mCookingLabel.SetText(Strings.Job.skill6);
            mAlchemyLabel.SetText(Strings.Job.skill7);

            // Update experience labels
            /*  mFarmingExpLabel.SetText("Exp: " + Globals.Me.FarmingExperience.ToString() + "/" + Globals.Me.ExperienceToFarmingNextLevel.ToString());
              mMiningExpLabel.SetText("Exp: " + Globals.Me.MiningExperience.ToString() + "/" + Globals.Me.ExperienceToMiningNextLevel.ToString());
              mLumberjackExpLabel.SetText("Exp: " + Globals.Me.WoodExperience.ToString() + "/" + Globals.Me.ExperienceToWoodNextLevel.ToString());
              mFishingExpLabel.SetText("Exp: " + Globals.Me.FishingExperience.ToString() + "/" + Globals.Me.ExperienceToFishingNextLevel.ToString());
              mHunterExpLabel.SetText("Exp: " + Globals.Me.HuntingExperience.ToString() + "/" + Globals.Me.ExperienceToHuntingNextLevel.ToString());
              mBlacksmithExpLabel.SetText("Exp: " + Globals.Me.BlacksmithExperience.ToString() + "/" + Globals.Me.ExperienceToBlacksmithNextLevel.ToString());
              mCookingExpLabel.SetText("Exp: " + Globals.Me.CookingExperience.ToString() + "/" + Globals.Me.ExperienceToCookingNextLevel.ToString());
              mAlchemyExpLabel.SetText("Exp: " + Globals.Me.AlchemyExperience.ToString() + "/" + Globals.Me.ExperienceToAlchemyNextLevel.ToString());

             */
            UpdateXpBar(elapsedTime);
            UpdateFarmingXpBar(elapsedTime);
            UpdateMiningXpBar(elapsedTime);
            UpdateLumberjackXpBar(elapsedTime);
            UpdateFishingXpBar(elapsedTime);
            UpdateHuntingXpBar(elapsedTime);
            UpdateBlacksmithXpBar(elapsedTime);
            UpdateCookingXpBar(elapsedTime);
            UpdateAlchemyXpBar(elapsedTime);
        }

        private void UpdateXpBar(float elapsedTime)
        {
            float ExpSize = 1;
            if ((float)(xtnl) > 0)
            {
                ExpSize = (float)(Jobexp) /
                                 (float)(xtnl);

                ExpLabel.Text = Strings.EntityBox.expval.ToString(
                    ((float)(Jobexp)), ((float)(xtnl)));
            }
            else
            {
                ExpSize = 1f;
                ExpLabel.Text = Strings.EntityBox.maxlevel;
            }

            ExpSize *= ExpBackground.Width;
            if (Math.Abs((int)ExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)ExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)ExpSize)
                {
                    CurExpWidth = ExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < ExpSize)
                {
                    CurExpWidth = ExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpBar.IsHidden = true;
            }
            else
            {
                ExpBar.Width = (int)CurExpWidth;
                ExpBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpBar.Height);
                ExpBar.IsHidden = false;
            }
        }
        private void UpdateFarmingXpBar(float elapsedTime)
        {
            float farningExpSize = 1;
            if ((float)(Globals.Me.ExperienceToFarmingNextLevel) > 0)
            {
                farningExpSize = (float)(Globals.Me.FarmingExperience) /
                                 (float)(Globals.Me.ExperienceToFarmingNextLevel);

                ExpFarmingLbl.Text = Strings.EntityBox.expval.ToString(
                    ((float)(Globals.Me.FarmingExperience)), ((float)(Globals.Me.ExperienceToFarmingNextLevel)));
            }
            else
            {
                farningExpSize = 1f;
                ExpFarmingLbl.Text = Strings.EntityBox.maxlevel;
            }

            farningExpSize *= ExpFarmingBackground.Width;
            if (Math.Abs((int)farningExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)farningExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)farningExpSize)
                {
                    CurExpWidth = farningExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < farningExpSize)
                {
                    CurExpWidth = farningExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpFarmingBar.IsHidden = true;
            }
            else
            {
                ExpFarmingBar.Width = (int)CurExpWidth;
                ExpFarmingBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpFarmingBar.Height);
                ExpFarmingBar.IsHidden = false;
            }
        }
        private void UpdateMiningXpBar(float elapsedTime)
        {
            float miningExpSize = 1;
            if ((float)(Globals.Me.ExperienceToMiningNextLevel) > 0)
            {
                miningExpSize = (float)(Globals.Me.MiningExperience) /
                                 (float)(Globals.Me.ExperienceToMiningNextLevel);

                ExpMiningLbl.Text = Strings.EntityBox.expval.ToString(
                    ((float)(Globals.Me.MiningExperience)), ((float)(Globals.Me.ExperienceToMiningNextLevel)));
            }
            else
            {
                miningExpSize = 1f;
                ExpMiningLbl.Text = Strings.EntityBox.maxlevel;
            }

            miningExpSize *= ExpMiningBackground.Width;
            if (Math.Abs((int)miningExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)miningExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)miningExpSize)
                {
                    CurExpWidth = miningExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < miningExpSize)
                {
                    CurExpWidth = miningExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpMiningBar.IsHidden = true;
            }
            else
            {
                ExpMiningBar.Width = (int)CurExpWidth;
                ExpMiningBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpMiningBar.Height);
                ExpMiningBar.IsHidden = false;
            }
        }
        private void UpdateFishingXpBar(float elapsedTime)
        {
            float fishingExpSize = 1;
            if ((float)(Globals.Me.ExperienceToFishingNextLevel) > 0)
            {
                fishingExpSize = (float)(Globals.Me.FishingExperience) /
                                  (float)(Globals.Me.ExperienceToFishingNextLevel);

                ExpFishingLbl.Text = Strings.EntityBox.expval.ToString(
                    ((float)(Globals.Me.FishingExperience)), ((float)(Globals.Me.ExperienceToFishingNextLevel)));
            }
            else
            {
                fishingExpSize = 1f;
                ExpFishingLbl.Text = Strings.EntityBox.maxlevel;
            }

            fishingExpSize *= ExpFishingBackground.Width;
            if (Math.Abs((int)fishingExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)fishingExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)fishingExpSize)
                {
                    CurExpWidth = fishingExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < fishingExpSize)
                {
                    CurExpWidth = fishingExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpFishingBar.IsHidden = true;
            }
            else
            {
                ExpFishingBar.Width = (int)CurExpWidth;
                ExpFishingBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpFishingBar.Height);
                ExpFishingBar.IsHidden = false;
            }
        }
        private void UpdateLumberjackXpBar(float elapsedTime)
        {
            float lumberjackExpSize = 1;
            if ((float)(Globals.Me.ExperienceToWoodNextLevel) > 0)
            {
                lumberjackExpSize = (float)(Globals.Me.WoodExperience) /
                                         (float)(Globals.Me.ExperienceToWoodNextLevel);

                ExpLumberjackLbl.Text = Strings.EntityBox.expval.ToString(
                    ((float)(Globals.Me.WoodExperience)), ((float)(Globals.Me.ExperienceToWoodNextLevel)));
            }
            else
            {
                lumberjackExpSize = 1f;
                ExpLumberjackLbl.Text = Strings.EntityBox.maxlevel;
            }

            lumberjackExpSize *= ExpLumberjackBackground.Width;
            if (Math.Abs((int)lumberjackExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)lumberjackExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)lumberjackExpSize)
                {
                    CurExpWidth = lumberjackExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < lumberjackExpSize)
                {
                    CurExpWidth = lumberjackExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpLumberjackBar.IsHidden = true;
            }
            else
            {
                ExpLumberjackBar.Width = (int)CurExpWidth;
                ExpLumberjackBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpLumberjackBar.Height);
                ExpLumberjackBar.IsHidden = false;
            }
        }
        private void UpdateHuntingXpBar(float elapsedTime)
        {
            float huntingExpSize = 1;
            if ((float)(Globals.Me.ExperienceToHuntingNextLevel) > 0)
            {
                huntingExpSize = (float)(Globals.Me.HuntingExperience) /
                (float)(Globals.Me.ExperienceToHuntingNextLevel);

                ExpHuntingLbl.Text = Strings.EntityBox.expval.ToString(
        ((float)(Globals.Me.HuntingExperience)), ((float)(Globals.Me.ExperienceToHuntingNextLevel)));
            }
            else
            {
                huntingExpSize = 1f;
                ExpHuntingLbl.Text = Strings.EntityBox.maxlevel;
            }

            huntingExpSize *= ExpHuntingBackground.Width;
            if (Math.Abs((int)huntingExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)huntingExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)huntingExpSize)
                {
                    CurExpWidth = huntingExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < huntingExpSize)
                {
                    CurExpWidth = huntingExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpHuntingBar.IsHidden = true;
            }
            else
            {
                ExpHuntingBar.Width = (int)CurExpWidth;
                ExpHuntingBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpHuntingBar.Height);
                ExpHuntingBar.IsHidden = false;
            }
        }

        private void UpdateBlacksmithXpBar(float elapsedTime)
        {
            float blacksmithExpSize = 1;
            if ((float)(Globals.Me.ExperienceToBlacksmithNextLevel) > 0)
            {
                blacksmithExpSize = (float)(Globals.Me.BlacksmithExperience) /
                (float)(Globals.Me.ExperienceToBlacksmithNextLevel);
                ExpBlacksmithLbl.Text = Strings.EntityBox.expval.ToString(
        ((float)(Globals.Me.BlacksmithExperience)), ((float)(Globals.Me.ExperienceToBlacksmithNextLevel)));
            }
            else
            {
                blacksmithExpSize = 1f;
                ExpBlacksmithLbl.Text = Strings.EntityBox.maxlevel;
            }

            blacksmithExpSize *= ExpBlacksmithBackground.Width;
            if (Math.Abs((int)blacksmithExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)blacksmithExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)blacksmithExpSize)
                {
                    CurExpWidth = blacksmithExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < blacksmithExpSize)
                {
                    CurExpWidth = blacksmithExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpBlacksmithBar.IsHidden = true;
            }
            else
            {
                ExpBlacksmithBar.Width = (int)CurExpWidth;
                ExpBlacksmithBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpBlacksmithBar.Height);
                ExpBlacksmithBar.IsHidden = false;
            }
        }
        private void UpdateCookingXpBar(float elapsedTime)
        {
            float cookingExpSize = 1;
            if ((float)(Globals.Me.ExperienceToCookingNextLevel) > 0)
            {
                cookingExpSize = (float)(Globals.Me.CookingExperience) /
                                         (float)(Globals.Me.ExperienceToCookingNextLevel);

                ExpCookingLbl.Text = Strings.EntityBox.expval.ToString(
                    ((float)(Globals.Me.CookingExperience)), ((float)(Globals.Me.ExperienceToCookingNextLevel)));
            }
            else
            {
                cookingExpSize = 1f;
                ExpCookingLbl.Text = Strings.EntityBox.maxlevel;
            }

            cookingExpSize *= ExpCookingBackground.Width;
            if (Math.Abs((int)cookingExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)cookingExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)cookingExpSize)
                {
                    CurExpWidth = cookingExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < cookingExpSize)
                {
                    CurExpWidth = cookingExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpCookingBar.IsHidden = true;
            }
            else
            {
                ExpCookingBar.Width = (int)CurExpWidth;
                ExpCookingBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpCookingBar.Height);
                ExpCookingBar.IsHidden = false;
            }
        }

        private void UpdateAlchemyXpBar(float elapsedTime)
        {
            float alchemyExpSize = 1;
            if ((float)(Globals.Me.ExperienceToAlchemyNextLevel) > 0)
            {
                alchemyExpSize = (float)(Globals.Me.AlchemyExperience) /
                                         (float)(Globals.Me.ExperienceToAlchemyNextLevel);

                ExpAlchemyLbl.Text = Strings.EntityBox.expval.ToString(
                    ((float)(Globals.Me.AlchemyExperience)), ((float)(Globals.Me.ExperienceToAlchemyNextLevel)));
            }
            else
            {
                alchemyExpSize = 1f;
                ExpAlchemyLbl.Text = Strings.EntityBox.maxlevel;
            }

            alchemyExpSize *= ExpAlchemyBackground.Width;
            if (Math.Abs((int)alchemyExpSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)alchemyExpSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)alchemyExpSize)
                {
                    CurExpWidth = alchemyExpSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < alchemyExpSize)
                {
                    CurExpWidth = alchemyExpSize;
                }
            }

            if (CurExpWidth == 0)
            {
                ExpAlchemyBar.IsHidden = true;
            }
            else
            {
                ExpAlchemyBar.Width = (int)CurExpWidth;
                ExpAlchemyBar.SetTextureRect(0, 0, (int)CurExpWidth, ExpAlchemyBar.Height);
                ExpAlchemyBar.IsHidden = false;
            }
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







