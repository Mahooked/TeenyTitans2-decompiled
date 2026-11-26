using System;
using UnityEngine;

[Serializable]
public class LocalizedString : IComparable<LocalizedString>
{
	[SerializeField]
	private string _text = string.Empty;

	[SerializeField]
	private string _key = string.Empty;

	public string text => _text;

	public string key => _key;

	public bool IsNullOrEmpty => string.IsNullOrEmpty(_text) && string.IsNullOrEmpty(_key);

	public LocalizedString()
	{
		_text = string.Empty;
		_key = string.Empty;
	}

	public LocalizedString(string text)
	{
		_text = text;
		_key = string.Empty;
	}

	public LocalizedString(string text, string key)
	{
		_text = text;
		_key = key;
	}

	public override string ToString()
	{
		return _text;
	}

	public int CompareTo(LocalizedString other)
	{
		return text.CompareTo(other.text);
	}

	public static implicit operator string(LocalizedString s)
	{
		return (!string.IsNullOrEmpty(s.key)) ? s.key : s.text;
	}

	public LocalizedString Copy()
	{
		return new LocalizedString(_text, _key);
	}
}
