package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.os.IBinder;
import android.os.Parcel;
import android.os.RemoteException;
import com.google.android.gms.internal.zzed;
import com.google.android.gms.internal.zzef;

/* JADX INFO: loaded from: classes.dex */
public final class zzan extends zzed implements zzal {
    zzan(IBinder iBinder) {
        super(iBinder, "com.google.android.gms.common.internal.IAccountAccessor");
    }

    @Override // com.google.android.gms.common.internal.zzal
    public final Account getAccount() throws RemoteException {
        Parcel parcelZza = zza(2, zzZ());
        Account account = (Account) zzef.zza(parcelZza, Account.CREATOR);
        parcelZza.recycle();
        return account;
    }
}
