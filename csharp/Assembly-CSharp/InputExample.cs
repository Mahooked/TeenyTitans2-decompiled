using TouchScript;
using UnityEngine;

public class InputExample : MonoBehaviour
{
	public GameObject Prefab;

	private void OnEnable()
	{
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += touchBeganHandler;
		}
	}

	private void OnDisable()
	{
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan -= touchBeganHandler;
		}
	}

	private void spawnPrefabAt(Vector2 position)
	{
		GameObject gameObject = Object.Instantiate(Prefab);
		gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(position.x, position.y, 10f));
	}

	private void touchBeganHandler(object sender, TouchEventArgs e)
	{
		foreach (ITouch touch in e.Touches)
		{
			spawnPrefabAt(touch.Position);
		}
	}
}
