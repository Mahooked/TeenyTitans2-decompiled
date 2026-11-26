public class ActionMeterFiller : StatusEffect
{
	private ActionMeter actionMeter;

	private float fillAmount;

	public override StatusEffectType type => (fillAmount > 0f) ? StatusEffectType.Buff : StatusEffectType.Debuff;

	public override bool isStackable => false;

	public override bool isResettable => false;

	public override bool isZoomedOut => false;

	public ActionMeterFiller(ActionMeter actionMeter, float fillAmount, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.actionMeter = actionMeter;
		this.fillAmount = fillAmount;
	}

	protected override void InitEffect()
	{
		actionMeter.progress += fillAmount;
		Remove();
	}

	protected override void UpdateEffect()
	{
	}

	protected override void ResetEffect()
	{
	}

	protected override void RemoveEffect()
	{
		actionMeter = null;
	}

	protected override void CleanUp()
	{
	}
}
