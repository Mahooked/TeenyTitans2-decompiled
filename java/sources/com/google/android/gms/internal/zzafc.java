package com.google.android.gms.internal;

import android.content.Context;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzafc implements zzafe {
    @Override // com.google.android.gms.internal.zzafe
    public final zzajm<String> zze(zzaae zzaaeVar) {
        return new zzajh(zzaaeVar.zzSB);
    }

    @Override // com.google.android.gms.internal.zzafe
    public final zzajm<AdvertisingIdClient.Info> zzz(Context context) {
        zzajg zzajgVar = new zzajg();
        zzji.zzds();
        if (zzaiy.zzY(context)) {
            zzagt.zza(new zzafd(this, context, zzajgVar));
        }
        return zzajgVar;
    }
}
