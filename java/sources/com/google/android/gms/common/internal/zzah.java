package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.ServiceConnection;
import android.os.IBinder;
import java.util.HashSet;
import java.util.Iterator;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
final class zzah implements ServiceConnection {
    private ComponentName zzaHO;
    private boolean zzaHU;
    private final zzaf zzaHV;
    private /* synthetic */ zzag zzaHW;
    private IBinder zzaHj;
    private final Set<ServiceConnection> zzaHT = new HashSet();
    private int mState = 2;

    public zzah(zzag zzagVar, zzaf zzafVar) {
        this.zzaHW = zzagVar;
        this.zzaHV = zzafVar;
    }

    public final IBinder getBinder() {
        return this.zzaHj;
    }

    public final ComponentName getComponentName() {
        return this.zzaHO;
    }

    public final int getState() {
        return this.mState;
    }

    public final boolean isBound() {
        return this.zzaHU;
    }

    @Override // android.content.ServiceConnection
    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        synchronized (this.zzaHW.zzaHP) {
            this.zzaHW.mHandler.removeMessages(1, this.zzaHV);
            this.zzaHj = iBinder;
            this.zzaHO = componentName;
            Iterator<ServiceConnection> it = this.zzaHT.iterator();
            while (it.hasNext()) {
                it.next().onServiceConnected(componentName, iBinder);
            }
            this.mState = 1;
        }
    }

    @Override // android.content.ServiceConnection
    public final void onServiceDisconnected(ComponentName componentName) {
        synchronized (this.zzaHW.zzaHP) {
            this.zzaHW.mHandler.removeMessages(1, this.zzaHV);
            this.zzaHj = null;
            this.zzaHO = componentName;
            Iterator<ServiceConnection> it = this.zzaHT.iterator();
            while (it.hasNext()) {
                it.next().onServiceDisconnected(componentName);
            }
            this.mState = 2;
        }
    }

    public final void zza(ServiceConnection serviceConnection, String str) {
        com.google.android.gms.common.stats.zza unused = this.zzaHW.zzaHQ;
        Context unused2 = this.zzaHW.mApplicationContext;
        this.zzaHV.zzrB();
        this.zzaHT.add(serviceConnection);
    }

    public final boolean zza(ServiceConnection serviceConnection) {
        return this.zzaHT.contains(serviceConnection);
    }

    public final void zzb(ServiceConnection serviceConnection, String str) {
        com.google.android.gms.common.stats.zza unused = this.zzaHW.zzaHQ;
        Context unused2 = this.zzaHW.mApplicationContext;
        this.zzaHT.remove(serviceConnection);
    }

    public final void zzcB(String str) {
        this.mState = 3;
        com.google.android.gms.common.stats.zza unused = this.zzaHW.zzaHQ;
        this.zzaHU = com.google.android.gms.common.stats.zza.zza(this.zzaHW.mApplicationContext, str, this.zzaHV.zzrB(), this, 129);
        if (this.zzaHU) {
            this.zzaHW.mHandler.sendMessageDelayed(this.zzaHW.mHandler.obtainMessage(1, this.zzaHV), this.zzaHW.zzaHS);
        } else {
            this.mState = 2;
            try {
                com.google.android.gms.common.stats.zza unused2 = this.zzaHW.zzaHQ;
                this.zzaHW.mApplicationContext.unbindService(this);
            } catch (IllegalArgumentException e) {
            }
        }
    }

    public final void zzcC(String str) {
        this.zzaHW.mHandler.removeMessages(1, this.zzaHV);
        com.google.android.gms.common.stats.zza unused = this.zzaHW.zzaHQ;
        this.zzaHW.mApplicationContext.unbindService(this);
        this.zzaHU = false;
        this.mState = 2;
    }

    public final boolean zzrC() {
        return this.zzaHT.isEmpty();
    }
}
