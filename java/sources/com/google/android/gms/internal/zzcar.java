package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.DeadObjectException;
import android.os.HandlerThread;
import android.support.v4.media.session.PlaybackStateCompat;
import com.google.android.gms.common.ConnectionResult;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;

/* JADX INFO: loaded from: classes.dex */
final class zzcar implements com.google.android.gms.common.internal.zzf, com.google.android.gms.common.internal.zzg {
    private final String packageName;
    private zzcas zzbfo;
    private final String zzbfp;
    private final LinkedBlockingQueue<zzax> zzbfq;
    private final HandlerThread zzbfr = new HandlerThread("GassClient");

    public zzcar(Context context, String str, String str2) {
        this.packageName = str;
        this.zzbfp = str2;
        this.zzbfr.start();
        this.zzbfo = new zzcas(context, this.zzbfr.getLooper(), this, this);
        this.zzbfq = new LinkedBlockingQueue<>();
        this.zzbfo.zzrb();
    }

    private final void zzgA() {
        if (this.zzbfo != null) {
            if (this.zzbfo.isConnected() || this.zzbfo.isConnecting()) {
                this.zzbfo.disconnect();
            }
        }
    }

    private final zzcax zzvx() {
        try {
            return this.zzbfo.zzvz();
        } catch (DeadObjectException | IllegalStateException e) {
            return null;
        }
    }

    private static zzax zzvy() {
        zzax zzaxVar = new zzax();
        zzaxVar.zzbq = Long.valueOf(PlaybackStateCompat.ACTION_PREPARE_FROM_MEDIA_ID);
        return zzaxVar;
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnected(Bundle bundle) {
        zzcax zzcaxVarZzvx = zzvx();
        try {
            if (zzcaxVarZzvx != null) {
                try {
                    this.zzbfq.put(zzcaxVarZzvx.zza(new zzcat(this.packageName, this.zzbfp)).zzvA());
                } catch (Throwable th) {
                    try {
                        this.zzbfq.put(zzvy());
                    } catch (InterruptedException e) {
                    }
                    zzgA();
                    this.zzbfr.quit();
                }
            }
        } finally {
            zzgA();
            this.zzbfr.quit();
        }
    }

    @Override // com.google.android.gms.common.internal.zzg
    public final void onConnectionFailed(ConnectionResult connectionResult) {
        try {
            this.zzbfq.put(zzvy());
        } catch (InterruptedException e) {
        }
    }

    @Override // com.google.android.gms.common.internal.zzf
    public final void onConnectionSuspended(int i) {
        try {
            this.zzbfq.put(zzvy());
        } catch (InterruptedException e) {
        }
    }

    public final zzax zzbe(int i) {
        zzax zzaxVarPoll;
        try {
            zzaxVarPoll = this.zzbfq.poll(5000L, TimeUnit.MILLISECONDS);
        } catch (InterruptedException e) {
            zzaxVarPoll = null;
        }
        return zzaxVarPoll == null ? zzvy() : zzaxVarPoll;
    }
}
