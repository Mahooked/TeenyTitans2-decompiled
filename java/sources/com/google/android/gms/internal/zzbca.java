package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.DeadObjectException;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Result;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbo;
import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
public final class zzbca implements zzbcw {
    private final zzbcx zzaCZ;
    private boolean zzaDa = false;

    public zzbca(zzbcx zzbcxVar) {
        this.zzaCZ = zzbcxVar;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void begin() {
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void connect() {
        if (this.zzaDa) {
            this.zzaDa = false;
            this.zzaCZ.zza(new zzbcc(this, this));
        }
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final boolean disconnect() {
        if (this.zzaDa) {
            return false;
        }
        if (!this.zzaCZ.zzaCl.zzqf()) {
            this.zzaCZ.zzg(null);
            return true;
        }
        this.zzaDa = true;
        Iterator<zzbes> it = this.zzaCZ.zzaCl.zzaDK.iterator();
        while (it.hasNext()) {
            it.next().zzqK();
        }
        return false;
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void onConnected(Bundle bundle) {
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void onConnectionSuspended(int i) {
        this.zzaCZ.zzg(null);
        this.zzaCZ.zzaDY.zze(i, this.zzaDa);
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final void zza(ConnectionResult connectionResult, Api<?> api, boolean z) {
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final <A extends Api.zzb, R extends Result, T extends zzbay<R, A>> T zzd(T t) {
        return (T) zze(t);
    }

    @Override // com.google.android.gms.internal.zzbcw
    public final <A extends Api.zzb, T extends zzbay<? extends Result, A>> T zze(T t) {
        try {
            this.zzaCZ.zzaCl.zzaDL.zzb(t);
            zzbcp zzbcpVar = this.zzaCZ.zzaCl;
            Api.zze zzeVar = zzbcpVar.zzaDF.get(t.zzpd());
            zzbo.zzb(zzeVar, "Appropriate Api was not requested.");
            if (zzeVar.isConnected() || !this.zzaCZ.zzaDU.containsKey(t.zzpd())) {
                if (zzeVar instanceof com.google.android.gms.common.internal.zzbx) {
                    zzeVar = null;
                }
                t.zzb(zzeVar);
            } else {
                t.zzr(new Status(17));
            }
        } catch (DeadObjectException e) {
            this.zzaCZ.zza(new zzbcb(this, this));
        }
        return t;
    }

    final void zzpU() {
        if (this.zzaDa) {
            this.zzaDa = false;
            this.zzaCZ.zzaCl.zzaDL.release();
            disconnect();
        }
    }
}
