using ArabicSupport;
using UnityEngine;

public class SetArabicTextExample : MonoBehaviour
{
	public string text;

	private void Start()
	{
		gameObject.GetComponent<GUIText>().text = "This sentence (wrong display):\n" + text + "\n\nWill appear correctly as:\n" + ArabicFixer.Fix(text, showTashkeel: false, useHinduNumbers: false);
	}

	private void Update()
	{
	}
}
