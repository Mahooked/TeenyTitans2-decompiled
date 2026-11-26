package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.IInterface;
import android.os.RemoteException;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public interface zzut extends IInterface {
    void destroy() throws RemoteException;

    Bundle getInterstitialAdapterInfo() throws RemoteException;

    zzks getVideoController() throws RemoteException;

    IObjectWrapper getView() throws RemoteException;

    boolean isInitialized() throws RemoteException;

    void pause() throws RemoteException;

    void resume() throws RemoteException;

    void setImmersiveMode(boolean z) throws RemoteException;

    void showInterstitial() throws RemoteException;

    void showVideo() throws RemoteException;

    void zza(IObjectWrapper iObjectWrapper, zzaea zzaeaVar, List<String> list) throws RemoteException;

    void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzaea zzaeaVar, String str2) throws RemoteException;

    void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException;

    void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException;

    void zza(IObjectWrapper iObjectWrapper, zzir zzirVar, String str, String str2, zzuw zzuwVar, zzon zzonVar, List<String> list) throws RemoteException;

    void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, zzuw zzuwVar) throws RemoteException;

    void zza(IObjectWrapper iObjectWrapper, zziv zzivVar, zzir zzirVar, String str, String str2, zzuw zzuwVar) throws RemoteException;

    void zza(zzir zzirVar, String str, String str2) throws RemoteException;

    void zzc(zzir zzirVar, String str) throws RemoteException;

    zzvc zzfq() throws RemoteException;

    zzvf zzfr() throws RemoteException;

    Bundle zzfs() throws RemoteException;

    Bundle zzft() throws RemoteException;

    boolean zzfu() throws RemoteException;

    zzpj zzfv() throws RemoteException;

    void zzk(IObjectWrapper iObjectWrapper) throws RemoteException;
}
