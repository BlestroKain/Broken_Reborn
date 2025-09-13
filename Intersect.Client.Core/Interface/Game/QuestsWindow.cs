using System;
using System.Collections.Generic;
using System.Linq;
using Intersect.Client.Core;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.General;
using Intersect.Client.Interface.Shared;
using Intersect.Client.Localization;
using Intersect.Client.Networking;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Framework.Core.GameObjects.NPCs;
using Intersect.Framework.Core.GameObjects.Quests;
using Intersect.GameObjects;
using Intersect.Utilities;

namespace Intersect.Client.Interface.Game
{
    public partial class QuestsWindow : IQuestWindow
    {

        private readonly ScrollControl mQuestDescArea;
        private readonly RichLabel mQuestDescLabel;
        private readonly Label mQuestDescTemplateLabel;

        private readonly ListBox _questList;
        private readonly Label mQuestStatus;

        private readonly ScrollControl mQuestTasksContainer;
        private readonly ListBox mQuestTasksList;

        // Window + título
        private readonly WindowControl mQuestsWindow;
        private readonly Label mQuestTitle;

        private readonly Button mQuitButton;

        // Contenedor raíz de recompensas (ya existía)
        private readonly ScrollControl _rewardContainer;

        // NUEVO: sub-contenedores para recompensas
        private readonly ScrollControl _rewardItemsContainer;
        private readonly ScrollControl _rewardExpContainer;

        private QuestDescriptor mSelectedQuest;

        // Helpers de layout recompensas
        private const int RewardPaddingX = 10;
        private const int RewardPaddingY = 10;
        private const int RewardSpacing = 3;
        private const int RewardExpHeight = 40;
        // --- arriba de la clase (campos nuevos) ---
        private readonly Label _taskTemplateLabel;   // Para heredar font/estilo desde JSON si existe

        // Colores sugeridos (ajústalos a tus CustomColors si quieres)
        private static readonly Color TaskColorPending = new Color(220, 220, 220, 255);
        private static readonly Color TaskColorActive = new Color(255, 230, 110, 255);  // ámbar
        private static readonly Color TaskColorDone = new Color(120, 230, 120, 255);  // verde

        // Tamaño/spacing de filas
        private const int TaskRowHeight = 22;
        private const int TaskIconSize = 22;
        public QuestsWindow(Canvas gameCanvas)
        {
            mQuestsWindow = new WindowControl(gameCanvas, Strings.QuestLog.Title, false, "QuestsWindow");
            mQuestsWindow.DisableResizing();

            _questList = new ListBox(mQuestsWindow, "QuestList");
            _questList.EnableScroll(false, true);

            mQuestTitle = new Label(mQuestsWindow, "QuestTitle");
            mQuestTitle.SetText("");

            mQuestStatus = new Label(mQuestsWindow, "QuestStatus");
            mQuestStatus.SetText("");

            mQuestDescArea = new ScrollControl(mQuestsWindow, "QuestDescription");
            mQuestDescTemplateLabel = new Label(mQuestDescArea, "QuestDescriptionTemplate");
            mQuestDescLabel = new RichLabel(mQuestDescArea);

            mQuestTasksContainer = new ScrollControl(mQuestsWindow, "QuestTasksContainer");
            mQuestTasksContainer.EnableScroll(false, false);
            mQuestTasksList = new ListBox(mQuestTasksContainer, "QuestTasksList");
            mQuestTasksList.EnableScroll(false, true);
            mQuestTasksList.Dock = Pos.Fill;
            mQuestTasksList.Margin = new Margin(0, 0, 0, 0);
            _taskTemplateLabel = new Label(mQuestTasksContainer, "QuestTaskTemplate");
            // Si no existe en JSON, le damos defaults amables:
            if (_taskTemplateLabel.Font == null)
            {
                // Si tu build permite GetFont por nombre, úsalo; si no, hereda del template de descripción:
                // _taskTemplateLabel.SetFont(GameContentManager.Current.GetFont("sourcesans", 12));
                _taskTemplateLabel.Font = mQuestDescTemplateLabel.Font;
            }
            _taskTemplateLabel.SetTextColor(TaskColorPending, ComponentState.Normal);
            _rewardContainer = new ScrollControl(mQuestsWindow, "QuestRewardContainer");

            // Intentamos tomar sub-controles del JSON por nombre; si no existen, los creamos
            _rewardExpContainer = TryGetOrCreate(_rewardContainer, "QuestRewardExpContainer", 10, 10, 380, RewardExpHeight);
            _rewardItemsContainer = TryGetOrCreate(_rewardContainer, "QuestRewardItemContainer", 10, 60, 380, 50);


            mQuitButton = new Button(mQuestsWindow, "AbandonQuestButton");
            mQuitButton.SetText(Strings.QuestLog.Abandon);
            mQuitButton.Clicked += _quitButton_Clicked;

            mQuestsWindow.LoadJsonUi(GameContentManager.UI.InGame, Graphics.Renderer.GetResolutionString());

            // Override JSON raro
            _questList.IsDisabled = false;
            _questList.IsVisibleInTree = true;

            // Inicial oculto por defecto
            _rewardExpContainer.IsHidden = true;
            _rewardItemsContainer.IsHidden = true;
            _rewardContainer.IsHidden = true;
        }

