using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TheLibraryCharacterView : MonoBehaviour
{
	public enum ViewType
	{
		Regular,
		FromCharacterView
	}

	private static Dictionary<CharacterClass, string> classIconSpriteNames = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"menu_tabs_classbutton_beast"
		},
		{
			CharacterClass.Cute,
			"menu_tabs_classbutton_cute"
		},
		{
			CharacterClass.DarkArts,
			"menu_tabs_classbutton_darkarts"
		},
		{
			CharacterClass.MartialArts,
			"menu_tabs_classbutton_martialarts"
		},
		{
			CharacterClass.Super,
			"menu_tabs_classbutton_super"
		},
		{
			CharacterClass.Tech,
			"menu_tabs_classbutton_tech"
		},
		{
			CharacterClass.None,
			"menu_tabs_classbutton_none"
		}
	};

	public tk2dSlicedSpriteHelper backingSprite;

	public TextMeshPro nameText;

	public TextMeshPro libraryNumberText;

	public TextMeshPro classText;

	public tk2dSprite classIcon;

	public TextMeshPro descriptionText;

	public ModelViewer modelViewer;

	public ModelViewerCamera modelViewerCamera;

	public tk2dUIItem leftArrow;

	public tk2dUIItem rightArrow;

	private ShopFigureModel shopFigure;

	private Renderer[] shopFigureRenderers;

	private List<CharacterDefinition> allCharacters;

	private CharacterDefinition _character;

	public CharacterDefinition character
	{
		get
		{
			return _character;
		}
		set
		{
			Init(value);
		}
	}

	private void OnEnable()
	{
		modelViewer.xSpeed = 75f;
		modelViewer.ySpeed = 75f;
		modelViewer.useHitArea = false;
		modelViewer.autoRotate = false;
		modelViewerCamera.zoomEnabled = false;
		modelViewerCamera.targetOffset.y = 0.6f;
		modelViewerCamera.targetOffset.z = 0.68f;
		leftArrow.OnClickUIItem += HandleArrowClicked;
		rightArrow.OnClickUIItem += HandleArrowClicked;
		if (_character != null)
		{
			UpdateView();
		}
	}

	private void OnDisable()
	{
		leftArrow.OnClickUIItem -= HandleArrowClicked;
		rightArrow.OnClickUIItem -= HandleArrowClicked;
		Init(null);
	}

	private void Start()
	{
		InitAllCharactersList();
	}

	private void InitAllCharactersList()
	{
		allCharacters = GameManager.instance.characterDatabase.characters.FindAll((CharacterDefinition obj) => obj.isActive && (!obj.isSecret || Player.instance.HasAcquiredCharacter(obj)));
		allCharacters.Sort((CharacterDefinition x, CharacterDefinition y) =>
		{
			if (x.isSecret == y.isSecret)
			{
				return x.libraryNumber.CompareTo(y.libraryNumber);
			}
			return x.isSecret ? 1 : (-1);
		});
	}

	public void Init(CharacterDefinition character, ViewType viewType = ViewType.Regular, int characterFigID = -1)
	{
		if (character == _character && viewType != ViewType.FromCharacterView)
		{
			return;
		}
		if (allCharacters == null)
		{
			InitAllCharactersList();
		}
		RemoveFigure();
		_character = character;
		if (_character != null)
		{
			if (!SaveUtility.HasKey(character.characterName + "Viewed") && Player.instance.HasAcquiredCharacter(character))
			{
				SaveUtility.SetInt(character.characterName + "Viewed", 1, save: false);
				AchievementsDatabase.UnlockAchievementId("TT2_DOYOURHOMEWORK", 1);
			}
			leftArrow.gameObject.SetActive(viewType == ViewType.Regular);
			rightArrow.gameObject.SetActive(viewType == ViewType.Regular);
			UpdateView();
			ShowFigure(viewType, characterFigID);
			EventMessenger.Instance.Raise(new ViewedCharacterInLibraryEvent(character.uid));
		}
	}

	private void UpdateView()
	{
		if (Player.instance.charactersAcquired.Contains(_character.uid))
		{
			nameText.text = Language.Get(_character.characterNameKey, "NPCS,FIGS");
			libraryNumberText.text = "#" + _character.libraryNumber;
			classText.text = Language.Get("CLASS_" + _character.characterClass.ToString().Replace(" ", string.Empty).ToUpper(), "MENUS,GUI");
			classIcon.gameObject.SetActive(value: true);
			classIcon.SetSprite(classIconSpriteNames[_character.characterClass]);
			descriptionText.gameObject.SetActive(value: true);
			descriptionText.text = Language.Get(_character.libraryDescriptionKey, "NPCS,FIGS");
		}
		else
		{
			nameText.text = "??????";
			libraryNumberText.text = "#??";
			classText.text = "??????";
			classIcon.gameObject.SetActive(value: false);
			descriptionText.gameObject.SetActive(value: false);
		}
		if (nameText.isActiveAndEnabled)
		{
			nameText.ForceMeshUpdate();
			libraryNumberText.ForceMeshUpdate();
			Bounds bounds = nameText.bounds;
			Bounds bounds2 = libraryNumberText.bounds;
			float num = bounds.extents.x + bounds.extents.x + bounds2.extents.x + bounds2.extents.x + 10f;
			float num2 = nameText.transform.localPosition.x + bounds.center.x - bounds.extents.x;
			float num3 = nameText.transform.localPosition.x - num2;
			Vector3 localPosition = nameText.transform.localPosition;
			localPosition.x = num * -0.5f + num3;
			nameText.transform.localPosition = localPosition;
			localPosition = libraryNumberText.transform.localPosition;
			localPosition.x = nameText.transform.localPosition.x + 145f;
			libraryNumberText.transform.localPosition = localPosition;
			classText.ForceMeshUpdate();
			Bounds bounds3 = classText.bounds;
			num = bounds3.extents.x + bounds3.extents.x + 35f;
			num2 = classText.transform.localPosition.x + bounds3.center.x - bounds3.extents.x;
			num3 = classText.transform.localPosition.x - num2;
			localPosition = classText.transform.localPosition;
			localPosition.x = num * -0.5f + num3;
			classText.transform.localPosition = localPosition;
			localPosition = classIcon.transform.localPosition;
			localPosition.x = classText.transform.localPosition.x + 70f;
			classIcon.transform.localPosition = localPosition;
		}
	}

	private void ShowFigure(ViewType viewType = ViewType.Regular, int characterFigID = -1)
	{
		if (shopFigure != null)
		{
			if (characterFigID >= 0)
			{
				RepaintFigureModel(shopFigure, characterFigID);
			}
			else
			{
				shopFigure.UpdateTextureToOriginalTexture();
			}
			return;
		}
		shopFigure = ObjectPool.GetObjectFromResource<ShopFigureModel>(_character.shopFigurePath);
		if (!(shopFigure != null))
		{
			return;
		}
		shopFigure.transform.parent = modelViewer.transform;
		if (viewType == ViewType.Regular)
		{
			shopFigure.transform.localPosition = new Vector3(0f, -1.075f, 0f);
		}
		else
		{
			shopFigure.transform.localPosition = new Vector3(0f, -0.86f, 0f);
			shopFigure.transform.DOLocalMove(new Vector3(0f, -1.075f, 0f), 0.25f).SetEase(Ease.OutSine);
		}
		shopFigure.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
		shopFigure.transform.localScale = Vector3.one * 0.9f;
		shopFigureRenderers = shopFigure.GetComponentsInChildren<Renderer>(includeInactive: true);
		if (Player.instance.charactersAcquired.Contains(_character.uid))
		{
			int num = shopFigureRenderers.Length;
			for (int i = 0; i < num; i++)
			{
				shopFigureRenderers[i].material.color = Color.white;
			}
		}
		else
		{
			if (shopFigure.figureFace != null)
			{
				shopFigure.figureFace.SetActive(value: false);
			}
			int num2 = shopFigureRenderers.Length;
			for (int j = 0; j < num2; j++)
			{
				shopFigureRenderers[j].material.color = Color.black;
			}
		}
		shopFigure.character = Player.instance.GetCharacter(characterFigID);
		if (characterFigID >= 0)
		{
			RepaintFigureModel(shopFigure, characterFigID);
		}
		else
		{
			shopFigure.UpdateTextureToOriginalTexture();
		}
	}

	private void RepaintFigureModel(ShopFigureModel model, int characterFigID)
	{
		if (!(model != null) || model.character == null)
		{
			return;
		}
		int repaintColorPreferenceForCharacterUID = Player.instance.GetRepaintColorPreferenceForCharacterUID(characterFigID);
		if (repaintColorPreferenceForCharacterUID <= 0)
		{
			return;
		}
		RepaintColorSlot repaintColorSlotForID = model.character.characterDefinition.GetRepaintColorSlotForID(repaintColorPreferenceForCharacterUID);
		if (repaintColorSlotForID != null)
		{
			string texturePathScaled = repaintColorSlotForID.texturePathScaled;
			Texture texture = Resources.Load<Texture>(texturePathScaled);
			if (texture != null)
			{
				model.UpdateTexture(texture);
			}
		}
	}

	private void RemoveFigure()
	{
		if (shopFigure != null)
		{
			if (shopFigure.figureFace != null)
			{
				shopFigure.figureFace.SetActive(value: true);
			}
			int num = shopFigureRenderers.Length;
			for (int i = 0; i < num; i++)
			{
				shopFigureRenderers[i].material.color = Color.white;
			}
			shopFigureRenderers = null;
			shopFigure.Init(null);
			ObjectPool.PoolObject(shopFigure.gameObject);
			shopFigure = null;
		}
	}

	private void HandleArrowClicked(tk2dUIItem uiItem)
	{
		if (_character == null)
		{
			return;
		}
		int num = allCharacters.IndexOf(_character);
		if (uiItem == leftArrow)
		{
			num--;
			if (num < 0)
			{
				num = allCharacters.Count - 1;
			}
		}
		else if (uiItem == rightArrow)
		{
			num++;
			if (num > allCharacters.Count - 1)
			{
				num = 0;
			}
		}
		character = allCharacters[num];
	}
}
