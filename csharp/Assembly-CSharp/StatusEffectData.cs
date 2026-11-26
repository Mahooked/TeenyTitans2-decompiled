using System;
using UnityEngine;

[Serializable]
public class StatusEffectData
{
	[SerializeField]
	private float _probability = 1f;

	[SerializeField]
	private bool _ignoresDodge = true;

	[SerializeField]
	private int _intValue1;

	[SerializeField]
	private int _intValue2;

	[SerializeField]
	private float _floatValue1;

	[SerializeField]
	private float _floatValue2;

	[SerializeField]
	private string _stringValue1;

	[SerializeField]
	private string _stringValue2;

	[SerializeField]
	private RangedInt _rangedIntValue1;

	[SerializeField]
	private RangedInt _rangedIntValue2;

	[SerializeField]
	private RangedFloat _rangedFloatValue1;

	[SerializeField]
	private RangedFloat _rangedFloatValue2;

	[SerializeField]
	private RangedFloat _rangedFloatValue3;

	[SerializeField]
	private RangedFloat _rangedFloatValue4;

	[SerializeField]
	private bool _boolValue1;

	public float probability => _probability;

	public bool ignoresDodge => _ignoresDodge;

	public int intValue1
	{
		get
		{
			return _intValue1;
		}
		set
		{
			_intValue1 = value;
		}
	}

	public int intValue2
	{
		get
		{
			return _intValue2;
		}
		set
		{
			_intValue2 = value;
		}
	}

	public float floatValue1
	{
		get
		{
			return _floatValue1;
		}
		set
		{
			_floatValue1 = value;
		}
	}

	public float floatValue2
	{
		get
		{
			return _floatValue2;
		}
		set
		{
			_floatValue2 = value;
		}
	}

	public string stringValue1
	{
		get
		{
			return _stringValue1;
		}
		set
		{
			_stringValue1 = value;
		}
	}

	public string stringValue2
	{
		get
		{
			return _stringValue2;
		}
		set
		{
			_stringValue2 = value;
		}
	}

	public RangedInt rangedIntValue1
	{
		get
		{
			return _rangedIntValue1;
		}
		set
		{
			_rangedIntValue1 = value;
		}
	}

	public RangedInt rangedIntValue2
	{
		get
		{
			return _rangedIntValue2;
		}
		set
		{
			_rangedIntValue2 = value;
		}
	}

	public RangedFloat rangedFloatValue1
	{
		get
		{
			return _rangedFloatValue1;
		}
		set
		{
			_rangedFloatValue1 = value;
		}
	}

	public RangedFloat rangedFloatValue2
	{
		get
		{
			return _rangedFloatValue2;
		}
		set
		{
			_rangedFloatValue2 = value;
		}
	}

	public RangedFloat rangedFloatValue3
	{
		get
		{
			return _rangedFloatValue3;
		}
		set
		{
			_rangedFloatValue3 = value;
		}
	}

	public RangedFloat rangedFloatValue4
	{
		get
		{
			return _rangedFloatValue4;
		}
		set
		{
			_rangedFloatValue4 = value;
		}
	}

	public bool boolValue1
	{
		get
		{
			return _boolValue1;
		}
		set
		{
			_boolValue1 = value;
		}
	}

	public float duration => _floatValue1;

	public BeatBoxType beatBoxType => (BeatBoxType)_intValue2;

	public CharacterStatType statType => (CharacterStatType)_intValue2;

	public StatusEffectData()
	{
	}

	public StatusEffectData(StatusEffectData data)
	{
		intValue1 = data.intValue1;
		intValue2 = data.intValue2;
		floatValue1 = data.floatValue1;
		floatValue2 = data.floatValue2;
		stringValue1 = data.stringValue1;
		stringValue2 = data.stringValue2;
		rangedIntValue1 = data.rangedIntValue1;
		rangedIntValue2 = data.rangedIntValue2;
		rangedFloatValue1 = data.rangedFloatValue1;
		rangedFloatValue2 = data.rangedFloatValue2;
		rangedFloatValue3 = data.rangedFloatValue3;
		rangedFloatValue4 = data.rangedFloatValue4;
		boolValue1 = data.boolValue1;
	}
}
