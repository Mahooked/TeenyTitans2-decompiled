package com.comscore.analytics;

/* JADX INFO: loaded from: classes.dex */
class s implements Runnable {
    final /* synthetic */ int a;
    final /* synthetic */ Core b;

    s(Core core, int i) {
        this.b = core;
        this.a = i;
    }

    @Override // java.lang.Runnable
    public void run() {
        this.b.a.setCacheMeasurementExpiry(this.a);
    }
}
