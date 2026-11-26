using UnityEngine;

[CreateAssetMenu(menuName = "Teen Titans/Story Mission Paths")]
public class StoryMissionPaths : ScriptableObject
{
	[SerializeField]
	private int[] _gothamPath;

	[SerializeField]
	private int[] _metropolisPath;

	public int[] gothamPath => _gothamPath;

	public int[] metropolisPath => _metropolisPath;
}
