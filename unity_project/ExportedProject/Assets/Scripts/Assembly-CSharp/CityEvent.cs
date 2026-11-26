using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(tk2dUIItem))]
[RequireComponent(typeof(BoxCollider))]
public class CityEvent : MonoBehaviour, IInteractiveBuilding
{
	public string guid = string.Empty;

	public int instanceID;

	public List<CityEventData> eventData = new List<CityEventData>();

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<bool, CityEventData> EventEnabled__BackingField;

	private tk2dUIItem uiItem;

	private CityEventData activeEventData;

	private Conversation conversation;

	private PathNode closestNode;

	private CityShopSign shopSign;

	public TournamentDefinition tournamentDef;

	private bool tournamentClosed;

	private bool isSetup;

	private Animator _animator;

	private AnimatorDisabler _animatorDisabler;

	private bool _shouldEnable;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<bool> OpenStatusUpdated__BackingField;

	public Animator animator
	{
		get
		{
			if (_animator == null)
			{
				_animator = base.transform.GetComponent<Animator>();
			}
			return _animator;
		}
		set
		{
			if (value != null)
			{
				_animator = value;
			}
		}
	}

	private AnimatorDisabler animatorDisabler
	{
		get
		{
			if (_animatorDisabler == null)
			{
				_animatorDisabler = animator.gameObject.GetComponent<AnimatorDisabler>();
			}
			return _animatorDisabler;
		}
	}

	private bool shouldEnable
	{
		get
		{
			return _shouldEnable;
		}
		set
		{
			_shouldEnable = value;
		}
	}

	public Action interactionFunction
	{
		get
		{
			return HandleOnClick;
		}
	}

