using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimpleFigureBox : MonoBehaviour, IPoolable
{
	public Transform figureTrans;

	public GameObject backOfBoxGroup;

	public tk2dSprite figureIconSprite;

	public tk2dSprite figureClassSprite;

	public TextMeshPro figureNameText;

	public TextMeshPro figureLvlText;

	public Renderer boxBacking;

	public Renderer boxClass;

	public Renderer boxCase;

	public Renderer boxColorFront;

	public Renderer boxColorBack;

	public Renderer boxPlasticFront;

	public GameObject frontOfBoxGroup;

	public TextMeshPro figureNameTextFront;

	public TextMeshPro figureLvlTextFront;

	public TextMeshPro figureLvlNumTextFront;

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

	[Header("Blind Boxes")]
	public GameObject[] blindBoxObjs;

	public Renderer[] blindBoxRenderers;

	private bool isBlindBox;

	private bool isGoldBlindBox;

	private CharacterDefinition _characterDefinition;

	private int _level = 1;

	public ShopFigureModel figure { get; private set; }

	public CharacterDefinition characterDefinition
	{
		get
		{
			return _characterDefinition;
		}
		set
		{
			Init(value, _level);
		}
	}

	public int level
	{
		get
		{
			return _level;
		}
		set
		{
			Init(_characterDefinition, value);
		}
	}

	public void Init(CharacterDefinition definition, int level = 1, bool isBlindBox = false, bool isGoldBlindBox = false)
	{
		_characterDefinition = definition;
		_level = level;
		this.isBlindBox = isBlindBox;
		this.isGoldBlindBox = isGoldBlindBox;
		Refresh();
	}

	private void Refresh()
	{
		if (figure != null)
		{
			Object.Destroy(figure.gameObject);
			figure = null;
		}
		if (_characterDefinition != null)
		{
			if (isBlindBox)
			{
				frontOfBoxGroup.SetActive(false);
				boxCase.gameObject.SetActive(false);
				boxClass.gameObject.SetActive(false);
				boxBacking.gameObject.SetActive(false);
				backOfBoxGroup.gameObject.SetActive(false);
				boxColorFront.gameObject.SetActive(false);
				boxColorBack.gameObject.SetActive(false);
				boxPlasticFront.gameObject.SetActive(false);
				int num = blindBoxObjs.Length;
				for (int i = 0; i < num; i++)
				{
					blindBoxObjs[i].SetActive(true);
				}
				num = blindBoxRenderers.Length;
				for (int j = 0; j < num; j++)
				{
					Renderer renderer = blindBoxRenderers[j];
					string text = string.Empty;
					if (isGoldBlindBox && !renderer.material.mainTexture.name.Contains("Gold"))
					{
						text = "Textures/BlindBox_GoldTexture-";
					}
					else if (!isGoldBlindBox && renderer.material.mainTexture.name.Contains("Gold"))
					{
						text = "Textures/BlindBox_Texture-";
					}
					Texture texture = null;
					if (!string.IsNullOrEmpty(text))
					{
						texture = Resources.Load<Texture>(text + tk2dSystem.CurrentPlatform);
					}
					if (texture != null)
					{
						renderer.material.mainTexture = texture;
					}
				}
			}
			else
			{
				frontOfBoxGroup.SetActive(true);
				boxCase.gameObject.SetActive(true);
				boxClass.gameObject.SetActive(true);
				boxBacking.gameObject.SetActive(true);
				backOfBoxGroup.gameObject.SetActive(true);
				boxColorFront.gameObject.SetActive(true);
				boxColorBack.gameObject.SetActive(true);
				boxPlasticFront.gameObject.SetActive(true);
				int num2 = blindBoxObjs.Length;
				for (int k = 0; k < num2; k++)
				{
					blindBoxObjs[k].SetActive(false);
				}
				boxBacking.material.SetColor("_Color", Color.white);
				boxClass.material.SetColor("_Color", Color.white);
				boxCase.material.SetColor("_Color", Color.white);
				boxColorFront.material.SetColor("_Color", _characterDefinition.shopStripeColor);
				boxColorBack.material.SetColor("_Color", _characterDefinition.shopStripeColor);
				GameObject gameObject = Object.Instantiate(Resources.Load("Boxes/tb_class_" + _characterDefinition.characterClass.ToString().ToLower() + "_mesh")) as GameObject;
				boxClass.GetComponent<MeshFilter>().mesh = gameObject.GetComponent<MeshFilter>().mesh;
				GameObject gameObject2 = Object.Instantiate(Resources.Load("Boxes/tb_" + _characterDefinition.characterRarity.ToString().ToLower() + "_case_mesh")) as GameObject;
				boxCase.GetComponent<MeshFilter>().mesh = gameObject2.GetComponent<MeshFilter>().mesh;
				GameObject gameObject3 = Object.Instantiate(Resources.Load("Boxes/tb_" + _characterDefinition.characterRarity.ToString().ToLower() + "_backing_mesh")) as GameObject;
				boxBacking.GetComponent<MeshFilter>().mesh = gameObject3.GetComponent<MeshFilter>().mesh;
				Object.Destroy(gameObject);
				Object.Destroy(gameObject2);
				Object.Destroy(gameObject3);
			}
			string shopFigurePath = _characterDefinition.shopFigurePath;
			if (!string.IsNullOrEmpty(shopFigurePath))
			{
				figureTrans.gameObject.SetActive(false);
				figure = ((GameObject)Object.Instantiate(Resources.Load(shopFigurePath))).GetComponent<ShopFigureModel>();
				figure.transform.SetParent(figureTrans.parent, true);
				figure.transform.localPosition = _characterDefinition.shopFigureOffset;
				figure.transform.localRotation = Quaternion.Euler(figureTrans.localRotation.eulerAngles + _characterDefinition.shopFigureRotationOffset);
				figure.transform.localScale = _characterDefinition.shopFigureScale;
				if (figure.figureBase != null)
				{
					figure.figureBase.SetActive(false);
				}
				figureNameTextFront.text = ((!string.IsNullOrEmpty(_characterDefinition.characterNameKey)) ? Language.Get(_characterDefinition.characterNameKey, "NPCS,FIGS") : _characterDefinition.characterName);
				figureLvlTextFront.text = Language.Get("LVL", "MENUS,GUI") + " \n <size=18>" + _level + "</size>";
				figureIconSprite.SetSprite(_characterDefinition.portraitSprite);
				figureNameText.text = ((!string.IsNullOrEmpty(_characterDefinition.characterNameKey)) ? Language.Get(_characterDefinition.characterNameKey, "NPCS,FIGS") : _characterDefinition.characterName);
				figureLvlText.text = _level.ToString();
				Character character = new Character(-1);
				character.Init(CharacterRoller.RollCharacterData(_characterDefinition, character.uid, _characterDefinition.uid, _level));
				healthStat.Init(character);
				attackStat.Init(character);
				dodgeStat.Init(character);
				luckStat.Init(character);
				List<AbilitySlot> orderedSlots = _characterDefinition.orderedSlots;
				AbilityDatabase abilityDatabase = GameManager.instance.abilityDatabase;
				if (orderedSlots.Count > 0)
				{
					AbilityData ability = abilityDatabase.GetAbility(orderedSlots[0].abilityId);
					SetAbilityButton((!character.abilities.Contains(ability)) ? null : ability, 1);
				}
				if (orderedSlots.Count > 1)
				{
					AbilityData ability = abilityDatabase.GetAbility(orderedSlots[1].abilityId);
					SetAbilityButton((!character.abilities.Contains(ability)) ? null : ability, 2);
				}
				if (orderedSlots.Count > 2)
				{
					AbilityData ability = abilityDatabase.GetAbility(orderedSlots[2].abilityId);
					SetAbilityButton((!character.abilities.Contains(ability)) ? null : ability, 3);
				}
				figureNameText.faceColor = _characterDefinition.shopTextFaceColor;
				figureNameText.outlineColor = _characterDefinition.shopTextOutlineColor;
			}
		}
		else
		{
			boxBacking.material.SetColor("_Color", Color.red);
			boxClass.material.SetColor("_Color", Color.red);
			boxCase.material.SetColor("_Color", Color.red);
			boxColorFront.material.SetColor("_Color", Color.red);
			boxColorBack.material.SetColor("_Color", Color.red);
		}
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

	public void ResetObject()
	{
		Init(null);
	}
}
