using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using UnityEngine;

public class PreBattleParty : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Character> CharacterSelected__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action DragStarted__BackingField;

	public tk2dSlicedSprite backingSprite;

	public List<MySquadSlot> slots;

	public CharacterInfoBox characterInfoBox;

	public Camera lowerCamera;

	public Camera upperCamera;

	public Camera upperPerspectiveCamera;

	public HandheldConsole handheldConsole;

	public BarSpeedGUI barSpeed;

	public AudioClip sfxOnDrag;

	public AudioClip sfxOnPlace;

	public AudioClip sfxOnRemove;

	public AudioClip sfxInfoPane;

	private MySquadSlot selectedSlot;

	private MySquadSlot prevSlotClick;

	private ShopFigureModel dragFigure;

	private Character dragCharacter;

	private Vector3 planePosition = Vector3.zero;

	private bool _isDragEnabled = true;

	private Player _player;

	private BattleDefinition _definition;

	public bool isDragEnabled
	{
		get
		{
			return _isDragEnabled;
		}
		set
		{
			_isDragEnabled = value;
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
			Init(value, _definition);
		}
	}

	public event Action<Character> CharacterSelected
	{
		add
		{
			Action<Character> action = CharacterSelected__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action<Character>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Character> action = CharacterSelected__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action<Character>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action DragStarted
	{
		add
		{
			Action action = DragStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragStarted__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = DragStarted__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragStarted__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].uiItem.OnClickUIItem += HandleOnClickUIItem;
			slots[i].uiItem.OnExitUIItem += HandleOnExitUIItem;
		}
		if (characterInfoBox.gameObject.activeSelf)
		{
			characterInfoBox.gameObject.SetActive(false);
		}
	}

	private void OnEnable()
	{
		handheldConsole.leftArrow.uiItem.OnClickUIItem += HandleOnClickArrow;
		handheldConsole.leftArrow.uiItem.OnDownUIItem += HandleOnDownArrow;
		handheldConsole.centerArrow.uiItem.OnClickUIItem += HandleOnClickArrow;
		handheldConsole.centerArrow.uiItem.OnDownUIItem += HandleOnDownArrow;
		handheldConsole.rightArrow.uiItem.OnClickUIItem += HandleOnClickArrow;
		handheldConsole.rightArrow.uiItem.OnDownUIItem += HandleOnDownArrow;
	}

	private void OnDisable()
	{
		handheldConsole.leftArrow.uiItem.OnClickUIItem -= HandleOnClickArrow;
		handheldConsole.leftArrow.uiItem.OnDownUIItem -= HandleOnDownArrow;
		handheldConsole.centerArrow.uiItem.OnClickUIItem -= HandleOnClickArrow;
		handheldConsole.centerArrow.uiItem.OnDownUIItem -= HandleOnDownArrow;
		handheldConsole.rightArrow.uiItem.OnClickUIItem -= HandleOnClickArrow;
		handheldConsole.rightArrow.uiItem.OnDownUIItem -= HandleOnDownArrow;
	}

	private void Update()
	{
		if (!(dragFigure != null))
		{
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Ray ray = upperPerspectiveCamera.ScreenPointToRay(Input.mousePosition);
			PreBattleArrow arrow = null;
			RaycastHit[] array = Physics.RaycastAll(ray, upperPerspectiveCamera.farClipPlane);
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].collider.tag.Equals("PreBattleArrow"))
					{
						arrow = array[i].collider.GetComponent<PreBattleArrow>();
						break;
					}
				}
			}
			dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Default"));
			if (arrow == null)
			{
				ObjectPool.PoolObject(dragFigure.gameObject);
				dragFigure.transform.localRotation = Quaternion.identity;
				dragFigure.transform.localScale = Vector3.one;
			}
			else
			{
				RemoveFromArrow(arrow);
				Tweener tween = dragFigure.transform.DOMove(arrow.figurePosition.position, 0.3f).SetEase(Ease.OutBounce);
				tween.OnUpdate(() =>
				{
					DropComplete(tween, arrow);
				});
				GFAudioManager.instance.PlayClip(sfxOnPlace, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
				arrow.figureModel = dragFigure;
				arrow.character = dragCharacter;
				_player.AddToTeam(dragCharacter);
				RefreshBarSpeed();
			}
			dragFigure = null;
			dragCharacter = null;
			handheldConsole.HideArrows();
			handheldConsole.model.classCircle.SetActive(true);
		}
		else
		{
			Plane plane = new Plane(Vector3.back, planePosition);
			Ray ray2 = upperPerspectiveCamera.ScreenPointToRay(Input.mousePosition);
			float enter;
			plane.Raycast(ray2, out enter);
			Vector3 point = ray2.GetPoint(enter);
			dragFigure.transform.position = point - new Vector3(0f, dragFigure.renderer.bounds.extents.y, 0f);
		}
	}

	private void OnDestroy()
	{
		if (_player != null)
		{
			_player.TeamChanged -= HandleTeamChanged;
		}
		_player = null;
	}

	public void Init(Player player, BattleDefinition definition)
	{
		if (player == _player)
		{
			return;
		}
		if (_player != null)
		{
			_player.TeamChanged -= HandleTeamChanged;
		}
		_player = player;
		_definition = definition;
		if (_player != null)
		{
			_player.TeamChanged += HandleTeamChanged;
			HandleTeamChanged();
		}
		if (_definition != null)
		{
			int count = slots.Count;
			for (int i = 0; i < count; i++)
			{
				slots[i].showHealth = _definition.type == BattleType.Tournament;
			}
		}
	}

	public void EnableButtons(bool enable = true)
	{
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			slots[i].uiItem.enabled = enable;
		}
	}

	public void SetInteractive(bool interactive)
	{
		EnableButtons(interactive);
		Color color = ((!interactive) ? Color.gray : Color.white);
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			MySquadSlot mySquadSlot = slots[i];
			mySquadSlot.backingSprite.color = color;
			mySquadSlot.portraitSprite.color = color;
			mySquadSlot.xpBackingSprite.color = color;
			mySquadSlot.xpFillSprite.slicedSprite.color = color;
			mySquadSlot.levelText.color = color;
			mySquadSlot.repaintIcon.color = color;
		}
		backingSprite.color = color;
	}

	public void ForceStopDragging()
	{
		if (dragFigure != null)
		{
			ObjectPool.PoolObject(dragFigure.gameObject);
			dragFigure.transform.localRotation = Quaternion.identity;
			dragFigure.transform.localScale = Vector3.one;
			dragFigure = null;
			dragCharacter = null;
			handheldConsole.HideArrows();
			handheldConsole.model.classCircle.SetActive(true);
		}
	}

	public void ForceStopEquipping()
	{
		if (characterInfoBox.gameObject.activeSelf && selectedSlot != null)
		{
			characterInfoBox.gameObject.SetActive(false);
			selectedSlot = null;
			handheldConsole.HideArrows();
			handheldConsole.model.classCircle.SetActive(true);
			if (CharacterSelected__BackingField != null)
			{
				CharacterSelected__BackingField(null);
			}
		}
	}

	private void RefreshBarSpeed()
	{
		int num = 0;
		int num2 = 0;
		num = _player.activeTeam.Count;
		for (int i = 0; i < num; i++)
		{
			num2 += _player.activeTeam[i].characterDefinition.actionMeterSpeedRating;
		}
		barSpeed.speedLevel = ((num <= 0) ? (-1) : Mathf.RoundToInt((float)num2 / (float)num));
	}

	private void DropComplete(Tweener tween, PreBattleArrow arrow)
	{
		if (!arrow.effect.enabled && tween.fullPosition >= 0.15f && arrow.character != null)
		{
			arrow.effect.enabled = true;
		}
	}

	private void RemoveFromArrow(PreBattleArrow arrow)
	{
		if (arrow.character != null)
		{
			_player.RemoveFromTeam(arrow.character);
			RefreshBarSpeed();
			ObjectPool.PoolObject(arrow.figureModel.gameObject);
			arrow.figureModel.transform.localRotation = Quaternion.identity;
			arrow.figureModel.transform.localScale = Vector3.one;
			arrow.effect.enabled = false;
			GFAudioManager.instance.PlayClip(sfxOnRemove, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			arrow.character = null;
			arrow.figureModel = null;
		}
	}

	private void HandleOnClickArrow(tk2dUIItem obj)
	{
		PreBattleArrow component = obj.GetComponent<PreBattleArrow>();
		if (component.character == null && selectedSlot != null && component.character == null && CanUseCharacterInBattle(selectedSlot.character) && !_player.activeTeam.Contains(selectedSlot.character))
		{
			SetCharacterForArrow(selectedSlot.character, component);
			handheldConsole.HideArrows();
			characterInfoBox.gameObject.SetActive(false);
			selectedSlot = null;
		}
	}

	public void SetCharacterForArrow(Character character, PreBattleArrow arrow)
	{
		if (arrow.character == null && character != null && CanUseCharacterInBattle(character) && !_player.activeTeam.Contains(character))
		{
			ShopFigureModel objectFromResource = ObjectPool.GetObjectFromResource<ShopFigureModel>(character.characterDefinition.shopFigurePath);
			objectFromResource.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			objectFromResource.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			objectFromResource.transform.position = arrow.figurePosition.position;
			arrow.effect.enabled = true;
			arrow.figureModel = objectFromResource;
			arrow.character = character;
			objectFromResource.character = character;
			RepaintFigureModel(objectFromResource);
			_player.AddToTeam(character);
			RefreshBarSpeed();
		}
	}

	private void RepaintFigureModel(ShopFigureModel model)
	{
		int repaintColorPreferenceForCharacterUID = Player.instance.GetRepaintColorPreferenceForCharacterUID(model.character.uid);
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

	private void HandleOnDownArrow(tk2dUIItem uiItem)
	{
		PreBattleArrow component = uiItem.GetComponent<PreBattleArrow>();
		if (component.character != null)
		{
			dragFigure = component.figureModel;
			dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Character"));
			dragCharacter = component.character;
			_player.RemoveFromTeam(component.character);
			RefreshBarSpeed();
			GFAudioManager.instance.PlayClip(sfxOnRemove, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			planePosition = Vector3.zero;
			planePosition.z = dragFigure.transform.position.z;
			Update();
			component.figureModel = null;
			component.character = null;
			component.effect.enabled = false;
			handheldConsole.ShowArrows();
			handheldConsole.model.classCircle.SetActive(false);
		}
	}

	private void HandleOnClickUIItem(tk2dUIItem obj)
	{
		selectedSlot = obj.GetComponent<MySquadSlot>();
		if (characterInfoBox.gameObject.activeSelf && selectedSlot == prevSlotClick)
		{
			characterInfoBox.gameObject.SetActive(false);
			selectedSlot = null;
			GFAudioManager.instance.PlayClip(sfxInfoPane, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			handheldConsole.HideArrows();
			handheldConsole.model.classCircle.SetActive(true);
			if (CharacterSelected__BackingField != null)
			{
				CharacterSelected__BackingField(null);
			}
		}
		else
		{
			Vector3 position = lowerCamera.WorldToScreenPoint(selectedSlot.transform.position + new Vector3(0f, 32f));
			position = upperCamera.ScreenToWorldPoint(position);
			position.z = characterInfoBox.transform.position.z;
			characterInfoBox.transform.position = position;
			characterInfoBox.Init(selectedSlot.character);
			characterInfoBox.gameObject.SetActive(true);
			GFAudioManager.instance.PlayClip(sfxInfoPane, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			handheldConsole.ShowArrows();
			handheldConsole.model.classCircle.SetActive(false);
			if (CharacterSelected__BackingField != null)
			{
				CharacterSelected__BackingField(selectedSlot.character);
			}
		}
		prevSlotClick = selectedSlot;
	}

	private void HandleOnExitUIItem(tk2dUIItem obj)
	{
		MySquadSlot component = obj.GetComponent<MySquadSlot>();
		if (_isDragEnabled && component.character != null && !_player.IsCharacterInTeam(component.character) && CanUseCharacterInBattle(component.character))
		{
			if (characterInfoBox.gameObject.activeSelf)
			{
				characterInfoBox.gameObject.SetActive(false);
				selectedSlot = null;
			}
			handheldConsole.ShowArrows();
			handheldConsole.model.classCircle.SetActive(false);
			planePosition = Vector3.zero;
			dragCharacter = component.character;
			dragFigure = ObjectPool.GetObjectFromResource<ShopFigureModel>(component.character.characterDefinition.shopFigurePath);
			dragFigure.gameObject.SetLayerRecursively(LayerMask.NameToLayer("Character"));
			dragFigure.transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
			dragFigure.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
			dragFigure.character = component.character;
			RepaintFigureModel(dragFigure);
			Update();
			GFAudioManager.instance.PlayClip(sfxOnDrag, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
			if (DragStarted__BackingField != null)
			{
				DragStarted__BackingField();
			}
		}
	}

	public bool CanUseCharacterInBattle(Character character)
	{
		if (!character.characterDefinition.canUseInBattle || character.isDead)
		{
			return false;
		}
		if (_definition != null && _definition.newBattleConditions != null)
		{
			bool flag = false;
			for (int i = 0; i < _definition.newBattleConditions.Count; i++)
			{
				BattleCondition battleCondition = _definition.newBattleConditions[i];
				switch (battleCondition.conditionType)
				{
				case BattleCondition.ConditionType.FigureName:
					if (character.characterDefinition.uid == battleCondition.intValue2)
					{
						return true;
					}
					break;
				case BattleCondition.ConditionType.FigureTags:
					if (character.characterDefinition.tags.Contains(battleCondition.stringValue1))
					{
						return true;
					}
					break;
				case BattleCondition.ConditionType.FigureLevel:
					if (ComparisonUtil.IsComparisonValid(character.level, battleCondition.comparison2, battleCondition.intValue2))
					{
						return true;
					}
					break;
				case BattleCondition.ConditionType.FigureShinyRank:
					if (ComparisonUtil.IsComparisonValid(character.shinyRank, battleCondition.comparison2, battleCondition.intValue2))
					{
						return true;
					}
					break;
				case BattleCondition.ConditionType.Accessory:
					return true;
				case BattleCondition.ConditionType.TeamSize:
					switch (battleCondition.comparison1)
					{
					case Comparison.Exactly:
					case Comparison.AtMost:
						if (_player.activeTeam.Count < battleCondition.intValue1)
						{
							return true;
						}
						break;
					case Comparison.LessThan:
						if (_player.activeTeam.Count < battleCondition.intValue1 - 1)
						{
							return true;
						}
						break;
					case Comparison.MoreThan:
					case Comparison.AtLeast:
						return true;
					}
					break;
				case BattleCondition.ConditionType.FigureClass:
					if (character.characterDefinition.characterClass == (CharacterClass)battleCondition.intValue2)
					{
						return true;
					}
					break;
				case BattleCondition.ConditionType.FigureRepainted:
					if (character.repaintColorID > 0)
					{
						return true;
					}
					break;
				}
			}
			return false;
		}
		if (_definition != null && _definition.battleConditions != null && _definition.battleConditions.Count > 0)
		{
			bool flag2 = false;
			for (int j = 0; j < _definition.battleConditions.Count; j++)
			{
				NPCBattleCondition nPCBattleCondition = _definition.battleConditions[j];
				if (nPCBattleCondition.type == NPCBattleConditionType.Character)
				{
					CharacterDefinition character2 = GameManager.instance.characterDatabase.GetCharacter(nPCBattleCondition.uid);
					if (character.characterDefinition.characterName.ToLower().Contains(character2.characterName.ToLower()))
					{
						flag2 = true;
					}
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.MinLevel && character.level >= nPCBattleCondition.amount)
				{
					flag2 = true;
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.GoldAbility && character.shinyRank >= nPCBattleCondition.amount)
				{
					flag2 = true;
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.Is80sFigure && character.characterDefinition.is80sFigure)
				{
					flag2 = true;
				}
				if (nPCBattleCondition.type == NPCBattleConditionType.IsVillainFigure && character.characterDefinition.isVillainFigure)
				{
					flag2 = true;
				}
			}
			return flag2 && !character.isDead;
		}
		return true;
	}

	private void HandleTeamChanged()
	{
		int count = slots.Count;
		for (int i = 0; i < count; i++)
		{
			MySquadSlot mySquadSlot = slots[i];
			if (i < _player.activeParty.Count)
			{
				mySquadSlot.Init(_player.activeParty[i]);
				Color color = ((!CanUseCharacterInBattle(_player.activeParty[i])) ? new Color(0.5f, 0.5f, 0.5f) : ((!_player.IsCharacterInTeam(_player.activeParty[i])) ? Color.white : new Color(0.5f, 0.5f, 0.5f)));
				tk2dSprite portraitSprite = mySquadSlot.portraitSprite;
				Color color2 = color;
				mySquadSlot.repaintIcon.color = color2;
				portraitSprite.color = color2;
			}
			else
			{
				mySquadSlot.Init(null);
				tk2dSprite portraitSprite2 = mySquadSlot.portraitSprite;
				Color color2 = Color.white;
				mySquadSlot.repaintIcon.color = color2;
				portraitSprite2.color = color2;
			}
		}
		handheldConsole.model.classCircle.SetActive(true);
	}
}
