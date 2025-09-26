using System;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Pets;
using Intersect.Server.Entities;
using Intersect.Server.Maps;
using Intersect.Server.Networking;
using NUnit.Framework;

namespace Intersect.Tests.Server.Entities;

[TestFixture]
public class PetTests
{
    private Guid _mapId;

    [SetUp]
    public void Setup()
    {
        Options.EnsureCreated();

        _mapId = Guid.NewGuid();
        MapController.Lookup.Set(_mapId, new MapController(_mapId));
    }

    private static Player CreateOwner(Guid mapId)
    {
        return new Player
        {
            MapId = mapId,
            MapInstanceId = Guid.NewGuid(),
            X = 0,
            Y = 0,
            Z = 0,
            Dir = Direction.Down,
        };
    }

    [Test]
    public void CalculateAttackTime_UsesStaticValueWhenModifierIndicatesStatic()
    {
        var descriptor = new PetDescriptor(Guid.NewGuid())
        {
            Name = "Static Pet",
            AttackSpeedModifier = 1,
            AttackSpeedValue = 123,
        };

        var owner = CreateOwner(_mapId);

        var pet = new Pet(descriptor, owner);

        Assert.That(pet.CalculateAttackTime(), Is.EqualTo(descriptor.AttackSpeedValue));
    }

    [Test]
    public void CalculateAttackTime_UsesBaseCalculationWhenModifierIsNotStatic()
    {
        var descriptor = new PetDescriptor(Guid.NewGuid())
        {
            Name = "Dynamic Pet",
            AttackSpeedModifier = 0,
            AttackSpeedValue = 5,
        };

        descriptor.Stats[(int)Stat.Agility] = 10;

        var owner = CreateOwner(_mapId);

        var pet = new Pet(descriptor, owner);

        var expected = (int)(Options.Instance.Combat.MaxAttackRate +
            (Options.Instance.Combat.MinAttackRate - Options.Instance.Combat.MaxAttackRate) *
            (((float)Options.Instance.Player.MaxStat - pet.Stat[(int)Stat.Agility].Value()) /
             Options.Instance.Player.MaxStat));

        Assert.That(pet.CalculateAttackTime(), Is.EqualTo(expected));
    }

    [Test]
    public void Despawn_KillDespawnable_SendsSingleEntityLeavePacket()
    {
        var descriptor = new PetDescriptor(Guid.NewGuid())
        {
            Name = "Leave Counter",
        };

        var owner = CreateOwner(_mapId);
        MapController.Get(_mapId).TryCreateInstance(owner.MapInstanceId, out _, owner);

        var pet = new Pet(descriptor, owner);

        var leaveCount = 0;
        var leaveAfterDeath = false;
        void Handler(Entity entity)
        {
            leaveCount++;
            leaveAfterDeath = entity.IsDead;
        }

        try
        {
            PacketSender.EntityLeaveSent += Handler;
            pet.Despawn(killIfDespawnable: true);
        }
        finally
        {
            PacketSender.EntityLeaveSent -= Handler;
        }

        Assert.That(
            leaveCount,
            Is.EqualTo(1),
            "Pet despawn should emit exactly one leave packet when kill cleanup is requested."
        );

        Assert.That(leaveAfterDeath, Is.True, "Pet should already be dead when the leave packet is emitted.");
    }
}
