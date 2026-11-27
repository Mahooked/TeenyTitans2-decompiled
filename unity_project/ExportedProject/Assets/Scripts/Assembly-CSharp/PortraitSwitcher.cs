using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class PortraitSwitcher : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Character> CharacterChanged__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SelectModeEntered__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SelectModeExited__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action DragModeEntered__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action DragModeExited__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action SelectModeExitedLate__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Enabled__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action Disabled__BackingField;

	public tk2dUIItem uiItem;

	public BoxCollider2D boxCollider2D;

	public List<Portrait> portraits;

	public float minDistance;

	public bool scrollWithX;

	public float scrollFactor = 1f;

	public Vector3 positionOffset;

	public bool usePositionBounds;

	public Rect positionBounds = new Rect(-200f, 200f, 400f, 400f);

	public float scaleFactor = 0.01f;

	public float minScale = 0.01f;

	public float maxScale = 2f;

	public float maxSpacing = 20f;

	public Ease onUpEase = Ease.OutElastic;

	public float onUpDuration = 1f;

	public float selectModeActivateDuration = 0.25f;

	public SimpleFaderTk2d blackFade;

	private Camera guiCamera;

	private Vector3 origin;

	private bool isDown;

	private bool isDragging;

	private bool isSelectMode;

	private Vector3 downMousePosition;

	private Vector3 curMousePosition;

	private Vector3 prevMousePosition;

	private bool isDirty;

	private List<Portrait> portraitPool;

	private Vector3 backLimit;

	private Vector3 frontLimit;

	private Vector3 modVector;

	private Tweener offsetTween;

	private Sequence selectModeSequence;

	private Tweener portraitTween;

	private Portrait possibleSelection;

	private float swipeCounter;

	private bool didSwipeOccur;

	private int nextIndex;

	private int currentlyViewedIndex = -1;

	private Character _selectedCharacter;

	private Portrait _selectedPortrait;

	private List<Character> _characters;

	private float _offsetRatio;

	private bool _isEnabled = true;

	private bool useDisabledVisuals = true;

	public Character selectedCharacter
	{
		get
		{
			return _selectedCharacter;
		}
	}

	public Portrait selectedPortrait
	{
		get
		{
			return _selectedPortrait;
		}
	}

	public List<Character> characters
	{
		get
		{
			return _characters;
		}
		set
		{
			Init(value);
		}
	}

	public Vector3 PositionOffset
	{
		get
		{
			return positionOffset;
		}
		set
		{
			if (!(value == positionOffset))
			{
				positionOffset = value;
				backLimit = positionOffset * _characters.Count;
				frontLimit = positionOffset * -1f;
				modVector = positionOffset * portraits.Count;
				isDirty = true;
			}
		}
	}

	public Rect PositionBounds
	{
		get
		{
			return positionBounds;
		}
		set
		{
			if (!(value == positionBounds))
			{
				positionBounds = value;
				isDirty = true;
			}
		}
	}

	public float MinScale
	{
		get
		{
			return minScale;
		}
		set
		{
			if (value != minScale)
			{
				minScale = value;
				isDirty = true;
			}
		}
	}

	public float MaxSpacing
	{
		get
		{
			return maxSpacing;
		}
		set
		{
			if (value != maxSpacing)
			{
				maxSpacing = value;
				isDirty = true;
			}
		}
	}

	public float offsetRatio
	{
		get
		{
			return _offsetRatio;
		}
		set
		{
			if (value != _offsetRatio)
			{
				_offsetRatio = value % 1f;
				if (_offsetRatio < 0f)
				{
					_offsetRatio++;
				}
				isDirty = true;
			}
		}
	}

	public bool IsSelectMode
	{
		get
		{
			return isSelectMode;
		}
	}

	public bool IsPlayerSwitcher
	{
		get
		{
			return BattleController.instance != null && this == BattleController.instance.playerPortraits;
		}
	}

	public bool isEnabled
	{
		get
		{
			return _isEnabled;
		}
		set
		{
			if (value != _isEnabled)
			{
				if (_isEnabled && isDown)
				{
					isSelectMode = true;
					HandleOnRelease();
				}
				_isEnabled = value;
				RefreshPortraitColor();
				if (_isEnabled)
				{
					OnEnabled();
				}
				else
				{
					OnDisabled();
				}
			}
		}
	}

	public bool UseDisabledVisuals
	{
		get
		{
			return useDisabledVisuals;
		}
		set
		{
			useDisabledVisuals = value;
			RefreshPortraitColor();
		}
	}

	public event Action<Character> CharacterChanged
	{
		add
		{
			Action<Character> action = CharacterChanged__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterChanged__BackingField, (Action<Character>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Character> action = CharacterChanged__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterChanged__BackingField, (Action<Character>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action SelectModeEntered
	{
		add
		{
			Action action = SelectModeEntered__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SelectModeEntered__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SelectModeEntered__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SelectModeEntered__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action SelectModeExited
	{
		add
		{
			Action action = SelectModeExited__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SelectModeExited__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SelectModeExited__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SelectModeExited__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action DragModeEntered
	{
		add
		{
			Action action = DragModeEntered__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragModeEntered__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = DragModeEntered__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragModeEntered__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action DragModeExited
	{
		add
		{
			Action action = DragModeExited__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragModeExited__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = DragModeExited__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragModeExited__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action SelectModeExitedLate
	{
		add
		{
			Action action = SelectModeExitedLate__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SelectModeExitedLate__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = SelectModeExitedLate__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref SelectModeExitedLate__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action Enabled
	{
		add
		{
			Action action = Enabled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Enabled__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Enabled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Enabled__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action Disabled
	{
		add
		{
			Action action = Disabled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Disabled__BackingField, (Action)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action action = Disabled__BackingField;
			Action action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref Disabled__BackingField, (Action)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	private void Awake()
	{
		if (uiItem == null)
		{
			uiItem = GetComponent<tk2dUIItem>();
		}
		if (blackFade == null)
		{
			blackFade = GameObject.Find("BlackFade").GetComponent<SimpleFaderTk2d>();
		}
		origin = base.transform.localPosition;
		portraitPool = new List<Portrait>();
		while (portraits.Count > 0)
		{
			Portrait portrait = portraits.Pop();
			portrait.gameObject.SetActive(false);
			portraitPool.Add(portrait);
		}
		guiCamera = GameObject.FindGameObjectWithTag("GUICamera").GetComponent<Camera>();
	}

	private void OnEnable()
	{
		uiItem.OnDownUIItem += HandleOnDown;
		uiItem.OnRelease += HandleOnRelease;
	}

	private void OnDisable()
	{
		uiItem.OnDownUIItem -= HandleOnDown;
		uiItem.OnRelease -= HandleOnRelease;
	}

	private void Update()
	{
		if (!isDown)
		{
			return;
		}
		if (swipeCounter > 0f)
		{
			swipeCounter -= Time.unscaledDeltaTime;
			return;
		}
		curMousePosition = guiCamera.ScreenToWorldPoint(Input.mousePosition);
		if (isDragging)
		{
		}
		prevMousePosition = curMousePosition;
	}

	private void LateUpdate()
	{
		if (isDirty)
		{
			UpdatePositions();
			isDirty = false;
		}
	}

	private void OnDrawGizmosSelected()
	{
		if (usePositionBounds)
		{
			Gizmos.color = Color.cyan;
			Vector3 vector = new Vector3(base.transform.position.x + positionBounds.x, base.transform.position.y + positionBounds.y);
			Vector3 vector2 = new Vector3(vector.x + positionBounds.width, vector.y);
			Vector3 vector3 = new Vector3(vector2.x, vector2.y - positionBounds.height);
			Vector3 vector4 = new Vector3(vector.x, vector3.y);
			Gizmos.DrawLine(vector, vector2);
			Gizmos.DrawLine(vector2, vector3);
			Gizmos.DrawLine(vector3, vector4);
			Gizmos.DrawLine(vector4, vector);
			Gizmos.color = Color.white;
		}
	}

	private void Reset()
	{
		uiItem = GetComponent<tk2dUIItem>();
	}

	public void NextPortrait(bool reverse = false)
	{
		int num = portraits.IndexOf(_selectedPortrait);
		if (reverse)
		{
			num--;
			if (num < 0)
			{
				num = portraits.Count - 1;
			}
		}
		else
		{
			num++;
			if (num >= portraits.Count)
			{
				num = 0;
			}
		}
		SelectIndex(num);
	}

	public void Init(List<Character> newCharacters)
	{
		if (newCharacters != _characters)
		{
			_characters = newCharacters;
			backLimit = positionOffset * _characters.Count;
			frontLimit = positionOffset * -1f;
			Portrait portrait = null;
			int num = _characters.Count * 2;
			for (int i = 0; i < num; i++)
			{
				portrait = portraitPool.Pop();
				portrait.Init(_characters[i % _characters.Count]);
				portrait.gameObject.SetActive(true);
				portraits.Add(portrait);
			}
			modVector = positionOffset * portraits.Count;
			_selectedCharacter = portraits[0].character;
			_selectedPortrait = portraits[0];
			_offsetRatio = 0f;
			isDirty = true;
		}
	}

	public unsafe void SelectCharacter(Character character, bool forceEventCall = false)
	{
		Portrait portrait = null;
		int count = portraits.Count;
		for (int i = 0; i < count; i++)
		{
			if (portraits[i].character == character)
			{
				if (portrait == null)
				{
					portrait = portraits[i];
				}
				else if (Mathf.Abs(portraits[i].transform.localPosition.y) < Mathf.Abs(portrait.transform.localPosition.y))
				{
					portrait = portraits[i];
				}
			}
		}
		if (portrait != null)
		{
			if (_selectedCharacter != character || forceEventCall)
			{
				_selectedCharacter = character;
				OnCharacterChanged(_selectedCharacter);
			}
			_selectedPortrait = portrait;
			if (offsetTween != null)
			{
				offsetTween.Kill();
				offsetTween = null;
			}
			float num = 1f - positionOffset.y * (float)portraits.IndexOf(_selectedPortrait) / modVector.y;
			float num2 = num + 1f;
			float num3 = num - 1f;
			if (Mathf.Abs(num2 - offsetRatio) < Mathf.Abs(num - offsetRatio))
			{
				num = num2;
			}
			if (Mathf.Abs(num3 - offsetRatio) < Mathf.Abs(num - offsetRatio))
			{
				num = num3;
			}
			offsetTween = DOTween.To(new DOGetter<float>(() => this.offsetRatio), (float x) =>
			{
				offsetRatio = x;
			}, num, onUpDuration).SetEase(onUpEase).SetUpdate(true)
				.OnComplete(OnOffsetTweenComplete);
		}
	}

	public void RefreshClassBonusIcons(CharacterClass opposingClass)
	{
		int count = portraits.Count;
		for (int i = 0; i < count; i++)
		{
			portraits[i].RefreshClassBonusIcon(opposingClass);
		}
	}

	private void RefreshPortraitColor()
	{
		Color portraitColor = ((_isEnabled || !UseDisabledVisuals) ? Color.white : new Color(160f / 255f, 160f / 255f, 160f / 255f));
		int count = portraits.Count;
		for (int i = 0; i < count; i++)
		{
			portraits[i].portraitColor = portraitColor;
		}
	}

	private unsafe void SelectIndex(int index)
	{
		if (index < 0 || index >= portraits.Count)
		{
			return;
		}
		Portrait portrait = portraits[index];
		if (_selectedCharacter != portrait.character)
		{
			if (BattleController.battleDefinition.playerAIType == PlayerAIType.Network && IsPlayerSwitcher)
			{
				StartCoroutine(WaitForSwitchConfirmation(portrait, portrait.character, index));
				return;
			}
			_selectedCharacter = portrait.character;
			OnCharacterChanged(_selectedCharacter);
		}
		_selectedPortrait = portrait;
		if (offsetTween != null)
		{
			offsetTween.Kill();
			offsetTween = null;
		}
		float num = 1f - positionOffset.y * (float)index / modVector.y;
		float num2 = num + 1f;
		float num3 = num - 1f;
		if (Mathf.Abs(num2 - offsetRatio) < Mathf.Abs(num - offsetRatio))
		{
			num = num2;
		}
		if (Mathf.Abs(num3 - offsetRatio) < Mathf.Abs(num - offsetRatio))
		{
			num = num3;
		}
		offsetTween = DOTween.To(new DOGetter<float>(() => this.offsetRatio), (float x) =>
		{
			offsetRatio = x;
		}, num, onUpDuration).SetEase(onUpEase).SetUpdate(true)
			.OnComplete(OnOffsetTweenComplete);
	}

	private IEnumerator WaitForSwitchConfirmation(Portrait portrait, Character character, int index)
	{
		BattleController.instance.ForceStartWaiting();
		TT2NetPlayerAction playerAction = TT2NetManager.SendCharacterSwap(character.characterDefinition.uid);
		yield return new WaitWhile(() => playerAction.isWaiting);
		if (!playerAction.isConfirmed)
		{
			BattleController.instance.ForceEndWaiting();
			yield break;
		}
		_selectedCharacter = character;
		OnCharacterChanged(_selectedCharacter);
		_selectedPortrait = portrait;
		if (offsetTween != null)
		{
			offsetTween.Kill();
			offsetTween = null;
		}
		float newRatio1 = 1f - positionOffset.y * (float)index / modVector.y;
		float newRatio2 = newRatio1 + 1f;
		float newRatio3 = newRatio1 - 1f;
		if (Mathf.Abs(newRatio2 - offsetRatio) < Mathf.Abs(newRatio1 - offsetRatio))
		{
			newRatio1 = newRatio2;
		}
		if (Mathf.Abs(newRatio3 - offsetRatio) < Mathf.Abs(newRatio1 - offsetRatio))
		{
			newRatio1 = newRatio3;
		}
		offsetTween = DOTween.To(new DOGetter<float>(() => this.offsetRatio), (float x) =>
		{
			offsetRatio = x;
		}, newRatio1, onUpDuration).SetEase(onUpEase).SetUpdate(true)
			.OnComplete(OnOffsetTweenComplete);
	}

	private void UpdatePositions()
	{
		Vector3 vector = new Vector3(modVector.x * _offsetRatio, modVector.y * _offsetRatio, modVector.z * _offsetRatio);
		Portrait portrait = null;
		int count = portraits.Count;
		for (int i = 0; i < count; i++)
		{
			portrait = portraits[i];
			Vector3 vector2 = vector;
			if ((positionOffset.y < 0f && vector2.y > 0f - positionOffset.y) || (positionOffset.y > 0f && vector2.y < 0f - positionOffset.y))
			{
				vector2 += positionOffset * portraits.Count;
			}
			else if ((positionOffset.y < 0f && vector2.y < (positionOffset * (portraits.Count - 1)).y) || (positionOffset.y > 0f && vector2.y > (positionOffset * (portraits.Count - 1)).y))
			{
				vector2 += -positionOffset * portraits.Count;
			}
			portrait.transform.localPosition = vector2;
			portrait.unboundedPosition = vector2;
			UpdatePortraitScale(portrait);
			if (usePositionBounds)
			{
				Vector3 bottomLeft = portrait.BottomLeft;
				Vector3 topRight = portrait.TopRight;
				if (bottomLeft.x < positionBounds.xMin)
				{
					vector2.x += positionBounds.xMin - bottomLeft.x;
				}
				else if (topRight.x > positionBounds.xMax)
				{
					vector2.x += positionBounds.xMax - topRight.x;
				}
				if (bottomLeft.y < positionBounds.y - positionBounds.height)
				{
					vector2.y += positionBounds.y - positionBounds.height - bottomLeft.y;
				}
				else if (topRight.y > positionBounds.y)
				{
					vector2.y += positionBounds.y - topRight.y;
				}
				portrait.transform.localPosition = vector2;
				if (i > 0)
				{
					bottomLeft = portrait.BottomLeft;
					topRight = portrait.TopRight;
					Vector3 bottomLeft2 = portraits[i - 1].BottomLeft;
					Vector3 topRight2 = portraits[i - 1].TopRight;
					if (bottomLeft.y - topRight2.y > maxSpacing)
					{
						vector2.y += maxSpacing - (bottomLeft.y - topRight2.y);
					}
					portrait.transform.localPosition = vector2;
				}
				UpdatePortraitScale(portrait);
				if (i == count - 1)
				{
					vector2 = portraits[0].transform.localPosition;
					bottomLeft = portraits[0].BottomLeft;
					topRight = portraits[0].TopRight;
					Vector3 bottomLeft3 = portrait.BottomLeft;
					Vector3 topRight3 = portrait.TopRight;
					if (bottomLeft.y - topRight3.y > maxSpacing)
					{
						vector2.y += maxSpacing - (bottomLeft.y - topRight3.y);
					}
					portraits[0].transform.localPosition = vector2;
					UpdatePortraitScale(portraits[0]);
					UpdatePortraitAlpha(portraits[0]);
				}
			}
			UpdatePortraitAlpha(portrait);
			vector += positionOffset;
		}
	}

	private void UpdatePortraitScale(Portrait portrait)
	{
		Vector3 localScale = portrait.transform.localScale;
		localScale.x = (localScale.y = Mathf.Clamp(1f + (0f - portrait.transform.localPosition.y) * scaleFactor, minScale, maxScale));
		portrait.transform.localScale = localScale;
	}

	private void UpdatePortraitAlpha(Portrait portrait)
	{
		if ((positionOffset.y < 0f && portrait.unboundedPosition.y > 0f) || (positionOffset.y > 0f && portrait.unboundedPosition.y < 0f))
		{
			portrait.alpha = 1f - Mathf.Abs(portrait.unboundedPosition.y / frontLimit.y);
		}
		else if ((positionOffset.y < 0f && portrait.unboundedPosition.y < backLimit.y - positionOffset.y) || (positionOffset.y > 0f && portrait.unboundedPosition.y > backLimit.y - positionOffset.y))
		{
			portrait.alpha = 1f - Mathf.Abs((portrait.unboundedPosition.y - (backLimit.y - positionOffset.y)) / (backLimit.y - (backLimit.y - positionOffset.y)));
		}
		else
		{
			portrait.alpha = 1f;
		}
	}

	private void HandleOnDown(tk2dUIItem uiItem)
	{
		if (!_isEnabled || _characters.Count == 1)
		{
			return;
		}
		if (!isSelectMode && uiItem == this.uiItem)
		{
			DOTween.To(() => _selectedPortrait.contentHolder.localScale, (Vector3 x) =>
			{
				_selectedPortrait.contentHolder.localScale = x;
			}, new Vector3(1.25f, 1.25f, 1f), 0.05f).SetEase(Ease.OutSine).SetUpdate(true);
			swipeCounter = 0.2f;
		}
		isDown = true;
		downMousePosition = Input.mousePosition;
		curMousePosition = (prevMousePosition = guiCamera.ScreenToWorldPoint(Input.mousePosition));
		Vector3 localPosition = base.transform.localPosition;
		localPosition.z = blackFade.transform.localPosition.z - 100f;
		base.transform.localPosition = localPosition;
		blackFade.FadeTo(0.5f, selectModeActivateDuration);
		if (BattleController.battleDefinition.playerAIType != PlayerAIType.Network)
		{
			Time.timeScale = 0f;
		}
		possibleSelection = uiItem.GetComponent<Portrait>();
	}

	private void HandleSwipeOccurred(SwipeResult result)
	{
		nextIndex = portraits.IndexOf(_selectedPortrait);
		if (result.direction2D.y > 0f)
		{
			if (positionOffset.y > 0f)
			{
				nextIndex--;
				if (nextIndex < 0)
				{
					nextIndex = portraits.Count - 1;
				}
			}
			else
			{
				nextIndex++;
				if (nextIndex >= portraits.Count)
				{
					nextIndex = 0;
				}
			}
		}
		else if (positionOffset.y > 0f)
		{
			nextIndex++;
			if (nextIndex >= portraits.Count)
			{
				nextIndex = 0;
			}
		}
		else
		{
			nextIndex--;
			if (nextIndex < 0)
			{
				nextIndex = portraits.Count - 1;
			}
		}
		didSwipeOccur = true;
	}

	private unsafe void HandleOnRelease()
	{
		if (!_isEnabled || _characters.Count == 1 || !isDown)
		{
			return;
		}
		isDown = false;
		Portrait prevSelectedPortrait = _selectedPortrait;
		if (BattleController.instance.batteryGUI != null && BattleController.instance.batteryGUI.isUsingBattery)
		{
			ExitSelectMode();
			DOTween.To(() => prevSelectedPortrait.contentHolder.localScale, (Vector3 x) =>
			{
				prevSelectedPortrait.contentHolder.localScale = x;
			}, Vector3.one, 0.05f).SetEase(Ease.OutSine).SetUpdate(true);
			DOTween.To(() => _selectedPortrait.contentHolder.localScale, (Vector3 x) =>
			{
				_selectedPortrait.contentHolder.localScale = x;
			}, Vector3.one, 0.05f).SetEase(Ease.OutSine).SetUpdate(true);
			BattleController.instance.batteryGUI.UseBatteryHeal();
			return;
		}
		if (didSwipeOccur)
		{
			didSwipeOccur = false;
			if (portraits[nextIndex].character.isDead || portraits[nextIndex].character.isKnockedOut)
			{
				SelectCharacter(_selectedCharacter);
			}
			else
			{
				SelectIndex(nextIndex);
			}
			blackFade.FadeTo(0f, selectModeActivateDuration, () =>
			{
				base.transform.localPosition = origin;
			});
			Time.timeScale = 1f;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_characterselector_switchchars", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else if (!isSelectMode && !isDragging)
		{
			if (selectModeSequence != null)
			{
				selectModeSequence.Kill();
			}
			selectModeSequence = DOTween.Sequence().SetEase(Ease.InOutSine).SetUpdate(true)
				.OnComplete(OnSelectModeSequenceComplete);
			selectModeSequence.Append(DOTween.To(new DOGetter<Vector3>(() => this.PositionOffset), (Vector3 x) =>
			{
				PositionOffset = x;
			}, new Vector3(-4f, 66f, 5f), selectModeActivateDuration));
			selectModeSequence.Insert(0f, DOTween.To(new DOGetter<Rect>(() => this.PositionBounds), (Rect x) =>
			{
				PositionBounds = x;
			}, new Rect(-28f, 300f, 400f, 400f), selectModeActivateDuration));
			selectModeSequence.Insert(0f, DOTween.To(new DOGetter<float>(() => this.MinScale), (float x) =>
			{
				MinScale = x;
			}, 0.809f, selectModeActivateDuration));
			selectModeSequence.Play();
			isSelectMode = true;
			blackFade.uiItem.OnClick += HandleOnRelease;
			int count = portraits.Count;
			for (int num = 0; num < count; num++)
			{
				portraits[num].uiItemCollider.enabled = true;
				portraits[num].uiItem.OnDownUIItem += HandleOnDown;
				portraits[num].uiItem.OnRelease += HandleOnRelease;
			}
			if (SelectModeEntered__BackingField != null)
			{
				SelectModeEntered__BackingField();
			}
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_characterselector_open", GFAudioManager.SFX_GROUP, string.Empty);
		}
		else
		{
			isDragging = false;
			if (possibleSelection == null)
			{
				Character character = portraits[GetClosestPortraitIndex()].character;
				SelectCharacter((!character.isDead && !character.isKnockedOut) ? character : _selectedCharacter);
			}
			else
			{
				if (possibleSelection.character.isDead || possibleSelection.character.isKnockedOut)
				{
					SelectCharacter(_selectedCharacter);
				}
				else
				{
					SelectIndex(portraits.IndexOf(possibleSelection));
				}
				possibleSelection = null;
			}
			ExitSelectMode();
		}
		DOTween.To(() => prevSelectedPortrait.contentHolder.localScale, (Vector3 x) =>
		{
			prevSelectedPortrait.contentHolder.localScale = x;
		}, Vector3.one, 0.05f).SetEase(Ease.OutSine).SetUpdate(true);
		DOTween.To(() => _selectedPortrait.contentHolder.localScale, (Vector3 x) =>
		{
			_selectedPortrait.contentHolder.localScale = x;
		}, Vector3.one, 0.05f).SetEase(Ease.OutSine).SetUpdate(true);
	}

	public unsafe void ExitSelectMode()
	{
		blackFade.FadeTo(0f, selectModeActivateDuration, () =>
		{
			base.transform.localPosition = origin;
		});
		if (selectModeSequence != null)
		{
			selectModeSequence.Kill();
		}
		selectModeSequence = DOTween.Sequence().SetEase(Ease.InOutSine).SetUpdate(true)
			.OnComplete(OnSelectModeSequenceComplete);
		selectModeSequence.Append(DOTween.To(new DOGetter<Vector3>(() => this.PositionOffset), (Vector3 x) =>
		{
			PositionOffset = x;
		}, new Vector3(-12f, 36f, 5f), selectModeActivateDuration));
		selectModeSequence.Insert(0f, DOTween.To(new DOGetter<Rect>(() => this.PositionBounds), (Rect x) =>
		{
			PositionBounds = x;
		}, new Rect(-54f, 300f, 400f, 400f), selectModeActivateDuration));
		selectModeSequence.Insert(0f, DOTween.To(new DOGetter<float>(() => this.MinScale), (float x) =>
		{
			MinScale = x;
		}, 0.6f, selectModeActivateDuration));
		selectModeSequence.Play();
		if (isSelectMode && SelectModeExited__BackingField != null)
		{
			SelectModeExited__BackingField();
		}
		isSelectMode = false;
		blackFade.uiItem.OnClick -= HandleOnRelease;
		int count = portraits.Count;
		for (int num = 0; num < count; num++)
		{
			portraits[num].uiItemCollider.enabled = false;
			portraits[num].uiItem.OnDownUIItem -= HandleOnDown;
			portraits[num].uiItem.OnRelease -= HandleOnRelease;
		}
		if (DragModeExited__BackingField != null)
		{
			DragModeExited__BackingField();
		}
		if (BattleController.battleDefinition.playerAIType != PlayerAIType.Network)
		{
			Time.timeScale = 1f;
		}
		GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_gui_characterselector_switchchars", GFAudioManager.SFX_GROUP, string.Empty);
	}

	private int GetClosestPortraitIndex()
	{
		int i;
		for (i = Mathf.FloorToInt((modVector.y * _offsetRatio - positionOffset.y / 2f) / (0f - positionOffset.y)); i < 0; i += portraits.Count)
		{
		}
		while (i >= portraits.Count)
		{
			i -= portraits.Count;
		}
		return i;
	}

	private void OnOffsetTweenComplete()
	{
		offsetTween = null;
	}

	private void OnSelectModeSequenceComplete()
	{
		selectModeSequence = null;
		if (!isSelectMode && SelectModeExitedLate__BackingField != null)
		{
			SelectModeExitedLate__BackingField();
		}
	}

	private void OnPortraitTweenComplete()
	{
		portraitTween = null;
	}

	protected void OnCharacterChanged(Character character)
	{
		if (CharacterChanged__BackingField != null)
		{
			CharacterChanged__BackingField(character);
		}
	}

	protected void OnEnabled()
	{
		if (Enabled__BackingField != null)
		{
			Enabled__BackingField();
		}
	}

	protected void OnDisabled()
	{
		if (Disabled__BackingField != null)
		{
			Disabled__BackingField();
		}
	}
}

