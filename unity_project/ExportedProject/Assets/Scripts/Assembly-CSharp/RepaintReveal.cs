using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RepaintReveal : MonoBehaviour
{
	public Transform figureHolder;

	public RepaintColorSlotGUI colorSlotGUI;

	public TextMeshPro header;

	public TextMeshPro message;

	public Color normalHeaderColor;

	public Color rareHeaderColor;

	public Vector3 figureRotationSpeed;

	private ShopFigureModel figureModel;

	private CharacterDefinition _charDef;

	private RepaintColorSlot _colorSlot;

	public CharacterDefinition charDef
	{
		get
		{
			return _charDef;
		}
		set
		{
			Init(value, _colorSlot);
		}
	}

	public RepaintColorSlot colorSlot
	{
		get
		{
			return _colorSlot;
		}
		set
		{
			Init(_charDef, value);
		}
	}

	private void Update()
	{
		if (figureModel != null)
		{
			figureModel.transform.Rotate(figureRotationSpeed * Time.deltaTime, Space.Self);
		}
	}

	public void Init(CharacterDefinition charDef, RepaintColorSlot colorSlot)
	{
		if (charDef != _charDef)
		{
			if (figureModel != null)
			{
				Object.Destroy(figureModel.gameObject);
				figureModel = null;
			}
			_charDef = charDef;
			if (_charDef != null)
			{
				ShopFigureModel shopFigureModel = Resources.Load<ShopFigureModel>(_charDef.shopFigurePath);
				if (shopFigureModel != null)
				{
					figureModel = Object.Instantiate(shopFigureModel);
					figureModel.transform.SetParent(figureHolder, true);
					figureModel.transform.localPosition = Vector3.zero;
					figureModel.transform.localRotation = Quaternion.Euler(0f, -45f, 0f);
					figureModel.transform.localScale = Vector3.one;
					figureModel.renderer.gameObject.layer = LayerMask.NameToLayer("UI");
					if (figureModel.faceRenderer != null)
					{
						figureModel.faceRenderer.gameObject.layer = LayerMask.NameToLayer("UI");
					}
					if (figureModel.baseRenderer != null)
					{
						figureModel.baseRenderer.gameObject.layer = LayerMask.NameToLayer("UI");
					}
				}
			}
		}
		if (colorSlot == _colorSlot)
		{
			return;
		}
		_colorSlot = colorSlot;
		colorSlotGUI.Init(_colorSlot);
		if (_colorSlot == null)
		{
			return;
		}
		if (_colorSlot.rare)
		{
			header.text = Language.Get("REPAINT_REVEAL_HEADER_SURPRISE_LE", "MENUS,GUI");
			header.faceColor = rareHeaderColor;
		}
		else
		{
			header.text = Language.Get("REPAINT_REVEAL_HEADER_SURPRISE", "MENUS,GUI");
			header.faceColor = normalHeaderColor;
		}
		if (SceneManager.GetActiveScene().name == "RepaintShop")
		{
			if (Player.instance.activeParty.FindIndex((Character x) => x.characterDefinition.uid == charDef.uid) > -1)
			{
				message.text = Language.Get("REPAINT_REVEAL_MESSAGE_CHOOSE_FIGURE", "MENUS,GUI");
			}
			else
			{
				message.text = string.Format(Language.Get("REPAINT_REVEAL_MESSAGE_VISIT_TO_APPLY_2", "MENUS,GUI"), "<sprite name=\"icon_notifier_titanTower\">", "<sprite name=\"icon_notifier_rainbowRaider\">");
			}
		}
		else
		{
			message.text = string.Format(Language.Get("REPAINT_REVEAL_MESSAGE_VISIT_TO_APPLY_1", "MENUS,GUI"), "<sprite name=\"icon_notifier_rainbowRaider\">", "<sprite name=\"icon_notifier_titanTower\">");
		}
		if (figureModel != null)
		{
			Texture texture = Resources.Load<Texture>(_colorSlot.texturePathScaled);
			if (texture != null)
			{
				figureModel.UpdateTexture(texture);
			}
		}
	}
}
