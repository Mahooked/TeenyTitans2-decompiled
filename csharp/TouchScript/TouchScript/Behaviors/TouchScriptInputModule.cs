using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TouchScript.Behaviors;

[AddComponentMenu("TouchScript/TouchScript Input Module")]
public class TouchScriptInputModule : BaseInputModule
{
	protected Dictionary<int, PointerEventData> pointerEvents = new Dictionary<int, PointerEventData>();

	[SerializeField]
	private string horizontalAxis = "Horizontal";

	[SerializeField]
	private string verticalAxis = "Vertical";

	[SerializeField]
	private string submitButton = "Submit";

	[SerializeField]
	private string cancelButton = "Cancel";

	[SerializeField]
	private float inputActionsPerSecond = 10f;

	private float nextActionTime;

	public string HorizontalAxis
	{
		get
		{
			return horizontalAxis;
		}
		set
		{
			horizontalAxis = value;
		}
	}

	public string VerticalAxis
	{
		get
		{
			return verticalAxis;
		}
		set
		{
			verticalAxis = value;
		}
	}

	public string SubmitButton
	{
		get
		{
			return submitButton;
		}
		set
		{
			submitButton = value;
		}
	}

	public string CancelButton
	{
		get
		{
			return cancelButton;
		}
		set
		{
			cancelButton = value;
		}
	}

	public override bool IsModuleSupported()
	{
		return true;
	}

	public override bool ShouldActivateModule()
	{
		if (!base.ShouldActivateModule())
		{
			return false;
		}
		return true;
	}

	public override bool IsPointerOverGameObject(int pointerId)
	{
		PointerEventData lastPointerEventData = getLastPointerEventData(pointerId);
		if (lastPointerEventData != null)
		{
			return lastPointerEventData.pointerEnter != null;
		}
		return false;
	}

	public override void ActivateModule()
	{
		base.ActivateModule();
		ITouchManager instance = TouchManager.Instance;
		if (instance != null)
		{
			instance.TouchesBegan += touchesBeganHandler;
			instance.TouchesMoved += touchesMovedHandler;
			instance.TouchesEnded += touchesEndedHandler;
			instance.TouchesCancelled += touchesCancelledHandler;
		}
		GameObject gameObject = eventSystem.currentSelectedGameObject;
		if (gameObject == null)
		{
			gameObject = eventSystem.firstSelectedGameObject;
		}
		eventSystem.SetSelectedGameObject(gameObject, GetBaseEventData());
	}

	public override void DeactivateModule()
	{
		base.DeactivateModule();
		ITouchManager instance = TouchManager.Instance;
		if (instance != null)
		{
			instance.TouchesBegan -= touchesBeganHandler;
			instance.TouchesMoved -= touchesMovedHandler;
			instance.TouchesEnded -= touchesEndedHandler;
			instance.TouchesCancelled -= touchesCancelledHandler;
		}
		clearSelection();
	}

	public override void Process()
	{
		bool flag = sendUpdateEventToSelectedObject();
		if (eventSystem.sendNavigationEvents)
		{
			if (!flag)
			{
				flag |= sendMoveEventToSelectedObject();
			}
			if (!flag)
			{
				sendSubmitEventToSelectedObject();
			}
		}
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder("<b>Pointer Input Module of type: </b>" + GetType());
		stringBuilder.AppendLine();
		foreach (KeyValuePair<int, PointerEventData> pointerEvent in pointerEvents)
		{
			if (pointerEvent.Value != null)
			{
				stringBuilder.AppendLine("<B>Pointer:</b> " + pointerEvent.Key);
				stringBuilder.AppendLine(pointerEvent.Value.ToString());
			}
		}
		return stringBuilder.ToString();
	}

	protected void raycastPointer(PointerEventData pointerEvent)
	{
		eventSystem.RaycastAll(pointerEvent, m_RaycastResultCache);
		RaycastResult pointerCurrentRaycast = BaseInputModule.FindFirstRaycast(m_RaycastResultCache);
		pointerEvent.pointerCurrentRaycast = pointerCurrentRaycast;
		m_RaycastResultCache.Clear();
	}

