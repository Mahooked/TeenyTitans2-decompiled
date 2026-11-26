using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using TMPro;
using UnityEngine;

public class CharacterSelectElevator : MonoBehaviour
{
	[Serializable]
	public struct CharacterElevatorAnimator
	{
		public CityCharacterType characterType;

		public AEControllerTk2d animator;

		public AudioClip clip;

		public AudioClip[] characterSelectedClips;
	}

	private Dictionary<CityCharacterType, string> characterSprites = new Dictionary<CityCharacterType, string>
	{
		{
			CityCharacterType.BeastBoy,
			"gui_popup_head_beastboy"
		},
		{
			CityCharacterType.Cyborg,
			"gui_popup_head_cyborg"
		},
		{
			CityCharacterType.Raven,
			"gui_popup_head_raven"
		},
		{
			CityCharacterType.Robin,
			"gui_popup_head_robin"
		},
		{
			CityCharacterType.Starfire,
			"gui_popup_head_starfire"
		}
	};

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action CharacterSelected__BackingField;

	public tk2dUIItem robinUIItem;

	public tk2dUIItem starfireUIItem;

	public tk2dUIItem ravenUIItem;

	public tk2dUIItem cyborgUIItem;

	public tk2dUIItem beastBoyUIItem;

	[Space]
	public GameObject headerText;

	public GameObject headerTitanText;

	public GameObject headerTR;

	public GameObject popUpWindow;

	public TextMeshPro characterNameTextMesh;

	public tk2dSprite characterSprite;

	public tk2dUIItem confirmBtn;

	public tk2dUIItem cancelBtn;

	public tk2dUIItem closeBtn;

	public tk2dUIItem outerCloseBtn;

	public AnimatorEventListener bannerEventListener;

	public CharacterElevatorAnimator[] characterAnimators;

	public RangedFloat rareIdleRate;

	private Animator popUpAnimator;

	private CityCharacterType selectedCharacter;

	private CharacterElevatorAnimator? lastCharacterPlayed;

	private float rareIdleTimer;

	private AudioSource characterSelectedSource;

	public static CharacterSelectElevator instance { get; private set; }

	public event Action CharacterSelected
	{
		add
		{
			Action action = CharacterSelected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = CharacterSelected__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		instance = this;
	}

	private void Start()
	{
		robinUIItem.OnClick += HandleRobinClicked;
		starfireUIItem.OnClick += HandleStarfireClicked;
		ravenUIItem.OnClick += HandleRavenClicked;
		cyborgUIItem.OnClick += HandleCyborgClicked;
		beastBoyUIItem.OnClick += HandleBeastBoyClicked;
		robinUIItem.enabled = false;
		starfireUIItem.enabled = false;
		ravenUIItem.enabled = false;
		cyborgUIItem.enabled = false;
		beastBoyUIItem.enabled = false;
		confirmBtn.OnClick += HandleConfirmClicked;
		cancelBtn.OnClick += HandleCancelClicked;
		closeBtn.OnClick += HandleCloseClicked;
		outerCloseBtn.OnClick += HandleCloseClicked;
		tk2dUIItem obj = confirmBtn;
		bool flag = false;
		cancelBtn.enabled = flag;
		obj.enabled = flag;
		popUpAnimator = popUpWindow.GetComponentInChildren<Animator>();
		bannerEventListener.EventTriggered += HandleBannerEvent;
		rareIdleTimer = rareIdleRate.GetRandomValue();
		GameObject gameObject = new GameObject("Character Selected AudioSource");
		characterSelectedSource = gameObject.AddComponent<AudioSource>();
		bool flag2 = Language.CurrentLanguage() == LanguageCode.TR;
		headerText.SetActive(!flag2);
		headerTitanText.SetActive(!flag2);
		headerTR.SetActive(flag2);
		if (Language.CurrentLanguage() == LanguageCode.PT || Language.CurrentLanguage() == LanguageCode.ES)
		{
			headerTitanText.transform.localPosition = new Vector3(headerTitanText.transform.position.x, -20f);
			RectTransform rectTransform = (RectTransform)headerTitanText.transform;
			rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 40f);
		}
	}

