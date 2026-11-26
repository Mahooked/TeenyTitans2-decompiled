package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class y implements Runnable {
    final /* synthetic */ Core a;

    y(Core core) {
        this.a = core;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.a.update();
    }
}
