using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BattleCharacterSounds
{
	[Serializable]
	public class BattleCharacterSoundData
	{
		[SerializeField]
		private float _probability = 1f;

		[SerializeField]
		private List<string> _soundPaths;

		public float probability
		{
			get
			{
				return _probability;
			}
		}

		public List<string> soundPaths
		{
			get
			{
				return _soundPaths;
			}
		}

		public BattleCharacterSoundData()
		{
			_soundPaths = new List<string>();
		}

		public string GetRandomSound()
		{
			if (_soundPaths.Count > 0)
			{
				return _soundPaths[UnityEngine.Random.Range(0, _soundPaths.Count)];
			}
			return string.Empty;
		}
	}

	[SerializeField]
	private BattleCharacterSoundData _swapInSounds;

	[SerializeField]
	private BattleCharacterSoundData _swapOutSounds;

	[SerializeField]
	private BattleCharacterSoundData _hurtSounds;

	[SerializeField]
	private BattleCharacterSoundData _healSounds;

	[SerializeField]
	private BattleCharacterSoundData _deathSounds;

	[SerializeField]
	private BattleCharacterSoundData _useTofuSounds;

	[SerializeField]
	private BattleCharacterSoundData _victorySounds;

	public BattleCharacterSoundData swapInSounds
	{
		get
		{
			return _swapInSounds;
		}
	}

	public BattleCharacterSoundData swapOutSounds
	{
		get
		{
			return _swapOutSounds;
		}
	}

	public BattleCharacterSoundData hurtSounds
	{
		get
		{
			return _hurtSounds;
		}
	}

	public BattleCharacterSoundData healSounds
	{
		get
		{
			return _healSounds;
		}
	}

	public BattleCharacterSoundData deathSounds
	{
		get
		{
			return _deathSounds;
		}
	}

	public BattleCharacterSoundData useTofuSounds
	{
		get
		{
			return _useTofuSounds;
		}
	}

	public BattleCharacterSoundData victorySounds
	{
		get
		{
			return _victorySounds;
		}
	}
}
