package com.google.android.gms.internal;

import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
final class zzbcq implements com.google.android.gms.common.internal.zzad {
    private /* synthetic */ zzbcp zzaDN;

    zzbcq(zzbcp zzbcpVar) {
        this.zzaDN = zzbcpVar;
    }

    @Override // com.google.android.gms.common.internal.zzad
    public final boolean isConnected() {
        return this.zzaDN.isConnected();
    }

    @Override // com.google.android.gms.common.internal.zzad
    public final Bundle zzoC() {
        return null;
    }
}
