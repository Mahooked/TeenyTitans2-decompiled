using System.Collections.Generic;
using UnityEngine;

public class GFSoundGroup
{
	private List<AudioSource> _sources;

	private float _volume = 1f;

	private bool _isMuted;

	public List<AudioSource> sources => _sources;

	public float volume
	{
		get
		{
			return (!_isMuted) ? _volume : 0f;
		}
		set
		{
			if (value != _volume)
			{
				_volume = value;
			}
		}
	}

	public bool isMuted
	{
		get
		{
			return _isMuted;
		}
		set
		{
			if (value != _isMuted)
			{
				_isMuted = value;
			}
		}
	}

	public bool isAnyPlaying
	{
		get
		{
			for (int i = 0; i < _sources.Count; i++)
			{
				if (_sources[i].isPlaying)
				{
					return true;
				}
			}
			return false;
		}
	}

	public GFSoundGroup(float volume = 1f)
	{
		_sources = new List<AudioSource>();
		_volume = volume;
	}
}
