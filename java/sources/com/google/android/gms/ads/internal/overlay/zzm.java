package com.google.android.gms.ads.internal.overlay;

import android.app.Activity;
import android.content.Intent;
import android.content.res.Configuration;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v4.view.InputDeviceCompat;
import android.support.v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.view.Window;
import android.webkit.WebChromeClient;
import android.widget.FrameLayout;
import android.widget.RelativeLayout;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.dynamic.IObjectWrapper;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzahe;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzakb;
import com.google.android.gms.internal.zzig;
import com.google.android.gms.internal.zzmo;
import com.google.android.gms.internal.zzwy;
import com.google.android.gms.internal.zzzn;
import java.util.Collections;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzm extends zzwy implements zzaj {
    private static int zzOF = Color.argb(0, 0, 0, 0);
    private final Activity mActivity;
    private zzaka zzJH;
    AdOverlayInfoParcel zzOG;
    private zzr zzOH;
    private zzae zzOI;
    private FrameLayout zzOK;
    private WebChromeClient.CustomViewCallback zzOL;
    private zzq zzOO;
    private Runnable zzOS;
    private boolean zzOT;
    private boolean zzOU;
    private boolean zzOJ = false;
    private boolean zzOM = false;
    private boolean zzON = false;
    private boolean zzOP = false;
    private int zzOQ = 0;
    private final Object zzOR = new Object();
    private boolean zzOV = false;
    private boolean zzOW = false;
    private boolean zzOX = true;

    public zzm(Activity activity) {
        this.mActivity = activity;
    }

    private final void zzfM() {
        if (!this.mActivity.isFinishing() || this.zzOV) {
            return;
        }
        this.zzOV = true;
        if (this.zzJH != null) {
            this.zzJH.zzA(this.zzOQ);
            synchronized (this.zzOR) {
                if (!this.zzOT && this.zzJH.zziI()) {
                    this.zzOS = new zzo(this);
                    zzagz.zzZr.postDelayed(this.zzOS, ((Long) zzbs.zzbL().zzd(zzmo.zzDS)).longValue());
                    return;
                }
            }
        }
        zzfN();
    }

    private final void zzfP() {
        this.zzJH.zzfP();
    }

    private final void zzr(boolean z) {
        int iIntValue = ((Integer) zzbs.zzbL().zzd(zzmo.zzGB)).intValue();
        zzaf zzafVar = new zzaf();
        zzafVar.size = 50;
        zzafVar.paddingLeft = z ? iIntValue : 0;
        zzafVar.paddingRight = z ? 0 : iIntValue;
        zzafVar.paddingTop = 0;
        zzafVar.paddingBottom = iIntValue;
        this.zzOI = new zzae(this.mActivity, zzafVar, this);
        RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams(-2, -2);
        layoutParams.addRule(10);
        layoutParams.addRule(z ? 11 : 9);
        this.zzOI.zza(z, this.zzOG.zzPj);
        this.zzOO.addView(this.zzOI, layoutParams);
    }

    /* JADX WARN: Code duplicated, block: B:95:0x023d  */
    private final void zzs(boolean z) throws zzp {
        boolean zZza;
        if (!this.zzOU) {
            this.mActivity.requestWindowFeature(1);
        }
        Window window = this.mActivity.getWindow();
        if (window == null) {
            throw new zzp("Invalid activity, no window available.");
        }
        if (com.google.android.gms.common.util.zzq.isAtLeastN()) {
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzGz)).booleanValue()) {
                zzbs.zzbz();
                zZza = zzagz.zza(this.mActivity, this.mActivity.getResources().getConfiguration());
            } else {
                zZza = true;
            }
        } else {
            zZza = true;
        }
        boolean z2 = this.zzOG.zzPo != null && this.zzOG.zzPo.zzus;
        if ((!this.zzON || z2) && zZza) {
            window.setFlags(1024, 1024);
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzDT)).booleanValue() && com.google.android.gms.common.util.zzq.zzsc() && this.zzOG.zzPo != null && this.zzOG.zzPo.zzux) {
                window.getDecorView().setSystemUiVisibility(InputDeviceCompat.SOURCE_TOUCHSCREEN);
            }
        }
        zzakb zzakbVarZziw = this.zzOG.zzPg.zziw();
        boolean zZzcn = zzakbVarZziw != null ? zzakbVarZziw.zzcn() : false;
        this.zzOP = false;
        if (zZzcn) {
            if (this.zzOG.orientation == zzbs.zzbB().zzhT()) {
                this.zzOP = this.mActivity.getResources().getConfiguration().orientation == 1;
            } else if (this.zzOG.orientation == zzbs.zzbB().zzhU()) {
                this.zzOP = this.mActivity.getResources().getConfiguration().orientation == 2;
            }
        }
        zzafr.zzaC(new StringBuilder(46).append("Delay onShow to next orientation change: ").append(this.zzOP).toString());
        setRequestedOrientation(this.zzOG.orientation);
        if (zzbs.zzbB().zza(window)) {
            zzafr.zzaC("Hardware acceleration on the AdActivity window enabled.");
        }
        if (this.zzON) {
            this.zzOO.setBackgroundColor(zzOF);
        } else {
            this.zzOO.setBackgroundColor(ViewCompat.MEASURED_STATE_MASK);
        }
        this.mActivity.setContentView(this.zzOO);
        this.zzOU = true;
        if (z) {
            try {
                this.zzJH = zzbs.zzbA().zza(this.mActivity, this.zzOG.zzPg.zzam(), true, zZzcn, null, this.zzOG.zzvT, null, null, this.zzOG.zzPg.zzak(), zzig.zzde());
                this.zzJH.zziw().zza(null, null, this.zzOG.zzPh, this.zzOG.zzPl, true, null, this.zzOG.zzPg.zziw().zziO(), null, null);
                this.zzJH.zziw().zza(new zzn(this));
                if (this.zzOG.url != null) {
                    this.zzJH.loadUrl(this.zzOG.url);
                } else {
                    if (this.zzOG.zzPk == null) {
                        throw new zzp("No URL or HTML to display in ad overlay.");
                    }
                    this.zzJH.loadDataWithBaseURL(this.zzOG.zzPi, this.zzOG.zzPk, "text/html", HTTP.UTF_8, null);
                }
                if (this.zzOG.zzPg != null) {
                    this.zzOG.zzPg.zzc(this);
                }
            } catch (Exception e) {
                throw new zzp("Could not obtain webview for the overlay.");
            }
        } else {
            this.zzJH = this.zzOG.zzPg;
            this.zzJH.setContext(this.mActivity);
        }
        this.zzJH.zzb(this);
        ViewParent parent = this.zzJH.getParent();
        if (parent != null && (parent instanceof ViewGroup)) {
            ((ViewGroup) parent).removeView(this.zzJH.getView());
        }
        if (this.zzON) {
            this.zzJH.zziN();
        }
        this.zzOO.addView(this.zzJH.getView(), -1, -1);
        if (!z && !this.zzOP) {
            zzfP();
        }
        zzr(zZzcn);
        if (this.zzJH.zzix()) {
            zza(zZzcn, true);
        }
    }

    public final void close() {
        this.zzOQ = 2;
        this.mActivity.finish();
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onActivityResult(int i, int i2, Intent intent) {
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void onBackPressed() {
        this.zzOQ = 0;
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onCreate(Bundle bundle) {
        this.mActivity.requestWindowFeature(1);
        this.zzOM = bundle != null ? bundle.getBoolean("com.google.android.gms.ads.internal.overlay.hasResumed", false) : false;
        try {
            this.zzOG = AdOverlayInfoParcel.zzb(this.mActivity.getIntent());
            if (this.zzOG == null) {
                throw new zzp("Could not get info for ad overlay.");
            }
            if (this.zzOG.zzvT.zzaaP > 7500000) {
                this.zzOQ = 3;
            }
            if (this.mActivity.getIntent() != null) {
                this.zzOX = this.mActivity.getIntent().getBooleanExtra("shouldCallOnOverlayOpened", true);
            }
            if (this.zzOG.zzPo != null) {
                this.zzON = this.zzOG.zzPo.zzur;
            } else {
                this.zzON = false;
            }
            if (((Boolean) zzbs.zzbL().zzd(zzmo.zzFh)).booleanValue() && this.zzON && this.zzOG.zzPo.zzuw != -1) {
                new zzs(this, null).zzhL();
            }
            if (bundle == null) {
                if (this.zzOG.zzPf != null && this.zzOX) {
                    this.zzOG.zzPf.zzaB();
                }
                if (this.zzOG.zzPm != 1 && this.zzOG.zzPe != null) {
                    this.zzOG.zzPe.onAdClicked();
                }
            }
            this.zzOO = new zzq(this.mActivity, this.zzOG.zzPn, this.zzOG.zzvT.zzaP);
            this.zzOO.setId(1000);
            switch (this.zzOG.zzPm) {
                case 1:
                    zzs(false);
                    return;
                case 2:
                    this.zzOH = new zzr(this.zzOG.zzPg);
                    zzs(false);
                    return;
                case 3:
                    zzs(true);
                    return;
                case 4:
                    if (this.zzOM) {
                        this.zzOQ = 3;
                        this.mActivity.finish();
                        return;
                    }
                    zzbs.zzbw();
                    if (zza.zza(this.mActivity, this.zzOG.zzPd, this.zzOG.zzPl)) {
                        return;
                    }
                    this.zzOQ = 3;
                    this.mActivity.finish();
                    return;
                default:
                    throw new zzp("Could not determine ad overlay type.");
            }
        } catch (zzp e) {
            zzafr.zzaT(e.getMessage());
            this.zzOQ = 3;
            this.mActivity.finish();
        }
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onDestroy() {
        if (this.zzJH != null) {
            this.zzOO.removeView(this.zzJH.getView());
        }
        zzfM();
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onPause() {
        zzfI();
        if (this.zzOG.zzPf != null) {
            this.zzOG.zzPf.onPause();
        }
        if (!((Boolean) zzbs.zzbL().zzd(zzmo.zzGA)).booleanValue() && this.zzJH != null && (!this.mActivity.isFinishing() || this.zzOH == null)) {
            zzbs.zzbB();
            zzahe.zzk(this.zzJH);
        }
        zzfM();
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void onRestart() {
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onResume() {
        if (this.zzOG != null && this.zzOG.zzPm == 4) {
            if (this.zzOM) {
                this.zzOQ = 3;
                this.mActivity.finish();
            } else {
                this.zzOM = true;
            }
        }
        if (this.zzOG.zzPf != null) {
            this.zzOG.zzPf.onResume();
        }
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzGA)).booleanValue()) {
            return;
        }
        if (this.zzJH == null || this.zzJH.isDestroyed()) {
            zzafr.zzaT("The webview does not exist. Ignoring action.");
        } else {
            zzbs.zzbB();
            zzahe.zzl(this.zzJH);
        }
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void onSaveInstanceState(Bundle bundle) {
        bundle.putBoolean("com.google.android.gms.ads.internal.overlay.hasResumed", this.zzOM);
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onStart() {
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzGA)).booleanValue()) {
            if (this.zzJH == null || this.zzJH.isDestroyed()) {
                zzafr.zzaT("The webview does not exist. Ignoring action.");
            } else {
                zzbs.zzbB();
                zzahe.zzl(this.zzJH);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzwx
    public void onStop() {
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzGA)).booleanValue() && this.zzJH != null && (!this.mActivity.isFinishing() || this.zzOH == null)) {
            zzbs.zzbB();
            zzahe.zzk(this.zzJH);
        }
        zzfM();
    }

    public final void setRequestedOrientation(int i) {
        this.mActivity.setRequestedOrientation(i);
    }

    public final void zza(View view, WebChromeClient.CustomViewCallback customViewCallback) {
        this.zzOK = new FrameLayout(this.mActivity);
        this.zzOK.setBackgroundColor(ViewCompat.MEASURED_STATE_MASK);
        this.zzOK.addView(view, -1, -1);
        this.mActivity.setContentView(this.zzOK);
        this.zzOU = true;
        this.zzOL = customViewCallback;
        this.zzOJ = true;
    }

    public final void zza(boolean z, boolean z2) {
        if (this.zzOI != null) {
            this.zzOI.zza(z, z2);
        }
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void zzaa() {
        this.zzOU = true;
    }

    public final void zzfI() {
        if (this.zzOG != null && this.zzOJ) {
            setRequestedOrientation(this.zzOG.orientation);
        }
        if (this.zzOK != null) {
            this.mActivity.setContentView(this.zzOO);
            this.zzOU = true;
            this.zzOK.removeAllViews();
            this.zzOK = null;
        }
        if (this.zzOL != null) {
            this.zzOL.onCustomViewHidden();
            this.zzOL = null;
        }
        this.zzOJ = false;
    }

    @Override // com.google.android.gms.ads.internal.overlay.zzaj
    public final void zzfJ() {
        this.zzOQ = 1;
        this.mActivity.finish();
    }

    @Override // com.google.android.gms.internal.zzwx
    public final boolean zzfK() {
        this.zzOQ = 0;
        if (this.zzJH == null) {
            return true;
        }
        boolean zZziC = this.zzJH.zziC();
        if (zZziC) {
            return zZziC;
        }
        this.zzJH.zza("onbackblocked", Collections.emptyMap());
        return zZziC;
    }

    public final void zzfL() {
        this.zzOO.removeView(this.zzOI);
        zzr(true);
    }

    final void zzfN() {
        if (this.zzOW) {
            return;
        }
        this.zzOW = true;
        if (this.zzJH != null) {
            this.zzOO.removeView(this.zzJH.getView());
            if (this.zzOH != null) {
                this.zzJH.setContext(this.zzOH.zzqD);
                this.zzJH.zzA(false);
                this.zzOH.parent.addView(this.zzJH.getView(), this.zzOH.index, this.zzOH.zzPa);
                this.zzOH = null;
            } else if (this.mActivity.getApplicationContext() != null) {
                this.zzJH.setContext(this.mActivity.getApplicationContext());
            }
            this.zzJH = null;
        }
        if (this.zzOG == null || this.zzOG.zzPf == null) {
            return;
        }
        this.zzOG.zzPf.zzaA();
    }

    public final void zzfO() {
        if (this.zzOP) {
            this.zzOP = false;
            zzfP();
        }
    }

    public final void zzfQ() {
        this.zzOO.zzOZ = true;
    }

    public final void zzfR() {
        synchronized (this.zzOR) {
            this.zzOT = true;
            if (this.zzOS != null) {
                zzagz.zzZr.removeCallbacks(this.zzOS);
                zzagz.zzZr.post(this.zzOS);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzwx
    public final void zzo(IObjectWrapper iObjectWrapper) {
        if (((Boolean) zzbs.zzbL().zzd(zzmo.zzGz)).booleanValue() && com.google.android.gms.common.util.zzq.isAtLeastN()) {
            Configuration configuration = (Configuration) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapper);
            zzbs.zzbz();
            if (zzagz.zza(this.mActivity, configuration)) {
                this.mActivity.getWindow().addFlags(1024);
                this.mActivity.getWindow().clearFlags(2048);
            } else {
                this.mActivity.getWindow().addFlags(2048);
                this.mActivity.getWindow().clearFlags(1024);
            }
        }
    }
}
