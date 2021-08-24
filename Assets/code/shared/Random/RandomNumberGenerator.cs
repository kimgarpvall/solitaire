using System;
using System.Collections.Generic;

public class RandomNumberGenerator
{
    private Random mRandom;
    public RandomNumberGenerator(int seed)
    {
        mRandom = new Random(seed);
    }

    public int GetInRange(int low, int high)
    {
        return mRandom.Next(low, high);
    }

    public int Next(int n)
    {
        return mRandom.Next(n);
    }
}