	protected void processBegan(ITouch touch)
	{
		getPointerData(touch.Id, out var data, create: true);
		data.position = touch.Position;
		data.button = PointerEventData.InputButton.Left;
		data.eligibleForClick = true;
		data.delta = Vector2.zero;
		data.dragging = false;
		data.useDragThreshold = true;
		data.pressPosition = data.position;
		raycastPointer(data);
		data.pointerPressRaycast = data.pointerCurrentRaycast;
		GameObject gameObject = data.pointerCurrentRaycast.gameObject;
		deselectIfSelectionChanged(gameObject, data);
		if (data.pointerEnter != gameObject)
		{
			HandlePointerExitAndEnter(data, gameObject);
			data.pointerEnter = gameObject;
		}
		GameObject gameObject2 = ExecuteEvents.ExecuteHierarchy(gameObject, data, ExecuteEvents.pointerDownHandler);
		if (gameObject2 == null)
		{
			gameObject2 = ExecuteEvents.GetEventHandler<IPointerClickHandler>(gameObject);
		}
		data.clickCount = 1;
		data.pointerPress = gameObject2;
		data.rawPointerPress = gameObject;
		data.clickTime = Time.unscaledTime;
		data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject);
		if (data.pointerDrag != null)
		{
			ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.initializePotentialDrag);
		}
	}

	private void processMove(ITouch touch)
	{
		getPointerData(touch.Id, out var data, create: true);
		data.position = touch.Position;
		data.delta = touch.Position - touch.PreviousPosition;
		raycastPointer(data);
		GameObject newEnterTarget = data.pointerCurrentRaycast.gameObject;
		HandlePointerExitAndEnter(data, newEnterTarget);
		bool flag = data.IsPointerMoving();
		if (flag && data.pointerDrag != null && !data.dragging && shouldStartDrag(data.pressPosition, data.position, eventSystem.pixelDragThreshold, data.useDragThreshold))
		{
			ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.beginDragHandler);
			data.dragging = true;
		}
		if (data.dragging && flag && data.pointerDrag != null)
		{
			if (data.pointerPress != data.pointerDrag)
			{
				ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
				data.eligibleForClick = false;
				data.pointerPress = null;
				data.rawPointerPress = null;
			}
			ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.dragHandler);
		}
	}

	private void processEnded(ITouch touch)
	{
		getPointerData(touch.Id, out var data, create: true);
		data.position = touch.Position;
		data.delta = Vector2.zero;
		raycastPointer(data);
		GameObject root = data.pointerCurrentRaycast.gameObject;
		ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
		GameObject eventHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(root);
		if (data.pointerPress == eventHandler && data.eligibleForClick)
		{
			ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
		}
		else if (data.pointerDrag != null)
		{
			ExecuteEvents.ExecuteHierarchy(root, data, ExecuteEvents.dropHandler);
		}
		data.eligibleForClick = false;
		data.pointerPress = null;
		data.rawPointerPress = null;
		if (data.pointerDrag != null && data.dragging)
		{
			ExecuteEvents.Execute(data.pointerDrag, data, ExecuteEvents.endDragHandler);
		}
		data.dragging = false;
		data.pointerDrag = null;
		ExecuteEvents.ExecuteHierarchy(data.pointerEnter, data, ExecuteEvents.pointerExitHandler);
		data.pointerEnter = null;
		removePointerData(data);
	}

	protected bool getPointerData(int id, out PointerEventData data, bool create)
	{
		if (!pointerEvents.TryGetValue(id, out data) && create)
		{
			data = new PointerEventData(eventSystem)
			{
				pointerId = id
			};
			pointerEvents.Add(id, data);
			return true;
		}
		return false;
	}

	protected void removePointerData(PointerEventData data)
	{
		pointerEvents.Remove(data.pointerId);
	}

	protected PointerEventData getLastPointerEventData(int id)
	{
		getPointerData(id, out var data, create: false);
		return data;
	}

	protected void clearSelection()
	{
		BaseEventData baseEventData = GetBaseEventData();
		foreach (PointerEventData value in pointerEvents.Values)
		{
			HandlePointerExitAndEnter(value, null);
		}
		pointerEvents.Clear();
		eventSystem.SetSelectedGameObject(null, baseEventData);
	}

	protected void deselectIfSelectionChanged(GameObject currentOverGo, BaseEventData pointerEvent)
	{
		GameObject eventHandler = ExecuteEvents.GetEventHandler<ISelectHandler>(currentOverGo);
		if (eventHandler != eventSystem.currentSelectedGameObject)
		{
			eventSystem.SetSelectedGameObject(null, pointerEvent);
		}
	}

	private bool allowMoveEventProcessing(float time)
	{
		bool buttonDown = Input.GetButtonDown(horizontalAxis);
		buttonDown |= Input.GetButtonDown(verticalAxis);
		return buttonDown | (time > nextActionTime);
	}

	private static bool shouldStartDrag(Vector2 pressPos, Vector2 currentPos, float threshold, bool useDragThreshold)
	{
		if (!useDragThreshold)
		{
			return true;
		}
		return (pressPos - currentPos).sqrMagnitude >= threshold * threshold;
	}

	private bool sendSubmitEventToSelectedObject()
	{
		if (eventSystem.currentSelectedGameObject == null)
		{
			return false;
		}
		BaseEventData baseEventData = GetBaseEventData();
		if (Input.GetButtonDown(submitButton))
		{
			ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.submitHandler);
		}
		if (Input.GetButtonDown(cancelButton))
		{
			ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.cancelHandler);
		}
		return baseEventData.used;
	}

	private Vector2 getRawMoveVector()
	{
		Vector2 zero = Vector2.zero;
		zero.x = Input.GetAxisRaw(horizontalAxis);
		zero.y = Input.GetAxisRaw(verticalAxis);
		if (Input.GetButtonDown(horizontalAxis))
		{
			if (zero.x < 0f)
			{
				zero.x = -1f;
			}
			if (zero.x > 0f)
			{
				zero.x = 1f;
			}
		}
		if (Input.GetButtonDown(verticalAxis))
		{
			if (zero.y < 0f)
			{
				zero.y = -1f;
			}
			if (zero.y > 0f)
			{
				zero.y = 1f;
			}
		}
		return zero;
	}

	private bool sendMoveEventToSelectedObject()
	{
		float unscaledTime = Time.unscaledTime;
		if (!allowMoveEventProcessing(unscaledTime))
		{
			return false;
		}
		Vector2 rawMoveVector = getRawMoveVector();
		AxisEventData axisEventData = GetAxisEventData(rawMoveVector.x, rawMoveVector.y, 0.6f);
		if (!Mathf.Approximately(axisEventData.moveVector.x, 0f) || !Mathf.Approximately(axisEventData.moveVector.y, 0f))
		{
			ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
		}
		nextActionTime = unscaledTime + 1f / inputActionsPerSecond;
		return ((BaseEventData)axisEventData).used;
	}

	private bool sendUpdateEventToSelectedObject()
	{
		if (eventSystem.currentSelectedGameObject == null)
		{
			return false;
		}
		BaseEventData baseEventData = GetBaseEventData();
		ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, baseEventData, ExecuteEvents.updateSelectedHandler);
		return baseEventData.used;
	}

	private void touchesBeganHandler(object sender, TouchEventArgs touchEventArgs)
	{
		IList<ITouch> touches = touchEventArgs.Touches;
		for (int i = 0; i < touches.Count; i++)
		{
			processBegan(touches[i]);
		}
	}

	private void touchesMovedHandler(object sender, TouchEventArgs touchEventArgs)
	{
		IList<ITouch> touches = touchEventArgs.Touches;
		for (int i = 0; i < touches.Count; i++)
		{
			processMove(touches[i]);
		}
	}

	private void touchesEndedHandler(object sender, TouchEventArgs touchEventArgs)
	{
		IList<ITouch> touches = touchEventArgs.Touches;
		for (int i = 0; i < touches.Count; i++)
		{
			processEnded(touches[i]);
		}
	}

	private void touchesCancelledHandler(object sender, TouchEventArgs touchEventArgs)
	{
		IList<ITouch> touches = touchEventArgs.Touches;
		for (int i = 0; i < touches.Count; i++)
		{
			processEnded(touches[i]);
		}
	}
}
