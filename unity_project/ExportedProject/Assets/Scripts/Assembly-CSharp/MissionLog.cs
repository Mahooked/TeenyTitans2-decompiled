using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionLog : MonoBehaviour
{
	public TextMeshPro chosenTitanTextMesh;

	public TextMeshPro completeListTextMesh;

	public TextMeshPro incompleteListTextMesh;

	public tk2dUIItem exitButton;

	private void OnEnable()
	{
		exitButton.OnClick += HandleExit;
		chosenTitanTextMesh.text = "Chosen Titan: " + CityCharacter.characterType;
		TextMeshPro textMeshPro = completeListTextMesh;
		string empty = string.Empty;
		incompleteListTextMesh.text = empty;
		textMeshPro.text = empty;
		List<Mission> allSideMissions = GameManager.instance.communicatorDatabase.GetAllSideMissions(false);
		allSideMissions.Sort((Mission x, Mission y) => x.title.CompareTo(y.title));
		for (int num = 0; num < allSideMissions.Count; num++)
		{
			Mission mission = allSideMissions[num];
			if (Player.instance.HasMissionCompleted(mission.uid))
			{
				completeListTextMesh.text += string.Format("* {0} - UID: {1}\n", mission.title, mission.uid);
			}
			else
			{
				incompleteListTextMesh.text += string.Format("* {0} - UID: {1}\n", mission.title, mission.uid);
			}
		}
	}

	private void OnDisable()
	{
		exitButton.OnClick -= HandleExit;
	}

	private void HandleExit()
	{
		base.gameObject.SetActive(false);
	}
}
