using System;

[Serializable]
public class AccessoryItemEffect : ModChipEffect
{
	public AccessoryEffectTriggerType accessoryTriggerType = AccessoryEffectTriggerType.OnActivation;

	public string animEventString;

	public RangedFloat timerDuration;

	public bool tickImmediately;

	public bool playAnimation;

	public bool playSoundEffect;

	public string animationName;

	public string animationNameEnemy;

	public float animationPlayOffset;

	public string soundEffectPath;

	public float soundEffectDelay;

	public bool autoReapplyEffect;

	public bool isApplied { get; set; }

	public bool hasTriggered { get; set; }
}
