using UnityEngine;

public static class MissionIntegrityChecker
{
	private static int MISSION_WHERE_TO_BOO = 101;

	private static int MISSION_DROP_IN_ON_JIM = 98;

	private static int MISSION_TO_METROPOLIS = 119;

	private static int MISSION_TREK_THE_HALLS = 105;

	private static int MISSION_TO_GOTHAM = 94;

	public static void CheckStoryMissionIntegrityForPlayer(Player player)
	{
		if (!player.HasMissionCompleted(MISSION_WHERE_TO_BOO))
		{
			return;
		}
		if (player.HasActiveMission(MISSION_DROP_IN_ON_JIM) || player.HasMissionCompleted(MISSION_DROP_IN_ON_JIM))
		{
			if (player.HasMissionCompleted(MISSION_TO_GOTHAM))
			{
				Debug.Log("PLAYER STARTED IN METROPOLIS");
				CheckStoryPath(player, false);
			}
			else
			{
				Debug.Log("PLAYER STARTED IN GOTHAM");
				CheckStoryPath(player, true);
			}
		}
		else if (player.HasActiveMission(MISSION_TREK_THE_HALLS) || player.HasMissionCompleted(MISSION_TREK_THE_HALLS))
		{
			if (player.HasMissionCompleted(MISSION_TO_METROPOLIS))
			{
				Debug.Log("PLAYER STARTED IN GOTHAM");
				CheckStoryPath(player, true);
			}
			else
			{
				Debug.Log("PLAYER STARTED IN METROPOLIS");
				CheckStoryPath(player, false);
			}
		}
		else if (player.HasTriggered("visited_Gotham"))
		{
			Debug.Log("Never got first mission in Gotham, so adding it.");
			player.AddMission(MISSION_DROP_IN_ON_JIM);
		}
		else
		{
			Debug.Log("Never got first mission in Metropolis, so adding it.");
			player.AddMission(MISSION_TREK_THE_HALLS);
		}
	}

	private static void CheckStoryPath(Player player, bool isGotham)
	{
		int[] array = ((!isGotham) ? GameManager.instance.storyMissionPaths.metropolisPath : GameManager.instance.storyMissionPaths.gothamPath);
		Mission mission = null;
		for (int num = player.completedMissions.Count - 1; num >= 0; num--)
		{
			Mission mission2 = GameManager.instance.communicatorDatabase.GetMission(player.completedMissions[num].uid);
			if (mission2 != null && mission2.type == MissionType.Story)
			{
				mission = mission2;
				break;
			}
		}
		if (!(mission != null))
		{
			return;
		}
		Debug.LogFormat("Last story mission completed was \"{0}\"", mission.title);
		Mission mission3 = null;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] == mission.uid && i < array.Length - 1)
			{
				mission3 = GameManager.instance.communicatorDatabase.GetMission(array[i + 1]);
				break;
			}
		}
		if (mission3 != null)
		{
			Debug.LogFormat("Current story mission should be \"{0}\"", mission3.title);
			if (player.HasActiveMission(mission3.uid))
			{
				Debug.Log("Player has the correct active story mission. Let's just make sure they have the requirements for this mission.");
				CheckMissionReqs(player, mission3);
			}
			else
			{
				Debug.Log("Player does not have the active story mission that they should have, so let's add it");
				player.AddMission(mission3.uid);
				CheckMissionReqs(player, mission3);
			}
		}
	}

	private static void CheckMissionReqs(Player player, Mission mission)
	{
		int uid = mission.uid;
		if (uid == 93 && player.GetItemCount(55) == 0)
		{
			Debug.Log("Adding the \"Ice Bulb\" quest item to the player's inventory");
			player.AddItem(55, 1);
		}
		player.AddMission(mission.uid);
	}
}
