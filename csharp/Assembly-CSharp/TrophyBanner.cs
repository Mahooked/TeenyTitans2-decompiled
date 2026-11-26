using TMPro;
using UnityEngine;

public class TrophyBanner : MonoBehaviour
{
	public tk2dBaseSprite bannerBacking;

	public tk2dBaseSprite bannerTopper;

	public TextMeshPro title;

	private Material bannerTextMaterial;

	public void SetBanner(TournamentDefinition tournament)
	{
		bannerBacking.SetSprite(tournament.bannerSprite);
		title.text = Language.Get(tournament.tournamentNameKey, "MENUS,GUI");
		if (bannerTextMaterial == null)
		{
			bannerTextMaterial = Resources.Load<Material>(tournament.bannerTextMaterial);
		}
		if (bannerTextMaterial != null)
		{
			title.fontMaterial = bannerTextMaterial;
		}
		bannerTopper.SetSprite(tournament.bannerTopperSprite);
	}

	public void SetUnlocked(bool unlocked)
	{
		tk2dBaseSprite tk2dBaseSprite2 = bannerBacking;
		Color color = ((!unlocked) ? Color.black : Color.white);
		bannerTopper.color = color;
		tk2dBaseSprite2.color = color;
		title.gameObject.SetActive(unlocked);
	}
}
