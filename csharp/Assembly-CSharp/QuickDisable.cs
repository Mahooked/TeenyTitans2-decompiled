using UnityEngine;

public class QuickDisable : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dBaseSprite sprite;

	private void Awake()
	{
		if (sprite == null)
		{
			sprite = GetComponentInChildren<tk2dBaseSprite>();
		}
		uiItem.enabled = false;
		if (sprite != null)
		{
			Color color = sprite.color;
			color.a = 0.5f;
			sprite.color = color;
		}
	}

	private void Reset()
	{
		uiItem = GetComponent<tk2dUIItem>();
		sprite = GetComponentInChildren<tk2dBaseSprite>();
		if (uiItem == null)
		{
			Object.DestroyImmediate(this);
		}
	}
}
