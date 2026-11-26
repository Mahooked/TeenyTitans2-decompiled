package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.content.Context;
import android.support.annotation.Nullable;
import android.webkit.WebResourceResponse;
import android.webkit.WebView;
import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.IOException;
import java.util.HashMap;
import java.util.Map;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(11)
public class zzalg extends zzakb {
    public zzalg(zzaka zzakaVar, boolean z) {
        super(zzakaVar, z);
    }

    /* JADX WARN: Multi-variable type inference failed */
    protected final WebResourceResponse zza(WebView webView, String str, @Nullable Map<String, String> map) {
        String str2;
        if (!(webView instanceof zzaka)) {
            zzafr.zzaT("Tried to intercept request from a WebView that wasn't an AdWebView.");
            return null;
        }
        zzaka zzakaVar = (zzaka) webView;
        if (this.zztr != null) {
            this.zztr.zza(str, map, 1);
        }
        if (!"mraid.js".equalsIgnoreCase(new File(str).getName())) {
            return super.shouldInterceptRequest(webView, str);
        }
        if (zzakaVar.zziw() != null) {
            zzakaVar.zziw().zzfL();
        }
        if (zzakaVar.zzam().zzAt) {
            str2 = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCP);
        } else if (zzakaVar.zziA()) {
            str2 = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCO);
        } else {
            str2 = (String) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCN);
        }
        try {
            Context context = zzakaVar.getContext();
            String str3 = zzakaVar.zziz().zzaP;
            HashMap map2 = new HashMap();
            map2.put(HTTP.USER_AGENT, com.google.android.gms.ads.internal.zzbs.zzbz().zzs(context, str3));
            map2.put("Cache-Control", "max-stale=3600");
            String str4 = new zzaie(context).zzb(str2, map2).get(60L, TimeUnit.SECONDS);
            if (str4 == null) {
                return null;
            }
            return new WebResourceResponse("application/javascript", HTTP.UTF_8, new ByteArrayInputStream(str4.getBytes(HTTP.UTF_8)));
        } catch (IOException | InterruptedException | ExecutionException | TimeoutException e) {
            String strValueOf = String.valueOf(e.getMessage());
            zzafr.zzaT(strValueOf.length() != 0 ? "Could not fetch MRAID JS. ".concat(strValueOf) : new String("Could not fetch MRAID JS. "));
            return null;
        }
    }
}
