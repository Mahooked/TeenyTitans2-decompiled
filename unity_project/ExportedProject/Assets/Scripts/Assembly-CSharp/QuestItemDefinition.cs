using UnityEngine;

public class QuestItemDefinition : ItemDefinition
{
	[SerializeField]
	private string _cityPrefabPath;

	public string cityPrefabPath
	{
		get
		{
			return _cityPrefabPath;
		}
	}
}
