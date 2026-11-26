package com.google.android.gms.internal;

import android.content.Context;
import android.net.Uri;
import android.view.MotionEvent;
import android.view.View;

/* JADX INFO: loaded from: classes.dex */
public final class zzcu {
    private static final String[] zzqs = {"/aclk", "/pcs/click"};
    private String zzqo = "googleads.g.doubleclick.net";
    private String zzqp = "/pagead/ads";
    private String zzqq = "ad.doubleclick.net";
    private String[] zzqr = {".doubleclick.net", ".googleadservices.com", ".googlesyndication.com"};
    private zzcp zzqt;

    public zzcu(zzcp zzcpVar) {
        this.zzqt = zzcpVar;
    }

    private final Uri zza(Uri uri, Context context, String str, boolean z, View view) throws zzcv {
        try {
            boolean zZzb = zzb(uri);
            if (zZzb) {
                if (uri.toString().contains("dc_ms=")) {
                    throw new zzcv("Parameter already exists: dc_ms");
                }
            } else if (uri.getQueryParameter("ms") != null) {
                throw new zzcv("Query parameter already exists: ms");
            }
            String strZza = z ? this.zzqt.zza(context, str, view) : this.zzqt.zza(context);
            if (!zZzb) {
                String string = uri.toString();
                int iIndexOf = string.indexOf("&adurl");
                if (iIndexOf == -1) {
                    iIndexOf = string.indexOf("?adurl");
                }
                return iIndexOf != -1 ? Uri.parse(string.substring(0, iIndexOf + 1) + "ms=" + strZza + "&" + string.substring(iIndexOf + 1)) : uri.buildUpon().appendQueryParameter("ms", strZza).build();
            }
            String string2 = uri.toString();
            int iIndexOf2 = string2.indexOf(";adurl");
            if (iIndexOf2 != -1) {
                return Uri.parse(string2.substring(0, iIndexOf2 + 1) + "dc_ms=" + strZza + ";" + string2.substring(iIndexOf2 + 1));
            }
            String encodedPath = uri.getEncodedPath();
            int iIndexOf3 = string2.indexOf(encodedPath);
            return Uri.parse(string2.substring(0, encodedPath.length() + iIndexOf3) + ";dc_ms=" + strZza + ";" + string2.substring(encodedPath.length() + iIndexOf3));
        } catch (UnsupportedOperationException e) {
            throw new zzcv("Provided Uri is not in a valid state");
        }
    }

    private final boolean zzb(Uri uri) {
        if (uri == null) {
            throw new NullPointerException();
        }
        try {
            return uri.getHost().equals(this.zzqq);
        } catch (NullPointerException e) {
            return false;
        }
    }

    public final zzcp zzB() {
        return this.zzqt;
    }

    public final Uri zza(Uri uri, Context context) throws zzcv {
        return zza(uri, context, null, false, null);
    }

    public final Uri zza(Uri uri, Context context, View view) throws zzcv {
        try {
            return zza(uri, context, uri.getQueryParameter("ai"), true, view);
        } catch (UnsupportedOperationException e) {
            throw new zzcv("Provided Uri is not in a valid state");
        }
    }

    public final void zza(MotionEvent motionEvent) {
        this.zzqt.zza(motionEvent);
    }

    public final boolean zza(Uri uri) {
        if (uri == null) {
            throw new NullPointerException();
        }
        try {
            return uri.getHost().equals(this.zzqo) && uri.getPath().equals(this.zzqp);
        } catch (NullPointerException e) {
            return false;
        }
    }

    public final void zzb(String str, String str2) {
        this.zzqo = str;
        this.zzqp = str2;
    }

    public final boolean zzc(Uri uri) {
        if (uri == null) {
            throw new NullPointerException();
        }
        try {
            String host = uri.getHost();
            for (String str : this.zzqr) {
                if (host.endsWith(str)) {
                    return true;
                }
            }
            return false;
        } catch (NullPointerException e) {
            return false;
        }
    }

    public final boolean zzd(Uri uri) {
        if (!zzc(uri)) {
            return false;
        }
        for (String str : zzqs) {
            if (uri.getPath().endsWith(str)) {
                return true;
            }
        }
        return false;
    }

    public final void zzk(String str) {
        this.zzqr = str.split(",");
    }
}
