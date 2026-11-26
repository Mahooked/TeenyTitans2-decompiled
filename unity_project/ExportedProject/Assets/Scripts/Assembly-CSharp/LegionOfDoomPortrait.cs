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
				costTextMesh.gameObject.SetActive(true);
				costTextMesh.text = unlockData.goldCost.ToString();
				coinTagObj.SetActive(true);
				rankTextMesh.gameObject.SetActive(false);
				questionMarkSprite.gameObject.SetActive(false);
				break;
			case LegionOfDoomPortraitUnlock.UnlockCondition.Level:
				uiItem.enabled = false;
				portraitSprite.color = Color.black;
				costTextMesh.gameObject.SetActive(false);
				coinTagObj.SetActive(false);
				rankTextMesh.gameObject.SetActive(true);
				rankTextMesh.text = "Rank " + unlockData.requiredLevel;
				questionMarkSprite.gameObject.SetActive(true);
				newSprite.gameObject.SetActive(false);
				break;
			case LegionOfDoomPortraitUnlock.UnlockCondition.LevelAndGold:
				if (Player.instance.pvpData.level < unlockData.requiredLevel)
				{
					uiItem.enabled = false;
					portraitSprite.color = Color.black;
					costTextMesh.gameObject.SetActive(false);
					coinTagObj.SetActive(false);
					rankTextMesh.gameObject.SetActive(true);
					rankTextMesh.text = "Rank " + unlockData.requiredLevel;
					questionMarkSprite.gameObject.SetActive(true);
					newSprite.gameObject.SetActive(false);
				}
				else
				{
					portraitSprite.color = Color.white;
					costTextMesh.gameObject.SetActive(true);
					costTextMesh.text = unlockData.goldCost.ToString();
					coinTagObj.SetActive(true);
					rankTextMesh.gameObject.SetActive(false);
					questionMarkSprite.gameObject.SetActive(false);
				}
				break;
			}
		}
		else
		{
			uiItem.enabled = true;
			lockSprite.color = Color.clear;
			portraitSprite.color = Color.white;
			costTextMesh.gameObject.SetActive(false);
			coinTagObj.SetActive(false);
			rankTextMesh.gameObject.SetActive(false);
			questionMarkSprite.gameObject.SetActive(false);
		}
	}

	private void HandleClicked()
	{
		if (unlockData != null)
		{
			Player.instance.pvpData.SetPortraitViewed(unlockData.portraitSprite, true);
			Refresh();
		}
	}
}
