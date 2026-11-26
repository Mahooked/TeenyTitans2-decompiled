using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPStore : MonoBehaviour, IStoreListener
{
	private static IAPStore instance;

	private IAPConfiguration iapConfig;

	private IStoreController controller;

	private IExtensionProvider extensions;

	public static IAPStore Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("IAPStore").AddComponent<IAPStore>();
			}
			return instance;
		}
	}

	public IStoreController Controller => controller;

	public IExtensionProvider Extensions => extensions;

	public bool IsInitialized { get; private set; }

	public event Action Initialized;

	public event Action<InitializationFailureReason> InitializeFailed;

	public event Action<PurchaseEventArgs> PurchaseCompleted;

	public event Action<Product, PurchaseFailureReason> PurchaseFailed;

	public event Action ReceiptInvalid;

	private void Awake()
	{
		if (instance != null && !object.ReferenceEquals(instance, this))
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		instance = this;
		UnityEngine.Object.DontDestroyOnLoad(gameObject);
	}

	public void Initialize(IAPConfiguration config)
	{
		if (!IsInitialized)
		{
			iapConfig = config;
			StandardPurchasingModule first = StandardPurchasingModule.Instance();
			ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(first);
			for (int i = 0; i < config.idMap.idSets.Length; i++)
			{
				Debug.LogFormat("Adding product with Product ID: {0}", config.idMap.idSets[i].ProductID);
				configurationBuilder.AddProduct(config.idMap.idSets[i].ProductID, ProductType.Consumable, config.idMap.idSets[i].ids);
			}
			UnityPurchasing.Initialize(this, configurationBuilder);
		}
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.extensions = extensions;
		IsInitialized = true;
		if (Initialized != null)
		{
			Initialized();
		}
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.LogFormat("[IAPStore] - Initialization Failed | Reason: {0}", error);
		if (InitializeFailed != null)
		{
			InitializeFailed(error);
		}
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		Debug.LogFormat("[IAPStore] - Purchased Product - Product ID: {0}    Transaction ID: {1}", e.purchasedProduct.definition.id, e.purchasedProduct.transactionID);
		if (true)
		{
			if (PurchaseCompleted != null)
			{
				PurchaseCompleted(e);
			}
		}
		else if (ReceiptInvalid != null)
		{
			ReceiptInvalid();
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		Debug.LogFormat("[IAPStore] - Purchase Failed because: {0}", p);
		if (PurchaseFailed != null)
		{
			PurchaseFailed(i, p);
		}
	}
}
