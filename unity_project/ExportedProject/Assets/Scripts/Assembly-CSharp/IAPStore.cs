using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPStore : MonoBehaviour, IStoreListener
{
	private static IAPStore instance;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Initialized__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<InitializationFailureReason> InitializeFailed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<PurchaseEventArgs> PurchaseCompleted__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Product, PurchaseFailureReason> PurchaseFailed__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action ReceiptInvalid__BackingField;

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

	public IStoreController Controller
	{
		get
		{
			return controller;
		}
	}

	public IExtensionProvider Extensions
	{
		get
		{
			return extensions;
		}
	}

	public bool IsInitialized { get; private set; }

	public event Action Initialized
	{
		add
		{
			Action action = Initialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Initialized__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Initialized__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Initialized__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<InitializationFailureReason> InitializeFailed
	{
		add
		{
			Action<InitializationFailureReason> action = InitializeFailed__BackingField;
			Action<InitializationFailureReason> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InitializeFailed__BackingField, (Action<InitializationFailureReason>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<InitializationFailureReason> action = InitializeFailed__BackingField;
			Action<InitializationFailureReason> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref InitializeFailed__BackingField, (Action<InitializationFailureReason>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<PurchaseEventArgs> PurchaseCompleted
	{
		add
		{
			Action<PurchaseEventArgs> action = PurchaseCompleted__BackingField;
			Action<PurchaseEventArgs> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PurchaseCompleted__BackingField, (Action<PurchaseEventArgs>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<PurchaseEventArgs> action = PurchaseCompleted__BackingField;
			Action<PurchaseEventArgs> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PurchaseCompleted__BackingField, (Action<PurchaseEventArgs>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<Product, PurchaseFailureReason> PurchaseFailed
	{
		add
		{
			Action<Product, PurchaseFailureReason> action = PurchaseFailed__BackingField;
			Action<Product, PurchaseFailureReason> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PurchaseFailed__BackingField, (Action<Product, PurchaseFailureReason>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Product, PurchaseFailureReason> action = PurchaseFailed__BackingField;
			Action<Product, PurchaseFailureReason> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref PurchaseFailed__BackingField, (Action<Product, PurchaseFailureReason>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action ReceiptInvalid
	{
		add
		{
			Action action = ReceiptInvalid__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ReceiptInvalid__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = ReceiptInvalid__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ReceiptInvalid__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (instance != null && !object.ReferenceEquals(instance, this))
		{
			UnityEngine.Object.Destroy(this);
			return;
		}
		instance = this;
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
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
				UnityEngine.Debug.LogFormat("Adding product with Product ID: {0}", config.idMap.idSets[i].ProductID);
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
		if (Initialized__BackingField != null)
		{
			Initialized__BackingField();
		}
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.LogFormat("[IAPStore] - Initialization Failed | Reason: {0}", error);
		if (InitializeFailed__BackingField != null)
		{
			InitializeFailed__BackingField(error);
		}
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		UnityEngine.Debug.LogFormat("[IAPStore] - Purchased Product - Product ID: {0}    Transaction ID: {1}", e.purchasedProduct.definition.id, e.purchasedProduct.transactionID);
		if (true)
		{
			if (PurchaseCompleted__BackingField != null)
			{
				PurchaseCompleted__BackingField(e);
			}
		}
		else if (ReceiptInvalid__BackingField != null)
		{
			ReceiptInvalid__BackingField();
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		UnityEngine.Debug.LogFormat("[IAPStore] - Purchase Failed because: {0}", p);
		if (PurchaseFailed__BackingField != null)
		{
			PurchaseFailed__BackingField(i, p);
		}
	}
}
