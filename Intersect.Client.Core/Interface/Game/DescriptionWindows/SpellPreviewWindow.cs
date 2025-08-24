using System;
using System.Linq;
using System.Reflection;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Client.Localization;
using Intersect.Framework.Core.GameObjects.Spells;
using Intersect.GameObjects;
using Intersect.Enums;

namespace Intersect.Client.Interface.Game.DescriptionWindows;

public class SpellPreviewWindow : SpellDescriptionWindow
{
    private static readonly FieldInfo? DescField = typeof(SpellDescriptionWindow).GetField("_spellDescriptor", BindingFlags.Instance | BindingFlags.NonPublic);
    private static readonly FieldInfo? PropsField = typeof(SpellDescriptionWindow).GetField("_spellProperties", BindingFlags.Instance | BindingFlags.NonPublic);
    private static readonly FieldInfo? EffectiveField = typeof(SpellDescriptionWindow).GetField("_effectiveProps", BindingFlags.Instance | BindingFlags.NonPublic);

    private int _previewLevel;
    private int _currentLevel;

    public SpellPreviewWindow(Base parent)
        : base()
    {
        Parent = parent;
        IsVisibleInParent = false;
    }

    public void ShowPreview(Guid spellId, int level)
    {
        _previewLevel = level;
        _currentLevel = Globals.Me?.Spells.FirstOrDefault(s => s.Id == spellId)?.Properties?.Level ?? 1;

        var desc = SpellDescriptor.Get(spellId);
        if (desc != null)
        {
            var props = desc.BuildEffectiveProperties(level);
            DescField?.SetValue(this, desc);
            PropsField?.SetValue(this, props);
            EffectiveField?.SetValue(this, props);
        }
        else
        {
            DescField?.SetValue(this, null);
            PropsField?.SetValue(this, null);
            EffectiveField?.SetValue(this, null);
        }

        SetupDescriptionWindow();
        base.Show();
    }

    protected override void SetupHeader()
    {
        var desc = (SpellDescriptor?)DescField?.GetValue(this);
        if (desc == null)
        {
            return;
        }

        var header = AddHeader();

        var tex = GameContentManager.Current.GetTexture(Framework.Content.TextureType.Spell, desc.Icon);
        if (tex != null)
        {
            header.SetIcon(tex, Color.White);
        }

        header.SetTitle(desc.Name, Color.White);
        Strings.SpellDescription.SpellTypes.TryGetValue((int)desc.SpellType, out var spellType);
        header.SetSubtitle(spellType, Color.White);

        var text = _previewLevel > _currentLevel
            ? $"Nivel necesario: {_previewLevel}"
            : $"Nivel {_previewLevel}";
        var color = _previewLevel > _currentLevel ? new Color(170, 170, 170, 255) : Color.White;
        header.SetDescription(text, color);

        header.SizeToChildren(true, false);
    }
}

