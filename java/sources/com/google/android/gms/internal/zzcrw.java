package com.google.android.gms.internal;

import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.safetynet.SafeBrowsingData;

/* JADX INFO: loaded from: classes.dex */
public interface zzcrw extends IInterface {
    void zzF(Status status) throws RemoteException;

    void zza(Status status, SafeBrowsingData safeBrowsingData) throws RemoteException;

    void zza(Status status, com.google.android.gms.safetynet.zza zzaVar) throws RemoteException;

    void zza(Status status, com.google.android.gms.safetynet.zzd zzdVar) throws RemoteException;

    void zza(Status status, com.google.android.gms.safetynet.zzf zzfVar) throws RemoteException;

    void zza(Status status, boolean z) throws RemoteException;

    void zzb(Status status, boolean z) throws RemoteException;

    void zzc(Status status, boolean z) throws RemoteException;

    void zzd(Status status, boolean z) throws RemoteException;

    void zzeG(String str) throws RemoteException;
}
