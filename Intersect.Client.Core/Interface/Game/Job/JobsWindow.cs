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
            IsResizable = false;

            // Panel izquierdo de trabajos
            JobsPanel = new ImagePanel(this, "JobsPanel");
            JobsPanel.SetSize(200, 400);
            JobsPanel.SetPosition(0, 0);

            // Panel derecho de información
            InfoPanel = new ImagePanel(this, "InfoPanel");
            InfoPanel.SetPosition(200, 0);

            InfoPanel.SetSize(450, 400); // Más espacio horizontal
            SetSize(650, 400);           // Ventana completa más ancha


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

                var container = new Button(scrollContainer, $"{jobType}Container")
                {
                    FontName = "sourcesansproblack",
                    FontSize = 16,
                    RenderColor = Color.White,

                };
                container.SetSize(scrollContainer.Width - 20, 50);
                container.SetPosition(5, yOffset);
                container.Clicked += (s, e) => JobButtonClicked(s, e, jobType);

                var icon = new ImagePanel(container, $"{jobType}Icon")
                {
                    Texture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Gui, $"{jobType.ToString().ToLower()}.png")
                };
                icon.SetSize(40, 40);
                icon.SetPosition(5, 5);

                var label = new Label(container, $"{jobType}NameLbl")
                {
                    Text = Strings.Job.GetJobName(jobType),
                    FontName = "sourcesansproblack",
                    FontSize = 16,
                    TextColorOverride = Color.White
                };
                label.SetPosition(50, 15);

                yOffset += 60;
            }

            scrollContainer.SetInnerSize(scrollContainer.Width, yOffset);
        }

        private void InitializeInfoPanel()
        {

            JobNameLabel = new Label(InfoPanel, "JobNameLabel")
            {
                FontName = "sourcesansproblack",
                FontSize = 16,
                TextColorOverride = Color.White
            };
            JobNameLabel.SetPosition(60, 10);

            JobLevelLabel = new Label(InfoPanel, "JobLevelLabel")
            {
                FontName = "sourcesansproblack",
                FontSize = 16,
                TextColorOverride = Color.White
            };
            JobLevelLabel.SetPosition(360, 10);

            ExpTitle = new Label(InfoPanel, "ExpTitle")
            {
                Text = Strings.Job.Exp,
                FontName = "sourcesansproblack",
                FontSize = 12,
                TextColorOverride = Color.White
            };
            ExpTitle.SetPosition(10, 200);

            ExpBackground = new ImagePanel(InfoPanel, "ExpBackground")
            {
                RenderColor = Color.FromArgb(255, 100, 100, 100),
                Texture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Gui, "emptybar.png")
            };

            ExpBar = new ImagePanel(InfoPanel, "ExpBar")
            {
                RenderColor = Color.FromArgb(255, 50, 150, 50),
                Texture = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Gui, "hpbar.png")
            };

            ExpLabel = new Label(InfoPanel, "ExpLabel")
            {
                FontName = "sourcesansproblack",
                FontSize = 12,
                RenderColor = Color.White
            };

            mJobtDescTemplateLabel = new Label(InfoPanel, "JobDescriptionTemplate")
            {
                FontName = "sourcesansproblack",
                FontSize = 12,
                RenderColor = Color.White
            };

            JobDescriptionLabel = new RichLabel(mJobtDescTemplateLabel, "Jobdesc")
            {
                FontName = "sourcesansproblack",
                FontSize = 12
            };
            JobDescriptionLabel.SetPosition(10, 120);
            JobDescriptionLabel.SetSize(260, 160);

            mRecipePanel = new ScrollControl(InfoPanel, "RecipePanel");
            mRecipePanel.SetPosition(10, 230);
            mRecipePanel.SetSize(300, 170);
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
        {  // Record current scroll position before clearing to preserve it
            var currentScroll = mRecipePanel.VerticalScrollBar.ScrollAmount;
            // Limpiar visual y lógicamente las recetas anteriores
            foreach (var craftedItem in mItems)
            {
                if (craftedItem.Container is { } container)
                {
                    container.Dispose(); // Para evitar fugas visuales
                }
                Interface.GameUi.ItemDescriptionWindow?.Hide();
            }

            // Limpiar visualmente y en memoria
            mRecipePanel.ClearChildren(); // Limpia todo visual
            mItems.Clear();               // Limpia la lista de recetas cargadas

            // Reiniciar desplazamiento
            mRecipePanel.SetInnerSize(mRecipePanel.Width, 1); // Evita que se quede "pegado" sin scroll
            mRecipePanel.UpdateScrollBars(); // Refresca visualmente

            // Obtener la tabla
            var tableId = Guid.Parse(Options.Instance.JobOpts.RecipesId);
            var table = CraftingTableDescriptor.Get(tableId);
            if (table == null)
                return;

            var inventoryItemsByDescriptorId = Globals.Me.Inventory
                .Where(i => i != null)
                .GroupBy(i => i.ItemId)
                .ToDictionary(g => g.Key, g => g.Sum(i => i.Quantity));

            int yOffset = 0;

            foreach (var recipeId in table.Crafts)
            {
                var recipe = CraftingRecipeDescriptor.Get(recipeId);
                if (recipe == null || recipe.Jobs != jobType)
                    continue;

                // Contenedor de receta
                var recipeContainer = new ImagePanel(mRecipePanel, "JobsRecipeContainer");
                recipeContainer.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                recipeContainer.SetSize(265, 80);
                recipeContainer.SetPosition(0, yOffset);

                // Nombre
                var nameLbl = new Label(recipeContainer, "RecipeName")
                {
                    Text = recipe.Name,
                    FontName = "sourcesansproblack",
                    FontSize = 10,
                    RenderColor = Color.White,
                    TextColorOverride = Color.White
                };
                nameLbl.SetPosition(50, 5);

                // XP
                var xpLbl = new Label(recipeContainer, "RecipeExp")
                {
                    Text = Strings.Crafting.Exp.ToString(recipe.ExperienceAmount),
                    FontName = "sourcesansproblack",
                    FontSize = 10,
                    RenderColor = Color.White,
                    TextColorOverride = Color.ForestGreen
                };
                xpLbl.SetPosition(50, 20);

                // Ítem a fabricar
                var craftedItem = new RecipeItem(this, new CraftingRecipeIngredient(recipe.ItemId, 1))
                {
                    Container = new ImagePanel(recipeContainer, "JobRecipeItem")
                };
                craftedItem.Setup("ItemIcon");
                craftedItem.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                craftedItem.Container.SetSize(32, 32);
                craftedItem.Container.SetPosition(5, 5);
                craftedItem.LoadItem();

                // Panel de ingredientes
                var ingredientsPanel = new ScrollControl(recipeContainer, "IngredientsPanel");
                ingredientsPanel.SetSize(295, 50);
                ingredientsPanel.SetPosition(0, 40);
                ingredientsPanel.EnableScroll(false, false);

                int xOff = 0;
                for (int i = 0; i < recipe.Ingredients.Count; i++)
                {
                    var ing = recipe.Ingredients[i];
                    var recipeItem = new RecipeItem(this, ing)
                    {
                        Container = new ImagePanel(ingredientsPanel, "JobIngredientContainer")
                    };
                    recipeItem.Setup("IngredientItemIcon");
                    recipeItem.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                    recipeItem.Container.SetSize(32, 32);
                    recipeItem.Container.SetPosition(xOff, 0);
                    recipeItem.LoadItem();

                    var onHand = inventoryItemsByDescriptorId.GetValueOrDefault(ing.ItemId, 0);
                    var lbl = new Label(recipeItem.Container, "IngredientItemValue")
                    {
                        Text = $"{onHand}/{ing.Quantity}",
                        FontName = "sourcesansproblack",
                        FontSize = 8,
                        TextColorOverride = Color.White
                    };

                    // Colorear ítem
                    if (onHand == 0)
                        recipeItem.Container.RenderColor = Color.FromArgb(255, 255, 0, 0); // Rojo
                    else if (onHand < ing.Quantity)
                        recipeItem.Container.RenderColor = Color.FromArgb(255, 255, 255, 0); // Amarillo
                    else
                        recipeItem.Container.RenderColor = Color.FromArgb(255, 0, 255, 0); // Verde

                    ingredientsPanel.AddChild(recipeItem.Container);
                    mItems.Add(recipeItem);

                    xOff += 37;
                }

                recipeContainer.AddChild(ingredientsPanel);
                mRecipePanel.AddChild(recipeContainer);

                yOffset += recipeContainer.Height + 10;
            }

            var innerHeight = Math.Max(yOffset, mRecipePanel.Height + 1);
            mRecipePanel.SetInnerSize(mRecipePanel.Width, innerHeight);
            mRecipePanel.UpdateScrollBars();
            // Restore previous scroll position to avoid jumping to the top
            mRecipePanel.VerticalScrollBar.ScrollAmount = currentScroll;
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
