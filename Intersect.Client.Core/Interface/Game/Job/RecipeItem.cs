using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Gwen.Input;
using Intersect.Client.Framework.Input;
using Intersect.Client.General;
using Intersect.Framework.Core.GameObjects.Crafting;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.GameObjects;

namespace Intersect.Client.Interface.Game.Job;

public partial class RecipeItem
{
    public ImagePanel? Container;
    private readonly CraftingRecipeIngredient _ingredient;
    private readonly JobsWindow _jobsWindow;
    public ImagePanel? Pnl;

    public RecipeItem(JobsWindow jobsWindow, CraftingRecipeIngredient ingredient)
    {
        _jobsWindow = jobsWindow;
        _ingredient = ingredient;
    }

    public void Setup(string name)
    {
        Pnl = new ImagePanel(Container, name);
        Pnl.HoverEnter += Pnl_HoverEnter;
        Pnl.HoverLeave += Pnl_HoverLeave;
    }

    public void LoadItem()
    {
        if (!ItemDescriptor.TryGet(_ingredient.ItemId, out var item))
        {
            Pnl!.Texture = null;
            return;
        }

        var itemTex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Item, item.Icon);
        if (itemTex != null)
        {
            Pnl!.Texture = itemTex;
            Pnl.RenderColor = item.Color;
        }
        else
        {
            Pnl!.Texture = null;
        }
    }

    private void Pnl_HoverLeave(Base sender, EventArgs arguments)
    {
        Interface.GameUi.ItemDescriptionWindow?.Hide();
    }

    private void Pnl_HoverEnter(Base sender, EventArgs arguments)
    {
        if (InputHandler.MouseFocus != null)
        {
            return;
        }

        if (Globals.InputManager.IsMouseButtonDown(MouseButton.Left))
        {
            return;
        }

        if (ItemDescriptor.TryGet(_ingredient.ItemId, out var item))
        {
            Interface.GameUi.ItemDescriptionWindow?.Show(item, _ingredient.Quantity);
        }
    }
}
