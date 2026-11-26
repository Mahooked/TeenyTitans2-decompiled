package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.os.Binder;
import android.os.RemoteException;
import android.util.Log;

/* JADX INFO: loaded from: classes.dex */
public final class zza extends zzam {
    private int zzaGG;

    public static Account zza(zzal zzalVar) {
        Account account = null;
        if (zzalVar != null) {
            long jClearCallingIdentity = Binder.clearCallingIdentity();
            try {
                account = zzalVar.getAccount();
            } catch (RemoteException e) {
                Log.w("AccountAccessor", "Remote account accessor probably died");
            } finally {
                Binder.restoreCallingIdentity(jClearCallingIdentity);
            }
        }
        return account;
    }

    public final boolean equals(Object obj) {
        Account account = null;
        if (this == obj) {
            return true;
        }
        if (obj instanceof zza) {
            return account.equals(null);
        }
        return false;
    }

    @Override // com.google.android.gms.common.internal.zzal
    public final Account getAccount() {
        int callingUid = Binder.getCallingUid();
        if (callingUid != this.zzaGG) {
            if (!com.google.android.gms.common.zzo.zzf(null, callingUid)) {
                throw new SecurityException("Caller is not GooglePlayServices");
            }
            this.zzaGG = callingUid;
        }
        return null;
    }
}
