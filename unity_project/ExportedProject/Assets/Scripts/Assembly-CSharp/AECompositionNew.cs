using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AECompositionNew
{
	public int id;

	public float width;

	public float height;

	public float duration;

	public int totalFrames;

	public float frameDuration;

	[SerializeField]
	private List<AELayer> _layers = new List<AELayer>();

	[SerializeField]
	private int _numLayers;

	public List<AELayer> layers
	{
		get
		{
			return _layers;
		}
	}

	public int numLayers
	{
		get
		{
			return _numLayers;
		}
	}

	public void AddLayer(AELayer layer)
	{
		if (_layers == null)
		{
			_layers = new List<AELayer>();
		}
		while (_layers.Count <= layer.index)
		{
			_layers.Add(null);
		}
		_layers[layer.index] = layer;
		_numLayers++;
	}

	public AELayer GetLayer(int index)
	{
		if (index < 0 || index >= _layers.Count)
		{
			return null;
		}
		return _layers[index];
	}

	public void Clear()
	{
		_layers.Clear();
		_numLayers = 0;
	}
}
