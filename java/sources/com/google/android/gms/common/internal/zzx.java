package com.google.android.gms.common.internal;

import android.accounts.Account;
import android.os.Bundle;
import android.os.IBinder;
import android.os.IInterface;
import android.os.Parcel;
import android.os.Parcelable;
import com.google.android.gms.common.api.Scope;

/* JADX INFO: loaded from: classes.dex */
public final class zzx extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzx> CREATOR = new zzy();
    private int version;
    Account zzaHA;
    com.google.android.gms.common.zzc[] zzaHB;
    private int zzaHu;
    private int zzaHv;
    String zzaHw;
    IBinder zzaHx;
    Scope[] zzaHy;
    Bundle zzaHz;

    public zzx(int i) {
        this.version = 3;
        this.zzaHv = com.google.android.gms.common.zze.GOOGLE_PLAY_SERVICES_VERSION_CODE;
        this.zzaHu = i;
    }

    zzx(int i, int i2, int i3, String str, IBinder iBinder, Scope[] scopeArr, Bundle bundle, Account account, com.google.android.gms.common.zzc[] zzcVarArr) {
        Account accountZza = null;
        zzal zzanVar = null;
        this.version = i;
        this.zzaHu = i2;
        this.zzaHv = i3;
        if ("com.google.android.gms".equals(str)) {
            this.zzaHw = "com.google.android.gms";
        } else {
            this.zzaHw = str;
        }
        if (i < 2) {
            if (iBinder != null) {
                if (iBinder != null) {
                    IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.google.android.gms.common.internal.IAccountAccessor");
                    zzanVar = iInterfaceQueryLocalInterface instanceof zzal ? (zzal) iInterfaceQueryLocalInterface : new zzan(iBinder);
                }
                accountZza = zza.zza(zzanVar);
            }
            this.zzaHA = accountZza;
        } else {
            this.zzaHx = iBinder;
            this.zzaHA = account;
        }
        this.zzaHy = scopeArr;
        this.zzaHz = bundle;
        this.zzaHB = zzcVarArr;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.version);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 2, this.zzaHu);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 3, this.zzaHv);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzaHw, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzaHx, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, (Parcelable[]) this.zzaHy, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzaHz, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, (Parcelable) this.zzaHA, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 10, (Parcelable[]) this.zzaHB, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
