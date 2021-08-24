using System;
using System.Collections.Generic;
using UnityEngine;

public class SingleSystemEventBus : IEventBus
{
    private struct CallInfo
    {
        public Call callback;
        public int eventHandle;
    }
    private Dictionary<Type, List<CallInfo>> mListeners = new Dictionary<Type, List<CallInfo>>();
    private static int mEventHandleCounter = 0;

    public int AddListener(Event e, Call callback)
    {
        var eventHandle = mEventHandleCounter;
        mEventHandleCounter++;

        if (mListeners.ContainsKey(e.GetType()))
        {
            mListeners.TryGetValue(e.GetType(), out var callbacks);
            callbacks.Add(new CallInfo { callback = callback, eventHandle = eventHandle });
        }
        else
        {
            List<CallInfo> callbackInfos = new List<CallInfo>();
            callbackInfos.Add(new CallInfo { callback = callback, eventHandle = eventHandle });
            mListeners.Add(e.GetType(), callbackInfos);
        }

        return eventHandle;
    }

    public void RemoveListener(int eventHandle)
    {
        foreach (var mapping in mListeners)
        {
            for (int i = 0; i < mapping.Value.Count; i++)
            {
                if (mapping.Value[i].eventHandle == eventHandle)
                {
                    mapping.Value.RemoveAt(i);
                    return;
                }
            }
        }

        Debug.LogAssertion("Could not remove listener for eventHandle " + eventHandle + " because there's no mapping for it");
    }

    public void PostMessage(Event e)
    {
        if (mListeners.ContainsKey(e.GetType()))
        {
            mListeners.TryGetValue(e.GetType(), out var callbackInfos);
            foreach (var callbackInfo in callbackInfos)
            {
                callbackInfo.callback?.Invoke(e);
            }
        }
    }

    public void ClearAllListeners()
    {
        mListeners.Clear();
    }
}
