using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TouchScript.Devices.Display;
using TouchScript.Hit;
using TouchScript.InputSources;
using TouchScript.Layers;
using TouchScript.Utils;
using UnityEngine;

namespace TouchScript;

internal sealed class TouchManagerInstance : MonoBehaviour, ITouchManager
{
	private EventHandler<TouchEventArgs> touchesBeganInvoker;

	private EventHandler<TouchEventArgs> touchesMovedInvoker;

	private EventHandler<TouchEventArgs> touchesEndedInvoker;

	private EventHandler<TouchEventArgs> touchesCancelledInvoker;

	private EventHandler frameStartedInvoker;

	private EventHandler frameFinishedInvoker;

	private static bool shuttingDown;

	private static TouchManagerInstance instance;

	private bool shouldCreateCameraLayer = true;

	private IDisplayDevice displayDevice;

	private float dpi = 96f;

	private float dotsPerCentimeter = 37.795273f;

	private List<TouchLayer> layers = new List<TouchLayer>(10);

	private List<TouchPoint> touches = new List<TouchPoint>(30);

	private Dictionary<int, TouchPoint> idToTouch = new Dictionary<int, TouchPoint>(30);

	private List<TouchPoint> touchesBegan = new List<TouchPoint>(30);

	private HashSet<int> touchesUpdated = new HashSet<int>();

	private HashSet<int> touchesEnded = new HashSet<int>();

	private HashSet<int> touchesCancelled = new HashSet<int>();

	private int nextTouchId;

