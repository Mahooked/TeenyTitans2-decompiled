using DG.Tweening;
using TMPro;
using UnityEngine;

public class WalletGUI : MonoBehaviour
{
	public CurrencyType currencyType;

	public Animator animator;

	public bool cityWallet;

	public TextMeshPro totalText;

	public ParticleAttractor particleAttractor;

	public tk2dBaseSprite coinSprite;

	public tk2dBaseSprite ticketSprite;

	public float displayTime = 3f;

	private int total;

	private int totalTickets;

	private float displayTimeCounter;

	public bool isShown => !animator.GetCurrentAnimatorStateInfo(0).IsName("Hidden");

	public bool isHidden => animator.GetCurrentAnimatorStateInfo(0).IsName("Hidden");

	private void Awake()
	{
		if (currencyType == CurrencyType.Coins)
		{
			total = Player.instance.currency;
			SetTotalText(total.ToString());
		}
		else
		{
			total = Player.instance.rainbowCurrency;
			SetTotalText(total + "<sprite name=\"icon_currency_rainbowCoin\">");
		}
		UpdateCurrencySprite();
		if (cityWallet)
		{
			Player.instance.CurrencyUpdated += CurrencyUpdated;
		}
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
		if (cityWallet)
		{
			Player.instance.CurrencyUpdated -= CurrencyUpdated;
		}
	}

	private void CurrencyUpdated(int currency)
	{
		if (FlashSaleController.instance == null || !FlashSaleController.instance.FlashSaleActive)
		{
			if (currencyType == CurrencyType.Coins)
			{
				total = Player.instance.currency;
				SetTotalText(total.ToString());
			}
			else
			{
				total = Player.instance.rainbowCurrency;
				SetTotalText(total + "<sprite name=\"icon_currency_rainbowCoin\">");
			}
		}
		else
		{
			SetTotalText(totalTickets.ToString());
		}
	}

	private void Update()
	{
		if ((!cityWallet || currencyType == CurrencyType.RainbowCoins) && displayTimeCounter > 0f)
		{
			displayTimeCounter -= Time.deltaTime;
			if (displayTimeCounter < 0f)
			{
				displayTimeCounter = 0f;
				Hide();
			}
		}
	}

	public void Show(bool force = false, bool resetDisplayTimer = false)
	{
		if (force || !cityWallet)
		{
			if (cityWallet)
			{
				if (DOTween.IsTweening(totalText.transform))
				{
					totalText.transform.DOComplete();
				}
				totalText.transform.DOLocalMoveX(-88f, 0.3333f).SetEase(Ease.OutBack);
			}
			else
			{
				animator.Play("Show");
			}
		}
		if (resetDisplayTimer)
		{
			displayTimeCounter = displayTime;
		}
		UpdateCurrencySprite();
	}

	public void Hide()
	{
		if (!isShown)
		{
			return;
		}
		if (cityWallet)
		{
			if (DOTween.IsTweening(totalText.transform))
			{
				totalText.transform.DOComplete();
			}
			totalText.transform.DOLocalMoveX(200f, 0.3333f).SetEase(Ease.InBack);
		}
		else
		{
			animator.Play("Hide");
		}
	}

	public void AddToWallet(int amount)
	{
		if (FlashSaleController.instance != null && FlashSaleController.instance.FlashSaleActive)
		{
			if (amount >= 0)
			{
				Player.instance.AddCurrency(amount);
			}
			else if (amount < 0)
			{
				Player.instance.RemoveCurrency(amount);
			}
			total += amount;
			total = Mathf.Max(total, 0);
			PlayBumpAnimation();
		}
		else
		{
			Show();
			displayTimeCounter = displayTime;
			AddToTotal(amount);
		}
	}

	public void AddToWalletDontShow(int amount)
	{
		displayTimeCounter = displayTime;
		AddToTotal(amount);
	}

	public void AddToWalletTickets(int amount, bool show = true)
	{
		if (show)
		{
			Show();
		}
		displayTimeCounter = displayTime;
		AddToTotalTickets(amount);
	}

	private void AddToTotal(int amount)
	{
		if (amount >= 0)
		{
			Player.instance.AddCurrency(currencyType, amount);
		}
		else if (amount < 0)
		{
			Player.instance.RemoveCurrency(currencyType, -amount);
		}
		if (!cityWallet)
		{
			total += amount;
		}
		total = Mathf.Max(total, 0);
		if (currencyType == CurrencyType.Coins)
		{
			SetTotalText(total.ToString());
		}
		else
		{
			SetTotalText(total + "<sprite name=\"icon_currency_rainbowCoin\">");
		}
		PlayBumpAnimation();
	}

	private void AddToTotalTickets(int amount)
	{
		if (amount >= 0)
		{
			Player.instance.AddCurrencyTickets(amount);
		}
		else if (amount < 0)
		{
			Player.instance.RemoveCurrencyTickets(-amount);
		}
		totalTickets += amount;
		totalTickets = Mathf.Max(totalTickets, 0);
		SetTotalText(totalTickets.ToString());
		PlayBumpAnimation();
	}

	private void PlayBumpAnimation()
	{
		if (cityWallet)
		{
			animator.Play("WalletCity_Bump", 0, 0f);
		}
		else if (isShown)
		{
			animator.Play("Bump", 0, 0f);
		}
	}

	public void BumpAmount(int amount)
	{
		if (!cityWallet)
		{
			return;
		}
		total += amount;
		if (FlashSaleController.instance == null || !FlashSaleController.instance.FlashSaleActive)
		{
			if (currencyType == CurrencyType.Coins)
			{
				SetTotalText(total.ToString());
			}
			else
			{
				SetTotalText(total + "<sprite name=\"icon_currency_rainbowCoin\">");
			}
		}
		animator.Play("WalletCity_Bump", 0, 0f);
	}

	public void UpdateCurrencySprite()
	{
		if (FlashSaleController.instance != null && (FlashSaleController.instance.FlashSaleActive || FlashSaleController.instance.IntroSequenceActive))
		{
			if (coinSprite != null)
			{
				coinSprite.gameObject.SetActive(value: false);
			}
			if (ticketSprite != null)
			{
				ticketSprite.gameObject.SetActive(value: true);
			}
			SetTotalText(totalTickets.ToString());
			return;
		}
		if (coinSprite != null)
		{
			coinSprite.gameObject.SetActive(value: true);
		}
		if (ticketSprite != null)
		{
			ticketSprite.gameObject.SetActive(value: false);
		}
		if (currencyType == CurrencyType.Coins)
		{
			SetTotalText(total.ToString());
		}
		else
		{
			SetTotalText(total + "<sprite name=\"icon_currency_rainbowCoin\">");
		}
	}

	private void SetTotalText(string newText)
	{
		totalText.text = newText;
	}

	public void Add200Coins()
	{
		AddToWallet(200);
	}
}
