using System.Collections.Generic;
using UnityEngine;

public static class PlayerRewardFactory
{
	public static PlayerReward GetRandomRepaintReward(bool rareOnly = false, float rareChance = 0.25f, float dupesChance = 0.1f)
	{
		rareChance = Mathf.Clamp01(rareChance);
		dupesChance = Mathf.Clamp01(dupesChance);
		bool isRare = rareOnly || (rareChance > 0f && Random.value <= rareChance);
		bool allowDupes = dupesChance > 0f && Random.value <= dupesChance;
		List<RepaintColorSlot> list = new List<RepaintColorSlot>();
		for (int i = 0; i < Player.instance.charactersAcquired.Count; i++)
		{
			CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(Player.instance.charactersAcquired[i]);
			list.AddRange(character.repaintSlots.FindAll((RepaintColorSlot x) => x.rare == isRare && (allowDupes || !Player.instance.repaintSlotsAcquired.Contains(x.id))));
		}
		if (list.Count == 0)
		{
			Debug.Log("Resorting to duplicate repaint color slots due to having already acquired some");
			for (int num = 0; num < Player.instance.charactersAcquired.Count; num++)
			{
				CharacterDefinition character = GameManager.instance.characterDatabase.GetCharacter(Player.instance.charactersAcquired[num]);
				list.AddRange(character.repaintSlots.FindAll((RepaintColorSlot x) => x.rare == isRare));
			}
		}
		if (list.Count == 0)
		{
			Debug.Log("There are no repaint slots available to reward the player. This is due to none of their acquired characters having any repaints set in the database. You may want to check on this.");
			return null;
		}
		PlayerReward playerReward = new PlayerReward();
		playerReward.type = PlayerRewardType.Repaint;
		RepaintColorSlot repaintColorSlot = list[Random.Range(0, list.Count)];
		playerReward.characterUID = GameManager.instance.characterDatabase.GetCharacterWithRepaintColorSlot(repaintColorSlot).uid;
		playerReward.repaintUID = repaintColorSlot.id;
		return playerReward;
	}
}
