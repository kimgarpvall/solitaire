using System;
public interface IEventBus
{
    int AddListener(Event e, Call callback);
    void RemoveListener(int eventHandle);
    void PostMessage(Event e);
    void ClearAllListeners();
}
