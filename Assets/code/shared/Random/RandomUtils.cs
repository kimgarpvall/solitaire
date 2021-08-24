﻿using System.Collections.Generic;

public class RandomUtils
{
    public static void Shuffle<T>(IList<T> list, RandomNumberGenerator rng)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}