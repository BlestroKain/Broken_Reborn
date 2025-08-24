using System;
using Intersect.Client.Framework.Content;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.Framework.Graphics;
using Newtonsoft.Json.Linq;

namespace Intersect.Client.Interface.Game.DescriptionWindows.Components;

public partial class SetItemComponent : ComponentBase
{
    private readonly ImagePanel _icon;
    private readonly ImagePanel _status;

    private string _ownedIcon = "set_check.png";
    private string _missingIcon = "set_x.png";

    public SetItemComponent(Base parent, string name = "SetItem") : base(parent, name)
    {
        _icon = new ImagePanel(this, "Icon");
        _status = new ImagePanel(this, "Status");
    }

    public override JObject? GetJson(bool isRoot = false, bool onlySerializeIfNotEmpty = false)
    {
        var serialized = base.GetJson(isRoot, onlySerializeIfNotEmpty);
        serialized?.Add(nameof(_ownedIcon), _ownedIcon);
        serialized?.Add(nameof(_missingIcon), _missingIcon);
        return serialized;
    }

    public override void LoadJson(JToken token, bool isRoot = false)
    {
        base.LoadJson(token, isRoot);

        if (token is not JObject obj)
        {
            return;
        }

        if (obj[nameof(_ownedIcon)] is JValue { Type: JTokenType.String } owned)
        {
            _ownedIcon = owned.Value<string>() ?? _ownedIcon;
        }

        if (obj[nameof(_missingIcon)] is JValue { Type: JTokenType.String } missing)
        {
            _missingIcon = missing.Value<string>() ?? _missingIcon;
        }

        _status.Texture = GameContentManager.Current.GetTexture(TextureType.Misc, _missingIcon);
    }

    public void SetIcon(IGameTexture texture, Color color)
    {
        _icon.Texture = texture;
        _icon.RenderColor = color;
        _icon.SizeToContents();
        SetSize(_icon.Width, _icon.Height);
        Align.Center(_icon);
    }

    public void SetStatus(bool owned)
    {
        var textureName = owned ? _ownedIcon : _missingIcon;
        var texture = GameContentManager.Current.GetTexture(TextureType.Misc, textureName);
        _status.Texture = texture;
        _status.RenderColor = owned ? Color.Green : Color.Red;

        if (texture != null)
        {
            _status.SizeToContents();
        }

        SetSize(Math.Max(_icon.Width, _status.Width), Math.Max(_icon.Height, _status.Height));
        Align.Center(_status);
    }

    public override void CorrectWidth()
    {

    }
}
