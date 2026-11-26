package com.google.android.gms.auth.api.signin.internal;

import android.content.Context;
import android.content.SharedPreferences;
import android.text.TextUtils;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.internal.zzbo;
import java.util.concurrent.locks.Lock;
import java.util.concurrent.locks.ReentrantLock;
import org.json.JSONException;

/* JADX INFO: loaded from: classes.dex */
public final class zzy {
    private static final Lock zzamD = new ReentrantLock();
    private static zzy zzamE;
    private final Lock zzamF = new ReentrantLock();
    private final SharedPreferences zzamG;

    private zzy(Context context) {
        this.zzamG = context.getSharedPreferences("com.google.android.gms.signin", 0);
    }

    public static zzy zzaj(Context context) {
        zzbo.zzu(context);
        zzamD.lock();
        try {
            if (zzamE == null) {
                zzamE = new zzy(context.getApplicationContext());
            }
            return zzamE;
        } finally {
            zzamD.unlock();
        }
    }

    private final GoogleSignInAccount zzbS(String str) {
        String strZzbU;
        if (TextUtils.isEmpty(str) || (strZzbU = zzbU(zzs("googleSignInAccount", str))) == null) {
            return null;
        }
        try {
            return GoogleSignInAccount.zzbP(strZzbU);
        } catch (JSONException e) {
            return null;
        }
    }

    private final GoogleSignInOptions zzbT(String str) {
        String strZzbU;
        if (TextUtils.isEmpty(str) || (strZzbU = zzbU(zzs("googleSignInOptions", str))) == null) {
            return null;
        }
        try {
            return GoogleSignInOptions.zzbQ(strZzbU);
        } catch (JSONException e) {
            return null;
        }
    }

    private final String zzbU(String str) {
        this.zzamF.lock();
        try {
            return this.zzamG.getString(str, null);
        } finally {
            this.zzamF.unlock();
        }
    }

    private final void zzbV(String str) {
        this.zzamF.lock();
        try {
            this.zzamG.edit().remove(str).apply();
        } finally {
            this.zzamF.unlock();
        }
    }

    private final void zzr(String str, String str2) {
        this.zzamF.lock();
        try {
            this.zzamG.edit().putString(str, str2).apply();
        } finally {
            this.zzamF.unlock();
        }
    }

    private static String zzs(String str, String str2) {
        String strValueOf = String.valueOf(":");
        return new StringBuilder(String.valueOf(str).length() + String.valueOf(strValueOf).length() + String.valueOf(str2).length()).append(str).append(strValueOf).append(str2).toString();
    }

    public final void zza(GoogleSignInAccount googleSignInAccount, GoogleSignInOptions googleSignInOptions) {
        zzbo.zzu(googleSignInAccount);
        zzbo.zzu(googleSignInOptions);
        zzr("defaultGoogleSignInAccount", googleSignInAccount.zzmx());
        zzbo.zzu(googleSignInAccount);
        zzbo.zzu(googleSignInOptions);
        String strZzmx = googleSignInAccount.zzmx();
        zzr(zzs("googleSignInAccount", strZzmx), googleSignInAccount.zzmy());
        zzr(zzs("googleSignInOptions", strZzmx), googleSignInOptions.zzmC());
    }

    public final GoogleSignInAccount zzmN() {
        return zzbS(zzbU("defaultGoogleSignInAccount"));
    }

    public final GoogleSignInOptions zzmO() {
        return zzbT(zzbU("defaultGoogleSignInAccount"));
    }

    public final void zzmP() {
        String strZzbU = zzbU("defaultGoogleSignInAccount");
        zzbV("defaultGoogleSignInAccount");
        if (TextUtils.isEmpty(strZzbU)) {
            return;
        }
        zzbV(zzs("googleSignInAccount", strZzbU));
        zzbV(zzs("googleSignInOptions", strZzbU));
    }
}
