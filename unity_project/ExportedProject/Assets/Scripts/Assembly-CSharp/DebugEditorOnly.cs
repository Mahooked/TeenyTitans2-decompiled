using UnityEngine;

public class DebugEditorOnly : MonoBehaviour
{
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}
}
