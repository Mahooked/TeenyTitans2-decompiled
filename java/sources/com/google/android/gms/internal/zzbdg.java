package com.google.android.gms.internal;

import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
final class zzbdg implements Runnable {
    private /* synthetic */ zzbdd zzaEv;
    private /* synthetic */ ConnectionResult zzaEw;

    zzbdg(zzbdd zzbddVar, ConnectionResult connectionResult) {
        this.zzaEv = zzbddVar;
        this.zzaEw = connectionResult;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzaEv.onConnectionFailed(this.zzaEw);
    }
}
