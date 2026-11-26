using UnityEngine;

public class TournamentPlayerPortrait : MonoBehaviour
{
	public Animator animator;

	public tk2dSprite portraitSprite;

	public tk2dSprite defeatedSprite;

	private void Awake()
	{
		if (CityCharacter.RobinType == CharacterName.DogRobin)
		{
			portraitSprite.SetSprite("menu_vsscreen_portrait_robin_dog");
		}
		else
		{
			portraitSprite.SetSprite("menu_vsscreen_portrait_robin");
		}
	}
}
