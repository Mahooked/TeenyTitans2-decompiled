public class Fear : StatusEffect
{
	private ActionMeter targetMeter;

	private ActionMeter casterMeter;

	private CharacterStatType statType;

	private int casterStatBonus;

	private int targetStatBonus;

	public override StatusEffectType type => StatusEffectType.Debuff;

	public override bool isStackable => false;

	public override bool isResettable => false;

	public override bool isZoomedOut => false;

	public Fear(ActionMeter casterMeter, ActionMeter targetMeter, CharacterStatType statType, int casterStatBonus, int targetStatBonus, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.casterMeter = casterMeter;
		this.targetMeter = targetMeter;
		this.statType = statType;
		this.casterStatBonus = casterStatBonus;
		this.targetStatBonus = targetStatBonus;
	}

	protected override void InitEffect()
	{
		StatusEffectManager.instance.AddActionMeterStatusEffect(casterMeter, new TeamStatBuff(casterMeter, statType, casterStatBonus));
		StatusEffectManager.instance.AddActionMeterStatusEffect(targetMeter, new TeamStatBuff(targetMeter, statType, targetStatBonus));
	}

	protected override void UpdateEffect()
	{
		Remove();
	}

	protected override void ResetEffect()
	{
	}

	protected override void RemoveEffect()
	{
	}

	protected override void CleanUp()
	{
	}
}
