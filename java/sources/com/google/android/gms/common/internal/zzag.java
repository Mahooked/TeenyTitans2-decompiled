package com.google.android.gms.common.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.ServiceConnection;
import android.os.Handler;
import android.os.Message;
import android.support.v4.os.EnvironmentCompat;
import android.util.Log;
import com.comscore.utils.Constants;
import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
final class zzag extends zzae implements Handler.Callback {
    private final Context mApplicationContext;
    private final Handler mHandler;
    private final HashMap<zzaf, zzah> zzaHP = new HashMap<>();
    private final com.google.android.gms.common.stats.zza zzaHQ = com.google.android.gms.common.stats.zza.zzrU();
    private final long zzaHR = 5000;
    private final long zzaHS = Constants.USER_SESSION_INACTIVE_PERIOD;

    zzag(Context context) {
        this.mApplicationContext = context.getApplicationContext();
        this.mHandler = new Handler(context.getMainLooper(), this);
    }

    @Override // android.os.Handler.Callback
    public final boolean handleMessage(Message message) {
        switch (message.what) {
            case 0:
                synchronized (this.zzaHP) {
                    zzaf zzafVar = (zzaf) message.obj;
                    zzah zzahVar = this.zzaHP.get(zzafVar);
                    if (zzahVar != null && zzahVar.zzrC()) {
                        if (zzahVar.isBound()) {
                            zzahVar.zzcC("GmsClientSupervisor");
                        }
                        this.zzaHP.remove(zzafVar);
                    }
                    break;
                }
                return true;
            case 1:
                synchronized (this.zzaHP) {
                    zzaf zzafVar2 = (zzaf) message.obj;
                    zzah zzahVar2 = this.zzaHP.get(zzafVar2);
                    if (zzahVar2 != null && zzahVar2.getState() == 3) {
                        String strValueOf = String.valueOf(zzafVar2);
                        Log.wtf("GmsClientSupervisor", new StringBuilder(String.valueOf(strValueOf).length() + 47).append("Timeout waiting for ServiceConnection callback ").append(strValueOf).toString(), new Exception());
                        ComponentName componentName = zzahVar2.getComponentName();
                        if (componentName == null) {
                            componentName = zzafVar2.getComponentName();
                        }
                        zzahVar2.onServiceDisconnected(componentName == null ? new ComponentName(zzafVar2.getPackage(), EnvironmentCompat.MEDIA_UNKNOWN) : componentName);
                    }
                    break;
                }
                return true;
            default:
                return false;
        }
    }

    @Override // com.google.android.gms.common.internal.zzae
    protected final boolean zza(zzaf zzafVar, ServiceConnection serviceConnection, String str) {
        boolean zIsBound;
        zzbo.zzb(serviceConnection, "ServiceConnection must not be null");
        synchronized (this.zzaHP) {
            zzah zzahVar = this.zzaHP.get(zzafVar);
            if (zzahVar != null) {
                this.mHandler.removeMessages(0, zzafVar);
                if (!zzahVar.zza(serviceConnection)) {
                    zzahVar.zza(serviceConnection, str);
                    switch (zzahVar.getState()) {
                        case 1:
                            serviceConnection.onServiceConnected(zzahVar.getComponentName(), zzahVar.getBinder());
                            break;
                        case 2:
                            zzahVar.zzcB(str);
                            break;
                    }
                } else {
                    String strValueOf = String.valueOf(zzafVar);
                    throw new IllegalStateException(new StringBuilder(String.valueOf(strValueOf).length() + 81).append("Trying to bind a GmsServiceConnection that was already connected before.  config=").append(strValueOf).toString());
                }
            } else {
                zzahVar = new zzah(this, zzafVar);
                zzahVar.zza(serviceConnection, str);
                zzahVar.zzcB(str);
                this.zzaHP.put(zzafVar, zzahVar);
            }
            zIsBound = zzahVar.isBound();
        }
        return zIsBound;
    }

    @Override // com.google.android.gms.common.internal.zzae
    protected final void zzb(zzaf zzafVar, ServiceConnection serviceConnection, String str) {
        zzbo.zzb(serviceConnection, "ServiceConnection must not be null");
        synchronized (this.zzaHP) {
            zzah zzahVar = this.zzaHP.get(zzafVar);
            if (zzahVar == null) {
                String strValueOf = String.valueOf(zzafVar);
                throw new IllegalStateException(new StringBuilder(String.valueOf(strValueOf).length() + 50).append("Nonexistent connection status for service config: ").append(strValueOf).toString());
            }
            if (!zzahVar.zza(serviceConnection)) {
                String strValueOf2 = String.valueOf(zzafVar);
                throw new IllegalStateException(new StringBuilder(String.valueOf(strValueOf2).length() + 76).append("Trying to unbind a GmsServiceConnection  that was not bound before.  config=").append(strValueOf2).toString());
            }
            zzahVar.zzb(serviceConnection, str);
            if (zzahVar.zzrC()) {
                this.mHandler.sendMessageDelayed(this.mHandler.obtainMessage(0, zzafVar), this.zzaHR);
            }
        }
    }
}
