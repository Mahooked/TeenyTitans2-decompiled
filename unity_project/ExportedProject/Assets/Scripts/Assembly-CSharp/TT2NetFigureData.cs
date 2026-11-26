using System;
using System.Collections.Generic;

[Serializable]
public class TT2NetFigureData
{
	public int uid = -1;

	public int level = 1;

	public List<int> abilityUIDs;

	public List<int> selectedRewards;

	public int shinyRank;

	public int modChipUid = -1;

	public int repaintUid = -1;

	public TT2NetFigureData(Character character)
	{
		uid = character.characterDefinition.uid;
		level = character.level;
		abilityUIDs = new List<int>(character.abilityUIDs);
		selectedRewards = new List<int>(character.selectedRewards);
		shinyRank = character.shinyRank;
		modChipUid = ((!(character.modChip != null)) ? (-1) : character.modChip.uid);
		repaintUid = character.repaintColorID;
	}
}
