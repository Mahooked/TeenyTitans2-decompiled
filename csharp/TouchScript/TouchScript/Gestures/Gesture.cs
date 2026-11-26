using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TouchScript.Hit;
using TouchScript.Layers;
using TouchScript.Utils;
using TouchScript.Utils.Attributes;
using UnityEngine;

namespace TouchScript.Gestures;

public abstract class Gesture : MonoBehaviour
{
	public enum GestureState
	{
		Possible = 0,
		Began = 1,
		Changed = 2,
		Ended = 3,
		Cancelled = 4,
		Failed = 5,
		Recognized = Ended
	}

	public const string STATE_CHANGE_MESSAGE = "OnGestureStateChange";

	private EventHandler<GestureStateChangeEventArgs> stateChangedInvoker;

	protected List<ITouch> activeTouches = new List<ITouch>();

	protected Transform cachedTransform;

	[SerializeField]
	private bool advancedProps;

	[SerializeField]
	[ToggleLeft]
	private bool combineTouches;

	[SerializeField]
	private float combineTouchesInterval = 0.3f;

	[SerializeField]
	[ToggleLeft]
	private bool useSendMessage;

	[ToggleLeft]
	[SerializeField]
	private bool sendStateChangeMessages;

	[SerializeField]
	private GameObject sendMessageTarget;

	[NullToggle]
	[SerializeField]
	private Gesture requireGestureToFail;

	[SerializeField]
	private List<Gesture> friendlyGestures = new List<Gesture>();

	private List<int> friendlyGestureIds = new List<int>();

	private TimedSequence<ITouch> touchSequence = new TimedSequence<ITouch>();

	private GestureManagerInstance gestureManagerInstance;

	private GestureState delayedStateChange;

	private bool requiredGestureFailed;

	private GestureState state;

	private Vector2 cachedScreenPosition;

	private Vector2 cachedPreviousScreenPosition;

	public Gesture RequireGestureToFail
	{
		get
		{
			return requireGestureToFail;
		}
		set
		{
			if (requireGestureToFail != null)
			{
				requireGestureToFail.StateChanged -= requiredToFailGestureStateChangedHandler;
			}
			requireGestureToFail = value;
			if (requireGestureToFail != null)
			{
				requireGestureToFail.StateChanged += requiredToFailGestureStateChangedHandler;
			}
		}
	}

	public bool CombineTouches
	{
		get
		{
			return combineTouches;
		}
		set
		{
			combineTouches = value;
		}
	}

	public float CombineTouchesInterval
	{
		get
		{
			return combineTouchesInterval;
		}
		set
		{
			combineTouchesInterval = value;
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
			useSendMessage = value;
		}
	}

