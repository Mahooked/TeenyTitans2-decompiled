using UnityEngine;

public class tk2dSpriteForceRebuild : MonoBehaviour
{
	private void OnEnable()
	{
		if (GetComponent<tk2dSprite>() != null)
		{
			GetComponent<tk2dSprite>().ForceBuild();
		}
	}
}
