using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class PortraitPageController : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public PageDots pageDots;

	public tk2dUIToggleButtonGroup pageDotGroup;

	public Transform pageContainer;

	public int rowsPerPage = 2;

	public int columnsPerPage = 3;

	public Vector3 topLeftPosition;

	public Vector2 positionStep;

	public float pageWidth = 100f;

	public float startDragDistance = 0.5f;

	public float swipeDistance = 0.5f;

	public GameObject portraitPrefab;

	public GameObject pagePrefab;

	public bool dragEnabled = true;

	public bool dragNavigationEnabled = true;

	public bool dotNavigationEnabled = true;

	public bool portraitSelectionEnabled = true;

	private InitLODPortaitDelegate initPortraitCallback;

	private CleanUpLODPortraitDelegate cleanUpPortraitCallback;

	private bool isInitialized;

	private List<LegionOfDoomPage> pagePool = new List<LegionOfDoomPage>();

	private bool isDown;

	private Vector3 downPosition;

	private Vector3 curPosition;

	private Vector3 prevPosition;

	private List<Vector3> points = new List<Vector3>();

	private List<float> timestamps = new List<float>();

	private LegionOfDoomPortrait downPortrait;

	private bool isDraggingPortrait;

	private Vector3 portraitDragOffset;

	private int numItemsPerPage;

	private float maxScrollPos;

	private int maxPageIndex;

	private int firstVisible = -1;

	private int lastVisible = -1;

	private int prevFirstVisible = -1;

	private int prevLastVisible = -1;

	private List<LegionOfDoomPage> _pages = new List<LegionOfDoomPage>();

	private List<LegionOfDoomPortraitUnlock> _allPortraitUnlocks = new List<LegionOfDoomPortraitUnlock>();

	private bool forceRepopulate;

	private Tweener moveTween;

	private float _scrollPos;

	private int _pageIndex;

	private bool _isDragging;

	private float scrollPos
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

	private int pageIndex
	{
		get
		{
			return _pageIndex;
		}
		set
		{
			// ILSpy could not decompile this member: Error decompiling @060024E4 PortraitPageController.set_pageIndex: Value cannot be null. (Parameter 'source')
			// Please report the exception below,
			// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
			// Error decompiling @060024E4 PortraitPageController.set_pageIndex
			// in assembly "C:\AIC\TeenyTitans2-decompiled\assets\bin\Data\Managed\Assembly-CSharp.dll"
			//  ---> System.ArgumentNullException: Value cannot be null. (Parameter 'source')
			//    at System.Linq.ThrowHelper.ThrowArgumentNullException(ExceptionArgument) + 0x31
			//    at System.Linq.Enumerable.Take[TSource](IEnumerable`1, Int32) + 0x48
			//    at ICSharpCode.Decompiler.CSharp.ArgumentList.GetArgumentResolveResultsDirect(Int32) + 0x24
			//    at ICSharpCode.Decompiler.CSharp.Disambiguator.Probe() + 0xc8
			//    at ICSharpCode.Decompiler.CSharp.Disambiguator.Run() + 0x10
			//    at ICSharpCode.Decompiler.CSharp.Disambiguator.ForMethodReference(ExpressionBuilder, IMethod, IType, TranslatedExpression, Boolean, ExpectedTargetDetails, Boolean, Boolean) + 0x103
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.DisambiguateDelegateReference(IMethod, IMethod, ExpectedTargetDetails, ILInstruction) + 0x25f
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildDelegateReference(IMethod, IMethod, ExpectedTargetDetails, ILInstruction) + 0x53
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitLdFtn(LdFtn, TranslationContext) + 0x98
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildArgumentList(ExpectedTargetDetails, ResolveResult, IMethod, Int32, IReadOnlyList`1, IReadOnlyList`1, Boolean) + 0x38f
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.HandleDelegateConstruction(CallInstruction) + 0x139
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(CallInstruction, IType) + 0x9f
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitNewObj(NewObj, TranslationContext) + 0x150
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildArgumentList(ExpectedTargetDetails, ResolveResult, IMethod, Int32, IReadOnlyList`1, IReadOnlyList`1, Boolean) + 0x38f
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(OpCode, IMethod, IReadOnlyList`1, IReadOnlyList`1, IType) + 0x4e7
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(CallInstruction, IType) + 0x12f
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitCall(Call, TranslationContext) + 0x57
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildArgumentList(ExpectedTargetDetails, ResolveResult, IMethod, Int32, IReadOnlyList`1, IReadOnlyList`1, Boolean) + 0x38f
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(OpCode, IMethod, IReadOnlyList`1, IReadOnlyList`1, IType) + 0x4e7
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(CallInstruction, IType) + 0x12f
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitCall(Call, TranslationContext) + 0x57
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.BuildArgumentList(ExpectedTargetDetails, ResolveResult, IMethod, Int32, IReadOnlyList`1, IReadOnlyList`1, Boolean) + 0x38f
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(OpCode, IMethod, IReadOnlyList`1, IReadOnlyList`1, IType) + 0x4e7
			//    at ICSharpCode.Decompiler.CSharp.CallBuilder.Build(CallInstruction, IType) + 0x12f
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitCall(Call, TranslationContext) + 0x57
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.Translate(ILInstruction, IType) + 0xa7
			//    at ICSharpCode.Decompiler.CSharp.ExpressionBuilder.VisitStObj(StObj, TranslationContext) + 0x311
			//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitStObj(StObj) + 0xa9
			//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockStatement, BlockContainer, IEnumerable`1, Boolean) + 0x26b
			//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockContainer, Boolean) + 0x47
			//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlockContainer(BlockContainer) + 0x94
			//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
			//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertAsBlock(ILInstruction) + 0x11
			//    at ICSharpCode.Decompiler.CSharp.CSharpDecompiler.DecompileBody(IMethod, EntityDeclaration, DecompileRun, ITypeResolveContext, ExtensionInfo) + 0x426
			// -- continuing with outer exception (ICSharpCode.Decompiler.DecompilerException) --
			// 
		}
	}

	public bool isDragging => _isDragging;

	public LegionOfDoomPage currentPage => _pages[pageIndex];

	private void Awake()
	{
		if (uiItem == null)
		{
			uiItem = GetComponent<tk2dUIItem>();
		}
		pageDotGroup.OnChange += HandleOnPageDotChange;
		numItemsPerPage = rowsPerPage * columnsPerPage;
		maxScrollPos = pageWidth * (float)Mathf.Max(0, maxPageIndex);
		pagePool = new List<LegionOfDoomPage>();
		for (int i = 0; i < 2; i++)
		{
			LegionOfDoomPage component = Object.Instantiate(pagePrefab).GetComponent<LegionOfDoomPage>();
			component.gameObject.SetLayerRecursively(gameObject.layer);
			component.items = new List<LegionOfDoomPortrait>();
			component.transform.parent = pageContainer;
			component.transform.localPosition = Vector3.zero;
			for (int j = 0; j < numItemsPerPage; j++)
			{
				LegionOfDoomPortrait component2 = Object.Instantiate(portraitPrefab).GetComponent<LegionOfDoomPortrait>();
				component2.gameObject.SetLayerRecursively(gameObject.layer);
				component2.transform.parent = component.transform;
				component2.uiItem.UpdateParent();
				component2.uiItem.OnDownUIItem += HandleOnDownPortrait;
				component2.gameObject.SetActive(value: false);
				component.items.Add(component2);
			}
			component.gameObject.SetActive(value: false);
			pagePool.Add(component);
		}
		isInitialized = true;
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
		SetVisiblePortraitsViewed(forceRefresh: false);
		int count = _pages.Count;
		for (int i = 0; i < count; i++)
		{
			LegionOfDoomPage legionOfDoomPage = _pages[i];
			int count2 = legionOfDoomPage.items.Count;
			for (int j = 0; j < count2; j++)
			{
				LegionOfDoomPortrait portrait = legionOfDoomPage.items[j];
				cleanUpPortraitCallback(portrait);
			}
		}
		count = pagePool.Count;
		for (int k = 0; k < count; k++)
		{
			LegionOfDoomPage legionOfDoomPage2 = pagePool[k];
			int count3 = legionOfDoomPage2.items.Count;
			for (int l = 0; l < count3; l++)
			{
				LegionOfDoomPortrait portrait2 = legionOfDoomPage2.items[l];
				cleanUpPortraitCallback(portrait2);
			}
		}
	}

	public void Init(InitLODPortaitDelegate callback, CleanUpLODPortraitDelegate cleanUpCallback, List<LegionOfDoomPortraitUnlock> portraitUnlocks)
	{
		if (!isInitialized)
		{
			return;
		}
		initPortraitCallback = callback;
		cleanUpPortraitCallback = cleanUpCallback;
		if (portraitUnlocks.SequenceEqual(_allPortraitUnlocks))
		{
			UpdatePages();
		}
		else
		{
			_allPortraitUnlocks.Clear();
			_allPortraitUnlocks.AddRange(portraitUnlocks);
			maxPageIndex = Mathf.Max(0, Mathf.CeilToInt((float)_allPortraitUnlocks.Count / (float)numItemsPerPage) - 1);
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
			UpdatePosition();
		}
		SetVisiblePortraitsViewed(forceRefresh: false);
	}

	private void Update()
	{
		if (!isDown)
		{
			return;
		}
		curPosition = Input.mousePosition;
		if (_isDragging)
		{
			points.Add(curPosition - prevPosition);
			timestamps.Add(Time.time);
			if (dragNavigationEnabled)
			{
				Vector3 vector = GUICamera.instance.ScreenToWorldPoint(prevPosition) - GUICamera.instance.ScreenToWorldPoint(curPosition);
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
				_isDragging = true;
				if (downPortrait != null)
				{
					downPortrait.uiItem.Release(ignoreParent: true);
				}
				downPortrait = null;
			}
		}
		prevPosition = curPosition;
	}

	private void HandleOnDownPortrait(tk2dUIItem obj)
	{
		downPortrait = obj.GetComponent<LegionOfDoomPortrait>();
	}

	private void ForceUpdate()
	{
		forceRepopulate = true;
		UpdatePosition();
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
			LegionOfDoomPage legionOfDoomPage = _pages[i];
			if (flag)
			{
				PopulatePage(legionOfDoomPage, numItemsPerPage * num3);
			}
			Vector3 localPosition = legionOfDoomPage.transform.localPosition;
			localPosition.x = num2;
			legionOfDoomPage.transform.localPosition = localPosition;
			num2 += pageWidth;
			num3++;
		}
		prevFirstVisible = firstVisible;
		prevLastVisible = lastVisible;
		forceRepopulate = false;
	}

	private void UpdatePages()
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

	protected virtual void PopulatePage(LegionOfDoomPage page, int startIndex)
	{
		Vector3 localPosition = topLeftPosition;
		int num = startIndex + numItemsPerPage;
		for (int i = startIndex; i < num; i++)
		{
			int num2 = i % numItemsPerPage;
			LegionOfDoomPortrait legionOfDoomPortrait = page.items[num2];
			if (i < _allPortraitUnlocks.Count)
			{
				if (!legionOfDoomPortrait.gameObject.activeSelf)
				{
					legionOfDoomPortrait.gameObject.SetActive(value: true);
				}
				initPortraitCallback(legionOfDoomPortrait, _allPortraitUnlocks[i]);
				localPosition.x = topLeftPosition.x + positionStep.x * (float)(i % columnsPerPage);
				localPosition.y = topLeftPosition.y + positionStep.y * (float)Mathf.FloorToInt((float)num2 / (float)columnsPerPage);
				legionOfDoomPortrait.transform.localPosition = localPosition;
			}
			else
			{
				legionOfDoomPortrait.gameObject.SetActive(value: false);
			}
		}
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
		if (_isDragging)
		{
			_isDragging = false;
			points.Add(curPosition - prevPosition);
			timestamps.Add(Time.time);
			float num = Time.time - 0.2f;
			Vector3 vector = default;
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
		else if (portraitSelectionEnabled && downPortrait != null && Vector3.Distance(downPosition, Input.mousePosition) / Screen.dpi < 0.2f)
		{
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

	private LegionOfDoomPage GetPage()
	{
		LegionOfDoomPage legionOfDoomPage;
		if (pagePool.Count == 0)
		{
			legionOfDoomPage = Object.Instantiate(pagePrefab).GetComponent<LegionOfDoomPage>();
			legionOfDoomPage.gameObject.SetLayerRecursively(gameObject.layer);
			legionOfDoomPage.transform.parent = pageContainer;
			legionOfDoomPage.items = new List<LegionOfDoomPortrait>();
			for (int i = 0; i < numItemsPerPage; i++)
			{
				LegionOfDoomPortrait component = Object.Instantiate(portraitPrefab).GetComponent<LegionOfDoomPortrait>();
				component.gameObject.SetLayerRecursively(gameObject.layer);
				component.transform.parent = legionOfDoomPage.transform;
				component.uiItem.UpdateParent();
				component.uiItem.OnDownUIItem += HandleOnDownPortrait;
				component.gameObject.SetActive(value: false);
				legionOfDoomPage.items.Add(component);
			}
		}
		else
		{
			legionOfDoomPage = pagePool.Pop();
		}
		legionOfDoomPage.gameObject.SetActive(value: true);
		return legionOfDoomPage;
	}

	private void ReturnPage(LegionOfDoomPage page)
	{
		page.gameObject.SetActive(value: false);
		pagePool.Add(page);
	}

	private void SetVisiblePortraitsViewed(bool forceRefresh, bool updatePageIndex1 = false)
	{
		if (!isInitialized)
		{
			return;
		}
		int num = (updatePageIndex1 ? 1 : 0);
		if (_pages.Count <= num)
		{
			return;
		}
		LegionOfDoomPage legionOfDoomPage = _pages[num];
		int count = legionOfDoomPage.items.Count;
		for (int i = 0; i < count; i++)
		{
			LegionOfDoomPortrait legionOfDoomPortrait = legionOfDoomPage.items[i];
			Player.instance.pvpData.SetPortraitViewed(legionOfDoomPortrait.portraitSprite.CurrentSprite.name, isViewed: true);
			if (forceRefresh)
			{
				legionOfDoomPortrait.Refresh();
			}
		}
	}
}
