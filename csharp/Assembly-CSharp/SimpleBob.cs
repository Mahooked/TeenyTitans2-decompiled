using DG.Tweening;
using UnityEngine;

public class SimpleBob : MonoBehaviour
{
	public float duration = 1f;

	public float deltaY = 10f;

	private Tweener tween;

	private void Start()
	{
		tween = transform.DOLocalMoveY(transform.localPosition.y + deltaY, duration).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
		tween.Goto(Random.Range(0f, tween.Duration(includeLoops: false)), andPlay: true);
	}

	public void Pause()
	{
		if (tween != null)
		{
			tween.Pause();
		}
	}

	public void Resume()
	{
		if (tween != null)
		{
			tween.Play();
		}
	}
}
