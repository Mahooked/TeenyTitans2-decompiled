package com.google.android.gms.internal;

import com.google.android.gms.ads.AdListener;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zzjk extends AdListener {
    private final Object lock = new Object();
    private AdListener zzAQ;

    @Override // com.google.android.gms.ads.AdListener
    public void onAdClosed() {
        synchronized (this.lock) {
            if (this.zzAQ != null) {
                this.zzAQ.onAdClosed();
            }
        }
    }

    @Override // com.google.android.gms.ads.AdListener
    public void onAdFailedToLoad(int i) {
        synchronized (this.lock) {
            if (this.zzAQ != null) {
                this.zzAQ.onAdFailedToLoad(i);
            }
        }
    }

    @Override // com.google.android.gms.ads.AdListener
    public void onAdLeftApplication() {
        synchronized (this.lock) {
            if (this.zzAQ != null) {
                this.zzAQ.onAdLeftApplication();
            }
        }
    }

    @Override // com.google.android.gms.ads.AdListener
    public void onAdLoaded() {
        synchronized (this.lock) {
            if (this.zzAQ != null) {
                this.zzAQ.onAdLoaded();
            }
        }
    }

    @Override // com.google.android.gms.ads.AdListener
    public void onAdOpened() {
        synchronized (this.lock) {
            if (this.zzAQ != null) {
                this.zzAQ.onAdOpened();
            }
        }
    }

    public final void zza(AdListener adListener) {
        synchronized (this.lock) {
            this.zzAQ = adListener;
        }
    }
}
