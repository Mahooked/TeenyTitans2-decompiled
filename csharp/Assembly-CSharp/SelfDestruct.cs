using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
	public float time = 1f;

	public SelfDestructType destructType = SelfDestructType.Destroy;

	public Transform parentHolder;

	public bool destroyComponent;

	private float _time;

	private void Start()
	{
		_time = time;
	}

	private void OnEnable()
	{
		_time = time;
	}

	private void OnDisable()
	{
		_time = time;
	}

	private void Update()
	{
		_time -= Time.deltaTime;
		if (!(_time <= 0f))
		{
			return;
		}
		switch (destructType)
		{
		case SelfDestructType.Pool:
			ObjectPool.PoolObject(gameObject);
			break;
		case SelfDestructType.Disable:
			gameObject.SetActive(value: false);
			if (parentHolder != null)
			{
				transform.parent = parentHolder;
			}
			break;
		default:
			Object.Destroy(gameObject);
			break;
		}
		if (destroyComponent)
		{
			Object.Destroy(this);
		}
	}

	public void Init(float time, SelfDestructType type = SelfDestructType.Destroy, Transform parent = null, bool destroyComponent = false)
	{
		_time = (this.time = time);
		destructType = type;
		parentHolder = parent;
		this.destroyComponent = destroyComponent;
	}

	public void SetTime(float newTime)
	{
		_time = newTime;
	}
}
