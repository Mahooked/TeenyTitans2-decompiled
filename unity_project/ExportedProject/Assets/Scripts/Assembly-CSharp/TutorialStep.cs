using System;

[Serializable]
public class TutorialStep
{
	public string name;

	public bool isPlaying { get; set; }

	public TutorialStep(string name)
	{
		this.name = name;
	}
}
