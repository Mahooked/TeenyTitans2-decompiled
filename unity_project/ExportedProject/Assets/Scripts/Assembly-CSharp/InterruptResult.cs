public class InterruptResult
{
	public bool isInterrupted;

	public bool isCausedByDamage;

	public InterruptResult(bool isInterrupted = true, bool isCausedByDamage = true)
	{
		this.isInterrupted = isInterrupted;
		this.isCausedByDamage = isCausedByDamage;
	}
}
