using UnityEngine;

public class AndroidBackListener : MonoBehaviour
{
	public static float lastUsed;

	public static bool lastInputNotUsed;

	public static bool inputDirty;

	public bool highPriotity;

	public int priorityNumber;

	private bool paused;

	private void OnEnable()
	{
		PauseManager.instance.Paused += HandleOnPaused;
		paused = PauseManager.instance.isPaused;
	}

	private void OnDisable()
	{
		PauseManager.instance.Paused -= HandleOnPaused;
	}

	private void HandleOnPaused(bool isPaused)
	{
		paused = isPaused;
	}

	private void Start()
	{
	}

	private bool isButtonAvailable(AndroidBackListener button)
	{
		bool flag = false;
		if ((bool)button.gameObject.GetComponent<BoxCollider>() && button.gameObject.GetComponent<tk2dUIItem>().enabled)
		{
			flag = button.gameObject.GetComponent<BoxCollider>().enabled;
		}
		if ((bool)button.gameObject.GetComponent<BoxCollider2D>() && button.gameObject.GetComponent<tk2dUIItem>().enabled)
		{
			flag = button.gameObject.GetComponent<BoxCollider2D>().enabled;
		}
		if (button.gameObject.transform.lossyScale.x < 0.01f)
		{
			flag = false;
		}
		if (button.gameObject.activeInHierarchy && flag)
		{
			return true;
		}
		return false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !inputDirty && (Time.time - lastUsed > 0.01f || paused))
		{
			lastUsed = Time.time;
			lastInputNotUsed = false;
			inputDirty = true;
		}
		if (!inputDirty)
		{
			return;
		}
		AndroidBackListener[] array = Object.FindObjectsOfType<AndroidBackListener>();
		int num = 0;
		AndroidBackListener[] array2 = array;
		foreach (AndroidBackListener androidBackListener in array2)
		{
			if (isButtonAvailable(androidBackListener) && androidBackListener.priorityNumber > num)
			{
				num = androidBackListener.priorityNumber;
			}
		}
		AndroidBackListener[] array3 = array;
		foreach (AndroidBackListener androidBackListener2 in array3)
		{
			if (androidBackListener2.highPriotity && androidBackListener2.priorityNumber == num && isButtonAvailable(androidBackListener2) && inputDirty)
			{
				inputDirty = false;
				lastInputNotUsed = false;
				androidBackListener2.gameObject.GetComponent<tk2dUIItem>().SimulateClick();
			}
		}
		AndroidBackListener[] array4 = array;
		foreach (AndroidBackListener androidBackListener3 in array4)
		{
			if (isButtonAvailable(androidBackListener3) && inputDirty)
			{
				inputDirty = false;
				lastInputNotUsed = false;
				androidBackListener3.gameObject.GetComponent<tk2dUIItem>().SimulateClick();
			}
		}
		if (inputDirty)
		{
			lastInputNotUsed = true;
		}
		else
		{
			lastInputNotUsed = false;
		}
		inputDirty = false;
	}
}
