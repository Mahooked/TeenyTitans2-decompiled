using UnityEngine;

namespace TouchScript;

internal sealed class GestureManager : MonoBehaviour
{
	public static IGestureManager Instance => GestureManagerInstance.Instance;
}
