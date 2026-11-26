using TMPro;
using UnityEngine;

public class KeepOnScreenSpriteTk2d : MonoBehaviour
{
	[Tooltip("The Transform component of the GameObject whose position should be updated to keep the content on-screen")]
	public Transform holder;

	public tk2dBaseSprite boundsSprite;

	public tk2dTextMesh boundsTextMesh;

	public TextMeshPro boundsTMPro;

	public Vector2 padding;

	public bool useWorldCoordinates;

	public bool checkHorizontal = true;

	public bool checkVertical = true;

	private tk2dCamera _cameraTk2d;

	private Camera _camera;

	private Vector3 _originalPosition;

	private Vector3 previousPosition;

	private Vector3 previousTransformScale;

	private Vector3 previousHolderScale;

	private Vector3 previousScale;

	private Vector3 previousBoundsPosition;

	private Vector2 previousPadding;

	private bool isDirty;

	private void OnEnable()
	{
		isDirty = true;
	}

	private void Start()
	{
		ResetCameras();
		_originalPosition = holder.localPosition;
		previousPosition = transform.position;
		previousTransformScale = transform.localScale;
		previousHolderScale = holder.localScale;
		ref Vector3 reference = ref previousScale;
		Vector3 vector;
		if (boundsSprite != null)
		{
			vector = boundsSprite.scale;
		}
		else
		{
			vector = ((!(boundsTextMesh != null)) ? boundsTMPro.transform.localScale : boundsTextMesh.scale);
		}
		reference = vector;
		ref Vector3 reference2 = ref previousBoundsPosition;
		Vector3 vector2;
		if (boundsSprite != null)
		{
			vector2 = boundsSprite.transform.position;
		}
		else
		{
			vector2 = ((!(boundsTextMesh != null)) ? boundsTMPro.transform.position : boundsTextMesh.transform.position);
		}
		reference2 = vector2;
		previousPadding = padding;
	}

