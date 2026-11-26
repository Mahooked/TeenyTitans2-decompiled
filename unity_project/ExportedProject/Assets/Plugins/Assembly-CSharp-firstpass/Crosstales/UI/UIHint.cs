using System.Collections;
using UnityEngine;

namespace Crosstales.UI
{
	public class UIHint : MonoBehaviour
	{
		[Tooltip("Group to fade.")]
		public CanvasGroup Group;

		[Tooltip("Delay in seconds before fading (default: 2).")]
		public float Delay = 2f;

		[Tooltip("Fade time in seconds (default: 2).")]
		public float FadeTime = 2f;

		[Tooltip("Destroy UI element after the fade (default: true).")]
		public bool DestroyWhenFinished = true;

		public void Start()
		{
			StartCoroutine(fadeTo(0f, Delay, FadeTime, DestroyWhenFinished));
		}

		private IEnumerator fadeTo(float aValue, float delay, float aTime, bool destroy)
		{
			yield return new WaitForSeconds(delay);
			float alpha = Group.alpha;
			for (float t = 0f; t < 1f; t += Time.deltaTime / aTime)
			{
				Group.alpha = Mathf.Lerp(alpha, aValue, t);
				yield return null;
			}
			if (destroy)
			{
				Object.Destroy(base.gameObject, 0.5f);
			}
		}
	}
}
