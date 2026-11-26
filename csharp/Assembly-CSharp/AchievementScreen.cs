using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AchievementScreen : MonoBehaviour
{
	public Action Closed;

	public AchievementEntry entryPrefab;

	public tk2dUIScrollableArea scrollableArea;

	public TextMeshPro unlockedAchievementsNum;

	public TextMeshPro totalAchievementsNum;

	public tk2dUIItem exitButton;

	public tk2dUIItem exitPanel;

	public GameObject animGroup;

	public Vector3 onscreenLocalPosition;

	public Vector3 offscreenLocalPosition;

	public float ySpacing;

	public float tweenDuration = 0.25f;

	private List<AchievementEntry> entries;

	private void Awake()
	{
		Hide(animate: false);
		InitAchievementEntries();
	}

	private void OnDestroy()
	{
		exitButton.OnClick -= HandleExitClicked;
		exitPanel.OnClick -= HandleExitClicked;
	}

	private void InitAchievementEntries()
	{
		int count = GameManager.instance.achievementsDatabase.achievements.Count;
		if (entries == null)
		{
			entries = new List<AchievementEntry> { entryPrefab };
			for (int i = 1; i < count; i++)
			{
				AchievementEntry achievementEntry = UnityEngine.Object.Instantiate(entryPrefab);
				achievementEntry.transform.position = entryPrefab.transform.position + new Vector3(0f, (0f - ySpacing) * (float)i);
				achievementEntry.transform.parent = entryPrefab.transform.parent;
				entries.Add(achievementEntry);
			}
		}
	}

	public void Init()
	{
		InitAchievementEntries();
		int num = 0;
		int count = GameManager.instance.achievementsDatabase.achievements.Count;
		for (int i = 0; i < count; i++)
		{
			AchievementData achievementData = GameManager.instance.achievementsDatabase.achievements[i];
			entries[i].Init(achievementData);
			if (GameManager.instance.achievementsDatabase.IsAchievementUnlocked(achievementData.id))
			{
				num++;
			}
		}
		unlockedAchievementsNum.text = num.ToString();
		totalAchievementsNum.text = count.ToString();
		exitButton.gameObject.SetActive(value: true);
		exitPanel.gameObject.SetActive(value: true);
		animGroup.gameObject.SetActive(value: true);
		animGroup.transform.DOKill();
		animGroup.transform.DOLocalMove(onscreenLocalPosition, tweenDuration).SetEase(Ease.OutBack).OnComplete(() =>
		{
			exitButton.OnClick += HandleExitClicked;
			exitPanel.OnClick += HandleExitClicked;
		});
		scrollableArea.ContentLength = (float)count * ySpacing + 31f;
	}

	private void Hide(bool animate = true)
	{
		exitButton.OnClick -= HandleExitClicked;
		exitPanel.OnClick -= HandleExitClicked;
		if (animate)
		{
			animGroup.transform.DOLocalMove(offscreenLocalPosition, tweenDuration).SetEase(Ease.InBack).OnComplete(HideImmediate);
		}
		else
		{
			HideImmediate();
		}
	}

	private void HideImmediate()
	{
		animGroup.transform.localPosition = offscreenLocalPosition;
		animGroup.gameObject.SetActive(value: false);
		exitButton.gameObject.SetActive(value: false);
		exitPanel.gameObject.SetActive(value: false);
		if (Closed != null)
		{
			Closed();
		}
	}

	private void HandleExitClicked()
	{
		Hide();
	}
}
