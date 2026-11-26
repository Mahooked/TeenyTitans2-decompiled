using UnityEngine;

namespace Crosstales.UI
{
	public class UIDrag : MonoBehaviour
	{
		private float offsetX;

		private float offsetY;

		public void BeginDrag()
		{
			offsetX = base.transform.position.x - Input.mousePosition.x;
			offsetY = base.transform.position.y - Input.mousePosition.y;
		}

		public void OnDrag()
		{
			base.transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
		}
	}
}
