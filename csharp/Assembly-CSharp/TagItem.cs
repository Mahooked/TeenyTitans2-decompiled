using System;
using UnityEngine;

[Serializable]
public class TagItem
{
	[SerializeField]
	private string _tagName = string.Empty;

	[SerializeField]
	private LocalizedString _displayTextSingular;

	[SerializeField]
	private LocalizedString _displayTextPlural;

	private static TagItem _NULL = new TagItem("null", new LocalizedString("null"), new LocalizedString("null"));

	public string tagName => _tagName;

	public LocalizedString displayTextSingular => _displayTextSingular;

	public LocalizedString displayTextPlural => _displayTextPlural;

	public static TagItem NULL => _NULL;

	public TagItem(string tagName, LocalizedString singular, LocalizedString plural)
	{
		_tagName = tagName;
		_displayTextSingular = singular;
		_displayTextPlural = plural;
	}
}
