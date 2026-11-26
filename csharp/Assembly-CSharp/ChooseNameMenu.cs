using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseNameMenu : MonoBehaviour
{
	public TextMeshPro selectedName;

	public tk2dUIItem randomizeNameBtn;

	public TextMeshPro nameOptionPrototype;

	public float nameSpacing;

	[Header("Sound")]
	public AudioClip diceRollSFX;

	[Header("Name Part 1 Selector")]
	public TextMeshPro selectedNamePart1;

	public tk2dUIItem nameDropdownPart1;

	public tk2dUIScrollableArea part1ScrollableArea;

	[Header("Name Part 2 Selector")]
	public TextMeshPro selectedNamePart2;

	public tk2dUIItem nameDropdownPart2;

	public tk2dUIScrollableArea part2ScrollableArea;

	[Header("Name Part 3 Selector")]
	public TextMeshPro selectedNamePart3;

	public tk2dUIItem nameDropdownPart3;

	public tk2dUIScrollableArea part3ScrollableArea;

	private List<tk2dUIItem> part1NameUIItems;

	private List<tk2dUIItem> part2NameUIItems;

	private List<tk2dUIItem> part3NameUIItems;

	private bool part1ScrollableAreaToggle;

	private bool part2ScrollableAreaToggle;

	private bool part3ScrollableAreaToggle;

	private Vector2 part1ScrollAreaMinMaxY;

	private Vector2 part2ScrollAreaMinMaxY;

	private Vector2 part3ScrollAreaMinMaxY;

	private tk2dSprite saveBtnSprite;

	private TextMeshPro saveBtnTextMesh;

	private bool prepared;

	public event Action<string> NameSelected;

	public void Init()
	{
		Bounds bounds = part1ScrollableArea.GetComponent<Collider2D>().bounds;
		Bounds bounds2 = part2ScrollableArea.GetComponent<Collider2D>().bounds;
		Bounds bounds3 = part3ScrollableArea.GetComponent<Collider2D>().bounds;
		part1ScrollAreaMinMaxY.Set(bounds.min.y, bounds.max.y);
		part2ScrollAreaMinMaxY.Set(bounds2.min.y, bounds2.max.y);
		part3ScrollAreaMinMaxY.Set(bounds3.min.y, bounds3.max.y);
		part1ScrollableArea.gameObject.SetActive(value: false);
		part2ScrollableArea.gameObject.SetActive(value: false);
		part3ScrollableArea.gameObject.SetActive(value: false);
		selectedNamePart1.text = string.Empty;
		selectedNamePart2.text = string.Empty;
		selectedNamePart3.text = string.Empty;
		nameDropdownPart1.OnClick += HandleDropDownPart1Clicked;
		nameDropdownPart2.OnClick += HandleDropDownPart2Clicked;
		nameDropdownPart3.OnClick += HandleDropDownPart3Clicked;
		randomizeNameBtn.OnClick += HandleRandomizeNameBtnClicked;
		LegionOfDoomController.instance.saveNameBtn.OnClick += HandleSaveNameBtnClicked;
		saveBtnSprite = LegionOfDoomController.instance.saveNameBtn.GetComponentInChildren<tk2dSprite>();
		saveBtnTextMesh = LegionOfDoomController.instance.saveNameBtn.GetComponentInChildren<TextMeshPro>();
		int count = part1NameUIItems.Count;
		for (int i = 0; i < count; i++)
		{
			part1NameUIItems[i].OnClickUIItem += HandlePart1NameClicked;
		}
		count = part2NameUIItems.Count;
		for (int j = 0; j < count; j++)
		{
			part2NameUIItems[j].OnClickUIItem += HandlePart2NameClicked;
		}
		count = part3NameUIItems.Count;
		for (int k = 0; k < count; k++)
		{
			part3NameUIItems[k].OnClickUIItem += HandlePart3NameClicked;
		}
		part1ScrollableArea.OnScroll += HandlePart1Scrolling;
		part2ScrollableArea.OnScroll += HandlePart2Scrolling;
		part3ScrollableArea.OnScroll += HandlePart3Scrolling;
		RefreshSelectedName();
	}

	private void OnDisable()
	{
		nameDropdownPart1.OnClick -= HandleDropDownPart1Clicked;
		nameDropdownPart2.OnClick -= HandleDropDownPart2Clicked;
		nameDropdownPart3.OnClick -= HandleDropDownPart3Clicked;
		randomizeNameBtn.OnClick -= HandleRandomizeNameBtnClicked;
		int count = part1NameUIItems.Count;
		for (int i = 0; i < count; i++)
		{
			part1NameUIItems[i].OnClickUIItem -= HandlePart1NameClicked;
		}
		count = part2NameUIItems.Count;
		for (int j = 0; j < count; j++)
		{
			part2NameUIItems[j].OnClickUIItem -= HandlePart2NameClicked;
		}
		count = part3NameUIItems.Count;
		for (int k = 0; k < count; k++)
		{
			part3NameUIItems[k].OnClickUIItem -= HandlePart3NameClicked;
		}
		part1ScrollableArea.OnScroll -= HandlePart1Scrolling;
		part2ScrollableArea.OnScroll -= HandlePart2Scrolling;
		part3ScrollableArea.OnScroll -= HandlePart3Scrolling;
	}

	private void HandleRandomizeNameBtnClicked()
	{
		List<string> namePart1Options = GameManager.instance.legionOfDoomSettings.namePart1Options;
		selectedNamePart1.text = namePart1Options[UnityEngine.Random.Range(0, namePart1Options.Count)];
		List<string> namePart2Options = GameManager.instance.legionOfDoomSettings.namePart2Options;
		selectedNamePart2.text = namePart2Options[UnityEngine.Random.Range(0, namePart2Options.Count)];
		List<string> namePart3Options = GameManager.instance.legionOfDoomSettings.namePart3Options;
		selectedNamePart3.text = namePart3Options[UnityEngine.Random.Range(0, namePart3Options.Count)];
		GFAudioManager.instance.PlayClip(diceRollSFX, 1f, loop: false, 0f, 128, GFAudioManager.SFX_GROUP);
		RefreshSelectedName();
	}

	private void HandlePart1NameClicked(tk2dUIItem uiItem)
	{
		TextMeshPro component = uiItem.GetComponent<TextMeshPro>();
		selectedNamePart1.text = component.text;
		RefreshSelectedName();
	}

	private void HandlePart2NameClicked(tk2dUIItem uiItem)
	{
		TextMeshPro component = uiItem.GetComponent<TextMeshPro>();
		selectedNamePart2.text = component.text;
		RefreshSelectedName();
	}

	private void HandlePart3NameClicked(tk2dUIItem uiItem)
	{
		TextMeshPro component = uiItem.GetComponent<TextMeshPro>();
		selectedNamePart3.text = component.text;
		RefreshSelectedName();
	}

	private void RefreshSelectedName()
	{
		string text = (string.IsNullOrEmpty(selectedNamePart1.text) ? "???" : selectedNamePart1.text);
		string text2 = (string.IsNullOrEmpty(selectedNamePart2.text) ? "???" : selectedNamePart2.text);
		string text3 = (string.IsNullOrEmpty(selectedNamePart3.text) ? "???" : selectedNamePart3.text);
		if (string.IsNullOrEmpty(selectedNamePart1.text) || string.IsNullOrEmpty(selectedNamePart2.text) || string.IsNullOrEmpty(selectedNamePart3.text))
		{
			LegionOfDoomController.instance.saveNameBtn.enabled = false;
			saveBtnSprite.color = Color.gray;
			saveBtnTextMesh.color = Color.gray;
		}
		else
		{
			LegionOfDoomController.instance.saveNameBtn.enabled = true;
			saveBtnSprite.color = Color.white;
			saveBtnTextMesh.color = Color.white;
		}
		selectedName.text = text + text2 + text3;
	}

	private void HandleDropDownPart1Clicked()
	{
		part1ScrollableAreaToggle = !part1ScrollableAreaToggle;
		part1ScrollableArea.gameObject.SetActive(part1ScrollableAreaToggle);
		if (part1ScrollableAreaToggle)
		{
			HandlePart1Scrolling(part1ScrollableArea);
		}
	}

	private void HandleDropDownPart2Clicked()
	{
		part2ScrollableAreaToggle = !part2ScrollableAreaToggle;
		part2ScrollableArea.gameObject.SetActive(part2ScrollableAreaToggle);
		if (part2ScrollableAreaToggle)
		{
			HandlePart2Scrolling(part2ScrollableArea);
		}
	}

	private void HandleDropDownPart3Clicked()
	{
		part3ScrollableAreaToggle = !part3ScrollableAreaToggle;
		part3ScrollableArea.gameObject.SetActive(part3ScrollableAreaToggle);
		if (part3ScrollableAreaToggle)
		{
			HandlePart3Scrolling(part3ScrollableArea);
		}
	}

	private void HandleSaveNameBtnClicked()
	{
		if (!string.IsNullOrEmpty(selectedNamePart1.text) && !string.IsNullOrEmpty(selectedNamePart2.text) && !string.IsNullOrEmpty(selectedNamePart3.text) && NameSelected != null)
		{
			NameSelected(selectedName.text);
		}
	}

	private void HandlePart1Scrolling(tk2dUIScrollableArea tk2dUiScrollableArea)
	{
		int count = part1NameUIItems.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dUIItem tk2dUIItem2 = part1NameUIItems[i];
			float y = tk2dUIItem2.transform.position.y;
			tk2dUIItem2.GetComponent<Collider2D>().enabled = y >= part1ScrollAreaMinMaxY.x && y <= part1ScrollAreaMinMaxY.y;
		}
	}

	private void HandlePart2Scrolling(tk2dUIScrollableArea tk2dUiScrollableArea)
	{
		int count = part2NameUIItems.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dUIItem tk2dUIItem2 = part2NameUIItems[i];
			float y = tk2dUIItem2.transform.position.y;
			tk2dUIItem2.GetComponent<Collider2D>().enabled = y >= part2ScrollAreaMinMaxY.x && y <= part2ScrollAreaMinMaxY.y;
		}
	}

	private void HandlePart3Scrolling(tk2dUIScrollableArea tk2dUiScrollableArea)
	{
		int count = part3NameUIItems.Count;
		for (int i = 0; i < count; i++)
		{
			tk2dUIItem tk2dUIItem2 = part3NameUIItems[i];
			float y = tk2dUIItem2.transform.position.y;
			tk2dUIItem2.GetComponent<Collider2D>().enabled = y >= part3ScrollAreaMinMaxY.x && y <= part3ScrollAreaMinMaxY.y;
		}
	}

	public void PrepareMenu()
	{
		if (!prepared)
		{
			prepared = true;
			part1NameUIItems = new List<tk2dUIItem>();
			part2NameUIItems = new List<tk2dUIItem>();
			part3NameUIItems = new List<tk2dUIItem>();
			int count = GameManager.instance.legionOfDoomSettings.namePart1Options.Count;
			for (int i = 0; i < count; i++)
			{
				TextMeshPro textMeshPro = UnityEngine.Object.Instantiate(nameOptionPrototype);
				textMeshPro.transform.SetParent(part1ScrollableArea.contentContainer.transform, worldPositionStays: false);
				textMeshPro.transform.localPosition = new Vector3(0f, (0f - nameSpacing) * (float)i, nameOptionPrototype.transform.localPosition.z);
				textMeshPro.text = GameManager.instance.legionOfDoomSettings.namePart1Options[i];
				textMeshPro.gameObject.name = textMeshPro.text;
				part1NameUIItems.Add(textMeshPro.GetComponent<tk2dUIItem>());
			}
			part1ScrollableArea.ContentLength = (float)count * nameSpacing;
			count = GameManager.instance.legionOfDoomSettings.namePart2Options.Count;
			for (int j = 0; j < count; j++)
			{
				TextMeshPro textMeshPro2 = UnityEngine.Object.Instantiate(nameOptionPrototype);
				textMeshPro2.transform.SetParent(part2ScrollableArea.contentContainer.transform, worldPositionStays: false);
				textMeshPro2.transform.localPosition = new Vector3(0f, (0f - nameSpacing) * (float)j, nameOptionPrototype.transform.localPosition.z);
				textMeshPro2.text = GameManager.instance.legionOfDoomSettings.namePart2Options[j];
				part2NameUIItems.Add(textMeshPro2.GetComponent<tk2dUIItem>());
			}
			part2ScrollableArea.ContentLength = (float)count * nameSpacing;
			count = GameManager.instance.legionOfDoomSettings.namePart3Options.Count;
			for (int k = 0; k < count; k++)
			{
				TextMeshPro textMeshPro3 = UnityEngine.Object.Instantiate(nameOptionPrototype);
				textMeshPro3.transform.SetParent(part3ScrollableArea.contentContainer.transform, worldPositionStays: false);
				textMeshPro3.transform.localPosition = new Vector3(0f, (0f - nameSpacing) * (float)k, nameOptionPrototype.transform.localPosition.z);
				textMeshPro3.text = GameManager.instance.legionOfDoomSettings.namePart3Options[k];
				part3NameUIItems.Add(textMeshPro3.GetComponent<tk2dUIItem>());
			}
			part3ScrollableArea.ContentLength = (float)count * nameSpacing;
			nameOptionPrototype.gameObject.SetActive(value: false);
		}
	}
}
