using UnityEngine;

public class QuestItemDefinition : ItemDefinition
{
	[SerializeField]
	private string _cityPrefabPath;

	public string cityPrefabPath => _cityPrefabPath;
}