	public bool SendStateChangeMessages
	{
		get
		{
			return sendStateChangeMessages;
		}
		set
		{
			sendStateChangeMessages = value;
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

	public GestureState State
	{
		get
		{
			return state;
		}
		private set
		{
			PreviousState = state;
			state = value;
			switch (value)
			{
			case GestureState.Possible:
				onPossible();
				break;
			case GestureState.Began:
				onBegan();
				break;
			case GestureState.Changed:
				onChanged();
				break;
			case GestureState.Ended:
				onRecognized();
				break;
			case GestureState.Failed:
				onFailed();
				break;
			case GestureState.Cancelled:
				onCancelled();
				break;
			}
			stateChangedInvoker.InvokeHandleExceptions(this, new GestureStateChangeEventArgs(state, PreviousState));
			if (useSendMessage && sendStateChangeMessages && SendMessageTarget != null)
			{
				sendMessageTarget.SendMessage("OnGestureStateChange", this, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	public GestureState PreviousState { get; private set; }

	public virtual Vector2 ScreenPosition
	{
		get
		{
			if (activeTouches.Count == 0)
			{
				if (!TouchManager.IsInvalidPosition(cachedScreenPosition))
				{
					return cachedScreenPosition;
				}
				return TouchManager.INVALID_POSITION;
			}
			return ClusterUtils.Get2DCenterPosition(activeTouches);
		}
	}

	public virtual Vector2 PreviousScreenPosition
	{
		get
		{
			if (activeTouches.Count == 0)
			{
				if (!TouchManager.IsInvalidPosition(cachedPreviousScreenPosition))
				{
					return cachedPreviousScreenPosition;
				}
				return TouchManager.INVALID_POSITION;
			}
			return ClusterUtils.GetPrevious2DCenterPosition(activeTouches);
		}
	}

	public Vector2 NormalizedScreenPosition
	{
		get
		{
			Vector2 screenPosition = ScreenPosition;
			if (TouchManager.IsInvalidPosition(screenPosition))
			{
				return TouchManager.INVALID_POSITION;
			}
			return new Vector2(screenPosition.x / (float)Screen.width, screenPosition.y / (float)Screen.height);
		}
	}

	public Vector2 PreviousNormalizedScreenPosition
	{
		get
		{
			Vector2 previousScreenPosition = PreviousScreenPosition;
			if (TouchManager.IsInvalidPosition(previousScreenPosition))
			{
				return TouchManager.INVALID_POSITION;
			}
			return new Vector2(previousScreenPosition.x / (float)Screen.width, previousScreenPosition.y / (float)Screen.height);
		}
	}

	public IList<ITouch> ActiveTouches => new ReadOnlyCollection<ITouch>(activeTouches);

	public IGestureDelegate Delegate { get; set; }

	protected IGestureManager gestureManager => gestureManagerInstance;

	protected ITouchManager touchManager { get; private set; }

	public event EventHandler<GestureStateChangeEventArgs> StateChanged
	{
		add
		{
			stateChangedInvoker = (EventHandler<GestureStateChangeEventArgs>)System.Delegate.Combine(stateChangedInvoker, value);
		}
		remove
		{
			stateChangedInvoker = (EventHandler<GestureStateChangeEventArgs>)System.Delegate.Remove(stateChangedInvoker, value);
		}
	}

	public virtual void AddFriendlyGesture(Gesture gesture)
	{
		if (!(gesture == null) && !(gesture == this))
		{
			registerFriendlyGesture(gesture);
			gesture.registerFriendlyGesture(this);
		}
	}

	public bool IsFriendly(Gesture gesture)
	{
		return friendlyGestureIds.Contains(gesture.GetInstanceID());
	}

	public virtual bool GetTargetHitResult()
	{
		ITouchHit hit;
		return GetTargetHitResult(ScreenPosition, out hit);
	}

	public virtual bool GetTargetHitResult(out ITouchHit hit)
	{
		return GetTargetHitResult(ScreenPosition, out hit);
	}

	public virtual bool GetTargetHitResult(Vector2 position)
	{
		ITouchHit hit;
		return GetTargetHitResult(position, out hit);
	}

	public virtual bool GetTargetHitResult(Vector2 position, out ITouchHit hit)
	{
		TouchLayer layer = null;
		if (!touchManager.GetHitTarget(position, out hit, out layer))
		{
			return false;
		}
		if (cachedTransform == hit.Transform || hit.Transform.IsChildOf(cachedTransform))
		{
			return true;
		}
		return false;
	}

	public bool HasTouch(ITouch touch)
	{
		return activeTouches.Contains(touch);
	}

	public virtual bool CanPreventGesture(Gesture gesture)
	{
		if (Delegate == null)
		{
			if (gesture.CanBePreventedByGesture(this))
			{
				return !IsFriendly(gesture);
			}
			return false;
		}
		return !Delegate.ShouldRecognizeSimultaneously(this, gesture);
	}

	public virtual bool CanBePreventedByGesture(Gesture gesture)
	{
		if (Delegate == null)
		{
			return !IsFriendly(gesture);
		}
		return !Delegate.ShouldRecognizeSimultaneously(this, gesture);
	}

	public virtual bool ShouldReceiveTouch(ITouch touch)
	{
		if (Delegate == null)
		{
			return true;
		}
		return Delegate.ShouldReceiveTouch(this, touch);
	}

	public virtual bool ShouldBegin()
	{
		if (Delegate == null)
		{
			return true;
		}
		return Delegate.ShouldBegin(this);
	}

	protected virtual void Awake()
	{
		cachedTransform = GetComponent<Transform>();
		foreach (Gesture friendlyGesture in friendlyGestures)
		{
			AddFriendlyGesture(friendlyGesture);
		}
		RequireGestureToFail = requireGestureToFail;
	}

	protected virtual void OnEnable()
	{
		touchManager = TouchManager.Instance;
		gestureManagerInstance = GestureManager.Instance as GestureManagerInstance;
		if (touchManager == null)
		{
			Debug.LogError("No TouchManager found! Please add an instance of TouchManager to the scene!");
		}
		if (gestureManagerInstance == null)
		{
			Debug.LogError("No GesturehManager found! Please add an instance of GesturehManager to the scene!");
		}
		if (sendMessageTarget == null)
		{
			sendMessageTarget = gameObject;
		}
		Reset();
	}

	protected virtual void OnDisable()
	{
		setState(GestureState.Failed);
	}

	protected virtual void OnDestroy()
	{
		List<Gesture> list = new List<Gesture>(friendlyGestures);
		foreach (Gesture item in list)
		{
			RemoveFriendlyGesture(item);
		}
		RequireGestureToFail = null;
	}

	internal void SetState(GestureState value)
	{
		setState(value);
	}

	internal void Reset()
	{
		activeTouches.Clear();
		delayedStateChange = GestureState.Possible;
		requiredGestureFailed = false;
		reset();
	}

	internal void TouchesBegan(IList<ITouch> touches)
	{
		activeTouches.AddRange(touches);
		touchesBegan(touches);
	}

	internal void TouchesMoved(IList<ITouch> touches)
	{
		touchesMoved(touches);
	}

	internal void TouchesEnded(IList<ITouch> touches)
	{
		for (int i = 0; i < touches.Count; i++)
		{
			activeTouches.Remove(touches[i]);
		}
		touchesEnded(touches);
	}

	internal void TouchesCancelled(IList<ITouch> touches)
	{
		for (int i = 0; i < touches.Count; i++)
		{
			activeTouches.Remove(touches[i]);
		}
		touchesCancelled(touches);
	}

	internal virtual void RemoveFriendlyGesture(Gesture gesture)
	{
		if (!(gesture == null) && !(gesture == this))
		{
			unregisterFriendlyGesture(gesture);
			gesture.unregisterFriendlyGesture(this);
		}
	}

	protected virtual bool shouldCacheTouchPosition(ITouch value)
	{
		return true;
	}

	protected bool setState(GestureState value)
	{
		if (gestureManagerInstance == null)
		{
			return false;
		}
		if (requireGestureToFail != null)
		{
			switch (value)
			{
			case GestureState.Began:
			case GestureState.Ended:
				if (!requiredGestureFailed)
				{
					delayedStateChange = value;
					return false;
				}
				break;
			case GestureState.Possible:
			case GestureState.Cancelled:
			case GestureState.Failed:
				delayedStateChange = GestureState.Possible;
				break;
			}
		}
		GestureState gestureState = (State = gestureManagerInstance.GestureChangeState(this, value));
		return value == gestureState;
	}

	protected virtual void touchesBegan(IList<ITouch> touches)
	{
	}

	protected virtual void touchesMoved(IList<ITouch> touches)
	{
	}

	protected virtual void touchesEnded(IList<ITouch> touches)
	{
		if (combineTouches)
		{
			foreach (ITouch touch2 in touches)
			{
				touchSequence.Add(touch2);
			}
			if (activeTouches.Count == 0)
			{
				IList<ITouch> touches2 = touchSequence.FindElementsLaterThan(Time.time - combineTouchesInterval, shouldCacheTouchPosition);
				cachedScreenPosition = ClusterUtils.Get2DCenterPosition(touches2);
				cachedPreviousScreenPosition = ClusterUtils.GetPrevious2DCenterPosition(touches2);
			}
		}
		else if (activeTouches.Count == 0)
		{
			ITouch touch = touches[touches.Count - 1];
			if (shouldCacheTouchPosition(touch))
			{
				cachedScreenPosition = touch.Position;
				cachedPreviousScreenPosition = touch.PreviousPosition;
			}
			else
			{
				cachedScreenPosition = TouchManager.INVALID_POSITION;
				cachedPreviousScreenPosition = TouchManager.INVALID_POSITION;
			}
		}
	}

	protected virtual void touchesCancelled(IList<ITouch> touches)
	{
	}

	protected virtual void reset()
	{
		cachedScreenPosition = TouchManager.INVALID_POSITION;
		cachedPreviousScreenPosition = TouchManager.INVALID_POSITION;
	}

	protected virtual void onPossible()
	{
	}

	protected virtual void onBegan()
	{
	}

	protected virtual void onChanged()
	{
	}

	protected virtual void onRecognized()
	{
	}

	protected virtual void onFailed()
	{
	}

	protected virtual void onCancelled()
	{
	}

	private void registerFriendlyGesture(Gesture gesture)
	{
		if (!(gesture == null) && !(gesture == this))
		{
			addFriendlyGestureId(gesture);
			if (!friendlyGestures.Contains(gesture))
			{
				friendlyGestures.Add(gesture);
			}
		}
	}

	private void addFriendlyGestureId(Gesture gesture)
	{
		int instanceID = gesture.GetInstanceID();
		if (!friendlyGestureIds.Contains(instanceID))
		{
			friendlyGestureIds.Add(instanceID);
		}
	}

	private void unregisterFriendlyGesture(Gesture gesture)
	{
		if (!(gesture == null) && !(gesture == this))
		{
			removeFriendlyGestureId(gesture);
			friendlyGestures.Remove(gesture);
		}
	}

	private void removeFriendlyGestureId(Gesture gesture)
	{
		friendlyGestureIds.Remove(gesture.GetInstanceID());
	}

	private void requiredToFailGestureStateChangedHandler(object sender, GestureStateChangeEventArgs e)
	{
		if (sender as Gesture != requireGestureToFail)
		{
			return;
		}
		switch (e.State)
		{
		case GestureState.Failed:
			requiredGestureFailed = true;
			if (delayedStateChange != GestureState.Possible)
			{
				setState(delayedStateChange);
			}
			break;
		case GestureState.Began:
		case GestureState.Ended:
		case GestureState.Cancelled:
			if (state != GestureState.Failed)
			{
				setState(GestureState.Failed);
			}
			break;
		case GestureState.Changed:
			break;
		}
	}
}
