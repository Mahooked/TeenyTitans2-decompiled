using UnityEngine;

public class RemoteBomb : StatusEffect
{
	private ActionMeter actionMeter;

	private ActionMeter casterMeter;

	private string bombPrefabPath;

	private int placeAbilityUID;

	private int detonateAbilityUID;

	private GameObject bombObj;

	private AbilityButton detonateButton;

	private AbilityData detonateAbilityData;

	private AbilityData placeAbilityData;

	private bool isPlaceAbilityShiny;

	private int currentAttackDamage;

	private int previousAttackDamage;

	private float timeToMax;

	private Character casterCharacter;

	private bool casterIsActiveCharacter;

	public float timeElapsed { get; private set; }

	public override StatusEffectType type
	{
		get
		{
			return StatusEffectType.Debuff;
		}
	}

	public override bool isStackable
	{
		get
		{
			return true;
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
			return true;
		}
	}

	public override bool isCleansable
	{
		get
		{
			return true;
		}
	}

	public Character CasterCharacter
	{
		get
		{
			return casterCharacter;
		}
	}

	public GameObject BombObj
	{
		get
		{
			return bombObj;
		}
	}

	public RemoteBomb(ActionMeter actionMeter, ActionMeter casterMeter, string bombPrefabPath, int detonateAbilityUID, int placeAbilityUID, Accessory casterAccessory = null)
		: base(casterAccessory)
	{
		this.actionMeter = actionMeter;
		this.casterMeter = casterMeter;
		this.bombPrefabPath = bombPrefabPath;
		this.detonateAbilityUID = detonateAbilityUID;
		this.placeAbilityUID = placeAbilityUID;
	}

	public void SetAbilityButton(AbilityButton btn)
	{
		detonateButton = btn;
	}

	protected override void InitEffect()
	{
		casterIsActiveCharacter = true;
		casterCharacter = casterMeter.character.character;
		casterMeter.CharacterChanged += HandleCasterCharacterChanged;
		casterCharacter.Death += HandleCharacterDeath;
		bombObj = ObjectPool.GetObjectFromResource(bombPrefabPath);
		bombObj.transform.position = actionMeter.character.model.position;
		bombObj.transform.localScale = Vector3.Scale(actionMeter.character.transform.localScale, new Vector3(1f, 1f, -1f));
		timeElapsed = 0f;
		currentAttackDamage = 0;
		previousAttackDamage = 0;
		detonateAbilityData = GameManager.instance.abilityDatabase.GetAbility(detonateAbilityUID);
		placeAbilityData = GameManager.instance.abilityDatabase.GetAbility(placeAbilityUID);
		isPlaceAbilityShiny = casterCharacter.IsAbilityShiny(placeAbilityData);
		timeToMax = ((!isPlaceAbilityShiny) ? detonateAbilityData.floatValue1 : detonateAbilityData.shinyEffect.floatValue1);
	}

	protected override void UpdateEffect()
	{
		if (BattleController.instance.waiting)
		{
			return;
		}
		timeElapsed += base.DeltaTime;
		if (!casterIsActiveCharacter)
		{
			return;
		}
		int num = TTGAbilityCalculator.CalculateDamage(detonateAbilityData, casterCharacter, detonateButton.position, true, true, true, isPlaceAbilityShiny);
		int num2 = TTGAbilityCalculator.CalculateDamage(detonateAbilityData, casterCharacter, detonateButton.position, true, true, true, isPlaceAbilityShiny, true);
		currentAttackDamage = Mathf.FloorToInt(Mathf.Lerp(num, num2, Mathf.Clamp01(timeElapsed / timeToMax)));
		currentAttackDamage = DiminishingReturns.instance.GetDiminishedValue(currentAttackDamage, casterCharacter, detonateAbilityData);
		if (previousAttackDamage != currentAttackDamage)
		{
			previousAttackDamage = currentAttackDamage;
			if (detonateButton != null)
			{
				detonateButton.ForceUpdate();
			}
		}
	}

	protected override void RemoveEffect()
	{
		casterMeter.CharacterChanged -= HandleCasterCharacterChanged;
		casterCharacter.Death -= HandleCharacterDeath;
		if (casterMeter.character.character == casterCharacter)
		{
			AbilityButton abilityButton = casterMeter.abilityButtons.Find((AbilityButton x) => x.abilityData != null && x.abilityData.uid == detonateAbilityUID);
			if (abilityButton != null)
			{
				ActionMeter actionMeter = this.actionMeter;
				abilityButton.SwapAbility(placeAbilityData, () => true);
			}
		}
		ObjectPool.PoolObject(bombObj);
		bombObj = null;
	}

	private void HandleCasterCharacterChanged()
	{
		casterIsActiveCharacter = casterMeter.character.character == casterCharacter;
	}

	private void HandleCharacterDeath()
	{
		Remove();
	}

	protected override void CleanUp()
	{
	}

	protected override void ResetEffect()
	{
	}
}
