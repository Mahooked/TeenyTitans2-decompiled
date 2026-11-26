package com.google.android.gms.internal;

import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzbaa extends zzazy {
    private /* synthetic */ zzazz zzazN;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzbaa(zzazz zzazzVar) {
        super();
        this.zzazN = zzazzVar;
    }

    @Override // com.google.android.gms.internal.zzazy, com.google.android.gms.internal.zzbad
    public final void zzl(Status status) {
        this.zzazN.setResult(status);
    }
}
