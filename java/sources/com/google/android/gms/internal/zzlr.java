package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public final class zzlr extends zzacz {
    private zzadd zzBu;

    @Override // com.google.android.gms.internal.zzacy
    public final void destroy() throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final String getMediationAdapterClassName() throws RemoteException {
        return null;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final boolean isLoaded() throws RemoteException {
        return false;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void pause() throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void resume() throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void setImmersiveMode(boolean z) throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void setUserId(String str) throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void show() throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zza(zzadd zzaddVar) throws RemoteException {
        this.zzBu = zzaddVar;
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zza(zzadj zzadjVar) throws RemoteException {
        zzajc.e("This app is using a lightweight version of the Google Mobile Ads SDK that requires the latest Google Play services to be installed, but Google Play services is either missing or out of date.");
        zzaiy.zzaaH.post(new zzls(this));
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzf(IObjectWrapper iObjectWrapper) throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzg(IObjectWrapper iObjectWrapper) throws RemoteException {
    }

    @Override // com.google.android.gms.internal.zzacy
    public final void zzh(IObjectWrapper iObjectWrapper) throws RemoteException {
    }
}
