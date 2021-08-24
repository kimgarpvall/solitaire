using System.Collections.Generic;
using UnityEngine;

public class MouseInputController : IInputController, IUpdater
{
    int mInputId = 0;
    OneInput mMouseInput;

    public MouseInputController()
    {
        mMouseInput = new OneInput {
            mPosition = Vector2.zero,
            mId = -1,
            mInputState = InputState.Inactive
        };
    }

    public void OnUpdate(float dt)
    {
        if(Input.GetMouseButtonDown(0))
        {
            // Input is starting
            mMouseInput.mPosition = Input.mousePosition;
            mMouseInput.mId = NextInputId();
            mMouseInput.mInputState = InputState.Down;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            // Input is about to finish
            mMouseInput.mInputState = InputState.Up;
        }
        else if(mMouseInput.mId != -1)
        {
            // Input is over
            if(mMouseInput.mInputState == InputState.Up)
            {
                mMouseInput.mId = -1;
                mMouseInput.mInputState = InputState.Inactive;
            }
            // Input is active
            else
            {
                mMouseInput.mPosition = Input.mousePosition;
                mMouseInput.mInputState = InputState.Active;
            }
            
        }
    }

    public List<OneInput> GetActiveInputs()
    {
        return mMouseInput.mInputState == InputState.Inactive ? new List<OneInput>() : new List<OneInput> { mMouseInput };
    }

    public OneInput? GetMainInput()
    {
        return mMouseInput.mInputState == InputState.Inactive ? (OneInput?)null : mMouseInput;
    }

    private int NextInputId()
    {
        var inputId = mInputId;
        mInputId++;
        return inputId;
    }
}
