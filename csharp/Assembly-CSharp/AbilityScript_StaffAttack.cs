using System.Collections;
using DG.Tweening;
using UnityEngine;

public class AbilityScript_StaffAttack : AbilityScript_Default
{
	public override IEnumerator Play()
	{
		yield return base.Play();
		DOTween.Kill(_casterCamera.offset);
		_casterCamera.offset.DOLocalMove((!(_casterCamera.tag == "Player")) ? new Vector3(0f, -0.71f, -0.21f) : new Vector3(0f, 0.49f, 0.22f), 0.5f).SetEase(Ease.InOutSine);
		_casterCamera.offset.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
	}
}
