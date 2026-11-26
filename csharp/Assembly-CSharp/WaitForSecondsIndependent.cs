using UnityEngine;

public class WaitForSecondsIndependent : CustomYieldInstruction
{
	private float _waitingTime;

	public override bool keepWaiting
	{
		get
		{
			_waitingTime -= Time.unscaledDeltaTime;
			return _waitingTime > 0f;
		}
	}

	public WaitForSecondsIndependent(float seconds)
	{
		_waitingTime = seconds;
	}
}
