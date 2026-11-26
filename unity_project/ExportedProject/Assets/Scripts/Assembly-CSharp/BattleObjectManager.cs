using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;

public class BattleObjectManager : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SpawnedBattery__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SpawnedCoins__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SpawnedTofu__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<GameObject> ObjectAdded__BackingField;

	private List<GameObject> battleObjects = new List<GameObject>();

	private int maxItems = 4;

	private int maxBatteries = 2;

	private int didNotSpawnBatteryCount;

	private float _spawnChanceTofu = 0.4f;

	private float _spawnChanceCoin = 0.8f;

	private float _spawnChanceBattery = 0.8f;

	private float _spawnChanceCoinBonus;

	private float _spawnChanceTofuBonus;

	private float _spawnChanceBatteryBonus;

	private static BattleObjectManager _instance;

	public float spawnChanceTofu
	{
		get
		{
			return _spawnChanceTofu;
		}
		set
		{
			_spawnChanceTofu = Mathf.Clamp01(value);
		}
	}

	public float spawnChanceCoin
	{
		get
		{
			return _spawnChanceCoin;
		}
		set
		{
			_spawnChanceCoin = Mathf.Clamp01(value);
		}
	}

	public float spawnChanceBattery
	{
		get
		{
			return _spawnChanceBattery;
		}
		set
		{
			_spawnChanceBattery = Mathf.Clamp01(value);
		}
	}

	public float spawnChanceCoinBonus
	{
		get
		{
			return _spawnChanceCoinBonus;
		}
		set
		{
			_spawnChanceCoinBonus = value;
		}
	}

	public float spawnChanceTofuBonus
	{
		get
		{
			return _spawnChanceTofuBonus;
		}
		set
		{
			_spawnChanceTofuBonus = value;
		}
	}

	public float spawnChanceBatteryBonus
	{
		get
		{
			return _spawnChanceBatteryBonus;
		}
		set
		{
			_spawnChanceBatteryBonus = value;
		}
	}

	public static BattleObjectManager instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = new GameObject("BattleObjectManager").AddComponent<BattleObjectManager>();
				_instance.ResetSpawnChances();
			}
			return _instance;
		}
	}

	public event Action SpawnedBattery
	{
		add
		{
			Action action = SpawnedBattery__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SpawnedBattery__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SpawnedBattery__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SpawnedBattery__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action SpawnedCoins
	{
		add
		{
			Action action = SpawnedCoins__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SpawnedCoins__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SpawnedCoins__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SpawnedCoins__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action SpawnedTofu
	{
		add
		{
			Action action = SpawnedTofu__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SpawnedTofu__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SpawnedTofu__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SpawnedTofu__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<GameObject> ObjectAdded
	{
		add
		{
			Action<GameObject> action = ObjectAdded__BackingField;
			Action<GameObject> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ObjectAdded__BackingField, (Action<GameObject>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<GameObject> action = ObjectAdded__BackingField;
			Action<GameObject> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref ObjectAdded__BackingField, (Action<GameObject>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		maxBatteries = GameManager.instance.gameSettings.maxBatteryDrop;
	}

	private void OnDestroy()
	{
		_instance = null;
	}

	public void CheckForItemSpawns(Vector3 worldPoint, bool allowTofu = true)
	{
		int num = 0;
		bool flag = false;
		if (UnityEngine.Random.value < spawnChanceTofu + spawnChanceTofuBonus)
		{
			flag = allowTofu;
		}
		bool flag2 = UnityEngine.Random.value < spawnChanceCoin + spawnChanceCoinBonus;
		bool flag3 = false;
		if (UnityEngine.Random.value < spawnChanceBattery + spawnChanceBatteryBonus)
		{
			flag3 = true;
		}
		else
		{
			didNotSpawnBatteryCount++;
		}
		if (flag)
		{
			SpawnTofuAt(worldPoint + new Vector3(0f, 50f, 0f));
			num++;
			if (SpawnedTofu__BackingField != null)
			{
				SpawnedTofu__BackingField();
			}
		}
		if (flag2)
		{
			for (int i = 0; i < UnityEngine.Random.Range(1, maxItems + 1) - num; i++)
			{
				SpawnCoinAt(worldPoint + new Vector3(-40f + UnityEngine.Random.Range(-10f, 10f), 50f - (float)i * 50f, i * 5));
			}
			if (SpawnedCoins__BackingField != null)
			{
				SpawnedCoins__BackingField();
			}
		}
		if (didNotSpawnBatteryCount > 2)
		{
			didNotSpawnBatteryCount = 0;
			flag3 = true;
		}
		if (flag3)
		{
			for (int j = 0; j < UnityEngine.Random.Range(1, maxBatteries + 1) - num; j++)
			{
				SpawnBatteryAt(worldPoint + new Vector3(-40f + UnityEngine.Random.Range(-15f, 16f), 50f - (float)j * 50f, j * 5), true);
			}
			if (SpawnedBattery__BackingField != null)
			{
				SpawnedBattery__BackingField();
			}
		}
	}

	public void SpawnTofuAt(Vector3 position, bool isPlayerTofu = true)
	{
		SpawnTofuAt(position, isPlayerTofu, (!isPlayerTofu) ? 1 : (-1));
	}

	public void SpawnTofuAt(Vector3 position, bool isPlayerTofu, int direction)
	{
		if (isPlayerTofu)
		{
			Player.instance.SetTrigger("TofuSpawned");
		}
		position.z = -175f;
		TofuPickup objectFromResource = ObjectPool.GetObjectFromResource<TofuPickup>("Prefabs/GUI/Pickups/TofuPickup", position, false);
		objectFromResource.Init(isPlayerTofu, direction);
		objectFromResource.gameObject.SetActive(true);
		battleObjects.Add(objectFromResource.gameObject);
	}

	public void SpawnCoinAt(Vector3 position)
	{
		CoinPickup objectFromResource = ObjectPool.GetObjectFromResource<CoinPickup>("Prefabs/GUI/Pickups/CoinPickup", position);
		battleObjects.Add(objectFromResource.gameObject);
	}

	public void SpawnBatteryAt(Vector3 position, bool isPlayerBattery)
	{
		if ((isPlayerBattery && !BattleController.instance.playerAccessoryButton.isInitialized) || (!isPlayerBattery && !BattleController.instance.enemyAccessoryButton.isInitialized))
		{
			UnityEngine.Debug.Log("Battle Object Manager, Tried to spawn a battery but the accessory GUI is NOT initialized.");
			return;
		}
		BatteryPickup objectFromResource = ObjectPool.GetObjectFromResource<BatteryPickup>("Prefabs/GUI/Pickups/BatteryPickup", position, false);
		objectFromResource.Init(isPlayerBattery);
		objectFromResource.gameObject.SetActive(true);
		battleObjects.Add(objectFromResource.gameObject);
	}

	public T SpawnPickupAt<T>(string path, Vector3 position, bool isPlayerPickup = true) where T : BattlePickup
	{
		position.z = -175f;
		T objectFromResource = ObjectPool.GetObjectFromResource<T>(path, position, false);
		objectFromResource.Init(isPlayerPickup);
		objectFromResource.gameObject.SetActive(true);
		battleObjects.Add(objectFromResource.gameObject);
		return objectFromResource;
	}

	public void AddObject(GameObject obj)
	{
		if (obj != null)
		{
			battleObjects.Add(obj);
			OnObjectAdded(obj);
		}
	}

	public void RemoveObject(GameObject obj)
	{
		if (obj != null)
		{
			battleObjects.Remove(obj);
		}
	}

	public T GetObject<T>() where T : MonoBehaviour
	{
		int count = battleObjects.Count;
		T val = (T)null;
		for (int i = 0; i < count; i++)
		{
			val = battleObjects[i].GetComponent<T>();
			if (val != null)
			{
				break;
			}
		}
		return val;
	}

	public List<T> GetAllObjects<T>() where T : MonoBehaviour
	{
		List<T> list = new List<T>();
		T val = (T)null;
		int count = battleObjects.Count;
		for (int i = 0; i < count; i++)
		{
			val = battleObjects[i].GetComponent<T>();
			if (val != null)
			{
				list.Add(val);
			}
		}
		return list;
	}

	public void ResetSpawnChances()
	{
		_spawnChanceTofu = GameManager.instance.gameSettings.defaultTofuDropChance;
		_spawnChanceCoin = GameManager.instance.gameSettings.defaultCoinDropChance;
		_spawnChanceBattery = GameManager.instance.gameSettings.defaultBatteryDropChance;
	}

	private bool CanSpawnTofu()
	{
		for (int i = 0; i < instance.battleObjects.Count; i++)
		{
			if (battleObjects[i].GetComponent<TofuPickup>() != null || battleObjects[i].GetComponent<TofuBubble>() != null)
			{
				return false;
			}
		}
		return true;
	}

	protected void OnObjectAdded(GameObject obj)
	{
		if (ObjectAdded__BackingField != null)
		{
			ObjectAdded__BackingField(obj);
		}
	}
}
