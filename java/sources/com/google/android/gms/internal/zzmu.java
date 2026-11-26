package com.google.android.gms.internal;

import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzmu {
    public static boolean zza(@Nullable zznb zznbVar, @Nullable zzmz zzmzVar, String... strArr) {
        if (zznbVar == null || zzmzVar == null) {
            return false;
        }
        return zznbVar.zza(zzmzVar, strArr);
    }

    @Nullable
    public static zzmz zzb(@Nullable zznb zznbVar) {
        if (zznbVar == null) {
            return null;
        }
        return zznbVar.zzdS();
    }
}
