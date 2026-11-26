package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class v implements Runnable {
    final /* synthetic */ Core a;

    v(Core core) {
        this.a = core;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.a.a.flush();
    }
}
