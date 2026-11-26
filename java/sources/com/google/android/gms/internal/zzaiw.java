package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.media.MediaCodecInfo;
import android.media.MediaCodecList;
import android.os.Build;
import android.util.Range;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaiw {
    private static List<MediaCodecInfo> zzaaA;
    private static Map<String, List<Map<String, Object>>> zzaaz = new HashMap();
    private static final Object zzaaB = new Object();

    @TargetApi(21)
    private static Integer[] zza(Range<Integer> range) {
        return new Integer[]{(Integer) range.getLower(), (Integer) range.getUpper()};
    }

    @TargetApi(16)
    public static List<Map<String, Object>> zzaQ(String str) {
        synchronized (zzaaB) {
            if (zzaaz.containsKey(str)) {
                return zzaaz.get(str);
            }
            try {
                synchronized (zzaaB) {
                    if (zzaaA == null) {
                        if (Build.VERSION.SDK_INT >= 21) {
                            zzaaA = Arrays.asList(new MediaCodecList(0).getCodecInfos());
                        } else if (Build.VERSION.SDK_INT >= 16) {
                            int codecCount = MediaCodecList.getCodecCount();
                            zzaaA = new ArrayList(codecCount);
                            for (int i = 0; i < codecCount; i++) {
                                zzaaA.add(MediaCodecList.getCodecInfoAt(i));
                            }
                        } else {
                            zzaaA = Collections.emptyList();
                        }
                    }
                }
                ArrayList arrayList = new ArrayList();
                for (MediaCodecInfo mediaCodecInfo : zzaaA) {
                    if (!mediaCodecInfo.isEncoder() && Arrays.asList(mediaCodecInfo.getSupportedTypes()).contains(str)) {
                        HashMap map = new HashMap();
                        map.put("codecName", mediaCodecInfo.getName());
                        MediaCodecInfo.CodecCapabilities capabilitiesForType = mediaCodecInfo.getCapabilitiesForType(str);
                        ArrayList arrayList2 = new ArrayList();
                        for (MediaCodecInfo.CodecProfileLevel codecProfileLevel : capabilitiesForType.profileLevels) {
                            arrayList2.add(new Integer[]{Integer.valueOf(codecProfileLevel.profile), Integer.valueOf(codecProfileLevel.level)});
                        }
                        map.put("profileLevels", arrayList2);
                        if (Build.VERSION.SDK_INT >= 21) {
                            MediaCodecInfo.VideoCapabilities videoCapabilities = capabilitiesForType.getVideoCapabilities();
                            map.put("bitRatesBps", zza(videoCapabilities.getBitrateRange()));
                            map.put("widthAlignment", Integer.valueOf(videoCapabilities.getWidthAlignment()));
                            map.put("heightAlignment", Integer.valueOf(videoCapabilities.getHeightAlignment()));
                            map.put("frameRates", zza(videoCapabilities.getSupportedFrameRates()));
                            map.put("widths", zza(videoCapabilities.getSupportedWidths()));
                            map.put("heights", zza(videoCapabilities.getSupportedHeights()));
                        }
                        if (Build.VERSION.SDK_INT >= 23) {
                            map.put("instancesLimit", Integer.valueOf(capabilitiesForType.getMaxSupportedInstances()));
                        }
                        arrayList.add(map);
                    }
                }
                zzaaz.put(str, arrayList);
                return arrayList;
            } catch (LinkageError e) {
                e = e;
                HashMap map2 = new HashMap();
                map2.put("error", e.getClass().getSimpleName());
                ArrayList arrayList3 = new ArrayList();
                arrayList3.add(map2);
                zzaaz.put(str, arrayList3);
                return arrayList3;
            } catch (RuntimeException e2) {
                e = e2;
                HashMap map3 = new HashMap();
                map3.put("error", e.getClass().getSimpleName());
                ArrayList arrayList4 = new ArrayList();
                arrayList4.add(map3);
                zzaaz.put(str, arrayList4);
                return arrayList4;
            }
        }
    }
}
