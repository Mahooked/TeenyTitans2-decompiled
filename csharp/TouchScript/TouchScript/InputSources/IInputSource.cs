namespace TouchScript.InputSources;

public interface IInputSource
{
	ICoordinatesRemapper CoordinatesRemapper { get; set; }
}
