public class AEControllerTk2d : AEController
{
	public tk2dSpriteCollectionData spriteCollection
	{
		get
		{
			return animatorTk2d.spriteCollection;
		}
		set
		{
			if (!(value == animatorTk2d.spriteCollection))
			{
				animatorTk2d.spriteCollection = value;
			}
		}
	}

	public AEAnimatorTk2d animatorTk2d
	{
		get
		{
			return animator as AEAnimatorTk2d;
		}
	}
}
