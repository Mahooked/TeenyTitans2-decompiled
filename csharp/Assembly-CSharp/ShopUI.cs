using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{
	public static ShopUI instance;

	public TextMeshPro changeText;

	public TextMeshPro costText;

	public TextMeshPro msgText;

	public tk2dSprite darkSprite;

	public GameObject changeGroup;

	public GameObject homeButton;

	public GameObject retryButton;

	public GameObject checkoutButton;

	public GameObject buyButton;

	public BoxCollider buyButtonCollider;

	public GameObject receiptExitButton;

	public GameObject receiptGroup;

	public GameObject receiptContinueButton;

	public GameObject receiptContinueButtonBehind;

	public GameObject receiptTowerButton;

	public GameObject revealFigureNxtBtn;

	public GameObject revealFigureText;

	public TextMeshPro revealNewIndicatorAmtText;

	public tk2dSprite changeCoinSprite;

	public tk2dSprite changeTicketSprite;

	public tk2dSprite costCoinSprite;

	public tk2dSprite costTicketSprite;

	public Transform AnchorUpperCenter;

	public Transform missionFlagTarget;

	private Coroutine displayMessageRoutine;

	public ModChipDescriptionBox modchipReveal;

	public AccessoryReveal accessoryReveal;

	private bool freeFigureReveal;

	public CinemaBars cinemaBarTop;

	public CinemaBars cinemaBarBottom;

	public MissionFlag missionFlag;

	private IEnumerator revealCoroutine;

	public bool IsRevealing => revealCoroutine != null;

	public event Action ReceiptShown;

	private void Awake()
	{
		instance = this;
	}

	private void OnDisable()
	{
		revealCoroutine = null;
	}

	private void OnDestroy()
	{
		instance = null;
	}

	private void Start()
	{
		if (ShopController.isFlashSaleShop)
		{
			changeCoinSprite.gameObject.SetActive(value: false);
			costCoinSprite.gameObject.SetActive(value: false);
			changeTicketSprite.gameObject.SetActive(value: true);
			costTicketSprite.gameObject.SetActive(value: true);
		}
		else
		{
			changeCoinSprite.gameObject.SetActive(value: true);
			costCoinSprite.gameObject.SetActive(value: true);
			changeTicketSprite.gameObject.SetActive(value: false);
			costTicketSprite.gameObject.SetActive(value: false);
		}
	}

	public void UpdateCostText(string cost)
	{
		costText.text = cost;
		costText.ForceMeshUpdate();
		float num = 15f;
		float x = costText.bounds.size.x;
		costText.transform.localPosition = new Vector3(0f - num, costText.transform.localPosition.y, costText.transform.localPosition.z);
		Transform parent = costCoinSprite.transform.parent;
		parent.localPosition = new Vector3(costText.transform.localPosition.x + x / 2f + num, parent.localPosition.y, parent.localPosition.z);
	}

	public void DisplayMessage(string text, Color textColor)
	{
		if (displayMessageRoutine != null)
		{
			StopCoroutine(displayMessageRoutine);
		}
		displayMessageRoutine = StartCoroutine(DisplayMessageWithText(text, textColor));
	}

	private IEnumerator DisplayMessageWithText(string text, Color textColor)
	{
		msgText.gameObject.SetActive(value: true);
		msgText.text = text;
		msgText.color = textColor;
		yield return new WaitForSeconds(1f);
		msgText.gameObject.SetActive(value: false);
	}

	public void DisplayCheckOutButton()
	{
		checkoutButton.transform.DOLocalMove(new Vector3(-54f, 30f, 0f), 0.3f).SetEase(Ease.OutSine);
	}

	public void HideCheckOutButton()
	{
		checkoutButton.transform.DOLocalMove(new Vector3(-54f, -100f, 0f), 0.3f).SetEase(Ease.OutSine);
	}

	public void DisplayBuyButton()
	{
		buyButton.SetActive(value: true);
		UpdateCostText(costText.text);
		StopCoroutine("HideButtonRoutine");
		buyButton.GetComponent<Animator>().Play("ShopBuyButton_Enable", 0, 0f);
		buyButtonCollider.enabled = true;
	}

	public void HideBuyButton()
	{
		buyButtonCollider.enabled = false;
		StopCoroutine("HideButtonRoutine");
		StartCoroutine("HideButtonRoutine");
	}

	private IEnumerator HideButtonRoutine()
	{
		buyButton.GetComponent<Animator>().Play("ShopBuyButton_Disable", 0, 0f);
		yield return new WaitForEndOfFrame();
		while (buyButton.GetComponent<Animator>().NormalizedTime() < 0.9f)
		{
			yield return new WaitForEndOfFrame();
		}
		buyButton.SetActive(value: false);
	}

	private void CheckOut()
	{
		if (receiptGroup.activeSelf)
		{
			return;
		}
		if (CoreGameTutorial.instance != null)
		{
			CoreGameTutorial.instance.CheckOutButtonPressed();
		}
		if (ShopController.instance.HasNewFiguresInCart())
		{
			RevealNext();
			return;
		}
		List<ShopToyBox> list = ShopController.instance.FiguresInCart();
		for (int i = 0; i < list.Count; i++)
		{
			ShopToyBox shopToyBox = list[i];
			MissionTypeConfigBuy missionBuyConfig = shopToyBox.GetMissionBuyConfig();
			if (missionBuyConfig != null && !missionBuyConfig.mintInBox)
			{
				if (missionBuyConfig.toyType == ToyType.Figure)
				{
					Character character = new Character(shopToyBox.charDef.uid);
					character.Init(CharacterRoller.RollCharacterData(GameManager.instance.characterDatabase.GetCharacter(shopToyBox.charDef.uid), character.uid, shopToyBox.charDef.uid, 1));
					missionFlag.CheckCharacterAdded(character);
				}
				else if (missionBuyConfig.toyType == ToyType.ModChip)
				{
					missionFlag.CheckModChipAdded(shopToyBox.modchipSelectPortrait.modChip);
				}
			}
		}
		if (!freeFigureReveal)
		{
			if (!ShopController.isFlashSaleShop)
			{
				Player.instance.RemoveCurrency(ShopController.instance.TotalInCart());
			}
			else
			{
				Player.instance.RemoveCurrencyTickets(ShopController.instance.TotalInCart());
			}
		}
		freeFigureReveal = false;
		if (ShopController.instance.FiguresInCart().Count > 0)
		{
			ShowReceipt();
			return;
		}
		ContinueShoping();
		HideCheckOutButton();
		homeButton.SetActive(value: true);
		retryButton.SetActive(value: true);
	}

	public void CheckOutTutorial()
	{
		RevealNext();
		if (Player.instance.characters.Count == 0)
		{
			CharacterDefinition characterDefinition = null;
			if (CityCharacter.characterType == CityCharacterType.Robin)
			{
				characterDefinition = GameManager.instance.characterDatabase.GetCharacter(93);
			}
			if (CityCharacter.characterType == CityCharacterType.BeastBoy)
			{
				characterDefinition = GameManager.instance.characterDatabase.GetCharacter(94);
			}
			if (CityCharacter.characterType == CityCharacterType.Raven)
			{
				characterDefinition = GameManager.instance.characterDatabase.GetCharacter(98);
			}
			if (CityCharacter.characterType == CityCharacterType.Starfire)
			{
				characterDefinition = GameManager.instance.characterDatabase.GetCharacter(99);
			}
			if (CityCharacter.characterType == CityCharacterType.Cyborg)
			{
				characterDefinition = GameManager.instance.characterDatabase.GetCharacter(97);
			}
			if (characterDefinition != null)
			{
				Player.instance.AddCharacter(characterDefinition);
				Player.instance.AcquireCharacter(characterDefinition.uid);
				Player.instance.AddToParty(Player.instance.characters.GetLastItem());
			}
		}
		Player.instance.Save();
	}

	private void ShowReceipt()
	{
		HideCheckOutButton();
		homeButton.SetActive(value: false);
		retryButton.SetActive(value: false);
		darkSprite.gameObject.SetActive(value: true);
		darkSprite.color = new Color(0f, 0f, 0f, 0f);
		DOTween.To(() => darkSprite.color, (Color x) =>
		{
			darkSprite.color = x;
		}, new Color(0f, 0f, 0f, 0.45f), 0.3f).SetEase(Ease.OutBack);
		receiptGroup.transform.localEulerAngles = new Vector3(0f, 0f, 3f);
		receiptGroup.GetComponent<ShopReceipt>().BuildReceiptWithFigures();
		receiptGroup.transform.localPosition = new Vector3(90f, -600f, 10f);
		receiptGroup.transform.DOLocalMove(new Vector3(0f, 0f, 10f), 0.2f).SetEase(Ease.OutSine);
		receiptGroup.SetActive(value: true);
		receiptExitButton.SetActive(value: true);
		bool flag = Player.instance.totalNewCharacters > 0;
		revealNewIndicatorAmtText.transform.parent.gameObject.SetActive(flag);
		if (flag)
		{
			revealNewIndicatorAmtText.transform.parent.transform.localScale = new Vector3(0f, 0f, 0f);
			revealNewIndicatorAmtText.transform.parent.transform.DOScale(1f, 0.15f).OnComplete(() =>
			{
				revealNewIndicatorAmtText.transform.parent.DOPunchScale(new Vector3(1f, 1f, 1f), 0.2f);
			}).SetDelay(0.2f);
			revealNewIndicatorAmtText.text = Player.instance.totalNewCharacters.ToString();
		}
		if (ReceiptShown != null)
		{
			ReceiptShown();
		}
	}

	private void DismissReceiptBehindButton()
	{
		if (!(CoreGameTutorial.instance != null) || CoreGameTutorial.instance.currentStep != CoreGameTutorial.Step.ShopTutorial2Finished)
		{
			ContinueShoping();
		}
	}

	private void ContinueShoping()
	{
		revealNewIndicatorAmtText.transform.parent.gameObject.SetActive(value: false);
		ShopController.instance.ClearItemsPurchased();
		receiptGroup.transform.DOLocalMove(new Vector3(90f, -600f, 10f), 0.2f).SetEase(Ease.OutSine).OnComplete(() =>
		{
			receiptGroup.SetActive(value: false);
		});
		DOTween.To(() => darkSprite.color, (Color x) =>
		{
			darkSprite.color = x;
		}, new Color(0f, 0f, 0f, 0f), 0.3f).SetEase(Ease.OutBack).OnComplete(() =>
		{
			darkSprite.gameObject.SetActive(value: false);
		});
		receiptExitButton.SetActive(value: false);
		homeButton.SetActive(value: true);
		retryButton.SetActive(value: true);
	}

	private void RevealNextButton()
	{
		if (CoreGameTutorial.instance != null)
		{
			if (CoreGameTutorial.instance.CheckRevealButtonNext())
			{
				revealFigureNxtBtn.SetActive(value: false);
				changeGroup.SetActive(value: true);
				if (ShopController.instance.figureReveal.gameObject.activeSelf)
				{
					ShopController.instance.figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
					ShopController.instance.figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Outro");
				}
				StartCoroutine(DeactivateRevealScreen(checkOut: false));
			}
			else
			{
				RevealNext();
			}
		}
		else
		{
			RevealNext();
		}
	}

	private void RevealNext()
	{
		Debug.Log("Reveal Next...");
		ShopToyBox shopToyBox = ShopController.instance.NextFigureToReveal();
		if (shopToyBox != null)
		{
			revealFigureNxtBtn.SetActive(value: false);
			homeButton.SetActive(value: false);
			retryButton.SetActive(value: false);
			changeGroup.SetActive(value: false);
			HideCheckOutButton();
			revealCoroutine = RevealNextFigure(shopToyBox.blindBox);
			StartCoroutine(revealCoroutine);
		}
		else
		{
			revealFigureNxtBtn.SetActive(value: false);
			changeGroup.SetActive(value: true);
			if (ShopController.instance.figureReveal.gameObject.activeSelf)
			{
				ShopController.instance.figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
				ShopController.instance.figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Outro");
			}
			StartCoroutine(DeactivateRevealScreen());
		}
	}

	private IEnumerator DeactivateRevealScreen(bool checkOut = true)
	{
		yield return new WaitForSeconds(1f);
		if (checkOut)
		{
			CheckOut();
		}
		ShopController.instance.figureReveal.gameObject.SetActive(value: false);
	}

	private IEnumerator RevealNextFigure(bool forceNewFigure = false)
	{
		ShopToyBox nextToReveal = ShopController.instance.NextFigureToReveal();
		ShopFigureReveal figureReveal = ShopController.instance.figureReveal;
		bool firstReveal = !figureReveal.gameObject.activeSelf;
		MissionTypeConfigBuy missionBuyConfig = nextToReveal.GetMissionBuyConfig();
		bool isMissionFigure = missionBuyConfig != null;
		figureReveal.transform.position = ShopCart.instance.gameObject.transform.position;
		if (nextToReveal.toyType == ToyType.ModChip)
		{
			if (!firstReveal)
			{
				figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
				yield return new WaitForEndOfFrame();
				while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
				{
					yield return new WaitForEndOfFrame();
				}
			}
			nextToReveal.alreadyRevealed = true;
			nextToReveal.gameObject.SetActive(value: false);
			modchipReveal.gameObject.SetActive(value: true);
			modchipReveal.Init(nextToReveal.modchipSelectPortrait.modChip);
			modchipReveal.animator.Play("ModChip_Reveal_Intro", 0, 0f);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_modchip_reveal", GFAudioManager.SFX_GROUP, string.Empty);
			yield return new WaitForEndOfFrame();
			if (isMissionFigure)
			{
				if (missionBuyConfig.mintInBox)
				{
					yield return new WaitForSeconds(1f);
					PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_MissionFigureWindow", default, isActive: false);
					dialog.transform.parent = transform;
					dialog.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
					dialog.transform.localPosition = new Vector3(0f, 0f, -300f);
					dialog.headerText.text = Language.Get("HOLDIT", "MENUS,GUI");
					dialog.descriptionText.text = Language.Get("MISSIONMINTBOX", "MENUS,GUI");
					dialog.gameObject.SetActive(value: true);
					yield return new WaitForSeconds(1f);
					while (dialog.gameObject.activeSelf)
					{
						yield return null;
					}
					if (dialog.isConfirmed)
					{
						GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_bean_collect", GFAudioManager.SFX_GROUP, string.Empty);
						missionFlag.CheckModChipAdded(nextToReveal.modchipSelectPortrait.modChip);
						revealFigureNxtBtn.SetActive(value: false);
						nextToReveal.inCart = false;
						ShopToyBox shopToyBox = nextToReveal;
						Player.instance.AddFigureToPurchaseData(ShopController.inventory.name, shopToyBox.hangerRack + "-" + shopToyBox.hangerNumber + "-" + shopToyBox.hangerSpot);
						Player.instance.RemoveCurrency(shopToyBox.Cost());
						ObjectPool.PoolObject(dialog.gameObject);
					}
					else
					{
						ObjectPool.PoolObject(dialog.gameObject);
						yield return new WaitForTap();
					}
				}
				else
				{
					missionFlag.CheckModChipAdded(nextToReveal.modchipSelectPortrait.modChip);
				}
			}
			else
			{
				yield return new WaitForTap();
			}
			modchipReveal.animator.Play("ModChip_Reveal_Outro", 0, 0f);
			yield return new WaitForEndOfFrame();
			while (modchipReveal.animator.NormalizedTime() < 0.7f)
			{
				yield return new WaitForEndOfFrame();
			}
			modchipReveal.gameObject.SetActive(value: false);
			revealCoroutine = null;
			RevealNext();
			yield break;
		}
		if (nextToReveal.toyType == ToyType.Accessory)
		{
			Debug.Log("Reveal Accessory");
			if (!firstReveal)
			{
				figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
				yield return new WaitForEndOfFrame();
				while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
				{
					yield return new WaitForEndOfFrame();
				}
			}
			nextToReveal.alreadyRevealed = true;
			nextToReveal.gameObject.SetActive(value: false);
			accessoryReveal.gameObject.SetActive(value: true);
			accessoryReveal.Init(nextToReveal.accessoryItemDefinition);
			accessoryReveal.animator.Play("ModChip_Reveal_Intro", 0, 0f);
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_modchip_reveal", GFAudioManager.SFX_GROUP, string.Empty);
			yield return new WaitForEndOfFrame();
			yield return new WaitForTap();
			Debug.Log("Reveal Accessory, Outro");
			accessoryReveal.animator.Play("ModChip_Reveal_Outro", 0, 0f);
			yield return new WaitForEndOfFrame();
			while (accessoryReveal.animator.NormalizedTime() < 0.7f)
			{
				yield return new WaitForEndOfFrame();
			}
			Debug.Log("Reveal Accessory, Finished");
			accessoryReveal.gameObject.SetActive(value: false);
			revealCoroutine = null;
			RevealNext();
			yield break;
		}
		yield return new WaitForEndOfFrame();
		figureReveal.transform.position = ShopCart.instance.gameObject.transform.position;
		if (!firstReveal)
		{
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_outro");
			yield return new WaitForEndOfFrame();
			while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		figureReveal.figureRevealBoxGroupAnimator.Play("empty");
		bool isNewFigure = nextToReveal.IsNewFigure();
		bool showNewTag = ((nextToReveal.blindBox && isNewFigure) || (isMissionFigure && isNewFigure) || !isMissionFigure) && !nextToReveal.blindBox;
		figureReveal.newFigureBacking.SetActive(showNewTag);
		figureReveal.isGoldBox = nextToReveal.isGoldBox;
		if (nextToReveal.blindBox)
		{
			figureReveal.MakeBlindBox();
		}
		else
		{
			figureReveal.RemoveBlindBox();
		}
		figureReveal.figureRevealToyBox.figureData = nextToReveal.figureData;
		figureReveal.figureRevealToyBox.UpdateBoxForCharacter(nextToReveal.charDef, newFigureReveal: true);
		nextToReveal.previouslyBlindBox = nextToReveal.blindBox;
		nextToReveal.blindBox = false;
		if (CoreGameTutorial.instance == null || CoreGameTutorial.instance.currentStep != CoreGameTutorial.Step.ShopTutorialPart7)
		{
			Player.instance.AcquireCharacter(nextToReveal.figureData.characterUID);
		}
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.SetActive(value: false);
		}
		yield return new WaitForSeconds(0.2f);
		if (firstReveal)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_firstfigappear", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_subsequentfigappear", GFAudioManager.SFX_GROUP, string.Empty);
		}
		nextToReveal.gameObject.SetActive(value: false);
		figureReveal.gameObject.SetActive(value: true);
		figureReveal.figuresDiscoveredAmt.transform.parent.gameObject.SetActive(isNewFigure);
		figureReveal.figuresDiscoveredAmt.text = $"<color=\"orange\">{Mathf.Max(0, Player.instance.charactersAcquired.Count - 1)}<color=\"white\">/{GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance)}";
		figureReveal.figuredRevealName.text = ((!string.IsNullOrEmpty(nextToReveal.charDef.characterNameKey)) ? Language.Get(nextToReveal.charDef.characterNameKey, "NPCS,FIGS") : nextToReveal.charDef.characterName);
		Vector3 amountPosition = figureReveal.figuresDiscoveredAmt.transform.position;
		amountPosition.x = GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0f, 0f)).x;
		figureReveal.figuresDiscoveredAmt.transform.parent.position = amountPosition;
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.transform.DOKill();
			figureReveal.figureRevealToyBox.figure.transform.localEulerAngles = new Vector3(nextToReveal.charDef.shopFigureRotationOffset.x, 180f + nextToReveal.charDef.shopFigureRotationOffset.y, nextToReveal.charDef.shopFigureRotationOffset.z);
		}
		if (!firstReveal)
		{
			while (figureReveal.figureRevealAnimator.IsName("NewFigureReveal_Fig_Outro") && figureReveal.figureRevealAnimator.NormalizedTime() < 0.9f)
			{
				yield return new WaitForEndOfFrame();
			}
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Intro");
		}
		else
		{
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Intro");
			figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Intro");
		}
		yield return new WaitForSeconds(0.1f);
		if (figureReveal.figureRevealToyBox.figure != null)
		{
			figureReveal.figureRevealToyBox.figure.SetActive(value: true);
		}
		float lastTimeWigglePlayed = Time.time;
		bool openBox = true;
		if (isMissionFigure)
		{
			if (missionBuyConfig.mintInBox)
			{
				yield return new WaitForSeconds(1f);
				PopupDialog dialog2 = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_MissionFigureWindow", default, isActive: false);
				dialog2.transform.parent = transform;
				dialog2.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
				dialog2.transform.localPosition = new Vector3(0f, 0f, -300f);
				dialog2.headerText.text = Language.Get("HOLDIT", "MENUS,GUI");
				dialog2.descriptionText.text = Language.Get("MISSIONMINTBOX", "MENUS,GUI");
				dialog2.gameObject.SetActive(value: true);
				yield return new WaitForSeconds(1f);
				while (dialog2.gameObject.activeSelf)
				{
					yield return null;
				}
				if (dialog2.isConfirmed)
				{
					openBox = false;
					GameObject figureBoxCopy = UnityEngine.Object.Instantiate(figureReveal.figureRevealToyBox.gameObject);
					figureReveal.figureRevealScreenAnimator.Play("NewFigureReveal_Outro");
					figureBoxCopy.transform.position = figureReveal.figureRevealToyBox.transform.position;
					figureBoxCopy.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI"));
					figureBoxCopy.transform.position = GetComponent<Camera>().ScreenToWorldPoint(ShopCart.instance.camera.WorldToScreenPoint(figureReveal.figureRevealToyBox.transform.position));
					figureBoxCopy.transform.localScale = new Vector3(150f, 150f, 150f);
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_bean_collect", GFAudioManager.SFX_GROUP, string.Empty);
					figureBoxCopy.transform.DOMove(missionFlagTarget.position, 0.5f).SetEase(Ease.OutSine).OnComplete(() =>
					{
						figureBoxCopy.gameObject.SetActive(value: false);
					});
					figureReveal.figureRevealAnimator.transform.localScale = Vector3.zero;
					figureBoxCopy.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.OutSine);
					Character tempCharacter = new Character(nextToReveal.charDef.uid);
					tempCharacter.Init(CharacterRoller.RollCharacterData(GameManager.instance.characterDatabase.GetCharacter(nextToReveal.charDef.uid), tempCharacter.uid, nextToReveal.charDef.uid, 1));
					missionFlag.CheckCharacterAdded(tempCharacter);
					revealFigureNxtBtn.SetActive(value: false);
					while (figureBoxCopy.gameObject.activeSelf)
					{
						figureReveal.figureRevealToyBox.gameObject.SetActive(value: false);
						yield return null;
					}
					yield return new WaitForSeconds(2f);
					figureReveal.gameObject.SetActive(value: false);
					figureReveal.figureRevealAnimator.transform.localScale = Vector3.one;
					nextToReveal.inCart = false;
					ShopToyBox toyBox = nextToReveal.GetComponent<ShopToyBox>();
					Player.instance.AddFigureToPurchaseData(ShopController.inventory.name, toyBox.hangerRack + "-" + toyBox.hangerNumber + "-" + toyBox.hangerSpot);
					Player.instance.RemoveCurrency(toyBox.Cost());
					RevealNextButton();
				}
			}
			else
			{
				Character character = new Character(nextToReveal.charDef.uid);
				character.Init(CharacterRoller.RollCharacterData(GameManager.instance.characterDatabase.GetCharacter(nextToReveal.charDef.uid), character.uid, nextToReveal.charDef.uid, 1));
				missionFlag.CheckCharacterAdded(character);
			}
		}
		if (openBox)
		{
			figureReveal.waitToOpenBox = true;
			while (figureReveal.waitToOpenBox)
			{
				yield return new WaitForEndOfFrame();
			}
			figureReveal.figureRevealAnimator.Play("NewFigureReveal_Fig_Tap");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_open", GFAudioManager.SFX_GROUP, string.Empty);
			yield return new WaitForSeconds(1.6f);
			if (!string.IsNullOrEmpty(nextToReveal.charDef.shopRevealNameSoundPath))
			{
				GFAudioManager.instance.PlayClipFromResources(nextToReveal.charDef.shopRevealNameSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
			}
			yield return new WaitForSeconds(0.6f);
			revealFigureNxtBtn.SetActive(value: true);
			figureReveal.figuresDiscoveredAmt.text = $"<color=\"orange\">{Player.instance.charactersAcquired.Count}<color=\"white\">/{GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance)}";
			figureReveal.figureRevealToyBox.figure.transform.DOLocalRotate(new Vector3(0f, 900f, 0f), 25f, RotateMode.FastBeyond360);
		}
		revealCoroutine = null;
	}

	public void RevealFreeFigure()
	{
		ShopToyBox freeFigureToReveal = ShopController.instance.GetFreeFigureToReveal();
		freeFigureReveal = true;
		if (freeFigureToReveal != null)
		{
			revealFigureNxtBtn.SetActive(value: false);
			homeButton.SetActive(value: false);
			retryButton.SetActive(value: false);
			changeGroup.SetActive(value: false);
			HideCheckOutButton();
			revealCoroutine = RevealNextFigure();
			StartCoroutine(revealCoroutine);
		}
	}

	public void ShowCinemaBars(float t)
	{
		cinemaBarTop.ShowCinemaBars(t);
		cinemaBarBottom.ShowCinemaBars(t);
	}

	public void HideCinemaBars(float t)
	{
		cinemaBarTop.HideCinemaBars(t);
		cinemaBarBottom.HideCinemaBars(t);
	}

	private void CheckOutButtonDown()
	{
		ShopController.instance.initialTouchOnButton = true;
	}

	private void CheckOutButtonUp()
	{
		ShopController.instance.initialTouchOnButton = false;
	}

	private void CartArrowPressedDown()
	{
		ShopController.instance.initialTouchOnButton = true;
	}

	private void CartArrowPressedUp()
	{
		ShopController.instance.initialTouchOnButton = false;
	}

	private void CartArrowPressed()
	{
		ShopController.instance.CartArrowPressed();
	}

	private void RestartShopScene()
	{
		ShopController.fromRetry = true;
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
	}

	private void ExitToTitansTower()
	{
		ShopController.newsFlashUID = -1;
		Player.instance.RemoveCurrencyTickets(Player.instance.currencyTickets);
		LoadingScreenCover.LoadScene("TitanTower");
	}

	private void ExitShop()
	{
		ShopController.newsFlashUID = -1;
		Player.instance.RemoveCurrencyTickets(Player.instance.currencyTickets);
		if (!string.IsNullOrEmpty(LoadingScreenCover.lastScene))
		{
			LoadingScreenCover.LoadScene(LoadingScreenCover.lastScene);
		}
		else
		{
			LoadingScreenCover.LoadScene(CityController.previousCityScene);
		}
	}
}
