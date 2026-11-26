using System;
using TMPro;

public class RiddlerShopProductWindow : RiddlerShopBaseWindow
{
	public TextMeshPro headerText;

	public RiddlerShopProductButton[] productButtons;

	public event Action<IAPProduct> ProductSelected;

	public event Action<IAPProduct> HelpRequested;

	protected override void OnEnable()
	{
		base.OnEnable();
		for (int i = 0; i < productButtons.Length; i++)
		{
			productButtons[i].uiItem.OnClickUIItem += HandleOnProductClick;
			if (productButtons[i].helpButton != null)
			{
				productButtons[i].helpButton.OnClickUIItem += HandleOnHelpRequested;
			}
		}
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		for (int i = 0; i < productButtons.Length; i++)
		{
			productButtons[i].uiItem.OnClickUIItem -= HandleOnProductClick;
			if (productButtons[i].helpButton != null)
			{
				productButtons[i].helpButton.OnClickUIItem -= HandleOnHelpRequested;
			}
		}
	}

	public void Init(IAPProduct[] products)
	{
		for (int i = 0; i < productButtons.Length; i++)
		{
			if (i < products.Length)
			{
				productButtons[i].gameObject.SetActive(value: true);
				productButtons[i].Init(products[i]);
			}
			else
			{
				productButtons[i].gameObject.SetActive(value: false);
			}
		}
	}

	private void HandleOnProductClick(tk2dUIItem obj)
	{
		RiddlerShopProductButton component = obj.GetComponent<RiddlerShopProductButton>();
		if (component != null && ProductSelected != null)
		{
			ProductSelected(component.product);
		}
	}

	private void HandleOnHelpRequested(tk2dUIItem uiItem)
	{
		RiddlerShopProductButton componentInParent = uiItem.GetComponentInParent<RiddlerShopProductButton>();
		if (componentInParent != null && HelpRequested != null)
		{
			HelpRequested(componentInParent.product);
		}
	}
}
