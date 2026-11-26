package com.google.android.gms.internal;

import com.google.ads.AdRequest;
import com.google.ads.AdSize;
import com.google.ads.mediation.MediationAdRequest;
import java.util.Date;
import java.util.HashSet;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwb {
    public static int zza(AdRequest.ErrorCode errorCode) {
        switch (zzwc.zzNn[errorCode.ordinal()]) {
            case 2:
                return 1;
            case 3:
                return 2;
            case 4:
                return 3;
            default:
                return 0;
        }
    }

    public static AdSize zzb(zziv zzivVar) {
        AdSize[] adSizeArr = {AdSize.SMART_BANNER, AdSize.BANNER, AdSize.IAB_MRECT, AdSize.IAB_BANNER, AdSize.IAB_LEADERBOARD, AdSize.IAB_WIDE_SKYSCRAPER};
        for (int i = 0; i < 6; i++) {
            if (adSizeArr[i].getWidth() == zzivVar.width && adSizeArr[i].getHeight() == zzivVar.height) {
                return adSizeArr[i];
            }
        }
        return new AdSize(com.google.android.gms.ads.zzb.zza(zzivVar.width, zzivVar.height, zzivVar.zzAs));
    }

    public static MediationAdRequest zzn(zzir zzirVar) {
        AdRequest.Gender gender;
        HashSet hashSet = zzirVar.zzzP != null ? new HashSet(zzirVar.zzzP) : null;
        Date date = new Date(zzirVar.zzzN);
        switch (zzirVar.zzzO) {
            case 1:
                gender = AdRequest.Gender.MALE;
                break;
            case 2:
                gender = AdRequest.Gender.FEMALE;
                break;
            default:
                gender = AdRequest.Gender.UNKNOWN;
                break;
        }
        return new MediationAdRequest(date, gender, hashSet, zzirVar.zzzQ, zzirVar.zzzV);
    }
}
