package com.google.android.gms.internal;

import android.content.Context;
import android.support.annotation.Nullable;
import com.google.android.gms.common.api.Releasable;
import java.lang.ref.WeakReference;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public abstract class zzsb implements Releasable {
    protected Context mContext;
    private String zzJP;
    private WeakReference<zzaka> zzJQ;

    public zzsb(zzaka zzakaVar) {
        this.mContext = zzakaVar.getContext();
        this.zzJP = com.google.android.gms.ads.internal.zzbs.zzbz().zzs(this.mContext, zzakaVar.zziz().zzaP);
        this.zzJQ = new WeakReference<>(zzakaVar);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static String zzV(String str) {
        switch (str) {
            case "error":
            case "playerFailed":
            case "inProgress":
            case "contentLengthMissing":
                return "internal";
            case "noCacheDir":
            case "expireFailed":
                return "io";
            case "badUrl":
            case "downloadTimeout":
                return "network";
            case "sizeExceeded":
            case "externalAbort":
                return "policy";
            default:
                return "internal";
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public final void zza(String str, Map<String, String> map) {
        zzaka zzakaVar = this.zzJQ.get();
        if (zzakaVar != null) {
            zzakaVar.zza(str, map);
        }
    }

    public abstract void abort();

    @Override // com.google.android.gms.common.api.Releasable
    public void release() {
    }

    public abstract boolean zzU(String str);

    protected final void zza(String str, String str2, int i) {
        zzaiy.zzaaH.post(new zzsd(this, str, str2, i));
    }

    public final void zza(String str, String str2, String str3, @Nullable String str4) {
        zzaiy.zzaaH.post(new zzse(this, str, str2, str3, str4));
    }
}
