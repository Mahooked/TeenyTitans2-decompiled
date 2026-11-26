using UnityEngine;

public class tk2dWaitForOnClick : CustomYieldInstruction
{
	private tk2dUIItem uiItem;

	private bool didClick;

	public override bool keepWaiting => !didClick;

	public tk2dWaitForOnClick(tk2dUIItem uiItem)
	{
		this.uiItem = uiItem;
		this.uiItem.OnClick += UiItem_OnClick;
	}

	private void UiItem_OnClick()
	{
		uiItem.OnClick -= UiItem_OnClick;
		didClick = true;
	}
}
