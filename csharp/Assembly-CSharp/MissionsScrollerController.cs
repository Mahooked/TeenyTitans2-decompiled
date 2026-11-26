using UnityEngine;

public class MissionsScrollerController : MonoBehaviour
{
	public tk2dSprite buttonJumpCity;

	public Color disabledColor = new Color(1f, 1f, 1f, 0.5f);

	private CityName currentCityName;

	private void Start()
	{
		CityController.instance.communicator.MissionScrollerWillAppear += HandleMissionScrollerWillAppear;
	}

	private void OnDestroy()
	{
		if (CityController.instance != null && CityController.instance.communicator != null)
		{
			CityController.instance.communicator.MissionScrollerWillAppear -= HandleMissionScrollerWillAppear;
		}
	}

	private void HandleMissionScrollerWillAppear()
	{
		UpdateCityButton(currentCityName);
	}

	public void HandleCityButtonPressed()
	{
		CityName cityName = CityController.instance.cityName;
		if (currentCityName == CityName.JumpCity)
		{
			cityName = CityName.Gotham;
		}
		if (currentCityName == CityName.Gotham)
		{
			cityName = CityName.Metropolis;
		}
		if (currentCityName == CityName.Metropolis)
		{
			cityName = CityName.JumpCity;
		}
		FilterMissions(cityName);
	}

	public void UpdateCityButton(CityName cityName)
	{
		if (cityName == CityName.Any)
		{
			currentCityName = ((CityController.instance != null) ? CityController.instance.cityName : CityName.JumpCity);
		}
		else
		{
			currentCityName = cityName;
		}
		switch (cityName)
		{
		case CityName.JumpCity:
			buttonJumpCity.SetSprite("gui_communicator_missions_tab_logo_jumpcity");
			break;
		case CityName.Gotham:
			buttonJumpCity.SetSprite("gui_communicator_missions_tab_logo_gotham");
			break;
		case CityName.Metropolis:
			buttonJumpCity.SetSprite("gui_communicator_missions_tab_logo_metropolis");
			break;
		}
	}

	private void FilterMissions(CityName cityName)
	{
		UpdateCityButton(cityName);
		CityController.instance.communicator.UpdateMissions(playMissionSideIntro: false, cityName);
	}
}
