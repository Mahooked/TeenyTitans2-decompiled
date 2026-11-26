package com.comscore.utils.id;

import android.content.Context;
import android.os.Build;
import android.provider.Settings;
import android.support.v4.os.EnvironmentCompat;
import com.comscore.utils.Constants;
import com.comscore.utils.Storage;
import com.comscore.utils.Utils;

/* JADX INFO: loaded from: classes.dex */
public class IdChecker {
    private static final String[] a = {"0123456789ABCDEF", "0123456789abcdef", "9774d56d682e549c", "9774D56D682E549C", EnvironmentCompat.MEDIA_UNKNOWN, "UNKNOWN", "android_id", "ANDROID_ID"};
    private Context b;
    private boolean c = false;
    private String d;
    private Storage e;

    public IdChecker(Context context, Storage storage) {
        this.b = context;
        this.e = storage;
    }

    private boolean a(String str, String str2) {
        for (String str3 : this.e.get(str2).split(";")) {
            if (str3.equals(str)) {
                return true;
            }
        }
        return false;
    }

    private String b() {
        if (Integer.valueOf(Build.VERSION.SDK_INT).intValue() >= 9) {
            return Build.SERIAL;
        }
        return null;
    }

    private void b(String str, String str2) {
        String str3 = this.e.get(str2);
        if (str3.length() > 0) {
            str = str3 + ";" + str;
        }
        this.e.set(str2, str);
    }

    private String c() {
        String string;
        if (Integer.valueOf(Build.VERSION.SDK_INT).intValue() < 3 || (string = Settings.Secure.getString(this.b.getContentResolver(), "android_id")) == null || string.length() <= 0) {
            return null;
        }
        return string;
    }

    protected String a() {
        boolean z = false;
        if (this.c) {
            return this.d;
        }
        this.c = true;
        String strB = b();
        if ((strB == null || strB.length() == 0) && ((strB = c()) == null || strB.length() == 0)) {
            return null;
        }
        for (int i = 0; i < a.length; i++) {
            if (a[i].equals(strB)) {
                z = true;
                break;
            }
        }
        if (!z) {
            return null;
        }
        this.d = strB;
        return strB;
    }

    public boolean checkCrossPublisherId(String str) {
        String strA;
        if (str == null || str.length() == 0 || (strA = a()) == null || a(strA, Constants.ADID_CHECK_DATA)) {
            return true;
        }
        b(strA, Constants.ADID_CHECK_DATA);
        return !Utils.md5(strA).equals(str);
    }

    public boolean checkVisitorId() {
        String strA = a();
        if (strA == null || a(strA, Constants.ID_CHECK_DATA)) {
            return true;
        }
        b(strA, Constants.ID_CHECK_DATA);
        return false;
    }
}
