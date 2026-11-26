package com.google.android.gms.dynamic;

import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.ViewGroup;
import android.widget.FrameLayout;

/* JADX INFO: loaded from: classes.dex */
final class zze implements zzi {
    private /* synthetic */ zza zzaSv;
    private /* synthetic */ FrameLayout zzaSx;
    private /* synthetic */ LayoutInflater zzaSy;
    private /* synthetic */ ViewGroup zzaSz;
    private /* synthetic */ Bundle zzxV;

    zze(zza zzaVar, FrameLayout frameLayout, LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        this.zzaSv = zzaVar;
        this.zzaSx = frameLayout;
        this.zzaSy = layoutInflater;
        this.zzaSz = viewGroup;
        this.zzxV = bundle;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final int getState() {
        return 2;
    }

    @Override // com.google.android.gms.dynamic.zzi
    public final void zzb(LifecycleDelegate lifecycleDelegate) {
        this.zzaSx.removeAllViews();
        this.zzaSx.addView(this.zzaSv.zzaSr.onCreateView(this.zzaSy, this.zzaSz, this.zzxV));
    }
}
