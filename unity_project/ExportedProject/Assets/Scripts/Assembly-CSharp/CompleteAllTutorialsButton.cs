using UnityEngine;

public class CompleteAllTutorialsButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	private void Start()
	{
		uiItem.OnClick += CompleteAllTutorials;
	}

	private void CompleteAllTutorials()
	{
		Debug.Log("Title Screen, Complete All Tutorials");
		if (CoreGameTutorial.instance != null)
		{
			CoreGameTutorial.instance.CompleteAllTutorials();
		}
		if (TutorialManager.instance != null)
		{
			TutorialManager.instance.CompleteAllTutorials();
		}
		int[] array = new int[6] { 93, 94, 98, 99, 97, 0 };
		for (int i = 0; i < array.Length; i++)
		{
			CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(array[i]);
			if (character != null && !Player.instance.HasCharacter(character.uid))
			{
				Player.instance.AddCharacter(character);
				Player.instance.AcquireCharacter(character.uid);
				Player.instance.AddToParty(Player.instance.characters.GetLastItem());
			}
		}
		if (!Player.instance.HasTriggered("PLAYER_PLAYED_INTRO_SEQUENCE"))
		{
			NewGameIntroController.completeAllTutorialsMode = true;
		}
		Player.instance.SilentlyCompleteMission(4, false);
		Player.instance.AddMission(101);
	}
}
