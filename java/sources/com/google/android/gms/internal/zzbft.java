package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.common.api.Status;

/* JADX INFO: loaded from: classes.dex */
final class zzbft extends zzbfn {
    private final zzbaz<Status> zzaIz;

    public zzbft(zzbaz<Status> zzbazVar) {
        this.zzaIz = zzbazVar;
    }

    @Override // com.google.android.gms.internal.zzbfn, com.google.android.gms.internal.zzbfx
    public final void zzaC(int i) throws RemoteException {
        this.zzaIz.setResult(new Status(i));
    }
}
