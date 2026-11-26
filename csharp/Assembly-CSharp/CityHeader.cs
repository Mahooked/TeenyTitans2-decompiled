using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CityHeader : MonoBehaviour
{
	public Dictionary<CityLocation, string> cityLocationSpriteNames = new Dictionary<CityLocation, string>
	{
		{
			CityLocation.JumpCity,
			"city_header_jumpcity"
		},
		{
			CityLocation.MovieLot,
			"city_header_movielot"
		},
		{
			CityLocation.Gotham,
			"city_header_gotham"
		},
		{
			CityLocation.Arkham,
			"city_header_arkham"
		},
		{
			CityLocation.WayneManor,
			"city_header_waynemanor"
		},
		{
			CityLocation.Metropolis,
			"city_header_metropolis"
		},
		{
			CityLocation.HallOfJustice,
			"city_header_hallofjustice"
		},
		{
			CityLocation.Watchtower,
			"city_header_watchtower"
		},
		{
			CityLocation.Beach,
			"city_header_thebeach"
		}
	};

	public Dictionary<CityLocation, string> cityLocationNames = new Dictionary<CityLocation, string>
	{
		{
			CityLocation.JumpCity,
			"JUMPCITY"
		},
		{
			CityLocation.MovieLot,
			"MOVIELOT"
		},
		{
			CityLocation.Gotham,
			"GOTHAMCITY"
		},
		{
			CityLocation.Arkham,
			"ARKHAMASYLUM"
		},
		{
			CityLocation.WayneManor,
			"WAYNEMANOR"
		},
		{
			CityLocation.Metropolis,
			"METROPOLIS"
		},
		{
			CityLocation.HallOfJustice,
			"HALLOFJUSTICE"
		},
		{
			CityLocation.Watchtower,
			"THEWATCHTOWER"
		},
		{
			CityLocation.Beach,
			"THEBEACH"
		}
	};

	public Animator animator;

	public tk2dBaseSprite headerSprite;

	public TextMeshPro headerText;

	private void OnEnable()
	{
		animator.gameObject.SetActive(value: false);
	}

	private void Start()
	{
		CityController.instance.OnCityLocationChanged += HandleCityLocationChanged;
	}

	private void OnDestroy()
	{
		if (CityController.instance != null)
		{
			CityController.instance.OnCityLocationChanged -= HandleCityLocationChanged;
		}
	}

	private void HandleCityLocationChanged(CityLocation cityLocation)
	{
		if ((TitleScreenController.instance != null && TitleScreenController.instance.titleScreenActive) || cityLocation == CityLocation.None || !gameObject.activeInHierarchy || CityScriptedSequence.scriptedSequenceActive)
		{
			animator.gameObject.SetActive(value: false);
			return;
		}
		headerSprite.SetSprite(cityLocationSpriteNames[cityLocation]);
		headerText.text = Language.Get(cityLocationNames[cityLocation], "MENUS,GUI");
		StartCoroutine(AnimationSequence());
	}

	private IEnumerator AnimationSequence()
	{
		while (!LoadingScreenCover.outroFinished)
		{
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		animator.gameObject.SetActive(value: true);
		yield return null;
		yield return new WaitWhile(() => animator.IsPlaying());
		animator.gameObject.SetActive(value: false);
	}
}
