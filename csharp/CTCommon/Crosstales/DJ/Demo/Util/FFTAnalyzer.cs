using UnityEngine;

namespace Crosstales.DJ.Demo.Util;

public class FFTAnalyzer : MonoBehaviour
{
	public float[] Samples = new float[256];

	public int Channel;

	public FFTWindow FFTMode = FFTWindow.BlackmanHarris;

	public void Update()
	{
		AudioListener.GetSpectrumData(Samples, Channel, FFTMode);
	}
}
