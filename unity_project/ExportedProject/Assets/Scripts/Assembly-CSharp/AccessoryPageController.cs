using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;

public class AccessoryPageController : MonoBehaviour
{
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AccessoryItemDefinition> AccessorySelected__BackingField;

	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private Action<AccessorySelectPortrait> DragFinished__BackingField;

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

	private InitAccessoryPortraitDelegate initPortraitCallback;

	private bool isInitialized;

	private List<AccessoryPage> pagePool = new List<AccessoryPage>();

	private bool isDown;

	private bool isDragging;

	private Vector3 downPosition;

	private Vector3 curPosition;

	private Vector3 prevPosition;

	private List<Vector3> points = new List<Vector3>();

	private List<float> timestamps = new List<float>();

	private AccessorySelectPortrait downPortrait;

	private float portraitDragCounter;

	private bool isDraggingPortrait;

	private Vector3 portraitDragOffset;

	private AccessorySelectPortrait dragPortrait;

	private int numItemsPerPage;

	private float maxScrollPos;

	private int maxPageIndex;

	private int firstVisible = -1;

	private int lastVisible = -1;

	private int prevFirstVisible = -1;

	private int prevLastVisible = -1;

	private List<AccessoryPage> _pages = new List<AccessoryPage>();

	private bool forceRepopulate;

	private Tweener moveTween;

	private float _scrollPos;

	private int _pageIndex;

	private List<AccessoryItemDefinition> _filteredAccessories = new List<AccessoryItemDefinition>();

