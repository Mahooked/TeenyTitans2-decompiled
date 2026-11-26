package com.google.android.gms.internal;

import android.os.RemoteException;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
final class zzyt implements zzajl<List<zznp>, zznn> {
    private /* synthetic */ String zzRL;
    private /* synthetic */ Integer zzRM;
    private /* synthetic */ Integer zzRN;
    private /* synthetic */ int zzRO;
    private /* synthetic */ int zzRP;
    private /* synthetic */ int zzRQ;
    private /* synthetic */ int zzRR;
    private /* synthetic */ boolean zzRS;

    zzyt(zzyn zzynVar, String str, Integer num, Integer num2, int i, int i2, int i3, int i4, boolean z) {
        this.zzRL = str;
        this.zzRM = num;
        this.zzRN = num2;
        this.zzRO = i;
        this.zzRP = i2;
        this.zzRQ = i3;
        this.zzRR = i4;
        this.zzRS = z;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.zzajl
    /* JADX INFO: renamed from: zzl, reason: merged with bridge method [inline-methods] */
    public final zznn apply(List<zznp> list) {
        if (list != null) {
            try {
                if (!list.isEmpty()) {
                    return new zznn(this.zzRL, zzyn.zzj(list), this.zzRM, this.zzRN, this.zzRO > 0 ? Integer.valueOf(this.zzRO) : null, this.zzRP + this.zzRQ, this.zzRR, this.zzRS);
                }
            } catch (RemoteException e) {
                zzafr.zzb("Could not get attribution icon", e);
                return null;
            }
        }
        return null;
    }
}
