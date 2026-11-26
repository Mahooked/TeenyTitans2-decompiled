using TMPro;
using UnityEngine;

public class SimpleTextButton : MonoBehaviour
{
	public tk2dSprite backing;

	public TextMeshPro textMesh;

	public tk2dUIItem uiItem;

	public Color disabledColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	public Color disabledTextColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	public Color disabledTextOutlineColor = new Color(0.5f, 0.5f, 0.5f, 1f);

	private Color originalBackingColor;

	private Color originalTextColor;

	private Color originalTextOutlineColor;

	private bool isInitialized;

	[SerializeField]
	private bool _isEnabled = true;

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			SetEnabled(value);
		}
	}

	private void Awake()
	{
		Init();
	}

	public void Init()
	{
		if (!isInitialized)
		{
			originalBackingColor = backing.color;
			originalTextColor = textMesh.color;
			originalTextOutlineColor = textMesh.outlineColor;
			Refresh();
			isInitialized = true;
		}
	}

	public void SetEnabled(bool enabled)
	{
		if (!isInitialized)
		{
			Init();
		}
		if (enabled != _isEnabled)
		{
			_isEnabled = enabled;
			Refresh();
		}
	}

	private void Refresh()
	{
		if (_isEnabled)
		{
			uiItem.enabled = true;
			backing.color = originalBackingColor;
			textMesh.color = originalTextColor;
			textMesh.outlineColor = originalTextOutlineColor;
		}
		else
		{
			uiItem.enabled = false;
			backing.color = disabledColor;
			textMesh.color = disabledTextColor;
			textMesh.outlineColor = disabledTextOutlineColor;
		}
	}
}