	public event Action<bool, CityEventData> EventEnabled
	{
		add
		{
			Action<bool, CityEventData> action = EventEnabled__BackingField;
			Action<bool, CityEventData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref EventEnabled__BackingField, (Action<bool, CityEventData>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool, CityEventData> action = EventEnabled__BackingField;
			Action<bool, CityEventData> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref EventEnabled__BackingField, (Action<bool, CityEventData>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<bool> OpenStatusUpdated
	{
		add
		{
			Action<bool> action = OpenStatusUpdated__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OpenStatusUpdated__BackingField, (Action<bool>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<bool> action = OpenStatusUpdated__BackingField;
			Action<bool> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref OpenStatusUpdated__BackingField, (Action<bool>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		uiItem = GetComponent<tk2dUIItem>();
	}

	private void OnEnable()
	{
		Player.instance.MissionAdded += HandleMissionAdded;
		Player.instance.MissionCompleted += HandleMissionCompleted;
		StartCoroutine("DelayStart");
	}

	private void OnDisable()
	{
		Player.instance.MissionAdded -= HandleMissionAdded;
		Player.instance.MissionCompleted -= HandleMissionCompleted;
		if (isSetup)
		{
			isSetup = false;
			if (CityController.instance != null)
			{
				CityController.instance.communicator.StartDayTime -= CheckShouldEnable;
				CityController.instance.communicator.StartNightTime -= CheckShouldEnable;
			}
		}
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		uiItem.OnClick += HandleOnClick;
		SetupCityEvent();
	}

	private IEnumerator DelayStart()
	{
		yield return new WaitForEndOfFrame();
		SetupCityEvent();
	}

	private void PlayShopAnimation()
	{
		if (animator != null)
		{
			if (animatorDisabler != null)
			{
				animatorDisabler.enabled = true;
			}
			animator.Play("ShopBuilding_AnimLoop", 0, 0f);
			animator.enabled = true;
		}
	}

	private void PlayShopTappedAnimation()
	{
		if (animator != null)
		{
			animator.Play("ShopBuilding_AnimTap");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_wes_boing", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			UnityEngine.Debug.Log("animator null");
		}
	}

	private void StopShopAnimation()
	{
		if (animator != null)
		{
			animator.Play("ShopBuilding_AnimLoop", 0, 0f);
			animator.enabled = false;
			if (animatorDisabler != null)
			{
				animatorDisabler.enabled = false;
			}
			animator.transform.localScale = Vector3.one;
		}
	}

	private void SetupCityEvent()
	{
		if (!isSetup)
		{
			if (GetComponent<Landmark>() != null && GetComponent<Landmark>().icon == null)
			{
				GetComponent<Landmark>().ResetLandmarkIcon();
			}
			shopSign = GetComponentInChildren<CityShopSign>(true);
			if (shopSign != null)
			{
				shopSign.gameObject.SetActive(false);
			}
			isSetup = true;
			uiItem.enabled = false;
			if (CityController.instance != null)
			{
				CityController.instance.communicator.StartDayTime += CheckShouldEnable;
				CityController.instance.communicator.StartNightTime += CheckShouldEnable;
			}
			CheckShouldEnable();
		}
	}

	private void HandleMissionAdded(int missionUID, bool dialogueMission)
	{
		CheckShouldEnable();
	}

	private void HandleMissionCompleted(int missionUID)
	{
		CheckShouldEnable();
	}

	public void ForceCheckShouldEnable()
	{
		CheckShouldEnable();
	}

	private void CheckShouldEnable()
	{
		shouldEnable = false;
		if (activeEventData != null && activeEventData.disablesGameObject && activeEventData.disabledGameObject != null)
		{
			activeEventData.disabledGameObject.SetActive(true);
		}
		activeEventData = null;
		bool flag = false;
		for (int i = 0; i < eventData.Count; i++)
		{
			CityEventData cityEventData = eventData[i];
			if (cityEventData.activatesTournament != null)
			{
				flag = true;
				tournamentDef = cityEventData.activatesTournament;
			}
			if (activeEventData != null)
			{
				break;
			}
			if (cityEventData.activeOnCompletedMissionUID > -1)
			{
				MissionCompletedData missionCompletedData = Player.instance.GetMissionCompletedData(cityEventData.activeOnCompletedMissionUID);
				if (missionCompletedData != null)
				{
					shouldEnable = true;
				}
			}
			if (cityEventData.hidesOnCompletedMissionUID > -1 && Player.instance.HasMissionCompleted(cityEventData.hidesOnCompletedMissionUID))
			{
				shouldEnable = false;
				if (cityEventData.hidesChildrenWhenInvalid)
				{
					foreach (Transform item in base.transform)
					{
						item.gameObject.SetActive(false);
					}
				}
			}
			if (cityEventData.activeOnCompletedMissionUID == -1 && cityEventData.hidesOnCompletedMissionUID == -1)
			{
				shouldEnable = true;
			}
			if (shouldEnable)
			{
				activeEventData = cityEventData;
			}
			if (!shouldEnable || !(cityEventData.initialConversation != null))
			{
				continue;
			}
			if (cityEventData.initialConversation.missionUID < 0)
			{
				conversation = cityEventData.initialConversation;
				activeEventData = cityEventData;
				continue;
			}
			MissionData missionData = Player.instance.GetMissionData(cityEventData.initialConversation.missionUID);
			if (missionData == null)
			{
				conversation = cityEventData.initialConversation;
				activeEventData = cityEventData;
			}
			else if (missionData.active && !missionData.completed)
			{
				conversation = cityEventData.activeConversation;
				activeEventData = cityEventData;
			}
		}
		if (activeEventData != null)
		{
			EnableEvent();
			if (activeEventData != null && activeEventData.disablesGameObject && activeEventData.disabledGameObject != null)
			{
				activeEventData.disabledGameObject.SetActive(false);
			}
		}
		else
		{
			DisableEvent();
			HideTournamentLandmark();
			if (activeEventData != null && activeEventData.disablesGameObject && activeEventData.disabledGameObject != null)
			{
				activeEventData.disabledGameObject.SetActive(true);
			}
		}
		if (flag)
		{
			bool flag2 = false;
			if (CityController.instance != null && !CityController.instance.tournamentBuildings.Contains(base.gameObject))
			{
				CityController.instance.tournamentBuildings.Add(base.gameObject);
			}
			if (tournamentDef != null)
			{
				CityLocation district = tournamentDef.district;
				int num = -1;
				if (num > -1)
				{
					MissionCompletedData missionCompletedData2 = Player.instance.GetMissionCompletedData(num);
					if (missionCompletedData2 != null)
					{
						flag2 = true;
					}
				}
				bool flag3 = false;
				bool flag4 = false;
				if (tournamentDef.timeOfDay == TimeOfDay.Day && CityController.instance != null && !CityController.instance.communicator.isDayTime)
				{
					flag3 = true;
				}
				if (tournamentDef.timeOfDay == TimeOfDay.Night && CityController.instance != null && CityController.instance.communicator.isDayTime)
				{
					flag4 = true;
				}
				if (flag3 || flag4)
				{
					tournamentClosed = true;
					if (shopSign != null)
					{
						if (tournamentDef != null)
						{
							StopShopAnimation();
							shopSign.gameObject.SetActive(true);
							if (flag3)
							{
								shopSign.SetState(CityShopSign.State.ClosedUntilDay);
							}
							else if (flag4)
							{
								shopSign.SetState(CityShopSign.State.ClosedUntilNight);
							}
						}
						else
						{
							PlayShopAnimation();
							shopSign.gameObject.SetActive(false);
						}
					}
				}
				else
				{
					tournamentClosed = false;
					if (shopSign != null)
					{
						PlayShopAnimation();
						shopSign.gameObject.SetActive(false);
					}
				}
			}
			else
			{
				StopShopAnimation();
			}
			if (flag2)
			{
				ShowTournamentLandmark();
			}
			else
			{
				HideTournamentLandmark();
			}
		}
		else
		{
			HideTournamentLandmark();
		}
		CheckBuildingPad();
		if (activeEventData != null && activeEventData.activatesTournament == null)
		{
			if (shopSign != null)
			{
				shopSign.gameObject.SetActive(false);
			}
			PlayShopAnimation();
		}
		if (EventEnabled__BackingField != null)
		{
			EventEnabled__BackingField(shouldEnable, activeEventData);
		}
	}

	public void CheckBuildingPad()
	{
		if (shopSign != null)
		{
			bool obj = !shopSign.gameObject.activeSelf || (shopSign.gameObject.activeSelf && !shopSign.closedComingSoonText.gameObject.activeSelf);
			if (OpenStatusUpdated__BackingField != null)
			{
				OpenStatusUpdated__BackingField(obj);
			}
		}
		else if (OpenStatusUpdated__BackingField != null)
		{
			OpenStatusUpdated__BackingField(shouldEnable);
		}
	}

	private void ShowTournamentLandmark()
	{
		if (GetComponent<Landmark>() == null)
		{
			StartCoroutine("SetupLandmarkIcon");
		}
	}

	private IEnumerator SetupLandmarkIcon()
	{
		if (GetComponent<Landmark>() == null)
		{
			Landmark landmark = base.gameObject.AddComponent<Landmark>();
			landmark.spriteName = "gui_citynotifiers_tournament";
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			landmark.ResetSpriteIcon();
			landmark.zoomedInSettings.showTail = true;
			landmark.zoomedInSettings.hideWhenInView = true;
			landmark.zoomedInSettings.hideWhenInView = true;
			landmark.zoomedOutSettings.snapToEdge = false;
			landmark.zoomedOutSettings.hideAtMinScale = false;
			landmark.zoomedOutSettings.minScale = 0.6f;
			landmark.zoomedOutSettings.maxScale = 0.6f;
			landmark.UpdateZoomSettings();
		}
		yield return new WaitForEndOfFrame();
	}

	private void HideTournamentLandmark()
	{
		if (GetComponent<Landmark>() != null)
		{
			UnityEngine.Object.Destroy(GetComponent<Landmark>());
		}
	}

	private void EnableEvent()
	{
		uiItem.enabled = true;
	}

	private void DisableEvent()
	{
		uiItem.enabled = false;
		if (activeEventData != null && activeEventData.disablesGameObject && activeEventData.disabledGameObject != null)
		{
			activeEventData.disabledGameObject.SetActive(true);
		}
	}

	private void HandleOnClick()
	{
		UnityEngine.Debug.Log("Handle on click");
		if (activeEventData != null && (!(CoreGameTutorial.instance != null) || CoreGameTutorial.instance.currentStep != CoreGameTutorial.Step.CityTutorialViewMapStart) && CityController.instance.cityCharacter.AllowInput && (!(FlashSaleController.instance != null) || !FlashSaleController.instance.FlashSaleActive) && !CityController.instance.pinchZoomer.isZoomedOut && !CityReward.instance.showingRewards)
		{
			if (closestNode == null)
			{
				closestNode = CityController.instance.GetNearestPathNodeToPosition(base.transform.position);
			}
			PlayShopTappedAnimation();
			if (CityController.instance.cityCharacter.CurrentNode == closestNode)
			{
				InteractWithEvent();
				return;
			}
			CityController.instance.cityCharacter.MoveToInteractiveNode(closestNode, InteractWithEvent);
			CityController.instance.pinchZoomer.ZoomIn();
		}
	}

	private void InteractWithEvent()
	{
		CityController.instance.cityCharacter.StopMove();
		if (activeEventData.completesMissionUID > -1)
		{
			MissionData missionData = Player.instance.GetMissionData(activeEventData.completesMissionUID);
			if (missionData != null)
			{
				int uid = missionData.uid;
				Player.instance.CollectMission(uid);
				Player.instance.CompleteMission(uid);
				Player.instance.Save();
			}
			if (activeEventData.hidesChildrenWhenInvalid)
			{
				foreach (Transform item in base.transform)
				{
					item.gameObject.SetActive(false);
				}
			}
		}
		if (activeEventData != null && activeEventData.activatesTournament != null && !tournamentClosed)
		{
			if (Player.instance.activeParty.Count < 6)
			{
				CityController.instance.HandleShowDialogue((Conversation)Resources.Load("Data/TT2/" + CityCharacter.GetCityCharacterConvoName("BB-CONVO_TOURNYREQ_SIXFIGS")));
			}
			else if (!CityController.instance.exitingCityScene)
			{
				SaveUtility.SetString("PlayerPosition", CityController.instance.cityCharacter.gameObject.transform.position.ToString(), false);
				CityController.instance.communicator.SaveDayAndTime();
				Player.instance.IncreaseCurrentTime(20);
				CityController.instance.ExitCityScene();
				TournamentController.initDefinition = activeEventData.activatesTournament;
				LoadingScreenCover.LoadScene("Tournament");
			}
		}
		else if (activeEventData != null && !string.IsNullOrEmpty(activeEventData.activatesScene))
		{
			if (!CityController.instance.exitingCityScene)
			{
				SaveUtility.SetString("PlayerPosition", CityController.instance.cityCharacter.gameObject.transform.position.ToString(), false);
				SaveUtility.SetString("PlayerScene", SceneManagerHelper.ActiveSceneName, false);
				CityController.instance.communicator.SaveDayAndTime();
				Player.instance.IncreaseCurrentTime(20);
				CityController.instance.ExitCityScene();
				LoadingScreenCover.LoadScene(activeEventData.activatesScene);
			}
		}
		else if (activeEventData != null && activeEventData.initialConversation != null)
		{
			CityController.instance.HandleShowDialogue(activeEventData.initialConversation);
		}
	}
}
