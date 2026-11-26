package com.google.android.gms.internal;

import android.R;
import android.annotation.TargetApi;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.Context;
import android.os.Message;
import android.support.v4.media.session.PlaybackStateCompat;
import android.view.View;
import android.view.WindowManager;
import android.webkit.ConsoleMessage;
import android.webkit.GeolocationPermissions;
import android.webkit.JsPromptResult;
import android.webkit.JsResult;
import android.webkit.WebChromeClient;
import android.webkit.WebStorage;
import android.webkit.WebView;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import org.apache.commons.io.FileUtils;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(11)
public class zzakw extends WebChromeClient {
    private final zzaka zzJH;

    public zzakw(zzaka zzakaVar) {
        this.zzJH = zzakaVar;
    }

    /* JADX WARN: Multi-variable type inference failed */
    private static Context zza(WebView webView) {
        if (!(webView instanceof zzaka)) {
            return webView.getContext();
        }
        zzaka zzakaVar = (zzaka) webView;
        Activity activityZzis = zzakaVar.zzis();
        return activityZzis == null ? zzakaVar.getContext() : activityZzis;
    }

    private static boolean zza(Context context, String str, String str2, String str3, JsResult jsResult, JsPromptResult jsPromptResult, boolean z) {
        try {
            AlertDialog.Builder builder = new AlertDialog.Builder(context);
            builder.setTitle(str);
            if (z) {
                LinearLayout linearLayout = new LinearLayout(context);
                linearLayout.setOrientation(1);
                TextView textView = new TextView(context);
                textView.setText(str2);
                EditText editText = new EditText(context);
                editText.setText(str3);
                linearLayout.addView(textView);
                linearLayout.addView(editText);
                builder.setView(linearLayout).setPositiveButton(R.string.ok, new zzalc(jsPromptResult, editText)).setNegativeButton(R.string.cancel, new zzalb(jsPromptResult)).setOnCancelListener(new zzala(jsPromptResult)).create().show();
            } else {
                builder.setMessage(str2).setPositiveButton(R.string.ok, new zzakz(jsResult)).setNegativeButton(R.string.cancel, new zzaky(jsResult)).setOnCancelListener(new zzakx(jsResult)).create().show();
            }
        } catch (WindowManager.BadTokenException e) {
            zzafr.zzc("Fail to display Dialog.", e);
        }
        return true;
    }

    /* JADX WARN: Multi-variable type inference failed */
    @Override // android.webkit.WebChromeClient
    public final void onCloseWindow(WebView webView) {
        if (!(webView instanceof zzaka)) {
            zzafr.zzaT("Tried to close a WebView that wasn't an AdWebView.");
            return;
        }
        com.google.android.gms.ads.internal.overlay.zzm zzmVarZziu = ((zzaka) webView).zziu();
        if (zzmVarZziu == null) {
            zzafr.zzaT("Tried to close an AdWebView not associated with an overlay.");
        } else {
            zzmVarZziu.close();
        }
    }

    @Override // android.webkit.WebChromeClient
    public final boolean onConsoleMessage(ConsoleMessage consoleMessage) {
        String strValueOf = String.valueOf(consoleMessage.message());
        String strValueOf2 = String.valueOf(consoleMessage.sourceId());
        String string = new StringBuilder(String.valueOf(strValueOf).length() + 19 + String.valueOf(strValueOf2).length()).append("JS: ").append(strValueOf).append(" (").append(strValueOf2).append(":").append(consoleMessage.lineNumber()).append(")").toString();
        if (string.contains("Application Cache")) {
            return super.onConsoleMessage(consoleMessage);
        }
        switch (zzald.zzacK[consoleMessage.messageLevel().ordinal()]) {
            case 1:
                zzafr.e(string);
                break;
            case 2:
                zzafr.zzaT(string);
                break;
            case 3:
            case 4:
                zzafr.zzaS(string);
                break;
            case 5:
                zzafr.zzaC(string);
                break;
            default:
                zzafr.zzaS(string);
                break;
        }
        return super.onConsoleMessage(consoleMessage);
    }

    @Override // android.webkit.WebChromeClient
    public final boolean onCreateWindow(WebView webView, boolean z, boolean z2, Message message) {
        WebView.WebViewTransport webViewTransport = (WebView.WebViewTransport) message.obj;
        WebView webView2 = new WebView(webView.getContext());
        webView2.setWebViewClient(this.zzJH.zziw());
        webViewTransport.setWebView(webView2);
        message.sendToTarget();
        return true;
    }

