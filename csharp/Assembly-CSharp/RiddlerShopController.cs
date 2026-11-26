using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using Crosstales.OnlineCheck;
using UnityEngine;
using UnityEngine.Analytics.Experimental;
using UnityEngine.Purchasing;

public class RiddlerShopController : MonoBehaviour
{
	public tk2dUIItem backButton;

	public RiddlerShopMainMenu mainMenu;

	public RiddlerShopProductWindow riddleEggsWindow;

	public RiddlerShopProductWindow repaintTokensWindow;

	public RiddlerShopProductWindow figuresWindow;

	public RiddlerShopBaseWindow oddsGreenEggWindow;

	public RiddlerShopBaseWindow oddsGoldenEggWindow;

	public CurrencyGUI currencyGUI;

	public tk2dBaseSprite coverSprite;

	[Space]
	public IAPConfiguration iapConfig;

	private bool initialInternetCheckComplete;

	private RiddlerShopProductWindow activeProductWindow;

	private RiddlerShopBaseWindow activeOddsWindow;

	private bool isExiting;

	private IAPProduct purchasingProduct;

	private void Awake()
	{
		mainMenu.gameObject.SetActive(value: false);
		riddleEggsWindow.gameObject.SetActive(value: false);
		repaintTokensWindow.gameObject.SetActive(value: false);
		figuresWindow.gameObject.SetActive(value: false);
		oddsGreenEggWindow.gameObject.SetActive(value: false);
		oddsGoldenEggWindow.gameObject.SetActive(value: false);
		coverSprite.gameObject.SetActive(value: false);
		if (iapConfig == null)
		{
			iapConfig = GameManager.instance.iapConfig;
		}
	}

	private void OnEnable()
	{
		backButton.OnClick += BackButton_OnClick;
		OnlineCheck.OnOnlineStatusChange += OnlineCheck_OnlineStatusChange;
		OnlineCheck.OnOnlineCheckComplete += OnlineCheck_OnlineCheckComplete;
	}

	private void OnDisable()
	{
		backButton.OnClick -= BackButton_OnClick;
		OnlineCheck.OnOnlineStatusChange -= OnlineCheck_OnlineStatusChange;
		OnlineCheck.OnOnlineCheckComplete -= OnlineCheck_OnlineCheckComplete;
	}

