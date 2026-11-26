using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ChallengeModeManager : MonoBehaviour
{
	public static bool useTestMode;

	public static ChallengeModeType testMode;

	public static ChallengeModeDefinition modeDefinition;

	public static ChallengeModeValues currentModeValues;

	public static bool isChallengeModeActive;

	public static bool isNonJLChallenge;

	public static bool challengeWon;

	public static bool challengeModeDirty;

	public static bool challengeModeHard;

	private ChallengeModeType modeType;

	private BattleController battleController;

	private ActionMeter playerActionMeter;

	private ActionMeter enemyActionMeter;

	private AbilityBlocker playerWaffle;

	private AbilityBlocker enemyWaffle;

	private StatusEffect playerDanceEffect;

	private StatusEffect enemyDanceEffect;

	private bool playerSwapDirty;

	private bool enemySwapDirty;

	private CharacterStatBuff keepItClassyBuff;

	private float reapplyDelay = 1f;

	private float keepItClassyBuffTimer;

	private float keepItClassySwitchTimer;

	private bool updateSwitchTimerWhenWaiting;

	private GameObject hotPotato;

	private float blinkTimer;

	private float blinkSpeed;

	private float startSpeed = 1f;

	private float totalSpeedIncrease = 3.5f;

	private float timeElapsed;

	private float maxTimeElapsed = 20f;

	private float currentPotatoTimer = 20f;

	private bool potatoMoving;

	private static ChallengeModeManager _instance;

	public float skillChillCooldownTime
	{
		get
		{
			if (currentModeValues == null)
			{
				return 3f;
			}
			return currentModeValues.floatValue1;
		}
	}

	public static ChallengeModeManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("ChallengeModeManager").AddComponent<ChallengeModeManager>();
			}
			return _instance;
		}
	}

	public void Init()
	{
		battleController = BattleController.instance;
		playerActionMeter = battleController.playerActionMeter;
		enemyActionMeter = battleController.enemyActionMeter;
		if (modeDefinition != null)
		{
			modeType = modeDefinition.challengeModeType;
			if (modeType == ChallengeModeType.ChatteringTeeth)
			{
				StartCoroutine("ChatteringTeethRoutine");
			}
			if (modeType == ChallengeModeType.CrazyTofu)
			{
				StartCoroutine("CrazyTofuRoutine");
			}
			if (modeType == ChallengeModeType.Waffles)
			{
				StartWaffles();
			}
			if (modeType == ChallengeModeType.DanceParty)
			{
				StartDanceParty();
			}
			if (modeType == ChallengeModeType.SkillChill)
			{
				StartSkillChill();
			}
			if (modeType == ChallengeModeType.FigFlipper)
			{
				StartFigFlipper();
			}
			if (modeType == ChallengeModeType.KeepItClassy)
			{
				StartKeepItClassy();
			}
			if (modeType == ChallengeModeType.BossFight)
			{
				StartBossFight();
			}
			if (modeType == ChallengeModeType.HotPotato)
			{
				StartHotPotato();
			}
		}
	}

	private IEnumerator ChatteringTeethRoutine()
	{
		yield return null;
		while (currentModeValues != null)
		{
			if (BattleController.instance != null && BattleController.instance.isBattleRunning)
			{
				BattleCharacter playerCharacter = BattleController.instance.playerCharacter;
				BattleCharacter enemyCharacter = BattleController.instance.enemyCharacter;
				ActionMeter actionMeter = BattleController.instance.playerActionMeter;
				ActionMeter actionMeter2 = BattleController.instance.enemyActionMeter;
				if (playerCharacter != null && enemyCharacter != null && currentModeValues != null)
				{
					Drone objectFromResource = ObjectPool.GetObjectFromResource<Drone>("Prefabs/FX/JokerTeeth", playerCharacter.model.position);
					if (objectFromResource != null)
					{
						objectFromResource.transform.localScale = playerCharacter.transform.localScale;
					}
					StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter2, new AttackDrone(objectFromResource, actionMeter2, null, currentModeValues.floatValue1, currentModeValues.intValue1, currentModeValues.floatValue2));
					Drone objectFromResource2 = ObjectPool.GetObjectFromResource<Drone>("Prefabs/FX/JokerTeeth", enemyCharacter.model.position);
					if (objectFromResource2 != null)
					{
						objectFromResource2.transform.localScale = enemyCharacter.transform.localScale;
					}
					StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, new AttackDrone(objectFromResource2, actionMeter, null, currentModeValues.floatValue1, currentModeValues.intValue1, currentModeValues.floatValue2));
				}
			}
			yield return new WaitForSeconds(currentModeValues.timer);
		}
	}

	private IEnumerator CrazyTofuRoutine()
	{
		yield return null;
		while (currentModeValues != null)
		{
			if (BattleController.instance != null && BattleController.instance.isBattleRunning && !BattleController.instance.waiting)
			{
				BattleCharacter playerCharacter = BattleController.instance.playerCharacter;
				BattleCharacter enemyCharacter = BattleController.instance.enemyCharacter;
				BattleObjectManager.instance.SpawnTofuAt(GetCenterPosition(playerCharacter, BattleController.instance.playerBattleCamera), isPlayerTofu: true, 1);
				BattleObjectManager.instance.SpawnTofuAt(GetCenterPosition(enemyCharacter, BattleController.instance.enemyBattleCamera), isPlayerTofu: false, -1);
			}
			yield return new WaitForSeconds(currentModeValues.timer);
		}
	}

	private void StartWaffles()
	{
		playerActionMeter = BattleController.instance.playerActionMeter;
		enemyActionMeter = BattleController.instance.enemyActionMeter;
		enemyActionMeter.AbilityUsed += HandleEnemyAbilityUsed;
		playerActionMeter.AbilityUsed += HandlePlayerAbilityUsed;
		StartCoroutine("PlayerWaffles");
		StartCoroutine("EnemyWaffles");
	}

	private IEnumerator PlayerWaffles()
	{
		yield return null;
		while (currentModeValues != null)
		{
			if (BattleController.instance != null && BattleController.instance.isBattleRunning)
			{
				ActionMeter actionMeter = BattleController.instance.playerActionMeter;
				if (playerWaffle == null)
				{
					bool flag = true;
					if (actionMeter != null)
					{
						for (int i = 0; i < actionMeter.statusEffects.Count; i++)
						{
							StatusEffect statusEffect = actionMeter.statusEffects[i];
							if (statusEffect is Immunity)
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						playerWaffle = new AbilityBlocker(actionMeter, -1f);
						StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, playerWaffle);
					}
				}
			}
			yield return new WaitForSeconds(1f);
		}
	}

	private IEnumerator EnemyWaffles()
	{
		yield return null;
		while (currentModeValues != null)
		{
			if (BattleController.instance != null && BattleController.instance.isBattleRunning)
			{
				ActionMeter actionMeter = BattleController.instance.enemyActionMeter;
				if (enemyWaffle == null)
				{
					bool flag = true;
					if (actionMeter != null)
					{
						for (int i = 0; i < actionMeter.statusEffects.Count; i++)
						{
							StatusEffect statusEffect = actionMeter.statusEffects[i];
							if (statusEffect is Immunity)
							{
								flag = false;
							}
						}
					}
					if (flag)
					{
						enemyWaffle = new AbilityBlocker(actionMeter, -1f);
						StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, enemyWaffle);
					}
				}
			}
			yield return new WaitForSeconds(1f);
		}
	}

	public Vector3 GetCenterPosition(BattleCharacter battleCharacter, BattleCamera casterCamera)
	{
		Camera guiCamera = BattleController.instance.guiCamera;
		Vector3 position = casterCamera.camera.WorldToScreenPoint(battleCharacter.center.position);
		return guiCamera.ScreenToWorldPoint(position);
	}

	private void StartDanceParty()
	{
		StartCoroutine("DanceParty");
	}

	private IEnumerator DanceParty()
	{
		yield return new WaitForEndOfFrame();
		if (currentModeValues == null)
		{
			yield break;
		}
		playerActionMeter.StatusEffectAdded += HandlePlayerStatusEffectAdded_DanceParty;
		enemyActionMeter.StatusEffectAdded += HandleEnemyStatusEffectAdded_DanceParty;
		while (true)
		{
			if (battleController.isBattleRunning && !battleController.waiting)
			{
				if (playerDanceEffect == null)
				{
					bool flag = true;
					for (int i = 0; i < playerActionMeter.statusEffects.Count; i++)
					{
						StatusEffect statusEffect = playerActionMeter.statusEffects[i];
						if (statusEffect is ActionMeterSpeedModifier && statusEffect.type == StatusEffectType.Debuff)
						{
							flag = false;
							break;
						}
						if (statusEffect is HealBuffBlocker)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						if (enemyActionMeter != null)
						{
							for (int j = 0; j < enemyActionMeter.statusEffects.Count; j++)
							{
								StatusEffect statusEffect = enemyActionMeter.statusEffects[j];
								if (statusEffect is Immunity)
								{
									flag = false;
								}
							}
						}
						if (flag)
						{
							playerDanceEffect = new ActionMeterSpeedModifier(playerActionMeter, currentModeValues.intValue1, -1f);
							playerDanceEffect.Removed += HandlePlayerDanceEffectRemoved;
							StatusEffectManager.instance.AddActionMeterStatusEffect(playerActionMeter, playerDanceEffect);
						}
					}
				}
				if (enemyDanceEffect == null)
				{
					bool flag2 = true;
					for (int k = 0; k < enemyActionMeter.statusEffects.Count; k++)
					{
						StatusEffect statusEffect2 = enemyActionMeter.statusEffects[k];
						if (statusEffect2 is ActionMeterSpeedModifier && statusEffect2.type == StatusEffectType.Debuff)
						{
							flag2 = false;
							break;
						}
						if (statusEffect2 is HealBuffBlocker)
						{
							flag2 = false;
							break;
						}
					}
					if (flag2)
					{
						if (playerActionMeter != null)
						{
							for (int l = 0; l < playerActionMeter.statusEffects.Count; l++)
							{
								StatusEffect statusEffect2 = playerActionMeter.statusEffects[l];
								if (statusEffect2 is Immunity)
								{
									flag2 = false;
								}
							}
						}
						if (flag2)
						{
							enemyDanceEffect = new ActionMeterSpeedModifier(enemyActionMeter, currentModeValues.intValue1, -1f);
							enemyDanceEffect.Removed += HandleEnemyDanceEffectRemoved;
							StatusEffectManager.instance.AddActionMeterStatusEffect(enemyActionMeter, enemyDanceEffect);
						}
					}
				}
			}
			yield return null;
		}
	}

	private void HandlePlayerStatusEffectAdded_DanceParty(StatusEffect effect)
	{
		if (effect != playerDanceEffect && effect is ActionMeterSpeedModifier && effect.type == StatusEffectType.Buff)
		{
			effect.Remove();
		}
	}

	private void HandleEnemyStatusEffectAdded_DanceParty(StatusEffect effect)
	{
		if (effect != enemyDanceEffect && effect is ActionMeterSpeedModifier && effect.type == StatusEffectType.Buff)
		{
			effect.Remove();
		}
	}

	private void HandlePlayerDanceEffectRemoved(StatusEffect effect)
	{
		effect.Removed -= HandlePlayerDanceEffectRemoved;
		playerDanceEffect = null;
	}

	private void HandleEnemyDanceEffectRemoved(StatusEffect effect)
	{
		effect.Removed -= HandleEnemyDanceEffectRemoved;
		enemyDanceEffect = null;
	}

	private void StartSkillChill()
	{
		playerActionMeter.AbilityUsed += HandleAbilityUsedSkillChill;
		enemyActionMeter.AbilityUsed += HandleAbilityUsedSkillChill;
	}

	private void HandleAbilityUsedSkillChill(ActionMeter meter, Character character, AbilityData ability)
	{
		bool flag = true;
		if (meter != null)
		{
			for (int i = 0; i < meter.statusEffects.Count; i++)
			{
				StatusEffect statusEffect = meter.statusEffects[i];
				if (statusEffect is Immunity)
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			StatusEffectManager.instance.AddCharacterStatusEffect(character, new CharacterAbilityBlocker(character, meter, ability, skillChillCooldownTime));
		}
	}

	private void StartFigFlipper()
	{
		playerActionMeter = BattleController.instance.playerActionMeter;
		enemyActionMeter = BattleController.instance.enemyActionMeter;
		enemyActionMeter.AbilityUsed += HandleEnemyAbilityUsed;
		playerActionMeter.AbilityUsed += HandlePlayerAbilityUsed;
		StartCoroutine("FigFlipper");
	}

	private IEnumerator FigFlipper()
	{
		yield return new WaitForEndOfFrame();
		while (currentModeValues != null)
		{
			if (BattleController.instance != null && BattleController.instance.isBattleRunning)
			{
				BattleController.instance.playerPortraits.isEnabled = false;
				BattleController.instance.enemyPortraits.isEnabled = false;
				if (playerSwapDirty && !BattleController.instance.waiting && !playerActionMeter.isCasting && !playerActionMeter.isChanneling)
				{
					playerSwapDirty = false;
					Character playerSwapCharacter = GetPlayerSwapCharacter();
					if (playerSwapCharacter != null)
					{
						BattleController.instance.playerPortraits.SelectCharacter(playerSwapCharacter);
					}
				}
				if (enemySwapDirty && !BattleController.instance.waiting && !enemyActionMeter.isCasting && !enemyActionMeter.isChanneling)
				{
					enemySwapDirty = false;
					Character enemySwapCharacter = GetEnemySwapCharacter();
					if (enemySwapCharacter != null)
					{
						BattleController.instance.enemyPortraits.SelectCharacter(enemySwapCharacter);
					}
				}
			}
			yield return null;
		}
	}

	protected Character GetPlayerSwapCharacter()
	{
		List<Character> playerParty = BattleController.instance.playerParty;
		if (playerParty.Count > 1)
		{
			List<Character> list = new List<Character>();
			int count = playerParty.Count;
			for (int i = 0; i < count; i++)
			{
				if (playerParty[i] != BattleController.instance.playerCharacter.character && !playerParty[i].isDead && !playerParty[i].isKnockedOut)
				{
					list.Add(playerParty[i]);
				}
			}
			if (list.Count > 0)
			{
				return list[Random.Range(0, list.Count)];
			}
		}
		return null;
	}

	protected Character GetEnemySwapCharacter()
	{
		List<Character> enemyParty = BattleController.instance.enemyParty;
		if (enemyParty.Count > 1)
		{
			List<Character> list = new List<Character>();
			int count = enemyParty.Count;
			for (int i = 0; i < count; i++)
			{
				if (enemyParty[i] != BattleController.instance.enemyCharacter.character && !enemyParty[i].isDead && !enemyParty[i].isKnockedOut)
				{
					list.Add(enemyParty[i]);
				}
			}
			if (list.Count > 0)
			{
				return list[Random.Range(0, list.Count)];
			}
		}
		return null;
	}

	private void StartKeepItClassy()
	{
		BattleController.instance.BattleStarted += HandleBattleStarted;
	}

	private void HandleBattleStarted()
	{
		BattleController.instance.BattleStarted -= HandleBattleStarted;
		playerActionMeter.CharacterChanged += HandleCharacterChanged_KeepItClassy;
		enemyActionMeter.CharacterChanged += HandleCharacterChanged_KeepItClassy;
		HandleCharacterChanged_KeepItClassy();
		BattleController.instance.BattleEnded += HandleBattleEnded;
		StartCoroutine("UpdateKeepItClassy");
	}

	private void HandleBattleEnded()
	{
		BattleController.instance.BattleEnded -= HandleBattleEnded;
		StopCoroutine("UpdateKeepItClassy");
		playerActionMeter.CharacterChanged -= HandleCharacterChanged_KeepItClassy;
		enemyActionMeter.CharacterChanged -= HandleCharacterChanged_KeepItClassy;
		BattleController.instance.playerPortraits.Enabled -= HandlePlayerPortraitsEnabled;
		BattleController.instance.enemyPortraits.Enabled -= HandleEnemyPortraitsEnabled;
		BattleController.instance.playerPortraitSwitcherTimer.gameObject.SetActive(value: false);
		BattleController.instance.enemyPortraitSwitcherTimer.gameObject.SetActive(value: false);
	}

	private IEnumerator UpdateKeepItClassy()
	{
		while (true)
		{
			if (!BattleController.instance.waiting && keepItClassyBuffTimer > 0f)
			{
				keepItClassyBuffTimer -= Time.deltaTime;
				if (keepItClassyBuffTimer <= 0f)
				{
					HandleCharacterChanged_KeepItClassy();
				}
			}
			if (keepItClassySwitchTimer > 0f && (updateSwitchTimerWhenWaiting || !BattleController.instance.waiting))
			{
				keepItClassySwitchTimer -= Time.deltaTime;
				if (keepItClassySwitchTimer <= 0f)
				{
					BattleController.instance.playerPortraitSwitcherTimer.gameObject.SetActive(value: false);
					BattleController.instance.enemyPortraitSwitcherTimer.gameObject.SetActive(value: false);
					BattleController.instance.playerPortraits.Enabled -= HandlePlayerPortraitsEnabled;
					BattleController.instance.enemyPortraits.Enabled -= HandleEnemyPortraitsEnabled;
					if (!BattleController.instance.waiting)
					{
						BattleController.instance.playerPortraits.isEnabled = true;
						BattleController.instance.enemyPortraits.isEnabled = true;
					}
				}
				else
				{
					string text = Mathf.CeilToInt(keepItClassySwitchTimer).ToString();
					BattleController.instance.playerPortraitSwitcherTimer.text = text;
					BattleController.instance.enemyPortraitSwitcherTimer.text = text;
				}
			}
			yield return null;
		}
	}

	private void HandleCharacterChanged_KeepItClassy()
	{
		if (keepItClassyBuff != null)
		{
			keepItClassyBuff.Removed -= HandleKeepItClassyBuffRemoved;
			keepItClassyBuff.Remove();
			keepItClassyBuff = null;
		}
		if (keepItClassySwitchTimer > 0f)
		{
			keepItClassySwitchTimer = 0f;
			BattleController.instance.playerPortraitSwitcherTimer.gameObject.SetActive(value: false);
			BattleController.instance.enemyPortraitSwitcherTimer.gameObject.SetActive(value: false);
			BattleController.instance.playerPortraits.Enabled -= HandlePlayerPortraitsEnabled;
			BattleController.instance.enemyPortraits.Enabled -= HandleEnemyPortraitsEnabled;
			if (!BattleController.instance.waiting)
			{
				BattleController.instance.playerPortraits.isEnabled = true;
				BattleController.instance.enemyPortraits.isEnabled = true;
			}
		}
		Character strongerCharacter = GetStrongerCharacter(playerActionMeter.character.character, enemyActionMeter.character.character);
		if (strongerCharacter != null)
		{
			keepItClassyBuff = new CharacterStatBuff(strongerCharacter, (strongerCharacter != playerActionMeter.character.character) ? enemyActionMeter : playerActionMeter, CharacterStatType.Attack, 0, currentModeValues.floatValue1);
			keepItClassyBuff.Removed += HandleKeepItClassyBuffRemoved;
			StatusEffectManager.instance.AddCharacterStatusEffect(strongerCharacter, keepItClassyBuff);
			keepItClassySwitchTimer = currentModeValues.floatValue2;
			BattleController.instance.playerPortraitSwitcherTimer.text = keepItClassySwitchTimer.ToString();
			BattleController.instance.enemyPortraitSwitcherTimer.text = keepItClassySwitchTimer.ToString();
			BattleController.instance.playerPortraitSwitcherTimer.gameObject.SetActive(value: true);
			BattleController.instance.enemyPortraitSwitcherTimer.gameObject.SetActive(value: true);
			BattleController.instance.playerPortraits.Enabled += HandlePlayerPortraitsEnabled;
			BattleController.instance.enemyPortraits.Enabled += HandleEnemyPortraitsEnabled;
			HandlePlayerPortraitsEnabled();
			HandleEnemyPortraitsEnabled();
		}
	}

	private void HandlePlayerPortraitsEnabled()
	{
		BattleController.instance.playerPortraits.isEnabled = false;
	}

	private void HandleEnemyPortraitsEnabled()
	{
		BattleController.instance.enemyPortraits.isEnabled = false;
	}

	private void HandleKeepItClassyBuffRemoved(StatusEffect obj)
	{
		obj.Removed -= HandleKeepItClassyBuffRemoved;
		keepItClassyBuffTimer = reapplyDelay;
	}

	private Character GetStrongerCharacter(Character character1, Character character2)
	{
		CharacterClass characterClass = AbilityScript.classStrengths[character1.characterClass];
		CharacterClass characterClass2 = AbilityScript.classStrengths[character2.characterClass];
		if (characterClass == character2.characterClass)
		{
			return character1;
		}
		if (characterClass2 == character1.characterClass)
		{
			return character2;
		}
		return null;
	}

	private void StartBossFight()
	{
		StartCoroutine("BossFightRoutine");
	}

	private IEnumerator BossFightRoutine()
	{
		yield return null;
		while (currentModeValues != null)
		{
			if (BattleController.instance != null && BattleController.instance.isBattleRunning)
			{
				BattleCharacter enemyCharacter = BattleController.instance.enemyCharacter;
				BattleObjectManager.instance.SpawnTofuAt(GetCenterPosition(enemyCharacter, BattleController.instance.enemyBattleCamera), isPlayerTofu: false);
			}
			yield return new WaitForSeconds(currentModeValues.floatValue1);
		}
	}

	private void StartHotPotato()
	{
		StartCoroutine("HotPotatoRoutine");
	}

	private IEnumerator HotPotatoRoutine()
	{
		yield return null;
		while (currentModeValues != null)
		{
			if (BattleController.instance != null && BattleController.instance.isBattleRunning && GUICamera.instance != null)
			{
				if (hotPotato != null)
				{
					blinkSpeed = startSpeed + totalSpeedIncrease * (timeElapsed / maxTimeElapsed);
					blinkTimer += Time.deltaTime * blinkSpeed;
					if (hotPotato.GetComponent<tk2dSprite>().color.Equals(Color.red) && blinkTimer > 0.16f)
					{
						hotPotato.GetComponent<tk2dSprite>().color = Color.white;
					}
					if (blinkTimer >= 1f)
					{
						blinkTimer = 0f;
						if (hotPotato.GetComponent<tk2dSprite>().color.Equals(Color.white))
						{
							hotPotato.GetComponent<tk2dSprite>().color = Color.red;
						}
					}
				}
				if (!BattleController.instance.waiting)
				{
					if (hotPotato == null)
					{
						hotPotato = Object.Instantiate(Resources.Load("Prefabs/GUI/HotPotato") as GameObject);
						hotPotato.transform.parent = GUICamera.instance.anchorMiddleCenter.transform;
						hotPotato.transform.localScale = Vector3.zero;
						hotPotato.transform.localRotation = Quaternion.identity;
						hotPotato.transform.localPosition = Vector3.zero;
						potatoMoving = true;
						hotPotato.transform.DOScale(new Vector3(2f, 2f, 2f), 0.15f).SetEase(Ease.InOutSine).OnComplete(() =>
						{
							hotPotato.transform.parent = ((!(Random.Range(0f, 1f) > 0.5f)) ? BattleController.instance.playerActionMeter.barEnd : BattleController.instance.enemyActionMeter.barEnd);
							hotPotato.transform.DOLocalMove(Vector3.zero, 0.66f).SetDelay(0.7f).SetEase(Ease.OutSine)
								.OnComplete(() =>
								{
									potatoMoving = false;
								});
							Vector3 endValue = ((!(hotPotato.transform.parent == BattleController.instance.enemyActionMeter.barEnd)) ? Vector3.one : new Vector3(-1.25f, 1.25f, 1.25f));
							hotPotato.transform.DOScale(endValue, 0.3f);
						});
					}
					if (!potatoMoving)
					{
						currentPotatoTimer -= Time.deltaTime;
						timeElapsed += Time.deltaTime;
						if (currentPotatoTimer <= 0f)
						{
							if (hotPotato.transform.parent == BattleController.instance.playerActionMeter.barEnd)
							{
								currentPotatoTimer = Random.Range(currentModeValues.floatValue1, currentModeValues.floatValue2);
								maxTimeElapsed = currentPotatoTimer;
								timeElapsed = 0f;
								Object.Destroy(hotPotato);
								GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/GUI/HotPotatoExplosion") as GameObject);
								gameObject.transform.parent = BattleController.instance.playerActionMeter.barEnd;
								gameObject.transform.localScale = new Vector3(50f, 50f, 50f);
								gameObject.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
								gameObject.transform.localPosition = Vector3.zero;
								GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_boom", GFAudioManager.SFX_GROUP, string.Empty);
								BattleController battleController = BattleController.instance;
								battleController.playerCharacter.character.DoDamage(new DamageResult(battleController.playerCharacter.character.hitPoints));
							}
							if (hotPotato.transform.parent == BattleController.instance.enemyActionMeter.barEnd)
							{
								currentPotatoTimer = Random.Range(currentModeValues.floatValue1, currentModeValues.floatValue2);
								maxTimeElapsed = currentPotatoTimer;
								timeElapsed = 0f;
								Object.Destroy(hotPotato);
								GameObject gameObject2 = Object.Instantiate(Resources.Load("Prefabs/GUI/HotPotatoExplosion") as GameObject);
								gameObject2.transform.parent = BattleController.instance.enemyActionMeter.barEnd;
								gameObject2.transform.localScale = new Vector3(50f, 50f, 50f);
								gameObject2.transform.localEulerAngles = new Vector3(-90f, 0f, 0f);
								gameObject2.transform.localPosition = Vector3.zero;
								GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_boom", GFAudioManager.SFX_GROUP, string.Empty);
								BattleController battleController2 = BattleController.instance;
								if (battleController2.enemyCharacter != null && battleController2.enemyCharacter.character != null)
								{
									battleController2.enemyCharacter.character.DoDamage(new DamageResult(battleController2.enemyCharacter.character.hitPoints));
								}
							}
						}
						if (BattleController.instance.playerActionMeter.progress >= 1f && hotPotato != null && hotPotato.transform.parent == BattleController.instance.playerActionMeter.barEnd)
						{
							potatoMoving = true;
							hotPotato.GetComponent<tk2dSprite>().color = Color.white;
							hotPotato.transform.parent = BattleController.instance.enemyActionMeter.barEnd;
							hotPotato.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
							hotPotato.transform.DOLocalMove(Vector3.zero, 0.4f).SetEase(Ease.OutSine).OnComplete(() =>
							{
								potatoMoving = false;
							});
						}
						if (BattleController.instance.enemyActionMeter.progress >= 1f && hotPotato != null && hotPotato.transform.parent == BattleController.instance.enemyActionMeter.barEnd)
						{
							potatoMoving = true;
							hotPotato.GetComponent<tk2dSprite>().color = Color.white;
							hotPotato.transform.parent = BattleController.instance.playerActionMeter.barEnd;
							hotPotato.transform.localScale = Vector3.one;
							hotPotato.transform.DOLocalMove(Vector3.zero, 0.4f).SetEase(Ease.OutSine).OnComplete(() =>
							{
								potatoMoving = false;
							});
						}
					}
				}
			}
			yield return null;
		}
	}

	private void HandlePlayerAbilityUsed(ActionMeter actionMeter, Character arg2, AbilityData arg3)
	{
		if (modeType == ChallengeModeType.Waffles)
		{
			if (playerWaffle != null)
			{
				playerWaffle.Remove();
			}
			if (BattleController.instance != null && !BattleController.instance.isBattleRunning)
			{
				return;
			}
			bool flag = true;
			if (playerActionMeter != null)
			{
				for (int i = 0; i < playerActionMeter.statusEffects.Count; i++)
				{
					StatusEffect statusEffect = playerActionMeter.statusEffects[i];
					if (statusEffect is Immunity)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				playerWaffle = new AbilityBlocker(actionMeter, -1f);
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, playerWaffle);
			}
		}
		if (modeType == ChallengeModeType.FigFlipper)
		{
			playerSwapDirty = true;
		}
	}

	private void HandleEnemyAbilityUsed(ActionMeter actionMeter, Character arg2, AbilityData arg3)
	{
		if (modeType == ChallengeModeType.Waffles)
		{
			if (enemyWaffle != null)
			{
				enemyWaffle.Remove();
			}
			if (BattleController.instance != null && !BattleController.instance.isBattleRunning)
			{
				return;
			}
			bool flag = true;
			if (enemyActionMeter != null)
			{
				for (int i = 0; i < enemyActionMeter.statusEffects.Count; i++)
				{
					StatusEffect statusEffect = enemyActionMeter.statusEffects[i];
					if (statusEffect is Immunity)
					{
						flag = false;
					}
				}
			}
			if (flag)
			{
				enemyWaffle = new AbilityBlocker(actionMeter, -1f);
				StatusEffectManager.instance.AddActionMeterStatusEffect(actionMeter, enemyWaffle);
			}
		}
		if (modeType == ChallengeModeType.FigFlipper)
		{
			enemySwapDirty = true;
		}
	}

	private void CleanUpModes()
	{
		switch (modeType)
		{
		case ChallengeModeType.Waffles:
		case ChallengeModeType.FigFlipper:
			if (enemyActionMeter != null)
			{
				enemyActionMeter.AbilityUsed -= HandleEnemyAbilityUsed;
			}
			if (playerActionMeter != null)
			{
				playerActionMeter.AbilityUsed -= HandlePlayerAbilityUsed;
			}
			break;
		case ChallengeModeType.SkillChill:
			if (playerActionMeter != null)
			{
				playerActionMeter.AbilityUsed -= HandleAbilityUsedSkillChill;
			}
			if (enemyActionMeter != null)
			{
				enemyActionMeter.AbilityUsed -= HandleAbilityUsedSkillChill;
			}
			break;
		case ChallengeModeType.DanceParty:
			if (playerActionMeter != null)
			{
				playerActionMeter.StatusEffectAdded -= HandlePlayerStatusEffectAdded_DanceParty;
			}
			if (enemyActionMeter != null)
			{
				enemyActionMeter.StatusEffectAdded -= HandleEnemyStatusEffectAdded_DanceParty;
			}
			if (playerDanceEffect != null)
			{
				playerDanceEffect.Removed -= HandlePlayerDanceEffectRemoved;
			}
			if (enemyDanceEffect != null)
			{
				enemyDanceEffect.Removed -= HandleEnemyDanceEffectRemoved;
			}
			break;
		case ChallengeModeType.HotPotato:
			break;
		}
	}

	private void OnDestroy()
	{
		CleanUpModes();
		_instance = null;
	}
}
