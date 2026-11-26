using DG.Tweening;
using UnityEngine;

public class PortraitTestController : MonoBehaviour
{
	public PortraitSwitcher playerPortraits;

	public Ease ease = Ease.InOutSine;

	public float duration = 0.5f;

	public bool reverse;

	private bool happened;

	private void Start()
	{
	}
}
