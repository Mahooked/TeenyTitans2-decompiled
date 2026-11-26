package com.google.android.gms.dynamite;

import android.content.Context;

/* JADX INFO: loaded from: classes.dex */
final class zze implements DynamiteModule.zzd {
    zze() {
    }

    @Override // com.google.android.gms.dynamite.DynamiteModule.zzd
    public final zzi zza(Context context, String str, zzh zzhVar) throws DynamiteModule.zzc {
        zzi zziVar = new zzi();
        zziVar.zzaSU = zzhVar.zzE(context, str);
        zziVar.zzaSV = zzhVar.zzb(context, str, true);
        if (zziVar.zzaSU == 0 && zziVar.zzaSV == 0) {
            zziVar.zzaSW = 0;
        } else if (zziVar.zzaSV >= zziVar.zzaSU) {
            zziVar.zzaSW = 1;
        } else {
            zziVar.zzaSW = -1;
        }
        return zziVar;
    }
}
