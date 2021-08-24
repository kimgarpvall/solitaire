public enum CommandUndoBehaviour
{
    AllowUndo,
    DoNotAllowUndo
}

public delegate Command ExecuteCallback(Event e);
public abstract class Command
{
    public abstract CommandUndoBehaviour Execute();
    public abstract void Redo();
    public abstract void Undo();
}
