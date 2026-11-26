package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import android.webkit.WebResourceError;
import android.webkit.WebResourceRequest;
import android.webkit.WebView;
import android.webkit.WebViewClient;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzmo;

/* JADX INFO: loaded from: classes.dex */
final class zzbn extends WebViewClient {
    private /* synthetic */ zzbm zzvf;

    zzbn(zzbm zzbmVar) {
        this.zzvf = zzbmVar;
    }

    @Override // android.webkit.WebViewClient
    public final void onReceivedError(WebView webView, WebResourceRequest webResourceRequest, WebResourceError webResourceError) {
        if (this.zzvf.zztK != null) {
            try {
                this.zzvf.zztK.onAdFailedToLoad(0);
            } catch (RemoteException e) {
                zzafr.zzc("Could not call AdListener.onAdFailedToLoad().", e);
            }
        }
    }

    @Override // android.webkit.WebViewClient
    public final boolean shouldOverrideUrlLoading(WebView webView, String str) {
        if (str.startsWith(this.zzvf.zzbq())) {
            return false;
        }
        if (str.startsWith((String) zzbs.zzbL().zzd(zzmo.zzFS))) {
            if (this.zzvf.zztK != null) {
                try {
                    this.zzvf.zztK.onAdFailedToLoad(3);
                } catch (RemoteException e) {
                    zzafr.zzc("Could not call AdListener.onAdFailedToLoad().", e);
                }
            }
            this.zzvf.zzf(0);
            return true;
        }
        if (str.startsWith((String) zzbs.zzbL().zzd(zzmo.zzFT))) {
            if (this.zzvf.zztK != null) {
                try {
                    this.zzvf.zztK.onAdFailedToLoad(0);
                } catch (RemoteException e2) {
                    zzafr.zzc("Could not call AdListener.onAdFailedToLoad().", e2);
                }
            }
            this.zzvf.zzf(0);
            return true;
        }
        if (str.startsWith((String) zzbs.zzbL().zzd(zzmo.zzFU))) {
            if (this.zzvf.zztK != null) {
                try {
                    this.zzvf.zztK.onAdLoaded();
                } catch (RemoteException e3) {
                    zzafr.zzc("Could not call AdListener.onAdLoaded().", e3);
                }
            }
            this.zzvf.zzf(this.zzvf.zzv(str));
            return true;
        }
        if (str.startsWith("gmsg://")) {
            return true;
        }
        if (this.zzvf.zztK != null) {
            try {
                this.zzvf.zztK.onAdLeftApplication();
            } catch (RemoteException e4) {
                zzafr.zzc("Could not call AdListener.onAdLeftApplication().", e4);
            }
        }
        this.zzvf.zzx(this.zzvf.zzw(str));
        return true;
    }
}
