using System.Collections.Generic;
using UnityEngine;

namespace TouchScript.Debugging;

[AddComponentMenu("TouchScript/Touch Debugger")]
public class TouchDebugger : MonoBehaviour
{
	[SerializeField]
	private bool showTouchId = true;

	[SerializeField]
	private bool showTags;

	[SerializeField]
	private Texture2D texture;

	[SerializeField]
	private bool useDPI = true;

	[SerializeField]
	private float touchSize = 1f;

	[SerializeField]
	private Color fontColor = new Color(0f, 1f, 1f, 1f);

	private Dictionary<int, ITouch> dummies = new Dictionary<int, ITouch>(10);

	private Dictionary<int, string> tags = new Dictionary<int, string>(10);

	private float textureDPI;

	private float scale;

	private float dpi;

	private float shadowOffset;

	private int textureWidth;

	private int textureHeight;

	private int halfTextureWidth;

	private int halfTextureHeight;

	private int xOffset;

	private int yOffset;

	private int labelWidth;

	private int labelHeight;

	private int fontSize;

	private GUIStyle style;

	public bool ShowTouchId
	{
		get
		{
			return showTouchId;
		}
		set
		{
			showTouchId = value;
		}
	}

	public bool ShowTags
	{
		get
		{
			return showTags;
		}
		set
		{
			showTags = value;
		}
	}

	public Texture2D TouchTexture
	{
		get
		{
			return texture;
		}
		set
		{
			texture = value;
			update();
		}
	}

	public bool UseDPI
	{
		get
		{
			return useDPI;
		}
		set
		{
			useDPI = value;
			update();
		}
	}

	public float TouchSize
	{
		get
		{
			return touchSize;
		}
		set
		{
			touchSize = value;
			update();
		}
	}

	public Color FontColor
	{
		get
		{
			return fontColor;
		}
		set
		{
			fontColor = value;
		}
	}

	private void OnEnable()
	{
		if (TouchTexture == null)
		{
			Debug.LogError("Touch Debugger doesn't have touch texture assigned!");
			return;
		}
		update();
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan += touchesBeganHandler;
			TouchManager.Instance.TouchesEnded += touchesEndedHandler;
			TouchManager.Instance.TouchesMoved += touchesMovedHandler;
			TouchManager.Instance.TouchesCancelled += touchesCancelledHandler;
		}
	}

	private void OnDisable()
	{
		if (TouchManager.Instance != null)
		{
			TouchManager.Instance.TouchesBegan -= touchesBeganHandler;
			TouchManager.Instance.TouchesEnded -= touchesEndedHandler;
			TouchManager.Instance.TouchesMoved -= touchesMovedHandler;
			TouchManager.Instance.TouchesCancelled -= touchesCancelledHandler;
		}
	}

	private void OnGUI()
	{
		if (TouchTexture == null)
		{
			return;
		}
		if (style == null)
		{
			style = new GUIStyle(GUI.skin.label);
		}
		checkDPI();
		style.fontSize = fontSize;
		foreach (KeyValuePair<int, ITouch> dummy in dummies)
		{
			float x = dummy.Value.Position.x;
			float num = (float)Screen.height - dummy.Value.Position.y;
			GUI.DrawTexture(new Rect(x - (float)halfTextureWidth, num - (float)halfTextureHeight, textureWidth, textureHeight), TouchTexture, ScaleMode.ScaleToFit);
			int id = dummy.Value.Id;
			int num2 = 0;
			if (ShowTouchId)
			{
				string text = "id: " + id;
				GUI.color = Color.black;
				GUI.Label(new Rect(x + (float)xOffset + shadowOffset, num + (float)yOffset + shadowOffset, labelWidth, labelHeight), text, style);
				GUI.color = fontColor;
				GUI.Label(new Rect(x + (float)xOffset, num + (float)yOffset, labelWidth, labelHeight), text, style);
				num2++;
			}
			if (ShowTags && tags.ContainsKey(id))
			{
				string text = "tags: " + tags[id];
				GUI.color = Color.black;
				GUI.Label(new Rect(x + (float)xOffset + shadowOffset, num + (float)yOffset + (float)(fontSize * num2) + shadowOffset, labelWidth, labelHeight), text, style);
				GUI.color = fontColor;
				GUI.Label(new Rect(x + (float)xOffset, num + (float)yOffset + (float)(fontSize * num2), labelWidth, labelHeight), text, style);
			}
		}
	}

	private void checkDPI()
	{
		if (useDPI && !Mathf.Approximately(dpi, TouchManager.Instance.DPI))
		{
			update();
		}
	}

	private void update()
	{
		if (useDPI)
		{
			dpi = TouchManager.Instance.DPI;
			textureDPI = (float)texture.width * 2.54f / touchSize;
			scale = dpi / textureDPI;
			textureWidth = (int)((float)texture.width * scale);
			textureHeight = (int)((float)texture.height * scale);
			computeConsts();
		}
		else
		{
			textureWidth = 32;
			textureHeight = 32;
			scale = 0.25f;
			computeConsts();
		}
	}

	private void computeConsts()
	{
		halfTextureWidth = textureWidth / 2;
		halfTextureHeight = textureHeight / 2;
		xOffset = (int)((float)textureWidth * 0.35f);
		yOffset = (int)((float)textureHeight * 0.35f);
		fontSize = (int)(32f * scale);
		shadowOffset = 2f * scale;
		labelWidth = 20 * fontSize;
		labelHeight = 2 * fontSize;
	}

	private void updateDummy(ITouch dummy)
	{
		dummies[dummy.Id] = dummy;
	}

	private void touchesBeganHandler(object sender, TouchEventArgs e)
	{
		foreach (ITouch touch in e.Touches)
		{
			dummies.Add(touch.Id, touch);
			if (touch.Tags.Count > 0)
			{
				tags.Add(touch.Id, touch.Tags.ToString());
			}
		}
	}

	private void touchesMovedHandler(object sender, TouchEventArgs e)
	{
		foreach (ITouch touch in e.Touches)
		{
			if (!dummies.TryGetValue(touch.Id, out var _))
			{
				break;
			}
			updateDummy(touch);
		}
	}

	private void touchesEndedHandler(object sender, TouchEventArgs e)
	{
		foreach (ITouch touch in e.Touches)
		{
			if (!dummies.TryGetValue(touch.Id, out var _))
			{
				break;
			}
			dummies.Remove(touch.Id);
		}
	}

	private void touchesCancelledHandler(object sender, TouchEventArgs e)
	{
		touchesEndedHandler(sender, e);
	}
}
