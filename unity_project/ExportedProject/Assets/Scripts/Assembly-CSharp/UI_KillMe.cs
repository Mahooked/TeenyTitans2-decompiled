using UnityEngine;

public class UI_KillMe : MonoBehaviour
{
	public void KillMe()
	{
		Object.Destroy(base.transform.parent.gameObject);
	}
}