        private static ScrollControl TryGetOrCreate(ScrollControl parent, string name, int x, int y, int w, int h)
        {
            // Busca un hijo con ese name; si no hay, crea uno
            var child = parent.FindChildByName(name) as ScrollControl;
            if (child == null)
            {
                child = new ScrollControl(parent, name);
                child.SetPosition(x, y);
                child.SetSize(w, h);
                child.EnableScroll(false, true);
            }

            return child;
        }

        private void _quitButton_Clicked(Base sender, MouseButtonState arguments)
        {
            if (mSelectedQuest != null)
            {
                _ = new InputBox(
                    title: Strings.QuestLog.AbandonTitle.ToString(mSelectedQuest.Name),
                    prompt: Strings.QuestLog.AbandonPrompt.ToString(mSelectedQuest.Name),
                    inputType: InputType.YesNo,
                    userData: mSelectedQuest.Id,
                    onSubmit: (s, e) =>
                    {
                        if (s is InputBox inputBox && inputBox.UserData is Guid questId)
                        {
                            PacketSender.SendAbandonQuest(questId);
                            Globals.RemoveQuestRewards(questId);
                            // Limpia visual
                            ClearRewardWidgets();
                            UpdateQuestList();
                            UpdateSelectedQuest();
                            UpdateQuestTasks();
                        }
                    }
                );
            }
        }

        private bool _shouldUpdateList;
        // Cache de recompensas creadas (para medir/posicionar)
        private readonly List<Base> _rewardItemWidgets = new();
        private readonly List<Base> _rewardExpWidgets = new();

        public void Update(bool shouldUpdateList)
        {
            if (!mQuestsWindow.IsVisibleInTree)
            {
                _shouldUpdateList |= shouldUpdateList;
                return;
            }

            UpdateInternal(shouldUpdateList);
        }

        private void UpdateInternal(bool shouldUpdateList)
        {
            if (shouldUpdateList)
            {
                UpdateQuestList();
                UpdateSelectedQuest();
            }

            if (mQuestsWindow.IsHidden)
            {
                return;
            }

            if (mSelectedQuest != null)
            {
                UpdateQuestTasks();

                if (Globals.Me.QuestProgress.ContainsKey(mSelectedQuest.Id))
                {
                    if (Globals.Me.QuestProgress[mSelectedQuest.Id].Completed &&
                        Globals.Me.QuestProgress[mSelectedQuest.Id].TaskId == Guid.Empty)
                    {
                        if (!mSelectedQuest.LogAfterComplete)
                        {
                            mSelectedQuest = null;
                            UpdateSelectedQuest();
                        }

                        return;
                    }
                    else
                    {
                        if (Globals.Me.QuestProgress[mSelectedQuest.Id].TaskId == Guid.Empty)
                        {
                            if (!mSelectedQuest.LogBeforeOffer)
                            {
                                mSelectedQuest = null;
                                UpdateSelectedQuest();
                            }
                        }

                        return;
                    }
                }

                if (!mSelectedQuest.LogBeforeOffer)
                {
                    mSelectedQuest = null;
                    UpdateSelectedQuest();
                }
            }
        }

