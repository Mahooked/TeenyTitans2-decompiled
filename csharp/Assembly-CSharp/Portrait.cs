using TMPro;
using UnityEngine;

public class Portrait : MonoBehaviour
{
	public static Vector3 bottomLeft = new Vector3(-28f, -31f, 0f);

	public static Vector3 topRight = new Vector3(31f, 29f, 0f);

	private static string thumbsUpSpriteName = "menu_characterview_classdescription_thumbsup";

	private static string thumbsDownSpriteName = "menu_characterview_classdescription_thumbsdown";

	public Transform contentHolder;

	public tk2dSprite portraitSprite;

	public TextMeshPro healthText;

	public tk2dSprite classBonusSprite;

	public tk2dSprite lockSprite;

	public tk2dUIItem uiItem;

	public Collider2D uiItemCollider;

	public Animation shakeAnimation;

	public tk2dSprite repaintIcon;

	public tk2dSprite repaintIconBacking;

	private PortraitSwitcher portraitSwitcher;

	private ActionMeter actionMeter;

	private float _alpha = 1f;

	private Character _character;

	private Color _portraitColor = Color.white;

	public float alpha
	{
		get
		{
			return _alpha;
		}
		set
		{
			if (value != _alpha)
			{
				_alpha = Mathf.Clamp01(value);
				Color color = portraitSprite.color;
				color.a = _alpha;
				portraitSprite.color = color;
				color = healthText.color;
				color.a = _alpha;
				healthText.color = color;
				color = lockSprite.color;
				color.a = _alpha;
				lockSprite.color = color;
				color = classBonusSprite.color;
				color.a = _alpha;
				classBonusSprite.color = color;
				color = repaintIcon.color;
				color.a = _alpha;
				repaintIcon.color = color;
				color = repaintIconBacking.color;
				color.a = _alpha;
				repaintIconBacking.color = color;
				if (_alpha == 0f && gameObject.activeSelf)
				{
					gameObject.SetActive(value: false);
					contentHolder.localScale = Vector3.one;
				}
				else if (_alpha > 0f && !gameObject.activeSelf)
				{
					gameObject.SetActive(value: true);
				}
			}
		}
	}

	public Vector3 unboundedPosition { get; set; }

	public Vector3 BottomLeft => transform.parent.InverseTransformPoint(transform.TransformPoint(bottomLeft));

	public Vector3 TopRight => transform.parent.InverseTransformPoint(transform.TransformPoint(topRight));

	public Character character => _character;

	public Color portraitColor
	{
		get
		{
			return _portraitColor;
		}
		set
		{
			_portraitColor = value;
			HandleUpdated();
		}
	}

	private void Awake()
	{
		if (shakeAnimation == null)
		{
			shakeAnimation = GetComponent<Animation>();
		}
		if (uiItem == null)
		{
			uiItem = GetComponent<tk2dUIItem>();
		}
		if (uiItemCollider == null)
		{
			uiItemCollider = GetComponent<Collider2D>();
		}
		portraitSwitcher = GetComponentInParent<PortraitSwitcher>();
	}

	private void OnEnable()
	{
		portraitSwitcher.CharacterChanged += HandleCharacterChanged;
		HandleCharacterChanged(portraitSwitcher.selectedCharacter);
	}

	private void OnDisable()
	{
		portraitSwitcher.CharacterChanged -= HandleCharacterChanged;
	}

