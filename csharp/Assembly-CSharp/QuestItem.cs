using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(tk2dUIItem))]
[RequireComponent(typeof(BoxCollider))]
public class QuestItem : MonoBehaviour
{
	private tk2dUIItem uiItem;

	private BoxCollider collider;

	public string guid = string.Empty;

	public int activatedByMissionUID = -1;

	public int activatedBySecondMissionUID = -1;

	public int activatedByCompletedMissionUID = -1;

	public bool alwaysDisplay;

	public bool activatedByMultipleMissions;

	public QuestItemType type;

	public GameObject groupObject;

	public GameObject glintObject;

	public Animator animator;

	public List<string> tags = new List<string>();

	private void Awake()
	{
		uiItem = GetComponent<tk2dUIItem>();
		collider = GetComponent<BoxCollider>();
	}

	private void OnEnable()
	{
		uiItem.OnClick += HandleOnClick;
	}

	private void OnDisable()
	{
		uiItem.OnClick -= HandleOnClick;
	}

	private IEnumerator Start()
	{
		if (Application.isPlaying)
		{
			groupObject = transform.GetChild(0).gameObject;
			DisableQuestItem(forceOff: true);
			yield return new WaitForEndOfFrame();
			Player.instance.MissionAdded += HandleMissionAdded;
			Player.instance.MissionCompleted += HandleMissionCompleted;
			Player.instance.MissionUpdated += HandleMissionUpdated;
			yield return new WaitForEndOfFrame();
			CheckShouldEnable();
		}
		yield return new WaitForEndOfFrame();
	}

	private void OnDestroy()
	{
		if (Player.instance != null)
		{
			Player.instance.MissionAdded -= HandleMissionAdded;
			Player.instance.MissionCompleted -= HandleMissionCompleted;
			Player.instance.MissionUpdated -= HandleMissionUpdated;
		}
	}

	private void DisableQuestItem(bool forceOff = false)
	{
		if (collider != null)
		{
			collider.enabled = false;
		}
		if (!(groupObject != null))
		{
			return;
		}
		if (!forceOff && alwaysDisplay)
		{
			if (glintObject != null)
			{
				glintObject.SetActive(value: false);
			}
			groupObject.SetActive(value: true);
		}
		else
		{
			groupObject.SetActive(value: false);
		}
	}

	private void EnableQuestItem()
	{
		if (collider != null)
		{
			collider.enabled = true;
		}
		if (groupObject != null)
		{
			groupObject.SetActive(value: true);
		}
		if (glintObject != null)
		{
			glintObject.SetActive(value: true);
		}
	}

	private void HandleMissionUpdated(int missionUID)
	{
		CheckShouldEnable();
	}

	private void HandleMissionAdded(int missionUID, bool dialogueMission)
	{
		CheckShouldEnable();
	}

	private void HandleMissionCompleted(int missionUID)
	{
		CheckShouldEnable();
	}

	private void CheckShouldEnable()
	{
		if (Player.instance.HasCollectedGlintGUID(guid))
		{
			DisableQuestItem(forceOff: true);
			return;
		}
		bool flag = false;
		for (int i = 0; i < Player.instance.activeMissions.Count; i++)
		{
			MissionData missionData = Player.instance.activeMissions[i];
			if (missionData.uid == activatedByMissionUID && !missionData.completed)
			{
				flag = true;
			}
			if (activatedByMultipleMissions && missionData.uid == activatedBySecondMissionUID && !missionData.completed)
			{
				flag = true;
			}
		}
		if (activatedByCompletedMissionUID > -1 && Player.instance.GetMissionCompletedData(activatedByCompletedMissionUID) != null)
		{
			flag = true;
		}
		if (!flag)
		{
			DisableQuestItem();
		}
		else
		{
			EnableQuestItem();
		}
	}

	private void HandleQuestItemClicked()
	{
		if (FlashSaleController.instance != null && FlashSaleController.instance.FlashSaleActive)
		{
			return;
		}
		CityController.instance.cityCharacter.StopMove();
		uiItem.enabled = false;
		CommunicatorManager.instance.UpdateMissionWithTargetMessage(type.ToString());
		Camera thisCamera = CityController.instance.cameraHolder.GetComponent<CameraPinchZoomer>().thisCamera;
		Vector3 position = thisCamera.WorldToScreenPoint(groupObject.transform.position);
		Vector3 position2 = GameUI.instance.missionRevealCamera.ScreenToWorldPoint(position);
		groupObject.transform.parent = null;
		groupObject.transform.position = position2;
		groupObject.transform.localScale = new Vector3(125f, 125f, 125f);
		groupObject.layer = LayerMask.NameToLayer("UI");
		groupObject.transform.DOMove(GameUI.instance.missionFlagIcon.position, 0.8f).SetEase(Ease.OutSine).OnComplete(() =>
		{
			groupObject.transform.DOScale(new Vector3(175f, 175f, 175f), 0.1f).OnComplete(() =>
			{
				groupObject.transform.DOScale(new Vector3(0f, 0f, 0f), 0.1f).SetDelay(0.1f).OnComplete(() =>
				{
					groupObject.SetActive(value: false);
				});
			});
		});
		if (animator != null)
		{
			animator.Play("City_dumpster_tap");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_wes_boing", GFAudioManager.SFX_GROUP, string.Empty);
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_bean_collect", GFAudioManager.SFX_GROUP, string.Empty);
		Player.instance.CollectGlint(guid, CityController.instance.communicator.GetCurrentDay());
	}

	private void HandleOnClick()
	{
		if (!CityController.instance.pinchZoomer.isZoomedOut)
		{
			HandleQuestItemClicked();
		}
	}
}
