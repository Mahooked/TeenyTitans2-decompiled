using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AccessoryPageController : MonoBehaviour
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

	public int pageIndex
	{
		get
		{
			return _pageIndex;
		}
		set
		{
			// ILSpy could not decompile this member: Error decompiling @06001CE8 AccessoryPageController.set_pageIndex: Value cannot be null. (Parameter 'source')
			// Please report the exception below,
			// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
			// Error decompiling @06001CE8 AccessoryPageController.set_pageIndex
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

	public List<AccessoryPage> pages => _pages;

	public bool IsDraggingPortrait => isDraggingPortrait;

	public List<AccessoryItemDefinition> filteredAccessories => _filteredAccessories;

	public event Action<AccessoryItemDefinition> AccessorySelected;

	public event Action<AccessorySelectPortrait> DragFinished;

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
			component.gameObject.SetLayerRecursively(gameObject.layer);
			component.items = new List<AccessorySelectPortrait>();
			component.transform.parent = pageContainer;
			component.transform.localPosition = Vector3.zero;
			for (int j = 0; j < numItemsPerPage; j++)
			{
				AccessorySelectPortrait component2 = UnityEngine.Object.Instantiate(portraitPrefab).GetComponent<AccessorySelectPortrait>();
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

	private void HandleOnDownPortrait(tk2dUIItem obj)
	{
		downPortrait = obj.GetComponent<AccessorySelectPortrait>();
		if (downPortrait.mode == CharacterSelectPortraitMode.Normal)
		{
			portraitDragCounter = 0f;
		}
		else
		{
			downPortrait.uiItem.Release(ignoreParent: true);
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
						downPortrait.uiItem.Release(ignoreParent: true);
					}
					downPortrait = null;
				}
				else if (downPortrait != null && downPortrait.mode == CharacterSelectPortraitMode.Normal)
				{
					portraitDragCounter += Time.unscaledDeltaTime;
					if (dragEnabled && (portraitDragCounter >= portraitDragDelay || Mathf.Abs(vector2.y) / Screen.dpi >= startDragDistance))
					{
						downPortrait.uiItem.Release(ignoreParent: true);
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
			pageNumberText.text = $"{_pageIndex + 1}/{maxPageIndex + 1}";
		}
		UpdatePosition();
	}

	public void GoToPageWithCharacter(AccessoryItemDefinition accessoryItemDefinition)
	{
		int num = _filteredAccessories.IndexOf(accessoryItemDefinition);
		if (num > -1)
		{
			pageIndex = num / numItemsPerPage;
			Debug.Log($"Page index is {pageIndex}");
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
		else if (isDraggingPortrait)
		{
			Debug.Log("Stop Dragging Portrait");
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
					accessorySelectPortrait.gameObject.SetActive(value: true);
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
				accessorySelectPortrait.gameObject.SetActive(value: false);
			}
		}
	}

	private void OnCharacterPortaitCreated(AccessorySelectPortrait portrait)
	{
	}

	private void OnAccessorySelected(AccessoryItemDefinition accessoryItemDefinition)
	{
		if (AccessorySelected != null)
		{
			AccessorySelected(accessoryItemDefinition);
		}
	}

	private void OnDragFinished(AccessorySelectPortrait portrait)
	{
		if (DragFinished != null)
		{
			DragFinished(portrait);
		}
	}

	private AccessoryPage GetPage()
	{
		AccessoryPage accessoryPage;
		if (pagePool.Count == 0)
		{
			accessoryPage = UnityEngine.Object.Instantiate(pagePrefab).GetComponent<AccessoryPage>();
			accessoryPage.gameObject.SetLayerRecursively(gameObject.layer);
			accessoryPage.transform.parent = pageContainer;
			accessoryPage.items = new List<AccessorySelectPortrait>();
			for (int i = 0; i < numItemsPerPage; i++)
			{
				AccessorySelectPortrait component = UnityEngine.Object.Instantiate(portraitPrefab).GetComponent<AccessorySelectPortrait>();
				component.gameObject.SetLayerRecursively(gameObject.layer);
				component.transform.parent = accessoryPage.transform;
				component.uiItem.UpdateParent();
				component.uiItem.OnDownUIItem += HandleOnDownPortrait;
				component.gameObject.SetActive(value: false);
				accessoryPage.items.Add(component);
			}
		}
		else
		{
			accessoryPage = pagePool.Pop();
		}
		accessoryPage.gameObject.SetActive(value: true);
		return accessoryPage;
	}

	private void ReturnPage(AccessoryPage page)
	{
		page.gameObject.SetActive(value: false);
		pagePool.Add(page);
	}
}
