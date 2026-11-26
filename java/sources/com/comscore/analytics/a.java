package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class a implements Runnable {
    final /* synthetic */ Core a;

    a(Core core) {
        this.a = core;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.a.am.resetVisitorId();
    }
}
