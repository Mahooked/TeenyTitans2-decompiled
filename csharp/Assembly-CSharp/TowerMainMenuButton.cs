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
		tk2dSprite tk2dSprite2 = backingSprite;
		Color color = ((!enabled) ? Color.gray : Color.white);
		textMesh.color = color;
		color = color;
		iconSprite.color = color;
		tk2dSprite2.color = color;
	}
}