        private void UpdateQuestList()
        {
            _questList.RemoveAllRows();
            if (Globals.Me == null)
            {
                return;
            }

            var quests = QuestDescriptor.Lookup.Values;
            var dict = new Dictionary<string, List<Tuple<QuestDescriptor, int, Color>>>();

            foreach (QuestDescriptor quest in quests)
            {
                if (quest != null)
                {
                    AddQuestToDict(dict, quest);
                }
            }

            foreach (var category in Options.Instance.Quest.Categories)
            {
                if (dict.ContainsKey(category))
                {
                    AddCategoryToList(category, Color.White);
                    var sortedList = dict[category].OrderBy(l => l.Item2).ThenBy(l => l.Item1.OrderValue).ToList();
                    foreach (var qst in sortedList)
                    {
                        AddQuestToList(qst.Item1.Name, qst.Item3, qst.Item1.Id, true);
                    }
                }
            }

            if (dict.ContainsKey(string.Empty))
            {
                var sortedList = dict[string.Empty].OrderBy(l => l.Item2).ThenBy(l => l.Item1.OrderValue).ToList();
                foreach (var qst in sortedList)
                {
                    AddQuestToList(qst.Item1.Name, qst.Item3, qst.Item1.Id, false);
                }
            }
        }

        private void AddQuestToDict(Dictionary<string, List<Tuple<QuestDescriptor, int, Color>>> dict, QuestDescriptor quest)
        {
            var category = string.Empty;
            var add = false;
            var color = Color.White;
            var orderVal = -1;

            if (Globals.Me.QuestProgress.ContainsKey(quest.Id))
            {
                if (Globals.Me.QuestProgress[quest.Id].TaskId != Guid.Empty)
                {
                    add = true;
                    category = !TextUtils.IsNone(quest.InProgressCategory) ? quest.InProgressCategory : "";
                    color = CustomColors.QuestWindow.InProgress;
                    orderVal = 1;
                }
                else
                {
                    if (Globals.Me.QuestProgress[quest.Id].Completed)
                    {
                        if (quest.LogAfterComplete)
                        {
                            add = true;
                            category = !TextUtils.IsNone(quest.CompletedCategory) ? quest.CompletedCategory : "";
                            color = CustomColors.QuestWindow.Completed;
                            orderVal = 3;
                        }
                    }
                    else if (quest.LogBeforeOffer && !Globals.Me.HiddenQuests.Contains(quest.Id))
                    {
                        add = true;
                        category = !TextUtils.IsNone(quest.UnstartedCategory) ? quest.UnstartedCategory : "";
                        color = CustomColors.QuestWindow.NotStarted;
                        orderVal = 2;
                    }
                }
            }
            else if (quest.LogBeforeOffer && !Globals.Me.HiddenQuests.Contains(quest.Id))
            {
                add = true;
                category = !TextUtils.IsNone(quest.UnstartedCategory) ? quest.UnstartedCategory : "";
                color = CustomColors.QuestWindow.NotStarted;
                orderVal = 2;
            }

            if (!add) return;

            if (!dict.ContainsKey(category))
            {
                dict.Add(category, new List<Tuple<QuestDescriptor, int, Color>>());
            }

            dict[category].Add(new Tuple<QuestDescriptor, int, Color>(quest, orderVal, color));
        }

        private void AddQuestToList(string name, Color clr, Guid questId, bool indented = true)
        {
            var item = _questList.AddRow((indented ? "\t\t\t" : "") + name);
            item.UserData = questId;
            item.Clicked += QuestListItem_Clicked;
            item.Selected += Item_Selected;
            item.SetTextColor(clr);
            item.RenderColor = new Color(50, 255, 255, 255);
            item.SetSize(200, 25);
        }

        private void AddCategoryToList(string name, Color clr)
        {
            var item = _questList.AddRow(name);
            item.MouseInputEnabled = false;
            item.SetTextColor(clr);
            item.RenderColor = new Color(0, 255, 255, 255);
            item.SetSize(200, 25);
        }

