public class TT2NetPlayerAction
{
	public enum ActionType
	{
		Ability = 0,
		CharacterSwap = 1,
		Tofu = 2,
		StatusEffectTick = 3,
		StatusEffectExpire = 4,
		ChannelingTick = 5,
		ChannelingNextStep = 6,
		ChannelingExpire = 7,
		AccessoryUsed = 8,
		AccessoryTick = 9,
		AccessoryExpire = 10,
		ModChipTriggerTick = 11
	}

	public double timestamp;

	public ActionType type;

	public int seed;

	public int intValue1;

	public int intValue2;

	public bool boolValue1;

	public bool boolValue2;

	public float floatValue1;

	public float floatValue2;

	public bool isWaiting;

	public bool isConfirmed;

	public TT2NetPlayer.ProcessActionConfirmation confirmationDelegate;

	public int abilityUid
	{
		get
		{
			return intValue1;
		}
		set
		{
			intValue1 = value;
		}
	}

	public int characterUid
	{
		get
		{
			return intValue2;
		}
		set
		{
			intValue2 = value;
		}
	}

	public bool isCast
	{
		get
		{
			return boolValue1;
		}
		set
		{
			boolValue1 = value;
		}
	}

	public float meterProgress
	{
		get
		{
			return floatValue1;
		}
		set
		{
			floatValue1 = value;
		}
	}

	public float finalDamageMultiplier
	{
		get
		{
			return floatValue2;
		}
		set
		{
			floatValue2 = value;
		}
	}

	public int tofuType
	{
		get
		{
			return intValue1;
		}
		set
		{
			intValue1 = value;
		}
	}

	public int effectUid
	{
		get
		{
			return intValue1;
		}
		set
		{
			intValue1 = value;
		}
	}

	public int effectIndex
	{
		get
		{
			return intValue1;
		}
		set
		{
			intValue1 = value;
		}
	}

	public TT2NetPlayerAction(ActionType type)
	{
		this.type = type;
		isWaiting = true;
		isConfirmed = false;
	}

	public TT2NetPlayerAction(double timestamp, ActionType type)
	{
		this.timestamp = timestamp;
		this.type = type;
		isWaiting = true;
		isConfirmed = false;
	}
}
