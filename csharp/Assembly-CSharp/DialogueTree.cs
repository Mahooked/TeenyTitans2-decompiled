using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DialogueTree : MonoBehaviour
{
	private static Dictionary<int, List<Vector3>> choiceButtonOffsets = new Dictionary<int, List<Vector3>>
	{
		{
			1,
			new List<Vector3>
			{
				new Vector3(0f, -110f, 0f)
			}
		},
		{
			2,
			new List<Vector3>
			{
				new Vector3(-60f, -110f, 0f),
				new Vector3(60f, -110f, 0f)
			}
		},
		{
			3,
			new List<Vector3>
			{
				new Vector3(-88f, -115f, 0f),
				new Vector3(0f, -80f, 0f),
				new Vector3(88f, -115f, 0f)
			}
		}
	};

	private static Vector3 selectedChoicePosition = new Vector3(0f, -80f, 0f);

	public Animator animator;

	public Transform holder;

	public int maxBubbles = 2;

	public DialogueCharacter leftCharacter;

	public DialogueCharacter rightCharacter;

	public Vector3 leftBubbleStart = Vector3.zero;

	public Vector3 rightBubbleStart = Vector3.zero;

	public GameObject bubblePrefab;

	public AudioClip bubbleAppearSound;

	public AudioClip nextButtonSound;

	private List<DialogueBubble> _bubblePool;

	private List<DialogueChoiceButton> _choiceButtonPool;

	private Conversation _conversation;

	private int curIndex;

	private ConversationItem nextItem;

	private List<DialogueBubble> activeBubbles;

	private List<DialogueChoiceButton> buttonList;

	private DialogueChoiceButton curChoice;

	private List<ConversationItem> conversationHistory;

	public tk2dSprite missionStar;

	public tk2dSprite blackFade;

	public Transform backing;

	public TempDialogueButton leftButton;

	public TempDialogueButton rightButton;

	public GameObject backingNormal;

	public GameObject backingCabby;

	public tk2dUIItem exitButton;

	public CurrencyGUI currencyGUI;

	public string[] beastBoyChatEndVO;

	public string[] ravenChatEndVO;

	public string[] starfireChatEndVO;

	public string[] cyborgChatEndVO;

	private List<tk2dCameraAnchor> cameraAnchors;

	private Vector3 backingHomePosition;

	private Vector3 leftCharacterHomePosition;

	private Vector3 rightCharacterHomePosition;

	private Vector3 exitButtonHomePosition;

	private CityNPC _cityNPC;

	private CityEntityNPC _npcEntity;

	private CityEntityStateNPC _npcState;

	private bool conversationPlaying;

	private bool conversationExiting;

	private Conversation tempExitConvo;

	private Dictionary<CharacterName, tk2dSpriteCollectionData> characterSpriteCollections = new Dictionary<CharacterName, tk2dSpriteCollectionData>();

	private int completesMissionUID = -1;

	private bool exitButtonDown;

	private static DialogueTree _treeInstance;

	public Conversation conversation => _conversation;

	public static DialogueTree instance => _treeInstance;

	public bool isConverstationPlaying => conversationPlaying;

	public event Action<CityEntityNPC, CityEntityStateNPC, Mission, Mission> BeginBattle;

	private void Awake()
	{
		_bubblePool = new List<DialogueBubble>();
		_choiceButtonPool = new List<DialogueChoiceButton>();
		bubblePrefab.SetActive(value: false);
		leftCharacter.gameObject.SetActive(value: false);
		rightCharacter.gameObject.SetActive(value: false);
		activeBubbles = new List<DialogueBubble>();
		conversationHistory = new List<ConversationItem>();
		cameraAnchors = new List<tk2dCameraAnchor>();
		GetComponentsInChildren(cameraAnchors);
		backingHomePosition = backing.localPosition;
		leftCharacterHomePosition = leftCharacter.transform.localPosition;
		rightCharacterHomePosition = rightCharacter.transform.localPosition;
		exitButtonHomePosition = exitButton.transform.localPosition;
		currencyGUI.Init(Player.instance);
	}

	private void OnEnable()
	{
		leftButton.uiItem.OnClickUIItem += HandleOnDialogueButtonClick;
		rightButton.uiItem.OnClickUIItem += HandleOnDialogueButtonClick;
		exitButton.OnClick += HandleOnExitClick;
		exitButton.OnDown += HandleOnExitDown;
		exitButton.OnRelease += HandleOnExitRelease;
	}

	private void OnDisable()
	{
		leftButton.uiItem.OnClickUIItem -= HandleOnDialogueButtonClick;
		rightButton.uiItem.OnClickUIItem -= HandleOnDialogueButtonClick;
		exitButton.OnClick -= HandleOnExitClick;
		exitButton.OnDown -= HandleOnExitDown;
		exitButton.OnRelease -= HandleOnExitRelease;
		backing.localPosition = backingHomePosition;
		leftCharacter.transform.localPosition = leftCharacterHomePosition;
		rightCharacter.transform.localPosition = rightCharacterHomePosition;
		exitButton.transform.localPosition = exitButtonHomePosition;
	}

	private void OnDestroy()
	{
		_conversation = null;
		foreach (KeyValuePair<CharacterName, tk2dSpriteCollectionData> characterSpriteCollection in characterSpriteCollections)
		{
			if (characterSpriteCollection.Key != CharacterName.Robin && characterSpriteCollection.Key != CharacterName.DogRobin && characterSpriteCollection.Key != CharacterName.MrChibiTournament)
			{
				if (CityController.instance != null)
				{
					CityController.instance.HandleUnloadSpriteCollection(characterSpriteCollection.Value);
					continue;
				}
				characterSpriteCollection.Value.UnloadTextures();
				characterSpriteCollection.Value.ResetPlatformData();
			}
		}
		BeginBattle = null;
		_treeInstance = null;
		if (GFAudioManager.instance != null)
		{
			AudioSource audioSourceById = GFAudioManager.instance.GetAudioSourceById("printText");
			if (audioSourceById != null)
			{
				GFAudioManager.instance.StopById("printText");
			}
		}
	}

	private IEnumerator ExtraWaitTimeForConvo(Conversation conversation)
	{
		if (conversation != null)
		{
			if (conversation.name.Equals("CONVO_CLAM_AQUAMANREVEAL"))
			{
				yield return new WaitForSeconds(1.2f);
			}
			if (conversation.name.Equals("CONVO_HoodedHood_afterpetstore"))
			{
				yield return StartCoroutine(CityController.instance.StartCreateHoodedHoodRoutine(conversation.name));
			}
			if (conversation.name.Equals("CONVO_HOOD_AFTER2NDTOURNY1"))
			{
				yield return StartCoroutine(CityController.instance.StartCreateHoodedHoodRoutine(conversation.name));
			}
			if (conversation.name.Equals("CONVO_HOOD_AFTER3RDTOURNY"))
			{
				yield return StartCoroutine(CityController.instance.StartCreateHoodedHoodRoutine(conversation.name));
			}
			if (conversation.name.Equals("CONVO_HOOD_AFTERJAIL"))
			{
				yield return StartCoroutine(CityController.instance.StartCreateHoodedHoodRoutine(conversation.name));
			}
		}
		yield return new WaitForEndOfFrame();
	}

	private IEnumerator EntranceSequence(float forceWaitTime = -1f, bool immediateChat = false, Conversation conversation = null)
	{
		exitButton.enabled = false;
		animator.Play("Outro", 0, 1f);
		animator.Update(0f);
		while (LoadingScreenCover.isLoading || !LoadingScreenCover.allowInit)
		{
			yield return new WaitForEndOfFrame();
		}
		yield return StartCoroutine(ExtraWaitTimeForConvo(conversation));
		animator.Play("Intro", 0, 0f);
		animator.Update(0f);
		yield return new WaitForEndOfFrame();
		bool hasFirstMessage = false;
		if (!immediateChat && _npcState != null && _npcState.greetingConversation != null)
		{
			hasFirstMessage = true;
			forceWaitTime = 1f;
		}
		if (forceWaitTime < 0f)
		{
			while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
		}
		else
		{
			yield return new WaitForSeconds(forceWaitTime);
		}
		if (hasFirstMessage)
		{
			StartCoroutine(PlayConversation(_npcState.greetingConversation));
		}
		exitButton.enabled = true;
	}

	private void InitDialogueButton(TempDialogueButton button, ConvoButtonConfig config)
	{
		button.Init(config);
		if (config.type == ConvoButtonType.Battle)
		{
			if (Player.instance.activeParty.Count <= 0 || _npcState == null || !_npcState.hasFigures || (Player.instance.HasDefeatedNPC(_npcEntity.npcGuid) && !_npcEntity.NeededInActiveDailyMission()))
			{
				button.Disable();
				return;
			}
			button.Enable();
			if (!(_npcEntity != null) || _npcState == null)
			{
				return;
			}
			TournamentRoundFigureData[] figures = _npcState.battleConfig.figures;
			int num = 1;
			for (int i = 0; i < Player.instance.activeParty.Count; i++)
			{
				if (Player.instance.activeParty[i].level > num)
				{
					num = Player.instance.activeParty[i].level;
				}
			}
			int num2 = 1;
			for (int j = 0; j < figures.Length; j++)
			{
				if (figures[j].level > num2)
				{
					num2 = figures[j].level;
				}
			}
			if (num2 >= num + 3)
			{
				button.skullSprite.gameObject.SetActive(value: true);
				button.levelText.text = Language.Get("LVL", "MENUS,GUI") + " <size=15>" + num2 + "</size>";
			}
		}
		else
		{
			if (config.type != ConvoButtonType.Chat)
			{
				return;
			}
			if (_npcState.mainConversation != null)
			{
				button.Enable();
				if (_npcState.mainConversation.missionUID > -1)
				{
					MissionData missionData = Player.instance.GetMissionData(_npcState.mainConversation.missionUID);
					MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(_npcState.mainConversation.missionUID);
					bool flag = GameManager.instance.communicatorDatabase.GetMission(_npcState.mainConversation.missionUID).type == MissionType.Side;
					missionStar.gameObject.SetActive(missionData == null && missionCompletedData == null && flag);
				}
			}
			else
			{
				missionStar.gameObject.SetActive(value: false);
				button.Disable();
			}
		}
	}

	private tk2dSpriteCollectionData GetSpriteCollectionDataForCharacter(CharacterName character)
	{
		tk2dSpriteCollectionData value = null;
		characterSpriteCollections.TryGetValue(character, out value);
		if (value != null)
		{
			return value;
		}
		value = GameManager.instance.npcDatabase.GetNPCDefByType(character).dialogueSpriteCollection;
		if (!characterSpriteCollections.ContainsKey(character))
		{
			characterSpriteCollections.Add(character, value);
		}
		return value;
	}

	public void Init(Camera camera, CityEntityNPC npcEntity, CityEntityStateNPC npcState)
	{
		_npcEntity = npcEntity;
		_npcState = npcState;
		_treeInstance = this;
		backingNormal.SetActive(value: true);
		int count = cameraAnchors.Count;
		for (int i = 0; i < count; i++)
		{
			cameraAnchors[i].AnchorCamera = camera;
		}
		if (CityController.instance != null)
		{
			CityController.instance.StopAllCityVO();
		}
		AssignInitialSprites((!(npcState.greetingConversation != null)) ? npcState.mainConversation : npcState.greetingConversation);
		StartCoroutine(EntranceSequence());
		RefreshButtonsForNPC(_npcEntity.npcDefinition);
		if (npcEntity.npcDefinition.characterType == CharacterName.SpaceCabbie)
		{
			backingNormal.SetActive(value: false);
			backingCabby.SetActive(value: true);
		}
	}

	private void HandleOnNPCScavengerClick(MissionData missionData, Mission mission)
	{
		if (!conversationPlaying)
		{
			Conversation conversation = Resources.Load<Conversation>(mission.scavengerHuntConvoPaths[UnityEngine.Random.Range(0, mission.scavengerHuntConvoPaths.Length)]);
			if (conversation != null)
			{
				StartCoroutine(PlayConversation(conversation));
			}
		}
		missionData.collectedGUIDs.Add(_npcEntity.npcDefinition.guid);
		CommunicatorManager.instance.UpdateMission(missionData);
		RefreshButtonsForNPC(_npcEntity.npcDefinition);
	}

	private void RefreshButtonsForNPC(NPCDefinition npcDef)
	{
		if (npcDef == null)
		{
			return;
		}
		if (npcDef.convoButtonConfigs.Length > 0)
		{
			InitDialogueButton(leftButton, npcDef.convoButtonConfigs[0]);
			RefreshButtonEnabledState(leftButton);
		}
		if (npcDef.convoButtonConfigs.Length <= 1)
		{
			return;
		}
		ConvoButtonConfig convoButtonConfig = npcDef.convoButtonConfigs[1];
		MissionData missionData = Player.instance.GetMissionDataByType(MissionSideType.NPCScavengerHunt);
		if (missionData != null && !missionData.completed && !missionData.collectedGUIDs.Contains(npcDef.guid))
		{
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
			if (mission.missionLocationCity == CityName.Any || mission.missionLocationCity == CityController.instance.cityName)
			{
				convoButtonConfig = new ConvoButtonConfig(ConvoButtonType.Custom);
				convoButtonConfig.customBackingSprite = mission.scavengerHuntButtonSpriteName;
				convoButtonConfig.customLabel = mission.scavengerHuntButtonLabel;
				convoButtonConfig.customLabelOutlineColor = mission.scavengerHuntButtonLabelOutlineColor;
				convoButtonConfig.buttonCallback = () =>
				{
					HandleOnNPCScavengerClick(missionData, mission);
				};
			}
		}
		InitDialogueButton(rightButton, convoButtonConfig);
		RefreshButtonEnabledState(rightButton);
	}

	public void Init(Camera camera, Conversation conversation, CityEntityNPC npcEntity = null, CityEntityStateNPC npcState = null, bool showExitButton = true, bool skipEntrance = false)
	{
		_treeInstance = this;
		int count = cameraAnchors.Count;
		for (int i = 0; i < count; i++)
		{
			cameraAnchors[i].AnchorCamera = camera;
		}
		if (conversation != null && (conversation.completesMissionUID > -1 || conversation.missionUID > -1))
		{
			showExitButton = false;
		}
		exitButton.gameObject.SetActive(showExitButton);
		leftButton.gameObject.SetActive(value: false);
		rightButton.gameObject.SetActive(value: false);
		blackFade.gameObject.SetActive(conversation.tintScreen);
		if (CityController.instance != null)
		{
			CityController.instance.StopAllCityVO();
		}
		StartCoroutine(ImmediateChat(conversation, npcEntity, npcState, skipEntrance));
	}

	private void AssignInitialSprites(Conversation conversation)
	{
		if (conversation != null)
		{
			if (conversation.assignInitialFaces)
			{
				CharacterName characterName = CharacterName.None;
				CharacterName characterName2 = CharacterName.None;
				string empty = string.Empty;
				string empty2 = string.Empty;
				string empty3 = string.Empty;
				string empty4 = string.Empty;
				if (conversation.useCharacterItems)
				{
					Conversation.CharacterConversation characterConversation = conversation.GetCharacterConversation(CityCharacter.characterType);
					characterName = characterConversation.initialLeftCharacter;
					characterName2 = characterConversation.initialRightCharacter;
					empty = characterConversation.initialLeftBodySpriteName;
					empty2 = characterConversation.initialRightBodySpriteName;
					empty3 = characterConversation.initialLeftFaceSpriteName;
					empty4 = characterConversation.initialRightFaceSpriteName;
				}
				else
				{
					characterName = conversation.initialLeftCharacter;
					characterName2 = conversation.initialRightCharacter;
					empty = conversation.initialLeftBodySpriteName;
					empty2 = conversation.initialRightBodySpriteName;
					empty3 = conversation.initialLeftFaceSpriteName;
					empty4 = conversation.initialRightFaceSpriteName;
				}
				leftCharacter.character = characterName;
				rightCharacter.character = characterName2;
				if (leftCharacter.character != CharacterName.None)
				{
					leftCharacter.gameObject.SetActive(value: true);
					if (!string.IsNullOrEmpty(empty))
					{
						leftCharacter.bodySprite.SetSprite(GetSpriteCollectionDataForCharacter(leftCharacter.character), empty);
					}
					if (!string.IsNullOrEmpty(empty3))
					{
						leftCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(leftCharacter.character), empty3);
					}
				}
				if (rightCharacter.character != CharacterName.None)
				{
					rightCharacter.gameObject.SetActive(value: true);
					if (!string.IsNullOrEmpty(empty2))
					{
						rightCharacter.bodySprite.SetSprite(GetSpriteCollectionDataForCharacter(rightCharacter.character), empty2);
					}
					if (!string.IsNullOrEmpty(empty4))
					{
						rightCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(rightCharacter.character), empty4);
					}
				}
				return;
			}
			List<ConversationItem> list = ((!conversation.useCharacterItems) ? conversation.items : conversation.GetCharacterConversation(CityCharacter.characterType).items);
			int num = -1;
			CharacterName character = CharacterName.None;
			int num2 = -1;
			CharacterName character2 = CharacterName.None;
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].type == ConversationItemType.Dialogue)
				{
					if (num == -1 && list[i].position == AvatarPosition.Left)
					{
						num = i;
						character = list[i].character;
					}
					else if (num2 == -1 && list[i].position == AvatarPosition.Right)
					{
						num2 = i;
						if (list[i].isWildcard)
						{
							character2 = ((!(_npcEntity != null)) ? CharacterName.None : _npcEntity.npcDefinition.characterType);
						}
						else
						{
							character2 = list[i].character;
						}
					}
					if (num > -1 && num2 > -1)
					{
						break;
					}
				}
			}
			leftCharacter.character = character;
			rightCharacter.character = character2;
			if (num > -1 && !conversation.hideLeftCharacter)
			{
				leftCharacter.gameObject.SetActive(value: true);
				leftCharacter.bodySprite.SetSprite(GetSpriteCollectionDataForCharacter(leftCharacter.character), list[num].bodySpriteName);
				leftCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(leftCharacter.character), list[num].faceSpriteName);
			}
			else if (character2.ToString() != CityCharacter.characterType.ToString() && !conversation.hideLeftCharacter)
			{
				CheckStartingCityCharacter(leftCharacter);
			}
			else
			{
				leftCharacter.gameObject.SetActive(value: false);
			}
			if (num2 > -1 && !conversation.hideRightCharacter)
			{
				rightCharacter.gameObject.SetActive(value: true);
				tk2dSpriteCollectionData spriteCollectionDataForCharacter = GetSpriteCollectionDataForCharacter(rightCharacter.character);
				if (list[num2].isWildcard && _npcEntity != null)
				{
					rightCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter, string.IsNullOrEmpty(_npcEntity.npcDefinition.rightConversationBodySprite) ? _npcEntity.npcDefinition.conversationBodySprite : _npcEntity.npcDefinition.rightConversationBodySprite);
					ConversationFace conversationFace = _npcEntity.npcDefinition.conversationFaces[0];
					rightCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter, (!string.IsNullOrEmpty(conversationFace.rightSpriteName)) ? conversationFace.rightSpriteName : conversationFace.spriteName);
				}
				else
				{
					rightCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter, list[num2].bodySpriteName);
					rightCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter, list[num2].faceSpriteName);
				}
			}
			else if (character.ToString() != CityCharacter.characterType.ToString() && !conversation.hideRightCharacter)
			{
				CheckStartingCityCharacter(rightCharacter, AvatarPosition.Right);
			}
			else
			{
				rightCharacter.gameObject.SetActive(value: false);
			}
			return;
		}
		CheckStartingCityCharacter(leftCharacter);
		if (_npcEntity != null)
		{
			rightCharacter.character = _npcEntity.npcDefinition.characterType;
			tk2dSpriteCollectionData spriteCollectionDataForCharacter2 = GetSpriteCollectionDataForCharacter(rightCharacter.character);
			if (spriteCollectionDataForCharacter2 != null)
			{
				rightCharacter.gameObject.SetActive(value: true);
				rightCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter2, string.IsNullOrEmpty(_npcEntity.npcDefinition.rightConversationBodySprite) ? _npcEntity.npcDefinition.conversationBodySprite : _npcEntity.npcDefinition.rightConversationBodySprite);
				if (_npcEntity.npcDefinition.conversationFaces.Count > 0)
				{
					rightCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter2, string.IsNullOrEmpty(_npcEntity.npcDefinition.conversationFaces[0].rightSpriteName) ? _npcEntity.npcDefinition.conversationFaces[0].spriteName : _npcEntity.npcDefinition.conversationFaces[0].rightSpriteName);
				}
			}
			else
			{
				rightCharacter.gameObject.SetActive(value: false);
			}
		}
		else
		{
			rightCharacter.gameObject.SetActive(value: false);
		}
	}

	private IEnumerator ImmediateChat(Conversation conversation, CityEntityNPC npcEntity = null, CityEntityStateNPC npcState = null, bool skipEntrance = false)
	{
		_npcEntity = npcEntity;
		_npcState = npcState;
		AssignInitialSprites(conversation);
		if (!skipEntrance)
		{
			yield return StartCoroutine(EntranceSequence(1f, immediateChat: true, conversation));
		}
		yield return StartCoroutine(PlayConversation(conversation));
		conversationPlaying = false;
		if (tempExitConvo != null)
		{
			Conversation exitConvo = tempExitConvo;
			tempExitConvo = null;
			yield return StartCoroutine(PlayConversation(exitConvo));
			conversationPlaying = false;
		}
		if (!conversationExiting)
		{
			yield return StartCoroutine(ExitSequence());
		}
	}

	public IEnumerator BringInCharacters(Conversation conversation, Camera camera)
	{
		_npcEntity = null;
		_npcState = null;
		exitButton.gameObject.SetActive(value: false);
		int len = cameraAnchors.Count;
		for (int i = 0; i < len; i++)
		{
			cameraAnchors[i].AnchorCamera = camera;
		}
		AssignInitialSprites(conversation);
		yield return StartCoroutine(EntranceSequence(1f, immediateChat: true, conversation));
		conversationPlaying = false;
	}

	private void CheckStartingCityCharacter(DialogueCharacter dialogueCharacter, AvatarPosition position = AvatarPosition.Left)
	{
		dialogueCharacter.gameObject.SetActive(value: true);
		if (CityCharacter.characterType == CityCharacterType.Robin)
		{
			tk2dSpriteCollectionData spriteCollectionDataForCharacter = GetSpriteCollectionDataForCharacter(CharacterName.Robin);
			NPCDefinition nPCDefByType = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.Robin);
			dialogueCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType.rightConversationBodySprite)) ? nPCDefByType.conversationBodySprite : nPCDefByType.rightConversationBodySprite);
			dialogueCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType.conversationFaces[0].rightSpriteName)) ? nPCDefByType.conversationFaces[0].spriteName : nPCDefByType.conversationFaces[0].rightSpriteName);
			dialogueCharacter.character = CharacterName.Robin;
		}
		else if (CityCharacter.characterType == CityCharacterType.BeastBoy)
		{
			tk2dSpriteCollectionData spriteCollectionDataForCharacter2 = GetSpriteCollectionDataForCharacter(CharacterName.BeastBoy);
			NPCDefinition nPCDefByType2 = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.BeastBoy);
			dialogueCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter2, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType2.rightConversationBodySprite)) ? nPCDefByType2.conversationBodySprite : nPCDefByType2.rightConversationBodySprite);
			dialogueCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter2, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType2.conversationFaces[0].rightSpriteName)) ? nPCDefByType2.conversationFaces[0].spriteName : nPCDefByType2.conversationFaces[0].rightSpriteName);
			dialogueCharacter.character = CharacterName.BeastBoy;
		}
		else if (CityCharacter.characterType == CityCharacterType.Raven)
		{
			tk2dSpriteCollectionData spriteCollectionDataForCharacter3 = GetSpriteCollectionDataForCharacter(CharacterName.Raven);
			NPCDefinition nPCDefByType3 = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.Raven);
			dialogueCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter3, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType3.rightConversationBodySprite)) ? nPCDefByType3.conversationBodySprite : nPCDefByType3.rightConversationBodySprite);
			dialogueCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter3, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType3.conversationFaces[0].rightSpriteName)) ? nPCDefByType3.conversationFaces[0].spriteName : nPCDefByType3.conversationFaces[0].rightSpriteName);
			dialogueCharacter.character = CharacterName.Raven;
		}
		else if (CityCharacter.characterType == CityCharacterType.Starfire)
		{
			tk2dSpriteCollectionData spriteCollectionDataForCharacter4 = GetSpriteCollectionDataForCharacter(CharacterName.Starfire);
			NPCDefinition nPCDefByType4 = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.Starfire);
			dialogueCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter4, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType4.rightConversationBodySprite)) ? nPCDefByType4.conversationBodySprite : nPCDefByType4.rightConversationBodySprite);
			dialogueCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter4, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType4.conversationFaces[0].rightSpriteName)) ? nPCDefByType4.conversationFaces[0].spriteName : nPCDefByType4.conversationFaces[0].rightSpriteName);
			dialogueCharacter.character = CharacterName.Starfire;
		}
		else if (CityCharacter.characterType == CityCharacterType.Cyborg)
		{
			tk2dSpriteCollectionData spriteCollectionDataForCharacter5 = GetSpriteCollectionDataForCharacter(CharacterName.Cyborg);
			NPCDefinition nPCDefByType5 = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.Cyborg);
			dialogueCharacter.bodySprite.SetSprite(spriteCollectionDataForCharacter5, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType5.rightConversationBodySprite)) ? nPCDefByType5.conversationBodySprite : nPCDefByType5.rightConversationBodySprite);
			dialogueCharacter.faceSprite.SetSprite(spriteCollectionDataForCharacter5, (position != AvatarPosition.Right || string.IsNullOrEmpty(nPCDefByType5.conversationFaces[0].rightSpriteName)) ? nPCDefByType5.conversationFaces[0].spriteName : nPCDefByType5.conversationFaces[0].rightSpriteName);
			dialogueCharacter.character = CharacterName.Cyborg;
		}
	}

	private void HandleOnDialogueButtonClick(tk2dUIItem uiItem)
	{
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_button_press", GFAudioManager.SFX_GROUP, string.Empty);
		TempDialogueButton component = uiItem.GetComponent<TempDialogueButton>();
		switch (component.config.type)
		{
		case ConvoButtonType.Battle:
			HandleOnBattleClick();
			break;
		case ConvoButtonType.Buy:
			break;
		case ConvoButtonType.Chat:
			HandleOnChatClick();
			break;
		case ConvoButtonType.Shop:
			HandleOnShopClick(component);
			break;
		case ConvoButtonType.Tournament:
			HandleOnTournamentClick(component);
			break;
		case ConvoButtonType.Travel:
			HandleOnTravelClick(component);
			break;
		case ConvoButtonType.Repaint:
			HandleOnRepaintClick(component);
			break;
		case ConvoButtonType.Custom:
			if (component.config.buttonCallback != null)
			{
				component.config.buttonCallback();
			}
			break;
		case ConvoButtonType.LOD:
			HandleOnLODClick(component);
			break;
		}
	}

	private void HandleOnChatClick()
	{
		if (_npcState != null && !conversationPlaying)
		{
			StartCoroutine(PlayConversation(_npcState.mainConversation));
		}
	}

	private void HandleOnBattleClick()
	{
		if (Player.instance.HasDefeatedNPC(_npcEntity.npcGuid) && !_npcEntity.NeededInActiveDailyMission() && _npcState.defeatedConversation != null)
		{
			StartCoroutine(PlayConversation(_npcState.defeatedConversation));
			return;
		}
		bool flag = false;
		Mission mission = null;
		Mission mission2 = null;
		List<MissionData> missionDatasByType = Player.instance.GetMissionDatasByType(MissionSideType.Battle);
		if (missionDatasByType.Count > 0)
		{
			for (int i = 0; i < missionDatasByType.Count; i++)
			{
				MissionData missionData = missionDatasByType[i];
				if (missionData.completed)
				{
					continue;
				}
				Mission mission3 = GameManager.instance.communicatorDatabase.GetMission(missionData.uid);
				for (int j = 0; j < mission3.battleConfigs.Length; j++)
				{
					if (mission3.battleConfigs[j].IsNPCValidTarget(_npcEntity.npcDefinition))
					{
						if (mission3.isDaily && mission2 == null)
						{
							mission2 = mission3;
						}
						else if (!mission3.isDaily && mission == null)
						{
							mission = mission3;
						}
						break;
					}
				}
				if (mission != null && mission2 != null)
				{
					break;
				}
			}
			if (mission != null)
			{
				flag = true;
				List<Character> activeParty = Player.instance.activeParty;
				bool flag2 = true;
				for (int k = 0; k < mission.battleConditions.Length; k++)
				{
					BattleCondition battleCondition = mission.battleConditions[k];
					int num = 0;
					switch (battleCondition.conditionType)
					{
					case BattleCondition.ConditionType.Accessory:
					{
						bool flag3 = Player.instance.IsAccessoryEquipped(battleCondition.intValue1);
						if ((battleCondition.isOrNot1 == IsOrNot.Is && !flag3) || (battleCondition.isOrNot1 == IsOrNot.IsNot && flag3))
						{
							flag2 = false;
						}
						break;
					}
					case BattleCondition.ConditionType.FigureLevel:
					{
						for (int num4 = 0; num4 < activeParty.Count; num4++)
						{
							if (ComparisonUtil.IsComparisonValid(activeParty[num4].level, battleCondition.comparison2, battleCondition.intValue2))
							{
								num++;
							}
						}
						if (!ComparisonUtil.IsComparisonValid(num, Comparison.AtLeast, battleCondition.intValue1))
						{
							flag2 = false;
						}
						break;
					}
					case BattleCondition.ConditionType.FigureName:
					{
						for (int num2 = 0; num2 < activeParty.Count; num2++)
						{
							if (activeParty[num2].characterDefinition.uid == battleCondition.intValue2)
							{
								num++;
							}
						}
						if (!ComparisonUtil.IsComparisonValid(num, Comparison.AtLeast, battleCondition.intValue1))
						{
							flag2 = false;
						}
						break;
					}
					case BattleCondition.ConditionType.FigureShinyRank:
					{
						for (int m = 0; m < activeParty.Count; m++)
						{
							if (ComparisonUtil.IsComparisonValid(activeParty[m].shinyRank, battleCondition.comparison2, battleCondition.intValue2))
							{
								num++;
							}
						}
						if (!ComparisonUtil.IsComparisonValid(num, Comparison.AtLeast, battleCondition.intValue1))
						{
							flag2 = false;
						}
						break;
					}
					case BattleCondition.ConditionType.FigureTags:
					{
						for (int n = 0; n < activeParty.Count; n++)
						{
							if (activeParty[n].characterDefinition.tags.Contains(battleCondition.stringValue1))
							{
								num++;
							}
						}
						if (!ComparisonUtil.IsComparisonValid(num, Comparison.AtLeast, battleCondition.intValue1))
						{
							flag2 = false;
						}
						break;
					}
					case BattleCondition.ConditionType.FigureClass:
					{
						for (int num3 = 0; num3 < activeParty.Count; num3++)
						{
							if (activeParty[num3].characterDefinition.characterClass == (CharacterClass)battleCondition.intValue2)
							{
								num++;
							}
						}
						if (!ComparisonUtil.IsComparisonValid(num, Comparison.AtLeast, battleCondition.intValue1))
						{
							flag2 = false;
						}
						break;
					}
					case BattleCondition.ConditionType.FigureRepainted:
					{
						for (int l = 0; l < activeParty.Count; l++)
						{
							if (activeParty[l].repaintColorID > 0)
							{
								num++;
							}
						}
						if (!ComparisonUtil.IsComparisonValid(num, Comparison.AtLeast, battleCondition.intValue1))
						{
							flag2 = false;
						}
						break;
					}
					}
					if (!flag2)
					{
						break;
					}
				}
				if (!flag2)
				{
					if (mission.battleConditionFailConvo != null)
					{
						StartCoroutine(PlayConversation(mission.battleConditionFailConvo));
					}
					else
					{
						Debug.LogError("Assign an alternate conversation!");
					}
					return;
				}
			}
		}
		if (!flag && _npcEntity != null && _npcState != null && _npcState.battleRequirements.useRequirements && !conversationPlaying)
		{
			List<Character> activeParty2 = Player.instance.activeParty;
			List<int> list = new List<int>();
			int num5 = 0;
			NPCBattleCondition nPCBattleCondition = null;
			for (int num6 = 0; num6 < _npcState.battleRequirements.conditions.Length; num6++)
			{
				nPCBattleCondition = _npcState.battleRequirements.conditions[num6];
				for (int num7 = 0; num7 < activeParty2.Count; num7++)
				{
					if (list.Contains(num7))
					{
						continue;
					}
					Character character = activeParty2[num7];
					if (nPCBattleCondition.type == NPCBattleConditionType.MinLevel)
					{
						if (character.level >= nPCBattleCondition.amount)
						{
							num5++;
							list.Add(num7);
						}
					}
					else if (nPCBattleCondition.type == NPCBattleConditionType.GoldAbility)
					{
						if (character.shinyRank >= nPCBattleCondition.amount)
						{
							num5++;
							list.Add(num7);
						}
					}
					else if (nPCBattleCondition.type == NPCBattleConditionType.Character)
					{
						CharacterDefinition character2 = GameManager.instance.characterDatabase.GetCharacter(nPCBattleCondition.uid);
						CharacterDefinition character3 = GameManager.instance.characterDatabase.GetCharacter(nPCBattleCondition.uid);
						if (character.characterDefinition.characterName.ToLower().Contains(character2.characterName.ToLower()))
						{
							num5++;
							list.Add(num7);
						}
					}
					else if (nPCBattleCondition.type == NPCBattleConditionType.IsVillainFigure)
					{
						if (character.characterDefinition.isVillainFigure)
						{
							num5++;
							list.Add(num7);
						}
					}
					else if (nPCBattleCondition.type == NPCBattleConditionType.Is80sFigure && character.characterDefinition.is80sFigure)
					{
						num5++;
						list.Add(num7);
					}
				}
			}
			if (num5 < _npcState.battleRequirements.conditions.Length)
			{
				if (_npcState.battleRequirements.conversation != null)
				{
					StartCoroutine(PlayConversation(_npcState.battleRequirements.conversation));
				}
				else
				{
					Debug.LogError("Assign an alternate conversation!");
				}
				return;
			}
		}
		if (_npcEntity != null && _npcState != null && _npcState.preBattleConversation != null)
		{
			foreach (DialogueBubble activeBubble in activeBubbles)
			{
				if (activeBubble != null)
				{
					UnityEngine.Object.Destroy(activeBubble.gameObject);
				}
			}
			activeBubbles.Clear();
			Conversation preBattleConversation = _npcState.preBattleConversation;
			if (preBattleConversation.triggersBattle)
			{
				StartCoroutine(PlayConversation(preBattleConversation));
			}
			else
			{
				StartCoroutine(PlayConvoAndTriggerBattle(preBattleConversation));
			}
		}
		else if (BeginBattle != null)
		{
			BeginBattle(_npcEntity, _npcState, mission, mission2);
		}
	}

	private IEnumerator PlayConvoAndTriggerBattle(Conversation conversation, Mission battleMission = null, Mission dailyBattleMission = null)
	{
		yield return StartCoroutine(PlayConversation(conversation));
		HandleOnExitClick();
		if (!animator.IsName("Outro"))
		{
			animator.Play("Outro", 0, 0f);
			yield return new WaitForEndOfFrame();
			while (animator.NormalizedTime() < 0.98f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		if (BeginBattle != null)
		{
			BeginBattle(_npcEntity, _npcState, battleMission, dailyBattleMission);
		}
	}

	private void HandleOnBuyClick(TempDialogueButton button)
	{
		if (Player.instance.HasCurrency(button.config.currencyType, button.config.cost))
		{
			Player.instance.RemoveCurrency(button.config.currencyType, button.config.cost);
			button.Disable();
		}
	}

	private void HandleOnTravelClick(TempDialogueButton button)
	{
		if (button.cityName == CityName.Any)
		{
			switch (CityController.instance.cityName)
			{
			case CityName.JumpCity:
				leftButton.Init(button.config, CityName.Gotham);
				rightButton.Init(button.config, CityName.Metropolis);
				break;
			case CityName.Gotham:
				leftButton.Init(button.config, CityName.JumpCity);
				rightButton.Init(button.config, CityName.Metropolis);
				break;
			case CityName.Metropolis:
				leftButton.Init(button.config, CityName.JumpCity);
				rightButton.Init(button.config, CityName.Gotham);
				break;
			}
			if (Player.instance.HasCurrency(CurrencyType.Coins, GameManager.instance.gameSettings.spaceCabbieTravelCost))
			{
				leftButton.Enable();
				rightButton.Enable();
			}
			else
			{
				leftButton.Disable();
				rightButton.Disable();
			}
			animator.Play("Show_Buttons");
			animator.Update(0f);
			currencyGUI.Show();
		}
		else if (Player.instance.HasTriggered("visited_" + button.cityName))
		{
			if (Player.instance.HasCurrency(CurrencyType.Coins, GameManager.instance.gameSettings.spaceCabbieTravelCost))
			{
				Player.instance.RemoveCurrency(CurrencyType.Coins, GameManager.instance.gameSettings.spaceCabbieTravelCost);
				switch (button.cityName)
				{
				case CityName.JumpCity:
					HandleOnTravelToJumpCityClick();
					break;
				case CityName.Gotham:
					HandleOnTravelToGothamClick();
					break;
				case CityName.Metropolis:
					HandleOnTravelToMetropolisClick();
					break;
				}
			}
		}
		else
		{
			Conversation conversation = Resources.Load<Conversation>("Data/TT2/CONVO_SPACECABBIE_DENIAL");
			if (conversation != null)
			{
				StartCoroutine(PlayConversation(conversation));
			}
		}
	}

	private void HandleOnRepaintClick(TempDialogueButton button)
	{
		HandleOnExitClick();
		EventMessenger.Instance.Raise(new ExitCityEvent("RepaintShop", string.Empty));
	}

	private void HandleOnShopClick(TempDialogueButton button)
	{
		ShopDefinition shopDef = GameManager.instance.shopDatabase.GetShopDef(button.config.shopUID);
		HandleOnExitClick();
		EventMessenger.Instance.Raise(new EnterShopEvent(shopDef));
	}

	private void HandleOnTournamentClick(TempDialogueButton button)
	{
		if (!(button.config.tournament != null))
		{
			return;
		}
		if (Player.instance.isActivePartyFull)
		{
			HandleOnExitClick();
			EventMessenger.Instance.Raise(new StartTournamentEvent(button.config.tournament));
			return;
		}
		Conversation conversation = Resources.Load<Conversation>("Data/TT2/CONVO_TOURNYREQ_SIXFIGS");
		if (conversation != null)
		{
			StartCoroutine(PlayConversation(conversation));
		}
	}

	private void HandleOnLODClick(TempDialogueButton button)
	{
		HandleOnExitClick();
		EventMessenger.Instance.Raise(new ExitCityEvent("LegionOfDoomMode", string.Empty));
	}

	private void HandleOnExitClick()
	{
		conversationPlaying = false;
		while (activeBubbles.Count > 0)
		{
			UnityEngine.Object.Destroy(activeBubbles.Pop().gameObject);
		}
		AudioSource audioSourceById = GFAudioManager.instance.GetAudioSourceById("printText");
		if (audioSourceById != null)
		{
			GFAudioManager.instance.StopById("printText");
		}
		currencyGUI.Hide();
		StartCoroutine("PlayOutroAndDestroy");
	}

	private void HandleOnTravelToJumpCityClick()
	{
		EventMessenger.Instance.Raise(new ExitCityEvent("City_JumpCityNew", "PathNode_SpaceCabbie"));
	}

	private void HandleOnTravelToGothamClick()
	{
		EventMessenger.Instance.Raise(new ExitCityEvent("City_Gotham", "PathNode_SpaceCabbie"));
	}

	private void HandleOnTravelToMetropolisClick()
	{
		EventMessenger.Instance.Raise(new ExitCityEvent("City_Metropolis", "PathNode_SpaceCabbie"));
	}

	private void HandleOnExitDown()
	{
		exitButtonDown = true;
	}

	private void HandleOnExitRelease()
	{
		exitButtonDown = false;
	}

	private void HandleOnExitClickWithMission()
	{
		conversationPlaying = false;
		foreach (DialogueBubble activeBubble in activeBubbles)
		{
			UnityEngine.Object.Destroy(activeBubble.gameObject);
		}
		AudioSource audioSourceById = GFAudioManager.instance.GetAudioSourceById("printText");
		if (audioSourceById != null)
		{
			GFAudioManager.instance.StopById("printText");
		}
		StopCoroutine("PlayOutroAndDestroy");
		StartCoroutine("PlayOutroAndDestroyWithMission");
	}

	private IEnumerator PlayOutroAndDestroyWithMission()
	{
		conversationExiting = true;
		int missionUID = _conversation.missionUID;
		leftButton.Disable();
		rightButton.Disable();
		if (!animator.IsName("Outro"))
		{
			animator.Play("Outro", 0, 0f);
			yield return new WaitForEndOfFrame();
			while (animator.NormalizedTime() < 0.98f)
			{
				yield return new WaitForEndOfFrame();
			}
		}
		if (CityReward.instance != null && CityReward.instance.showingRewards)
		{
			yield return new WaitWhile(() => CityReward.instance.showingRewards);
		}
		if (_conversation.triggersBattle && _npcEntity != null && _npcState != null)
		{
			CityController.instance.DialogueBattleTriggered(_npcEntity, _npcState);
		}
		else if (_conversation.triggersTournament && _conversation.tournament != null)
		{
			EventMessenger.Instance.Raise(new StartTournamentEvent(_conversation.tournament));
		}
		else if (_conversation.triggersLoadScene && !string.IsNullOrEmpty(_conversation.sceneName))
		{
			EventMessenger.Instance.Raise(new ExitCityEvent(_conversation.sceneName, _conversation.pathNodeName));
		}
		yield return null;
		Player.instance.AddDialogueMission(missionUID);
		UnityEngine.Object.Destroy(gameObject, 0.01f);
	}

	private IEnumerator PlayOutroAndDestroy()
	{
		conversationExiting = true;
		leftButton.Disable();
		rightButton.Disable();
		if (!animator.IsName("Outro"))
		{
			animator.Play("Outro", 0, 0f);
			yield return new WaitForSeconds(1f);
		}
		else
		{
			yield return new WaitForEndOfFrame();
		}
		if (_npcEntity != null)
		{
			string text = string.Empty;
			switch (_npcEntity.npcDefinition.characterType)
			{
			case CharacterName.BeastBoy:
				if (beastBoyChatEndVO.Length > 0)
				{
					text = beastBoyChatEndVO[UnityEngine.Random.Range(0, beastBoyChatEndVO.Length)];
				}
				break;
			case CharacterName.Cyborg:
				if (cyborgChatEndVO.Length > 0)
				{
					text = cyborgChatEndVO[UnityEngine.Random.Range(0, cyborgChatEndVO.Length)];
				}
				break;
			case CharacterName.Raven:
				if (ravenChatEndVO.Length > 0)
				{
					text = ravenChatEndVO[UnityEngine.Random.Range(0, ravenChatEndVO.Length)];
				}
				break;
			case CharacterName.Starfire:
				if (starfireChatEndVO.Length > 0)
				{
					text = starfireChatEndVO[UnityEngine.Random.Range(0, starfireChatEndVO.Length)];
				}
				break;
			}
			if (!string.IsNullOrEmpty(text) && !GFAudioManager.instance.IsGroupPlaying(GFAudioManager.VOICEOVER_GROUP))
			{
				AudioSource voSource = GFAudioManager.instance.PlayClipFromResources("Audio/VO/" + text, GFAudioManager.VOICEOVER_GROUP, string.Empty);
				CityController.instance.RegisterCityVO(voSource);
			}
		}
		if (CityReward.instance != null && CityReward.instance.showingRewards)
		{
			yield return new WaitWhile(() => CityReward.instance.showingRewards);
		}
		UnityEngine.Object.Destroy(gameObject, 0.01f);
	}

	public IEnumerator ExitSequenceDoNotDestroy()
	{
		conversationExiting = true;
		leftButton.Disable();
		rightButton.Disable();
		if (!animator.IsName("Outro"))
		{
			animator.Play("Outro", 0, 0f);
			yield return new WaitForEndOfFrame();
			while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
		}
		yield return new WaitForEndOfFrame();
		conversationExiting = false;
		gameObject.SetActive(value: false);
	}

	private IEnumerator ExitSequence()
	{
		conversationExiting = true;
		leftButton.Disable();
		rightButton.Disable();
		if (!animator.IsName("Outro"))
		{
			animator.Play("Outro", 0, 0f);
			yield return new WaitForEndOfFrame();
			while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
		}
		yield return new WaitForEndOfFrame();
		UnityEngine.Object.Destroy(gameObject);
	}

	public void ResetObject()
	{
		leftCharacter.gameObject.SetActive(value: true);
		rightCharacter.gameObject.SetActive(value: true);
		backing.gameObject.SetActive(value: true);
		leftCharacter.faceSprite.gameObject.SetActive(value: true);
		rightCharacter.faceSprite.gameObject.SetActive(value: true);
	}

	private void TrackPlayedConversation(string conversationName)
	{
		if (conversationName.Equals("CONVO_PetStoreSign_part1"))
		{
			SaveUtility.SetInt(conversationName, 1, save: false);
		}
		if (conversationName.Equals("CONVO_SHADYTOURNYLOCKED"))
		{
			SaveUtility.SetInt(conversationName, 1, save: false);
		}
		if (conversationName.Equals("CONVO_STICKYJOE_AFTERTOURNAMENT"))
		{
			SaveUtility.SetInt(conversationName, 1, save: false);
		}
	}

	public IEnumerator PlayConversation(Conversation conversation)
	{
		if (conversation == null)
		{
			yield break;
		}
		TrackPlayedConversation(conversation.name);
		_conversation = conversation;
		conversationPlaying = true;
		conversationExiting = false;
		leftButton.Disable();
		rightButton.Disable();
		List<ConversationItem> items = ((!conversation.useCharacterItems) ? conversation.items : conversation.GetCharacterConversation(CityCharacter.characterType).items);
		curIndex = 0;
		nextItem = ((items.Count <= 0) ? null : items[curIndex]);
		while (nextItem != null)
		{
			if (nextItem == null)
			{
				yield return null;
			}
			else
			{
				yield return StartCoroutine(ProcessConversationItem(nextItem));
			}
		}
		Player.instance.TrackConversationPlayed(conversation);
		while (activeBubbles.Count > 0)
		{
			StartCoroutine(RemoveBubble(activeBubbles[activeBubbles.Count - 1]));
			activeBubbles.RemoveAt(activeBubbles.Count - 1);
		}
		if (!string.IsNullOrEmpty(conversation.achievementId))
		{
			AchievementsDatabase.UnlockAchievementId(conversation.achievementId);
		}
		float waitLength = 0f;
		curChoice = null;
		activeBubbles.Clear();
		conversationHistory.Clear();
		conversationPlaying = false;
		RefreshButtonEnabledState(leftButton);
		RefreshButtonEnabledState(rightButton);
	}

	private void RefreshButtonEnabledState(TempDialogueButton button)
	{
		if (button.config == null)
		{
			return;
		}
		if (button.config.type == ConvoButtonType.Battle)
		{
			if (conversationPlaying || _npcState == null || !_npcState.hasFigures || (Player.instance.HasDefeatedNPC(_npcEntity.npcGuid) && !_npcEntity.NeededInActiveDailyMission()))
			{
				button.Disable();
			}
			else
			{
				button.Enable();
			}
		}
		else if (button.config.type == ConvoButtonType.Chat)
		{
			if (!conversationExiting)
			{
				button.Enable();
			}
			else
			{
				button.Disable();
			}
		}
		else if (!conversationPlaying)
		{
			button.Enable();
		}
		else
		{
			button.Disable();
		}
	}

	private IEnumerator ProcessConversationItem(ConversationItem item)
	{
		switch (item.type)
		{
		case ConversationItemType.Dialogue:
			yield return StartCoroutine(ProcessDialogue(item));
			break;
		case ConversationItemType.Reward:
			yield return StartCoroutine(ProcessReward(item));
			break;
		case ConversationItemType.Bribe:
			yield return StartCoroutine(ProcessBribe(item));
			break;
		case ConversationItemType.Event:
			yield return StartCoroutine(ProcessEvent(item));
			break;
		}
	}

	private IEnumerator ProcessDialogue(ConversationItem dialogue)
	{
		if (!conversationPlaying)
		{
			yield break;
		}
		DialogueCharacter dialogueCharacter;
		Vector3 bubbleStartPosition;
		bool isFlipped;
		if (dialogue.position == AvatarPosition.Left)
		{
			dialogueCharacter = leftCharacter;
			string characterIntroAnim = DialogueCharacterAnim.DialogueTree_CharacterLeft_Intro.ToString();
			bubbleStartPosition = leftBubbleStart;
			isFlipped = true;
			if (curIndex > 0 && !string.IsNullOrEmpty(dialogue.otherFaceSpriteName) && rightCharacter.character != CharacterName.None)
			{
				rightCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(rightCharacter.character), dialogue.otherFaceSpriteName);
			}
		}
		else
		{
			dialogueCharacter = rightCharacter;
			string characterIntroAnim = DialogueCharacterAnim.DialogueTree_CharacterRight_Intro.ToString();
			bubbleStartPosition = rightBubbleStart;
			isFlipped = false;
			if (curIndex > 0 && !string.IsNullOrEmpty(dialogue.otherFaceSpriteName))
			{
				leftCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(leftCharacter.character), dialogue.otherFaceSpriteName);
			}
		}
		bool isSwitching = false;
		if (!dialogue.isWildcard && dialogueCharacter.character != dialogue.character && (dialogue.position != AvatarPosition.Right || dialogueCharacter.character != CharacterName.Robin))
		{
			isSwitching = true;
		}
		CharacterName previousCharacter = dialogueCharacter.character;
		if (!dialogue.isWildcard)
		{
			dialogueCharacter.character = dialogue.character;
		}
		DialogueBubble bubble = null;
		string message = dialogue.message;
		if (!string.IsNullOrEmpty(message))
		{
			bubble = GetDialogueBubble();
			bubble.transform.localPosition = bubbleStartPosition;
			bubble.character = ((!dialogue.isWildcard) ? dialogue.character : dialogueCharacter.character);
			if (!string.IsNullOrEmpty(dialogue.key))
			{
				message = Language.Get(dialogue.key, "DIALOGUE");
			}
			if (dialogue.formatArgs != null && dialogue.formatArgs.Length > 0)
			{
				object[] array = new object[dialogue.formatArgs.Length];
				for (int i = 0; i < array.Length; i++)
				{
					string text = dialogue.formatArgs[i];
					if (text != null && text == "[PVP_PLAYER_NAME]")
					{
						array[i] = Player.instance.pvpData.PlayerName;
					}
					else
					{
						array[i] = string.Empty;
					}
				}
				message = string.Format(message, array);
			}
			if (dialogue.position == AvatarPosition.Right)
			{
				bubble.tailSprite.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
			}
			bubble.SetText(message);
			ShiftActiveBubbles(bubble.background.dimensions.y + 5f);
			bubble.gameObject.SetActive(value: false);
		}
		if (!dialogueCharacter.gameObject.activeSelf)
		{
			if (!dialogue.isWildcard)
			{
				string bodySpriteName = dialogue.bodySpriteName;
				dialogueCharacter.bodySprite.SetSprite(GetSpriteCollectionDataForCharacter(dialogueCharacter.character), bodySpriteName);
				dialogueCharacter.faceSprite.gameObject.SetActive(!string.IsNullOrEmpty(dialogue.faceSpriteName) && dialogueCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(dialogueCharacter.character), dialogue.faceSpriteName));
			}
			Vector3 vector = new Vector3(Mathf.Sign(dialogueCharacter.bodySprite.scale.x) * 1f, 1f, 1f);
			tk2dSprite bodySprite = dialogueCharacter.bodySprite;
			Vector3 scale = vector;
			dialogueCharacter.faceSprite.scale = scale;
			bodySprite.scale = scale;
			if ((dialogue.position == AvatarPosition.Left && !conversation.hideLeftCharacter) || (dialogue.position == AvatarPosition.Right && !conversation.hideRightCharacter))
			{
				dialogueCharacter.gameObject.SetActive(value: true);
			}
		}
		else if (isSwitching)
		{
			yield return StartCoroutine(SwitchCharacters(dialogueCharacter, dialogue, previousCharacter));
		}
		else
		{
			if (!dialogue.isWildcard)
			{
				string bodySpriteName2 = dialogue.bodySpriteName;
				dialogueCharacter.bodySprite.SetSprite(GetSpriteCollectionDataForCharacter(dialogueCharacter.character), bodySpriteName2);
				dialogueCharacter.faceSprite.gameObject.SetActive(!string.IsNullOrEmpty(dialogue.faceSpriteName) && dialogueCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(dialogueCharacter.character), dialogue.faceSpriteName));
			}
			Vector3 vector2 = ((!dialogue.isPrismCreature) ? new Vector3(Mathf.Sign(dialogueCharacter.bodySprite.scale.x) * 1f, 1f, 1f) : new Vector3(Mathf.Sign(dialogueCharacter.bodySprite.scale.x) * 2f, 2f, 1f));
			tk2dSprite bodySprite2 = dialogueCharacter.bodySprite;
			Vector3 scale2 = vector2;
			dialogueCharacter.faceSprite.scale = scale2;
			bodySprite2.scale = scale2;
		}
		if (!string.IsNullOrEmpty(message))
		{
			if (!isSwitching)
			{
				yield return new WaitForSeconds(0.3f);
			}
			bubble.gameObject.SetActive(value: true);
			bubble.isFlipped = isFlipped;
			yield return new WaitForEndOfFrame();
			while (bubble.animator.IsPlaying())
			{
				yield return null;
			}
			bubble.animator.enabled = false;
			if (dialogue.clip != null)
			{
				GFAudioManager.instance.PlayClip(dialogue.clip, 1f, loop: false, 0f, 128, GFAudioManager.VOICEOVER_GROUP);
			}
			yield return StartCoroutine(bubble.Print());
			activeBubbles.Insert(0, bubble);
			while (!Input.GetMouseButtonDown(0) || exitButtonDown || LODRequestFlag.isInviteScreenActive)
			{
				yield return null;
			}
			if (dialogue.position == AvatarPosition.Left)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_nextbubble1", GFAudioManager.SFX_GROUP, string.Empty);
			}
			else
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_dialogue_nextbubble2", GFAudioManager.SFX_GROUP, string.Empty);
			}
		}
		if (dialogue.isEnd)
		{
			nextItem = null;
			CheckCompletesMission();
			bool addsMission = CheckAddsMission();
			if (CityReward.instance != null && CityReward.instance.showingRewards)
			{
				yield return new WaitWhile(() => CityReward.instance.showingRewards);
			}
			if (!addsMission && _conversation.triggersBattle && _npcEntity != null && _npcState != null)
			{
				CityController.instance.DialogueBattleTriggered(_npcEntity, _npcState);
			}
			else if (!addsMission && _conversation.triggersTournament && _conversation.tournament != null)
			{
				if (Player.instance.isActivePartyFull)
				{
					EventMessenger.Instance.Raise(new StartTournamentEvent(_conversation.tournament));
				}
				else
				{
					tempExitConvo = Resources.Load<Conversation>("Data/TT2/CONVO_TOURNYREQ_SIXFIGS");
				}
			}
			else if (!addsMission && _conversation.triggersLoadScene && !string.IsNullOrEmpty(_conversation.sceneName))
			{
				EventMessenger.Instance.Raise(new ExitCityEvent(_conversation.sceneName, _conversation.pathNodeName));
			}
		}
		else
		{
			curIndex++;
			List<ConversationItem> list = ((!_conversation.useCharacterItems) ? _conversation.items : _conversation.GetCharacterConversation(CityCharacter.characterType).items);
			nextItem = ((curIndex >= list.Count) ? null : list[curIndex]);
		}
	}

	private IEnumerator ProcessReward(ConversationItem item)
	{
		if (item.rewards.Length > 0)
		{
			bool previouslyEnabled = exitButton.enabled;
			exitButton.enabled = false;
			animator.enabled = false;
			while (activeBubbles.Count > 0)
			{
				StartCoroutine(RemoveBubble(activeBubbles[activeBubbles.Count - 1]));
				activeBubbles.RemoveAt(activeBubbles.Count - 1);
			}
			if (leftCharacter.gameObject.activeSelf)
			{
				leftCharacter.transform.DOLocalMove(new Vector3(-400f, leftCharacter.transform.localPosition.y, leftCharacter.transform.localPosition.z), 0.3f).SetEase(Ease.InSine);
			}
			if (rightCharacter.gameObject.activeSelf)
			{
				rightCharacter.transform.DOLocalMove(new Vector3(400f, rightCharacter.transform.localPosition.y, rightCharacter.transform.localPosition.z), 0.3f).SetEase(Ease.InSine);
			}
			int len = item.rewards.Length;
			for (int i = 0; i < len; i++)
			{
				yield return StartCoroutine(CityReward.instance.ShowPlayerReward(item.rewards[i]));
			}
			if (leftCharacter.gameObject.activeSelf)
			{
				leftCharacter.transform.DOLocalMove(new Vector3(-138f, leftCharacter.transform.localPosition.y, leftCharacter.transform.localPosition.z), 0.3f).SetEase(Ease.OutSine);
			}
			if (rightCharacter.gameObject.activeSelf)
			{
				rightCharacter.transform.DOLocalMove(new Vector3(138f, rightCharacter.transform.localPosition.y, rightCharacter.transform.localPosition.z), 0.3f).SetEase(Ease.OutSine);
			}
			yield return new WaitForSeconds(0.3f);
			exitButton.enabled = previouslyEnabled;
			animator.enabled = true;
		}
		if (item.isEnd)
		{
			nextItem = null;
			CheckCompletesMission();
			bool addsMission = CheckAddsMission();
			if (CityReward.instance != null && CityReward.instance.showingRewards)
			{
				yield return new WaitWhile(() => CityReward.instance.showingRewards);
			}
			if (!addsMission && _conversation.triggersBattle && _npcEntity != null && _npcState != null)
			{
				CityController.instance.DialogueBattleTriggered(_npcEntity, _npcState);
			}
			else if (!addsMission && _conversation.triggersTournament && _conversation.tournament != null)
			{
				EventMessenger.Instance.Raise(new StartTournamentEvent(_conversation.tournament));
			}
			else if (!addsMission && _conversation.triggersLoadScene && !string.IsNullOrEmpty(_conversation.sceneName))
			{
				EventMessenger.Instance.Raise(new ExitCityEvent(_conversation.sceneName, _conversation.pathNodeName));
			}
		}
		else
		{
			curIndex++;
			List<ConversationItem> list = ((!_conversation.useCharacterItems) ? _conversation.items : _conversation.GetCharacterConversation(CityCharacter.characterType).items);
			nextItem = ((curIndex >= list.Count) ? null : list[curIndex]);
		}
	}

	private IEnumerator ProcessBribe(ConversationItem item)
	{
		PopupDialog dialog = ObjectPool.GetObjectFromResource<PopupDialog>("Prefabs/GUI/PopUP_Window_Coins", default, isActive: false);
		dialog.transform.SetParent(transform.parent, worldPositionStays: true);
		dialog.transform.localPosition = new Vector3(0f, 0f, -200f);
		dialog.headerText.text = ((!string.IsNullOrEmpty(item.bribeHeader.key)) ? Language.Get(item.bribeHeader.key, "MISSIONS") : item.bribeHeader.text);
		dialog.descriptionText.text = ((!string.IsNullOrEmpty(item.bribeMessageBody.key)) ? Language.Get(item.bribeMessageBody.key, "MISSIONS") : item.bribeMessageBody.text);
		dialog.UpdateWithAmount(item.bribeCurrencyType, item.bribeAmount);
		dialog.gameObject.SetActive(value: true);
		if (!Player.instance.HasCurrency(item.bribeCurrencyType, item.bribeAmount))
		{
			dialog.DisableConfirmButton();
		}
		else
		{
			dialog.EnableConfirmButton();
		}
		while (dialog.gameObject.activeSelf)
		{
			yield return null;
		}
		if (dialog.isConfirmed)
		{
			Player.instance.RemoveCurrency(item.bribeCurrencyType, item.bribeAmount);
			if (item.bribeCompletesMissionUid > -1)
			{
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(item.bribeCompletesMissionUid);
				Player.instance.CompleteMission(item.bribeCompletesMissionUid, mission.autoCollect);
				if (mission.autoCollect && item.isEnd)
				{
					CityReward.instance.ShowRewardForMission(item.bribeCompletesMissionUid);
					HandleOnExitClick();
				}
			}
			ObjectPool.PoolObject(dialog.gameObject);
		}
		else
		{
			ObjectPool.PoolObject(dialog.gameObject);
		}
		if (item.isEnd)
		{
			nextItem = null;
			CheckCompletesMission();
			bool addsMission = CheckAddsMission();
			if (CityReward.instance != null && CityReward.instance.showingRewards)
			{
				yield return new WaitWhile(() => CityReward.instance.showingRewards);
			}
			if (!addsMission && _conversation.triggersBattle && _npcEntity != null && _npcState != null)
			{
				CityController.instance.DialogueBattleTriggered(_npcEntity, _npcState);
			}
			else if (!addsMission && _conversation.triggersTournament && _conversation.tournament != null)
			{
				EventMessenger.Instance.Raise(new StartTournamentEvent(_conversation.tournament));
			}
			else if (!addsMission && _conversation.triggersLoadScene && !string.IsNullOrEmpty(_conversation.sceneName))
			{
				EventMessenger.Instance.Raise(new ExitCityEvent(_conversation.sceneName, _conversation.pathNodeName));
			}
		}
		else
		{
			curIndex++;
			List<ConversationItem> list = ((!_conversation.useCharacterItems) ? _conversation.items : _conversation.GetCharacterConversation(CityCharacter.characterType).items);
			nextItem = ((curIndex >= list.Count) ? null : list[curIndex]);
		}
	}

	private IEnumerator ProcessEvent(ConversationItem item)
	{
		switch (item.eventConfig.type)
		{
		case ConversationEventType.EnterShop:
			EventMessenger.Instance.Raise(new EnterShopEvent(GameManager.instance.shopDatabase.GetShopDef(item.eventConfig.shopUID)));
			break;
		case ConversationEventType.LoadScene:
			EventMessenger.Instance.Raise(new ExitCityEvent(item.eventConfig.sceneName, item.eventConfig.pathNodeName));
			break;
		case ConversationEventType.ActivateMission:
			Player.instance.AddMission(item.eventConfig.missionUID);
			break;
		case ConversationEventType.SetTrigger:
			Player.instance.SetTrigger(item.eventConfig.triggerName);
			break;
		case ConversationEventType.UpdateMission:
			CommunicatorManager.instance.UpdateMissionWithTargetMessage(item.eventConfig.targetMessage);
			break;
		case ConversationEventType.Save:
			Player.instance.Save();
			break;
		case ConversationEventType.PlaySound:
			GFAudioManager.instance.PlayClipFromResources(item.eventConfig.soundPath, item.eventConfig.soundGroup, string.Empty, item.eventConfig.volume, 0f, item.eventConfig.loop);
			break;
		}
		if (item.isEnd)
		{
			nextItem = null;
			CheckCompletesMission();
			bool addsMission = CheckAddsMission();
			if (CityReward.instance != null && CityReward.instance.showingRewards)
			{
				yield return new WaitWhile(() => CityReward.instance.showingRewards);
			}
			if (!addsMission && _conversation.triggersBattle && _npcEntity != null && _npcState != null)
			{
				CityController.instance.DialogueBattleTriggered(_npcEntity, _npcState);
			}
			else if (!addsMission && _conversation.triggersTournament && _conversation.tournament != null)
			{
				EventMessenger.Instance.Raise(new StartTournamentEvent(_conversation.tournament));
			}
			else if (!addsMission && _conversation.triggersLoadScene && !string.IsNullOrEmpty(_conversation.sceneName))
			{
				EventMessenger.Instance.Raise(new ExitCityEvent(_conversation.sceneName, _conversation.pathNodeName));
			}
		}
		else
		{
			curIndex++;
			List<ConversationItem> list = ((!_conversation.useCharacterItems) ? _conversation.items : _conversation.GetCharacterConversation(CityCharacter.characterType).items);
			nextItem = ((curIndex >= list.Count) ? null : list[curIndex]);
		}
	}

	private void CheckCompletesMission()
	{
		if (_conversation.completesMissionUID > -1)
		{
			MissionData missionData = Player.instance.GetMissionData(_conversation.completesMissionUID);
			if (missionData != null)
			{
				int uid = missionData.uid;
				Mission mission = GameManager.instance.communicatorDatabase.GetMission(uid);
				bool flag = mission.type == MissionType.Story || mission.autoCollect;
				Player.instance.CompleteMission(uid, flag);
				if (flag)
				{
					CityReward.instance.ShowRewardForMission(_conversation.completesMissionUID);
					HandleOnExitClick();
				}
			}
		}
		else
		{
			if (!(_cityNPC != null) || _cityNPC.activeNPCData == null)
			{
				return;
			}
			if (_cityNPC.activeNPCData.missionCompleteConversation == _conversation && _cityNPC.activeNPCData.initialConversation.missionUID > -1)
			{
				int missionUID = _cityNPC.activeNPCData.initialConversation.missionUID;
				if (Player.instance.GetMissionData(missionUID) != null)
				{
					CityReward.instance.ShowRewardForMission(missionUID);
					Player.instance.CollectMission(missionUID);
					HandleOnExitClick();
				}
			}
			else
			{
				if (!(_cityNPC.activeNPCData.missionCompleteConversation == _conversation) || _cityNPC.activeNPCData.preBattleConversations.Count <= 0)
				{
					return;
				}
				for (int i = 0; i < _cityNPC.activeNPCData.preBattleConversations.Count; i++)
				{
					if (_cityNPC.activeNPCData.preBattleConversations[i].missionUID > -1)
					{
						int missionUID2 = _cityNPC.activeNPCData.preBattleConversations[i].missionUID;
						if (Player.instance.GetMissionData(missionUID2) != null)
						{
							CityReward.instance.ShowRewardForMission(missionUID2);
							Player.instance.CollectMission(missionUID2);
							HandleOnExitClick();
							break;
						}
					}
				}
			}
		}
	}

	private bool CheckAddsMission()
	{
		if (_conversation.missionUID > -1)
		{
			MissionData missionData = Player.instance.GetMissionData(_conversation.missionUID);
			MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(_conversation.missionUID);
			if (missionData == null && missionCompletedData == null)
			{
				HandleOnExitClickWithMission();
				return true;
			}
		}
		return false;
	}

	private void ShiftActiveBubbles(float shiftAmount)
	{
		int num = activeBubbles.Count - 1;
		for (int num2 = num; num2 >= 0; num2--)
		{
			if (num2 == maxBubbles - 1)
			{
				StartCoroutine(RemoveBubble(activeBubbles[num2]));
				activeBubbles.RemoveAt(num2);
			}
			else
			{
				Vector3 localPosition = activeBubbles[num2].transform.localPosition;
				localPosition.y += shiftAmount;
				if (num2 == 0)
				{
					StartCoroutine(activeBubbles[num2].StartMoveUp());
					localPosition.x += ((!(localPosition.x < 0f)) ? 2f : (-2f));
				}
				activeBubbles[num2].transform.DOKill();
				activeBubbles[num2].transform.DOLocalMove(localPosition, 0.4f).SetEase(Ease.InOutBack);
				activeBubbles[num2].transform.DOScale(new Vector3(0.92f, 0.92f, 1f), 0.4f).SetEase(Ease.InOutBack);
			}
		}
	}

	private IEnumerator SwitchCharacters(DialogueCharacter dialogueCharacter, ConversationItem newCharacter, CharacterName previousCharacter)
	{
		animator.enabled = false;
		bool sameCharacter = ((previousCharacter == newCharacter.character) ? true : false);
		float directionX = ((!(dialogueCharacter == leftCharacter)) ? 1 : (-1));
		if (!sameCharacter)
		{
			dialogueCharacter.transform.DOLocalMove(new Vector3(directionX * 400f, dialogueCharacter.transform.localPosition.y, dialogueCharacter.transform.localPosition.z), 0.3f).SetEase(Ease.OutSine);
			yield return new WaitForSeconds(0.3f);
		}
		Debug.Log($"Face Sprite Name: {newCharacter.faceSpriteName}");
		if (newCharacter.character == CharacterName.None)
		{
			dialogueCharacter.bodySprite.gameObject.SetActive(value: false);
			dialogueCharacter.faceSprite.gameObject.SetActive(value: false);
		}
		else
		{
			dialogueCharacter.bodySprite.gameObject.SetActive(value: true);
			dialogueCharacter.faceSprite.gameObject.SetActive(value: true);
			dialogueCharacter.bodySprite.SetSprite(GetSpriteCollectionDataForCharacter(dialogueCharacter.character), newCharacter.bodySpriteName);
			dialogueCharacter.faceSprite.gameObject.SetActive(!string.IsNullOrEmpty(newCharacter.faceSpriteName) && dialogueCharacter.faceSprite.SetSprite(GetSpriteCollectionDataForCharacter(dialogueCharacter.character), newCharacter.faceSpriteName));
		}
		Vector3 characterScale = new Vector3(Mathf.Sign(dialogueCharacter.bodySprite.scale.x) * 1f, 1f, 1f);
		tk2dSprite bodySprite = dialogueCharacter.bodySprite;
		Vector3 scale = characterScale;
		dialogueCharacter.faceSprite.scale = scale;
		bodySprite.scale = scale;
		if (!sameCharacter)
		{
			dialogueCharacter.transform.DOLocalMove(new Vector3(directionX * 138f, dialogueCharacter.transform.localPosition.y, dialogueCharacter.transform.localPosition.z), 0.3f).SetEase(Ease.OutSine);
			yield return new WaitForSeconds(0.3f);
		}
		yield return new WaitForEndOfFrame();
		animator.enabled = true;
	}

	private IEnumerator RemoveBubble(DialogueBubble bubble)
	{
		if (bubble != null)
		{
			ReturnDialogueBubble(bubble);
		}
		yield break;
	}

	private void ReturnDialogueBubble(DialogueBubble bubble)
	{
		if (bubble.gameObject != null)
		{
			bubble.gameObject.SetActive(value: false);
			UnityEngine.Object.Destroy(bubble.gameObject);
		}
	}

	private DialogueBubble GetDialogueBubble()
	{
		DialogueBubble dialogueBubble;
		if (_bubblePool.Count > 0)
		{
			dialogueBubble = _bubblePool[_bubblePool.Count - 1];
			_bubblePool.RemoveAt(_bubblePool.Count - 1);
		}
		else
		{
			bubblePrefab.SetActive(value: true);
			dialogueBubble = UnityEngine.Object.Instantiate(bubblePrefab).GetComponent<DialogueBubble>();
			dialogueBubble.transform.parent = holder;
			bubblePrefab.SetActive(value: false);
		}
		return dialogueBubble;
	}
}
