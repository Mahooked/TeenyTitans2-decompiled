package com.google.android.gms.common.util;

import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzp {
    public static void zza(StringBuilder sb, HashMap<String, String> map) {
        sb.append("{");
        boolean z = true;
        for (String str : map.keySet()) {
            if (z) {
                z = false;
            } else {
                sb.append(",");
                z = z;
            }
            String str2 = map.get(str);
            sb.append("\"").append(str).append("\":");
            if (str2 == null) {
                sb.append("null");
            } else {
                sb.append("\"").append(str2).append("\"");
            }
        }
        sb.append("}");
    }
}
