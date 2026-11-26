using System;
using UnityEngine;

[Serializable]
public class AccessoryAnimationCue
{
	public AccessoryEffectTriggerType animationTriggerType;

	public string animation;

	public string animationP2;

	public string animEventString;

	public float timerDuration;

	public bool triggerBattleWait;

	public int ignoreInterruptFrame;

	public bool triggerCameraAnimation;

	public RuntimeAnimatorController cameraAnimatorController;

	public string cameraAnimation;

	public bool playSoundEffect;

	public string soundEffectPath;

	public float soundEffectPlayOffset;

	public bool fadeOutMusicGroup;
}