        private void Item_Selected(Base sender, ItemSelectedEventArgs arguments)
        {
            _questList.UnselectAll();
        }

        private void QuestListItem_Clicked(Base sender, MouseButtonState arguments)
        {
            if (sender.UserData is not Guid questId)
            {
                return;
            }

            if (!QuestDescriptor.TryGet(questId, out var questDescriptor))
            {
                _questList.UnselectAll();
                return;
            }

            mSelectedQuest = questDescriptor;
            UpdateSelectedQuest();
            UpdateQuestTasks();
        }

        private void UpdateSelectedQuest()
        {
            _questList.Show();

            if (mSelectedQuest == null)
            {
                mQuestTitle.Hide();
                mQuestDescArea.Hide();
                mQuestStatus.Hide();
                mQuitButton.Hide();
                ClearRewardWidgets();
                return;
            }

            mQuestDescLabel.ClearText();
            mQuitButton.IsDisabled = true;

            if (Globals.Me.QuestProgress.ContainsKey(mSelectedQuest.Id))
            {
                if (Globals.Me.QuestProgress[mSelectedQuest.Id].TaskId != Guid.Empty)
                {
                    // En progreso
                    mQuestStatus.SetText(Strings.QuestLog.InProgress);
                    mQuestStatus.SetTextColor(CustomColors.QuestWindow.InProgress, ComponentState.Normal);
                    mQuestDescTemplateLabel.SetTextColor(CustomColors.QuestWindow.QuestDesc, ComponentState.Normal);

                    if (mSelectedQuest.InProgressDescription.Length > 0)
                    {
                        mQuestDescLabel.AddText(mSelectedQuest.InProgressDescription, mQuestDescTemplateLabel);
                        mQuestDescLabel.AddLineBreak();
                        mQuestDescLabel.AddLineBreak();
                    }

                    mQuestDescLabel.AddText(Strings.QuestLog.CurrentTask, mQuestDescTemplateLabel);
                    mQuestDescLabel.AddLineBreak();

                    for (var i = 0; i < mSelectedQuest.Tasks.Count; i++)
                    {
                        if (mSelectedQuest.Tasks[i].Id == Globals.Me.QuestProgress[mSelectedQuest.Id].TaskId)
                        {
                            if (mSelectedQuest.Tasks[i].Description.Length > 0)
                            {
                                mQuestDescLabel.AddText(mSelectedQuest.Tasks[i].Description, mQuestDescTemplateLabel);
                                mQuestDescLabel.AddLineBreak();
                                mQuestDescLabel.AddLineBreak();
                            }

                            if (mSelectedQuest.Tasks[i].Objective == QuestObjective.GatherItems)
                            {
                                mQuestDescLabel.AddText(
                                    Strings.QuestLog.TaskItem.ToString(
                                        Globals.Me.QuestProgress[mSelectedQuest.Id].TaskProgress,
                                        mSelectedQuest.Tasks[i].Quantity,
                                        ItemDescriptor.GetName(mSelectedQuest.Tasks[i].TargetId)
                                    ),
                                    mQuestDescTemplateLabel
                                );
                            }
                            else if (mSelectedQuest.Tasks[i].Objective == QuestObjective.KillNpcs)
                            {
                                mQuestDescLabel.AddText(
                                    Strings.QuestLog.TaskNpc.ToString(
                                        Globals.Me.QuestProgress[mSelectedQuest.Id].TaskProgress,
                                        mSelectedQuest.Tasks[i].Quantity,
                                        NPCDescriptor.GetName(mSelectedQuest.Tasks[i].TargetId)
                                    ),
                                    mQuestDescTemplateLabel
                                );
                            }
                        }
                    }

                    mQuitButton.IsDisabled = !mSelectedQuest.Quitable;
                }
                else
                {
                    if (Globals.Me.QuestProgress[mSelectedQuest.Id].Completed)
                    {
                        if (mSelectedQuest.LogAfterComplete)
                        {
                            mQuestStatus.SetText(Strings.QuestLog.Completed);
                            mQuestStatus.SetTextColor(CustomColors.QuestWindow.Completed, ComponentState.Normal);
                            mQuestDescLabel.AddText(mSelectedQuest.EndDescription, mQuestDescTemplateLabel);
                        }
                    }
                    else
                    {
                        if (mSelectedQuest.LogBeforeOffer)
                        {
                            mQuestStatus.SetText(Strings.QuestLog.NotStarted);
                            mQuestStatus.SetTextColor(CustomColors.QuestWindow.NotStarted, ComponentState.Normal);
                            mQuestDescLabel.AddText(mSelectedQuest.BeforeDescription, mQuestDescTemplateLabel);
                            mQuitButton?.Hide();
                        }
                    }
                }
            }
            else
            {
                if (mSelectedQuest.LogBeforeOffer)
                {
                    mQuestStatus.SetText(Strings.QuestLog.NotStarted);
                    mQuestStatus.SetTextColor(CustomColors.QuestWindow.NotStarted, ComponentState.Normal);
                    mQuestDescLabel.AddText(mSelectedQuest.BeforeDescription, mQuestDescTemplateLabel);
                }
            }

            // Mostrar
 
            mQuestTitle.IsHidden = false;
            mQuestTitle.Text = mSelectedQuest.Name;
            mQuestDescArea.IsHidden = false;
            mQuestDescLabel.Width = mQuestDescArea.Width - mQuestDescArea.VerticalScrollBar.Width;
            mQuestDescLabel.SizeToChildren(false, true);
            mQuestStatus.Show();
            mQuitButton.Show();

            // Cargar recompensas de esta quest (ítems + exp) y acomodar
            LoadRewardWidgets(mSelectedQuest.Id);
        }

