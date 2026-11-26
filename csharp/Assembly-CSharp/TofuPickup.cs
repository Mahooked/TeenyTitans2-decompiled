using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TofuPickup : MonoBehaviour, IPoolable
{
	public Animator animator;

	public Renderer renderer;

	public tk2dUIItem uiItem;

	public float duration = 6f;

	private float lifetime;

	private float enemyCollectTime;

	private bool blinking;

	private bool _collected;

	private IEnumerator showBubbleCoroutine;

	private IEnumerator waitForUseCoroutine;

	private bool _isPlayerTofu = true;

	private int _direction;

	private Vector3 _randomDirection;

	public bool isPlayerTofu
	{
		get
		{
			return _isPlayerTofu;
		}
		set
		{
			Init(value);
		}
	}

	public bool collected => _collected;

	private void Awake()
	{
		if (renderer == null)
		{
			renderer = GetComponentInChildren<Renderer>();
		}
	}

	private void OnEnable()
	{
		if (BattleController.instance != null)
		{
			BattleController.instance.BattleEnded += HandleBattleEnded;
		}
		StartCoroutine("Spawn");
	}

	private void OnDisable()
	{
		if (BattleController.instance != null)
		{
			BattleController.instance.BattleEnded -= HandleBattleEnded;
		}
	}

	private void Update()
	{
		lifetime -= Time.deltaTime;
		if (!_collected && !_isPlayerTofu && (BattleController.battleDefinition == null || BattleController.battleDefinition.playerAIType != PlayerAIType.Network))
		{
			enemyCollectTime -= Time.deltaTime;
			if (enemyCollectTime <= 0f)
			{
				Collected();
			}
		}
		if (lifetime <= 0f && !_collected)
		{
			BattleObjectManager.instance.RemoveObject(gameObject);
			transform.DOKill();
			ObjectPool.PoolObject(gameObject);
		}
	}

	public void Init(bool isPlayerTofu)
	{
		Init(isPlayerTofu, (!isPlayerTofu) ? 1 : (-1));
	}

	public void Init(bool isPlayerTofu, int direction)
	{
		_isPlayerTofu = isPlayerTofu;
		_direction = direction;
		_randomDirection = new Vector3(_direction, UnityEngine.Random.Range(-0.3f, 1f), 0f);
		_randomDirection.x *= 250f;
		_randomDirection.y *= 150f;
		if (_isPlayerTofu)
		{
			renderer.material.color = Color.white;
			uiItem.enabled = true;
		}
		else
		{
			renderer.material.color = new Color(1f, 1f, 1f, 0.35f);
			uiItem.enabled = false;
			enemyCollectTime = UnityEngine.Random.Range(1f, duration - 1f);
		}
	}

	public void ForceCollect()
	{
		Collected();
	}

	private IEnumerator Spawn()
	{
		lifetime = duration;
		ShortcutExtensions.DOLocalMove(endValue: transform.localPosition + _randomDirection, target: transform, duration: duration * duration).SetEase(Ease.OutExpo);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_appear", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(duration - 1f);
		StartCoroutine("Blink");
	}

	private IEnumerator Blink()
	{
		int blinks = 10;
		float blinkOnTime = 0.1f;
		float blinkOffTime = 0.1f;
		float decay = 0.015f;
		blinking = true;
		while (blinking)
		{
			renderer.enabled = false;
			yield return new WaitForSeconds(blinkOnTime);
			blinkOnTime -= decay;
			renderer.enabled = true;
			yield return new WaitForSeconds(blinkOffTime);
			blinkOffTime -= decay;
			blinks--;
			if (blinks <= 0)
			{
				blinking = false;
			}
			yield return null;
		}
	}

	private bool CanPickup()
	{
		if (BattleController.instance.batteryGUI == null)
		{
			return true;
		}
		return BattleController.instance.batteryGUI.allowItemPickups;
	}

	private void Collected()
	{
		if (!CanPickup() || _collected)
		{
			return;
		}
		_collected = true;
		blinking = false;
		transform.DOKill();
		renderer.enabled = true;
		StopCoroutine("Spawn");
		StopCoroutine("Blink");
		animator.Play("select");
		if (_isPlayerTofu)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_collect", GFAudioManager.SFX_GROUP, string.Empty);
			if (BattleController.battleDefinition != null && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				TT2NetManager.SendTofuCollected();
			}
		}
		Vector3 endValue = ((!_isPlayerTofu) ? BattleController.instance.enemyPortraits.transform.position : BattleController.instance.playerPortraits.uiItem.transform.position);
		endValue.z = transform.position.z;
		transform.DOMove(endValue, 0.333f).SetDelay(0.3f).SetEase(Ease.OutSine)
			.OnComplete(() =>
			{
				transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f).OnComplete(() =>
				{
					transform.DOScale(0f, 0.1f).OnComplete(OnCollectedComplete);
				});
			});
	}

	private void OnCollectedComplete()
	{
		if (_isPlayerTofu)
		{
			PreShowBubble();
		}
		else if (BattleController.battleDefinition == null || BattleController.battleDefinition.playerAIType != PlayerAIType.Network)
		{
			PreWaitToUse();
		}
		BattleObjectManager.instance.RemoveObject(gameObject);
		ObjectPool.PoolObject(gameObject);
	}

	private void HandleBattleEnded()
	{
		StopCoroutine("Spawn");
		StopCoroutine("Blink");
		if (showBubbleCoroutine != null)
		{
			Coroutiner.instance.StopCoroutine(showBubbleCoroutine);
			showBubbleCoroutine = null;
		}
		if (waitForUseCoroutine != null)
		{
			Coroutiner.instance.StopCoroutine(waitForUseCoroutine);
			waitForUseCoroutine = null;
		}
		DOTween.Kill(transform);
		BattleObjectManager.instance.RemoveObject(gameObject);
		ObjectPool.PoolObject(gameObject);
	}

	private void PreShowBubble()
	{
		if (showBubbleCoroutine == null)
		{
			showBubbleCoroutine = ShowBubble();
			Coroutiner.instance.StartCoroutine(showBubbleCoroutine);
		}
	}

	private IEnumerator ShowBubble()
	{
		BattleController battleController = BattleController.instance;
		while (battleController != null && (battleController.waiting || battleController.playerActionMeter.isCasting || battleController.playerActionMeter.isChanneling))
		{
			yield return null;
		}
		if (battleController != null && battleController.isBattleRunning)
		{
			TofuBubble tofuBubble = BattleObjectManager.instance.GetObject<TofuBubble>();
			if (tofuBubble != null)
			{
				BattleObjectManager.instance.RemoveObject(tofuBubble.gameObject);
				ObjectPool.PoolObject(tofuBubble.gameObject);
			}
			tofuBubble = ObjectPool.GetObjectFromResource<TofuBubble>("Prefabs/GUI/TofuBubble");
			tofuBubble.Init(BattleController.instance.playerCharacter);
			BattleObjectManager.instance.AddObject(tofuBubble.gameObject);
		}
		showBubbleCoroutine = null;
	}

	private void PreWaitToUse()
	{
		if (waitForUseCoroutine == null)
		{
			waitForUseCoroutine = WaitToUse(UnityEngine.Random.Range(0f, 2f));
			Coroutiner.instance.StartCoroutine(waitForUseCoroutine);
		}
	}

	private IEnumerator WaitToUse(float delay = 0f)
	{
		yield return new WaitForSeconds(delay);
		if (TT2NetMatch.IsPlayer2Available())
		{
			waitForUseCoroutine = null;
			yield break;
		}
		BattleController battleController = BattleController.instance;
		while (battleController != null && battleController.waiting && battleController.isBattleRunning)
		{
			yield return null;
		}
		if (battleController != null && battleController.isBattleRunning)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_battle_tofu_collect", GFAudioManager.SFX_GROUP, string.Empty);
			List<TofuType> list = new List<TofuType>((TofuType[])Enum.GetValues(typeof(TofuType)));
			if (battleController.enemyAccessoryUID < 0)
			{
				list.Remove(TofuType.Battery);
			}
			TofuType tofuType = list[UnityEngine.Random.Range(0, list.Count)];
			TofuEffect tofuEffect = new GameObject("TofuEffect - " + battleController.enemyCharacter.characterDefinition.characterName).AddComponent<TofuEffect>();
			tofuEffect.StartEffectForTarget(tofuType, battleController.enemyCharacter, null);
			if (tofuType == TofuType.Cleanse || tofuType == TofuType.Sleepy)
			{
				BattleDisplayManager.instance.ShowMessage(Language.Get("TOFUD", "ABILS,CHIPS,ACHVS,LDBS"), battleController.playerCharacter);
			}
		}
		waitForUseCoroutine = null;
	}

	public void ResetObject()
	{
		_collected = false;
		blinking = false;
		renderer.enabled = true;
		transform.localScale = Vector3.one;
	}
}
