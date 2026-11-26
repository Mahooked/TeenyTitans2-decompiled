using System.Collections;
using DG.Tweening;
using UnityEngine;

public class CoinPickup : CityPickup, IPoolable
{
	public CurrencyType currencyType;

	public Animator animator;

	public Renderer renderer;

	public BoxCollider2D boxCollider2D;

	public float duration = 6f;

	public bool cityCoin;

	public CoinSpawn coinSpawner;

	public int coinNumber;

	private string randomAnimName;

	private Vector3 randomDirection;

	private ParticleSystem ps;

	private void Awake()
	{
		if (boxCollider2D == null)
		{
			boxCollider2D = GetComponent<BoxCollider2D>();
		}
	}

	protected override void OnEnable()
	{
		base.OnEnable();
		if (!cityCoin)
		{
			string[] array = new string[3] { "spawn1", "spawn2", "spawn3" };
			randomAnimName = array[Random.Range(0, array.Length)];
			randomDirection = new Vector3(-1f, Random.Range(-0.3f, 1f), 0f);
			randomDirection.x *= 220f;
			randomDirection.y *= 150f;
		}
	}

	private new IEnumerator Start()
	{
		if (!cityCoin)
		{
			transform.localPosition += new Vector3(0f, 0f, 50f);
			animator.Play(randomAnimName);
			ShortcutExtensions.DOLocalMove(endValue: transform.localPosition + randomDirection, target: transform, duration: duration * duration).SetEase(Ease.OutExpo);
			yield return new WaitForSeconds(duration);
			Collected();
		}
	}

	private new void Collected()
	{
		if (collected)
		{
			return;
		}
		collected = true;
		transform.DOKill();
		animator.Play("select");
		if (boxCollider2D != null)
		{
			boxCollider2D.enabled = false;
		}
		GFAudioManager.instance.PlayClipFromResources((currencyType != CurrencyType.Coins) ? "Audio/SFX/sfx_collect_rainbowcoin" : "Audio/SFX/sfx_battle_tofu_coincollect", GFAudioManager.SFX_GROUP, string.Empty);
		BattleController.instance.walletGUI.Show();
		transform.DOMove(BattleController.instance.walletGUI.transform.position, 0.3f).SetDelay(0.3f).SetEase(Ease.OutSine)
			.OnComplete(() =>
			{
				transform.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f).OnComplete(() =>
				{
					BattleController.instance.walletGUI.AddToWalletDontShow(5);
					transform.DOScale(0f, 0.1f).OnComplete(() =>
					{
						BattleObjectManager.instance.RemoveObject(gameObject);
						ObjectPool.PoolObject(gameObject);
					});
				});
			});
	}

	protected override void PlayerCollected()
	{
		GFAudioManager.instance.PlayClipFromResources((currencyType != CurrencyType.Coins) ? "Audio/SFX/sfx_collect_rainbowcoin" : "Audio/SFX/sfx_battle_tofu_coincollect", GFAudioManager.SFX_GROUP, string.Empty);
		ps = ObjectPool.GetObjectFromResource<ParticleSystem>((currencyType != CurrencyType.Coins) ? "Prefabs/GUI/VictoryRainbowCoin_Particle System" : "Prefabs/GUI/VictoryCoin_Particle System");
		ParticleSystem.MainModule main = ps.main;
		main.maxParticles = 1;
		Vector3 position = GUICamera.instance.ScreenToWorldPoint(Camera.main.WorldToScreenPoint(transform.position));
		position.z = 0f;
		ps.transform.position = position;
		switch (currencyType)
		{
		case CurrencyType.Coins:
			CityController.instance.walletGUI.particleAttractor.AttractParticleSystem(ps);
			CityController.instance.walletGUI.particleAttractor.ParticleDestroyedParticleSystem += HandleParticleDestroyed;
			Player.instance.AddCurrencyNoEvent(5);
			break;
		case CurrencyType.RainbowCoins:
			CityController.instance.rainbowWalletGUI.particleAttractor.AttractParticleSystem(ps);
			CityController.instance.rainbowWalletGUI.particleAttractor.ParticleDestroyedParticleSystem += HandleParticleDestroyed;
			CityController.instance.rainbowWalletGUI.Show();
			Player.instance.AddRainbowCurrencyNoEvent(1);
			break;
		}
		coinSpawner.CoinPickedUp(coinNumber);
		gameObject.SetActive(value: false);
	}

	private void HandleParticleDestroyed(ParticleSystem psParticleDestroyed)
	{
		if (psParticleDestroyed != null && ps != null && psParticleDestroyed == ps)
		{
			switch (currencyType)
			{
			case CurrencyType.Coins:
				CityController.instance.walletGUI.particleAttractor.ParticleDestroyedParticleSystem -= HandleParticleDestroyed;
				CityController.instance.walletGUI.BumpAmount(5);
				break;
			case CurrencyType.RainbowCoins:
				CityController.instance.rainbowWalletGUI.particleAttractor.ParticleDestroyedParticleSystem -= HandleParticleDestroyed;
				CityController.instance.rainbowWalletGUI.BumpAmount(1);
				CityController.instance.rainbowWalletGUI.SetToAutoHide();
				break;
			}
		}
	}

	public void ResetObject()
	{
		if (boxCollider2D != null)
		{
			boxCollider2D.enabled = true;
		}
	}
}
