using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using UnityEngine;

[SelectionBase]
public class AbilityButton : MonoBehaviour, IPoolable
{
	public struct ColorSet
	{
		public Color activated;

		public Color deactivated;

		public Color disabled;

		public Color buffFire;

		public ColorSet(Color activated, Color deactivated, Color disabled)
		{
			this.activated = activated;
			this.deactivated = deactivated;
			this.disabled = disabled;
			buffFire = Color.white;
		}

		public ColorSet(Color color)
		{
			activated = color;
			deactivated = color;
			disabled = color;
			buffFire = Color.white;
		}
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AbilityButton> Pressed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AbilityButton> LongPressed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AbilityData> AbilityChanged__BackingField;

	public ActionMeter actionMeter;

	public tk2dSprite tickSprite;

	public tk2dSprite tickLightSprite;

	public tk2dSprite backingSprite;

	public tk2dSprite iconSprite;

	public tk2dUIItem uiItem;

	public Transform buttonContainer;

	public Transform chargesContainer;

	public Transform buffFire;

	public tk2dSprite buffFireSprite;

	public Animator buffFireAnimator;

	public Transform buffBattery;

	public tk2dSpriteAnimator buffBatteryAnimator;

	public Transform debuffArrows;

	public GameObject diminishedEffect;

	public GameObject rootedEffectObj;

	public tk2dAnimationAdapter rootedSpriteAdapter;

	public AbilityButtonNumber abilityButtonNumber;

	public BoxCollider2D boxCollider2D;

	public Color activatedColor = Color.white;

	public Color deactivatedColor = new Color(131f / 255f, 131f / 255f, 131f / 255f, 1f);

	public Vector3 deactivatedScale = new Vector3(0.75f, 0.75f, 1f);

	public Vector3 activatedScale = new Vector3(1f, 1f, 1f);

	public bool playActivateSound = true;

	public tk2dUITweenItem tweenItem;

	public Color pulseColor = Color.red;

	public float pulseTime = 1f;

	private bool isDown;

	private bool isLongPress;

	private float downTimer;

	private Sequence activationSequence;

	private Sequence redFlashSequence;

	private List<tk2dSprite> chargeSprites = new List<tk2dSprite>();

	private Tweener pulseTween;

	private Coroutine pulseRoutine;

	private Coroutine swapAbilityCoroutine;

	private Sequence flipTween;

	private Sequence flipBackTween;

	private Character _character;

	private AbilityData _abilityData;

	private bool _canActivate = true;

	[SerializeField]
	[HideInInspector]
	private bool _showDamageNumber = true;

	[SerializeField]
	[HideInInspector]
	private bool _isFlipped;

	public Character character
	{
		get
		{
			return _character;
		}
	}

	public AbilityData abilityData
	{
		get
		{
			return _abilityData;
		}
		set
		{
			Init(_character, value);
		}
	}

	public bool isActivated { get; set; }

	public int position { get; set; }

	public ColorSet colorSet { get; set; }

	public bool isShinyAbility
	{
		get
		{
			if (_character != null && _abilityData != null)
			{
				return _character.IsAbilityShiny(_abilityData);
			}
			return false;
		}
	}

	public bool canActivate
	{
		get
		{
			return _canActivate;
		}
		set
		{
			if (value != _canActivate)
			{
				_canActivate = value;
				HandleActionMeterUpdated(actionMeter.progress);
			}
		}
	}

	public bool showDamageNumber
	{
		get
		{
			return _showDamageNumber;
		}
		set
		{
			if (value != _showDamageNumber)
			{
				_showDamageNumber = value;
				HandleCharacterUpdated();
			}
		}
	}

	public bool isFlipped
	{
		get
		{
			return _isFlipped;
		}
		set
		{
			if (value != _isFlipped)
			{
				_isFlipped = value;
				UpdateFlipped();
			}
		}
	}

	public bool ignorePress { get; set; }

