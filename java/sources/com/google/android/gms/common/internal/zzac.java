package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.os.Message;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApiClient;
import java.util.ArrayList;
import java.util.concurrent.atomic.AtomicInteger;

/* JADX INFO: loaded from: classes.dex */
public final class zzac implements Handler.Callback {
    private final Handler mHandler;
    private final zzad zzaHE;
    private final ArrayList<GoogleApiClient.ConnectionCallbacks> zzaHF = new ArrayList<>();
    private ArrayList<GoogleApiClient.ConnectionCallbacks> zzaHG = new ArrayList<>();
    private final ArrayList<GoogleApiClient.OnConnectionFailedListener> zzaHH = new ArrayList<>();
    private volatile boolean zzaHI = false;
    private final AtomicInteger zzaHJ = new AtomicInteger(0);
    private boolean zzaHK = false;
    private final Object mLock = new Object();

    public zzac(Looper looper, zzad zzadVar) {
        this.zzaHE = zzadVar;
        this.mHandler = new Handler(looper, this);
    }

    @Override // android.os.Handler.Callback
    public final boolean handleMessage(Message message) {
        if (message.what != 1) {
            Log.wtf("GmsClientEvents", new StringBuilder(45).append("Don't know how to handle message: ").append(message.what).toString(), new Exception());
            return false;
        }
        GoogleApiClient.ConnectionCallbacks connectionCallbacks = (GoogleApiClient.ConnectionCallbacks) message.obj;
        synchronized (this.mLock) {
            if (this.zzaHI && this.zzaHE.isConnected() && this.zzaHF.contains(connectionCallbacks)) {
                connectionCallbacks.onConnected(this.zzaHE.zzoC());
            }
        }
        return true;
    }

    public final boolean isConnectionCallbacksRegistered(GoogleApiClient.ConnectionCallbacks connectionCallbacks) {
        boolean zContains;
        zzbo.zzu(connectionCallbacks);
        synchronized (this.mLock) {
            zContains = this.zzaHF.contains(connectionCallbacks);
        }
        return zContains;
    }

    public final boolean isConnectionFailedListenerRegistered(GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        boolean zContains;
        zzbo.zzu(onConnectionFailedListener);
        synchronized (this.mLock) {
            zContains = this.zzaHH.contains(onConnectionFailedListener);
        }
        return zContains;
    }

