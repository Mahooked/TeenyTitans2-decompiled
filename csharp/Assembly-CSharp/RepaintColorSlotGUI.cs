using UnityEngine;

public class RepaintColorSlotGUI : MonoBehaviour
{
	public tk2dSprite backingSprite;

	public tk2dSprite colorSprite;

	private RepaintColorSlot _colorSlot;

	public RepaintColorSlot colorSlot
	{
		get
		{
			return _colorSlot;
		}
		set
		{
			Init(value);
		}
	}

	private void Awake()
	{
		Refresh();
	}

	public void Init(RepaintColorSlot colorSlot)
	{
		_colorSlot = colorSlot;
		Refresh();
	}

	private void Refresh()
	{
		if (_colorSlot == null)
		{
			colorSprite.gameObject.SetActive(value: false);
			backingSprite.SetSprite("gui_reward_repaint_iconbg");
		}
		else
		{
			colorSprite.gameObject.SetActive(value: true);
			colorSprite.SetSprite(_colorSlot.colorSpriteId);
			backingSprite.SetSprite((!_colorSlot.rare) ? "gui_reward_repaint_iconbg" : "gui_reward_repaint_limitededition_iconbg");
		}
	}
}
