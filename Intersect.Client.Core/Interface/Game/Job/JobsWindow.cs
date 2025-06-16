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
        private float CurExpWidth = 0;
        // Panels

        public int X;
        public int Y;
        public float CurExpSize = -1;
        private long mLastUpdateTime;
        public float xtnl;
        public float Jobexp;
        private List<Label> mValues = new List<Label>();
        private Label mNameLabel;
        private ScrollControl ingredientsPanel;
        private ImagePanel recipeContainer;

        public Label mXpLabel;

        public JobsWindow(Canvas gameCanvas) : base(gameCanvas, Strings.Job.job, false, nameof(JobsWindow))
        {
            DisableResizing();
          
            InfoPanel = new ImagePanel(this, "InfoPanel");

          
            // Initialize the InfoPanel
            InitializeInfoPanel(SelectedJob);

            JobsPanel = new ImagePanel(this, "JobsPanel");
    
            JobsPanel.SetPosition(0, 0);
            SetSize(600, 400); // Tamaño general reducido

            JobsPanel.SetSize(200, 400); // Panel izquierdo más angosto
            JobsPanel.SetPosition(0, 0);

            InfoPanel.SetSize(400, 400); // Panel derecho para la información del trabajo
            InfoPanel.SetPosition(200, 0);

            var jobContainerHeight = 50; // Altura de cada botón
            var jobContainerWidth = 190; // Más compacto
            var jobIconSize = 40; // Íconos más pequeños

            InitializeJobsPanel();    
            AddChild(JobsPanel);
            LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
            // Start hidden until explicitly shown
            Hide();
        }

        private void InitializeJobsPanel()
        {
            var jobTypes = Enum.GetValues(typeof(JobType)).Cast<JobType>();

            // Crear un ScrollControl para contener todos los trabajos
            var scrollContainer = new ScrollControl(JobsPanel, "JobsScrollContainer");
            scrollContainer.SetPosition(0, 0);
            scrollContainer.SetSize(JobsPanel.Width, JobsPanel.Height);
            scrollContainer.EnableScroll(false, true); // Habilita el scroll vertical

            int yOffset = 10; // Espaciado inicial

            foreach (var jobType in jobTypes)
            {
                if (jobType == JobType.None || jobType == JobType.JobCount)
                {
                    continue;
                }

                // Contenedor del trabajo
                var container = new Button(scrollContainer, $"{jobType}Container");
                container.SetPosition(5, yOffset);
                container.SetSize(scrollContainer.Width - 20, 50); // Botón más compacto
                container.Clicked += (sender, args) => JobButtonClicked(sender, args, jobType);

                // Ícono del trabajo
                var icon = new ImagePanel(container, $"{jobType}Icon");
                icon.SetPosition(5, 5);
                icon.SetSize(40, 40); // Tamaño ajustado para mejor visibilidad

                // Etiqueta del trabajo
                var label = new Label(container, $"{jobType}NameLbl");
                label.SetPosition(50, 15);
                label.SetText(Strings.Job.GetJobName(jobType));

                scrollContainer.AddChild(container); // Añade el contenedor al ScrollControl

                yOffset += 60; // Espaciado entre botones
            }

            // Ajustar el tamaño interno del scroll para que se ajuste a todos los trabajos
            scrollContainer.SetInnerSize(scrollContainer.Width, yOffset);
        }

        private void InitializeInfoPanel(JobType jobType)
        {
                     
                JobIcon = new ImagePanel(InfoPanel, "JobIcon");
                JobIcon.SetPosition(10, 10);
                JobIcon.SetSize(50, 50);

                // Nombre del trabajo
                JobNameLabel = new Label(InfoPanel, "JobNameLabel");
                JobNameLabel.SetPosition(70, 10);
                JobNameLabel.SetText(Strings.Job.GetJobName(jobType));

                // Nivel del trabajo
                JobLevelLabel = new Label(InfoPanel, "JobLevelLabel");
                JobLevelLabel.SetPosition(200, 10);
                JobLevelLabel.SetText(string.Format(Strings.Job.Level, Globals.Me.JobLevel)); // Ejemplo con nivel inicial

                // Título de experiencia
                ExpTitle = new Label(InfoPanel, "ExpTitle");
                ExpTitle.SetText(Strings.Job.Exp); // Referencia localizada
                ExpTitle.SetPosition(10, 200);
                ExpTitle.RenderColor = Color.FromArgb(255, 255, 255, 255);

                // Fondo de la barra de experiencia
                ExpBackground = new ImagePanel(InfoPanel, "ExpBackground");

                ExpBackground.RenderColor = Color.FromArgb(255, 100, 100, 100);

                // Barra de experiencia
                ExpBar = new ImagePanel(InfoPanel, "ExpBar");

                ExpBar.RenderColor = Color.FromArgb(255, 50, 150, 50);

                // Etiqueta de experiencia
                ExpLabel = new Label(InfoPanel, "ExpLabel");

                ExpLabel.SetText(string.Format(Strings.Job.ExpValue, Globals.Me.JobExp, Globals.Me.JobExpToNextLevel)); // Ejemplo inicial
                ExpLabel.RenderColor = Color.FromArgb(255, 255, 255, 255);
                mJobtDescTemplateLabel = new Label(InfoPanel, "JobDescriptionTemplate");
                // Descripción del trabajo
                JobDescriptionLabel = new RichLabel(InfoPanel, "Jobdesc");

                JobDescriptionLabel.ClearText();
                JobIcon.SetSize(40, 40); // Ícono del trabajo más pequeño
                JobIcon.SetPosition(10, 10);

                JobNameLabel.SetPosition(60, 10); // Nombre del trabajo


                JobLevelLabel.SetPosition(300, 10); // Nivel del trabajo alineado

                ExpBar.SetSize(380, 20); // Barra de experiencia más angosta
                ExpBar.SetPosition(10, 60);

                ExpLabel.SetPosition(10, 90); // Etiqueta de experiencia


                JobDescriptionLabel.SetPosition(10, 120); // Descripción del trabajo
                JobDescriptionLabel.SetSize(380, 100); // Tamaño compacto


                if (mJobtDescTemplateLabel != null)
                {
                    JobDescriptionLabel.AddText(Strings.Job.GetJobDescription(JobType.None), mJobtDescTemplateLabel);
                }
                else
                {
                    //PacketSender.SendChatMsg("Error: No se pudo crear el Label para el template.", 5);
                }
                // Recipe panel
                mRecipePanel = new ScrollControl(InfoPanel, "RecipePanel");
                mRecipePanel.SetPosition(10, 220); // Ajusta según sea necesario
                mRecipePanel.SetSize(245, 180); // Ajusta según sea necesario
        
                mRecipePanel.EnableScroll(false, true); // Habilita barras de desplazamiento

                // Crear un contenedor para la receta
                recipeContainer = new ImagePanel(mRecipePanel, "RecipeContainer");
                recipeContainer.SetSize(245, 80); // Ajusta el tamaño del contenedor según sea necesario
                recipeContainer.Margin = new Margin(0, 0, 0, 10); // Añade un margen inferior para espacio entre recetas
                mNameLabel = new Label(recipeContainer, "RecipeName", false);
                mNameLabel.SetPosition(45, 20);
                mNameLabel.SetTextColor(Color.White, ComponentState.Normal);

                mXpLabel = new Label(recipeContainer, "RecipeExp", false);
                mXpLabel.SetPosition(200, 20);
                mXpLabel.SetTextColor(Color.White,ComponentState.Normal);
                // Llenar el panel de recetas

                recipeContainer.AddChild(mNameLabel);
                recipeContainer.AddChild(mXpLabel);
                InfoPanel.AddChild(mRecipePanel);
                mRecipePanel.AddChild(recipeContainer);
                LoadRecipes(jobType);
            }
        

        private void JobButtonClicked (Base sender, MouseButtonState arguments, JobType jobType)
        {
            JobDescriptionLabel.ClearText();
            ExpBackground.IsHidden = false;
            UpdateJobInfo(jobType);
        }
        private void UpdateJobInfo(JobType jobType)
        {
          
            // Validar que el trabajo es válido
            if (jobType == JobType.None || jobType == JobType.JobCount)
            {
                //  PacketSender.SendChatMsg($"Advertencia: Trabajo '{jobType}' no es válido para actualizar.", 5);
                return;
            }

            // Validar que los diccionarios de trabajos estén inicializados
            if (Globals.Me.JobLevel == null || Globals.Me.JobExp == null || Globals.Me.JobExpToNextLevel == null)
            {
                //PacketSender.SendChatMsg("Error: Los datos de trabajos no están inicializados.", 5);
                return;
            }
            if (jobType == JobType.None || jobType == JobType.JobCount)
            {
                InfoPanel.Show();
            }
            else
            {
                InfoPanel.Show();
                // Obtener valores de los diccionarios con valores predeterminados
                var level = Globals.Me.JobLevel.ContainsKey(jobType) ? Globals.Me.JobLevel[jobType] : 1;
                var exp = Globals.Me.JobExp.ContainsKey(jobType) ? Globals.Me.JobExp[jobType] : 0;
                var expToNextLevel = Globals.Me.JobExpToNextLevel.ContainsKey(jobType) ? Globals.Me.JobExpToNextLevel[jobType] : 100;

                // Validar que los datos son razonables
                if (level <= 0 || exp < 0 || expToNextLevel <= 0)
                {
                    // PacketSender.SendChatMsg($"Error: Datos inválidos para el trabajo '{jobType}'. Nivel: {level}, Exp: {exp}, Exp para siguiente nivel: {expToNextLevel}.", 5);
                    return;
                }


                // Validar que los componentes de la interfaz están inicializados
                if (JobNameLabel == null || JobLevelLabel == null || ExpLabel == null || ExpBar == null || ExpBackground == null || JobDescriptionLabel == null)
                {
                    //PacketSender.SendChatMsg("Error: Los componentes de la interfaz no están inicializados.", 5);
                    return;
                }

                // Actualizar la interfaz con los datos disponibles
                JobNameLabel.SetText(Strings.Job.GetJobName(jobType));
                JobLevelLabel.SetText(string.Format(Strings.Job.Level, level));
                ExpLabel.SetText(string.Format(Strings.Job.ExpValue, exp, expToNextLevel));
                ExpBar.Width = (int)(ExpBackground.Width * (exp / (float)expToNextLevel));
                ExpBar.SetTextureRect(0, 0, ExpBar.Width, ExpBar.Height);

                JobDescriptionLabel.ClearText();
                JobDescriptionLabel.AddText(Strings.Job.GetJobDescription(jobType), mJobtDescTemplateLabel);
                LoadRecipes(jobType);
                // Mensaje de depuración
                //PacketSender.SendChatMsg($"Trabajo {jobType}: Nivel {level}, Exp {exp}/{expToNextLevel}", 1);
               
            }
        }

        private void LoadRecipes(JobType jobType)
        {
            // Limpiar el panel de recetas antes de cargar nuevas recetas
            if (mRecipePanel != null)
            {
                InfoPanel.RemoveChild(mRecipePanel, true);
                mRecipePanel = null;
                mItems.Clear();
                mValues.Clear();
            }

            // Clear the old item description box
            if (mCombinedItem?.DescWindow != null)
            {
                mCombinedItem.DescWindow.Dispose();
                mCombinedItem = null;
            }
            mRecipePanel = new ScrollControl(InfoPanel, "RecipePanel");
            mRecipePanel.SetPosition(10, 220); // Ajusta según sea necesario
            mRecipePanel.SetSize(250, 180); // Ajusta según sea necesario
            mRecipePanel.EnableScroll(false, true); // Habilita barras de desplazamiento

           // var fixedCraftingTable = CraftingTableBase.GetCraftsByJob(jobType).ToList();
            // Obtener la tabla de crafteo fija
            var fixedCraftingTable = CraftingTableDescriptor.Get(Guid.Parse(Options.Instance.JobOpts.RecipesId));
            if (fixedCraftingTable == null)
            {
                return;
            }

            var recipes = fixedCraftingTable.Crafts;

            int yOffset = 0; // Para mantener la posición vertical de cada contenedor de recetas

            foreach (var recipeId in recipes)
            {
                var recipe = CraftingRecipeDescriptor.Get(recipeId);
                if (recipe == null || recipe.Jobs != jobType)
                {
                    continue;
                }

                var recipeName = recipe.Name; // Nombre de la receta
                var recipexp = recipe.ExperienceAmount;
                var itemIcon = ItemDescriptor.Get(recipe.ItemId)?.Icon; // Ícono del item a craftear
                recipeContainer = new ImagePanel(mRecipePanel, "RecipeContainer");
                recipeContainer.SetSize(265, 80); // Ajusta el tamaño del contenedor según sea necesario

                var xPadding = recipeContainer.Margin.Left + recipeContainer.Margin.Right;
                var yPadding = recipeContainer.Margin.Top + recipeContainer.Margin.Bottom;

                var availableWidth = mRecipePanel.Width - mRecipePanel.VerticalScrollBar.Width;
                var itemWidth = recipeContainer.Width + xPadding;

                if (itemWidth > 0)
                {
                    var sizeFactor = availableWidth / itemWidth;

                    if (sizeFactor > 0)
                    {
                        recipeContainer.SetPosition(
                            yOffset % sizeFactor * (recipeContainer.Width + xPadding) + xPadding,
                            yOffset / sizeFactor * (recipeContainer.Height + yPadding) + yPadding
                        );
                    }
                    else
                    {
                        recipeContainer.SetPosition(0, yOffset);
                    }
                }
                else
                {
                    recipeContainer.SetPosition(0, yOffset);
                }
                mNameLabel = new Label(recipeContainer, "RecipeName", false);
                mNameLabel.Text = recipeName;
                mNameLabel.SetPosition(45, 10);
                mNameLabel.SetTextColor(Color.White, ComponentState.Normal);

                mXpLabel = new Label(recipeContainer, "RecipeExp", false);
                mXpLabel.SetText(Strings.Crafting.Exp.ToString(recipexp));
                mXpLabel.SetPosition(200, 10);
                mXpLabel.SetTextColor(Color.White, ComponentState.Normal);

                // Configurar el panel del ítem a craftear
                mCombinedItem = new RecipeItem(this, new CraftingRecipeIngredient(recipe.ItemId, 1))
                {
                    Container = new ImagePanel(recipeContainer, "RecipeItem")
                };
                mCombinedItem.Setup("ItemIcon");

                mCombinedItem.Container.SetSize(32, 32); // Establece el tamaño del ícono del ítem
                mCombinedItem.Container.SetPosition(5, 5);
                recipeContainer.AddChild(mCombinedItem.Container);
                mCombinedItem.Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());
                mCombinedItem.LoadItem();

                ingredientsPanel = new ScrollControl(recipeContainer, "IngredientsPanel");
                ingredientsPanel.SetPosition(0, 40); // Ajusta la posición según sea necesario
                ingredientsPanel.SetSize(260, 50);
                ingredientsPanel.EnableScroll(false, false);
                // ingredientsPanel.GetHorizontalScrollBar();

                // Añadir los ingredientes
                var itemdict = new Dictionary<Guid, int>();
                foreach (var item in Globals.Me.Inventory)
                {
                    if (item != null)
                    {
                        if (itemdict.ContainsKey(item.ItemId))
                        {
                            itemdict[item.ItemId] += item.Quantity;
                        }
                        else
                        {
                            itemdict.Add(item.ItemId, item.Quantity);
                        }
                    }
                }

                int xOffset = 0;

                var craftableQuantity = -1;

                for (var i = 0; i < recipe.Ingredients.Count; i++)
                {
                    mItems.Add(new RecipeItem(this, recipe.Ingredients[i]));
                    mItems[^1].Container = new ImagePanel(ingredientsPanel, "IngredientContainer");
                    mItems[^1].Setup("IngredientItemIcon");
                    ingredientsPanel.AddChild(mItems[^1].Container);
                    var lblTemp = new Label(mItems[mItems.Count - 1].Container, "IngredientItemValue");

                    var onHand = 0;
                    if (itemdict.ContainsKey(recipe.Ingredients[i].ItemId))
                    {
                        onHand = itemdict[recipe.Ingredients[i].ItemId];
                    }
                    lblTemp.Text = onHand + "/" + recipe.Ingredients[i].Quantity;

                    var possibleToCraft = (int)Math.Floor(onHand / (double)recipe.Ingredients[i].Quantity);

                    if (craftableQuantity == -1 || possibleToCraft < craftableQuantity)
                    {
                        craftableQuantity = possibleToCraft;
                    }

                    mValues.Add(lblTemp);

                    mItems[mItems.Count - 1].Container.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

                    mItems[mItems.Count - 1].LoadItem();

                    mItems[mItems.Count - 1].Container.SetSize(32, 32);
                    mItems[mItems.Count - 1].Container.SetPosition(xOffset, 0);
                    // Colorear el contenedor según la cantidad de ingredientes disponibles
                    if (onHand == 0)
                    {

                        mItems[mItems.Count - 1].Container.RenderColor = Color.FromArgb(255, 255, 0, 0); // Rojo
                    }
                    else if (onHand < recipe.Ingredients[i].Quantity)
                    {
                        mItems[mItems.Count - 1].Container.RenderColor = Color.FromArgb(255, 255, 255, 0); // Amarillo
                    }
                    else
                    {
                        mItems[mItems.Count - 1].Container.RenderColor = Color.FromArgb(255, 0, 255, 0); // Verde
                    }
                    xOffset += 32 + 5; // Espacio entre ingredientes

                }
                recipeContainer.AddChild(ingredientsPanel);

                // Añadir el contenedor de la receta al panel principal
                mRecipePanel.AddChild(recipeContainer);

                // Incrementar el yOffset para la siguiente receta
                yOffset += recipeContainer.Height + 10; // Ajusta el espaciado entre contenedores de recetas
            }

            // Ajustar manualmente el tamaño del mInnerPanel
            mRecipePanel.SetInnerSize(mRecipePanel.Width, yOffset);

            // Actualizar las barras de desplazamiento después de añadir todos los elementos
            mRecipePanel.UpdateScrollBars();
        }

        public void Show()
        {
            if (SelectedJob == JobType.None || SelectedJob == JobType.JobCount)
            {
                InfoPanel.Hide();
            }
            ExpBackground?.Show();
            ExpBar?.Show();
            ExpLabel?.Show();
            ExpTitle?.Show();
            JobLevelLabel?.Show();
            JobNameLabel?.Show();
            JobDescriptionLabel?.Show();
            
            SelectedJob = JobType.None;
            IsHidden = false;
        }

        public void Hide()
        {
            SelectedJob = JobType.None;
            InfoPanel.Hide();
            ExpBackground.Hide();
            ExpBar.Hide();
            ExpLabel.Hide();
            ExpTitle.Hide();
            JobLevelLabel.Hide();
            JobNameLabel.Hide();
            JobDescriptionLabel.Hide();
            JobDescriptionLabel.ClearText();

            IsHidden = true;
        }

        public bool IsVisible()
        {
            return !IsHidden;
        }
        public void Update()
        {
            if (SelectedJob == JobType.None)
            {
                return; // No actualices si no hay un trabajo seleccionado
            }

            UpdateJobInfo(SelectedJob);
        }
    }

}
