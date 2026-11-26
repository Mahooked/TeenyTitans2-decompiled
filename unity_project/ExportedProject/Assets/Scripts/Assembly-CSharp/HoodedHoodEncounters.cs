using System.Collections;
using UnityEngine;

public class HoodedHoodEncounters : MonoBehaviour
{
	public EncounterGroup petshopTournament;

	public EncounterGroup shadyTournament;

	public EncounterGroup beachTournament;

	public EncounterGroup uptownTournament;

	public EncounterGroup jailTournament;

	public EncounterGroup finalTournament;

	private void Awake()
	{
		StartCoroutine("CheckForUnplayedHoodedHoodConvo");
	}

	public Vector3 GetHoodPositionForName(string convoName)
	{
		if (convoName.Equals("CONVO_HoodedHood_afterpetstore"))
		{
			return petshopTournament.Hoodedhood.transform.position;
		}
		if (convoName.Equals("CONVO_HOOD_AFTER2NDTOURNY1"))
		{
			return shadyTournament.Hoodedhood.transform.position;
		}
		if (convoName.Equals("CONVO_HOOD_AFTER3RDTOURNY"))
		{
			return beachTournament.Hoodedhood.transform.position;
		}
		if (convoName.Equals("CONVO_HOOD_AFTERJAIL"))
		{
			return jailTournament.Hoodedhood.transform.position;
		}
		if (convoName.Equals("CONVO_GAMEEND"))
		{
			return finalTournament.Hoodedhood.transform.position;
		}
		return CityController.instance.cityCharacter.transform.position + new Vector3(-0.2f, 0.088f, -0.3f);
	}

	private void PositionRobinForEncounter(EncounterGroup encounterGroup)
	{
		CityController.instance.cityCharacter.transform.position = encounterGroup.Robin.transform.position;
		CityController.instance.cameraHolder.position = CityController.instance.cityCharacter.transform.position;
		if (encounterGroup.Robin.transform.localScale.x == -1f)
		{
			CityController.instance.cityCharacter.animator.isFlipped = true;
		}
		else
		{
			CityController.instance.cityCharacter.animator.isFlipped = false;
		}
		CityController.instance.cityCharacter.UpdateStartNode();
	}

	private IEnumerator CheckForUnplayedHoodedHoodConvo()
	{
		yield return new WaitForEndOfFrame();
		for (int i = 0; i < Player.instance.completedMissions.Count; i++)
		{
			int uid = Player.instance.completedMissions[i].uid;
			Mission mission = GameManager.instance.communicatorDatabase.GetMission(uid);
			if (mission != null && mission.type == MissionType.Story && mission.completionConvo != null && SaveUtility.GetInt("GAME_PLAYED_" + mission.completionConvo.name) == 0)
			{
				if (uid == 8)
				{
					PositionRobinForEncounter(petshopTournament);
				}
				if (uid == 13)
				{
					PositionRobinForEncounter(shadyTournament);
				}
				if (uid == 18)
				{
					PositionRobinForEncounter(beachTournament);
				}
				if (uid == 20)
				{
					PositionRobinForEncounter(uptownTournament);
				}
				if (uid == 36)
				{
					PositionRobinForEncounter(jailTournament);
				}
				if (uid == 37)
				{
					PositionRobinForEncounter(finalTournament);
				}
			}
		}
		yield return new WaitForEndOfFrame();
	}
}
