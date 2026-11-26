public class AbilityBatteryBuff : StatusEffect
{
	private Character character;

	private ActionMeter meter;

	private AbilityData ability;

	private BattleController controller;

	private AbilityButton _button;

	private bool activeOnCharacter;

	public AbilityData abilityData => ability;

	public AbilityButton button => _button;

	public override StatusEffectType type => StatusEffectType.Buff;

	public override bool isStackable => true;

	public override bool isResettable => true;

	public override bool isZoomedOut => false;

	public AbilityBatteryBuff(Character character, ActionMeter meter, AbilityButton button, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.character = character;
		this.meter = meter;
		ability = ability;
		this.meter.AbilityUsed += HandleAbilityUsed;
		this.meter.AbilityFinished += HandleAbilityFinished;
		_button = button;
		ability = button.abilityData;
		activeOnCharacter = true;
		Player.instance.BatteriesAdded += UpdateBuffWithBatteryLevel;
		Player.instance.BatteriesRemoved += UpdateBuffWithBatteryLevel;
	}

	private void HandleAbilityUsed(ActionMeter actionMeter, Character character, AbilityData abilityData)
	{
	}

	private void HandleAbilityFinished(ActionMeter actionMeter, Character character, AbilityData abilityData)
	{
		if (activeOnCharacter && meter != null && actionMeter != null && actionMeter == meter && abilityData.uid == ability.uid)
		{
			BattleController.instance.batteryGUI.RemoveAllBatteriesForCharacterAbility(character, _button);
			Remove();
		}
	}

	private void HandleCharacterChanged()
	{
		if (meter.character != null && meter.character.character != null)
		{
			if (meter.character.character == character)
			{
				activeOnCharacter = true;
				_button.DisplayBuffBattery(active: true);
				UpdateBuffWithBatteryLevel();
			}
			else
			{
				activeOnCharacter = false;
				_button.DisplayBuffBattery(active: false);
			}
		}
	}

	protected override void InitEffect()
	{
		controller = BattleController.instance;
		meter.CharacterChanged += HandleCharacterChanged;
		AbilityButton abilityButton = meter.GetAbilityButton(ability);
		if (abilityButton != null)
		{
			UpdateBuffWithBatteryLevel();
		}
	}

	protected void UpdateBuffWithBatteryLevel(int totalBatteries)
	{
		UpdateBuffWithBatteryLevel();
	}

	public void UpdateBuffWithBatteryLevel()
	{
		if (!activeOnCharacter)
		{
			if (_button != null)
			{
				_button.DisplayBuffBattery(active: false);
			}
			meter.ForceUpdateButtons();
			return;
		}
		int batteryCountForAbility = BattleController.instance.batteryGUI.GetBatteryCountForAbility(character, ability);
		if (batteryCountForAbility <= 1)
		{
			_button.buffBatteryAnimator.Play("battery_electricity_stage01");
		}
		else if (batteryCountForAbility == 2)
		{
			_button.buffBatteryAnimator.Play("battery_electricity_stage02");
		}
		else
		{
			_button.buffBatteryAnimator.Play("battery_electricity_stage03");
		}
		meter.ForceUpdateButtons();
	}

	public AbilityShinyEffect CurrentAbilityBatteryEffect()
	{
		return BattleController.instance.batteryGUI.abilityBatteryEffects.Find((AbilityDataBatteryEffectType x) => x.abilityType == ability.type).effect;
	}

	protected override void UpdateEffect()
	{
	}

	protected override void ResetEffect()
	{
	}

	protected override void RemoveEffect()
	{
		if (meter != null)
		{
			meter.AbilityUsed -= HandleAbilityUsed;
			meter.AbilityFinished -= HandleAbilityFinished;
			meter.CharacterChanged -= HandleCharacterChanged;
			meter.ForceUpdateButtons();
		}
		Player.instance.BatteriesAdded -= UpdateBuffWithBatteryLevel;
		Player.instance.BatteriesRemoved -= UpdateBuffWithBatteryLevel;
		if (_button != null)
		{
			_button.DisplayBuffBattery(active: false);
		}
		activeOnCharacter = false;
		character = null;
		meter = null;
		ability = null;
		controller = null;
		_button = null;
	}

	protected override void CleanUp()
	{
	}
}
