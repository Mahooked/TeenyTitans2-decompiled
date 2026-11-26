package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzhu implements zzgy {
    private /* synthetic */ zzhs zzzs;

    zzhu(zzhs zzhsVar) {
        this.zzzs = zzhsVar;
    }

    @Override // com.google.android.gms.internal.zzgy
    public final void zzf(boolean z) {
        if (z) {
            this.zzzs.connect();
        } else {
            this.zzzs.disconnect();
        }
    }
}
