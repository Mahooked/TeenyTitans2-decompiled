package com.comscore.metrics;

import android.content.Context;
import android.os.Build;
import android.support.v4.os.EnvironmentCompat;
import com.comscore.analytics.Core;
import com.comscore.measurement.Measurement;
import com.comscore.utils.CSLog;
import com.comscore.utils.Connectivity;
import com.comscore.utils.Constants;
import com.comscore.utils.Date;
import com.comscore.utils.Permissions;
import com.comscore.utils.Storage;
import com.comscore.utils.TransmissionMode;
import com.comscore.utils.Utils;
import java.io.UnsupportedEncodingException;
import java.net.HttpURLConnection;
import java.net.InetSocketAddress;
import java.net.MalformedURLException;
import java.net.Proxy;
import java.net.URL;
import java.net.URLEncoder;
import org.apache.http.HttpStatus;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public class Request {
    public static final boolean REDIRECTION_SUPPORTED;
    protected URL a = process();
    protected Proxy b;
    private Measurement c;
    private Core d;
    private Storage e;

    static {
        int i = Build.VERSION.SDK_INT;
        REDIRECTION_SUPPORTED = i < 11 || i > 13;
    }

    public Request(Core core, Measurement measurement) {
        this.d = core;
        this.e = core.getStorage();
        this.c = measurement;
    }

    private static Proxy a(String str) {
        int i;
        int iIndexOf = str.indexOf(58);
        if (iIndexOf != -1) {
            String strSubstring = str.substring(0, iIndexOf);
            i = Integer.parseInt(str.substring(iIndexOf + 1));
            str = strSubstring;
        } else {
            i = 80;
        }
        return new Proxy(Proxy.Type.HTTP, new InetSocketAddress(str, i));
    }

    private boolean c() throws Throwable {
        d();
        boolean zA = a();
        if (!zA) {
            e();
        }
        return zA;
    }

    private void d() {
        TransmissionMode offlineTransmissionMode = this.d.getOfflineTransmissionMode();
        if (offlineTransmissionMode == TransmissionMode.DEFAULT || ((offlineTransmissionMode == TransmissionMode.WIFIONLY && Connectivity.isConnectedWiFi(this.d.getAppContext())) || offlineTransmissionMode == TransmissionMode.PIGGYBACK)) {
            this.d.getOfflineCache().flush();
        }
    }

    private void e() {
        CSLog.e(this, "Measurement was not transmitted: " + this.c.retrieveLabelsAsString(this.d.getMeasurementLabelOrder()));
        this.d.getOfflineCache().saveEvent(this.c);
    }

    protected HttpURLConnection a(URL url) {
        HttpURLConnection httpURLConnection = this.b != null ? (HttpURLConnection) url.openConnection(this.b) : (HttpURLConnection) url.openConnection();
        httpURLConnection.setRequestProperty(HTTP.CONN_DIRECTIVE, HTTP.CONN_CLOSE);
        return httpURLConnection;
    }

    protected URL a(URL url, int i, String str) {
        switch (i) {
            case HttpStatus.SC_MULTIPLE_CHOICES /* 300 */:
            case HttpStatus.SC_MOVED_PERMANENTLY /* 301 */:
            case HttpStatus.SC_MOVED_TEMPORARILY /* 302 */:
            case HttpStatus.SC_SEE_OTHER /* 303 */:
            case HttpStatus.SC_USE_PROXY /* 305 */:
                if (str == null) {
                    return null;
                }
                if (i != 305) {
                    URL url2 = new URL(url, str);
                    if (url.getProtocol().equals(url2.getProtocol())) {
                        return url2;
                    }
                    return null;
                }
                int length = str.startsWith(new StringBuilder().append(url.getProtocol()).append(':').toString()) ? url.getProtocol().length() + 1 : 0;
                if (str.startsWith("//", length)) {
                    length += 2;
                }
                this.b = a(str.substring(length));
                return url;
            case HttpStatus.SC_NOT_MODIFIED /* 304 */:
            default:
                return null;
        }
    }

    /* JADX WARN: Code duplicated, block: B:41:0x00ab  */
    protected boolean a() throws Throwable {
        boolean z;
        HttpURLConnection httpURLConnection;
        Exception exc;
        int responseCode;
        boolean z2 = false;
        HttpURLConnection httpURLConnectionA = null;
        try {
            try {
                URL url = this.a;
                try {
                    if (b()) {
                        httpURLConnectionA = a(url);
                        responseCode = httpURLConnectionA.getResponseCode();
                    } else {
                        int i = 0;
                        URL urlA = url;
                        responseCode = 0;
                        while (urlA != null && i < 5) {
                            httpURLConnectionA = a(urlA);
                            httpURLConnectionA.setInstanceFollowRedirects(false);
                            int responseCode2 = httpURLConnectionA.getResponseCode();
                            urlA = a(urlA, responseCode2, httpURLConnectionA.getHeaderField("Location"));
                            i++;
                            responseCode = responseCode2;
                        }
                    }
                    String headerField = httpURLConnectionA.getHeaderField(HTTP.CONTENT_TYPE);
                    String headerField2 = httpURLConnectionA.getHeaderField(HTTP.CONTENT_LEN);
                    if ((responseCode == 200 && Utils.isNotEmpty(headerField) && headerField.indexOf("image/") == 0 && Utils.isNotEmpty(headerField2) && Integer.parseInt(headerField2) > 0) || (responseCode == 204 && Utils.isNotEmpty(headerField2) && Integer.parseInt(headerField2) == 0)) {
                        z2 = true;
                        this.d.getKeepAlive().reset();
                    }
                    z = z2;
                    if (httpURLConnectionA != null) {
                        httpURLConnectionA.disconnect();
                    }
                } catch (Exception e) {
                    z = z2;
                    httpURLConnection = httpURLConnectionA;
                    exc = e;
                    try {
                        CSLog.e(this, "Exception sending measurement:" + exc.getLocalizedMessage());
                        CSLog.printStackTrace(exc);
                        if (httpURLConnection != null) {
                            httpURLConnection.disconnect();
                        }
                    } catch (Throwable th) {
                        th = th;
                        httpURLConnectionA = httpURLConnection;
                        if (httpURLConnectionA != null) {
                            httpURLConnectionA.disconnect();
                        }
                        throw th;
                    }
                }
            } catch (Exception e2) {
                z = false;
                httpURLConnection = null;
                exc = e2;
            }
            return z;
        } catch (Throwable th2) {
            th = th2;
            if (httpURLConnectionA != null) {
                httpURLConnectionA.disconnect();
            }
            throw th;
        }
    }

    public Boolean availableConnection() {
        String str;
        try {
            Context appContext = this.d.getAppContext();
            boolean z = true;
            if (Connectivity.isEmulator()) {
                str = "emu";
            } else if (Connectivity.isConnectedWiFi(appContext)) {
                str = "wifi";
            } else if (Connectivity.isConnectedMobile(appContext)) {
                str = "wwan";
            } else if (Connectivity.isConnectBluetooth(appContext)) {
                str = "bth";
            } else if (Connectivity.isConnectEthernet(appContext)) {
                str = "eth";
            } else {
                str = EnvironmentCompat.MEDIA_UNKNOWN;
                z = false;
            }
            this.c.setLabel(Constants.NETWORK_TYPE_LABEL_NAME, str);
            return Boolean.valueOf(z);
        } catch (NullPointerException e) {
            return false;
        }
    }

    protected boolean b() {
        return REDIRECTION_SUPPORTED;
    }

    public URL process() {
        return process(this.c.getPixelURL());
    }

    public URL process(String str) {
        String str2;
        String strReplace;
        availableConnection();
        String strConcat = str.concat(this.c.retrieveLabelsAsString(this.d.getMeasurementLabelOrder()));
        if (strConcat.length() <= 4096 || strConcat.indexOf("&") <= 0) {
            str2 = strConcat;
        } else {
            int iLastIndexOf = strConcat.substring(0, 4088).lastIndexOf("&");
            try {
                strReplace = URLEncoder.encode(strConcat.substring(iLastIndexOf + 1), HTTP.UTF_8).replace("+", "%20");
            } catch (UnsupportedEncodingException e) {
                strReplace = "0";
            }
            str2 = strConcat.substring(0, iLastIndexOf) + "&ns_cut=" + strReplace;
        }
        try {
            return new URL(str2.length() > 4096 ? str2.substring(0, 4096) : str2);
        } catch (MalformedURLException e2) {
            return null;
        }
    }

    public boolean send() {
        Context appContext = this.d.getAppContext();
        boolean zBooleanValue = Permissions.check(appContext, "android.permission.ACCESS_NETWORK_STATE").booleanValue();
        TransmissionMode liveTransmissionMode = this.d.getLiveTransmissionMode();
        this.e.set(Constants.LAST_MEASUREMENT_PROCESSED_TIMESTAMP_KEY, String.valueOf(Date.unixTime()));
        switch (a.a[liveTransmissionMode.ordinal()]) {
            case 1:
                e();
                return false;
            case 2:
                e();
                return false;
            case 3:
                if (!zBooleanValue || availableConnection().booleanValue()) {
                    return c();
                }
                e();
                return false;
            case 4:
                if (!zBooleanValue || Connectivity.isEmulator() || Connectivity.isConnectedWiFi(appContext) || Connectivity.isConnectEthernet(appContext)) {
                    return c();
                }
                e();
                return false;
            case 5:
                if (!zBooleanValue || Connectivity.isEmulator() || Connectivity.isConnectedWiFi(appContext) || Connectivity.isDataConnected(appContext)) {
                    return c();
                }
                e();
                return false;
            default:
                return false;
        }
    }
}