        public void Show()
        {
            if (_shouldUpdateList)
            {
                UpdateInternal(_shouldUpdateList);
                _shouldUpdateList = false;
            }

            mQuestsWindow.IsHidden = false;
        }

        public bool IsVisible() => !mQuestsWindow.IsHidden;

        public void Hide()
        {
            mQuestsWindow.IsHidden = true;
            mSelectedQuest = null;
        }

        // ---------- Recompensas: API de IQuestWindow ----------
        // Reemplaza TODO el método por esto:
        public void AddRewardWidget(Base widget)
        {
            if (widget == null) return;

            var n = widget.Name ?? string.Empty;

            // Marcamos por nombre (los widgets que te paso crean "RewardExpChip" y "RewardItem")
            var goesToExp = n.Equals("RewardExpChip", StringComparison.OrdinalIgnoreCase) ||
                            n.StartsWith("RewardExp", StringComparison.OrdinalIgnoreCase);

            if (goesToExp)
            {
                widget.Parent = _rewardExpContainer;
                _rewardExpWidgets.Add(widget);
            }
            else
            {
                widget.Parent = _rewardItemsContainer;
                _rewardItemWidgets.Add(widget);
            }

            // NO vuelvas a cargar JSON aquí; ya lo hizo el widget. (evita pisar tamaños)
            // widget.LoadJsonUi(...);  // <- quítalo
            widget.Show();
        }
     public void ClearRewardWidgets()
        {
            // Quitar hijos visibles y limpiar cache
            ClearChildren(_rewardItemsContainer);
            ClearChildren(_rewardExpContainer);
            _rewardItemWidgets.Clear();
            _rewardExpWidgets.Clear();

            _rewardItemsContainer.IsHidden = true;
            _rewardExpContainer.IsHidden = true;
            _rewardContainer.IsHidden = true;
        }


        private static void ClearChildren(Base container)
        {
            // Seguro para builds donde no existe DeleteChildren/DeleteAllChildren
            var kids = container.Children?.ToArray();
            if (kids == null) return;
            foreach (var c in kids)
            {
                container.RemoveChild(c, true);
            }
        }

