using System;
using System.Linq;
using System.Threading.Tasks;
using Intersect.Config;
using Intersect.Enums;
using Intersect.Framework.Core.GameObjects.Variables;
using Intersect.Server.Database;
using Intersect.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace Intersect.Server.Core.Services;

internal static class HonorDecayService
{
    private const string LastDecayVariableName = "HonorDecayLastRun";

    public static async Task TryRunAsync()
    {
        var now = DateTime.UtcNow;
        var lastRun = GetLastRun();
        if ((now - lastRun).TotalDays < 7)
        {
            return;
        }

        var decayPercent = Options.Instance.Player.HonorDecayPercent;
        if (decayPercent <= 0)
        {
            SetLastRun(now);
            return;
        }

        using var context = DbInterface.CreatePlayerContext(readOnly: false);
        var players = await context.Players.ToListAsync().ConfigureAwait(false);
        var fraction = decayPercent / 100.0;
        foreach (var player in players)
        {
            if (player.Honor == 0)
            {
                continue;
            }

            var reduction = (int)Math.Round(player.Honor * fraction);
            player.Honor -= reduction;
        }

        await context.SaveChangesAsync().ConfigureAwait(false);

        foreach (var online in Player.OnlinePlayers)
        {
            if (online == null || online.Honor == 0)
            {
                continue;
            }

            var reduction = (int)Math.Round(online.Honor * fraction);
            online.Honor -= reduction;
        }

        SetLastRun(now);
    }

    private static DateTime GetLastRun()
    {
        var variable = ServerVariableDescriptor.Lookup.Values
            .OfType<ServerVariableDescriptor>()
            .FirstOrDefault(v => v.Name == LastDecayVariableName);

        if (variable == null)
        {
            variable = new ServerVariableDescriptor(Guid.NewGuid())
            {
                Name = LastDecayVariableName,
                DataType = VariableDataType.String,
                ValueData = DateTime.MinValue.ToString("o")
            };
            ServerVariableDescriptor.Lookup.Set(variable.Id, variable);
            DbInterface.UpdatedServerVariables.TryAdd(variable.Id, variable);
            return DateTime.MinValue;
        }

        return DateTime.TryParse(variable.Value.String, out var parsed)
            ? parsed
            : DateTime.MinValue;
    }

    private static void SetLastRun(DateTime time)
    {
        var variable = ServerVariableDescriptor.Lookup.Values
            .OfType<ServerVariableDescriptor>()
            .FirstOrDefault(v => v.Name == LastDecayVariableName);
        if (variable == null)
        {
            return;
        }

        variable.Value.String = time.ToString("o");
        DbInterface.UpdatedServerVariables.AddOrUpdate(variable.Id, variable, (id, old) => variable);
    }
}

