using System;
using System.Collections.Generic;

[Serializable]
public class ChoiceList
{
	public List<Choice> choices;

	public ChoiceList()
	{
		choices = new List<Choice>
		{
			new Choice(),
			new Choice(),
			new Choice()
		};
	}
}
