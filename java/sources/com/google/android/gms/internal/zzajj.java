package com.google.android.gms.internal;

import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;

/* JADX INFO: loaded from: classes.dex */
final class zzajj implements Runnable {
    private /* synthetic */ zzajg zzaaV;
    private /* synthetic */ zzajl zzaaW;
    private /* synthetic */ zzajm zzaaX;

    zzajj(zzajg zzajgVar, zzajl zzajlVar, zzajm zzajmVar) {
        this.zzaaV = zzajgVar;
        this.zzaaW = zzajlVar;
        this.zzaaX = zzajmVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            this.zzaaV.zzg(this.zzaaW.apply(this.zzaaX.get()));
        } catch (InterruptedException | CancellationException | ExecutionException e) {
            this.zzaaV.cancel(true);
        }
    }
}
