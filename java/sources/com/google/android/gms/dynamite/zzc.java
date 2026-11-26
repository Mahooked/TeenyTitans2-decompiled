package com.google.android.gms.dynamite;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
final class zzc implements DynamiteModule.zzd {
    zzc() {
    }

    @Override // com.google.android.gms.dynamite.DynamiteModule.zzd
    public final zzi zza(Context context, String str, zzh zzhVar) throws DynamiteModule.zzc {
        zzi zziVar = new zzi();
        zziVar.zzaSU = zzhVar.zzE(context, str);
        if (zziVar.zzaSU != 0) {
            zziVar.zzaSW = -1;
        } else {
            zziVar.zzaSV = zzhVar.zzb(context, str, true);
            if (zziVar.zzaSV != 0) {
                zziVar.zzaSW = 1;
            }
        }
        return zziVar;
    }
}