	public static TouchManagerInstance Instance
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
				TouchManagerInstance[] array = UnityEngine.Object.FindObjectsOfType<TouchManagerInstance>();
				if (array.Length == 0)
				{
					GameObject gameObject = new GameObject("TouchManager Instance");
					instance = gameObject.AddComponent<TouchManagerInstance>();
				}
				else if (array.Length >= 1)
				{
					instance = array[0];
				}
			}
			return instance;
		}
	}

	public IDisplayDevice DisplayDevice
	{
		get
		{
			if (displayDevice == null)
			{
				displayDevice = ScriptableObject.CreateInstance<GenericDisplayDevice>();
			}
			return displayDevice;
		}
		set
		{
			if (value == null)
			{
				displayDevice = ScriptableObject.CreateInstance<GenericDisplayDevice>();
			}
			else
			{
				displayDevice = value;
			}
			updateDPI();
		}
	}

	public float DPI => dpi;

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

	public IList<TouchLayer> Layers => new ReadOnlyCollection<TouchLayer>(layers);

	public float DotsPerCentimeter => dotsPerCentimeter;

	public int NumberOfTouches => touches.Count;

	public IList<ITouch> ActiveTouches => touches.Cast<ITouch>().ToList();

	public event EventHandler FrameStarted
	{
		add
		{
			frameStartedInvoker = (EventHandler)Delegate.Combine(frameStartedInvoker, value);
		}
		remove
		{
			frameStartedInvoker = (EventHandler)Delegate.Remove(frameStartedInvoker, value);
		}
	}

	public event EventHandler FrameFinished
	{
		add
		{
			frameFinishedInvoker = (EventHandler)Delegate.Combine(frameFinishedInvoker, value);
		}
		remove
		{
			frameFinishedInvoker = (EventHandler)Delegate.Remove(frameFinishedInvoker, value);
		}
	}

	public event EventHandler<TouchEventArgs> TouchesBegan
	{
		add
		{
			touchesBeganInvoker = (EventHandler<TouchEventArgs>)Delegate.Combine(touchesBeganInvoker, value);
		}
		remove
		{
			touchesBeganInvoker = (EventHandler<TouchEventArgs>)Delegate.Remove(touchesBeganInvoker, value);
		}
	}

	public event EventHandler<TouchEventArgs> TouchesMoved
	{
		add
		{
			touchesMovedInvoker = (EventHandler<TouchEventArgs>)Delegate.Combine(touchesMovedInvoker, value);
		}
		remove
		{
			touchesMovedInvoker = (EventHandler<TouchEventArgs>)Delegate.Remove(touchesMovedInvoker, value);
		}
	}

	public event EventHandler<TouchEventArgs> TouchesEnded
	{
		add
		{
			touchesEndedInvoker = (EventHandler<TouchEventArgs>)Delegate.Combine(touchesEndedInvoker, value);
		}
		remove
		{
			touchesEndedInvoker = (EventHandler<TouchEventArgs>)Delegate.Remove(touchesEndedInvoker, value);
		}
	}

	public event EventHandler<TouchEventArgs> TouchesCancelled
	{
		add
		{
			touchesCancelledInvoker = (EventHandler<TouchEventArgs>)Delegate.Combine(touchesCancelledInvoker, value);
		}
		remove
		{
			touchesCancelledInvoker = (EventHandler<TouchEventArgs>)Delegate.Remove(touchesCancelledInvoker, value);
		}
	}

	public bool AddLayer(TouchLayer layer)
	{
		if (layer == null)
		{
			return false;
		}
		if (layers.Contains(layer))
		{
			return true;
		}
		layers.Add(layer);
		return true;
	}

	public bool AddLayer(TouchLayer layer, int index)
	{
		if (layer == null)
		{
			return false;
		}
		if (index >= layers.Count)
		{
			return AddLayer(layer);
		}
		int num = layers.IndexOf(layer);
		if (num == -1)
		{
			layers.Insert(index, layer);
		}
		else
		{
			if (index == num || num == index - 1)
			{
				return true;
			}
			layers.RemoveAt(num);
			if (index < num)
			{
				layers.Insert(index, layer);
			}
			else
			{
				layers.Insert(index - 1, layer);
			}
		}
		return true;
	}

	public bool RemoveLayer(TouchLayer layer)
	{
		if (layer == null)
		{
			return false;
		}
		return layers.Remove(layer);
	}

	public void ChangeLayerIndex(int at, int to)
	{
		if (at >= 0 && at < layers.Count && to >= 0 && to < layers.Count)
		{
			TouchLayer item = layers[at];
			layers.RemoveAt(at);
			layers.Insert(to, item);
		}
	}

	public Transform GetHitTarget(Vector2 position)
	{
		if (GetHitTarget(position, out var hit, out var _))
		{
			return hit.Transform;
		}
		return null;
	}

	public bool GetHitTarget(Vector2 position, out ITouchHit hit)
	{
		TouchLayer layer;
		return GetHitTarget(position, out hit, out layer);
	}

	public bool GetHitTarget(Vector2 position, out ITouchHit hit, out TouchLayer layer)
	{
		hit = null;
		layer = null;
		foreach (TouchLayer layer2 in layers)
		{
			if (layer2 == null || layer2.Hit(position, out var hit2) != TouchLayer.LayerHitResult.Hit)
			{
				continue;
			}
			hit = hit2;
			layer = layer2;
			return true;
		}
		return false;
	}

	internal ITouch BeginTouch(Vector2 position)
	{
		return BeginTouch(position, null);
	}

	internal ITouch BeginTouch(Vector2 position, Tags tags)
	{
		lock (touchesBegan)
		{
			TouchPoint touchPoint = new TouchPoint(nextTouchId++, position, tags);
			touchesBegan.Add(touchPoint);
			return touchPoint;
		}
	}

	internal void UpdateTouch(int id)
	{
		lock (touchesUpdated)
		{
			if (idToTouch.ContainsKey(id))
			{
				touchesUpdated.Add(id);
			}
		}
	}

	internal void MoveTouch(int id, Vector2 position)
	{
		lock (touchesUpdated)
		{
			if (idToTouch.TryGetValue(id, out var value))
			{
				value.SetPosition(position);
				touchesUpdated.Add(id);
			}
		}
	}

	internal void EndTouch(int id)
	{
		lock (touchesEnded)
		{
			if (!idToTouch.TryGetValue(id, out var value))
			{
				value = touchesBegan.Find((TouchPoint t) => t.Id == id);
				if (value == null)
				{
					return;
				}
			}
			touchesEnded.Add(value.Id);
		}
	}

	internal void CancelTouch(int id)
	{
		lock (touchesCancelled)
		{
			if (!idToTouch.TryGetValue(id, out var value))
			{
				value = touchesBegan.Find((TouchPoint t) => t.Id == id);
				if (value == null)
				{
					return;
				}
			}
			touchesCancelled.Add(value.Id);
		}
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
		updateDPI();
		StopAllCoroutines();
		StartCoroutine("lateAwake");
	}

	private void OnLevelWasLoaded(int value)
	{
		StopAllCoroutines();
		StartCoroutine("lateAwake");
	}

	private IEnumerator lateAwake()
	{
		yield return null;
		updateLayers();
		createCameraLayer();
		createTouchInput();
	}

	private void Update()
	{
		updateTouches();
	}

	private void OnApplicationQuit()
	{
		shuttingDown = true;
	}

	private void updateDPI()
	{
		dpi = ((DisplayDevice != null) ? DisplayDevice.DPI : 96f);
		dotsPerCentimeter = 50f / 127f * dpi;
	}

	private void updateLayers()
	{
		layers = layers.FindAll((TouchLayer l) => l != null);
	}

	private void createCameraLayer()
	{
		if (layers.Count != 0 || !shouldCreateCameraLayer)
		{
			return;
		}
		Debug.LogWarning("No camera layers, adding CameraLayer for the main camera. (this message is harmless)");
		if (Camera.main != null)
		{
			TouchLayer touchLayer = Camera.main.GetComponent<TouchLayer>();
			if (touchLayer == null)
			{
				touchLayer = Camera.main.gameObject.AddComponent<CameraLayer>();
			}
			AddLayer(touchLayer);
		}
	}

	private void createTouchInput()
	{
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(InputSource));
		if (array.Length != 0)
		{
			return;
		}
		GameObject gameObject = null;
		TouchManager[] array2 = UnityEngine.Object.FindObjectsOfType<TouchManager>();
		if (array2.Length == 0)
		{
			gameObject = GameObject.Find("TouchScript");
			if (gameObject == null)
			{
				gameObject = new GameObject("TouchScript");
			}
		}
		else
		{
			gameObject = array2[0].gameObject;
		}
		switch (Application.platform)
		{
		case RuntimePlatform.IPhonePlayer:
		case RuntimePlatform.Android:
		case RuntimePlatform.MetroPlayerX86:
		case RuntimePlatform.MetroPlayerX64:
		case RuntimePlatform.MetroPlayerARM:
		case RuntimePlatform.WP8Player:
		case RuntimePlatform.BlackBerryPlayer:
			gameObject.AddComponent<MobileInput>();
			break;
		default:
			gameObject.AddComponent<MouseInput>();
			break;
		}
	}

	private void updateBegan()
	{
		if (touchesBegan.Count <= 0)
		{
			return;
		}
		List<ITouch> list = new List<ITouch>(touchesBegan.Count);
		foreach (TouchPoint item in touchesBegan)
		{
			list.Add(item);
			touches.Add(item);
			idToTouch.Add(item.Id, item);
			foreach (TouchLayer layer in layers)
			{
				if (layer == null || !layer.BeginTouch(item))
				{
					continue;
				}
				break;
			}
		}
		touchesBeganInvoker.InvokeHandleExceptions(this, new TouchEventArgs(list));
		touchesBegan.Clear();
	}

	private void updateUpdated()
	{
		if (touchesUpdated.Count <= 0)
		{
			return;
		}
		List<ITouch> list = new List<ITouch>(touchesUpdated.Count);
		foreach (TouchPoint touch in touches)
		{
			touch.ResetPosition();
			if (touchesUpdated.Contains(touch.Id))
			{
				list.Add(touch);
				if (touch.Layer != null)
				{
					touch.Layer.UpdateTouch(touch);
				}
			}
		}
		touchesMovedInvoker.InvokeHandleExceptions(this, new TouchEventArgs(list));
		touchesUpdated.Clear();
	}

	private void updateEnded()
	{
		if (touchesEnded.Count <= 0)
		{
			return;
		}
		List<ITouch> list = new List<ITouch>(touchesEnded.Count);
		foreach (int item in touchesEnded)
		{
			TouchPoint touchPoint = idToTouch[item];
			idToTouch.Remove(touchPoint.Id);
			touches.Remove(touchPoint);
			list.Add(touchPoint);
			if (touchPoint.Layer != null)
			{
				touchPoint.Layer.EndTouch(touchPoint);
			}
		}
		touchesEndedInvoker.InvokeHandleExceptions(this, new TouchEventArgs(list));
		touchesEnded.Clear();
	}

	private void updateCancelled()
	{
		if (touchesCancelled.Count <= 0)
		{
			return;
		}
		List<ITouch> list = new List<ITouch>(touchesCancelled.Count);
		foreach (int item in touchesCancelled)
		{
			TouchPoint touchPoint = idToTouch[item];
			idToTouch.Remove(touchPoint.Id);
			touches.Remove(touchPoint);
			list.Add(touchPoint);
			if (touchPoint.Layer != null)
			{
				touchPoint.Layer.CancelTouch(touchPoint);
			}
		}
		touchesCancelledInvoker.InvokeHandleExceptions(this, new TouchEventArgs(list));
		touchesCancelled.Clear();
	}

	private void updateTouches()
	{
		frameStartedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
		lock (touchesBegan)
		{
			updateBegan();
		}
		lock (touchesUpdated)
		{
			updateUpdated();
		}
		lock (touchesEnded)
		{
			updateEnded();
		}
		lock (touchesCancelled)
		{
			updateCancelled();
		}
		frameFinishedInvoker.InvokeHandleExceptions(this, EventArgs.Empty);
	}
}
