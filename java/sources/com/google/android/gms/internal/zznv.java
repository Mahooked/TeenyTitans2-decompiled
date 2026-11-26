package com.google.android.gms.internal;

import android.view.MotionEvent;
import com.google.android.gms.ads.formats.NativeCustomTemplateAd;

/* JADX INFO: loaded from: classes.dex */
final class zznv implements zznw {
    private /* synthetic */ zznu zzHU;

    zznv(zznu zznuVar) {
        this.zzHU = zznuVar;
    }

    @Override // com.google.android.gms.internal.zznw
    public final void zzc(MotionEvent motionEvent) {
    }

    @Override // com.google.android.gms.internal.zznw
    public final void zzeo() {
        this.zzHU.performClick(NativeCustomTemplateAd.ASSET_NAME_VIDEO);
    }
}
