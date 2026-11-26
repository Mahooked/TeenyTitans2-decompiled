using System;
using UnityEngine;

[Serializable]
public class ConvoButtonConfig
{
	public delegate void CustomButtonCallback();

	public ConvoButtonType type;

	public string customBackingSprite;

	public LocalizedString customLabel;

	public Color customLabelOutlineColor;

	public CustomButtonCallback buttonCallback;

	public int shopUID;

	public CurrencyType currencyType;

	public int cost;

	public TournamentDefinition tournament;

	public ConvoButtonConfig()
	{
	}

	public ConvoButtonConfig(ConvoButtonType type)
	{
		this.type = type;
	}
}
