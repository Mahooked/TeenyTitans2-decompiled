using UnityEngine;

public class WaitForInactive : CustomYieldInstruction
{
	private GameObject _go;

	public override bool keepWaiting
	{
		get
		{
			return _go.activeSelf;
		}
	}

	public WaitForInactive(GameObject go)
	{
		_go = go;
	}
}
