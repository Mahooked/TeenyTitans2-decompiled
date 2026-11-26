package com.google.android.gms.internal;

import java.util.Comparator;

/* JADX INFO: loaded from: classes.dex */
final class zzhh implements Comparator<zzhn> {
    zzhh(zzhg zzhgVar) {
    }

    @Override // java.util.Comparator
    public final /* synthetic */ int compare(zzhn zzhnVar, zzhn zzhnVar2) {
        zzhn zzhnVar3 = zzhnVar;
        zzhn zzhnVar4 = zzhnVar2;
        int i = zzhnVar3.zzzf - zzhnVar4.zzzf;
        return i != 0 ? i : (int) (zzhnVar3.value - zzhnVar4.value);
    }
}
