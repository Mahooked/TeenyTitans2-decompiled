using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Teen Titans/Tag Collection")]
public class TagCollection : ScriptableObject
{
	[SerializeField]
	private List<TagItem> _tagItems = new List<TagItem>();

	public List<TagItem> tagItems => _tagItems;

	public TagItem GetTagItem(string tagName)
	{
		for (int i = 0; i < _tagItems.Count; i++)
		{
			if (_tagItems[i].tagName.Equals(tagName))
			{
				return _tagItems[i];
			}
		}
		return TagItem.NULL;
	}

	public TagItem GetRandomTagItem()
	{
		return _tagItems[Random.Range(0, _tagItems.Count)];
	}

	public bool HasTag(string tagName)
	{
		for (int i = 0; i < _tagItems.Count; i++)
		{
			if (_tagItems[i].tagName.Equals(tagName))
			{
				return true;
			}
		}
		return false;
	}

	public string GetDisplayTextForTag(string tagName, bool isPlural)
	{
		TagItem tagItem = GetTagItem(tagName);
		if (tagItem != null)
		{
			return (!isPlural) ? tagItem.displayTextSingular : tagItem.displayTextPlural;
		}
		return string.Empty;
	}
}
