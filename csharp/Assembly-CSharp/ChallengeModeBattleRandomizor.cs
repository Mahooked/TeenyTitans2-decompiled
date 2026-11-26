using System.Collections.Generic;
using UnityEngine;

public static class ChallengeModeBattleRandomizor
{
	private static string[] wikiUnlockedCharacters = new string[11]
	{
		"Santa Claus", "Multiverse B'wana Beast", "Red Arrow", "Argyle Trigon", "Artemis", "Blue Beetle", "Bizarro", "Multiverse Jinx", "Killer Croc", "Alfred",
		"Harley Quinn"
	};

	public static BattleDefinition GetBattleDefinitionForEasyMode()
	{
		BattleDefinition battleDefinition = new BattleDefinition(BattleType.Challenge);
		NPCDefinition nPCDefByType = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.EasyDoor);
		battleDefinition.enemyName = ((!string.IsNullOrEmpty(nPCDefByType.nameKey)) ? Language.Get(nPCDefByType.nameKey, "NPCS,FIGS") : nPCDefByType.name);
		battleDefinition.enemyDefeatedMsg = nPCDefByType.name;
		battleDefinition.enemyPortraitSprite = ((!string.IsNullOrEmpty(nPCDefByType.preBattleSpriteRight)) ? nPCDefByType.preBattleSpriteRight : nPCDefByType.preBattleSprite);
		battleDefinition.enemyCharacterName = nPCDefByType.characterType;
		battleDefinition.enemyTeam = GetRandomEnemyCharacters();
		return battleDefinition;
	}

	public static BattleDefinition GetBattleDefinitionForHardMode()
	{
		BattleDefinition battleDefinition = new BattleDefinition(BattleType.Challenge);
		NPCDefinition nPCDefByType = GameManager.instance.npcDatabase.GetNPCDefByType(CharacterName.IntenseDoor);
		battleDefinition.enemyName = ((!string.IsNullOrEmpty(nPCDefByType.nameKey)) ? Language.Get(nPCDefByType.nameKey, "NPCS,FIGS") : nPCDefByType.name);
		battleDefinition.enemyDefeatedMsg = nPCDefByType.name;
		battleDefinition.enemyPortraitSprite = ((!string.IsNullOrEmpty(nPCDefByType.preBattleSpriteRight)) ? nPCDefByType.preBattleSpriteRight : nPCDefByType.preBattleSprite);
		battleDefinition.enemyCharacterName = nPCDefByType.characterType;
		battleDefinition.enemyTeam = GetRandomEnemyCharacters(isHard: true);
		return battleDefinition;
	}

	private static List<Character> GetRandomEnemyCharacters(bool isHard = false)
	{
		int num = 0;
		List<Character> list = new List<Character>();
		ChallengeModeDatabase challengeModeDatabase = GameManager.instance.challengeModeDatabase;
		while (num < 3)
		{
			int count = GameManager.instance.characterDatabase.characters.Count;
			CharacterDefinition characterDefinition = GameManager.instance.characterDatabase.characters[Random.Range(0, count)];
			if (CharacterAllowed(characterDefinition))
			{
				if (isHard)
				{
					Character character = new Character(CharacterRoller.RollCharacterDataChallengeMode(characterDefinition, -1, characterDefinition.uid, 20, hardMode: true));
					character.shinyRank = 3;
					character.hitPointsTempIncrease = Random.Range(challengeModeDatabase.minHardModeHitPointsIncrease, challengeModeDatabase.maxHardModeHitPointsIncrease);
					character.attackTempIncrease = Random.Range(challengeModeDatabase.minHardModeAttackIncrease, challengeModeDatabase.maxHardModeAttackIncrease);
					CharacterData characterData = character.GetCharacterData();
					characterData.dodge = (int)((float)characterData.dodge * 0.7f);
					list.Add(character);
				}
				else
				{
					Character character2 = new Character(CharacterRoller.RollCharacterData(characterDefinition, -1, characterDefinition.uid, Random.Range(challengeModeDatabase.minNormalModeEnemyLvl, challengeModeDatabase.maxNormalModeEnemyLvl)));
					CharacterData characterData2 = character2.GetCharacterData();
					characterData2.dodge = (int)((float)characterData2.dodge * 0.7f);
					list.Add(character2);
				}
				num++;
			}
		}
		return list;
	}

	public static List<Character> GetClassyEnemyCharacters(List<Character> otherTeam, bool isHard = false)
	{
		List<Character> list = new List<Character>();
		List<int> definitionsAdded = new List<int>();
		Character character = null;
		CharacterDefinition characterDefinition = null;
		int count = otherTeam.Count;
		for (int i = 0; i < count; i++)
		{
			character = otherTeam[i];
			characterDefinition = ((character.characterClass == CharacterClass.None) ? GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition def) => def.canUseInBattle && !definitionsAdded.Contains(def.uid) && CharacterAllowed(def)) : (i switch
			{
				0 => GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition def) => def.canUseInBattle && !definitionsAdded.Contains(def.uid) && def.characterClass == AbilityScript.classStrengths[character.characterClass] && CharacterAllowed(def)), 
				1 => GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition def) => def.canUseInBattle && !definitionsAdded.Contains(def.uid) && def.characterClass == AbilityScript.classWeaknesses[character.characterClass] && CharacterAllowed(def)), 
				_ => GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition def) => def.canUseInBattle && !definitionsAdded.Contains(def.uid) && CharacterAllowed(def)), 
			}));
			definitionsAdded.Add(characterDefinition.uid);
			Character character2 = null;
			if (isHard)
			{
				character2 = new Character(CharacterRoller.RollCharacterDataChallengeMode(characterDefinition, -1, characterDefinition.uid, 20, hardMode: true));
				character2.shinyRank = 3;
				ChallengeModeDatabase challengeModeDatabase = GameManager.instance.challengeModeDatabase;
				character2.hitPointsTempIncrease = Random.Range(challengeModeDatabase.minHardModeHitPointsIncrease, challengeModeDatabase.maxHardModeHitPointsIncrease);
				character2.attackTempIncrease = Random.Range(challengeModeDatabase.minHardModeAttackIncrease, challengeModeDatabase.maxHardModeAttackIncrease);
			}
			else
			{
				character2 = new Character(CharacterRoller.RollCharacterData(characterDefinition, -1, characterDefinition.uid, Random.Range(8, 13)));
			}
			list.Add(character2);
		}
		while (list.Count < 3)
		{
			characterDefinition = GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition def) => def.canUseInBattle && !definitionsAdded.Contains(def.uid) && CharacterAllowed(def));
			definitionsAdded.Add(characterDefinition.uid);
			Character character3 = null;
			if (isHard)
			{
				character3 = new Character(CharacterRoller.RollCharacterDataChallengeMode(characterDefinition, -1, characterDefinition.uid, 20, hardMode: true));
				character3.shinyRank = 3;
				ChallengeModeDatabase challengeModeDatabase2 = GameManager.instance.challengeModeDatabase;
				character3.hitPointsTempIncrease = Random.Range(challengeModeDatabase2.minHardModeHitPointsIncrease, challengeModeDatabase2.maxHardModeHitPointsIncrease);
				character3.attackTempIncrease = Random.Range(challengeModeDatabase2.minHardModeAttackIncrease, challengeModeDatabase2.maxHardModeAttackIncrease);
			}
			else
			{
				character3 = new Character(CharacterRoller.RollCharacterData(characterDefinition, -1, characterDefinition.uid, Random.Range(8, 13)));
			}
			list.Add(character3);
		}
		list.Shuffle();
		return list;
	}

	private static bool CharacterAllowed(CharacterDefinition characterDef)
	{
		if (characterDef == null)
		{
			return false;
		}
		string characterName = characterDef.characterName;
		if (characterName.Equals("80s Cyborg") && Player.instance.GetMissionCompletedData(8) == null)
		{
			return false;
		}
		if (characterName.Equals("Sticky Joe") && Player.instance.GetMissionCompletedData(13) == null)
		{
			return false;
		}
		if (characterName.Equals("Pirate Aqualad") && Player.instance.GetMissionCompletedData(18) == null)
		{
			return false;
		}
		if (characterName.Equals("Terra") && Player.instance.GetMissionCompletedData(20) == null)
		{
			return false;
		}
		if (characterName.Equals("Jinx") && Player.instance.GetMissionCompletedData(36) == null)
		{
			return false;
		}
		if (characterName.Equals("The Hooded Hood") && Player.instance.GetMissionCompletedData(37) == null)
		{
			return false;
		}
		if (characterName.Equals("Mr. Chibi") && Player.instance.GetMissionCompletedData(37) == null)
		{
			return false;
		}
		if (characterName.Equals("Darkseid") && Player.instance.GetMissionCompletedData(36) == null)
		{
			return false;
		}
		if (characterName.Equals("Joker") && Player.instance.GetMissionCompletedData(37) == null)
		{
			return false;
		}
		if (characterName.Contains("Multiverse") && Player.instance.GetMissionCompletedData(18) == null)
		{
			return false;
		}
		if (NeedsCharacterWikiUnlocked(characterName) && !Player.instance.HasAcquiredCharacter(characterDef.uid))
		{
			return false;
		}
		if (characterName.Equals("Shadow Fig"))
		{
			return false;
		}
		return true;
	}

	private static bool NeedsCharacterWikiUnlocked(string characterName)
	{
		for (int i = 0; i < wikiUnlockedCharacters.Length; i++)
		{
			if (wikiUnlockedCharacters[i].Equals(characterName))
			{
				return true;
			}
		}
		return false;
	}

	private static NPCDefinition GetRandomOpponentNPC()
	{
		bool flag = false;
		NPCDefinition nPCDefinition = null;
		while (!flag)
		{
			nPCDefinition = GameManager.instance.npcDatabase.npcs[Random.Range(0, GameManager.instance.npcDatabase.npcs.Count)];
			if (NPCAllowed(nPCDefinition))
			{
				flag = true;
			}
		}
		return nPCDefinition;
	}

	private static bool NPCAllowed(NPCDefinition npcDef)
	{
		if (npcDef.name.Equals("None") || npcDef.name.Equals("Robin") || npcDef.name.Equals("Mr. Chibi Tournament"))
		{
			return false;
		}
		return true;
	}
}
