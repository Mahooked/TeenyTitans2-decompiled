package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.support.annotation.Nullable;
import android.view.View;
import android.webkit.WebChromeClient;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.FrameLayout;
import java.util.Map;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
@zzzn
final class zzakn extends FrameLayout implements zzaka {
    private static final int zzOF = Color.argb(0, 0, 0, 0);
    private final zzaka zzabQ;
    private final zzajz zzabR;

    public zzakn(zzaka zzakaVar) {
        super(zzakaVar.getContext());
        this.zzabQ = zzakaVar;
        this.zzabR = new zzajz(zzakaVar.zzit(), this, this);
        zzakb zzakbVarZziw = this.zzabQ.zziw();
        if (zzakbVarZziw != null) {
            zzakbVarZziw.zzJH = this;
        }
        addView(this.zzabQ.getView());
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void destroy() {
        this.zzabQ.destroy();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final String getRequestId() {
        return this.zzabQ.getRequestId();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final int getRequestedOrientation() {
        return this.zzabQ.getRequestedOrientation();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final View getView() {
        return this;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final WebView getWebView() {
        return this.zzabQ.getWebView();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean isDestroyed() {
        return this.zzabQ.isDestroyed();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void loadData(String str, String str2, String str3) {
        this.zzabQ.loadData(str, str2, str3);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void loadDataWithBaseURL(String str, String str2, String str3, String str4, String str5) {
        this.zzabQ.loadDataWithBaseURL(str, str2, str3, str4, str5);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void loadUrl(String str) {
        this.zzabQ.loadUrl(str);
    }

    @Override // com.google.android.gms.internal.zzaka
    public void onPause() {
        this.zzabR.onPause();
        this.zzabQ.onPause();
    }

    @Override // com.google.android.gms.internal.zzaka
    public void onResume() {
        this.zzabQ.onResume();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void setContext(Context context) {
        this.zzabQ.setContext(context);
    }

    @Override // android.view.View, com.google.android.gms.internal.zzaka
    public final void setOnClickListener(View.OnClickListener onClickListener) {
        this.zzabQ.setOnClickListener(onClickListener);
    }

    @Override // android.view.View, com.google.android.gms.internal.zzaka
    public final void setOnTouchListener(View.OnTouchListener onTouchListener) {
        this.zzabQ.setOnTouchListener(onTouchListener);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void setRequestedOrientation(int i) {
        this.zzabQ.setRequestedOrientation(i);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void setWebChromeClient(WebChromeClient webChromeClient) {
        this.zzabQ.setWebChromeClient(webChromeClient);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void setWebViewClient(WebViewClient webViewClient) {
        this.zzabQ.setWebViewClient(webViewClient);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void stopLoading() {
        this.zzabQ.stopLoading();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzA(int i) {
        this.zzabQ.zzA(i);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzA(boolean z) {
        this.zzabQ.zzA(z);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzB(boolean z) {
        this.zzabQ.zzB(z);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzC(boolean z) {
        this.zzabQ.zzC(z);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzD(boolean z) {
        this.zzabQ.zzD(z);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(Context context, zziv zzivVar, zznb zznbVar) {
        this.zzabR.onDestroy();
        this.zzabQ.zza(context, zzivVar, zznbVar);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(zzaks zzaksVar) {
        this.zzabQ.zza(zzaksVar);
    }

    @Override // com.google.android.gms.internal.zzgi
    public final void zza(zzgh zzghVar) {
        this.zzabQ.zza(zzghVar);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(zziv zzivVar) {
        this.zzabQ.zza(zzivVar);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, zzrd zzrdVar) {
        this.zzabQ.zza(str, zzrdVar);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zza(String str, Map<String, ?> map) {
        this.zzabQ.zza(str, map);
    }

    @Override // com.google.android.gms.internal.zzaka, com.google.android.gms.ads.internal.js.zzai
    public final void zza(String str, JSONObject jSONObject) {
        this.zzabQ.zza(str, jSONObject);
    }

    @Override // com.google.android.gms.ads.internal.zzbl
    public final void zzaJ() {
        this.zzabQ.zzaJ();
    }

    @Override // com.google.android.gms.ads.internal.zzbl
    public final void zzaK() {
        this.zzabQ.zzaK();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzaU(String str) {
        this.zzabQ.zzaU(str);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzaV(String str) {
        this.zzabQ.zzaV(str);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final com.google.android.gms.ads.internal.zzv zzak() {
        return this.zzabQ.zzak();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zziv zzam() {
        return this.zzabQ.zzam();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzb(com.google.android.gms.ads.internal.overlay.zzm zzmVar) {
        this.zzabQ.zzb(zzmVar);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzb(@Nullable zznw zznwVar) {
        this.zzabQ.zzb(zznwVar);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, zzrd zzrdVar) {
        this.zzabQ.zzb(str, zzrdVar);
    }

    @Override // com.google.android.gms.ads.internal.js.zzai
    public final void zzb(String str, JSONObject jSONObject) {
        this.zzabQ.zzb(str, jSONObject);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzc(com.google.android.gms.ads.internal.overlay.zzm zzmVar) {
        this.zzabQ.zzc(zzmVar);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzfP() {
        this.zzabQ.zzfP();
    }

    @Override // com.google.android.gms.internal.zzaka, com.google.android.gms.ads.internal.js.zzai
    public final void zzi(String str, String str2) {
        this.zzabQ.zzi(str, str2);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziA() {
        return this.zzabQ.zziA();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziB() {
        this.zzabR.onDestroy();
        this.zzabQ.zziB();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziC() {
        return this.zzabQ.zziC();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziD() {
        return this.zzabQ.zziD();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzajz zziE() {
        return this.zzabR;
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzmz zziF() {
        return this.zzabQ.zziF();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzna zziG() {
        return this.zzabQ.zziG();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzaks zziH() {
        return this.zzabQ.zziH();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zziI() {
        return this.zzabQ.zziI();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziJ() {
        this.zzabQ.zziJ();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziK() {
        this.zzabQ.zziK();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final View.OnClickListener zziL() {
        return this.zzabQ.zziL();
    }

    @Override // com.google.android.gms.internal.zzaka
    @Nullable
    public final zznw zziM() {
        return this.zzabQ.zziM();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziN() {
        setBackgroundColor(zzOF);
        this.zzabQ.setBackgroundColor(zzOF);
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zziq() {
        this.zzabQ.zziq();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final void zzir() {
        this.zzabQ.zzir();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final Activity zzis() {
        return this.zzabQ.zzis();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final Context zzit() {
        return this.zzabQ.zzit();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final com.google.android.gms.ads.internal.overlay.zzm zziu() {
        return this.zzabQ.zziu();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final com.google.android.gms.ads.internal.overlay.zzm zziv() {
        return this.zzabQ.zziv();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzakb zziw() {
        return this.zzabQ.zziw();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final boolean zzix() {
        return this.zzabQ.zzix();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzcu zziy() {
        return this.zzabQ.zziy();
    }

    @Override // com.google.android.gms.internal.zzaka
    public final zzaje zziz() {
        return this.zzabQ.zziz();
    }
}
