using TMPro;
using UnityEngine;

public class RepaintColorButton : MonoBehaviour
{
	[SerializeField]
	private tk2dSprite backgroundSprite;

	[SerializeField]
	private tk2dSprite colorWheelSprite;

	[SerializeField]
	private tk2dSprite highlightSprite;

	[SerializeField]
	private tk2dSprite checkmarkSprite;

	[SerializeField]
	private tk2dSprite coinSprite;

	[SerializeField]
	private Renderer lockRenderer;

	[SerializeField]
	private Renderer equipIconRenderer;

	[SerializeField]
	private TextMeshPro coinTextMesh;

	[SerializeField]
	private Collider2D _collider2D;

	[SerializeField]
	private GameObject _newTag;

	[SerializeField]
	private Color rareTextColor;

	[SerializeField]
	private string checkSpriteName;

	[SerializeField]
	private string checkSpriteRareName;

	private RepaintColorSlot currentColorSlot;

	private Character currentCharacter;

	private readonly Color white = Color.white;

	private readonly Color clear = Color.clear;

	private tk2dUIItem _tk2dUIItem;

	public Collider2D collider2D
	{
		get
		{
			return _collider2D;
		}
	}

	public int id { get; private set; }

	public tk2dUIItem tk2dUIItem
	{
		get
		{
			if (_tk2dUIItem == null)
			{
				_tk2dUIItem = GetComponent<tk2dUIItem>();
			}
			return _tk2dUIItem;
		}
	}

	public tk2dSprite colorSampleSprite
	{
		get
		{
			return colorWheelSprite;
		}
	}

	public bool isHighlighted
	{
		get
		{
			return highlightSprite.color == white;
		}
	}

	private void Awake()
	{
		if (_collider2D == null)
		{
			_collider2D = GetComponent<Collider2D>();
		}
	}

	public void Init(RepaintColorSlot repaintColorSlot, Character character, bool isCharacterView = false)
	{
		currentColorSlot = repaintColorSlot;
		currentCharacter = character;
		id = currentColorSlot.id;
		colorWheelSprite.SetSprite(currentColorSlot.colorSpriteId);
		int repaintColorPreferenceForCharacterUID = Player.instance.GetRepaintColorPreferenceForCharacterUID(character.uid);
		bool highlightActive = repaintColorPreferenceForCharacterUID == id;
		SetHighlightActive(highlightActive);
		if (equipIconRenderer != null)
		{
			equipIconRenderer.enabled = highlightActive;
		}
		bool flag = !character.repaintColorsUnlocked.Contains(id);
		bool flag2 = Player.instance.HasFreeRepaint(id);
		bool flag3 = Player.instance.HasAcquiredRepaintSlot(id);
		bool purchased = Player.instance.HasPurchasedRepaintColor(character, id);
		string sprite;
		if (isCharacterView)
		{
			sprite = (currentColorSlot.rare ? ((!flag || flag2 || flag3) ? "menu_shop_repaint_choosecolor_rare" : "menu_shop_repaint_notunlocked_gold") : ((!flag || flag2 || flag3) ? "menu_shop_repaint_choosecolor_normal" : "menu_shop_repaint_notunlocked"));
			colorWheelSprite.color = ((!flag || flag2 || flag3) ? white : clear);
			_newTag.SetActive(flag && flag2);
			_collider2D.enabled = !flag || flag2;
			if (flag3 && flag && !flag2)
			{
				backgroundSprite.color = new Color(0.32f, 0.32f, 0.32f);
				colorWheelSprite.color = new Color(0.5f, 0.5f, 0.5f);
			}
			else
			{
				backgroundSprite.color = white;
			}
		}
		else
		{
			sprite = ((!currentColorSlot.rare) ? "menu_shop_repaint_choosecolor_normal" : "menu_shop_repaint_choosecolor_rare");
			backgroundSprite.color = white;
			colorWheelSprite.color = white;
			if (_newTag != null)
			{
				_newTag.SetActive(flag2 && flag);
			}
			if (_collider2D != null)
			{
				_collider2D.enabled = true;
			}
		}
		string sprite2 = ((!currentColorSlot.rare) ? checkSpriteName : checkSpriteRareName);
		if (!repaintColorSlot.rare || flag3)
		{
			if (lockRenderer != null)
			{
				lockRenderer.enabled = false;
			}
			if (coinTextMesh != null && !flag2)
			{
				coinTextMesh.color = white;
				coinTextMesh.text = ((!Player.instance.HasFreeRepaint(repaintColorSlot.id)) ? currentColorSlot.cost.ToString() : "0");
				if (coinSprite != null)
				{
					coinSprite.color = white;
				}
			}
			else
			{
				if (coinTextMesh != null)
				{
					coinTextMesh.color = clear;
				}
				if (coinSprite != null)
				{
					coinSprite.color = clear;
				}
			}
		}
		else
		{
			if (coinTextMesh != null)
			{
				coinTextMesh.color = clear;
			}
			if (lockRenderer != null)
			{
				lockRenderer.enabled = true;
			}
		}
		backgroundSprite.SetSprite(sprite);
		if (checkmarkSprite != null)
		{
			checkmarkSprite.SetSprite(sprite2);
		}
		SetPurchased(purchased);
	}

