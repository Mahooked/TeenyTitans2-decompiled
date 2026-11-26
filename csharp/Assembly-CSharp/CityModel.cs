using UnityEngine;

public class CityModel : MonoBehaviour
{
	public static CityModel instance;

	public GameObject tutorialShopBuilding;

	public GameObject tutorialShopBuilding2;

	public GameObject tutorialTournamentBuilding;

	public GameObject tournamentShady;

	public GameObject tournamentUptown;

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		instance = null;
	}
}
