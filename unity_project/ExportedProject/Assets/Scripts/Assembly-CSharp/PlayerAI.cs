using System.Collections.Generic;
using UnityEngine;

public class PlayerAI : MonoBehaviour
{
	private static Dictionary<AbilityType, int> abilityUseLimits = new Dictionary<AbilityType, int> { 
	{
		AbilityType.Transformation,
		1
	} };

	private static Dictionary<string, int> scriptUseLimits = new Dictionary<string, int>
	{
		{ "AbilityScript_Nuhuh", 1 },
		{ "AbilityScript_Tofu", 1 }
	};

	protected BattleController battleController;

	protected ActionMeter actionMeter;

	protected BattleCharacter battleCharacter;

	protected Character character;

	protected PortraitSwitcher portraits;

	protected List<Character> party;

	protected AccessoryBattleButton accessoryBattleButton;

	protected ActionMeter enemyActionMeter;

	protected BattleCharacter enemyCharacter;

	protected List<Character> enemyParty;

	protected int numEnemyAbilityUsesSinceLast;

	private float thinkCounter;

	private bool forceDecision;

	private AbilityData lastUsedAbility;

	private int lastUsedConsecutiveCount;

	private int numAbilitiesToSwapCount;

	private int numAbilitiesToSwapNeeded;

	private Dictionary<AbilityData, int> consecutiveUseCounts = new Dictionary<AbilityData, int>();

	private Dictionary<Character, int> consecutiveDodgeCounts = new Dictionary<Character, int>();

	private Dictionary<Character, int> consecutiveDodgeMaxCounts = new Dictionary<Character, int>();

	private float emoteTimer;

	private float emoteDelay = 10f;

	private bool _isEnabled = true;

	protected virtual int defaultAbilityUseLimit
	{
		get
		{
			return aiConfig.consecutiveAbilityUseLimit;
		}
	}

	protected virtual float thinkDelay
	{
		get
		{
			return aiConfig.decisionInterval;
		}
	}

	protected virtual float indecisiveChance
	{
		get
		{
			return aiConfig.brainFartProbability;
		}
	}

	protected virtual RangedFloat abilityDelay
	{
		get
		{
			return aiConfig.abilityDelay;
		}
	}

	protected virtual float healthThreshold
	{
		get
		{
			return aiConfig.healthThreshold;
		}
	}

	protected virtual RangedInt numAbilitiesToSwap
	{
		get
		{
			return aiConfig.minAbilitiesBeforeSwap;
		}
	}

	protected virtual float swapChance
	{
		get
		{
			return aiConfig.swapProbability;
		}
	}

	protected virtual int numConsecutiveAbilitiesToForce
	{
		get
		{
			return 5;
		}
	}

