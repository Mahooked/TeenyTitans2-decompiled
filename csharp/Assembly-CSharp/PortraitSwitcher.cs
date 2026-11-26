using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DG.Tweening;
using UnityEngine;

public class PortraitSwitcher : MonoBehaviour
{
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

	public Character selectedCharacter => _selectedCharacter;

	public Portrait selectedPortrait => _selectedPortrait;

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

	public bool IsSelectMode => isSelectMode;

	public bool IsPlayerSwitcher => BattleController.instance != null && this == BattleController.instance.playerPortraits;

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

	public event Action<Character> CharacterChanged;

	public event Action SelectModeEntered;

	public event Action SelectModeExited;

	public event Action DragModeEntered;

	public event Action DragModeExited;

	public event Action SelectModeExitedLate;

	public event Action Enabled;

	public event Action Disabled;

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
		origin = transform.localPosition;
		portraitPool = new List<Portrait>();
		while (portraits.Count > 0)
		{
			Portrait portrait = portraits.Pop();
			portrait.gameObject.SetActive(value: false);
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
			Vector3 vector = new Vector3(transform.position.x + positionBounds.x, transform.position.y + positionBounds.y);
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
				portrait.gameObject.SetActive(value: true);
				portraits.Add(portrait);
			}
			modVector = positionOffset * portraits.Count;
			_selectedCharacter = portraits[0].character;
			_selectedPortrait = portraits[0];
			_offsetRatio = 0f;
			isDirty = true;
		}
	}

	public void SelectCharacter(Character character, bool forceEventCall = false)
	{
		// ILSpy could not decompile this member: Error decompiling @06002195 PortraitSwitcher.SelectCharacter: Value cannot be null. (Parameter 'source')
		// Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// Error decompiling @06002195 PortraitSwitcher.SelectCharacter
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
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlock(Block) + 0x51
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitIfInstruction(IfInstruction) + 0x3d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockStatement, BlockContainer, IEnumerable`1, Boolean) + 0x26b
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockContainer, Boolean) + 0x47
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlockContainer(BlockContainer) + 0x94
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertAsBlock(ILInstruction) + 0x11
		//    at ICSharpCode.Decompiler.CSharp.CSharpDecompiler.DecompileBody(IMethod, EntityDeclaration, DecompileRun, ITypeResolveContext, ExtensionInfo) + 0x426
		// -- continuing with outer exception (ICSharpCode.Decompiler.DecompilerException) --
		// 
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

	private void SelectIndex(int index)
	{
		// ILSpy could not decompile this member: Error decompiling @06002198 PortraitSwitcher.SelectIndex: Value cannot be null. (Parameter 'source')
		// Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// Error decompiling @06002198 PortraitSwitcher.SelectIndex
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

	[DebuggerHidden]
	private IEnumerator WaitForSwitchConfirmation(Portrait portrait, Character character, int index)
	{
		// ILSpy could not decompile this member: Error decompiling @06002199 PortraitSwitcher.WaitForSwitchConfirmation: Value cannot be null. (Parameter 'source')
		// Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// Error decompiling @06002199 PortraitSwitcher.WaitForSwitchConfirmation
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
			}, new Vector3(1.25f, 1.25f, 1f), 0.05f).SetEase(Ease.OutSine).SetUpdate(isIndependentUpdate: true);
			swipeCounter = 0.2f;
		}
		isDown = true;
		downMousePosition = Input.mousePosition;
		curMousePosition = (prevMousePosition = guiCamera.ScreenToWorldPoint(Input.mousePosition));
		Vector3 localPosition = transform.localPosition;
		localPosition.z = blackFade.transform.localPosition.z - 100f;
		transform.localPosition = localPosition;
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

	private void HandleOnRelease()
	{
		// ILSpy could not decompile this member: Error decompiling @0600219F PortraitSwitcher.HandleOnRelease: Value cannot be null. (Parameter 'source')
		// Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// Error decompiling @0600219F PortraitSwitcher.HandleOnRelease
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
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Default(ILInstruction) + 0xa7
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlock(Block) + 0x51
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitIfInstruction(IfInstruction) + 0x3d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlock(Block) + 0x51
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitIfInstruction(IfInstruction) + 0x56
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockStatement, BlockContainer, IEnumerable`1, Boolean) + 0x26b
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockContainer, Boolean) + 0x47
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlockContainer(BlockContainer) + 0x94
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertAsBlock(ILInstruction) + 0x11
		//    at ICSharpCode.Decompiler.CSharp.CSharpDecompiler.DecompileBody(IMethod, EntityDeclaration, DecompileRun, ITypeResolveContext, ExtensionInfo) + 0x426
		// -- continuing with outer exception (ICSharpCode.Decompiler.DecompilerException) --
		// 
	}

	public void ExitSelectMode()
	{
		// ILSpy could not decompile this member: Error decompiling @060021A0 PortraitSwitcher.ExitSelectMode: Value cannot be null. (Parameter 'source')
		// Please report the exception below,
		// along with the assembly it came from, at https://github.com/icsharpcode/ILSpy/issues/new
		// Error decompiling @060021A0 PortraitSwitcher.ExitSelectMode
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
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Default(ILInstruction) + 0xa7
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockStatement, BlockContainer, IEnumerable`1, Boolean) + 0x26b
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertBlockContainer(BlockContainer, Boolean) + 0x47
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.VisitBlockContainer(BlockContainer) + 0x94
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.Convert(ILInstruction) + 0x5d
		//    at ICSharpCode.Decompiler.CSharp.StatementBuilder.ConvertAsBlock(ILInstruction) + 0x11
		//    at ICSharpCode.Decompiler.CSharp.CSharpDecompiler.DecompileBody(IMethod, EntityDeclaration, DecompileRun, ITypeResolveContext, ExtensionInfo) + 0x426
		// -- continuing with outer exception (ICSharpCode.Decompiler.DecompilerException) --
		// 
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
		if (!isSelectMode && SelectModeExitedLate != null)
		{
			SelectModeExitedLate();
		}
	}

	private void OnPortraitTweenComplete()
	{
		portraitTween = null;
	}

	protected void OnCharacterChanged(Character character)
	{
		if (CharacterChanged != null)
		{
			CharacterChanged(character);
		}
	}

	protected void OnEnabled()
	{
		if (Enabled != null)
		{
			Enabled();
		}
	}

	protected void OnDisabled()
	{
		if (Disabled != null)
		{
			Disabled();
		}
	}
}
