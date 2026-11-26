using UnityEngine;

public class FlashSaleSetup : MonoBehaviour
{
	private FlashSaleSetupObj[] flashSaleObjs;

	public FlashSaleSetupObj[] FlashSaleObjs => flashSaleObjs;

	private void Start()
	{
		flashSaleObjs = GetComponentsInChildren<FlashSaleSetupObj>(includeInactive: true);
	}
}
