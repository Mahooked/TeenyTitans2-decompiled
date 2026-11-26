using UnityEngine;

public class TitanChoiceScreen : MonoBehaviour
{
	public void OnHandleChooseRobin()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Robin);
		gameObject.SetActive(value: false);
	}

	public void OnHandleChooseBeastBoy()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.BeastBoy);
		gameObject.SetActive(value: false);
	}

	public void OnHandleChooseRaven()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Raven);
		gameObject.SetActive(value: false);
	}

	public void OnHandleChooseStarfire()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Starfire);
		gameObject.SetActive(value: false);
	}

	public void OnHandleChooseCyborg()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Cyborg);
		gameObject.SetActive(value: false);
	}
}
