using System.Collections.Generic;
using UnityEngine;

public enum InputState
{
    Down,
    Active,
    Up,
    Inactive
}
public struct OneInput
{
    public int mId;
    public Vector2 mPosition;
    public InputState mInputState;
}

public interface IInputController
{
    List<OneInput> GetActiveInputs();
    OneInput? GetMainInput();
}
