package com.google.android.gms.internal;

import android.os.Bundle;
import android.view.View;
import com.google.android.gms.ads.formats.NativeAd;
import com.google.android.gms.ads.mediation.NativeContentAdMapper;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzvm extends zzvg {
    private final NativeContentAdMapper zzNg;

    public zzvm(NativeContentAdMapper nativeContentAdMapper) {
        this.zzNg = nativeContentAdMapper;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getAdvertiser() {
        return this.zzNg.getAdvertiser();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getBody() {
        return this.zzNg.getBody();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getCallToAction() {
        return this.zzNg.getCallToAction();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final Bundle getExtras() {
        return this.zzNg.getExtras();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final String getHeadline() {
        return this.zzNg.getHeadline();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final List getImages() {
        List<NativeAd.Image> images = this.zzNg.getImages();
        if (images == null) {
            return null;
        }
        ArrayList arrayList = new ArrayList();
        for (NativeAd.Image image : images) {
            arrayList.add(new zznp(image.getDrawable(), image.getUri(), image.getScale()));
        }
        return arrayList;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final boolean getOverrideClickHandling() {
        return this.zzNg.getOverrideClickHandling();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final boolean getOverrideImpressionRecording() {
        return this.zzNg.getOverrideImpressionRecording();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final zzks getVideoController() {
        if (this.zzNg.getVideoController() != null) {
            return this.zzNg.getVideoController().zzae();
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void recordImpression() {
        this.zzNg.recordImpression();
    }

    @Override // com.google.android.gms.internal.zzvf
    public final zzos zzem() {
        NativeAd.Image logo = this.zzNg.getLogo();
        if (logo != null) {
            return new zznp(logo.getDrawable(), logo.getUri(), logo.getScale());
        }
        return null;
    }

    @Override // com.google.android.gms.internal.zzvf
    public final IObjectWrapper zzfw() {
        View adChoicesContent = this.zzNg.getAdChoicesContent();
        if (adChoicesContent == null) {
            return null;
        }
        return com.google.android.gms.dynamic.zzn.zzw(adChoicesContent);
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void zzl(IObjectWrapper iObjectWrapper) {
        this.zzNg.handleClick((View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void zzm(IObjectWrapper iObjectWrapper) {
        this.zzNg.trackView((View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.internal.zzvf
    public final void zzn(IObjectWrapper iObjectWrapper) {
        this.zzNg.untrackView((View) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper));
    }
}
