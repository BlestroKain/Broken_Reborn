using System;
using System.IO;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Intersect.Server.Services;
using NUnit.Framework;

namespace Intersect.Tests.Server.Services;

[TestFixture]
public class AlignmentServiceTests
{
    [Test]
    public void TrySetAlignmentNeutralTurnsWingsOff()
    {
        Options.EnsureCreated();
        Directory.CreateDirectory("resources");

        var player = new Player
        {
            Id = Guid.NewGuid(),
            Faction = Factions.Chaos,
            Wings = WingState.Off,
        };

        using (var context = DbInterface.CreatePlayerContext(readOnly: false))
        {
            context.Database.EnsureCreated();
            context.Players.Add(player);
            context.SaveChanges();
        }

        AlignmentService.TrySetAlignment(
            player,
            Factions.Neutral,
            new AlignmentApplyOptions
            {
                IgnoreCooldown = true,
                IgnoreGuildLock = true,
            }
        );

        Assert.That(player.Wings, Is.EqualTo(WingState.Off));
    }
}
