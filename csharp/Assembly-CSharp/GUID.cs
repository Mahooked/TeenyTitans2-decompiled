using System;
using UnityEngine;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class GUID : MonoBehaviour
{
	[SerializeField]
	private string guid = string.Empty;

	public string ID
	{
		get
		{
			return guid;
		}
		set
		{
			guid = value;
		}
	}

	private void Awake()
	{
		if (string.IsNullOrEmpty(guid))
		{
			GenerateNewGUID();
		}
	}

	public void GenerateNewGUID()
	{
		ID = Guid.NewGuid().ToString();
	}
}
