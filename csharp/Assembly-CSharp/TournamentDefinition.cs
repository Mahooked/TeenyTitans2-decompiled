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

	public string tournamentName => _tournamentName;

	public string tournamentNameKey => _tournamentNameKey;

	public tk2dSpriteCollectionData tournamentSpriteCollection => _tournamentSpriteCollection;

	public string backgroundSprite => _backgroundSprite;

	public int backgroundSpriteId => _backgroundSpriteId;

	public string bannerSprite => _bannerSprite;

	public int bannerSpriteId => _bannerSpriteId;

	public string bannerTopperSprite => _bannerTopperSprite;

	public int bannerTopperSpriteId => _bannerTopperSpriteId;

	public string bannerTextMaterial => _bannerTextMaterial;

	public string trophySprite => _trophySprite;

	public int trophySpriteId => _trophySpriteId;

	public string trophyModel => _trophyModel;

	public Conversation preConversation => _preConversation;

	public Conversation postConversation => _postConversation;

	public int coinReward => _coinReward;

	public bool containsChallenge => _containsChallenge;

	public List<CharacterRewardData> figureRewards => _figureRewards;

	public List<TournamentRound> rounds => _rounds;

	public int numRounds => _rounds.Count;

	public int tournamentOrder => _tournamentOrder;

	public TimeOfDay timeOfDay => _timeOfDay;

	public string achievementReward => _achievementReward;
}
