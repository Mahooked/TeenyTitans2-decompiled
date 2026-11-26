using UnityEngine;

public class Coroutiner : MonoBehaviour
{
	private static Coroutiner _instance;

	public static Coroutiner instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("Coroutiner").AddComponent<Coroutiner>();
			}
			return _instance;
		}
	}

	private void Awake()
	{
		Object.DontDestroyOnLoad(base.gameObject);
	}
}
