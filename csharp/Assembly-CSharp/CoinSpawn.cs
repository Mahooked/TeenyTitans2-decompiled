using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CoinSpawn : MonoBehaviour
{
	[HideInInspector]
	public string guid;

	[HideInInspector]
	public int instanceID = -1;

	public CurrencyType currencyType;

	public int amount = 1;

	public float spacing = 0.2f;

	[Range(0f, 1f)]
	public float spawnChance = 1f;

	public TimeOfDay timeOfDay;

	public bool onlyOnce;

	private bool isDayTime;

	private List<CoinPickup> coins = new List<CoinPickup>();

	private bool isSetup;

	private IEnumerator Start()
	{
		if (Application.isPlaying)
		{
			yield return new WaitForEndOfFrame();
			yield return new WaitForEndOfFrame();
			if (CityController.instance != null)
			{
				CityController.instance.communicator.StartDayTime += HandleStartDayTime;
				CityController.instance.communicator.StartNightTime += HandleStartNightTime;
				isDayTime = CityController.instance.communicator.isDayTime;
			}
			isSetup = true;
			CheckShouldSpawn();
		}
	}

	private void OnEnable()
	{
		if (!isSetup && Application.isPlaying && !(CityController.instance == null))
		{
			CityController.instance.communicator.StartDayTime += HandleStartDayTime;
			CityController.instance.communicator.StartNightTime += HandleStartNightTime;
			isDayTime = CityController.instance.communicator.isDayTime;
			isSetup = true;
			CheckShouldSpawn();
		}
	}

	private void OnDisable()
	{
		if (isSetup && Application.isPlaying)
		{
			if (CityController.instance != null)
			{
				CityController.instance.communicator.StartDayTime -= HandleStartDayTime;
				CityController.instance.communicator.StartNightTime -= HandleStartNightTime;
			}
			isSetup = false;
		}
	}

	private void OnDestroy()
	{
		if (Application.isPlaying)
		{
			if (CityController.instance != null)
			{
				CityController.instance.communicator.StartDayTime -= HandleStartDayTime;
			}
			if (CityController.instance != null)
			{
				CityController.instance.communicator.StartNightTime -= HandleStartNightTime;
			}
		}
	}

	private void HandleStartDayTime()
	{
		isDayTime = true;
		CheckShouldSpawn();
	}

	private void HandleStartNightTime()
	{
		isDayTime = false;
		CheckShouldSpawn();
	}

	private void CheckShouldSpawn()
	{
		if (CoreGameTutorial.instance != null && !CoreGameTutorial.instance.CheckShouldAllowCoinSpawn())
		{
			HideCoins();
			return;
		}
		if (timeOfDay != TimeOfDay.Both)
		{
			if (timeOfDay == TimeOfDay.Day && !isDayTime)
			{
				HideCoins();
				return;
			}
			if (timeOfDay == TimeOfDay.Night && isDayTime)
			{
				HideCoins();
				return;
			}
		}
		if (!gameObject.activeInHierarchy)
		{
			HideCoins();
		}
		else if (spawnChance >= 1f)
		{
			SpawnCoins();
		}
		else if (spawnChance > 0f && Random.value < spawnChance)
		{
			SpawnCoins();
		}
	}

	private void HideCoins()
	{
		for (int i = 0; i < coins.Count; i++)
		{
			coins[i].gameObject.SetActive(value: false);
		}
	}

	private string shortGUID()
	{
		return guid.Substring(0, Mathf.Min(6, guid.Length));
	}

	public void CoinPickedUp(int coinNumber)
	{
		string key = shortGUID() + "_COIN_" + coinNumber;
		SaveUtility.SetInt(key, 1, save: false);
	}

	private bool HasPickedUpCoinForDay(int coinNumber)
	{
		string key = shortGUID() + "_COIN_" + coinNumber;
		return SaveUtility.GetInt(key) == 1;
	}

	private void ClearSavedPickedUpCoins()
	{
		if (CityController.instance == null || onlyOnce)
		{
			return;
		}
		int num = SaveUtility.GetInt(shortGUID() + "_DAY", 0);
		int currentDay = CityController.instance.communicator.GetCurrentDay();
		if (currentDay > num)
		{
			SaveUtility.SetInt(shortGUID() + "_DAY", currentDay, save: false);
			for (int i = 0; i < amount; i++)
			{
				string key = shortGUID() + "_COIN_" + i;
				SaveUtility.GameSaveData.DeleteKey(key);
			}
			for (int j = 0; j < coins.Count; j++)
			{
				coins[j].collected = false;
			}
		}
	}

	private void SpawnCoins()
	{
		ClearSavedPickedUpCoins();
		if (coins.Count > 0)
		{
			CoinPickup coinPickup = null;
			for (int i = 0; i < coins.Count; i++)
			{
				coinPickup = coins[i];
				if (coinPickup.collected || HasPickedUpCoinForDay(coinPickup.coinNumber))
				{
					coinPickup.gameObject.SetActive(value: false);
				}
				else
				{
					coinPickup.gameObject.SetActive(value: true);
				}
			}
			return;
		}
		Vector3 position = transform.position;
		for (int j = 0; j < amount; j++)
		{
			CoinPickup objectFromResource = ObjectPool.GetObjectFromResource<CoinPickup>((currencyType != CurrencyType.Coins) ? "Prefabs/GUI/CityRainbowCoin" : "Prefabs/GUI/CityCoin", position);
			objectFromResource.collected = false;
			objectFromResource.coinSpawner = this;
			objectFromResource.coinNumber = j;
			objectFromResource.transform.parent = transform;
			objectFromResource.transform.localEulerAngles = Vector3.zero;
			position += transform.forward * spacing;
			coins.Add(objectFromResource);
			if (HasPickedUpCoinForDay(j))
			{
				objectFromResource.gameObject.SetActive(value: false);
			}
		}
	}
}
