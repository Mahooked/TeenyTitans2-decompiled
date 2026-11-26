package com.google.android.gms.ads;

import android.content.Context;
import android.support.annotation.RequiresPermission;
import android.util.AttributeSet;
import android.view.View;
import android.view.ViewGroup;
import com.google.android.gms.ads.doubleclick.AppEventListener;
import com.google.android.gms.internal.zzajc;
import com.google.android.gms.internal.zzim;
import com.google.android.gms.internal.zzlc;

/* JADX INFO: loaded from: classes.dex */
class BaseAdView extends ViewGroup {
    protected final zzlc zzrZ;

    public BaseAdView(Context context, int i) {
        super(context);
        this.zzrZ = new zzlc(this, i);
    }

    public BaseAdView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet);
        this.zzrZ = new zzlc(this, attributeSet, false, i);
    }

    public BaseAdView(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i);
        this.zzrZ = new zzlc(this, attributeSet, false, i2);
    }

    public void destroy() {
        this.zzrZ.destroy();
    }

    public AdListener getAdListener() {
        return this.zzrZ.getAdListener();
    }

    public AdSize getAdSize() {
        return this.zzrZ.getAdSize();
    }

    public String getAdUnitId() {
        return this.zzrZ.getAdUnitId();
    }

    public String getMediationAdapterClassName() {
        return this.zzrZ.getMediationAdapterClassName();
    }

    public boolean isLoading() {
        return this.zzrZ.isLoading();
    }

    @RequiresPermission("android.permission.INTERNET")
    public void loadAd(AdRequest adRequest) {
        this.zzrZ.zza(adRequest.zzab());
    }

    @Override // android.view.ViewGroup, android.view.View
    protected void onLayout(boolean z, int i, int i2, int i3, int i4) {
        View childAt = getChildAt(0);
        if (childAt == null || childAt.getVisibility() == 8) {
            return;
        }
        int measuredWidth = childAt.getMeasuredWidth();
        int measuredHeight = childAt.getMeasuredHeight();
        int i5 = ((i3 - i) - measuredWidth) / 2;
        int i6 = ((i4 - i2) - measuredHeight) / 2;
        childAt.layout(i5, i6, measuredWidth + i5, measuredHeight + i6);
    }

    @Override // android.view.View
    protected void onMeasure(int i, int i2) {
        AdSize adSize;
        int widthInPixels;
        int heightInPixels = 0;
        View childAt = getChildAt(0);
        if (childAt == null || childAt.getVisibility() == 8) {
            try {
                adSize = getAdSize();
            } catch (NullPointerException e) {
                zzajc.zzb("Unable to retrieve ad size.", e);
                adSize = null;
            }
            if (adSize != null) {
                Context context = getContext();
                widthInPixels = adSize.getWidthInPixels(context);
                heightInPixels = adSize.getHeightInPixels(context);
            } else {
                widthInPixels = 0;
            }
        } else {
            measureChild(childAt, i, i2);
            widthInPixels = childAt.getMeasuredWidth();
            heightInPixels = childAt.getMeasuredHeight();
        }
        setMeasuredDimension(View.resolveSize(Math.max(widthInPixels, getSuggestedMinimumWidth()), i), View.resolveSize(Math.max(heightInPixels, getSuggestedMinimumHeight()), i2));
    }

    public void pause() {
        this.zzrZ.pause();
    }

    public void resume() {
        this.zzrZ.resume();
    }

    /* JADX WARN: Multi-variable type inference failed */
    public void setAdListener(AdListener adListener) {
        this.zzrZ.setAdListener(adListener);
        if (adListener == 0) {
            this.zzrZ.zza((zzim) null);
            this.zzrZ.setAppEventListener(null);
            return;
        }
        if (adListener instanceof zzim) {
            this.zzrZ.zza((zzim) adListener);
        }
        if (adListener instanceof AppEventListener) {
            this.zzrZ.setAppEventListener((AppEventListener) adListener);
        }
    }

    public void setAdSize(AdSize adSize) {
        this.zzrZ.setAdSizes(adSize);
    }

    public void setAdUnitId(String str) {
        this.zzrZ.setAdUnitId(str);
    }
}
