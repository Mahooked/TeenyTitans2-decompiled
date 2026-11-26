package com.google.android.gms.internal;

import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public final class zzbzz extends zzbzu<String> {
    public zzbzz(int i, String str, String str2) {
        super(0, str, str2);
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.zzbzu
    /* JADX INFO: renamed from: zze, reason: merged with bridge method [inline-methods] */
    public final String zza(zzcac zzcacVar) {
        try {
            return zzcacVar.getStringFlagValue(getKey(), zzdI(), getSource());
        } catch (RemoteException e) {
            return zzdI();
        }
    }
}
