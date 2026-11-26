package com.google.android.gms.internal;

import android.content.Context;
import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.dynamite.DynamiteModule;
import com.google.android.gms.dynamite.descriptors.com.google.android.gms.flags.ModuleDescriptor;

/* JADX INFO: loaded from: classes.dex */
public final class zzcab {
    private boolean zzuH = false;
    private zzcac zzaXG = null;

    public final void initialize(Context context) {
        synchronized (this) {
            if (this.zzuH) {
                return;
            }
            try {
                this.zzaXG = zzcad.asInterface(DynamiteModule.zza(context, DynamiteModule.zzaSP, ModuleDescriptor.MODULE_ID).zzcV("com.google.android.gms.flags.impl.FlagProviderImpl"));
                this.zzaXG.init(com.google.android.gms.dynamic.zzn.zzw(context));
                this.zzuH = true;
            } catch (RemoteException | DynamiteModule.zzc e) {
                Log.w("FlagValueProvider", "Failed to initialize flags module.", e);
            }
        }
    }

    public final <T> T zzb(zzbzu<T> zzbzuVar) {
        synchronized (this) {
            if (this.zzuH) {
                return zzbzuVar.zza(this.zzaXG);
            }
            return zzbzuVar.zzdI();
        }
    }
}
