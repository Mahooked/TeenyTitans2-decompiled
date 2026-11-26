using System;
using UnityEngine;
using UnityEngine.Purchasing;

public class AppStoreTest : MonoBehaviour, IStoreListener
{
	private static AppStoreTest instance;

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

	public IStoreController Controller => controller;

	public IExtensionProvider Extensions => extensions;

	public bool IsInitialized { get; private set; }

	public event Action Initialized;

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
		if (Initialized != null)
		{
			Initialized();
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
