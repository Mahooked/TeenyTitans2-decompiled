public class PlayerAI_HoodedHood : PlayerAI
{
	private Character hoodedHood;

	protected override float swapChance => 1f;

	protected override void Start()
	{
		base.Start();
		hoodedHood = party.Find((Character obj) => obj.characterDefinition.characterName == "The Hooded Hood");
		if (hoodedHood != null)
		{
			hoodedHood.Death += HandleDeath;
		}
	}

	private new void OnDestroy()
	{
		if (hoodedHood != null)
		{
			hoodedHood.Death -= HandleDeath;
		}
	}

	private void HandleDeath()
	{
		hoodedHood.Death -= HandleDeath;
		int count = party.Count;
		for (int i = 0; i < count; i++)
		{
			if (party[i] != hoodedHood)
			{
				party[i].DoDamage(new DamageResult(party[i].hitPoints, ImpactType.None));
			}
		}
	}

	protected override bool CheckForSwap()
	{
		if (character.characterDefinition.characterName == "Shadow Fig")
		{
			return false;
		}
		return base.CheckForSwap();
	}
}
