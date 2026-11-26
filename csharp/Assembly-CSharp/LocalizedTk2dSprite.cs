using System.Collections;
using UnityEngine;

public class LocalizedTk2dSprite : MonoBehaviour
{
	public void UpdateSpriteCollection()
	{
		tk2dSpriteCollectionData newCollection = Resources.Load("SpriteCollections/" + Language.CurrentLanguage().ToString() + "_SpriteCollection", typeof(tk2dSpriteCollectionData)) as tk2dSpriteCollectionData;
		tk2dBaseSprite component = GetComponent<tk2dBaseSprite>();
		Debug.Log(component.CurrentSprite.name);
		component.SetSprite(newCollection, component.CurrentSprite.name);
	}

	private IEnumerator Start()
	{
		yield return new WaitForEndOfFrame();
		UpdateSpriteCollection();
		yield return new WaitForEndOfFrame();
	}
}
