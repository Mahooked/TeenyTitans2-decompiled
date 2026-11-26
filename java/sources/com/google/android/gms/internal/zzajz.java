package com.google.android.gms.internal;

import android.content.Context;
import android.view.ViewGroup;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzajz {
    private final Context mContext;
    private final zzaka zzJH;
    private com.google.android.gms.ads.internal.overlay.zzaa zzQK;
    private final ViewGroup zzabm;

    public zzajz(Context context, ViewGroup viewGroup, zzaka zzakaVar) {
        this(context, viewGroup, zzakaVar, null);
    }

    private zzajz(Context context, ViewGroup viewGroup, zzaka zzakaVar, com.google.android.gms.ads.internal.overlay.zzaa zzaaVar) {
        this.mContext = context;
        this.zzabm = viewGroup;
        this.zzJH = zzakaVar;
        this.zzQK = null;
    }

    public void onDestroy() {
        zzbo.zzcz("onDestroy must be called from the UI thread.");
        if (this.zzQK != null) {
            this.zzQK.destroy();
            this.zzabm.removeView(this.zzQK);
            this.zzQK = null;
        }
    }

    public void onPause() {
        zzbo.zzcz("onPause must be called from the UI thread.");
        if (this.zzQK != null) {
            this.zzQK.pause();
        }
    }

    public final void zza(int i, int i2, int i3, int i4, int i5, boolean z, com.google.android.gms.ads.internal.overlay.zzaq zzaqVar) {
        if (this.zzQK != null) {
            return;
        }
        zzmu.zza(this.zzJH.zziG().zzdR(), this.zzJH.zziF(), "vpr2");
        this.zzQK = new com.google.android.gms.ads.internal.overlay.zzaa(this.mContext, this.zzJH, i5, z, this.zzJH.zziG().zzdR(), zzaqVar);
        this.zzabm.addView(this.zzQK, 0, new ViewGroup.LayoutParams(-1, -1));
        this.zzQK.zzd(i, i2, i3, i4);
        this.zzJH.zziw().zzE(false);
    }

    public final void zze(int i, int i2, int i3, int i4) {
        zzbo.zzcz("The underlay may only be modified from the UI thread.");
        if (this.zzQK != null) {
            this.zzQK.zzd(i, i2, i3, i4);
        }
    }

    public final com.google.android.gms.ads.internal.overlay.zzaa zzip() {
        zzbo.zzcz("getAdVideoUnderlay must be called from the UI thread.");
        return this.zzQK;
    }
}
