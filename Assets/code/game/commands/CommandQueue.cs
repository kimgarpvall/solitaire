using System;
using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : IInitialiser
{
    private EventBus mEventBus;
    private EventListenerHandlesHelper mEventListener;

    private List<Command> mQueue = new List<Command>();
    private int mQueueIndex = -1;

    // IInitialiser
    public void OnInit()
    {
        mEventBus = Dependencies.Get().Get<EventBus>();
        mEventListener = new EventListenerHandlesHelper(mEventBus);
        Reset();
    }

    // CommandQueue
    public void MapCommand(Event e, ExecuteCallback callback)
    {
        mEventListener.AddHandle(mEventBus.AddListener(e, (Event ev) => {
            var command = callback(ev);
            if (command.Execute() == CommandUndoBehaviour.AllowUndo)
            {
                // Remove commands ahead of the queue index
                if(mQueueIndex > -1 && mQueueIndex < mQueue.Count)
                {
                    var toRemove = mQueue.Count - mQueueIndex;
                    mQueue.RemoveRange(mQueueIndex, toRemove);
                }

                mQueue.Add(command);
                mQueueIndex = mQueue.Count;
            }

            // Update view
            mEventBus.PostMessage(new SetupDeckViewEvent());
        }));
    }

    public void Undo()
    {
        mQueueIndex--;

        // Have we moved all the way back to before the first command? If yes - stay there and don't do anything
        if (mQueueIndex <= -1)
        {
            mQueueIndex = 0;
            return;
        }

        // Undo the command
        mQueue[mQueueIndex].Undo();

        // Update view
        mEventBus.PostMessage(new SetupDeckViewEvent());
    }
	public void Redo()
    {
        // Have we moved all the way to final index? If yes - stay there and don't do anything
        if (mQueueIndex >= mQueue.Count)
        {
            mQueueIndex = mQueue.Count;
            return;
        }

        // Redo the current command
        mQueue[mQueueIndex].Redo();

        // Update the index to point to the next command OR the index AFTER the last command (mQueue.Count)
        mQueueIndex++;

        // Update view
        mEventBus.PostMessage(new SetupDeckViewEvent());
    }

    public void Reset()
    {
        mQueueIndex = -1;
        mQueue.Clear();
    }
}
