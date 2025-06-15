using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Config;
using Intersect.Client.Framework.Gwen;
using Intersect.Framework.Core.GameObjects.Crafting;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Core;
using Microsoft.Extensions.Logging;

namespace Intersect.Client.Interface.Game.Job
{
    public partial class JobsWindow : Window
    {
        private ImagePanel InfoPanel;
        private ImagePanel JobsPanel;

        private Label JobNameLabel;
        public ImagePanel ExpBackground;
        public ImagePanel JobIcon;
        public ImagePanel ExpBar;
        private Label ExpLabel;
        private Label ExpTitle;
        private RichLabel JobDescriptionLabel;
        private Label JobLevelLabel;

        private ScrollControl mRecipePanel;
        private Label mJobtDescTemplateLabel;
        private List<RecipeItem> mItems = new List<RecipeItem>();
        private RecipeItem mCombinedItem;

        private JobType SelectedJob = JobType.None;

        public JobsWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Job.job, false, nameof(JobsWindow))
        {
            DisableResizing();

            // Panel izquierdo de trabajos
            JobsPanel = new ImagePanel(this, "JobsPanel");
            JobsPanel.SetSize(200, 400);
            JobsPanel.SetPosition(0, 0);

            // Panel derecho de información
            InfoPanel = new ImagePanel(this, "InfoPanel");
            InfoPanel.SetSize(400, 400);
            InfoPanel.SetPosition(200, 0);

            SetSize(600, 400);

            InitializeJobsPanel();
            InitializeInfoPanel();

            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            Hide();
        }

        private void InitializeJobsPanel()
        {
            var scrollContainer = new ScrollControl(JobsPanel, "JobsScrollContainer");
            scrollContainer.SetSize(JobsPanel.Width, JobsPanel.Height);
            scrollContainer.SetPosition(0, 0);
            scrollContainer.EnableScroll(false, true);

            int yOffset = 10;
            foreach (var jobType in Enum.GetValues(typeof(JobType)).Cast<JobType>())
            {
                if (jobType == JobType.None || jobType == JobType.JobCount) continue;

                var container = new Button(scrollContainer, $"{jobType}Container");
                container.SetSize(scrollContainer.Width - 20, 50);
                container.SetPosition(5, yOffset);
                container.Clicked += (s, e) => JobButtonClicked(s, e, jobType);

                var icon = new ImagePanel(container, $"{jobType}Icon");
                icon.SetSize(40, 40);
                icon.SetPosition(5, 5);

                var label = new Label(container, $"{jobType}NameLbl");
                label.SetText(Strings.Job.GetJobName(jobType));
                label.SetPosition(50, 15);

                yOffset += 60;
            }
            scrollContainer.SetInnerSize(scrollContainer.Width, yOffset);
        }

        private void InitializeInfoPanel()
        {
            // Icono y nombre del trabajo
            JobIcon = new ImagePanel(InfoPanel, "JobIcon");
            JobIcon.SetSize(40, 40);
            JobIcon.SetPosition(10, 10);

            JobNameLabel = new Label(InfoPanel, "JobNameLabel");
            JobNameLabel.SetPosition(60, 10);

            JobLevelLabel = new Label(InfoPanel, "JobLevelLabel");
            JobLevelLabel.SetPosition(300, 10);

            // Barra de experiencia
            ExpTitle = new Label(InfoPanel, "ExpTitle");
            ExpTitle.SetText(Strings.Job.Exp);
            ExpTitle.SetPosition(10, 200);

            ExpBackground = new ImagePanel(InfoPanel, "ExpBackground");
            ExpBackground.RenderColor = Color.FromArgb(255, 100, 100, 100);

            ExpBar = new ImagePanel(InfoPanel, "ExpBar");
            ExpBar.RenderColor = Color.FromArgb(255, 50, 150, 50);

            ExpLabel = new Label(InfoPanel, "ExpLabel");
            ExpLabel.RenderColor = Color.White;

            // Descripción
            mJobtDescTemplateLabel = new Label(InfoPanel, "JobDescriptionTemplate");
            JobDescriptionLabel = new RichLabel(InfoPanel, "Jobdesc");
            JobDescriptionLabel.SetPosition(10, 120);
            JobDescriptionLabel.SetSize(380, 100);

            // Contenedor de recetas listo para usar
            mRecipePanel = new ScrollControl(InfoPanel, "RecipePanel");
            mRecipePanel.SetPosition(10, 220);
            mRecipePanel.SetSize(250, 180);
            mRecipePanel.EnableScroll(false, true);
        }

        private void JobButtonClicked(Base sender, MouseButtonState arguments, JobType jobType)
        {
            SelectedJob = jobType;
            UpdateJobInfo(jobType);
        }

