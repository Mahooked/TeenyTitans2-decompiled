using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AccessoryItemDefinition : ItemDefinition
{
	[SerializeField]
	private List<AccessoryItemEffect> _effects = new List<AccessoryItemEffect>();

	[SerializeField]
	private List<AccessoryAnimationCue> _animationCues = new List<AccessoryAnimationCue>();

	public Vector3 cameraOffsetPositionZoomedOut;

	public Vector3 cameraOffsetPositionZoomedOutEnemy;

	public Vector3 offsetPosition;

	public Vector3 enemyOffsetPosition;

	public int maxHealthLowest;

	public int maxHealthHighest;

	public string prefabPath = string.Empty;

	public string modelPath = string.Empty;

	public string modelViewerAnimation = string.Empty;

	public float modelViewerAnimationTime;

	public Vector3 modelViewerOffset;

	public Vector3 modelViewerOffsetIpad;

	public Vector3 modelViewerCameraOffset;

	public Vector3 modelViewerCameraRotationOffset;

	public string cityRewardAnimation = string.Empty;

	public string particlePath = string.Empty;

	public bool showInTower = true;

	[SerializeField]
	private float _batteryChargeUpRate = 2f;

	[SerializeField]
	private float _batteryChargeUpPercent = 1f;

	[SerializeField]
	private float _batteryDrainRate = 0.2f;

	[SerializeField]
	private float _batteryDrainPercent = 1f;

	[SerializeField]
	private string _introSoundPath = string.Empty;

	[SerializeField]
	private string[] _introVOPaths = new string[0];

	[SerializeField]
	private string _introAnimation = string.Empty;

	[SerializeField]
	private string _introAnimationEnemy = string.Empty;

	[SerializeField]
	private int _cost;

	[SerializeField]
	private string _shopAnimation;

	[SerializeField]
	private Vector3 _shopOffsetPosition;

	[SerializeField]
	private Vector3 _shopRotation;

	[SerializeField]
	private Vector3 _shopScale;

	[SerializeField]
	private string _revealAnimation;

	[SerializeField]
	private Vector3 _revealOffsetPosition;

	[SerializeField]
	private Vector3 _revealRotation;

	[SerializeField]
	private Vector3 _revealScale;

	public List<AccessoryItemEffect> effects
	{
		get
		{
			return _effects;
		}
	}

	public List<AccessoryAnimationCue> animationCues
	{
		get
		{
			return _animationCues;
		}
	}

	public float batteryChargeUpRate
	{
		get
		{
			return _batteryChargeUpRate;
		}
	}

	public float batteryChargeUpPercent
	{
		get
		{
			return _batteryChargeUpPercent;
		}
	}

	public float batteryDrainRate
	{
		get
		{
			return _batteryDrainRate;
		}
	}

	public float batteryDrainPercent
	{
		get
		{
			return _batteryDrainPercent;
		}
	}

	public string introSoundPath
	{
		get
		{
			return _introSoundPath;
		}
	}

	public string[] introVOPaths
	{
		get
		{
			return _introVOPaths;
		}
	}

	public string introAnimation
	{
		get
		{
			return _introAnimation;
		}
		set
		{
			_introAnimation = value;
		}
	}

	public string introAnimationEnemy
	{
		get
		{
			return _introAnimationEnemy;
		}
		set
		{
			_introAnimationEnemy = value;
		}
	}

	public int cost
	{
		get
		{
			return _cost;
		}
		set
		{
			_cost = value;
		}
	}

	public string shopAnimation
	{
		get
		{
			return _shopAnimation;
		}
		set
		{
			_shopAnimation = value;
		}
	}

	public Vector3 shopOffsetPosition
	{
		get
		{
			return _shopOffsetPosition;
		}
		set
		{
			_shopOffsetPosition = value;
		}
	}

	public Vector3 shopRotation
	{
		get
		{
			return _shopRotation;
		}
		set
		{
			_shopRotation = value;
		}
	}

	public Vector3 shopScale
	{
		get
		{
			return _shopScale;
		}
		set
		{
			_shopScale = value;
		}
	}

	public string revealAnimation
	{
		get
		{
			return _revealAnimation;
		}
		set
		{
			_revealAnimation = value;
		}
	}

	public Vector3 revealOffsetPosition
	{
		get
		{
			return _revealOffsetPosition;
		}
		set
		{
			_revealOffsetPosition = value;
		}
	}

	public Vector3 revealRotation
	{
		get
		{
			return _revealRotation;
		}
		set
		{
			_revealRotation = value;
		}
	}

	public Vector3 revealScale
	{
		get
		{
			return _revealScale;
		}
		set
		{
			_revealScale = value;
		}
	}
}
