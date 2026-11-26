using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using Photon.Pun;
using UnityEngine;

[SelectionBase]
public class ActionMeter : MonoBehaviour
{
	private static Dictionary<int, float> fillTimes = new Dictionary<int, float>
	{
		{ 1, 16f },
		{ 2, 14.76f },
		{ 3, 13.54f },
		{ 4, 12.32f },
		{ 5, 11.1f },
		{ 6, 9.88f },
		{ 7, 8.66f },
		{ 8, 7.44f },
		{ 9, 6.22f },
		{ 10, 5f },
		{ 11, 3.78f },
		{ 12, 2.56f },
		{ 13, 1.34f }
	};

	private static Dictionary<ActionMeterSize, float> meterSizes = new Dictionary<ActionMeterSize, float>
	{
		{
			ActionMeterSize.Small,
			226f
		},
		{
			ActionMeterSize.Medium,
			226f
		},
		{
			ActionMeterSize.Large,
			226f
		}
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<float> Updated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<ActionMeter> CastStarted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<ActionMeter, AbilityButton> CastStartedAbility__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<ActionMeter, Character, AbilityData> AbilityUsed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<ActionMeter, Character, AbilityData> AbilityFinished__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Character, AbilityData, int> AbilityChargeUpdated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AbilityData, Character> ChannelStarted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AbilityData, Character> ChannelFinished__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Paused__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Resumed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action CharacterChanged__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<StatusEffect> StatusEffectAdded__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<StatusEffect> StatusEffectRemoved__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Initialized__BackingField;

	public ActionMeterColor color;

	public Transform barContainer;

	public Transform buttonContainer;

	public Transform batteryTickContainer;

	public Transform barEnd;

	public tk2dSlicedSprite backSprite;

	public tk2dSlicedSpriteHelper fillSprite;

	public tk2dSlicedSpriteHelper fillCastSprite;

	public tk2dSlicedSpriteHelper fillChannelSprite;

	public SpriteFlickerTk2d spriteFlicker;

	public List<AbilityButton> abilityButtons;

	public SimpleFaderTk2d blackFade;

	private Color _disabledColor = new Color(140f / 255f, 140f / 255f, 140f / 255f);

	private InterruptResult interruptResult;

	private Dictionary<Character, Dictionary<AbilityData, int>> abilityChargeCounts;

	private AbilityData lastAbilityUsed;

	private Character initialTarget;

	private bool _isPlayerMeter;

	private bool _isSetup;

	private float prevFillCastSpriteWidth;

	private Sequence spendBarSequence;

	private AbilityScript_Channeling channelingScript;

	private int channelingStepIndex;

	private ChannelingData.ChannelingStep channelingStep;

	private float channelingTickTimer;

	private float channelingStepDurationTimer;

	private int tempMinAbilitiesUsedCount;

	[SerializeField]
	private BattleCharacter _character;

	private List<StatusEffect> _statusEffects = new List<StatusEffect>();

	private float _fillSpeedModifier = 1f;

	private int _fillSpeedBonus;

	private float _stunDurationFactor = 1f;

	private float _maxScaleFactor = 1f;

	public float abilityUsedBatteryChargeDivider = 10f;

	public float abilityUsedBatteryChargeMultiplier = 2f;

	private bool _isEnabled = true;

	public Color disabledColor
	{
		get
		{
			return _disabledColor;
		}
	}

	public float maxWidth { get; private set; }

	public bool isCasting { get; private set; }

	public AbilityData castingAbility { get; private set; }

	public bool isChanneling { get; private set; }

	public bool isPaused { get; private set; }

	public bool isNetworkCasting { get; private set; }

	public bool isNetworkCastSuccessful { get; private set; }

	public AbilityData channelingAbility { get; private set; }

	public BattleCharacter character
	{
		get
		{
			return _character;
		}
		set
		{
			if (_character != null)
			{
				_character.Transformed -= HandleTransformed;
			}
			_character = value;
			stunDurationFactor = 1f;
			if (_character != null)
			{
				_character.Transformed += HandleTransformed;
			}
			Init();
			OnCharacterChanged();
		}
	}

	public List<StatusEffect> statusEffects
	{
		get
		{
			return _statusEffects;
		}
	}

	public float fillSpeedModifier
	{
		get
		{
			return _fillSpeedModifier;
		}
		set
		{
			if (value != _fillSpeedModifier)
			{
				_fillSpeedModifier = Mathf.Clamp(value, 0.05f, 2f);
			}
		}
	}

	public int fillSpeedBonus
	{
		get
		{
			return _fillSpeedBonus;
		}
		set
		{
			if (value != _fillSpeedBonus)
			{
				_fillSpeedBonus = value;
			}
		}
	}

	public float stunDurationFactor
	{
		get
		{
			return _stunDurationFactor;
		}
		set
		{
			_stunDurationFactor = value;
		}
	}

	public float maxScaleFactor
	{
		get
		{
			return _maxScaleFactor;
		}
		set
		{
			if (value != _maxScaleFactor)
			{
				_maxScaleFactor = value;
				if (fillSprite.width > maxWidth * _maxScaleFactor)
				{
					fillSprite.width = maxWidth * _maxScaleFactor;
					OnUpdated(fillSprite.width / maxWidth);
				}
			}
		}
	}

	public float progress
	{
		get
		{
			return Mathf.Clamp01(fillSprite.width / maxWidth);
		}
		set
		{
			fillSprite.width = maxWidth * value;
			if (fillSprite.width >= maxWidth * _maxScaleFactor)
			{
				fillSprite.width = maxWidth * _maxScaleFactor;
			}
			OnUpdated(fillSprite.width / maxWidth);
		}
	}

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

	public event Action<float> Updated
	{
		add
		{
			Action<float> action = Updated__BackingField;
			Action<float> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Updated__BackingField, (Action<float>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<float> action = Updated__BackingField;
			Action<float> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Updated__BackingField, (Action<float>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<ActionMeter> CastStarted
	{
		add
		{
			Action<ActionMeter> action = CastStarted__BackingField;
			Action<ActionMeter> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CastStarted__BackingField, (Action<ActionMeter>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ActionMeter> action = CastStarted__BackingField;
			Action<ActionMeter> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CastStarted__BackingField, (Action<ActionMeter>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<ActionMeter, AbilityButton> CastStartedAbility
	{
		add
		{
			Action<ActionMeter, AbilityButton> action = CastStartedAbility__BackingField;
			Action<ActionMeter, AbilityButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CastStartedAbility__BackingField, (Action<ActionMeter, AbilityButton>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ActionMeter, AbilityButton> action = CastStartedAbility__BackingField;
			Action<ActionMeter, AbilityButton> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CastStartedAbility__BackingField, (Action<ActionMeter, AbilityButton>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<ActionMeter, Character, AbilityData> AbilityUsed
	{
		add
		{
			Action<ActionMeter, Character, AbilityData> action = AbilityUsed__BackingField;
			Action<ActionMeter, Character, AbilityData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityUsed__BackingField, (Action<ActionMeter, Character, AbilityData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ActionMeter, Character, AbilityData> action = AbilityUsed__BackingField;
			Action<ActionMeter, Character, AbilityData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityUsed__BackingField, (Action<ActionMeter, Character, AbilityData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<ActionMeter, Character, AbilityData> AbilityFinished
	{
		add
		{
			Action<ActionMeter, Character, AbilityData> action = AbilityFinished__BackingField;
			Action<ActionMeter, Character, AbilityData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityFinished__BackingField, (Action<ActionMeter, Character, AbilityData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<ActionMeter, Character, AbilityData> action = AbilityFinished__BackingField;
			Action<ActionMeter, Character, AbilityData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityFinished__BackingField, (Action<ActionMeter, Character, AbilityData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Character, AbilityData, int> AbilityChargeUpdated
	{
		add
		{
			Action<Character, AbilityData, int> action = AbilityChargeUpdated__BackingField;
			Action<Character, AbilityData, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityChargeUpdated__BackingField, (Action<Character, AbilityData, int>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Character, AbilityData, int> action = AbilityChargeUpdated__BackingField;
			Action<Character, AbilityData, int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityChargeUpdated__BackingField, (Action<Character, AbilityData, int>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AbilityData, Character> ChannelStarted
	{
		add
		{
			Action<AbilityData, Character> action = ChannelStarted__BackingField;
			Action<AbilityData, Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ChannelStarted__BackingField, (Action<AbilityData, Character>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AbilityData, Character> action = ChannelStarted__BackingField;
			Action<AbilityData, Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ChannelStarted__BackingField, (Action<AbilityData, Character>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AbilityData, Character> ChannelFinished
	{
		add
		{
			Action<AbilityData, Character> action = ChannelFinished__BackingField;
			Action<AbilityData, Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ChannelFinished__BackingField, (Action<AbilityData, Character>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AbilityData, Character> action = ChannelFinished__BackingField;
			Action<AbilityData, Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ChannelFinished__BackingField, (Action<AbilityData, Character>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action Paused
	{
		add
		{
			Action action = Paused__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Paused__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Paused__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Paused__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action Resumed
	{
		add
		{
			Action action = Resumed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Resumed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Resumed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Resumed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action CharacterChanged
	{
		add
		{
			Action action = CharacterChanged__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterChanged__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = CharacterChanged__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterChanged__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<StatusEffect> StatusEffectAdded
	{
		add
		{
			Action<StatusEffect> action = StatusEffectAdded__BackingField;
			Action<StatusEffect> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusEffectAdded__BackingField, (Action<StatusEffect>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<StatusEffect> action = StatusEffectAdded__BackingField;
			Action<StatusEffect> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusEffectAdded__BackingField, (Action<StatusEffect>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<StatusEffect> StatusEffectRemoved
	{
		add
		{
			Action<StatusEffect> action = StatusEffectRemoved__BackingField;
			Action<StatusEffect> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusEffectRemoved__BackingField, (Action<StatusEffect>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<StatusEffect> action = StatusEffectRemoved__BackingField;
			Action<StatusEffect> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StatusEffectRemoved__BackingField, (Action<StatusEffect>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action Initialized
	{
		add
		{
			Action action = Initialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Initialized__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Initialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Initialized__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Start()
	{
		if (!_isSetup)
		{
			Setup();
		}
	}

	private void OnEnable()
	{
		int count = abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			abilityButtons[i].Pressed += HandlePressed;
		}
	}

	private void OnDisable()
	{
		int count = abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			abilityButtons[i].Pressed -= HandlePressed;
		}
		if (initialTarget != null)
		{
			initialTarget.InterruptReceived -= HandleInterruptReceived;
			initialTarget = null;
		}
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Transformed -= HandleTransformed;
		}
	}

	private void Update()
	{
		if (isPaused || isCasting || isChanneling || _character == null || (spendBarSequence != null && spendBarSequence.IsPlaying()))
		{
			return;
		}
		if (fillSprite.width < maxWidth * _maxScaleFactor)
		{
			float num = ((!GameManager.instance.gameSettings.actionMeterSizeEnabled) ? (maxWidth / fillTimes[_character.character.speed + _character.character.speedBonus + _fillSpeedBonus]) : (204f / (10f - 1.22f * (float)_fillSpeedBonus)));
			num *= _fillSpeedModifier;
			fillSprite.width += num * DeltaTime;
			if (fillSprite.width >= maxWidth * _maxScaleFactor)
			{
				fillSprite.width = maxWidth * _maxScaleFactor;
			}
			OnUpdated(fillSprite.width / maxWidth);
		}
		if (batteryTickContainer != null && batteryTickContainer.gameObject.activeSelf && maxWidth * progress >= batteryTickContainer.transform.localPosition.x)
		{
			batteryTickContainer.gameObject.SetActive(false);
			Vector3 vector = ((!_isPlayerMeter) ? new Vector3(0f, -100f) : new Vector3(0f, 100f));
			Vector3 position = BattleController.instance.guiCamera.WorldToScreenPoint(batteryTickContainer.transform.position + vector);
			Vector3 position2 = BattleController.instance.guiCamera.ScreenToWorldPoint(position);
			position2.z = -800f;
			BattleObjectManager.instance.SpawnBatteryAt(position2, _isPlayerMeter);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_battery_placeonability", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public void Pause(bool useDisabledVisuals = true)
	{
		if (isPaused)
		{
			return;
		}
		if (useDisabledVisuals)
		{
			fillSprite.slicedSprite.color = disabledColor;
			backSprite.color = disabledColor;
			for (int i = 0; i < abilityButtons.Count; i++)
			{
				abilityButtons[i].HandleActionMeterPaused();
			}
		}
		spriteFlicker.enabled = false;
		isPaused = true;
		if (Paused__BackingField != null)
		{
			Paused__BackingField();
		}
	}

	public void Resume()
	{
		if (isPaused)
		{
			fillSprite.slicedSprite.color = Color.white;
			backSprite.color = Color.white;
			for (int i = 0; i < abilityButtons.Count; i++)
			{
				abilityButtons[i].HandleActionMeterResumed();
			}
			spriteFlicker.enabled = true;
			isPaused = false;
			if (Resumed__BackingField != null)
			{
				Resumed__BackingField();
			}
		}
	}

	public AbilityButton GetAbilityButton(AbilityData abilityData)
	{
		for (int i = 0; i < abilityButtons.Count; i++)
		{
			if (abilityButtons[i].abilityData == abilityData)
			{
				return abilityButtons[i];
			}
		}
		return null;
	}

	public AbilityButton GetAbilityButton(int abilityUid)
	{
		for (int i = 0; i < abilityButtons.Count; i++)
		{
			if (abilityButtons[i].abilityData != null && abilityButtons[i].abilityData.uid == abilityUid)
			{
				return abilityButtons[i];
			}
		}
		return null;
	}

	public List<AbilityButton> GetActivatedAbilityButtons()
	{
		return abilityButtons.FindAll((AbilityButton obj) => obj.abilityData != null && obj.isActivated);
	}

	public void ForceUpdateButtons()
	{
		int count = abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			abilityButtons[i].ForceUpdate();
		}
	}

	public void AddStatusEffect(StatusEffect effect)
	{
		effect.Removed += HandleStatusEffectRemoved;
		_statusEffects.Add(effect);
		OnStatusEffectAdded(effect);
	}

	public void RemoveStatusEffect(Type type)
	{
		StatusEffect statusEffect = _statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(type));
		if (statusEffect != null)
		{
			statusEffect.Remove();
			OnUpdated(fillSprite.width / maxWidth);
		}
	}

	public void RemoveStatusEffect<T>()
	{
		StatusEffect statusEffect = _statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(typeof(T)));
		if (statusEffect != null)
		{
			statusEffect.Remove();
			OnUpdated(fillSprite.width / maxWidth);
		}
	}

	public void RemoveAllStatusEffectsByType(StatusEffectType type, bool isCleanse)
	{
		int num = _statusEffects.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			StatusEffect statusEffect = _statusEffects[num2];
			if (statusEffect.type == type && (!isCleanse || statusEffect.isCleansable))
			{
				statusEffect.Remove();
			}
		}
		OnUpdated(fillSprite.width / maxWidth);
	}

	public void RemoveAllStatusEffects()
	{
		int num = _statusEffects.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			_statusEffects[num2].Remove();
		}
		OnUpdated(fillSprite.width / maxWidth);
	}

	public bool HasZoomedOutStatusEffect()
	{
		return _statusEffects.Find((StatusEffect obj) => obj.isZoomedOut) != null;
	}

	public StatusEffect GetStatusEffect(Type type)
	{
		return _statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(type));
	}

	public T GetStatusEffect<T>() where T : StatusEffect
	{
		return (T)_statusEffects.Find((StatusEffect obj) => obj.GetType().Equals(typeof(T)));
	}

	public List<T> GetStatusEffects<T>() where T : StatusEffect
	{
		return _statusEffects.FindAll((StatusEffect obj) => obj.GetType().Equals(typeof(T))).ConvertAll((StatusEffect input) => (T)input);
	}

	public bool HasStatusEffect(StatusEffectType type)
	{
		return _statusEffects.Find((StatusEffect obj) => obj.type == type) != null;
	}

	public bool HasStatusEffect<T>() where T : StatusEffect
	{
		return GetStatusEffect<T>() != null;
	}

	public void EnableAllButtons(bool enable = true)
	{
		int count = abilityButtons.Count;
		for (int i = 0; i < count; i++)
		{
			abilityButtons[i].boxCollider2D.enabled = enable;
		}
	}

	public void StartNetworkCasting()
	{
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			isNetworkCasting = true;
			isNetworkCastSuccessful = false;
		}
	}

	public void FinishNetworkCasting()
	{
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			isNetworkCasting = false;
			isNetworkCastSuccessful = true;
		}
	}

	private void HandleStatusEffectRemoved(StatusEffect effect)
	{
		effect.Removed -= HandleStatusEffectRemoved;
		_statusEffects.Remove(effect);
		OnStatusEffectRemoved(effect);
	}

	private void Setup()
	{
		if (!_isSetup)
		{
			abilityChargeCounts = new Dictionary<Character, Dictionary<AbilityData, int>>();
			maxWidth = fillSprite.width;
			fillSprite.width = 0f;
			string sprite = string.Empty;
			switch (color)
			{
			case ActionMeterColor.Green:
				sprite = "TTG_battlebar_fill_green";
				break;
			case ActionMeterColor.Red:
				sprite = "TTG_battlebar_fill_red";
				break;
			}
			fillSprite.slicedSprite.SetSprite(sprite);
			if (fillCastSprite.gameObject.activeSelf)
			{
				fillCastSprite.gameObject.SetActive(false);
			}
			if (fillChannelSprite.gameObject.activeSelf)
			{
				fillChannelSprite.gameObject.SetActive(false);
			}
			_isSetup = true;
			Init();
		}
	}

	private void Init()
	{
		if (!_isSetup)
		{
			Setup();
			return;
		}
		string sprite = string.Empty;
		switch (color)
		{
		case ActionMeterColor.Green:
			sprite = "TTG_battlebar_bar_tic_green";
			break;
		case ActionMeterColor.Red:
			sprite = "TTG_battlebar_bar_tic_red";
			break;
		}
		if (_character == null)
		{
			int count = abilityButtons.Count;
			for (int i = 0; i < count; i++)
			{
				abilityButtons[i].tickLightSprite.SetSprite(sprite);
				abilityButtons[i].gameObject.SetActive(false);
			}
			_isPlayerMeter = false;
			return;
		}
		if (GameManager.instance.gameSettings.actionMeterSizeEnabled)
		{
			float num = meterSizes[character.character.actionMeterSize];
			Vector2 dimensions = backSprite.dimensions;
			dimensions.x = num;
			backSprite.dimensions = dimensions;
			maxWidth = num - 22f;
			fillSprite.width = Mathf.Min(fillSprite.width, maxWidth);
		}
		int count2 = abilityButtons.Count;
		int num2 = 0;
		for (int j = 0; j < count2; j++)
		{
			AbilityButton abilityButton = abilityButtons[j];
			abilityButton.tickLightSprite.SetSprite(sprite);
			int uid = ((_character.character.activeTransformation == null) ? _character.characterDefinition.orderedSlots[j].abilityId : _character.character.activeTransformation.orderedSlots[j].abilityId);
			if (_character.character.abilities.Contains(GameManager.instance.abilityDatabase.GetAbility(uid)))
			{
				abilityButton.position = ((_character.character.activeTransformation == null) ? _character.characterDefinition.orderedSlots[j].position : _character.character.activeTransformation.orderedSlots[j].position);
				Vector3 localPosition = abilityButton.transform.localPosition;
				localPosition.x = maxWidth * ((float)abilityButton.position * 0.01f);
				localPosition.z = -10f * (float)j;
				abilityButton.transform.localPosition = localPosition;
				AbilityData abilityData = _character.character.abilities[num2];
				if (abilityData.type == AbilityType.StatusEffect && abilityData.statusEffect == StatusEffectName.RemoteBomb)
				{
					ActionMeter actionMeter = ((!(this == BattleController.instance.playerActionMeter)) ? BattleController.instance.playerActionMeter : BattleController.instance.enemyActionMeter);
					if (actionMeter.statusEffects.FindIndex((StatusEffect obj) => obj is RemoteBomb && ((RemoteBomb)obj).CasterCharacter == _character.character) > -1)
					{
						abilityData = GameManager.instance.abilityDatabase.GetAbility(abilityData.intValue1);
					}
				}
				abilityButton.gameObject.SetActive(true);
				abilityButton.Init(_character.character, abilityData);
				num2++;
			}
			else
			{
				abilityButton.gameObject.SetActive(false);
				abilityButton.Init(null, null);
			}
		}
		_isPlayerMeter = _character == BattleController.instance.playerCharacter;
		OnUpdated(fillSprite.width / maxWidth);
		OnInitialized();
	}

	private void HandlePressed(AbilityButton button)
	{
		UnityEngine.Debug.LogFormat("[ActionMeter::HandlePressed] - _isEnabled: {0}    isCasting: {1}    isChanneling: {2}    spendBarSequence: {3}", _isEnabled, isCasting, isChanneling, spendBarSequence);
		if (!_isPlayerMeter && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			StartCoroutine(PlayAbility(button));
		}
		else
		{
			if (!_isEnabled || isCasting || isChanneling || (spendBarSequence != null && spendBarSequence.IsPlaying()))
			{
				return;
			}
			if (BattleController.instance.batteryGUI != null && BattleController.instance.batteryGUI.isUsingBattery && character == BattleController.instance.playerCharacter)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
				ActionMeter playerActionMeter = BattleController.instance.playerActionMeter;
				BattleController.instance.batteryGUI.UseBatteryOnAbilityButton(button);
				BattleController.instance.batteryGUI.StopUsingBatteryMode();
				return;
			}
			if (isPaused)
			{
				if (HasStatusEffect<ActionMeterElectrify>())
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_abilitynotready", GFAudioManager.SFX_GROUP, string.Empty);
					button.FlashRed();
				}
				return;
			}
			if (!button.isActivated)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_abilitynotready", GFAudioManager.SFX_GROUP, string.Empty);
				button.FlashRed();
				return;
			}
			int num = Mathf.RoundToInt((float)button.abilityData.GetHealthCost(_character.character) * ((!HasStatusEffect<AmplifyDamage>()) ? 1f : GetStatusEffect<AmplifyDamage>().DamageMultiplier));
			if (num > 0 && character.character.hitPoints <= num)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_abilitynotready", GFAudioManager.SFX_GROUP, string.Empty);
				button.FlashRed();
				PortraitSwitcher portraitSwitcher = ((!(this == BattleController.instance.playerActionMeter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
				portraitSwitcher.selectedPortrait.healthText.transform.DOKill(true);
				portraitSwitcher.selectedPortrait.healthText.transform.DOScale(new Vector3(1.7f, 1.7f, 1f), 0.15f).SetEase(Ease.OutSine).SetLoops(4, LoopType.Yoyo);
				return;
			}
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network && button.abilityData.targetCount == TargetCount.SelfAndTarget)
			{
				int num2 = TTGAbilityCalculator.CalculateDamage(button.abilityData, _character.character, button.position, true, true, true, _character.character.IsAbilityShiny(button.abilityData));
				if (num2 > 0 && character.character.hitPoints <= num2)
				{
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_abilitynotready", GFAudioManager.SFX_GROUP, string.Empty);
					button.FlashRed();
					PortraitSwitcher portraitSwitcher2 = ((!(this == BattleController.instance.playerActionMeter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
					portraitSwitcher2.selectedPortrait.healthText.transform.DOKill(true);
					portraitSwitcher2.selectedPortrait.healthText.transform.DOScale(new Vector3(1.7f, 1.7f, 1f), 0.15f).SetEase(Ease.OutSine).SetLoops(4, LoopType.Yoyo);
					return;
				}
			}
			if (button.abilityData.numUses > -1 && BattleController.instance.GetAbilityUseCount(_character.character, button.abilityData) >= button.abilityData.numUses)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_abilitynotready", GFAudioManager.SFX_GROUP, string.Empty);
				button.FlashRed();
				return;
			}
			if (character == BattleController.instance.playerCharacter)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
			}
			StartCoroutine(PlayAbility(button));
		}
	}

	private IEnumerator PlayAbility(AbilityButton button)
	{
		AbilityData ability = button.abilityData;
		bool isShiny = _character.character.IsAbilityShiny(ability);
		float chargeModifier = GameManager.instance.gameSettings.accessoryChargeOnAbilityUsedModifier;
		AccessoryBattleButton accessoryBattleButton = ((!(character == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyAccessoryButton : BattleController.instance.playerAccessoryButton);
		if (accessoryBattleButton.isActivated)
		{
			chargeModifier = GameManager.instance.gameSettings.accessoryChargeOnAbilityUsedModifierActiveAccessory;
		}
		if (!accessoryBattleButton.isActivated)
		{
			float maxHealthPercent = Mathf.Max((float)button.position * chargeModifier, 1f);
			accessoryBattleButton.AddChargeHealthPercentage(maxHealthPercent, true, true);
		}
		TT2NetPlayerAction playerAction = null;
		float finalCastTime = ((!(_character.character.instantCastChance > 0f) || !(UnityEngine.Random.value <= _character.character.instantCastChance)) ? (ability.castTime * _character.character.castTimeModifier) : 0f);
		if (!_isPlayerMeter && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			finalCastTime = ((!isNetworkCasting) ? 0f : (ability.castTime * _character.character.castTimeModifier));
		}
		if (finalCastTime > 0f && (!isShiny || ability.shinyEffect.effectType != AbilityShinyEffectType.InstantCast || UnityEngine.Random.value > ability.shinyEffect.probability))
		{
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network && _isPlayerMeter)
			{
				BattleController.instance.ForceStartWaiting(false);
				playerAction = TT2NetManager.SendBattleDecision(button.abilityData.uid, character.characterDefinition.uid, true, progress, character.character.finalDamageMultiplier, Environment.TickCount);
				yield return new WaitWhile(() => playerAction.isWaiting);
				if (TT2NetManager.Instance.myNetworkPlayer == null || !TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
				{
					BattleController.instance.ForceEndWaiting();
				}
				if (!playerAction.isConfirmed)
				{
					yield break;
				}
			}
			isCasting = true;
			castingAbility = ability;
			interruptResult = null;
			if (this == BattleController.instance.playerActionMeter)
			{
				Player.instance.SetTrigger("AbilityCast");
			}
			OnCastStarted(button);
			if (!string.IsNullOrEmpty(ability.castSoundPath))
			{
				GFAudioManager.instance.PlayClipFromResources(ability.castSoundPath, GFAudioManager.SFX_GROUP, string.Empty, 1f, ability.castSoundDelay);
			}
			if (ability.castVOSoundPaths.Count > 0 && UnityEngine.Random.value < ability.castVOSoundProbability)
			{
				int index = UnityEngine.Random.Range(0, ability.castVOSoundPaths.Count);
				GFAudioManager.instance.PlayClipFromResources(ability.castVOSoundPaths[index], GFAudioManager.VOICEOVER_GROUP, string.Empty, 1f, ability.castVOSoundDelays[index]);
			}
			float maxCastWidth = maxWidth * ((float)button.position * 0.01f);
			float widthPerSecond = maxCastWidth / finalCastTime;
			fillCastSprite.width = 0f;
			fillCastSprite.gameObject.SetActive(true);
			initialTarget = _character.character;
			if (!isShiny || ability.shinyEffect.effectType != AbilityShinyEffectType.IgnoreInterrupt)
			{
				initialTarget.InterruptReceived += HandleInterruptReceived;
			}
			_character.Play(ability.animName + " cast intro", true);
			while (isCasting)
			{
				yield return null;
				if (interruptResult != null && interruptResult.isInterrupted)
				{
					isCasting = false;
					castingAbility = null;
					isNetworkCasting = false;
					isNetworkCastSuccessful = false;
					fillCastSprite.gameObject.SetActive(false);
					break;
				}
				if (isPaused)
				{
					continue;
				}
				if (_character.animator.IsName("idle"))
				{
					_character.Play(ability.animName + " cast intro", true);
				}
				if (fillCastSprite.width < maxCastWidth)
				{
					prevFillCastSpriteWidth = fillCastSprite.width;
					fillCastSprite.width += widthPerSecond * DeltaTime;
					if (!(fillCastSprite.width >= maxCastWidth))
					{
						continue;
					}
					fillCastSprite.width = maxCastWidth;
					if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
					{
						if (!_isPlayerMeter)
						{
							continue;
						}
						playerAction = TT2NetManager.SendBattleDecision(button.abilityData.uid, character.characterDefinition.uid, false, progress, character.character.finalDamageMultiplier, Environment.TickCount);
						yield return new WaitWhile(() => playerAction.isWaiting);
						if (playerAction.isConfirmed)
						{
							fillCastSprite.gameObject.SetActive(false);
							isCasting = false;
							isNetworkCastSuccessful = false;
							castingAbility = null;
							while (BattleController.instance.waiting)
							{
								yield return null;
							}
							break;
						}
						fillCastSprite.width = prevFillCastSpriteWidth;
					}
					else
					{
						fillCastSprite.gameObject.SetActive(false);
						isCasting = false;
						isNetworkCastSuccessful = false;
						castingAbility = null;
					}
				}
				else if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network && !_isPlayerMeter && !isNetworkCasting && isNetworkCastSuccessful)
				{
					fillCastSprite.gameObject.SetActive(false);
					isCasting = false;
					isNetworkCastSuccessful = false;
					castingAbility = null;
					while (BattleController.instance.waiting)
					{
						yield return null;
					}
				}
			}
			if (this == BattleController.instance.playerActionMeter)
			{
				Player.instance.UnsetTrigger("AbilityCast");
			}
			initialTarget.InterruptReceived -= HandleInterruptReceived;
			if (_character.character != initialTarget)
			{
				yield break;
			}
			if (interruptResult != null && interruptResult.isInterrupted)
			{
				if (!interruptResult.isCausedByDamage)
				{
					_character.Play("idle");
				}
				tk2dSlicedSpriteHelper barChunk2 = UnityEngine.Object.Instantiate(fillSprite);
				barChunk2.transform.parent = fillSprite.transform.parent;
				barChunk2.transform.localPosition = fillSprite.transform.localPosition;
				barChunk2.transform.localRotation = fillSprite.transform.localRotation;
				barChunk2.transform.localScale = fillSprite.transform.localScale;
				barChunk2.width = maxWidth * ((float)button.position * 0.01f);
				fillSprite.width = Mathf.Max(0f, fillSprite.width - barChunk2.width);
				OnUpdated(fillSprite.width / maxWidth);
				Vector3 newPosition2 = fillSprite.transform.localPosition;
				newPosition2.x = barChunk2.width;
				fillSprite.transform.localPosition = newPosition2;
				Sequence seq = DOTween.Sequence();
				seq.Append(DOTween.ToAlpha(() => barChunk2.slicedSprite.color, (Color x) =>
				{
					barChunk2.slicedSprite.color = x;
				}, 0f, 0.25f));
				seq.AppendInterval(0.25f);
				seq.Append(fillSprite.transform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutBounce));
				seq.Play();
				yield return seq.WaitForCompletion();
				UnityEngine.Object.Destroy(barChunk2.gameObject);
				yield break;
			}
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			if (_isPlayerMeter && playerAction == null)
			{
				BattleController.instance.ForceStartWaiting();
				playerAction = TT2NetManager.SendBattleDecision(button.abilityData.uid, character.characterDefinition.uid, false, progress, character.character.finalDamageMultiplier, Environment.TickCount);
				yield return new WaitWhile(() => playerAction.isWaiting);
				if (!playerAction.isConfirmed)
				{
					if (TT2NetManager.Instance.myNetworkPlayer == null || !TT2NetManager.Instance.myNetworkPlayer.IsPlayingAction)
					{
						if ((character != null && character.character.isDead) || (BattleController.instance.enemyActionMeter.character != null && BattleController.instance.enemyActionMeter.character.character.isDead))
						{
							BattleController.instance.CheckCharacterDeath();
						}
						else
						{
							BattleController.instance.ForceEndWaiting();
						}
					}
					yield break;
				}
			}
			TT2NetManager.SendPlayingAction(true, RpcTarget.All);
		}
		OnAbilityUsed(ability);
		if ((!isShiny || ability.shinyEffect.effectType != AbilityShinyEffectType.NoBarCost) && ability.channelingData.speed <= 0f)
		{
			tk2dSlicedSpriteHelper barChunk3 = UnityEngine.Object.Instantiate(fillSprite);
			barChunk3.transform.parent = fillSprite.transform.parent;
			barChunk3.transform.localPosition = fillSprite.transform.localPosition;
			barChunk3.transform.localRotation = fillSprite.transform.localRotation;
			barChunk3.transform.localScale = fillSprite.transform.localScale;
			barChunk3.width = maxWidth * ((float)button.position * 0.01f);
			fillSprite.width -= barChunk3.width;
			OnUpdated(fillSprite.width / maxWidth);
			Vector3 newPosition3 = fillSprite.transform.localPosition;
			newPosition3.x = barChunk3.width;
			fillSprite.transform.localPosition = newPosition3;
			tk2dTintAdapter chunkTint = barChunk3.GetComponent<tk2dTintAdapter>();
			spendBarSequence = DOTween.Sequence();
			spendBarSequence.Append(DOTween.To(() => chunkTint.tintColor, (Color x) =>
			{
				chunkTint.tintColor = x;
			}, Color.white, 1f / 30f).SetLoops(6, LoopType.Yoyo));
			spendBarSequence.Append(DOTween.ToAlpha(() => barChunk3.slicedSprite.color, (Color x) =>
			{
				barChunk3.slicedSprite.color = x;
			}, 0f, 0.25f));
			spendBarSequence.AppendCallback(() =>
			{
				UnityEngine.Object.Destroy(barChunk3.gameObject);
			});
			spendBarSequence.AppendInterval(0.25f);
			spendBarSequence.Append(fillSprite.transform.DOLocalMoveX(0f, 0.5f).SetEase(Ease.OutBounce));
			Vector3 fillSpriteLocalPos = fillSprite.transform.localPosition;
			fillSpriteLocalPos.x = 0f;
			spendBarSequence.OnComplete(() =>
			{
				fillSprite.transform.localPosition = fillSpriteLocalPos;
			});
			spendBarSequence.Play();
			if (ability.pauseGameplayOnUse)
			{
				yield return new WaitForSeconds(0.5f);
			}
		}
		BattleCharacter target = null;
		List<Character> targetParty;
		List<Character> casterParty;
		if (ability.targetCount == TargetCount.Self || ability.targetType == TargetType.Friendly)
		{
			target = _character;
			targetParty = ((!(_character == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyParty : BattleController.instance.playerParty);
			casterParty = ((!(_character == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyParty : BattleController.instance.playerParty);
		}
		else
		{
			target = ((!(_character == BattleController.instance.playerCharacter)) ? BattleController.instance.playerCharacter : BattleController.instance.enemyCharacter);
			targetParty = ((!(_character == BattleController.instance.playerCharacter)) ? BattleController.instance.playerParty : BattleController.instance.enemyParty);
			casterParty = ((!(_character == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyParty : BattleController.instance.playerParty);
		}
		Type scriptType = null;
		int chargeCount = 0;
		if (ability.numChargesRequired > 0)
		{
			TrackAbilityCharge(_character.character, ability);
			chargeCount = GetAbilityChargeCount(_character.character, ability);
			scriptType = ((chargeCount != ability.numChargesRequired) ? typeof(AbilityScript_Charge) : Type.GetType(ability.scriptName));
		}
		else
		{
			scriptType = Type.GetType(ability.scriptName);
			if (scriptType == null)
			{
				UnityEngine.Debug.LogError("The specified ability script does not exist, so falling back to the default");
				scriptType = typeof(AbilityScript_Default);
			}
		}
		AbilityScript script = Activator.CreateInstance(scriptType) as AbilityScript;
		script.Init(ability, _character, target, casterParty, targetParty, button.position);
		int seed = -1;
		UnityEngine.Random.State prevRandomState = UnityEngine.Random.state;
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			seed = ((playerAction == null) ? GetComponent<PlayerAI_Network>().NextAbilitySeed : playerAction.seed);
			UnityEngine.Random.InitState(seed);
		}
		if (ability.pauseGameplayOnUse)
		{
			yield return StartCoroutine(script.Play());
		}
		else
		{
			StartCoroutine(script.Play());
		}
		while (BattleController.instance.playerBattleCamera.isPlaying || BattleController.instance.enemyBattleCamera.isPlaying)
		{
			yield return null;
		}
		script.CleanUp();
		if (ability.numChargesRequired > 0 && chargeCount == ability.numChargesRequired)
		{
			ClearAbilityCharges(_character.character, ability);
		}
		if (ability.channelingData.speed > 0f)
		{
			isChanneling = true;
			channelingAbility = ability;
		}
		if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
		{
			TT2NetManager.SendPlayingAction(false);
			yield return new WaitWhile(() => TT2NetManager.Instance.IsPlayerPlayingAction);
			UnityEngine.Debug.Log("BOTH PLAYERS DONE PLAYING ACTION");
		}
		UnityEngine.Random.state = prevRandomState;
		OnAbilityFinished(ability);
		CheckForBatteryCharge(seed);
		if (!isChanneling)
		{
			yield break;
		}
		interruptResult = null;
		if (this == BattleController.instance.playerActionMeter)
		{
			Player.instance.SetTrigger("AbilityChannel");
		}
		OnChannelStarted(ability, character.character);
		if (!string.IsNullOrEmpty(ability.castSoundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(ability.castSoundPath, GFAudioManager.SFX_GROUP, string.Empty, 1f, ability.castSoundDelay);
		}
		if (ability.castVOSoundPaths.Count > 0 && UnityEngine.Random.value < ability.castVOSoundProbability)
		{
			int index2 = UnityEngine.Random.Range(0, ability.castVOSoundPaths.Count);
			GFAudioManager.instance.PlayClipFromResources(ability.castVOSoundPaths[index2], GFAudioManager.VOICEOVER_GROUP, string.Empty, 1f, ability.castVOSoundDelays[index2]);
		}
		fillChannelSprite.width = fillSprite.width;
		fillChannelSprite.gameObject.SetActive(true);
		fillSprite.gameObject.SetActive(false);
		initialTarget = _character.character;
		if (!isShiny || ability.shinyEffect.effectType != AbilityShinyEffectType.IgnoreInterrupt)
		{
			initialTarget.InterruptReceived += HandleInterruptReceived;
		}
		channelingStepIndex = 0;
		channelingStep = channelingAbility.channelingData.channelingSteps[channelingStepIndex];
		channelingScript = script as AbilityScript_Channeling;
		channelingTickTimer = 0f;
		channelingStepDurationTimer = channelingStep.abilityEffectTickRate;
		PortraitSwitcher portraits = ((!(this == BattleController.instance.playerActionMeter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
		if (channelingScript != null)
		{
			while (isChanneling)
			{
				yield return null;
				if (interruptResult != null && interruptResult.isInterrupted)
				{
					isChanneling = false;
					fillChannelSprite.gameObject.SetActive(false);
					break;
				}
				if (BattleController.instance.waiting || isPaused)
				{
					continue;
				}
				if (portraits.isEnabled)
				{
					portraits.isEnabled = false;
				}
				if (_character.animator.IsName("idle"))
				{
					_character.Play(ability.animName);
				}
				if (_character.animator.IsName(ability.animName))
				{
					continue;
				}
				float prevTickTimer = channelingTickTimer;
				float prevDurationTimer = channelingStepDurationTimer;
				float width = fillChannelSprite.width;
				channelingTickTimer += DeltaTime;
				if (channelingTickTimer >= channelingStep.abilityEffectTickRate)
				{
					channelingTickTimer = 0f;
					if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
					{
						if (_isPlayerMeter)
						{
							StartCoroutine(WaitForChannelingTickConfirmation(prevTickTimer, prevDurationTimer, width));
						}
					}
					else
					{
						ChannelingTick();
						if (!isChanneling)
						{
							break;
						}
						if (interruptResult != null && interruptResult.isInterrupted)
						{
							continue;
						}
					}
				}
				if (channelingStepIndex < ability.channelingData.channelingSteps.Count - 1)
				{
					channelingStepDurationTimer += DeltaTime;
					if (channelingStepDurationTimer >= channelingStep.duration)
					{
						channelingStepDurationTimer = 0f;
						if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
						{
							if (_isPlayerMeter)
							{
								StartCoroutine(WaitForChannelingNextStepConfirmation(prevTickTimer, prevDurationTimer, width));
							}
						}
						else
						{
							ChannelingNextStep();
						}
					}
				}
				float num = (ability.channelingData.speed + (float)(-_fillSpeedBonus)) * (1f + (1f - _fillSpeedModifier));
				fillChannelSprite.width -= num * (DeltaTime * _character.character.channelTimeModifier);
				if (!(fillChannelSprite.width <= 0f))
				{
					continue;
				}
				if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
				{
					if (_isPlayerMeter)
					{
						StartCoroutine(WaitForChannelingExpireConfirmation(prevTickTimer, prevDurationTimer, width));
					}
				}
				else
				{
					ChannelingExpire();
				}
			}
		}
		if (this == BattleController.instance.playerActionMeter)
		{
			Player.instance.UnsetTrigger("AbilityChannel");
		}
		initialTarget.InterruptReceived -= HandleInterruptReceived;
		OnChannelFinished(ability, initialTarget);
		if (interruptResult != null && interruptResult.isInterrupted)
		{
			if (!interruptResult.isCausedByDamage)
			{
				_character.Play("idle");
			}
			if (!BattleController.instance.waiting && !HasStatusEffect<ActionMeterDisabler>() && !HasStatusEffect<Root>())
			{
				portraits.isEnabled = true;
			}
			fillSprite.gameObject.SetActive(true);
			fillSprite.width = 0f;
			OnUpdated(fillSprite.width / maxWidth);
		}
	}

	private IEnumerator WaitForChannelingTickConfirmation(float prevTickTimer, float prevDurationTimer, float prevFillChannelSpriteWidth)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendChannelingTick(_character.character.uid, Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		if (!playerAction.isConfirmed)
		{
			channelingTickTimer = prevTickTimer;
			channelingStepDurationTimer = prevDurationTimer;
			fillChannelSprite.width = prevFillChannelSpriteWidth;
		}
		else
		{
			ChannelingTick(playerAction.seed);
		}
	}

	private IEnumerator WaitForChannelingNextStepConfirmation(float prevTickTimer, float prevDurationTimer, float prevFillChannelSpriteWidth)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendChannelingNextStep(Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		if (!playerAction.isConfirmed)
		{
			channelingTickTimer = prevTickTimer;
			channelingStepDurationTimer = prevDurationTimer;
			fillChannelSprite.width = prevFillChannelSpriteWidth;
		}
		else
		{
			ChannelingNextStep(playerAction.seed);
		}
	}

	private IEnumerator WaitForChannelingExpireConfirmation(float prevTickTimer, float prevDurationTimer, float prevFillChannelSpriteWidth)
	{
		TT2NetPlayerAction playerAction = TT2NetManager.SendChannelingExpire(Environment.TickCount);
		yield return new WaitWhile(() => playerAction.isWaiting);
		if (!playerAction.isConfirmed)
		{
			channelingTickTimer = prevTickTimer;
			channelingStepDurationTimer = prevDurationTimer;
			fillChannelSprite.width = prevFillChannelSpriteWidth;
		}
		else
		{
			ChannelingExpire(playerAction.seed);
		}
	}

	public void ChannelingTick(int seed = -1)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (seed != -1)
		{
			UnityEngine.Random.InitState(seed);
		}
		bool resolved;
		channelingScript.Tick(channelingStep, out resolved);
		if (seed != -1)
		{
			UnityEngine.Random.state = state;
		}
		if (!resolved)
		{
			interruptResult = new InterruptResult(true, false);
		}
		else if (channelingScript.target.character.isDead)
		{
			isChanneling = false;
			fillSprite.gameObject.SetActive(true);
			fillSprite.width = fillChannelSprite.width;
			OnUpdated(fillSprite.width / maxWidth);
			fillChannelSprite.gameObject.SetActive(false);
			_character.Play(channelingAbility.animName + " outro", true);
		}
	}

	public void ChannelingNextStep(int seed = -1)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (seed != -1)
		{
			UnityEngine.Random.InitState(seed);
		}
		channelingStepIndex = Mathf.Clamp(++channelingStepIndex, 0, channelingAbility.channelingData.channelingSteps.Count - 1);
		channelingStep = channelingAbility.channelingData.channelingSteps[channelingStepIndex];
		if (seed != -1)
		{
			UnityEngine.Random.state = state;
		}
	}

	public void ChannelingExpire(int seed = -1)
	{
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (seed != -1)
		{
			UnityEngine.Random.InitState(seed);
		}
		fillChannelSprite.width = 0f;
		fillChannelSprite.gameObject.SetActive(false);
		fillSprite.gameObject.SetActive(true);
		fillSprite.width = 0f;
		OnUpdated(fillSprite.width / maxWidth);
		isChanneling = false;
		_character.Play(channelingAbility.animName + " outro", true);
		if (!HasStatusEffect<ActionMeterDisabler>() && !HasStatusEffect<Root>())
		{
			PortraitSwitcher portraitSwitcher = ((!(this == BattleController.instance.playerActionMeter)) ? BattleController.instance.enemyPortraits : BattleController.instance.playerPortraits);
			portraitSwitcher.isEnabled = true;
		}
		if (seed != -1)
		{
			UnityEngine.Random.state = state;
		}
	}

	private void CheckForBatteryCharge(int seed = -1)
	{
		if ((_isPlayerMeter && !BattleController.instance.playerAccessoryButton.isInitialized) || (!_isPlayerMeter && !BattleController.instance.enemyAccessoryButton.isInitialized))
		{
			return;
		}
		UnityEngine.Random.State state = UnityEngine.Random.state;
		if (seed != -1)
		{
			UnityEngine.Random.InitState(seed);
		}
		tempMinAbilitiesUsedCount++;
		if (tempMinAbilitiesUsedCount > GameManager.instance.gameSettings.minAbilitiesUsedToShowTic && batteryTickContainer != null && !batteryTickContainer.gameObject.activeSelf && GameManager.instance.gameSettings.defaultBatteryChargeTicChance > 0f && UnityEngine.Random.value <= GameManager.instance.gameSettings.defaultBatteryChargeTicChance)
		{
			batteryTickContainer.gameObject.SetActive(true);
			batteryTickContainer.transform.localPosition = new Vector3(Mathf.Min(UnityEngine.Random.Range(maxWidth * progress + 20f, maxWidth - 2f), maxWidth), 0f, 0f);
			float num = 15f;
			if (abilityButtons != null && abilityButtons.Count > 1 && batteryTickContainer.transform.position.x > abilityButtons[1].transform.position.x - num && batteryTickContainer.transform.position.x < abilityButtons[1].transform.position.x + num)
			{
				batteryTickContainer.transform.localPosition = new Vector3(batteryTickContainer.transform.localPosition.x - num, batteryTickContainer.transform.localPosition.y, batteryTickContainer.transform.localPosition.z);
			}
			if (abilityButtons != null && abilityButtons.Count > 2 && batteryTickContainer.transform.position.x > abilityButtons[2].transform.position.x - num && batteryTickContainer.transform.position.x < abilityButtons[2].transform.position.x + num)
			{
				batteryTickContainer.transform.localPosition = new Vector3(batteryTickContainer.transform.localPosition.x - num, batteryTickContainer.transform.localPosition.y, batteryTickContainer.transform.localPosition.z);
			}
		}
		if (seed != -1)
		{
			UnityEngine.Random.state = state;
		}
	}

	private void TrackAbilityCharge(Character character, AbilityData abilityData)
	{
		if (!abilityChargeCounts.ContainsKey(character))
		{
			abilityChargeCounts.Add(character, new Dictionary<AbilityData, int>());
		}
		if (!abilityChargeCounts[character].ContainsKey(abilityData))
		{
			abilityChargeCounts[character].Add(abilityData, 1);
		}
		else
		{
			abilityChargeCounts[character][abilityData]++;
		}
		if (AbilityChargeUpdated__BackingField != null)
		{
			AbilityChargeUpdated__BackingField(character, abilityData, abilityChargeCounts[character][abilityData]);
		}
	}

	public int GetAbilityChargeCount(Character character, AbilityData abilityData)
	{
		if (!abilityChargeCounts.ContainsKey(character))
		{
			return 0;
		}
		if (!abilityChargeCounts[character].ContainsKey(abilityData))
		{
			return 0;
		}
		return abilityChargeCounts[character][abilityData];
	}

	private void ClearAbilityCharges(Character character, AbilityData abilityData)
	{
		if (abilityChargeCounts.ContainsKey(character) && abilityChargeCounts[character].ContainsKey(abilityData))
		{
			abilityChargeCounts[character][abilityData] = 0;
			if (AbilityChargeUpdated__BackingField != null)
			{
				AbilityChargeUpdated__BackingField(character, abilityData, 0);
			}
		}
	}

	private float GetPartySpeedRating()
	{
		int num = 0;
		int num2 = 0;
		if (_isPlayerMeter)
		{
			num = BattleController.instance.playerParty.Count;
			for (int i = 0; i < num; i++)
			{
				num2 += BattleController.instance.playerParty[i].characterDefinition.actionMeterSpeedRating;
			}
		}
		else
		{
			num = BattleController.instance.enemyParty.Count;
			for (int j = 0; j < num; j++)
			{
				num2 += BattleController.instance.enemyParty[j].characterDefinition.actionMeterSpeedRating;
			}
		}
		return (float)num2 / (float)num;
	}

	private void HandleInterruptReceived(InterruptResult result)
	{
		if (result.isInterrupted && _character != null && _character.character != null)
		{
			_character.character.InterruptReceived -= HandleInterruptReceived;
		}
		interruptResult = result;
	}

	private void OnInitialized()
	{
		if (Initialized__BackingField != null)
		{
			Initialized__BackingField();
		}
	}

	protected void OnUpdated(float percent)
	{
		if (Updated__BackingField != null)
		{
			Updated__BackingField(percent);
		}
	}

	protected void OnCastStarted(AbilityButton button)
	{
		if (CastStarted__BackingField != null)
		{
			CastStarted__BackingField(this);
		}
		if (CastStartedAbility__BackingField != null)
		{
			CastStartedAbility__BackingField(this, button);
		}
	}

	protected void OnChannelStarted(AbilityData abilityData, Character channelerCharacter)
	{
		if (ChannelStarted__BackingField != null)
		{
			ChannelStarted__BackingField(abilityData, channelerCharacter);
		}
	}

	protected void OnChannelFinished(AbilityData abilityData, Character channelerCharacter)
	{
		if (ChannelFinished__BackingField != null)
		{
			ChannelFinished__BackingField(abilityData, channelerCharacter);
		}
	}

	protected void OnAbilityUsed(AbilityData abilityData)
	{
		if (AbilityUsed__BackingField != null)
		{
			AbilityUsed__BackingField(this, _character.character, abilityData);
		}
	}

	protected void OnAbilityFinished(AbilityData abilityData)
	{
		if (AbilityFinished__BackingField != null)
		{
			AbilityFinished__BackingField(this, _character.character, abilityData);
		}
		if ((abilityData == lastAbilityUsed && abilityData.type == AbilityType.StatusEffect && abilityData.statusEffect == StatusEffectName.ActionMeterDisabler) || abilityData.secondaryStatusEffect == StatusEffectName.ActionMeterDisabler)
		{
			stunDurationFactor = Mathf.Max(stunDurationFactor + GameManager.instance.gameSettings.stunDurationReduction, GameManager.instance.gameSettings.minStunDurationFactor);
		}
		else
		{
			stunDurationFactor = 1f;
		}
		lastAbilityUsed = abilityData;
	}

	protected void OnCharacterChanged()
	{
		if (CharacterChanged__BackingField != null)
		{
			CharacterChanged__BackingField();
		}
	}

	protected void OnStatusEffectAdded(StatusEffect effect)
	{
		if (StatusEffectAdded__BackingField != null)
		{
			StatusEffectAdded__BackingField(effect);
		}
	}

	protected void OnStatusEffectRemoved(StatusEffect effect)
	{
		if (StatusEffectRemoved__BackingField != null)
		{
			StatusEffectRemoved__BackingField(effect);
		}
	}

	private void HandleTransformed()
	{
		Init();
	}
}
