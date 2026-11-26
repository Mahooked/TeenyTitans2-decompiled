package com.google.android.gms.internal;

import android.app.Activity;
import android.graphics.Bitmap;
import android.text.TextUtils;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.view.Window;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.PopupWindow;
import android.widget.RelativeLayout;
import com.google.android.gms.ads.AdSize;
import com.squareup.otto.Bus;
import java.util.Map;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzwk extends zzwu {
    private static Set<String> zzNy = com.google.android.gms.common.util.zzf.zzb("top-left", "top-right", "top-center", "center", "bottom-left", "bottom-right", "bottom-center");
    private final Object mLock;
    private final zzaka zzJH;
    private boolean zzNA;
    private int zzNB;
    private int zzNC;
    private int zzND;
    private int zzNE;
    private ImageView zzNF;
    private LinearLayout zzNG;
    private zzwv zzNH;
    private PopupWindow zzNI;
    private RelativeLayout zzNJ;
    private ViewGroup zzNK;
    private final Activity zzNo;
    private String zzNz;
    private int zzrW;
    private int zzrX;
    private zziv zzuZ;

    public zzwk(zzaka zzakaVar, zzwv zzwvVar) {
        super(zzakaVar, "resize");
        this.zzNz = "top-right";
        this.zzNA = true;
        this.zzNB = 0;
        this.zzNC = 0;
        this.zzrX = -1;
        this.zzND = 0;
        this.zzNE = 0;
        this.zzrW = -1;
        this.mLock = new Object();
        this.zzJH = zzakaVar;
        this.zzNo = zzakaVar.zzis();
        this.zzNH = zzwvVar;
    }

    private final void zza(int i, int i2) {
        zzb(i, i2 - com.google.android.gms.ads.internal.zzbs.zzbz().zzh(this.zzNo)[0], this.zzrW, this.zzrX);
    }

    /* JADX WARN: Code duplicated, block: B:58:0x0131  */
    private final int[] zzfB() {
        boolean z;
        int i;
        int i2;
        int[] iArrZzg = com.google.android.gms.ads.internal.zzbs.zzbz().zzg(this.zzNo);
        int[] iArrZzh = com.google.android.gms.ads.internal.zzbs.zzbz().zzh(this.zzNo);
        int i3 = iArrZzg[0];
        int i4 = iArrZzg[1];
        if (this.zzrW < 50 || this.zzrW > i3) {
            zzafr.zzaT("Width is too small or too large.");
            z = false;
        } else if (this.zzrX < 50 || this.zzrX > i4) {
            zzafr.zzaT("Height is too small or too large.");
            z = false;
        } else if (this.zzrX == i4 && this.zzrW == i3) {
            zzafr.zzaT("Cannot resize to a full-screen ad.");
            z = false;
        } else if (this.zzNA) {
            switch (this.zzNz) {
                case "top-left":
                    i = this.zzND + this.zzNB;
                    i2 = this.zzNC + this.zzNE;
                    break;
                case "top-center":
                    i = ((this.zzNB + this.zzND) + (this.zzrW / 2)) - 25;
                    i2 = this.zzNC + this.zzNE;
                    break;
                case "center":
                    i = ((this.zzNB + this.zzND) + (this.zzrW / 2)) - 25;
                    i2 = ((this.zzNC + this.zzNE) + (this.zzrX / 2)) - 25;
                    break;
                case "bottom-left":
                    i = this.zzND + this.zzNB;
                    i2 = ((this.zzNC + this.zzNE) + this.zzrX) - 50;
                    break;
                case "bottom-center":
                    i = ((this.zzNB + this.zzND) + (this.zzrW / 2)) - 25;
                    i2 = ((this.zzNC + this.zzNE) + this.zzrX) - 50;
                    break;
                case "bottom-right":
                    i = ((this.zzNB + this.zzND) + this.zzrW) - 50;
                    i2 = ((this.zzNC + this.zzNE) + this.zzrX) - 50;
                    break;
                default:
                    i = ((this.zzNB + this.zzND) + this.zzrW) - 50;
                    i2 = this.zzNC + this.zzNE;
                    break;
            }
            if (i < 0 || i + 50 > i3 || i2 < iArrZzh[0] || i2 + 50 > iArrZzh[1]) {
                z = false;
            } else {
                z = true;
            }
        } else {
            z = true;
        }
        if (!z) {
            return null;
        }
        if (this.zzNA) {
            return new int[]{this.zzNB + this.zzND, this.zzNC + this.zzNE};
        }
        int[] iArrZzg2 = com.google.android.gms.ads.internal.zzbs.zzbz().zzg(this.zzNo);
        int[] iArrZzh2 = com.google.android.gms.ads.internal.zzbs.zzbz().zzh(this.zzNo);
        int i5 = iArrZzg2[0];
        int i6 = this.zzNB + this.zzND;
        int i7 = this.zzNC + this.zzNE;
        if (i6 < 0) {
            i6 = 0;
        } else if (this.zzrW + i6 > i5) {
            i6 = i5 - this.zzrW;
        }
        if (i7 < iArrZzh2[0]) {
            i7 = iArrZzh2[0];
        } else if (this.zzrX + i7 > iArrZzh2[1]) {
            i7 = iArrZzh2[1] - this.zzrX;
        }
        return new int[]{i6, i7};
    }

    public final void execute(Map<String, String> map) {
        synchronized (this.mLock) {
            if (this.zzNo == null) {
                zzan("Not an activity context. Cannot resize.");
                return;
            }
            if (this.zzJH.zzam() == null) {
                zzan("Webview is not yet available, size is not set.");
                return;
            }
            if (this.zzJH.zzam().zzAt) {
                zzan("Is interstitial. Cannot resize an interstitial.");
                return;
            }
            if (this.zzJH.zziA()) {
                zzan("Cannot resize an expanded banner.");
                return;
            }
            if (!TextUtils.isEmpty(map.get("width"))) {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                this.zzrW = zzagz.zzaJ(map.get("width"));
            }
            if (!TextUtils.isEmpty(map.get("height"))) {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                this.zzrX = zzagz.zzaJ(map.get("height"));
            }
            if (!TextUtils.isEmpty(map.get("offsetX"))) {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                this.zzND = zzagz.zzaJ(map.get("offsetX"));
            }
            if (!TextUtils.isEmpty(map.get("offsetY"))) {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                this.zzNE = zzagz.zzaJ(map.get("offsetY"));
            }
            if (!TextUtils.isEmpty(map.get("allowOffscreen"))) {
                this.zzNA = Boolean.parseBoolean(map.get("allowOffscreen"));
            }
            String str = map.get("customClosePosition");
            if (!TextUtils.isEmpty(str)) {
                this.zzNz = str;
            }
            if (!(this.zzrW >= 0 && this.zzrX >= 0)) {
                zzan("Invalid width and height options. Cannot resize.");
                return;
            }
            Window window = this.zzNo.getWindow();
            if (window == null || window.getDecorView() == null) {
                zzan("Activity context is not ready, cannot get window or decor view.");
                return;
            }
            int[] iArrZzfB = zzfB();
            if (iArrZzfB == null) {
                zzan("Resize location out of screen or close button is not visible.");
                return;
            }
            zzji.zzds();
            int iZzc = zzaiy.zzc(this.zzNo, this.zzrW);
            zzji.zzds();
            int iZzc2 = zzaiy.zzc(this.zzNo, this.zzrX);
            ViewParent parent = this.zzJH.getView().getParent();
            if (parent == null || !(parent instanceof ViewGroup)) {
                zzan("Webview is detached, probably in the middle of a resize or expand.");
                return;
            }
            ((ViewGroup) parent).removeView(this.zzJH.getView());
            if (this.zzNI == null) {
                this.zzNK = (ViewGroup) parent;
                com.google.android.gms.ads.internal.zzbs.zzbz();
                Bitmap bitmapZzl = zzagz.zzl(this.zzJH.getView());
                this.zzNF = new ImageView(this.zzNo);
                this.zzNF.setImageBitmap(bitmapZzl);
                this.zzuZ = this.zzJH.zzam();
                this.zzNK.addView(this.zzNF);
            } else {
                this.zzNI.dismiss();
            }
            this.zzNJ = new RelativeLayout(this.zzNo);
            this.zzNJ.setBackgroundColor(0);
            this.zzNJ.setLayoutParams(new ViewGroup.LayoutParams(iZzc, iZzc2));
            com.google.android.gms.ads.internal.zzbs.zzbz();
            this.zzNI = zzagz.zza((View) this.zzNJ, iZzc, iZzc2, false);
            this.zzNI.setOutsideTouchable(true);
            this.zzNI.setTouchable(true);
            this.zzNI.setClippingEnabled(!this.zzNA);
            this.zzNJ.addView(this.zzJH.getView(), -1, -1);
            this.zzNG = new LinearLayout(this.zzNo);
            zzji.zzds();
            int iZzc3 = zzaiy.zzc(this.zzNo, 50);
            zzji.zzds();
            RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(iZzc3, zzaiy.zzc(this.zzNo, 50));
            switch (this.zzNz) {
                case "top-left":
                    layoutParams.addRule(10);
                    layoutParams.addRule(9);
                    break;
                case "top-center":
                    layoutParams.addRule(10);
                    layoutParams.addRule(14);
                    break;
                case "center":
                    layoutParams.addRule(13);
                    break;
                case "bottom-left":
                    layoutParams.addRule(12);
                    layoutParams.addRule(9);
                    break;
                case "bottom-center":
                    layoutParams.addRule(12);
                    layoutParams.addRule(14);
                    break;
                case "bottom-right":
                    layoutParams.addRule(12);
                    layoutParams.addRule(11);
                    break;
                default:
                    layoutParams.addRule(10);
                    layoutParams.addRule(11);
                    break;
            }
            this.zzNG.setOnClickListener(new zzwl(this));
            this.zzNG.setContentDescription("Close button");
            this.zzNJ.addView(this.zzNG, layoutParams);
            try {
                PopupWindow popupWindow = this.zzNI;
                View decorView = window.getDecorView();
                zzji.zzds();
                int iZzc4 = zzaiy.zzc(this.zzNo, iArrZzfB[0]);
                zzji.zzds();
                popupWindow.showAtLocation(decorView, 0, iZzc4, zzaiy.zzc(this.zzNo, iArrZzfB[1]));
                int i = iArrZzfB[0];
                int i2 = iArrZzfB[1];
                if (this.zzNH != null) {
                    this.zzNH.zza(i, i2, this.zzrW, this.zzrX);
                }
                this.zzJH.zza(new zziv(this.zzNo, new AdSize(this.zzrW, this.zzrX)));
                zza(iArrZzfB[0], iArrZzfB[1]);
                zzap("resized");
            } catch (RuntimeException e) {
                String strValueOf = String.valueOf(e.getMessage());
                zzan(strValueOf.length() != 0 ? "Cannot show popup window: ".concat(strValueOf) : new String("Cannot show popup window: "));
                this.zzNJ.removeView(this.zzJH.getView());
                if (this.zzNK != null) {
                    this.zzNK.removeView(this.zzNF);
                    this.zzNK.addView(this.zzJH.getView());
                    this.zzJH.zza(this.zzuZ);
                }
            }
        }
    }

    public final void zza(int i, int i2, boolean z) {
        synchronized (this.mLock) {
            this.zzNB = i;
            this.zzNC = i2;
            if (this.zzNI != null && z) {
                int[] iArrZzfB = zzfB();
                if (iArrZzfB != null) {
                    PopupWindow popupWindow = this.zzNI;
                    zzji.zzds();
                    int iZzc = zzaiy.zzc(this.zzNo, iArrZzfB[0]);
                    zzji.zzds();
                    popupWindow.update(iZzc, zzaiy.zzc(this.zzNo, iArrZzfB[1]), this.zzNI.getWidth(), this.zzNI.getHeight());
                    zza(iArrZzfB[0], iArrZzfB[1]);
                } else {
                    zzk(true);
                }
            }
        }
    }

    public final void zzb(int i, int i2) {
        this.zzNB = i;
        this.zzNC = i2;
    }

    public final boolean zzfC() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzNI != null;
        }
        return z;
    }

    public final void zzk(boolean z) {
        synchronized (this.mLock) {
            if (this.zzNI != null) {
                this.zzNI.dismiss();
                this.zzNJ.removeView(this.zzJH.getView());
                if (this.zzNK != null) {
                    this.zzNK.removeView(this.zzNF);
                    this.zzNK.addView(this.zzJH.getView());
                    this.zzJH.zza(this.zzuZ);
                }
                if (z) {
                    zzap(Bus.DEFAULT_IDENTIFIER);
                    if (this.zzNH != null) {
                        this.zzNH.zzaN();
                    }
                }
                this.zzNI = null;
                this.zzNJ = null;
                this.zzNK = null;
                this.zzNG = null;
            }
        }
    }
}
