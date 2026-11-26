package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class x implements Runnable {
    final /* synthetic */ int a;
    final /* synthetic */ boolean b;
    final /* synthetic */ Core c;

    x(Core core, int i, boolean z) {
        this.c = core;
        this.a = i;
        this.b = z;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.c.a(this.a, this.b);
    }
}
