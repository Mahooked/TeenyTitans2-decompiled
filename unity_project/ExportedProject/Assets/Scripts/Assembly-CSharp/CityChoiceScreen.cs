using UnityEngine;

public class CityChoiceScreen : MonoBehaviour
{
	public tk2dUIItem gothamButton;

	public tk2dUIItem metropolisButton;

	public GameObject headerObject;

	public AudioClip choiceAudio;

	public bool gothamChosen { get; set; }

	public bool metropolisChosen { get; set; }

	private void OnEnable()
	{
		gothamButton.OnClick += GothamButton_OnClick;
		metropolisButton.OnClick += MetropolisButton_OnClick;
	}

	public void ShowHeader()
	{
		if (headerObject != null)
		{
			headerObject.SetActive(true);
		}
	}

	public void HideHeader()
	{
		if (headerObject != null)
		{
			headerObject.SetActive(false);
		}
	}

	private void OnDisable()
	{
		gothamButton.OnClick -= GothamButton_OnClick;
		metropolisButton.OnClick -= MetropolisButton_OnClick;
	}

	public void PlayOutro()
	{
		GetComponent<Animator>().Play("cityChoiceScreen_outro");
	}

	private void GothamButton_OnClick()
	{
		Debug.Log("GOTHAM");
		gothamChosen = true;
		GFAudioManager.instance.PlayClip(choiceAudio, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
	}

	private void MetropolisButton_OnClick()
	{
		Debug.Log("METROPOLIS");
		GFAudioManager.instance.PlayClip(choiceAudio, 1f, false, 0f, 128, GFAudioManager.SFX_GROUP);
	}
}
