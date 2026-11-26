package com.google.android.gms.internal;

import android.os.Bundle;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Result;
import java.util.Collections;
import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
public final class zzbco implements zzbcw {
    private final zzbcx zzaCZ;

    public zzbco(zzbcx zzbcxVar) {
        this.zzaCZ = zzbcxVar;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void begin() {
        Iterator<Api.zze> it = this.zzaCZ.zzaDF.values().iterator();
        while (it.hasNext()) {
            it.next().disconnect();
        }
        this.zzaCZ.zzaCl.zzaDG = Collections.emptySet();
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void connect() {
        this.zzaCZ.zzqh();
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final boolean disconnect() {
        return true;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void onConnected(Bundle bundle) {
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void onConnectionSuspended(int i) {
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(T t) {
        this.zzaCZ.zzaCl.zzaCJ.add(t);
        return t;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(T t) {
        throw new IllegalStateException("GoogleApiClient is not connected yet.");
    }
}
