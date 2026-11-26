using System.Collections;
using TMPro;
using UnityEngine;

public class BattleCountdown : MonoBehaviour
{
	public Animator animator;

	public TextMeshPro numberText;

	public tk2dUIItem uiItem;

	private CountdownSoundSet soundSet;

	private bool _isBonusTap;

	private bool _allowBonusTapping = true;

	public bool isBonusTap => _isBonusTap;

	public bool allowBonusTapping
	{
		get
		{
			return _allowBonusTapping;
		}
		set
		{
			_allowBonusTapping = value;
		}
	}

	private void Awake()
	{
		animator.gameObject.SetActive(value: false);
	}

	public void Init(CountdownSoundSet soundSet)
	{
		this.soundSet = soundSet;
	}

	public void ShowNumber(int number)
	{
		StopAllCoroutines();
		StartCoroutine(NumberSequence(number));
	}

	public void ShowGo()
	{
		StartCoroutine(GoSequence());
	}

	private IEnumerator NumberSequence(int number)
	{
		numberText.text = number.ToString();
		animator.Play("Countdown", 0, 0f);
		animator.gameObject.SetActive(value: true);
		if (soundSet != null)
		{
			GFAudioManager.instance.PlayClipFromResources(soundSet.GetNumberPath(number), GFAudioManager.VOICEOVER_GROUP, string.Empty);
		}
		while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			yield return null;
		}
		animator.gameObject.SetActive(value: false);
	}

	private IEnumerator GoSequence()
	{
		numberText.text = Language.Get("GO", "MENUS,GUI");
		_isBonusTap = false;
		animator.gameObject.SetActive(value: true);
		animator.Play("GO", 0, 0f);
		yield return null;
		if (soundSet != null)
		{
			GFAudioManager.instance.PlayClipFromResources(soundSet.GetRandomGoPath(), GFAudioManager.VOICEOVER_GROUP, string.Empty);
		}
		while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
		{
			if (!_isBonusTap && Input.GetMouseButtonDown(0) && _allowBonusTapping)
			{
				_isBonusTap = true;
				animator.Play("Activated", 0, 0f);
			}
			yield return null;
		}
		animator.gameObject.SetActive(value: false);
	}
}
