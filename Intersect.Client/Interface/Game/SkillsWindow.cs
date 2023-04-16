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
        public ImagePanel FarmingIcon;          

        // Mining
        public Button MiningContainer;
        Label mMiningLabel;
        public ImagePanel MiningIcon;
       
        // Fishing
        public Button FishingContainer;
        Label mFishingLabel;
        public ImagePanel FishingIcon;     

        // Lumberjack
        public Button LumberjackContainer;
        Label mLumberjackLabel;
        public ImagePanel LumberjackIcon;

    
        // Hunter
        public Button HuntingContainer;
        Label mHuntingLabel;
        public ImagePanel HuntingIcon;

        // Blacksmith
        public Button BlacksmithContainer;
        Label mBlacksmithLabel;
        public ImagePanel BlacksmithIcon;
        

        // Cooking
        public Button CookingContainer;
        Label mCookingLabel;
        public ImagePanel CookingIcon;      

        // Alchemy
        public Button AlchemyContainer;
        Label mAlchemyLabel;
        public ImagePanel AlchemyIcon;
    
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

            JobsPanel.AddChild(LumberjackContainer);
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

          

            JobsPanel.AddChild(AlchemyContainer); 
            #endregion


            mSkillsWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
        }

        private void FishingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {        
            JobDescriptionLabel.ClearText();
            ExpBackground.IsHidden=false;          
            UpdateFishingInfo();       
        }

        private void LumberjackBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ExpBackground.IsHidden = false;
            JobDescriptionLabel.ClearText();
            UpdateLumberjackInfo();                    
        }

      

        private void MiningBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ExpBackground.IsHidden = false;           
            JobDescriptionLabel.ClearText();
            UpdateMiningInfo();        
            
        }



        private void FarmingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ExpBackground.IsHidden = false;           
            JobDescriptionLabel.ClearText();
            UpdateFarmingInfo();      
           
        }

        private void AlchemyBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ExpBackground.IsHidden = false;
            JobDescriptionLabel.ClearText();
            UpdateAlchemyInfo();                     
        }

        private void CookingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ExpBackground.IsHidden = false;
            JobDescriptionLabel.ClearText();
            UpdateCookingInfo();          
            
        }

        private void BlacksmithBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            ExpBackground.IsHidden = false;           
            JobDescriptionLabel.ClearText();
            UpdateBlacksmithInfo();
               
        }

        private void HuntingBtn_Clicked(Base sender, ClickedEventArgs arguments)
        {
            
            ExpBackground.IsHidden = false;
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.WoodLevel);
            InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.WoodExperience, Globals.Me.ExperienceToWoodNextLevel);
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.FarmingLevel);
            InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.FarmingExperience, Globals.Me.ExperienceToFarmingNextLevel);
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.MiningLevel);
            InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.MiningExperience,Globals.Me.ExperienceToMiningNextLevel);
        
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.FishingLevel);
            InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.FishingExperience, Globals.Me.ExperienceToFishingNextLevel);
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.HunterLevel);
            InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.HuntingExperience, Globals.Me.ExperienceToHuntingNextLevel);
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.BlacksmithLevel);
            InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.BlacksmithExperience, Globals.Me.ExperienceToBlacksmithNextLevel);
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.CookingLevel);
            InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show(); 
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.CookingExperience, Globals.Me.ExperienceToCookingNextLevel);
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
            JobLevelLabel.Text = Strings.Job.level.ToString(Globals.Me.AlchemyLevel);
           InfoPanel.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
            UpdateExperience(Globals.Me.AlchemyExperience, Globals.Me.ExperienceToAlchemyNextLevel);

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
            
            UpdateXpBar(elapsedTime);
               
            

        }
        private void UpdateExperience(float currentExperience, float requiredExperience)
        {
            var elapsedTime = (Timing.Global.Milliseconds - mLastUpdateTime) / 1000.0f;
            float expSize;
            if (requiredExperience > 0)
            {
                expSize = currentExperience / requiredExperience;
                ExpLabel.Text = Strings.EntityBox.expval.ToString(currentExperience, requiredExperience);
            }
            else
            {
                expSize = 1f;
                ExpLabel.Text = Strings.EntityBox.maxlevel;
            }

            expSize *= ExpBackground.Width;
            if (Math.Abs((int)expSize - CurExpWidth) < 0.01)
            {
                return;
            }

            if ((int)expSize > CurExpWidth)
            {
                CurExpWidth += 100f * elapsedTime;
                if (CurExpWidth > (int)expSize)
                {
                    CurExpWidth = expSize;
                }
            }
            else
            {
                CurExpWidth -= 100f * elapsedTime;
                if (CurExpWidth < expSize)
                {
                    CurExpWidth = expSize;
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
       
        public void Show()
        {
            mSkillsWindow.IsHidden = false;
           InfoPanel.Hide();
            ExpBackground.Hide();
            ExpBar.Hide();
            ExpLabel.Hide();
            ExpTitle.Hide();            
            JobLevelLabel.Hide();
            JobNameLabel.Hide();
            JobDescriptionLabel.Hide();           
        }

        public bool IsVisible()
        {
            return !mSkillsWindow.IsHidden;
        }

        public void Hide()
        {
            mSkillsWindow.IsHidden = true;
            JobDescriptionLabel.ClearText();
        }


    }


}







