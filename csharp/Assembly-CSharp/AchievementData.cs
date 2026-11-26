using System;

[Serializable]
public class AchievementData
{
	public string id;

	public string title;

	public string description;

	public string icon;

	public int iconSpriteId;

	public string playID;

	public bool reportProgress = true;

	public int amountNeeded;

	public bool foldout = true;

	public AchievementData(string _id, string _title, string _description, string _icon, string _playID)
	{
		id = _id;
		title = _title;
		description = _description;
		icon = _icon;
		playID = _playID;
	}
}
