package com.google.android.gms.internal;

import android.os.Bundle;
import android.view.View;
import com.google.android.gms.ads.formats.NativeAd;
import com.google.android.gms.ads.mediation.NativeAppInstallAdMapper;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzvl extends zzvd {
    private final NativeAppInstallAdMapper zzNf;

    public zzvl(NativeAppInstallAdMapper nativeAppInstallAdMapper) {
        this.zzNf = nativeAppInstallAdMapper;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getBody() {
        return this.zzNf.getBody();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getCallToAction() {
        return this.zzNf.getCallToAction();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final Bundle getExtras() {
        return this.zzNf.getExtras();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getHeadline() {
        return this.zzNf.getHeadline();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final List getImages() {
        List<NativeAd.Image> images = this.zzNf.getImages();
        if (images == null) {
            return null;
        }
        ArrayList arrayList = new ArrayList();
        for (NativeAd.Image image : images) {
            arrayList.add(new zznp(image.getDrawable(), image.getUri(), image.getScale()));
        }
        return arrayList;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final boolean getOverrideClickHandling() {
        return this.zzNf.getOverrideClickHandling();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final boolean getOverrideImpressionRecording() {
        return this.zzNf.getOverrideImpressionRecording();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getPrice() {
        return this.zzNf.getPrice();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final double getStarRating() {
        return this.zzNf.getStarRating();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final String getStore() {
        return this.zzNf.getStore();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final zzks getVideoController() {
        if (this.zzNf.getVideoController() != null) {
            return this.zzNf.getVideoController().zzae();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void recordImpression() {
        this.zzNf.recordImpression();
    }

    @Override // com.google.android.gms.internal.zzvc
    public final zzos zzeh() {
        NativeAd.Image icon = this.zzNf.getIcon();
        if (icon != null) {
            return new zznp(icon.getDrawable(), icon.getUri(), icon.getScale());
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzvc
    public final IObjectWrapper zzfw() {
        View adChoicesContent = this.zzNf.getAdChoicesContent();
        if (adChoicesContent == null) {
            return null;
        }
        return com.google.android.gms.dynamic.zzn.zzw(adChoicesContent);
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void zzl(IObjectWrapper iObjectWrapper) {
        this.zzNf.handleClick((View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void zzm(IObjectWrapper iObjectWrapper) {
        this.zzNf.trackView((View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzvc
    public final void zzn(IObjectWrapper iObjectWrapper) {
        this.zzNf.untrackView((View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }
}
