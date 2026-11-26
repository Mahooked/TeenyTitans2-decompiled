package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.Context;
import android.net.Uri;
import android.net.http.SslError;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.media.TransportMediator;
import android.support.v4.view.ViewCompat;
import android.text.TextUtils;
import android.view.KeyEvent;
import android.view.View;
import android.view.ViewTreeObserver;
import android.webkit.SslErrorHandler;
import android.webkit.WebResourceResponse;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import com.google.ads.AdSize;
import com.google.android.gms.ads.internal.overlay.AdOverlayInfoParcel;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.concurrent.CopyOnWriteArrayList;
import org.apache.http.HttpHost;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public class zzakb extends WebViewClient {
    private static final String[] zzabn = {"UNKNOWN", "HOST_LOOKUP", "UNSUPPORTED_AUTH_SCHEME", "AUTHENTICATION", "PROXY_AUTHENTICATION", "CONNECT", "IO", "TIMEOUT", "REDIRECT_LOOP", "UNSUPPORTED_SCHEME", "FAILED_SSL_HANDSHAKE", "BAD_URL", "FILE", "FILE_NOT_FOUND", "TOO_MANY_REQUESTS"};
    private static final String[] zzabo = {"NOT_YET_VALID", "EXPIRED", "ID_MISMATCH", "UNTRUSTED", "DATE_INVALID", "INVALID"};
    private final Object mLock;
    private zzqk zzIT;
    private zzrm zzJC;
    private com.google.android.gms.ads.internal.zzw zzJE;
    private zzwk zzJF;
    protected zzaka zzJH;
    private zzwv zzNH;
    private final zzwt zzabA;
    private zzakj zzabB;
    private boolean zzabC;
    private boolean zzabD;
    private boolean zzabE;
    private int zzabF;
    private View.OnAttachStateChangeListener zzabG;
    private final HashMap<String, List<zzrd>> zzabp;
    private com.google.android.gms.ads.internal.overlay.zzw zzabq;
    private zzakf zzabr;
    private zzakg zzabs;
    private zzakh zzabt;
    private boolean zzabu;
    private boolean zzabv;
    private ViewTreeObserver.OnGlobalLayoutListener zzabw;
    private ViewTreeObserver.OnScrollChangedListener zzabx;
    private boolean zzaby;
    private com.google.android.gms.ads.internal.overlay.zzag zzabz;

    @Nullable
    protected zzaet zztr;
    private boolean zzwI;
    private zzim zzzL;

    public zzakb(zzaka zzakaVar, boolean z) {
        this(zzakaVar, z, new zzwt(zzakaVar, zzakaVar.zzit(), new zzlz(zzakaVar.getContext())), null);
    }

    private zzakb(zzaka zzakaVar, boolean z, zzwt zzwtVar, zzwk zzwkVar) {
        this.zzabp = new HashMap<>();
        this.mLock = new Object();
        this.zzabu = false;
        this.zzJH = zzakaVar;
        this.zzwI = z;
        this.zzabA = zzwtVar;
        this.zzJF = null;
    }

    static /* synthetic */ zzakh zza(zzakb zzakbVar, zzakh zzakhVar) {
        zzakbVar.zzabt = null;
        return null;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(View view, zzaet zzaetVar, int i) {
        if (!zzaetVar.zzgZ() || i <= 0) {
            return;
        }
        zzaetVar.zzk(view);
        if (zzaetVar.zzgZ()) {
            zzagz.zzZr.postDelayed(new zzakc(this, view, zzaetVar, i), 100L);
        }
    }

    private final void zza(AdOverlayInfoParcel adOverlayInfoParcel) {
        boolean zZzfC = this.zzJF != null ? this.zzJF.zzfC() : false;
        com.google.android.gms.ads.internal.zzbs.zzbx();
        com.google.android.gms.ads.internal.overlay.zzu.zza(this.zzJH.getContext(), adOverlayInfoParcel, zZzfC ? false : true);
        if (this.zztr != null) {
            String str = adOverlayInfoParcel.url;
            if (str == null && adOverlayInfoParcel.zzPd != null) {
                str = adOverlayInfoParcel.zzPd.url;
            }
            this.zztr.zzaA(str);
        }
    }

    /* JADX WARN: Code duplicated, block: B:11:0x004f  */
    private final void zzc(Context context, String str, String str2, String str3) {
        String host;
        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEw)).booleanValue()) {
            Bundle bundle = new Bundle();
            bundle.putString("err", str);
            bundle.putString("code", str2);
            if (TextUtils.isEmpty(str3)) {
                host = "";
            } else {
                Uri uri = Uri.parse(str3);
                if (uri.getHost() != null) {
                    host = uri.getHost();
                } else {
                    host = "";
                }
            }
            bundle.putString("host", host);
            com.google.android.gms.ads.internal.zzbs.zzbz().zza(context, this.zzJH.zziz().zzaP, "gmob-apps", bundle, true);
        }
    }

    private final void zzi(Uri uri) {
        String path = uri.getPath();
        List<zzrd> list = this.zzabp.get(path);
        if (list == null) {
            String strValueOf = String.valueOf(uri);
            zzafr.v(new StringBuilder(String.valueOf(strValueOf).length() + 32).append("No GMSG handler found for GMSG: ").append(strValueOf).toString());
            return;
        }
        com.google.android.gms.ads.internal.zzbs.zzbz();
        Map<String, String> mapZzg = zzagz.zzg(uri);
        if (zzafr.zzz(2)) {
            String strValueOf2 = String.valueOf(path);
            zzafr.v(strValueOf2.length() != 0 ? "Received GMSG: ".concat(strValueOf2) : new String("Received GMSG: "));
            for (String str : mapZzg.keySet()) {
                String str2 = mapZzg.get(str);
                zzafr.v(new StringBuilder(String.valueOf(str).length() + 4 + String.valueOf(str2).length()).append("  ").append(str).append(": ").append(str2).toString());
            }
        }
        Iterator<zzrd> it = list.iterator();
        while (it.hasNext()) {
            it.next().zza(this.zzJH, mapZzg);
        }
    }

    private final void zziU() {
        if (this.zzabG == null) {
            return;
        }
        this.zzJH.getView().removeOnAttachStateChangeListener(this.zzabG);
    }

    private final void zziZ() {
        if (this.zzabr != null && ((this.zzabD && this.zzabF <= 0) || this.zzabE)) {
            this.zzabr.zza(this.zzJH, !this.zzabE);
            this.zzabr = null;
        }
        this.zzJH.zziK();
    }

    @Override // android.webkit.WebViewClient
    public final void onLoadResource(WebView webView, String str) {
        String strValueOf = String.valueOf(str);
        zzafr.v(strValueOf.length() != 0 ? "Loading resource: ".concat(strValueOf) : new String("Loading resource: "));
        Uri uri = Uri.parse(str);
        if ("gmsg".equalsIgnoreCase(uri.getScheme()) && "mobileads.google.com".equalsIgnoreCase(uri.getHost())) {
            zzi(uri);
        }
    }

    @Override // android.webkit.WebViewClient
    public final void onPageFinished(WebView webView, String str) {
        synchronized (this.mLock) {
            if (this.zzabC) {
                zzafr.v("Blank page loaded, 1...");
                this.zzJH.zziB();
                return;
            }
            this.zzabD = true;
            if (this.zzabs != null) {
                this.zzabs.zzj(this.zzJH);
                this.zzabs = null;
            }
            zziZ();
        }
    }

    @Override // android.webkit.WebViewClient
    public final void onReceivedError(WebView webView, int i, String str, String str2) {
        zzc(this.zzJH.getContext(), "http_err", (i >= 0 || (-i) + (-1) >= zzabn.length) ? String.valueOf(i) : zzabn[(-i) - 1], str2);
        super.onReceivedError(webView, i, str, str2);
    }

    @Override // android.webkit.WebViewClient
    public final void onReceivedSslError(WebView webView, SslErrorHandler sslErrorHandler, SslError sslError) {
        if (sslError != null) {
            int primaryError = sslError.getPrimaryError();
            zzc(this.zzJH.getContext(), "ssl_err", (primaryError < 0 || primaryError >= zzabo.length) ? String.valueOf(primaryError) : zzabo[primaryError], com.google.android.gms.ads.internal.zzbs.zzbB().zza(sslError));
        }
        super.onReceivedSslError(webView, sslErrorHandler, sslError);
    }

    public final void reset() {
        if (this.zztr != null) {
            this.zztr.zzhb();
            this.zztr = null;
        }
        zziU();
        synchronized (this.mLock) {
            this.zzabp.clear();
            this.zzzL = null;
            this.zzabq = null;
            this.zzabr = null;
            this.zzabs = null;
            this.zzIT = null;
            this.zzabu = false;
            this.zzwI = false;
            this.zzabv = false;
            this.zzaby = false;
            this.zzabz = null;
            this.zzabt = null;
            if (this.zzJF != null) {
                this.zzJF.zzk(true);
                this.zzJF = null;
            }
        }
    }

    @Override // android.webkit.WebViewClient
    @TargetApi(11)
    public WebResourceResponse shouldInterceptRequest(WebView webView, String str) {
        zzhx zzhxVarZza;
        WebResourceResponse webResourceResponse;
        try {
            String strZzb = zzaez.zzb(str, this.zzJH.getContext());
            if (strZzb.equals(str)) {
                zzia zziaVarZzB = zzia.zzB(str);
                webResourceResponse = (zziaVarZzB == null || (zzhxVarZza = com.google.android.gms.ads.internal.zzbs.zzbE().zza(zziaVarZzB)) == null || !zzhxVarZza.zzcY()) ? null : new WebResourceResponse("", "", zzhxVarZza.zzcZ());
            } else {
                HttpURLConnection httpURLConnection = (HttpURLConnection) new URL(strZzb).openConnection();
                com.google.android.gms.ads.internal.zzbs.zzbz().zza(this.zzJH.getContext(), this.zzJH.zziz().zzaP, true, httpURLConnection);
                webResourceResponse = new WebResourceResponse(httpURLConnection.getContentType(), httpURLConnection.getHeaderField("encoding"), httpURLConnection.getInputStream());
            }
            return webResourceResponse;
        } catch (Throwable th) {
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(th, "AdWebViewClient.shouldInterceptRequest");
            return null;
        }
    }

    @Override // android.webkit.WebViewClient
    public boolean shouldOverrideKeyEvent(WebView webView, KeyEvent keyEvent) {
        switch (keyEvent.getKeyCode()) {
            case 79:
            case 85:
            case 86:
            case 87:
            case 88:
            case 89:
            case AdSize.LARGE_AD_HEIGHT /* 90 */:
            case 91:
            case TransportMediator.KEYCODE_MEDIA_PLAY /* 126 */:
            case TransportMediator.KEYCODE_MEDIA_PAUSE /* 127 */:
            case 128:
            case 129:
            case TransportMediator.KEYCODE_MEDIA_RECORD /* 130 */:
            case 222:
                return true;
            default:
                return false;
        }
    }

    @Override // android.webkit.WebViewClient
    public final boolean shouldOverrideUrlLoading(WebView webView, String str) {
        Uri uri;
        String strValueOf = String.valueOf(str);
        zzafr.v(strValueOf.length() != 0 ? "AdWebView shouldOverrideUrlLoading: ".concat(strValueOf) : new String("AdWebView shouldOverrideUrlLoading: "));
        Uri uriZza = Uri.parse(str);
        if ("gmsg".equalsIgnoreCase(uriZza.getScheme()) && "mobileads.google.com".equalsIgnoreCase(uriZza.getHost())) {
            zzi(uriZza);
        } else {
            if (this.zzabu && webView == this.zzJH.getWebView()) {
                String scheme = uriZza.getScheme();
                if (HttpHost.DEFAULT_SCHEME_NAME.equalsIgnoreCase(scheme) || "https".equalsIgnoreCase(scheme)) {
                    if (this.zzzL != null) {
                        if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDo)).booleanValue()) {
                            this.zzzL.onAdClicked();
                            if (this.zztr != null) {
                                this.zztr.zzaA(str);
                            }
                            this.zzzL = null;
                        }
                    }
                    return super.shouldOverrideUrlLoading(webView, str);
                }
            }
            if (this.zzJH.getWebView().willNotDraw()) {
                String strValueOf2 = String.valueOf(str);
                zzafr.zzaT(strValueOf2.length() != 0 ? "AdWebView unable to handle URL: ".concat(strValueOf2) : new String("AdWebView unable to handle URL: "));
            } else {
                try {
                    zzcu zzcuVarZziy = this.zzJH.zziy();
                    if (zzcuVarZziy != null && zzcuVarZziy.zzc(uriZza)) {
                        uriZza = zzcuVarZziy.zza(uriZza, this.zzJH.getContext(), this.zzJH.getView());
                    }
                    uri = uriZza;
                } catch (zzcv e) {
                    String strValueOf3 = String.valueOf(str);
                    zzafr.zzaT(strValueOf3.length() != 0 ? "Unable to append parameter to URL: ".concat(strValueOf3) : new String("Unable to append parameter to URL: "));
                    uri = uriZza;
                }
                if (this.zzJE == null || this.zzJE.zzaR()) {
                    zza(new com.google.android.gms.ads.internal.overlay.zzc("android.intent.action.VIEW", uri.toString(), null, null, null, null, null));
                } else {
                    this.zzJE.zzt(str);
                }
            }
        }
        return true;
    }

    public final void zzE(boolean z) {
        this.zzabu = false;
    }

    public final void zza(int i, int i2, boolean z) {
        this.zzabA.zzc(i, i2);
        if (this.zzJF != null) {
            this.zzJF.zza(i, i2, z);
        }
    }

    public final void zza(ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListener, ViewTreeObserver.OnScrollChangedListener onScrollChangedListener) {
        synchronized (this.mLock) {
            this.zzabv = true;
            this.zzJH.zziJ();
            this.zzabw = onGlobalLayoutListener;
            this.zzabx = onScrollChangedListener;
        }
    }

    public final void zza(com.google.android.gms.ads.internal.overlay.zzc zzcVar) {
        boolean zZziA = this.zzJH.zziA();
        zza(new AdOverlayInfoParcel(zzcVar, (!zZziA || this.zzJH.zzam().zzAt) ? this.zzzL : null, zZziA ? null : this.zzabq, this.zzabz, this.zzJH.zziz()));
    }

    public final void zza(zzakf zzakfVar) {
        this.zzabr = zzakfVar;
    }

    public final void zza(zzakg zzakgVar) {
        this.zzabs = zzakgVar;
    }

    public final void zza(zzakh zzakhVar) {
        this.zzabt = zzakhVar;
    }

    public final void zza(zzakj zzakjVar) {
        this.zzabB = zzakjVar;
    }

    public final void zza(zzim zzimVar, com.google.android.gms.ads.internal.overlay.zzw zzwVar, zzqk zzqkVar, com.google.android.gms.ads.internal.overlay.zzag zzagVar, boolean z, @Nullable zzrm zzrmVar, com.google.android.gms.ads.internal.zzw zzwVar2, zzwv zzwvVar, @Nullable zzaet zzaetVar) {
        if (zzwVar2 == null) {
            zzwVar2 = new com.google.android.gms.ads.internal.zzw(zzaetVar);
        }
        this.zzJF = new zzwk(this.zzJH, zzwvVar);
        this.zztr = zzaetVar;
        zza("/appEvent", new zzqj(zzqkVar));
        zza("/backButton", zzqn.zzJe);
        zza("/refresh", zzqn.zzJf);
        zza("/canOpenURLs", zzqn.zzIV);
        zza("/canOpenIntents", zzqn.zzIW);
        zza("/click", zzqn.zzIX);
        zza("/close", zzqn.zzIY);
        zza("/customClose", zzqn.zzIZ);
        zza("/instrument", zzqn.zzJk);
        zza("/delayPageLoaded", zzqn.zzJm);
        zza("/delayPageClosed", zzqn.zzJn);
        zza("/getLocationInfo", zzqn.zzJo);
        zza("/httpTrack", zzqn.zzJa);
        zza("/log", zzqn.zzJb);
        zza("/mraid", new zzrp(zzwVar2, this.zzJF));
        zza("/mraidLoaded", this.zzabA);
        zza("/open", new zzrq(zzwVar2, this.zzJF));
        zza("/precache", zzqn.zzJj);
        zza("/touch", zzqn.zzJd);
        zza("/video", zzqn.zzJg);
        zza("/videoMeta", zzqn.zzJh);
        if (com.google.android.gms.ads.internal.zzbs.zzbY().zzp(this.zzJH.getContext())) {
            zza("/logScionEvent", zzqn.zzJi);
        }
        if (zzrmVar != null) {
            zza("/setInterstitialProperties", new zzrl(zzrmVar));
        }
        this.zzzL = zzimVar;
        this.zzabq = zzwVar;
        this.zzIT = zzqkVar;
        this.zzabz = zzagVar;
        this.zzJE = zzwVar2;
        this.zzNH = zzwvVar;
        this.zzJC = zzrmVar;
        this.zzabu = z;
    }

    public final void zza(String str, zzrd zzrdVar) {
        synchronized (this.mLock) {
            List<zzrd> copyOnWriteArrayList = this.zzabp.get(str);
            if (copyOnWriteArrayList == null) {
                copyOnWriteArrayList = new CopyOnWriteArrayList<>();
                this.zzabp.put(str, copyOnWriteArrayList);
            }
            copyOnWriteArrayList.add(zzrdVar);
        }
    }

    public final void zza(boolean z, int i) {
        zza(new AdOverlayInfoParcel((!this.zzJH.zziA() || this.zzJH.zzam().zzAt) ? this.zzzL : null, this.zzabq, this.zzabz, this.zzJH, z, i, this.zzJH.zziz()));
    }

    public final void zza(boolean z, int i, String str) {
        boolean zZziA = this.zzJH.zziA();
        zza(new AdOverlayInfoParcel((!zZziA || this.zzJH.zzam().zzAt) ? this.zzzL : null, zZziA ? null : new zzaki(this.zzJH, this.zzabq), this.zzIT, this.zzabz, this.zzJH, z, i, str, this.zzJH.zziz()));
    }

    public final void zza(boolean z, int i, String str, String str2) {
        boolean zZziA = this.zzJH.zziA();
        zza(new AdOverlayInfoParcel((!zZziA || this.zzJH.zzam().zzAt) ? this.zzzL : null, zZziA ? null : new zzaki(this.zzJH, this.zzabq), this.zzIT, this.zzabz, this.zzJH, z, i, str, str2, this.zzJH.zziz()));
    }

    public final void zzb(int i, int i2) {
        if (this.zzJF != null) {
            this.zzJF.zzb(i, i2);
        }
    }

    public final void zzb(String str, zzrd zzrdVar) {
        synchronized (this.mLock) {
            List<zzrd> list = this.zzabp.get(str);
            if (list == null) {
                return;
            }
            list.remove(zzrdVar);
        }
    }

    public final boolean zzcn() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzwI;
        }
        return z;
    }

    public final void zzfL() {
        synchronized (this.mLock) {
            this.zzabu = false;
            this.zzwI = true;
            com.google.android.gms.ads.internal.zzbs.zzbz();
            zzagz.runOnUiThread(new zzake(this));
        }
    }

    public final com.google.android.gms.ads.internal.zzw zziO() {
        return this.zzJE;
    }

    public final boolean zziP() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzabv;
        }
        return z;
    }

    public final ViewTreeObserver.OnGlobalLayoutListener zziQ() {
        ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListener;
        synchronized (this.mLock) {
            onGlobalLayoutListener = this.zzabw;
        }
        return onGlobalLayoutListener;
    }

    public final ViewTreeObserver.OnScrollChangedListener zziR() {
        ViewTreeObserver.OnScrollChangedListener onScrollChangedListener;
        synchronized (this.mLock) {
            onScrollChangedListener = this.zzabx;
        }
        return onScrollChangedListener;
    }

    public final boolean zziS() {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzaby;
        }
        return z;
    }

    public final void zziT() {
        synchronized (this.mLock) {
            zzafr.v("Loading blank page in WebView, 2...");
            this.zzabC = true;
            this.zzJH.zzaU("about:blank");
        }
    }

    public final void zziV() {
        zzaet zzaetVar = this.zztr;
        if (zzaetVar != null) {
            WebView webView = this.zzJH.getWebView();
            if (ViewCompat.isAttachedToWindow(webView)) {
                zza(webView, zzaetVar, 10);
                return;
            }
            zziU();
            this.zzabG = new zzakd(this, zzaetVar);
            this.zzJH.getView().addOnAttachStateChangeListener(this.zzabG);
        }
    }

    public final void zziW() {
        synchronized (this.mLock) {
            this.zzaby = true;
        }
        this.zzabF++;
        zziZ();
    }

    public final void zziX() {
        this.zzabF--;
        zziZ();
    }

    public final void zziY() {
        this.zzabE = true;
        zziZ();
    }

    public final zzakj zzja() {
        return this.zzabB;
    }
}
