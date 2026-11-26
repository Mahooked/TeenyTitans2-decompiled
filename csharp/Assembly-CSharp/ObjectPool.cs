using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[AddComponentMenu("Gameplay/ObjectPool")]
public class ObjectPool : MonoBehaviour
{
	[Serializable]
	public class ObjectPoolEntry : IEquatable<ObjectPoolEntry>
	{
		public GameObject prefab;

		public int count;

		public ObjectPoolEntry()
		{
			prefab = null;
			count = 0;
		}

		public ObjectPoolEntry(GameObject prefab, int count)
		{
			this.prefab = prefab;
			this.count = count;
		}

		public bool Equals(ObjectPoolEntry otherEntry)
		{
			if (otherEntry == null)
			{
				return false;
			}
			if (otherEntry.prefab.Equals(prefab))
			{
				return true;
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (!(obj is ObjectPoolEntry otherEntry))
			{
				return false;
			}
			return Equals(otherEntry);
		}
	}

	private static ObjectPool _instance;

	private static bool _isApplicationQuit;

	public List<ObjectPoolEntry> entries = new List<ObjectPoolEntry>();

	public List<ObjectPoolEntry> permanentEntries = new List<ObjectPoolEntry>();

	private Dictionary<string, GameObject> _prefabs = new Dictionary<string, GameObject>();

	private Dictionary<string, List<GameObject>> _pool = new Dictionary<string, List<GameObject>>();

	private List<GameObject> latePool = new List<GameObject>();

	protected bool _isDestroyed;

	private bool _isInitialized;

	public static ObjectPool instance
	{
		get
		{
			if (_instance == null && !_isApplicationQuit)
			{
				_instance = UnityEngine.Object.FindObjectOfType<ObjectPool>();
				if (_instance == null || _instance._isDestroyed)
				{
					_instance = new GameObject("ObjectPool").AddComponent<ObjectPool>();
				}
				_instance.Init();
			}
			return _instance;
		}
	}

	public bool isInitialized => _isInitialized;

	private void Awake()
	{
		if (_instance != null && _instance != this)
		{
			Debug.LogError("An instance of ObjectPool already exists. Deleting this object");
			UnityEngine.Object.Destroy(gameObject);
		}
		else
		{
			_instance = this;
			Init();
		}
	}

	private void OnApplicationQuit()
	{
		_isApplicationQuit = true;
	}

	private void OnDestroy()
	{
		_isDestroyed = true;
		_instance = null;
		_isInitialized = false;
	}

	private void LateUpdate()
	{
		for (int num = latePool.Count - 1; num >= 0; num--)
		{
			if (latePool[num] != null)
			{
				if (!_pool.ContainsKey(latePool[num].name))
				{
					AddType(latePool[num]);
				}
				if (_pool[latePool[num].name].Contains(latePool[num]))
				{
					Debug.Log("Object has already been pooled");
				}
				else
				{
					latePool[num].SetActive(value: false);
					latePool[num].transform.parent = transform;
					_pool[latePool[num].name].Add(latePool[num]);
					if (latePool[num].GetComponent(typeof(IPoolable)) is IPoolable poolable)
					{
						poolable.ResetObject();
					}
				}
			}
			latePool.RemoveAt(num);
		}
	}

	public void Init()
	{
		if (!_isInitialized)
		{
			int count = permanentEntries.Count;
			for (int i = 0; i < count; i++)
			{
				AddType(permanentEntries[i].prefab, permanentEntries[i].count);
			}
			count = entries.Count;
			for (int j = 0; j < count; j++)
			{
				AddType(entries[j].prefab, entries[j].count);
			}
			_isInitialized = true;
		}
	}

	public void AddType(GameObject prefab, int initialCount = 0)
	{
		if (prefab == null)
		{
			return;
		}
		if (HasType(prefab.name))
		{
			if (_pool[prefab.name].Count < initialCount)
			{
				for (int i = _pool[prefab.name].Count; i < initialCount; i++)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
					gameObject.name = prefab.name;
					_PoolObject(gameObject, instant: true);
				}
			}
		}
		else
		{
			_prefabs.Add(prefab.name, prefab);
			_pool.Add(prefab.name, new List<GameObject>());
			for (int j = 0; j < initialCount; j++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
				gameObject.name = prefab.name;
				_PoolObject(gameObject, instant: true);
			}
		}
	}

	public void AddType(string path, int initialCount = 0)
	{
		GameObject gameObject = Resources.Load<GameObject>(path);
		if (!(gameObject == null))
		{
			AddType(gameObject, initialCount);
		}
	}

	public bool HasType(string objectType)
	{
		return _prefabs.ContainsKey(objectType);
	}

