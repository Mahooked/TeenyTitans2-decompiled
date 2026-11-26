using Crosstales.Common.Util;
using UnityEngine;

namespace Crosstales.DJ.Demo.Util;

[HelpURL("https://www.crosstales.com/media/data/assets/radio/api/class_crosstales_1_1_radio_1_1_demo_1_1_util_1_1_spectrum_visualizer.html")]
public class SpectrumVisualizer : MonoBehaviour
{
	public FFTAnalyzer Analyzer;

	public GameObject VisualPrefab;

	public float Width = 0.075f;

	public float Gain = 70f;

	public bool LeftToRight = true;

	[Range(0f, 1f)]
	public float Opacity = 1f;

	private Transform tf;

	private Transform[] visualTransforms;

	private Vector3 visualPos = Vector3.zero;

	private int samplesPerChannel;

	public void Start()
	{
		tf = transform;
		samplesPerChannel = Analyzer.Samples.Length / 2;
		visualTransforms = new Transform[samplesPerChannel];
		for (int i = 0; i < samplesPerChannel; i++)
		{
			GameObject gameObject = ((!LeftToRight) ? ((GameObject)Object.Instantiate((Object)VisualPrefab, new Vector3(tf.position.x - (float)i * Width, tf.position.y, tf.position.z), Quaternion.identity)) : ((GameObject)Object.Instantiate((Object)VisualPrefab, new Vector3(tf.position.x + (float)i * Width, tf.position.y, tf.position.z), Quaternion.identity)));
			gameObject.GetComponent<Renderer>().material.color = BaseHelper.HSVToRGB(360f / (float)samplesPerChannel * (float)i, 1f, 1f, Opacity);
			visualTransforms[i] = gameObject.GetComponent<Transform>();
			visualTransforms[i].parent = tf;
		}
	}

	public void Update()
	{
		for (int i = 0; i < visualTransforms.Length; i++)
		{
			visualPos.Set(Width, Analyzer.Samples[i] * Gain, Width);
			visualTransforms[i].localScale = visualPos;
		}
	}
}
