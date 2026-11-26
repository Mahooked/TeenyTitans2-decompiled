package com.google.android.gms.internal;

import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.RemoteException;
import com.google.android.gms.ads.formats.NativeAd;
import com.google.android.gms.dynamic.IObjectWrapper;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzov extends NativeAd.Image {
    private final Drawable mDrawable;
    private final Uri mUri;
    private final double zzHA;
    private final zzos zzID;

    public zzov(zzos zzosVar) {
        Drawable drawable;
        Uri uri = null;
        this.zzID = zzosVar;
        try {
            IObjectWrapper iObjectWrapperZzeg = this.zzID.zzeg();
            drawable = iObjectWrapperZzeg != null ? (Drawable) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapperZzeg) : null;
        } catch (RemoteException e) {
            zzajc.zzb("Failed to get drawable.", e);
        }
        this.mDrawable = drawable;
        try {
            uri = this.zzID.getUri();
        } catch (RemoteException e2) {
            zzajc.zzb("Failed to get uri.", e2);
        }
        this.mUri = uri;
        double scale = 1.0d;
        try {
            scale = this.zzID.getScale();
        } catch (RemoteException e3) {
            zzajc.zzb("Failed to get scale.", e3);
        }
        this.zzHA = scale;
    }

    @Override // com.google.android.gms.ads.formats.NativeAd.Image
    public final Drawable getDrawable() {
        return this.mDrawable;
    }

    @Override // com.google.android.gms.ads.formats.NativeAd.Image
    public final double getScale() {
        return this.zzHA;
    }

    @Override // com.google.android.gms.ads.formats.NativeAd.Image
    public final Uri getUri() {
        return this.mUri;
    }
}
