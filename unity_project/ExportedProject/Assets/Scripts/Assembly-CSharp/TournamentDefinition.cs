using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TournamentDefinition : ScriptableObject
{
	public CityName cityName;

	public CityLocation district;

	[SerializeField]
	private string _tournamentName = string.Empty;

	[SerializeField]
	private string _tournamentNameKey = string.Empty;

	[SerializeField]
	private tk2dSpriteCollectionData _tournamentSpriteCollection;

	[SerializeField]
	private string _backgroundSprite = string.Empty;

	[SerializeField]
	private int _backgroundSpriteId;

	[SerializeField]
	private string _bannerSprite = string.Empty;

	[SerializeField]
	private int _bannerSpriteId;

	[SerializeField]
	private string _bannerTopperSprite = string.Empty;

	[SerializeField]
	private int _bannerTopperSpriteId;

	[SerializeField]
	private string _bannerTextMaterial = string.Empty;

	[SerializeField]
	private string _trophySprite = string.Empty;

	[SerializeField]
	private int _trophySpriteId;

	[SerializeField]
	private string _trophyModel = string.Empty;

	[SerializeField]
	private Conversation _preConversation;

	[SerializeField]
	private Conversation _postConversation;

	[SerializeField]
	private int _coinReward;

	[SerializeField]
	private bool _containsChallenge;

	[SerializeField]
	private List<CharacterRewardData> _figureRewards = new List<CharacterRewardData>();

	[SerializeField]
	private List<TournamentRound> _rounds = new List<TournamentRound>();

	[SerializeField]
	private int _tournamentOrder = -1;

	[SerializeField]
	private TimeOfDay _timeOfDay;

	[SerializeField]
	private string _achievementReward;

	public string tournamentName
	{
		get
		{
			return _tournamentName;
		}
	}

	public string tournamentNameKey
	{
		get
		{
			return _tournamentNameKey;
		}
	}

	public tk2dSpriteCollectionData tournamentSpriteCollection
	{
		get
		{
			return _tournamentSpriteCollection;
		}
	}

	public string backgroundSprite
	{
		get
		{
			return _backgroundSprite;
		}
	}

	public int backgroundSpriteId
	{
		get
		{
			return _backgroundSpriteId;
		}
	}

	public string bannerSprite
	{
		get
		{
			return _bannerSprite;
		}
	}

	public int bannerSpriteId
	{
		get
		{
			return _bannerSpriteId;
		}
	}

	public string bannerTopperSprite
	{
		get
		{
			return _bannerTopperSprite;
		}
	}

	public int bannerTopperSpriteId
	{
		get
		{
			return _bannerTopperSpriteId;
		}
	}

	public string bannerTextMaterial
	{
		get
		{
			return _bannerTextMaterial;
		}
	}

	public string trophySprite
	{
		get
		{
			return _trophySprite;
		}
	}

	public int trophySpriteId
	{
		get
		{
			return _trophySpriteId;
		}
	}

	public string trophyModel
	{
		get
		{
			return _trophyModel;
		}
	}

	public Conversation preConversation
	{
		get
		{
			return _preConversation;
		}
	}

	public Conversation postConversation
	{
		get
		{
			return _postConversation;
		}
	}

	public int coinReward
	{
		get
		{
			return _coinReward;
		}
	}

	public bool containsChallenge
	{
		get
		{
			return _containsChallenge;
		}
	}

	public List<CharacterRewardData> figureRewards
	{
		get
		{
			return _figureRewards;
		}
	}

	public List<TournamentRound> rounds
	{
		get
		{
			return _rounds;
		}
	}

	public int numRounds
	{
		get
		{
			return _rounds.Count;
		}
	}

	public int tournamentOrder
	{
		get
		{
			return _tournamentOrder;
		}
	}

	public TimeOfDay timeOfDay
	{
		get
		{
			return _timeOfDay;
		}
	}

	public string achievementReward
	{
		get
		{
			return _achievementReward;
		}
	}
}
