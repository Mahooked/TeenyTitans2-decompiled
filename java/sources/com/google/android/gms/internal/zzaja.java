package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzaja extends Thread {
    private /* synthetic */ String zzsD;

    zzaja(zzaiz zzaizVar, String str) {
        this.zzsD = str;
    }

    @Override // java.lang.Thread, java.lang.Runnable
    public final void run() {
        new zzajd().zzaN(this.zzsD);
    }
}
