using TMPro;
using UnityEngine;

public class VersusScreen : MonoBehaviour
{
	public Animator animator;

	public tk2dSprite backingSprite;

	public MeshRenderer backingTopRenderer;

	public MeshRenderer backingBottomRenderer;

	public tk2dSlicedSprite stripSprite;

	public tk2dSlicedSprite stripSprite2;

	public tk2dSprite playerPortraitSprite;

	public TextMeshPro playerNameText;

	public tk2dSprite enemyPortraitSprite;

	public TextMeshPro enemyNameText;

	public tk2dSprite vsSprite;

	[Header("PvP Related Objects")]
	public TextMeshPro playerSkillRatingTextMesh;

	public TextMeshPro playerLevelTextMesh;

	public tk2dSprite playerPortraitBacking;

	public tk2dSprite playerRankPlateBacking;

	public tk2dSprite enemySkillRatingSprite;

	public TextMeshPro enemySkillRatingTextMesh;

	public TextMeshPro enemyLevelTextMesh;

	public tk2dSprite enemyPortraitBacking;

	public tk2dSprite enemyRankPlateBacking;
}
