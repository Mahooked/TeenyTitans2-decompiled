package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class n implements Runnable {
    final /* synthetic */ boolean a;
    final /* synthetic */ Core b;

    n(Core core, boolean z) {
        this.b = core;
        this.a = z;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.b.v = this.a;
    }
}
