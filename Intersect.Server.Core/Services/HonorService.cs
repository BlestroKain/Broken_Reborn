using System;

namespace Intersect.Server.Core.Services;

public static class HonorService
{
    public const int MinHonor = -5000;
    public const int MaxHonor = 5000;

    // Bracket table defining honor ranges, decay amounts, and grades.
    private static readonly (int Min, int Max, int Decay, int Grade)[] Brackets =
    {
        (MinHonor, -4000, 500, -5),
        (-3999, -3000, 400, -4),
        (-2999, -2000, 300, -3),
        (-1999, -1000, 200, -2),
        (-999, -1, 100, -1),
        (0, 0, 0, 0),
        (1, 999, 100, 1),
        (1000, 1999, 200, 2),
        (2000, 2999, 300, 3),
        (3000, 3999, 400, 4),
        (4000, MaxHonor, 500, 5)
    };

    public static int Clamp(int honor)
    {
        return Math.Clamp(honor, MinHonor, MaxHonor);
    }

    public static int CalculateGrade(int honor)
    {
        foreach (var bracket in Brackets)
        {
            if (honor >= bracket.Min && honor <= bracket.Max)
            {
                return bracket.Grade;
            }
        }

        return 0;
    }

    public static int DecayTowardsZero(int honor)
    {
        if (honor == 0)
        {
            return 0;
        }

        var abs = Math.Abs(honor);
        var decay = 0;
        foreach (var bracket in Brackets)
        {
            var minAbs = Math.Abs(bracket.Min);
            var maxAbs = Math.Abs(bracket.Max);
            if (abs >= minAbs && abs <= maxAbs)
            {
                decay = bracket.Decay;
                break;
            }
        }

        if (honor > 0)
        {
            honor = Math.Max(0, honor - decay);
        }
        else
        {
            honor = Math.Min(0, honor + decay);
        }

        return honor;
    }
}