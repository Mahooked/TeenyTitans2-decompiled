using UnityEngine;

public class CharacterViewShinyRank : MonoBehaviour
{
	public GameObject rank1;

	public GameObject rank2;

	public GameObject rank3;

	public void UpdateWithCharacter(Character character)
	{
		rank1.SetActive(false);
		rank2.SetActive(false);
		rank3.SetActive(false);
		if (character.shinyRank == 1)
		{
			rank1.SetActive(true);
		}
		if (character.shinyRank == 2)
		{
			rank1.SetActive(true);
			rank2.SetActive(true);
		}
		if (character.shinyRank == 3)
		{
			rank1.SetActive(true);
			rank2.SetActive(true);
			rank3.SetActive(true);
		}
	}
}
