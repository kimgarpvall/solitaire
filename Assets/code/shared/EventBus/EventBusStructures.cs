// All events inherit this
public class Event { }
// But how does the callback work then? PostMessage calls it and sends along the event
public delegate void Call(Event e);