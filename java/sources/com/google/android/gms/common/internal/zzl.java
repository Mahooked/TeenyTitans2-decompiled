package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;

/* JADX INFO: loaded from: classes.dex */
public final class zzl implements ServiceConnection {
    private /* synthetic */ zzd zzaHe;
    private final int zzaHh;

    public zzl(zzd zzdVar, int i) {
        this.zzaHe = zzdVar;
        this.zzaHh = i;
    }

    @Override // android.content.ServiceConnection
    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        zzaw zzaxVar;
        if (iBinder == null) {
            this.zzaHe.zzaz(16);
            return;
        }
        synchronized (this.zzaHe.zzaGO) {
            zzd zzdVar = this.zzaHe;
            if (iBinder == null) {
                zzaxVar = null;
            } else {
                IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.IGmsServiceBroker");
                zzaxVar = (iInterfaceQueryLocalInterface == null || !(iInterfaceQueryLocalInterface instanceof zzaw)) ? new zzax(iBinder) : (zzaw) iInterfaceQueryLocalInterface;
            }
            zzdVar.zzaGP = zzaxVar;
        }
        this.zzaHe.zza(0, (Bundle) null, this.zzaHh);
    }

    @Override // android.content.ServiceConnection
    public final void onServiceDisconnected(ComponentName componentName) {
        synchronized (this.zzaHe.zzaGO) {
            this.zzaHe.zzaGP = null;
        }
        this.zzaHe.mHandler.sendMessage(this.zzaHe.mHandler.obtainMessage(6, this.zzaHh, 1));
    }
}
