using System;

namespace TouchScript.Gestures;

public class GestureStateChangeEventArgs : EventArgs
{
	public Gesture.GestureState PreviousState { get; private set; }

	public Gesture.GestureState State { get; private set; }

	public GestureStateChangeEventArgs(Gesture.GestureState state, Gesture.GestureState previousState)
	{
		State = state;
		PreviousState = previousState;
	}
}
