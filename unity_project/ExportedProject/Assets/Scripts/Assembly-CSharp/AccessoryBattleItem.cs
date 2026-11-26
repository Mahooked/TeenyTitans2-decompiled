using System.Collections;
using UnityEngine;

public class AccessoryBattleItem : MonoBehaviour, IPoolable
{
	public Animator animator;

	public AnimatorEventListener animatorEventListener;

	public Transform destroyedEffectTargetPosition;

	public Transform itemSpawnPosition;

	public bool faceCamera = true;

	private new Transform transform;

	private IEnumerator updateRotationCoroutine;

	protected BattleCharacter targetCharacter;

	private ActionMeter targetActionMeter;

	private BattleCamera targetBattleCamera;

	private BattleCamera targetLookAtCamera;

	private IEnumerator interruptionCoroutine;

	private void Awake()
	{
		transform = GetComponent<Transform>();
	}

	protected virtual void OnEnable()
	{
		animatorEventListener.EventTriggered += HandleEventTriggered;
	}

	protected virtual void OnDisable()
	{
		animatorEventListener.EventTriggered -= HandleEventTriggered;
		if (updateRotationCoroutine != null)
		{
			StopCoroutine(updateRotationCoroutine);
		}
		if (targetBattleCamera != null)
		{
			targetBattleCamera.OffsetOutStart -= HandleOffsetOutStart;
		}
		if (targetActionMeter != null)
		{
			targetActionMeter.AbilityUsed -= HandleAbilityUsed;
		}
		targetBattleCamera = null;
		targetActionMeter = null;
	}

	public void Init(BattleCharacter battleCharacter)
	{
		targetCharacter = battleCharacter;
		if (faceCamera)
		{
			if (targetCharacter == BattleController.instance.playerCharacter)
			{
				targetActionMeter = BattleController.instance.enemyActionMeter;
				targetBattleCamera = BattleController.instance.enemyBattleCamera;
				targetLookAtCamera = BattleController.instance.playerBattleCamera;
			}
			else if (targetCharacter == BattleController.instance.enemyCharacter)
			{
				targetActionMeter = BattleController.instance.playerActionMeter;
				targetBattleCamera = BattleController.instance.playerBattleCamera;
				targetLookAtCamera = BattleController.instance.enemyBattleCamera;
			}
			targetActionMeter.AbilityUsed += HandleAbilityUsed;
			targetBattleCamera.OffsetOutStart += HandleOffsetOutStart;
			updateRotationCoroutine = UpdateRotation();
			StartCoroutine(updateRotationCoroutine);
		}
	}

	private IEnumerator UpdateRotation()
	{
		while (true)
		{
			if (BattleController.instance.isBattleRunning)
			{
				transform.rotation = Quaternion.Euler(0f, targetLookAtCamera.camera.transform.rotation.eulerAngles.y + 90f, 0f);
			}
			yield return null;
		}
	}

	private void HandleAbilityUsed(ActionMeter actionMeter, Character character, AbilityData abilityData)
	{
		if (character != targetCharacter.character && abilityData.type == AbilityType.Attack)
		{
			if (targetCharacter == BattleController.instance.playerCharacter)
			{
				targetLookAtCamera = BattleController.instance.enemyBattleCamera;
			}
			else if (targetCharacter == BattleController.instance.enemyCharacter)
			{
				targetLookAtCamera = BattleController.instance.playerBattleCamera;
			}
		}
	}

	private void HandleOffsetOutStart()
	{
		if (targetCharacter == BattleController.instance.playerCharacter)
		{
			targetLookAtCamera = BattleController.instance.playerBattleCamera;
		}
		else if (targetCharacter == BattleController.instance.enemyCharacter)
		{
			targetLookAtCamera = BattleController.instance.enemyBattleCamera;
		}
	}

	public void PlayAnimation(string animationName, bool allowBattleWaitingPause = true, bool forcePlay = false)
	{
		if (animator.IsName(animationName) && !forcePlay)
		{
			return;
		}
		animator.Play(animationName, 0, 0f);
		if (allowBattleWaitingPause)
		{
			if (interruptionCoroutine != null)
			{
				StopCoroutine(interruptionCoroutine);
			}
			interruptionCoroutine = CheckInterruptionFrame();
			StartCoroutine(interruptionCoroutine);
		}
	}

	private IEnumerator CheckInterruptionFrame(AccessoryAnimationCue animationCue = null)
	{
		yield return null;
		while (true)
		{
			animator.speed = ((!BattleController.instance.waiting || !BattleController.instance.isBattleRunning) ? 1 : 0);
			yield return null;
		}
	}

	public void PlayAnimationCue(AccessoryAnimationCue animationCue, int layer = 0)
	{
		string stateName = ((!(targetCharacter == BattleController.instance.enemyCharacter) || string.IsNullOrEmpty(animationCue.animationP2)) ? animationCue.animation : animationCue.animationP2);
		animator.Play(stateName, layer, 0f);
	}

	public void PlayOutro()
	{
		Transform transform = ((!(destroyedEffectTargetPosition != null)) ? this.transform : destroyedEffectTargetPosition);
		ObjectPool.GetObjectFromResource("Prefabs/FX/OmniExplosion", transform.position);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_abilities_beatboxexplode", GFAudioManager.SFX_GROUP, string.Empty);
		animator.speed = 1f;
		string stateName = "death";
		if (animator.HasState(0, stateName))
		{
			animator.Play(stateName, 0, 0f);
		}
		if (interruptionCoroutine != null)
		{
			StopCoroutine(interruptionCoroutine);
		}
		base.gameObject.SetActive(false);
	}

	protected virtual void HandleEventTriggered(AnimationEvent evt)
	{
		if (evt.stringParameter.Equals("DeathComplete"))
		{
			ObjectPool.PoolObject(base.gameObject);
		}
	}

	public void ResetObject()
	{
		animator.transform.localPosition = Vector3.zero;
	}
}
