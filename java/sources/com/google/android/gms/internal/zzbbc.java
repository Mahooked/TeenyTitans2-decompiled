package com.google.android.gms.internal;

import android.support.annotation.MainThread;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.api.GoogleApiActivity;

/* JADX INFO: loaded from: classes.dex */
final class zzbbc implements Runnable {
    private final zzbbb zzaBR;
    final /* synthetic */ zzbba zzaBS;

    zzbbc(zzbba zzbbaVar, zzbbb zzbbbVar) {
        this.zzaBS = zzbbaVar;
        this.zzaBR = zzbbbVar;
    }

    @Override // java.lang.Runnable
    @MainThread
    public final void run() {
        if (this.zzaBS.mStarted) {
            ConnectionResult connectionResultZzpz = this.zzaBR.zzpz();
            if (connectionResultZzpz.hasResolution()) {
                this.zzaBS.zzaEG.startActivityForResult(GoogleApiActivity.zza(this.zzaBS.getActivity(), connectionResultZzpz.getResolution(), this.zzaBR.zzpy(), false), 1);
                return;
            }
            if (this.zzaBS.zzaBd.isUserResolvableError(connectionResultZzpz.getErrorCode())) {
                this.zzaBS.zzaBd.zza(this.zzaBS.getActivity(), this.zzaBS.zzaEG, connectionResultZzpz.getErrorCode(), 2, this.zzaBS);
            } else if (connectionResultZzpz.getErrorCode() != 18) {
                this.zzaBS.zza(connectionResultZzpz, this.zzaBR.zzpy());
            } else {
                GoogleApiAvailability.zza(this.zzaBS.getActivity().getApplicationContext(), new zzbbd(this, GoogleApiAvailability.zza(this.zzaBS.getActivity(), this.zzaBS)));
            }
        }
    }
}
