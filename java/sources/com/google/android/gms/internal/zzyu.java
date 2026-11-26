package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.res.Resources;
import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.graphics.drawable.BitmapDrawable;
import android.net.Uri;
import android.os.Looper;
import android.os.SystemClock;
import java.io.InputStream;

/* JADX INFO: loaded from: classes.dex */
final class zzyu implements zzaii<zznp> {
    private /* synthetic */ String zzNO;
    private /* synthetic */ zzyn zzRF;
    private /* synthetic */ boolean zzRT;
    private /* synthetic */ double zzRU;
    private /* synthetic */ boolean zzRV;

    zzyu(zzyn zzynVar, boolean z, double d, boolean z2, String str) {
        this.zzRF = zzynVar;
        this.zzRT = z;
        this.zzRU = d;
        this.zzRV = z2;
        this.zzNO = str;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.zzaii
    @TargetApi(19)
    /* JADX INFO: renamed from: zzg, reason: merged with bridge method [inline-methods] */
    public final zznp zzh(InputStream inputStream) {
        Bitmap bitmapDecodeStream;
        BitmapFactory.Options options = new BitmapFactory.Options();
        options.inDensity = (int) (160.0d * this.zzRU);
        if (!this.zzRV) {
            options.inPreferredConfig = Bitmap.Config.RGB_565;
        }
        long jUptimeMillis = SystemClock.uptimeMillis();
        try {
            bitmapDecodeStream = BitmapFactory.decodeStream(inputStream, null, options);
        } catch (Exception e) {
            zzafr.zzb("Error grabbing image.", e);
            bitmapDecodeStream = null;
        }
        if (bitmapDecodeStream == null) {
            this.zzRF.zzc(2, this.zzRT);
            return null;
        }
        long jUptimeMillis2 = SystemClock.uptimeMillis();
        if (com.google.android.gms.common.util.zzq.zzsc() && zzafr.zzhM()) {
            int width = bitmapDecodeStream.getWidth();
            zzafr.v(new StringBuilder(108).append("Decoded image w: ").append(width).append(" h:").append(bitmapDecodeStream.getHeight()).append(" bytes: ").append(bitmapDecodeStream.getAllocationByteCount()).append(" time: ").append(jUptimeMillis2 - jUptimeMillis).append(" on ui thread: ").append(Looper.getMainLooper().getThread() == Thread.currentThread()).toString());
        }
        return new zznp(new BitmapDrawable(Resources.getSystem(), bitmapDecodeStream), Uri.parse(this.zzNO), this.zzRU);
    }

    @Override // com.google.android.gms.internal.zzaii
    public final /* synthetic */ zznp zzgy() {
        this.zzRF.zzc(2, this.zzRT);
        return null;
    }
}
