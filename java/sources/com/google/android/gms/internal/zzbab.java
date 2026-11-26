package com.google.android.gms.internal;

import android.content.Context;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Looper;
import com.google.android.gms.common.api.GoogleApiClient;

/* JADX INFO: loaded from: classes.dex */
public final class zzbab extends com.google.android.gms.common.internal.zzz<zzbaf> {
    public zzbab(Context context, Looper looper, com.google.android.gms.common.internal.zzq zzqVar, GoogleApiClient.ConnectionCallbacks connectionCallbacks, GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        super(context, looper, 40, zzqVar, connectionCallbacks, onConnectionFailedListener);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final /* synthetic */ IInterface zzd(IBinder iBinder) {
        if (iBinder == null) {
            return null;
        }
        IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.clearcut.internal.IClearcutLoggerService");
        return iInterfaceQueryLocalInterface instanceof zzbaf ? (zzbaf) iInterfaceQueryLocalInterface : new zzbag(iBinder);
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdb() {
        return "com.google.android.gms.clearcut.service.START";
    }

    @Override // com.google.android.gms.common.internal.zzd
    protected final String zzdc() {
        return "com.google.android.gms.clearcut.internal.IClearcutLoggerService";
    }
}
