package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class ah implements Runnable {
    final /* synthetic */ String a;
    final /* synthetic */ Core b;

    ah(Core core, String str) {
        this.b = core;
        this.a = str;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.b.a(this.a);
    }
}
