package com.google.android.gms.internal;

import android.content.Context;
import com.google.android.gms.ads.identifier.AdvertisingIdClient;
import com.google.android.gms.common.GooglePlayServicesNotAvailableException;
import com.google.android.gms.common.GooglePlayServicesRepairableException;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
final class zzafd implements Runnable {
    private /* synthetic */ zzajg zzXK;
    private /* synthetic */ Context zztF;

    zzafd(zzafc zzafcVar, Context context, zzajg zzajgVar) {
        this.zztF = context;
        this.zzXK = zzajgVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzXK.zzg(AdvertisingIdClient.getAdvertisingIdInfo(this.zztF));
        } catch (GooglePlayServicesNotAvailableException | GooglePlayServicesRepairableException | IOException | IllegalStateException e) {
            this.zzXK.zzb(e);
            zzajc.zzb("Exception while getting advertising Id info", e);
        }
    }
}
