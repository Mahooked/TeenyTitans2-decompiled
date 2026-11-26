# Building Teeny Titans 2 as 64-bit Android (arm64-v8a)

## Prerequisites
- Unity Editor **2017.2.0f3** (installed under \C:\AIC\unity\2017.2.0f3\ on this machine)
- Android Build Support module (installed)
- Android SDK + NDK (Unity's bundled NDK or a compatible NDK r13b)
- A Unity **personal/pro license** activated in Unity Hub (required to run the editor)
- JDK 8 (Unity 2017.2 prefers JDK 8)

## Steps
1. Open Unity Hub, sign in, and activate a personal license.
2. Open project: \unity_project/ExportedProject\
3. Fix compile errors:
   - Remove duplicate managed DLLs from \Assets/Plugins\ that now have source under \Assets/Scripts\ (e.g. Assembly-CSharp.dll) to avoid duplicate type definitions.
   - Resolve missing script references / shaders.
4. Open **File > Build Settings**, switch platform to **Android**.
5. Player Settings:
   - Target Architectures: **ARM64** (and ARMv7 if you want 32-bit fallback)
   - Scripting Backend: **IL2CPP**
   - API Level: at least 21
6. Run the menu-less build via:
   \\\
   Unity.exe -batchmode -nographics -projectPath <path> -executeMethod BuildScript.BuildAndroidArm64 -quit -logFile build.log
   \\\
   (Build script provided at \Assets/Editor/BuildScript.cs\.)

## Known blockers
- AssetRipper export is not a clean build; expect missing references and shader errors.
- Native plugin \libBlueDoveMediaRender.so\ has no 64-bit variant in the original APK; media playback may break unless a 64-bit version is sourced or the plugin is removed.
- Third-party SDKs (Facebook, Photon, Google Play, Amazon) may need 64-bit-compatible versions.
- IL2CPP builds require the NDK and a compatible C++ toolchain.

## Status on this machine
- Unity 2017.2.0f3 + Android module installed.
- **License not activated** — building requires you to sign in to Unity Hub and activate a personal license.
