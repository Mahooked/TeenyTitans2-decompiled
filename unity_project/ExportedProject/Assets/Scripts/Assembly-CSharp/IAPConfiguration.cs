using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Teen Titans/IAP Config")]
public class IAPConfiguration : ScriptableObject
{
	[SerializeField]
	private IAPProductIDMap _idMap;

	[SerializeField]
	private IAPProduct[] _riddleEggs;

	[SerializeField]
	private IAPProduct[] _repaintTokens;

	[SerializeField]
	private IAPProduct[] _rareFigs;

	[SerializeField]
	private IAPFigureCampaign[] _figureCampaigns;

	[SerializeField]
	private IAPRiddleEggOdds _greenEggOdds;

	[SerializeField]
	private IAPRiddleEggOdds _goldenEggOdds;

	public IAPProductIDMap idMap
	{
		get
		{
			return _idMap;
		}
	}

	public IAPProduct[] riddleEggs
	{
		get
		{
			return _riddleEggs;
		}
	}

	public IAPProduct[] repaintTokens
	{
		get
		{
			return _repaintTokens;
		}
	}

	public IAPProduct[] rareFigs
	{
		get
		{
			return _rareFigs;
		}
	}

	public IAPFigureCampaign[] figureCampaigns
	{
		get
		{
			return _figureCampaigns;
		}
	}

	public IAPRiddleEggOdds greenEggOdds
	{
		get
		{
			return _greenEggOdds;
		}
	}

	public IAPRiddleEggOdds goldenEggOdds
	{
		get
		{
			return _goldenEggOdds;
		}
	}

	public IAPProduct GetProduct(string productId)
	{
		for (int i = 0; i < _riddleEggs.Length; i++)
		{
			if (_riddleEggs[i].ProductID == productId)
			{
				return _riddleEggs[i];
			}
		}
		for (int j = 0; j < _repaintTokens.Length; j++)
		{
			if (_repaintTokens[j].ProductID == productId)
			{
				return _repaintTokens[j];
			}
		}
		for (int k = 0; k < _rareFigs.Length; k++)
		{
			if (_rareFigs[k].ProductID == productId)
			{
				return _rareFigs[k];
			}
		}
		return null;
	}

	public IAPRiddleEggOdds GetEggOddsByType(RiddleEggType type)
	{
		if (type == RiddleEggType.Gold)
		{
			return _goldenEggOdds;
		}
		return _greenEggOdds;
	}

	public PlayerReward GenerateRiddleEggReward(RiddleEggType type)
	{
		PlayerReward playerReward = new PlayerReward();
		playerReward.type = PlayerRewardType.RiddleEgg;
		playerReward.riddleEggType = type;
		List<PlayerReward> list = new List<PlayerReward>();
		IAPRiddleEggOdds eggOddsByType = GetEggOddsByType(type);
		List<IAPRiddleEggOdds.RewardEntry> guaranteedRewardEntries = eggOddsByType.GetGuaranteedRewardEntries();
		for (int i = 0; i < guaranteedRewardEntries.Count; i++)
		{
			if (guaranteedRewardEntries[i].isActive)
			{
				list.Add(GetPlayerRewardForEntry(guaranteedRewardEntries[i]));
			}
		}
		if (list.Count < 4)
		{
			List<IAPRiddleEggOdds.RewardEntry> nonGuaranteedRewardEntries = eggOddsByType.GetNonGuaranteedRewardEntries();
			int num = 0;
			for (int j = 0; j < nonGuaranteedRewardEntries.Count; j++)
			{
				if (nonGuaranteedRewardEntries[j].isActive)
				{
					num += nonGuaranteedRewardEntries[j].weight;
				}
			}
			while (list.Count < 4)
			{
				int num2 = Random.Range(0, num);
				int num3 = 0;
				for (int k = 0; k < nonGuaranteedRewardEntries.Count; k++)
				{
					if (!nonGuaranteedRewardEntries[k].isActive)
					{
						continue;
					}
					num3 += nonGuaranteedRewardEntries[k].weight;
					if (num2 < num3)
					{
						PlayerReward playerRewardForEntry = GetPlayerRewardForEntry(nonGuaranteedRewardEntries[k]);
						if (playerRewardForEntry != null)
						{
							list.Add(playerRewardForEntry);
						}
						break;
					}
				}
			}
		}
		playerReward.eggRewards = new PlayerReward[list.Count];
		for (int l = 0; l < playerReward.eggRewards.Length; l++)
		{
			int index = Random.Range(0, list.Count);
			playerReward.eggRewards[l] = list[index];
			list.RemoveAt(index);
		}
		return playerReward;
	}

	private PlayerReward GetPlayerRewardForEntry(IAPRiddleEggOdds.RewardEntry entry)
	{
		PlayerReward playerReward = new PlayerReward();
		playerReward.type = entry.reward.type;
		PlayerRewardType type = playerReward.type;
		switch (type)
		{
		case PlayerRewardType.Currency:
			playerReward.currencyType = entry.reward.currencyType;
			playerReward.amount = entry.reward.amount;
			break;
		case PlayerRewardType.Figure:
		{
			int num2 = entry.figureWeights.Length;
			if (num2 == 1)
			{
				int uid = GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition x) => x.characterRarity == entry.figureWeights[0].rarity && x.isActive && x.canUseInBattle && !x.isSecret).uid;
				CharacterRewardData characterRewardData = new CharacterRewardData(uid);
				characterRewardData.characterLvl = 5;
				playerReward.figure = characterRewardData;
			}
			else
			{
				if (num2 <= 1)
				{
					break;
				}
				int num3 = 0;
				for (int num4 = 0; num4 < num2; num4++)
				{
					if (entry.isActive)
					{
						num3 += entry.figureWeights[num4].weight;
					}
				}
				int num5 = Random.Range(0, num3);
				int num6 = 0;
				for (int i2 = 0; i2 < num2; i2++)
				{
					if (!entry.isActive)
					{
						continue;
					}
					num6 += entry.figureWeights[i2].weight;
					if (num5 < num6)
					{
						int uid2 = GameManager.instance.characterDatabase.GetRandomCharacter((CharacterDefinition x) => x.characterRarity == entry.figureWeights[i2].rarity && x.isActive && x.canUseInBattle && !x.isSecret).uid;
						CharacterRewardData characterRewardData2 = new CharacterRewardData(uid2);
						characterRewardData2.characterLvl = 5;
						playerReward.figure = characterRewardData2;
						break;
					}
				}
			}
			break;
		}
		case PlayerRewardType.Item:
			playerReward.itemUID = entry.reward.itemUID;
			playerReward.amount = entry.reward.amount;
			break;
		case PlayerRewardType.Repaint:
		{
			bool isRare = entry.reward.isRepaintRare || Random.value <= 0.25f;
			bool allowDupes = Random.value <= 0.1f;
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
				Debug.LogError("There are no repaint slots available to reward the player. This is due to none of their acquired characters having any repaints set in the database. You may want to check on this. For now just going to choose another reward type.");
				return null;
			}
			RepaintColorSlot repaintColorSlot = list[Random.Range(0, list.Count)];
			playerReward.characterUID = GameManager.instance.characterDatabase.GetCharacterWithRepaintColorSlot(repaintColorSlot).uid;
			playerReward.repaintUID = repaintColorSlot.id;
			break;
		}
		}
		return playerReward;
	}
}
