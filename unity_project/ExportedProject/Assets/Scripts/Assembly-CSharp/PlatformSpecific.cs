using UnityEngine;

public class PlatformSpecific : MonoBehaviour
{
	public bool tvOS;

	public bool iOS;

	public bool Google;

	public bool Amazon;

	public bool hide;

	public bool remove;

	public bool move;

	public bool offset;

	public bool enableScript;

	public Vector3 movePostion;

	public MonoBehaviour script;

	private void Awake()
	{
		if (Amazon)
		{
			if (hide)
			{
				Hide();
			}
			if (remove)
			{
				Remove();
			}
			if (move)
			{
				Move();
			}
			if (offset)
			{
				Offset();
			}
			if (enableScript)
			{
				EnableScript();
			}
		}
	}

	private void Hide()
	{
		base.gameObject.SetActive(false);
	}

	private void Remove()
	{
		Object.Destroy(base.gameObject);
	}

	private void Move()
	{
		base.transform.localPosition = movePostion;
	}

	private void Offset()
	{
		base.transform.localPosition += movePostion;
	}

	private void EnableScript()
	{
		if (script != null)
		{
			script.enabled = true;
		}
	}
}
