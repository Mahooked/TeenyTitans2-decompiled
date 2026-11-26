using UnityEngine;

public class DialogueChoiceButton : MonoBehaviour
{
	public Animation animator;

	public tk2dTextMesh textMesh;

	public tk2dUIItem button;

	private Choice _choice;

	public Choice choice
	{
		get
		{
			return _choice;
		}
		set
		{
			if (value != _choice)
			{
				_choice = value;
			}
		}
	}

	public void SetChoice(Choice newChoice)
	{
		_choice = newChoice;
		textMesh.text = _choice.text;
		textMesh.Commit();
	}
}
