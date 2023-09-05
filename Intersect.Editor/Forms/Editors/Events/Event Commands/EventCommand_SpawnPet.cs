using System;
using System.Linq;
using System.Windows.Forms;

using Intersect.Editor.Forms.Helpers;
using Intersect.Editor.Localization;
using Intersect.Enums;
using Intersect.GameObjects;
using Intersect.GameObjects.Events;
using Intersect.GameObjects.Events.Commands;
using Intersect.GameObjects.Maps;
using Intersect.GameObjects.Maps.MapList;

namespace Intersect.Editor.Forms.Editors.Events.Event_Commands
{

    public partial class EventCommandSpawnPet : UserControl
    {

        private readonly FrmEvent mEventEditor;

        private MapBase mCurrentMap;

        private EventBase mEditingEvent;

        private SpawnPetCommand mMyCommand;

        private int mSpawnX;

        private int mSpawnY;

        private Grid? mGrid;

        public EventCommandSpawnPet(
            FrmEvent eventEditor,
            MapBase currentMap,
            EventBase currentEvent,
            SpawnPetCommand editingCommand
        )
        {
            InitializeComponent();
            mMyCommand = editingCommand;
            mEventEditor = eventEditor;
            mEditingEvent = currentEvent;
            mCurrentMap = currentMap;
            InitLocalization();
            cmbPet.Items.Clear();
            cmbPet.Items.AddRange(PetBase.Names);
            cmbPet.SelectedIndex = PetBase.ListIndex(mMyCommand.PetId);
            

           
            UpdateFormElements();
          
                    mSpawnX = mMyCommand.X;
                    mSpawnY = mMyCommand.Y;
                    chkDirRelative.Checked = Convert.ToBoolean(mMyCommand.Dir);
                    UpdateSpawnPreview();

          
        }

        private void InitLocalization()
        {
            grpSpawnPet.Text = Strings.EventSpawnNpc.title;
            lblPet.Text = Strings.EventSpawnNpc.npc;
           
            grpEntitySpawn.Text = Strings.EventSpawnNpc.spawntype1;

            lblEntity.Text = Strings.EventSpawnNpc.entity;
            lblRelativeLocation.Text = Strings.EventSpawnNpc.relativelocation;
            chkDirRelative.Text = Strings.EventSpawnNpc.spawnrelative;

            btnSave.Text = Strings.EventSpawnNpc.okay;
            btnCancel.Text = Strings.EventSpawnNpc.cancel;
        }

        private void UpdateFormElements()
        {
            grpEntitySpawn.Show();
            //On/Around Entity Spawn
                    grpEntitySpawn.Show();
                    cmbEntities.Items.Clear();
                    cmbEntities.Items.Add(Strings.EventSpawnNpc.player);
                    cmbEntities.SelectedIndex = 0;

                    if (!mEditingEvent.CommonEvent)
                    {
                        foreach (var evt in mCurrentMap.LocalEvents)
                        {
                            cmbEntities.Items.Add(
                                evt.Key == mEditingEvent.Id ? Strings.EventSpawnNpc.This + " " : "" + evt.Value.Name
                            );

                            if (mMyCommand.EntityId == evt.Key)
                            {
                                cmbEntities.SelectedIndex = cmbEntities.Items.Count - 1;
                            }
                        }
                    }

                    UpdateSpawnPreview();

                  
            }
        

        private void UpdateSpawnPreview()
        {
            if (mGrid == null)
            {
                mGrid = new Grid
                {
                    DisplayWidth = pnlSpawnLoc.Width,
                    DisplayHeight = pnlSpawnLoc.Height,
                    Columns = 5,
                    Rows = 5,
                    Cells = new[] { new GridCell(2, 2, null, "E") }
                };
            }

            pnlSpawnLoc.BackgroundImage = GridHelper.DrawGrid(
                mGrid.Value.WithAdditionalCells(
                    new GridCell(mSpawnX + 2, mSpawnY + 2, System.Drawing.Color.Red)
                )
            );
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            mMyCommand.PetId = PetBase.IdFromList(cmbPet.SelectedIndex);
           
                //On/Around Entity Spawn
                    mMyCommand.MapId = Guid.Empty;
                    if (cmbEntities.SelectedIndex == 0 || cmbEntities.SelectedIndex == -1)
                    {
                        mMyCommand.EntityId = Guid.Empty;
                    }
                    else
                    {
                        mMyCommand.EntityId = mCurrentMap.LocalEvents.Keys.ToList()[cmbEntities.SelectedIndex - 1];
                    }

                    mMyCommand.X = (sbyte)mSpawnX;
                    mMyCommand.Y = (sbyte)mSpawnY;
                    mMyCommand.Dir = (Direction)Convert.ToInt32(chkDirRelative.Checked);


            mEventEditor.FinishCommandEdit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            mEventEditor.CancelCommandEdit();
        }

        private void cmbConditionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFormElements();
        }
              
        private void pnlSpawnLoc_MouseDown(object sender, MouseEventArgs e)
        {
            if (mGrid == null)
            {
                return;
            }

            var cell = GridHelper.CellFromPoint(mGrid.Value, e.X, e.Y);
            if (cell != null)
            {
                (mSpawnX, mSpawnY) = cell.Value;
                UpdateSpawnPreview();
            }
        }

    }

}
