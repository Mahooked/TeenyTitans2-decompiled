using UnityEngine;

public class FlashSaleSetup : MonoBehaviour
{
	private FlashSaleSetupObj[] flashSaleObjs;

	public FlashSaleSetupObj[] FlashSaleObjs
	{
		get
		{
			return flashSaleObjs;
		}
	}

	private void Start()
	{
		flashSaleObjs = GetComponentsInChildren<FlashSaleSetupObj>(true);
	}
}
