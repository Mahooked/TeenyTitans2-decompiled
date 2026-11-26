package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzakq implements Runnable {
    private /* synthetic */ zzako zzacr;

    zzakq(zzako zzakoVar) {
        this.zzacr = zzakoVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        super/*android.webkit.WebView*/.destroy();
    }
}
