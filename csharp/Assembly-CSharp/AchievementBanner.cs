using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AchievementBanner : MonoBehaviour
{
	private static List<string> AchievementQueue = new List<string>();

	public GameObject bannerGroup;

	public tk2dSprite iconSprite;

	public TextMeshPro titleText;

	public TextMeshPro descriptionText;

	public AudioClip achievementSfx;

	public Vector3 bannerOffscreenLocalPosition = new Vector3(0f, 200f);

	public Vector3 bannerOnScreenLocalPosition = Vector3.zero;

	public float tweenDuration = 0.3f;

	public float bannerEntranceDelay = 0.02f;

	public float bannerExitDelay = 3f;

	private static AchievementBanner _instance;

	public static AchievementBanner instance
	{
		get
		{
			if (_instance == null)
			{
				GameObject gameObject = Object.Instantiate(Resources.Load("Prefabs/GUI/AchievementBanner") as GameObject);
				_instance = gameObject.GetComponent<AchievementBanner>();
			}
			return _instance;
		}
	}

	private void Awake()
	{
		if (_instance != null)
		{
			Object.Destroy(gameObject);
		}
		Object.DontDestroyOnLoad(gameObject);
		gameObject.SetActive(value: false);
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public void DisplayAchievementID(string achievementID, float bannerDisplayDelay = 0f)
	{
		bool flag = AchievementQueue.Count == 0;
		AchievementQueue.Add(achievementID);
		if (flag)
		{
			gameObject.SetActive(value: true);
			StartCoroutine(StartAchievementQueue(bannerDisplayDelay + 0.5f));
		}
	}

	private IEnumerator StartAchievementQueue(float bannerDisplayDelay = 0f)
	{
		if (bannerDisplayDelay > 0f)
		{
			bannerGroup.transform.localPosition = bannerOffscreenLocalPosition;
			yield return new WaitForSecondsIndependent(bannerDisplayDelay);
		}
		while (AchievementQueue.Count > 0)
		{
			AchievementData achievementData = GameManager.instance.achievementsDatabase.AchievementDataForId(AchievementQueue[0]);
			bannerGroup.transform.localPosition = bannerOffscreenLocalPosition;
			if (achievementData != null)
			{
				iconSprite.SetSprite(achievementData.icon);
				titleText.text = Language.Get(achievementData.id.ToUpper(), "ABILS,CHIPS,ACHVS,LDBS");
				descriptionText.text = Language.Get(achievementData.id.ToUpper() + "_DESC", "ABILS,CHIPS,ACHVS,LDBS");
			}
			else
			{
				Debug.LogError("Achievement Banner, Achievement Data is NULL. id: " + AchievementQueue[0]);
				titleText.text = "ERROR";
				descriptionText.text = "I AM ERROR.";
			}
			GFAudioManager.instance.PlayClip(achievementSfx, GFAudioManager.SFX_GROUP, 1f, loop: false, 0f, 128, string.Empty);
			Sequence bannerSequence = DOTween.Sequence();
			bannerSequence.SetUpdate(isIndependentUpdate: true);
			bannerSequence.Append(bannerGroup.transform.DOLocalMove(bannerOnScreenLocalPosition, tweenDuration).SetDelay(bannerEntranceDelay).SetEase(Ease.OutBack)
				.SetUpdate(isIndependentUpdate: true));
			bannerSequence.Append(bannerGroup.transform.DOLocalMove(bannerOffscreenLocalPosition, tweenDuration).SetDelay(bannerExitDelay).SetEase(Ease.InBack)
				.SetUpdate(isIndependentUpdate: true));
			bannerSequence.AppendCallback(() =>
			{
				AchievementQueue.RemoveAt(0);
			});
			yield return bannerSequence.WaitForCompletion();
		}
		gameObject.SetActive(value: false);
	}
}
