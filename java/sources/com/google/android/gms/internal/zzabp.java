package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzabp implements zzajq {
    private /* synthetic */ zzabn zzUE;

    zzabp(zzabn zzabnVar) {
        this.zzUE = zzabnVar;
    }

    @Override // com.google.android.gms.internal.zzajq
    public final void run() {
        zzafr.e("JS engine could not be obtained. Cancelling ad request");
        this.zzUE.zzUA.fail();
    }
}
