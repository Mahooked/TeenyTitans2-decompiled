using TouchScript.Gestures;

namespace TouchScript;

public interface IGestureDelegate
{
	bool ShouldReceiveTouch(Gesture gesture, ITouch touch);

	bool ShouldBegin(Gesture gesture);

	bool ShouldRecognizeSimultaneously(Gesture first, Gesture second);
}