	private AIConfig aiConfig
	{
		get
		{
			return GameManager.instance.aiConfig;
		}
	}

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			_isEnabled = value;
		}
	}

	protected virtual void Awake()
	{
		actionMeter = GetComponent<ActionMeter>();
		thinkCounter = thinkDelay;
		numAbilitiesToSwapNeeded = numAbilitiesToSwap.GetRandomValue();
	}

	protected virtual void Start()
	{
		battleController = BattleController.instance;
		if (actionMeter == battleController.playerActionMeter)
		{
			portraits = battleController.playerPortraits;
			party = battleController.playerParty;
			accessoryBattleButton = battleController.playerAccessoryButton;
			enemyActionMeter = battleController.enemyActionMeter;
			enemyParty = battleController.enemyParty;
		}
		else
		{
			portraits = battleController.enemyPortraits;
			party = battleController.enemyParty;
			accessoryBattleButton = battleController.enemyAccessoryButton;
			enemyActionMeter = battleController.playerActionMeter;
			enemyParty = battleController.playerParty;
		}
		actionMeter.AbilityUsed += HandleAbilityUsed;
		actionMeter.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged();
		enemyActionMeter.AbilityUsed += HandleEnemyAbilityUsed;
		enemyActionMeter.CharacterChanged += HandleEnemyCharacterChanged;
		HandleEnemyCharacterChanged();
		OnEnable();
	}

	protected virtual void OnEnable()
	{
		int count = actionMeter.abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			actionMeter.abilityButtons[i].boxCollider2D.enabled = false;
		}
		if (portraits != null && portraits.boxCollider2D != null)
		{
			portraits.boxCollider2D.enabled = false;
		}
		if (accessoryBattleButton != null)
		{
			accessoryBattleButton.boxCollider2D.enabled = false;
		}
	}

	protected virtual void OnDisable()
	{
		int count = actionMeter.abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			actionMeter.abilityButtons[i].boxCollider2D.enabled = true;
		}
		if (portraits != null && (bool)portraits.boxCollider2D)
		{
			portraits.boxCollider2D.enabled = true;
		}
		if (accessoryBattleButton != null)
		{
			accessoryBattleButton.boxCollider2D.enabled = true;
		}
	}

	protected virtual void OnDestroy()
	{
		if (character != null)
		{
			character.IncomingDamage -= HandleIncomingDamage;
			character.DamageReceived -= HandleDamageReceived;
			character = null;
		}
		if (actionMeter != null)
		{
			actionMeter.AbilityUsed -= HandleAbilityUsed;
			actionMeter.CharacterChanged -= HandleCharacterChanged;
			actionMeter = null;
		}
		if (enemyActionMeter != null)
		{
			enemyActionMeter.AbilityUsed -= HandleEnemyAbilityUsed;
			enemyActionMeter.CharacterChanged -= HandleEnemyCharacterChanged;
			enemyActionMeter = null;
		}
	}

	protected virtual void Update()
	{
		if (_isEnabled && !actionMeter.isPaused && !battleController.waiting && !actionMeter.isCasting && !actionMeter.isChanneling && BattleController.battleDefinition != null)
		{
			if (thinkCounter <= 0f)
			{
				MakeDecision();
				thinkCounter += thinkDelay;
			}
			else
			{
				thinkCounter -= Time.deltaTime;
			}
		}
	}

	protected virtual void MakeDecision()
	{
		List<AbilityButton> activatedAbilityButtons = actionMeter.GetActivatedAbilityButtons();
		AbilityButton abilityButton = null;
		if (CheckForSwap())
		{
			return;
		}
		if (accessoryBattleButton.currentFillAmount >= 1f)
		{
			accessoryBattleButton.ClickAccessory();
		}
		if (activatedAbilityButtons.Count <= 0 || (!forceDecision && !(Random.value > indecisiveChance) && !Mathf.Approximately(actionMeter.progress, 1f)))
		{
			return;
		}
		bool flag = true;
		if (IsPartyHurt(party, healthThreshold))
		{
			List<AbilityButton> list = activatedAbilityButtons.FindAll((AbilityButton obj) => obj.abilityData != null && obj.abilityData.type == AbilityType.Heal);
			if (list.Count > 0 && Random.value <= 0.25f)
			{
				while (list.Count > 0)
				{
					int index = Random.Range(0, list.Count);
					AbilityButton abilityButton2 = list[index];
					list.RemoveAt(index);
					if (CanUseAbility(abilityButton2.abilityData))
					{
						abilityButton = abilityButton2;
						break;
					}
				}
			}
			flag = false;
		}
		if (abilityButton == null)
		{
			List<AbilityButton> list = ((!flag || !IsPartyHurt(party) || !(Random.value < 0.5f)) ? activatedAbilityButtons.FindAll((AbilityButton obj) => obj.abilityData.type != AbilityType.Heal) : new List<AbilityButton>(activatedAbilityButtons));
			AbilityButton abilityButton2;
			if (actionMeter.progress >= 0.85f)
			{
				abilityButton2 = list.Find((AbilityButton obj) => obj.abilityData.type == AbilityType.ActionMeter && obj.abilityData.actionMeterEffect == ActionMeterEffect.Fill);
				if (abilityButton2 != null)
				{
					list.Remove(abilityButton2);
				}
			}
			abilityButton2 = list.Find((AbilityButton obj) => obj.abilityData.type == AbilityType.StatusEffect && obj.abilityData.statusEffect == StatusEffectName.ActionMeterSpeedModifier && obj.abilityData.bonus > 0);
			if (abilityButton2 != null)
			{
				ActionMeterSpeedModifier statusEffect = actionMeter.GetStatusEffect<ActionMeterSpeedModifier>();
				if (statusEffect != null && statusEffect.isBonus)
				{
					list.Remove(abilityButton2);
				}
			}
			abilityButton2 = list.Find((AbilityButton obj) => obj.abilityData.type == AbilityType.StatusEffect && obj.abilityData.statusEffect == StatusEffectName.ProtectionBubble);
			if (abilityButton2 != null)
			{
				ProtectionBubble statusEffect2 = actionMeter.GetStatusEffect<ProtectionBubble>();
				if (statusEffect2 != null)
				{
					list.Remove(abilityButton2);
				}
			}
			abilityButton2 = list.Find((AbilityButton obj) => obj.abilityData.scriptName == "AbilityScript_Nuhuh");
			if (abilityButton2 != null)
			{
				bool flag2 = enemyCharacter.character.HasStatusEffect(StatusEffectType.Buff) || enemyActionMeter.HasStatusEffect(StatusEffectType.Buff);
				bool flag3 = character.HasStatusEffect(StatusEffectType.Debuff) || actionMeter.HasStatusEffect(StatusEffectType.Debuff);
				if (!flag2 && !flag3)
				{
					list.Remove(abilityButton2);
				}
			}
			while (list.Count > 0)
			{
				int index2 = Random.Range(0, list.Count);
				abilityButton2 = list[index2];
				list.RemoveAt(index2);
				if (CanUseAbility(abilityButton2.abilityData))
				{
					abilityButton = abilityButton2;
					break;
				}
			}
		}
		if (abilityButton != null)
		{
			if (abilityButton.abilityData.scriptName == "AbilityScript_Tofu")
			{
				List<TofuPickup> allObjects = BattleObjectManager.instance.GetAllObjects<TofuPickup>();
				if (allObjects.Count > 0 && (bool)allObjects.Find((TofuPickup obj) => !obj.isPlayerTofu))
				{
					abilityButton = null;
					Debug.Log("Tofu Pickup already exists, so not using Tofu ability");
				}
			}
			PressButton(abilityButton);
			return;
		}
		Character swapCharacter = GetSwapCharacter();
		if (swapCharacter != null)
		{
			portraits.SelectCharacter(swapCharacter);
			return;
		}
		bool flag4 = true;
		AbilityData abilityData = null;
		int count = actionMeter.abilityButtons.Count;
		for (int num = 0; num < count; num++)
		{
			abilityData = actionMeter.abilityButtons[num].abilityData;
			int num2 = consecutiveUseCounts[abilityData];
			if (!(abilityData == null))
			{
				if (abilityData.type == AbilityType.Heal && IsPartyHurt(party) && (abilityData != lastUsedAbility || (num2 < (abilityData.overrideAbilityUseLimit ? abilityData.abilityUseLimitOverride : ((!abilityUseLimits.ContainsKey(abilityData.type)) ? defaultAbilityUseLimit : abilityUseLimits[abilityData.type])) && num2 < (abilityData.overrideAbilityUseLimit ? abilityData.abilityUseLimitOverride : ((!scriptUseLimits.ContainsKey(abilityData.scriptName)) ? defaultAbilityUseLimit : scriptUseLimits[abilityData.scriptName])))))
				{
					flag4 = false;
				}
				else if (abilityData.type == AbilityType.Transformation && lastUsedAbility != null && lastUsedAbility.type != AbilityType.Transformation)
				{
					flag4 = false;
				}
				else if (abilityData != lastUsedAbility || (num2 < (abilityData.overrideAbilityUseLimit ? abilityData.abilityUseLimitOverride : ((!abilityUseLimits.ContainsKey(abilityData.type)) ? defaultAbilityUseLimit : abilityUseLimits[abilityData.type])) && num2 < (abilityData.overrideAbilityUseLimit ? abilityData.abilityUseLimitOverride : ((!scriptUseLimits.ContainsKey(abilityData.scriptName)) ? defaultAbilityUseLimit : scriptUseLimits[abilityData.scriptName]))))
				{
					flag4 = false;
				}
			}
		}
		if (flag4)
		{
			lastUsedAbility = null;
			lastUsedConsecutiveCount = 0;
			consecutiveUseCounts.Clear();
		}
	}

	protected bool IsPartyHurt(List<Character> checkParty, float threshold = 1f)
	{
		int count = checkParty.Count;
		for (int i = 0; i < count; i++)
		{
			if (checkParty[i].isAlive && checkParty[i].hitPointsRatio < threshold)
			{
				return true;
			}
		}
		return false;
	}

	protected bool CanUseAbility(AbilityData ability)
	{
		if (ability == null)
		{
			return false;
		}
		if (lastUsedAbility != null && ability.type == AbilityType.Transformation && lastUsedAbility.type == AbilityType.Transformation)
		{
			return false;
		}
		if (character.level >= 3 && ability == lastUsedAbility)
		{
			int num = (ability.overrideAbilityUseLimit ? ability.abilityUseLimitOverride : ((!abilityUseLimits.ContainsKey(ability.type)) ? defaultAbilityUseLimit : abilityUseLimits[ability.type]));
			int num2 = (ability.overrideAbilityUseLimit ? ability.abilityUseLimitOverride : ((!scriptUseLimits.ContainsKey(ability.scriptName)) ? defaultAbilityUseLimit : scriptUseLimits[ability.scriptName]));
			int num3 = consecutiveUseCounts[ability];
			if (num3 >= num || num3 >= num2)
			{
				return false;
			}
		}
		return true;
	}

	protected Character GetSwapCharacter()
	{
		if (portraits.isEnabled && party.Count > 1)
		{
			List<Character> list = new List<Character>();
			int count = party.Count;
			for (int i = 0; i < count; i++)
			{
				if (party[i] != battleCharacter.character && !party[i].isDead && !party[i].isKnockedOut)
				{
					list.Add(party[i]);
				}
			}
			if (list.Count > 0)
			{
				return list[Random.Range(0, list.Count)];
			}
		}
		return null;
	}

	protected virtual bool CheckForSwap()
	{
		if (portraits.isEnabled && numAbilitiesToSwapCount >= numAbilitiesToSwapNeeded && swapChance > 0f && Random.value <= swapChance)
		{
			Character swapCharacter = GetSwapCharacter();
			if (swapCharacter != null)
			{
				portraits.SelectCharacter(swapCharacter);
				return true;
			}
		}
		return false;
	}

	protected virtual void PressButton(AbilityButton button)
	{
		if (button == null || (BattleController.battleDefinition.playerAIType != PlayerAIType.Network && !button.isActivated))
		{
			return;
		}
		button.Press();
		forceDecision = false;
		if (button.abilityData == lastUsedAbility)
		{
			lastUsedConsecutiveCount++;
			consecutiveUseCounts[button.abilityData]++;
		}
		else
		{
			lastUsedAbility = button.abilityData;
			lastUsedConsecutiveCount = 1;
			consecutiveUseCounts[button.abilityData] = 1;
			AbilityData abilityData = null;
			int count = character.abilities.Count;
			for (int i = 0; i < count; i++)
			{
				abilityData = character.abilities[i];
				if (abilityData != button.abilityData && consecutiveUseCounts[abilityData] > 0)
				{
					consecutiveUseCounts[abilityData]--;
				}
			}
		}
		numAbilitiesToSwapCount++;
		thinkCounter += abilityDelay.GetRandomValue();
	}

	private void HandleAbilityUsed(ActionMeter actionMeter, Character character, AbilityData ability)
	{
		numEnemyAbilityUsesSinceLast = 0;
	}

	private void HandleEnemyAbilityUsed(ActionMeter actionMeter, Character character, AbilityData ability)
	{
		numEnemyAbilityUsesSinceLast++;
		if (numEnemyAbilityUsesSinceLast >= numConsecutiveAbilitiesToForce)
		{
			forceDecision = true;
		}
	}

	private void HandleCharacterChanged()
	{
		if (character != null)
		{
			character.IncomingDamage -= HandleIncomingDamage;
			character.DamageReceived -= HandleDamageReceived;
			if (actionMeter.character.character != character)
			{
				lastUsedAbility = null;
				lastUsedConsecutiveCount = 0;
				numAbilitiesToSwapCount = 0;
				numAbilitiesToSwapNeeded = numAbilitiesToSwap.GetRandomValue();
				consecutiveUseCounts.Clear();
			}
		}
		battleCharacter = actionMeter.character;
		character = battleCharacter.character;
		if (character == null)
		{
			return;
		}
		character.IncomingDamage += HandleIncomingDamage;
		character.DamageReceived += HandleDamageReceived;
		if (!consecutiveDodgeMaxCounts.ContainsKey(character))
		{
			consecutiveDodgeMaxCounts.Add(character, Random.Range(1, 3));
		}
		consecutiveUseCounts.Clear();
		int count = character.abilities.Count;
		for (int i = 0; i < count; i++)
		{
			if (!consecutiveUseCounts.ContainsKey(character.abilities[i]))
			{
				consecutiveUseCounts.Add(character.abilities[i], 0);
			}
		}
	}

	public virtual void HandleIncomingDamage(DamageResult damageResult)
	{
		if (consecutiveDodgeCounts.ContainsKey(character) && consecutiveDodgeCounts[character] >= consecutiveDodgeMaxCounts[character])
		{
			damageResult.isDodged = false;
		}
	}

	public virtual void HandleDamageReceived(DamageResult damageResult)
	{
		if (damageResult.isDodged)
		{
			if (consecutiveDodgeCounts.ContainsKey(character))
			{
				consecutiveDodgeCounts[character]++;
			}
			else
			{
				consecutiveDodgeCounts.Add(character, 1);
			}
			return;
		}
		if (consecutiveDodgeCounts.ContainsKey(character))
		{
			consecutiveDodgeCounts[character] = 0;
		}
		else
		{
			consecutiveDodgeCounts.Add(character, 0);
		}
		consecutiveDodgeMaxCounts[character] = Random.Range(1, 3);
	}

	private void HandleEnemyCharacterChanged()
	{
		enemyCharacter = enemyActionMeter.character;
	}
}
