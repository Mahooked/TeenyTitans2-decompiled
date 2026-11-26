using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class BattleCharacter : MonoBehaviour, IPoolable
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Transformed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action DeathCompleted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<string> AbilityAnimationPlayed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<RepaintColorSlot> Repainted__BackingField;

	public Transform model;

	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public Animator faceAnimator;

	public AnimatorEventListener faceAnimatorEventListener;

	public Animator extrasAnimator;

	public AnimatorEventListener extrasAnimatorEventListener;

	public Animator faceAnimatorSecondary;

	public Transform top;

	public Transform center;

	public Transform tofuAnchor;

	private Texture originalMainTexture;

	private Character _character;

	private Material _sharedMat;

	private Material _sharedFaceMat;

	private MaterialTex[] _sharedMats;

	private MaterialTex[] _sharedExtrasMats;

	[SerializeField]
	private RuntimeAnimatorController _cameraController;

	[SerializeField]
	private Avatar _cameraAvatar;

	private float _dizzyWeight;

	private float _dancingWeight;

	private float _straightLookWeight;

	private float _cuteWeight;

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
					_character.HealReceived -= HandleHealReceived;
					_character.BuffReceived -= HandleBuffReceived;
					_character.Transformed -= HandleTransformed;
				}
				_character = value;
				if (_character != null)
				{
					_character.DamageReceived += HandleDamageReceived;
					_character.HealReceived += HandleHealReceived;
					_character.BuffReceived += HandleBuffReceived;
					_character.Transformed += HandleTransformed;
				}
			}
		}
	}

	public CharacterDefinition characterDefinition
	{
		get
		{
			return (_character != null) ? _character.characterDefinition : null;
		}
	}

	public Material sharedMat
	{
		get
		{
			return _sharedMat;
		}
	}

	public Material sharedFaceMat
	{
		get
		{
			return _sharedFaceMat;
		}
	}

	public MaterialTex[] sharedMats
	{
		get
		{
			return _sharedMats;
		}
	}

	public MaterialTex[] sharedExtrasMats
	{
		get
		{
			return _sharedExtrasMats;
		}
	}

	public RuntimeAnimatorController cameraController
	{
		get
		{
			return _cameraController;
		}
		set
		{
			_cameraController = value;
		}
	}

	public Avatar cameraAvatar
	{
		get
		{
			return _cameraAvatar;
		}
		set
		{
			_cameraAvatar = value;
		}
	}

	public float dizzyWeight
	{
		get
		{
			return _dizzyWeight;
		}
		set
		{
			if (value != _dizzyWeight)
			{
				_dizzyWeight = value;
				SetAnimationLayerWeights("Dizzy", _dizzyWeight);
			}
		}
	}

	public float dancingWeight
	{
		get
		{
			return _dancingWeight;
		}
		set
		{
			if (value != _dancingWeight)
			{
				_dancingWeight = value;
				SetAnimationLayerWeights("Dancing", _dancingWeight);
			}
		}
	}

	public float straightLookWeight
	{
		get
		{
			return _straightLookWeight;
		}
		set
		{
			if (value != _straightLookWeight)
			{
				_straightLookWeight = value;
				SetAnimationLayerWeights("Straight Look", _straightLookWeight);
			}
		}
	}

	public float cuteWeight
	{
		get
		{
			return _cuteWeight;
		}
		set
		{
			if (value != _cuteWeight)
			{
				_cuteWeight = value;
				SetAnimationLayerWeights("Cute", _cuteWeight);
			}
		}
	}

	public event Action Transformed
	{
		add
		{
			Action action = Transformed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Transformed__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Transformed__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Transformed__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action DeathCompleted
	{
		add
		{
			Action action = DeathCompleted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeathCompleted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = DeathCompleted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DeathCompleted__BackingField, (Action)Delegate.Remove(action2, value), action);
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

	public event Action<RepaintColorSlot> Repainted
	{
		add
		{
			Action<RepaintColorSlot> action = Repainted__BackingField;
			Action<RepaintColorSlot> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Repainted__BackingField, (Action<RepaintColorSlot>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<RepaintColorSlot> action = Repainted__BackingField;
			Action<RepaintColorSlot> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Repainted__BackingField, (Action<RepaintColorSlot>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (animatorEventListener == null)
		{
			animatorEventListener = animator.GetComponent<AnimatorEventListener>();
		}
		if (faceAnimatorEventListener == null && faceAnimator != null)
		{
			faceAnimatorEventListener = faceAnimator.GetComponent<AnimatorEventListener>();
		}
		if (extrasAnimatorEventListener == null)
		{
			extrasAnimatorEventListener = extrasAnimator.GetComponent<AnimatorEventListener>();
		}
		if (tofuAnchor == null)
		{
			model.Find("Model_Master_jnt/Model_C_COM_jnt");
		}
		SkinnedMeshRenderer[] componentsInChildren = GetComponentsInChildren<SkinnedMeshRenderer>(true);
		_sharedMat = new Material(componentsInChildren[0].sharedMaterial);
		List<MaterialTex> list = new List<MaterialTex>();
		int num = componentsInChildren.Length;
		for (int i = 0; i < num; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = componentsInChildren[i];
			if (skinnedMeshRenderer.sharedMaterial == componentsInChildren[0].sharedMaterial)
			{
				skinnedMeshRenderer.material = _sharedMat;
				continue;
			}
			Material mat = new Material(skinnedMeshRenderer.sharedMaterial);
			MaterialTex materialTex = list.Find((MaterialTex x) => x.mat == mat || (x.mat.mainTexture == mat.mainTexture && x.mat.shader == mat.shader));
			if (materialTex == null)
			{
				skinnedMeshRenderer.material = mat;
				list.Add(new MaterialTex(mat, mat.mainTexture));
			}
			else
			{
				skinnedMeshRenderer.material = materialTex.mat;
			}
		}
		_sharedMats = list.ToArray();
		if (faceAnimator != null)
		{
			Renderer[] componentsInChildren2 = faceAnimator.GetComponentsInChildren<Renderer>(true);
			if (componentsInChildren2.Length > 0)
			{
				_sharedFaceMat = new Material(componentsInChildren2[0].sharedMaterial);
				num = componentsInChildren2.Length;
				for (int num2 = 0; num2 < num; num2++)
				{
					componentsInChildren2[num2].material = sharedFaceMat;
				}
			}
			if (faceAnimatorSecondary != null)
			{
				Renderer[] componentsInChildren3 = faceAnimatorSecondary.GetComponentsInChildren<Renderer>(true);
				if (componentsInChildren3.Length > 0)
				{
					num = componentsInChildren3.Length;
					for (int num3 = 0; num3 < num; num3++)
					{
						componentsInChildren3[num3].material = sharedFaceMat;
					}
				}
			}
		}
		List<MaterialTex> list2 = new List<MaterialTex>();
		if (extrasAnimator != null)
		{
			Renderer[] componentsInChildren4 = extrasAnimator.GetComponentsInChildren<Renderer>(true);
			if (componentsInChildren4.Length > 0)
			{
				num = componentsInChildren4.Length;
				for (int num4 = 0; num4 < num; num4++)
				{
					if (componentsInChildren4[num4].sharedMaterial != null)
					{
						Material material = new Material(componentsInChildren4[num4].sharedMaterial);
						componentsInChildren4[num4].material = material;
						list2.Add(new MaterialTex(material, material.mainTexture));
					}
				}
			}
			_sharedExtrasMats = list2.ToArray();
		}
		originalMainTexture = _sharedMat.mainTexture;
	}

	private void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleAnimatorEventTriggered;
	}

	private void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleAnimatorEventTriggered;
	}

	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(_sharedMat);
		if (_sharedFaceMat != null)
		{
			UnityEngine.Object.Destroy(_sharedFaceMat);
		}
		if (_character != null)
		{
			_character.DamageReceived -= HandleDamageReceived;
			_character.HealReceived -= HandleHealReceived;
			_character.BuffReceived -= HandleBuffReceived;
			_character.Transformed -= HandleTransformed;
			_character = null;
		}
	}

	public bool Play(string stateName, float normalizedTime = 0f, bool isAbilityAnimation = false)
	{
		if (animator.HasState(0, Animator.StringToHash("Base Layer." + stateName)))
		{
			animator.Play(stateName, 0, normalizedTime);
			if (faceAnimator != null)
			{
				faceAnimator.Play(stateName, 0, normalizedTime);
			}
			if (faceAnimatorSecondary != null)
			{
				faceAnimatorSecondary.Play(stateName, 0, normalizedTime);
			}
			if (extrasAnimator != null)
			{
				extrasAnimator.Play(stateName, 0, normalizedTime);
			}
			if (isAbilityAnimation)
			{
				OnAbilityAnimationPlayed(stateName);
			}
			return true;
		}
		return false;
	}

	public bool Play(string stateName, bool isAbilityAnimation)
	{
		return Play(stateName, 0f, isAbilityAnimation);
	}

	public void PlayHit()
	{
		Play("hit");
		DOTween.Kill(_sharedMat);
		_sharedMat.SetColor("_TintColor", Color.white);
		_sharedMat.DOFade(0f, "_TintColor", 0.4f).SetEase(Ease.InSine);
		if (_sharedFaceMat != null)
		{
			DOTween.Kill(_sharedFaceMat);
			_sharedFaceMat.SetColor("_TintColor", Color.white);
			_sharedFaceMat.DOFade(0f, "_TintColor", 0.4f).SetEase(Ease.InSine);
		}
		GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/FX/Hit_ParticleSystem");
		objectFromResource.transform.position = new Vector3(top.position.x, top.position.y * 0.5f, top.position.z);
		objectFromResource.transform.localRotation = Quaternion.Euler(0f, (base.transform.localScale.z < 0f) ? 180 : 0, 0f);
	}

	public void PlaySmallHit()
	{
		Play("hit small");
		DOTween.Kill(_sharedMat);
		_sharedMat.SetColor("_TintColor", Color.white);
		_sharedMat.DOFade(0f, "_TintColor", 0.4f).SetEase(Ease.InSine);
		if (_sharedFaceMat != null)
		{
			DOTween.Kill(_sharedFaceMat);
			_sharedFaceMat.SetColor("_TintColor", Color.white);
			_sharedFaceMat.DOFade(0f, "_TintColor", 0.4f).SetEase(Ease.InSine);
		}
		GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/FX/SmallHit_ParticleSystem");
		objectFromResource.transform.position = new Vector3(top.position.x, top.position.y * 0.5f, top.position.z);
		objectFromResource.transform.localRotation = Quaternion.Euler(0f, (base.transform.localScale.z < 0f) ? 180 : 0, 0f);
	}

	public unsafe void PlayDeath()
	{
		Play("death");
		DOTween.Kill(_sharedMat);
		_sharedMat.SetColor("_TintColor", Color.white);
		if (_sharedFaceMat != null)
		{
			DOTween.Kill(_sharedFaceMat);
			_sharedFaceMat.SetColor("_TintColor", Color.white);
		}
		GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/FX/Hit_ParticleSystem");
		objectFromResource.transform.position = new Vector3(top.position.x, top.position.y * 0.5f, top.position.z);
		objectFromResource.transform.localRotation = Quaternion.Euler(0f, (base.transform.localScale.z < 0f) ? 180 : 0, 0f);
		if (DOTween.IsTweening("deathTime"))
		{
			DOTween.Kill("deathTime");
		}
		Time.timeScale = 0.25f;
		DOTween.To(new DOGetter<float>(null, (IntPtr)(void*)(ulong)(UIntPtr/*delegate*<float>*/)(&Time.get_timeScale)), (float x) =>
		{
			Time.timeScale = x;
		}, 1f, 0.5f).SetDelay(1f).SetEase(Ease.InSine)
			.SetUpdate(true)
			.SetId("deathTime");
	}

	public void PlayRevive()
	{
		Play("idle");
		if (DOTween.IsTweening("deathTime"))
		{
			DOTween.Kill("deathTime");
		}
		DOTween.Kill(_sharedMat);
		_sharedMat.SetColor("_TintColor", Color.clear);
		if (_sharedFaceMat != null)
		{
			DOTween.Kill(_sharedFaceMat);
			_sharedFaceMat.SetColor("_TintColor", Color.clear);
		}
		animator.gameObject.SetActive(true);
		if (faceAnimator != null)
		{
			faceAnimator.gameObject.SetActive(true);
		}
		if (faceAnimatorSecondary != null)
		{
			faceAnimatorSecondary.gameObject.SetActive(true);
		}
		if (extrasAnimator != null)
		{
			extrasAnimator.gameObject.SetActive(true);
		}
	}

	private void SetAnimationLayerWeights(string layerName, float weight)
	{
		weight = Mathf.Clamp01(weight);
		int num = ((!animator.gameObject.activeInHierarchy) ? (-1) : animator.GetLayerIndex(layerName));
		if (num != -1)
		{
			animator.SetLayerWeight(num, weight);
			if (faceAnimator != null)
			{
				faceAnimator.SetLayerWeight(num, Mathf.RoundToInt(weight));
			}
			if (faceAnimatorSecondary != null)
			{
				faceAnimatorSecondary.SetLayerWeight(num, Mathf.RoundToInt(weight));
			}
			if (extrasAnimator != null)
			{
				extrasAnimator.SetLayerWeight(num, weight);
			}
		}
	}

	public void SetFloatParameter(string paramName, float value)
	{
		animator.SetFloat(paramName, value);
		if (faceAnimator != null)
		{
			faceAnimator.SetFloat(paramName, value);
		}
		if (faceAnimatorSecondary != null)
		{
			faceAnimatorSecondary.SetFloat(paramName, value);
		}
		if (extrasAnimator != null)
		{
			extrasAnimator.SetFloat(paramName, value);
		}
	}

	private void HandleAnimatorEventTriggered(AnimationEvent evt)
	{
		string stringParameter = evt.stringParameter;
		if (stringParameter != null && stringParameter == "deathComplete")
		{
			animator.gameObject.SetActive(false);
			if (faceAnimator != null)
			{
				faceAnimator.gameObject.SetActive(false);
			}
			if (faceAnimatorSecondary != null)
			{
				faceAnimatorSecondary.gameObject.SetActive(false);
			}
			if (extrasAnimator != null)
			{
				extrasAnimator.gameObject.SetActive(false);
			}
			GameObject objectFromResource = ObjectPool.GetObjectFromResource("Prefabs/FX/HitDeath_ParticleSystem");
			objectFromResource.transform.position = new Vector3(top.position.x, top.position.y * 0.5f, top.position.z);
			objectFromResource.transform.localRotation = Quaternion.Euler(0f, (base.transform.localScale.z < 0f) ? 180 : 0, 0f);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_death", GFAudioManager.SFX_GROUP, string.Empty);
			if (_character.characterDefinition.sounds.deathSounds.soundPaths.Count > 0 && UnityEngine.Random.value < _character.characterDefinition.sounds.deathSounds.probability)
			{
				GFAudioManager.instance.PlayClipFromResources(_character.characterDefinition.sounds.deathSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
			OnDeathCompleted();
		}
	}

	private void HandleDamageReceived(DamageResult damageResult)
	{
		if (damageResult.isDodged)
		{
			if (damageResult.useMissedMessage)
			{
				BattleDisplayManager.instance.ShowMessage(Language.Get("MISSED", "ABILS,CHIPS,ACHVS,LDBS"), this);
			}
			else
			{
				ObjectPool.GetObjectFromResource("Prefabs/GUI/Battle/Dodge_Text", BattleDisplayManager.instance.GetPositionInGUICamera(this));
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_dodge", GFAudioManager.SFX_GROUP, string.Empty);
			return;
		}
		BattleDisplayManager.instance.ShowDamageResult(damageResult, this);
		if (damageResult.luckBonusDamage > 0)
		{
			ObjectPool.GetObjectFromResource("Prefabs/FX/LuckyDuck_Particle", top.position - new Vector3(0f, top.position.y * 0.5f, 0f));
		}
		if (damageResult.impactType != ImpactType.None)
		{
			if (damageResult.abilityData != null && !string.IsNullOrEmpty(damageResult.abilityData.hitSoundPath))
			{
				GFAudioManager.instance.PlayClipFromResources(damageResult.abilityData.hitSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
			}
			else
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_charhurt", GFAudioManager.SFX_GROUP, string.Empty);
			}
		}
		if (_character.isDead && damageResult.isKillingBlow)
		{
			PlayDeath();
		}
		else
		{
			if (damageResult.totalDamage <= 0)
			{
				return;
			}
			if (damageResult.impactType == ImpactType.Small)
			{
				PlaySmallHit();
				if (_character.characterDefinition.sounds.hurtSounds.soundPaths.Count > 0 && UnityEngine.Random.value < _character.characterDefinition.sounds.hurtSounds.probability)
				{
					GFAudioManager.instance.PlayClipFromResources(_character.characterDefinition.sounds.hurtSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
				}
			}
			else if (damageResult.impactType == ImpactType.Normal)
			{
				PlayHit();
				if (_character.characterDefinition.sounds.hurtSounds.soundPaths.Count > 0 && UnityEngine.Random.value < _character.characterDefinition.sounds.hurtSounds.probability)
				{
					GFAudioManager.instance.PlayClipFromResources(_character.characterDefinition.sounds.hurtSounds.GetRandomSound(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
				}
			}
		}
	}

	private void HandleHealReceived(HealResult healResult)
	{
		BattleDisplayManager.instance.ShowHealResult(healResult, this);
	}

	private void HandleBuffReceived(BuffResult buffResult)
	{
		BattleDisplayManager.instance.ShowBuffResult(buffResult, this);
	}

	private void HandleTransformed()
	{
		if (Transformed__BackingField != null)
		{
			Transformed__BackingField();
		}
	}

	protected void OnDeathCompleted()
	{
		if (DeathCompleted__BackingField != null)
		{
			DeathCompleted__BackingField();
		}
	}

	protected void OnAbilityAnimationPlayed(string animName)
	{
		if (AbilityAnimationPlayed__BackingField != null)
		{
			AbilityAnimationPlayed__BackingField(animName);
		}
	}

	public void RepaintCharacter(int repaintColorID)
	{
		RepaintColorSlot repaintColorSlotForID = character.characterDefinition.GetRepaintColorSlotForID(repaintColorID);
		if (repaintColorSlotForID != null)
		{
			string texturePathScaled = repaintColorSlotForID.texturePathScaled;
			Texture texture = Resources.Load<Texture>(texturePathScaled);
			if (texture != null)
			{
				if (_sharedMat != null)
				{
					_sharedMat.mainTexture = texture;
				}
				if (_sharedFaceMat != null)
				{
					_sharedFaceMat.mainTexture = texture;
				}
				if (_sharedMats != null)
				{
					for (int i = 0; i < _sharedMats.Length; i++)
					{
						Material mat = _sharedMats[i].mat;
						if (mat.mainTexture == originalMainTexture)
						{
							mat.mainTexture = texture;
							mat.SetTexture("_Texture", texture);
						}
					}
				}
				if (_sharedExtrasMats != null)
				{
					for (int j = 0; j < _sharedExtrasMats.Length; j++)
					{
						Material mat2 = _sharedExtrasMats[j].mat;
						if (mat2.mainTexture == originalMainTexture)
						{
							mat2.mainTexture = texture;
							mat2.SetTexture("_Texture", texture);
						}
					}
				}
			}
		}
		else
		{
			if (_sharedMat != null)
			{
				_sharedMat.mainTexture = originalMainTexture;
			}
			if (_sharedFaceMat != null)
			{
				_sharedFaceMat.mainTexture = originalMainTexture;
			}
			if (_sharedMats != null)
			{
				for (int k = 0; k < _sharedMats.Length; k++)
				{
					MaterialTex materialTex = _sharedMats[k];
					materialTex.mat.mainTexture = materialTex.originalTexture;
				}
			}
			if (_sharedExtrasMats != null)
			{
				for (int l = 0; l < _sharedExtrasMats.Length; l++)
				{
					MaterialTex materialTex2 = _sharedExtrasMats[l];
					materialTex2.mat.mainTexture = materialTex2.originalTexture;
				}
			}
		}
		if (Repainted__BackingField != null)
		{
			Repainted__BackingField(repaintColorSlotForID);
		}
	}

	public void ResetObject()
	{
		base.transform.localScale = Vector3.one;
		base.transform.localPosition = Vector3.zero;
		animator.gameObject.SetActive(true);
		if (faceAnimator != null)
		{
			faceAnimator.gameObject.SetActive(true);
		}
		if (faceAnimatorSecondary != null)
		{
			faceAnimatorSecondary.gameObject.SetActive(true);
		}
		if (extrasAnimator != null)
		{
			extrasAnimator.gameObject.SetActive(true);
		}
		sharedMat.SetColor("_TintColor", new Color(1f, 1f, 1f, 0f));
		if (_sharedFaceMat != null)
		{
			sharedFaceMat.SetColor("_TintColor", new Color(1f, 1f, 1f, 0f));
		}
		if (_character != null)
		{
			_character.DamageReceived -= HandleDamageReceived;
			_character.HealReceived -= HandleHealReceived;
			_character.BuffReceived -= HandleBuffReceived;
			_character.Transformed -= HandleTransformed;
			_character = null;
		}
		dancingWeight = 0f;
		dizzyWeight = 0f;
		straightLookWeight = 0f;
		cuteWeight = 0f;
	}
}
