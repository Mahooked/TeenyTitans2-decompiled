package com.google.android.gms.internal;

import android.support.annotation.Nullable;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzud extends zzux {
    private final Object mLock = new Object();
    private zzui zzMq;
    private zzuc zzMr;

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdClicked() {
        synchronized (this.mLock) {
            if (this.zzMr != null) {
                this.zzMr.zzaC();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdClosed() {
        synchronized (this.mLock) {
            if (this.zzMr != null) {
                this.zzMr.zzaD();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdFailedToLoad(int i) {
        synchronized (this.mLock) {
            if (this.zzMq != null) {
                this.zzMq.zzo(i == 3 ? 1 : 2);
                this.zzMq = null;
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdImpression() {
        synchronized (this.mLock) {
            if (this.zzMr != null) {
                this.zzMr.zzaH();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdLeftApplication() {
        synchronized (this.mLock) {
            if (this.zzMr != null) {
                this.zzMr.zzaE();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdLoaded() {
        synchronized (this.mLock) {
            if (this.zzMq != null) {
                this.zzMq.zzo(0);
                this.zzMq = null;
            } else {
                if (this.zzMr != null) {
                    this.zzMr.zzaG();
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAdOpened() {
        synchronized (this.mLock) {
            if (this.zzMr != null) {
                this.zzMr.zzaF();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void onAppEvent(String str, String str2) {
        synchronized (this.mLock) {
            if (this.zzMr != null) {
                this.zzMr.zze(str, str2);
            }
        }
    }

    public final void zza(@Nullable zzuc zzucVar) {
        synchronized (this.mLock) {
            this.zzMr = zzucVar;
        }
    }

    public final void zza(zzui zzuiVar) {
        synchronized (this.mLock) {
            this.zzMq = zzuiVar;
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void zza(zzuz zzuzVar) {
        synchronized (this.mLock) {
            if (this.zzMq != null) {
                this.zzMq.zza(0, zzuzVar);
                this.zzMq = null;
            } else {
                if (this.zzMr != null) {
                    this.zzMr.zzaG();
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzuw
    public final void zzb(zzpj zzpjVar, String str) {
        synchronized (this.mLock) {
            if (this.zzMr != null) {
                this.zzMr.zza(zzpjVar, str);
            }
        }
    }
}
