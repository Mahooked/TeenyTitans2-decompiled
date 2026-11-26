package com.google.android.gms.internal;

import android.view.View;

/* JADX INFO: loaded from: classes.dex */
final class zzha implements Runnable {
    private /* synthetic */ View zzyI;
    private /* synthetic */ zzgz zzyJ;

    zzha(zzgz zzgzVar, View view) {
        this.zzyJ = zzgzVar;
        this.zzyI = view;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzyJ.zzf(this.zzyI);
    }
}
