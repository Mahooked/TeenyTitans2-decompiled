using UnityEngine;

[ExecuteInEditMode]
[DisallowMultipleComponent]
public class NewGUID : MonoBehaviour
{
	[SerializeField]
	private string _guid;

	public string guid
	{
		get
		{
			return _guid;
		}
	}

	private void OnEnable()
	{
	}
}