	private void LateUpdate()
	{
		if (isDirty || transform.position != previousPosition || transform.localScale != previousTransformScale || holder.localScale != previousHolderScale || IsBoundsDirty() || padding != previousPadding)
		{
			isDirty = false;
			Vector3 zero = Vector3.zero;
			holder.localPosition = _originalPosition;
			if (useWorldCoordinates)
			{
				Rect screenExtents = _cameraTk2d.ScreenExtents;
				screenExtents.x += _cameraTk2d.transform.position.x;
				screenExtents.y += _cameraTk2d.transform.position.y;
				Bounds bounds = GetBounds();
				Vector3 vector = Vector3.zero;
				Vector3 vector2 = Vector3.zero;
				if (boundsSprite != null)
				{
					if (boundsSprite.transform.parent == null)
					{
						vector = bounds.min;
						vector2 = bounds.max;
					}
					else
					{
						vector = boundsSprite.transform.parent.TransformPoint(bounds.min);
						vector2 = boundsSprite.transform.parent.TransformPoint(bounds.max);
					}
				}
				else if (boundsTextMesh != null)
				{
					vector = boundsTextMesh.transform.TransformPoint(bounds.min);
					vector2 = boundsTextMesh.transform.TransformPoint(bounds.max);
				}
				else if (boundsTMPro != null)
				{
					vector = boundsTMPro.transform.TransformPoint(bounds.min);
					vector2 = boundsTMPro.transform.TransformPoint(bounds.max);
				}
				if (checkHorizontal)
				{
					if (vector2.x > screenExtents.xMax - padding.x)
					{
						zero.x = new Vector3(screenExtents.xMax - padding.x, 0f).x - new Vector3(vector2.x, 0f).x;
					}
					else if (vector.x < screenExtents.xMin + padding.x)
					{
						zero.x = new Vector3(screenExtents.xMin + padding.x, 0f).x - new Vector3(vector.x, 0f).x;
					}
				}
				if (checkVertical)
				{
					if (vector2.y > screenExtents.yMax - padding.y)
					{
						zero.y = new Vector3(0f, screenExtents.yMax - padding.y).y - new Vector3(0f, vector2.y).y;
					}
					else if (vector.y < screenExtents.yMin + padding.y)
					{
						zero.y = new Vector3(0f, screenExtents.yMin + padding.y).y - new Vector3(0f, vector.y).y;
					}
				}
			}
			else
			{
				Rect pixelRect = _camera.pixelRect;
				Bounds bounds2 = GetBounds();
				Vector3 vector3 = Vector3.zero;
				Vector3 vector4 = Vector3.zero;
				if (boundsSprite != null)
				{
					if (boundsSprite.transform.parent == null)
					{
						vector3 = _camera.WorldToScreenPoint(bounds2.min);
						vector4 = _camera.WorldToScreenPoint(bounds2.max);
					}
					else
					{
						vector3 = _camera.WorldToScreenPoint(boundsSprite.transform.parent.TransformPoint(bounds2.min));
						vector4 = _camera.WorldToScreenPoint(boundsSprite.transform.parent.TransformPoint(bounds2.max));
					}
				}
				else if (boundsTextMesh != null)
				{
					vector3 = _camera.WorldToScreenPoint(boundsTextMesh.transform.TransformPoint(bounds2.min));
					vector4 = _camera.WorldToScreenPoint(boundsTextMesh.transform.TransformPoint(bounds2.max));
				}
				else if (boundsTMPro != null)
				{
					vector3 = _camera.WorldToScreenPoint(boundsTMPro.transform.TransformPoint(bounds2.min));
					vector4 = _camera.WorldToScreenPoint(boundsTMPro.transform.TransformPoint(bounds2.max));
				}
				if (checkHorizontal)
				{
					if (vector4.x > pixelRect.width - padding.x)
					{
						zero.x = _camera.ScreenToWorldPoint(new Vector3(pixelRect.width - padding.x, 0f)).x - _camera.ScreenToWorldPoint(new Vector3(vector4.x, 0f)).x;
					}
					else if (vector3.x < padding.x)
					{
						zero.x = _camera.ScreenToWorldPoint(new Vector3(padding.x, 0f)).x - _camera.ScreenToWorldPoint(new Vector3(vector3.x, 0f)).x;
					}
				}
				if (checkVertical)
				{
					if (vector4.y > pixelRect.height - padding.y)
					{
						zero.y = _camera.ScreenToWorldPoint(new Vector3(0f, pixelRect.height - padding.y)).y - _camera.ScreenToWorldPoint(new Vector3(0f, vector4.y)).y;
					}
					else if (vector3.y < padding.y)
					{
						zero.y = _camera.ScreenToWorldPoint(new Vector3(0f, padding.y)).y - _camera.ScreenToWorldPoint(new Vector3(0f, vector3.y)).y;
					}
				}
			}
			holder.localPosition += zero;
		}
		previousPosition = transform.position;
		previousTransformScale = transform.localScale;
		previousHolderScale = holder.localScale;
		ref Vector3 reference = ref previousScale;
		Vector3 vector5;
		if (boundsSprite != null)
		{
			vector5 = boundsSprite.scale;
		}
		else
		{
			vector5 = ((!(boundsTextMesh != null)) ? boundsTMPro.transform.localScale : boundsTextMesh.scale);
		}
		reference = vector5;
		ref Vector3 reference2 = ref previousBoundsPosition;
		Vector3 vector6;
		if (boundsSprite != null)
		{
			vector6 = boundsSprite.transform.position;
		}
		else
		{
			vector6 = ((!(boundsTextMesh != null)) ? boundsTMPro.transform.position : boundsTextMesh.transform.position);
		}
		reference2 = vector6;
		previousPadding = padding;
	}

	public void ResetCameras()
	{
		ref Camera camera = ref _camera;
		GameObject go;
		if (boundsSprite != null)
		{
			go = boundsSprite.gameObject;
		}
		else
		{
			go = ((!(boundsTextMesh != null)) ? boundsTMPro.gameObject : boundsTextMesh.gameObject);
		}
		camera = CameraUtility.GetCameraRenderingObject(go);
		if (_camera != null)
		{
			_cameraTk2d = _camera.GetComponent<tk2dCamera>();
			isDirty = true;
		}
	}

