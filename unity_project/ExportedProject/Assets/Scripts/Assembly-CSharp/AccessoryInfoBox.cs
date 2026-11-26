using TMPro;
using UnityEngine;

public class AccessoryInfoBox : MonoBehaviour
{
	public TextMeshPro nameText;

	public TextMeshPro descriptionText;

	public void Init(AccessoryItemDefinition accessoryItemDef)
	{
		nameText.text = ((!(accessoryItemDef != null)) ? string.Empty : Language.Get(accessoryItemDef.itemNameKey, "ABILS,CHIPS,ACHVS,LDBS"));
		descriptionText.text = ((!(accessoryItemDef != null)) ? string.Empty : TTGParseHelper.GetAccessoryDescription(accessoryItemDef));
	}
}
