using System;
using System.Diagnostics;
using System.Threading;
using TMPro;

public class RiddlerShopProductWindow : RiddlerShopBaseWindow
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<IAPProduct> ProductSelected__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<IAPProduct> HelpRequested__BackingField;

	public TextMeshPro headerText;

	public RiddlerShopProductButton[] productButtons;

	public event Action<IAPProduct> ProductSelected
	{
		add
		{
			Action<IAPProduct> action = ProductSelected__BackingField;
			Action<IAPProduct> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ProductSelected__BackingField, (Action<IAPProduct>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IAPProduct> action = ProductSelected__BackingField;
			Action<IAPProduct> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ProductSelected__BackingField, (Action<IAPProduct>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<IAPProduct> HelpRequested
	{
		add
		{
			Action<IAPProduct> action = HelpRequested__BackingField;
			Action<IAPProduct> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref HelpRequested__BackingField, (Action<IAPProduct>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<IAPProduct> action = HelpRequested__BackingField;
			Action<IAPProduct> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref HelpRequested__BackingField, (Action<IAPProduct>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

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
				productButtons[i].gameObject.SetActive(true);
				productButtons[i].Init(products[i]);
			}
			else
			{
				productButtons[i].gameObject.SetActive(false);
			}
		}
	}

	private void HandleOnProductClick(tk2dUIItem obj)
	{
		RiddlerShopProductButton component = obj.GetComponent<RiddlerShopProductButton>();
		if (component != null && ProductSelected__BackingField != null)
		{
			ProductSelected__BackingField(component.product);
		}
	}

	private void HandleOnHelpRequested(tk2dUIItem uiItem)
	{
		RiddlerShopProductButton componentInParent = uiItem.GetComponentInParent<RiddlerShopProductButton>();
		if (componentInParent != null && HelpRequested__BackingField != null)
		{
			HelpRequested__BackingField(componentInParent.product);
		}
	}
}