	public void SetOriginalPosition(Vector3 position)
	{
		holder.localPosition = position;
		_originalPosition = position;
		isDirty = true;
	}

	private bool IsBoundsDirty()
	{
		if (boundsSprite != null)
		{
			return boundsSprite.scale != previousScale || boundsSprite.transform.position != previousBoundsPosition;
		}
		if (boundsTextMesh != null)
		{
			return boundsTextMesh.scale != previousScale || boundsTextMesh.transform.position != previousBoundsPosition;
		}
		if (boundsTMPro != null)
		{
			return boundsTMPro.transform.localScale != previousScale || boundsTMPro.transform.position != previousBoundsPosition;
		}
		return false;
	}

	private Bounds GetBounds()
	{
		Bounds result = default;
		if (boundsSprite != null)
		{
			if (!(boundsSprite is tk2dSlicedSprite))
			{
				return boundsSprite.GetBounds();
			}
			tk2dSlicedSprite tk2dSlicedSprite2 = boundsSprite as tk2dSlicedSprite;
			Vector3 localPosition = tk2dSlicedSprite2.transform.localPosition;
			switch (tk2dSlicedSprite2.anchor)
			{
			case tk2dBaseSprite.Anchor.LowerCenter:
				result.center = new Vector3(localPosition.x, localPosition.y + tk2dSlicedSprite2.dimensions.y * 0.5f, 0f);
				break;
			case tk2dBaseSprite.Anchor.LowerLeft:
				result.center = new Vector3(localPosition.x + tk2dSlicedSprite2.dimensions.x * 0.5f, localPosition.y + tk2dSlicedSprite2.dimensions.y * 0.5f, 0f);
				break;
			case tk2dBaseSprite.Anchor.LowerRight:
				result.center = new Vector3(localPosition.x - tk2dSlicedSprite2.dimensions.x * 0.5f, localPosition.y + tk2dSlicedSprite2.dimensions.y * 0.5f, 0f);
				break;
			case tk2dBaseSprite.Anchor.MiddleCenter:
				result.center = new Vector3(localPosition.x, localPosition.y, 0f);
				break;
			case tk2dBaseSprite.Anchor.MiddleLeft:
				result.center = new Vector3(localPosition.x + tk2dSlicedSprite2.dimensions.x * 0.5f, localPosition.y, 0f);
				break;
			case tk2dBaseSprite.Anchor.MiddleRight:
				result.center = new Vector3(localPosition.x - tk2dSlicedSprite2.dimensions.x * 0.5f, localPosition.y, 0f);
				break;
			case tk2dBaseSprite.Anchor.UpperCenter:
				result.center = new Vector3(localPosition.x, localPosition.y - tk2dSlicedSprite2.dimensions.y * 0.5f, 0f);
				break;
			case tk2dBaseSprite.Anchor.UpperLeft:
				result.center = new Vector3(localPosition.x + tk2dSlicedSprite2.dimensions.x * 0.5f, localPosition.y - tk2dSlicedSprite2.dimensions.y * 0.5f, 0f);
				break;
			case tk2dBaseSprite.Anchor.UpperRight:
				result.center = new Vector3(localPosition.x - tk2dSlicedSprite2.dimensions.x * 0.5f, localPosition.y - tk2dSlicedSprite2.dimensions.y * 0.5f, 0f);
				break;
			}
			result.extents = new Vector3(tk2dSlicedSprite2.dimensions.x * 0.5f, tk2dSlicedSprite2.dimensions.y * 0.5f, 0f);
		}
		else
		{
			if (boundsTextMesh != null)
			{
				return boundsTextMesh.GetEstimatedMeshBoundsForString(boundsTextMesh.text);
			}
			if (boundsTMPro != null)
			{
				return boundsTMPro.bounds;
			}
		}
		return result;
	}
}
