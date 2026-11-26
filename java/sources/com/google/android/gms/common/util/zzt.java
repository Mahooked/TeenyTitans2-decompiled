package com.google.android.gms.common.util;

import java.util.regex.Pattern;

/* JADX INFO: loaded from: classes.dex */
public final class zzt {
    private static final Pattern zzaJY = Pattern.compile("\\$\\{(.*?)\\}");

    public static boolean zzcL(String str) {
        return str == null || str.trim().isEmpty();
    }
}
