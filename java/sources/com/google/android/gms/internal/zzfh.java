package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import android.view.View;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.Map;
import java.util.WeakHashMap;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzfh implements zzfp {
    private final Context mApplicationContext;
    private final zzaje zztW;
    private final com.google.android.gms.ads.internal.js.zzl zzwM;
    private final Object mLock = new Object();
    private final WeakHashMap<zzaff, zzfi> zzwK = new WeakHashMap<>();
    private final ArrayList<zzfi> zzwL = new ArrayList<>();

    public zzfh(Context context, zzaje zzajeVar, com.google.android.gms.ads.internal.js.zzl zzlVar) {
        this.mApplicationContext = context.getApplicationContext();
        this.zztW = zzajeVar;
        this.zzwM = zzlVar;
    }

    private final boolean zzf(zzaff zzaffVar) {
        boolean z;
        synchronized (this.mLock) {
            zzfi zzfiVar = this.zzwK.get(zzaffVar);
            z = zzfiVar != null && zzfiVar.zzcs();
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzfp
    public final void zza(zzfi zzfiVar) {
        synchronized (this.mLock) {
            if (!zzfiVar.zzcs()) {
                this.zzwL.remove(zzfiVar);
                Iterator<Map.Entry<zzaff, zzfi>> it = this.zzwK.entrySet().iterator();
                while (it.hasNext()) {
                    if (it.next().getValue() == zzfiVar) {
                        it.remove();
                    }
                }
            }
        }
    }

    public final void zza(zziv zzivVar, zzaff zzaffVar) {
        zza(zzivVar, zzaffVar, zzaffVar.zzPg.getView());
    }

    public final void zza(zziv zzivVar, zzaff zzaffVar, View view) {
        zza(zzivVar, zzaffVar, new zzfo(view, zzaffVar), (com.google.android.gms.ads.internal.js.zzai) null);
    }

    public final void zza(zziv zzivVar, zzaff zzaffVar, View view, com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zza(zzivVar, zzaffVar, new zzfo(view, zzaffVar), zzaiVar);
    }

    public final void zza(zziv zzivVar, zzaff zzaffVar, zzgs zzgsVar, @Nullable com.google.android.gms.ads.internal.js.zzai zzaiVar) {
        zzfi zzfiVar;
        synchronized (this.mLock) {
            if (zzf(zzaffVar)) {
                zzfiVar = this.zzwK.get(zzaffVar);
            } else {
                zzfiVar = new zzfi(this.mApplicationContext, zzivVar, zzaffVar, this.zztW, zzgsVar);
                zzfiVar.zza(this);
                this.zzwK.put(zzaffVar, zzfiVar);
                this.zzwL.add(zzfiVar);
            }
            if (zzaiVar != null) {
                zzfiVar.zza(new zzfq(zzfiVar, zzaiVar));
            } else {
                zzfiVar.zza(new zzfu(zzfiVar, this.zzwM, this.mApplicationContext));
            }
        }
    }

    public final void zzg(zzaff zzaffVar) {
        synchronized (this.mLock) {
            zzfi zzfiVar = this.zzwK.get(zzaffVar);
            if (zzfiVar != null) {
                zzfiVar.zzcq();
            }
        }
    }

    public final void zzh(zzaff zzaffVar) {
        synchronized (this.mLock) {
            zzfi zzfiVar = this.zzwK.get(zzaffVar);
            if (zzfiVar != null) {
                zzfiVar.stop();
            }
        }
    }

    public final void zzi(zzaff zzaffVar) {
        synchronized (this.mLock) {
            zzfi zzfiVar = this.zzwK.get(zzaffVar);
            if (zzfiVar != null) {
                zzfiVar.pause();
            }
        }
    }

    public final void zzj(zzaff zzaffVar) {
        synchronized (this.mLock) {
            zzfi zzfiVar = this.zzwK.get(zzaffVar);
            if (zzfiVar != null) {
                zzfiVar.resume();
            }
        }
    }
}
