using UnityEngine;

namespace Crosstales.UI;

public class WindowManager : MonoBehaviour
{
	[Tooltip("Window movement speed (default: 3).")]
	public float Speed = 3f;

	[Tooltip("Dependent GameObjects (active == open).")]
	public GameObject[] Dependencies;

	private UIFocus focus;

	private bool open;

	private bool close;

	private Vector3 startPos;

	private Vector3 centerPos;

	private Vector3 lerpPos;

	private float openProgress;

	private float closeProgress;

	private GameObject panel;

	public void Start()
	{
		panel = transform.Find("Panel").gameObject;
		startPos = transform.position;
		ClosePanel();
		panel.SetActive(value: false);
		if (Dependencies != null)
		{
			GameObject[] dependencies = Dependencies;
			foreach (GameObject gameObject in dependencies)
			{
				gameObject.SetActive(value: false);
			}
		}
	}

	public void Update()
	{
		centerPos = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
		if (open && openProgress < 1f)
		{
			openProgress += Speed * Time.deltaTime;
			transform.position = Vector3.Lerp(lerpPos, centerPos, openProgress);
		}
		else
		{
			if (!close)
			{
				return;
			}
			if (closeProgress < 1f)
			{
				closeProgress += Speed * Time.deltaTime;
				transform.position = Vector3.Lerp(lerpPos, startPos, closeProgress);
				return;
			}
			panel.SetActive(value: false);
			if (Dependencies != null)
			{
				GameObject[] dependencies = Dependencies;
				foreach (GameObject gameObject in dependencies)
				{
					gameObject.SetActive(value: false);
				}
			}
		}
	}

	public void SwitchPanel()
	{
		if (open)
		{
			ClosePanel();
		}
		else
		{
			OpenPanel();
		}
	}

	public void OpenPanel()
	{
		panel.SetActive(value: true);
		if (Dependencies != null)
		{
			GameObject[] dependencies = Dependencies;
			foreach (GameObject gameObject in dependencies)
			{
				gameObject.SetActive(value: true);
			}
		}
		focus = base.gameObject.GetComponent<UIFocus>();
		focus.OnPanelEnter();
		lerpPos = transform.position;
		open = true;
		close = false;
		openProgress = 0f;
	}

	public void ClosePanel()
	{
		lerpPos = transform.position;
		open = false;
		close = true;
		closeProgress = 0f;
	}
}