        private void UpdateJobInfo(JobType jobType)
        {
            // Datos básicos
            var level = Globals.Me.JobLevel.GetValueOrDefault(jobType, 1);
            var exp = Globals.Me.JobExp.GetValueOrDefault(jobType, 0);
            var expNext = Globals.Me.JobExpToNextLevel.GetValueOrDefault(jobType, 100);

          //  JobIcon.SetTexture(Strings.Job.GetJobIcon(jobType));
            JobNameLabel.SetText(Strings.Job.GetJobName(jobType));
            JobLevelLabel.SetText(string.Format(Strings.Job.Level, level));
            ExpLabel.SetText(string.Format(Strings.Job.ExpValue, exp, expNext));

            ExpBar.Width = (int)(ExpBackground.Width * (exp / (float)expNext));
            ExpBar.SetTextureRect(0, 0, ExpBar.Width, ExpBar.Height);

            JobDescriptionLabel.ClearText();
            JobDescriptionLabel.AddText(Strings.Job.GetJobDescription(jobType), mJobtDescTemplateLabel);

            LoadRecipes(jobType);
        }

        private void LoadRecipes(JobType jobType)
        {
            // Limpiar hijos previos
            mRecipePanel.ClearChildren(dispose: true);
            mItems.Clear();

            // Obtener recetas
            var table = CraftingTableDescriptor.Get(Guid.Parse(Options.Instance.JobOpts.RecipesId));
            if (table == null) return;

            int yOffset = 0;
            foreach (var recipeId in table.Crafts)
            {
                var recipe = CraftingRecipeDescriptor.Get(recipeId);
                if (recipe == null || recipe.Jobs != jobType) continue;

                var recipeContainer = new ImagePanel(mRecipePanel, "RecipeContainer");
                recipeContainer.SetSize(265, 80);
                recipeContainer.SetPosition(0, yOffset);

                var nameLbl = new Label(recipeContainer, "RecipeName", false);
                nameLbl.Text = recipe.Name;
                nameLbl.SetPosition(45, 10);

                var xpLbl = new Label(recipeContainer, "RecipeExp", false);
                xpLbl.SetText(Strings.Crafting.Exp.ToString(recipe.ExperienceAmount));
                xpLbl.SetPosition(200, 10);

                // Ícono
                mCombinedItem = new RecipeItem(this, new CraftingRecipeIngredient(recipe.ItemId, 1))
                {
                    Container = new ImagePanel(recipeContainer, "RecipeItem")
                };
                mCombinedItem.Setup("ItemIcon");
                mCombinedItem.Container.SetSize(32, 32);
                mCombinedItem.Container.SetPosition(5, 5);
                mCombinedItem.LoadItem();

                // Ingredientes
                var ingredientsPanel = new ScrollControl(recipeContainer, "IngredientsPanel");
                ingredientsPanel.SetPosition(0, 40);
                ingredientsPanel.SetSize(260, 50);
                ingredientsPanel.EnableScroll(false, false);

                var itemdict = Globals.Me.Inventory
                    .Where(i => i != null)
                    .GroupBy(i => i.ItemId)
                    .ToDictionary(g => g.Key, g => g.Sum(i => i.Quantity));

                int xOff = 0;
                foreach (var ing in recipe.Ingredients)
                {
                    var ui = new RecipeItem(this, ing)
                    {
                        Container = new ImagePanel(ingredientsPanel, "IngredientContainer")
                    };
                    ui.Setup("IngredientItemIcon");
                    ui.LoadItem();
                    ui.Container.SetSize(32, 32);
                    ui.Container.SetPosition(xOff, 0);

                    var onHand = itemdict.GetValueOrDefault(ing.ItemId, 0);
                    var lbl = new Label(ui.Container, "IngredientItemValue") { Text = $"{onHand}/{ing.Quantity}" };

                    ui.Container.RenderColor = onHand < ing.Quantity
                        ? Color.Yellow
                        : Color.Green;

                    ingredientsPanel.AddChild(ui.Container);
                    mItems.Add(ui);
                    xOff += 37;
                }

                recipeContainer.AddChild(ingredientsPanel);
                yOffset += recipeContainer.Height + 10;
            }

            mRecipePanel.SetInnerSize(mRecipePanel.Width, yOffset);
            mRecipePanel.UpdateScrollBars();
        }

        public override void Show()
        {
            base.Show();
            ExpBackground.Show();
            ExpBar.Show();
            ExpLabel.Show();
            ExpTitle.Show();
            JobLevelLabel.Show();
            JobNameLabel.Show();
            JobDescriptionLabel.Show();
        }

        public override void Hide()
        {
            base.Hide();
            JobDescriptionLabel.ClearText();
        }

        public void Update()
        {
            if (SelectedJob != JobType.None)
            {
                UpdateJobInfo(SelectedJob);
            }
        }
        public bool IsVisible()
        {
            return !IsHidden;
        }
        protected override void EnsureInitialized()
        {
    
            if (Globals.ActiveCraftingTable?.Crafts?.Count > 0)
            {
                LoadRecipes(SelectedJob);
            }
        }
    }
}
