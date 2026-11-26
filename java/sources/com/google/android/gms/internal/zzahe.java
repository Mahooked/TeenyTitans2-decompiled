package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.app.DownloadManager;
import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.net.http.SslError;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewTreeObserver;
import android.view.Window;
import android.webkit.CookieManager;
import android.webkit.CookieSyncManager;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import java.util.Collections;
import java.util.LinkedHashSet;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(8)
public class zzahe {
    private zzahe() {
    }

    public static boolean zzk(zzaka zzakaVar) {
        if (zzakaVar == null) {
            return false;
        }
        zzakaVar.onPause();
        return true;
    }

    public static boolean zzl(zzaka zzakaVar) {
        if (zzakaVar == null) {
            return false;
        }
        zzakaVar.onResume();
        return true;
    }

    public String getDefaultUserAgent(Context context) {
        return "";
    }

    public boolean isAttachedToWindow(View view) {
        return (view.getWindowToken() == null && view.getWindowVisibility() == 8) ? false : true;
    }

    public void setBackground(View view, Drawable drawable) {
        view.setBackgroundDrawable(drawable);
    }

    public void zzR(Context context) {
        com.google.android.gms.ads.internal.zzbs.zzbD().zzhH();
    }

    public CookieManager zzS(Context context) {
        try {
            CookieSyncManager.createInstance(context);
            return CookieManager.getInstance();
        } catch (Exception e) {
            zzafr.zzb("Failed to obtain CookieManager.", e);
            com.google.android.gms.ads.internal.zzbs.zzbD().zza(e, "ApiLevelUtil.getCookieManager");
            return null;
        }
    }

    public Drawable zza(Context context, Bitmap bitmap, boolean z, float f) {
        return new BitmapDrawable(context.getResources(), bitmap);
    }

    public String zza(SslError sslError) {
        return "";
    }

    public void zza(ViewTreeObserver viewTreeObserver, ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListener) {
        viewTreeObserver.removeGlobalOnLayoutListener(onGlobalLayoutListener);
    }

    public boolean zza(DownloadManager.Request request) {
        return false;
    }

    public boolean zza(Context context, WebSettings webSettings) {
        return false;
    }

    public boolean zza(Window window) {
        return false;
    }

    public zzakb zzb(zzaka zzakaVar, boolean z) {
        return new zzakb(zzakaVar, z);
    }

    public void zzb(Activity activity, ViewTreeObserver.OnGlobalLayoutListener onGlobalLayoutListener) {
        Window window = activity.getWindow();
        if (window == null || window.getDecorView() == null || window.getDecorView().getViewTreeObserver() == null) {
            return;
        }
        zza(window.getDecorView().getViewTreeObserver(), onGlobalLayoutListener);
    }

    public Set<String> zzh(Uri uri) {
        String encodedQuery;
        if (!uri.isOpaque() && (encodedQuery = uri.getEncodedQuery()) != null) {
            LinkedHashSet linkedHashSet = new LinkedHashSet();
            int i = 0;
            do {
                int iIndexOf = encodedQuery.indexOf(38, i);
                if (iIndexOf == -1) {
                    iIndexOf = encodedQuery.length();
                }
                int iIndexOf2 = encodedQuery.indexOf(61, i);
                if (iIndexOf2 > iIndexOf || iIndexOf2 == -1) {
                    iIndexOf2 = iIndexOf;
                }
                linkedHashSet.add(Uri.decode(encodedQuery.substring(i, iIndexOf2)));
                i = iIndexOf + 1;
            } while (i < encodedQuery.length());
            return Collections.unmodifiableSet(linkedHashSet);
        }
        return Collections.emptySet();
    }

    public int zzhT() {
        return 0;
    }

    public int zzhU() {
        return 1;
    }

    public int zzhV() {
        return 5;
    }

    public ViewGroup.LayoutParams zzhW() {
        return new ViewGroup.LayoutParams(-2, -2);
    }

    public int zzhX() {
        return 0;
    }

    public WebChromeClient zzm(zzaka zzakaVar) {
        return null;
    }

    public boolean zzq(View view) {
        return false;
    }

    public boolean zzr(View view) {
        return false;
    }
}
