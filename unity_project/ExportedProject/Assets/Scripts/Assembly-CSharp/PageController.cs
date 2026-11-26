using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

public class PageController : MonoBehaviour
{
	public enum ViewMode
	{
		Single = 0,
		Folder = 1
	}

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<Character> CharacterSelected__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CharacterSelectPortrait> DragFinished__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<CharacterSelectPortrait> CharacterPortraitCreated__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<int> CharacterFolderSelected__BackingField;

	public tk2dUIItem uiItem;

	public TextMeshPro pageNumberText;

	public PageDots pageDots;

	public tk2dUIToggleButtonGroup pageDotGroup;

	public Transform pageContainer;

	public int rowsPerPage = 3;

	public int columnsPerPage = 3;

	public Vector3 topLeftPosition;

	public Vector2 positionStep;

	public float pageWidth = 100f;

	public float startDragDistance = 0.5f;

	public float swipeDistance = 0.5f;

	public float portraitDragDelay = 0.5f;

	public Camera lowerCamera;

	public Camera upperCamera;

	public string upperCameraLayerName = "UI3";

	public GameObject portraitPrefab;

	public GameObject pagePrefab;

	public bool dragEnabled = true;

	public bool dragNavigationEnabled = true;

	public bool dotNavigationEnabled = true;

	public bool portraitSelectionEnabled = true;

	private InitPortaitDelegate initPortraitCallback;

	private InitFolderPortraitDelegate initFolderPortraitCallback;

	private bool isInitialized;

	private List<Page> pagePool = new List<Page>();

	protected bool isDown;

	protected bool isDragging;

	protected Vector3 downPosition;

	protected Vector3 curPosition;

	protected Vector3 prevPosition;

	protected List<Vector3> points = new List<Vector3>();

	protected List<float> timestamps = new List<float>();

	protected CharacterSelectPortrait downPortrait;

	protected float portraitDragCounter;

	protected bool isDraggingPortrait;

	protected Vector3 portraitDragOffset;

	protected CharacterSelectPortrait dragPortrait;

	protected int numItemsPerPage;

	protected float maxScrollPos;

	protected int maxPageIndex;

	protected int firstVisible = -1;

	protected int lastVisible = -1;

	protected int prevFirstVisible = -1;

	protected int prevLastVisible = -1;

	protected List<Page> _pages = new List<Page>();

	private bool forceRepopulate;

	private Tweener moveTween;

	private List<CharacterDefinition> _filteredGroupedCharacters = new List<CharacterDefinition>();

	protected float _scrollPos;

	protected int _pageIndex;

	private List<Character> _filteredCharacters = new List<Character>();

	private List<Character> _masterListCharacters = new List<Character>();

	public float scrollPos
	{
		get
		{
			return _scrollPos;
		}
		set
		{
			if (value != _scrollPos)
			{
				_scrollPos = value;
				UpdatePosition();
			}
		}
	}

	public unsafe int pageIndex
	{
		get
		{
			return _pageIndex;
		}
		set
		{
			_pageIndex = Mathf.Max(0, value);
			if (pageNumberText != null)
			{
				pageNumberText.text = string.Format("{0}/{1}", _pageIndex + 1, maxPageIndex + 1);
			}
			if (moveTween != null)
			{
				moveTween.Kill();
			}
			moveTween = DOTween.To(new DOGetter<float>(() => this.scrollPos), (float x) =>
			{
				scrollPos = x;
			}, (float)_pageIndex * pageWidth, 0.25f).SetEase(Ease.OutSine).OnComplete(OnMoveComplete);
			pageDotGroup.SelectedIndex = _pageIndex;
			GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_scrollcharpages", GFAudioManager.SFX_GROUP, string.Empty);
		}
	}

	public List<Character> filteredCharacters
	{
		get
		{
			return _filteredCharacters;
		}
	}

