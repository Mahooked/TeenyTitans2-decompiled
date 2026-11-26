using System;
using System.Collections.Generic;
using AOT;
using Uniject;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Purchasing.Security;

namespace UnityEngine.Purchasing;

internal class AppleStoreImpl : JSONStore, IAppleExtensions, IStoreExtension, IAppleConfiguration, IStoreConfiguration
{
	private Action<Product> m_DeferredCallback;

	private Action m_RefreshReceiptError;

	private Action<string> m_RefreshReceiptSuccess;

	private Action<bool> m_RestoreCallback;

	private Action<Product> m_PromotionalPurchaseCallback;

	private INativeAppleStore m_Native;

	private static IUtil util;

	private static AppleStoreImpl instance;

	public string appReceipt => m_Native.appReceipt;

	public bool canMakePayments => m_Native.canMakePayments;

	public bool simulateAskToBuy
	{
		get
		{
			return m_Native.simulateAskToBuy;
		}
		set
		{
			m_Native.simulateAskToBuy = value;
		}
	}

	public AppleStoreImpl(IUtil util)
	{
		AppleStoreImpl.util = util;
		instance = this;
	}

	public void SetNativeStore(INativeAppleStore apple)
	{
		SetNativeStore((INativeStore)apple);
		m_Native = apple;
		apple.SetUnityPurchasingCallback(MessageCallback);
	}

	public void SetApplePromotionalPurchaseInterceptorCallback(Action<Product> callback)
	{
		m_PromotionalPurchaseCallback = callback;
	}

	public void SetStorePromotionOrder(List<Product> products)
	{
		List<string> list = new List<string>();
		foreach (Product product in products)
		{
			if (product != null && !string.IsNullOrEmpty(product.definition.storeSpecificId))
			{
				list.Add(product.definition.storeSpecificId);
			}
		}
		Dictionary<string, object> json = new Dictionary<string, object> { { "products", list } };
		m_Native.SetStorePromotionOrder(MiniJson.JsonEncode(json));
	}

	public void SetStorePromotionVisibility(Product product, AppleStorePromotionVisibility visibility)
	{
		if (product == null)
		{
			throw new ArgumentNullException("product");
		}
		m_Native.SetStorePromotionVisibility(product.definition.storeSpecificId, visibility.ToString());
	}

	public string GetTransactionReceiptForProduct(Product product)
	{
		return m_Native.GetTransactionReceiptForProductId(product.definition.storeSpecificId);
	}

	public void SetApplicationUsername(string applicationUsername)
	{
		m_Native.SetApplicationUsername(applicationUsername);
	}

	public override void OnProductsRetrieved(string json)
	{
		List<ProductDescription> list = JSONSerializer.DeserializeProductDescriptions(json);
		List<ProductDescription> list2 = null;
		if (m_Native != null)
		{
			string text = m_Native.appReceipt;
			if (!string.IsNullOrEmpty(text))
			{
				AppleReceiptParser appleReceiptParser = new AppleReceiptParser();
				AppleReceipt appleReceipt = null;
				try
				{
					appleReceipt = appleReceiptParser.Parse(Convert.FromBase64String(text));
				}
				catch (ArgumentException)
				{
				}
				catch (IAPSecurityException)
				{
				}
				catch (NullReferenceException)
				{
				}
				if (appleReceipt != null && appleReceipt.inAppPurchaseReceipts != null && appleReceipt.inAppPurchaseReceipts.Length != 0)
				{
					list2 = new List<ProductDescription>();
					foreach (ProductDescription productDescription in list)
					{
						AppleInAppPurchaseReceipt appleInAppPurchaseReceipt = Array.Find(appleReceipt.inAppPurchaseReceipts, (AppleInAppPurchaseReceipt r) => r.productID == productDescription.storeSpecificId);
						if (appleInAppPurchaseReceipt == null)
						{
							list2.Add(productDescription);
						}
						else
						{
							list2.Add(new ProductDescription(productDescription.storeSpecificId, productDescription.metadata, text, appleInAppPurchaseReceipt.transactionID));
						}
					}
				}
			}
		}
		unity.OnProductsRetrieved(list2 ?? list);
		Promo.ProvideProductsToAds(this, unity);
		if (m_PromotionalPurchaseCallback != null)
		{
			m_Native.InterceptPromotionalPurchases();
		}
		m_Native.AddTransactionObserver();
	}

	public void RestoreTransactions(Action<bool> callback)
	{
		m_RestoreCallback = callback;
		m_Native.RestoreTransactions();
	}

	public void RefreshAppReceipt(Action<string> successCallback, Action errorCallback)
	{
		m_RefreshReceiptSuccess = successCallback;
		m_RefreshReceiptError = errorCallback;
		m_Native.RefreshAppReceipt();
	}

	public void RegisterPurchaseDeferredListener(Action<Product> callback)
	{
		m_DeferredCallback = callback;
	}

	public void ContinuePromotionalPurchases()
	{
		m_Native.ContinuePromotionalPurchases();
	}

	public void OnPurchaseDeferred(string productId)
	{
		if (m_DeferredCallback != null)
		{
			Product product = unity.products.WithStoreSpecificID(productId);
			if (product != null)
			{
				m_DeferredCallback(product);
			}
		}
	}

	public void OnPromotionalPurchaseAttempted(string productId)
	{
		if (m_PromotionalPurchaseCallback != null)
		{
			Product product = unity.products.WithStoreSpecificID(productId);
			if (product != null)
			{
				m_PromotionalPurchaseCallback(product);
			}
		}
	}

	public void OnTransactionsRestoredSuccess()
	{
		if (m_RestoreCallback != null)
		{
			m_RestoreCallback(obj: true);
		}
	}

	public void OnTransactionsRestoredFail(string error)
	{
		if (m_RestoreCallback != null)
		{
			m_RestoreCallback(obj: false);
		}
	}

	public void OnAppReceiptRetrieved(string receipt)
	{
		if (receipt != null && m_RefreshReceiptSuccess != null)
		{
			m_RefreshReceiptSuccess(receipt);
		}
	}

	public void OnAppReceiptRefreshedFailed()
	{
		if (m_RefreshReceiptError != null)
		{
			m_RefreshReceiptError();
		}
	}

	[MonoPInvokeCallback(typeof(UnityPurchasingCallback))]
	private static void MessageCallback(string subject, string payload, string receipt, string transactionId)
	{
		util.RunOnMainThread(() =>
		{
			instance.ProcessMessage(subject, payload, receipt, transactionId);
		});
	}

	private void ProcessMessage(string subject, string payload, string receipt, string transactionId)
	{
		switch (subject)
		{
		case "OnSetupFailed":
			OnSetupFailed(payload);
			break;
		case "OnProductsRetrieved":
			OnProductsRetrieved(payload);
			break;
		case "OnPurchaseSucceeded":
			OnPurchaseSucceeded(payload, receipt, transactionId);
			break;
		case "OnPurchaseFailed":
			OnPurchaseFailed(payload);
			break;
		case "onProductPurchaseDeferred":
			OnPurchaseDeferred(payload);
			break;
		case "onPromotionalPurchaseAttempted":
			OnPromotionalPurchaseAttempted(payload);
			break;
		case "onTransactionsRestoredSuccess":
			OnTransactionsRestoredSuccess();
			break;
		case "onTransactionsRestoredFail":
			OnTransactionsRestoredFail(payload);
			break;
		case "onAppReceiptRefreshed":
			OnAppReceiptRetrieved(payload);
			break;
		case "onAppReceiptRefreshFailed":
			OnAppReceiptRefreshedFailed();
			break;
		}
	}
}
