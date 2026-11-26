package com.google.android.gms.internal;

import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.Api;
import com.google.android.gms.common.api.Scope;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
final class zzbdh implements com.google.android.gms.common.internal.zzj, zzbel {
    private final zzbat<?> zzaAK;
    private final Api.zze zzaCy;
    final /* synthetic */ zzbdb zzaEm;
    private com.google.android.gms.common.internal.zzal zzaDl = null;
    private Set<Scope> zzame = null;
    private boolean zzaEx = false;

    public zzbdh(zzbdb zzbdbVar, Api.zze zzeVar, zzbat<?> zzbatVar) {
        this.zzaEm = zzbdbVar;
        this.zzaCy = zzeVar;
        this.zzaAK = zzbatVar;
    }

    static /* synthetic */ boolean zza(zzbdh zzbdhVar, boolean z) {
        zzbdhVar.zzaEx = true;
        return true;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @WorkerThread
    public final void zzqz() {
        if (!this.zzaEx || this.zzaDl == null) {
            return;
        }
        this.zzaCy.zza(this.zzaDl, this.zzame);
    }

    @Override // com.google.android.gms.internal.zzbel
    @WorkerThread
    public final void zzb(com.google.android.gms.common.internal.zzal zzalVar, Set<Scope> set) {
        if (zzalVar == null || set == null) {
            Log.wtf("GoogleApiManager", "Received null response from onSignInSuccess", new Exception());
            zzh(new ConnectionResult(4));
        } else {
            this.zzaDl = zzalVar;
            this.zzame = set;
            zzqz();
        }
    }

    @Override // com.google.android.gms.common.internal.zzj
    public final void zzf(@NonNull ConnectionResult connectionResult) {
        this.zzaEm.mHandler.post(new zzbdi(this, connectionResult));
    }

    @Override // com.google.android.gms.internal.zzbel
    @WorkerThread
    public final void zzh(ConnectionResult connectionResult) {
        ((zzbdd) this.zzaEm.zzaCB.get(this.zzaAK)).zzh(connectionResult);
    }
}
