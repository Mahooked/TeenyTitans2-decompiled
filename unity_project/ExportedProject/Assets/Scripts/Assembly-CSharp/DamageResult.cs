public class DamageResult
{
	public int damage;

	public int luckBonusDamage;

	public int classBonusDamage;

	public CharacterClass bonusClass;

	public bool isDodged;

	public ImpactType impactType;

	public bool isKillingBlow;

	public Character source;

	public Character target;

	public AbilityData abilityData;

	public bool isDirect;

	public bool isCounter;

	public bool isCutenessDamage;

	public bool useMissedMessage;

	public bool useRandomPositionOffset = true;

	public bool showDamage = true;

	public int totalDamage
	{
		get
		{
			return damage + luckBonusDamage + classBonusDamage;
		}
	}

	public DamageResult(int damage, ImpactType impactType = ImpactType.Normal, bool isKillingBlow = true)
	{
		this.damage = damage;
		classBonusDamage = 0;
		bonusClass = CharacterClass.None;
		isDodged = false;
		this.impactType = impactType;
		this.isKillingBlow = isKillingBlow;
		source = null;
		target = null;
		isDirect = true;
		isCounter = false;
		isCutenessDamage = false;
		useMissedMessage = false;
	}

	public DamageResult(Character source, Character target)
	{
		this.source = source;
		this.target = target;
		damage = 0;
		classBonusDamage = 0;
		bonusClass = CharacterClass.None;
		isDodged = false;
		impactType = ImpactType.Normal;
		isKillingBlow = true;
		isDirect = true;
		isCounter = false;
		isCutenessDamage = false;
		useMissedMessage = false;
	}

	public DamageResult(int damage, int classBonusDamage, CharacterClass characterClass, Character source = null, Character target = null)
	{
		this.damage = damage;
		this.classBonusDamage = classBonusDamage;
		bonusClass = characterClass;
		isDodged = false;
		impactType = ImpactType.Normal;
		isKillingBlow = true;
		this.source = source;
		this.target = target;
		isDirect = true;
		isCounter = false;
		isCutenessDamage = false;
		useMissedMessage = false;
	}
}
