using TMPro;
using UnityEngine;

public class Marquee : MonoBehaviour
{
	public Vector3 resetPosition;

	private float offScreenLeft;

	private float width = 100f;

	private TextMeshPro _textMesh;

	private TextMeshPro textMesh
	{
		get
		{
			if (_textMesh == null)
			{
				_textMesh = GetComponent<TextMeshPro>();
			}
			return _textMesh;
		}
	}

	private void Start()
	{
		offScreenLeft = CityController.instance.guiCamera2.ScreenToWorldPoint(Vector3.zero).x;
	}

	private void LateUpdate()
	{
		if (textMesh != null)
		{
			width = textMesh.bounds.size.x;
		}
		float x = base.transform.localPosition.x - Time.deltaTime * 60f;
		if (base.transform.position.x + width < offScreenLeft)
		{
			x = resetPosition.x;
		}
		base.transform.localPosition = new Vector3(x, base.transform.localPosition.y, base.transform.localPosition.z);
	}

	public void ResetPosition()
	{
		base.transform.localPosition = new Vector3(resetPosition.x, base.transform.localPosition.y, base.transform.localPosition.z);
	}
}
