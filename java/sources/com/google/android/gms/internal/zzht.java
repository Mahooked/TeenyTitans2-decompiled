package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzht implements Runnable {
    private /* synthetic */ zzhs zzzs;

    zzht(zzhs zzhsVar) {
        this.zzzs = zzhsVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        this.zzzs.disconnect();
    }
}
