package com.google.android.gms.common.internal;

import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.RemoteException;
import android.support.annotation.BinderThread;
import android.util.Log;
import com.google.android.gms.common.ConnectionResult;

/* JADX INFO: loaded from: classes.dex */
public final class zzn extends zze {
    private /* synthetic */ zzd zzaHe;
    private IBinder zzaHi;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    @BinderThread
    public zzn(zzd zzdVar, int i, IBinder iBinder, Bundle bundle) {
        super(zzdVar, i, bundle);
        this.zzaHe = zzdVar;
        this.zzaHi = iBinder;
    }

    @Override // com.google.android.gms.common.internal.zze
    protected final void zzj(ConnectionResult connectionResult) {
        if (this.zzaHe.zzaGW != null) {
            this.zzaHe.zzaGW.onConnectionFailed(connectionResult);
        }
        this.zzaHe.onConnectionFailed(connectionResult);
    }

    @Override // com.google.android.gms.common.internal.zze
    protected final boolean zzrj() {
        try {
            String interfaceDescriptor = this.zzaHi.getInterfaceDescriptor();
            if (!this.zzaHe.zzdc().equals(interfaceDescriptor)) {
                String strValueOf = String.valueOf(this.zzaHe.zzdc());
                Log.e("GmsClient", new StringBuilder(String.valueOf(strValueOf).length() + 34 + String.valueOf(interfaceDescriptor).length()).append("service descriptor mismatch: ").append(strValueOf).append(" vs. ").append(interfaceDescriptor).toString());
                return false;
            }
            IInterface iInterfaceZzd = this.zzaHe.zzd(this.zzaHi);
            if (iInterfaceZzd == null) {
                return false;
            }
            if (!this.zzaHe.zza(2, 4, iInterfaceZzd) && !this.zzaHe.zza(3, 4, iInterfaceZzd)) {
                return false;
            }
            this.zzaHe.zzaGZ = null;
            Bundle bundleZzoC = this.zzaHe.zzoC();
            if (this.zzaHe.zzaGV != null) {
                this.zzaHe.zzaGV.onConnected(bundleZzoC);
            }
            return true;
        } catch (RemoteException e) {
            Log.w("GmsClient", "service probably died");
            return false;
        }
    }
}
