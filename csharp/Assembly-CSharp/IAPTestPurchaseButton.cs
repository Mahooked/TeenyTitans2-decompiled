using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPTestPurchaseButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public TextMeshPro nameText;

	public TextMeshPro priceText;

	private Product _product;

	public Product product
	{
		get
		{
			return _product;
		}
		set
		{
			_product = value;
		}
	}

	public void SetProduct(Product product)
	{
		if (product != _product)
		{
			_product = product;
			RefreshButton();
		}
	}

	private void RefreshButton()
	{
		if (product != null)
		{
			nameText.text = product.metadata.localizedTitle;
			priceText.text = product.metadata.localizedPriceString;
		}
		else
		{
			nameText.text = string.Empty;
			priceText.text = string.Empty;
		}
	}
}
