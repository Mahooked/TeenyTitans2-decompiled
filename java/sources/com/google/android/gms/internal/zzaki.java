package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzaki implements com.google.android.gms.ads.internal.overlay.zzw {
    private zzaka zzabK;
    private com.google.android.gms.ads.internal.overlay.zzw zzabq;

    public zzaki(zzaka zzakaVar, com.google.android.gms.ads.internal.overlay.zzw zzwVar) {
        this.zzabK = zzakaVar;
        this.zzabq = zzwVar;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public void onPause() {
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public void onResume() {
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public final void zzaA() {
        this.zzabq.zzaA();
        this.zzabK.zziq();
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzw
    public final void zzaB() {
        this.zzabq.zzaB();
        this.zzabK.zzfP();
    }
}
