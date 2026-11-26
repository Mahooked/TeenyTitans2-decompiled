using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelUpScreen : MonoBehaviour
{
	private List<int> silkieUIDs = new List<int> { 30, 51, 45, 41, 71, 23 };

	public Animator bgAnimator;

	public Animator modelAnimator;

	public AnimatorEventListener bgAnimatorEventListener;

	public AnimatorEventListener modelAnimatorEventListener;

	public Transform modelPosition;

	public TextMeshPro characterName;

	public TextMeshPro levelNumber;

	public float buttonRevealStagger = 0.1f;

	private GameObject currentModel;

	private Character currentCharacter;

	private List<Character> charactersToLevelUp = new List<Character>();

	private List<LevelUpScreenButton> rewardButtons = new List<LevelUpScreenButton>();

	private void OnEnable()
	{
		bgAnimatorEventListener.EventTriggered += HandleOnBGEventTriggered;
		modelAnimatorEventListener.EventTriggered += HandleOnModelEventTriggered;
	}

	private void OnDisable()
	{
		bgAnimatorEventListener.EventTriggered -= HandleOnBGEventTriggered;
		modelAnimatorEventListener.EventTriggered -= HandleOnModelEventTriggered;
	}

	private void HandleOnBGEventTriggered(AnimationEvent evt)
	{
		string stringParameter = evt.stringParameter;
		if (stringParameter != null && stringParameter == "outroComplete")
		{
			gameObject.SetActive(value: false);
		}
	}

	private void HandleOnModelEventTriggered(AnimationEvent evt)
	{
		switch (evt.stringParameter)
		{
		case "introComplete":
			StartCoroutine(ShowRewards());
			break;
		case "outroComplete":
			if (charactersToLevelUp.Count > 0)
			{
				UpdateWithCharacter(charactersToLevelUp[0], startFromDrop: true);
			}
			break;
		}
	}

	public void QueueUpCharacter(Character character)
	{
		if (!charactersToLevelUp.Contains(character))
		{
			charactersToLevelUp.Add(character);
		}
	}

	public void CheckQueue()
	{
		if (charactersToLevelUp.Count > 0)
		{
			UpdateWithCharacter(charactersToLevelUp[0]);
		}
	}

	public void UpdateWithCharacter(Character character, bool startFromDrop = false)
	{
		gameObject.SetActive(value: true);
		characterName.text = ((!string.IsNullOrEmpty(character.characterDefinition.characterNameKey)) ? Language.Get(character.characterDefinition.characterNameKey, "NPCS,FIGS") : character.characterDefinition.characterName);
		levelNumber.text = Language.Get("LVL", "MENUS,GUI") + " " + character.level;
		if (currentModel != null)
		{
			Object.Destroy(currentModel);
		}
		currentModel = Object.Instantiate(Resources.Load(character.characterDefinition.shopFigurePath) as GameObject);
		currentModel.SetLayerRecursively(LayerMask.NameToLayer("UI"));
		currentModel.transform.parent = modelPosition.parent;
		currentModel.transform.localPosition = modelPosition.localPosition;
		currentModel.transform.localEulerAngles = Vector3.zero;
		currentModel.transform.localScale = Vector3.one;
		ShopFigureModel component = currentModel.GetComponent<ShopFigureModel>();
		if (component != null)
		{
			component.character = character;
			RepaintFigureModel(component);
		}
		if (silkieUIDs.Contains(character.characterDefinition.uid))
		{
			currentModel.transform.localPosition += new Vector3(0f, 0.4f, 0f);
		}
		currentCharacter = character;
		if (!startFromDrop)
		{
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_upgradescreen_firstfigureland", GFAudioManager.SFX_GROUP, string.Empty, 1f, 1.15f);
		}
		modelAnimator.Play("Intro", 0, (!startFromDrop) ? 0f : 0.2875f);
	}

	private void RepaintFigureModel(ShopFigureModel model)
	{
		if (!(model != null) || model.character == null)
		{
			return;
		}
		int repaintColorPreferenceForCharacterUID = Player.instance.GetRepaintColorPreferenceForCharacterUID(model.character.uid);
		if (repaintColorPreferenceForCharacterUID <= 0)
		{
			return;
		}
		RepaintColorSlot repaintColorSlotForID = model.character.characterDefinition.GetRepaintColorSlotForID(repaintColorPreferenceForCharacterUID);
		if (repaintColorSlotForID != null)
		{
			string texturePathScaled = repaintColorSlotForID.texturePathScaled;
			Texture texture = Resources.Load<Texture>(texturePathScaled);
			if (texture != null)
			{
				model.UpdateTexture(texture);
			}
		}
	}

	private IEnumerator ShowRewards()
	{
		LevelData levelData = currentCharacter.characterDefinition.levelTable.GetDataForLevel(currentCharacter.lastRewardLevel + 1);
		List<LevelUpReward> rewards = new List<LevelUpReward>();
		int len = levelData.rewards.Count;
		for (int i = 0; i < len; i++)
		{
			if (levelData.rewards[i].isForced)
			{
				rewards.Add(levelData.rewards[i]);
			}
		}
		if (rewards.Count == 0 && levelData.fillWithStats)
		{
			List<int> list = new List<int>();
			list.Add(0);
			list.Add(1);
			list.Add(3);
			list.Add(4);
			List<int> list2 = list;
			int num = currentCharacter.selectedRewards.Count - 1;
			for (int num2 = num; num2 >= 0; num2--)
			{
				LevelUpReward rewardById = currentCharacter.characterDefinition.levelTable.GetRewardById(currentCharacter.selectedRewards[num2]);
				if (rewardById.type == LevelUpRewardType.StatUpgrade)
				{
					list2.Remove((int)rewardById.statType);
					break;
				}
			}
			while (rewards.Count < 3 && list2.Count > 0)
			{
				int index = Random.Range(0, list2.Count);
				CharacterStatType characterStatType = (CharacterStatType)list2[index];
				list2.RemoveAt(index);
				switch (characterStatType)
				{
				case CharacterStatType.HitPoints:
					if (currentCharacter.hitPointsTier < currentCharacter.levelTable.hitPointsUpgradeRewards.Count && currentCharacter.GetBaseStatPlusOffsetByType(CharacterStatType.HitPoints) < currentCharacter.characterDefinition.maxHitPoints)
					{
						rewards.Add(currentCharacter.levelTable.hitPointsUpgradeRewards[currentCharacter.hitPointsTier]);
					}
					break;
				case CharacterStatType.Attack:
					if (currentCharacter.attackTier < currentCharacter.levelTable.attackUpgradeRewards.Count && currentCharacter.GetBaseStatPlusOffsetByType(CharacterStatType.Attack) < currentCharacter.characterDefinition.maxAttack)
					{
						rewards.Add(currentCharacter.levelTable.attackUpgradeRewards[currentCharacter.attackTier]);
					}
					break;
				case CharacterStatType.Speed:
					if (currentCharacter.speedTier < currentCharacter.levelTable.speedUpgradeRewards.Count && currentCharacter.GetBaseStatPlusOffsetByType(CharacterStatType.Speed) < currentCharacter.characterDefinition.maxSpeed)
					{
						rewards.Add(currentCharacter.levelTable.speedUpgradeRewards[currentCharacter.speedTier]);
					}
					break;
				case CharacterStatType.Luck:
					if (currentCharacter.luckTier < currentCharacter.levelTable.luckUpgradeRewards.Count && currentCharacter.GetBaseStatPlusOffsetByType(CharacterStatType.Luck) < currentCharacter.characterDefinition.maxLuck)
					{
						rewards.Add(currentCharacter.levelTable.luckUpgradeRewards[currentCharacter.luckTier]);
					}
					break;
				case CharacterStatType.Dodge:
					if (currentCharacter.dodgeTier < currentCharacter.levelTable.dodgeUpgradeRewards.Count && currentCharacter.GetBaseStatPlusOffsetByType(CharacterStatType.Dodge) < currentCharacter.characterDefinition.maxDodge)
					{
						rewards.Add(currentCharacter.levelTable.dodgeUpgradeRewards[currentCharacter.dodgeTier]);
					}
					break;
				}
			}
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_upgradescreen_buttonsappear", GFAudioManager.SFX_GROUP, string.Empty);
		int numRewards = rewards.Count;
		Vector3 newPosition = new Vector3(104f * (float)(numRewards - 1) * -0.5f, 0f, -400f);
		for (int j = 0; j < numRewards; j++)
		{
			LevelUpScreenButton button = ObjectPool.GetObjectFromResource<LevelUpScreenButton>("Prefabs/GUI/Battle/LevelUp_Screen_Button" + ((numRewards != 1) ? string.Empty : "_Single"));
			button.ignoreRefresh = false;
			button.Init(currentCharacter, rewards[j]);
			button.transform.parent = transform;
			button.transform.localPosition = newPosition;
			button.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
			button.collider2D.enabled = false;
			rewardButtons.Add(button);
			newPosition.x += 104f;
			yield return new WaitForSeconds(buttonRevealStagger);
		}
		len = rewardButtons.Count;
		for (int k = 0; k < len; k++)
		{
			rewardButtons[k].collider2D.enabled = true;
			rewardButtons[k].uiItem.OnClickUIItem += HandleOnRewardClick;
		}
	}

	private void HandleOnRewardClick(tk2dUIItem uiItem)
	{
		LevelUpScreenButton component = uiItem.GetComponent<LevelUpScreenButton>();
		component.ignoreRefresh = true;
		currentCharacter.SelectReward(component.reward);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_upgradescreen_makingachoice", GFAudioManager.SFX_GROUP, string.Empty);
		component.Select();
		Debug.Log($"Selected reward of type {component.reward.type}");
		int count = rewardButtons.Count;
		for (int i = 0; i < count; i++)
		{
			rewardButtons[i].collider2D.enabled = false;
			rewardButtons[i].uiItem.OnClickUIItem -= HandleOnRewardClick;
			if (!(rewardButtons[i] == component))
			{
				rewardButtons[i].animator.Play("Outro");
			}
		}
		StartCoroutine(DelayedOutro(component));
	}

	private IEnumerator DelayedOutro(LevelUpScreenButton button)
	{
		yield return new WaitForSeconds(1.5f);
		button.animator.Play("Outro");
		yield return new WaitForSeconds(1f);
		int len = rewardButtons.Count;
		for (int i = 0; i < len; i++)
		{
			ObjectPool.PoolObject(rewardButtons[i].gameObject);
		}
		rewardButtons.Clear();
		if (currentCharacter.lastRewardLevel < currentCharacter.level)
		{
			StartCoroutine(ShowRewards());
			yield break;
		}
		if (charactersToLevelUp.Count > 0)
		{
			charactersToLevelUp.RemoveAt(0);
		}
		if (charactersToLevelUp.Count > 0)
		{
			modelAnimator.Play("Outro");
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_upgradescreen_subsequentfigureland", GFAudioManager.SFX_GROUP, string.Empty, 1f, 0.1f);
		}
		else
		{
			bgAnimator.Play("Outro");
			modelAnimator.Play("Outro");
		}
	}
}
