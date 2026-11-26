using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
	private static Dictionary<CharacterClass, string> damageMaterialPaths = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"GrilledCheese_Damagetext_ClassBonus_beast"
		},
		{
			CharacterClass.Cute,
			"GrilledCheese_Damagetext_ClassBonus_cute"
		},
		{
			CharacterClass.DarkArts,
			"GrilledCheese_Damagetext_ClassBonus_dark"
		},
		{
			CharacterClass.MartialArts,
			"GrilledCheese_Damagetext_ClassBonus_martialarts"
		},
		{
			CharacterClass.Super,
			"GrilledCheese_Damagetext_ClassBonus_super"
		},
		{
			CharacterClass.Tech,
			"GrilledCheese_Damagetext_ClassBonus_tech"
		},
		{
			CharacterClass.None,
			"GrilledCheese_Damagetext_ClassBonus_tech"
		}
	};

	private static Dictionary<CharacterClass, string> wordMaterialPaths = new Dictionary<CharacterClass, string>
	{
		{
			CharacterClass.Beast,
			"baveuse_damagetext_classbonus_beast"
		},
		{
			CharacterClass.Cute,
			"baveuse_damagetext_classbonus_cute"
		},
		{
			CharacterClass.DarkArts,
			"baveuse_damagetext_classbonus_dark"
		},
		{
			CharacterClass.MartialArts,
			"baveuse_damagetext_classbonus_martialarts"
		},
		{
			CharacterClass.Super,
			"baveuse_damagetext_classbonus_super"
		},
		{
			CharacterClass.Tech,
			"baveuse_damagetext_classbonus_tech"
		},
		{
			CharacterClass.None,
			"baveuse_damagetext_classbonus_tech"
		}
	};

	private static string healMaterialPath = "Materials/Fonts/GrilledCheese_Healtext";

	private static Material damageMaterial = null;

	private static Dictionary<string, Material> loadedMaterials = new Dictionary<string, Material>();

	public GameObject damage;

	public GameObject luckyDuck;

	public GameObject classBonus;

	public ParticleSystem particleSystem;

	public Animator damageAnimator;

	public Animator luckyDuckAnimator;

	public Animator classBonusAnimator;

	public TextMeshPro damageText;

	public TextMeshPro luckyDuckDamageText;

	public TextMeshPro classDamageText;

	public TextMeshPro classWord;

	public LocalizedText localizedDamageText;

	public LocalizedText localizedClassWord;

	public float lifetime = 3f;

	private float deathCounter;

	private string damageAnimationToPlay = string.Empty;

	private void Awake()
	{
		if (damageMaterial == null)
		{
			damageMaterial = damageText.fontSharedMaterial;
		}
	}

	private void OnEnable()
	{
		deathCounter = lifetime;
		localizedDamageText.ForceInitFontProperties();
		localizedDamageText.UpdateFont();
		localizedClassWord.ForceInitFontProperties();
		localizedClassWord.UpdateFont();
		if (!string.IsNullOrEmpty(damageAnimationToPlay))
		{
			damageAnimator.Play(damageAnimationToPlay);
			damageAnimationToPlay = string.Empty;
		}
	}

	private void OnDisable()
	{
	}

	private void OnDestroy()
	{
		List<Material> list = new List<Material>(loadedMaterials.Values);
		int count = list.Count;
		for (int i = 0; i < count; i++)
		{
			Resources.UnloadAsset(list[i]);
		}
		loadedMaterials.Clear();
	}

	private void Update()
	{
		if (!(deathCounter > 0f))
		{
			return;
		}
		deathCounter -= Time.deltaTime;
		if (deathCounter <= 0f)
		{
			if (damage.activeSelf)
			{
				damageAnimator.Play("Default", 0, 0f);
			}
			if (luckyDuck.activeSelf)
			{
				luckyDuckAnimator.Play("Default", 0, 0f);
			}
			if (classBonus.activeSelf)
			{
				classBonusAnimator.Play("Default", 0, 0f);
			}
			ObjectPool.PoolObject(base.gameObject);
		}
	}

	public void Init(string text)
	{
		damageText.fontSharedMaterial = damageMaterial;
		damageText.text = text;
		damage.SetActive(true);
		luckyDuck.SetActive(false);
		classBonus.SetActive(false);
		particleSystem.gameObject.SetActive(false);
		localizedDamageText.ForceInitFontProperties();
		localizedDamageText.UpdateFont();
		localizedClassWord.ForceInitFontProperties();
		localizedClassWord.UpdateFont();
	}

	public void Init(HealResult healResult)
	{
		damageText.fontSharedMaterial = GetHealMaterial();
		damageText.text = ((!healResult.full) ? healResult.amount.ToString() : Language.Get("FULL", "MENUS,GUI"));
		damage.SetActive(true);
		luckyDuck.SetActive(false);
		classBonus.SetActive(false);
		particleSystem.gameObject.SetActive(false);
		localizedDamageText.ForceInitFontProperties();
		localizedDamageText.UpdateFont();
		localizedClassWord.ForceInitFontProperties();
		localizedClassWord.UpdateFont();
	}

	public void Init(DamageResult damageResult, string animName = "Default")
	{
		if (damageResult.luckBonusDamage > 0)
		{
			damage.SetActive(false);
			luckyDuck.SetActive(true);
			luckyDuckDamageText.text = (damageResult.damage + damageResult.luckBonusDamage).ToString();
		}
		else
		{
			luckyDuck.SetActive(false);
			damage.SetActive(true);
			damageText.fontSharedMaterial = damageMaterial;
			damageText.text = damageResult.damage.ToString();
			damageAnimationToPlay = animName;
		}
		if (damageResult.classBonusDamage > 0)
		{
			classDamageText.fontSharedMaterial = GetDamageMaterial(damageResult.bonusClass);
			classWord.fontSharedMaterial = GetWordMaterial(damageResult.bonusClass);
			classDamageText.text = "+" + damageResult.classBonusDamage;
			classWord.text = GameManager.instance.gameSettings.GetRandomClassPhrase(damageResult.bonusClass);
			GFAudioManager.instance.PlayClipFromResources(GameManager.instance.gameSettings.GetRandomClassSound(damageResult.bonusClass), GFAudioManager.SFX_GROUP, string.Empty);
			classBonus.SetActive(true);
			particleSystem.gameObject.SetActive(true);
		}
		else
		{
			classBonus.SetActive(false);
			particleSystem.gameObject.SetActive(false);
		}
		localizedDamageText.ForceInitFontProperties();
		localizedDamageText.UpdateFont();
		localizedClassWord.ForceInitFontProperties();
		localizedClassWord.UpdateFont();
	}

	private Material GetHealMaterial()
	{
		if (!loadedMaterials.ContainsKey(healMaterialPath))
		{
			loadedMaterials.Add(healMaterialPath, Resources.Load<Material>(healMaterialPath));
		}
		return loadedMaterials[healMaterialPath];
	}

	private Material GetDamageMaterial(CharacterClass characterClass)
	{
		string text = damageMaterialPaths[characterClass];
		if (!loadedMaterials.ContainsKey(text))
		{
			loadedMaterials.Add(text, Resources.Load<Material>("Materials/Fonts/" + text));
		}
		return loadedMaterials[text];
	}

	private Material GetWordMaterial(CharacterClass characterClass)
	{
		string text = wordMaterialPaths[characterClass];
		if (!loadedMaterials.ContainsKey(text))
		{
			loadedMaterials.Add(text, Resources.Load<Material>("Materials/Fonts/" + text));
		}
		return loadedMaterials[text];
	}
}
