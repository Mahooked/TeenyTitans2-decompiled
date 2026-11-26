using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PageControllerModChip : MonoBehaviour
{
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

	public bool hideWhileDragging;

	public Camera lowerCamera;

	public Camera upperCamera;

	public GameObject pagePrefab;

	private InitModChipPortaitDelegate initModChipPortraitCallback;

	private bool isInitialized;

	private List<PageModChips> pagePool = new List<PageModChips>();

	protected bool isDown;

	protected bool isDragging;

	protected Vector3 downPosition;

	protected Vector3 curPosition;

	protected Vector3 prevPosition;

	protected List<Vector3> points = new List<Vector3>();

	protected List<float> timestamps = new List<float>();

	protected ModChipSelectPortrait downPortrait;

	protected float portraitDragCounter;

	protected bool isDraggingPortrait;

	protected Vector3 portraitDragOffset;

	protected ModChipSelectPortrait dragPortrait;

	protected int numItemsPerPage;

	protected float maxScrollPos;

	protected int maxPageIndex;

	protected int firstVisible = -1;

	protected int lastVisible = -1;

	protected int prevFirstVisible = -1;

	protected int prevLastVisible = -1;

	protected List<PageModChips> _pages = new List<PageModChips>();

	private bool forceRepopulate;

	private Tweener moveTween;

	protected float _scrollPos;

	protected int _pageIndex;

	private List<ModChipDefinition> _filteredModChips = new List<ModChipDefinition>();

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

	public int pageIndex
	{
		get
		{
			return _pageIndex;
		}
		set
		{
			// ILSpy could not decompile this member: Error decompiling @060020E7 PageControllerModChip.set_pageIndex: Value cannot be null. (Parameter 'source')
			// Please report the exception below,
			// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
			// Error decompiling @060020E7 PageControllerModChip.set_pageIndex
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

	public List<PageModChips> pages => _pages;

	public List<ModChipDefinition> filteredModChips => _filteredModChips;

	public bool ignorePageDragging { get; set; }

	public bool ignorePortraitDragging { get; set; }

	public event Action<ModChipDefinition> ModChipSelected;

	public event Action<ModChipSelectPortrait> PortraitSelected;

	public event Action PageDragStarted;

	public event Action PageDragFinished;

	public event Action<ModChipSelectPortrait> DragStarted;

	public event Action<ModChipSelectPortrait> DragFinished;

	public event Action<ModChipSelectPortrait> ModChipPortraitCreated;

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
		PageModChips pageModChips = null;
		ModChipSelectPortrait modChipSelectPortrait = null;
		pagePool = new List<PageModChips>();
		for (int i = 0; i < 2; i++)
		{
			pageModChips = UnityEngine.Object.Instantiate(pagePrefab).GetComponent<PageModChips>();
			pageModChips.items = new List<ModChipSelectPortrait>();
			pageModChips.transform.parent = pageContainer;
			pageModChips.transform.localPosition = Vector3.zero;
			for (int j = 0; j < numItemsPerPage; j++)
			{
				modChipSelectPortrait = ObjectPool.GetObjectFromResource<ModChipSelectPortrait>("Prefabs/GUI/ModChipGUI");
				modChipSelectPortrait.transform.parent = pageModChips.transform;
				modChipSelectPortrait.uiItem.UpdateParent();
				modChipSelectPortrait.uiItem.OnDownUIItem += HandleOnDownPortrait;
				modChipSelectPortrait.gameObject.SetActive(value: false);
				pageModChips.items.Add(modChipSelectPortrait);
			}
			pageModChips.gameObject.SetActive(value: false);
			pagePool.Add(pageModChips);
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
	}

	private void HandleOnDownPortrait(tk2dUIItem obj)
	{
		downPortrait = obj.GetComponent<ModChipSelectPortrait>();
		portraitDragCounter = 0f;
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
				Vector3 vector = lowerCamera.ScreenToWorldPoint(prevPosition) - lowerCamera.ScreenToWorldPoint(curPosition);
				if (scrollPos + vector.x < 0f || scrollPos + vector.x > maxScrollPos)
				{
					vector.x *= 0.5f;
				}
				scrollPos += vector.x;
			}
			else
			{
				Vector2 vector2 = new Vector2(curPosition.x - downPosition.x, curPosition.y - downPosition.y);
				if (Mathf.Abs(vector2.x) / Screen.dpi >= startDragDistance && Mathf.Abs(vector2.x / vector2.y) > 1.6f && !ignorePageDragging)
				{
					isDragging = true;
					if (downPortrait != null)
					{
						downPortrait.uiItem.Release(ignoreParent: true);
					}
					downPortrait = null;
					OnPageDragStarted();
				}
				else if (downPortrait != null && !ignorePortraitDragging)
				{
					portraitDragCounter += Time.unscaledDeltaTime;
					if (portraitDragCounter >= portraitDragDelay || Mathf.Abs(vector2.y) / Screen.dpi >= startDragDistance)
					{
						downPortrait.uiItem.Release(ignoreParent: true);
						isDraggingPortrait = true;
						isDown = false;
						dragPortrait = UnityEngine.Object.Instantiate(downPortrait);
						dragPortrait.modChip = downPortrait.modChip;
						dragPortrait.transform.position = downPortrait.transform.position;
						dragPortrait.transform.localScale = Vector3.one;
						dragPortrait.gameObject.SetLayerRecursively(LayerMask.NameToLayer("UI4"));
						Vector3 vector3 = lowerCamera.ScreenToWorldPoint(curPosition);
						portraitDragOffset = new Vector3(dragPortrait.transform.position.x - vector3.x, dragPortrait.transform.position.y - vector3.y);
						if (hideWhileDragging)
						{
							downPortrait.gameObject.SetActive(value: false);
						}
						OnModChipPortaitCreated(dragPortrait);
						OnDragStarted(dragPortrait);
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

	public void Init(InitModChipPortaitDelegate callback)
	{
		initModChipPortraitCallback = callback;
	}

	public void SetList(List<ModChipDefinition> modChips)
	{
		if (!isInitialized)
		{
			return;
		}
		if (modChips == _filteredModChips)
		{
			UpdatePages();
			return;
		}
		_filteredModChips.Clear();
		_filteredModChips.AddRange(modChips);
		maxPageIndex = Mathf.Max(0, Mathf.CeilToInt((float)_filteredModChips.Count / (float)numItemsPerPage) - 1);
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
			pageNumberText.text = $"{_pageIndex + 1}/{maxPageIndex + 1}";
		}
		UpdatePosition();
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
			Vector3 vector = default;
			int num2 = points.Count - 1;
			int num3 = num2;
			while (num3 >= 0 && !(timestamps[num3] < num))
			{
				vector += points[num3];
				num3--;
			}
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
			OnPageDragFinished();
		}
		else if (isDraggingPortrait)
		{
			Debug.Log("Stop Dragging Portrait");
			if (hideWhileDragging)
			{
				downPortrait.gameObject.SetActive(value: true);
			}
			OnDragFinished(dragPortrait);
			isDraggingPortrait = false;
			downPortrait = null;
			UnityEngine.Object.Destroy(dragPortrait.gameObject);
			dragPortrait = null;
		}
		else if (downPortrait != null && Vector3.Distance(downPosition, Input.mousePosition) / Screen.dpi < 0.2f)
		{
			OnModChipSelected(downPortrait.modChip);
			OnPortraitSelected(downPortrait);
			downPortrait = null;
		}
		points.Clear();
		timestamps.Clear();
	}

	private void HandleOnPageDotChange(tk2dUIToggleButtonGroup obj)
	{
		if (!isDown)
		{
			pageIndex = obj.SelectedIndex;
		}
	}

	private void OnMoveComplete()
	{
		moveTween = null;
	}

	protected void UpdatePosition()
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
		PageModChips pageModChips = null;
		int num3 = firstVisible;
		int num4 = lastVisible - firstVisible + 1;
		for (int i = 0; i < num4; i++)
		{
			if (i == _pages.Count)
			{
				_pages.Add(GetPage());
			}
			pageModChips = _pages[i];
			if (flag)
			{
				PopulatePage(pageModChips, numItemsPerPage * num3);
			}
			Vector3 localPosition = pageModChips.transform.localPosition;
			localPosition.x = num2;
			pageModChips.transform.localPosition = localPosition;
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

	protected virtual void PopulatePage(PageModChips page, int startIndex)
	{
		ModChipSelectPortrait modChipSelectPortrait = null;
		Vector3 localPosition = topLeftPosition;
		int num = startIndex + numItemsPerPage;
		for (int i = startIndex; i < num; i++)
		{
			int num2 = i % numItemsPerPage;
			modChipSelectPortrait = page.items[num2];
			if (i < _filteredModChips.Count)
			{
				if (!modChipSelectPortrait.gameObject.activeSelf)
				{
					modChipSelectPortrait.gameObject.SetActive(value: true);
				}
				if (initModChipPortraitCallback != null)
				{
					initModChipPortraitCallback(modChipSelectPortrait, _filteredModChips[i]);
				}
				localPosition.x = topLeftPosition.x + positionStep.x * (float)(i % columnsPerPage);
				localPosition.y = topLeftPosition.y + positionStep.y * (float)Mathf.FloorToInt(num2 / columnsPerPage);
				modChipSelectPortrait.transform.localPosition = localPosition;
			}
			else
			{
				modChipSelectPortrait.gameObject.SetActive(value: false);
			}
		}
	}

	protected void OnPageDragStarted()
	{
		if (PageDragStarted != null)
		{
			PageDragStarted();
		}
	}

	protected void OnPageDragFinished()
	{
		if (PageDragFinished != null)
		{
			PageDragFinished();
		}
	}

	protected void OnModChipPortaitCreated(ModChipSelectPortrait portrait)
	{
		if (ModChipPortraitCreated != null)
		{
			ModChipPortraitCreated(portrait);
		}
	}

	protected void OnModChipSelected(ModChipDefinition modChip)
	{
		if (ModChipSelected != null)
		{
			ModChipSelected(modChip);
		}
	}

	protected void OnPortraitSelected(ModChipSelectPortrait portrait)
	{
		if (PortraitSelected != null)
		{
			PortraitSelected(portrait);
		}
	}

	protected void OnDragStarted(ModChipSelectPortrait portrait)
	{
		if (DragStarted != null)
		{
			DragStarted(portrait);
		}
	}

	protected void OnDragFinished(ModChipSelectPortrait portrait)
	{
		if (DragFinished != null)
		{
			DragFinished(portrait);
		}
	}

	protected PageModChips GetPage()
	{
		PageModChips pageModChips = null;
		if (pagePool.Count == 0)
		{
			pageModChips = UnityEngine.Object.Instantiate(pagePrefab).GetComponent<PageModChips>();
			pageModChips.transform.parent = pageContainer;
			pageModChips.items = new List<ModChipSelectPortrait>();
			ModChipSelectPortrait modChipSelectPortrait = null;
			for (int i = 0; i < numItemsPerPage; i++)
			{
				modChipSelectPortrait = ObjectPool.GetObjectFromResource<ModChipSelectPortrait>("Prefabs/GUI/ModChipGUI");
				modChipSelectPortrait.transform.parent = pageModChips.transform;
				modChipSelectPortrait.uiItem.UpdateParent();
				modChipSelectPortrait.uiItem.OnDownUIItem += HandleOnDownPortrait;
				modChipSelectPortrait.gameObject.SetActive(value: false);
				pageModChips.items.Add(modChipSelectPortrait);
			}
		}
		else
		{
			pageModChips = pagePool.Pop();
		}
		pageModChips.gameObject.SetActive(value: true);
		return pageModChips;
	}

	protected void ReturnPage(PageModChips page)
	{
		page.gameObject.SetActive(value: false);
		pagePool.Add(page);
	}
}
