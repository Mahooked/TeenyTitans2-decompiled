using UnityEngine;

public class WaitForInactive : CustomYieldInstruction
{
	private GameObject _go;

	public override bool keepWaiting => _go.activeSelf;

	public WaitForInactive(GameObject go)
	{
		_go = go;
	}
}
