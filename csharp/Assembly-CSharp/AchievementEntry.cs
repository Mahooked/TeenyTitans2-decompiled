using System.Text;
using TMPro;
using UnityEngine;

public class AchievementEntry : MonoBehaviour
{
	public tk2dSprite iconSprite;

	public tk2dBaseSprite bgSprite;

	public TextMeshPro titleTextMesh;

	public TextMeshPro descriptionTextMesh;

	public GameObject questionMarkObj;

	public void Init(AchievementData achievementData)
	{
		if (achievementData != null)
		{
			iconSprite.SetSprite(achievementData.icon);
			StringBuilder text = new StringBuilder(Language.Get(achievementData.id, "ABILS,CHIPS,ACHVS,LDBS"));
			titleTextMesh.SetText(text);
			StringBuilder text2 = new StringBuilder(Language.Get(achievementData.id + "_DESC", "ABILS,CHIPS,ACHVS,LDBS"));
			descriptionTextMesh.SetText(text2);
			if (!GameManager.instance.achievementsDatabase.IsAchievementUnlocked(achievementData.id))
			{
				iconSprite.color = Color.black;
				tk2dBaseSprite tk2dBaseSprite2 = bgSprite;
				Color gray = Color.gray;
				descriptionTextMesh.color = gray;
				gray = gray;
				titleTextMesh.color = gray;
				tk2dBaseSprite2.color = gray;
				questionMarkObj.SetActive(value: true);
			}
			else
			{
				tk2dSprite tk2dSprite2 = iconSprite;
				Color gray = Color.white;
				descriptionTextMesh.color = gray;
				gray = gray;
				titleTextMesh.color = gray;
				gray = gray;
				bgSprite.color = gray;
				tk2dSprite2.color = gray;
				questionMarkObj.SetActive(value: false);
			}
		}
	}
}