    public final void registerConnectionCallbacks(GoogleApiClient.ConnectionCallbacks connectionCallbacks) {
        zzbo.zzu(connectionCallbacks);
        synchronized (this.mLock) {
            if (this.zzaHF.contains(connectionCallbacks)) {
                String strValueOf = String.valueOf(connectionCallbacks);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(strValueOf).length() + 62).append("registerConnectionCallbacks(): listener ").append(strValueOf).append(" is already registered").toString());
            } else {
                this.zzaHF.add(connectionCallbacks);
            }
        }
        if (this.zzaHE.isConnected()) {
            this.mHandler.sendMessage(this.mHandler.obtainMessage(1, connectionCallbacks));
        }
    }

    public final void registerConnectionFailedListener(GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        zzbo.zzu(onConnectionFailedListener);
        synchronized (this.mLock) {
            if (this.zzaHH.contains(onConnectionFailedListener)) {
                String strValueOf = String.valueOf(onConnectionFailedListener);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(strValueOf).length() + 67).append("registerConnectionFailedListener(): listener ").append(strValueOf).append(" is already registered").toString());
            } else {
                this.zzaHH.add(onConnectionFailedListener);
            }
        }
    }

    public final void unregisterConnectionCallbacks(GoogleApiClient.ConnectionCallbacks connectionCallbacks) {
        zzbo.zzu(connectionCallbacks);
        synchronized (this.mLock) {
            if (!this.zzaHF.remove(connectionCallbacks)) {
                String strValueOf = String.valueOf(connectionCallbacks);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(strValueOf).length() + 52).append("unregisterConnectionCallbacks(): listener ").append(strValueOf).append(" not found").toString());
            } else if (this.zzaHK) {
                this.zzaHG.add(connectionCallbacks);
            }
        }
    }

    public final void unregisterConnectionFailedListener(GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener) {
        zzbo.zzu(onConnectionFailedListener);
        synchronized (this.mLock) {
            if (!this.zzaHH.remove(onConnectionFailedListener)) {
                String strValueOf = String.valueOf(onConnectionFailedListener);
                Log.w("GmsClientEvents", new StringBuilder(String.valueOf(strValueOf).length() + 57).append("unregisterConnectionFailedListener(): listener ").append(strValueOf).append(" not found").toString());
            }
        }
    }

    public final void zzaA(int i) {
        int i2 = 0;
        zzbo.zza(Looper.myLooper() == this.mHandler.getLooper(), "onUnintentionalDisconnection must only be called on the Handler thread");
        this.mHandler.removeMessages(1);
        synchronized (this.mLock) {
            this.zzaHK = true;
            ArrayList arrayList = new ArrayList(this.zzaHF);
            int i3 = this.zzaHJ.get();
            ArrayList arrayList2 = arrayList;
            int size = arrayList2.size();
            while (i2 < size) {
                Object obj = arrayList2.get(i2);
                i2++;
                GoogleApiClient.ConnectionCallbacks connectionCallbacks = (GoogleApiClient.ConnectionCallbacks) obj;
                if (!this.zzaHI || this.zzaHJ.get() != i3) {
                    break;
                } else if (this.zzaHF.contains(connectionCallbacks)) {
                    connectionCallbacks.onConnectionSuspended(i);
                }
            }
            this.zzaHG.clear();
            this.zzaHK = false;
        }
    }

    public final void zzk(ConnectionResult connectionResult) {
        int i = 0;
        zzbo.zza(Looper.myLooper() == this.mHandler.getLooper(), "onConnectionFailure must only be called on the Handler thread");
        this.mHandler.removeMessages(1);
        synchronized (this.mLock) {
            ArrayList arrayList = new ArrayList(this.zzaHH);
            int i2 = this.zzaHJ.get();
            ArrayList arrayList2 = arrayList;
            int size = arrayList2.size();
            while (i < size) {
                Object obj = arrayList2.get(i);
                i++;
                GoogleApiClient.OnConnectionFailedListener onConnectionFailedListener = (GoogleApiClient.OnConnectionFailedListener) obj;
                if (!this.zzaHI || this.zzaHJ.get() != i2) {
                    return;
                }
                if (this.zzaHH.contains(onConnectionFailedListener)) {
                    onConnectionFailedListener.onConnectionFailed(connectionResult);
                }
            }
        }
    }

    public final void zzn(Bundle bundle) {
        int i = 0;
        zzbo.zza(Looper.myLooper() == this.mHandler.getLooper(), "onConnectionSuccess must only be called on the Handler thread");
        synchronized (this.mLock) {
            zzbo.zzae(!this.zzaHK);
            this.mHandler.removeMessages(1);
            this.zzaHK = true;
            zzbo.zzae(this.zzaHG.size() == 0);
            ArrayList arrayList = new ArrayList(this.zzaHF);
            int i2 = this.zzaHJ.get();
            ArrayList arrayList2 = arrayList;
            int size = arrayList2.size();
            while (i < size) {
                Object obj = arrayList2.get(i);
                i++;
                GoogleApiClient.ConnectionCallbacks connectionCallbacks = (GoogleApiClient.ConnectionCallbacks) obj;
                if (!this.zzaHI || !this.zzaHE.isConnected() || this.zzaHJ.get() != i2) {
                    break;
                } else if (!this.zzaHG.contains(connectionCallbacks)) {
                    connectionCallbacks.onConnected(bundle);
                }
            }
            this.zzaHG.clear();
            this.zzaHK = false;
        }
    }

    public final void zzrA() {
        this.zzaHI = true;
    }

    public final void zzrz() {
        this.zzaHI = false;
        this.zzaHJ.incrementAndGet();
    }
}
