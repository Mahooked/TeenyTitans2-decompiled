package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbzy extends zzbzu<Long> {
    public zzbzy(int i, String str, Long l) {
        super(0, str, l);
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.zzbzu
    /* JADX INFO: renamed from: zzd, reason: merged with bridge method [inline-methods] */
    public final Long zza(zzcac zzcacVar) {
        try {
            return Long.valueOf(zzcacVar.getLongFlagValue(getKey(), zzdI().longValue(), getSource()));
        } catch (RemoteException e) {
            return zzdI();
        }
    }
}
