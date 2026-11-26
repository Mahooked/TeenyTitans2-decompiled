using UnityEngine;

public class TitanChoiceScreen : MonoBehaviour
{
	public void OnHandleChooseRobin()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Robin);
		base.gameObject.SetActive(false);
	}

	public void OnHandleChooseBeastBoy()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.BeastBoy);
		base.gameObject.SetActive(false);
	}

	public void OnHandleChooseRaven()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Raven);
		base.gameObject.SetActive(false);
	}

	public void OnHandleChooseStarfire()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Starfire);
		base.gameObject.SetActive(false);
	}

	public void OnHandleChooseCyborg()
	{
		CityController.instance.cityCharacter.SwitchToCharacter(CityCharacterType.Cyborg);
		base.gameObject.SetActive(false);
	}
}