	private GameObject _GetObject(string objectType, Vector3 destination = default(Vector3), bool isActive = true)
	{
		if (_pool.ContainsKey(objectType))
		{
			if (_pool[objectType].Count > 0)
			{
				GameObject gameObject = _pool[objectType][0];
				_pool[objectType].RemoveAt(0);
				gameObject.transform.parent = null;
				gameObject.transform.position = destination;
				gameObject.SetActive(isActive);
				return gameObject;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate(_prefabs[objectType]);
			gameObject2.SetActive(value: false);
			gameObject2.name = objectType;
			gameObject2.transform.parent = null;
			gameObject2.transform.position = destination;
			gameObject2.SetActive(isActive);
			return gameObject2;
		}
		return null;
	}

	private T _GetObject<T>(string objectType, Vector3 destination = default(Vector3), bool isActive = true) where T : Component
	{
		GameObject gameObject = _GetObject(objectType, destination, isActive);
		if (gameObject == null)
		{
			return (T)null;
		}
		return gameObject.GetComponent<T>();
	}

	private GameObject _GetObject(GameObject go, Vector3 destination = default(Vector3), bool isActive = true)
	{
		if (go == null)
		{
			return null;
		}
		if (!_pool.ContainsKey(go.name))
		{
			AddType(go);
		}
		return _GetObject(go.name, destination, isActive);
	}

	private T _GetObject<T>(GameObject go, Vector3 destination = default(Vector3), bool isActive = true) where T : Component
	{
		GameObject gameObject = _GetObject(go, destination, isActive);
		if (gameObject == null)
		{
			return (T)null;
		}
		return gameObject.GetComponent<T>();
	}

	private GameObject _GetObjectFromResource(string path, Vector3 destination = default(Vector3), bool isActive = true)
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		if (HasType(fileNameWithoutExtension))
		{
			return _GetObject(fileNameWithoutExtension, destination, isActive);
		}
		return _GetObject((GameObject)Resources.Load(path), destination, isActive);
	}

	private T _GetObjectFromResource<T>(string path, Vector3 destination = default(Vector3), bool isActive = true) where T : Component
	{
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path);
		if (HasType(fileNameWithoutExtension))
		{
			return _GetObject<T>(fileNameWithoutExtension, destination, isActive);
		}
		return _GetObject<T>((GameObject)Resources.Load(path), destination, isActive);
	}

	public GameObject GetPrefab(string objectType)
	{
		if (_prefabs.ContainsKey(objectType))
		{
			return _prefabs[objectType];
		}
		return null;
	}

	private void _PoolObject(GameObject obj, bool instant = false)
	{
		if (obj == null || _isDestroyed || _isApplicationQuit)
		{
			return;
		}
		if (instant)
		{
			if (_pool.ContainsKey(obj.name))
			{
				if (_pool[obj.name].Contains(obj))
				{
					Debug.LogWarning("Object has already been pooled");
					return;
				}
				obj.SetActive(value: false);
				obj.transform.parent = transform;
				_pool[obj.name].Add(obj);
				if (_isInitialized && obj.GetComponent(typeof(IPoolable)) is IPoolable poolable)
				{
					poolable.ResetObject();
				}
			}
			else
			{
				AddType(obj);
				_PoolObject(obj);
			}
		}
		else
		{
			latePool.Add(obj);
		}
	}

	private void _PoolObject(GameObject obj, float delay)
	{
		StartCoroutine(PoolObjectDelayed(obj, delay));
	}

	private IEnumerator PoolObjectDelayed(GameObject obj, float delay)
	{
		yield return new WaitForSeconds(delay);
		_PoolObject(obj);
	}

	public static GameObject GetObject(string objectType, Vector3 destination = default(Vector3), bool isActive = true)
	{
		if (instance != null)
		{
			return instance._GetObject(objectType, destination, isActive);
		}
		return null;
	}

	public static T GetObject<T>(string objectType, Vector3 destination = default(Vector3), bool isActive = true) where T : Component
	{
		if (instance != null)
		{
			return instance._GetObject<T>(objectType, destination, isActive);
		}
		return (T)null;
	}

	public static GameObject GetObject(GameObject go, Vector3 destination = default(Vector3), bool isActive = true)
	{
		if (instance != null)
		{
			return instance._GetObject(go, destination, isActive);
		}
		return null;
	}

	public static GameObject GetObjectFromResource(string path, Vector3 destination = default(Vector3), bool isActive = true)
	{
		if (instance != null)
		{
			return instance._GetObjectFromResource(path, destination, isActive);
		}
		return null;
	}

	public static T GetObjectFromResource<T>(string path, Vector3 destination = default(Vector3), bool isActive = true) where T : Component
	{
		if (instance != null)
		{
			return instance._GetObjectFromResource<T>(path, destination, isActive);
		}
		return (T)null;
	}

	public static void PoolObject(GameObject obj, bool instant = false)
	{
		if (instance != null)
		{
			instance._PoolObject(obj, instant);
		}
	}

	public static void PoolObject(GameObject obj, float delay)
	{
		if (instance != null)
		{
			instance._PoolObject(obj, delay);
		}
	}

	public static void PoolObjects(params GameObject[] args)
	{
		if (instance != null)
		{
			int num = args.Length;
			for (int i = 0; i < num; i++)
			{
				PoolObject(args[i]);
			}
		}
	}
}
