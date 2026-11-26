using System;
using System.Collections.Generic;
using TouchScript.Gestures;
using UnityEngine;

namespace TouchScript;

internal sealed class GestureManagerInstance : MonoBehaviour, IGestureManager
{
	private static GestureManagerInstance instance;

	private static bool shuttingDown;

	private List<Gesture> gesturesToReset = new List<Gesture>(10);

	private Dictionary<Transform, List<ITouch>> targetTouches = new Dictionary<Transform, List<ITouch>>(10);

	private Dictionary<Gesture, List<ITouch>> gestureTouches = new Dictionary<Gesture, List<ITouch>>(10);

	private List<Gesture> activeGestures = new List<Gesture>(10);

	public static IGestureManager Instance
	{
		get
		{
			if (shuttingDown)
			{
				return null;
			}
			if (instance == null)
			{
				if (!Application.isPlaying)
				{
					return null;
				}
				GestureManagerInstance[] array = UnityEngine.Object.FindObjectsOfType<GestureManagerInstance>();
				if (array.Length == 0)
				{
					GameObject gameObject = new GameObject("GestureManager Instance");
					gameObject.hideFlags = HideFlags.HideInHierarchy;
					UnityEngine.Object.DontDestroyOnLoad(gameObject);
					instance = gameObject.AddComponent<GestureManagerInstance>();
				}
				else if (array.Length >= 1)
				{
					instance = array[0];
				}
			}
			return instance;
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	private void OnEnable()
	{
		ITouchManager touchManager = TouchManager.Instance;
		if (touchManager != null)
		{
			touchManager.FrameStarted += frameStartedHandler;
			touchManager.FrameFinished += frameFinishedHandler;
			touchManager.TouchesBegan += touchBeganHandler;
			touchManager.TouchesMoved += touchMovedHandler;
			touchManager.TouchesEnded += touchEndedHandler;
			touchManager.TouchesCancelled += touchCancelledHandler;
		}
	}

	private void OnDisable()
	{
		ITouchManager touchManager = TouchManager.Instance;
		if (touchManager != null)
		{
			touchManager.FrameStarted -= frameStartedHandler;
			touchManager.FrameFinished -= frameFinishedHandler;
			touchManager.TouchesBegan -= touchBeganHandler;
			touchManager.TouchesMoved -= touchMovedHandler;
			touchManager.TouchesEnded -= touchEndedHandler;
			touchManager.TouchesCancelled -= touchCancelledHandler;
		}
	}

	private void OnApplicationQuit()
	{
		shuttingDown = true;
	}

	internal Gesture.GestureState GestureChangeState(Gesture gesture, Gesture.GestureState state)
	{
		bool flag = false;
		switch (state)
		{
		case Gesture.GestureState.Began:
			if (gesture.State != Gesture.GestureState.Possible)
			{
				MonoBehaviour.print(string.Format("Gesture {0} erroneously tried to enter state {1} from state {2}", new object[3] { gesture, state, gesture.State }));
			}
			if (!recognizeGestureIfNotPrevented(gesture))
			{
				if (!gesturesToReset.Contains(gesture))
				{
					gesturesToReset.Add(gesture);
				}
				return Gesture.GestureState.Failed;
			}
			break;
		case Gesture.GestureState.Changed:
		{
			Gesture.GestureState state2 = gesture.State;
			if (state2 != Gesture.GestureState.Began && state2 != Gesture.GestureState.Changed)
			{
				MonoBehaviour.print(string.Format("Gesture {0} erroneously tried to enter state {1} from state {2}", new object[3] { gesture, state, gesture.State }));
			}
			break;
		}
		case Gesture.GestureState.Failed:
			if (!gesturesToReset.Contains(gesture))
			{
				gesturesToReset.Add(gesture);
			}
			break;
		case Gesture.GestureState.Ended:
			if (!gesturesToReset.Contains(gesture))
			{
				gesturesToReset.Add(gesture);
			}
			switch (gesture.State)
			{
			case Gesture.GestureState.Possible:
				if (!recognizeGestureIfNotPrevented(gesture))
				{
					return Gesture.GestureState.Failed;
				}
				break;
			default:
				MonoBehaviour.print(string.Format("Gesture {0} erroneously tried to enter state {1} from state {2}", new object[3] { gesture, state, gesture.State }));
				break;
			case Gesture.GestureState.Began:
			case Gesture.GestureState.Changed:
				break;
			}
			break;
		case Gesture.GestureState.Cancelled:
			if (!gesturesToReset.Contains(gesture))
			{
				gesturesToReset.Add(gesture);
			}
			break;
		}
		return state;
	}

	private void updateBegan(IList<ITouch> touches)
	{
		update(touches, processTargetBegan, (Gesture gesture, IList<ITouch> touchPoints) =>
		{
			gesture.TouchesBegan(touchPoints);
		});
	}

	private void updateMoved(IList<ITouch> touches)
	{
		update(touches, processTarget, (Gesture gesture, IList<ITouch> touchPoints) =>
		{
			gesture.TouchesMoved(touchPoints);
		});
	}

	private void updateEnded(IList<ITouch> touches)
	{
		update(touches, processTarget, (Gesture gesture, IList<ITouch> touchPoints) =>
		{
			gesture.TouchesEnded(touchPoints);
		});
	}

	private void updateCancelled(IList<ITouch> touches)
	{
		update(touches, processTarget, (Gesture gesture, IList<ITouch> touchPoints) =>
		{
			gesture.TouchesCancelled(touchPoints);
		});
	}

	private void update(IList<ITouch> touches, Action<Transform> process, Action<Gesture, IList<ITouch>> dispatch)
	{
		targetTouches.Clear();
		gestureTouches.Clear();
		activeGestures.Clear();
		foreach (ITouch touch in touches)
		{
			if (touch.Target != null)
			{
				if (!targetTouches.TryGetValue(touch.Target, out var value))
				{
					value = new List<ITouch>();
					targetTouches.Add(touch.Target, value);
				}
				value.Add(touch);
			}
		}
		foreach (Transform key in targetTouches.Keys)
		{
			process(key);
		}
		foreach (Gesture activeGesture in activeGestures)
		{
			if (gestureIsActive(activeGesture))
			{
				dispatch(activeGesture, gestureTouches[activeGesture]);
			}
		}
	}

	private void processTarget(Transform target)
	{
		List<Gesture> hierarchyEndingWith = getHierarchyEndingWith(target);
		foreach (Gesture item in hierarchyEndingWith)
		{
			if (gestureIsActive(item))
			{
				distributePointsByGestures(target, item, item.HasTouch);
			}
		}
	}

	private void processTargetBegan(Transform target)
	{
		List<Gesture> hierarchyContaining = getHierarchyContaining(target);
		List<Gesture> hierarchyEndingWith = getHierarchyEndingWith(target);
		foreach (Gesture item in hierarchyEndingWith)
		{
			if (!gestureIsActive(item))
			{
				continue;
			}
			bool flag = true;
			foreach (Gesture item2 in hierarchyContaining)
			{
				if (item == item2 || (item2.State != Gesture.GestureState.Began && item2.State != Gesture.GestureState.Changed) || !item2.CanPreventGesture(item))
				{
					continue;
				}
				flag = false;
				break;
			}
			if (flag)
			{
				distributePointsByGestures(target, item, item.ShouldReceiveTouch);
			}
		}
	}

	private void distributePointsByGestures(Transform target, Gesture gesture, Predicate<ITouch> condition)
	{
		List<ITouch> list = targetTouches[target].FindAll(condition);
		if (list.Count > 0)
		{
			if (gestureTouches.ContainsKey(gesture))
			{
				gestureTouches[gesture].AddRange(list);
				return;
			}
			activeGestures.Add(gesture);
			gestureTouches.Add(gesture, list);
		}
	}

	private void resetGestures()
	{
		if (gesturesToReset.Count == 0)
		{
			return;
		}
		foreach (Gesture item in gesturesToReset)
		{
			if (!(item == null))
			{
				item.Reset();
				item.SetState(Gesture.GestureState.Possible);
			}
		}
		gesturesToReset.Clear();
	}

	private List<Gesture> getHierarchyEndingWith(Transform target)
	{
		List<Gesture> list = new List<Gesture>(10);
		while (target != null)
		{
			list.AddRange(getEnabledGesturesOnTarget(target));
			target = target.parent;
		}
		return list;
	}

	private List<Gesture> getHierarchyBeginningWith(Transform target, bool includeSelf)
	{
		List<Gesture> list = new List<Gesture>(10);
		if (includeSelf)
		{
			list.AddRange(getEnabledGesturesOnTarget(target));
		}
		foreach (Transform item in target)
		{
			list.AddRange(getHierarchyBeginningWith(item, includeSelf: true));
		}
		return list;
	}

	private List<Gesture> getHierarchyContaining(Transform target)
	{
		List<Gesture> hierarchyEndingWith = getHierarchyEndingWith(target);
		hierarchyEndingWith.AddRange(getHierarchyBeginningWith(target, includeSelf: false));
		return hierarchyEndingWith;
	}

	private List<Gesture> getEnabledGesturesOnTarget(Transform target)
	{
		List<Gesture> list = new List<Gesture>(10);
		if (target.gameObject.activeInHierarchy)
		{
			Gesture[] components = target.GetComponents<Gesture>();
			Gesture[] array = components;
			foreach (Gesture gesture in array)
			{
				if (gesture != null && gesture.enabled)
				{
					list.Add(gesture);
				}
			}
		}
		return list;
	}

	private bool gestureIsActive(Gesture gesture)
	{
		if (!gesture.gameObject.activeInHierarchy)
		{
			return false;
		}
		if (!gesture.enabled)
		{
			return false;
		}
		switch (gesture.State)
		{
		case Gesture.GestureState.Ended:
		case Gesture.GestureState.Cancelled:
		case Gesture.GestureState.Failed:
			return false;
		default:
			return true;
		}
	}

	private bool recognizeGestureIfNotPrevented(Gesture gesture)
	{
		if (!gesture.ShouldBegin())
		{
			return false;
		}
		bool flag = true;
		List<Gesture> list = new List<Gesture>(10);
		List<Gesture> hierarchyContaining = getHierarchyContaining(gesture.transform);
		foreach (Gesture item in hierarchyContaining)
		{
			if (gesture == item || !gestureIsActive(item))
			{
				continue;
			}
			if (item.State == Gesture.GestureState.Began || item.State == Gesture.GestureState.Changed)
			{
				if (item.CanPreventGesture(gesture))
				{
					flag = false;
					break;
				}
			}
			else if (gesture.CanPreventGesture(item))
			{
				list.Add(item);
			}
		}
		if (flag)
		{
			foreach (Gesture item2 in list)
			{
				failGesture(item2);
			}
		}
		return flag;
	}

	private void failGesture(Gesture gesture)
	{
		gesture.SetState(Gesture.GestureState.Failed);
	}

	private void frameFinishedHandler(object sender, EventArgs eventArgs)
	{
		resetGestures();
	}

	private void frameStartedHandler(object sender, EventArgs eventArgs)
	{
		resetGestures();
	}

	private void touchBeganHandler(object sender, TouchEventArgs touchEventArgs)
	{
		updateBegan(touchEventArgs.Touches);
	}

	private void touchMovedHandler(object sender, TouchEventArgs touchEventArgs)
	{
		updateMoved(touchEventArgs.Touches);
	}

	private void touchEndedHandler(object sender, TouchEventArgs touchEventArgs)
	{
		updateEnded(touchEventArgs.Touches);
	}

	private void touchCancelledHandler(object sender, TouchEventArgs touchEventArgs)
	{
		updateCancelled(touchEventArgs.Touches);
	}
}
