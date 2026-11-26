using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;

public class RiddlerShopProductButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public tk2dUIItem helpButton;

	public TextMeshPro headerText;

	public TextMeshPro descriptionText;

	public TextMeshPro amountText;

	public TextMeshPro priceText;

	private IAPProduct _product;

	public IAPProduct product
	{
		get
		{
			return _product;
		}
		set
		{
			Init(value);
		}
	}

	private void OnEnable()
	{
		uiItem.OnClick += UiItem_OnClick;
		if (helpButton != null)
		{
			helpButton.OnClick += HelpButton_OnClick;
		}
	}

	private void OnDisable()
	{
		uiItem.OnClick -= UiItem_OnClick;
		if (helpButton != null)
		{
			helpButton.OnClick -= HelpButton_OnClick;
		}
	}

	public virtual void Init(IAPProduct product)
	{
		_product = product;
		if (_product.name != null && !_product.name.IsNullOrEmpty)
		{
			headerText.text = ((!string.IsNullOrEmpty(product.name.key)) ? Language.Get(product.name.key, "MENUS,GUI") : product.name.text);
		}
		if (_product.description != null && !_product.description.IsNullOrEmpty)
		{
			descriptionText.text = ((!string.IsNullOrEmpty(product.description.key)) ? Language.Get(product.description.key, "MENUS,GUI") : product.description.text);
		}
		Product product2 = IAPStore.Instance.Controller.products.WithID(_product.ProductID);
		if (product2 != null)
		{
			priceText.text = product2.metadata.localizedPriceString;
		}
	}

	private void UiItem_OnClick()
	{
	}

	private void HelpButton_OnClick()
	{
	}
}
