package com.google.android.gms.internal;

import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public interface zzjr extends IInterface {
    String getMediationAdapterClassName() throws RemoteException;

    boolean isLoading() throws RemoteException;

    String zzaI() throws RemoteException;

    void zzc(zzir zzirVar) throws RemoteException;
}
