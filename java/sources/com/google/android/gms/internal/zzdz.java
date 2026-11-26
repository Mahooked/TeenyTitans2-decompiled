package com.google.android.gms.internal;

import android.view.View;
import java.lang.reflect.InvocationTargetException;

/* JADX INFO: loaded from: classes.dex */
public final class zzdz extends zzec {
    private final View zzru;

    public zzdz(zzdb zzdbVar, String str, String str2, zzax zzaxVar, int i, int i2, View view) {
        super(zzdbVar, str, str2, zzaxVar, i, 57);
        this.zzru = view;
    }

    @Override // com.google.android.gms.internal.zzec
    protected final void zzT() throws IllegalAccessException, InvocationTargetException {
        if (this.zzru != null) {
            zzdh zzdhVar = new zzdh((String) this.zzrx.invoke(null, this.zzru, this.zzpJ.getContext().getResources().getDisplayMetrics(), Boolean.valueOf(((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzEY)).booleanValue())));
            zzaz zzazVar = new zzaz();
            zzazVar.zzcu = zzdhVar.zzrj;
            zzazVar.zzcv = zzdhVar.zzcv;
            zzazVar.zzcw = zzdhVar.zzcw;
            this.zzro.zzbR = zzazVar;
        }
    }
}
