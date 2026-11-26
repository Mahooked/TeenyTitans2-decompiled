using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NewFigureReveal : MonoBehaviour, IPoolable
{
	public SimpleFigureBox figureBox;

	public Animator mainAnimator;

	public Animator figureRevealAnimator;

	public Animator figureRevealGroupAnimator;

	public TextMeshPro figureNameText;

	public TextMeshPro numFiguresDiscoveredText;

	public tk2dUIItem nextButton;

	public GameObject newFigureBacking;

	public GameObject[] nonBlindBoxObjects;

	public GameObject[] blindBoxObjects;

	public MeshRenderer[] blindBoxRenderers;

	private List<PlayerReward> rewardQueue = new List<PlayerReward>();

	private static NewFigureReveal instance;

	public static NewFigureReveal Instance => instance;

	private void _Show(PlayerReward reward)
	{
		AddToQueue(reward);
		StartCoroutine(WaitForIntro());
	}

	private void _Show(List<PlayerReward> rewards)
	{
		AddToQueue(rewards);
		StartCoroutine(WaitForIntro());
	}

	private void AddToQueue(PlayerReward reward)
	{
		if (reward.type == PlayerRewardType.Figure)
		{
			rewardQueue.Add(reward);
		}
	}

	private void AddToQueue(List<PlayerReward> rewards)
	{
		for (int i = 0; i < rewards.Count; i++)
		{
			AddToQueue(rewards[i]);
		}
	}

	private IEnumerator WaitForIntro()
	{
		StartCoroutine(WaitForRewards());
		yield break;
	}

	private IEnumerator WaitForRewards()
	{
		while (rewardQueue.Count > 0)
		{
			yield return StartCoroutine(ShowReward(rewardQueue.Shift()));
		}
		mainAnimator.Play("NewFigureReveal_Outro");
		mainAnimator.Update(0f);
		figureRevealAnimator.Play("NewFigureReveal_Fig_outro", 0, 0f);
		yield return new WaitWhile(() => mainAnimator.IsPlaying());
		ObjectPool.PoolObject(gameObject);
	}

	private IEnumerator ShowReward(PlayerReward reward)
	{
		figureRevealGroupAnimator.Play("empty");
		newFigureBacking.SetActive(value: true);
		RemoveBlindBox();
		int characterLvl;
		CharacterDefinition charDef;
		if (reward.figure.blindBox)
		{
			newFigureBacking.SetActive(value: false);
			BlindBoxShopFigure blindBoxShopFigure = reward.figure.blindBoxShopFigures[Random.Range(0, reward.figure.blindBoxShopFigures.Count)];
			characterLvl = blindBoxShopFigure.level;
			charDef = GameManager.instance.characterDatabase.GetCharacter(blindBoxShopFigure.characterUID);
			Player.instance.AcquireCharacter(charDef.uid);
			Player.instance.AddCharacter(CharacterRoller.RollCharacterData(charDef, -1, charDef.uid, characterLvl));
			Player.instance.AddToParty(Player.instance.characters.GetLastItem());
		}
		else
		{
			charDef = GameManager.instance.characterDatabase.GetCharacter(reward.figure.characterUID);
			characterLvl = reward.figure.characterLvl;
		}
		figureBox.Init(charDef, characterLvl, reward.figure.blindBox, reward.figure.goldBox);
		figureBox.figure.gameObject.SetActive(value: false);
		figureBox.figure.figureBase.SetActive(value: true);
		yield return new WaitForSeconds(0.2f);
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_firstfigappear", GFAudioManager.SFX_GROUP, string.Empty);
		numFiguresDiscoveredText.transform.parent.gameObject.SetActive(value: true);
		numFiguresDiscoveredText.text = $"<color=\"orange\">{Mathf.Max(0, Player.instance.charactersAcquired.Count)}<color=\"white\">/{GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance)}";
		figureNameText.text = ((!string.IsNullOrEmpty(charDef.characterNameKey)) ? Language.Get(charDef.characterNameKey, "NPCS,FIGS") : charDef.characterName);
		figureBox.figure.transform.DOKill();
		figureBox.figure.transform.localEulerAngles = new Vector3(charDef.shopFigureRotationOffset.x, 180f + charDef.shopFigureRotationOffset.y, charDef.shopFigureRotationOffset.z);
		yield return new WaitForSeconds(0.1f);
		figureBox.figure.gameObject.SetActive(value: true);
		yield return new WaitForTap();
		figureRevealAnimator.Play("NewFigureReveal_Fig_Tap");
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_newfigreveal_open", GFAudioManager.SFX_GROUP, string.Empty);
		yield return new WaitForSeconds(1.6f);
		if (!string.IsNullOrEmpty(charDef.shopRevealNameSoundPath))
		{
			GFAudioManager.instance.PlayClipFromResources(charDef.shopRevealNameSoundPath, GFAudioManager.SFX_GROUP, string.Empty);
		}
		yield return new WaitForSeconds(0.6f);
		numFiguresDiscoveredText.text = $"<color=\"orange\">{Player.instance.charactersAcquired.Count}<color=\"white\">/{GameManager.instance.characterDatabase.GetTotalDiscoverableCharactersForPlayer(Player.instance)}";
		figureBox.figure.transform.DOLocalRotate(new Vector3(0f, 900f, 0f), 25f, RotateMode.FastBeyond360);
		nextButton.gameObject.SetActive(value: true);
		yield return new tk2dWaitForOnClick(nextButton);
		nextButton.gameObject.SetActive(value: false);
	}

	private void MakeBlindBox()
	{
		if (blindBoxRenderers.Length > 0)
		{
			MeshRenderer meshRenderer = null;
			for (int i = 0; i < blindBoxRenderers.Length; i++)
			{
				meshRenderer = blindBoxRenderers[i];
				if (meshRenderer.material.mainTexture.name.Contains("Gold"))
				{
					Texture texture = Resources.Load<Texture>("Textures/BlindBox_Texture-" + tk2dSystem.CurrentPlatform);
					if (texture != null)
					{
						meshRenderer.material.mainTexture = texture;
					}
				}
			}
		}
		for (int j = 0; j < blindBoxObjects.Length; j++)
		{
			blindBoxObjects[j].SetActive(value: true);
		}
		for (int k = 0; k < nonBlindBoxObjects.Length; k++)
		{
			nonBlindBoxObjects[k].SetActive(value: false);
		}
	}

	private void RemoveBlindBox()
	{
		for (int i = 0; i < blindBoxObjects.Length; i++)
		{
			blindBoxObjects[i].SetActive(value: false);
		}
		for (int j = 0; j < nonBlindBoxObjects.Length; j++)
		{
			nonBlindBoxObjects[j].SetActive(value: true);
		}
	}

	public static void Show(PlayerReward reward)
	{
		if (instance == null)
		{
			if (reward.type == PlayerRewardType.Figure)
			{
				instance = ObjectPool.GetObjectFromResource<NewFigureReveal>("Prefabs/GUI/NewFigureReveal", new Vector3(5000f, 0f, 0f));
				instance._Show(reward);
			}
			else
			{
				Debug.Log("The reward needs to be of the Figure type for this reveal screen.");
			}
		}
		else if (reward.type == PlayerRewardType.Figure)
		{
			Debug.Log("An instance of NewFigureReveal is already open, so adding the reward to the queue");
			instance.AddToQueue(reward);
		}
		else
		{
			Debug.Log("The reward needs to be of the Figure type for this reveal screen.");
		}
	}

	public static void Show(List<PlayerReward> rewards)
	{
		if (instance == null)
		{
			instance = ObjectPool.GetObjectFromResource<NewFigureReveal>("Prefabs/GUI/NewFigureReveal", new Vector3(5000f, 0f, 0f));
			instance._Show(rewards);
		}
		else
		{
			instance.AddToQueue(rewards);
		}
	}

	public void ResetObject()
	{
		instance = null;
	}
}
