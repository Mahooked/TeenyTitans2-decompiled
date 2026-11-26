using System.Collections.Generic;
using UnityEngine;

public class NPCDefinition : ScriptableObject
{
	public CharacterName characterType;

	public string preBattleSprite = string.Empty;

	public string preBattleSpriteRight = string.Empty;

	public string prefabPath = string.Empty;

	public string conversationBodySprite = string.Empty;

	public string rightConversationBodySprite = string.Empty;

	public int leftConversationBodySpriteId = -1;

	public int rightConversationBodySpriteId = -1;

	public string conversationBubbleSprite = "TTG_gui_dialoguebox_npc";

	public bool prefabFacingLeft;

	public bool prefabPreventFlip;

	public Vector3 colliderCenter = new Vector3(0f, 0.12f, -0.08f);

	public Vector3 colliderSize = new Vector3(0.25f, 0.45f, 0.2f);

	public List<string> tags = new List<string>();

	public int conversationBodySpriteId;

	public List<ConversationFace> conversationFaces = new List<ConversationFace>();

	public List<NPCSpawnPointSetup> spawnSetups = new List<NPCSpawnPointSetup>();

	public int conversationFacePreviewIndex;

	public Color dialogueFaceColor = Color.white;

	public Color dialogueOutlineColor = Color.black;

	public bool useDefaultPreBattleSounds;

	public BattleCharacterSounds.BattleCharacterSoundData preBattleSoundData = new BattleCharacterSounds.BattleCharacterSoundData();

	public int countdownCharacterUID = -1;

	[SerializeField]
	[HideInInspector]
	private int _uid = -1;

	[SerializeField]
	private string _guid = string.Empty;

	[SerializeField]
	private string _name = string.Empty;

	[SerializeField]
	private string _nameKey = string.Empty;

	[SerializeField]
	private int _preBattleSpriteId = -1;

	[SerializeField]
	private int _preBattleSpriteRightId = -1;

	[SerializeField]
	private int _nextSpawnSetupId = -1;

	[SerializeField]
	private tk2dSpriteCollectionData _dialogueSpriteCollection;

	[SerializeField]
	private tk2dSpriteCollectionData _battleIconSpriteCollection;

	[SerializeField]
	private List<string> _cityIdleSFX = new List<string>();

	[SerializeField]
	private ConvoButtonConfig[] _convoButtonConfigs;

	public int uid => _uid;

	public string guid => _guid;

	public new string name => _name;

	public string nameKey => _nameKey;

	public int preBattleSpriteId => _preBattleSpriteId;

	public int preBattleSpriteRightId
	{
		get
		{
			return _preBattleSpriteRightId;
		}
		set
		{
			_preBattleSpriteRightId = value;
		}
	}

	public int nextSpawnSetupId
	{
		get
		{
			_nextSpawnSetupId++;
			return _nextSpawnSetupId;
		}
	}

	public tk2dSpriteCollectionData dialogueSpriteCollection => _dialogueSpriteCollection;

	public tk2dSpriteCollectionData battleIconSpriteCollection => _battleIconSpriteCollection;

	public List<string> cityIdleSFX => _cityIdleSFX;

	public ConvoButtonConfig[] convoButtonConfigs => _convoButtonConfigs;
}
