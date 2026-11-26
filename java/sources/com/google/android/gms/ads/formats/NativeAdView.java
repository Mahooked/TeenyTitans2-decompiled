package com.google.android.gms.ads.formats;

import android.annotation.TargetApi;
import android.content.Context;
import android.os.RemoteException;
import android.util.AttributeSet;
import android.view.View;
import android.view.ViewGroup;
import android.widget.FrameLayout;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.dynamic.zzn;
import com.google.android.gms.internal.zzajc;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzow;

/* JADX INFO: loaded from: classes.dex */
public class NativeAdView extends FrameLayout {
    private final FrameLayout zzss;
    private final zzow zzst;

    public NativeAdView(Context context) {
        super(context);
        this.zzss = zzc(context);
        this.zzst = zzah();
    }

    public NativeAdView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        this.zzss = zzc(context);
        this.zzst = zzah();
    }

    public NativeAdView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        this.zzss = zzc(context);
        this.zzst = zzah();
    }

    @TargetApi(21)
    public NativeAdView(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        this.zzss = zzc(context);
        this.zzst = zzah();
    }

    private final zzow zzah() {
        zzbo.zzb(this.zzss, "createDelegate must be called after mOverlayFrame has been created");
        return zzji.zzdt().zza(this.zzss.getContext(), this, this.zzss);
    }

    private final FrameLayout zzc(Context context) {
        FrameLayout frameLayout = new FrameLayout(context);
        frameLayout.setLayoutParams(new FrameLayout.LayoutParams(-1, -1));
        addView(frameLayout);
        return frameLayout;
    }

    @Override // android.view.ViewGroup
    public void addView(View view, int i, ViewGroup.LayoutParams layoutParams) {
        super.addView(view, i, layoutParams);
        super.bringChildToFront(this.zzss);
    }

    @Override // android.view.ViewGroup, android.view.ViewParent
    public void bringChildToFront(View view) {
        super.bringChildToFront(view);
        if (this.zzss != view) {
            super.bringChildToFront(this.zzss);
        }
    }

    public void destroy() {
        try {
            this.zzst.destroy();
        } catch (RemoteException e) {
            zzajc.zzb("Unable to destroy native ad view", e);
        }
    }

    public AdChoicesView getAdChoicesView() {
        View viewZzp = zzp("1098");
        if (viewZzp instanceof AdChoicesView) {
            return (AdChoicesView) viewZzp;
        }
        return null;
    }

    @Override // android.view.View
    public void onVisibilityChanged(View view, int i) {
        super.onVisibilityChanged(view, i);
        if (this.zzst != null) {
            try {
                this.zzst.zzb(zzn.zzw(view), i);
            } catch (RemoteException e) {
                zzajc.zzb("Unable to call onVisibilityChanged on delegate", e);
            }
        }
    }

    @Override // android.view.ViewGroup
    public void removeAllViews() {
        super.removeAllViews();
        super.addView(this.zzss);
    }

    @Override // android.view.ViewGroup, android.view.ViewManager
    public void removeView(View view) {
        if (this.zzss == view) {
            return;
        }
        super.removeView(view);
    }

    public void setAdChoicesView(AdChoicesView adChoicesView) {
        zza("1098", adChoicesView);
    }

    public void setNativeAd(NativeAd nativeAd) {
        try {
            this.zzst.zze((IObjectWrapper) nativeAd.zzag());
        } catch (RemoteException e) {
            zzajc.zzb("Unable to call setNativeAd on delegate", e);
        }
    }

    protected final void zza(String str, View view) {
        try {
            this.zzst.zzd(str, zzn.zzw(view));
        } catch (RemoteException e) {
            zzajc.zzb("Unable to call setAssetView on delegate", e);
        }
    }

    protected final View zzp(String str) {
        try {
            IObjectWrapper iObjectWrapperZzL = this.zzst.zzL(str);
            if (iObjectWrapperZzL != null) {
                return (View) zzn.zzE(iObjectWrapperZzL);
            }
        } catch (RemoteException e) {
            zzajc.zzb("Unable to call getAssetView on delegate", e);
        }
        return null;
    }
}
