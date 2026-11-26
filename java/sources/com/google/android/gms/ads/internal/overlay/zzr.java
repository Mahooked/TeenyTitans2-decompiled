package com.google.android.gms.ads.internal.overlay;

import android.content.Context;
import android.view.ViewGroup;
import android.view.ViewParent;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzr {
    public final int index;
    public final ViewGroup parent;
    public final ViewGroup.LayoutParams zzPa;
    public final Context zzqD;

    public zzr(zzaka zzakaVar) throws zzp {
        this.zzPa = zzakaVar.getLayoutParams();
        ViewParent parent = zzakaVar.getParent();
        this.zzqD = zzakaVar.zzit();
        if (parent == null || !(parent instanceof ViewGroup)) {
            throw new zzp("Could not get the parent of the WebView for an overlay.");
        }
        this.parent = (ViewGroup) parent;
        this.index = this.parent.indexOfChild(zzakaVar.getView());
        this.parent.removeView(zzakaVar.getView());
        zzakaVar.zzA(true);
    }
}