	public float scrollPos
	{
		get
		{
			return _scrollPos;
		}
		set
		{
			if (!Mathf.Approximately(value, _scrollPos))
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

	public List<AccessoryPage> pages
	{
		get
		{
			return _pages;
		}
	}

	public bool IsDraggingPortrait
	{
		get
		{
			return isDraggingPortrait;
		}
	}

	public List<AccessoryItemDefinition> filteredAccessories
	{
		get
		{
			return _filteredAccessories;
		}
	}

	public event Action<AccessoryItemDefinition> AccessorySelected
	{
		add
		{
			Action<AccessoryItemDefinition> action = AccessorySelected__BackingField;
			Action<AccessoryItemDefinition> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AccessorySelected__BackingField, (Action<AccessoryItemDefinition>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AccessoryItemDefinition> action = AccessorySelected__BackingField;
			Action<AccessoryItemDefinition> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref AccessorySelected__BackingField, (Action<AccessoryItemDefinition>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public event Action<AccessorySelectPortrait> DragFinished
	{
		add
		{
			Action<AccessorySelectPortrait> action = DragFinished__BackingField;
			Action<AccessorySelectPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragFinished__BackingField, (Action<AccessorySelectPortrait>)Delegate.Combine(action2, value), action);
			}
			while ((object)action != action2);
		}
		remove
		{
			Action<AccessorySelectPortrait> action = DragFinished__BackingField;
			Action<AccessorySelectPortrait> action2;
			do
			{
				action2 = action;
				action = Interlocked.CompareExchange(ref DragFinished__BackingField, (Action<AccessorySelectPortrait>)Delegate.Remove(action2, value), action);
			}
			while ((object)action != action2);
		}
	}

	public void Init(InitAccessoryPortraitDelegate callback)
	{
		initPortraitCallback = callback;
		pageDotGroup.OnChange += HandleOnPageDotChange;
		if (pageNumberText != null)
		{
			pageNumberText.text = string.Format("{0}/{1}", "-", "-");
		}
		numItemsPerPage = rowsPerPage * columnsPerPage;
		maxScrollPos = pageWidth * (float)Mathf.Max(0, maxPageIndex);
		pagePool = new List<AccessoryPage>();
		for (int i = 0; i < 2; i++)
		{
			AccessoryPage component = UnityEngine.Object.Instantiate(pagePrefab).GetComponent<AccessoryPage>();
			component.gameObject.SetLayerRecursively(base.gameObject.layer);
			component.items = new List<AccessorySelectPortrait>();
			component.transform.parent = pageContainer;
			component.transform.localPosition = Vector3.zero;
			for (int j = 0; j < numItemsPerPage; j++)
			{
				AccessorySelectPortrait component2 = UnityEngine.Object.Instantiate(portraitPrefab).GetComponent<AccessorySelectPortrait>();
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
		downPortrait = obj.GetComponent<AccessorySelectPortrait>();
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
		ForceUpdate();
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
				else if (downPortrait != null && downPortrait.mode == CharacterSelectPortraitMode.Normal)
				{
					portraitDragCounter += Time.unscaledDeltaTime;
					if (dragEnabled && (portraitDragCounter >= portraitDragDelay || Mathf.Abs(vector2.y) / Screen.dpi >= startDragDistance))
					{
						downPortrait.uiItem.Release(true);
						isDraggingPortrait = true;
						isDown = false;
						dragPortrait = UnityEngine.Object.Instantiate(downPortrait);
						dragPortrait.accessoryItemDefinition = downPortrait.accessoryItemDefinition;
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

	public void SetList(List<AccessoryItemDefinition> accessoryItemDefinitions)
	{
		if (!isInitialized)
		{
			return;
		}
		if (accessoryItemDefinitions == _filteredAccessories)
		{
			UpdatePages();
			return;
		}
		_filteredAccessories.Clear();
		_filteredAccessories.AddRange(accessoryItemDefinitions);
		maxPageIndex = Mathf.Max(0, Mathf.CeilToInt((float)_filteredAccessories.Count / (float)numItemsPerPage) - 1);
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
		UpdatePosition();
	}

	public void GoToPageWithCharacter(AccessoryItemDefinition accessoryItemDefinition)
	{
		int num = _filteredAccessories.IndexOf(accessoryItemDefinition);
		if (num > -1)
		{
			pageIndex = num / numItemsPerPage;
			UnityEngine.Debug.Log(string.Format("Page index is {0}", pageIndex));
		}
	}

	public AccessoryPage GetPageWithCharacter(AccessoryItemDefinition accessoryItemDefinition)
	{
		return _pages.Find((AccessoryPage page) => page.items.Find((AccessorySelectPortrait portrait) => portrait.accessoryItemDefinition == accessoryItemDefinition));
	}

	public void ForceUpdate()
	{
		forceRepopulate = true;
		UpdatePosition();
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
			OnAccessorySelected(downPortrait.accessoryItemDefinition);
			downPortrait = null;
		}
		points.Clear();
		timestamps.Clear();
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

	private void UpdatePosition()
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
			AccessoryPage accessoryPage = _pages[i];
			if (flag)
			{
				PopulatePage(accessoryPage, numItemsPerPage * num3);
			}
			Vector3 localPosition = accessoryPage.transform.localPosition;
			localPosition.x = num2;
			accessoryPage.transform.localPosition = localPosition;
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

	protected virtual void PopulatePage(AccessoryPage page, int startIndex)
	{
		Vector3 localPosition = topLeftPosition;
		int num = startIndex + numItemsPerPage;
		for (int i = startIndex; i < num; i++)
		{
			int num2 = i % numItemsPerPage;
			AccessorySelectPortrait accessorySelectPortrait = page.items[num2];
			if (i < _filteredAccessories.Count)
			{
				if (!accessorySelectPortrait.gameObject.activeSelf)
				{
					accessorySelectPortrait.gameObject.SetActive(true);
				}
				if (initPortraitCallback != null)
				{
					initPortraitCallback(accessorySelectPortrait, _filteredAccessories[i]);
				}
				localPosition.x = topLeftPosition.x + positionStep.x * (float)(i % columnsPerPage);
				localPosition.y = topLeftPosition.y + positionStep.y * (float)Mathf.FloorToInt((float)num2 / (float)columnsPerPage);
				accessorySelectPortrait.transform.localPosition = localPosition;
			}
			else
			{
				accessorySelectPortrait.gameObject.SetActive(false);
			}
		}
	}

	private void OnCharacterPortaitCreated(AccessorySelectPortrait portrait)
	{
	}

	private void OnAccessorySelected(AccessoryItemDefinition accessoryItemDefinition)
	{
		if (AccessorySelected__BackingField != null)
		{
			AccessorySelected__BackingField(accessoryItemDefinition);
		}
	}

	private void OnDragFinished(AccessorySelectPortrait portrait)
	{
		if (DragFinished__BackingField != null)
		{
			DragFinished__BackingField(portrait);
		}
	}

	private AccessoryPage GetPage()
	{
		AccessoryPage accessoryPage;
		if (pagePool.Count == 0)
		{
			accessoryPage = UnityEngine.Object.Instantiate(pagePrefab).GetComponent<AccessoryPage>();
			accessoryPage.gameObject.SetLayerRecursively(base.gameObject.layer);
			accessoryPage.transform.parent = pageContainer;
			accessoryPage.items = new List<AccessorySelectPortrait>();
			for (int i = 0; i < numItemsPerPage; i++)
			{
				AccessorySelectPortrait component = UnityEngine.Object.Instantiate(portraitPrefab).GetComponent<AccessorySelectPortrait>();
				component.gameObject.SetLayerRecursively(base.gameObject.layer);
				component.transform.parent = accessoryPage.transform;
				component.uiItem.UpdateParent();
				component.uiItem.OnDownUIItem += HandleOnDownPortrait;
				component.gameObject.SetActive(false);
				accessoryPage.items.Add(component);
			}
		}
		else
		{
			accessoryPage = pagePool.Pop();
		}
		accessoryPage.gameObject.SetActive(true);
		return accessoryPage;
	}

	private void ReturnPage(AccessoryPage page)
	{
		page.gameObject.SetActive(false);
		pagePool.Add(page);
	}
}
