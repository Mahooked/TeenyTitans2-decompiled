package com.kayac.unitywebview;

import android.app.Activity;
import android.content.res.Resources;
import android.util.Log;
import android.view.ViewGroup;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import android.widget.FrameLayout;
import com.unity3d.player.UnityPlayer;
import java.util.concurrent.SynchronousQueue;

/* JADX INFO: loaded from: classes.dex */
public class WebViewPlugin {
    private static FrameLayout layout = null;
    private JSInterface mJSInterface;
    private WebView mWebView;
    private Resources res = null;
    private String name = null;

    private class JSInterface {
        public SynchronousQueue<String> mMessageQueue = new SynchronousQueue<>();

        JSInterface() {
        }

        public void pushMessage(String message) {
            Log.d("WebView", message);
            try {
                this.mMessageQueue.put(message);
            } catch (InterruptedException e) {
                Log.d("WebView", "Queueing error - " + e.getMessage());
            }
        }
    }

    public void Init(final String gameObject) {
        final Activity a = UnityPlayer.currentActivity;
        a.runOnUiThread(new Runnable() { // from class: com.kayac.unitywebview.WebViewPlugin.1
            @Override // java.lang.Runnable
            public void run() {
                WebViewPlugin.this.CreateWebView(a);
                WebViewPlugin.this.AddWebView(a);
                WebViewPlugin.this.SetWebView();
                WebViewPlugin.this.mJSInterface = WebViewPlugin.this.new JSInterface();
                WebViewPlugin.this.mWebView.addJavascriptInterface(WebViewPlugin.this.mJSInterface, "UnityInterface");
                WebViewPlugin.this.mWebView.setWebChromeClient(new WebChromeClient());
                WebViewPlugin.this.mWebView.setWebViewClient(new WebViewClient() { // from class: com.kayac.unitywebview.WebViewPlugin.1.1
                    @Override // android.webkit.WebViewClient
                    public void onPageFinished(WebView view, String url) {
                        UnityPlayer.UnitySendMessage(gameObject, "LoadComplete", "1");
                    }

                    @Override // android.webkit.WebViewClient
                    public void onReceivedError(WebView view, int errorCode, String description, String failingUrl) {
                        UnityPlayer.UnitySendMessage(gameObject, "LoadComplete", "0");
                    }
                });
            }
        });
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void CreateWebView(Activity a) {
        this.mWebView = new WebView(a);
        this.mWebView.setVisibility(8);
        this.mWebView.setFocusable(true);
        this.mWebView.setFocusableInTouchMode(true);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void AddWebView(Activity a) {
        if (layout == null) {
            layout = new FrameLayout(a);
            a.addContentView(layout, new ViewGroup.LayoutParams(-1, -1));
            layout.setFocusable(true);
            layout.setFocusableInTouchMode(true);
        }
        layout.addView(this.mWebView, new FrameLayout.LayoutParams(-1, -1, 0));
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void SetWebView() {
        this.mWebView.setWebViewClient(new WebViewClient());
        WebSettings webSettings = this.mWebView.getSettings();
        webSettings.setSupportZoom(false);
        webSettings.setJavaScriptEnabled(true);
        webSettings.setPluginsEnabled(true);
        webSettings.setDatabaseEnabled(true);
        webSettings.setDomStorageEnabled(true);
        this.mWebView.setScrollBarStyle(0);
        this.mWebView.setVerticalScrollbarOverlay(true);
    }

    private void SetLayoutParams(Activity a) {
        FrameLayout.LayoutParams params = new FrameLayout.LayoutParams(-1, 5, 0);
        params.leftMargin = 0;
        params.rightMargin = 0;
        String scompare = "Bottom".toString();
        if ("Top".equals(scompare)) {
            Log.d("WebView", "Top");
            params.topMargin = 0;
            params.gravity = 51;
        } else {
            Log.d("WebView", "Bottom");
            params.bottomMargin = 0;
            params.gravity = 83;
        }
    }

    public void Destroy() {
        Activity a = UnityPlayer.currentActivity;
        a.runOnUiThread(new Runnable() { // from class: com.kayac.unitywebview.WebViewPlugin.2
            @Override // java.lang.Runnable
            public void run() {
                if (WebViewPlugin.this.mWebView != null) {
                    WebViewPlugin.layout.removeView(WebViewPlugin.this.mWebView);
                    WebViewPlugin.this.mWebView = null;
                }
            }
        });
    }

    public void LoadURL(final String url) {
        Activity a = UnityPlayer.currentActivity;
        a.runOnUiThread(new Runnable() { // from class: com.kayac.unitywebview.WebViewPlugin.3
            @Override // java.lang.Runnable
            public void run() {
                WebViewPlugin.this.mWebView.loadUrl(url);
            }
        });
    }

    public void EvaluateJS(final String js) {
        Activity a = UnityPlayer.currentActivity;
        a.runOnUiThread(new Runnable() { // from class: com.kayac.unitywebview.WebViewPlugin.4
            @Override // java.lang.Runnable
            public void run() {
                WebViewPlugin.this.mWebView.loadUrl("javascript:" + js);
            }
        });
    }

    public void SetMargins(int left, int top, int right, int bottom) {
        final FrameLayout.LayoutParams params = new FrameLayout.LayoutParams(-1, -1, 0);
        params.setMargins(left, top, right, bottom);
        Activity a = UnityPlayer.currentActivity;
        a.runOnUiThread(new Runnable() { // from class: com.kayac.unitywebview.WebViewPlugin.5
            @Override // java.lang.Runnable
            public void run() {
                WebViewPlugin.this.mWebView.setLayoutParams(params);
            }
        });
    }

    public void SetVisibility(final boolean visibility) {
        Activity a = UnityPlayer.currentActivity;
        a.runOnUiThread(new Runnable() { // from class: com.kayac.unitywebview.WebViewPlugin.6
            @Override // java.lang.Runnable
            public void run() {
                if (visibility) {
                    WebViewPlugin.this.mWebView.setVisibility(0);
                    WebViewPlugin.layout.requestFocus();
                    WebViewPlugin.this.mWebView.requestFocus();
                    return;
                }
                WebViewPlugin.this.mWebView.setVisibility(8);
            }
        });
    }

    public String pollWebViewMessage() {
        return this.mJSInterface.mMessageQueue.poll();
    }
}
