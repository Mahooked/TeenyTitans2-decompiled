using TMPro;
using UnityEngine;

public class NewFigureIndicator : MonoBehaviour
{
	public TextMeshPro numberText;

	private MeshRenderer meshRenderer;

	private Player player;

	private void Awake()
	{
		meshRenderer = GetComponent<MeshRenderer>();
		player = Player.instance;
	}

	private void OnEnable()
	{
		player.CharacterCollectionChanged += UpdateNewFiguresAmount;
		UpdateNewFiguresAmount();
	}

	private void OnDisable()
	{
		player.CharacterCollectionChanged -= UpdateNewFiguresAmount;
	}

	private void UpdateNewFiguresAmount()
	{
		if (numberText != null)
		{
			int totalNewCharacters = player.totalNewCharacters;
			if (totalNewCharacters > 0)
			{
				meshRenderer.enabled = true;
				numberText.gameObject.SetActive(value: true);
				numberText.text = totalNewCharacters.ToString();
			}
			else
			{
				meshRenderer.enabled = false;
				numberText.gameObject.SetActive(value: false);
			}
		}
	}
}
