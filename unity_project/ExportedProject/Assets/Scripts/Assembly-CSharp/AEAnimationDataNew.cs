using System.Collections.Generic;

public class AEAnimationDataNew
{
	public AECompositionNew composition;

	public List<AECompositionNew> usedCompositions = new List<AECompositionNew>();

	public float duration;

	public int totalFrames;

	public float frameDuration;

	public void AddComposition(AECompositionNew comp)
	{
		usedCompositions.Add(comp);
	}

	public AECompositionNew GetCompositionById(int id)
	{
		foreach (AECompositionNew usedComposition in usedCompositions)
		{
			if (usedComposition.id == id)
			{
				return usedComposition;
			}
		}
		return null;
	}
}
