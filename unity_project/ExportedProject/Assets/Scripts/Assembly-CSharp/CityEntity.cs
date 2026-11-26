using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class CityEntity : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CityEntity> StateChanged__BackingField;

	[SerializeField]
	private string _guid;

	private bool isInitialized;

	protected Communicator communicator;

	protected CameraPinchZoomer cameraPinchZoomer;

	protected Player player;

	private int stateChangeCount;

	public string guid
	{
		get
		{
			return _guid;
		}
	}

	public abstract CityEntityState[] states { get; }

	public CityEntityState activeState { get; private set; }

	public int activeIndex { get; private set; }

	protected bool isFirstState { get; private set; }

	public event Action<CityEntity> StateChanged
	{
		add
		{
			Action<CityEntity> action = StateChanged__BackingField;
			Action<CityEntity> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StateChanged__BackingField, (Action<CityEntity>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CityEntity> action = StateChanged__BackingField;
			Action<CityEntity> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref StateChanged__BackingField, (Action<CityEntity>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
		Init();
	}

	protected virtual void OnEnable()
	{
		InitListeners();
	}

	protected virtual void OnDisable()
	{
		if (isInitialized)
		{
			if (communicator != null)
			{
				communicator.StartDayTime -= ReevaluateActiveState;
				communicator.StartNightTime -= ReevaluateActiveState;
				communicator.NewsFlashStarted -= Communicator_NewsFlashStarted;
			}
			if (CoreGameTutorial.instance != null)
			{
				CoreGameTutorial.instance.StepComplete -= CoreGameTutorial_instance_StepComplete;
			}
			CityCharacter.CharacterTypeChanged -= CityCharacter_CharacterTypeChanged;
			if (player != null)
			{
				player.TriggerSet -= Player_TriggerSet;
				player.TriggerUnset -= Player_TriggerSet;
				player.MissionAdded -= Player_MissionAdded;
				player.MissionCompleted -= Player_MissionCompleted;
				player.MissionUpdated -= Player_MissionUpdated;
				player.InventoryChanged -= ReevaluateActiveState;
				player.CurrencyUpdated -= Player_CurrencyUpdated;
				player.RainbowCurrencyUpdated -= Player_CurrencyUpdated;
				player.DefeatedNPC -= Player_DefeatedNPC;
				player.CharacterCollectionChanged -= ReevaluateActiveState;
				player.PartyChanged -= ReevaluateActiveState;
				player.ConversationPlayed -= Player_ConversationPlayed;
			}
		}
	}

	private void Init()
	{
		if (!isInitialized)
		{
			communicator = Communicator.instance;
			cameraPinchZoomer = CityController.instance.pinchZoomer;
			player = Player.instance;
			isInitialized = true;
			isFirstState = true;
			InitListeners();
		}
	}

	private void InitListeners()
	{
		if (isInitialized)
		{
			if (communicator != null)
			{
				communicator.StartDayTime += ReevaluateActiveState;
				communicator.StartNightTime += ReevaluateActiveState;
				communicator.NewsFlashStarted += Communicator_NewsFlashStarted;
			}
			if (CoreGameTutorial.instance != null)
			{
				CoreGameTutorial.instance.StepComplete += CoreGameTutorial_instance_StepComplete;
			}
			CityCharacter.CharacterTypeChanged += CityCharacter_CharacterTypeChanged;
			player.TriggerSet += Player_TriggerSet;
			player.TriggerUnset += Player_TriggerSet;
			player.MissionAdded += Player_MissionAdded;
			player.MissionCompleted += Player_MissionCompleted;
			player.MissionUpdated += Player_MissionUpdated;
			player.InventoryChanged += ReevaluateActiveState;
			player.CurrencyUpdated += Player_CurrencyUpdated;
			player.RainbowCurrencyUpdated += Player_CurrencyUpdated;
			player.DefeatedNPC += Player_DefeatedNPC;
			player.CharacterCollectionChanged += ReevaluateActiveState;
			player.PartyChanged += ReevaluateActiveState;
			player.ConversationPlayed += Player_ConversationPlayed;
			ReevaluateActiveState();
		}
	}

	private void SetActiveState(int index)
	{
		if (index == -1)
		{
			activeIndex = index;
			activeState = null;
			InitNullState();
		}
		else if ((index != activeIndex || activeState == null) && index >= 0 && index < states.Length)
		{
			activeIndex = index;
			activeState = states[activeIndex];
			stateChangeCount++;
			if (isFirstState && stateChangeCount > 1)
			{
				isFirstState = false;
			}
			InitState(activeState);
		}
		OnStateChanged(this);
	}

	protected abstract void InitState(CityEntityState state);

	protected abstract void InitNullState();

	protected void ReevaluateActiveState()
	{
		if (states.Length <= 0)
		{
			return;
		}
		CityEntityState cityEntityState = null;
		int num = states.Length - 1;
		int num2;
		for (num2 = num; num2 >= 0; num2--)
		{
			cityEntityState = states[num2];
			if (cityEntityState.isActive)
			{
				if (IsStateValid(cityEntityState))
				{
					break;
				}
				if (num2 == 0)
				{
					num2 = -1;
					break;
				}
			}
		}
		SetActiveState(num2);
	}

	protected bool IsStateValid(CityEntityState state)
	{
		if (state.useTimeOfDay && communicator != null)
		{
			if (state.timeOfDay == TimeOfDay.Day && !communicator.isDayTime)
			{
				return false;
			}
			if (state.timeOfDay == TimeOfDay.Night && communicator.isDayTime)
			{
				return false;
			}
		}
		if (state.useTrigger && !player.HasTriggered(state.triggerName))
		{
			return false;
		}
		if (state.useTutorialStep && state.tutorialStepConditions.Length > 0)
		{
			CityEntityState.Condition_TutorialStep condition_TutorialStep = null;
			int num = state.tutorialStepConditions.Length;
			for (int i = 0; i < num; i++)
			{
				condition_TutorialStep = state.tutorialStepConditions[i];
				if (condition_TutorialStep.isOrNot == IsOrNot.Is)
				{
					if (!CoreGameTutorial.IsTutorialStepComplete(condition_TutorialStep.step))
					{
						return false;
					}
				}
				else if (CoreGameTutorial.IsTutorialStepComplete(condition_TutorialStep.step))
				{
					return false;
				}
			}
		}
		if (state.useMissionConditions && state.missionConditions.Length > 0)
		{
			CityEntityState.Condition_Mission condition_Mission = null;
			int num2 = state.missionConditions.Length;
			for (int j = 0; j < num2; j++)
			{
				condition_Mission = state.missionConditions[j];
				if (condition_Mission.isOrNot == IsOrNot.Is)
				{
					if (condition_Mission.state == CityEntityState.MissionState.Unassigned && (player.HasMissionCompleted(condition_Mission.missionUID) || player.HasActiveMission(condition_Mission.missionUID)))
					{
						return false;
					}
					if (condition_Mission.state == CityEntityState.MissionState.Active && !player.HasActiveMission(condition_Mission.missionUID))
					{
						return false;
					}
					if (condition_Mission.state == CityEntityState.MissionState.Complete && !player.HasMissionCompleted(condition_Mission.missionUID))
					{
						return false;
					}
				}
				else
				{
					if (condition_Mission.state == CityEntityState.MissionState.Unassigned && !player.HasMissionCompleted(condition_Mission.missionUID) && !player.HasActiveMission(condition_Mission.missionUID))
					{
						return false;
					}
					if (condition_Mission.state == CityEntityState.MissionState.Active && player.HasActiveMission(condition_Mission.missionUID))
					{
						return false;
					}
					if (condition_Mission.state == CityEntityState.MissionState.Complete && player.HasMissionCompleted(condition_Mission.missionUID))
					{
						return false;
					}
				}
				if (condition_Mission.state == CityEntityState.MissionState.Complete && condition_Mission.isOrNot == IsOrNot.Is && communicator.GetCurrentDay() < player.GetMissionCompletedData(condition_Mission.missionUID).dayCompleted + condition_Mission.daysAfter)
				{
					return false;
				}
			}
		}
		if (state.useGUIDCollectedConditions && state.guidCollectedConditions.Length > 0)
		{
			int num3 = state.guidCollectedConditions.Length;
			for (int k = 0; k < num3; k++)
			{
				CityEntityState.Condition_GUIDCollected condition_GUIDCollected = state.guidCollectedConditions[k];
				MissionData missionData = player.GetMissionData(condition_GUIDCollected.missionUID);
				if (missionData == null)
				{
					return false;
				}
				if (condition_GUIDCollected.isOrNot == IsOrNot.Is)
				{
					if (!missionData.collectedGUIDs.Contains(guid))
					{
						return false;
					}
				}
				else if (missionData.collectedGUIDs.Contains(guid))
				{
					return false;
				}
			}
		}
		if (state.useItemCondition)
		{
			CityEntityState.Condition_Item itemCondition = state.itemCondition;
			int itemCount = player.GetItemCount(itemCondition.itemUID);
			if (itemCondition.comparison == Comparison.Exactly && itemCount != itemCondition.amount)
			{
				return false;
			}
			if (itemCondition.comparison == Comparison.MoreThan && itemCount <= itemCondition.amount)
			{
				return false;
			}
			if (itemCondition.comparison == Comparison.LessThan && itemCount >= itemCondition.amount)
			{
				return false;
			}
			if (itemCondition.comparison == Comparison.AtLeast && itemCount < itemCondition.amount)
			{
				return false;
			}
			if (itemCondition.comparison == Comparison.AtMost && itemCount > itemCondition.amount)
			{
				return false;
			}
		}
		if (state.useCurrencyCondition)
		{
			CityEntityState.Condition_Currency currencyCondition = state.currencyCondition;
			int num4 = ((currencyCondition.type != CurrencyType.Coins) ? player.rainbowCurrency : player.currency);
			if (currencyCondition.comparison == Comparison.Exactly && num4 != currencyCondition.amount)
			{
				return false;
			}
			if (currencyCondition.comparison == Comparison.MoreThan && num4 <= currencyCondition.amount)
			{
				return false;
			}
			if (currencyCondition.comparison == Comparison.LessThan && num4 >= currencyCondition.amount)
			{
				return false;
			}
			if (currencyCondition.comparison == Comparison.AtLeast && num4 < currencyCondition.amount)
			{
				return false;
			}
			if (currencyCondition.comparison == Comparison.AtMost && num4 > currencyCondition.amount)
			{
				return false;
			}
		}
		if (state.usePlayerCharacterType && state.playerCharacterType != 0 && state.playerCharacterType != (CityCharacterType)(-1))
		{
			if (CityCharacter.characterType == (CityCharacterType)0)
			{
				return false;
			}
			if ((state.playerCharacterType & CityCharacterType.Robin) == 0 && CityCharacter.characterType == CityCharacterType.Robin)
			{
				return false;
			}
			if ((state.playerCharacterType & CityCharacterType.BeastBoy) == 0 && CityCharacter.characterType == CityCharacterType.BeastBoy)
			{
				return false;
			}
			if ((state.playerCharacterType & CityCharacterType.Raven) == 0 && CityCharacter.characterType == CityCharacterType.Raven)
			{
				return false;
			}
			if ((state.playerCharacterType & CityCharacterType.Starfire) == 0 && CityCharacter.characterType == CityCharacterType.Starfire)
			{
				return false;
			}
			if ((state.playerCharacterType & CityCharacterType.Cyborg) == 0 && CityCharacter.characterType == CityCharacterType.Cyborg)
			{
				return false;
			}
		}
		if (state.useHasDefeatedNPC)
		{
			CityEntityState.Condition_NPCDefeated condition_NPCDefeated = null;
			int num5 = state.npcDefeatedConditions.Length;
			for (int l = 0; l < num5; l++)
			{
				condition_NPCDefeated = state.npcDefeatedConditions[l];
				if (condition_NPCDefeated.isOrNot == IsOrNot.Is)
				{
					if (!player.HasDefeatedNPC(condition_NPCDefeated.npcGuid))
					{
						return false;
					}
				}
				else if (player.HasDefeatedNPC(condition_NPCDefeated.npcGuid))
				{
					return false;
				}
			}
		}
		if (state.useFigureConditions && state.figureConditions.Length > 0)
		{
			CityEntityState.Condition_Figure condition_Figure = null;
			int num6 = state.figureConditions.Length;
			for (int m = 0; m < num6; m++)
			{
				condition_Figure = state.figureConditions[m];
				List<Character> list = ((!condition_Figure.inParty) ? player.characters : player.activeParty);
				int count = list.Count;
				int num7 = 0;
				if (condition_Figure.type == CityEntityState.FigureConditionType.Character)
				{
					for (int n = 0; n < count; n++)
					{
						if (list[n].characterDefinition.uid == condition_Figure.figureUID)
						{
							num7++;
						}
					}
				}
				else if (condition_Figure.type == CityEntityState.FigureConditionType.Tag)
				{
					for (int num8 = 0; num8 < count; num8++)
					{
						if (list[num8].characterDefinition.tags.Contains(condition_Figure.stringValue1))
						{
							num7++;
						}
					}
				}
				else if (condition_Figure.type == CityEntityState.FigureConditionType.MinLevel)
				{
					for (int num9 = 0; num9 < count; num9++)
					{
						if (list[num9].level >= condition_Figure.intValue1)
						{
							num7++;
						}
					}
				}
				else
				{
					for (int num10 = 0; num10 < count; num10++)
					{
						if (list[num10].shinyRank >= condition_Figure.intValue1)
						{
							num7++;
						}
					}
				}
				if (condition_Figure.comparison == Comparison.Exactly && num7 != condition_Figure.count)
				{
					return false;
				}
				if (condition_Figure.comparison == Comparison.MoreThan && num7 <= condition_Figure.count)
				{
					return false;
				}
				if (condition_Figure.comparison == Comparison.LessThan && num7 >= condition_Figure.count)
				{
					return false;
				}
				if (condition_Figure.comparison == Comparison.AtLeast && num7 < condition_Figure.count)
				{
					return false;
				}
				if (condition_Figure.comparison == Comparison.AtMost && num7 > condition_Figure.count)
				{
					return false;
				}
			}
		}
		if (state.useConversationPlayed && state.conversationPlayed != null && SaveUtility.GetInt(state.conversationPlayed.name, 0) == 0)
		{
			return false;
		}
		if (state.useTournamentCondition && !string.IsNullOrEmpty(state.tournamentCondition.tournamentName))
		{
			if (state.tournamentCondition.hasOrNot == HasOrNot.Has)
			{
				if (!player.HasCompletedTournament(state.tournamentCondition.tournamentName))
				{
					return false;
				}
			}
			else if (player.HasCompletedTournament(state.tournamentCondition.tournamentName))
			{
				return false;
			}
		}
		if (state.useNewsFlash && state.newsFlash.uid >= 0)
		{
			if (state.newsFlash.isOrNot == IsOrNot.Is)
			{
				if ((state.newsFlash.state == CityEntityState.MissionState.Active || state.newsFlash.state == CityEntityState.MissionState.Complete) && !communicator.IsActiveNewsFlash(state.newsFlash.uid) && !Player.instance.HasCompletedNewsFlash(state.newsFlash.uid))
				{
					return false;
				}
				if (state.newsFlash.state == CityEntityState.MissionState.Unassigned && (communicator.IsActiveNewsFlash(state.newsFlash.uid) || player.HasCompletedNewsFlash(state.newsFlash.uid)))
				{
					return false;
				}
			}
			else
			{
				if (((state.newsFlash.state == CityEntityState.MissionState.Active || state.newsFlash.state == CityEntityState.MissionState.Complete) && communicator.IsActiveNewsFlash(state.newsFlash.uid)) || Player.instance.HasCompletedNewsFlash(state.newsFlash.uid))
				{
					return false;
				}
				if (state.newsFlash.state == CityEntityState.MissionState.Unassigned && !communicator.IsActiveNewsFlash(state.newsFlash.uid) && !player.HasCompletedNewsFlash(state.newsFlash.uid))
				{
					return false;
				}
			}
		}
		return true;
	}

	protected virtual void OnStateChanged(CityEntity entity)
	{
		if (StateChanged__BackingField != null)
		{
			StateChanged__BackingField(entity);
		}
	}

	private void Communicator_NewsFlashStarted(NewsFlash obj)
	{
		ReevaluateActiveState();
	}

	private void CoreGameTutorial_instance_StepComplete(CoreGameTutorial.Step obj)
	{
		ReevaluateActiveState();
	}

	private void CityCharacter_CharacterTypeChanged(CityCharacterType obj)
	{
		ReevaluateActiveState();
	}

	private void Player_CurrencyUpdated(int obj)
	{
		ReevaluateActiveState();
	}

	protected virtual void Player_MissionAdded(int arg1, bool arg2)
	{
		ReevaluateActiveState();
	}

	protected virtual void Player_MissionCompleted(int obj)
	{
		ReevaluateActiveState();
	}

	protected virtual void Player_MissionUpdated(int obj)
	{
		ReevaluateActiveState();
	}

	private void Player_DefeatedNPC(string obj)
	{
		ReevaluateActiveState();
	}

	private void Player_ConversationPlayed(Conversation conversation)
	{
		ReevaluateActiveState();
	}

	private void Player_TriggerSet(string obj)
	{
		ReevaluateActiveState();
	}
}
