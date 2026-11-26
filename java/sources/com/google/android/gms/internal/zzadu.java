package com.google.android.gms.internal;

import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzadu extends zzaeb {
    private volatile zzads zzWL;
    private volatile zzadt zzWM;
    private volatile zzadv zzWz;

    public zzadu(zzadt zzadtVar) {
        this.zzWM = zzadtVar;
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zza(IObjectWrapper iObjectWrapper, zzaee zzaeeVar) {
        if (this.zzWM != null) {
            this.zzWM.zzc(zzaeeVar);
        }
    }

    public final void zza(zzads zzadsVar) {
        this.zzWL = zzadsVar;
    }

    public final void zza(zzadv zzadvVar) {
        this.zzWz = zzadvVar;
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzc(IObjectWrapper iObjectWrapper, int i) {
        if (this.zzWL != null) {
            this.zzWL.zzv(i);
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzd(IObjectWrapper iObjectWrapper, int i) {
        if (this.zzWz != null) {
            this.zzWz.zza(com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper).getClass().getName(), i);
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzq(IObjectWrapper iObjectWrapper) {
        if (this.zzWL != null) {
            this.zzWL.zzgT();
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzr(IObjectWrapper iObjectWrapper) {
        if (this.zzWz != null) {
            this.zzWz.zzaw(com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper).getClass().getName());
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzs(IObjectWrapper iObjectWrapper) {
        if (this.zzWM != null) {
            this.zzWM.onRewardedVideoAdOpened();
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzt(IObjectWrapper iObjectWrapper) {
        if (this.zzWM != null) {
            this.zzWM.onRewardedVideoStarted();
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzu(IObjectWrapper iObjectWrapper) {
        if (this.zzWM != null) {
            this.zzWM.onRewardedVideoAdClosed();
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzv(IObjectWrapper iObjectWrapper) {
        if (this.zzWM != null) {
            this.zzWM.zzgQ();
        }
    }

    @Override // com.google.android.gms.internal.zzaea
    public final void zzw(IObjectWrapper iObjectWrapper) {
        if (this.zzWM != null) {
            this.zzWM.onRewardedVideoAdLeftApplication();
        }
    }
}
