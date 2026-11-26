using System;
using UnityEngine.Events;

[Serializable]
public class CityEntityInteractConfig
{
	public CityEntityInteractType type;

	public Conversation conversation;

	public int shopUID;

	public ShopInventory shopInventoryOverride;

	public string sceneName;

	public string pathNodeName;

	public UnityEvent unityEvent;

	public TournamentDefinition tournament;

	public PathNode pathNode;

	public string teleportSound;
}
