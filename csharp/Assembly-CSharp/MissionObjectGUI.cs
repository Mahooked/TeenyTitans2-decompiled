using System.Collections;
using UnityEngine;

public class MissionObjectGUI : MonoBehaviour
{
	public Animator animator;

	public tk2dSprite sprite;

	public IEnumerator Show(ItemDefinition itemDef, int count = 1)
	{
		gameObject.SetActive(value: true);
		sprite.SetSprite(itemDef.iconSprite);
		animator.Play("MissionObjectItem_intro", 0, 0f);
		animator.Update(0f);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_questitem_appear", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitWhile(() => animator.NormalizedTime() < 0.8f);
		yield return new WaitForTap();
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_questitem_touch", GFAudioManager.SFX_GROUP, string.Empty);
		animator.Play("MissionObjectItem_Outro", 0, 0f);
		animator.Update(0f);
		yield return new WaitWhile(() => animator.NormalizedTime() < 0.9f);
		Player.instance.AddItem(itemDef, count);
		gameObject.SetActive(value: false);
	}
}
