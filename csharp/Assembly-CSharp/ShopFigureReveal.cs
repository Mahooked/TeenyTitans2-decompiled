using TMPro;
using UnityEngine;

public class ShopFigureReveal : MonoBehaviour
{
	public ShopToyBox figureRevealToyBox;

	public TextMeshPro figuresDiscoveredAmt;

	public TextMeshPro figuredRevealName;

	public Animator figureRevealAnimator;

	public Animator figureRevealScreenAnimator;

	public Animator figureRevealBoxGroupAnimator;

	public GameObject newFigureBacking;

	public GameObject[] nonBlindBoxObjects;

	public GameObject[] blindBoxObjects;

	[HideInInspector]
	public bool waitToOpenBox;

	public MeshRenderer[] blindBoxRenderers;

	public bool isGoldBox;

	private void Update()
	{
		if (waitToOpenBox && Input.GetMouseButtonDown(0))
		{
			figureRevealBoxGroupAnimator.Play("NewFigureReveal_SelectBox");
		}
		if (Input.GetMouseButtonUp(0))
		{
			waitToOpenBox = false;
		}
	}

	public void MakeBlindBox()
	{
		if (blindBoxRenderers.Length > 0)
		{
			for (int i = 0; i < blindBoxRenderers.Length; i++)
			{
				MeshRenderer meshRenderer = blindBoxRenderers[i];
				if (isGoldBox)
				{
					if (!meshRenderer.material.mainTexture.name.Contains("Gold"))
					{
						Texture texture = Resources.Load<Texture>("Textures/BlindBox_GoldTexture-" + tk2dSystem.CurrentPlatform);
						if (texture != null)
						{
							meshRenderer.material.mainTexture = texture;
						}
					}
				}
				else if (meshRenderer.material.mainTexture.name.Contains("Gold"))
				{
					Texture texture2 = Resources.Load<Texture>("Textures/BlindBox_Texture-" + tk2dSystem.CurrentPlatform);
					if (texture2 != null)
					{
						meshRenderer.material.mainTexture = texture2;
					}
				}
			}
		}
		for (int j = 0; j < blindBoxObjects.Length; j++)
		{
			blindBoxObjects[j].SetActive(value: true);
		}
		for (int k = 0; k < nonBlindBoxObjects.Length; k++)
		{
			nonBlindBoxObjects[k].SetActive(value: false);
		}
	}

	public void RemoveBlindBox()
	{
		for (int i = 0; i < blindBoxObjects.Length; i++)
		{
			blindBoxObjects[i].SetActive(value: false);
		}
		for (int j = 0; j < nonBlindBoxObjects.Length; j++)
		{
			nonBlindBoxObjects[j].SetActive(value: true);
		}
	}
}
