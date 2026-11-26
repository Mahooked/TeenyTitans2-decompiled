using System;
using UnityEngine;

public class DailyMissionBoard : MonoBehaviour
{
	[SerializeField]
	private TransformLink _missionStar;

	private void Start()
	{
		if (_missionStar != null)
		{
			_missionStar.LinkTo = CityController.instance.cameraHolder;
		}
		if (DailyMissionProgressMenu.instance == null)
		{
			DailyMissionProgressMenu objectFromResource = ObjectPool.GetObjectFromResource<DailyMissionProgressMenu>("Prefabs/GUI/DailyMissionProgressGUI");
			objectFromResource.transform.parent = ((!(CityController.instance != null)) ? null : CityController.instance.guiCamera2.transform);
			objectFromResource.transform.localPosition = new Vector3(0f, 0f, -150f);
		}
		Player.instance.MissionAdded += HandleMissionAdded;
		DailyMissionManager instance = DailyMissionManager.instance;
		instance.DailyMissionGenerated = (Action)Delegate.Combine(instance.DailyMissionGenerated, new Action(HandleDailyMissionGenerated));
		RefreshMissionStar();
	}

	private void OnDestroy()
	{
		if (Player.instance != null)
		{
			Player.instance.MissionAdded -= HandleMissionAdded;
		}
		if (DailyMissionManager.instance != null)
		{
			DailyMissionManager instance = DailyMissionManager.instance;
			instance.DailyMissionGenerated = (Action)Delegate.Remove(instance.DailyMissionGenerated, new Action(HandleDailyMissionGenerated));
		}
	}

	public void ShowDailyMissionPopup()
	{
		CityController.instance.cityCharacter.StopMove();
		if ((!(TitleScreenController.instance != null) || !TitleScreenController.instance.titleScreenActive) && !CityReward.instance.showingRewards && !DayNightCard.instance.isActive)
		{
			DailyMissionGUI objectFromResource = ObjectPool.GetObjectFromResource<DailyMissionGUI>("Prefabs/GUI/DailyMissionGUI");
			objectFromResource.transform.SetParent(CityController.instance.guiCamera2.transform);
			objectFromResource.transform.localPosition = new Vector3(0f, 0f, -100f);
			objectFromResource.Init();
		}
	}

	private void RefreshMissionStar()
	{
		_missionStar.gameObject.SetActive(DailyMissionManager.instance.IsDailyMissionAvailable());
	}

	private void HandleMissionAdded(int missionUid, bool isDialogueMission)
	{
		RefreshMissionStar();
	}

	private void HandleDailyMissionGenerated()
	{
		RefreshMissionStar();
	}
}
