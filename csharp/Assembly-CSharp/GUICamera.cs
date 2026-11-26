using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(tk2dCamera))]
public class GUICamera : MonoBehaviour
{
	private Camera thisCamera;

	private Dictionary<tk2dBaseSprite.Anchor, Transform> anchorDict;

	private static GUICamera _instance;

	public Camera camera => thisCamera;

	public Transform anchorLowerCenter => GetAnchor(tk2dBaseSprite.Anchor.LowerCenter);

	public Transform anchorLowerLeft => GetAnchor(tk2dBaseSprite.Anchor.LowerLeft);

	public Transform anchorLowerRight => GetAnchor(tk2dBaseSprite.Anchor.LowerRight);

	public Transform anchorMiddleCenter => GetAnchor(tk2dBaseSprite.Anchor.MiddleCenter);

	public Transform anchorMiddleLeft => GetAnchor(tk2dBaseSprite.Anchor.MiddleLeft);

	public Transform anchorMiddleRight => GetAnchor(tk2dBaseSprite.Anchor.MiddleRight);

	public Transform anchorUpperCenter => GetAnchor(tk2dBaseSprite.Anchor.UpperCenter);

	public Transform anchorUpperLeft => GetAnchor(tk2dBaseSprite.Anchor.UpperLeft);

	public Transform anchorUpperRight => GetAnchor(tk2dBaseSprite.Anchor.UpperRight);

	public static GUICamera instance => _instance;

	private void Awake()
	{
		_instance = this;
		thisCamera = GetComponent<Camera>();
		anchorDict = new Dictionary<tk2dBaseSprite.Anchor, Transform>();
		tk2dCameraAnchor[] componentsInChildren = thisCamera.GetComponentsInChildren<tk2dCameraAnchor>();
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			if (componentsInChildren[i].transform.parent == transform)
			{
				if (anchorDict.ContainsKey(componentsInChildren[i].AnchorPoint))
				{
					Object.Destroy(componentsInChildren[i].gameObject);
				}
				else
				{
					anchorDict.Add(componentsInChildren[i].AnchorPoint, componentsInChildren[i].transform);
				}
			}
		}
	}

	private void OnDestroy()
	{
		if (_instance == this)
		{
			_instance = null;
		}
	}

	public Vector3 ScreenToWorldPoint(Vector3 screenPosition)
	{
		return camera.ScreenToWorldPoint(screenPosition);
	}

	public Transform GetAnchor(tk2dBaseSprite.Anchor anchorPoint)
	{
		if (anchorDict.ContainsKey(anchorPoint))
		{
			return anchorDict[anchorPoint];
		}
		return CreateNewAnchor(anchorPoint);
	}

	private Transform CreateNewAnchor(tk2dBaseSprite.Anchor anchorPoint)
	{
		if (anchorDict.ContainsKey(anchorPoint))
		{
			return anchorDict[anchorPoint];
		}
		GameObject gameObject = new GameObject("Anchor (" + anchorPoint.ToString() + ")");
		gameObject.transform.parent = thisCamera.transform;
		gameObject.transform.localPosition = new Vector3(0f, 0f, 0f - thisCamera.transform.localPosition.z);
		gameObject.transform.localRotation = Quaternion.identity;
		gameObject.transform.localScale = Vector3.one;
		tk2dCameraAnchor tk2dCameraAnchor2 = gameObject.AddComponent<tk2dCameraAnchor>();
		tk2dCameraAnchor2.AnchorCamera = thisCamera;
		tk2dCameraAnchor2.AnchorPoint = anchorPoint;
		anchorDict.Add(anchorPoint, tk2dCameraAnchor2.transform);
		return tk2dCameraAnchor2.transform;
	}
}
