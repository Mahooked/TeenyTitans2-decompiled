using UnityEngine;

public static class AEResources
{
	public static AESpriteTk2d CreateSpriteTk2d()
	{
		return (Object.Instantiate(Resources.Load("AESpriteTk2d")) as GameObject).GetComponent<AESpriteTk2d>();
	}

	public static AEAnimationBuilderSprite CreateAnimationBuilderSprite()
	{
		return (Object.Instantiate(Resources.Load("AEAnimationBuilderSprite")) as GameObject).GetComponent<AEAnimationBuilderSprite>();
	}
}
