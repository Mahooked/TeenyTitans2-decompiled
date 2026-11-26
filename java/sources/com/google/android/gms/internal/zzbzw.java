package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbzw extends zzbzu<Boolean> {
    public zzbzw(int i, String str, Boolean bool) {
        super(0, str, bool);
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.zzbzu
    /* JADX INFO: renamed from: zzb, reason: merged with bridge method [inline-methods] */
    public final Boolean zza(zzcac zzcacVar) {
        try {
            return Boolean.valueOf(zzcacVar.getBooleanFlagValue(getKey(), zzdI().booleanValue(), getSource()));
        } catch (RemoteException e) {
            return zzdI();
        }
    }
}
