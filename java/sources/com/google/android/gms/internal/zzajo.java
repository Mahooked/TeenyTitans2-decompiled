package com.google.android.gms.internal;

import java.io.IOException;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLConnection;
import org.apache.http.HttpHost;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzajo {
    public static HttpURLConnection zzb(String str, int i) throws IOException {
        URL url = new URL(str);
        int i2 = 0;
        while (true) {
            int i3 = i2 + 1;
            if (i3 > 20) {
                throw new IOException("Too many redirects (20)");
            }
            URLConnection uRLConnectionOpenConnection = url.openConnection();
            uRLConnectionOpenConnection.setConnectTimeout(i);
            uRLConnectionOpenConnection.setReadTimeout(i);
            if (!(uRLConnectionOpenConnection instanceof HttpURLConnection)) {
                throw new IOException("Invalid protocol.");
            }
            HttpURLConnection httpURLConnection = (HttpURLConnection) uRLConnectionOpenConnection;
            httpURLConnection.setInstanceFollowRedirects(false);
            if (httpURLConnection.getResponseCode() / 100 != 3) {
                return httpURLConnection;
            }
            String headerField = httpURLConnection.getHeaderField("Location");
            if (headerField == null) {
                throw new IOException("Missing Location header in redirect");
            }
            URL url2 = new URL(url, headerField);
            String protocol = url2.getProtocol();
            if (protocol == null) {
                throw new IOException("Protocol is null");
            }
            if (!protocol.equals(HttpHost.DEFAULT_SCHEME_NAME) && !protocol.equals("https")) {
                String strValueOf = String.valueOf(protocol);
                throw new IOException(strValueOf.length() != 0 ? "Unsupported scheme: ".concat(strValueOf) : new String("Unsupported scheme: "));
            }
            String strValueOf2 = String.valueOf(headerField);
            zzafr.zzaC(strValueOf2.length() != 0 ? "Redirecting to ".concat(strValueOf2) : new String("Redirecting to "));
            httpURLConnection.disconnect();
            i2 = i3;
            url = url2;
        }
    }
}
