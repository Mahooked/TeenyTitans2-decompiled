# Teeny Titans 2 (v1.1.4) - Decompiled

Full decompilation of the Android APK \Teeny Titans 2 1.1.4.apk\.

## Contents

- \AndroidManifest.xml\, \pktool.yml\ - decoded manifest & apktool metadata
- \es/\ - decoded resources (layouts, drawables, values)
- \ssets/\ - original APK assets (Unity data, videos, shaders, managed DLLs)
- \smali/\ - smali (Dalvik bytecode) from \classes.dex\
- \java/\ - Java source decompiled with JADX (from \classes.dex\)
- \csharp/\ - C# source decompiled with ILSpy from Unity managed assemblies
  (Assembly-CSharp, Assembly-CSharp-firstpass, Assembly-UnityScript, etc.)
- \lib/\ - native libraries (arm/x86)
- \original/\, \unknown/\ - apktool preserved files

## Tools used

- apktool 3.0.3
- JADX 1.5.7
- ILSpy (ilspycmd) 11.2.0
- UnityPy 1.25.4 (available for asset extraction)

## Notes

This is a Unity (Mono) game. Managed game code lives in
\ssets/bin/Data/Managed/Assembly-CSharp.dll\ and related assemblies,
decompiled under \csharp/Assembly-CSharp/\.

Native code (\lib/*.so\) is not decompiled here.
