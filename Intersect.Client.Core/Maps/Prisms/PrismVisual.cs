using System;
using System.Linq;
using Intersect.Client.Framework.GenericClasses;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.General;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Animations;
using Intersect.Framework.Core.GameObjects.Prisms;
using Intersect.Utilities;
using Intersect.Client.Maps;
using Color = Microsoft.Xna.Framework.Color;

namespace Intersect.Client.Maps.Prisms;

public class PrismVisual
{
    private readonly Guid _id;
    private readonly bool _tintByFaction;
    private readonly int _spriteOffsetY;
    private readonly AnimationDescriptor? _idle;
    private readonly AnimationDescriptor? _vulnerable;
    private readonly AnimationDescriptor? _underAttack;

    private long _stateStart = Timing.Global.Milliseconds;
    private FloatRect _bounds;

    public PrismVisual(PrismDescriptor descriptor)
    {
        _id = descriptor.Id;
        _tintByFaction = descriptor.TintByFaction;
        _spriteOffsetY = descriptor.SpriteOffsetY;
        if (descriptor.IdleAnimationId.HasValue && descriptor.IdleAnimationId != Guid.Empty)
        {
            _idle = AnimationDescriptor.Get(descriptor.IdleAnimationId.Value);
        }

        if (descriptor.VulnerableAnimationId.HasValue && descriptor.VulnerableAnimationId != Guid.Empty)
        {
            _vulnerable = AnimationDescriptor.Get(descriptor.VulnerableAnimationId.Value);
        }

        if (descriptor.UnderAttackAnimationId.HasValue && descriptor.UnderAttackAnimationId != Guid.Empty)
        {
            _underAttack = AnimationDescriptor.Get(descriptor.UnderAttackAnimationId.Value);
        }
    }

    public Guid Id => _id;
    public Guid MapId { get; private set; } = Guid.Empty;
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool TintByFaction => _tintByFaction;
    public int SpriteOffsetY => _spriteOffsetY;

    public Factions Owner { get; private set; } = Factions.Neutral;
    public PrismState State { get; private set; } = PrismState.Placed;
    public int Hp { get; private set; }
    public int MaxHp { get; private set; }

    private AnimationDescriptor? DescriptorForState => State switch
    {
        PrismState.UnderAttack => _underAttack ?? _vulnerable ?? _idle,
        PrismState.Vulnerable => _vulnerable ?? _idle,
        _ => _idle,
    };

    public void Update(Guid mapId, int x, int y, Factions owner, PrismState state, int hp, int maxHp)
    {
        MapId = mapId;
        X = x;
        Y = y;
        Owner = owner;
        Hp = hp;
        MaxHp = maxHp;
        if (State != state)
        {
            State = state;
            _stateStart = Timing.Global.Milliseconds;
        }
    }

    private static Color GetFactionColor(Factions faction) => faction switch
    {
        Factions.Serolf => Color.Blue,
        Factions.Nidraj => Color.Red,
        _ => Color.White,
    };

    public void Draw(MapInstance map)
    {
        var descriptor = DescriptorForState;
        if (descriptor == null)
        {
            return;
        }

        _bounds = FloatRect.Empty;

        DrawLayer(map, descriptor.Lower);
        DrawLayer(map, descriptor.Upper);

        DrawHp();
    }

    private void DrawLayer(MapInstance map, AnimationLayer layer)
    {
        if (string.IsNullOrWhiteSpace(layer.Sprite))
        {
            return;
        }

        var tex = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Animation, layer.Sprite);
        if (tex == null)
        {
            return;
        }

        var frameCount = Math.Max(1, layer.FrameCount);
        var xFrames = Math.Max(1, layer.XFrames);
        var yFrames = Math.Max(1, layer.YFrames);
        var frameSpeed = Math.Max(1, layer.FrameSpeed);
        var frame = (int)((Timing.Global.Milliseconds - _stateStart) / frameSpeed) % frameCount;

        var frameWidth = tex.Width / xFrames;
        var frameHeight = tex.Height / yFrames;

        var worldX = map.X + X * Options.Instance.Map.TileWidth + Options.Instance.Map.TileWidth / 2;
        var worldY = map.Y + Y * Options.Instance.Map.TileHeight + Options.Instance.Map.TileHeight / 2 + SpriteOffsetY;

        var src = new FloatRect(
            frame % xFrames * frameWidth,
            (float)Math.Floor(frame / (double)xFrames) * frameHeight,
            frameWidth,
            frameHeight
        );

        var dest = new FloatRect(worldX - frameWidth / 2f, worldY - frameHeight / 2f, frameWidth, frameHeight);

        var color = TintByFaction ? GetFactionColor(Owner) : Color.White;
        Graphics.DrawGameTexture(tex, src, dest, color);

        if (_bounds.Width == 0 && _bounds.Height == 0)
        {
            _bounds = dest;
        }
        else
        {
            _bounds = FloatRect.FromLtrb(
                Math.Min(_bounds.Left, dest.Left),
                Math.Min(_bounds.Top, dest.Top),
                Math.Max(_bounds.Right, dest.Right),
                Math.Max(_bounds.Bottom, dest.Bottom)
            );
        }
    }

    private void DrawHp()
    {
        if (MaxHp <= 0)
        {
            return;
        }

        var hpBackground = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Misc, "hpbackground.png");
        var hpForeground = Globals.ContentManager.GetTexture(Framework.Content.TextureType.Misc, "hpbar.png");
        var bounding = GameTexture.GetBoundingTexture(BoundsComparison.Height, hpBackground, hpForeground);

        var x = (int)Math.Ceiling(_bounds.Left + _bounds.Width / 2f);
        var y = (int)Math.Ceiling(_bounds.Top) - 2 - bounding.Height / 2;

        var hpFillWidth = hpForeground != null && MaxHp > 0
            ? (int)(hpForeground.Width * (Hp / (float)MaxHp))
            : 0;

        if (hpBackground != null)
        {
            Graphics.DrawGameTexture(
                hpBackground,
                new FloatRect(0, 0, hpBackground.Width, hpBackground.Height),
                new FloatRect(x - hpBackground.Width / 2f, y - hpBackground.Height / 2f, hpBackground.Width, hpBackground.Height),
                Color.White
            );
        }

        if (hpForeground != null)
        {
            Graphics.DrawGameTexture(
                hpForeground,
                new FloatRect(0, 0, hpFillWidth, hpForeground.Height),
                new FloatRect(x - hpForeground.Width / 2f, y - hpForeground.Height / 2f, hpFillWidth, hpForeground.Height),
                Color.White
            );
        }
    }

    public bool HitTest(int worldX, int worldY) => _bounds.Contains(worldX, worldY);
}

