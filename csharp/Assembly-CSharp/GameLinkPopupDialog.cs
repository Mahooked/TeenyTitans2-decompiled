using DG.Tweening;
using TMPro;
using UnityEngine;

public class GameLinkPopupDialog : MonoBehaviour, IPoolable
{
	public tk2dSprite tint;

	public Transform animGrp;

	public TextMeshPro titleText;

	public TextMeshPro messageText;

	public TextMeshPro buttonText;

	public tk2dUIItem button;

	private static GameLinkPopupDialog instance;

	public static GameLinkPopupDialog Instance => instance;

	private void OnEnable()
	{
		button.OnClick += HandleOnClick;
	}

	private void OnDisable()
	{
		button.OnClick -= HandleOnClick;
	}

	private void _Show(string title, string message, string button)
	{
		message = message.Replace("\\n", "\n");
		titleText.text = title;
		messageText.text = message;
		buttonText.text = button;
		DOTween.ToAlpha(() => tint.color, (Color x) =>
		{
			tint.color = x;
		}, 0f, 0.2f).SetTarget(tint).From()
			.SetUpdate(isIndependentUpdate: true);
		animGrp.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 0.2f).From().SetEase(Ease.OutBack)
			.SetUpdate(isIndependentUpdate: true);
	}

	private void Close()
	{
		tint.DOKill(complete: true);
		animGrp.DOKill(complete: true);
		ObjectPool.PoolObject(instance.gameObject);
		instance = null;
	}

	private void HandleOnClick()
	{
		Close();
	}

	public static void Show(string title, string message, string button, string popupPath = "")
	{
		if (instance == null)
		{
			string path = ((!string.IsNullOrEmpty(popupPath)) ? popupPath : "Prefabs/GUI/GameLinkPopupDialog");
			instance = ObjectPool.GetObjectFromResource<GameLinkPopupDialog>(path, new Vector3(4000f, 0f, 0f));
			instance._Show(title, message, button);
		}
		else
		{
			Debug.Log("An instance of the popup dialog is already open, so ignoring this Show request");
		}
	}

	public void ResetObject()
	{
		tint.color = new Color(0f, 0f, 0f, 160f / 255f);
		animGrp.localScale = Vector3.one;
	}
}
