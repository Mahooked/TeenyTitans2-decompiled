using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AwardsView : MonoBehaviour
{
	public GameObject bannerTemplate;

	public GameObject trophySpriteTemplate;

	public Transform trophySpriteHolder;

	public tk2dUIItem[] dragUIItems;

	public Camera dragCamera;

	public PageDots pageDots;

	public tk2dUIToggleButtonGroup pageDotGroup;

	public float startDragDistance = 0.07f;

	public float swipeDistance = 0.16f;

	private List<TournamentDefinition> tournaments;

	private float pageWidth;

	private Vector3 trophyHolderOrigin;

	private List<TrophyGroup> trophyGroups = new List<TrophyGroup>();

	private float maxScrollPos;

	private int maxPageIndex = 6;

	private bool isDown;

	private bool isDragging;

	private Vector3 downPosition;

	private Vector3 curPosition;

	private Vector3 prevPosition;

	private List<Vector3> points = new List<Vector3>();

	private List<float> timestamps = new List<float>();

	private Tweener moveTween;

	private bool screenIsSetUp;

	private Vector3 defaultModelViewerPos;

	private float bannerLocalY = 92f;

	private float _scrollPos;

	private int prevPageIndex;

	private int _pageIndex;

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
			_pageIndex = Mathf.Max(0, value);
			if (Application.isPlaying)
			{
				GFAudioManager.instance.PlayClipFromResources("Audio/SFX/sfx_menus_tower_scrollcharpages", GFAudioManager.SFX_GROUP, string.Empty);
			}
		}
	}

	private void OnEnable()
	{
		pageDotGroup.OnChange += HandleOnPageDotChange;
		tk2dUIItem[] array = dragUIItems;
		foreach (tk2dUIItem tk2dUIItem2 in array)
		{
			tk2dUIItem2.OnDown += HandleOnDown;
			tk2dUIItem2.OnRelease += HandleOnRelease;
		}
	}

	private void OnDisable()
	{
		pageDotGroup.OnChange -= HandleOnPageDotChange;
		tk2dUIItem[] array = dragUIItems;
		foreach (tk2dUIItem tk2dUIItem2 in array)
		{
			tk2dUIItem2.OnDown -= HandleOnDown;
			tk2dUIItem2.OnRelease -= HandleOnRelease;
		}
		screenIsSetUp = false;
	}

	private void HandleOnPageDotChange(tk2dUIToggleButtonGroup obj)
	{
		if (!isDown && screenIsSetUp)
		{
			SetPageIndex(obj.SelectedIndex);
		}
	}

	public void Init(int index = -1)
	{
		if (tournaments == null)
		{
			tournaments = new List<TournamentDefinition>(Resources.LoadAll<TournamentDefinition>("Data/Tournaments"));
			tournaments.Sort((TournamentDefinition x, TournamentDefinition y) => x.tournamentOrder.CompareTo(y.tournamentOrder));
			trophyHolderOrigin = trophySpriteHolder.localPosition;
			trophyGroups.Clear();
			pageWidth = GUICamera.instance.GetComponent<tk2dCamera>().nativeResolutionWidth;
			for (int num = 0; num < tournaments.Count; num++)
			{
				TournamentDefinition tournamentDefinition = tournaments[num];
				TrophyBanner component;
				tk2dSprite component2;
				if (num > 0)
				{
					GameObject gameObject = Object.Instantiate(bannerTemplate, bannerTemplate.transform.position, Quaternion.identity);
					gameObject.transform.parent = bannerTemplate.transform.parent;
					gameObject.transform.localPosition = new Vector3(pageWidth * (float)num, bannerLocalY, 0f);
					component = gameObject.GetComponent<TrophyBanner>();
					component2 = Object.Instantiate(trophySpriteTemplate).GetComponent<tk2dSprite>();
					component2.transform.parent = trophySpriteTemplate.transform.parent;
					component2.transform.localPosition = new Vector3(pageWidth * (float)num, trophySpriteTemplate.transform.localPosition.y);
				}
				else
				{
					component = bannerTemplate.GetComponent<TrophyBanner>();
					component2 = trophySpriteTemplate.GetComponent<tk2dSprite>();
				}
				component.SetBanner(tournamentDefinition);
				component2.SetSprite(tournamentDefinition.trophySprite);
				component.gameObject.name = tournamentDefinition.tournamentName + "_Banner";
				component2.gameObject.name = tournamentDefinition.tournamentName + "_Trophy";
				trophyGroups.Add(new TrophyGroup(tournamentDefinition, component2, component));
			}
			maxPageIndex = tournaments.Count - 1;
			maxScrollPos = pageWidth * (float)maxPageIndex;
			_pageIndex = 0;
			_scrollPos = 0f;
			pageDots.numDots = maxPageIndex + 1;
			pageDotGroup.SelectedIndex = 0;
		}
		foreach (TrophyGroup trophyGroup in trophyGroups)
		{
			bool flag = Player.instance.GetTournamentCompletedData(trophyGroup.definition) != null;
			trophyGroup.trophySprite.color = ((!flag) ? Color.black : Color.white);
			trophyGroup.banner.SetUnlocked(flag);
		}
		pageIndex = index;
		scrollPos = pageWidth;
		trophySpriteHolder.localPosition = new Vector3(trophyHolderOrigin.x + scrollPos, trophyHolderOrigin.y, trophyHolderOrigin.z);
		if (pageIndex == -1)
		{
			List<TournamentCompletedData> completedTournaments = Player.instance.completedTournaments;
			int num2 = -1;
			foreach (TournamentCompletedData completedData in completedTournaments)
			{
				if (completedData.dayCompleted > num2)
				{
					num2 = completedData.dayCompleted;
					pageIndex = tournaments.FindIndex((TournamentDefinition x) => x.tournamentName == completedData.tournamentName);
				}
			}
		}
		pageIndex = Mathf.Max(0, pageIndex);
		SetPageIndex(pageIndex);
		screenIsSetUp = true;
	}

	private void SetPageIndex(int index)
	{
		// ILSpy could not decompile this member: Error decompiling @06001D89 AwardsView.SetPageIndex: Value cannot be null. (Parameter 'source')
		// Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// Error decompiling @06001D89 AwardsView.SetPageIndex
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

	private void OnMoveComplete()
	{
		moveTween = null;
		trophySpriteHolder.localPosition = new Vector3(trophyHolderOrigin.x + scrollPos, trophyHolderOrigin.y, trophyHolderOrigin.z);
		UpdateBannerPositions();
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
				Vector3 vector = dragCamera.ScreenToWorldPoint(curPosition) - dragCamera.ScreenToWorldPoint(prevPosition);
				if (scrollPos + vector.x > 0f || scrollPos + vector.x < 0f - maxScrollPos)
				{
					vector.x *= 0.5f;
				}
				scrollPos += vector.x;
			}
			else
			{
				Vector2 vector2 = new Vector2(curPosition.x - downPosition.x, curPosition.y - downPosition.y);
				if (Mathf.Abs(vector2.x) / Screen.dpi >= startDragDistance && Mathf.Abs(vector2.x / vector2.y) > 1.6f)
				{
					isDragging = true;
				}
			}
			prevPosition = curPosition;
		}
		trophySpriteHolder.localPosition = new Vector3(trophyHolderOrigin.x + scrollPos, trophyHolderOrigin.y, trophyHolderOrigin.z);
	}

	private void LateUpdate()
	{
		if (isDragging || moveTween != null)
		{
			UpdateBannerPositions();
		}
	}

	private void UpdateBannerPositions()
	{
		for (int i = 0; i < trophyGroups.Count; i++)
		{
			TrophyGroup trophyGroup = trophyGroups[i];
			trophyGroup.banner.transform.position = trophyGroup.trophySprite.transform.position;
			trophyGroup.banner.transform.localPosition = new Vector3(trophyGroup.banner.transform.localPosition.x, bannerLocalY, 0f);
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
				if (pageIndex > 0 && vector.x > 0f)
				{
					pageIndex--;
				}
				else if (pageIndex < maxPageIndex && vector.x < 0f)
				{
					pageIndex++;
				}
				else
				{
					float num4 = ((!(scrollPos < 0f)) ? 0f : Mathf.Abs(scrollPos));
					pageIndex = Mathf.Clamp(Mathf.RoundToInt(num4 / pageWidth), 0, maxPageIndex);
				}
			}
			else
			{
				float num5 = ((!(scrollPos < 0f)) ? 0f : Mathf.Abs(scrollPos));
				pageIndex = Mathf.Clamp(Mathf.RoundToInt(num5 / pageWidth), 0, maxPageIndex);
			}
			SetPageIndex(pageIndex);
		}
		points.Clear();
		timestamps.Clear();
	}
}
