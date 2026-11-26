package com.google.android.gms.common.internal;

import android.os.IBinder;
import android.os.RemoteException;
import com.google.android.gms.internal.zzed;

/* JADX INFO: loaded from: classes.dex */
public final class zzaq extends zzed implements zzao {
    zzaq(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.common.internal.ICancelToken");
    }

    @Override // com.google.android.gms.common.internal.zzao
    public final void cancel() throws RemoteException {
        zzc(2, zzZ());
    }
}
