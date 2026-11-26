package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.support.annotation.Nullable;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import com.google.android.gms.ads.internal.zzbl;
import java.util.Map;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public interface zzaka extends com.google.android.gms.ads.internal.js.zzai, zzbl, zzgi {
    void destroy();

    Context getContext();

    int getHeight();

    ViewGroup.LayoutParams getLayoutParams();

    void getLocationOnScreen(int[] iArr);

    int getMeasuredHeight();

    int getMeasuredWidth();

    ViewParent getParent();

    String getRequestId();

    int getRequestedOrientation();

    View getView();

    WebView getWebView();

    int getWidth();

    boolean isDestroyed();

    void loadData(String str, String str2, String str3);

    void loadDataWithBaseURL(String str, String str2, String str3, String str4, @Nullable String str5);

    void loadUrl(String str);

    void measure(int i, int i2);

    void onPause();

    void onResume();

    void setBackgroundColor(int i);

    void setContext(Context context);

    void setOnClickListener(View.OnClickListener onClickListener);

    void setOnTouchListener(View.OnTouchListener onTouchListener);

    void setRequestedOrientation(int i);

    void setWebChromeClient(WebChromeClient webChromeClient);

    void setWebViewClient(WebViewClient webViewClient);

    void stopLoading();

    void zzA(int i);

    void zzA(boolean z);

    void zzB(boolean z);

    void zzC(boolean z);

    void zzD(boolean z);

    void zza(Context context, zziv zzivVar, zznb zznbVar);

    void zza(zzaks zzaksVar);

    void zza(zziv zzivVar);

    void zza(String str, Map<String, ?> map);

    @Override // com.google.android.gms.ads.internal.js.zzai
    void zza(String str, JSONObject jSONObject);

    void zzaU(String str);

    void zzaV(String str);

    com.google.android.gms.ads.internal.zzv zzak();

    zziv zzam();

    void zzb(com.google.android.gms.ads.internal.overlay.zzm zzmVar);

    void zzb(zznw zznwVar);

    void zzc(com.google.android.gms.ads.internal.overlay.zzm zzmVar);

    void zzfP();

    @Override // com.google.android.gms.ads.internal.js.zzai
    void zzi(String str, String str2);

    boolean zziA();

    void zziB();

    boolean zziC();

    boolean zziD();

    @Nullable
    zzajz zziE();

    @Nullable
    zzmz zziF();

    zzna zziG();

    @Nullable
    zzaks zziH();

    boolean zziI();

    void zziJ();

    void zziK();

    @Nullable
    View.OnClickListener zziL();

    zznw zziM();

    void zziN();

    void zziq();

    void zzir();

    Activity zzis();

    Context zzit();

    com.google.android.gms.ads.internal.overlay.zzm zziu();

    com.google.android.gms.ads.internal.overlay.zzm zziv();

    @Nullable
    zzakb zziw();

    boolean zzix();

    zzcu zziy();

    zzaje zziz();
}
