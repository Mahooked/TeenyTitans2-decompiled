using TMPro;
using UnityEngine;

public class LegionOfDoomPortrait : MonoBehaviour
{
	public tk2dUIItem uiItem;

	[Header("Visual References")]
	public tk2dSprite lockSprite;

	public tk2dSprite portraitSprite;

	public TextMeshPro costTextMesh;

	public GameObject coinTagObj;

	public TextMeshPro rankTextMesh;

	public tk2dSprite questionMarkSprite;

	public tk2dSprite newSprite;

	public LegionOfDoomPortraitUnlock unlockData { get; private set; }

	public void Init(LegionOfDoomPortraitUnlock unlockData)
	{
		this.unlockData = unlockData;
		uiItem.OnClick -= HandleClicked;
		uiItem.OnClick += HandleClicked;
		Refresh();
	}

	private void OnDisable()
	{
		uiItem.OnClick -= HandleClicked;
	}

	public void Refresh()
	{
		portraitSprite.SetSprite(unlockData.portraitSprite);
		newSprite.gameObject.SetActive(!Player.instance.pvpData.IsPortraitViewed(unlockData.portraitSprite));
		if (!Player.instance.pvpData.IsPortraitUnlocked(unlockData.portraitSprite))
		{
			lockSprite.color = Color.white;
			uiItem.enabled = true;
			switch (unlockData.unlockCondition)
			{
			case LegionOfDoomPortraitUnlock.UnlockCondition.Gold:
				portraitSprite.color = Color.white;
				costTextMesh.gameObject.SetActive(value: true);
				costTextMesh.text = unlockData.goldCost.ToString();
				coinTagObj.SetActive(value: true);
				rankTextMesh.gameObject.SetActive(value: false);
				questionMarkSprite.gameObject.SetActive(value: false);
				break;
			case LegionOfDoomPortraitUnlock.UnlockCondition.Level:
				uiItem.enabled = false;
				portraitSprite.color = Color.black;
				costTextMesh.gameObject.SetActive(value: false);
				coinTagObj.SetActive(value: false);
				rankTextMesh.gameObject.SetActive(value: true);
				rankTextMesh.text = "Rank " + unlockData.requiredLevel;
				questionMarkSprite.gameObject.SetActive(value: true);
				newSprite.gameObject.SetActive(value: false);
				break;
			case LegionOfDoomPortraitUnlock.UnlockCondition.LevelAndGold:
				if (Player.instance.pvpData.level < unlockData.requiredLevel)
				{
					uiItem.enabled = false;
					portraitSprite.color = Color.black;
					costTextMesh.gameObject.SetActive(value: false);
					coinTagObj.SetActive(value: false);
					rankTextMesh.gameObject.SetActive(value: true);
					rankTextMesh.text = "Rank " + unlockData.requiredLevel;
					questionMarkSprite.gameObject.SetActive(value: true);
					newSprite.gameObject.SetActive(value: false);
				}
				else
				{
					portraitSprite.color = Color.white;
					costTextMesh.gameObject.SetActive(value: true);
					costTextMesh.text = unlockData.goldCost.ToString();
					coinTagObj.SetActive(value: true);
					rankTextMesh.gameObject.SetActive(value: false);
					questionMarkSprite.gameObject.SetActive(value: false);
				}
				break;
			}
		}
		else
		{
			uiItem.enabled = true;
			lockSprite.color = Color.clear;
			portraitSprite.color = Color.white;
			costTextMesh.gameObject.SetActive(value: false);
			coinTagObj.SetActive(value: false);
			rankTextMesh.gameObject.SetActive(value: false);
			questionMarkSprite.gameObject.SetActive(value: false);
		}
	}

	private void HandleClicked()
	{
		if (unlockData != null)
		{
			Player.instance.pvpData.SetPortraitViewed(unlockData.portraitSprite, isViewed: true);
			Refresh();
		}
	}
}
