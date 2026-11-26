using Crosstales.Common.Util;
using Crosstales.OnlineCheck.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Crosstales.OnlineCheck.Demo
{
	[HelpURL("https://crosstales.com/media/data/assets/OnlineCheck/api/class_crosstales_1_1_online_check_1_1_demo_1_1_g_u_i_main.html")]
	public class GUIMain : MonoBehaviour
	{
		public Text Name;

		public Text Version;

		public Text Scene;

		public Image Status;

		public Slider MinimumSlider;

		public Slider MaximumSlider;

		public Text MinValue;

		public Text MaxValue;

		[Header("Stats")]
		public Text Available;

		public Text Reachability;

		public Text LastCheck;

		public Text Total;

		public Text PerMinute;

		public Text Data;

		public Text Runtime;

		public Text Uptime;

		public Text Downtime;

		private Color32 green = new Color32(129, 199, 132, 224);

		private Color32 red = new Color32(150, 39, 39, 224);

		private float elapsedTime;

		public void Start()
		{
			if (Name != null)
			{
				Name.text = "Online Check";
			}
			if (Version != null)
			{
				Version.text = "1.6.1";
			}
			if (Scene != null)
			{
				Scene.text = SceneManager.GetActiveScene().name;
			}
			if (OnlineCheck.isEndlessMode && MinimumSlider != null && MaximumSlider != null && MinValue != null && MaxValue != null)
			{
				MinimumSlider.value = OnlineCheck.CheckIntervalMin;
				MaximumSlider.value = OnlineCheck.CheckIntervalMax;
				MinValue.text = OnlineCheck.CheckIntervalMin.ToString();
				MaxValue.text = OnlineCheck.CheckIntervalMax.ToString();
				MinimumSlider.onValueChanged.AddListener(delegate
				{
					ChangeIntervalMin();
				});
				MaximumSlider.onValueChanged.AddListener(delegate
				{
					ChangeIntervalMax();
				});
			}
		}

		public void Update()
		{
			if (MinimumSlider != null && MaximumSlider != null && MinimumSlider.value >= MaximumSlider.value)
			{
				MinimumSlider.value = MaximumSlider.value - 1f;
			}
			elapsedTime += Time.deltaTime;
			if (elapsedTime > 1f)
			{
				if (Available != null)
				{
					Available.text = ((!OnlineCheck.isInternetAvailable) ? "no" : "yes");
				}
				if (Reachability != null)
				{
					Reachability.text = OnlineCheck.NetworkReachability.ToString();
				}
				if (LastCheck != null)
				{
					LastCheck.text = OnlineCheck.LastCheck.ToString();
				}
				if (Total != null)
				{
					Total.text = Context.NumberOfChecks.ToString();
				}
				if (PerMinute != null)
				{
					PerMinute.text = Context.ChecksPerMinute.ToString("#0.0");
				}
				if (Data != null)
				{
					Data.text = BaseHelper.FormatBytesToHRF(OnlineCheck.DataDownloaded).ToString();
				}
				if (Runtime != null)
				{
					Runtime.text = BaseHelper.FormatSecondsToHourMinSec(Context.Runtime);
				}
				if (Uptime != null)
				{
					Uptime.text = BaseHelper.FormatSecondsToHourMinSec(Context.Uptime);
				}
				if (Downtime != null)
				{
					Downtime.text = BaseHelper.FormatSecondsToHourMinSec(Context.Downtime);
				}
				elapsedTime = 0f;
			}
		}

		public void OnEnable()
		{
			OnlineCheck.OnOnlineCheckComplete += colorKnob;
		}

		public void OnDisable()
		{
			OnlineCheck.OnOnlineCheckComplete -= colorKnob;
		}

		public void Check()
		{
			OnlineCheck.Refresh();
		}

		public void ChangeIntervalMin()
		{
			OnlineCheck.CheckIntervalMax = (int)MaximumSlider.value;
			MaxValue.text = OnlineCheck.CheckIntervalMax.ToString();
			OnlineCheck.CheckIntervalMin = (int)MinimumSlider.value;
			MinValue.text = OnlineCheck.CheckIntervalMin.ToString();
		}

		public void ChangeIntervalMax()
		{
			OnlineCheck.CheckIntervalMin = (int)MinimumSlider.value;
			MinValue.text = OnlineCheck.CheckIntervalMin.ToString();
			OnlineCheck.CheckIntervalMax = (int)MaximumSlider.value;
			MaxValue.text = OnlineCheck.CheckIntervalMax.ToString();
		}

		private void colorKnob(bool available, NetworkReachability networkReachability)
		{
			if ((bool)Status)
			{
				if (available)
				{
					Status.color = green;
				}
				else
				{
					Status.color = red;
				}
			}
		}
	}
}
