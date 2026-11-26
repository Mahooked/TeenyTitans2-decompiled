package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import java.util.Collections;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaai extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzaai> CREATOR = new zzaaj();
    public String body;
    public final int errorCode;
    public final int orientation;
    private int versionCode;
    public final boolean zzAv;
    public final boolean zzAw;
    public final boolean zzAx;
    private zzaae zzMM;
    public final List<String> zzMa;
    public final List<String> zzMb;
    public final List<String> zzMd;
    public final boolean zzMe;
    public final long zzMg;
    public final String zzPi;
    public final boolean zzSH;
    public final boolean zzSV;

    @Nullable
    public String zzSW;
    private zzaau zzTA;
    public String zzTB;
    public final String zzTC;

    @Nullable
    public final zzaee zzTD;

    @Nullable
    public final List<String> zzTE;

    @Nullable
    public final List<String> zzTF;
    public final boolean zzTG;

    @Nullable
    public final zzaak zzTH;

    @Nullable
    public final String zzTI;

    @Nullable
    public final zzaeq zzTJ;

    @Nullable
    public final String zzTK;
    public final boolean zzTL;
    private Bundle zzTM;
    public final boolean zzTh;
    public final long zzTn;
    public final boolean zzTo;
    public final long zzTp;
    public final List<String> zzTq;
    public final String zzTr;
    public final long zzTs;
    public final String zzTt;
    public final boolean zzTu;
    public final String zzTv;
    public final String zzTw;
    public final boolean zzTx;
    public final boolean zzTy;
    public final boolean zzTz;

    public zzaai(int i) {
        this(19, null, null, null, i, null, -1L, false, -1L, null, -1L, -1, null, -1L, null, false, null, null, false, false, false, true, false, null, null, null, false, false, null, null, null, false, null, false, null, null, false, null, null, null, true, false, null);
    }

    public zzaai(int i, long j) {
        this(19, null, null, null, i, null, -1L, false, -1L, null, j, -1, null, -1L, null, false, null, null, false, false, false, true, false, null, null, null, false, false, null, null, null, false, null, false, null, null, false, null, null, null, true, false, null);
    }

    zzaai(int i, String str, String str2, List<String> list, int i2, List<String> list2, long j, boolean z, long j2, List<String> list3, long j3, int i3, String str3, long j4, String str4, boolean z2, String str5, String str6, boolean z3, boolean z4, boolean z5, boolean z6, boolean z7, zzaau zzaauVar, String str7, String str8, boolean z8, boolean z9, zzaee zzaeeVar, List<String> list4, List<String> list5, boolean z10, zzaak zzaakVar, boolean z11, String str9, List<String> list6, boolean z12, String str10, zzaeq zzaeqVar, String str11, boolean z13, boolean z14, Bundle bundle) {
        zzabi zzabiVar;
        this.versionCode = i;
        this.zzPi = str;
        this.body = str2;
        this.zzMa = list != null ? Collections.unmodifiableList(list) : null;
        this.errorCode = i2;
        this.zzMb = list2 != null ? Collections.unmodifiableList(list2) : null;
        this.zzTn = j;
        this.zzTo = z;
        this.zzTp = j2;
        this.zzTq = list3 != null ? Collections.unmodifiableList(list3) : null;
        this.zzMg = j3;
        this.orientation = i3;
        this.zzTr = str3;
        this.zzTs = j4;
        this.zzTt = str4;
        this.zzTu = z2;
        this.zzTv = str5;
        this.zzTw = str6;
        this.zzTx = z3;
        this.zzAv = z4;
        this.zzSH = z5;
        this.zzTy = z6;
        this.zzTL = z13;
        this.zzTz = z7;
        this.zzTA = zzaauVar;
        this.zzTB = str7;
        this.zzTC = str8;
        if (this.body == null && this.zzTA != null && (zzabiVar = (zzabi) this.zzTA.zza(zzabi.CREATOR)) != null && !TextUtils.isEmpty(zzabiVar.zzHg)) {
            this.body = zzabiVar.zzHg;
        }
        this.zzAw = z8;
        this.zzAx = z9;
        this.zzTD = zzaeeVar;
        this.zzTE = list4;
        this.zzTF = list5;
        this.zzTG = z10;
        this.zzTH = zzaakVar;
        this.zzSV = z11;
        this.zzSW = str9;
        this.zzMd = list6;
        this.zzMe = z12;
        this.zzTI = str10;
        this.zzTJ = zzaeqVar;
        this.zzTK = str11;
        this.zzTh = z14;
        this.zzTM = bundle;
    }

    public zzaai(zzaae zzaaeVar, String str, String str2, List<String> list, List<String> list2, long j, boolean z, long j2, List<String> list3, long j3, int i, String str3, long j4, String str4, String str5, boolean z2, boolean z3, boolean z4, boolean z5, boolean z6, String str6, boolean z7, boolean z8, zzaee zzaeeVar, List<String> list4, List<String> list5, boolean z9, zzaak zzaakVar, boolean z10, String str7, List<String> list6, boolean z11, String str8, zzaeq zzaeqVar, String str9, boolean z12, boolean z13) {
        this(19, str, str2, list, -2, list2, j, z, -1L, list3, j3, i, str3, j4, str4, false, null, str5, z2, z3, z4, z5, false, null, null, str6, z7, z8, zzaeeVar, list4, list5, z9, zzaakVar, z10, str7, list6, z11, str8, zzaeqVar, str9, z12, z13, null);
        this.zzMM = zzaaeVar;
    }

    public zzaai(zzaae zzaaeVar, String str, String str2, List<String> list, List<String> list2, long j, boolean z, long j2, List<String> list3, long j3, int i, String str3, long j4, String str4, boolean z2, String str5, String str6, boolean z3, boolean z4, boolean z5, boolean z6, boolean z7, String str7, boolean z8, boolean z9, zzaee zzaeeVar, List<String> list4, List<String> list5, boolean z10, zzaak zzaakVar, boolean z11, String str8, List<String> list6, boolean z12, String str9, zzaeq zzaeqVar, String str10, boolean z13, boolean z14) {
        this(19, str, str2, list, -2, list2, j, z, j2, list3, j3, i, str3, j4, str4, z2, str5, str6, z3, z4, z5, z6, z7, null, null, str7, z8, z9, zzaeeVar, list4, list5, z10, zzaakVar, z11, str8, list6, z12, str9, zzaeqVar, str10, z13, z14, null);
        this.zzMM = zzaaeVar;
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        if (this.zzMM != null && this.zzMM.versionCode >= 9 && !TextUtils.isEmpty(this.body)) {
            this.zzTA = new zzaau(new zzabi(this.body));
            this.body = null;
        }
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.versionCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzPi, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.body, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 4, this.zzMa, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 5, this.errorCode);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 6, this.zzMb, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzTn);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzTo);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 9, this.zzTp);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 10, this.zzTq, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 11, this.zzMg);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 12, this.orientation);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 13, this.zzTr, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 14, this.zzTs);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 15, this.zzTt, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 18, this.zzTu);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 19, this.zzTv, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 21, this.zzTw, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 22, this.zzTx);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 23, this.zzAv);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 24, this.zzSH);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 25, this.zzTy);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 26, this.zzTz);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 28, (Parcelable) this.zzTA, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 29, this.zzTB, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 30, this.zzTC, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 31, this.zzAw);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 32, this.zzAx);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 33, (Parcelable) this.zzTD, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 34, this.zzTE, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 35, this.zzTF, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 36, this.zzTG);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 37, (Parcelable) this.zzTH, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 38, this.zzSV);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 39, this.zzSW, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzb(parcel, 40, this.zzMd, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 42, this.zzMe);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 43, this.zzTI, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 44, (Parcelable) this.zzTJ, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 45, this.zzTK, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 46, this.zzTL);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 47, this.zzTh);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 48, this.zzTM, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
