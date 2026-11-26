using System.Collections.Generic;
using UnityEngine;

public class EmoteDatabase : ScriptableObject
{
	[SerializeField]
	private int _maxSpamUses = 4;

	[SerializeField]
	private float _spamDelay = 5f;

	public List<EmoteDefinition> emoteDefinitions = new List<EmoteDefinition>();

	public int maxSpamUses => _maxSpamUses;

	public float spamDelay => _spamDelay;
}
