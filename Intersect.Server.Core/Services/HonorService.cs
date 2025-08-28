using System;
using Intersect.Server.Entities;

namespace Intersect.Server.Services;

internal static class HonorService
{
    private static readonly int[] GradeThresholds = { 0, 1000, 2000, 4000, 8000 };

    public static void AdjustHonor(Player player, int amount)
    {
        if (player == null || amount == 0)
        {
            return;
        }

        player.Honor += amount;
        if (player.Honor < 0)
        {
            player.Honor = 0;
        }

        RecalculateGrade(player);
    }

    public static void RecalculateGrade(Player player)
    {
        var newGrade = 0;
        for (var i = 0; i < GradeThresholds.Length; i++)
        {
            if (player.Honor >= GradeThresholds[i])
            {
                newGrade = i;
            }
            else
            {
                break;
            }
        }

        player.Grade = newGrade;
    }
}
