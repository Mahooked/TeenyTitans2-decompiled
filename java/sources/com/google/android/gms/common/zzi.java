package com.google.android.gms.common;

import java.lang.ref.WeakReference;

/* JADX INFO: loaded from: classes.dex */
abstract class zzi extends zzg {
    private static final WeakReference<byte[]> zzaAj = new WeakReference<>(null);
    private WeakReference<byte[]> zzaAi;

    zzi(byte[] bArr) {
        super(bArr);
        this.zzaAi = zzaAj;
    }

    @Override // com.google.android.gms.common.zzg
    final byte[] getBytes() {
        byte[] bArrZzpa;
        synchronized (this) {
            bArrZzpa = this.zzaAi.get();
            if (bArrZzpa == null) {
                bArrZzpa = zzpa();
                this.zzaAi = new WeakReference<>(bArrZzpa);
            }
        }
        return bArrZzpa;
    }

    protected abstract byte[] zzpa();
}
