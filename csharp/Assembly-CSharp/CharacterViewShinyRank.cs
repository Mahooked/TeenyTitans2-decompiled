using UnityEngine;

public class CharacterViewShinyRank : MonoBehaviour
{
	public GameObject rank1;

	public GameObject rank2;

	public GameObject rank3;

	public void UpdateWithCharacter(Character character)
	{
		rank1.SetActive(value: false);
		rank2.SetActive(value: false);
		rank3.SetActive(value: false);
		if (character.shinyRank == 1)
		{
			rank1.SetActive(value: true);
		}
		if (character.shinyRank == 2)
		{
			rank1.SetActive(value: true);
			rank2.SetActive(value: true);
		}
		if (character.shinyRank == 3)
		{
			rank1.SetActive(value: true);
			rank2.SetActive(value: true);
			rank3.SetActive(value: true);
		}
	}
}
