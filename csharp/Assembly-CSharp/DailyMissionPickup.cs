using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DailyMissionPickup : MonoBehaviour
{
	public string guid = string.Empty;

	public List<string> tags = new List<string>();

	private Collider _touchCollider;

	public Collider touchCollider
	{
		get
		{
			if (_touchCollider == null)
			{
				_touchCollider = GetComponent<Collider>();
			}
			return _touchCollider;
		}
	}

	private void Awake()
	{
	}

	private void OnDestroy()
	{
	}
}
