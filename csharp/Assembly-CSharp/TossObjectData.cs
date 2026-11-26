using System;

[Serializable]
public class TossObjectData
{
	public string path;

	public int damage;

	public RangedInt damageRange;

	public int weight;

	public TossObjectData()
	{
		path = string.Empty;
		damage = 3;
		damageRange = new RangedInt(3, 3);
		weight = 50;
	}

	public TossObjectData(string path)
	{
		this.path = path;
		damage = 3;
		damageRange = new RangedInt(3, 3);
		weight = 50;
	}

	public TossObjectData(string path, int damage, int weight)
	{
		this.path = path;
		this.damage = damage;
		this.weight = weight;
		damageRange = new RangedInt(3, 3);
	}
}