	private void OnDestroy()
	{
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
			_character.Hurt -= HandleHurt;
			_character.Transformed -= HandleTransformed;
		}
		if (portraitSwitcher != null)
		{
			portraitSwitcher.CharacterChanged -= HandleCharacterChanged;
		}
		if (actionMeter != null)
		{
			actionMeter.StatusEffectAdded -= HandleMeterStatusEffectUpdated;
			actionMeter.StatusEffectRemoved -= HandleMeterStatusEffectUpdated;
		}
	}

	public void Init(Character character)
	{
		if (_character == character)
		{
			return;
		}
		if (_character != null)
		{
			_character.Updated -= HandleUpdated;
			_character.Hurt -= HandleHurt;
			_character.Transformed -= HandleTransformed;
			if (actionMeter != null)
			{
				actionMeter.StatusEffectAdded -= HandleMeterStatusEffectUpdated;
				actionMeter.StatusEffectRemoved -= HandleMeterStatusEffectUpdated;
			}
		}
		_character = character;
		if (_character != null)
		{
			portraitSprite.SetSprite(_character.portraitSprite);
			if (BattleController.instance != null)
			{
				actionMeter = ((!BattleController.instance.playerParty.Contains(character)) ? BattleController.instance.enemyActionMeter : BattleController.instance.playerActionMeter);
				actionMeter.StatusEffectAdded += HandleMeterStatusEffectUpdated;
				actionMeter.StatusEffectRemoved += HandleMeterStatusEffectUpdated;
			}
			character.Updated += HandleUpdated;
			character.Hurt += HandleHurt;
			character.Transformed += HandleTransformed;
			HandleUpdated();
		}
	}

	public void RefreshClassBonusIcon(CharacterClass opposingClass)
	{
		if (AbilityScript.classStrengths.ContainsKey(_character.characterClass) && AbilityScript.classStrengths[_character.characterClass] == opposingClass)
		{
			classBonusSprite.SetSprite(thumbsUpSpriteName);
			classBonusSprite.gameObject.SetActive(value: true);
		}
		else if (AbilityScript.classWeaknesses.ContainsKey(_character.characterClass) && AbilityScript.classWeaknesses[_character.characterClass] == opposingClass)
		{
			classBonusSprite.SetSprite(thumbsDownSpriteName);
			classBonusSprite.gameObject.SetActive(value: true);
		}
		else
		{
			classBonusSprite.gameObject.SetActive(value: false);
		}
	}

	private void HandleHurt()
	{
		shakeAnimation.Stop();
		shakeAnimation.Play();
	}

	private void HandleMeterStatusEffectUpdated(StatusEffect statusEffect)
	{
		if (statusEffect is Root)
		{
			HandleUpdated();
		}
	}

	private void HandleUpdated()
	{
		healthText.text = _character.hitPoints.ToString();
		Color color = default;
		color = ((!(_character.hitPointsRatio >= 1f)) ? Color.Lerp(Color.red, Color.yellow, _character.hitPointsRatio) : Color.green);
		color.a = healthText.color.a;
		healthText.color = color;
		color = portraitSprite.color;
		color.r = (color.g = (color.b = ((!_character.isDead) ? (1f * portraitColor.r) : (0.65f * portraitColor.r))));
		portraitSprite.color = color;
		bool flag = false;
		if (BattleController.instance != null)
		{
			Character character = ((!BattleController.instance.playerParty.Contains(_character)) ? BattleController.instance.enemyCharacter.character : BattleController.instance.playerCharacter.character);
			flag = actionMeter.HasStatusEffect<Root>() && _character != character;
		}
		lockSprite.gameObject.SetActive(_character.isKnockedOut || flag);
		if (ChallengeModeManager.isChallengeModeActive && ChallengeModeManager.modeDefinition != null && ChallengeModeManager.modeDefinition.challengeModeType == ChallengeModeType.FigFlipper && BattleController.instance != null)
		{
			if (BattleController.instance.playerCharacter.character == _character || BattleController.instance.enemyCharacter.character == _character)
			{
				lockSprite.gameObject.SetActive(_character.isKnockedOut);
			}
			else
			{
				lockSprite.gameObject.SetActive(value: true);
			}
		}
		RepaintColorSlot repaintColorSlotForID = GameManager.instance.characterDatabase.GetCharacter(_character.characterDefinition.uid).GetRepaintColorSlotForID(_character.repaintColorID);
		if (repaintColorSlotForID != null)
		{
			repaintIcon.gameObject.SetActive(value: true);
			repaintIcon.SetSprite(repaintColorSlotForID.colorSpriteId);
			repaintIcon.color = color;
			repaintIconBacking.gameObject.SetActive(value: true);
		}
		else
		{
			repaintIcon.gameObject.SetActive(value: false);
			repaintIconBacking.gameObject.SetActive(value: false);
		}
	}

	private void HandleCharacterChanged(Character character)
	{
		if (_character != null && _character.characterDefinition.characterName == "Shadow Fig")
		{
			healthText.gameObject.SetActive(character == _character);
		}
	}

	private void HandleTransformed()
	{
		portraitSprite.SetSprite(_character.portraitSprite);
	}
}
