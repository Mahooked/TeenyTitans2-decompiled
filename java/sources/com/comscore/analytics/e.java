package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class e implements Runnable {
    final /* synthetic */ String a;
    final /* synthetic */ Core b;

    e(Core core, String str) {
        this.b = core;
        this.a = str;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.b.c(this.a);
    }
}
