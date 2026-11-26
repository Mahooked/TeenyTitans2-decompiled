using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript.InputSources;

[AddComponentMenu("TouchScript/Input Sources/Mouse Input")]
public sealed class MouseInput : InputSource
{
	[ToggleLeft]
	public bool DisableOnMobilePlatforms = true;

	public Tags Tags = new Tags("Mouse");

	private int mousePointId = -1;

	private int fakeMousePointId = -1;

	private Vector3 mousePointPos = Vector3.zero;

	protected override void OnEnable()
	{
		if (DisableOnMobilePlatforms)
		{
			switch (Application.platform)
			{
			case RuntimePlatform.IPhonePlayer:
			case RuntimePlatform.Android:
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
			case RuntimePlatform.WP8Player:
				enabled = false;
				return;
			}
		}
		base.OnEnable();
		mousePointId = -1;
		fakeMousePointId = -1;
	}

	protected override void OnDisable()
	{
		if (mousePointId != -1)
		{
			cancelTouch(mousePointId);
		}
		if (fakeMousePointId != -1)
		{
			cancelTouch(fakeMousePointId);
		}
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
		bool flag = false;
		if (Input.GetMouseButtonUp(0) && mousePointId != -1)
		{
			endTouch(mousePointId);
			mousePointId = -1;
			flag = true;
		}
		if (fakeMousePointId > -1 && !Input.GetKey(KeyCode.LeftAlt) && !Input.GetKey(KeyCode.RightAlt))
		{
			endTouch(fakeMousePointId);
			fakeMousePointId = -1;
		}
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && fakeMousePointId == -1)
			{
				if (fakeMousePointId == -1)
				{
					fakeMousePointId = beginTouch(new Vector2(mousePosition.x, mousePosition.y)).Id;
				}
			}
			else if (mousePointId == -1)
			{
				mousePointId = beginTouch(new Vector2(mousePosition.x, mousePosition.y)).Id;
			}
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = Input.mousePosition;
			if (mousePointPos != mousePosition2)
			{
				mousePointPos = mousePosition2;
				if (fakeMousePointId > -1 && mousePointId == -1)
				{
					moveTouch(fakeMousePointId, new Vector2(mousePosition2.x, mousePosition2.y));
				}
				else
				{
					moveTouch(mousePointId, new Vector2(mousePosition2.x, mousePosition2.y));
				}
			}
		}
		if (Input.GetMouseButtonUp(0) && !flag)
		{
			endTouch(mousePointId);
			mousePointId = -1;
		}
	}

	protected override ITouch beginTouch(Vector2 position)
	{
		return beginTouch(position, new Tags(Tags));
	}
}
