package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class ac implements Runnable {
    final /* synthetic */ Core a;

    ac(Core core) {
        this.a = core;
    }

    @Override // java.lang.Runnable
    public void run() {
        if (this.a.z()) {
            return;
        }
        this.a.z.getAndIncrement();
        this.a.n();
    }
}
