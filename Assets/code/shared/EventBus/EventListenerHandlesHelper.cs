using System.Collections.Generic;

public class EventListenerHandlesHelper
{
    private readonly IEventBus mEventBus;
    private List<int> mListenerHandles = new List<int>();

    public EventListenerHandlesHelper(IEventBus eventBus)
    {
        mEventBus = eventBus;
    }

    ~EventListenerHandlesHelper()
    {
        ClearAllHandles();
    }

    public void AddHandle(int handle)
    {
        mListenerHandles.Add(handle);
    }
    public void ClearAllHandles()
    {
        foreach(var handle in mListenerHandles)
        {
            mEventBus.RemoveListener(handle);
        }
        mListenerHandles.Clear();
    }
}
