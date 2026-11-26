package com.google.android.gms.internal;

import android.content.Context;
import android.os.Bundle;
import android.os.RemoteException;
import android.support.annotation.Nullable;
import android.view.View;
import com.google.android.gms.common.internal.zzbo;
import java.lang.ref.WeakReference;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznx extends zzoc {
    private Object mLock;

    @Nullable
    private zzvc zzHV;

    @Nullable
    private zzvf zzHW;
    private final zznz zzHX;

    @Nullable
    private zzny zzHY;
    private boolean zzHZ;

    private zznx(Context context, zznz zznzVar, zzcu zzcuVar, zzoa zzoaVar) {
        super(context, zznzVar, null, zzcuVar, null, zzoaVar, null, null);
        this.zzHZ = false;
        this.mLock = new Object();
        this.zzHX = zznzVar;
    }

    public zznx(Context context, zznz zznzVar, zzcu zzcuVar, zzvc zzvcVar, zzoa zzoaVar) {
        this(context, zznzVar, zzcuVar, zzoaVar);
        this.zzHV = zzvcVar;
    }

    public zznx(Context context, zznz zznzVar, zzcu zzcuVar, zzvf zzvfVar, zzoa zzoaVar) {
        this(context, zznzVar, zzcuVar, zzoaVar);
        this.zzHW = zzvfVar;
    }

    /* JADX WARN: Code restructure failed: missing block: B:31:0x001a, code lost:
    
        r0 = null;
     */
    @Override // com.google.android.gms.internal.zzoc, com.google.android.gms.internal.zzny
    @android.support.annotation.Nullable
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    public final android.view.View zza(android.view.View.OnClickListener r5, boolean r6) {
        /*
            r4 = this;
            r1 = 0
            java.lang.Object r2 = r4.mLock
            monitor-enter(r2)
            com.google.android.gms.internal.zzny r0 = r4.zzHY     // Catch: java.lang.Throwable -> L24
            if (r0 == 0) goto L10
            com.google.android.gms.internal.zzny r0 = r4.zzHY     // Catch: java.lang.Throwable -> L24
            android.view.View r0 = r0.zza(r5, r6)     // Catch: java.lang.Throwable -> L24
            monitor-exit(r2)     // Catch: java.lang.Throwable -> L24
        Lf:
            return r0
        L10:
            com.google.android.gms.internal.zzvc r0 = r4.zzHV     // Catch: java.lang.Throwable -> L24 android.os.RemoteException -> L32
            if (r0 == 0) goto L27
            com.google.android.gms.internal.zzvc r0 = r4.zzHV     // Catch: java.lang.Throwable -> L24 android.os.RemoteException -> L32
            com.google.android.gms.dynamic.IObjectWrapper r0 = r0.zzfw()     // Catch: java.lang.Throwable -> L24 android.os.RemoteException -> L32
        L1a:
            if (r0 == 0) goto L3a
            java.lang.Object r0 = com.google.android.gms.dynamic.zzn.zzE(r0)     // Catch: java.lang.Throwable -> L24
            android.view.View r0 = (android.view.View) r0     // Catch: java.lang.Throwable -> L24
            monitor-exit(r2)     // Catch: java.lang.Throwable -> L24
            goto Lf
        L24:
            r0 = move-exception
            monitor-exit(r2)     // Catch: java.lang.Throwable -> L24
            throw r0
        L27:
            com.google.android.gms.internal.zzvf r0 = r4.zzHW     // Catch: java.lang.Throwable -> L24 android.os.RemoteException -> L32
            if (r0 == 0) goto L38
            com.google.android.gms.internal.zzvf r0 = r4.zzHW     // Catch: java.lang.Throwable -> L24 android.os.RemoteException -> L32
            com.google.android.gms.dynamic.IObjectWrapper r0 = r0.zzfw()     // Catch: java.lang.Throwable -> L24 android.os.RemoteException -> L32
            goto L1a
        L32:
            r0 = move-exception
            java.lang.String r3 = "Failed to call getAdChoicesContent"
            com.google.android.gms.internal.zzafr.zzc(r3, r0)     // Catch: java.lang.Throwable -> L24
        L38:
            r0 = r1
            goto L1a
        L3a:
            monitor-exit(r2)     // Catch: java.lang.Throwable -> L24
            r0 = r1
            goto Lf
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zznx.zza(android.view.View$OnClickListener, boolean):android.view.View");
    }

    @Override // com.google.android.gms.internal.zzoc, com.google.android.gms.internal.zzny
    public final void zza(View view, Map<String, WeakReference<View>> map) {
        zzbo.zzcz("recordImpression must be called on the main UI thread.");
        synchronized (this.mLock) {
            this.zzId = true;
            if (this.zzHY != null) {
                this.zzHY.zza(view, map);
                this.zzHX.recordImpression();
            } else {
                try {
                    if (this.zzHV != null && !this.zzHV.getOverrideImpressionRecording()) {
                        this.zzHV.recordImpression();
                        this.zzHX.recordImpression();
                    } else if (this.zzHW != null && !this.zzHW.getOverrideImpressionRecording()) {
                        this.zzHW.recordImpression();
                        this.zzHX.recordImpression();
                    }
                } catch (RemoteException e) {
                    zzafr.zzc("Failed to call recordImpression", e);
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzoc, com.google.android.gms.internal.zzny
    public final void zza(View view, Map<String, WeakReference<View>> map, Bundle bundle, View view2) {
        zzbo.zzcz("performClick must be called on the main UI thread.");
        synchronized (this.mLock) {
            if (this.zzHY != null) {
                this.zzHY.zza(view, map, bundle, view2);
                this.zzHX.onAdClicked();
            } else {
                try {
                    if (this.zzHV != null && !this.zzHV.getOverrideClickHandling()) {
                        this.zzHV.zzl(com.google.android.gms.dynamic.zzn.zzw(view));
                        this.zzHX.onAdClicked();
                    }
                    if (this.zzHW != null && !this.zzHW.getOverrideClickHandling()) {
                        this.zzHW.zzl(com.google.android.gms.dynamic.zzn.zzw(view));
                        this.zzHX.onAdClicked();
                    }
                } catch (RemoteException e) {
                    zzafr.zzc("Failed to call performClick", e);
                }
            }
        }
    }

    @Override // com.google.android.gms.internal.zzoc
    public final void zza(View view, Map<String, WeakReference<View>> map, View.OnTouchListener onTouchListener, View.OnClickListener onClickListener) {
        synchronized (this.mLock) {
            this.zzHZ = true;
            try {
                if (this.zzHV != null) {
                    this.zzHV.zzm(com.google.android.gms.dynamic.zzn.zzw(view));
                } else if (this.zzHW != null) {
                    this.zzHW.zzm(com.google.android.gms.dynamic.zzn.zzw(view));
                }
            } catch (RemoteException e) {
                zzafr.zzc("Failed to call prepareAd", e);
            }
            this.zzHZ = false;
        }
    }

    @Override // com.google.android.gms.internal.zzoc, com.google.android.gms.internal.zzny
    public final void zzb(View view, Map<String, WeakReference<View>> map) {
        synchronized (this.mLock) {
            try {
                if (this.zzHV != null) {
                    this.zzHV.zzn(com.google.android.gms.dynamic.zzn.zzw(view));
                } else if (this.zzHW != null) {
                    this.zzHW.zzn(com.google.android.gms.dynamic.zzn.zzw(view));
                }
            } catch (RemoteException e) {
                zzafr.zzc("Failed to call untrackView", e);
            }
        }
    }

    public final void zzc(@Nullable zzny zznyVar) {
        synchronized (this.mLock) {
            this.zzHY = zznyVar;
        }
    }

    @Override // com.google.android.gms.internal.zzoc, com.google.android.gms.internal.zzny
    public final boolean zzep() {
        boolean zZzep;
        synchronized (this.mLock) {
            zZzep = this.zzHY != null ? this.zzHY.zzep() : this.zzHX.zzaP();
        }
        return zZzep;
    }

    public final boolean zzeq() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzHZ;
        }
        return z;
    }

    public final zzny zzer() {
        zzny zznyVar;
        synchronized (this.mLock) {
            zznyVar = this.zzHY;
        }
        return zznyVar;
    }

    @Override // com.google.android.gms.internal.zzoc
    @Nullable
    public final zzaka zzes() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzoc, com.google.android.gms.internal.zzny
    public final void zzet() {
    }
}
