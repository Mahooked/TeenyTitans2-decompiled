using System;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class BattleCamera : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OffsetInStart__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OffsetInComplete__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OffsetOutStart__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action OffsetOutComplete__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action AnimationComplete__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> AbilityAnimationPlayed__BackingField;

	public SplitCameras splitCameras;

	public Camera camera;

	public Camera fxCamera;

	public Camera iconCamera;

	public Animator animator;

	public Transform offset;

	public CameraShake cameraShake;

	public AnimatorEventListener animatorEventListener;

	public Vector3 finalOffsetPosition;

	private CharacterDefinition otherCharacterData;

	private BattleCamera otherCamera;

	private Vector3 otherCharacterPortraitOffset;

	private Vector3 splitOffset;

	private Vector3 fullSplitOffset;

	private string prevMode;

	private float prevAngle;

	private Quaternion prevOtherCameraRotation;

	private bool isOffsetRunning;

	private Tweener positionTween;

	private Tweener rotationTween;

	private BattleController _battleController;

	private bool _isPlaying;

	private Character _character;

	private Vector3 targetOffsetPosition
	{
		get
		{
			if (_character == null)
			{
				return Vector3.zero + finalOffsetPosition;
			}
			if (CompareTag("Player"))
			{
				bool flag = _character.HasStatusEffect<Flight>() && _character.GetStatusEffect<Flight>().allowFlightCameraOffset;
				if (_character.HasZoomedOutStatusEffect() || battleController.playerActionMeter.HasZoomedOutStatusEffect() || battleController.playerAccessoryButton.isActivated)
				{
					if (flag)
					{
						return _character.playerFlightCameraOffsetPositionZoomedOut + finalOffsetPosition;
					}
					return _character.playerCameraOffsetPositionZoomedOut + ((!battleController.playerAccessoryButton.isActivated) ? Vector3.zero : battleController.playerAccessoryButton.accessoryCameraOffsetPosition) + finalOffsetPosition;
				}
				if (flag)
				{
					return _character.playerFlightCameraOffsetPosition + finalOffsetPosition;
				}
				return _character.playerCameraOffsetPosition + finalOffsetPosition;
			}
			bool flag2 = _character.HasStatusEffect<Flight>() && _character.GetStatusEffect<Flight>().allowFlightCameraOffset;
			if (_character.HasZoomedOutStatusEffect() || battleController.enemyActionMeter.HasZoomedOutStatusEffect() || battleController.enemyAccessoryButton.isActivated)
			{
				if (flag2)
				{
					return _character.enemyFlightCameraOffsetPositionZoomedOut + finalOffsetPosition;
				}
				return _character.enemyCameraOffsetPositionZoomedOut + ((!battleController.enemyAccessoryButton.isActivated) ? Vector3.zero : battleController.enemyAccessoryButton.accessoryCameraOffsetPositionEnemy) + finalOffsetPosition;
			}
			if (flag2)
			{
				return _character.enemyFlightCameraOffsetPosition + finalOffsetPosition;
			}
			return _character.enemyCameraOffsetPosition + finalOffsetPosition;
		}
	}

	private Vector3 targetOffsetRotation
	{
		get
		{
			if (_character == null)
			{
				return Vector3.zero;
			}
			if (CompareTag("Player"))
			{
				bool flag = _character.HasStatusEffect<Flight>() && _character.GetStatusEffect<Flight>().allowFlightCameraOffset;
				if (_character.HasZoomedOutStatusEffect() || battleController.playerActionMeter.HasZoomedOutStatusEffect() || battleController.playerAccessoryButton.isActivated)
				{
					if (flag)
					{
						return _character.playerFlightCameraOffsetRotationZoomedOut;
					}
					return _character.playerCameraOffsetRotationZoomedOut;
				}
				if (flag)
				{
					return _character.playerFlightCameraOffsetRotation;
				}
				return _character.playerCameraOffsetRotation;
			}
			bool flag2 = _character.HasStatusEffect<Flight>() && _character.GetStatusEffect<Flight>().allowFlightCameraOffset;
			if (_character.HasZoomedOutStatusEffect() || battleController.enemyActionMeter.HasZoomedOutStatusEffect() || battleController.enemyAccessoryButton.isActivated)
			{
				if (flag2)
				{
					return _character.enemyFlightCameraOffsetRotationZoomedOut;
				}
				return _character.enemyCameraOffsetRotationZoomedOut;
			}
			if (flag2)
			{
				return _character.enemyFlightCameraOffsetRotation;
			}
			return _character.enemyCameraOffsetRotation;
		}
	}

	public bool isFullscreen { get; private set; }

	private BattleController battleController
	{
		get
		{
			if (_battleController == null)
			{
				_battleController = BattleController.instance;
			}
			return _battleController;
		}
	}

	public bool isPlaying
	{
		get
		{
			return _isPlaying || isOffsetRunning;
		}
	}

	public Character character
	{
		get
		{
			return _character;
		}
		set
		{
			if (value != _character)
			{
				if (_character != null)
				{
					_character.DamageReceived -= HandleDamageReceived;
					_character.StatusEffectAdded -= HandleStatusEffectUpdate;
					_character.StatusEffectRemoved -= HandleStatusEffectUpdate;
				}
				_character = value;
				if (_character != null)
				{
					_character.DamageReceived += HandleDamageReceived;
					_character.StatusEffectAdded += HandleStatusEffectUpdate;
					_character.StatusEffectRemoved += HandleStatusEffectUpdate;
				}
			}
		}
	}

	public event Action OffsetInStart
	{
		add
		{
			Action action = OffsetInStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetInStart__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OffsetInStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetInStart__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OffsetInComplete
	{
		add
		{
			Action action = OffsetInComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetInComplete__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OffsetInComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetInComplete__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OffsetOutStart
	{
		add
		{
			Action action = OffsetOutStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetOutStart__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OffsetOutStart__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetOutStart__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action OffsetOutComplete
	{
		add
		{
			Action action = OffsetOutComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetOutComplete__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = OffsetOutComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OffsetOutComplete__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action AnimationComplete
	{
		add
		{
			Action action = AnimationComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AnimationComplete__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = AnimationComplete__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AnimationComplete__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<string> AbilityAnimationPlayed
	{
		add
		{
			Action<string> action = AbilityAnimationPlayed__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityAnimationPlayed__BackingField, (Action<string>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<string> action = AbilityAnimationPlayed__BackingField;
			Action<string> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AbilityAnimationPlayed__BackingField, (Action<string>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
		if (animatorEventListener == null)
		{
			animatorEventListener = GetComponent<AnimatorEventListener>();
		}
		if (base.tag.Equals("Player"))
		{
			splitOffset = new Vector3(0.03f, 0.075f);
			fullSplitOffset = new Vector3(0.16f, 0.075f);
		}
		else
		{
			splitOffset = new Vector3(-0.03f, -0.075f);
			fullSplitOffset = new Vector3(-0.16f, -0.075f);
		}
	}

	private void Start()
	{
		if (base.tag == "Player")
		{
			battleController.playerActionMeter.StatusEffectAdded += HandleStatusEffectUpdate;
			battleController.playerActionMeter.StatusEffectRemoved += HandleStatusEffectUpdate;
			battleController.playerAccessoryButton.AccessoryDestroyed += HandleAccessoryDestroyed;
		}
		else
		{
			battleController.enemyActionMeter.StatusEffectAdded += HandleStatusEffectUpdate;
			battleController.enemyActionMeter.StatusEffectRemoved += HandleStatusEffectUpdate;
			battleController.enemyAccessoryButton.AccessoryDestroyed += HandleAccessoryDestroyed;
		}
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.DamageReceived -= HandleDamageReceived;
			_character.StatusEffectAdded -= HandleStatusEffectUpdate;
			_character.StatusEffectRemoved -= HandleStatusEffectUpdate;
			_character = null;
		}
		if (battleController != null)
		{
			if (base.tag == "Player")
			{
				battleController.playerActionMeter.StatusEffectAdded -= HandleStatusEffectUpdate;
				battleController.playerActionMeter.StatusEffectRemoved -= HandleStatusEffectUpdate;
				battleController.playerAccessoryButton.AccessoryDestroyed -= HandleAccessoryDestroyed;
			}
			else
			{
				battleController.enemyActionMeter.StatusEffectAdded -= HandleStatusEffectUpdate;
				battleController.enemyActionMeter.StatusEffectRemoved -= HandleStatusEffectUpdate;
				battleController.enemyAccessoryButton.AccessoryDestroyed -= HandleAccessoryDestroyed;
			}
		}
		OffsetInStart__BackingField = null;
		OffsetInComplete__BackingField = null;
		OffsetOutStart__BackingField = null;
		OffsetOutComplete__BackingField = null;
		AnimationComplete__BackingField = null;
	}

	public void Play(string stateName, float normalizedTime = 0f, bool isAbilityAnimation = false)
	{
		if (animator.HasState(0, Animator.StringToHash("Base Layer." + stateName)))
		{
			animator.Play(stateName, 0, normalizedTime);
			_isPlaying = true;
			if (isAbilityAnimation)
			{
				OnAbilityAnimationPlayed(stateName);
			}
		}
	}

	public void Play(string stateName, bool isAbilityAnimation)
	{
		Play(stateName, 0f, isAbilityAnimation);
	}

	public void EnableCameras(bool enable = true)
	{
		camera.enabled = enable;
		if (fxCamera != null)
		{
			fxCamera.enabled = enable;
		}
		if (iconCamera != null)
		{
			iconCamera.enabled = enable;
		}
	}

	private void ShowAbstract(AnimationEvent evt)
	{
		camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Default"));
		camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Background"));
		AbstractCamera.instance.Show(evt.objectReferenceParameter);
	}

	private void HideAbstract(AnimationEvent evt)
	{
		camera.cullingMask |= 1 << LayerMask.NameToLayer("Default");
		camera.cullingMask |= 1 << LayerMask.NameToLayer("Background");
		AbstractCamera.instance.Hide();
	}

	public void OffsetIn(AnimationEvent evt)
	{
		isOffsetRunning = true;
		if (base.tag.Equals("Player"))
		{
			otherCharacterData = BattleController.instance.enemyCharacter.characterDefinition;
			otherCamera = BattleController.instance.enemyBattleCamera;
			if (otherCharacterData != null)
			{
				otherCharacterPortraitOffset = otherCharacterData.topRightPortraitOffset;
			}
		}
		else
		{
			otherCharacterData = BattleController.instance.playerCharacter.characterDefinition;
			otherCamera = BattleController.instance.playerBattleCamera;
			if (otherCharacterData != null)
			{
				otherCharacterPortraitOffset = otherCharacterData.bottomLeftPortraitOffset;
			}
		}
		prevAngle = splitCameras.angle;
		prevMode = evt.stringParameter.ToLower();
		prevOtherCameraRotation = otherCamera.camera.transform.localRotation;
		float duration = (float)evt.intParameter / 60f;
		DOTween.Kill(offset);
		Sequence sequence = DOTween.Sequence().OnComplete(HandleOffsetInComplete);
		sequence.Append(offset.DOLocalMove(Vector3.zero, duration).SetEase(Ease.InOutSine));
		sequence.Insert(0f, offset.DOLocalRotate(Vector3.zero, duration).SetEase(Ease.InOutSine));
		if (prevMode.Equals("portrait"))
		{
			DOTween.Kill(otherCamera.offset);
			sequence.Insert(0f, DOTween.To(() => splitCameras.offset, (Vector3 x) =>
			{
				splitCameras.offset = x;
			}, splitOffset, duration).SetEase(Ease.InOutSine));
			sequence.Insert(0f, DOTween.To(() => splitCameras.angle, (float x) =>
			{
				splitCameras.angle = x;
			}, 240f, duration).SetEase(Ease.InOutSine));
			sequence.Insert(0f, otherCamera.offset.DOLocalMove(otherCharacterPortraitOffset, duration).SetEase(Ease.InOutSine));
		}
		else if (prevMode.Equals("fullscreen"))
		{
			sequence.Insert(0f, DOTween.To(() => splitCameras.offset, (Vector3 x) =>
			{
				splitCameras.offset = x;
			}, fullSplitOffset, duration).SetEase(Ease.InOutSine));
			sequence.Insert(0f, DOTween.To(() => splitCameras.angle, (float x) =>
			{
				splitCameras.angle = x;
			}, 240f, duration).SetEase(Ease.InOutSine));
			isFullscreen = true;
		}
		else if (prevMode.Equals("merge"))
		{
			DOTween.Kill(otherCamera.offset);
			splitCameras.SetMainCamera(camera);
			positionTween = otherCamera.offset.DOMove(camera.transform.position, duration).SetEase(Ease.InOutSine).SetUpdate(UpdateType.Late);
			positionTween.OnUpdate(UpdatePosition);
			positionTween.OnComplete(OnTweenComplete);
			rotationTween = otherCamera.camera.transform.DORotate(camera.transform.rotation.eulerAngles, duration).SetEase(Ease.InOutSine).SetUpdate(UpdateType.Late);
			rotationTween.OnUpdate(UpdateRotation);
			rotationTween.OnComplete(OnTweenComplete);
		}
		sequence.Play();
		OnOffsetInStart();
	}

	public void OffsetOut(AnimationEvent evt)
	{
		DOTween.Kill(offset);
		otherCamera.EnableCameras();
		Sequence sequence = DOTween.Sequence().OnComplete(HandleOffsetOutComplete);
		float duration = (float)evt.intParameter / 60f;
		sequence.Append(offset.DOLocalMove(targetOffsetPosition, duration).SetEase(Ease.InOutSine));
		sequence.Insert(0f, offset.DOLocalRotate(targetOffsetRotation, duration).SetEase(Ease.InOutSine));
		if (prevMode.Equals("portrait"))
		{
			DOTween.Kill(otherCamera.offset);
			sequence.Insert(0f, DOTween.To(() => splitCameras.offset, (Vector3 x) =>
			{
				splitCameras.offset = x;
			}, Vector3.zero, duration).SetEase(Ease.InOutSine));
			sequence.Insert(0f, DOTween.To(() => splitCameras.angle, (float x) =>
			{
				splitCameras.angle = x;
			}, prevAngle, duration).SetEase(Ease.InOutSine));
			sequence.Insert(0f, otherCamera.offset.DOLocalMove(otherCamera.targetOffsetPosition, duration).SetEase(Ease.InOutSine));
		}
		else if (prevMode.Equals("fullscreen"))
		{
			sequence.Insert(0f, DOTween.To(() => splitCameras.offset, (Vector3 x) =>
			{
				splitCameras.offset = x;
			}, Vector3.zero, duration).SetEase(Ease.InOutSine));
			sequence.Insert(0f, DOTween.To(() => splitCameras.angle, (float x) =>
			{
				splitCameras.angle = x;
			}, prevAngle, duration).SetEase(Ease.InOutSine));
			isFullscreen = false;
		}
		else if (prevMode.Equals("merge"))
		{
			DOTween.Kill(otherCamera.offset);
			otherCamera.offset.position = camera.transform.position;
			otherCamera.camera.transform.rotation = camera.transform.rotation;
			splitCameras.InstantSplit();
			positionTween = otherCamera.offset.DOLocalMove(otherCamera.targetOffsetPosition, duration).SetEase(Ease.InOutSine).SetUpdate(UpdateType.Late);
			rotationTween = otherCamera.camera.transform.DOLocalRotate(prevOtherCameraRotation.eulerAngles, duration).SetEase(Ease.InOutSine).SetUpdate(UpdateType.Late);
		}
		sequence.Play();
		OnOffsetOutStart();
	}

	public void UpdateCameraOffsetPosition(float tweenDuration = 1f, bool force = false)
	{
		if (!isOffsetRunning || force)
		{
			DOTween.Kill(offset);
			offset.DOLocalMove(targetOffsetPosition, tweenDuration).SetEase(Ease.InOutSine);
			offset.DOLocalRotate(targetOffsetRotation, tweenDuration).SetEase(Ease.InOutSine);
		}
	}

	private void UpdatePosition()
	{
		float num = positionTween.Duration() - positionTween.fullPosition;
		if (num == 0f)
		{
			positionTween.Complete();
			positionTween = null;
		}
		else
		{
			positionTween = positionTween.ChangeEndValue(camera.transform.position, num, true);
		}
	}

	private void UpdateRotation()
	{
		float num = rotationTween.Duration() - rotationTween.fullPosition;
		if (num == 0f)
		{
			rotationTween.Complete();
			rotationTween = null;
		}
		else
		{
			rotationTween = rotationTween.ChangeEndValue(camera.transform.rotation.eulerAngles, num, true);
		}
	}

	private void HandleDamageReceived(DamageResult damageResult)
	{
		if (!damageResult.isDodged && !_character.isDead && damageResult.impactType == ImpactType.Normal && damageResult.totalDamage > 0)
		{
			Play("hit");
		}
	}

	private void HandleStatusEffectUpdate(StatusEffect effect)
	{
		UpdateCameraOffsetPosition();
	}

	private void HandleOffsetInComplete()
	{
		otherCamera.EnableCameras(false);
		OnOffsetInComplete();
	}

	private void HandleOffsetOutComplete()
	{
		OnOffsetOutComplete();
	}

	private void HandleAccessoryDestroyed()
	{
		UpdateCameraOffsetPosition();
	}

	private void OnTweenComplete()
	{
		if (positionTween == null && rotationTween == null)
		{
			splitCameras.InstantMerge(true);
		}
	}

	protected void OnOffsetInStart()
	{
		if (OffsetInStart__BackingField != null)
		{
			OffsetInStart__BackingField();
		}
	}

	protected void OnOffsetInComplete()
	{
		if (OffsetInComplete__BackingField != null)
		{
			OffsetInComplete__BackingField();
		}
	}

	protected void OnOffsetOutStart()
	{
		if (OffsetOutStart__BackingField != null)
		{
			OffsetOutStart__BackingField();
		}
	}

	protected void OnOffsetOutComplete()
	{
		isOffsetRunning = false;
		if (OffsetOutComplete__BackingField != null)
		{
			OffsetOutComplete__BackingField();
		}
	}

	protected void OnAnimationComplete()
	{
		if (AnimationComplete__BackingField != null)
		{
			AnimationComplete__BackingField();
		}
	}

	protected void OnAbilityAnimationPlayed(string animName)
	{
		if (AbilityAnimationPlayed__BackingField != null)
		{
			AbilityAnimationPlayed__BackingField(animName);
		}
	}

	private void Complete(AnimationEvent evt)
	{
		_isPlaying = false;
		OnAnimationComplete();
	}
}