	public List<Character> masterListCharacters
	{
		get
		{
			return _masterListCharacters;
		}
	}

	public ViewMode currentViewMode { get; private set; }

	public Page currentPage
	{
		get
		{
			return _pages[pageIndex];
		}
	}

	public event Action<Character> CharacterSelected
	{
		add
		{
			Action<Character> action = CharacterSelected__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action<Character>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<Character> action = CharacterSelected__BackingField;
			Action<Character> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterSelected__BackingField, (Action<Character>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<CharacterSelectPortrait> DragFinished
	{
		add
		{
			Action<CharacterSelectPortrait> action = DragFinished__BackingField;
			Action<CharacterSelectPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragFinished__BackingField, (Action<CharacterSelectPortrait>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CharacterSelectPortrait> action = DragFinished__BackingField;
			Action<CharacterSelectPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragFinished__BackingField, (Action<CharacterSelectPortrait>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<CharacterSelectPortrait> CharacterPortraitCreated
	{
		add
		{
			Action<CharacterSelectPortrait> action = CharacterPortraitCreated__BackingField;
			Action<CharacterSelectPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterPortraitCreated__BackingField, (Action<CharacterSelectPortrait>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<CharacterSelectPortrait> action = CharacterPortraitCreated__BackingField;
			Action<CharacterSelectPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterPortraitCreated__BackingField, (Action<CharacterSelectPortrait>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<int> CharacterFolderSelected
	{
		add
		{
			Action<int> action = CharacterFolderSelected__BackingField;
			Action<int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterFolderSelected__BackingField, (Action<int>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<int> action = CharacterFolderSelected__BackingField;
			Action<int> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref CharacterFolderSelected__BackingField, (Action<int>)Delegate.Remove(action2, value), action);
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
		pageDotGroup.OnChange += HandleOnPageDotChange;
		if (pageNumberText != null)
		{
			pageNumberText.text = string.Format("{0}/{1}", "-", "-");
		}
		numItemsPerPage = rowsPerPage * columnsPerPage;
		maxScrollPos = pageWidth * (float)Mathf.Max(0, maxPageIndex);
		pagePool = new List<Page>();
		for (int i = 0; i < 2; i++)
		{
			Page component = UnityEngine.Object.Instantiate(pagePrefab).GetComponent<Page>();
			component.gameObject.SetLayerRecursively(base.gameObject.layer);
			component.items = new List<CharacterSelectPortrait>();
			component.transform.parent = pageContainer;
			component.transform.localPosition = Vector3.zero;
			for (int j = 0; j < numItemsPerPage; j++)
			{
				CharacterSelectPortrait component2 = UnityEngine.Object.Instantiate(portraitPrefab).GetComponent<CharacterSelectPortrait>();
				component2.gameObject.SetLayerRecursively(base.gameObject.layer);
				component2.transform.parent = component.transform;
				component2.uiItem.UpdateParent();
				component2.uiItem.OnDownUIItem += HandleOnDownPortrait;
				component2.gameObject.SetActive(false);
				component.items.Add(component2);
			}
			component.gameObject.SetActive(false);
			pagePool.Add(component);
		}
		isInitialized = true;
	}

	private void HandleOnDownPortrait(tk2dUIItem obj)
	{
		downPortrait = obj.GetComponent<CharacterSelectPortrait>();
		if (downPortrait.mode == CharacterSelectPortraitMode.Normal)
		{
			portraitDragCounter = 0f;
		}
		else
		{
			downPortrait.uiItem.Release(true);
		}
	}

	private void OnEnable()
	{
		uiItem.OnDown += HandleOnDown;
		uiItem.OnRelease += HandleOnRelease;
		ForceUpdate(true);
	}

	private void OnDisable()
	{
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnRelease -= HandleOnRelease;
	}

	private void Update()
	{
		if (isDown)
		{
			curPosition = Input.mousePosition;
			if (isDragging)
			{
				points.Add(curPosition - prevPosition);
				timestamps.Add(Time.time);
				if (dragNavigationEnabled)
				{
					Vector3 vector = lowerCamera.ScreenToWorldPoint(prevPosition) - lowerCamera.ScreenToWorldPoint(curPosition);
					if (scrollPos + vector.x < 0f || scrollPos + vector.x > maxScrollPos)
					{
						vector.x *= 0.5f;
					}
					scrollPos += vector.x;
				}
			}
			else
			{
				Vector2 vector2 = new Vector2(curPosition.x - downPosition.x, curPosition.y - downPosition.y);
				if (Mathf.Abs(vector2.x) / Screen.dpi >= startDragDistance && Mathf.Abs(vector2.x / vector2.y) > 1.6f)
				{
					isDragging = true;
					if (downPortrait != null)
					{
						downPortrait.uiItem.Release(true);
					}
					downPortrait = null;
				}
				else if (downPortrait != null && downPortrait.mode == CharacterSelectPortraitMode.Normal && downPortrait.viewMode == CharacterSelectPortrait.ViewMode.Single)
				{
					portraitDragCounter += Time.unscaledDeltaTime;
					if (dragEnabled && (portraitDragCounter >= portraitDragDelay || Mathf.Abs(vector2.y) / Screen.dpi >= startDragDistance))
					{
						downPortrait.uiItem.Release(true);
						isDraggingPortrait = true;
						isDown = false;
						dragPortrait = UnityEngine.Object.Instantiate(downPortrait);
						dragPortrait.character = downPortrait.character;
						dragPortrait.transform.position = downPortrait.transform.position;
						dragPortrait.transform.localScale = Vector3.one;
						dragPortrait.gameObject.SetLayerRecursively(LayerMask.NameToLayer(upperCameraLayerName));
						Vector3 vector3 = lowerCamera.ScreenToWorldPoint(curPosition);
						portraitDragOffset = new Vector3(dragPortrait.transform.position.x - vector3.x, dragPortrait.transform.position.y - vector3.y);
						OnCharacterPortaitCreated(dragPortrait);
					}
				}
			}
			prevPosition = curPosition;
		}
		else if (isDraggingPortrait)
		{
			curPosition = Input.mousePosition;
			Vector3 vector4 = lowerCamera.ScreenToWorldPoint(curPosition);
			Vector3 position = dragPortrait.transform.position;
			position.x = vector4.x + portraitDragOffset.x;
			position.y = vector4.y + portraitDragOffset.y;
			dragPortrait.transform.position = position;
			prevPosition = curPosition;
		}
	}

	public void Init(InitPortaitDelegate callback, InitFolderPortraitDelegate folderCallback)
	{
		initPortraitCallback = callback;
		initFolderPortraitCallback = folderCallback;
	}

	public void SetList(List<Character> characters, ViewMode viewMode, bool animateButtonsIn = false, bool recordAsMasterList = false)
	{
		if (!isInitialized)
		{
			return;
		}
		currentViewMode = viewMode;
		if (characters.SequenceEqual(_filteredCharacters))
		{
			UpdatePages();
			return;
		}
		if (recordAsMasterList)
		{
			_masterListCharacters.Clear();
			_masterListCharacters.AddRange(characters);
		}
		_filteredCharacters.Clear();
		_filteredCharacters.AddRange(characters);
		_filteredGroupedCharacters.Clear();
		for (int i = 0; i < _filteredCharacters.Count; i++)
		{
			Character character = _filteredCharacters[i];
			bool flag = false;
			for (int j = 0; j < _filteredGroupedCharacters.Count; j++)
			{
				if (character.characterDefinition.uid == _filteredGroupedCharacters[j].uid)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				_filteredGroupedCharacters.Add(character.characterDefinition);
			}
		}
		maxPageIndex = Mathf.Max(0, Mathf.CeilToInt((float)((currentViewMode != ViewMode.Folder) ? _filteredCharacters.Count : _filteredGroupedCharacters.Count) / (float)numItemsPerPage) - 1);
		maxScrollPos = pageWidth * (float)maxPageIndex;
		_pageIndex = 0;
		_scrollPos = 0f;
		firstVisible = -1;
		lastVisible = -1;
		prevFirstVisible = -1;
		prevLastVisible = -1;
		while (_pages.Count > 0)
		{
			ReturnPage(_pages.Pop());
		}
		pageDots.numDots = maxPageIndex + 1;
		pageDotGroup.SelectedIndex = 0;
		if (pageNumberText != null)
		{
			pageNumberText.text = string.Format("{0}/{1}", _pageIndex + 1, maxPageIndex + 1);
		}
		if (currentViewMode == ViewMode.Single)
		{
			ReturnExitButtonControl();
		}
		UpdatePosition(animateButtonsIn);
	}

	public void ForceUpdate(bool animateButtonsIn = false)
	{
		forceRepopulate = true;
		UpdatePosition(animateButtonsIn);
	}

	protected void UpdatePosition(bool animateButtonsIn = false)
	{
		if (!isInitialized)
		{
			return;
		}
		firstVisible = Mathf.FloorToInt(scrollPos / pageWidth);
		if (firstVisible < 0)
		{
			firstVisible = 0;
		}
		else if (firstVisible > maxPageIndex)
		{
			firstVisible = maxPageIndex;
		}
		lastVisible = firstVisible;
		for (float num = 0f - scrollPos + pageWidth * (float)firstVisible + pageWidth; num >= 0f && num < pageWidth; num += pageWidth)
		{
			lastVisible++;
			if (lastVisible > maxPageIndex)
			{
				lastVisible = maxPageIndex;
				break;
			}
		}
		while (_pages.Count > lastVisible - firstVisible + 1)
		{
			ReturnPage(_pages.Pop());
		}
		bool flag = firstVisible != prevFirstVisible || lastVisible != prevLastVisible || forceRepopulate;
		float num2 = 0f - scrollPos + pageWidth * (float)firstVisible;
		int num3 = firstVisible;
		int num4 = lastVisible - firstVisible + 1;
		for (int i = 0; i < num4; i++)
		{
			if (i == _pages.Count)
			{
				_pages.Add(GetPage());
			}
			Page page = _pages[i];
			if (flag)
			{
				PopulatePage(page, numItemsPerPage * num3, animateButtonsIn);
			}
			Vector3 localPosition = page.transform.localPosition;
			localPosition.x = num2;
			page.transform.localPosition = localPosition;
			num2 += pageWidth;
			num3++;
		}
		prevFirstVisible = firstVisible;
		prevLastVisible = lastVisible;
		forceRepopulate = false;
	}

	public void UpdatePages()
	{
		if (isInitialized)
		{
			int num = firstVisible;
			int count = _pages.Count;
			for (int i = 0; i < count; i++)
			{
				PopulatePage(_pages[i], numItemsPerPage * num);
				num++;
			}
		}
	}

	protected virtual void PopulatePage(Page page, int startIndex, bool animate = false)
	{
		Vector3 localPosition = topLeftPosition;
		int num = startIndex + numItemsPerPage;
		for (int i = startIndex; i < num; i++)
		{
			int num2 = i % numItemsPerPage;
			CharacterSelectPortrait characterSelectPortrait = page.items[num2];
			if ((currentViewMode == ViewMode.Single && i < _filteredCharacters.Count) || (currentViewMode == ViewMode.Folder && i < _filteredGroupedCharacters.Count))
			{
				if (!characterSelectPortrait.gameObject.activeSelf)
				{
					characterSelectPortrait.gameObject.SetActive(true);
				}
				if (currentViewMode == ViewMode.Single)
				{
					initPortraitCallback(characterSelectPortrait, _filteredCharacters[i], animate);
				}
				else
				{
					int num3 = 0;
					CharacterDefinition definition = _filteredGroupedCharacters[i];
					for (int j = 0; j < _filteredCharacters.Count; j++)
					{
						if (definition.uid == _filteredCharacters[j].characterDefinition.uid)
						{
							num3++;
						}
					}
					if (num3 > 1)
					{
						initFolderPortraitCallback(characterSelectPortrait, definition, num3, animate);
					}
					else
					{
						Character character = _filteredCharacters.Find((Character x) => x.characterDefinition.uid == definition.uid);
						initPortraitCallback(characterSelectPortrait, character, animate);
					}
				}
				localPosition.x = topLeftPosition.x + positionStep.x * (float)(i % columnsPerPage);
				localPosition.y = topLeftPosition.y + positionStep.y * (float)Mathf.FloorToInt((float)num2 / (float)columnsPerPage);
				characterSelectPortrait.transform.localPosition = localPosition;
			}
			else
			{
				characterSelectPortrait.gameObject.SetActive(false);
			}
		}
	}

	public void GoToPageWithCharacter(Character character)
	{
		int num = _filteredCharacters.IndexOf(character);
		if (num > -1)
		{
			pageIndex = num / numItemsPerPage;
			UnityEngine.Debug.Log(string.Format("Page index is {0}", pageIndex));
		}
	}

	public Page GetPageWithCharacter(Character character)
	{
		return _pages.Find((Page page) => page.items.Find((CharacterSelectPortrait portrait) => portrait.character == character));
	}

	private void HandleOnDown()
	{
		isDown = true;
		if (moveTween != null)
		{
			moveTween.Kill();
			moveTween = null;
		}
		downPosition = (curPosition = (prevPosition = Input.mousePosition));
	}

	protected virtual void HandleOnRelease()
	{
		isDown = false;
		if (isDragging)
		{
			isDragging = false;
			points.Add(curPosition - prevPosition);
			timestamps.Add(Time.time);
			float num = Time.time - 0.2f;
			Vector3 vector = default(Vector3);
			int num2 = points.Count - 1;
			int num3 = num2;
			while (num3 >= 0 && !(timestamps[num3] < num))
			{
				vector += points[num3];
				num3--;
			}
			if (dragNavigationEnabled)
			{
				if (Mathf.Abs(vector.x) / Screen.dpi >= swipeDistance)
				{
					if (_pageIndex > 0 && vector.x > 0f)
					{
						pageIndex--;
					}
					else if (_pageIndex < maxPageIndex && vector.x < 0f)
					{
						pageIndex++;
					}
					else
					{
						pageIndex = Mathf.Clamp(Mathf.RoundToInt(scrollPos / pageWidth), 0, maxPageIndex);
					}
				}
				else
				{
					pageIndex = Mathf.Clamp(Mathf.RoundToInt(scrollPos / pageWidth), 0, maxPageIndex);
				}
			}
		}
		else if (isDraggingPortrait)
		{
			UnityEngine.Debug.Log("Stop Dragging Portrait");
			OnDragFinished(dragPortrait);
			isDraggingPortrait = false;
			downPortrait = null;
			UnityEngine.Object.Destroy(dragPortrait.gameObject);
			dragPortrait = null;
		}
		else if (portraitSelectionEnabled && downPortrait != null && Vector3.Distance(downPosition, Input.mousePosition) / Screen.dpi < 0.2f)
		{
			if (downPortrait.viewMode == CharacterSelectPortrait.ViewMode.Single)
			{
				ReturnExitButtonControl();
				OnCharacterSelected(downPortrait.character);
				downPortrait = null;
			}
			else
			{
				List<Character> list = _filteredCharacters.FindAll((Character character) => character.characterDefinition.uid == downPortrait.characterDefinition.uid);
				if (list.Count > 1)
				{
					SetList(list, ViewMode.Single, true);
					GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_opencharinfopage", GFAudioManager.SFX_GROUP, string.Empty);
					OverrideExitButtonFunction();
					OnCharacterFolderSelected(list[0].characterDefinition.uid);
				}
				else
				{
					ReturnExitButtonControl();
					OnCharacterSelected(list[0]);
					downPortrait = null;
				}
			}
		}
		points.Clear();
		timestamps.Clear();
	}

	public void OverrideExitButtonFunction()
	{
		TitanTowerController.instance.exitButton.OnClick += HandleReturnToFolderView;
		TitanTowerController.instance.ignoreExitButtonFunction = true;
	}

	private void HandleReturnToFolderView()
	{
		ReturnExitButtonControl();
		if (TitanTowerController.instance.characterSelect.gameObject.activeInHierarchy)
		{
			TitanTowerController.instance.characterSelect.ForceUpdatePageControllerList();
		}
		else if (TitanTowerController.instance.combineScreen.gameObject.activeInHierarchy)
		{
			TitanTowerController.instance.combineScreen.ForceUpdatePageControllerList();
		}
	}

	private void ReturnExitButtonControl()
	{
		TitanTowerController.instance.exitButton.OnClick -= HandleReturnToFolderView;
		TitanTowerController.instance.ignoreExitButtonFunction = false;
	}

	public void ForceReturnExitButtonControl()
	{
		ReturnExitButtonControl();
	}

	private void HandleOnPageDotChange(tk2dUIToggleButtonGroup obj)
	{
		if (!dotNavigationEnabled)
		{
			obj.SelectedIndex = pageIndex;
		}
		else if (!isDown)
		{
			pageIndex = obj.SelectedIndex;
		}
	}

	private void OnMoveComplete()
	{
		moveTween = null;
	}

	protected void OnCharacterPortaitCreated(CharacterSelectPortrait portrait)
	{
		if (CharacterPortraitCreated__BackingField != null)
		{
			CharacterPortraitCreated__BackingField(portrait);
		}
	}

	protected void OnCharacterSelected(Character character)
	{
		if (CharacterSelected__BackingField != null)
		{
			CharacterSelected__BackingField(character);
		}
	}

	private void OnCharacterFolderSelected(int characterUID)
	{
		if (CharacterFolderSelected__BackingField != null)
		{
			CharacterFolderSelected__BackingField(characterUID);
		}
	}

	protected void OnDragFinished(CharacterSelectPortrait portrait)
	{
		if (DragFinished__BackingField != null)
		{
			DragFinished__BackingField(portrait);
		}
	}

	protected Page GetPage()
	{
		Page page = null;
		if (pagePool.Count == 0)
		{
			page = UnityEngine.Object.Instantiate(pagePrefab).GetComponent<Page>();
			page.gameObject.SetLayerRecursively(base.gameObject.layer);
			page.transform.parent = pageContainer;
			page.items = new List<CharacterSelectPortrait>();
			CharacterSelectPortrait characterSelectPortrait = null;
			for (int i = 0; i < numItemsPerPage; i++)
			{
				characterSelectPortrait = UnityEngine.Object.Instantiate(portraitPrefab).GetComponent<CharacterSelectPortrait>();
				characterSelectPortrait.gameObject.SetLayerRecursively(base.gameObject.layer);
				characterSelectPortrait.transform.parent = page.transform;
				characterSelectPortrait.uiItem.UpdateParent();
				characterSelectPortrait.uiItem.OnDownUIItem += HandleOnDownPortrait;
				characterSelectPortrait.gameObject.SetActive(false);
				page.items.Add(characterSelectPortrait);
			}
		}
		else
		{
			page = pagePool.Pop();
		}
		page.gameObject.SetActive(true);
		return page;
	}

	protected void ReturnPage(Page page)
	{
		page.gameObject.SetActive(false);
		pagePool.Add(page);
	}
}
