using UnityEngine;

public class WaitForTap : CustomYieldInstruction
{
	private bool isFirstFrame = true;

	public override bool keepWaiting
	{
		get
		{
			if (isFirstFrame)
			{
				isFirstFrame = false;
				return true;
			}
			return !Input.GetMouseButtonDown(0);
		}
	}
}
