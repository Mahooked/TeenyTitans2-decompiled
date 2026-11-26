using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class PageController2 : PageController
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CharacterDefinition> CharacterDefinitionSelected__BackingField;

	private InitPortaitDelegate2 initPortraitCallback;

	private List<CharacterDefinition> _filteredCharacterDefinitions = new List<CharacterDefinition>();

	public List<CharacterDefinition> filteredCharacterDefinitions
	{
		get
		{
			return _filteredCharacterDefinitions;
		}
	}

	public event Action<CharacterDefinition> CharacterDefinitionSelected
	{
		add
		{
			Action<CharacterDefinition> action = CharacterDefinitionSelected__BackingField;
			Action<CharacterDefinition> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterDefinitionSelected__BackingField, (Action<CharacterDefinition>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CharacterDefinition> action = CharacterDefinitionSelected__BackingField;
			Action<CharacterDefinition> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterDefinitionSelected__BackingField, (Action<CharacterDefinition>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void Init(InitPortaitDelegate2 callback)
	{
		initPortraitCallback = callback;
	}

	public void SetList(List<CharacterDefinition> characters)
	{
		if (characters == _filteredCharacterDefinitions)
		{
			UpdatePages();
			return;
		}
		_filteredCharacterDefinitions.Clear();
		_filteredCharacterDefinitions.AddRange(characters);
		maxPageIndex = Mathf.Max(0, Mathf.CeilToInt((float)_filteredCharacterDefinitions.Count / (float)numItemsPerPage) - 1);
		maxScrollPos = pageWidth * (float)maxPageIndex;
		_pageIndex = 0;
		_scrollPos = 0f;
		firstVisible = -1;
		lastVisible = -1;
		prevFirstVisible = -1;
		prevLastVisible = -1;
		while (_pages.Count > 0)
		{
			ReturnPage(_pages.Pop());
		}
		pageDots.numDots = maxPageIndex + 1;
		pageDotGroup.SelectedIndex = 0;
		pageNumberText.text = string.Format("{0}/{1}", _pageIndex + 1, maxPageIndex + 1);
		UpdatePosition();
	}

	protected override void HandleOnRelease()
	{
		isDown = false;
		if (isDragging)
		{
			isDragging = false;
			points.Add(curPosition - prevPosition);
			timestamps.Add(Time.time);
			float num = Time.time - 0.2f;
			Vector3 vector = default(Vector3);
			int num2 = points.Count - 1;
			int num3 = num2;
			while (num3 >= 0 && !(timestamps[num3] < num))
			{
				vector += points[num3];
				num3--;
			}
			if (Mathf.Abs(vector.x) / Screen.dpi >= swipeDistance)
			{
				if (_pageIndex > 0 && vector.x > 0f)
				{
					base.pageIndex--;
				}
				else if (_pageIndex < maxPageIndex && vector.x < 0f)
				{
					base.pageIndex++;
				}
				else
				{
					base.pageIndex = Mathf.Clamp(Mathf.RoundToInt(base.scrollPos / pageWidth), 0, maxPageIndex);
				}
			}
			else
			{
				base.pageIndex = Mathf.Clamp(Mathf.RoundToInt(base.scrollPos / pageWidth), 0, maxPageIndex);
			}
		}
		else if (isDraggingPortrait)
		{
			OnDragFinished(dragPortrait);
			isDraggingPortrait = false;
			downPortrait = null;
			UnityEngine.Object.Destroy(dragPortrait.gameObject);
			dragPortrait = null;
		}
		else if (downPortrait != null && Vector3.Distance(downPosition, Input.mousePosition) / Screen.dpi < 0.2f)
		{
			OnCharacterDefinitionSelected(downPortrait.characterDefinition);
			downPortrait = null;
		}
		points.Clear();
		timestamps.Clear();
	}

	protected override void PopulatePage(Page page, int startIndex, bool animate = false)
	{
		CharacterSelectPortrait characterSelectPortrait = null;
		Vector3 localPosition = topLeftPosition;
		int num = startIndex + numItemsPerPage;
		for (int i = startIndex; i < num; i++)
		{
			int num2 = i % numItemsPerPage;
			characterSelectPortrait = page.items[num2];
			if (i < _filteredCharacterDefinitions.Count)
			{
				if (!characterSelectPortrait.gameObject.activeSelf)
				{
					characterSelectPortrait.gameObject.SetActive(true);
				}
				if (initPortraitCallback != null)
				{
					initPortraitCallback(characterSelectPortrait, _filteredCharacterDefinitions[i]);
				}
				localPosition.x = topLeftPosition.x + positionStep.x * (float)(i % columnsPerPage);
				localPosition.y = topLeftPosition.y + positionStep.y * (float)Mathf.FloorToInt(num2 / columnsPerPage);
				characterSelectPortrait.transform.localPosition = localPosition;
			}
			else
			{
				characterSelectPortrait.gameObject.SetActive(false);
			}
		}
	}

	protected void OnCharacterDefinitionSelected(CharacterDefinition character)
	{
		if (CharacterDefinitionSelected__BackingField != null)
		{
			CharacterDefinitionSelected__BackingField(character);
		}
	}
}