        // ---------- Recompensas: Carga & layout ----------
        private void LoadRewardWidgets(Guid questId)
        {
            ClearRewardWidgets();

            // --- EXP ---
            Globals.QuestExperience.TryGetValue(questId, out var playerExp);
            Globals.QuestJobExperience.TryGetValue(questId, out Dictionary<JobType, long>? jobExp);
            Globals.QuestGuildExperience.TryGetValue(questId, out var guildExp);
            Globals.QuestFactionHonor.TryGetValue(questId, out Dictionary<Factions, int>? factionHonor);

            var anyExp =
                (playerExp > 0) ||
                (jobExp != null && jobExp.Count > 0) ||
                (guildExp > 0) ||
                (factionHonor != null && factionHonor.Count > 0);

            // Aseguramos posición base del EXP
            _rewardExpContainer.SetPosition(RewardPaddingX, RewardPaddingY);

            if (anyExp)
            {
                // Crea los chips (esto llenará _rewardExpWidgets vía AddRewardWidget)
                _ = new QuestRewardExp(this, playerExp, jobExp, guildExp, factionHonor);

                // Layout en fila
                const int minW = 100;
                const int spacing = 6;
                var x = 0;

                foreach (var chip in _rewardExpWidgets)
                {
                    var w = Math.Max(chip.Width, minW);
                    chip.SetPosition(x, 0);
                    x += w + spacing;
                }

                _rewardExpContainer.SetSize(Math.Max(x - spacing, 1), RewardExpHeight);
                _rewardExpContainer.IsHidden = _rewardExpWidgets.Count == 0;
            }
            else
            {
                _rewardExpContainer.IsHidden = true;
                _rewardExpContainer.SetSize(1, 1);
            }

            // --- ÍTEMS ---
            if (Globals.QuestRewards.TryGetValue(questId, out var rewards) && rewards.Count > 0)
            {
                foreach (var kv in rewards)
                {
                    _ = new QuestRewardItem(this, kv.Key, kv.Value);
                }
                _rewardItemsContainer.IsHidden = _rewardItemWidgets.Count == 0;
            }
            else
            {
                _rewardItemsContainer.IsHidden = true;
            }

            // Si no hay nada, escondemos raíz
            _rewardContainer.IsHidden = _rewardExpContainer.IsHidden && _rewardItemsContainer.IsHidden;
            if (_rewardContainer.IsHidden) return;

            // EXP arriba, ÍTEMS debajo
            var itemsY = _rewardExpContainer.IsHidden
                ? RewardPaddingY
                : RewardPaddingY + _rewardExpContainer.Height + RewardSpacing;

            _rewardItemsContainer.SetPosition(RewardPaddingX, itemsY);

            // --- Grilla de ítems ---
            if (!_rewardItemsContainer.IsHidden && _rewardItemWidgets.Count > 0)
            {
                // Ancho disponible dentro del contenedor raíz (menos padding)
                var availW = Math.Max(_rewardContainer.Width - 2 * RewardPaddingX, 1);
                _rewardItemsContainer.SetSize(availW, _rewardItemsContainer.Height);

                var first = _rewardItemWidgets[0];
                var itemW = Math.Max(first.Width + first.Margin.Left + first.Margin.Right, 40);
                var itemH = Math.Max(first.Height + first.Margin.Top + first.Margin.Bottom, 40);

                const int xPad = 10, yPad = 10;
                var perRow = Math.Max(availW / Math.Max(itemW, 1), 1);

                for (int i = 0; i < _rewardItemWidgets.Count; i++)
                {
                    var x = (i % perRow) * itemW + xPad;
                    var y = (i / perRow) * itemH + yPad;
                    _rewardItemWidgets[i].SetPosition(x, y);
                    _rewardItemWidgets[i].Show();
                }

                var rows = (int)Math.Ceiling(_rewardItemWidgets.Count / (double)perRow);
                _rewardItemsContainer.SetSize(availW, rows * itemH + yPad);
            }

            // Alto total del contenedor raíz
            var totalH = RewardPaddingY;
            if (!_rewardExpContainer.IsHidden) totalH += _rewardExpContainer.Height + RewardSpacing;
            if (!_rewardItemsContainer.IsHidden) totalH += _rewardItemsContainer.Height;
            totalH += RewardPaddingY;

            _rewardContainer.EnableScroll(false, true); // por si el contenido crece
            _rewardContainer.SetSize(_rewardContainer.Width, totalH);
        }

