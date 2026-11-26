public class ActionMeterFiller : StatusEffect
{
	private ActionMeter actionMeter;

	private float fillAmount;

	public override StatusEffectType type
	{
		get
		{
			return (fillAmount > 0f) ? StatusEffectType.Buff : StatusEffectType.Debuff;
		}
	}

	public override bool isStackable
	{
		get
		{
			return false;
		}
	}

	public override bool isResettable
	{
		get
		{
			return false;
		}
	}

	public override bool isZoomedOut
	{
		get
		{
			return false;
		}
	}

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