	private void OnDestroy()
	{
		robinUIItem.OnClick -= HandleRobinClicked;
		starfireUIItem.OnClick -= HandleStarfireClicked;
		ravenUIItem.OnClick -= HandleRavenClicked;
		cyborgUIItem.OnClick -= HandleCyborgClicked;
		beastBoyUIItem.OnClick -= HandleBeastBoyClicked;
		confirmBtn.OnClick -= HandleConfirmClicked;
		cancelBtn.OnClick -= HandleCancelClicked;
		outerCloseBtn.OnClick -= HandleCloseClicked;
		bannerEventListener.EventTriggered -= HandleBannerEvent;
	}

	private void Update()
	{
		rareIdleTimer -= Time.deltaTime;
		if (rareIdleTimer <= 0f)
		{
			rareIdleTimer = rareIdleRate.GetRandomValue();
			PlayCharacterRareIdle();
		}
	}

	private void PlayCharacterRareIdle()
	{
		List<CharacterElevatorAnimator> list = new List<CharacterElevatorAnimator>(characterAnimators);
		CharacterElevatorAnimator? characterElevatorAnimator = lastCharacterPlayed;
		if (characterElevatorAnimator.HasValue)
		{
			UnityEngine.Debug.Log(lastCharacterPlayed.Value.animator.gameObject.name + " played last. Remove them from list...");
			list.Remove(lastCharacterPlayed.Value);
		}
		CharacterElevatorAnimator value = list[UnityEngine.Random.Range(0, list.Count)];
		value.animator.Play("idleRare");
		value.animator.AddToQueue("idle");
		GFAudioManager.instance.PlayClip(value.clip, 1f, false, 0f, 128, GFAudioManager.VOICEOVER_GROUP);
		lastCharacterPlayed = value;
	}

	private void CharacterClicked(CityCharacterType character)
	{
		UnityEngine.Debug.Log(string.Concat(character, " clicked"));
		selectedCharacter = character;
		popUpWindow.SetActive(true);
		popUpAnimator.Play("CharacterSelector_Popup_Intro");
		characterSprite.SetSprite(characterSprites[character]);
		characterNameTextMesh.text = Language.Get(character.ToString().ToUpper(), "NPCS,FIGS");
		if (characterSelectedSource.clip != null && characterSelectedSource.isPlaying)
		{
			characterSelectedSource.Stop();
		}
		for (int i = 0; i < characterAnimators.Length; i++)
		{
			CharacterElevatorAnimator characterElevatorAnimator = characterAnimators[i];
			if (characterElevatorAnimator.characterType == character)
			{
				characterSelectedSource.clip = characterElevatorAnimator.characterSelectedClips[UnityEngine.Random.Range(0, characterElevatorAnimator.characterSelectedClips.Length)];
				characterSelectedSource.Play();
			}
		}
		StartCoroutine(WaitForAnimationEnd());
	}

	private IEnumerator WaitForAnimationEnd()
	{
		yield return null;
		yield return new WaitWhile(() => popUpAnimator.IsPlaying());
		tk2dUIItem obj = confirmBtn;
		bool flag = true;
		cancelBtn.enabled = flag;
		obj.enabled = flag;
	}

	private void HandleRobinClicked()
	{
		CharacterClicked(CityCharacterType.Robin);
	}

	private void HandleStarfireClicked()
	{
		CharacterClicked(CityCharacterType.Starfire);
	}

	private void HandleRavenClicked()
	{
		CharacterClicked(CityCharacterType.Raven);
	}

	private void HandleCyborgClicked()
	{
		CharacterClicked(CityCharacterType.Cyborg);
	}

	private void HandleBeastBoyClicked()
	{
		CharacterClicked(CityCharacterType.BeastBoy);
	}

	private void HandleConfirmClicked()
	{
		GameSkinManager.instance.ChangeGameSkin(selectedCharacter.ToString().ToUpper());
		HandleCloseClicked();
		if (CharacterSelected__BackingField != null)
		{
			CharacterSelected__BackingField();
		}
	}

	private void HandleCancelClicked()
	{
		popUpAnimator.Play("CharacterSelector_Popup_Outro");
		tk2dUIItem obj = confirmBtn;
		bool flag = false;
		cancelBtn.enabled = flag;
		obj.enabled = flag;
	}

	private void HandleCloseClicked()
	{
		HandleCancelClicked();
	}

	private void HandleBannerEvent(AnimationEvent animationEvent)
	{
		if (animationEvent.stringParameter == "bannerEnter")
		{
			robinUIItem.enabled = true;
			starfireUIItem.enabled = true;
			ravenUIItem.enabled = true;
			cyborgUIItem.enabled = true;
			beastBoyUIItem.enabled = true;
		}
	}
}
