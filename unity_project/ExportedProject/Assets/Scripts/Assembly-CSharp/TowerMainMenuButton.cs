using TMPro;
using UnityEngine;

public class TowerMainMenuButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dSprite backingSprite;

	public tk2dSprite iconSprite;

	public TextMeshPro textMesh;

	public void SetButtonEnabled(bool enabled)
	{
		uiItem.enabled = enabled;
		tk2dSprite obj = backingSprite;
		Color color = ((!enabled) ? Color.gray : Color.white);
		textMesh.color = color;
		color = color;
		iconSprite.color = color;
		obj.color = color;
	}
}
