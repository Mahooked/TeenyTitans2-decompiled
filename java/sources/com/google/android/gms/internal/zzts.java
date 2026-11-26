package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzts {
    private static final zztp zzKJ = zztp.zzeN();
    private static final float zzKK = ((Float) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEk)).floatValue();
    private static final long zzKL = ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEi)).longValue();
    private static final float zzKM = ((Float) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEl)).floatValue();
    private static final long zzKN = ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEj)).longValue();

    private static int zzb(long j, int i) {
        return (int) ((j >>> ((i % 16) * 4)) & 15);
    }

    static boolean zzeX() {
        int iZzb;
        int iZzb2 = Integer.MAX_VALUE;
        int iZzeU = zzKJ.zzeU();
        int iZzeV = zzKJ.zzeV();
        int iZzeS = zzKJ.zzeS() + zzKJ.zzeT();
        if (iZzeU >= 16 || zzKN == 0) {
            iZzb = zzKM != 0.0f ? ((int) (zzKM * iZzeU)) + 1 : Integer.MAX_VALUE;
        } else {
            iZzb = zzb(zzKN, iZzeU);
        }
        if (iZzeV <= iZzb) {
            if (iZzeU < 16 && zzKL != 0) {
                iZzb2 = zzb(zzKL, iZzeU);
            } else if (zzKK != 0.0f) {
                iZzb2 = (int) (zzKK * iZzeU);
            }
            if (iZzeS <= iZzb2) {
                return true;
            }
        }
        return false;
    }
}
