package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class u implements Runnable {
    final /* synthetic */ String[] a;
    final /* synthetic */ Core b;

    u(Core core, String[] strArr) {
        this.b = core;
        this.a = strArr;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.b.al = this.a;
    }
}
