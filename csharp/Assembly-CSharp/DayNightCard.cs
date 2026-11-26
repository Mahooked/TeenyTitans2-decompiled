using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCard : MonoBehaviour
{
	public static DayNightCard instance;

	public Animator animator;

	public tk2dSprite card;

	public Color dayWaterColor;

	public Color nightWaterColor;

	public Color nightCharacterColor;

	public Renderer waterRenderer;

	public Material[] materials;

	public List<AEControllerTk2d> charactersToTint = new List<AEControllerTk2d>();

	public List<tk2dSprite> spritesToTint = new List<tk2dSprite>();

	public tk2dSpriteCollectionData dayPlateSpriteCollectionData;

	public tk2dSpriteCollectionData nightPlateSpriteCollectionData;

	private bool isDayTime;

	private Dictionary<string, List<Material>> textureMaterialSets = new Dictionary<string, List<Material>>();

	private bool _isActive;

	public bool isActive => _isActive;

	private void Awake()
	{
		instance = this;
		for (int i = 0; i < materials.Length; i++)
		{
			Material material = materials[i];
			if (material.mainTexture != null)
			{
				string text = material.mainTexture.name.Replace("_Night", string.Empty);
				text = text.Replace("-1x", string.Empty);
				text = text.Replace("-2x", string.Empty);
				text = text.Replace("-4x", string.Empty);
				if (textureMaterialSets.ContainsKey(text))
				{
					textureMaterialSets[text].Add(material);
					continue;
				}
				textureMaterialSets.Add(text, new List<Material> { material });
			}
		}
	}

	private void OnDestroy()
	{
		instance = null;
		charactersToTint.Clear();
		spritesToTint.Clear();
		for (int i = 0; i < materials.Length; i++)
		{
			Material material = materials[i];
			if (material != null && material.mainTexture != null)
			{
				Resources.UnloadAsset(material.mainTexture);
			}
		}
	}

	public void RegisterCharacterToTint(AEControllerTk2d aeControllerTk2d)
	{
		if (!charactersToTint.Contains(aeControllerTk2d))
		{
			charactersToTint.Add(aeControllerTk2d);
		}
	}

	public void RegisterSpriteToTint(tk2dSprite sprite)
	{
		if (!spritesToTint.Contains(sprite))
		{
			spritesToTint.Add(sprite);
		}
	}

	public void ShowNightCard()
	{
		_isActive = true;
		if (CityController.instance != null)
		{
			CityController.instance.viewDeobstructer.paused = true;
		}
		card.SetSprite(nightPlateSpriteCollectionData, "bg_night_1");
		isDayTime = false;
		StartCoroutine("PlayAnimation");
	}

	public void ShowDayCard()
	{
		_isActive = true;
		card.SetSprite(dayPlateSpriteCollectionData, "bg_day_1");
		isDayTime = true;
		StartCoroutine("PlayAnimation");
	}

	public void UpdateForTimeOfDay(bool dayTime)
	{
		isDayTime = dayTime;
		if (dayTime)
		{
			for (int i = 0; i < charactersToTint.Count; i++)
			{
				AEControllerTk2d aEControllerTk2d = charactersToTint[i];
				if (aEControllerTk2d != null)
				{
					aEControllerTk2d.animatorTk2d.SetColor(new Color(1f, 1f, 1f));
				}
			}
			for (int j = 0; j < spritesToTint.Count; j++)
			{
				if (spritesToTint[j] != null)
				{
					spritesToTint[j].color = new Color(1f, 1f, 1f);
				}
			}
			if (waterRenderer != null)
			{
				waterRenderer.material.color = dayWaterColor;
			}
		}
		else
		{
			for (int k = 0; k < charactersToTint.Count; k++)
			{
				AEControllerTk2d aEControllerTk2d2 = charactersToTint[k];
				if (aEControllerTk2d2 != null)
				{
					aEControllerTk2d2.animatorTk2d.SetColor(nightCharacterColor);
				}
			}
			for (int l = 0; l < spritesToTint.Count; l++)
			{
				if (spritesToTint[l] != null)
				{
					spritesToTint[l].color = nightCharacterColor;
				}
			}
			if (waterRenderer != null)
			{
				waterRenderer.material.color = nightWaterColor;
			}
		}
		if (CityController.instance != null)
		{
			CityController.instance.viewDeobstructer.ResetAllObstructionsNow();
		}
		List<string> list = new List<string>(textureMaterialSets.Keys);
		for (int m = 0; m < list.Count; m++)
		{
			string text = list[m];
			if (!dayTime)
			{
				text += "_Night";
			}
			text = text + "-" + tk2dSystem.CurrentPlatform;
			List<Material> list2 = textureMaterialSets[list[m]];
			Texture texture = ((list2 == null || !(list2[0] != null)) ? null : list2[0].mainTexture);
			if (!(texture != null) || !(texture.name != text))
			{
				continue;
			}
			Texture texture2 = Resources.Load<Texture>("Textures/" + text);
			if (!(texture2 != null))
			{
				continue;
			}
			if (texture != null)
			{
				Resources.UnloadAsset(texture);
			}
			for (int n = 0; n < list2.Count; n++)
			{
				Material material = list2[n];
				if (material != null)
				{
					material.mainTexture = texture2;
				}
			}
		}
		if (CityController.instance != null)
		{
			CityController.instance.viewDeobstructer.paused = false;
		}
	}

	public void AddAnimationControllerToTint(AEControllerTk2d animationController)
	{
		if (!charactersToTint.Contains(animationController))
		{
			charactersToTint.Add(animationController);
		}
		animationController.animatorTk2d.SetColor((!isDayTime) ? nightCharacterColor : new Color(1f, 1f, 1f));
	}

	public void RemoveAnimationControllerToTint(AEControllerTk2d animationController)
	{
		charactersToTint.Remove(animationController);
		charactersToTint.TrimExcess();
	}

	public void AddSpriteToTint(tk2dSprite sprite)
	{
		if (!spritesToTint.Contains(sprite))
		{
			spritesToTint.Add(sprite);
		}
		sprite.color = ((!isDayTime) ? nightCharacterColor : new Color(1f, 1f, 1f));
	}

	public void RemoveSpriteToTint(tk2dSprite sprite)
	{
		if (spritesToTint.Contains(sprite))
		{
			spritesToTint.Remove(sprite);
			spritesToTint.TrimExcess();
		}
	}

	private IEnumerator PlayAnimation()
	{
		if (CityController.instance != null)
		{
			CityController.instance.cityCharacter.StopMove();
		}
		animator.Play("DayNight_Titlecard_AnimTemp", 0, 0f);
		card.gameObject.SetActive(value: true);
		yield return new WaitForSeconds(0.5f);
		UpdateForTimeOfDay(isDayTime);
		while (animator.NormalizedTime() < 0.99f)
		{
			yield return new WaitForEndOfFrame();
		}
		_isActive = false;
		card.gameObject.SetActive(value: false);
		dayPlateSpriteCollectionData.UnloadTextures();
		dayPlateSpriteCollectionData.ResetPlatformData();
		nightPlateSpriteCollectionData.UnloadTextures();
		nightPlateSpriteCollectionData.ResetPlatformData();
		yield return new WaitForEndOfFrame();
		yield return Resources.UnloadUnusedAssets();
	}
}