	public event Action<AbilityButton> Pressed
	{
		add
		{
			Action<AbilityButton> action = Pressed__BackingField;
			Action<AbilityButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Pressed__BackingField, (Action<AbilityButton>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AbilityButton> action = Pressed__BackingField;
			Action<AbilityButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Pressed__BackingField, (Action<AbilityButton>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AbilityButton> LongPressed
	{
		add
		{
			Action<AbilityButton> action = LongPressed__BackingField;
			Action<AbilityButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LongPressed__BackingField, (Action<AbilityButton>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AbilityButton> action = LongPressed__BackingField;
			Action<AbilityButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref LongPressed__BackingField, (Action<AbilityButton>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AbilityData> AbilityChanged
	{
		add
		{
			Action<AbilityData> action = AbilityChanged__BackingField;
			Action<AbilityData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityChanged__BackingField, (Action<AbilityData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AbilityData> action = AbilityChanged__BackingField;
			Action<AbilityData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityChanged__BackingField, (Action<AbilityData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (actionMeter == null)
		{
			actionMeter = GetComponentInParent<ActionMeter>();
		}
		if (buffFireSprite == null)
		{
			buffFireSprite = buffFireAnimator.GetComponent<tk2dSprite>();
		}
		ResetColorSet();
		deactivatedScale.z = (activatedScale.z = 1f);
		buttonContainer.localScale = deactivatedScale;
		backingSprite.color = colorSet.deactivated;
		iconSprite.color = colorSet.deactivated;
		tickLightSprite.gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		uiItem.OnDown += HandleOnDown;
		uiItem.OnRelease += HandleOnRelease;
		actionMeter.Updated += HandleActionMeterUpdated;
		actionMeter.Resumed += HandleActionMeterResumed;
		actionMeter.AbilityChargeUpdated += HandleAbilityChargeUpdated;
		actionMeter.ChannelStarted += HandleActionMeterChannelStarted;
		actionMeter.ChannelFinished += HandleActionMeterChannelFinished;
		actionMeter.StatusEffectRemoved += HandleActionMeterStatusEffectRemoved;
		if (actionMeter.isPaused)
		{
			HandleActionMeterPaused();
		}
		else
		{
			HandleActionMeterResumed();
		}
	}

	private void OnDisable()
	{
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnRelease -= HandleOnRelease;
		actionMeter.Updated -= HandleActionMeterUpdated;
		actionMeter.Resumed -= HandleActionMeterResumed;
		actionMeter.AbilityChargeUpdated -= HandleAbilityChargeUpdated;
		actionMeter.ChannelStarted -= HandleActionMeterChannelStarted;
		actionMeter.ChannelFinished -= HandleActionMeterChannelFinished;
		actionMeter.StatusEffectRemoved -= HandleActionMeterStatusEffectRemoved;
	}

	private void Update()
	{
		if (!isLongPress && isDown)
		{
			downTimer += Time.unscaledDeltaTime;
			if (downTimer >= GameManager.instance.gameSettings.buttonLongPressTime)
			{
				isLongPress = true;
				OnLongPressed();
			}
		}
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
		}
		if (DiminishingReturns.instance != null)
		{
			DiminishingReturns instance = DiminishingReturns.instance;
			instance.DiminishedAbilityUpdated = (Action)Delegate.Remove(instance.DiminishedAbilityUpdated, new Action(HandleDiminishedAbilityUpdated));
		}
		if (actionMeter != null)
		{
			actionMeter.Updated -= HandleActionMeterUpdated;
			actionMeter.Resumed -= HandleActionMeterResumed;
			actionMeter.AbilityChargeUpdated -= HandleAbilityChargeUpdated;
			actionMeter.StatusEffectRemoved -= HandleActionMeterStatusEffectRemoved;
		}
	}

	public void SetStandardSprites()
	{
		if (!(abilityData == null))
		{
			if (abilityData.numChargesRequired > 0)
			{
				tickSprite.SetSprite("TTG_battlebar_bar_tic");
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple");
			}
			else
			{
				tickSprite.SetSprite("TTG_battlebar_bar_tic");
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing");
			}
		}
	}

	public void ResetSprites()
	{
		if (!(abilityData == null))
		{
			bool flag = isShinyAbility;
			if (actionMeter.HasStatusEffect<ActionMeterHack>())
			{
				tickSprite.SetSprite("TTG_battlebar_bar_tic_riddler");
				backingSprite.SetSprite((abilityData.numChargesRequired <= 0) ? "TTG_battlebar_abilityicon_backing_riddler" : "TTG_battlebar_ablilityicon_backing_trippleHACKED");
			}
			else if (abilityData.castTime > 0f)
			{
				tickSprite.SetSprite("TTG_battlebar_bar_tic_" + ((!flag) ? "cast" : "purplegold"));
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_" + ((!flag) ? "cast" : "goldcast"));
			}
			else if (abilityData.numChargesRequired > 0)
			{
				tickSprite.SetSprite("TTG_battlebar_bar_tic" + ((!flag) ? string.Empty : "_gold"));
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing_tripple" + ((!flag) ? string.Empty : "_gold"));
			}
			else if (abilityData.channelingData.speed > 0f)
			{
				tickSprite.SetSprite("TT2_battlebar_bar_tic_" + ((!flag) ? "channel" : "channelgold"));
				backingSprite.SetSprite("TT2_battlebar_abilityicon_backing_channel" + ((!flag) ? string.Empty : "_gold"));
			}
			else
			{
				tickSprite.SetSprite("TTG_battlebar_bar_tic" + ((!flag) ? string.Empty : "_gold"));
				backingSprite.SetSprite("TTG_battlebar_abilityicon_backing" + ((!flag) ? string.Empty : "_gold"));
			}
		}
	}

	public void Init(Character character, AbilityData abilityData)
	{
		if (character == _character && abilityData == _abilityData)
		{
			return;
		}
		for (int i = 0; i < chargeSprites.Count; i++)
		{
			chargeSprites[i].gameObject.SetLayerRecursively(LayerMask.NameToLayer("Default"));
			ObjectPool.PoolObject(chargeSprites[i].gameObject);
		}
		chargeSprites.Clear();
		if (_character != null)
		{
			_character.Updated -= HandleCharacterUpdated;
		}
		_character = character;
		_abilityData = abilityData;
		if (_character != null && _abilityData != null)
		{
			iconSprite.SetSprite(_abilityData.iconSprite);
			ResetSprites();
			if (abilityData.numChargesRequired > 0)
			{
				tk2dSprite tk2dSprite2 = null;
				Vector3 zero = Vector3.zero;
				float num = 31f;
				float num2 = 30f;
				float num3 = 0f;
				num3 = ((!_isFlipped) ? (90f + num2 * (float)(abilityData.numChargesRequired - 1) * 0.5f) : (-90f + num2 * (float)(abilityData.numChargesRequired - 1) * 0.5f));
				for (int j = 0; j < abilityData.numChargesRequired; j++)
				{
					tk2dSprite2 = ObjectPool.GetObjectFromResource<tk2dSprite>("Prefabs/Sprites/GUISprite");
					tk2dSprite2.SetSprite("TTG_battlebar_abilityicon_backing_tripple_light");
					tk2dSprite2.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
					tk2dSprite2.transform.parent = chargesContainer;
					tk2dSprite2.transform.localScale = new Vector3(1f, 1f, 1f);
					zero.x = Mathf.Cos(num3 * ((float)Math.PI / 180f)) * num;
					zero.y = Mathf.Sin(num3 * ((float)Math.PI / 180f)) * num;
					tk2dSprite2.transform.localPosition = zero;
					chargeSprites.Add(tk2dSprite2);
					num3 -= num2;
				}
			}
		}
		if (_character != null)
		{
			if (DiminishingReturns.instance != null)
			{
				DiminishingReturns instance = DiminishingReturns.instance;
				instance.DiminishedAbilityUpdated = (Action)Delegate.Combine(instance.DiminishedAbilityUpdated, new Action(HandleDiminishedAbilityUpdated));
			}
			_character.Updated += HandleCharacterUpdated;
			HandleCharacterUpdated();
		}
		OnAbilityChanged(_abilityData);
	}

	public void SwapAbility(AbilityData newAbilityData, Func<bool> validityCheck)
	{
		if (!(newAbilityData == _abilityData))
		{
			if (swapAbilityCoroutine != null)
			{
				StopCoroutine(swapAbilityCoroutine);
			}
			swapAbilityCoroutine = StartCoroutine(SwapAbilitySequence(newAbilityData, validityCheck));
		}
	}

	private IEnumerator SwapAbilitySequence(AbilityData newAbilityData, Func<bool> validityCheck)
	{
		Vector3 targetRotation = new Vector3(0f, -90f, 0f);
		float duration = 0.1f;
		if (flipTween != null)
		{
			flipTween.Kill();
		}
		if (flipBackTween != null)
		{
			flipBackTween.Kill();
		}
		flipTween = DOTween.Sequence();
		flipTween.Insert(0f, backingSprite.transform.DORotate(targetRotation, duration).SetEase(Ease.Linear));
		flipTween.Insert(0f, iconSprite.transform.DORotate(targetRotation, duration).SetEase(Ease.Linear));
		canActivate = false;
		AbilityData originalAbilityData = _abilityData;
		Init(_character, null);
		yield return flipTween.WaitForCompletion();
		Init(_character, (!validityCheck()) ? originalAbilityData : newAbilityData);
		flipBackTween = DOTween.Sequence();
		flipBackTween.Insert(0f, backingSprite.transform.DORotate(Vector3.zero, duration).SetEase(Ease.Linear));
		flipBackTween.Insert(0f, iconSprite.transform.DORotate(Vector3.zero, duration).SetEase(Ease.Linear));
		canActivate = !StatusEffectManager.instance.CharacterHasAbilityBlockerOnButton(actionMeter.character.character, this);
	}

	public void ResetColorSet()
	{
		colorSet = new ColorSet(activatedColor, deactivatedColor, actionMeter.disabledColor);
	}

	public void FlashRed()
	{
		if (redFlashSequence != null)
		{
			redFlashSequence.Kill(true);
		}
		redFlashSequence = DOTween.Sequence().OnComplete(() =>
		{
			redFlashSequence = null;
		});
		redFlashSequence.Insert(0f, DOTween.To(() => backingSprite.color, (Color x) =>
		{
			backingSprite.color = x;
		}, Color.red, 0.15f).SetEase(Ease.Linear).SetLoops(4, LoopType.Yoyo));
		redFlashSequence.Insert(0f, DOTween.To(() => iconSprite.color, (Color x) =>
		{
			iconSprite.color = x;
		}, Color.red, 0.15f).SetEase(Ease.Linear).SetLoops(4, LoopType.Yoyo));
		redFlashSequence.Play();
	}

	public void DisplayBuffBattery(bool active)
	{
		if (buffBattery != null)
		{
			buffBattery.gameObject.SetActive(active);
		}
	}

	private void HandleCharacterUpdated()
	{
		if (!base.gameObject.activeSelf || _character == null || _abilityData == null)
		{
			return;
		}
		if (_abilityData.type == AbilityType.Attack || _abilityData.type == AbilityType.Heal || (_abilityData.type == AbilityType.StatusEffect && (_abilityData.statusEffect == StatusEffectName.AttackDrone || _abilityData.statusEffect == StatusEffectName.Poison)))
		{
			abilityButtonNumber.Init(_abilityData, _character, BattleController.instance, position);
			abilityButtonNumber.gameObject.SetActive(_showDamageNumber && (_abilityData.type == AbilityType.Attack || _abilityData.type == AbilityType.Heal));
			if (abilityButtonNumber.isBonus)
			{
				if (buffFire.gameObject.activeSelf)
				{
					buffFireAnimator.Play("intro");
				}
				else
				{
					buffFire.gameObject.SetActive(true);
				}
				DisplayBuffBattery(false);
				debuffArrows.gameObject.SetActive(false);
			}
			else if (abilityButtonNumber.isReduced)
			{
				debuffArrows.gameObject.SetActive(true);
				buffFire.gameObject.SetActive(false);
				DisplayBuffBattery(false);
			}
			else
			{
				buffFire.gameObject.SetActive(false);
				DisplayBuffBattery(false);
				debuffArrows.gameObject.SetActive(false);
			}
		}
		else
		{
			abilityButtonNumber.gameObject.SetActive(false);
			buffFire.gameObject.SetActive(false);
			DisplayBuffBattery(false);
			debuffArrows.gameObject.SetActive(false);
		}
		if (DiminishingReturns.instance != null)
		{
			if (DiminishingReturns.instance.GetDiminishingReturnsFactor(_character, _abilityData) > 0f)
			{
				if (pulseTween == null)
				{
					pulseRoutine = StartCoroutine(DiminishingReturnsPulseSequence());
				}
			}
			else if (pulseTween != null && pulseRoutine != null)
			{
				StopCoroutine(pulseRoutine);
				pulseTween.Kill();
				pulseTween = null;
				tk2dSprite obj = backingSprite;
				Color color = (actionMeter.isPaused ? colorSet.disabled : ((!isActivated) ? colorSet.deactivated : colorSet.activated));
				iconSprite.color = color;
				obj.color = color;
			}
		}
		if (_abilityData.numChargesRequired > 0)
		{
			HandleAbilityChargeUpdated(_character, _abilityData, actionMeter.GetAbilityChargeCount(_character, _abilityData));
		}
	}

	private void HandleActionMeterStatusEffectRemoved(StatusEffect statusEffect)
	{
		if (statusEffect is ActionMeterHack)
		{
			ResetSprites();
		}
	}

	private void HandleDiminishedAbilityUpdated()
	{
		HandleCharacterUpdated();
	}

	public void Press()
	{
		OnPressed();
	}

	public void ForceUpdate()
	{
		HandleCharacterUpdated();
	}

	private void HandleActionMeterUpdated(float percent)
	{
		if (base.gameObject.activeSelf)
		{
			if (_canActivate && !isActivated && percent * 100f >= (float)position)
			{
				Activate();
			}
			else if (isActivated && (percent * 100f < (float)position || !_canActivate))
			{
				Deactivate();
			}
		}
	}

	public void HandleActionMeterPaused()
	{
		if (activationSequence != null)
		{
			activationSequence.Kill(true);
			activationSequence = null;
		}
		if (redFlashSequence != null)
		{
			redFlashSequence.Kill(true);
			redFlashSequence = null;
		}
		tickSprite.color = colorSet.disabled;
		tickLightSprite.color = colorSet.disabled;
		backingSprite.color = colorSet.disabled;
		iconSprite.color = colorSet.disabled;
	}

	public void HandleActionMeterResumed()
	{
		if (activationSequence != null)
		{
			activationSequence.Kill(true);
			activationSequence = null;
		}
		if (redFlashSequence != null)
		{
			redFlashSequence.Kill(true);
			redFlashSequence = null;
		}
		tickSprite.color = colorSet.activated;
		tickLightSprite.color = Color.white;
		backingSprite.color = ((!isActivated) ? colorSet.deactivated : colorSet.activated);
		iconSprite.color = ((!isActivated) ? colorSet.deactivated : colorSet.activated);
	}

	private void HandleAbilityChargeUpdated(Character character, AbilityData abilityData, int charges)
	{
		if (abilityData == _abilityData)
		{
			for (int i = 0; i < chargeSprites.Count; i++)
			{
				chargeSprites[i].gameObject.SetActive(i < charges);
			}
		}
	}

	private void HandleActionMeterChannelStarted(AbilityData abilityData, Character channelerCharacter)
	{
		HandleActionMeterPaused();
	}

	private void HandleActionMeterChannelFinished(AbilityData abilityData, Character channelerCharacter)
	{
		HandleActionMeterResumed();
	}

	private void Activate()
	{
		if (!isActivated)
		{
			if (redFlashSequence != null)
			{
				redFlashSequence.Kill(true);
				redFlashSequence = null;
			}
			if (activationSequence != null)
			{
				activationSequence.Kill(true);
			}
			activationSequence = DOTween.Sequence().OnComplete(() =>
			{
				activationSequence = null;
			});
			activationSequence.Insert(0f, buttonContainer.DOScale(activatedScale, 0.25f).SetEase(Ease.OutBack));
			activationSequence.Insert(0f, DOTween.To(() => backingSprite.color, (Color x) =>
			{
				backingSprite.color = x;
			}, (!actionMeter.isPaused) ? colorSet.activated : colorSet.disabled, 0.25f).SetEase(Ease.OutBack));
			activationSequence.Insert(0f, DOTween.To(() => iconSprite.color, (Color x) =>
			{
				iconSprite.color = x;
			}, (!actionMeter.isPaused) ? colorSet.activated : colorSet.disabled, 0.25f).SetEase(Ease.OutBack));
			activationSequence.Insert(0f, DOTween.To(() => tickSprite.color, (Color x) =>
			{
				tickSprite.color = x;
			}, (!actionMeter.isPaused) ? colorSet.activated : colorSet.disabled, 0.25f).SetEase(Ease.OutBack));
			activationSequence.Play();
			tickLightSprite.gameObject.SetActive(true);
			if (playActivateSound)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_batte_abilityavailable", GFAudioManager.SFX_GROUP, string.Empty);
			}
			isActivated = true;
		}
	}

	private void Deactivate()
	{
		if (isActivated)
		{
			if (redFlashSequence != null)
			{
				redFlashSequence.Kill(true);
				redFlashSequence = null;
			}
			if (activationSequence != null)
			{
				activationSequence.Kill(true);
			}
			activationSequence = DOTween.Sequence().OnComplete(() =>
			{
				activationSequence = null;
			});
			activationSequence.Insert(0f, buttonContainer.DOScale(deactivatedScale, 0.25f).SetEase(Ease.OutBack));
			activationSequence.Insert(0f, DOTween.To(() => backingSprite.color, (Color x) =>
			{
				backingSprite.color = x;
			}, (!actionMeter.isPaused) ? colorSet.deactivated : colorSet.disabled, 0.25f));
			activationSequence.Insert(0f, DOTween.To(() => iconSprite.color, (Color x) =>
			{
				iconSprite.color = x;
			}, (!actionMeter.isPaused) ? colorSet.deactivated : colorSet.disabled, 0.25f));
			activationSequence.Insert(0f, DOTween.To(() => tickSprite.color, (Color x) =>
			{
				tickSprite.color = x;
			}, (!actionMeter.isPaused) ? colorSet.deactivated : colorSet.disabled, 0.25f));
			activationSequence.Play();
			tickLightSprite.gameObject.SetActive(false);
			isActivated = false;
		}
	}

	private void UpdateFlipped()
	{
		if (_isFlipped)
		{
			Vector3 localPosition = buttonContainer.localPosition;
			localPosition.y = -11f;
			buttonContainer.localPosition = localPosition;
			localPosition = uiItem.transform.localPosition;
			localPosition.y = -30f;
			uiItem.transform.localPosition = localPosition;
			iconSprite.FlipX = true;
			backingSprite.FlipY = true;
			chargesContainer.localPosition = new Vector3(0f, 0f, -1f);
		}
		else
		{
			Vector3 localPosition = buttonContainer.localPosition;
			localPosition.y = 11f;
			buttonContainer.localPosition = localPosition;
			localPosition = uiItem.transform.localPosition;
			localPosition.y = 30f;
			uiItem.transform.localPosition = localPosition;
			iconSprite.FlipX = false;
			backingSprite.FlipY = false;
			chargesContainer.localPosition = new Vector3(0f, 0f, -1f);
		}
	}

	private IEnumerator DiminishingReturnsPulseSequence()
	{
		float t = 0f;
		pulseTween = DOTween.To(() => t, (float x) =>
		{
			t = x;
		}, 1f, pulseTime).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
		while (true)
		{
			yield return null;
			if (redFlashSequence == null || !redFlashSequence.IsPlaying())
			{
				Color a = (actionMeter.isPaused ? colorSet.disabled : ((!isActivated) ? colorSet.deactivated : colorSet.activated));
				backingSprite.color = Color.Lerp(a, pulseColor, t);
				iconSprite.color = Color.Lerp(a, pulseColor, t);
			}
		}
	}

	private void HandleOnDown()
	{
		isDown = true;
		downTimer = 0f;
	}

	private void HandleOnRelease()
	{
		isDown = false;
		if (!isLongPress && !ignorePress)
		{
			Press();
		}
		isLongPress = false;
	}

	private void OnPressed()
	{
		if (Pressed__BackingField != null)
		{
			Pressed__BackingField(this);
		}
	}

	private void OnLongPressed()
	{
		if (LongPressed__BackingField != null)
		{
			LongPressed__BackingField(this);
		}
	}

	private void OnAbilityChanged(AbilityData ability)
	{
		if (AbilityChanged__BackingField != null)
		{
			AbilityChanged__BackingField(ability);
		}
	}

	public void ResetObject()
	{
	}
}
