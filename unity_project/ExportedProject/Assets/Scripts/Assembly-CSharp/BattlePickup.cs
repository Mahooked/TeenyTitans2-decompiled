using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class BattlePickup : MonoBehaviour, IPoolable
{
	public Animator animator;

	public Renderer renderer;

	public tk2dUIItem uiItem;

	public BoxCollider2D boxCollider2D;

	public float duration = 6f;

	public string appearSoundPath = string.Empty;

	public string collectSoundPath = string.Empty;

	public float spawnFactorX = -1f;

	public float spawnMinY = -0.3f;

	public float spawnMaxY = 1f;

	private float lifetime;

	private float enemyCollectTime;

	private bool isBlinking;

	private bool isCollected;

	protected bool _isPlayerPickup = true;

	private Vector3 _randomDirection;

	public bool isPlayerPickup
	{
		get
		{
			return _isPlayerPickup;
		}
		set
		{
			Init(value);
		}
	}

	public abstract BattlePickupType type { get; }

	private void OnEnable()
	{
		uiItem.OnDown += HandleOnDown;
		StartCoroutine("Spawn");
	}

	private void OnDisable()
	{
		uiItem.OnDown -= HandleOnDown;
	}

	private void Update()
	{
		lifetime -= Time.deltaTime;
		if (!isCollected && !_isPlayerPickup && (BattleController.battleDefinition == null || BattleController.battleDefinition.playerAIType != PlayerAIType.Network))
		{
			enemyCollectTime -= Time.deltaTime;
			if (enemyCollectTime <= 0f)
			{
				HandleOnDown();
			}
		}
		if (lifetime <= 0f && !isCollected)
		{
			BattleObjectManager.instance.RemoveObject(base.gameObject);
			base.transform.DOKill();
			ObjectPool.PoolObject(base.gameObject);
		}
	}

	public void Init(bool isPlayerPickup)
	{
		_isPlayerPickup = isPlayerPickup;
		_randomDirection = new Vector3(spawnFactorX * (float)(_isPlayerPickup ? 1 : (-1)), Random.Range(spawnMinY, spawnMaxY) * (float)(_isPlayerPickup ? 1 : (-1)), 0f);
		_randomDirection.x *= 250f;
		_randomDirection.y *= 150f;
		if (_isPlayerPickup)
		{
			renderer.material.color = Color.white;
			uiItem.enabled = true;
		}
		else
		{
			renderer.material.color = new Color(1f, 1f, 1f, 0.35f);
			uiItem.enabled = false;
			enemyCollectTime = Random.Range(1f, duration - 1f);
		}
	}

	public void ForceCollect()
	{
		HandleOnDown();
	}

	private IEnumerator Spawn()
	{
		lifetime = duration;
		ShortcutExtensions.DOLocalMove(endValue: base.transform.localPosition + _randomDirection, target: base.transform, duration: duration * duration).SetEase(Ease.OutExpo);
		GFAudioManager.instance.PlayClipFromResources(appearSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(duration - 1f);
		StartCoroutine("Blink");
	}

	private IEnumerator Blink()
	{
		int blinks = 10;
		float blinkOnTime = 0.1f;
		float blinkOffTime = 0.1f;
		float decay = 0.015f;
		isBlinking = true;
		while (isBlinking)
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
				isBlinking = false;
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

	protected virtual void HandleOnDown()
	{
		if (!CanPickup() || isCollected)
		{
			return;
		}
		isCollected = true;
		isBlinking = false;
		base.transform.DOKill();
		renderer.enabled = true;
		boxCollider2D.enabled = false;
		StopCoroutine("Spawn");
		StopCoroutine("Blink");
		animator.Play("select");
		if (_isPlayerPickup)
		{
			GFAudioManager.instance.PlayClipFromResources(collectSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
			if (BattleController.battleDefinition != null && BattleController.battleDefinition.playerAIType == PlayerAIType.Network)
			{
				TT2NetManager.SendBattlePickupCollected((int)type);
			}
		}
		Vector3 destination = GetDestination();
		destination.z = base.transform.position.z;
		base.transform.DOMove(destination, 0.333f).SetDelay(0.3f).SetEase(Ease.OutSine)
			.OnComplete(() =>
			{
				base.transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f).OnComplete(() =>
				{
					base.transform.DOScale(0f, 0.1f).OnComplete(() =>
					{
						Collected();
						EventMessenger.Instance.Raise(new BattlePickupCollectedEvent(this));
						BattleObjectManager.instance.RemoveObject(base.gameObject);
						ObjectPool.PoolObject(base.gameObject);
					});
				});
			});
	}

	protected abstract Vector3 GetDestination();

	protected abstract void Collected();

	public void ResetObject()
	{
		isCollected = false;
		isBlinking = false;
		renderer.enabled = true;
		base.transform.localScale = Vector3.one;
		boxCollider2D.enabled = true;
	}
}
