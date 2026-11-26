package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.text.TextUtils;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzmt {
    @Nullable
    public static zzmr zza(@Nullable zzmq zzmqVar) {
        if (!zzmqVar.zzdL()) {
            zzafr.v("CsiReporterFactory: CSI is not enabled. No CSI reporter created.");
            return null;
        }
        if (zzmqVar.getContext() == null) {
            throw new IllegalArgumentException("Context can't be null. Please set up context in CsiConfiguration.");
        }
        if (TextUtils.isEmpty(zzmqVar.zzck())) {
            throw new IllegalArgumentException("AfmaVersion can't be null or empty. Please set up afmaVersion in CsiConfiguration.");
        }
        return new zzmr(zzmqVar.getContext(), zzmqVar.zzck(), zzmqVar.zzdM(), zzmqVar.zzdN());
    }
}
