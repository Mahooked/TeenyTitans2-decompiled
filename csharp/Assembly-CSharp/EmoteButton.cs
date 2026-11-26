using UnityEngine;

public class EmoteButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dSprite emoteSprite;

	public EmoteType currentEmoteType { get; private set; }

	public void Init(EmoteType emoteType, string emoteSpriteName)
	{
		currentEmoteType = emoteType;
		emoteSprite.SetSprite(emoteSpriteName);
	}
}
