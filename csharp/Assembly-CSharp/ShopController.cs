using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ShopController : MonoBehaviour
{
	public static ShopController instance;

	public static ShopInventory inventory;

	public static int newsFlashUID = -1;

	public static ShopType shopType;

	public static ShopDefinition shopDef;

	public static bool isFlashSaleShop = false;

	public static bool fromRetry = false;

	private static Dictionary<CharacterClass, string> localizationRestrictionKey = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"BEAST_ONLY"
		},
		{
			CharacterClass.Cute,
			"CUTE_ONLY"
		},
		{
			CharacterClass.DarkArts,
			"DARK_ARTS_ONLY"
		},
		{
			CharacterClass.MartialArts,
			"MARTIAL_ARTS_ONLY"
		},
		{
			CharacterClass.None,
			"NONE_ONLY"
		},
		{
			CharacterClass.Super,
			"SUPER_ONLY"
		},
		{
			CharacterClass.Tech,
			"TECH_ONLY"
		}
	};

	public bool debugShopInventory;

	public ShopInventory debugInventory;

	public bool debugShopSetup;

	public ShopType debugShopType;

	public ShopFigureReveal figureReveal;

	public Transform dialogueSystemParent;

	public GameObject racksGroup;

	public GameObject figureRackPrefab;

	public GameObject figureBoxPrefab;

	public GameObject modchipBoxPrefab;

	public GameObject accessoryBoxPrefab;

	private ShopControllerSetupType shopSetup;

	public ShopSetup traditionalSetup;

	public ShopSetup surfShop;

	public ShopSetup saphiresSetup;

	public ShopSetup modchipShop;

	[HideInInspector]
	public bool initialTouchOnButton;

	[HideInInspector]
	public List<GameObject> allFigures = new List<GameObject>();

	public List<ShopToyBox> allToyBoxes = new List<ShopToyBox>();

	public GameObject currentFigureObject;

	private Vector3 inputStartPosition = Vector3.zero;

	private const int maxItemsInCart = 5;

	private readonly bool[] cartSpotHasItem = new bool[5];

	public bool viewingFigure;

	private bool initialTouchOnFigure;

	private bool finishedMove = true;

	private bool finishedRotate = true;

	private bool showingDarkness;

	private bool freeFigureToReveal;

	public int discount;

	private int totalRacks;

	private int itemsInCartCount;

	private float rotateStart;

	private float rotateStartX;

	private float rotateDelta;

	private float rotateDeltaX;

	private float inputDownTime;

	private float defaultDPI = 129f;

	private float moveableStart;

	private float startingRotation;

	private int tossCount;

	private Dictionary<string, int> figTossSFXPlays;

	private Dictionary<string, int> figTossTutorialSFXPlays;

	public List<Material> sharedStripeMaterials = new List<Material>();

	public List<TextMeshPro> sharedFontMaterialRef = new List<TextMeshPro>();

	private int _currentRack;

	private int totalCurrency => isFlashSaleShop ? Player.instance.currencyTickets : Player.instance.currency;

	public int CurrentRack
	{
		get
		{
			int result = (int)((ShopCart.instance.gameObject.transform.position.x + shopSetup.rackSpacing / 2f) / shopSetup.rackSpacing);
			if (shopType == ShopType.JailBird)
			{
				if (ShopCart.instance.gameObject.transform.position.x > 10f && ShopCart.instance.gameObject.transform.position.x < 16.4f)
				{
					result = 2;
				}
				if (ShopCart.instance.gameObject.transform.position.x >= 16.4f && ShopCart.instance.gameObject.transform.position.x < 22.4f)
				{
					result = 3;
				}
				if (ShopCart.instance.gameObject.transform.position.x >= 22.4f)
				{
					result = 4;
				}
			}
			return result;
		}
	}

	public event Action<ShopToyBox> FigureTossed;

	private void Awake()
	{
		instance = this;
		Input.multiTouchEnabled = false;
		Player.instance.pvpData.BattleRequestAccepted += PvpData_BattleRequestAccepted;
	}

	private void OnDestroy()
	{
		instance = null;
		Input.multiTouchEnabled = true;
		Player.instance.pvpData.BattleRequestAccepted -= PvpData_BattleRequestAccepted;
	}

	private IEnumerator Start()
	{
		if (shopDef != null)
		{
			shopType = shopDef.shopType;
		}
		SetupShopInventory();
		ShopUI.instance.changeText.text = totalCurrency.ToString();
		CityCharacterVOSet voSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(CityCharacter.characterType);
		figTossTutorialSFXPlays = new Dictionary<string, int>();
		for (int i = 0; i < voSet.storeTutorialFigTossClipSet.Length; i++)
		{
			figTossTutorialSFXPlays.Add(voSet.storeTutorialFigTossClipSet[i], 0);
		}
		figTossSFXPlays = new Dictionary<string, int>();
		for (int j = 0; j < voSet.storeFigTossClipSet.Length; j++)
		{
			figTossSFXPlays.Add(voSet.storeFigTossClipSet[j], 0);
		}
		yield return new WaitForEndOfFrame();
		discount = 0;
		if (newsFlashUID > -1 && !isFlashSaleShop)
		{
			NewsFlashData newsFlashData = Player.instance.GetNewsFlashData(newsFlashUID);
			NewsFlash newsFlash = GameManager.instance.communicatorDatabase.GetNewsFlash(newsFlashUID);
			if (newsFlashData != null)
			{
				string text = "Resolving News Flash....";
				switch (newsFlash.newsFlashType)
				{
				case NewsFlashType.FreeFigure:
					if (!newsFlashData.completed || newsFlash.repeatable)
					{
						text += "\n \t Resolve Free Figure...";
						freeFigureToReveal = true;
						StartCoroutine(StartFreeReveal());
						Player.instance.CompleteNewsFlash(newsFlashUID);
					}
					break;
				case NewsFlashType.Discount:
					text += "\n \t Resolve Discount...";
					freeFigureToReveal = false;
					discount = newsFlash.shopDiscountAmount;
					Player.instance.CompleteNewsFlash(newsFlashUID);
					StartCoroutine(ShowDiscountPopUp());
					break;
				case NewsFlashType.GrandOpening:
				case NewsFlashType.NewShipment:
					text += "\n \t Resolve New Shipment/Grand Opening...";
					freeFigureToReveal = false;
					Player.instance.CompleteNewsFlash(newsFlashUID);
					break;
				}
				Debug.Log(text);
			}
			else
			{
				Debug.Log("Skipping News Flash...");
			}
		}
		if (!fromRetry)
		{
			Player.instance.IncreaseCurrentTime(20);
		}
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		if (isFlashSaleShop)
		{
			int currencyTickets = Player.instance.currencyTickets;
			for (int k = 0; k < Player.instance.activeMissions.Count; k++)
			{
				MissionData missionData = Player.instance.activeMissions[k];
				if (missionData.active && !missionData.completed)
				{
					Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
					if (mission.targetMsg == "FlashSaleTicketPickup" && currencyTickets >= mission.amountNeeded)
					{
						CommunicatorManager.instance.UpdateMissionWithTargetMessage(mission.targetMsg, mission.amountNeeded);
					}
				}
			}
		}
		if (!fromRetry && (CoreGameTutorial.instance == null || !CoreGameTutorial.instance.currentStep.ToString().Contains("ShopTutorial")))
		{
			GFAudioManager.instance.PlayClipFromResources(voSet.storeEnterClipSet.GetRandom(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
		}
		fromRetry = false;
	}

	private void SetupShopInventory()
	{
		if (inventory == null)
		{
			inventory = Resources.Load("Data/ShopInventory/NewShopInventory") as ShopInventory;
		}
		if (!(inventory != null))
		{
			return;
		}
		totalRacks = inventory.racks.Count;
		ChooseShopSetupType();
		if (shopType == ShopType.JailBird)
		{
			totalRacks++;
		}
		if (shopType == ShopType.Zinthos)
		{
			totalRacks = 3;
		}
		if (shopType == ShopType.MartianManhuntersShop)
		{
			totalRacks = 3;
		}
		if (!string.IsNullOrEmpty(shopDef.shopWallPrefab))
		{
			UnityEngine.Object.Instantiate(Resources.Load(shopDef.shopWallPrefab) as GameObject);
		}
		for (int i = 0; i < inventory.racks.Count; i++)
		{
			ShopRack shopRack = inventory.racks[i];
			GameObject glassCaseObject = null;
			if (shopRack.glassCase)
			{
				if (!string.IsNullOrEmpty(shopDef.shopRackPrefab))
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load(shopDef.shopRackPrefab) as GameObject);
					if (gameObject != null)
					{
						foreach (Transform item in gameObject.transform)
						{
							if (item.gameObject.name.Equals("glassCase"))
							{
								item.gameObject.SetActive(value: true);
								glassCaseObject = item.gameObject;
							}
							if (item.gameObject.name.Equals("shop_hanger_mesh"))
							{
								item.gameObject.SetActive(value: false);
							}
						}
						gameObject.transform.position = new Vector3((float)i * shopSetup.rackSpacing, 0f, shopSetup.rackStartingZ);
					}
				}
			}
			else if (!string.IsNullOrEmpty(shopDef.shopRackPrefab))
			{
				bool flag = false;
				bool flag2 = false;
				if (i + 1 < inventory.racks.Count)
				{
					ShopRack shopRack2 = inventory.racks[i + 1];
					flag = shopRack2.glassCase;
				}
				if (i > 0)
				{
					ShopRack shopRack3 = inventory.racks[i - 1];
					flag2 = shopRack3.glassCase;
				}
				GameObject gameObject2 = UnityEngine.Object.Instantiate(Resources.Load(shopDef.shopRackPrefab) as GameObject);
				if (i == 0 || flag2)
				{
					gameObject2.transform.Find("left").gameObject.SetActive(value: true);
				}
				else if (i > 0 && i < inventory.racks.Count - 1 && !flag)
				{
					if (shopType == ShopType.ToyMasters && i == 2 && totalRacks == 5)
					{
						gameObject2.transform.Find("girly").gameObject.SetActive(value: true);
						bool flag3 = true;
						for (int j = 0; j < shopRack.hangers.Count; j++)
						{
							if (shopRack.hangers[j].figures.Count > 0)
							{
								flag3 = false;
							}
						}
						if (flag3)
						{
							GameObject gameObject3 = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Shop/ComingSoonSign") as GameObject);
							gameObject3.transform.parent = gameObject2.transform;
							gameObject3.transform.localPosition = new Vector3(0.1f, -3.75f, 0.033f);
							gameObject3.transform.localScale = new Vector3(6f, 6f, 6f);
						}
					}
					else
					{
						gameObject2.transform.Find("center").gameObject.SetActive(value: true);
					}
				}
				else if (i == inventory.racks.Count - 1 || flag)
				{
					gameObject2.transform.Find("right").gameObject.SetActive(value: true);
				}
				float num = 0f;
				if (shopType == ShopType.JailBird && (i == 2 || i == 3))
				{
					num = 10.2f;
				}
				gameObject2.transform.position = new Vector3((float)i * shopSetup.rackSpacing + num, 0f, shopSetup.rackStartingZ);
				gameObject2.transform.parent = racksGroup.transform;
			}
			for (int k = 0; k < Mathf.Min(shopSetup.maxPositions, shopRack.hangers.Count); k++)
			{
				SetupHangerWithFigures(i, k, shopRack, shopRack.hangers[k].figures, glassCaseObject);
			}
		}
	}

	private void ChooseShopSetupType()
	{
		ShopSetup shopSetup = null;
		if (shopDef == null)
		{
			if (debugShopSetup)
			{
				shopDef = GameManager.instance.shopDatabase.GetShopDefByType(debugShopType);
				shopType = shopDef.shopType;
			}
			else
			{
				shopDef = GameManager.instance.shopDatabase.GetShopDef(0);
				shopType = shopDef.shopType;
			}
		}
		switch (shopType)
		{
		case ShopType.AlfredsToyShop:
		case ShopType.Bizarros:
		case ShopType.KordsWires:
		case ShopType.ShadyShop:
		case ShopType.ToyMasters:
		case ShopType.FlashSaleShop:
		case ShopType.TeenyBUs:
		case ShopType.ControlFreak:
		case ShopType.IcebergLounge:
		case ShopType.KryptosToyBin:
		case ShopType.MartianManhuntersShop:
		case ShopType.MovieMagicShop:
		case ShopType.LeagueOfShadowsShop:
		case ShopType.FalafelStand:
		case ShopType.SuperFriendsShop:
		case ShopType.PowZap66:
			shopSetup = traditionalSetup;
			break;
		case ShopType.Saphires:
			shopSetup = saphiresSetup;
			break;
		case ShopType.ModchipShop:
			shopSetup = modchipShop;
			break;
		case ShopType.BeachBums:
			shopSetup = surfShop;
			break;
		default:
			throw new ArgumentOutOfRangeException();
		case ShopType.JailBird:
		case ShopType.LexToys:
		case ShopType.RiddlerShop:
		case ShopType.StickyJoe:
		case ShopType.Zinthos:
		case ShopType.WayneToys:
		case ShopType.ManorShop:
			break;
		}
		UnityEngine.Object obj = Resources.Load(shopDef.shopModelPrefab);
		if (obj != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(obj) as GameObject;
			ShopSetup component = gameObject.GetComponent<ShopSetup>();
			if (component != null)
			{
				shopSetup = component;
			}
		}
		shopSetup.gameObject.SetActive(value: true);
		this.shopSetup = shopSetup.shopSetupValues;
	}

	private void SetupHangerWithFigures(int rackNumber, int hangerNumber, ShopRack shopRack, List<ShopFigure> figures, GameObject glassCaseObject)
	{
		int num = hangerNumber / 2;
		float num2 = ((hangerNumber % 2 != 0) ? (shopSetup.figureBoxHorzSpacing / 2f) : ((0f - shopSetup.figureBoxHorzSpacing) / 2f));
		if (shopType == ShopType.JailBird && (rackNumber == 2 || rackNumber == 3))
		{
			num2 += 10.2f;
		}
		if (shopRack.glassCase && figures.Count == 0)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/Shop/ComingSoonSign") as GameObject);
			gameObject.transform.parent = glassCaseObject.transform;
			gameObject.transform.localPosition = new Vector3(-0.053f, (shopType != ShopType.Saphires) ? 0.109f : 0.1148f, -0.045f);
		}
		figures = new List<ShopFigure>(figures);
		figures.RemoveAll((ShopFigure fig) => fig.toyType == ToyType.Accessory && Player.instance.HasAcquiredAccessory(fig.accessoryUID));
		for (int num3 = 0; num3 < figures.Count; num3++)
		{
			ShopFigure shopFigure = figures[num3].Clone(new ShopFigure());
			if ((shopType == ShopType.BeachBums || shopType == ShopType.LexToys || shopType == ShopType.ManorShop) && num3 > 0)
			{
				break;
			}
			Vector3 vector = new Vector3((float)rackNumber * shopSetup.rackSpacing + num2, shopSetup.figureBoxVertTop + (float)num * (0f - shopSetup.figureBoxVertSpacing), shopSetup.rackStartingZ + shopSetup.figureBoxZStart + shopSetup.figureBoxZOffset * (float)num3);
			Vector3 localEulerAngles = Vector3.zero;
			if (shopRack.glassCase)
			{
				vector = new Vector3((float)rackNumber * shopSetup.rackSpacing, 0f, shopSetup.rackStartingZ + shopSetup.figureBoxZStart + shopSetup.figureBoxZOffset * (float)num3);
				GameObject gameObject2 = new GameObject("GlassCase");
				gameObject2.transform.position = vector + new Vector3(0f, -2f, 0f);
				gameObject2.AddComponent<BoxCollider>();
				gameObject2.GetComponent<BoxCollider>().size = new Vector3(1.5f, 0.1f, 1.5f);
			}
			else if (shopSetup.customPositions.Length > 0)
			{
				if (hangerNumber < shopSetup.customPositions.Length)
				{
					vector = new Vector3((float)rackNumber * shopSetup.rackSpacing, 0f, shopSetup.figureBoxZOffset * (float)num3) + shopSetup.customPositions[hangerNumber].position;
					localEulerAngles = shopSetup.customPositions[hangerNumber].localEulerAngles;
				}
				else
				{
					Debug.LogError("Missing custom position setup number " + hangerNumber);
				}
			}
			int hangerRack;
			if (shopType == ShopType.JailBird && rackNumber > 1)
			{
				hangerRack = rackNumber + 1;
			}
			else
			{
				hangerRack = ((shopType != ShopType.Zinthos || hangerNumber <= 3) ? rackNumber : 2);
			}
			if (Player.instance.HasPurchasedShopFigure(inventory.name, hangerRack + "-" + hangerNumber + "-" + (num3 + 1)))
			{
				continue;
			}
			if (shopFigure.toyType == ToyType.ModChip || shopFigure.modchip)
			{
				GameObject gameObject3 = UnityEngine.Object.Instantiate(modchipBoxPrefab);
				ShopToyBox component = gameObject3.GetComponent<ShopToyBox>();
				ModChipDefinition modChipDefinition = (ModChipDefinition)GameManager.instance.itemDatabase.GetItem(shopFigure.modchipUID);
				component.modchipNameText.text = Language.Get(modChipDefinition.itemNameKey, "ABILS,CHIPS,ACHVS,LDBS");
				component.modchipDescText.text = TTGParseHelper.GetModChipDescription(modChipDefinition);
				if (modChipDefinition.classRestrictions.Count > 0)
				{
					component.modChipClassText.text = Language.Get(localizationRestrictionKey[modChipDefinition.classRestrictions[0]], "MENUS,GUI");
				}
				else
				{
					component.modChipClassText.text = string.Empty;
				}
				if (modChipDefinition.quality == ModChipQuality.Common)
				{
					component.modchipBronzeBox.SetActive(value: true);
					component.modchipSilverBox.SetActive(value: false);
					component.modchipGoldBox.SetActive(value: false);
					component.modchipNameText.outlineColor = new Color(152f / 255f, 32f / 255f, 1f / 17f);
					component.modchipDescText.outlineColor = new Color(152f / 255f, 32f / 255f, 1f / 17f);
				}
				if (modChipDefinition.quality == ModChipQuality.Rare)
				{
					component.modchipBronzeBox.SetActive(value: false);
					component.modchipSilverBox.SetActive(value: true);
					component.modchipGoldBox.SetActive(value: false);
					component.modchipNameText.outlineColor = new Color(152f / 255f, 32f / 255f, 1f / 17f);
					component.modchipDescText.outlineColor = new Color(152f / 255f, 32f / 255f, 1f / 17f);
				}
				if (modChipDefinition.quality == ModChipQuality.Legendary)
				{
					component.modchipBronzeBox.SetActive(value: false);
					component.modchipSilverBox.SetActive(value: false);
					component.modchipGoldBox.SetActive(value: true);
					component.modchipNameText.outlineColor = new Color(0f, 0f, 0f);
					component.modchipDescText.outlineColor = new Color(0f, 0f, 0f);
				}
				component.modchipSelectPortrait.Init(modChipDefinition);
				component.shelfPosition = vector;
				component.hangerNumber = hangerNumber;
				component.hangerSpot = num3 + 1;
				if (shopType == ShopType.Zinthos)
				{
					SimpleBob simpleBob = component.boxGroup.AddComponent<SimpleBob>();
					simpleBob.duration = 1f;
					simpleBob.deltaY = 0.1f;
				}
				if (shopType == ShopType.MartianManhuntersShop)
				{
					SimpleBob simpleBob2 = component.boxGroup.AddComponent<SimpleBob>();
					simpleBob2.duration = 1f;
					simpleBob2.deltaY = 0.1f;
				}
				component.hangerRack = hangerRack;
				component.underGlass = shopRack.glassCase;
				if (component.underGlass && glassCaseObject != null)
				{
					foreach (Transform item in glassCaseObject.transform)
					{
						if (item.gameObject.name.Contains("glass"))
						{
							component.glassObject = item.gameObject;
						}
					}
				}
				component.hanging = shopSetup.figuresHanging;
				gameObject3.transform.position = vector;
				gameObject3.transform.localEulerAngles = localEulerAngles;
				gameObject3.transform.parent = racksGroup.transform;
				allFigures.Add(gameObject3);
				allToyBoxes.Add(component);
				continue;
			}
			if (shopFigure.toyType == ToyType.Accessory)
			{
				Debug.Log("Accessory Toy Position: " + vector);
				GameObject gameObject4 = UnityEngine.Object.Instantiate(accessoryBoxPrefab);
				ShopToyBox component2 = gameObject4.GetComponent<ShopToyBox>();
				Debug.Log("Accessory Figure Data UID: " + shopFigure.accessoryUID);
				AccessoryItemDefinition accessoryItemDefinition = (AccessoryItemDefinition)GameManager.instance.itemDatabase.GetItem(shopFigure.accessoryUID);
				component2.accessoryNameText.text = Language.Get(accessoryItemDefinition.itemNameKey, "ABILS,CHIPS,ACHVS,LDBS");
				component2.accessoryShortDescText.text = string.Empty;
				component2.accessoryLongDescText.text = TTGParseHelper.GetAccessoryDescription(accessoryItemDefinition);
				component2.shelfPosition = vector;
				component2.hangerNumber = hangerNumber;
				component2.hangerSpot = num3 + 1;
				component2.hanging = shopSetup.figuresHanging;
				component2.hangerRack = hangerRack;
				component2.underGlass = shopRack.glassCase;
				component2.figureData.accessoryUID = shopFigure.accessoryUID;
				if (component2.underGlass && glassCaseObject != null)
				{
					foreach (Transform item2 in glassCaseObject.transform)
					{
						if (item2.gameObject.name.Contains("glass"))
						{
							component2.glassObject = item2.gameObject;
						}
					}
				}
				if (shopType == ShopType.Zinthos)
				{
					SimpleBob simpleBob3 = component2.boxGroup.AddComponent<SimpleBob>();
					simpleBob3.duration = 1f;
					simpleBob3.deltaY = 0.1f;
				}
				if (shopType == ShopType.MartianManhuntersShop)
				{
					SimpleBob simpleBob4 = component2.boxGroup.AddComponent<SimpleBob>();
					simpleBob4.duration = 1f;
					simpleBob4.deltaY = 0.1f;
				}
				gameObject4.transform.position = vector;
				gameObject4.transform.localEulerAngles = localEulerAngles;
				gameObject4.transform.parent = racksGroup.transform;
				allFigures.Add(gameObject4);
				allToyBoxes.Add(component2);
				continue;
			}
			GameObject gameObject5 = UnityEngine.Object.Instantiate(figureBoxPrefab);
			ShopToyBox component3 = gameObject5.GetComponent<ShopToyBox>();
			component3.blindBox = shopFigure.blindBox;
			component3.isGoldBox = shopFigure.goldBox;
			component3.figureData = shopFigure;
			if (shopFigure.overrideFigure)
			{
				if (Player.instance.HasAcquiredCharacter(shopFigure.overrideCollectedUID))
				{
					shopFigure.characterUID = shopFigure.overrideNewUID;
					shopFigure.level = shopFigure.overrideNewLevel;
					component3.didOverrideFigure = true;
				}
			}
			else if (shopFigure.blindBox)
			{
				if (shopFigure.blindBoxShopFigures.Count > 0)
				{
					bool flag = true;
					int num4 = 0;
					while (flag)
					{
						int index = UnityEngine.Random.Range(0, shopFigure.blindBoxShopFigures.Count);
						int characterUID = shopFigure.blindBoxShopFigures[index].characterUID;
						if (!ShopToyBox.CheckIsMissionFigure(characterUID, shopFigure.blindBoxShopFigures[index].level))
						{
							shopFigure.characterUID = characterUID;
							shopFigure.level = shopFigure.blindBoxShopFigures[index].level;
							flag = false;
						}
						num4++;
						if (num4 >= 8 && flag)
						{
							flag = false;
							shopFigure.characterUID = characterUID;
							shopFigure.level = shopFigure.blindBoxShopFigures[index].level;
						}
					}
				}
				component3.blindBoxCost = shopFigure.blindBoxCost;
			}
			component3.shelfPosition = vector;
			component3.hangerSpot = num3 + 1;
			component3.hangerNumber = hangerNumber;
			if (shopType == ShopType.Zinthos)
			{
				SimpleBob simpleBob5 = component3.boxGroup.AddComponent<SimpleBob>();
				simpleBob5.duration = 1f;
				simpleBob5.deltaY = 0.1f;
			}
			if (shopType == ShopType.MartianManhuntersShop)
			{
				SimpleBob simpleBob6 = component3.boxGroup.AddComponent<SimpleBob>();
				simpleBob6.duration = 1f;
				simpleBob6.deltaY = 0.1f;
			}
			component3.hangerRack = hangerRack;
			component3.underGlass = shopRack.glassCase;
			if (component3.underGlass && glassCaseObject != null)
			{
				foreach (Transform item3 in glassCaseObject.transform)
				{
					if (item3.gameObject.name.Contains("glass"))
					{
						component3.glassObject = item3.gameObject;
					}
				}
			}
			component3.hanging = (!shopSetup.figuresHanging || !shopSetup.ignoreHangingForPositions.Contains(hangerNumber)) && shopSetup.figuresHanging;
			gameObject5.transform.position = vector;
			gameObject5.transform.localEulerAngles = localEulerAngles;
			gameObject5.transform.parent = racksGroup.transform;
			allFigures.Add(gameObject5);
			allToyBoxes.Add(component3);
		}
	}

	private IEnumerator ShowDiscountPopUp()
	{
		yield return new WaitForSeconds(1f);
		TutorialPopUp discountPopup = UnityEngine.Object.Instantiate(Resources.Load("Prefabs/GUI/Discount_PopUp_Shop") as GameObject).GetComponent<TutorialPopUp>();
		discountPopup.text.text = string.Format(Language.Get("ALL_FIGS_DISCOUNT", "MENUS,GUI"), discount);
		discountPopup.transform.parent = ShopUI.instance.AnchorUpperCenter;
		discountPopup.transform.localPosition = new Vector3(0f, -100f, 10f);
		discountPopup.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
		discountPopup.PlayIntro();
		yield return new WaitForEndOfFrame();
		bool stayInLoop = true;
		while (stayInLoop)
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
			{
				stayInLoop = false;
			}
			yield return null;
		}
		discountPopup.PlayOutro();
	}

	private IEnumerator StartFreeReveal()
	{
		yield return new WaitForEndOfFrame();
		while (!LoadingScreenCover.allowInit)
		{
			yield return null;
		}
		ShopUI.instance.RevealFreeFigure();
	}

	public IEnumerator PlayDialogue(string convoName)
	{
		yield return StartCoroutine(ShowDialogue(Resources.Load("Data/Conversations/" + convoName) as Conversation, showExitButton: false));
	}

	public IEnumerator PlayTT2Dialogue(string convoName)
	{
		string conversationName = CityCharacter.GetCityCharacterConvoName(convoName);
		Conversation convo = (Conversation)Resources.Load("Data/TT2/" + conversationName);
		if (!(convo == null))
		{
			yield return StartCoroutine(ShowDialogue(convo, showExitButton: false));
		}
	}

	private void Update()
	{
		if (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
		{
			return;
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		bool flag5 = false;
		bool flag6 = false;
		if (CoreGameTutorial.instance != null)
		{
			flag3 = CoreGameTutorial.instance.PreventShopShelfMovement();
			flag2 = CoreGameTutorial.instance.PreventShopToyBoxToss();
			flag = CoreGameTutorial.instance.PreventShopToyBoxTouch();
			flag4 = CoreGameTutorial.instance.PreventShopToyBoxInitialTouch();
			flag5 = CoreGameTutorial.instance.PreventShopToyBoxMoveBackToHanger();
			flag6 = CoreGameTutorial.instance.PreventShopToyBoxMoveToCart();
		}
		if (Input.GetKeyDown(KeyCode.B))
		{
			if (!isFlashSaleShop)
			{
				Player.instance.AddCurrency(1000);
				ShopUI.instance.changeText.text = (totalCurrency - TotalInCart()).ToString();
			}
			else
			{
				Player.instance.AddCurrencyTickets(5);
				ShopUI.instance.changeText.text = (totalCurrency - TotalInCart()).ToString();
			}
		}
		if (Input.GetKeyDown(KeyCode.Keypad6))
		{
			Debug.Log("Create Test Battle Request...");
			if (Player.instance.pvpData.friends.Count > 0)
			{
				BattleRequest battleRequest = new BattleRequest();
				battleRequest.playerId = Player.instance.pvpData.friends[0].playerID;
				battleRequest.region = "US";
				battleRequest.roomName = "TestRoom";
				BattleRequest battleRequest2 = battleRequest;
				Player.instance.pvpData.AddBattleRequest(battleRequest2);
			}
			else
			{
				Debug.LogError("You need at least 1 friend to test a battle request...");
			}
		}
		if (Input.GetKeyDown(KeyCode.Keypad7))
		{
			Debug.Log("Create Test Friend Request...");
			FriendRequest friendRequest = new FriendRequest();
			friendRequest.playerId = FriendCodeGenerator.Generate();
			friendRequest.playerName = "TestFriend" + UnityEngine.Random.Range(0, 101);
			friendRequest.level = UnityEngine.Random.Range(1, GameManager.instance.legionOfDoomLevelTable.MaxLevel + 1);
			friendRequest.portraitSpriteName = GameManager.instance.legionOfDoomSettings.GetRandomPortraitSprite();
			friendRequest.skillRating = UnityEngine.Random.Range(1000, 4001);
			FriendRequest friendRequest2 = friendRequest;
			Player.instance.pvpData.AddFriendRequest(friendRequest2);
		}
		if (ShopCart.instance != null)
		{
			figureReveal.transform.position = ShopCart.instance.gameObject.transform.position;
		}
		if (ShopUI.instance.receiptGroup.activeSelf || figureReveal.gameObject.activeSelf || ShopUI.instance.modchipReveal.gameObject.activeInHierarchy || LODRequestFlag.isInviteScreenActive || ShopUI.instance.IsRevealing)
		{
			return;
		}
		if (!viewingFigure)
		{
			if (showingDarkness)
			{
				showingDarkness = false;
				StopCoroutine("ShowDarkness");
				StopCoroutine("HideDarkness");
				StartCoroutine("HideDarkness");
			}
			if (Input.GetMouseButtonDown(0))
			{
				inputStartPosition = Input.mousePosition;
				inputDownTime = Time.time;
				initialTouchOnFigure = false;
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray, out var hitInfo))
				{
					Transform transform = hitInfo.collider.transform;
					if ((bool)transform && transform.gameObject.GetComponent<ShopToyBox>() != null && !transform.gameObject.GetComponent<ShopToyBox>().moving && !flag4)
					{
						if (transform.gameObject.GetComponent<ShopToyBox>().CanSelectBox())
						{
							initialTouchOnFigure = true;
							currentFigureObject = transform.gameObject;
							startingRotation = ShopCart.instance.rotatingTransform.localEulerAngles.y;
							if (!currentFigureObject.GetComponent<ShopToyBox>().underGlass)
							{
								currentFigureObject.GetComponent<ShopToyBox>().TouchedBox();
							}
							GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figuretouched", GFAudioManager.SFX_GROUP, string.Empty);
						}
						return;
					}
				}
				moveableStart = ShopCart.instance.gameObject.transform.position.x;
				if (!initialTouchOnFigure && DOTween.Kill(ShopCart.instance.gameObject.transform) > 0)
				{
					FinishedMove();
				}
			}
			if (Input.GetMouseButton(0) && !initialTouchOnFigure && finishedMove && !flag3)
			{
				Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
				Ray ray3 = Camera.main.ScreenPointToRay(inputStartPosition);
				Vector3 vector = Camera.main.transform.position + ray2.direction * (shopSetup.rackStartingZ - Camera.main.transform.position.z);
				Vector3 vector2 = Camera.main.transform.position + ray3.direction * (shopSetup.rackStartingZ - Camera.main.transform.position.z);
				Vector3 vector3 = vector2 - vector;
				float num = (float)totalRacks * shopSetup.rackSpacing - shopSetup.rackSpacing / 2f;
				if (shopType == ShopType.JailBird)
				{
					num += 5f;
				}
				ShopCart.instance.gameObject.transform.position = new Vector3(Mathf.Min(num, Mathf.Max((0f - shopSetup.rackSpacing) / 2f, moveableStart + vector3.x)), ShopCart.instance.gameObject.transform.position.y, ShopCart.instance.gameObject.transform.position.z);
			}
			if (!Input.GetMouseButtonUp(0))
			{
				return;
			}
			bool flag7 = false;
			Vector3 mousePosition = Input.mousePosition;
			Vector3 vector4 = mousePosition - inputStartPosition;
			bool flag8 = (vector4.magnitude < 0.001f && Time.time - inputDownTime < 0.8f) || (vector4.magnitude < 8f * Screen.dpi / defaultDPI && Time.time - inputDownTime < 0.13f);
			if (finishedMove && initialTouchOnFigure && !initialTouchOnButton && flag8 && !flag)
			{
				Ray ray4 = Camera.main.ScreenPointToRay(Input.mousePosition);
				if (Physics.Raycast(ray4, out var hitInfo2))
				{
					Transform transform2 = hitInfo2.collider.transform;
					if ((bool)transform2 && transform2.gameObject.GetComponent<ShopToyBox>() != null && currentFigureObject == transform2.gameObject)
					{
						bool flag9 = false;
						if (CoreGameTutorial.instance != null && currentFigureObject.GetComponent<ShopToyBox>().charDef != null)
						{
							flag9 = CoreGameTutorial.instance.PreventShopToyBoxTouchOn(currentFigureObject.GetComponent<ShopToyBox>().charDef.characterName);
						}
						if (transform2.gameObject.GetComponent<ShopToyBox>().inCart)
						{
							flag7 = true;
							itemsInCartCount = Mathf.Max(0, itemsInCartCount - 1);
							SetItemCartSpot(currentFigureObject.GetComponent<ShopToyBox>().itemCartNumber, available: false);
							currentFigureObject.transform.parent = racksGroup.transform;
							currentFigureObject.GetComponent<Rigidbody>().isKinematic = true;
							currentFigureObject.GetComponent<ShopToyBox>().inCart = false;
							Vector3 endValue = ((!transform2.GetComponent<ShopToyBox>().isModChip) ? ShopCart.instance.viewingTransform.position : (ShopCart.instance.viewingTransform.position + new Vector3(0f, -0.392f, -0.279f)));
							transform2.DOMove(endValue, 0.2f).SetEase(Ease.Linear).OnComplete(() =>
							{
								FinishedViewingMove();
							});
							if (itemsInCartCount <= 0)
							{
								ShopUI.instance.HideCheckOutButton();
							}
						}
						else if (!flag9)
						{
							currentFigureObject.GetComponent<ShopToyBox>().DestroyHingeJoint();
							MoveToViewingPosition();
							ShakeRacks();
						}
						else
						{
							bool flag10 = currentFigureObject.GetComponent<ShopToyBox>().charDef != null && currentFigureObject.GetComponent<ShopToyBox>().charDef.characterName.Equals("Robin");
							currentFigureObject.GetComponent<ShopToyBox>().DestroyHingeJoint(makeKinematic: false, !flag10);
						}
						if (!flag9)
						{
							viewingFigure = true;
							currentFigureObject.transform.DORotate(Vector3.zero, 0.2f);
							currentFigureObject = transform2.gameObject;
							finishedMove = false;
						}
						if (CoreGameTutorial.instance != null)
						{
							bool flag11 = false;
							if (currentFigureObject.GetComponent<ShopToyBox>().charDef != null)
							{
								flag11 = CoreGameTutorial.instance.CheckToyBoxTouched(currentFigureObject.GetComponent<ShopToyBox>().charDef.characterName);
							}
							if (flag11)
							{
								currentFigureObject.GetComponent<Rigidbody>().isKinematic = true;
								currentFigureObject.GetComponent<ShopToyBox>().inCart = true;
								itemsInCartCount = Mathf.Min(5, itemsInCartCount + 1);
								currentFigureObject.GetComponent<ShopToyBox>().itemCartNumber = GetAvailableItemCartSpot();
								currentFigureObject.GetComponent<ShopToyBox>().forceIsNewFigure = true;
								currentFigureObject.transform.DOKill();
								MoveItemToCart(currentFigureObject, revealScreen: true);
								ShopUI.instance.HideCheckOutButton();
								viewingFigure = false;
								ShopUI.instance.CheckOutTutorial();
							}
						}
						return;
					}
				}
			}
			if (currentFigureObject != null)
			{
				currentFigureObject.GetComponent<ShopToyBox>().UnTouchedBox();
			}
			bool flag12 = false;
			bool flag13 = false;
			bool flag14 = false;
			bool flag15 = false;
			if (vector4.magnitude > 50f * Screen.dpi / defaultDPI && Time.time - inputDownTime < 0.3f)
			{
				if (vector4.y > 100f * Screen.dpi / defaultDPI)
				{
					flag14 = true;
				}
				if (vector4.x > 0f && !flag15)
				{
					flag13 = true;
				}
				if (vector4.x < 0f && !flag15)
				{
					flag12 = true;
				}
			}
			if (!finishedMove)
			{
				return;
			}
			if (flag13 && !initialTouchOnFigure && !flag3)
			{
				_currentRack = Mathf.Max(0, _currentRack - 1);
				MoveCartToRack(_currentRack);
			}
			if (flag12 && !initialTouchOnFigure && !flag3)
			{
				_currentRack = Mathf.Min(totalRacks - 1, _currentRack + 1);
				MoveCartToRack(_currentRack);
			}
			if (!flag13 && !flag12 && !initialTouchOnFigure && !flag3)
			{
				ShopCart.instance.gameObject.transform.DOMove(new Vector3(RackCenter(Mathf.Min(totalRacks - 1, CurrentRack)), ShopCart.instance.gameObject.transform.position.y, ShopCart.instance.gameObject.transform.position.z), 0.5f).OnComplete(FinishedMove);
			}
			if ((flag13 || flag12) && initialTouchOnFigure && !initialTouchOnButton && !flag2 && currentFigureObject != null && !currentFigureObject.GetComponent<ShopToyBox>().toyBoxInfront && !flag7 && !currentFigureObject.GetComponent<ShopToyBox>().inCart && !currentFigureObject.GetComponent<ShopToyBox>().underGlass)
			{
				CharacterDefinition charDef = currentFigureObject.GetComponent<ShopToyBox>().charDef;
				if (CoreGameTutorial.instance != null && charDef != null)
				{
					CoreGameTutorial.instance.CheckToyBoxTossed(currentFigureObject.GetComponent<ShopToyBox>().charDef.characterName);
				}
				bool flag16 = false;
				if (CoreGameTutorial.instance != null && charDef != null)
				{
					flag16 = CoreGameTutorial.instance.PreventToyBoxTossed(charDef.characterName);
				}
				if (!flag16)
				{
					if (UnityEngine.Random.value <= 0.5f || CoreGameTutorial.instance.currentStep == CoreGameTutorial.Step.ShopTutorialPart4 || CoreGameTutorial.instance.currentStep == CoreGameTutorial.Step.ShopTutorialPart5)
					{
						string text = string.Empty;
						int num2 = int.MaxValue;
						foreach (KeyValuePair<string, int> figTossSFXPlay in figTossSFXPlays)
						{
							if (figTossSFXPlay.Value < num2 || (figTossSFXPlay.Value == num2 && UnityEngine.Random.value <= 0.5f))
							{
								num2 = figTossSFXPlay.Value;
								text = figTossSFXPlay.Key;
							}
						}
						if (figTossSFXPlays.ContainsKey(text))
						{
							figTossSFXPlays[text]++;
						}
						string text2 = text;
						if (CoreGameTutorial.instance.currentStep == CoreGameTutorial.Step.ShopTutorialPart4 || CoreGameTutorial.instance.currentStep == CoreGameTutorial.Step.ShopTutorialPart5)
						{
							tossCount++;
							if (tossCount >= 4)
							{
								tossCount = 0;
								string text3 = string.Empty;
								int num3 = int.MaxValue;
								foreach (KeyValuePair<string, int> figTossTutorialSFXPlay in figTossTutorialSFXPlays)
								{
									if (figTossTutorialSFXPlay.Value < num3)
									{
										num3 = figTossTutorialSFXPlay.Value;
										text3 = figTossTutorialSFXPlay.Key;
									}
								}
								if (figTossTutorialSFXPlays.ContainsKey(text3))
								{
									figTossTutorialSFXPlays[text3]++;
								}
								text2 = text3;
							}
						}
						if (!GFAudioManager.instance.IsPlaying("figTossSFX") && !string.IsNullOrEmpty(text2))
						{
							GFAudioManager.instance.PlayClipFromResources(text2, GFAudioManager.VOICEOVER_GROUP, "figTossSFX");
						}
					}
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figureflungwav", GFAudioManager.SFX_GROUP, string.Empty);
					currentFigureObject.GetComponent<ShopToyBox>().DestroyHingeJoint(makeKinematic: false);
					Vector3 velocity = currentFigureObject.GetComponent<Rigidbody>().velocity;
					velocity.x = ((!flag12) ? (Mathf.Abs(vector4.normalized.x) * 15f) : ((0f - Mathf.Abs(vector4.normalized.x)) * 15f));
					velocity.y = vector4.normalized.y * 15f;
					velocity.z = -10f;
					currentFigureObject.GetComponent<Rigidbody>().velocity = velocity;
					currentFigureObject.GetComponent<Rigidbody>().AddForce(new Vector3(0f, (0f - Physics.gravity.y) * currentFigureObject.GetComponent<Rigidbody>().mass, 0f));
					if (FigureTossed != null)
					{
						FigureTossed(currentFigureObject.GetComponent<ShopToyBox>());
					}
				}
			}
			if (flag14 && !flag5 && currentFigureObject != null && currentFigureObject.GetComponent<ShopToyBox>().inCart && initialTouchOnFigure && !currentFigureObject.GetComponent<ShopToyBox>().underGlass)
			{
				itemsInCartCount = Mathf.Max(0, itemsInCartCount - 1);
				SetItemCartSpot(currentFigureObject.GetComponent<ShopToyBox>().itemCartNumber, available: false);
				currentFigureObject.transform.parent = racksGroup.transform;
				currentFigureObject.GetComponent<Rigidbody>().isKinematic = true;
				currentFigureObject.GetComponent<ShopToyBox>().inCart = false;
				MoveBackToHanger(currentFigureObject);
				finishedMove = false;
				ShopUI.instance.changeText.text = (totalCurrency - TotalInCart()).ToString();
				if (itemsInCartCount <= 0)
				{
					ShopUI.instance.HideCheckOutButton();
				}
			}
		}
		else
		{
			if (!viewingFigure)
			{
				return;
			}
			rotateDelta = 0f;
			rotateDeltaX = 0f;
			bool flag17 = false;
			bool flag18 = true;
			if (currentFigureObject != null)
			{
				flag17 = currentFigureObject.GetComponent<ShopToyBox>().underGlass;
				flag18 = currentFigureObject.GetComponent<ShopToyBox>().viewing;
				if (currentFigureObject.GetComponent<ShopToyBox>().backOfBoxGroup != null && !currentFigureObject.GetComponent<ShopToyBox>().blindBox)
				{
					currentFigureObject.GetComponent<ShopToyBox>().backOfBoxGroup.SetActive(value: true);
				}
			}
			if (Input.GetMouseButtonDown(0))
			{
				inputStartPosition = Input.mousePosition;
				inputDownTime = Time.time;
				initialTouchOnFigure = false;
				if (currentFigureObject != null)
				{
					rotateStart = ShopCart.instance.rotatingTransform.localEulerAngles.y;
					rotateStartX = ShopCart.instance.rotatingTransform.localEulerAngles.x;
					Ray ray5 = Camera.main.ScreenPointToRay(Input.mousePosition);
					Debug.DrawRay(ray5.origin, ray5.direction * 100f, Color.red, 3f);
					if (Physics.Raycast(ray5, out var hitInfo3))
					{
						Transform transform3 = hitInfo3.collider.transform;
						if (transform3 == currentFigureObject.transform)
						{
							initialTouchOnFigure = true;
							GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figuretouched", GFAudioManager.SFX_GROUP, string.Empty);
							return;
						}
					}
				}
			}
			if (Input.GetMouseButton(0))
			{
				if (currentFigureObject != null)
				{
					Ray ray6 = Camera.main.ScreenPointToRay(Input.mousePosition);
					Ray ray7 = Camera.main.ScreenPointToRay(inputStartPosition);
					Vector3 vector5 = Camera.main.transform.position + ray6.direction * ((ShopCart.instance.rotatingTransform.position - ray6.origin).magnitude - 0.15f);
					Vector3 vector6 = Camera.main.transform.position + ray7.direction * ((ShopCart.instance.rotatingTransform.position - ray7.origin).magnitude - 0.15f);
					Vector3 vector7 = vector5 - ShopCart.instance.rotatingTransform.position;
					Vector3 to = vector6 - ShopCart.instance.rotatingTransform.position;
					vector7.Normalize();
					to.Normalize();
					Vector3 vector8 = new Vector3(vector7.y, vector7.z, 0f);
					Vector3 to2 = new Vector3(to.y, to.z, 0f);
					vector7.y = vector7.z;
					to.y = to.z;
					vector7.z = 0f;
					to.z = 0f;
					rotateDelta = Vector3.Angle(vector7, to);
					rotateDeltaX = Vector3.Angle(vector8, to2);
					if (ray6.origin.x > ray7.origin.x)
					{
						rotateDelta = 0f - rotateDelta;
					}
					if (ray6.origin.y < ray7.origin.y)
					{
						rotateDeltaX = ((!(rotateStart < 180f)) ? rotateDeltaX : (0f - rotateDeltaX));
					}
					else if (ray6.origin.y > ray7.origin.y)
					{
						rotateDeltaX = ((!(rotateStart < 180f)) ? (0f - rotateDeltaX) : rotateDeltaX);
					}
				}
				if (viewingFigure && finishedMove && finishedRotate && currentFigureObject != null && flag18)
				{
					Vector3 localEulerAngles = new Vector3(ShopCart.instance.rotatingTransform.localRotation.eulerAngles.x, ShopCart.instance.rotatingTransform.localRotation.eulerAngles.y, ShopCart.instance.rotatingTransform.localRotation.eulerAngles.z);
					localEulerAngles.y = rotateStart + rotateDelta;
					localEulerAngles.x = rotateStartX + rotateDeltaX;
					localEulerAngles.z = 0f;
					ShopCart.instance.rotatingTransform.localEulerAngles = localEulerAngles;
				}
			}
			if (!Input.GetMouseButtonUp(0) || !(currentFigureObject != null))
			{
				return;
			}
			bool flag19 = false;
			bool flag20 = false;
			bool flag21 = false;
			bool flag22 = false;
			bool flag23 = false;
			Vector3 mousePosition2 = Input.mousePosition;
			Vector3 vector9 = mousePosition2 - inputStartPosition;
			float y = ShopCart.instance.rotatingTransform.localEulerAngles.y;
			float y2 = 0f;
			if (vector9.magnitude > 30f * Screen.dpi / defaultDPI && Time.time - inputDownTime < 0.4f)
			{
				if (vector9.y > 90f * Screen.dpi / defaultDPI)
				{
					flag21 = true;
				}
				if (vector9.y < -70f * Screen.dpi / defaultDPI)
				{
					flag22 = true;
				}
				if (vector9.x > 0f && !flag22 && !flag21)
				{
					flag20 = true;
				}
				if (vector9.x < 0f && !flag22 && !flag21)
				{
					flag19 = true;
				}
			}
			else if (vector9.magnitude < 8f * Screen.dpi / defaultDPI && Time.time - inputDownTime < 0.13f)
			{
				flag23 = true;
			}
			if (flag23 && initialTouchOnFigure && !flag5)
			{
				finishedMove = false;
				finishedRotate = true;
				if (flag17)
				{
					if (CanMoveToCart(currentFigureObject) && !currentFigureObject.GetComponent<ShopToyBox>().viewing)
					{
						Vector3 currentPos = currentFigureObject.GetComponent<ShopToyBox>().glassObject.transform.localPosition;
						currentFigureObject.GetComponent<ShopToyBox>().glassObject.transform.DOLocalMove(new Vector3(currentPos.x, 0.16f, currentPos.z), 0.2f).SetEase(Ease.OutSine).OnComplete(() =>
						{
							currentFigureObject.GetComponent<ShopToyBox>().glassObject.transform.DOLocalMove(currentPos, 0.2f).SetEase(Ease.OutSine).SetDelay(0.2f);
							currentFigureObject.GetComponent<Rigidbody>().isKinematic = true;
							currentFigureObject.GetComponent<ShopToyBox>().inCart = true;
							itemsInCartCount = Mathf.Min(5, itemsInCartCount + 1);
							currentFigureObject.GetComponent<ShopToyBox>().itemCartNumber = GetAvailableItemCartSpot();
							MoveItemToCart(currentFigureObject);
							viewingFigure = false;
						});
					}
					else if (currentFigureObject.GetComponent<ShopToyBox>().viewing)
					{
						MoveBackToHanger(currentFigureObject);
					}
				}
				else
				{
					MoveBackToHanger(currentFigureObject);
				}
				return;
			}
			if ((flag19 || flag20) && finishedMove && flag18)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figureflipped", GFAudioManager.SFX_GROUP, string.Empty);
				y2 = ((!(startingRotation > 90f) || !(startingRotation <= 270f)) ? 180f : 0f);
				currentFigureObject.GetComponent<ShopToyBox>().HideAbilityInfoBox();
				finishedRotate = false;
				ShopCart.instance.rotatingTransform.DORotate(new Vector3(0f, y2, 0f), 0.4f).OnComplete(FinishedRotate);
				startingRotation = y2;
			}
			else if (flag18)
			{
				if (y > 90f && y <= 270f)
				{
					y2 = 180f;
				}
				ShopCart.instance.rotatingTransform.DORotate(new Vector3(0f, y2, 0f), 0.2f);
				startingRotation = y2;
			}
			if (flag22 && finishedMove && !flag6 && CanMoveToCart(currentFigureObject) && !currentFigureObject.GetComponent<ShopToyBox>().underGlass)
			{
				currentFigureObject.GetComponent<Rigidbody>().isKinematic = true;
				currentFigureObject.GetComponent<ShopToyBox>().inCart = true;
				itemsInCartCount = Mathf.Min(5, itemsInCartCount + 1);
				currentFigureObject.GetComponent<ShopToyBox>().itemCartNumber = GetAvailableItemCartSpot();
				MoveItemToCart(currentFigureObject);
				viewingFigure = false;
			}
			if (flag21 && finishedMove && !flag5 && !currentFigureObject.GetComponent<ShopToyBox>().underGlass)
			{
				finishedMove = false;
				finishedRotate = true;
				MoveBackToHanger(currentFigureObject);
			}
			if (!(currentFigureObject != null) || initialTouchOnFigure)
			{
				return;
			}
			Ray ray8 = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (!Physics.Raycast(ray8, out var hitInfo4))
			{
				return;
			}
			Transform transform4 = hitInfo4.collider.transform;
			if ((transform4.gameObject == ShopCart.instance.cartGroup || transform4.gameObject.name == "CartArrow") && CanMoveToCart(currentFigureObject) && !flag6)
			{
				CartArrowPressed();
			}
			else
			{
				if (!(transform4 != currentFigureObject.transform) || !((inputStartPosition - Input.mousePosition).magnitude < 10f * Screen.dpi / defaultDPI) || !(Input.mousePosition.y > 150f * defaultDPI / Screen.dpi) || initialTouchOnButton || flag5)
				{
					return;
				}
				finishedMove = false;
				finishedRotate = true;
				if (!flag17 || (flag17 && currentFigureObject.GetComponent<ShopToyBox>() != null && currentFigureObject.GetComponent<ShopToyBox>().viewing))
				{
					MoveBackToHanger(currentFigureObject);
					if (!flag17)
					{
						ShakeRacks();
					}
				}
				else if (flag17 && currentFigureObject != null && currentFigureObject.GetComponent<ShopToyBox>() != null && !currentFigureObject.GetComponent<ShopToyBox>().viewing)
				{
					FinishedHangerMove(currentFigureObject);
					ShopCart.instance.camera.transform.DOLocalMove(ShopCart.instance.mainCameraPostion.localPosition, 0.2f);
					ShopCart.instance.camera.transform.DOLocalRotate(ShopCart.instance.mainCameraPostion.localEulerAngles, 0.2f);
					if (itemsInCartCount > 0)
					{
						ShopUI.instance.DisplayCheckOutButton();
					}
				}
			}
		}
	}

	public void CartArrowPressed()
	{
		if (!(currentFigureObject != null) || !CanMoveToCart(currentFigureObject) || !viewingFigure)
		{
			return;
		}
		currentFigureObject.GetComponent<Rigidbody>().isKinematic = true;
		ShopToyBox currentFigShopToyBox = currentFigureObject.GetComponent<ShopToyBox>();
		currentFigShopToyBox.inCart = true;
		itemsInCartCount = Mathf.Min(5, itemsInCartCount + 1);
		currentFigShopToyBox.itemCartNumber = GetAvailableItemCartSpot();
		if (currentFigShopToyBox.underGlass && !currentFigShopToyBox.viewing)
		{
			if (currentFigShopToyBox.glassObject != null)
			{
				Vector3 currentPos = currentFigShopToyBox.glassObject.transform.localPosition;
				currentFigShopToyBox.glassObject.transform.DOLocalMove(new Vector3(currentPos.x, 0.16f, currentPos.z), 0.2f).SetEase(Ease.OutSine).OnComplete(() =>
				{
					currentFigShopToyBox.glassObject.transform.DOLocalMove(currentPos, 0.2f).SetEase(Ease.OutSine).SetDelay(0.2f);
					MoveItemToCart(currentFigureObject);
				});
			}
		}
		else
		{
			MoveItemToCart(currentFigureObject);
		}
		viewingFigure = false;
	}

	private bool CanMoveToCart(GameObject figureObject)
	{
		int num = figureObject.GetComponent<ShopToyBox>().Cost();
		if (num > Mathf.Max(0, totalCurrency - TotalInCart()))
		{
			string text = (isFlashSaleShop ? Language.Get("NOTENOUGHTICKETS", "MENUS,GUI") : Language.Get("NOTENOUGHCHANGE", "MENUS,GUI"));
			ShopUI.instance.DisplayMessage(text, new Color(1f, 0.16f, 0.16f));
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_modchipscreen_warning", GFAudioManager.SFX_GROUP, string.Empty);
			if (figureObject.GetComponent<ShopToyBox>().underGlass)
			{
				FinishedHangerMove(figureObject);
				ShopCart.instance.camera.transform.DOLocalMove(ShopCart.instance.mainCameraPostion.localPosition, 0.2f);
				ShopCart.instance.camera.transform.DOLocalRotate(ShopCart.instance.mainCameraPostion.localEulerAngles, 0.2f);
				if (itemsInCartCount > 0)
				{
					ShopUI.instance.DisplayCheckOutButton();
				}
			}
			return false;
		}
		if (itemsInCartCount >= 5)
		{
			ShopUI.instance.DisplayMessage(Language.Get("FULLCART", "MENUS,GUI"), Color.white);
			return false;
		}
		return true;
	}

	private void ShakeRacks()
	{
		if (shopType != ShopType.JailBird && shopType != ShopType.Zinthos && shopType != ShopType.BeachBums && shopType != ShopType.StickyJoe && shopType != ShopType.LexToys)
		{
			racksGroup.transform.DOShakePosition(0.4f, new Vector3(0.01f, 0f, 0.05f), 8).SetEase(Ease.OutBounce).SetDelay(0.2f);
		}
	}

	private IEnumerator ShowDarkness()
	{
		ShopCart.instance.darknessSprite.color = new Color(0f, 0f, 0f, 0f);
		yield return new WaitForEndOfFrame();
		ShopCart.instance.darknessSprite.gameObject.SetActive(value: true);
		float current = 0f;
		while (ShopCart.instance.darknessSprite.color.a < 0.4f)
		{
			current += 0.09f;
			ShopCart.instance.darknessSprite.color = new Color(0f, 0f, 0f, current);
			yield return new WaitForEndOfFrame();
		}
	}

	private IEnumerator HideDarkness()
	{
		yield return new WaitForEndOfFrame();
		float current = ShopCart.instance.darknessSprite.color.a;
		while (ShopCart.instance.darknessSprite.color.a > 0f)
		{
			current -= 0.09f;
			ShopCart.instance.darknessSprite.color = new Color(0f, 0f, 0f, current);
			yield return new WaitForEndOfFrame();
		}
		ShopCart.instance.darknessSprite.gameObject.SetActive(value: false);
	}

	public List<ShopToyBox> FiguresInCart()
	{
		List<ShopToyBox> list = new List<ShopToyBox>();
		foreach (GameObject allFigure in allFigures)
		{
			ShopToyBox component = allFigure.GetComponent<ShopToyBox>();
			if (component.inCart)
			{
				list.Add(component);
			}
		}
		return list;
	}

	private float RackCenter(int rackNumber)
	{
		float result = (float)rackNumber * shopSetup.rackSpacing;
		if (shopType == ShopType.JailBird)
		{
			if (rackNumber == 2)
			{
				result = 12.4f;
			}
			if (rackNumber > 2)
			{
				result = (float)rackNumber * shopSetup.rackSpacing + 6f;
			}
			return result;
		}
		return result;
	}

	public int TotalInCart()
	{
		int num = 0;
		foreach (GameObject allFigure in allFigures)
		{
			if (allFigure.GetComponent<ShopToyBox>().inCart)
			{
				num += allFigure.GetComponent<ShopToyBox>().Cost();
			}
		}
		return num;
	}

	public void ClearItemsPurchased()
	{
		for (int i = 0; i < allFigures.Count; i++)
		{
			GameObject gameObject = allFigures[i];
			if (gameObject.GetComponent<ShopToyBox>().inCart)
			{
				gameObject.GetComponent<ShopToyBox>().inCart = false;
				gameObject.SetActive(value: false);
			}
		}
		allFigures.TrimExcess();
		itemsInCartCount = 0;
		for (int j = 0; j < cartSpotHasItem.Length; j++)
		{
			cartSpotHasItem[j] = false;
		}
	}

	public bool HasNewFiguresInCart()
	{
		foreach (ShopToyBox item in FiguresInCart())
		{
			if (item != null)
			{
				if (!item.gameObject.activeInHierarchy)
				{
					Debug.LogErrorFormat(item, "Figure {0} is NOT active in the Hierarchy.", item.name);
				}
				MissionTypeConfigBuy missionBuyConfig = item.GetMissionBuyConfig();
				if (item.gameObject.activeInHierarchy && (item.IsNewFigure() || item.blindBox || (missionBuyConfig != null && missionBuyConfig.mintInBox)))
				{
					return true;
				}
			}
			else
			{
				Debug.LogError("Player has figure in cart that is NULL. HasNewFiguresInCart()");
			}
		}
		return false;
	}

	public ShopToyBox NextFigureToReveal()
	{
		List<ShopToyBox> list = FiguresInCart();
		for (int i = 0; i < list.Count; i++)
		{
			ShopToyBox shopToyBox = list[i];
			if (shopToyBox != null)
			{
				if (!shopToyBox.gameObject.activeInHierarchy)
				{
					Debug.LogErrorFormat(shopToyBox, "Figure {0} is NOT active in the Hierarchy. It will be skipped as the next figure to reveal", shopToyBox.name);
				}
				MissionTypeConfigBuy missionBuyConfig = shopToyBox.GetMissionBuyConfig();
				if (shopToyBox.gameObject.activeInHierarchy && (shopToyBox.IsNewFigure() || shopToyBox.blindBox || (missionBuyConfig != null && missionBuyConfig.mintInBox)))
				{
					return shopToyBox;
				}
				if (missionBuyConfig != null && !missionBuyConfig.mintInBox)
				{
					if (shopToyBox.toyType == ToyType.Figure)
					{
						Character character = new Character(shopToyBox.charDef.uid);
						character.Init(CharacterRoller.RollCharacterData(GameManager.instance.characterDatabase.GetCharacter(shopToyBox.charDef.uid), character.uid, shopToyBox.charDef.uid, 1));
						ShopUI.instance.missionFlag.CheckCharacterAdded(character);
					}
					else if (shopToyBox.toyType == ToyType.ModChip)
					{
						ShopUI.instance.missionFlag.CheckModChipAdded(shopToyBox.modchipSelectPortrait.modChip);
					}
				}
			}
			else
			{
				Debug.LogError("Player has figure in cart that is NULL. NextFigureToReveal()");
			}
		}
		return null;
	}

	public ShopToyBox GetFreeFigureToReveal()
	{
		freeFigureToReveal = false;
		NewsFlash newsFlash = GameManager.instance.communicatorDatabase.GetNewsFlash(newsFlashUID);
		newsFlashUID = -1;
		GameObject gameObject2;
		if (newsFlash != null)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(figureBoxPrefab);
			gameObject2 = gameObject;
			if (newsFlash.shopFigure.blindBox && newsFlash.shopFigure.blindBoxShopFigures.Count > 0)
			{
				int index = UnityEngine.Random.Range(0, newsFlash.shopFigure.blindBoxShopFigures.Count);
				newsFlash.shopFigure.characterUID = newsFlash.shopFigure.blindBoxShopFigures[index].characterUID;
				newsFlash.shopFigure.level = newsFlash.shopFigure.blindBoxShopFigures[index].level;
			}
			gameObject2.GetComponent<ShopToyBox>().figureData = newsFlash.shopFigure;
			gameObject2.GetComponent<ShopToyBox>().isGoldBox = newsFlash.shopFigure.goldBox;
			gameObject2.GetComponent<ShopToyBox>().blindBox = newsFlash.shopFigure.blindBox;
			gameObject2.GetComponent<ShopToyBox>().charDef = GameManager.instance.characterDatabase.GetCharacter(newsFlash.shopFigure.characterUID);
			allFigures.Add(gameObject2);
		}
		else
		{
			gameObject2 = allFigures[UnityEngine.Random.Range(0, allFigures.Count)];
		}
		gameObject2.GetComponent<Rigidbody>().isKinematic = true;
		gameObject2.GetComponent<ShopToyBox>().inCart = true;
		return gameObject2.GetComponent<ShopToyBox>();
	}

	private void MoveCartToRack(int rack)
	{
		finishedMove = false;
		float x = RackCenter(rack);
		ShopCart.instance.gameObject.transform.DOMove(new Vector3(x, ShopCart.instance.gameObject.transform.position.y, ShopCart.instance.gameObject.transform.position.z), 0.3f).OnComplete(FinishedMove);
		string[] array = new string[3] { "sfx_shop_cartscroll1", "sfx_shop_cartscroll2", "sfx_shop_cartscroll3" };
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/" + array[UnityEngine.Random.Range(0, array.Length)], GFAudioManager.SFX_GROUP, string.Empty);
	}

	private int GetAvailableItemCartSpot()
	{
		for (int i = 0; i < cartSpotHasItem.Length; i++)
		{
			if (!cartSpotHasItem[i])
			{
				cartSpotHasItem[i] = true;
				return i;
			}
		}
		return 0;
	}

	private void SetItemCartSpot(int spot, bool available)
	{
		if (spot >= 0 && spot < cartSpotHasItem.Length)
		{
			cartSpotHasItem[Mathf.Max(0, spot)] = available;
		}
	}

	private void PauseSimpleBob(ShopToyBox figureBox)
	{
		if (figureBox.boxGroup.GetComponent<SimpleBob>() != null)
		{
			figureBox.boxGroup.GetComponent<SimpleBob>().Pause();
		}
	}

	private void ResumeSimpleBob(ShopToyBox figureBox)
	{
		if (figureBox.boxGroup.GetComponent<SimpleBob>() != null)
		{
			figureBox.boxGroup.GetComponent<SimpleBob>().Resume();
		}
	}

	private void MoveItemToCart(GameObject figureObject, bool revealScreen = false)
	{
		int itemCartNumber = figureObject.GetComponent<ShopToyBox>().itemCartNumber;
		ShopUI.instance.HideBuyButton();
		int num = Mathf.Min(ShopCart.instance.figureCartPositions.Length - 1, Mathf.Max(0, itemCartNumber));
		Vector3 finalPosition = ShopCart.instance.figureCartPositions[num].position;
		Vector3 localEulerAngles = ShopCart.instance.figureCartPositions[num].localEulerAngles;
		finishedMove = false;
		figureObject.transform.parent = ShopCart.instance.cartGroup.transform;
		if (shopType == ShopType.Zinthos)
		{
			PauseSimpleBob(figureObject.GetComponent<ShopToyBox>());
		}
		if (revealScreen)
		{
			figureObject.transform.DOMove(finalPosition, 0.4f).OnComplete(() =>
			{
				finishedMove = true;
			});
		}
		else
		{
			if (figureObject.GetComponent<ShopToyBox>().underGlass && !figureObject.GetComponent<ShopToyBox>().viewing)
			{
				Vector3 endValue = new Vector3(figureObject.transform.position.x, figureObject.transform.position.y, finalPosition.z);
				figureObject.transform.DOMove(endValue, 0.3f).SetEase(Ease.Linear).OnComplete(() =>
				{
					figureObject.transform.DOMove(finalPosition, 0.08f).SetEase(Ease.OutSine).OnComplete(() =>
					{
						finishedMove = true;
						figureObject.GetComponent<ShopToyBox>().backOfBoxGroup.SetActive(value: false);
						if (figureObject.GetComponent<ShopToyBox>().figureBase != null)
						{
							figureObject.GetComponent<ShopToyBox>().figureBase.SetActive(value: false);
						}
					});
				});
			}
			else
			{
				figureObject.transform.DOMove(finalPosition, 0.8f).SetEase(Ease.OutBounce).OnComplete(() =>
				{
					finishedMove = true;
					figureObject.GetComponent<ShopToyBox>().backOfBoxGroup.SetActive(value: false);
					if (figureObject.GetComponent<ShopToyBox>().figureBase != null)
					{
						figureObject.GetComponent<ShopToyBox>().figureBase.SetActive(value: false);
					}
				});
			}
			ShopCart.instance.camera.transform.DOLocalMove(ShopCart.instance.mainCameraPostion.localPosition, 0.2f);
			ShopCart.instance.camera.transform.DOLocalRotate(ShopCart.instance.mainCameraPostion.localEulerAngles, 0.2f);
		}
		figureObject.transform.DORotate(localEulerAngles, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
		{
			if (!revealScreen)
			{
				ShopCart.instance.cartGroup.transform.DOShakePosition(0.3f, new Vector3(0.1f, 0.1f, 0f));
			}
		});
		figureObject.GetComponent<ShopToyBox>().viewing = false;
		if (!revealScreen)
		{
			ShopUI.instance.changeText.text = (totalCurrency - TotalInCart()).ToString();
			if (UnityEngine.Random.value < 0.5f)
			{
				CityCharacterVOSet characterVOSet = GameManager.instance.characterVODatabase.GetCharacterVOSet(CityCharacter.characterType);
				GFAudioManager.instance.PlayClipFromResources(characterVOSet.storeAddCartClipSet.GetRandom(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figuretocart", GFAudioManager.SFX_GROUP, string.Empty);
			ShopUI.instance.DisplayCheckOutButton();
		}
	}

	private void MoveCurrentFigureObjectToViewingPosition()
	{
		if (shopType == ShopType.Zinthos || shopType == ShopType.BeachBums || shopType == ShopType.StickyJoe || shopType == ShopType.LexToys || shopType == ShopType.MartianManhuntersShop)
		{
			PauseSimpleBob(currentFigureObject.GetComponent<ShopToyBox>());
			Vector3 endValue = ((!currentFigureObject.GetComponent<ShopToyBox>().isModChip) ? ShopCart.instance.viewingTransform.position : (ShopCart.instance.viewingTransform.position + new Vector3(0f, -0.392f, -0.279f)));
			currentFigureObject.transform.DOMove(endValue, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
			{
				FinishedViewingMove();
			});
			currentFigureObject.transform.DORotate(new Vector3(0f, 0f, 0f), 0.15f).SetEase(Ease.OutSine);
		}
		else
		{
			currentFigureObject.transform.DOMove(new Vector3(currentFigureObject.transform.position.x, currentFigureObject.transform.position.y, shopSetup.rackStartingZ + -3.1f), 0.15f / (float)currentFigureObject.GetComponent<ShopToyBox>().hangerSpot).SetEase(Ease.Linear).OnComplete(() =>
			{
				if (!currentFigureObject.GetComponent<ShopToyBox>().blindBox)
				{
					currentFigureObject.GetComponent<ShopToyBox>().backOfBoxGroup.SetActive(value: true);
				}
				Vector3 endValue2 = new Vector3(currentFigureObject.transform.position.x, currentFigureObject.transform.position.y + 0.26f, shopSetup.rackStartingZ + -3.6f);
				currentFigureObject.transform.DOMove(endValue2, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
				{
					Vector3 endValue3 = ((!currentFigureObject.GetComponent<ShopToyBox>().isModChip) ? ShopCart.instance.viewingTransform.position : (ShopCart.instance.viewingTransform.position + new Vector3(0f, -0.392f, -0.279f)));
					currentFigureObject.transform.DOMove(endValue3, 0.15f).SetEase(Ease.OutSine).OnComplete(() =>
					{
						FinishedViewingMove();
					});
					currentFigureObject.transform.DORotate(new Vector3(0f, 0f, 0f), 0.15f).SetEase(Ease.OutSine);
				});
			});
			currentFigureObject.transform.DORotate(new Vector3(14f, 0f, 0f), 0.15f).SetEase(Ease.OutSine);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figurezoomup", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void MoveToViewingPosition()
	{
		ShopUI.instance.HideCheckOutButton();
		if (currentFigureObject.GetComponent<ShopToyBox>().figureBase != null)
		{
			currentFigureObject.GetComponent<ShopToyBox>().figureBase.SetActive(value: true);
		}
		if (CurrentRack != currentFigureObject.GetComponent<ShopToyBox>().hangerRack)
		{
			int hangerRack = currentFigureObject.GetComponent<ShopToyBox>().hangerRack;
			ShopCart.instance.gameObject.transform.DOMove(new Vector3(RackCenter(hangerRack), ShopCart.instance.gameObject.transform.position.y, ShopCart.instance.gameObject.transform.position.z), 0.15f).OnComplete(MoveCurrentFigureObjectToViewingPosition);
			string[] array = new string[3] { "sfx_shop_cartscroll1", "sfx_shop_cartscroll2", "sfx_shop_cartscroll3" };
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/" + array[UnityEngine.Random.Range(0, array.Length)], GFAudioManager.SFX_GROUP, string.Empty);
		}
		else if (currentFigureObject.GetComponent<ShopToyBox>().underGlass)
		{
			ShopCart.instance.camera.transform.DOLocalMove(ShopCart.instance.glassViewingTransform.localPosition, 0.2f).OnComplete(() =>
			{
				FinishedViewingMove(showDarkness: false, glassView: true);
			});
			ShopCart.instance.camera.transform.DOLocalRotate(ShopCart.instance.glassViewingTransform.localEulerAngles, 0.2f);
		}
		else
		{
			MoveCurrentFigureObjectToViewingPosition();
		}
	}

	private void MoveFigureObjectToHanger(GameObject figureObject, Vector3 hangerPosition)
	{
		if (shopType == ShopType.Zinthos || shopType == ShopType.BeachBums || shopType == ShopType.StickyJoe || shopType == ShopType.LexToys)
		{
			figureObject.transform.DOMove(figureObject.GetComponent<ShopToyBox>().shelfPosition, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
			{
				FinishedHangerMove(figureObject);
			});
			figureObject.transform.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
			{
				figureObject.GetComponent<ShopToyBox>().backOfBoxGroup.SetActive(value: false);
				ResumeSimpleBob(figureObject.GetComponent<ShopToyBox>());
			});
		}
		else
		{
			figureObject.transform.DOMove(hangerPosition, 0.25f).SetEase(Ease.OutSine).OnComplete(() =>
			{
				figureObject.transform.DOMove(figureObject.GetComponent<ShopToyBox>().shelfPosition, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
				{
					FinishedHangerMove(figureObject);
				});
				figureObject.transform.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
				{
					figureObject.GetComponent<ShopToyBox>().backOfBoxGroup.SetActive(value: false);
				});
			});
			figureObject.transform.DORotate(new Vector3(14f, 0f, 0f), 0.25f).SetEase(Ease.OutSine);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figuresentback", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private void MoveBackToHanger(GameObject figureObject)
	{
		currentFigureObject = null;
		viewingFigure = false;
		figureObject.transform.DOKill();
		ShopToyBox figureBox = figureObject.GetComponent<ShopToyBox>();
		figureBox.moving = true;
		figureBox.HideAbilityInfoBox();
		ShopUI.instance.HideBuyButton();
		if (figureBox.underGlass)
		{
			if (CurrentRack != figureBox.hangerRack)
			{
				int hangerRack = figureBox.hangerRack;
				ShopCart.instance.gameObject.transform.DOMove(new Vector3(RackCenter(hangerRack), ShopCart.instance.gameObject.transform.position.y, ShopCart.instance.gameObject.transform.position.z), 0.15f).OnComplete(() =>
				{
					if (figureBox.underGlass && figureBox.glassObject != null)
					{
						Vector3 currentPos2 = figureBox.glassObject.transform.localPosition;
						figureBox.glassObject.transform.DOLocalMove(new Vector3(currentPos2.x, 0.16f, currentPos2.z), 0.2f).SetEase(Ease.OutSine).OnComplete(() =>
						{
							figureBox.glassObject.transform.DOLocalMove(currentPos2, 0.2f).SetEase(Ease.OutSine).SetDelay(0.2f);
							figureObject.transform.DOMove(figureBox.shelfPosition, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
							{
								FinishedHangerMove(figureObject);
							});
							figureObject.transform.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
							{
								figureBox.backOfBoxGroup.SetActive(value: false);
							});
							figureObject.transform.DORotate(new Vector3(14f, 0f, 0f), 0.25f).SetEase(Ease.OutSine);
							GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figuresentback", GFAudioManager.SFX_GROUP, string.Empty);
						});
					}
				});
				string[] array = new string[3] { "sfx_shop_cartscroll1", "sfx_shop_cartscroll2", "sfx_shop_cartscroll3" };
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/" + array[UnityEngine.Random.Range(0, array.Length)], GFAudioManager.SFX_GROUP, string.Empty);
			}
			else if (figureBox.underGlass && figureBox.glassObject != null)
			{
				Vector3 currentPos = figureBox.glassObject.transform.localPosition;
				figureBox.glassObject.transform.DOLocalMove(new Vector3(currentPos.x, 0.16f, currentPos.z), 0.2f).SetEase(Ease.OutSine).OnComplete(() =>
				{
					figureBox.glassObject.transform.DOLocalMove(currentPos, 0.2f).SetEase(Ease.OutSine).SetDelay(0.2f);
					figureObject.transform.DOMove(figureBox.shelfPosition, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
					{
						FinishedHangerMove(figureObject);
					});
					figureObject.transform.DORotate(Vector3.zero, 0.2f).OnComplete(() =>
					{
						figureBox.backOfBoxGroup.SetActive(value: false);
					});
					figureObject.transform.DORotate(new Vector3(14f, 0f, 0f), 0.25f).SetEase(Ease.OutSine);
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_shop_figuresentback", GFAudioManager.SFX_GROUP, string.Empty);
				});
			}
			if (itemsInCartCount > 0)
			{
				ShopUI.instance.DisplayCheckOutButton();
			}
			return;
		}
		Vector3 shelfPosition = figureBox.shelfPosition;
		Vector3 hangerPosition = new Vector3(shelfPosition.x, shelfPosition.y + 0.26f, shopSetup.rackStartingZ + -3.6f);
		if (CurrentRack != figureBox.hangerRack)
		{
			int hangerRack2 = figureBox.hangerRack;
			ShopCart.instance.gameObject.transform.DOMove(new Vector3(RackCenter(hangerRack2), ShopCart.instance.gameObject.transform.position.y, ShopCart.instance.gameObject.transform.position.z), 0.15f).OnComplete(() =>
			{
				MoveFigureObjectToHanger(figureObject, hangerPosition);
			});
			string[] array2 = new string[3] { "sfx_shop_cartscroll1", "sfx_shop_cartscroll2", "sfx_shop_cartscroll3" };
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/" + array2[UnityEngine.Random.Range(0, array2.Length)], GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			MoveFigureObjectToHanger(figureObject, hangerPosition);
		}
		if (itemsInCartCount > 0)
		{
			ShopUI.instance.DisplayCheckOutButton();
		}
	}

	private void FinishedHangerMove(GameObject figureObject)
	{
		figureObject.GetComponent<ShopToyBox>().CreateHingeJoint();
		viewingFigure = false;
		ShopUI.instance.HideBuyButton();
		finishedMove = true;
		figureObject.transform.parent = racksGroup.transform;
		figureObject.GetComponent<Rigidbody>().isKinematic = false;
		figureObject.GetComponent<ShopToyBox>().viewing = false;
		figureObject.GetComponent<ShopToyBox>().moving = false;
		if (figureObject.GetComponent<ShopToyBox>().figureBase != null)
		{
			figureObject.GetComponent<ShopToyBox>().figureBase.SetActive(value: false);
		}
		ShopUI.instance.changeText.text = (totalCurrency - TotalInCart()).ToString();
	}

	private void FinishedViewingMove(bool showDarkness = true, bool glassView = false)
	{
		viewingFigure = true;
		finishedMove = true;
		if (!showingDarkness && showDarkness)
		{
			showingDarkness = true;
			StopCoroutine("ShowDarkness");
			StopCoroutine("HideDarkness");
			StartCoroutine("ShowDarkness");
		}
		if (currentFigureObject != null)
		{
			currentFigureObject.transform.parent = ShopCart.instance.rotatingTransform;
			currentFigureObject.GetComponent<ShopToyBox>().viewing = !glassView;
			if (CoreGameTutorial.instance != null)
			{
				if (CoreGameTutorial.instance.preventBuyButtonDisplay())
				{
					ShopUI.instance.HideBuyButton();
				}
				else
				{
					ShopUI.instance.DisplayBuyButton();
				}
			}
			else
			{
				ShopUI.instance.DisplayBuyButton();
			}
			ShopUI.instance.UpdateCostText(currentFigureObject.GetComponent<ShopToyBox>().Cost().ToString());
		}
		ShopUI.instance.changeText.text = (totalCurrency - TotalInCart()).ToString();
	}

	private void FinishedMove()
	{
		finishedMove = true;
	}

	private void FinishedRotate()
	{
		finishedRotate = true;
	}

	private IEnumerator ShowDialogue(Conversation conversation, bool showExitButton = true)
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsintro", GFAudioManager.SFX_GROUP, string.Empty);
		DialogueTree dialogueSystem = ObjectPool.GetObjectFromResource<DialogueTree>("Prefabs/GUI/DialogueSystem", default, isActive: false);
		dialogueSystem.transform.parent = dialogueSystemParent;
		dialogueSystem.transform.localPosition = new Vector3(0f, 0f, -100f);
		dialogueSystem.gameObject.SetActive(value: true);
		dialogueSystem.Init(ShopUI.instance.GetComponent<Camera>(), conversation, null, null, showExitButton);
		while (dialogueSystem.gameObject.activeSelf)
		{
			yield return null;
			if (dialogueSystem == null)
			{
				break;
			}
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_charcloseupsoutro", GFAudioManager.SFX_GROUP, string.Empty);
	}

	public void AddSharedSpriteMaterial(Renderer stripeRenderer, Color stripeColor)
	{
		bool flag = false;
		for (int i = 0; i < sharedStripeMaterials.Count; i++)
		{
			Material material = sharedStripeMaterials[i];
			Color color = material.GetColor("_Color");
			if (stripeColor.CompareRGB(color))
			{
				flag = true;
			}
		}
		if (!flag)
		{
			stripeRenderer.material.SetColor("_Color", stripeColor);
			Material material2 = stripeRenderer.material;
			material2.name = material2.name.Replace("(Instance)", string.Empty);
			sharedStripeMaterials.Add(material2);
			stripeRenderer.sharedMaterial = material2;
		}
		else
		{
			Material sharedStripeColoredMaterial = GetSharedStripeColoredMaterial(stripeColor);
			if (sharedStripeColoredMaterial != null)
			{
				stripeRenderer.material = sharedStripeColoredMaterial;
			}
		}
	}

	private Material GetSharedStripeColoredMaterial(Color stripeColor)
	{
		for (int i = 0; i < sharedStripeMaterials.Count; i++)
		{
			Material material = sharedStripeMaterials[i];
			Color color = material.GetColor("_Color");
			if (color.CompareRGB(stripeColor))
			{
				return material;
			}
		}
		return null;
	}

	public void AddSharedFontMaterial(TextMeshPro textMesh, Color32 faceColor, Color32 outlineColor)
	{
		bool flag = false;
		for (int i = 0; i < sharedFontMaterialRef.Count; i++)
		{
			TextMeshPro textMeshPro = sharedFontMaterialRef[i];
			Color32 faceColor2 = textMeshPro.faceColor;
			Color32 outlineColor2 = textMeshPro.outlineColor;
			if (faceColor2.CompareRGB(faceColor) && outlineColor2.CompareRGB(outlineColor))
			{
				flag = true;
			}
		}
		if (!flag)
		{
			textMesh.faceColor = faceColor;
			textMesh.outlineColor = outlineColor;
			Material fontSharedMaterial = textMesh.fontSharedMaterial;
			fontSharedMaterial.name = fontSharedMaterial.name.Replace("(Instance)", string.Empty);
			textMesh.fontSharedMaterial = fontSharedMaterial;
			sharedFontMaterialRef.Add(textMesh);
		}
		else
		{
			TextMeshPro sharedFontRef = GetSharedFontRef(faceColor, outlineColor);
			if (sharedFontRef != null)
			{
				textMesh.fontSharedMaterial = sharedFontRef.fontSharedMaterial;
			}
		}
	}

	private TextMeshPro GetSharedFontRef(Color32 faceColor, Color32 outlineColor)
	{
		for (int i = 0; i < sharedFontMaterialRef.Count; i++)
		{
			TextMeshPro textMeshPro = sharedFontMaterialRef[i];
			Color32 faceColor2 = textMeshPro.faceColor;
			Color32 outlineColor2 = textMeshPro.outlineColor;
			if (faceColor2.CompareRGB(faceColor) && outlineColor2.CompareRGB(outlineColor))
			{
				return textMeshPro;
			}
		}
		return null;
	}

	private void PvpData_BattleRequestAccepted(BattleRequest battleRequest)
	{
		LegionOfDoomController.initBattleRequest = battleRequest;
		LoadingScreenCover.LoadScene("LegionOfDoomMode");
	}
}
