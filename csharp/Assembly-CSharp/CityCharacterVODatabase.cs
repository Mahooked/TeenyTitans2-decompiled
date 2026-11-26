using System;
using UnityEngine;

public class CityCharacterVODatabase : ScriptableObject
{
	[Serializable]
	public class AudioClipSet
	{
		public string[] clipNames;

		[SerializeField]
		private bool _foldout;

		public bool foldout
		{
			get
			{
				return _foldout;
			}
			set
			{
				_foldout = value;
			}
		}

		public string this[int index]
		{
			get
			{
				return clipNames[index];
			}
			set
			{
				clipNames[index] = value;
			}
		}

		public int Length => clipNames.Length;

		public string GetRandom()
		{
			string text = ((clipNames.Length <= 0) ? string.Empty : clipNames[UnityEngine.Random.Range(0, clipNames.Length)]);
			Debug.Log("Audio Clip Set, Get Random(), name: " + text);
			return text;
		}
	}

	public CityCharacterVOSet[] characterVOSets;

	public CityCharacterVOSet GetCharacterVOSet(CityCharacterType character)
	{
		int num = characterVOSets.Length;
		for (int i = 0; i < num; i++)
		{
			CityCharacterVOSet cityCharacterVOSet = characterVOSets[i];
			if (cityCharacterVOSet.cityCharacterType == character)
			{
				return cityCharacterVOSet;
			}
		}
		return null;
	}
}
