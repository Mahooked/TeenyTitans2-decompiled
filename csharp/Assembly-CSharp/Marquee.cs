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
		float x = transform.localPosition.x - Time.deltaTime * 60f;
		if (transform.position.x + width < offScreenLeft)
		{
			x = resetPosition.x;
		}
		transform.localPosition = new Vector3(x, transform.localPosition.y, transform.localPosition.z);
	}

	public void ResetPosition()
	{
		transform.localPosition = new Vector3(resetPosition.x, transform.localPosition.y, transform.localPosition.z);
	}
}
