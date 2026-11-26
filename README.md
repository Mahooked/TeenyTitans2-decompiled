# Teeny Titans 2 (v1.1.4) - Fully Decompiled

Full decompilation of the Android APK \Teeny Titans 2 1.1.4.apk\.

## Contents

- \AndroidManifest.xml\, \pktool.yml\ - decoded manifest & apktool metadata
- \es/\ - decoded Android resources (layouts, drawables, values)
- \ssets/\ - original APK assets (Unity data files, videos, shaders, managed DLLs)
- \smali/\ - smali (Dalvik bytecode) from \classes.dex\
- \java/\ - Java source decompiled with JADX (from \classes.dex\)
- \csharp/\ - C# source decompiled with ILSpy from Unity managed assemblies
  (Assembly-CSharp, Assembly-CSharp-firstpass, Assembly-UnityScript, DOTween, Photon, ProBuilder, etc.)
- \lib/\ - native libraries (arm/x86)
- \extracted/\ - media & data extracted from Unity assets
  (Texture2D, Sprite, Cubemap, AudioClip, Mesh (OBJ), TextAsset, Material (JSON), AnimationClip (JSON))
- \unity_project/\ - full Unity project reconstructed by AssetRipper
  (scenes, prefabs, materials, project settings) under \unity_project/ExportedProject\
- \
ative_strings/\ - readable strings extracted from native .so libraries
- \original/\, \unknown/\ - apktool preserved files

## Tools used

- apktool 3.0.3
- JADX 1.5.7
- ILSpy (ilspycmd) 11.2.0
- UnityPy 1.25.4
- AssetRipper 2.0.0

## Notes

This is a Unity (Mono) game. Managed game code lives in
\ssets/bin/Data/Managed/Assembly-CSharp.dll\ and related assemblies,
decompiled under \csharp/Assembly-CSharp/\.

Native code (\lib/*.so\) is not fully decompiled; only strings were extracted.
