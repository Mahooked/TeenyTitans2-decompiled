using UnityEngine;

namespace Crosstales.OnlineCheck.Demo.Util
{
	[HelpURL("https://crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_demo_1_1_util_1_1_manage_endless_mode.html")]
	public class ManageEndlessMode : MonoBehaviour
	{
		public bool EndlessMode;

		private void Awake()
		{
			OnlineCheck.isEndlessMode = EndlessMode;
		}
	}
}
