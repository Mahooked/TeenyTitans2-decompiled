using System;
using System.Collections;
using UnityEngine;

public class TransitionScene : MonoBehaviour
{
	[Serializable]
	public class TransitionSceneData
	{
		public string prevScene;

		public string nextScene;

		public tk2dSpriteCollectionData[] spriteCollectionToUnload;
	}

	public static bool isDone;

	public TransitionSceneData[] transitionData;

	private IEnumerator Start()
	{
		TransitionSceneData data = null;
		int len = transitionData.Length;
		for (int i = 0; i < len; i++)
		{
			data = transitionData[i];
			if (!(LoadingScreenCover.prevScene == data.prevScene) || !(LoadingScreenCover.nextScene == data.nextScene))
			{
				continue;
			}
			Debug.Log(string.Format("Unloading tk2d sprite collection data for transition {0} -> {1}", data.prevScene, data.nextScene));
			int num = data.spriteCollectionToUnload.Length;
			for (int j = 0; j < num; j++)
			{
				if (!(data.spriteCollectionToUnload[j] == null))
				{
					data.spriteCollectionToUnload[j].UnloadTextures();
					data.spriteCollectionToUnload[j].ResetPlatformData();
				}
			}
			break;
		}
		yield return new WaitForEndOfFrame();
		yield return Resources.UnloadUnusedAssets();
		isDone = true;
	}
}
