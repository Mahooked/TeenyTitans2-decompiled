using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace TouchScript;

[Serializable]
public sealed class Tags
{
	public const string INPUT_TOUCH = "Touch";

	public const string INPUT_MOUSE = "Mouse";

	public const string INPUT_PEN = "Pen";

	public const string INPUT_OBJECT = "Object";

	public const string SOURCE_WINDOWS = "Windows";

	public const string SOURCE_TUIO = "TUIO";

	[SerializeField]
	private List<string> tagList = new List<string>();

	public ICollection<string> TagList => new ReadOnlyCollection<string>(tagList);

	public int Count => tagList.Count;

	public Tags(Tags tags)
		: this()
	{
		if (tags == null)
		{
			return;
		}
		foreach (string tag in tags.tagList)
		{
			AddTag(tag);
		}
	}

	public Tags(IEnumerable<string> tags)
		: this()
	{
		foreach (string tag in tags)
		{
			AddTag(tag);
		}
	}

	public Tags(string tag)
		: this()
	{
		tagList.Add(tag);
	}

	public Tags()
	{
	}

	public void AddTag(string tag)
	{
		if (!string.IsNullOrEmpty(tag) && !tagList.Contains(tag))
		{
			tagList.Add(tag);
		}
	}

	public void RemoveTag(string tag)
	{
		tagList.Remove(tag);
	}

	public bool HasTag(string tag)
	{
		return tagList.Contains(tag);
	}

	public override string ToString()
	{
		return string.Join(", ", tagList.ToArray());
	}
}
