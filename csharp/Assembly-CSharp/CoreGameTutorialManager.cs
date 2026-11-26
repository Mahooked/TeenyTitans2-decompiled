using System.Collections.Generic;
using UnityEngine;

public class CoreGameTutorialManager : MonoBehaviour
{
	public tk2dCamera cameraFitHeight;

	public tk2dCamera cameraFitVisible;

	public SimpleFaderTk2d blackFade;

	private PauseManager pauseManager;

	private Dictionary<GameObject, int> highlightedObjects = new Dictionary<GameObject, int>();

	private Dictionary<GameObject, Vector3> objectPositions = new Dictionary<GameObject, Vector3>();

	private List<TutorialPopup> activePopups = new List<TutorialPopup>();

	private TutorialPopUp activeHeader;

	private TutorialHands activeTutorialHands;

	private TutorialHands activeArrow;

	private static CoreGameTutorialManager _instance;

	public static CoreGameTutorialManager instance => _instance;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Object.Destroy(gameObject);
		}
		else
		{
			_instance = this;
		}
	}

	private void Start()
	{
		pauseManager = PauseManager.instance;
		if (pauseManager != null)
		{
			pauseManager.Paused += HandlePaused;
		}
	}

	private void OnDestroy()
	{
		if (pauseManager != null)
		{
			pauseManager.Paused -= HandlePaused;
		}
		_instance = null;
	}

	public static void Highlight(GameObject go)
	{
		instance._Highlight(go);
	}

	private void _Highlight(GameObject go)
	{
		if (!highlightedObjects.ContainsKey(go))
		{
			highlightedObjects.Add(go, go.layer);
			objectPositions.Add(go, go.transform.position);
			tk2dCamera component = CameraUtility.GetCameraRenderingObject(go).GetComponent<tk2dCamera>();
			tk2dCamera tk2dCamera2 = ((component.CurrentResolutionOverride.autoScaleMode != tk2dCameraResolutionOverride.AutoScaleMode.FitHeight) ? cameraFitVisible : cameraFitHeight);
			int layer = LayerMask.NameToLayer((!(tk2dCamera2 == cameraFitHeight)) ? "Tutorial_FitVisible" : "Tutorial_FitHeight");
			go.SetLayerRecursively(layer);
			Vector3 position = component.ScreenCamera.WorldToScreenPoint(go.transform.position);
			Vector3 position2 = tk2dCamera2.ScreenCamera.ScreenToWorldPoint(position);
			go.transform.position = position2;
			if (highlightedObjects.Count == 1)
			{
				_ShowBlackFade(0.6f);
			}
		}
	}

	public static void UnHighlight(GameObject go)
	{
		instance._UnHighlight(go);
	}

	private void _UnHighlight(GameObject go)
	{
		if (highlightedObjects.ContainsKey(go))
		{
			go.SetLayerRecursively(highlightedObjects[go]);
			go.transform.position = objectPositions[go];
			highlightedObjects.Remove(go);
			objectPositions.Remove(go);
			if (highlightedObjects.Count == 0)
			{
				_RemoveBlackFade();
			}
		}
	}

	public static void ShowArrow(Transform target, TutorialArrowDirection direction = TutorialArrowDirection.Down, Vector3 offset = default(Vector3))
	{
		instance._ShowArrow(target, direction, offset);
	}

	private void _ShowArrow(Transform target, TutorialArrowDirection direction, Vector3 offset)
	{
		_RemoveArrow();
		activeArrow = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tk2dCamera component = CameraUtility.GetCameraRenderingObject(target.gameObject).GetComponent<tk2dCamera>();
		tk2dCamera tk2dCamera2 = ((component.CurrentResolutionOverride.autoScaleMode != tk2dCameraResolutionOverride.AutoScaleMode.FitHeight) ? cameraFitVisible : cameraFitHeight);
		int layer = LayerMask.NameToLayer((!(tk2dCamera2 == cameraFitHeight)) ? "Tutorial_FitVisible" : "Tutorial_FitHeight");
		activeArrow.gameObject.SetLayerRecursively(layer);
		Vector3 position = component.ScreenCamera.WorldToScreenPoint(target.position);
		Vector3 vector = tk2dCamera2.ScreenCamera.ScreenToWorldPoint(position);
		Vector3 position2 = vector + offset;
		position2.z = tk2dCamera2.transform.position.z + 200f;
		activeArrow.ShowArrow(position2, direction);
	}

	public static void RemoveArrow()
	{
		instance._RemoveArrow();
	}

	private void _RemoveArrow()
	{
		if (activeArrow != null)
		{
			ObjectPool.PoolObject(activeArrow.gameObject);
			activeArrow = null;
		}
	}

	public static void ShowTap(Transform target, Vector3 offset = default(Vector3))
	{
		instance._ShowTap(target, offset);
	}

	private void _ShowTap(Transform target, Vector3 offset)
	{
		_RemoveTutorialHands();
		activeTutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		tk2dCamera component = CameraUtility.GetCameraRenderingObject(target.gameObject).GetComponent<tk2dCamera>();
		tk2dCamera tk2dCamera2 = ((component.CurrentResolutionOverride.autoScaleMode != tk2dCameraResolutionOverride.AutoScaleMode.FitHeight) ? cameraFitVisible : cameraFitHeight);
		int layer = LayerMask.NameToLayer((!(tk2dCamera2 == cameraFitHeight)) ? "Tutorial_FitVisible" : "Tutorial_FitHeight");
		activeTutorialHands.gameObject.SetLayerRecursively(layer);
		Vector3 position = component.ScreenCamera.WorldToScreenPoint(target.position);
		Vector3 vector = tk2dCamera2.ScreenCamera.ScreenToWorldPoint(position);
		Vector3 position2 = vector + offset;
		position2.z = tk2dCamera2.transform.position.z + 200f;
		activeTutorialHands.ShowTap(position2);
	}

	public static void ShowDrag(Transform fromTarget, Transform toTarget)
	{
		instance._ShowDrag(fromTarget, toTarget);
	}

	private void _ShowDrag(Transform fromTarget, Transform toTarget)
	{
		_RemoveTutorialHands();
		activeTutorialHands = ObjectPool.GetObjectFromResource<TutorialHands>("Prefabs/GUI/Tutorial/Tutorial_Hands_Battle");
		Camera cameraRenderingObject = CameraUtility.GetCameraRenderingObject(fromTarget.gameObject);
		Camera cameraRenderingObject2 = CameraUtility.GetCameraRenderingObject(toTarget.gameObject);
		tk2dCamera component = cameraRenderingObject.GetComponent<tk2dCamera>();
		tk2dCamera component2 = cameraRenderingObject2.GetComponent<tk2dCamera>();
		tk2dCamera tk2dCamera2 = null;
		if (component == null && component2 == null)
		{
			tk2dCamera2 = cameraFitVisible;
		}
		else if (component != null)
		{
			tk2dCamera2 = ((component.CurrentResolutionOverride.autoScaleMode != tk2dCameraResolutionOverride.AutoScaleMode.FitHeight) ? cameraFitVisible : cameraFitHeight);
		}
		else
		{
			tk2dCamera2 = ((component2.CurrentResolutionOverride.autoScaleMode != tk2dCameraResolutionOverride.AutoScaleMode.FitHeight) ? cameraFitVisible : cameraFitHeight);
		}
		int layer = LayerMask.NameToLayer((!(tk2dCamera2 == cameraFitHeight)) ? "Tutorial_FitVisible" : "Tutorial_FitHeight");
		activeTutorialHands.gameObject.SetLayerRecursively(layer);
		Vector3 position = cameraRenderingObject.WorldToScreenPoint(fromTarget.position);
		Vector3 startPosition = tk2dCamera2.ScreenCamera.ScreenToWorldPoint(position);
		position = cameraRenderingObject2.WorldToScreenPoint(toTarget.position);
		Vector3 endPosition = tk2dCamera2.ScreenCamera.ScreenToWorldPoint(position);
		startPosition.z = tk2dCamera2.transform.position.z + 200f;
		endPosition.z = tk2dCamera2.transform.position.z + 200f;
		activeTutorialHands.ShowDrag(startPosition, endPosition, 300f, 0.25f);
	}

	public static void RemoveTutorialHands()
	{
		if (instance != null)
		{
			instance._RemoveTutorialHands();
		}
	}

	private void _RemoveTutorialHands()
	{
		if (activeTutorialHands != null)
		{
			ObjectPool.PoolObject(activeTutorialHands.gameObject);
			activeTutorialHands = null;
		}
	}

	public static void ShowPopup(string message, Transform target, TutorialPopup.TailPosition tailPosition = TutorialPopup.TailPosition.Bottom, Vector3 offset = default(Vector3))
	{
		instance._ShowPopup(message, target, tailPosition, offset);
	}

	private void _ShowPopup(string message, Transform target, TutorialPopup.TailPosition tailPosition = TutorialPopup.TailPosition.Bottom, Vector3 offset = default(Vector3))
	{
		TutorialPopup objectFromResource = ObjectPool.GetObjectFromResource<TutorialPopup>("Prefabs/GUI/Tutorial/Tutorial_PopUp_Battle");
		objectFromResource.messageText.text = message;
		objectFromResource.tailPosition = tailPosition;
		Camera cameraRenderingObject = CameraUtility.GetCameraRenderingObject(target.gameObject);
		tk2dCamera component = cameraRenderingObject.GetComponent<tk2dCamera>();
		tk2dCamera tk2dCamera2 = null;
		if (component == null)
		{
			tk2dCamera2 = cameraFitVisible;
		}
		else
		{
			tk2dCamera2 = ((component.CurrentResolutionOverride.autoScaleMode != tk2dCameraResolutionOverride.AutoScaleMode.FitHeight) ? cameraFitVisible : cameraFitHeight);
		}
		int layer = LayerMask.NameToLayer((!(tk2dCamera2 == cameraFitHeight)) ? "Tutorial_FitVisible" : "Tutorial_FitHeight");
		objectFromResource.gameObject.SetLayerRecursively(layer);
		objectFromResource.box.ResetCameras();
		Vector3 position = cameraRenderingObject.WorldToScreenPoint(target.position);
		Vector3 vector = tk2dCamera2.ScreenCamera.ScreenToWorldPoint(position);
		Vector3 position2 = vector + offset;
		position2.z = tk2dCamera2.transform.position.z + 200f;
		objectFromResource.transform.position = position2;
		activePopups.Add(objectFromResource);
	}

	public static void RemoveAllPopups()
	{
		instance._RemoveAllPopups();
	}

	private void _RemoveAllPopups()
	{
		int num = activePopups.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			ObjectPool.PoolObject(activePopups.Pop().gameObject);
		}
	}

	public static void ShowHeader(string message, float offset = 0f)
	{
		instance._ShowHeader(message, offset);
	}

	private void _ShowHeader(string message, float offset)
	{
		RemoveHeader();
		TutorialPopUp objectFromResource = ObjectPool.GetObjectFromResource<TutorialPopUp>("Prefabs/GUI/Tutorial/Tutorial_Header");
		objectFromResource.text.text = message;
		objectFromResource.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Tutorial_FitVisible"));
		Vector3 position = cameraFitVisible.transform.position + new Vector3(0f, offset, 0f);
		position.z = 0f;
		objectFromResource.transform.position = position;
		activeHeader = objectFromResource;
	}

	public static void RemoveHeader()
	{
		instance._RemoveHeader();
	}

	private void _RemoveHeader()
	{
		if (activeHeader != null)
		{
			ObjectPool.PoolObject(activeHeader.gameObject);
			activeHeader = null;
		}
	}

	public static void ShowBlackFade(float alpha)
	{
		instance._ShowBlackFade(alpha);
	}

	private void _ShowBlackFade(float alpha)
	{
		blackFade.FadeTo(alpha, 0.2f);
	}

	public static void RemoveBlackFade()
	{
		instance._RemoveBlackFade();
	}

	private void _RemoveBlackFade()
	{
		blackFade.FadeTo(0f, 0.2f);
	}

	private void HandlePaused(bool pauseStatus)
	{
		if (TitanTowerController.instance == null)
		{
			cameraFitHeight.ScreenCamera.enabled = !pauseStatus;
			cameraFitVisible.ScreenCamera.enabled = !pauseStatus;
		}
	}
}
