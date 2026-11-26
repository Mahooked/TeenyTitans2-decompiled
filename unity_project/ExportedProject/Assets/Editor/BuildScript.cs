using UnityEditor;
using UnityEngine;

public static class BuildScript
{
    public static void BuildAndroidArm64()
    {
        string[] scenes = EditorBuildSettings.scenes.Length > 0
            ? System.Array.ConvertAll(EditorBuildSettings.scenes, s => s.path)
            : new string[] { "Assets/Scenes/Init/Init.unity" };

        PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64 | AndroidArchitecture.ARMv7;
        // Use IL2CPP for 64-bit support in 2017.2+
        PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);

        string outPath = "Builds/TeenyTitans2_arm64.apk";
        System.IO.Directory.CreateDirectory("Builds");
        BuildPipeline.BuildPlayer(scenes, outPath, BuildTarget.Android, BuildOptions.None);
    }
}
