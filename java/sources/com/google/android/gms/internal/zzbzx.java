package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbzx extends zzbzu<Integer> {
    public zzbzx(int i, String str, Integer num) {
        super(0, str, num);
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.zzbzu
    /* JADX INFO: renamed from: zzc, reason: merged with bridge method [inline-methods] */
    public final Integer zza(zzcac zzcacVar) {
        try {
            return Integer.valueOf(zzcacVar.getIntFlagValue(getKey(), zzdI().intValue(), getSource()));
        } catch (RemoteException e) {
            return zzdI();
        }
    }
}
