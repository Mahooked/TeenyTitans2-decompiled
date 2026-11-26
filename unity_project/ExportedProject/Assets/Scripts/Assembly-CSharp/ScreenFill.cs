using System.Collections;
using DG.Tweening;
using UnityEngine;

public class ScreenFill : MonoBehaviour
{
	[SerializeField]
	private tk2dSprite fillSprite;

	[SerializeField]
	private Renderer fillSpriteRenderer;

	private IEnumerator activeFlashSequence;

	private static ScreenFill _instance;

	public static ScreenFill instance
	{
		get
		{
			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
		fillSprite.color = new Color(1f, 1f, 1f, 0f);
		fillSpriteRenderer.enabled = false;
	}

	public void Flash(Color color, float duration)
	{
		if (activeFlashSequence != null)
		{
			StopCoroutine(activeFlashSequence);
		}
		activeFlashSequence = FlashSequence(color, duration);
		StartCoroutine(activeFlashSequence);
	}

	private IEnumerator FlashSequence(Color color, float duration)
	{
		fillSprite.color = color;
		fillSpriteRenderer.enabled = true;
		yield return DOTween.To(() => fillSprite.color, (Color x) =>
		{
			fillSprite.color = x;
		}, new Color(color.r, color.g, color.b, 0f), duration).WaitForCompletion();
		fillSpriteRenderer.enabled = false;
		activeFlashSequence = null;
	}
}