        // ---------- Tareas ----------
        private bool IsTaskCompleted(QuestTaskDescriptor task)
        {
            if (mSelectedQuest == null || Globals.Me?.QuestProgress == null)
            {
                return false;
            }

            if (!Globals.Me.QuestProgress.TryGetValue(mSelectedQuest.Id, out var progress))
            {
                return false;
            }

            if (progress.Completed)
            {
                return true;
            }

            var currentIndex = mSelectedQuest.GetTaskIndex(progress.TaskId);
            var taskIndex = mSelectedQuest.GetTaskIndex(task.Id);

            return currentIndex > taskIndex;
        }

        private int GetTaskProgress(QuestTaskDescriptor task)
        {
            if (mSelectedQuest == null || Globals.Me?.QuestProgress == null)
            {
                return 0;
            }

            if (!Globals.Me.QuestProgress.TryGetValue(mSelectedQuest.Id, out var progress))
            {
                return 0;
            }

            var currentIndex = mSelectedQuest.GetTaskIndex(progress.TaskId);
            var taskIndex = mSelectedQuest.GetTaskIndex(task.Id);

            if (progress.Completed || currentIndex > taskIndex)
            {
                return task.Quantity;
            }

            if (currentIndex == taskIndex)
            {
                return progress.TaskProgress;
            }

            return 0;
        }
        private static void ApplyTaskStyle(Label label, Label template, Color color)
        {
            if (template?.Font != null)
            {
                label.Font = template.Font; // o label.SetFont(template.Font) según tu build
            }

            label.SetTextColor(color, ComponentState.Normal);

            // Altura consistente; el ListBox maneja el ancho
            int h = Math.Max(TaskRowHeight - 2, 12);
            label.Height = h;

        }

        private void UpdateQuestTasks()
        {
            mQuestTasksList.RemoveAllRows();

            if (mSelectedQuest == null || mSelectedQuest.Tasks.Count == 0)
            {
                mQuestTasksContainer.Hide();
                return;
            }

            mQuestTasksContainer.Show();

            // Ubicamos el índice de la tarea actual para formateo
            int currentIndex = -1;
            bool questCompleted = false;

            if (Globals.Me?.QuestProgress != null &&
                Globals.Me.QuestProgress.TryGetValue(mSelectedQuest.Id, out var prog))
            {
                questCompleted = prog.Completed;
                currentIndex = mSelectedQuest.GetTaskIndex(prog.TaskId);
            }

            for (int i = 0; i < mSelectedQuest.Tasks.Count; i++)
            {
                var task = mSelectedQuest.Tasks[i];

                bool completed = IsTaskCompleted(task);
                int progress = GetTaskProgress(task);
                bool isCurrent = !questCompleted && (currentIndex == i);

                // Texto amigable
                var desc = task.Description;
                switch (task.Objective)
                {
                    case QuestObjective.GatherItems:
                        desc = Strings.QuestLog.TaskItem.ToString(
                            progress, task.Quantity, ItemDescriptor.GetName(task.TargetId));
                        break;

                    case QuestObjective.KillNpcs:
                        desc = Strings.QuestLog.TaskNpc.ToString(
                            progress, task.Quantity, NPCDescriptor.GetName(task.TargetId));
                        break;
                }

                // Fila
                var row = mQuestTasksList.AddRow(string.Empty);
                // Altura fija; el ancho lo maneja el ListBox
                row.Height = TaskRowHeight;

                var icon = new ImagePanel(row) { Width = TaskIconSize, Height = TaskIconSize };
              

                var texture = GameContentManager.Current.GetTexture(
                    Framework.Content.TextureType.Gui,
                    completed ? "checkboxfull.png" : "checkboxempty.png"
                );
                if (texture != null) icon.Texture = texture;
                icon.SetPosition(2, (TaskRowHeight - TaskIconSize) / 2);

                // Label
                var lbl = new Label(row) { Text = "• " + desc };
                lbl.SetPosition(2 + TaskIconSize + 4, 1);

                // Aplica estilo (ya sin lambda)
                ApplyTaskStyle(lbl, _taskTemplateLabel, completed ? TaskColorDone : (isCurrent ? TaskColorActive : TaskColorPending));

            }

            mQuestTasksList.Invalidate();
        }

    }
}
