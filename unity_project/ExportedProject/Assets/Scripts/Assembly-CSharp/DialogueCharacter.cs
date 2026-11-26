using UnityEngine;

public class DialogueCharacter : MonoBehaviour
{
	public Animator animator;

	public tk2dSprite bodySprite;

	public tk2dSprite faceSprite;

	private CharacterName _character;

	public CharacterName character
	{
		get
		{
			return _character;
		}
		set
		{
			if (value != _character)
			{
				_character = value;
			}
		}
	}
}