	[DebuggerHidden]
	private IEnumerator Start()
	{
		// ILSpy could not decompile this member: Error decompiling @060015E2 RiddlerShopController.Start: Value cannot be null. (Parameter 'source')
		// Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// Error decompiling @060015E2 RiddlerShopController.Start
		// in assembly "C:\AIC\TeenyTitans2-decompiled\assets\bin\Data\Managed\Assembly-CSharp.dll"
		//  ---> System.ArgumentNullException: Value cannot be null. (Parameter 'source')
		//    at System.Linq.ThrowHelper.ThrowArgumentNullException(ExceptionArgument) + 0x31
		//    at System.Linq.Enumerable.Take[TSource](IEnumerable`1, Int32) + 0x48
		//    at ICSharpCode.Decompiler.CSharp.ArgumentList.GetArgumentResolveResultsDirect(Int32) + 0x24
		//    at ICSharpCode.Decompiler.CSharp.Disambiguator.Probe() + 0xc8
		//    at ICSharpCode.Decompiler.CSharp.Disambiguator.Run() + 0x10
		//    at ICSharpCode.Decompiler.CSharp.Disambiguator.ForMethodReference(ExpressionBuilder, IMethod, IType, TranslatedExpression, Boolean, ExpectedTargetDetails, Boolean, Boolean) + 0x103
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.DisambiguateDelegateReference(IMethod, IMethod, ExpectedTargetDetails, ILInstruction) + 0x25f
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildDelegateReference(IMethod, IMethod, ExpectedTargetDetails, ILInstruction) + 0x53
		//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitLdFtn(LdFtn, TranslationContext) + 0x98
		//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildArgumentList(ExpectedTargetDetails, ResolveResult, IMethod, Int32, IReadOnlyList`1, IReadOnlyList`1, Boolean) + 0x38f
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.HandleDelegateConstruction(CallInstruction) + 0x139
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(CallInstruction, IType) + 0x9f
		//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitNewObj(NewObj, TranslationContext) + 0x150
		//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildArgumentList(ExpectedTargetDetails, ResolveResult, IMethod, Int32, IReadOnlyList`1, IReadOnlyList`1, Boolean) + 0x38f
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(OpCode, IMethod, IReadOnlyList`1, IReadOnlyList`1, IType) + 0x4e7
		//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(CallInstruction, IType) + 0x12f
		//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitNewObj(NewObj, TranslationContext) + 0x150
		//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitYieldReturn(YieldReturn) + 0x63
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlock(Block) + 0x51
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitIfInstruction(IfInstruction) + 0x3d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockStatement, BlockContainer, IEnumerable`1, Boolean) + 0x26b
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockContainer, Boolean) + 0x47
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlockContainer(BlockContainer) + 0x94
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertAsBlock(ILInstruction) + 0x11
		//    at ICSharpCode.Decompiler.CSharp.CSharpDecompiler.DecompileBody(IMethod, EntityDeclaration, DecompileRun, ITypeResolveContext, ExtensionInfo) + 0x426
		// -- continuing with outer exception (ICSharpCode.Decompiler.DecompilerException) --
		// 
	}

	private void OnDestroy()
	{
		IAPStore.Instance.Initialized -= IAPStore_Instance_Initialized;
		IAPStore.Instance.InitializeFailed -= IAPStore_Instance_InitializeFailed;
		IAPStore.Instance.PurchaseCompleted -= IAPStore_Instance_PurchaseCompleted;
		IAPStore.Instance.PurchaseFailed -= IAPStore_Instance_PurchaseFailed;
		IAPStore.Instance.ReceiptInvalid -= IAPStore_Instance_ReceiptInvalid;
		OnlineCheck.isEndlessMode = false;
		RemoteSettings.Updated -= RemoteSettingsOnUpdated;
	}

	private void OnlineCheck_OnlineStatusChange(bool isConnected)
	{
	}

	private void OnlineCheck_OnlineCheckComplete(bool isConnected, NetworkReachability networkReachability)
	{
		if (!initialInternetCheckComplete)
		{
			initialInternetCheckComplete = true;
		}
	}

	private void RemoteSettingsOnUpdated()
	{
		if (iapConfig.figureCampaigns.Length > 0)
		{
			int num = 0;
			if (RemoteSettings.HasKey("figure_campaign_index"))
			{
				num = RemoteSettings.GetInt("figure_campaign_index", 0);
				if (num < 0)
				{
					num = 0;
				}
				else if (num >= iapConfig.figureCampaigns.Length)
				{
					num = iapConfig.figureCampaigns.Length - 1;
				}
			}
			IAPFigureCampaign iAPFigureCampaign = iapConfig.figureCampaigns[num];
			UnityEngine.Debug.LogFormat("Using Campaign with Index {0} and Campaign Type of {1}", num, iAPFigureCampaign.campaignType);
			IAPFigureCampaign.FigureSet figureSet = null;
			int numDaysPerCycle = iAPFigureCampaign.GetNumDaysPerCycle();
			if (iAPFigureCampaign.campaignType == IAPFigureCampaign.CampaignType.Fixed || numDaysPerCycle <= 0)
			{
				figureSet = iAPFigureCampaign.figureSets[0];
			}
			else
			{
				int num2 = iAPFigureCampaign.figureSets.Length * numDaysPerCycle;
				int num3 = 0;
				DateTime dateTime;
				try
				{
					dateTime = ((!RemoteSettings.HasKey("figure_campaign_start_date")) ? DateTime.Now : DateTime.ParseExact(RemoteSettings.GetString("figure_campaign_start_date"), "d", CultureInfo.InvariantCulture));
				}
				catch (Exception)
				{
					dateTime = DateTime.Now;
				}
				DateTime now = DateTime.Now;
				TimeSpan timeSpan = now.Subtract(dateTime);
				UnityEngine.Debug.LogFormat("Campaign Start Date: {0}    Current Date: {1}", dateTime, now);
				if (timeSpan.Days != 0)
				{
					int num4 = ((timeSpan.Days <= 0) ? (-timeSpan.Days + 1) : timeSpan.Days);
					int num5 = ((timeSpan.Days <= 0) ? (num2 - num4 % num2) : (num4 % num2));
					num3 = Mathf.FloorToInt((float)num5 / (float)numDaysPerCycle);
					UnityEngine.Debug.LogFormat("Days since Start: {0}    Days Per Cycle: {1}    Total Days for Campaign: {2}    Days into Campaign: {3}    Campaign Index: {4}", num4, numDaysPerCycle, num2, num5, num3);
				}
				figureSet = iAPFigureCampaign.figureSets[num3];
			}
			if (figureSet != null)
			{
				IAPProduct[] array = new IAPProduct[figureSet.figures.Length];
				CharacterDefinition characterDefinition = null;
				for (int i = 0; i < figureSet.figures.Length; i++)
				{
					characterDefinition = GameManager.instance.characterDatabase.GetCharacter(figureSet.figures[i].characterUID);
					PlayerReward playerReward = new PlayerReward();
					playerReward.type = PlayerRewardType.Figure;
					playerReward.figure = new CharacterRewardData(characterDefinition.uid);
					playerReward.figure.characterLvl = 5;
					array[i] = new IAPProduct(figureSet.figures[i].productID, new LocalizedString(characterDefinition.characterName, characterDefinition.characterNameKey), new PlayerReward[1] { playerReward });
				}
				figuresWindow.Init(array);
			}
		}
		else
		{
			UnityEngine.Debug.LogError("There are no campaigns set up in the IAP Config, so not initializing the Rare Figs window.");
		}
	}

	private void ExitShop()
	{
		if (!isExiting)
		{
			if (!string.IsNullOrEmpty(LoadingScreenCover.lastScene))
			{
				LoadingScreenCover.LoadScene(LoadingScreenCover.lastScene);
			}
			else
			{
				LoadingScreenCover.LoadScene(CityController.previousCityScene);
			}
			isExiting = true;
		}
	}

	private void ShowMainMenu()
	{
		mainMenu.gameObject.SetActive(value: true);
		backButton.gameObject.SetActive(value: true);
		mainMenu.riddleEggsButton.uiItem.OnClick += RiddleEggsButton_uiItem_OnClick;
		mainMenu.repaintTokensButton.uiItem.OnClick += RepaintTokensButton_uiItem_OnClick;
		mainMenu.figuresButton.uiItem.OnClick += FiguresButton_uiItem_OnClick;
	}

	private void HideMainMenu()
	{
		mainMenu.riddleEggsButton.uiItem.OnClick -= RiddleEggsButton_uiItem_OnClick;
		mainMenu.repaintTokensButton.uiItem.OnClick -= RepaintTokensButton_uiItem_OnClick;
		mainMenu.figuresButton.uiItem.OnClick -= FiguresButton_uiItem_OnClick;
		mainMenu.gameObject.SetActive(value: false);
		backButton.gameObject.SetActive(value: false);
	}

	private void ShowRiddleEggsWindow()
	{
		riddleEggsWindow.closeButton.OnClick += RiddleEggsWindow_closeButton_OnClick;
		riddleEggsWindow.ProductSelected += RiddleEggsWindow_ProductSelected;
		riddleEggsWindow.HelpRequested += RiddleEggsWindow_HelpRequested;
		riddleEggsWindow.Open();
	}

	private void HideRiddleEggsWindow()
	{
		riddleEggsWindow.closeButton.OnClick -= RiddleEggsWindow_closeButton_OnClick;
		riddleEggsWindow.ProductSelected -= RiddleEggsWindow_ProductSelected;
		riddleEggsWindow.HelpRequested -= RiddleEggsWindow_HelpRequested;
		riddleEggsWindow.Close();
	}

	private void RiddleEggsWindow_closeButton_OnClick()
	{
		HideRiddleEggsWindow();
		activeOddsWindow = null;
		ShowMainMenu();
	}

	private void RiddleEggsWindow_ProductSelected(IAPProduct product)
	{
		if (!string.IsNullOrEmpty(product.id) && purchasingProduct == null)
		{
			UnityEngine.Debug.LogFormat("Try purchasing {0} with ID \"{1}\"", product.name, product.ProductID);
			coverSprite.gameObject.SetActive(value: true);
			tk2dUIManager.Instance.InputEnabled = false;
			purchasingProduct = product;
			IAPStore.Instance.Controller.InitiatePurchase(IAPStore.Instance.Controller.products.WithID(product.ProductID));
		}
	}

	private void RiddleEggsWindow_HelpRequested(IAPProduct product)
	{
		HideRiddleEggsWindow();
		ShowOddsWindow(product.name.text.Equals("Golden Riddle Eggs") ? RiddleEggType.Gold : RiddleEggType.Green);
	}

	private void ShowRepaintTokensWindow()
	{
		repaintTokensWindow.closeButton.OnClick += RepaintTokensWindow_closeButton_OnClick;
		repaintTokensWindow.ProductSelected += RepaintTokensWindow_ProductSelected;
		repaintTokensWindow.Open();
	}

	private void HideRepaintTokensWindow()
	{
		repaintTokensWindow.closeButton.OnClick -= RepaintTokensWindow_closeButton_OnClick;
		repaintTokensWindow.ProductSelected -= RepaintTokensWindow_ProductSelected;
		repaintTokensWindow.Close();
	}

	private void RepaintTokensWindow_closeButton_OnClick()
	{
		HideRepaintTokensWindow();
		ShowMainMenu();
	}

	private void RepaintTokensWindow_ProductSelected(IAPProduct product)
	{
		if (!string.IsNullOrEmpty(product.id) && purchasingProduct == null)
		{
			UnityEngine.Debug.LogFormat("Try purchasing {0} with ID \"{1}\"", product.name, product.ProductID);
			coverSprite.gameObject.SetActive(value: true);
			tk2dUIManager.Instance.InputEnabled = false;
			purchasingProduct = product;
			IAPStore.Instance.Controller.InitiatePurchase(IAPStore.Instance.Controller.products.WithID(product.ProductID));
		}
	}

	private void ShowFiguresWindow()
	{
		figuresWindow.closeButton.OnClick += FiguresWindow_closeButton_OnClick;
		figuresWindow.ProductSelected += FiguresWindow_ProductSelected;
		figuresWindow.Open();
	}

	private void HideFiguresWindow()
	{
		figuresWindow.closeButton.OnClick -= FiguresWindow_closeButton_OnClick;
		figuresWindow.ProductSelected -= FiguresWindow_ProductSelected;
		figuresWindow.Close();
	}

	private void FiguresWindow_closeButton_OnClick()
	{
		HideFiguresWindow();
		ShowMainMenu();
	}

	private void FiguresWindow_ProductSelected(IAPProduct product)
	{
		if (!string.IsNullOrEmpty(product.id) && purchasingProduct == null)
		{
			UnityEngine.Debug.LogFormat("Try purchasing {0} with ID \"{1}\"", product.name, product.ProductID);
			coverSprite.gameObject.SetActive(value: true);
			tk2dUIManager.Instance.InputEnabled = false;
			purchasingProduct = product;
			IAPStore.Instance.Controller.InitiatePurchase(IAPStore.Instance.Controller.products.WithID(product.ProductID));
		}
	}

	private void ShowOddsWindow(RiddleEggType eggType)
	{
		activeOddsWindow = ((eggType != RiddleEggType.Green) ? oddsGoldenEggWindow : oddsGreenEggWindow);
		activeOddsWindow.closeButton.OnClick += ActiveOddsWindow_closeButton_OnClick;
		activeOddsWindow.Open();
	}

	private void HideOddsWindow()
	{
		activeOddsWindow.closeButton.OnClick -= ActiveOddsWindow_closeButton_OnClick;
		activeOddsWindow.Close();
		activeOddsWindow = null;
	}

	private void ActiveOddsWindow_closeButton_OnClick()
	{
		HideOddsWindow();
		ShowRiddleEggsWindow();
	}

	private void IAPStore_Instance_Initialized()
	{
	}

	private void IAPStore_Instance_InitializeFailed(InitializationFailureReason reason)
	{
		GameLinkPopupDialog.Show(Language.Get("IAP_POPUP_FAILEDINIT_HEADER", "MENUS,GUI"), Language.Get("IAP_POPUP_FAILEDINIT_MESSAGE", "MENUS,GUI"), Language.Get("IAP_POPUP_FAILEDINIT_BUTTON", "MENUS,GUI"), string.Empty);
	}

	private void IAPStore_Instance_PurchaseCompleted(PurchaseEventArgs e)
	{
		if (purchasingProduct == null)
		{
			UnityEngine.Debug.Log("Purchasing Product is NULL, so not going to process this purchase.");
			return;
		}
		UnityEngine.Debug.LogFormat("[RiddlerShopController] - Purchase Completed    Unity IAP ID: {0}    Store ID: {1}", e.purchasedProduct.definition.id, e.purchasedProduct.definition.storeSpecificId);
		coverSprite.gameObject.SetActive(value: false);
		tk2dUIManager.Instance.InputEnabled = true;
		bool flag = false;
		string id = e.purchasedProduct.definition.id;
		IAPProduct iAPProduct = purchasingProduct;
		UnityEngine.Debug.LogFormat("IAP Product: {0}    # Rewards: {1}", (iAPProduct == null) ? "N/A" : iAPProduct.name.text, iAPProduct?.rewards.Length ?? 0);
		List<PlayerReward> list = new List<PlayerReward>();
		for (int i = 0; i < iAPProduct.rewards.Length; i++)
		{
			if (iAPProduct.rewards[i].type == PlayerRewardType.RiddleEgg)
			{
				list.Add(iapConfig.GenerateRiddleEggReward(iAPProduct.rewards[i].riddleEggType));
				flag = true;
				continue;
			}
			if (iAPProduct.rewards[i].type == PlayerRewardType.Figure)
			{
				CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(iAPProduct.rewards[i].figure.characterUID);
				AnalyticsEvent.Custom("tt2_figure_purchased", new Dictionary<string, object>
				{
					{ "figure", character.characterName },
					{
						"price",
						e.purchasedProduct.metadata.localizedPriceString
					}
				});
			}
			list.Add(iAPProduct.rewards[i]);
		}
		Player.instance.AddToPurchasedRewardsQueue(list);
		Player.instance.Save();
		EventMessenger.Instance.Raise(new RevealPurchasedRewardsEvent());
		purchasingProduct = null;
	}

	private void IAPStore_Instance_PurchaseFailed(Product product, PurchaseFailureReason purchaseFailureReason)
	{
		coverSprite.gameObject.SetActive(value: false);
		tk2dUIManager.Instance.InputEnabled = true;
		GameLinkPopupDialog.Show(Language.Get("IAP_POPUP_PURCHASEFAILED_HEADER", "MENUS,GUI"), Language.Get("IAP_POPUP_PURCHASEFAILED_MESSAGE", "MENUS,GUI"), Language.Get("IAP_POPUP_PURCHASEFAILED_BUTTON", "MENUS,GUI"), string.Empty);
		purchasingProduct = null;
	}

	private void IAPStore_Instance_ReceiptInvalid()
	{
		coverSprite.gameObject.SetActive(value: false);
		tk2dUIManager.Instance.InputEnabled = true;
		GameLinkPopupDialog.Show(Language.Get("IAP_POPUP_PURCHASEFAILED_HEADER", "MENUS,GUI"), Language.Get("IAP_POPUP_PURCHASEFAILED_MESSAGE", "MENUS,GUI"), Language.Get("IAP_POPUP_PURCHASEFAILED_BUTTON", "MENUS,GUI"), string.Empty);
		purchasingProduct = null;
	}

	private void BackButton_OnClick()
	{
		ExitShop();
	}

	private void RiddleEggsButton_uiItem_OnClick()
	{
		HideMainMenu();
		activeProductWindow = riddleEggsWindow;
		ShowRiddleEggsWindow();
	}

	private void RepaintTokensButton_uiItem_OnClick()
	{
		HideMainMenu();
		activeProductWindow = repaintTokensWindow;
		ShowRepaintTokensWindow();
	}

	private void FiguresButton_uiItem_OnClick()
	{
		HideMainMenu();
		activeProductWindow = figuresWindow;
		ShowFiguresWindow();
	}
}
