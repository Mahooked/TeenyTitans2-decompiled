package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzzk implements Thread.UncaughtExceptionHandler {
    private /* synthetic */ zzzi zzSk;
    private /* synthetic */ Thread.UncaughtExceptionHandler zzSl;

    zzzk(zzzi zzziVar, Thread.UncaughtExceptionHandler uncaughtExceptionHandler) {
        this.zzSk = zzziVar;
        this.zzSl = uncaughtExceptionHandler;
    }

    @Override // java.lang.Thread.UncaughtExceptionHandler
    public final void uncaughtException(Thread thread, Throwable th) {
        try {
            this.zzSk.zza(thread, th);
        } catch (Throwable th2) {
            zzafr.e("AdMob exception reporter failed reporting the exception.");
        } finally {
            if (this.zzSl != null) {
                this.zzSl.uncaughtException(thread, th);
            }
        }
    }
}
