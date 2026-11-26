using UnityEngine;
using UnityEngine.Purchasing;

public class IAPTestController : MonoBehaviour
{
	public CurrencyGUI currencyGUI;

	public IAPTestPurchaseButton[] purchaseButtons;

	private void Start()
	{
		currencyGUI.Init(Player.instance);
		currencyGUI.Show();
		if (!AppStoreTest.Instance.IsInitialized)
		{
			AppStoreTest.Instance.Initialized += AppStoreTest_Instance_Initialized;
		}
		else
		{
			RefreshProductList();
		}
	}

	private void OnEnable()
	{
		for (int i = 0; i < purchaseButtons.Length; i++)
		{
			purchaseButtons[i].uiItem.OnClickUIItem += HandlePurchaseButtonClick;
		}
	}

	private void OnDisable()
	{
		for (int i = 0; i < purchaseButtons.Length; i++)
		{
			purchaseButtons[i].uiItem.OnClickUIItem -= HandlePurchaseButtonClick;
		}
	}

	private void OnDestroy()
	{
		AppStoreTest.Instance.Initialized -= AppStoreTest_Instance_Initialized;
	}

	private void AppStoreTest_Instance_Initialized()
	{
		AppStoreTest.Instance.Initialized -= AppStoreTest_Instance_Initialized;
		RefreshProductList();
	}

	private void HandlePurchaseButtonClick(tk2dUIItem uiItem)
	{
		IAPTestPurchaseButton component = uiItem.GetComponent<IAPTestPurchaseButton>();
		if (component != null)
		{
			AppStoreTest.Instance.Controller.InitiatePurchase(component.product);
		}
	}

	private void RefreshProductList()
	{
		Product[] all = AppStoreTest.Instance.Controller.products.all;
		Product product = null;
		for (int i = 0; i < purchaseButtons.Length; i++)
		{
			if (i < all.Length)
			{
				purchaseButtons[i].SetProduct(all[i]);
				purchaseButtons[i].gameObject.SetActive(value: true);
			}
			else
			{
				purchaseButtons[i].SetProduct(null);
				purchaseButtons[i].gameObject.SetActive(value: false);
			}
		}
	}
}
