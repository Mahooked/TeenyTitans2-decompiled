package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzxx {
    public static zzahp zza(Context context, com.google.android.gms.ads.internal.zza zzaVar, zzafg zzafgVar, zzcu zzcuVar, @Nullable zzaka zzakaVar, zzuq zzuqVar, zzxy zzxyVar, zznb zznbVar) {
        zzahp zzyfVar;
        zzaai zzaaiVar = zzafgVar.zzXY;
        if (zzaaiVar.zzTo) {
            zzyfVar = new zzyd(context, zzafgVar, zzuqVar, zzxyVar, zznbVar, zzakaVar);
        } else if (zzaaiVar.zzAv || (zzaVar instanceof com.google.android.gms.ads.internal.zzbb)) {
            zzyfVar = (zzaaiVar.zzAv && (zzaVar instanceof com.google.android.gms.ads.internal.zzbb)) ? new zzyf(context, (com.google.android.gms.ads.internal.zzbb) zzaVar, zzafgVar, zzcuVar, zzxyVar, zznbVar) : new zzya(zzafgVar, zzxyVar);
        } else {
            zzyfVar = (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDm)).booleanValue() && com.google.android.gms.common.util.zzq.zzsc() && !com.google.android.gms.common.util.zzq.zzse() && zzakaVar != null && zzakaVar.zzam().zzAt) ? new zzyc(context, zzafgVar, zzakaVar, zzxyVar) : new zzxz(context, zzafgVar, zzakaVar, zzxyVar);
        }
        String strValueOf = String.valueOf(zzyfVar.getClass().getName());
        zzafr.zzaC(strValueOf.length() != 0 ? "AdRenderer: ".concat(strValueOf) : new String("AdRenderer: "));
        zzyfVar.zzgp();
        return zzyfVar;
    }
}
