package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
final class zzzj implements Thread.UncaughtExceptionHandler {
    private /* synthetic */ Thread.UncaughtExceptionHandler zzSj;
    private /* synthetic */ zzzi zzSk;

    zzzj(zzzi zzziVar, Thread.UncaughtExceptionHandler uncaughtExceptionHandler) {
        this.zzSk = zzziVar;
        this.zzSj = uncaughtExceptionHandler;
    }

    @Override // java.lang.Thread.UncaughtExceptionHandler
    public final void uncaughtException(Thread thread, Throwable th) {
        try {
            this.zzSk.zza(thread, th);
        } catch (Throwable th2) {
            zzafr.e("AdMob exception reporter failed reporting the exception.");
        } finally {
            if (this.zzSj != null) {
                this.zzSj.uncaughtException(thread, th);
            }
        }
    }
}
