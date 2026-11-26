package com.google.android.gms.common.internal;

import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzbg {
    private final List<String> zzaIh;
    private final Object zzaaw;

    private zzbg(Object obj) {
        this.zzaaw = zzbo.zzu(obj);
        this.zzaIh = new ArrayList();
    }

    public final String toString() {
        StringBuilder sbAppend = new StringBuilder(100).append(this.zzaaw.getClass().getSimpleName()).append('{');
        int size = this.zzaIh.size();
        for (int i = 0; i < size; i++) {
            sbAppend.append(this.zzaIh.get(i));
            if (i < size - 1) {
                sbAppend.append(", ");
            }
        }
        return sbAppend.append('}').toString();
    }

    public final zzbg zzg(String str, Object obj) {
        List<String> list = this.zzaIh;
        String str2 = (String) zzbo.zzu(str);
        String strValueOf = String.valueOf(String.valueOf(obj));
        list.add(new StringBuilder(String.valueOf(str2).length() + 1 + String.valueOf(strValueOf).length()).append(str2).append("=").append(strValueOf).toString());
        return this;
    }
}
