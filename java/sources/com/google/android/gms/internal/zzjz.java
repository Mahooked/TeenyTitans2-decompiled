package com.google.android.gms.internal;

import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
public interface zzjz extends IInterface {
    void destroy() throws RemoteException;

    String getAdUnitId() throws RemoteException;

    String getMediationAdapterClassName() throws RemoteException;

    zzks getVideoController() throws RemoteException;

    boolean isLoading() throws RemoteException;

    boolean isReady() throws RemoteException;

    void pause() throws RemoteException;

    void resume() throws RemoteException;

    void setImmersiveMode(boolean z) throws RemoteException;

    void setManualImpressionsEnabled(boolean z) throws RemoteException;

    void setUserId(String str) throws RemoteException;

    void showInterstitial() throws RemoteException;

    void stopLoading() throws RemoteException;

    void zza(zzadd zzaddVar) throws RemoteException;

    void zza(zziv zzivVar) throws RemoteException;

    void zza(zzjl zzjlVar) throws RemoteException;

    void zza(zzjo zzjoVar) throws RemoteException;

    void zza(zzke zzkeVar) throws RemoteException;

    void zza(zzkk zzkkVar) throws RemoteException;

    void zza(zzky zzkyVar) throws RemoteException;

    void zza(zzlx zzlxVar) throws RemoteException;

    void zza(zznh zznhVar) throws RemoteException;

    void zza(zzxg zzxgVar) throws RemoteException;

    void zza(zzxo zzxoVar, String str) throws RemoteException;

    boolean zza(zzir zzirVar) throws RemoteException;

    String zzaI() throws RemoteException;

    IObjectWrapper zzal() throws RemoteException;

    zziv zzam() throws RemoteException;

    void zzao() throws RemoteException;

    zzke zzax() throws RemoteException;

    zzjo zzay() throws RemoteException;
}
