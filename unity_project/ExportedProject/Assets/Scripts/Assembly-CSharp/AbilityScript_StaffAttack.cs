using System.Collections;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

public class AbilityScript_StaffAttack : AbilityScript_Default
{
	public override IEnumerator Play()
	{
		yield return _003CPlay_003E__BaseCallProxy0();
		DOTween.Kill(_casterCamera.offset);
		_casterCamera.offset.DOLocalMove((!(_casterCamera.tag == "Player")) ? new Vector3(0f, -0.71f, -0.21f) : new Vector3(0f, 0.49f, 0.22f), 0.5f).SetEase(Ease.InOutSine);
		_casterCamera.offset.DOLocalRotate(Vector3.zero, 0.5f).SetEase(Ease.InOutSine);
	}

	[CompilerGenerated]
	[DebuggerHidden]
	private IEnumerator _003CPlay_003E__BaseCallProxy0()
	{
		return base.Play();
	}
}
