package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.view.View;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznc extends zznf {
    private final com.google.android.gms.ads.internal.zzag zzHe;

    @Nullable
    private final String zzHf;
    private final String zzHg;

    public zznc(com.google.android.gms.ads.internal.zzag zzagVar, @Nullable String str, String str2) {
        this.zzHe = zzagVar;
        this.zzHf = str;
        this.zzHg = str2;
    }

    @Override // com.google.android.gms.internal.zzne
    public final String getContent() {
        return this.zzHg;
    }

    @Override // com.google.android.gms.internal.zzne
    public final void recordClick() {
        this.zzHe.zzaL();
    }

    @Override // com.google.android.gms.internal.zzne
    public final void recordImpression() {
        this.zzHe.zzaM();
    }

    @Override // com.google.android.gms.internal.zzne
    public final String zzdX() {
        return this.zzHf;
    }

    @Override // com.google.android.gms.internal.zzne
    public final void zzi(@Nullable IObjectWrapper iObjectWrapper) {
        if (iObjectWrapper == null) {
            return;
        }
        this.zzHe.zzc((View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }
}
