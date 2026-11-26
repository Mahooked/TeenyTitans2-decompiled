package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzna {
    private final Map<String, zzmz> zzGY = new HashMap();

    @Nullable
    private final zznb zzsK;

    public zzna(@Nullable zznb zznbVar) {
        this.zzsK = zznbVar;
    }

    public final void zza(String str, zzmz zzmzVar) {
        this.zzGY.put(str, zzmzVar);
    }

    public final void zza(String str, String str2, long j) {
        zznb zznbVar = this.zzsK;
        zzmz zzmzVar = this.zzGY.get(str2);
        String[] strArr = {str};
        if (zznbVar != null && zzmzVar != null) {
            zznbVar.zza(zzmzVar, j, strArr);
        }
        Map<String, zzmz> map = this.zzGY;
        zznb zznbVar2 = this.zzsK;
        map.put(str, zznbVar2 == null ? null : zznbVar2.zzc(j));
    }

    @Nullable
    public final zznb zzdR() {
        return this.zzsK;
    }
}
