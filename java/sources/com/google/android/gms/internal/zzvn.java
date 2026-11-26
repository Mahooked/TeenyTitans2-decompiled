package com.google.android.gms.internal;

import android.location.Location;
import android.support.annotation.Nullable;
import com.google.android.gms.ads.VideoOptions;
import com.google.android.gms.ads.formats.NativeAdOptions;
import com.google.android.gms.ads.mediation.NativeMediationAdRequest;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzvn implements NativeMediationAdRequest {
    private final int zzAe;
    private final boolean zzAq;
    private final int zzMZ;
    private final Date zzda;
    private final Set<String> zzdc;
    private final boolean zzdd;
    private final Location zzde;
    private final zzon zztS;
    private final List<String> zztT = new ArrayList();
    private final Map<String, Boolean> zzNh = new HashMap();

    public zzvn(@Nullable Date date, int i, @Nullable Set<String> set, @Nullable Location location, boolean z, int i2, zzon zzonVar, List<String> list, boolean z2) {
        this.zzda = date;
        this.zzAe = i;
        this.zzdc = set;
        this.zzde = location;
        this.zzdd = z;
        this.zzMZ = i2;
        this.zztS = zzonVar;
        this.zzAq = z2;
        if (list != null) {
            for (String str : list) {
                if (str.startsWith("custom:")) {
                    String[] strArrSplit = str.split(":", 3);
                    if (strArrSplit.length == 3) {
                        if ("true".equals(strArrSplit[2])) {
                            this.zzNh.put(strArrSplit[1], true);
                        } else if ("false".equals(strArrSplit[2])) {
                            this.zzNh.put(strArrSplit[1], false);
                        }
                    }
                } else {
                    this.zztT.add(str);
                }
            }
        }
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final Date getBirthday() {
        return this.zzda;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final int getGender() {
        return this.zzAe;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final Set<String> getKeywords() {
        return this.zzdc;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final Location getLocation() {
        return this.zzde;
    }

    @Override // com.google.android.gms.ads.mediation.NativeMediationAdRequest
    public final NativeAdOptions getNativeAdOptions() {
        if (this.zztS == null) {
            return null;
        }
        NativeAdOptions.Builder requestMultipleImages = new NativeAdOptions.Builder().setReturnUrlsForImageAssets(this.zztS.zzIn).setImageOrientation(this.zztS.zzIo).setRequestMultipleImages(this.zztS.zzIp);
        if (this.zztS.versionCode >= 2) {
            requestMultipleImages.setAdChoicesPlacement(this.zztS.zzIq);
        }
        if (this.zztS.versionCode >= 3 && this.zztS.zzIr != null) {
            requestMultipleImages.setVideoOptions(new VideoOptions(this.zztS.zzIr));
        }
        return requestMultipleImages.build();
    }

    @Override // com.google.android.gms.ads.mediation.NativeMediationAdRequest
    public final boolean isAppInstallAdRequested() {
        return this.zztT != null && this.zztT.contains("2");
    }

    @Override // com.google.android.gms.ads.mediation.NativeMediationAdRequest
    public final boolean isContentAdRequested() {
        return this.zztT != null && this.zztT.contains("1");
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final boolean isDesignedForFamilies() {
        return this.zzAq;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final boolean isTesting() {
        return this.zzdd;
    }

    @Override // com.google.android.gms.ads.mediation.MediationAdRequest
    public final int taggedForChildDirectedTreatment() {
        return this.zzMZ;
    }

    @Override // com.google.android.gms.ads.mediation.NativeMediationAdRequest
    public final Map<String, Boolean> zzfA() {
        return this.zzNh;
    }

    @Override // com.google.android.gms.ads.mediation.NativeMediationAdRequest
    public final boolean zzfz() {
        return this.zztT != null && this.zztT.contains("3");
    }
}
