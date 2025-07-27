using System.Diagnostics;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Control.EventArguments;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Client.Networking;
using Intersect.Configuration;
using Intersect.Framework.Core.GameObjects.Items;

namespace Intersect.Client.Interface.Game.Character;

public partial class EquipmentItem
{
    public List<ImagePanel> ContentPanels = new();

    private WindowControl mCharacterWindow;

    private List<Guid> mCurrentItemIds = new();

    private ItemProperties mItemProperties = null;

    private string? _loadedTexture;

    private int mYindex;

    public ImagePanel Pnl;

    public EquipmentItem(int index, WindowControl characterWindow)
    {
        mYindex = index;
        mCharacterWindow = characterWindow;
    }

    public void Setup()
    {
        Pnl.HoverEnter += pnl_HoverEnter;
        Pnl.HoverLeave += pnl_HoverLeave;
        Pnl.Clicked += pnl_RightClicked;

        var max = Options.Instance.Equipment.EquipmentSlots[mYindex].MaxItems;
        for (var i = 0; i < max; i++)
        {
            var panel = new ImagePanel(Pnl, "EquipmentIcon");
            panel.MouseInputEnabled = false;
            panel.Margin = new Margin(i * 20, 0, 0, 0);
            ContentPanels.Add(panel);
        }
        Pnl.SetToolTipText(Options.Instance.Equipment.Slots[mYindex]);
    }

    void pnl_RightClicked(Base sender, MouseButtonState arguments)
    {
        if (arguments.MouseButton != MouseButton.Right)
        {
            return;
        }

        if (ClientConfiguration.Instance.EnableContextMenus)
        {
            var window = Interface.GameUi.GameMenu.GetInventoryWindow();
            if (window != null)
            {
                var invSlot = Globals.Me.MyEquipment[mYindex];
                if (invSlot >= 0 && invSlot < Options.Instance.Player.MaxInventory)
                {
                    window.OpenContextMenu(invSlot);
                }
            }
        }
        else
        {
            PacketSender.SendUnequipItem(mYindex);
        }

    }

    void pnl_HoverLeave(Base sender, EventArgs arguments)
    {
        Interface.GameUi.ItemDescriptionWindow?.Hide();
    }

    void pnl_HoverEnter(Base sender, EventArgs arguments)
    {
        if (InputHandler.MouseFocus != null)
        {
            return;
        }

        if (Globals.InputManager.IsMouseButtonDown(MouseButton.Left))
        {
            return;
        }

        var item = mCurrentItemIds.Count > 0 ? ItemDescriptor.Get(mCurrentItemIds[0]) : null;
        if (item == null)
        {
            return;
        }

        Interface.GameUi.ItemDescriptionWindow?.Show(item, 1, mItemProperties);
    }

    public FloatRect RenderBounds()
    {
        var rect = new FloatRect()
        {
            X = Pnl.ToCanvas(new Point(0, 0)).X,
            Y = Pnl.ToCanvas(new Point(0, 0)).Y,
            Width = Pnl.Width,
            Height = Pnl.Height
        };

        return rect;
    }

    public void Update(List<Guid> itemIds, List<ItemProperties> properties)
    {
        mCurrentItemIds = itemIds;
        for (var i = 0; i < ContentPanels.Count; i++)
        {
            if (i >= itemIds.Count || !ItemDescriptor.TryGet(itemIds[i], out var item))
            {
                ContentPanels[i].Hide();
                continue;
            }

            if (GameContentManager.Current.GetTexture(Framework.Content.TextureType.Item, item.Icon) is not { } itemTexture)
            {
                ContentPanels[i].Hide();
                continue;
            }

            ContentPanels[i].Show();
            ContentPanels[i].Texture = itemTexture;
            ContentPanels[i].RenderColor = item.Color;
        }
    }
}
