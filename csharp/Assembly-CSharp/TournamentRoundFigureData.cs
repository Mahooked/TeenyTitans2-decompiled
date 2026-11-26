using System;
using System.Collections.Generic;

[Serializable]
public class TournamentRoundFigureData
{
	public int characterUID = -1;

	public int level = 1;

	public List<int> abilityUIDs;

	public List<int> selectedRewards;

	public int hitPoints;

	public int attack;

	public int speed;

	public int luck;

	public int dodge;

	public int shinyRank;

	public int modChipUID = -1;

	public int repaintUID = -1;

	public TournamentRoundFigureData()
	{
		abilityUIDs = new List<int>();
		selectedRewards = new List<int>();
	}

	public TournamentRoundFigureData Copy()
	{
		TournamentRoundFigureData tournamentRoundFigureData = new TournamentRoundFigureData();
		tournamentRoundFigureData.characterUID = characterUID;
		tournamentRoundFigureData.level = level;
		tournamentRoundFigureData.abilityUIDs = new List<int>(abilityUIDs);
		tournamentRoundFigureData.selectedRewards = new List<int>(selectedRewards);
		tournamentRoundFigureData.hitPoints = hitPoints;
		tournamentRoundFigureData.attack = attack;
		tournamentRoundFigureData.speed = speed;
		tournamentRoundFigureData.luck = luck;
		tournamentRoundFigureData.dodge = dodge;
		tournamentRoundFigureData.shinyRank = shinyRank;
		tournamentRoundFigureData.modChipUID = modChipUID;
		tournamentRoundFigureData.repaintUID = repaintUID;
		return tournamentRoundFigureData;
	}

	public override string ToString()
	{
		return "Name: " + GameManager.instance.characterDatabase.GetCharacter(characterUID).characterName + "\nLevel: " + level + "\nHit Points: " + hitPoints + "\nAttack: " + attack + "\nSpeed: " + speed + "\nLuck: " + luck + "\nDodge: " + dodge + "\nShiny Rank: " + shinyRank + "\nMod Chip UID: " + modChipUID + "\nRepaint UID: " + repaintUID + "\n";
	}
}
