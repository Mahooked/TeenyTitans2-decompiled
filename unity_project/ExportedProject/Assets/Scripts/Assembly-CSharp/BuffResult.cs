public class BuffResult
{
	public CharacterStatType statType;

	public int amount;

	public float modifier;

	public Character source;

	public Character target;

	public bool display = true;

	public string statTypeKey;

	public bool isBonus { get; private set; }

	public BuffResult(CharacterStatType statType, int amount, Character source = null, Character target = null, bool display = true)
	{
		this.statType = statType;
		this.amount = amount;
		this.source = source;
		this.target = target;
		this.display = display;
		switch (statType)
		{
		case CharacterStatType.Attack:
			statTypeKey = "POWER";
			break;
		case CharacterStatType.Dodge:
			statTypeKey = "DODGE";
			break;
		case CharacterStatType.HitPoints:
			statTypeKey = string.Empty;
			break;
		case CharacterStatType.Luck:
			statTypeKey = "LUCK";
			break;
		case CharacterStatType.Speed:
			statTypeKey = string.Empty;
			break;
		}
		isBonus = true;
	}

	public BuffResult(float modifier, CharacterStatType statType, Character source = null, Character target = null, bool display = true)
	{
		this.statType = statType;
		this.modifier = modifier;
		this.source = source;
		this.target = target;
		this.display = display;
		switch (statType)
		{
		case CharacterStatType.Attack:
			statTypeKey = "POWER";
			break;
		case CharacterStatType.Dodge:
			statTypeKey = "DODGE";
			break;
		case CharacterStatType.HitPoints:
			statTypeKey = string.Empty;
			break;
		case CharacterStatType.Luck:
			statTypeKey = "LUCK";
			break;
		case CharacterStatType.Speed:
			statTypeKey = string.Empty;
			break;
		}
		isBonus = false;
	}
}
