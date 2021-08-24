using System;
using System.Collections.Generic;

public class Dependencies
{
    // Singleton
    private static Dependencies mSingleton;
    public static Dependencies Get()
    {
        if (mSingleton == null)
        {
            mSingleton = new Dependencies();
        }

        return mSingleton;
    }

    // Implementation
    private Dictionary<Type, object> mDependencies = new Dictionary<Type, object>();
    public void Register<T>(object dependency)
    {
        mDependencies.Add(typeof(T), dependency);
    }

    public void Clear()
    {
        mDependencies.Clear();
    }

    public T Get<T>()
    {
        if(mDependencies.ContainsKey(typeof(T)))
        {
            mDependencies.TryGetValue(typeof(T), out object t);
            return (T)t;
        }
        else
        {
            return default(T);
        }
    }
}
