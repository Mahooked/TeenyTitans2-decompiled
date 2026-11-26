using System;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.Purchasing;

public class AppStoreTest : MonoBehaviour, IStoreListener
{
	private static AppStoreTest instance;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Initialized__BackingField;

	private IStoreController controller;

	private IExtensionProvider extensions;

	public static AppStoreTest Instance
	{
		get
		{
			if (instance == null)
			{
				instance = new GameObject("AppStoreTest").AddComponent<AppStoreTest>();
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

	private void Start()
	{
		StandardPurchasingModule first = StandardPurchasingModule.Instance();
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(first);
		configurationBuilder.AddProduct("com.grumpyface.teenytitans2.coins.1000", ProductType.Consumable);
		configurationBuilder.AddProduct("com.grumpyface.teenytitans2.coins.2000", ProductType.Consumable);
		UnityPurchasing.Initialize(this, configurationBuilder);
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
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
	{
		if (e.purchasedProduct.definition.id.Contains("coins."))
		{
			string[] array = e.purchasedProduct.definition.id.Split('.');
			int amount = int.Parse(array[array.Length - 1]);
			Player.instance.AddCurrency(amount);
		}
		return PurchaseProcessingResult.Complete;
	}

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
	}
}
