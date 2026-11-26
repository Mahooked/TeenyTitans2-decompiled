using UnityEngine;

public class HealResult
{
	public int amount;

	public int adjustedAmount;

	public Character source;

	public Character target;

	public bool display;

	public bool full;

	public HealResult(int amount)
	{
		this.amount = amount;
		adjustedAmount = 0;
		display = true;
		full = false;
	}

	public HealResult(Character source, Character target)
	{
		this.source = source;
		this.target = target;
		amount = 0;
		adjustedAmount = 0;
		display = true;
		full = false;
	}

	public HealResult(Character source, Character target, int amount)
	{
		this.source = source;
		this.target = target;
		this.amount = amount;
		adjustedAmount = Mathf.Min(target.maxHitPoints, target.hitPoints + amount) - target.hitPoints;
		if (target.hitPoints == target.maxHitPoints)
		{
			full = true;
		}
		else
		{
			full = false;
		}
		display = true;
	}
}
