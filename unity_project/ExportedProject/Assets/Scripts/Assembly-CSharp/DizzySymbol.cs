using UnityEngine;

public class DizzySymbol : MonoBehaviour, IPoolable
{
	private Camera lookAtCamera;

	private Transform followTrans;

	private Vector3 followOffset = new Vector3(0f, 1.55f, 0f);

	private BattleCharacter _battleCharacter;

	public BattleCharacter battleCharacter
	{
		get
		{
			return _battleCharacter;
		}
		set
		{
			Init(value);
		}
	}

	private void LateUpdate()
	{
		if (lookAtCamera != null)
		{
			base.transform.LookAt(lookAtCamera.transform);
		}
		if (followTrans != null)
		{
			base.transform.position = Vector3.Lerp(base.transform.position, followTrans.position + followOffset, Time.deltaTime * 12f);
		}
	}

	public void Init(BattleCharacter battleCharacter)
	{
		if (!(battleCharacter == _battleCharacter))
		{
			_battleCharacter = battleCharacter;
			if (_battleCharacter != null)
			{
				lookAtCamera = ((!(_battleCharacter == BattleController.instance.playerCharacter)) ? BattleController.instance.enemyBattleCamera.camera : BattleController.instance.playerBattleCamera.camera);
				followTrans = _battleCharacter.tofuAnchor;
				base.transform.position = followTrans.position + followOffset;
			}
		}
	}

	public void ResetObject()
	{
		_battleCharacter = null;
	}
}
