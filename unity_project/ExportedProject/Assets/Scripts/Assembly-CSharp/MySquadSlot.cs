using TMPro;
using UnityEngine;

public class MySquadSlot : MonoBehaviour
{
	public tk2dSprite backingSprite;

	public tk2dSprite portraitSprite;

	public tk2dTintAdapter portraitSpriteTint;

	public TextMeshPro levelText;

	public tk2dSprite xpBackingSprite;

	public tk2dSlicedSpriteHelper xpFillSprite;

	public TextMeshPro healthText;

	public tk2dSprite deathXSprite;

	public tk2dUIItem uiItem;

	public tk2dUIDragItem dragItem;

	public BoxCollider2D boxCollider2D;

	public tk2dSprite repaintIcon;

	public tk2dSprite repaintIconBacking;

	public GameObject arrowGrp;

	public Animator arrowAnimator;

	public float xpFillMaxWidth;

	private bool _showHealth;

	private Character _character;

	private Renderer _portraitRenderer;

	public bool showHealth
	{
		get
		{
			return _showHealth;
		}
		set
		{
			if (value != _showHealth)
			{
				_showHealth = value;
				HandleUpdated();
			}
		}
	}

	public Character character
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

	public Renderer portraitRenderer
	{
		get
		{
			if (_portraitRenderer == null)
			{
				_portraitRenderer = portraitSprite.GetComponent<Renderer>();
			}
			return _portraitRenderer;
		}
	}

	private void Awake()
	{
		if (boxCollider2D == null)
		{
			boxCollider2D = GetComponent<BoxCollider2D>();
		}
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged += HandleUpdated;
		}
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
		}
		if (TitleScreenController.instance != null)
		{
			TitleScreenController.instance.LanguageChanged -= HandleUpdated;
		}
		_character = null;
	}

	public void Init(Character character)
	{
		if (character == _character && character != null)
		{
			return;
		}
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
		}
		_character = character;
		if (_character != null)
		{
			if (boxCollider2D != null)
			{
				boxCollider2D.enabled = true;
			}
			if (portraitSprite != null)
			{
				portraitSprite.gameObject.SetActive(true);
			}
			if (levelText != null)
			{
				levelText.gameObject.SetActive(true);
			}
			if (xpBackingSprite != null)
			{
				xpBackingSprite.gameObject.SetActive(true);
			}
			if (xpFillSprite != null)
			{
				xpFillSprite.gameObject.SetActive(true);
			}
			_character.Updated += HandleUpdated;
			HandleUpdated();
		}
		else
		{
			if (boxCollider2D != null)
			{
				boxCollider2D.enabled = false;
			}
			if (portraitSprite != null)
			{
				portraitSprite.gameObject.SetActive(false);
			}
			if (levelText != null)
			{
				levelText.gameObject.SetActive(false);
			}
			if (xpBackingSprite != null)
			{
				xpBackingSprite.gameObject.SetActive(false);
			}
			if (xpFillSprite != null)
			{
				xpFillSprite.gameObject.SetActive(false);
			}
			repaintIcon.gameObject.SetActive(false);
			repaintIconBacking.gameObject.SetActive(false);
		}
	}

	private void HandleUpdated()
	{
		if (_character == null || portraitSprite == null)
		{
			return;
		}
		portraitSprite.SetSprite(_character.characterDefinition.portraitSpriedId);
		levelText.text = Language.Get("LVL", "MENUS,GUI") + " " + _character.level;
		levelText.color = ((_character.level < _character.levelTable.maxLevel) ? Color.white : TTGColors.MAX_LEVEL_TEXT_COLOR);
		xpFillSprite.width = ((!_character.isMaxLevel) ? (xpFillMaxWidth * _character.currentLevelProgress) : xpFillMaxWidth);
		if (_showHealth)
		{
			if (!_character.isDead)
			{
				if (healthText != null)
				{
					healthText.gameObject.SetActive(true);
				}
				if (deathXSprite != null)
				{
					deathXSprite.gameObject.SetActive(false);
				}
				if (healthText != null)
				{
					healthText.text = _character.hitPoints.ToString();
				}
				Color color = default(Color);
				color = ((!(_character.hitPointsRatio >= 1f)) ? Color.Lerp(Color.red, Color.yellow, _character.hitPointsRatio) : Color.green);
				color.a = healthText.color.a;
				if (healthText != null)
				{
					healthText.color = color;
				}
			}
			else
			{
				if (deathXSprite != null)
				{
					deathXSprite.gameObject.SetActive(true);
				}
				if (healthText != null)
				{
					healthText.gameObject.SetActive(false);
				}
			}
		}
		else
		{
			if (healthText != null)
			{
				healthText.gameObject.SetActive(false);
			}
			if (deathXSprite != null)
			{
				deathXSprite.gameObject.SetActive(false);
			}
		}
		RepaintColorSlot repaintColorSlotForID = GameManager.instance.characterDatabase.GetCharacter(_character.characterDefinition.uid).GetRepaintColorSlotForID(_character.repaintColorID);
		if (repaintColorSlotForID != null)
		{
			repaintIcon.gameObject.SetActive(true);
			repaintIcon.SetSprite(repaintColorSlotForID.colorSpriteId);
			repaintIconBacking.gameObject.SetActive(true);
		}
		else
		{
			repaintIcon.gameObject.SetActive(false);
			repaintIconBacking.gameObject.SetActive(false);
		}
	}
}
