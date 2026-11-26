package com.google.android.gms.ads.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import com.google.android.gms.internal.zzafj;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzac {
    private Context mContext;
    private final Object mLock = new Object();

    public final void zza(Context context, zzaje zzajeVar, String str, @Nullable Runnable runnable) {
        zza(context, zzajeVar, true, null, str, null, runnable);
    }

    final void zza(Context context, zzaje zzajeVar, boolean z, @Nullable zzafj zzafjVar, String str, @Nullable String str2, @Nullable Runnable runnable) {
        boolean z2;
        if (zzafjVar == null) {
            z2 = true;
        } else {
            z2 = (((zzbs.zzbF().currentTimeMillis() - zzafjVar.zzhi()) > ((Long) zzbs.zzbL().zzd(zzmo.zzFQ)).longValue() ? 1 : ((zzbs.zzbF().currentTimeMillis() - zzafjVar.zzhi()) == ((Long) zzbs.zzbL().zzd(zzmo.zzFQ)).longValue() ? 0 : -1)) > 0) || !zzafjVar.zzhj();
        }
        if (z2) {
            if (context == null) {
                zzafr.zzaT("Context not provided to fetch application settings");
            } else if (TextUtils.isEmpty(str) && TextUtils.isEmpty(str2)) {
                zzafr.zzaT("App settings could not be fetched. Required parameters missing");
            } else {
                this.mContext = context;
                zzagz.zzZr.post(new zzae(this, zzbs.zzbz().zze(context, zzajeVar), new zzad(this, runnable), str, str2, z, context));
            }
        }
    }
}
