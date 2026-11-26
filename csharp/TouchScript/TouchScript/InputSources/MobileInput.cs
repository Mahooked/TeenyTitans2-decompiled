using System.Collections.Generic;
using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript.InputSources;

[AddComponentMenu("TouchScript/Input Sources/Mobile Input")]
public sealed class MobileInput : InputSource
{
	[ToggleLeft]
	public bool DisableOnNonTouchPlatforms = true;

	public Tags Tags = new Tags("Touch");

	private Dictionary<int, TouchState> touchStates = new Dictionary<int, TouchState>();

	private HashSet<int> touchIds = new HashSet<int>();

	protected override void OnEnable()
	{
		if (DisableOnNonTouchPlatforms)
		{
			switch (Application.platform)
			{
			default:
				enabled = false;
				return;
			case RuntimePlatform.IPhonePlayer:
			case RuntimePlatform.Android:
			case RuntimePlatform.MetroPlayerX86:
			case RuntimePlatform.MetroPlayerX64:
			case RuntimePlatform.MetroPlayerARM:
			case RuntimePlatform.WP8Player:
				break;
			}
		}
		base.OnEnable();
		touchStates.Clear();
		touchIds.Clear();
	}

	protected override void OnDisable()
	{
		foreach (KeyValuePair<int, TouchState> touchState in touchStates)
		{
			cancelTouch(touchState.Value.Id);
		}
		base.OnDisable();
	}

	protected override void Update()
	{
		base.Update();
		for (int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			switch (touch.phase)
			{
			case TouchPhase.Began:
				if (touchIds.Contains(touch.fingerId))
				{
					endTouch(touch.fingerId);
					int id5 = beginTouch(touch.position).Id;
					touchStates[touch.fingerId] = new TouchState(id5, touch.phase, touch.position);
				}
				else
				{
					touchIds.Add(touch.fingerId);
					int id6 = beginTouch(touch.position).Id;
					touchStates.Add(touch.fingerId, new TouchState(id6, touch.phase, touch.position));
				}
				break;
			case TouchPhase.Moved:
				if (touchIds.Contains(touch.fingerId))
				{
					TouchState touchState3 = touchStates[touch.fingerId];
					touchStates[touch.fingerId] = new TouchState(touchState3.Id, touch.phase, touch.position);
					moveTouch(touchState3.Id, touch.position);
				}
				else
				{
					touchIds.Add(touch.fingerId);
					int id4 = beginTouch(touch.position).Id;
					touchStates.Add(touch.fingerId, new TouchState(id4, touch.phase, touch.position));
				}
				break;
			case TouchPhase.Ended:
				if (touchIds.Contains(touch.fingerId))
				{
					TouchState touchState = touchStates[touch.fingerId];
					touchIds.Remove(touch.fingerId);
					touchStates.Remove(touch.fingerId);
					endTouch(touchState.Id);
				}
				else
				{
					int id2 = beginTouch(touch.position).Id;
					endTouch(id2);
				}
				break;
			case TouchPhase.Canceled:
				if (touchIds.Contains(touch.fingerId))
				{
					TouchState touchState2 = touchStates[touch.fingerId];
					touchIds.Remove(touch.fingerId);
					touchStates.Remove(touch.fingerId);
					endTouch(touchState2.Id);
				}
				else
				{
					int id3 = beginTouch(touch.position).Id;
					cancelTouch(id3);
				}
				break;
			case TouchPhase.Stationary:
				if (!touchIds.Contains(touch.fingerId))
				{
					touchIds.Add(touch.fingerId);
					int id = beginTouch(touch.position).Id;
					touchStates.Add(touch.fingerId, new TouchState(id, touch.phase, touch.position));
				}
				break;
			}
		}
	}

	protected override ITouch beginTouch(Vector2 position)
	{
		return beginTouch(position, new Tags(Tags));
	}
}
