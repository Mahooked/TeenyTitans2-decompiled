using System;
using DG.Tweening;

[Serializable]
public class CloudsAnimationAdapter
{
	public tk2dAnimationAdapter animationAdapter;

	public Tweener tweener;

	public CloudsAnimationAdapter(tk2dAnimationAdapter newAdapter, Tweener newTweener)
	{
		animationAdapter = newAdapter;
		tweener = newTweener;
	}
}
