using UnityEngine;

public class UI_KillMe : MonoBehaviour
{
	public void KillMe()
	{
		Object.Destroy(transform.parent.gameObject);
	}
}
