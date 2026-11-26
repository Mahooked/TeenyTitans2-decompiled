package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;

/* JADX INFO: loaded from: classes.dex */
public final class zzbp extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzbp> CREATOR = new zzbq();
    private final int zzaIo;
    private final GoogleSignInAccount zzaIp;
    private final Account zzajb;
    private int zzaku;

    zzbp(int i, Account account, int i2, GoogleSignInAccount googleSignInAccount) {
        this.zzaku = i;
        this.zzajb = account;
        this.zzaIo = i2;
        this.zzaIp = googleSignInAccount;
    }

    public zzbp(Account account, int i, GoogleSignInAccount googleSignInAccount) {
        this(2, account, i, googleSignInAccount);
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzajb, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzaIo);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, (Parcelable) this.zzaIp, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
