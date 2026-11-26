using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CurrencyGUI : MonoBehaviour
{
	private static Dictionary<CurrencyType, string> currencySpriteNames = new Dictionary<CurrencyType, string>
	{
		{
			CurrencyType.Coins,
			"icon_currency_coin"
		},
		{
			CurrencyType.RainbowCoins,
			"icon_currency_rainbowCoin"
		}
	};

	public CurrencyType currencyType;

	public TextMeshPro amountText;

	public tk2dSlicedSprite backing;

	public ParticleAttractor particleAttractor;

	[Tooltip("The rate in Coins per second at which the display amount is updated")]
	[HideInInspector]
	public int changeSpeed = 100;

	[Tooltip("Whether or not to display commas in large numbers")]
	public bool showCommas;

	[Tooltip("If TRUE, the display of this will update automatically as the player data's currency changes. If FALSE, all updates will need to be manual, except for the initial one.")]
	public bool autoUpdate = true;

	[Tooltip("If TRUE this will automatically show itself whenever the player's currency is updated, and then hide itself after an appropriate amount of time")]
	public bool autoShow;

	[Tooltip("If autoShow is enabled, this is the wait time after the display amount has reached its target before this will hide itself")]
	public float autoShowDelay = 1f;

	[Tooltip("Duration of the show and hide animations")]
	public float showHideDuration = 0.25f;

	private int currentDisplayTotal;

	private int targetDisplayTotal;

	private float autoShowTimer;

	private Tweener punchTween;

	[SerializeField]
	private bool _showBacking = true;

	private Player _player;

	public bool isShowing { get; private set; }

	public bool isAutoShowing { get; private set; }

	public bool ShowCommas
	{
		get
		{
			return showCommas;
		}
		set
		{
			showCommas = value;
			UpdateAmountText();
		}
	}

	private int Currency
	{
		get
		{
			if (player == null)
			{
				return 0;
			}
			CurrencyType currencyType = this.currencyType;
			if (currencyType == CurrencyType.RainbowCoins)
			{
				return player.rainbowCurrency;
			}
			return player.currency;
		}
	}

	public bool showBacking
	{
		get
		{
			return _showBacking;
		}
		set
		{
			_showBacking = value;
			backing.gameObject.SetActive(_showBacking);
		}
	}

	public Player player
	{
		get
		{
			return _player;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		Vector3 anchoredPosition3D = amountText.rectTransform.anchoredPosition3D;
		anchoredPosition3D.x = backing.dimensions.x - backing.transform.localPosition.x - transform.localPosition.x;
		amountText.rectTransform.anchoredPosition3D = anchoredPosition3D;
		backing.gameObject.SetActive(_showBacking);
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.CurrencyUpdatedType -= HandleCurrencyUpdated;
			_player = null;
		}
	}

	private void Update()
	{
		if (currentDisplayTotal != targetDisplayTotal)
		{
			int num = Mathf.FloorToInt(Mathf.Log10(Mathf.Abs(targetDisplayTotal - currentDisplayTotal)) + 1f);
			float num2 = Mathf.Max(100f, Mathf.Pow(10f, num));
			int num3 = Mathf.RoundToInt(num2 * Time.deltaTime * Mathf.Sign(targetDisplayTotal - currentDisplayTotal));
			currentDisplayTotal += num3;
			if (num3 > 0 && currentDisplayTotal >= targetDisplayTotal)
			{
				currentDisplayTotal = targetDisplayTotal;
			}
			else if (num3 < 0 && currentDisplayTotal <= targetDisplayTotal)
			{
				currentDisplayTotal = targetDisplayTotal;
			}
			UpdateAmountText();
		}
		else if (isAutoShowing)
		{
			autoShowTimer -= Time.deltaTime;
			if (autoShowTimer <= 0f)
			{
				Hide();
			}
		}
	}

	public void Init(Player player)
	{
		if (player != _player)
		{
			if (_player != null)
			{
				_player.CurrencyUpdatedType -= HandleCurrencyUpdated;
			}
			_player = player;
			if (_player != null)
			{
				_player.CurrencyUpdatedType += HandleCurrencyUpdated;
				currentDisplayTotal = (targetDisplayTotal = Currency);
				UpdateAmountText();
			}
		}
	}

	public void Show()
	{
		if (!isShowing)
		{
			isShowing = true;
			if (DOTween.IsTweening(amountText.transform))
			{
				amountText.transform.DOComplete();
			}
			amountText.transform.DOLocalMoveX(0f, showHideDuration).SetEase(Ease.OutBack);
		}
		else if (isAutoShowing)
		{
			isAutoShowing = false;
		}
	}

	public void Hide()
	{
		if (isShowing)
		{
			isShowing = false;
			isAutoShowing = false;
			if (currentDisplayTotal != targetDisplayTotal)
			{
				currentDisplayTotal = targetDisplayTotal;
				UpdateAmountText();
			}
			if (DOTween.IsTweening(amountText.transform))
			{
				amountText.transform.DOComplete();
			}
			amountText.transform.DOLocalMoveX(backing.dimensions.x - backing.transform.localPosition.x - transform.localPosition.x, showHideDuration).SetEase(Ease.InBack);
		}
	}

	public void SetToAutoHide()
	{
		if (isShowing)
		{
			isAutoShowing = true;
			autoShowTimer = autoShowDelay;
		}
	}

	public void InstantUpdate()
	{
		if (player != null)
		{
			currentDisplayTotal = (targetDisplayTotal = Currency);
			UpdateAmountText();
		}
	}

	public void BumpAmount(int amount)
	{
		Show();
		currentDisplayTotal = (targetDisplayTotal = currentDisplayTotal + amount);
		UpdateAmountText();
		PlayBump();
	}

	public void PlayBump()
	{
		if (punchTween != null)
		{
			punchTween.Kill(complete: true);
		}
		transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
		punchTween = transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBounce);
	}

	private void UpdateAmountText()
	{
		amountText.text = string.Format("{0}<sprite name=\"{1}\">", currentDisplayTotal.ToString((!showCommas) ? string.Empty : "n0"), currencySpriteNames[currencyType]);
	}

	private void HandleCurrencyUpdated(CurrencyType type, int newTotal)
	{
		if (!autoUpdate || type != currencyType)
		{
			return;
		}
		targetDisplayTotal = newTotal;
		if (autoShow)
		{
			if (!isShowing)
			{
				Show();
				isAutoShowing = true;
				autoShowTimer = autoShowDelay;
			}
			else if (isAutoShowing)
			{
				autoShowTimer = autoShowDelay;
			}
		}
	}
}
