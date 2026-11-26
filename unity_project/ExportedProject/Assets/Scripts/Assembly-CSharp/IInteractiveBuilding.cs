using System;

public interface IInteractiveBuilding
{
	Action interactionFunction { get; }

	event Action<bool> OpenStatusUpdated;
}
