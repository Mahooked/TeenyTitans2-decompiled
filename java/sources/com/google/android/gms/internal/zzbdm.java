package com.google.android.gms.internal;

import android.content.Context;
import android.content.res.Resources;
import android.text.TextUtils;
import com.google.android.gms.R;
import com.google.android.gms.common.api.Status;
import com.google.android.gms.common.internal.zzbo;

/* JADX INFO: loaded from: classes.dex */
@Deprecated
public final class zzbdm {
    private static zzbdm zzaEB;
    private static final Object zzuF = new Object();
    private final String mAppId;
    private final Status zzaEC;
    private final boolean zzaED;
    private final boolean zzaEE;

    private zzbdm(Context context) {
        boolean z = true;
        Resources resources = context.getResources();
        int identifier = resources.getIdentifier("google_app_measurement_enable", "integer", resources.getResourcePackageName(R.string.common_google_play_services_unknown_issue));
        if (identifier != 0) {
            boolean z2 = resources.getInteger(identifier) != 0;
            this.zzaEE = z2 ? false : true;
            z = z2;
        } else {
            this.zzaEE = false;
        }
        this.zzaED = z;
        String strZzaD = com.google.android.gms.common.internal.zzbd.zzaD(context);
        strZzaD = strZzaD == null ? new com.google.android.gms.common.internal.zzby(context).getString("google_app_id") : strZzaD;
        if (TextUtils.isEmpty(strZzaD)) {
            this.zzaEC = new Status(10, "Missing google app id value from from string resources with name google_app_id.");
            this.mAppId = null;
        } else {
            this.mAppId = strZzaD;
            this.zzaEC = Status.zzaBm;
        }
    }

    public static Status zzaz(Context context) {
        Status status;
        zzbo.zzb(context, "Context must not be null.");
        synchronized (zzuF) {
            if (zzaEB == null) {
                zzaEB = new zzbdm(context);
            }
            status = zzaEB.zzaEC;
        }
        return status;
    }

    private static zzbdm zzcu(String str) {
        zzbdm zzbdmVar;
        synchronized (zzuF) {
            if (zzaEB == null) {
                throw new IllegalStateException(new StringBuilder(String.valueOf(str).length() + 34).append("Initialize must be called before ").append(str).append(".").toString());
            }
            zzbdmVar = zzaEB;
        }
        return zzbdmVar;
    }

    public static String zzqA() {
        return zzcu("getGoogleAppId").mAppId;
    }

    public static boolean zzqB() {
        return zzcu("isMeasurementExplicitlyDisabled").zzaEE;
    }
}
