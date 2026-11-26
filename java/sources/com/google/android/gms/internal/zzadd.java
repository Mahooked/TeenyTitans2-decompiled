package com.google.android.gms.internal;

import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public interface zzadd extends IInterface {
    void onRewardedVideoAdClosed() throws RemoteException;

    void onRewardedVideoAdFailedToLoad(int i) throws RemoteException;

    void onRewardedVideoAdLeftApplication() throws RemoteException;

    void onRewardedVideoAdLoaded() throws RemoteException;

    void onRewardedVideoAdOpened() throws RemoteException;

    void onRewardedVideoStarted() throws RemoteException;

    void zza(zzacv zzacvVar) throws RemoteException;
}
