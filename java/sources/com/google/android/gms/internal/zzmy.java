package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.text.TextUtils;

/* JADX INFO: loaded from: classes.dex */
final class zzmy extends zzmv {
    zzmy() {
    }

    @Nullable
    private static String zzN(@Nullable String str) {
        if (TextUtils.isEmpty(str)) {
            return str;
        }
        int i = 0;
        int length = str.length();
        while (i < str.length() && str.charAt(i) == ',') {
            i++;
        }
        while (length > 0 && str.charAt(length - 1) == ',') {
            length--;
        }
        return (i == 0 && length == str.length()) ? str : str.substring(i, length);
    }

    @Override // com.google.android.gms.internal.zzmv
    public final String zzg(@Nullable String str, String str2) {
        String strZzN = zzN(str);
        String strZzN2 = zzN(str2);
        if (TextUtils.isEmpty(strZzN)) {
            return strZzN2;
        }
        return TextUtils.isEmpty(strZzN2) ? strZzN : new StringBuilder(String.valueOf(strZzN).length() + 1 + String.valueOf(strZzN2).length()).append(strZzN).append(",").append(strZzN2).toString();
    }
}
