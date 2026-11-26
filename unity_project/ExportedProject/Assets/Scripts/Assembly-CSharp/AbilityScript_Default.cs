using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript_Default : AbilityScript
{
	private List<GameObject> addedObjects = new List<GameObject>();

	public override IEnumerator Play()
	{
		if (string.IsNullOrEmpty(_abilityData.animName))
		{
			yield return new WaitForSeconds(0.1f);
			ProcessAbility();
			yield return new WaitForSeconds(0.1f);
		}
		else
		{
			_caster.Play(_abilityData.animName, true);
			_casterCamera.Play(_abilityData.animName, true);
		}
	}

	protected override void HandleEventTriggered(AnimationEvent evt)
	{
		base.HandleEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		switch (array[0])
		{
		case "hit":
			ProcessAbility();
			if (array.Length > 1 && array[1].Equals("shake"))
			{
				ShakeCamera(_casterCamera, evt.floatParameter);
			}
			break;
		case "shake":
			ShakeCamera(_casterCamera, evt.floatParameter);
			break;
		case "addObject":
			if (array.Length > 1)
			{
				GameObject objectFromResource = ObjectPool.GetObjectFromResource(array[1], _caster.model.position);
				if (objectFromResource != null)
				{
					objectFromResource.transform.localScale = _caster.transform.localScale;
					addedObjects.Add(objectFromResource);
				}
			}
			else if (evt.objectReferenceParameter != null)
			{
				ObjectPool.instance.AddType((GameObject)evt.objectReferenceParameter, 1);
				GameObject gameObject2 = ObjectPool.GetObject(evt.objectReferenceParameter.name, _caster.model.position);
				if (gameObject2 != null)
				{
					gameObject2.transform.localScale = _caster.transform.localScale;
					addedObjects.Add(gameObject2);
				}
			}
			break;
		case "addObjectTo":
			if (array.Length > 1 && evt.objectReferenceParameter != null)
			{
				ObjectPool.instance.AddType((GameObject)evt.objectReferenceParameter, 1);
				GameObject gameObject = ObjectPool.GetObject(evt.objectReferenceParameter.name, _caster.model.position);
				if (gameObject != null)
				{
					Transform transform = _caster.transform.Find(array[1]);
					Debug.Log(array[1] + "     " + transform);
					gameObject.transform.parent = transform;
					gameObject.transform.localPosition = Vector3.zero;
					gameObject.transform.localRotation = Quaternion.identity;
					gameObject.transform.localScale = Vector3.one;
					addedObjects.Add(gameObject);
				}
			}
			break;
		case "playHit":
			if (!_target.character.HasStatusEffect<Flight>())
			{
				_target.PlayHit();
			}
			break;
		case "playSmallHit":
			if (!_target.character.HasStatusEffect<Flight>())
			{
				_target.PlaySmallHit();
			}
			break;
		}
	}

	protected override void HandleExtrasEventTriggered(AnimationEvent evt)
	{
		base.HandleExtrasEventTriggered(evt);
		string[] array = evt.stringParameter.Split(':');
		switch (array[0])
		{
		case "enable":
			if (array.Length > 1)
			{
				Transform transform2 = _caster.extrasAnimator.transform.Find(array[1]);
				if (transform2 != null)
				{
					transform2.gameObject.SetActive(true);
				}
			}
			break;
		case "disable":
			if (array.Length > 1)
			{
				Transform transform = _caster.extrasAnimator.transform.Find(array[1]);
				if (transform != null)
				{
					transform.gameObject.SetActive(false);
				}
			}
			break;
		}
	}

	public override void CleanUp()
	{
		base.CleanUp();
		while (addedObjects.Count > 0)
		{
			ObjectPool.PoolObject(addedObjects.Pop());
		}
	}
}
