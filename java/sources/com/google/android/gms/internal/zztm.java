package com.google.android.gms.internal;

import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zztm {
    private final Object[] mParams;

    zztm(zzir zzirVar, String str, int i) {
        this.mParams = com.google.android.gms.ads.internal.zzp.zza((String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEa), zzirVar, str, i, null);
    }

    public final boolean equals(Object obj) {
        if (obj instanceof zztm) {
            return Arrays.equals(this.mParams, ((zztm) obj).mParams);
        }
        return false;
    }

    public final int hashCode() {
        return Arrays.hashCode(this.mParams);
    }

    public final String toString() {
        String strValueOf = String.valueOf(Arrays.toString(this.mParams));
        return new StringBuilder(String.valueOf(strValueOf).length() + 24).append("[InterstitialAdPoolKey ").append(strValueOf).append("]").toString();
    }
}
