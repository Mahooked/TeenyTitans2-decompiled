using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AELayer
{
	public int id;

	public int index = -1;

	public int parent;

	public int width = 1;

	public int height = 1;

	public string name;

	public AELayerType type;

	public AELayerBlendingType blending;

	public AELayerBlendingType forcedBlending;

	public int inFrame;

	public int outFrame;

	public float inTime;

	public float outTime;

	public string source;

	public bool hasStaticMaterial;

	[SerializeField]
	private List<AEFrame> _frames;

	private string _sourceNoExt = string.Empty;

	public string sourceNoExt
	{
		get
		{
			if (string.IsNullOrEmpty(_sourceNoExt))
			{
				_sourceNoExt = source.Substring(0, source.Length - 4);
			}
			return _sourceNoExt;
		}
	}

	public int totalFrames
	{
		get
		{
			return _frames.Count;
		}
	}

	public void AddFrame(AEFrame frame)
	{
		if (_frames == null)
		{
			_frames = new List<AEFrame>();
		}
		_frames.Add(frame);
	}

	public void SetFrames(List<AEFrame> newFrames)
	{
		_frames.Clear();
		int count = newFrames.Count;
		for (int i = 0; i < count; i++)
		{
			AddFrame(newFrames[i]);
		}
	}

	public AEFrame GetFrame(int index)
	{
		if (index >= _frames.Count)
		{
			return null;
		}
		return _frames[index];
	}

	public void SetInOutTime(float timeIn, float timeOut, AECompositionNew comp)
	{
		inTime = timeIn;
		outTime = timeOut;
		inFrame = Mathf.RoundToInt(inTime / comp.frameDuration);
		outFrame = Mathf.RoundToInt(outTime / comp.frameDuration);
		if (outFrame + 1 == comp.totalFrames)
		{
			outFrame = comp.totalFrames;
		}
	}
}
