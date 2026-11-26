using UnityEngine;

public class ShopCart : MonoBehaviour
{
	public static ShopCart instance;

	public Camera camera;

	public GameObject cartGroup;

	public tk2dSprite darknessSprite;

	public Transform[] figureCartPositions;

	public Transform viewingTransform;

	public Transform glassViewingTransform;

	public Transform rotatingTransform;

	public Transform mainCameraPostion;

	private void Awake()
	{
		instance = this;
	}

	private void OnDestroy()
	{
		if (instance != null && instance == this)
		{
			instance = null;
		}
	}
}
