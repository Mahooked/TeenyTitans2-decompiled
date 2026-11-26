package com.google.android.gms.internal;

import android.annotation.SuppressLint;
import android.annotation.TargetApi;
import android.app.Activity;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.graphics.Canvas;
import android.net.Uri;
import android.os.Build;
import android.support.annotation.Nullable;
import android.util.DisplayMetrics;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewTreeObserver;
import android.view.WindowManager;
import android.webkit.DownloadListener;
import android.webkit.ValueCallback;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import com.google.android.gms.ads.internal.zzbl;
import com.squareup.otto.Bus;
import java.lang.ref.WeakReference;
import java.util.HashMap;
import java.util.Map;
import org.apache.http.cookie.ClientCookie;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzako extends WebView implements ViewTreeObserver.OnGlobalLayoutListener, DownloadListener, zzaka {
    private final Object mLock;

    @Nullable
    private final zzcu zzIc;
    private int zzNY;
    private int zzNZ;
    private int zzOb;
    private int zzOc;
    private String zzQx;
    private zzmz zzQy;
    private Boolean zzYA;
    private final zzakr zzabS;
    private final zzbl zzabT;
    private zzakb zzabU;
    private com.google.android.gms.ads.internal.overlay.zzm zzabV;
    private boolean zzabW;
    private boolean zzabX;
    private boolean zzabY;
    private boolean zzabZ;
    private int zzaca;
    private boolean zzacb;
    private boolean zzacc;
    private zzaks zzacd;
    private boolean zzace;
    private boolean zzacf;
    private zznw zzacg;
    private int zzach;
    private int zzaci;
    private zzmz zzacj;
    private zzmz zzack;
    private zzna zzacl;
    private WeakReference<View.OnClickListener> zzacm;
    private com.google.android.gms.ads.internal.overlay.zzm zzacn;
    private boolean zzaco;
    private Map<String, zzsb> zzacp;
    private final zzig zzacq;
    private final com.google.android.gms.ads.internal.zzv zzsS;
    private final zzaje zztW;
    private zziv zzuZ;
    private zzaix zzwC;
    private final WindowManager zzwR;

    private zzako(zzakr zzakrVar, zziv zzivVar, boolean z, boolean z2, @Nullable zzcu zzcuVar, zzaje zzajeVar, zznb zznbVar, zzbl zzblVar, com.google.android.gms.ads.internal.zzv zzvVar, zzig zzigVar) {
        super(zzakrVar);
        this.mLock = new Object();
        this.zzacb = true;
        this.zzacc = false;
        this.zzQx = "";
        this.zzNZ = -1;
        this.zzNY = -1;
        this.zzOb = -1;
        this.zzOc = -1;
        this.zzabS = zzakrVar;
        this.zzuZ = zzivVar;
        this.zzabY = z;
        this.zzaca = -1;
        this.zzIc = zzcuVar;
        this.zztW = zzajeVar;
        this.zzabT = zzblVar;
        this.zzsS = zzvVar;
        this.zzwR = (WindowManager) getContext().getSystemService("window");
        this.zzacq = zzigVar;
        setBackgroundColor(0);
        WebSettings settings = getSettings();
        settings.setAllowFileAccess(false);
        try {
            settings.setJavaScriptEnabled(true);
        } catch (NullPointerException e) {
            zzafr.zzb("Unable to enable Javascript.", e);
        }
        settings.setSavePassword(false);
        settings.setSupportMultipleWindows(true);
        settings.setJavaScriptCanOpenWindowsAutomatically(true);
        if (Build.VERSION.SDK_INT >= 21) {
            settings.setMixedContentMode(2);
        }
        settings.setUserAgentString(com.google.android.gms.ads.internal.zzbs.zzbz().zzs(zzakrVar, zzajeVar.zzaP));
        com.google.android.gms.ads.internal.zzbs.zzbB().zza(getContext(), settings);
        setDownloadListener(this);
        zzjd();
        if (com.google.android.gms.common.util.zzq.zzsa()) {
            addJavascriptInterface(new zzakv(this), "googleAdsJsInterface");
        }
        removeJavascriptInterface("accessibility");
        removeJavascriptInterface("accessibilityTraversal");
        this.zzwC = new zzaix(this.zzabS.zzis(), this, this, null);
        zzd(zznbVar);
        com.google.android.gms.ads.internal.zzbs.zzbB().zzR(zzakrVar);
    }

    private final void zzF(boolean z) {
        HashMap map = new HashMap();
        map.put("isVisible", z ? "1" : "0");
        zza("onAdVisibilityChanged", map);
    }

    private final void zza(Boolean bool) {
        synchronized (this.mLock) {
            this.zzYA = bool;
        }
        com.google.android.gms.ads.internal.zzbs.zzbD().zza(bool);
    }

    private final void zzaW(String str) {
        synchronized (this.mLock) {
            if (isDestroyed()) {
                zzafr.zzaT("The webview is destroyed. Ignoring action.");
            } else {
                loadUrl(str);
            }
        }
    }

    private final void zzaX(String str) {
        if (!com.google.android.gms.common.util.zzq.zzsc()) {
            String strValueOf = String.valueOf(str);
            zzaW(strValueOf.length() != 0 ? "javascript:".concat(strValueOf) : new String("javascript:"));
            return;
        }
        if (zzhw() == null) {
            synchronized (this.mLock) {
                this.zzYA = com.google.android.gms.ads.internal.zzbs.zzbD().zzhw();
                if (this.zzYA == null) {
                    try {
                        evaluateJavascript("(function(){})()", null);
                        zza((Boolean) true);
                    } catch (IllegalStateException e) {
                        zza((Boolean) false);
                    }
                }
            }
        }
        if (!zzhw().booleanValue()) {
            String strValueOf2 = String.valueOf(str);
            zzaW(strValueOf2.length() != 0 ? "javascript:".concat(strValueOf2) : new String("javascript:"));
        } else {
            synchronized (this.mLock) {
                if (isDestroyed()) {
                    zzafr.zzaT("The webview is destroyed. Ignoring action.");
                } else {
                    evaluateJavascript(str, null);
                }
            }
        }
    }

    static zzako zzb(Context context, zziv zzivVar, boolean z, boolean z2, @Nullable zzcu zzcuVar, zzaje zzajeVar, zznb zznbVar, zzbl zzblVar, com.google.android.gms.ads.internal.zzv zzvVar, zzig zzigVar) {
        return new zzako(new zzakr(context), zzivVar, z, z2, zzcuVar, zzajeVar, zznbVar, zzblVar, zzvVar, zzigVar);
    }

    private final void zzd(zznb zznbVar) {
        zzjg();
        this.zzacl = new zzna(new zznb(true, "make_wv", this.zzuZ.zzAs));
        this.zzacl.zzdR().zzc(zznbVar);
        this.zzQy = zzmu.zzb(this.zzacl.zzdR());
        this.zzacl.zza("native:view_create", this.zzQy);
        this.zzack = null;
        this.zzacj = null;
    }

    private final void zzhI() {
        synchronized (this.mLock) {
            if (!this.zzaco) {
                this.zzaco = true;
                com.google.android.gms.ads.internal.zzbs.zzbD().zzhI();
            }
        }
    }

    private final Boolean zzhw() {
        Boolean bool;
        synchronized (this.mLock) {
            bool = this.zzYA;
        }
        return bool;
    }

    private final boolean zzjb() {
        int iZzb;
        int iZzb2;
        if (!this.zzabU.zzcn() && !this.zzabU.zziP()) {
            return false;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        DisplayMetrics displayMetricsZza = zzagz.zza(this.zzwR);
        zzji.zzds();
        int iZzb3 = zzaiy.zzb(displayMetricsZza, displayMetricsZza.widthPixels);
        zzji.zzds();
        int iZzb4 = zzaiy.zzb(displayMetricsZza, displayMetricsZza.heightPixels);
        Activity activityZzis = this.zzabS.zzis();
        if (activityZzis == null || activityZzis.getWindow() == null) {
            iZzb = iZzb4;
            iZzb2 = iZzb3;
        } else {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            int[] iArrZzf = zzagz.zzf(activityZzis);
            zzji.zzds();
            iZzb2 = zzaiy.zzb(displayMetricsZza, iArrZzf[0]);
            zzji.zzds();
            iZzb = zzaiy.zzb(displayMetricsZza, iArrZzf[1]);
        }
        if (this.zzNY == iZzb3 && this.zzNZ == iZzb4 && this.zzOb == iZzb2 && this.zzOc == iZzb) {
            return false;
        }
        boolean z = (this.zzNY == iZzb3 && this.zzNZ == iZzb4) ? false : true;
        this.zzNY = iZzb3;
        this.zzNZ = iZzb4;
        this.zzOb = iZzb2;
        this.zzOc = iZzb;
        new zzwu(this).zza(iZzb3, iZzb4, iZzb2, iZzb, displayMetricsZza.density, this.zzwR.getDefaultDisplay().getRotation());
        return z;
    }

    private final void zzjc() {
        zzmu.zza(this.zzacl.zzdR(), this.zzQy, "aeh2");
    }

    private final void zzjd() {
        synchronized (this.mLock) {
            if (this.zzabY || this.zzuZ.zzAt) {
                zzafr.zzaC("Enabling hardware acceleration on an overlay.");
                zzje();
            } else if (Build.VERSION.SDK_INT < 18) {
                zzafr.zzaC("Disabling hardware acceleration on an AdView.");
                synchronized (this.mLock) {
                    if (!this.zzabZ) {
                        com.google.android.gms.ads.internal.zzbs.zzbB().zzr(this);
                    }
                    this.zzabZ = true;
                }
            } else {
                zzafr.zzaC("Enabling hardware acceleration on an AdView.");
                zzje();
            }
        }
    }

    private final void zzje() {
        synchronized (this.mLock) {
            if (this.zzabZ) {
                com.google.android.gms.ads.internal.zzbs.zzbB().zzq(this);
            }
            this.zzabZ = false;
        }
    }

    private final void zzjf() {
        synchronized (this.mLock) {
            this.zzacp = null;
        }
    }

    private final void zzjg() {
        zznb zznbVarZzdR;
        if (this.zzacl == null || (zznbVarZzdR = this.zzacl.zzdR()) == null || com.google.android.gms.ads.internal.zzbs.zzbD().zzhr() == null) {
            return;
        }
        com.google.android.gms.ads.internal.zzbs.zzbD().zzhr().zza(zznbVarZzdR);
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public final void destroy() {
        synchronized (this.mLock) {
            zzjg();
            this.zzwC.zzih();
            if (this.zzabV != null) {
                this.zzabV.close();
                this.zzabV.onDestroy();
                this.zzabV = null;
            }
            this.zzabU.reset();
            if (this.zzabX) {
                return;
            }
            com.google.android.gms.ads.internal.zzbs.zzbW();
            zzsa.zze(this);
            zzjf();
            this.zzabX = true;
            zzafr.v("Initiating WebView self destruct sequence in 3...");
            this.zzabU.zziT();
        }
    }

    @Override // android.webkit.WebView
    @TargetApi(19)
    public final void evaluateJavascript(String str, ValueCallback<String> valueCallback) {
        synchronized (this.mLock) {
            if (!isDestroyed()) {
                super.evaluateJavascript(str, valueCallback);
                return;
            }
            zzafr.zzaT("The webview is destroyed. Ignoring action.");
            if (valueCallback != null) {
                valueCallback.onReceiveValue(null);
            }
        }
    }

    protected final void finalize() throws Throwable {
        try {
            if (this.mLock != null) {
                synchronized (this.mLock) {
                    if (!this.zzabX) {
                        this.zzabU.reset();
                        com.google.android.gms.ads.internal.zzbs.zzbW();
                        zzsa.zze(this);
                        zzjf();
                        zzhI();
                    }
                }
            }
            super.finalize();
        } catch (Throwable th) {
            super.finalize();
            throw th;
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final String getRequestId() {
        String str;
        synchronized (this.mLock) {
            str = this.zzQx;
        }
        return str;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final int getRequestedOrientation() {
        int i;
        synchronized (this.mLock) {
            i = this.zzaca;
        }
        return i;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final View getView() {
        return this;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final WebView getWebView() {
        return this;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean isDestroyed() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzabX;
        }
        return z;
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public final void loadData(String str, String str2, String str3) {
        synchronized (this.mLock) {
            if (isDestroyed()) {
                zzafr.zzaT("The webview is destroyed. Ignoring action.");
            } else {
                super.loadData(str, str2, str3);
            }
        }
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public final void loadDataWithBaseURL(String str, String str2, String str3, String str4, String str5) {
        synchronized (this.mLock) {
            if (isDestroyed()) {
                zzafr.zzaT("The webview is destroyed. Ignoring action.");
            } else {
                super.loadDataWithBaseURL(str, str2, str3, str4, str5);
            }
        }
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public final void loadUrl(String str) {
        synchronized (this.mLock) {
            if (isDestroyed()) {
                zzafr.zzaT("The webview is destroyed. Ignoring action.");
            } else {
                try {
                    super.loadUrl(str);
                } catch (Throwable th) {
                    com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "AdWebViewImpl.loadUrl");
                    zzafr.zzc("Could not call loadUrl. ", th);
                }
            }
        }
    }

    @Override // android.webkit.WebView, android.view.ViewGroup, android.view.View
    protected final void onAttachedToWindow() {
        boolean z = true;
        synchronized (this.mLock) {
            super.onAttachedToWindow();
            if (!isDestroyed()) {
                this.zzwC.onAttachedToWindow();
            }
            boolean z2 = this.zzace;
            if (this.zzabU == null || !this.zzabU.zziP()) {
                z = z2;
            } else {
                if (!this.zzacf) {
                    ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListenerZziQ = this.zzabU.zziQ();
                    if (onGlobalLayoutListenerZziQ != null) {
                        com.google.android.gms.ads.internal.zzbs.zzbX();
                        zzajv.zza(this, onGlobalLayoutListenerZziQ);
                    }
                    ViewTreeObserver.OnScrollChangedListener onScrollChangedListenerZziR = this.zzabU.zziR();
                    if (onScrollChangedListenerZziR != null) {
                        com.google.android.gms.ads.internal.zzbs.zzbX();
                        zzajv.zza(this, onScrollChangedListenerZziR);
                    }
                    this.zzacf = true;
                }
                zzjb();
            }
            zzF(z);
        }
    }

    @Override // android.view.ViewGroup, android.view.View
    protected final void onDetachedFromWindow() {
        synchronized (this.mLock) {
            if (!isDestroyed()) {
                this.zzwC.onDetachedFromWindow();
            }
            super.onDetachedFromWindow();
            if (this.zzacf && this.zzabU != null && this.zzabU.zziP() && getViewTreeObserver() != null && getViewTreeObserver().isAlive()) {
                ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListenerZziQ = this.zzabU.zziQ();
                if (onGlobalLayoutListenerZziQ != null) {
                    com.google.android.gms.ads.internal.zzbs.zzbB().zza(getViewTreeObserver(), onGlobalLayoutListenerZziQ);
                }
                ViewTreeObserver.OnScrollChangedListener onScrollChangedListenerZziR = this.zzabU.zziR();
                if (onScrollChangedListenerZziR != null) {
                    getViewTreeObserver().removeOnScrollChangedListener(onScrollChangedListenerZziR);
                }
                this.zzacf = false;
            }
        }
        zzF(false);
    }

    @Override // android.webkit.DownloadListener
    public final void onDownloadStart(String str, String str2, String str3, String str4, long j) {
        try {
            Intent intent = new Intent("android.intent.action.VIEW");
            intent.setDataAndType(Uri.parse(str), str4);
            com.google.android.gms.ads.internal.zzbs.zzbz();
            zzagz.zzb(getContext(), intent);
        } catch (ActivityNotFoundException e) {
            zzafr.zzaC(new StringBuilder(String.valueOf(str).length() + 51 + String.valueOf(str4).length()).append("Couldn't find an Activity to view url/mimetype: ").append(str).append(" / ").append(str4).toString());
        }
    }

    @Override // android.webkit.WebView, android.view.View
    @TargetApi(21)
    protected final void onDraw(Canvas canvas) {
        if (isDestroyed()) {
            return;
        }
        if (Build.VERSION.SDK_INT == 21 && canvas.isHardwareAccelerated() && !isAttachedToWindow()) {
            return;
        }
        super.onDraw(canvas);
        if (this.zzabU == null || this.zzabU.zzja() == null) {
            return;
        }
        this.zzabU.zzja().zzaS();
    }

    @Override // android.webkit.WebView, android.view.View
    public final boolean onGenericMotionEvent(MotionEvent motionEvent) {
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDF)).booleanValue()) {
            float axisValue = motionEvent.getAxisValue(9);
            float axisValue2 = motionEvent.getAxisValue(10);
            if (motionEvent.getActionMasked() == 8 && ((axisValue > 0.0f && !canScrollVertically(-1)) || ((axisValue < 0.0f && !canScrollVertically(1)) || ((axisValue2 > 0.0f && !canScrollHorizontally(-1)) || (axisValue2 < 0.0f && !canScrollHorizontally(1)))))) {
                return false;
            }
        }
        return super.onGenericMotionEvent(motionEvent);
    }

    @Override // android.view.ViewTreeObserver.OnGlobalLayoutListener
    public final void onGlobalLayout() {
        boolean zZzjb = zzjb();
        com.google.android.gms.ads.internal.overlay.zzm zzmVarZziu = zziu();
        if (zzmVarZziu == null || !zZzjb) {
            return;
        }
        zzmVarZziu.zzfO();
    }

    @Override // android.webkit.WebView, android.widget.AbsoluteLayout, android.view.View
    @SuppressLint({"DrawAllocation"})
    protected final void onMeasure(int i, int i2) {
        int size;
        synchronized (this.mLock) {
            if (isDestroyed()) {
                setMeasuredDimension(0, 0);
                return;
            }
            if (isInEditMode() || this.zzabY || this.zzuZ.zzAv) {
                super.onMeasure(i, i2);
                return;
            }
            if (this.zzuZ.zzAw) {
                if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzFD)).booleanValue() || !com.google.android.gms.common.util.zzq.zzsa()) {
                    super.onMeasure(i, i2);
                    return;
                }
                zza("/contentHeight", new zzakp(this));
                zzaX("(function() {  var height = -1;  if (document.body) {    height = document.body.offsetHeight;  } else if (document.documentElement) {    height = document.documentElement.offsetHeight;  }  var url = 'gmsg://mobileads.google.com/contentHeight?';  url += 'height=' + height;  try {    window.googleAdsJsInterface.notify(url);  } catch (e) {    var frame = document.getElementById('afma-notify-fluid');    if (!frame) {      frame = document.createElement('IFRAME');      frame.id = 'afma-notify-fluid';      frame.style.display = 'none';      var body = document.body || document.documentElement;      body.appendChild(frame);    }    frame.src = url;  }})();");
                float f = this.zzabS.getResources().getDisplayMetrics().density;
                int size2 = View.MeasureSpec.getSize(i);
                switch (this.zzaci) {
                    case -1:
                        size = View.MeasureSpec.getSize(i2);
                        break;
                    default:
                        size = (int) (f * this.zzaci);
                        break;
                }
                setMeasuredDimension(size2, size);
                return;
            }
            if (this.zzuZ.zzAt) {
                DisplayMetrics displayMetrics = new DisplayMetrics();
                this.zzwR.getDefaultDisplay().getMetrics(displayMetrics);
                setMeasuredDimension(displayMetrics.widthPixels, displayMetrics.heightPixels);
                return;
            }
            int mode = View.MeasureSpec.getMode(i);
            int size3 = View.MeasureSpec.getSize(i);
            int mode2 = View.MeasureSpec.getMode(i2);
            int size4 = View.MeasureSpec.getSize(i2);
            int i3 = (mode == Integer.MIN_VALUE || mode == 1073741824) ? size3 : Integer.MAX_VALUE;
            int i4 = (mode2 == Integer.MIN_VALUE || mode2 == 1073741824) ? size4 : Integer.MAX_VALUE;
            if (this.zzuZ.widthPixels > i3 || this.zzuZ.heightPixels > i4) {
                float f2 = this.zzabS.getResources().getDisplayMetrics().density;
                int i5 = (int) (size3 / f2);
                zzafr.zzaT(new StringBuilder(103).append("Not enough space to show ad. Needs ").append((int) (this.zzuZ.widthPixels / f2)).append("x").append((int) (this.zzuZ.heightPixels / f2)).append(" dp, but only has ").append(i5).append("x").append((int) (size4 / f2)).append(" dp.").toString());
                if (getVisibility() != 8) {
                    setVisibility(4);
                }
                setMeasuredDimension(0, 0);
            } else {
                if (getVisibility() != 8) {
                    setVisibility(0);
                }
                setMeasuredDimension(this.zzuZ.widthPixels, this.zzuZ.heightPixels);
            }
        }
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public void onPause() {
        if (isDestroyed()) {
            return;
        }
        try {
            super.onPause();
        } catch (Exception e) {
            zzafr.zzb("Could not pause webview.", e);
        }
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public void onResume() {
        if (isDestroyed()) {
            return;
        }
        try {
            super.onResume();
        } catch (Exception e) {
            zzafr.zzb("Could not resume webview.", e);
        }
    }

    @Override // android.webkit.WebView, android.view.View
    public final boolean onTouchEvent(MotionEvent motionEvent) {
        if (this.zzabU.zziP()) {
            synchronized (this.mLock) {
                if (this.zzacg != null) {
                    this.zzacg.zzc(motionEvent);
                }
            }
        } else if (this.zzIc != null) {
            this.zzIc.zza(motionEvent);
        }
        if (isDestroyed()) {
            return false;
        }
        return super.onTouchEvent(motionEvent);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void setContext(Context context) {
        this.zzabS.setBaseContext(context);
        this.zzwC.zzi(this.zzabS.zzis());
    }

    @Override // android.view.View, com.google.android.gms.internal.zzaka
    public final void setOnClickListener(View.OnClickListener onClickListener) {
        this.zzacm = new WeakReference<>(onClickListener);
        super.setOnClickListener(onClickListener);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void setRequestedOrientation(int i) {
        synchronized (this.mLock) {
            this.zzaca = i;
            if (this.zzabV != null) {
                this.zzabV.setRequestedOrientation(this.zzaca);
            }
        }
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public final void setWebViewClient(WebViewClient webViewClient) {
        super.setWebViewClient(webViewClient);
        if (webViewClient instanceof zzakb) {
            this.zzabU = (zzakb) webViewClient;
        }
    }

    @Override // android.webkit.WebView, com.google.android.gms.internal.zzaka
    public final void stopLoading() {
        if (isDestroyed()) {
            return;
        }
        try {
            super.stopLoading();
        } catch (Exception e) {
            zzafr.zzb("Could not stop loading webview.", e);
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzA(int i) {
        if (i == 0) {
            zzmu.zza(this.zzacl.zzdR(), this.zzQy, "aebb2");
        }
        zzjc();
        if (this.zzacl.zzdR() != null) {
            this.zzacl.zzdR().zzh("close_type", String.valueOf(i));
        }
        HashMap map = new HashMap(2);
        map.put("closetype", String.valueOf(i));
        map.put(ClientCookie.VERSION_ATTR, this.zztW.zzaP);
        zza("onhide", map);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzA(boolean z) {
        synchronized (this.mLock) {
            boolean z2 = z != this.zzabY;
            this.zzabY = z;
            zzjd();
            if (z2) {
                new zzwu(this).zzap(z ? "expanded" : Bus.DEFAULT_IDENTIFIER);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzB(boolean z) {
        synchronized (this.mLock) {
            if (this.zzabV != null) {
                this.zzabV.zza(this.zzabU.zzcn(), z);
            } else {
                this.zzabW = z;
            }
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzC(boolean z) {
        synchronized (this.mLock) {
            this.zzacb = z;
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzD(boolean z) {
        synchronized (this.mLock) {
            this.zzach = (z ? 1 : -1) + this.zzach;
            if (this.zzach <= 0 && this.zzabV != null) {
                this.zzabV.zzfR();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(Context context, zziv zzivVar, zznb zznbVar) {
        synchronized (this.mLock) {
            this.zzwC.zzih();
            setContext(context);
            this.zzabV = null;
            this.zzuZ = zzivVar;
            this.zzabY = false;
            this.zzabW = false;
            this.zzQx = "";
            this.zzaca = -1;
            com.google.android.gms.ads.internal.zzbs.zzbB();
            zzahe.zzl(this);
            loadUrl("about:blank");
            this.zzabU.reset();
            setOnTouchListener(null);
            setOnClickListener(null);
            this.zzacb = true;
            this.zzacc = false;
            this.zzacd = null;
            zzd(zznbVar);
            this.zzace = false;
            this.zzach = 0;
            com.google.android.gms.ads.internal.zzbs.zzbW();
            zzsa.zze(this);
            zzjf();
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(zzaks zzaksVar) {
        synchronized (this.mLock) {
            if (this.zzacd != null) {
                zzafr.e("Attempt to create multiple AdWebViewVideoControllers.");
            } else {
                this.zzacd = zzaksVar;
            }
        }
    }

    @Override // com.google.android.gms.internal.zzgi
    public final void zza(zzgh zzghVar) {
        synchronized (this.mLock) {
            this.zzace = zzghVar.zzxS;
        }
        zzF(zzghVar.zzxS);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(zziv zzivVar) {
        synchronized (this.mLock) {
            this.zzuZ = zzivVar;
            requestLayout();
        }
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, zzrd zzrdVar) {
        if (this.zzabU != null) {
            this.zzabU.zza(str, zzrdVar);
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(String str, Map<String, ?> map) {
        try {
            zzb(str, com.google.android.gms.ads.internal.zzbs.zzbz().zzj(map));
        } catch (JSONException e) {
            zzafr.zzaT("Could not convert parameters to JSON.");
        }
    }

    @Override // com.google.android.gms.internal.zzaka, com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, JSONObject jSONObject) {
        if (jSONObject == null) {
            jSONObject = new JSONObject();
        }
        zzi(str, jSONObject.toString());
    }

    @Override // com.google.android.gms.ads.internal.zzbl
    public final void zzaJ() {
        synchronized (this.mLock) {
            this.zzacc = true;
            if (this.zzabT != null) {
                this.zzabT.zzaJ();
            }
        }
    }

    @Override // com.google.android.gms.ads.internal.zzbl
    public final void zzaK() {
        synchronized (this.mLock) {
            this.zzacc = false;
            if (this.zzabT != null) {
                this.zzabT.zzaK();
            }
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzaU(String str) {
        synchronized (this.mLock) {
            try {
                super.loadUrl(str);
            } catch (Throwable th) {
                com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "AdWebViewImpl.loadUrlUnsafe");
                zzafr.zzc("Could not call loadUrl. ", th);
            }
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzaV(String str) {
        synchronized (this.mLock) {
            if (str == null) {
                str = "";
            }
            this.zzQx = str;
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final com.google.android.gms.ads.internal.zzv zzak() {
        return this.zzsS;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zziv zzam() {
        zziv zzivVar;
        synchronized (this.mLock) {
            zzivVar = this.zzuZ;
        }
        return zzivVar;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzb(com.google.android.gms.ads.internal.overlay.zzm zzmVar) {
        synchronized (this.mLock) {
            this.zzabV = zzmVar;
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzb(zznw zznwVar) {
        synchronized (this.mLock) {
            this.zzacg = zznwVar;
        }
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, zzrd zzrdVar) {
        if (this.zzabU != null) {
            this.zzabU.zzb(str, zzrdVar);
        }
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, JSONObject jSONObject) {
        if (jSONObject == null) {
            jSONObject = new JSONObject();
        }
        String string = jSONObject.toString();
        StringBuilder sb = new StringBuilder();
        sb.append("(window.AFMA_ReceiveMessage || function() {})('");
        sb.append(str);
        sb.append("'");
        sb.append(",");
        sb.append(string);
        sb.append(");");
        String strValueOf = String.valueOf(sb.toString());
        zzafr.zzaC(strValueOf.length() != 0 ? "Dispatching AFMA event: ".concat(strValueOf) : new String("Dispatching AFMA event: "));
        zzaX(sb.toString());
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzc(com.google.android.gms.ads.internal.overlay.zzm zzmVar) {
        synchronized (this.mLock) {
            this.zzacn = zzmVar;
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzfP() {
        if (this.zzacj == null) {
            zzmu.zza(this.zzacl.zzdR(), this.zzQy, "aes2");
            this.zzacj = zzmu.zzb(this.zzacl.zzdR());
            this.zzacl.zza("native:view_show", this.zzacj);
        }
        HashMap map = new HashMap(1);
        map.put(ClientCookie.VERSION_ATTR, this.zztW.zzaP);
        zza("onshow", map);
    }

    @Override // com.google.android.gms.internal.zzaka, com.google.android.gms.ads.internal.js.zzai
    public final void zzi(String str, String str2) {
        zzaX(new StringBuilder(String.valueOf(str).length() + 3 + String.valueOf(str2).length()).append(str).append("(").append(str2).append(");").toString());
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziA() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzabY;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziB() {
        synchronized (this.mLock) {
            zzafr.v("Destroying WebView!");
            zzhI();
            zzagz.zzZr.post(new zzakq(this));
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziC() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzacb;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziD() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzacc;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzajz zziE() {
        return null;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzmz zziF() {
        return this.zzQy;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzna zziG() {
        return this.zzacl;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzaks zziH() {
        zzaks zzaksVar;
        synchronized (this.mLock) {
            zzaksVar = this.zzacd;
        }
        return zzaksVar;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziI() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzach > 0;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziJ() {
        this.zzwC.zzig();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziK() {
        if (this.zzack == null) {
            this.zzack = zzmu.zzb(this.zzacl.zzdR());
            this.zzacl.zza("native:view_load", this.zzack);
        }
    }

    @Override // com.google.android.gms.internal.zzaka
    public final View.OnClickListener zziL() {
        return this.zzacm.get();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zznw zziM() {
        zznw zznwVar;
        synchronized (this.mLock) {
            zznwVar = this.zzacg;
        }
        return zznwVar;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziN() {
        setBackgroundColor(0);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziq() {
        zzjc();
        HashMap map = new HashMap(1);
        map.put(ClientCookie.VERSION_ATTR, this.zztW.zzaP);
        zza("onhide", map);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzir() {
        HashMap map = new HashMap(3);
        com.google.android.gms.ads.internal.zzbs.zzbz();
        map.put("app_muted", String.valueOf(zzagz.zzbh()));
        com.google.android.gms.ads.internal.zzbs.zzbz();
        map.put("app_volume", String.valueOf(zzagz.zzbf()));
        com.google.android.gms.ads.internal.zzbs.zzbz();
        map.put("device_volume", String.valueOf(zzagz.zzM(getContext())));
        zza("volume", map);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final Activity zzis() {
        return this.zzabS.zzis();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final Context zzit() {
        return this.zzabS.zzit();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final com.google.android.gms.ads.internal.overlay.zzm zziu() {
        com.google.android.gms.ads.internal.overlay.zzm zzmVar;
        synchronized (this.mLock) {
            zzmVar = this.zzabV;
        }
        return zzmVar;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final com.google.android.gms.ads.internal.overlay.zzm zziv() {
        com.google.android.gms.ads.internal.overlay.zzm zzmVar;
        synchronized (this.mLock) {
            zzmVar = this.zzacn;
        }
        return zzmVar;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzakb zziw() {
        return this.zzabU;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zzix() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzabW;
        }
        return z;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzcu zziy() {
        return this.zzIc;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzaje zziz() {
        return this.zztW;
    }
}
