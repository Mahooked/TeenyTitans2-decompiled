package com.google.android.gms.internal;

import java.util.Comparator;

/* JADX INFO: loaded from: classes.dex */
public final class zzhq implements Comparator<zzhe> {
    public zzhq(zzhp zzhpVar) {
    }

    @Override // java.util.Comparator
    public final /* synthetic */ int compare(zzhe zzheVar, zzhe zzheVar2) {
        zzhe zzheVar3 = zzheVar;
        zzhe zzheVar4 = zzheVar2;
        if (zzheVar3.zzcS() < zzheVar4.zzcS()) {
            return -1;
        }
        if (zzheVar3.zzcS() > zzheVar4.zzcS()) {
            return 1;
        }
        if (zzheVar3.zzcR() < zzheVar4.zzcR()) {
            return -1;
        }
        if (zzheVar3.zzcR() > zzheVar4.zzcR()) {
            return 1;
        }
        float fZzcU = (zzheVar3.zzcU() - zzheVar3.zzcS()) * (zzheVar3.zzcT() - zzheVar3.zzcR());
        float fZzcU2 = (zzheVar4.zzcU() - zzheVar4.zzcS()) * (zzheVar4.zzcT() - zzheVar4.zzcR());
        if (fZzcU <= fZzcU2) {
            return fZzcU < fZzcU2 ? 1 : 0;
        }
        return -1;
    }
}
