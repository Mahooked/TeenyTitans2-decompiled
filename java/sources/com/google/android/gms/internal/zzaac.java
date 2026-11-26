package com.google.android.gms.internal;

import android.content.Context;
import android.os.Binder;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.support.annotation.NonNull;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaac extends zzzy implements com.google.android.gms.common.internal.zzf, com.google.android.gms.common.internal.zzg {
    private Context mContext;
    private final Object mLock;
    private zzajp<zzaae> zzSs;
    private final zzzw zzSt;
    private zzaad zzSw;
    private zzaje zztW;

    public zzaac(Context context, zzaje zzajeVar, zzajp<zzaae> zzajpVar, zzzw zzzwVar) {
        super(zzajpVar, zzzwVar);
        this.mLock = new Object();
        this.mContext = context;
        this.zztW = zzajeVar;
        this.zzSs = zzajpVar;
        this.zzSt = zzzwVar;
        this.zzSw = new zzaad(context, ((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCK)).booleanValue() ? com.google.android.gms.ads.internal.zzbs.zzbP().zzie() : context.getMainLooper(), this, this, this.zztW.zzaaP);
        this.zzSw.zzrb();
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnected(Bundle bundle) {
        zzgp();
    }

    @Override // com.google.android.gms.common.internal.zzg
    public final void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
        zzafr.zzaC("Cannot connect to remote service, fallback to local instance.");
        new zzaab(this.mContext, this.zzSs, this.zzSt).zzgp();
        Bundle bundle = new Bundle();
        bundle.putString("action", "gms_connection_failed_fallback_to_local");
        com.google.android.gms.ads.internal.zzbs.zzbz().zzb(this.mContext, this.zztW.zzaP, "gmob-apps", bundle, true);
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnectionSuspended(int i) {
        zzafr.zzaC("Disconnected from remote ad request service.");
    }

    @Override // com.google.android.gms.internal.zzzy
    public final void zzgA() {
        synchronized (this.mLock) {
            if (this.zzSw.isConnected() || this.zzSw.isConnecting()) {
                this.zzSw.disconnect();
            }
            Binder.flushPendingCommands();
        }
    }

    @Override // com.google.android.gms.internal.zzzy
    public final zzaam zzgB() {
        zzaam zzaamVarZzgC;
        synchronized (this.mLock) {
            try {
                zzaamVarZzgC = this.zzSw.zzgC();
            } catch (DeadObjectException | IllegalStateException e) {
                zzaamVarZzgC = null;
            }
        }
        return zzaamVarZzgC;
    }
}
