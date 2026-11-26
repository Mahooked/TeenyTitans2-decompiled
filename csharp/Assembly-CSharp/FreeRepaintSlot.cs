using System;

[Serializable]
public class FreeRepaintSlot
{
	public int repaintID;

	public int count;

	public FreeRepaintSlot(int repaintID, int count)
	{
		this.repaintID = repaintID;
		this.count = count;
	}

	public override bool Equals(object obj)
	{
		if (!obj.GetType().Equals(typeof(FreeRepaintSlot)))
		{
			return false;
		}
		return ((FreeRepaintSlot)obj).repaintID == repaintID;
	}

	public override int GetHashCode()
	{
		return base.GetHashCode() ^ repaintID.GetHashCode();
	}
}
