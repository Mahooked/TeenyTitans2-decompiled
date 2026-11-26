using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AccessoryBattleButton : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action LongPressed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action AccessoryDestroyed__BackingField;

	public Transform animGrp;

	public Transform batteryTargetPos;

	public tk2dUIItem uiItem;

	public tk2dClippedSprite portraitDarkSprite;

	public tk2dClippedSprite portraitFillSprite;

	public tk2dClippedSprite backingFillSprite;

	public TextMeshPro chargeHealthText;

	public Animator animator;

	public BoxCollider2D boxCollider2D;

	public Vector3 buttonPositionPartyCount1;

	public Vector3 buttonPositionPartyCount2;

	public Vector3 buttonPositionPartyCount3;

	[Header("Effects")]
	public tk2dSpriteAnimator electricityEffectAnimator;

	public Color readyTextColor;

	public Color readyTextOutlineColor;

	private int uid = -1;

	private float lastShakeToUse;

	private bool isShowing;

	private float autoChargeTimer;

	private float autoDrainTimer;

	private float prevAutoDrainTimer;

	private AccessoryBattleItem accessoryBattleItem;

	private readonly Color offWhite = new Color(0.8f, 0.8f, 0.8f, 1f);

	private readonly Color white = Color.white;

	private readonly Color black = Color.black;

	private Rect tempRect = default(Rect);

	private const float activationMinFill = 1f;

	private float baseFillSpriteHeight;

	private IEnumerator waitForExpireConfirmation;

	private float holdTime;

	private bool isBtnDown;

	private bool isLongPress;

	private IEnumerator inputUpdateCoroutine;

	private bool _isEnabled = true;

	private BoxCollider2D _touchCollider;

	private AccessoryItemDefinition _itemDef;

	public bool isPlayer { get; private set; }

	public bool isActivated { get; private set; }

	public float currentChargeHealth { get; set; }

	public bool depleteWhileActive { get; set; }

	public bool autoChargeWhileActive { get; set; }

	public bool isInitialized { get; private set; }

	public Accessory accessory { get; private set; }

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			_isEnabled = value;
			UpdateAccessoryUI();
		}
	}

	public float currentFillAmount
	{
		get
		{
			return currentChargeHealth / (float)chargeMaxHealthLowest;
		}
	}

	private int chargeMaxHealthLowest
	{
		get
		{
			return (itemDef != null) ? itemDef.maxHealthLowest : 0;
		}
	}

	private int chargeMaxHealthHighest
	{
		get
		{
			return (itemDef != null) ? itemDef.maxHealthHighest : 0;
		}
	}

	public Vector3 accessoryCameraOffsetPosition
	{
		get
		{
			return (!(itemDef != null)) ? Vector3.zero : itemDef.cameraOffsetPositionZoomedOut;
		}
	}

	public Vector3 accessoryCameraOffsetPositionEnemy
	{
		get
		{
			return (!(itemDef != null)) ? Vector3.zero : itemDef.cameraOffsetPositionZoomedOutEnemy;
		}
	}

	public Character characterOwner
	{
		get
		{
			return (!isPlayer) ? BattleController.instance.enemyCharacter.character : BattleController.instance.playerCharacter.character;
		}
	}

	public BoxCollider2D touchCollider
	{
		get
		{
			return _touchCollider ?? (_touchCollider = GetComponent<BoxCollider2D>());
		}
	}

	public AccessoryItemDefinition itemDef
	{
		get
		{
			return _itemDef ?? (_itemDef = (AccessoryItemDefinition)GameManager.instance.itemDatabase.GetItem(uid));
		}
	}

	public int accessoryUid
	{
		get
		{
			return uid;
		}
	}

	public bool entered { get; private set; }

	private float DeltaTime
	{
		get
		{
			if (BattleController.battleDefinition != null && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				return TT2NetManager.Instance.DeltaTimeSeconds;
			}
			return Time.deltaTime;
		}
	}

	public event Action LongPressed
	{
		add
		{
			Action action = LongPressed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LongPressed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = LongPressed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LongPressed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action AccessoryDestroyed
	{
		add
		{
			Action action = AccessoryDestroyed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AccessoryDestroyed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = AccessoryDestroyed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AccessoryDestroyed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void Init(int newAccessoryUID, bool isPlayerOwned)
	{
		uid = newAccessoryUID;
		isInitialized = false;
		isPlayer = isPlayerOwned;
		if (uid >= 0)
		{
			baseFillSpriteHeight = portraitFillSprite.CurrentSprite.GetUntrimmedBounds().size.y;
			BattleCharacter battleCharacter = ((!isPlayer) ? BattleController.instance.enemyCharacter : BattleController.instance.playerCharacter);
			Vector3 vector = ((!isPlayer) ? itemDef.enemyOffsetPosition : itemDef.offsetPosition);
			accessoryBattleItem = ObjectPool.GetObjectFromResource<AccessoryBattleItem>(itemDef.prefabPath, battleCharacter.model.position + vector);
			accessoryBattleItem.transform.localScale = battleCharacter.transform.localScale;
			accessoryBattleItem.gameObject.SetActive(false);
			Projector componentInChildren = accessoryBattleItem.GetComponentInChildren<Projector>();
			if (componentInChildren != null && (accessoryBattleItem.transform.localScale.z < 0f || accessoryBattleItem.transform.localScale.y < 0f))
			{
				componentInChildren.transform.rotation = Quaternion.Euler(-componentInChildren.transform.eulerAngles);
			}
			ActionMeter meter = ((!isPlayer) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
			accessory = new Accessory(this, accessoryBattleItem, meter);
			portraitDarkSprite.SetSprite(itemDef.iconSprite);
			portraitFillSprite.SetSprite(itemDef.iconSprite);
			SetGUISortingOrder(1);
			UpdateAccessoryUI();
			BattleController.instance.BattleStarted += HandleBattleStarted;
			BattleController.instance.BattleEnded += HandleBattleEnded;
			BattleController.instance.WaitingStarted += HandleWaitingStarted;
			BattleController.instance.WaitingEnded += HandleWaitingEnded;
			accessory.Destroyed += HandleAccessoryDestroyed;
			if (isPlayer)
			{
				BattleController.instance.playerPortraits.SelectModeEntered += HandlePortraitsFocused;
				BattleController.instance.playerPortraits.SelectModeExitedLate += HandlePortraitsUnfocused;
				BattleController.instance.AbilityTooltipOpened += HandlePortraitsFocused;
				BattleController.instance.AbilityTooltipClosed += HandlePortraitsUnfocused;
			}
			uiItem.OnDown += HandleAccessoryBtnDown;
			uiItem.OnUp += HandleAccessoryBtnUp;
			depleteWhileActive = true;
			autoChargeWhileActive = true;
			isInitialized = true;
		}
		float x = ((!isPlayer) ? 350 : (-350));
		animGrp.transform.localPosition = new Vector3(x, animGrp.transform.localPosition.y, animGrp.transform.localPosition.z);
	}

	private void OnDestroy()
	{
		if (BattleController.instance != null)
		{
			BattleController.instance.BattleStarted -= HandleBattleStarted;
			BattleController.instance.BattleEnded -= HandleBattleEnded;
			BattleController.instance.WaitingStarted -= HandleWaitingStarted;
			BattleController.instance.WaitingEnded -= HandleWaitingEnded;
			if (isPlayer)
			{
				BattleController.instance.playerPortraits.SelectModeEntered -= HandlePortraitsFocused;
				BattleController.instance.playerPortraits.SelectModeExited -= HandlePortraitsUnfocused;
				BattleController.instance.AbilityTooltipOpened -= HandlePortraitsFocused;
				BattleController.instance.AbilityTooltipClosed -= HandlePortraitsUnfocused;
			}
		}
		if (accessory != null)
		{
			accessory.Destroyed += HandleAccessoryDestroyed;
		}
		uiItem.OnDown -= HandleAccessoryBtnDown;
		uiItem.OnUp -= HandleAccessoryBtnUp;
		LongPressed__BackingField = null;
		accessory = null;
	}

	private void Show()
	{
		if (!isInitialized || isShowing)
		{
			return;
		}
		isShowing = true;
		UpdateAccessoryUI();
		autoChargeTimer = itemDef.batteryChargeUpRate * characterOwner.accessoryChargeTimeModifier;
		int num = ((!isPlayer) ? BattleController.instance.enemyParty.Count : BattleController.instance.playerParty.Count);
		Transform obj = base.transform;
		Vector3 localPosition;
		switch (num)
		{
		case 1:
			localPosition = buttonPositionPartyCount1;
			break;
		case 2:
			localPosition = buttonPositionPartyCount2;
			break;
		default:
			localPosition = buttonPositionPartyCount3;
			break;
		}
		obj.localPosition = localPosition;
		if (isPlayer || GameManager.instance.gameSettings.showEnemyAccessoryUI)
		{
			entered = false;
			animGrp.DOKill();
			animGrp.DOLocalMoveX(0f, 0.33f).SetEase(Ease.OutBack).OnComplete(() =>
			{
				entered = true;
			});
		}
	}

	private void Hide()
	{
		if (isInitialized && isShowing)
		{
			isShowing = false;
			if (isPlayer || GameManager.instance.gameSettings.showEnemyAccessoryUI)
			{
				entered = false;
				float endValue = ((!isPlayer) ? 350 : (-350));
				animGrp.DOKill();
				animGrp.DOLocalMoveX(endValue, 0.33f).SetEase(Ease.InBack);
			}
		}
	}

	private void Update()
	{
		if (!isInitialized || !isShowing || BattleController.instance == null || !BattleController.instance.isBattleRunning || PauseManager.instance == null || PauseManager.instance.isPaused)
		{
			return;
		}
		if (!isActivated)
		{
			if (autoChargeWhileActive && !BattleController.instance.waiting)
			{
				autoChargeTimer -= DeltaTime;
				if (autoChargeTimer <= 0f)
				{
					autoChargeTimer += itemDef.batteryChargeUpRate * characterOwner.accessoryChargeTimeModifier;
					AddChargeHealthPercentage(itemDef.batteryChargeUpPercent);
				}
			}
		}
		else if (depleteWhileActive && !BattleController.instance.waiting && currentChargeHealth > 0f)
		{
			prevAutoDrainTimer = autoDrainTimer;
			autoDrainTimer -= DeltaTime;
			if (autoDrainTimer <= 0f)
			{
				autoDrainTimer += itemDef.batteryDrainRate;
				RemoveChargeHealthPercentage(itemDef.batteryDrainPercent);
			}
		}
	}

	public void AddChargeHealthPercentage(float maxHealthPercent, bool triggerElectricityFX = false, bool shake = false)
	{
		if (isInitialized)
		{
			UpdateChargeHealth(currentChargeHealth + (float)chargeMaxHealthLowest * maxHealthPercent / 100f);
			if (shake)
			{
				animator.Play("Shakey_Anim", 0, 0f);
			}
			if (triggerElectricityFX && currentChargeHealth < (float)chargeMaxHealthLowest)
			{
				electricityEffectAnimator.gameObject.SetActive(true);
				electricityEffectAnimator.PlayFromFrame("battery_electricity_stage01", 0);
			}
		}
	}

	public void RemoveChargeHealthPercentage(float maxHealthPercent)
	{
		if (isInitialized)
		{
			UpdateChargeHealth(currentChargeHealth - (float)chargeMaxHealthLowest * maxHealthPercent / 100f);
		}
	}

	public void RemoveChargeHealth(float healthLost)
	{
		if (isInitialized)
		{
			UpdateChargeHealth(currentChargeHealth - healthLost);
		}
	}

	public void UpdateChargeHealth(float health)
	{
		if (!isInitialized)
		{
			return;
		}
		float num = currentChargeHealth;
		currentChargeHealth = Mathf.Clamp(health, 0f, chargeMaxHealthHighest);
		UpdateAccessoryUI();
		if (isActivated)
		{
			if (!(currentChargeHealth <= 0f))
			{
				return;
			}
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				if (isPlayer && waitForExpireConfirmation == null)
				{
					waitForExpireConfirmation = WaitForExpireConfirmation(num, prevAutoDrainTimer);
					StartCoroutine(waitForExpireConfirmation);
				}
			}
			else
			{
				Expire();
			}
		}
		else if (num < (float)chargeMaxHealthLowest && currentChargeHealth >= (float)chargeMaxHealthLowest)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_battery_fullcharge", GFAudioManager.SFX_GROUP, string.Empty);
			electricityEffectAnimator.gameObject.SetActive(true);
			electricityEffectAnimator.Play("battery_electricity_stage02");
			animator.SetBool("isFull", true);
			animator.Play("Shakey_Anim", 0, 0f);
		}
		else if (currentChargeHealth < (float)chargeMaxHealthLowest)
		{
			animator.SetBool("isFull", false);
			electricityEffectAnimator.gameObject.SetActive(false);
		}
	}

	private IEnumerator WaitForExpireConfirmation(float prevChargeHealth, float prevDrainTimer)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendAccessoryExpire(Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		waitForExpireConfirmation = null;
		if (!playerAction.isConfirmed)
		{
			UpdateChargeHealth(prevChargeHealth);
			autoDrainTimer = prevDrainTimer;
		}
		else
		{
			Expire(playerAction.seed);
		}
	}

	public void Expire(int seed = -1)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (seed != -1)
		{
			UnityEngine.Random.InitState(seed);
		}
		ResetCharge();
		accessory.DestroyAccessory();
		if (seed != -1)
		{
			UnityEngine.Random.state = state;
		}
	}

	public void ForceActivateAccessory()
	{
		ActivateAccessory();
		isActivated = true;
		autoDrainTimer = itemDef.batteryDrainRate;
	}

	private void ActivateAccessory()
	{
		animator.SetBool("isFull", false);
		animator.Play("Shakey_Anim", 0, 0f);
		electricityEffectAnimator.gameObject.SetActive(false);
		accessoryBattleItem.gameObject.SetActive(true);
		accessoryBattleItem.Init((!isPlayer) ? BattleController.instance.enemyCharacter : BattleController.instance.playerCharacter);
		accessory.Activate();
	}

	private void UpdateAccessoryUI()
	{
		if ((currentFillAmount < 1f && !isActivated) || (BattleController.instance.waiting && isActivated) || !_isEnabled)
		{
			backingFillSprite.color = offWhite;
			portraitFillSprite.color = offWhite;
		}
		else
		{
			backingFillSprite.color = white;
			portraitFillSprite.color = white;
		}
		int num = Mathf.RoundToInt(currentFillAmount * 100f);
		chargeHealthText.text = string.Format("{0}%", num.ToString());
		if (currentFillAmount >= 1f)
		{
			chargeHealthText.color = readyTextColor;
			chargeHealthText.outlineColor = readyTextOutlineColor;
		}
		else
		{
			chargeHealthText.color = white;
			chargeHealthText.outlineColor = black;
		}
		float num2 = Mathf.Clamp01(currentFillAmount);
		float y = portraitFillSprite.CurrentSprite.GetUntrimmedBounds().size.y;
		float num3 = (y - baseFillSpriteHeight) / 2f / y;
		float height = Mathf.Lerp(num3, 1f - num3, num2);
		tempRect.Set(0f, 0f, 1f, num2);
		backingFillSprite.ClipRect = tempRect;
		tempRect.Set(0f, 0f, 1f, height);
		portraitFillSprite.ClipRect = tempRect;
	}

	private void ResetCharge(bool hideAccessoryGUI = false)
	{
		if (isInitialized)
		{
			isActivated = false;
			currentChargeHealth = 0f;
			UpdateAccessoryUI();
			if (hideAccessoryGUI)
			{
				Hide();
			}
		}
	}

	private void SetGUISortingOrder(int sortingOrder)
	{
		portraitDarkSprite.SortingOrder = sortingOrder;
		portraitFillSprite.SortingOrder = sortingOrder;
		backingFillSprite.SortingOrder = sortingOrder;
		chargeHealthText.sortingOrder = sortingOrder;
		electricityEffectAnimator.Sprite.SortingOrder = sortingOrder;
	}

	private void HandleBattleStarted()
	{
		Show();
	}

	private void HandleBattleEnded()
	{
		Hide();
	}

	private void HandleWaitingStarted()
	{
		UpdateAccessoryUI();
	}

	private void HandleWaitingEnded()
	{
		UpdateAccessoryUI();
	}

	private void HandleAccessoryBtnDown()
	{
		isBtnDown = true;
		isLongPress = false;
		holdTime = 0f;
		inputUpdateCoroutine = InputUpdate();
		StartCoroutine(inputUpdateCoroutine);
	}

	private void HandleAccessoryBtnUp()
	{
		isBtnDown = false;
		StopCoroutine(inputUpdateCoroutine);
		inputUpdateCoroutine = null;
		if (!isLongPress)
		{
			HandleAccessoryClicked();
		}
		isLongPress = false;
	}

	private IEnumerator InputUpdate()
	{
		while (true)
		{
			if (isBtnDown && !isLongPress)
			{
				holdTime += Time.deltaTime;
				if (holdTime >= GameManager.instance.gameSettings.buttonLongPressTime)
				{
					break;
				}
			}
			yield return null;
		}
		isLongPress = true;
		if (LongPressed__BackingField != null)
		{
			LongPressed__BackingField();
		}
	}

	private IEnumerator WaitForActivateConfirmation()
	{
		BattleController.instance.ForceStartWaiting();
		TT2NetPlayerAction playerAction = TT2NetManager.SendAccessoryUsed(currentChargeHealth, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		if (!playerAction.isConfirmed)
		{
			if (TT2NetManager.Instance.myNetworkPlayer == null || !TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
			{
				BattleController.instance.ForceEndWaiting();
			}
		}
		else
		{
			ForceActivateAccessory();
		}
	}

	public void ClickAccessory()
	{
		HandleAccessoryClicked();
	}

	private void HandleAccessoryClicked()
	{
		if (BattleController.instance.waiting || isActivated || (!isActivated && currentFillAmount < 1f) || !_isEnabled)
		{
			return;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (isPlayer)
			{
				StartCoroutine(WaitForActivateConfirmation());
			}
		}
		else
		{
			ForceActivateAccessory();
		}
	}

	private void HandlePortraitsFocused()
	{
		SetGUISortingOrder(0);
	}

	private void HandlePortraitsUnfocused()
	{
		SetGUISortingOrder(2);
	}

	private void HandleAccessoryDestroyed()
	{
		if (AccessoryDestroyed__BackingField != null)
		{
			AccessoryDestroyed__BackingField();
		}
	}
}
