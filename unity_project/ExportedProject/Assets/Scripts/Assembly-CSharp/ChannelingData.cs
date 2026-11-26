using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChannelingData
{
	[Serializable]
	public class ChannelingStep
	{
		public float valueModifier;

		public float abilityEffectTickRate;

		public float duration;

		[SerializeField]
		private bool _foldout;

		public bool foldout
		{
			get
			{
				return _foldout;
			}
		}
	}

	public float speed;

	public List<ChannelingStep> channelingSteps = new List<ChannelingStep>();
}
