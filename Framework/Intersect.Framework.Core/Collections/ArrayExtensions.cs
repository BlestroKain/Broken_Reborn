using System;

namespace Intersect.Collections;

public static partial class ArrayExtensions
{
    public static T[] EnsureLen<T>(T[]? array, int length)
    {
        if (array?.Length == length)
        {
            return array;
        }

        var newArray = new T[length];
        if (array is { Length: > 0 })
        {
            Array.Copy(array, newArray, Math.Min(array.Length, length));
        }

        return newArray;
    }
}

