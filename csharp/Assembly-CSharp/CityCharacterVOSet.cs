using System;
using UnityEngine;

[Serializable]
public class CityCharacterVOSet
{
	public CityCharacterType cityCharacterType;

	public CityCharacterVODatabase.AudioClipSet idleClipSet;

	public CityCharacterVODatabase.AudioClipSet enterBeachClipSet;

	public CityCharacterVODatabase.AudioClipSet enterJumpCityClipSet;

	public CityCharacterVODatabase.AudioClipSet enterGothamCityClipSet;

	public CityCharacterVODatabase.AudioClipSet enterMetropolisClipSet;

	public CityCharacterVODatabase.AudioClipSet newDayClipSet;

	public CityCharacterVODatabase.AudioClipSet newNightClipSet;

	public CityCharacterVODatabase.AudioClipSet enhanceFeedClipSet;

	public CityCharacterVODatabase.AudioClipSet enhanceShinyClipSet;

	public CityCharacterVODatabase.AudioClipSet storeAddCartClipSet;

	public CityCharacterVODatabase.AudioClipSet storeEnterClipSet;

	public CityCharacterVODatabase.AudioClipSet storeFigTossClipSet;

	public CityCharacterVODatabase.AudioClipSet storeTutorialFirstSwipeClipSet;

	public CityCharacterVODatabase.AudioClipSet storeTutorialFigTossClipSet;

	public CityCharacterVODatabase.AudioClipSet towerFigsClipSet;

	public CityCharacterVODatabase.AudioClipSet towerFigsRareClipSet;

	public CityCharacterVODatabase.AudioClipSet enterPrebattleClipSet;

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
}
