using UnityEngine;

public class Roll
{
	private int _rolls = 1;

	private int _faces = 6;

	private int _modifier;

	public Roll(int rolls = 1, int faces = 6, int modifier = 0)
	{
		_rolls = rolls;
		_faces = faces;
		_modifier = modifier;
	}

	public int RollDie()
	{
		int num = 0;
		for (int i = 0; i < _rolls; i++)
		{
			num += Random.Range(1, _faces + 1);
		}
		return Mathf.Max(0, num + _modifier);
	}

	public static int QuickRoll(int rolls, int faces, int modifier = 0)
	{
		int num = 0;
		for (int i = 0; i < rolls; i++)
		{
			num += Random.Range(1, faces + 1);
		}
		return Mathf.Max(0, num + modifier);
	}
}
