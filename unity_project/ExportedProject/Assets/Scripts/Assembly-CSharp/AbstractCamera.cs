using DG.Tweening;
using UnityEngine;

public class AbstractCamera : MonoBehaviour
{
	public tk2dSprite whiteFade;

	public Transform centerAnchor;

	private Transform _background;

	private static AbstractCamera _instance;

	public Camera camera { get; private set; }

	public static AbstractCamera instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
		camera = GetComponent<Camera>();
		whiteFade.gameObject.SetActive(false);
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public void Show(Object background)
	{
		ObjectPool.instance.AddType((GameObject)background, 1);
		_background = ObjectPool.GetObject(background.name).transform;
		camera.enabled = true;
		whiteFade.color = Color.white;
		whiteFade.gameObject.SetActive(true);
		DOTween.ToAlpha(() => whiteFade.color, (Color x) =>
		{
			whiteFade.color = x;
		}, 0f, 0.2f).SetEase(Ease.InSine);
		_background.parent = centerAnchor;
		_background.localPosition = Vector3.zero;
		_background.gameObject.SetActive(true);
	}

	public void Hide()
	{
		camera.enabled = false;
		_background.gameObject.SetActive(false);
		whiteFade.gameObject.SetActive(false);
		ObjectPool.PoolObject(_background.gameObject);
		_background = null;
	}
}
