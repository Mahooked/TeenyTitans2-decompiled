package com.google.android.gms.internal;

import com.google.android.gms.ads.VideoController;

/* JADX INFO: loaded from: classes.dex */
public final class zzlw extends zzkw {
    private final VideoController.VideoLifecycleCallbacks zzse;

    public zzlw(VideoController.VideoLifecycleCallbacks videoLifecycleCallbacks) {
        this.zzse = videoLifecycleCallbacks;
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoEnd() {
        this.zzse.onVideoEnd();
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoMute(boolean z) {
        this.zzse.onVideoMute(z);
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoPause() {
        this.zzse.onVideoPause();
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoPlay() {
        this.zzse.onVideoPlay();
    }

    @Override // com.google.android.gms.internal.zzkv
    public final void onVideoStart() {
        this.zzse.onVideoStart();
    }
}
