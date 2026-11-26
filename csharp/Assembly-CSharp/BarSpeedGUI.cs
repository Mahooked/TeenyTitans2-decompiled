using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BarSpeedGUI : MonoBehaviour
{
	public tk2dSlicedSprite backing;

	[FormerlySerializedAs("fill")]
	public tk2dClippedSprite speedFill;

	public tk2dClippedSprite powerFill;

	public TextMeshPro headerText;

	private int _speedLevel = -1;

	public int speedLevel
	{
		get
		{
			return _speedLevel;
		}
		set
		{
			_speedLevel = value;
			Refresh();
		}
	}

	private void Start()
	{
		Refresh();
	}

	private void Refresh()
	{
		if (_speedLevel >= 0)
		{
			Rect clipRect = speedFill.ClipRect;
			clipRect.width = (float)_speedLevel / 10f;
			speedFill.ClipRect = clipRect;
			clipRect.width = 1f - (float)speedLevel / 10f;
			powerFill.ClipRect = clipRect;
			tk2dClippedSprite tk2dClippedSprite2 = speedFill;
			Color white = Color.white;
			powerFill.color = white;
			tk2dClippedSprite2.color = white;
		}
		else
		{
			tk2dClippedSprite tk2dClippedSprite3 = speedFill;
			Color white = Color.clear;
			powerFill.color = white;
			tk2dClippedSprite3.color = white;
		}
	}
}
