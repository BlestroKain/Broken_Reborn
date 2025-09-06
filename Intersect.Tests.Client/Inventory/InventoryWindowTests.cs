using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Intersect;
using Intersect.Client.Interface.Game.Inventory;
using Intersect.Client.Framework.File_Management;
using Intersect.Client.Framework.Gwen.Control;
using Intersect.Client.General;
using Intersect.Framework.Core.GameObjects.Items;
using Intersect.Client.Framework.Graphics;
using Intersect.Client.Framework.Content;
using NUnit.Framework;
using System.IO;
using Intersect.Client.Localization;
using Microsoft.Extensions.Logging;
using Intersect.Core;
using Intersect.Plugins.Interfaces;
using Intersect.Threading;

namespace Intersect.Tests.Client.Inventory;

[TestFixture]
public class InventoryWindowTests
{
    private sealed class StubRenderer : Intersect.Client.Framework.Gwen.Renderer.Base
    {
        public StubRenderer() : base()
        {
        }
    }

    private sealed class StubSkin : Intersect.Client.Framework.Gwen.Skin.Base
    {
        public StubSkin() : base(new StubRenderer())
        {
        }
    }

    private sealed class StubGameContentManager : GameContentManager
    {
        public StubGameContentManager() : base()
        {
        }

        public override IFont? GetFont(string? name) => null;

        public override void LoadTexturePacks() { }
        public override void LoadTilesets(string[] tilesetnames) { }
        public override void LoadItems() { }
        public override void LoadEntities() { }
        public override void LoadSpells() { }
        public override void LoadAnimations() { }
        public override void LoadFaces() { }
        public override void LoadImages() { }
        public override void LoadFogs() { }
        public override void LoadResources() { }
        public override void LoadPaperdolls() { }
        public override void LoadGui() { }
        public override void LoadMisc() { }
        public override void LoadFonts() { }
        public override void LoadShaders() { }
        public override void LoadGuild() { }
        public override void LoadSounds() { }
        public override void LoadMusic() { }
        protected override TAsset Load<TAsset>(Dictionary<string, IAsset> assetDict, ContentType type, string name, Func<Stream> loadMethod) => default!;
    }

    private sealed class DummyLogger : ILogger
    {
        public IDisposable BeginScope<TState>(TState state) => null!;
        public bool IsEnabled(LogLevel logLevel) => false;
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter) { }
    }

    private sealed class DummyApplicationContext : IApplicationContext
    {
        public void Dispose() { }
        public bool HasErrors => false;
        public bool IsDisposed => false;
        public bool IsStarted => true;
        public bool IsRunning => true;
        public string Name => "Test";
        public ICommandLineOptions StartupOptions => null!;
        public ILogger Logger { get; } = new DummyLogger();
        public IPacketHelper PacketHelper => null!;
        public List<IApplicationService> Services { get; } = new();
        public TApplicationService GetService<TApplicationService>() where TApplicationService : IApplicationService => default!;
        public void Start(bool lockUntilShutdown = true) { }
        public ILockingActionQueue StartWithActionQueue() => null!;
    }

    [Test]
    public void SubtypeComboFiltersByType()
    {
        var ensure = typeof(Options).GetMethod("EnsureCreated", BindingFlags.NonPublic | BindingFlags.Static);
        ensure!.Invoke(null, null);
        Options.Instance.Items.ItemSubtypes = new Dictionary<ItemType, List<string>>
        {
            { ItemType.Equipment, new() { "Sword", "Bow" } },
            { ItemType.Consumable, new() { "Drink", "Food" } },
        };

        _ = new StubGameContentManager();

        Globals.Me = null;

        ApplicationContext.Context.Value = new DummyApplicationContext();

        var canvas = new Canvas(new StubSkin(), "canvas");
        var window = new InventoryWindow(canvas);

        var populate = typeof(InventoryWindow).GetMethod(
            "PopulateSubtypeComboForType",
            BindingFlags.Instance | BindingFlags.NonPublic
        );

        var subtypeField = typeof(InventoryWindow).GetField(
            "_subtypeBox",
            BindingFlags.Instance | BindingFlags.NonPublic
        );
        var combo = (ComboBox)subtypeField!.GetValue(window)!;

        var menuField = typeof(ComboBox).GetField(
            "_menu",
            BindingFlags.Instance | BindingFlags.NonPublic
        );
        var menu = (Menu)menuField!.GetValue(combo)!;

        populate!.Invoke(window, new object?[] { ItemType.Equipment });
        var labels = menu.Children.OfType<MenuItem>().Select(mi => mi.Text).ToList();
        Assert.That(labels.TakeLast(3), Is.EqualTo(new[] { Strings.Inventory.All.ToString(), "Sword", "Bow" }));
        Assert.That(combo.SelectedItem?.UserData, Is.Null);

        populate.Invoke(window, new object?[] { ItemType.Consumable });
        labels = menu.Children.OfType<MenuItem>().Select(mi => mi.Text).ToList();
        Assert.That(labels.TakeLast(3), Is.EqualTo(new[] { Strings.Inventory.All.ToString(), "Drink", "Food" }));
        Assert.That(combo.SelectedItem?.UserData, Is.Null);
    }
}

