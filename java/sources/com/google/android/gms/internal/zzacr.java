package com.google.android.gms.internal;

import android.content.Context;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzacr extends zzacz {
    private final Context mContext;
    private final Object mLock;
    private final zzacs zzWq;
    private final zzaje zztW;

    public zzacr(Context context, com.google.android.gms.ads.internal.zzv zzvVar, zzuq zzuqVar, zzaje zzajeVar) {
        this(context, zzajeVar, new zzacs(context, zzvVar, zziv.zzdk(), zzuqVar, zzajeVar));
    }

    private zzacr(Context context, zzaje zzajeVar, zzacs zzacsVar) {
        this.mLock = new Object();
        this.mContext = context;
        this.zztW = zzajeVar;
        this.zzWq = zzacsVar;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void destroy() {
        zzh(null);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final String getMediationAdapterClassName() {
        String mediationAdapterClassName;
        synchronized (this.mLock) {
            mediationAdapterClassName = this.zzWq.getMediationAdapterClassName();
        }
        return mediationAdapterClassName;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final boolean isLoaded() {
        boolean zIsLoaded;
        synchronized (this.mLock) {
            zIsLoaded = this.zzWq.isLoaded();
        }
        return zIsLoaded;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void pause() {
        zzf(null);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void resume() {
        zzg(null);
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void setImmersiveMode(boolean z) {
        synchronized (this.mLock) {
            this.zzWq.setImmersiveMode(z);
        }
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void setUserId(String str) {
        zzafr.zzaT("RewardedVideoAd.setUserId() is deprecated. Please do not call this method.");
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void show() {
        synchronized (this.mLock) {
            this.zzWq.zzgP();
        }
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zza(zzadd zzaddVar) {
        synchronized (this.mLock) {
            this.zzWq.zza(zzaddVar);
        }
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zza(zzadj zzadjVar) {
        synchronized (this.mLock) {
            this.zzWq.zza(zzadjVar);
        }
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzf(IObjectWrapper iObjectWrapper) {
        synchronized (this.mLock) {
            this.zzWq.pause();
        }
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzg(IObjectWrapper iObjectWrapper) {
        Context context;
        synchronized (this.mLock) {
            if (iObjectWrapper == null) {
                context = null;
            } else {
                try {
                    context = (Context) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
                } catch (Exception e) {
                    zzafr.zzc("Unable to extract updated context.", e);
                }
            }
            if (context != null) {
                this.zzWq.onContextChanged(context);
            }
            this.zzWq.resume();
        }
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzh(IObjectWrapper iObjectWrapper) {
        synchronized (this.mLock) {
            this.zzWq.destroy();
        }
    }
}
