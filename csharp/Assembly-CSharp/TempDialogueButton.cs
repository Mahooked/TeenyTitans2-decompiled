using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TempDialogueButton : MonoBehaviour
{
	private static Dictionary<ConvoButtonType, string> backingSprites = new Dictionary<ConvoButtonType, string>
	{
		{
			ConvoButtonType.Battle,
			"TTG_gui_button_battle"
		},
		{
			ConvoButtonType.Buy,
			"TTG_gui_button_buyicecream"
		},
		{
			ConvoButtonType.Chat,
			"TTG_gui_button_chat"
		},
		{
			ConvoButtonType.Shop,
			"TTG_gui_button_shop_stickyjoe"
		},
		{
			ConvoButtonType.Tournament,
			"TTG_gui_button_tourney"
		},
		{
			ConvoButtonType.Travel,
			"TTG_gui_button_travel"
		},
		{
			ConvoButtonType.Repaint,
			"TTG_gui_button_repaintshop"
		},
		{
			ConvoButtonType.Custom,
			string.Empty
		},
		{
			ConvoButtonType.LOD,
			"TT2_gui_button_chat_LOD"
		}
	};

	private static Dictionary<ConvoButtonType, Color> textOutlineColors = new Dictionary<ConvoButtonType, Color>
	{
		{
			ConvoButtonType.Battle,
			new Color32(175, 75, 124, byte.MaxValue)
		},
		{
			ConvoButtonType.Buy,
			new Color32(0, 0, 0, byte.MaxValue)
		},
		{
			ConvoButtonType.Chat,
			new Color32(81, 17, 108, byte.MaxValue)
		},
		{
			ConvoButtonType.Shop,
			new Color32(83, 92, 59, byte.MaxValue)
		},
		{
			ConvoButtonType.Tournament,
			new Color32(0, 0, 0, byte.MaxValue)
		},
		{
			ConvoButtonType.Travel,
			new Color32(27, 27, 27, byte.MaxValue)
		},
		{
			ConvoButtonType.Repaint,
			new Color32(27, 27, 27, byte.MaxValue)
		},
		{
			ConvoButtonType.Custom,
			new Color32(27, 27, 27, byte.MaxValue)
		},
		{
			ConvoButtonType.LOD,
			new Color32(27, 27, 27, byte.MaxValue)
		}
	};

	private static Dictionary<CityName, string> cityBackingSprites = new Dictionary<CityName, string>
	{
		{
			CityName.JumpCity,
			"TT2_gui_button_spacecabbie_jumpcity"
		},
		{
			CityName.Gotham,
			"TT2_gui_button_spacecabbie_gotham"
		},
		{
			CityName.Metropolis,
			"TT2_gui_button_spacecabbie_metropolis"
		}
	};

	public tk2dUIItem uiItem;

	public tk2dSprite backSprite;

	public TextMeshPro textMesh;

	public tk2dSprite missionStarSprite;

	public tk2dSprite skullSprite;

	public TextMeshPro levelText;

	private bool _isEnabled = true;

	private ConvoButtonConfig _config;

	private CityName _cityName = CityName.Any;

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			if (value)
			{
				Enable();
			}
			else
			{
				Disable();
			}
		}
	}

	public ConvoButtonConfig config
	{
		get
		{
			return _config;
		}
		set
		{
			Init(value, _cityName);
		}
	}

	public CityName cityName
	{
		get
		{
			return _cityName;
		}
		set
		{
			Init(_config, value);
		}
	}

	private void Awake()
	{
		Refresh();
	}

	public void Init(ConvoButtonConfig config, CityName cityName = CityName.Any)
	{
		_config = config;
		_cityName = cityName;
		Refresh();
	}

	private void Refresh()
	{
		if (_config == null)
		{
			return;
		}
		textMesh.gameObject.SetActive(value: false);
		missionStarSprite.gameObject.SetActive(value: false);
		skullSprite.gameObject.SetActive(value: false);
		if (config.type == ConvoButtonType.Custom)
		{
			backSprite.SetSprite(config.customBackingSprite);
			textMesh.outlineColor = config.customLabelOutlineColor;
		}
		else
		{
			backSprite.SetSprite(backingSprites[config.type]);
			textMesh.outlineColor = textOutlineColors[config.type];
		}
		switch (_config.type)
		{
		case ConvoButtonType.Chat:
			textMesh.gameObject.SetActive(value: true);
			textMesh.text = Language.Get("CHAT", "MENUS,GUI");
			break;
		case ConvoButtonType.Battle:
			textMesh.gameObject.SetActive(value: true);
			textMesh.text = Language.Get("BATTLE", "MENUS,GUI");
			break;
		case ConvoButtonType.Shop:
			textMesh.gameObject.SetActive(value: true);
			textMesh.text = Language.Get("SHOP", "MENUS,GUI");
			break;
		case ConvoButtonType.Travel:
			textMesh.gameObject.SetActive(value: true);
			if (_cityName != CityName.Any)
			{
				backSprite.SetSprite(cityBackingSprites[_cityName]);
				textMesh.text = $"{GameManager.instance.gameSettings.spaceCabbieTravelCost}<sprite index=0 tint=1>";
			}
			else
			{
				textMesh.text = Language.Get("TRAVEL", "MENUS,GUI");
			}
			break;
		case ConvoButtonType.Buy:
			break;
		case ConvoButtonType.Tournament:
			break;
		case ConvoButtonType.Repaint:
			textMesh.gameObject.SetActive(value: true);
			textMesh.text = Language.Get("REPAINTS", "MENUS,GUI");
			break;
		case ConvoButtonType.Custom:
			if (!string.IsNullOrEmpty(config.customLabel.text) || !string.IsNullOrEmpty(config.customLabel.key))
			{
				textMesh.gameObject.SetActive(value: true);
				textMesh.text = ((!string.IsNullOrEmpty(config.customLabel.key)) ? Language.Get(config.customLabel.key, "MENUS,GUI") : config.customLabel.text);
			}
			break;
		case ConvoButtonType.LOD:
			textMesh.gameObject.SetActive(value: true);
			textMesh.text = Language.Get("HALLOFDOOM", "MENUS,GUI");
			break;
		}
	}

	public void Enable()
	{
		if (!_isEnabled)
		{
			backSprite.color = Color.white;
			textMesh.color = Color.white;
			TextMeshPro textMeshPro = textMesh;
			Color color;
			if (_config == null)
			{
				color = Color.white;
			}
			else
			{
				color = ((_config.type != ConvoButtonType.Custom) ? textOutlineColors[_config.type] : _config.customLabelOutlineColor);
			}
			textMeshPro.outlineColor = color;
			missionStarSprite.color = Color.white;
			skullSprite.color = Color.white;
			levelText.color = Color.white;
			uiItem.enabled = true;
			_isEnabled = true;
		}
	}

	public void Disable()
	{
		if (_isEnabled)
		{
			backSprite.color = new Color(0.6f, 0.6f, 0.6f);
			textMesh.color = new Color(0.6f, 0.6f, 0.6f);
			Color color;
			if (_config == null)
			{
				color = new Color(0.6f, 0.6f, 0.6f);
			}
			else
			{
				color = ((_config.type != ConvoButtonType.Custom) ? (textOutlineColors[_config.type] * 0.5f) : _config.customLabelOutlineColor);
			}
			color.a = 1f;
			textMesh.outlineColor = color;
			missionStarSprite.color = new Color(0.6f, 0.6f, 0.6f);
			skullSprite.color = new Color(0.6f, 0.6f, 0.6f);
			levelText.color = new Color(0.6f, 0.6f, 0.6f);
			uiItem.enabled = false;
			_isEnabled = false;
		}
	}
}
