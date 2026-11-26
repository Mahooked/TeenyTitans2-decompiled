package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class w implements Runnable {
    final /* synthetic */ Core a;

    w(Core core) {
        this.a = core;
    }

    @Override // java.lang.Runnable
    public void run() {
        if (!this.a.z() && this.a.A.get() > 0) {
            this.a.A.getAndDecrement();
            this.a.n();
        }
    }
}
