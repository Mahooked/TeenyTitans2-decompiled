package com.google.android.gms.internal;

import com.google.android.gms.common.data.DataHolder;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzbbx<L> implements zzbdz<L> {
    private final DataHolder zzaCX;

    protected zzbbx(DataHolder dataHolder) {
        this.zzaCX = dataHolder;
    }

    protected abstract void zza(L l, DataHolder dataHolder);

    @Override // com.google.android.gms.internal.zzbdz
    public final void zzpT() {
        if (this.zzaCX != null) {
            this.zzaCX.close();
        }
    }

    @Override // com.google.android.gms.internal.zzbdz
    public final void zzq(L l) {
        zza(l, this.zzaCX);
    }
}
