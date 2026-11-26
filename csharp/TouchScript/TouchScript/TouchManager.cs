using System;
using System.Collections.Generic;
using TouchScript.Devices.Display;
using TouchScript.Layers;
using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript;

[AddComponentMenu("TouchScript/Touch Manager")]
public sealed class TouchManager : MonoBehaviour
{
	[Flags]
	public enum MessageType
	{
		FrameStarted = 1,
		FrameFinished = 2,
		TouchesBegan = 4,
		TouchesMoved = 8,
		TouchesEnded = 0x10,
		TouchesCancelled = 0x20
	}

	public enum MessageName
	{
		OnTouchFrameStarted = 1,
		OnTouchFrameFinished = 2,
		OnTouchesBegan = 4,
		OnTouchesMoved = 8,
		OnTouchesEnded = 0x10,
		OnTouchesCancelled = 0x20
	}

	public const float CM_TO_INCH = 50f / 127f;

	public const float INCH_TO_CM = 2.54f;

	public static readonly Vector2 INVALID_POSITION = new Vector2(float.NaN, float.NaN);

	public static readonly Version VERSION = new Version(6, 8);

	[SerializeField]
	private UnityEngine.Object displayDevice;

	[SerializeField]
	[ToggleLeft]
	private bool shouldCreateCameraLayer = true;

	[ToggleLeft]
	[SerializeField]
	private bool useSendMessage;

	[SerializeField]
	private MessageType sendMessageEvents = MessageType.TouchesBegan | MessageType.TouchesMoved | MessageType.TouchesEnded | MessageType.TouchesCancelled;

	[SerializeField]
	private GameObject sendMessageTarget;

	[SerializeField]
	private List<TouchLayer> layers = new List<TouchLayer>();

	public static ITouchManager Instance => TouchManagerInstance.Instance;

	public IDisplayDevice DisplayDevice
	{
		get
		{
			if (Instance == null)
			{
				return displayDevice as IDisplayDevice;
			}
			return Instance.DisplayDevice;
		}
		set
		{
			if (Instance == null)
			{
				displayDevice = value as UnityEngine.Object;
			}
			else
			{
				Instance.DisplayDevice = value;
			}
		}
	}

	public bool ShouldCreateCameraLayer
	{
		get
		{
			return shouldCreateCameraLayer;
		}
		set
		{
			shouldCreateCameraLayer = value;
		}
	}

	public bool UseSendMessage
	{
		get
		{
			return useSendMessage;
		}
		set
		{
			if (value != useSendMessage)
			{
				useSendMessage = value;
				updateSubscription();
			}
		}
	}

	public MessageType SendMessageEvents
	{
		get
		{
			return sendMessageEvents;
		}
		set
		{
			if (sendMessageEvents != value)
			{
				sendMessageEvents = value;
				updateSubscription();
			}
		}
	}

	public GameObject SendMessageTarget
	{
		get
		{
			return sendMessageTarget;
		}
		set
		{
			sendMessageTarget = value;
			if (value == null)
			{
				sendMessageTarget = gameObject;
			}
		}
	}

	public static bool IsInvalidPosition(Vector2 position)
	{
		return position.Equals(INVALID_POSITION);
	}

	private void OnEnable()
	{
		if (Instance != null)
		{
			Instance.DisplayDevice = displayDevice as IDisplayDevice;
			Instance.ShouldCreateCameraLayer = ShouldCreateCameraLayer;
			for (int i = 0; i < layers.Count; i++)
			{
				Instance.AddLayer(layers[i], i);
			}
			updateSubscription();
		}
	}

	private void updateSubscription()
	{
		if (!Application.isPlaying || Instance == null)
		{
			return;
		}
		if (sendMessageTarget == null)
		{
			sendMessageTarget = gameObject;
		}
		Instance.FrameStarted -= frameStartedhandler;
		Instance.FrameFinished -= frameFinishedHandler;
		Instance.TouchesBegan -= touchesBeganHandler;
		Instance.TouchesMoved -= touchesMovedHandler;
		Instance.TouchesEnded -= touchesEndedHandler;
		Instance.TouchesCancelled -= touchesCancelledHandler;
		if (useSendMessage)
		{
			if ((SendMessageEvents & MessageType.FrameStarted) != 0)
			{
				Instance.FrameStarted += frameStartedhandler;
			}
			if ((SendMessageEvents & MessageType.FrameFinished) != 0)
			{
				Instance.FrameFinished += frameFinishedHandler;
			}
			if ((SendMessageEvents & MessageType.TouchesBegan) != 0)
			{
				Instance.TouchesBegan += touchesBeganHandler;
			}
			if ((SendMessageEvents & MessageType.TouchesMoved) != 0)
			{
				Instance.TouchesMoved += touchesMovedHandler;
			}
			if ((SendMessageEvents & MessageType.TouchesEnded) != 0)
			{
				Instance.TouchesEnded += touchesEndedHandler;
			}
			if ((SendMessageEvents & MessageType.TouchesCancelled) != 0)
			{
				Instance.TouchesCancelled += touchesCancelledHandler;
			}
		}
	}

	private void touchesBeganHandler(object sender, TouchEventArgs e)
	{
		sendMessageTarget.SendMessage(MessageName.OnTouchesBegan.ToString(), e.Touches, SendMessageOptions.DontRequireReceiver);
	}

	private void touchesMovedHandler(object sender, TouchEventArgs e)
	{
		sendMessageTarget.SendMessage(MessageName.OnTouchesMoved.ToString(), e.Touches, SendMessageOptions.DontRequireReceiver);
	}

	private void touchesEndedHandler(object sender, TouchEventArgs e)
	{
		sendMessageTarget.SendMessage(MessageName.OnTouchesEnded.ToString(), e.Touches, SendMessageOptions.DontRequireReceiver);
	}

	private void touchesCancelledHandler(object sender, TouchEventArgs e)
	{
		sendMessageTarget.SendMessage(MessageName.OnTouchesCancelled.ToString(), e.Touches, SendMessageOptions.DontRequireReceiver);
	}

	private void frameStartedhandler(object sender, EventArgs e)
	{
		sendMessageTarget.SendMessage(MessageName.OnTouchFrameStarted.ToString(), SendMessageOptions.DontRequireReceiver);
	}

	private void frameFinishedHandler(object sender, EventArgs e)
	{
		sendMessageTarget.SendMessage(MessageName.OnTouchFrameFinished.ToString(), SendMessageOptions.DontRequireReceiver);
	}
}
