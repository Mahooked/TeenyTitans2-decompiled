using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ShopToyBox : MonoBehaviour
{
	public ToyType toyType;

	public Vector3 shelfPosition;

	public ShopFigure figureData;

	public bool inCart;

	public bool moving;

	public bool viewing;

	public bool isRevealBox;

	public int hangerRack;

	public int hangerNumber;

	public int hangerSpot;

	public bool underGlass;

	public bool hanging = true;

	public GameObject boxGroup;

	public GameObject glassObject;

	public Renderer[] figureRenderer;

	public int itemCartNumber;

	public Transform figureTrans;

	private Rigidbody rigidbody;

	private HingeJoint hingeJoint;

	private HingeJoint touchHingeJoint;

	private BoxCollider collider;

	public GameObject figure;

	public bool touched;

	public bool toyBoxInfront;

	public CharacterDefinition charDef;

	public bool didOverrideFigure;

	[Header("Back of the Box Properties")]
	public GameObject backOfBoxGroup;

	public tk2dSprite figureIconSprite;

	public tk2dSprite figureClassSprite;

	public TextMeshPro figureNameText;

	public TextMeshPro figureLvlText;

	public CharacterViewStat healthStat;

	public CharacterViewStat attackStat;

	public CharacterViewStat dodgeStat;

	public CharacterViewStat luckStat;

	public tk2dSprite abilityBGSprite1;

	public tk2dSprite abilityBGSprite2;

	public tk2dSprite abilityBGSprite3;

	public tk2dSprite abilitySprite1;

	public tk2dSprite abilitySprite2;

	public tk2dSprite abilitySprite3;

	public tk2dUIItem abilityButton1;

	public tk2dUIItem abilityButton2;

	public tk2dUIItem abilityButton3;

	[Header("Front of the Box Properties")]
	public GameObject frontOfBoxGroup;

	public TextMeshPro figureNameTextFront;

	public TextMeshPro figureLvlTextFront;

	public TextMeshPro figureLvlNumTextFront;

	public Transform boxBacking;

	public Transform boxCase;

	public Transform boxClass;

	public Transform boxColorFront;

	public Transform boxColorBack;

	public GameObject figureBase;

	[Space]
	public ModChipSelectPortrait modchipSelectPortrait;

	public GameObject modchipBronzeBox;

	public GameObject modchipSilverBox;

	public GameObject modchipGoldBox;

	public TextMeshPro modchipNameText;

	public TextMeshPro modchipDescText;

	public TextMeshPro modChipClassText;

	[Space]
	public AccessoryItemDefinition accessoryItemDefinition;

	public TextMeshPro accessoryNameText;

	public TextMeshPro accessoryShortDescText;

	public TextMeshPro accessoryLongDescText;

	[Space]
	public GameObject[] nonBlindBoxObjects;

	public GameObject[] blindBoxObjects;

	public bool blindBox;

	public bool previouslyBlindBox;

	public bool alreadyRevealed;

	private AbilityInfoBox infoBox;

	private CharacterStatInfoBox statInfoBox;

	private Camera statButtonCamera;

	private Character _rolledCharacter;

	public MeshRenderer[] blindBoxRenderers;

	public bool isGoldBox;

	public int blindBoxCost = 100;

	public bool forceIsNewFigure;

	public Character rolledCharacter
	{
		get
		{
			return _rolledCharacter;
		}
	}

	public bool isModChip
	{
		get
		{
			return toyType == ToyType.ModChip;
		}
	}

	public ShopToyBox()
	{
		hangerSpot = 0;
	}

	private void Awake()
	{
		shelfPosition = base.transform.position;
		hingeJoint = GetComponent<HingeJoint>();
		rigidbody = GetComponent<Rigidbody>();
		if (toyType == ToyType.Figure)
		{
			figureLvlTextFront.enableCulling = true;
			figureLvlNumTextFront.enableCulling = true;
			if (healthStat != null && healthStat.uiItem != null)
			{
				statButtonCamera = CameraUtility.GetCameraForLayer(1 << healthStat.uiItem.gameObject.layer);
			}
		}
	}

	private void OnEnable()
	{
		if (healthStat != null && healthStat.uiItem != null)
		{
			healthStat.uiItem.OnClickUIItem += HandleStatClick;
			healthStat.uiItem.OnDown += HandleButtonDown;
			healthStat.uiItem.OnRelease += HandleButtonRelease;
		}
		if (attackStat != null && attackStat.uiItem != null)
		{
			attackStat.uiItem.OnClickUIItem += HandleStatClick;
			attackStat.uiItem.OnDown += HandleButtonDown;
			attackStat.uiItem.OnRelease += HandleButtonRelease;
		}
		if (dodgeStat != null && dodgeStat.uiItem != null)
		{
			dodgeStat.uiItem.OnClickUIItem += HandleStatClick;
			dodgeStat.uiItem.OnDown += HandleButtonDown;
			dodgeStat.uiItem.OnRelease += HandleButtonRelease;
		}
		if (luckStat != null && luckStat.uiItem != null)
		{
			luckStat.uiItem.OnClickUIItem += HandleStatClick;
			luckStat.uiItem.OnDown += HandleButtonDown;
			luckStat.uiItem.OnRelease += HandleButtonRelease;
		}
		if (abilityButton1 != null)
		{
			abilityButton1.OnClickUIItem += HandleAbilityButtonClick;
			abilityButton1.OnDown += HandleButtonDown;
			abilityButton1.OnRelease += HandleButtonRelease;
		}
		if (abilityButton2 != null)
		{
			abilityButton2.OnClickUIItem += HandleAbilityButtonClick;
			abilityButton2.OnDown += HandleButtonDown;
			abilityButton2.OnRelease += HandleButtonRelease;
		}
		if (abilityButton3 != null)
		{
			abilityButton3.OnClickUIItem += HandleAbilityButtonClick;
			abilityButton3.OnDown += HandleButtonDown;
			abilityButton3.OnRelease += HandleButtonRelease;
		}
	}

	private void OnDisable()
	{
		if (healthStat != null && healthStat.uiItem != null)
		{
			healthStat.uiItem.OnClickUIItem -= HandleStatClick;
			healthStat.uiItem.OnDown -= HandleButtonDown;
			healthStat.uiItem.OnRelease -= HandleButtonRelease;
		}
		if (attackStat != null && attackStat.uiItem != null)
		{
			attackStat.uiItem.OnClickUIItem -= HandleStatClick;
			attackStat.uiItem.OnDown -= HandleButtonDown;
			attackStat.uiItem.OnRelease -= HandleButtonRelease;
		}
		if (dodgeStat != null && dodgeStat.uiItem != null)
		{
			dodgeStat.uiItem.OnClickUIItem -= HandleStatClick;
			dodgeStat.uiItem.OnDown -= HandleButtonDown;
			dodgeStat.uiItem.OnRelease -= HandleButtonRelease;
		}
		if (luckStat != null && luckStat.uiItem != null)
		{
			luckStat.uiItem.OnClickUIItem -= HandleStatClick;
			luckStat.uiItem.OnDown -= HandleButtonDown;
			luckStat.uiItem.OnRelease -= HandleButtonRelease;
		}
		if (abilityButton1 != null)
		{
			abilityButton1.OnClickUIItem -= HandleAbilityButtonClick;
			abilityButton1.OnDown -= HandleButtonDown;
			abilityButton1.OnRelease -= HandleButtonRelease;
		}
		if (abilityButton2 != null)
		{
			abilityButton2.OnClickUIItem -= HandleAbilityButtonClick;
			abilityButton2.OnDown -= HandleButtonDown;
			abilityButton2.OnRelease -= HandleButtonRelease;
		}
		if (abilityButton3 != null)
		{
			abilityButton3.OnClickUIItem -= HandleAbilityButtonClick;
			abilityButton3.OnDown -= HandleButtonDown;
			abilityButton3.OnRelease -= HandleButtonRelease;
		}
	}

	private void HandleButtonDown()
	{
		if (ShopController.instance != null)
		{
			ShopController.instance.initialTouchOnButton = true;
		}
	}

	private void HandleButtonRelease()
	{
		if (ShopController.instance != null)
		{
			ShopController.instance.initialTouchOnButton = false;
		}
	}

	private void HandleStatClick(tk2dUIItem button)
	{
		CharacterViewStat component = button.GetComponent<CharacterViewStat>();
		if (component != null)
		{
			ShowStatInfoBox(component);
		}
	}

	private void ShowStatInfoBox(CharacterViewStat stat)
	{
		if (!(Mathf.Abs(180f - base.transform.rotation.eulerAngles.y) > 5f))
		{
			stat.uiItem.enabled = false;
			if (statInfoBox == null)
			{
				statInfoBox = ObjectPool.GetObjectFromResource<CharacterStatInfoBox>("Prefabs/GUI/CharacterStatInfoBox_FigureBox");
			}
			statInfoBox.Init(stat);
			statInfoBox.transform.parent = stat.transform;
			statInfoBox.transform.localPosition = Vector3.zero;
			statInfoBox.transform.localRotation = Quaternion.identity;
			statInfoBox.transform.localScale = Vector3.one;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void HideStatInfoBox()
	{
		if (statInfoBox != null)
		{
			statInfoBox.characterViewStat.uiItem.enabled = true;
			statInfoBox.Init(null);
			ObjectPool.PoolObject(statInfoBox.gameObject);
			statInfoBox = null;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	private void MakeBlindBox()
	{
		if (blindBoxRenderers.Length > 0)
		{
			for (int i = 0; i < blindBoxRenderers.Length; i++)
			{
				MeshRenderer meshRenderer = blindBoxRenderers[i];
				if (isGoldBox)
				{
					if (!meshRenderer.material.mainTexture.name.Contains("Gold"))
					{
						Texture texture = Resources.Load<Texture>("Textures/BlindBox_GoldTexture-" + tk2dSystem.CurrentPlatform);
						if (texture != null)
						{
							meshRenderer.material.mainTexture = texture;
						}
					}
				}
				else if (meshRenderer.material.mainTexture.name.Contains("Gold"))
				{
					Texture texture2 = Resources.Load<Texture>("Textures/BlindBox_Texture-" + tk2dSystem.CurrentPlatform);
					if (texture2 != null)
					{
						meshRenderer.material.mainTexture = texture2;
					}
				}
			}
		}
		for (int j = 0; j < blindBoxObjects.Length; j++)
		{
			blindBoxObjects[j].SetActive(true);
		}
		for (int k = 0; k < nonBlindBoxObjects.Length; k++)
		{
			nonBlindBoxObjects[k].SetActive(false);
		}
	}

	private void RemoveBlindBox()
	{
		for (int i = 0; i < blindBoxObjects.Length; i++)
		{
			blindBoxObjects[i].SetActive(false);
		}
		for (int j = 0; j < nonBlindBoxObjects.Length; j++)
		{
			nonBlindBoxObjects[j].SetActive(true);
		}
	}

	public void UpdateBoxForCharacter(CharacterDefinition charDef, bool newFigureReveal = false)
	{
		if (toyType != ToyType.Figure)
		{
			return;
		}
		if (charDef == null)
		{
			boxBacking.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			boxClass.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			boxCase.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			boxColorFront.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			boxColorBack.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
			Debug.LogError("ShopToyBox.cs, UpdateBoxForCharacter(), character definition is NULL!", base.gameObject);
			return;
		}
		if (ShopController.instance != null)
		{
			ShopController.instance.AddSharedSpriteMaterial(boxColorFront.GetComponent<Renderer>(), charDef.shopStripeColor);
			ShopController.instance.AddSharedSpriteMaterial(boxColorBack.GetComponent<Renderer>(), charDef.shopStripeColor);
		}
		else
		{
			boxColorFront.GetComponent<Renderer>().material.SetColor("_Color", charDef.shopStripeColor);
			boxColorBack.GetComponent<Renderer>().material.SetColor("_Color", charDef.shopStripeColor);
		}
		_rolledCharacter = new Character(-1);
		_rolledCharacter.Init(CharacterRoller.RollCharacterData(charDef, _rolledCharacter.uid, figureData.characterUID, figureData.level));
		GameObject gameObject = Object.Instantiate(Resources.Load("Boxes/tb_class_" + charDef.characterClass.ToString().ToLower() + "_mesh")) as GameObject;
		boxClass.GetComponent<MeshFilter>().mesh = gameObject.GetComponent<MeshFilter>().mesh;
		GameObject gameObject2 = Object.Instantiate(Resources.Load("Boxes/tb_" + charDef.characterRarity.ToString().ToLower() + "_case_mesh")) as GameObject;
		boxCase.GetComponent<MeshFilter>().mesh = gameObject2.GetComponent<MeshFilter>().mesh;
		GameObject gameObject3 = Object.Instantiate(Resources.Load("Boxes/tb_" + charDef.characterRarity.ToString().ToLower() + "_backing_mesh")) as GameObject;
		boxBacking.GetComponent<MeshFilter>().mesh = gameObject3.GetComponent<MeshFilter>().mesh;
		Object.Destroy(gameObject);
		Object.Destroy(gameObject2);
		Object.Destroy(gameObject3);
		string shopFigurePath = charDef.shopFigurePath;
		if (string.IsNullOrEmpty(shopFigurePath))
		{
			shopFigurePath = GameManager.instance.characterDatabase.GetCharacter(0).shopFigurePath;
		}
		if (string.IsNullOrEmpty(shopFigurePath))
		{
			return;
		}
		figureTrans.gameObject.SetActive(false);
		if (figure != null)
		{
			Object.Destroy(figure);
		}
		figure = Object.Instantiate(Resources.Load(shopFigurePath)) as GameObject;
		figure.transform.parent = figureTrans.parent;
		figure.transform.localPosition = charDef.shopFigureOffset;
		figure.transform.localRotation = Quaternion.Euler(figureTrans.localRotation.eulerAngles + charDef.shopFigureRotationOffset);
		figure.transform.localScale = charDef.shopFigureScale;
		if (figure.GetComponent<ShopFigureModel>() != null)
		{
			figureBase = figure.GetComponent<ShopFigureModel>().figureBase;
			if (!newFigureReveal && figureBase != null)
			{
				figureBase.SetActive(false);
			}
		}
		StringBuilder text = new StringBuilder((!string.IsNullOrEmpty(charDef.characterNameKey)) ? Language.Get(charDef.characterNameKey, "NPCS,FIGS") : charDef.characterName);
		figureNameTextFront.SetText(text);
		if (Language.CurrentLanguage() != LanguageCode.RU && Language.CurrentLanguage() != LanguageCode.TR)
		{
			figureNameTextFront.ForceMeshUpdate();
		}
		figureLvlTextFront.text = Language.Get("LVL", "MENUS,GUI") + " \n <size=18>" + figureData.level + "</size>";
		if (ShopController.instance != null)
		{
			ShopController.instance.AddSharedFontMaterial(figureNameTextFront, charDef.shopTextFaceColor, charDef.shopTextOutlineColor);
			if (!isRevealBox)
			{
				StartCoroutine("DelayRecheckFontMaterial");
			}
		}
		if (!isRevealBox)
		{
			base.gameObject.name = base.gameObject.name.Replace("(Clone)", string.Empty);
			base.gameObject.name = base.gameObject.name + "( " + figureData.characterAssetName + " )";
			figureRenderer = figure.GetComponentsInChildren<Renderer>();
		}
		figureIconSprite.SetSprite(charDef.portraitSprite);
		figureNameText.text = ((!string.IsNullOrEmpty(charDef.characterNameKey)) ? Language.Get(charDef.characterNameKey, "NPCS,FIGS") : charDef.characterName);
		figureLvlText.text = figureData.level.ToString();
		figureData.cost = charDef.shopFigureCosts[Mathf.Max(0, figureData.level - 1)];
		figureData.ticketCost = charDef.shopFigureTicketCosts[Mathf.Max(0, figureData.level - 1)];
		healthStat.Init(_rolledCharacter);
		attackStat.Init(_rolledCharacter);
		dodgeStat.Init(_rolledCharacter);
		luckStat.Init(_rolledCharacter);
		List<AbilitySlot> orderedSlots = charDef.orderedSlots;
		AbilityDatabase abilityDatabase = GameManager.instance.abilityDatabase;
		if (orderedSlots.Count > 0)
		{
			AbilityData ability = abilityDatabase.GetAbility(orderedSlots[0].abilityId);
			SetAbilityButton((!_rolledCharacter.abilities.Contains(ability)) ? null : ability, 1);
		}
		if (orderedSlots.Count > 1)
		{
			AbilityData ability = abilityDatabase.GetAbility(orderedSlots[1].abilityId);
			SetAbilityButton((!_rolledCharacter.abilities.Contains(ability)) ? null : ability, 2);
		}
		if (orderedSlots.Count > 2)
		{
			AbilityData ability = abilityDatabase.GetAbility(orderedSlots[2].abilityId);
			SetAbilityButton((!_rolledCharacter.abilities.Contains(ability)) ? null : ability, 3);
		}
		figureNameText.faceColor = charDef.shopTextFaceColor;
		figureNameText.outlineColor = charDef.shopTextOutlineColor;
		this.charDef = charDef;
	}

	private void UpdateBoxForAccessory(AccessoryItemDefinition accessoryItemDefinition)
	{
		if (toyType != ToyType.Accessory)
		{
			return;
		}
		figure = Object.Instantiate(Resources.Load(accessoryItemDefinition.modelPath)) as GameObject;
		if (figure == null)
		{
			Debug.LogError("Shop Toy Box, UpdateBoxForAccessory(), Accessory Model is NULL", base.gameObject);
			return;
		}
		figure.transform.parent = figureTrans;
		figure.transform.localPosition = accessoryItemDefinition.shopOffsetPosition;
		figure.transform.localRotation = Quaternion.Euler(accessoryItemDefinition.shopRotation);
		figure.transform.localScale = accessoryItemDefinition.shopScale;
		Animator componentInChildren = figure.GetComponentInChildren<Animator>();
		if (componentInChildren != null)
		{
			componentInChildren.Play(accessoryItemDefinition.modelViewerAnimation);
			componentInChildren.speed = 0f;
		}
		this.accessoryItemDefinition = accessoryItemDefinition;
	}

	private void SetAbilityButton(AbilityData ability, int slot)
	{
		tk2dSprite obj;
		switch (slot)
		{
		case 1:
			obj = abilityBGSprite1;
			break;
		case 2:
			obj = abilityBGSprite2;
			break;
		default:
			obj = abilityBGSprite3;
			break;
		}
		tk2dSprite tk2dSprite2 = obj;
		tk2dSprite obj2;
		switch (slot)
		{
		case 1:
			obj2 = abilitySprite1;
			break;
		case 2:
			obj2 = abilitySprite2;
			break;
		default:
			obj2 = abilitySprite3;
			break;
		}
		tk2dSprite tk2dSprite3 = obj2;
		if (ability == null)
		{
			tk2dSprite2.SetSprite("TTG_battlebar_abilityicon_backing");
			tk2dSprite3.SetSprite("TTG_battlebar_abilityicon_lock");
			tk2dSprite3.scale = new Vector3(0.8f, 0.8f, 0.8f);
			return;
		}
		if (ability.castTime > 0f)
		{
			tk2dSprite2.SetSprite("TTG_battlebar_abilityicon_backing_cast");
		}
		else if (ability.numChargesRequired > 0)
		{
			tk2dSprite2.SetSprite("TTG_battlebar_abilityicon_backing_tripple");
		}
		else if (ability.channelingData.speed > 0f)
		{
			tk2dSprite2.SetSprite("TT2_battlebar_abilityicon_backing_channel");
		}
		else
		{
			tk2dSprite2.SetSprite("TTG_battlebar_abilityicon_backing");
		}
		tk2dSprite3.SetSprite(ability.iconSprite);
		tk2dSprite3.scale = Vector3.one;
	}

	private void HandleAbilityButtonClick(tk2dUIItem button)
	{
		if (!(Mathf.Abs(180f - base.transform.rotation.eulerAngles.y) > 5f))
		{
			if (infoBox == null)
			{
				infoBox = ObjectPool.GetObjectFromResource<AbilityInfoBox>("Prefabs/GUI/AbilityInfoBox_FigureBox");
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tooltips", GFAudioManager.SFX_GROUP, string.Empty);
			AbilityData abilityData = null;
			if (button == abilityButton1)
			{
				abilityData = GameManager.instance.abilityDatabase.GetAbility(charDef.orderedSlots[0].abilityId);
			}
			else if (button == abilityButton2)
			{
				abilityData = GameManager.instance.abilityDatabase.GetAbility(charDef.orderedSlots[1].abilityId);
			}
			else if (button == abilityButton3)
			{
				abilityData = GameManager.instance.abilityDatabase.GetAbility(charDef.orderedSlots[2].abilityId);
			}
			if (infoBox.abilityData == abilityData || !_rolledCharacter.abilities.Contains(abilityData))
			{
				HideAbilityInfoBox();
				return;
			}
			infoBox.Init(abilityData, _rolledCharacter);
			infoBox.transform.parent = button.transform;
			infoBox.transform.localPosition = Vector3.zero;
			infoBox.transform.localRotation = Quaternion.identity;
			infoBox.transform.localScale = Vector3.one;
		}
	}

	private IEnumerator DelayRecheckFontMaterial()
	{
		yield return new WaitForEndOfFrame();
		if (figureNameTextFront != null && charDef != null)
		{
			ShopController.instance.AddSharedFontMaterial(figureNameTextFront, charDef.shopTextFaceColor, charDef.shopTextOutlineColor);
			ShopController.instance.AddSharedFontMaterial(figureLvlTextFront, new Color(1f, 1f, 1f, 1f), new Color(0f, 0f, 0f, 1f));
		}
	}

	public void HideAbilityInfoBox()
	{
		if (infoBox != null)
		{
			infoBox.Init(null, null);
			ObjectPool.PoolObject(infoBox.gameObject);
			infoBox = null;
		}
	}

	public int Cost()
	{
		switch (toyType)
		{
		case ToyType.ModChip:
			return (!ShopController.isFlashSaleShop) ? modchipSelectPortrait.modChip.cost : modchipSelectPortrait.modChip.ticketCost;
		case ToyType.Accessory:
			return accessoryItemDefinition.cost;
		default:
			if (!ShopController.isFlashSaleShop)
			{
				int num = figureData.cost;
				if (figureData.overrideFigure && didOverrideFigure)
				{
					num = figureData.overrideNewCost;
				}
				if (blindBox || previouslyBlindBox)
				{
					num = blindBoxCost;
				}
				if (ShopController.instance != null && ShopController.instance.discount > 0 && ShopController.instance.discount < 100)
				{
					float num2 = Mathf.Max(0f, (float)num * (float)(100 - ShopController.instance.discount) / 100f);
					return (int)num2;
				}
				return num;
			}
			return figureData.ticketCost;
		}
	}

	private void Start()
	{
		if (toyType == ToyType.Figure)
		{
			charDef = GameManager.instance.characterDatabase.GetCharacter(figureData.characterUID);
			if (isRevealBox)
			{
				return;
			}
			UpdateBoxForCharacter(charDef);
			if (blindBox)
			{
				MakeBlindBox();
			}
			else
			{
				RemoveBlindBox();
			}
		}
		else if (toyType == ToyType.Accessory)
		{
			accessoryItemDefinition = (AccessoryItemDefinition)GameManager.instance.itemDatabase.GetItem(figureData.accessoryUID);
			UpdateBoxForAccessory(accessoryItemDefinition);
		}
		CreateHingeJoint();
	}

	private void Update()
	{
		if (statInfoBox != null && Input.GetMouseButtonUp(0))
		{
			Collider2D collider2D = Physics2D.OverlapPoint(statButtonCamera.ScreenToWorldPoint(Input.mousePosition));
			if (collider2D != null)
			{
				tk2dUIItem component = collider2D.GetComponent<tk2dUIItem>();
				if (component == null || (component != healthStat.uiItem && component != attackStat.uiItem && component != dodgeStat.uiItem && component != luckStat.uiItem))
				{
					HideStatInfoBox();
				}
			}
			else
			{
				HideStatInfoBox();
			}
		}
		if (isRevealBox)
		{
			return;
		}
		if (!inCart && !moving && rigidbody.angularVelocity.x < 1f && rigidbody.angularVelocity.x > -1f)
		{
			if (hanging && !touched)
			{
				if (base.transform.localEulerAngles.z > 90f && base.transform.localEulerAngles.z < 270f)
				{
					if (collider != null && base.transform.position.Equals(shelfPosition))
					{
						collider.enabled = false;
					}
				}
				else if (collider != null)
				{
					collider.enabled = true;
				}
			}
			else if (collider != null)
			{
				collider.enabled = true;
			}
			float y = ((toyType != ToyType.ModChip) ? (-1.7f) : (-1f));
			float num = 0.4f;
			Ray ray = new Ray(base.transform.position + base.transform.rotation * new Vector3(0f - num, y, -0.1f), new Vector3(0f, 0f, -1f));
			Ray ray2 = new Ray(base.transform.position + base.transform.rotation * new Vector3(num, y, -0.1f), new Vector3(0f, 0f, -1f));
			RaycastHit hitInfo;
			bool flag = Physics.Raycast(ray, out hitInfo, 1.2f, LayerMask.GetMask("Character"));
			RaycastHit hitInfo2;
			bool flag2 = Physics.Raycast(ray2, out hitInfo2, 1.2f, LayerMask.GetMask("Character"));
			bool flag3 = false;
			if (!touched && flag && flag2)
			{
				Transform transform = hitInfo.collider.transform;
				if (transform != null && transform != base.transform && transform.gameObject.name.Contains("Box"))
				{
					flag3 = true;
					toyBoxInfront = true;
				}
				Transform transform2 = hitInfo2.collider.transform;
				if (transform2 != null && transform2 != base.transform && transform2.gameObject.name.Contains("Box"))
				{
					flag3 = true;
					toyBoxInfront = true;
				}
			}
			else
			{
				toyBoxInfront = false;
			}
			if (flag3)
			{
				HideRenderers();
				if (backOfBoxGroup != null && backOfBoxGroup.activeSelf)
				{
					backOfBoxGroup.SetActive(false);
				}
				if (frontOfBoxGroup != null && frontOfBoxGroup.activeSelf)
				{
					frontOfBoxGroup.SetActive(false);
				}
			}
			else
			{
				ShowRenderers();
				if (frontOfBoxGroup != null && !blindBox && !frontOfBoxGroup.activeSelf)
				{
					frontOfBoxGroup.SetActive(true);
				}
			}
		}
		else
		{
			ShowRenderers();
		}
		if (toyType != ToyType.ModChip && backOfBoxGroup != null)
		{
			if (base.transform.localEulerAngles.z > 15f && base.transform.localEulerAngles.z < 345f)
			{
				float z = ((!touched) ? (-0.01f) : (-0.011f));
				backOfBoxGroup.transform.parent.localPosition = new Vector3(0f, 0f, z);
			}
			else
			{
				backOfBoxGroup.transform.parent.localPosition = Vector3.zero;
			}
		}
		if (base.transform.position.y < -20f)
		{
			if (ShopController.instance != null)
			{
				ShopController.instance.allFigures.Remove(base.gameObject);
			}
			if (CoreGameTutorial.instance != null)
			{
				CoreGameTutorial.instance.NotifyHideToyBox(charDef);
			}
			base.gameObject.SetActive(false);
		}
	}

	public bool CanSelectBox()
	{
		return blindBox || frontOfBoxGroup.activeSelf;
	}

	private void FixedUpdate()
	{
		if (isRevealBox)
		{
			return;
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Debug.Break();
		}
		bool flag = false;
		if (DialogueTree.instance != null && DialogueTree.instance.gameObject.activeInHierarchy)
		{
			return;
		}
		if (CoreGameTutorial.instance != null)
		{
			flag = CoreGameTutorial.instance.PreventShopToyBoxMovement();
		}
		if (ShopController.instance != null)
		{
			flag = ShopController.shopType == ShopType.Zinthos;
		}
		if (touched && !flag && !underGlass)
		{
			if (touchHingeJoint != null && hingeJoint != null)
			{
				Vector3 mousePosition = Input.mousePosition;
				mousePosition.z = 0f - (Camera.main.transform.position.z - base.transform.position.z);
				Vector3 vector = Camera.main.ScreenToWorldPoint(mousePosition);
				vector.z = hingeJoint.connectedAnchor.z;
				Vector3 normalized = (hingeJoint.connectedAnchor - vector).normalized;
				Vector3 connectedAnchor = hingeJoint.connectedAnchor + new Vector3(1.4f * (0f - normalized.x), 1.4f * (0f - normalized.y), 0f);
				touchHingeJoint.connectedAnchor = connectedAnchor;
			}
			else if (touchHingeJoint != null && !hanging)
			{
				Vector3 mousePosition2 = Input.mousePosition;
				mousePosition2.z = 0f - (Camera.main.transform.position.z - base.transform.position.z);
				Vector3 connectedAnchor2 = Camera.main.ScreenToWorldPoint(mousePosition2);
				connectedAnchor2.z = touchHingeJoint.connectedAnchor.z;
				touchHingeJoint.connectedAnchor = connectedAnchor2;
			}
			if (rigidbody.IsSleeping())
			{
				rigidbody.WakeUp();
			}
		}
	}

	private void HideRenderers()
	{
		if (toyType == ToyType.ModChip)
		{
			if (modchipSelectPortrait.gameObject.activeSelf)
			{
				modchipSelectPortrait.gameObject.SetActive(false);
			}
		}
		else if (figureRenderer != null)
		{
			for (int i = 0; i < figureRenderer.Length; i++)
			{
				figureRenderer[i].enabled = false;
			}
		}
	}

	private void ShowRenderers()
	{
		if (toyType == ToyType.ModChip)
		{
			modchipSelectPortrait.gameObject.SetActive(true);
		}
		else if (figureRenderer != null)
		{
			for (int i = 0; i < figureRenderer.Length; i++)
			{
				figureRenderer[i].enabled = true;
			}
		}
	}

	public bool IsNewFigure()
	{
		if (forceIsNewFigure)
		{
			return true;
		}
		if (toyType == ToyType.Accessory && !alreadyRevealed)
		{
			return !Player.instance.HasAcquiredAccessory(accessoryItemDefinition.uid);
		}
		if (toyType == ToyType.ModChip && !alreadyRevealed)
		{
			return !Player.instance.HasAcquiredItem(modchipSelectPortrait.modChip);
		}
		return !Player.instance.HasAcquiredCharacter(figureData.characterUID);
	}

	public bool IsMissionFigure()
	{
		if (blindBox || !base.gameObject.activeInHierarchy)
		{
			return false;
		}
		return charDef != null && CheckIsMissionFigure(charDef.uid, _rolledCharacter.level);
	}

	public MissionTypeConfigBuy GetMissionBuyConfig()
	{
		if (blindBox || !base.gameObject.activeInHierarchy)
		{
			return null;
		}
		if (charDef != null)
		{
			for (int i = 0; i < Player.instance.activeMissions.Count; i++)
			{
				MissionData missionData = Player.instance.activeMissions[i];
				if (missionData == null || missionData.completed)
				{
					continue;
				}
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
				if (!(mission != null) || mission.sideType != MissionSideType.Buy)
				{
					continue;
				}
				for (int j = 0; j < mission.buyConfigs.Length; j++)
				{
					MissionTypeConfigBuy missionTypeConfigBuy = mission.buyConfigs[j];
					if (missionTypeConfigBuy.toyType == ToyType.Figure && missionData.counts[j] < missionTypeConfigBuy.count && missionTypeConfigBuy.figureConfig.IsCharacterValid(charDef.uid, _rolledCharacter.level))
					{
						return missionTypeConfigBuy;
					}
				}
			}
		}
		else if (isModChip)
		{
			for (int k = 0; k < Player.instance.activeMissions.Count; k++)
			{
				MissionData missionData2 = Player.instance.activeMissions[k];
				if (missionData2 == null || missionData2.completed)
				{
					continue;
				}
				Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(missionData2.uid);
				if (!(mission2 != null) || mission2.sideType != MissionSideType.Buy)
				{
					continue;
				}
				for (int l = 0; l < mission2.buyConfigs.Length; l++)
				{
					MissionTypeConfigBuy missionTypeConfigBuy2 = mission2.buyConfigs[l];
					if (missionTypeConfigBuy2.toyType == ToyType.ModChip && missionData2.counts[l] < missionTypeConfigBuy2.count && (!missionTypeConfigBuy2.boolValue1 || modchipSelectPortrait.modChip.uid == missionTypeConfigBuy2.intValue1))
					{
						return missionTypeConfigBuy2;
					}
				}
			}
		}
		return null;
	}

	public static bool CheckIsMissionFigure(int charUID, int level)
	{
		CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(charUID);
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData == null || missionData.completed)
			{
				continue;
			}
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (!(mission != null) || mission.sideType != MissionSideType.Buy)
			{
				continue;
			}
			for (int j = 0; j < mission.buyConfigs.Length; j++)
			{
				MissionTypeConfigBuy missionTypeConfigBuy = mission.buyConfigs[j];
				if (missionTypeConfigBuy.toyType == ToyType.Figure && missionData.counts[j] < missionTypeConfigBuy.count && missionTypeConfigBuy.figureConfig.IsCharacterValid(charUID, level))
				{
					return true;
				}
			}
		}
		return false;
	}

	public void TouchedBox()
	{
		touched = true;
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = 0f - (Camera.main.transform.position.z - base.transform.position.z);
		CreateTouchJoint(Camera.main.ScreenToWorldPoint(mousePosition));
	}

	public void UnTouchedBox()
	{
		touched = false;
		if (touchHingeJoint != null)
		{
			Object.Destroy(touchHingeJoint);
		}
	}

	private void CreateTouchJoint(Vector3 position)
	{
		if (!inCart && (!(hingeJoint == null) || !hanging))
		{
			if (touchHingeJoint == null)
			{
				touchHingeJoint = base.gameObject.AddComponent<HingeJoint>();
			}
			if (collider == null)
			{
				collider = base.gameObject.GetComponent<BoxCollider>();
			}
			touchHingeJoint.autoConfigureConnectedAnchor = false;
			position.z = ((!hanging) ? base.transform.localPosition.z : hingeJoint.connectedAnchor.z);
			touchHingeJoint.useMotor = hanging;
			touchHingeJoint.useSpring = hanging;
			Vector3 anchor = base.transform.InverseTransformPoint(position);
			anchor.z = 0f;
			touchHingeJoint.anchor = anchor;
			touchHingeJoint.connectedAnchor = position;
			touchHingeJoint.axis = new Vector3(0f, 0f, 1f);
			touchHingeJoint.enableCollision = false;
			if (!hanging)
			{
				touchHingeJoint.breakForce = 6000f;
				touchHingeJoint.breakTorque = 6000f;
				touchHingeJoint.useLimits = true;
				JointLimits limits = new JointLimits
				{
					min = 0f,
					max = 180f,
					bounceMinVelocity = 10000f,
					contactDistance = 0.0001f,
					bounciness = 0f
				};
				touchHingeJoint.limits = limits;
			}
			else
			{
				JointLimits limits2 = new JointLimits
				{
					min = 0f,
					max = 180f,
					bounceMinVelocity = 10000f,
					contactDistance = 0.0001f,
					bounciness = 0f
				};
				touchHingeJoint.limits = limits2;
			}
		}
	}

	public void CreateHingeJoint()
	{
		if (hanging)
		{
			if (hingeJoint == null)
			{
				hingeJoint = base.gameObject.AddComponent<HingeJoint>();
			}
			rigidbody.velocity = Vector3.zero;
			rigidbody.angularVelocity = Vector3.zero;
			rigidbody.isKinematic = false;
			hingeJoint.connectedAnchor = shelfPosition;
			hingeJoint.axis = new Vector3(0f, 0f, 1f);
			JointLimits limits = new JointLimits
			{
				min = 0f,
				max = 180f,
				bounceMinVelocity = 10000f,
				contactDistance = 0.0001f,
				bounciness = 0f
			};
			hingeJoint.limits = limits;
			hingeJoint.useMotor = true;
		}
	}

	public void DestroyHingeJoint(bool makeKinematic = true, bool destroyJoint = true)
	{
		if (hingeJoint != null && destroyJoint)
		{
			Object.Destroy(hingeJoint);
		}
		UnTouchedBox();
		rigidbody.isKinematic = makeKinematic;
	}

	public void RandomShakeForce()
	{
		rigidbody.AddForceAtPosition(new Vector3(Random.Range(-8f, 8f), Random.Range(-8f, 8f), 0f), base.transform.position - new Vector3(0f, -1.5f, 0f));
	}
}
