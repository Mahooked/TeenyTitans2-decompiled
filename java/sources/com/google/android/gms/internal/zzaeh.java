package com.google.android.gms.internal;

import android.graphics.Bitmap;
import java.io.ByteArrayOutputStream;

/* JADX INFO: loaded from: classes.dex */
final class zzaeh implements Runnable {
    private /* synthetic */ Bitmap val$bitmap;
    private /* synthetic */ zzaeg zzXf;

    zzaeh(zzaeg zzaegVar, Bitmap bitmap) {
        this.zzXf = zzaegVar;
        this.val$bitmap = bitmap;
    }

    @Override // java.lang.Runnable
    public final void run() {
        ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
        this.val$bitmap.compress(Bitmap.CompressFormat.PNG, 0, byteArrayOutputStream);
        synchronized (this.zzXf.mLock) {
            this.zzXf.zzWX.zzcsQ = new aea();
            this.zzXf.zzWX.zzcsQ.zzctl = byteArrayOutputStream.toByteArray();
            this.zzXf.zzWX.zzcsQ.mimeType = "image/png";
            this.zzXf.zzWX.zzcsQ.zzcsJ = 1;
        }
    }
}
