package com.google.android.gms.internal;

import android.content.Context;
import android.os.Binder;
import android.os.RemoteException;
import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzhs {

    @Nullable
    private Context mContext;

    @Nullable
    private zzhz zzzq;

    @Nullable
    private zzid zzzr;
    private final Runnable zzzp = new zzht(this);
    private final Object mLock = new Object();

    /* JADX INFO: Access modifiers changed from: private */
    public final void connect() {
        synchronized (this.mLock) {
            if (this.mContext == null || this.zzzq != null) {
                return;
            }
            this.zzzq = new zzhz(this.mContext, com.google.android.gms.ads.internal.zzbs.zzbP().zzie(), new zzhv(this), new zzhw(this));
            this.zzzq.zzrb();
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void disconnect() {
        synchronized (this.mLock) {
            if (this.zzzq == null) {
                return;
            }
            if (this.zzzq.isConnected() || this.zzzq.isConnecting()) {
                this.zzzq.disconnect();
            }
            this.zzzq = null;
            this.zzzr = null;
            Binder.flushPendingCommands();
        }
    }

    static /* synthetic */ zzhz zza(zzhs zzhsVar, zzhz zzhzVar) {
        zzhsVar.zzzq = null;
        return null;
    }

    public final void initialize(Context context) {
        if (context == null) {
            return;
        }
        synchronized (this.mLock) {
            if (this.mContext != null) {
                return;
            }
            this.mContext = context.getApplicationContext();
            if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGk)).booleanValue()) {
                connect();
            } else {
                if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGj)).booleanValue()) {
                    com.google.android.gms.ads.internal.zzbs.zzbC().zza(new zzhu(this));
                }
            }
        }
    }

    public final zzhx zza(zzia zziaVar) {
        zzhx zzhxVar;
        synchronized (this.mLock) {
            try {
                if (this.zzzr == null) {
                    zzhxVar = new zzhx();
                } else {
                    try {
                        zzhxVar = this.zzzr.zza(zziaVar);
                    } catch (RemoteException e) {
                        zzafr.zzb("Unable to call into cache service.", e);
                        zzhxVar = new zzhx();
                    }
                }
            } catch (Throwable th) {
                throw th;
            }
        }
        return zzhxVar;
    }

    public final void zzcX() {
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGl)).booleanValue()) {
            synchronized (this.mLock) {
                connect();
                com.google.android.gms.ads.internal.zzbs.zzbz();
                zzagz.zzZr.removeCallbacks(this.zzzp);
                com.google.android.gms.ads.internal.zzbs.zzbz();
                zzagz.zzZr.postDelayed(this.zzzp, ((Long) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzGm)).longValue());
            }
        }
    }
}
