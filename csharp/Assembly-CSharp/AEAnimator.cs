using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEAnimator : MonoBehaviour
{
	public class AEAnimationAssetSprites
	{
		public List<AEAbstractSprite> sprites;

		public List<AELayer> layers;
	}

	public int pixelsPerMeter = 100;

	public bool playOnStart;

	public bool playFromBeginning = true;

	public bool looping;

	public bool useUnscaledTime;

	private Material _activeMaterial;

	private Material _originalMaterial;

	private Material _uniqueMaterial;

	private bool _materialHasTintColor;

	protected bool isInitialized;

	protected bool doRebuild;

	private Dictionary<string, AEAnimationAssetSprites> _aeAnimationAssetSpritesDictionary = new Dictionary<string, AEAnimationAssetSprites>();

	private List<AEAbstractSprite> _currentAnimationSprites = new List<AEAbstractSprite>();

	[SerializeField]
	private AEAnimation _animationAsset;

	[SerializeField]
	private int _currentFrame;

	[SerializeField]
	private float _currentTime;

	[SerializeField]
	private float _timeScale = 1f;

	private bool _isPlaying;

	[SerializeField]
	private Transform _spritesHolder;

	[SerializeField]
	private bool _useSpritePooling;

	[SerializeField]
	private List<AEAbstractSprite> _sprites = new List<AEAbstractSprite>();

	[SerializeField]
	private string _sortingLayerName = "Default";

	[SerializeField]
	private int _sortingOrder;

	[SerializeField]
	private Color _color = Color.white;

	private Color _tintColor = Color.clear;

	[SerializeField]
	private float _pivotX = 0.5f;

	[SerializeField]
	private float _pivotY = 0.5f;

	[SerializeField]
	private bool _isFlipped;

	private bool _renderersEnabled = true;

	private bool _useCachedSprites;

	public AEAnimation animationAsset
	{
		get
		{
			return _animationAsset;
		}
		set
		{
			if (!(value == _animationAsset))
			{
				_animationAsset = value;
				Rebuild();
			}
		}
	}

	public int currentFrame => _currentFrame;

	public float currentTime => _currentTime;

	public float timeScale
	{
		get
		{
			return _timeScale;
		}
		set
		{
			if (value != _timeScale)
			{
				_timeScale = value;
			}
		}
	}

	public bool isPlaying => _isPlaying;

	public Transform spritesHolder
	{
		get
		{
			return _spritesHolder;
		}
		set
		{
			if (!(value == _spritesHolder))
			{
				_spritesHolder = value;
			}
		}
	}

	public bool useSpritePooling
	{
		get
		{
			return _useSpritePooling;
		}
		set
		{
			if (value == _useSpritePooling)
			{
				return;
			}
			_useSpritePooling = value;
			if (!_useSpritePooling)
			{
				int numLayers = _animationAsset.composition.numLayers;
				while (numLayers < sprites.Count)
				{
					UnityEngine.Object.DestroyImmediate(sprites[sprites.Count - 1].gameObject);
					sprites.RemoveAt(sprites.Count - 1);
				}
			}
		}
	}

	public List<AEAbstractSprite> sprites => _sprites;

	public string sortingLayerName
	{
		get
		{
			return _sortingLayerName;
		}
		set
		{
			SetSortingLayerName(value);
		}
	}

	public int sortingOrder
	{
		get
		{
			return _sortingOrder;
		}
		set
		{
			SetSortingOrder(value);
		}
	}

	public Color color
	{
		get
		{
			return _color;
		}
		set
		{
			SetColor(value);
		}
	}

	public Color tintColor
	{
		get
		{
			return _tintColor;
		}
		set
		{
			SetTintColor(value);
		}
	}

	public float pivotX
	{
		get
		{
			return _pivotX;
		}
		set
		{
			if (value != _pivotX)
			{
				_pivotX = value;
				RefreshPivot();
			}
		}
	}

	public float pivotY
	{
		get
		{
			return _pivotY;
		}
		set
		{
			if (value != _pivotY)
			{
				_pivotY = value;
				RefreshPivot();
			}
		}
	}

	public bool isFlipped
	{
		get
		{
			return _isFlipped;
		}
		set
		{
			if (value != _isFlipped)
			{
				_isFlipped = value;
				RefreshFlipped();
			}
		}
	}

	public bool renderersEnabled
	{
		get
		{
			return _renderersEnabled;
		}
		set
		{
			if (value != _renderersEnabled)
			{
				_renderersEnabled = value;
				for (int i = 0; i < _sprites.Count; i++)
				{
					_sprites[i].isRendererEnabled = _renderersEnabled;
				}
			}
		}
	}

	public bool useCachedSprites
	{
		get
		{
			return _useCachedSprites;
		}
		set
		{
			_useCachedSprites = value;
		}
	}

	public event Action<AEAnimator> AnimationComplete;

	public event Action<int> EnterFrame;

	public event Action LoopComplete;

	private void Start()
	{
		isInitialized = true;
		if (_sprites.Count > 0)
		{
			bool activeSelf = _sprites[0].gameObject.activeSelf;
			bool activeSelf2 = _sprites[0].spriteTransform.gameObject.activeSelf;
			_sprites[0].gameObject.SetActive(value: true);
			_sprites[0].spriteTransform.gameObject.SetActive(value: true);
			_activeMaterial = (_originalMaterial = _sprites[0].spriteTransform.GetComponent<Renderer>().sharedMaterial);
			_materialHasTintColor = _originalMaterial.HasProperty("_TintColor");
			if (doRebuild)
			{
				Rebuild();
				doRebuild = false;
			}
		}
	}

	private void OnEnable()
	{
		if (playOnStart)
		{
			GoToAndPlay((!playFromBeginning) ? UnityEngine.Random.Range(0f, animationAsset.duration) : 0f);
		}
	}

	private void OnDisable()
	{
		GoToAndStop(0);
	}

	private void Update()
	{
		if (!_isPlaying)
		{
			return;
		}
		float num = ((!useUnscaledTime) ? Time.deltaTime : Time.unscaledDeltaTime) * _timeScale;
		_currentTime += num;
		if (looping)
		{
			bool flag = false;
			if (_currentTime < 0f)
			{
				_currentTime = _currentTime % animationAsset.duration + animationAsset.duration;
				flag = true;
			}
			else if (_currentTime >= animationAsset.duration)
			{
				_currentTime %= animationAsset.duration;
				flag = true;
			}
			int num2 = Mathf.FloorToInt(_currentTime / animationAsset.composition.frameDuration);
			if (num2 >= animationAsset.totalFrames)
			{
				num2 -= animationAsset.totalFrames;
			}
			ProcessNewFrame(num2, (num > 0f) ? 1 : (-1));
			if (flag)
			{
				OnLoopComplete();
			}
		}
		else if (_currentTime <= 0f && _timeScale < 0f)
		{
			ProcessNewFrame(0, -1);
			Stop();
			OnAnimationComplete();
		}
		else if (_currentTime >= animationAsset.duration && _timeScale > 0f)
		{
			ProcessNewFrame(animationAsset.lastFrame, 1);
			Stop();
			OnAnimationComplete();
		}
		else
		{
			ProcessNewFrame(Mathf.FloorToInt(_currentTime / animationAsset.composition.frameDuration), (num > 0f) ? 1 : (-1));
		}
	}

	public void Play()
	{
		if (!_isPlaying)
		{
			_isPlaying = true;
		}
	}

	public void Stop()
	{
		if (_isPlaying)
		{
			_isPlaying = false;
		}
	}

	public void Stop(bool reset)
	{
		if (reset)
		{
			GoToAndStop(0);
		}
		else
		{
			Stop();
		}
	}

	public void GoToAndPlay(int frame)
	{
		GoToAndStop(frame);
		Play();
	}

	public void GoToAndPlay(float time)
	{
		GoToAndStop(time);
		Play();
	}

	public void GoToAndStop(int frame)
	{
		if (!(animationAsset == null))
		{
			if (frame < 0)
			{
				frame = 0;
			}
			else if (frame >= animationAsset.totalFrames)
			{
				frame = animationAsset.lastFrame;
			}
			Stop();
			_currentFrame = frame;
			_currentTime = (float)frame * animationAsset.composition.frameDuration;
			GoToCurrentFrameForced();
		}
	}

	public void GoToAndStop(float time)
	{
		if (!(animationAsset == null))
		{
			Stop();
			_currentTime = time % animationAsset.duration;
			if (_currentTime < 0f)
			{
				_currentTime += animationAsset.duration;
			}
			_currentFrame = Mathf.FloorToInt(_currentTime / animationAsset.composition.frameDuration);
			GoToCurrentFrameForced();
		}
	}

	public void GoToCurrentFrame()
	{
		Vector3 localScale = transform.localScale;
		transform.localScale = Vector3.one;
		if (useCachedSprites)
		{
			for (int i = 0; i < _currentAnimationSprites.Count; i++)
			{
				_currentAnimationSprites[i].GoToFrame(_currentFrame);
			}
		}
		else
		{
			int numLayers = _animationAsset.composition.numLayers;
			for (int j = 0; j < numLayers; j++)
			{
				_sprites[j].GoToFrame(_currentFrame);
			}
		}
		transform.localScale = localScale;
	}

	public void GoToCurrentFrameForced()
	{
		Vector3 localScale = transform.localScale;
		transform.localScale = Vector3.one;
		if (useCachedSprites)
		{
			for (int i = 0; i < _currentAnimationSprites.Count; i++)
			{
				_currentAnimationSprites[i].GoToFrameForced(_currentFrame);
			}
		}
		else
		{
			int numLayers = _animationAsset.composition.numLayers;
			for (int j = 0; j < numLayers; j++)
			{
				_sprites[j].GoToFrameForced(_currentFrame);
			}
		}
		transform.localScale = localScale;
	}

	public AEAbstractSprite GetSpriteByLayerIndex(int layerIndex)
	{
		int count = _sprites.Count;
		for (int i = 0; i < count; i++)
		{
			if (_sprites[i].layerIndex == layerIndex)
			{
				return _sprites[i];
			}
		}
		Debug.LogWarning("GetSpriteByLayerId  -> sprite not found, layer: " + layerIndex);
		return null;
	}

	public AEAbstractSprite GetSpriteByName(string name)
	{
		int count = _sprites.Count;
		for (int i = 0; i < count; i++)
		{
			if (_sprites[i].name.Equals(name))
			{
				return _sprites[i];
			}
		}
		return null;
	}

	public void SetSortingLayerName(string newSortingLayerName)
	{
		if (!(newSortingLayerName == _sortingLayerName))
		{
			_sortingLayerName = newSortingLayerName;
			int count = _sprites.Count;
			for (int i = 0; i < count; i++)
			{
				_sprites[i].SetSortingLayerName(newSortingLayerName);
			}
		}
	}

	public void SetSortingOrder(int newSortingOrder)
	{
		if (newSortingOrder != _sortingOrder)
		{
			_sortingOrder = newSortingOrder;
			int count = _sprites.Count;
			for (int i = 0; i < count; i++)
			{
				_sprites[i].SetSortingOrder(newSortingOrder);
			}
		}
	}

	public void SetColor(Color newColor)
	{
		if (!(newColor == _color))
		{
			_color = newColor;
			int count = _sprites.Count;
			for (int i = 0; i < count; i++)
			{
				_sprites[i].SetColor(newColor);
			}
		}
	}

	public virtual void Rebuild()
	{
		if (Application.isPlaying && !isInitialized)
		{
			doRebuild = true;
		}
		else if (_animationAsset != null)
		{
			if (useCachedSprites)
			{
				InitCachedSprites();
			}
			else
			{
				InitSprites();
			}
			RefreshPivot();
		}
		else if (_spritesHolder != null)
		{
			UnityEngine.Object.DestroyImmediate(_spritesHolder.gameObject);
			_spritesHolder = null;
			_sprites.Clear();
		}
	}

	public void InitCachedSprites()
	{
		if (_spritesHolder == null)
		{
			_spritesHolder = new GameObject("SpritesHolder").transform;
			_spritesHolder.parent = transform;
			_spritesHolder.localScale = Vector3.one;
			_spritesHolder.localPosition = Vector3.zero;
			_spritesHolder.localRotation = Quaternion.identity;
			_spritesHolder.gameObject.layer = gameObject.layer;
		}
		for (int i = 0; i < sprites.Count; i++)
		{
			sprites[i].spriteRenderer.enabled = false;
		}
		AEAnimationAssetSprites value = null;
		_aeAnimationAssetSpritesDictionary.TryGetValue(_animationAsset.name, out value);
		if (value != null)
		{
			for (int j = 0; j < value.sprites.Count; j++)
			{
				AEAbstractSprite aEAbstractSprite = value.sprites[j];
				aEAbstractSprite.gameObject.SetActive(value: true);
				aEAbstractSprite.spriteTransform.gameObject.SetActive(value: true);
				aEAbstractSprite.spriteRenderer.enabled = true;
				InitSprite(aEAbstractSprite, value.layers[j], updateSprite: false);
			}
			_currentAnimationSprites = value.sprites;
		}
		else
		{
			List<AELayer> list = new List<AELayer>();
			AEAbstractSprite aEAbstractSprite2 = null;
			AELayer aELayer = null;
			int num = 0;
			int count = _animationAsset.composition.layers.Count;
			List<AEAbstractSprite> list2 = new List<AEAbstractSprite>();
			List<AELayer> list3 = new List<AELayer>();
			for (int k = 0; k < count; k++)
			{
				aELayer = _animationAsset.composition.layers[k];
				if (aELayer == null || aELayer.index == -1)
				{
					continue;
				}
				if (aELayer.parent != 0)
				{
					list.Add(aELayer);
				}
				aEAbstractSprite2 = GetSpriteByName(aELayer.sourceNoExt);
				if (aEAbstractSprite2 != null && !list2.Contains(aEAbstractSprite2))
				{
					aEAbstractSprite2.gameObject.SetActive(value: true);
					aEAbstractSprite2.spriteTransform.gameObject.SetActive(value: true);
					aEAbstractSprite2.spriteRenderer.enabled = true;
				}
				else
				{
					switch (aELayer.type)
					{
					case AELayerType.FOOTAGE:
						aEAbstractSprite2 = CreateSprite();
						break;
					default:
						Debug.LogError("Unsupported layer type: " + aELayer.type);
						break;
					case AELayerType.COMPOSITION:
						break;
					}
					aEAbstractSprite2.gameObject.layer = gameObject.layer;
					aEAbstractSprite2.spriteTransform.gameObject.layer = gameObject.layer;
					aEAbstractSprite2.spriteRenderer.enabled = true;
					sprites.Add(aEAbstractSprite2);
					if (aEAbstractSprite2.cachedTransform.parent != _spritesHolder)
					{
						aEAbstractSprite2.cachedTransform.parent = _spritesHolder;
					}
				}
				InitSprite(aEAbstractSprite2, aELayer);
				aEAbstractSprite2.SetColor(_color);
				aEAbstractSprite2.SetSortingOrder(_sortingOrder);
				aEAbstractSprite2.SetSortingLayerName(_sortingLayerName);
				list2.Add(aEAbstractSprite2);
				list3.Add(aELayer);
				if (Application.isPlaying)
				{
					aEAbstractSprite2.SetMaterial(_activeMaterial);
				}
			}
			value = new AEAnimationAssetSprites();
			value.sprites = list2;
			value.layers = list3;
			_aeAnimationAssetSpritesDictionary.Add(_animationAsset.name, value);
			_currentAnimationSprites = list2;
		}
		RefreshFlipped();
		GoToAndStop(0);
	}

	public void InitSprites()
	{
		if (_spritesHolder == null)
		{
			_spritesHolder = new GameObject("SpritesHolder").transform;
			_spritesHolder.parent = transform;
			_spritesHolder.localScale = Vector3.one;
			_spritesHolder.localPosition = Vector3.zero;
			_spritesHolder.localRotation = Quaternion.identity;
			_spritesHolder.gameObject.layer = gameObject.layer;
		}
		List<AELayer> list = new List<AELayer>();
		AEAbstractSprite aEAbstractSprite = null;
		AELayer aELayer = null;
		int num = 0;
		int count = _animationAsset.composition.layers.Count;
		for (int i = 0; i < count; i++)
		{
			aELayer = _animationAsset.composition.layers[i];
			if (aELayer == null || aELayer.index == -1)
			{
				continue;
			}
			if (aELayer.parent != 0)
			{
				list.Add(aELayer);
			}
			if (num < sprites.Count)
			{
				aEAbstractSprite = sprites[num];
				aEAbstractSprite.gameObject.SetActive(value: true);
				aEAbstractSprite.spriteTransform.gameObject.SetActive(value: true);
				aEAbstractSprite.spriteRenderer.enabled = true;
			}
			else
			{
				switch (aELayer.type)
				{
				case AELayerType.FOOTAGE:
					aEAbstractSprite = CreateSprite();
					break;
				default:
					Debug.LogError("Unsupported layer type: " + aELayer.type);
					break;
				case AELayerType.COMPOSITION:
					break;
				}
				aEAbstractSprite.gameObject.layer = gameObject.layer;
				aEAbstractSprite.spriteTransform.gameObject.layer = gameObject.layer;
				sprites.Add(aEAbstractSprite);
			}
			if (aEAbstractSprite.cachedTransform.parent != _spritesHolder)
			{
				aEAbstractSprite.cachedTransform.parent = _spritesHolder;
			}
			InitSprite(aEAbstractSprite, aELayer);
			aEAbstractSprite.SetColor(_color);
			aEAbstractSprite.SetSortingOrder(_sortingOrder);
			aEAbstractSprite.SetSortingLayerName(_sortingLayerName);
			if (Application.isPlaying)
			{
				aEAbstractSprite.SetMaterial(_activeMaterial);
			}
			num++;
		}
		while (num < sprites.Count)
		{
			if (_useSpritePooling)
			{
				aEAbstractSprite = sprites[num];
				aEAbstractSprite.spriteRenderer.enabled = false;
				num++;
			}
			else
			{
				aEAbstractSprite = sprites[sprites.Count - 1];
				UnityEngine.Object.DestroyImmediate(aEAbstractSprite.gameObject);
				sprites.Remove(aEAbstractSprite);
			}
		}
		if (list.Count > 0)
		{
			AEAbstractSprite aEAbstractSprite2 = null;
			AEAbstractSprite aEAbstractSprite3 = null;
			count = list.Count;
			for (int j = 0; j < count; j++)
			{
				aELayer = list[j];
				aEAbstractSprite2 = GetSpriteByLayerIndex(aELayer.parent);
				aEAbstractSprite3 = GetSpriteByLayerIndex(aELayer.index);
				aEAbstractSprite2.AddChild(aEAbstractSprite3);
			}
		}
		GoToAndStop(0);
	}

	public void RefreshPivot()
	{
		if (!(_spritesHolder == null))
		{
			Vector3 zero = Vector3.zero;
			zero.x = (0f - animationAsset.composition.width / (float)pixelsPerMeter) * _pivotX;
			zero.y = animationAsset.composition.height / (float)pixelsPerMeter * _pivotY;
			_spritesHolder.localPosition = zero;
		}
	}

	public void RefreshFlipped()
	{
		Vector3 eulerAngles = transform.localRotation.eulerAngles;
		eulerAngles.y = (_isFlipped ? 180 : 0);
		transform.localRotation = Quaternion.Euler(eulerAngles);
		if (useCachedSprites)
		{
			for (int i = 0; i < _currentAnimationSprites.Count; i++)
			{
				_currentAnimationSprites[i].SetFlipped(_isFlipped);
			}
			return;
		}
		int count = _sprites.Count;
		for (int j = 0; j < count; j++)
		{
			_sprites[j].SetFlipped(_isFlipped);
		}
	}

	public void RefreshMaterial()
	{
		int count = _sprites.Count;
		for (int i = 0; i < count; i++)
		{
			_sprites[i].SetMaterial(_activeMaterial);
		}
	}

	public void SetTintColor(Color newTintColor)
	{
		if (!_materialHasTintColor || _originalMaterial == null)
		{
			Debug.LogError("This material can't be tinted", gameObject);
			return;
		}
		if (newTintColor.a.Equals(0f))
		{
			if (!_activeMaterial.Equals(_originalMaterial))
			{
				_activeMaterial = _originalMaterial;
				RefreshMaterial();
			}
			return;
		}
		if (_uniqueMaterial == null)
		{
			_uniqueMaterial = UnityEngine.Object.Instantiate(_originalMaterial);
			_tintColor = _originalMaterial.GetColor("_TintColor");
		}
		if (!newTintColor.Equals(_tintColor))
		{
			_tintColor = newTintColor;
			_uniqueMaterial.SetColor("_TintColor", _tintColor);
		}
		if (!_activeMaterial.Equals(_uniqueMaterial))
		{
			_activeMaterial = _uniqueMaterial;
			RefreshMaterial();
		}
	}

	public void SetAnimation(AEAnimation newAnimation, float newPivotX, float newPivotY)
	{
		if (!newAnimation.Equals(_animationAsset))
		{
			_animationAsset = newAnimation;
			_pivotX = newPivotX;
			_pivotY = newPivotY;
			Rebuild();
		}
	}

	private void ProcessNewFrame(int newFrame, int direction)
	{
		if (newFrame == _currentFrame)
		{
			return;
		}
		if (EnterFrame != null)
		{
			while (_currentFrame != newFrame)
			{
				_currentFrame += direction;
				if (_currentFrame >= animationAsset.totalFrames)
				{
					_currentFrame = 0;
				}
				else if (_currentFrame < 0)
				{
					_currentFrame = animationAsset.lastFrame;
				}
				if (_currentFrame == newFrame)
				{
					break;
				}
				OnEnterFrame(_currentFrame);
			}
		}
		_currentFrame = newFrame;
		GoToCurrentFrameForced();
		OnEnterFrame(newFrame);
	}

	protected void OnAnimationComplete()
	{
		if (AnimationComplete != null)
		{
			AnimationComplete(this);
		}
	}

	protected void OnEnterFrame(int frame)
	{
		if (EnterFrame != null)
		{
			EnterFrame(frame);
		}
	}

	protected void OnLoopComplete()
	{
		if (LoopComplete != null)
		{
			LoopComplete();
		}
	}

	protected abstract AEAbstractSprite CreateSprite();

	protected abstract void InitSprite(AEAbstractSprite sprite, AELayer layer, bool updateSprite = true);
}
