package com.google.android.gms.internal;

import android.os.Bundle;
import android.support.annotation.Nullable;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
final class zzaen implements GoogleApiClient.ConnectionCallbacks {
    final /* synthetic */ zzajg zzXk;
    final /* synthetic */ Set zzXl;
    final /* synthetic */ zzael zzXm;

    zzaen(zzael zzaelVar, Set set, zzajg zzajgVar) {
        this.zzXm = zzaelVar;
        this.zzXl = set;
        this.zzXk = zzajgVar;
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnected(@Nullable Bundle bundle) {
        try {
            this.zzXm.zzXi.schedule(new zzaeo(this), ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGd)).longValue());
        } catch (IllegalStateException e) {
            String strValueOf = String.valueOf(e.getMessage());
            zzaes.zzaC(strValueOf.length() != 0 ? "Cannot schedule thread: ".concat(strValueOf) : new String("Cannot schedule thread: "));
        }
    }

    @Override // com.google.android.gms.common.api.GoogleApiClient.ConnectionCallbacks
    public final void onConnectionSuspended(int i) {
    }
}