    @Override // android.webkit.WebChromeClient
    public final void onExceededDatabaseQuota(String str, String str2, long j, long j2, long j3, WebStorage.QuotaUpdater quotaUpdater) {
        long j4 = 5242880 - j3;
        if (j4 <= 0) {
            quotaUpdater.updateQuota(j);
            return;
        }
        if (j != 0) {
            if (j2 == 0) {
                j = Math.min(Math.min(PlaybackStateCompat.ACTION_PREPARE_FROM_URI, j4) + j, FileUtils.ONE_MB);
            } else if (j2 <= Math.min(FileUtils.ONE_MB - j, j4)) {
                j += j2;
            }
            j2 = j;
        } else if (j2 > j4 || j2 > FileUtils.ONE_MB) {
            j2 = 0;
        }
        quotaUpdater.updateQuota(j2);
    }

    /* JADX WARN: Code duplicated, block: B:8:0x0039  */
    @Override // android.webkit.WebChromeClient
    public final void onGeolocationPermissionsShowPrompt(String str, GeolocationPermissions.Callback callback) {
        boolean z;
        if (callback != null) {
            com.google.android.gms.ads.internal.zzbs.zzbz();
            if (zzagz.zzc(this.zzJH.getContext(), this.zzJH.getContext().getPackageName(), "android.permission.ACCESS_FINE_LOCATION")) {
                z = true;
            } else {
                com.google.android.gms.ads.internal.zzbs.zzbz();
                if (zzagz.zzc(this.zzJH.getContext(), this.zzJH.getContext().getPackageName(), "android.permission.ACCESS_COARSE_LOCATION")) {
                    z = true;
                } else {
                    z = false;
                }
            }
            callback.invoke(str, z, true);
        }
    }

    @Override // android.webkit.WebChromeClient
    public final void onHideCustomView() {
        com.google.android.gms.ads.internal.overlay.zzm zzmVarZziu = this.zzJH.zziu();
        if (zzmVarZziu == null) {
            zzafr.zzaT("Could not get ad overlay when hiding custom view.");
        } else {
            zzmVarZziu.zzfI();
        }
    }

    @Override // android.webkit.WebChromeClient
    public final boolean onJsAlert(WebView webView, String str, String str2, JsResult jsResult) {
        return zza(zza(webView), str, str2, null, jsResult, null, false);
    }

    @Override // android.webkit.WebChromeClient
    public final boolean onJsBeforeUnload(WebView webView, String str, String str2, JsResult jsResult) {
        return zza(zza(webView), str, str2, null, jsResult, null, false);
    }

    @Override // android.webkit.WebChromeClient
    public final boolean onJsConfirm(WebView webView, String str, String str2, JsResult jsResult) {
        return zza(zza(webView), str, str2, null, jsResult, null, false);
    }

    @Override // android.webkit.WebChromeClient
    public final boolean onJsPrompt(WebView webView, String str, String str2, String str3, JsPromptResult jsPromptResult) {
        return zza(zza(webView), str, str2, str3, null, jsPromptResult, true);
    }

    public final void onReachedMaxAppCacheSize(long j, long j2, WebStorage.QuotaUpdater quotaUpdater) {
        long j3 = 5242880 - j2;
        long j4 = PlaybackStateCompat.ACTION_PREPARE_FROM_URI + j;
        if (j3 < j4) {
            quotaUpdater.updateQuota(0L);
        } else {
            quotaUpdater.updateQuota(j4);
        }
    }

    @Override // android.webkit.WebChromeClient
    public final void onShowCustomView(View view, WebChromeClient.CustomViewCallback customViewCallback) {
        zza(view, -1, customViewCallback);
    }

    protected final void zza(View view, int i, WebChromeClient.CustomViewCallback customViewCallback) {
        com.google.android.gms.ads.internal.overlay.zzm zzmVarZziu = this.zzJH.zziu();
        if (zzmVarZziu == null) {
            zzafr.zzaT("Could not get ad overlay when showing custom view.");
            customViewCallback.onCustomViewHidden();
        } else {
            zzmVarZziu.zza(view, customViewCallback);
            zzmVarZziu.setRequestedOrientation(i);
        }
    }
}