	public void InitAsNoColorButton(bool isCharacterView = false)
	{
		id = -1;
		colorWheelSprite.SetSprite("menu_shop_repaint_color_nocolor");
		backgroundSprite.SetSprite("menu_shop_repaint_choosecolor_normal");
		SetHighlightActive(currentCharacter != null && Player.instance.GetRepaintColorPreferenceForCharacterUID(currentCharacter.uid) == 0);
		if (checkmarkSprite != null)
		{
			checkmarkSprite.SetSprite(checkSpriteName);
			tk2dSprite obj = checkmarkSprite;
			Color color = clear;
			highlightSprite.color = color;
			obj.color = color;
		}
		if (equipIconRenderer != null)
		{
			equipIconRenderer.enabled = false;
		}
		if (coinSprite != null)
		{
			coinSprite.color = clear;
		}
		if (coinTextMesh != null)
		{
			coinTextMesh.color = clear;
		}
		if (lockRenderer != null)
		{
			lockRenderer.enabled = false;
		}
		if (_newTag != null)
		{
			_newTag.SetActive(false);
		}
		currentColorSlot = null;
	}

	public void SetHighlightActive(bool highlightOn)
	{
		highlightSprite.color = ((!highlightOn) ? clear : white);
	}

	public void SetPurchased(bool purchased)
	{
		if (checkmarkSprite != null)
		{
			checkmarkSprite.color = ((!purchased) ? clear : white);
		}
		if (equipIconRenderer != null)
		{
			if (currentCharacter != null && currentColorSlot != null)
			{
				equipIconRenderer.enabled = Player.instance.GetRepaintColorPreferenceForCharacterUID(currentCharacter.uid) == currentColorSlot.id;
			}
			else
			{
				equipIconRenderer.enabled = false;
			}
		}
		bool flag = currentColorSlot != null && Player.instance.HasFreeRepaint(currentColorSlot.id);
		bool flag2 = currentColorSlot != null && Player.instance.HasAcquiredRepaintSlot(currentColorSlot.id);
		if (purchased || id == -1 || flag || (currentColorSlot.rare && !flag2))
		{
			if (coinTextMesh != null)
			{
				coinTextMesh.color = clear;
			}
			if (coinSprite != null)
			{
				coinSprite.color = clear;
			}
			if (lockRenderer != null)
			{
				lockRenderer.enabled = !purchased && id != -1 && !flag;
			}
			if (_newTag != null)
			{
				_newTag.SetActive(!purchased && flag);
			}
		}
		else
		{
			if (coinTextMesh != null)
			{
				coinTextMesh.color = ((!currentColorSlot.rare) ? white : rareTextColor);
			}
			if (coinSprite != null)
			{
				coinSprite.color = white;
			}
			if (lockRenderer != null)
			{
				lockRenderer.enabled = false;
			}
		}
	}
}
