package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class ag implements Runnable {
    final /* synthetic */ Core a;

    ag(Core core) {
        this.a = core;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.a.a();
        this.a.l();
    }
}
