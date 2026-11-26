package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class zzll extends zzjs {
    final /* synthetic */ zzlj zzBr;

    private zzll(zzlj zzljVar) {
        this.zzBr = zzljVar;
    }

    @Override // com.google.android.gms.internal.zzjr
    public final String getMediationAdapterClassName() throws RemoteException {
        return null;
    }

    @Override // com.google.android.gms.internal.zzjr
    public final boolean isLoading() throws RemoteException {
        return false;
    }

    @Override // com.google.android.gms.internal.zzjr
    public final String zzaI() throws RemoteException {
        return null;
    }

    @Override // com.google.android.gms.internal.zzjr
    public final void zzc(zzir zzirVar) throws RemoteException {
        zzajc.e("This app is using a lightweight version of the Google Mobile Ads SDK that requires the latest Google Play services to be installed, but Google Play services is either missing or out of date.");
        zzaiy.zzaaH.post(new zzlm(this));
    }
}
