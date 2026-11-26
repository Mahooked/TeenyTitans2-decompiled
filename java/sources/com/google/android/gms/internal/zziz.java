package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.IBinder;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.support.annotation.VisibleForTesting;
import android.widget.FrameLayout;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zziz {
    private final Object mLock = new Object();
    private final zziq zzAA;
    private final zzip zzAB;
    private final zzli zzAC;
    private final zzqc zzAD;
    private final zzadh zzAE;
    private final zzww zzAF;
    private zzkh zzAz;

    @VisibleForTesting
    abstract class zza<T> {
        zza() {
        }

        @Nullable
        protected abstract T zza(zzkh zzkhVar) throws RemoteException;

        @Nullable
        protected abstract T zzdo() throws RemoteException;

        @Nullable
        protected final T zzdp() {
            zzkh zzkhVarZzdn = zziz.this.zzdn();
            if (zzkhVarZzdn == null) {
                zzajc.zzaT("ClientApi class cannot be loaded.");
                return null;
            }
            try {
                return zza(zzkhVarZzdn);
            } catch (RemoteException e) {
                zzajc.zzc("Cannot invoke local loader using ClientApi class", e);
                return null;
            }
        }

        @Nullable
        protected final T zzdq() {
            try {
                return zzdo();
            } catch (RemoteException e) {
                zzajc.zzc("Cannot invoke remote loader", e);
                return null;
            }
        }
    }

    public zziz(zziq zziqVar, zzip zzipVar, zzli zzliVar, zzqc zzqcVar, zzadh zzadhVar, zzww zzwwVar) {
        this.zzAA = zziqVar;
        this.zzAB = zzipVar;
        this.zzAC = zzliVar;
        this.zzAD = zzqcVar;
        this.zzAE = zzadhVar;
        this.zzAF = zzwwVar;
    }

    @VisibleForTesting
    static <T> T zza(Context context, boolean z, zza<T> zzaVar) {
        if (!z) {
            zzji.zzds();
            if (!zzaiy.zzX(context)) {
                zzajc.zzaC("Google Play Services is not available");
                z = true;
            }
        }
        zzji.zzds();
        int iZzQ = zzaiy.zzQ(context);
        zzji.zzds();
        if (iZzQ <= zzaiy.zzP(context) ? z : true) {
            T tZzdp = zzaVar.zzdp();
            return tZzdp == null ? zzaVar.zzdq() : tZzdp;
        }
        T tZzdq = zzaVar.zzdq();
        return tZzdq == null ? zzaVar.zzdp() : tZzdq;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static void zzb(Context context, String str) {
        Bundle bundle = new Bundle();
        bundle.putString("action", "no_ads_fallback");
        bundle.putString("flow", str);
        zzji.zzds().zza(context, (String) null, "gmob-apps", bundle, true);
    }

    @Nullable
    private static zzkh zzdm() {
        zzkh zzkhVarAsInterface;
        try {
            Object objNewInstance = zziz.class.getClassLoader().loadClass("com.google.android.gms.ads.internal.ClientApi").newInstance();
            if (objNewInstance instanceof IBinder) {
                zzkhVarAsInterface = zzki.asInterface((IBinder) objNewInstance);
            } else {
                zzajc.zzaT("ClientApi class is not an instance of IBinder");
                zzkhVarAsInterface = null;
            }
            return zzkhVarAsInterface;
        } catch (Exception e) {
            zzajc.zzc("Failed to instantiate ClientApi class.", e);
            return null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Nullable
    public final zzkh zzdn() {
        zzkh zzkhVar;
        synchronized (this.mLock) {
            if (this.zzAz == null) {
                this.zzAz = zzdm();
            }
            zzkhVar = this.zzAz;
        }
        return zzkhVar;
    }

    public final zzow zza(Context context, FrameLayout frameLayout, FrameLayout frameLayout2) {
        return (zzow) zza(context, false, (zza) new zzjf(this, frameLayout, frameLayout2, context));
    }

    public final zzju zzb(Context context, String str, zzuq zzuqVar) {
        return (zzju) zza(context, false, (zza) new zzjd(this, context, str, zzuqVar));
    }

    @Nullable
    public final zzwx zzb(Activity activity) {
        boolean booleanExtra = false;
        Intent intent = activity.getIntent();
        if (intent.hasExtra("com.google.android.gms.ads.internal.overlay.useClientJar")) {
            booleanExtra = intent.getBooleanExtra("com.google.android.gms.ads.internal.overlay.useClientJar", false);
        } else {
            zzajc.e("useClientJar flag not found in activity intent extras.");
        }
        return (zzwx) zza(activity, booleanExtra, new zzjh(this, activity));
    }
}
