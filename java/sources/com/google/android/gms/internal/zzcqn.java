package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import android.util.Base64;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class zzcqn extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzcqn> CREATOR = new zzcqt();
    private static byte[][] zzazi = new byte[0][];
    private static zzcqn zzbAc = new zzcqn("", null, zzazi, zzazi, zzazi, zzazi, null, null);
    private static final zzcqs zzbAl = new zzcqo();
    private static final zzcqs zzbAm = new zzcqp();
    private static final zzcqs zzbAn = new zzcqq();
    private static final zzcqs zzbAo = new zzcqr();
    private String zzbAd;
    private byte[] zzbAe;
    private byte[][] zzbAf;
    private byte[][] zzbAg;
    private byte[][] zzbAh;
    private byte[][] zzbAi;
    private int[] zzbAj;
    private byte[][] zzbAk;

    public zzcqn(String str, byte[] bArr, byte[][] bArr2, byte[][] bArr3, byte[][] bArr4, byte[][] bArr5, int[] iArr, byte[][] bArr6) {
        this.zzbAd = str;
        this.zzbAe = bArr;
        this.zzbAf = bArr2;
        this.zzbAg = bArr3;
        this.zzbAh = bArr4;
        this.zzbAi = bArr5;
        this.zzbAj = iArr;
        this.zzbAk = bArr6;
    }

    private static void zza(StringBuilder sb, String str, int[] iArr) {
        sb.append(str);
        sb.append("=");
        if (iArr == null) {
            sb.append("null");
            return;
        }
        sb.append("(");
        int length = iArr.length;
        boolean z = true;
        int i = 0;
        while (i < length) {
            int i2 = iArr[i];
            if (!z) {
                sb.append(", ");
            }
            sb.append(i2);
            i++;
            z = false;
        }
        sb.append(")");
    }

    private static void zza(StringBuilder sb, String str, byte[][] bArr) {
        sb.append(str);
        sb.append("=");
        if (bArr == null) {
            sb.append("null");
            return;
        }
        sb.append("(");
        int length = bArr.length;
        boolean z = true;
        int i = 0;
        while (i < length) {
            byte[] bArr2 = bArr[i];
            if (!z) {
                sb.append(", ");
            }
            sb.append("'");
            sb.append(Base64.encodeToString(bArr2, 3));
            sb.append("'");
            i++;
            z = false;
        }
        sb.append(")");
    }

    private static List<String> zzb(byte[][] bArr) {
        if (bArr == null) {
            return Collections.emptyList();
        }
        ArrayList arrayList = new ArrayList(bArr.length);
        for (byte[] bArr2 : bArr) {
            arrayList.add(Base64.encodeToString(bArr2, 3));
        }
        Collections.sort(arrayList);
        return arrayList;
    }

    private static List<Integer> zzc(int[] iArr) {
        if (iArr == null) {
            return Collections.emptyList();
        }
        ArrayList arrayList = new ArrayList(iArr.length);
        for (int i : iArr) {
            arrayList.add(Integer.valueOf(i));
        }
        Collections.sort(arrayList);
        return arrayList;
    }

    public final boolean equals(Object obj) {
        if (!(obj instanceof zzcqn)) {
            return false;
        }
        zzcqn zzcqnVar = (zzcqn) obj;
        return zzcqu.equals(this.zzbAd, zzcqnVar.zzbAd) && Arrays.equals(this.zzbAe, zzcqnVar.zzbAe) && zzcqu.equals(zzb(this.zzbAf), zzb(zzcqnVar.zzbAf)) && zzcqu.equals(zzb(this.zzbAg), zzb(zzcqnVar.zzbAg)) && zzcqu.equals(zzb(this.zzbAh), zzb(zzcqnVar.zzbAh)) && zzcqu.equals(zzb(this.zzbAi), zzb(zzcqnVar.zzbAi)) && zzcqu.equals(zzc(this.zzbAj), zzc(zzcqnVar.zzbAj)) && zzcqu.equals(zzb(this.zzbAk), zzb(zzcqnVar.zzbAk));
    }

    public final String toString() {
        String string;
        StringBuilder sb = new StringBuilder("ExperimentTokens");
        sb.append("(");
        if (this.zzbAd == null) {
            string = "null";
        } else {
            String strValueOf = String.valueOf("'");
            String str = this.zzbAd;
            String strValueOf2 = String.valueOf("'");
            string = new StringBuilder(String.valueOf(strValueOf).length() + String.valueOf(str).length() + String.valueOf(strValueOf2).length()).append(strValueOf).append(str).append(strValueOf2).toString();
        }
        sb.append(string);
        sb.append(", ");
        byte[] bArr = this.zzbAe;
        sb.append("direct");
        sb.append("=");
        if (bArr == null) {
            sb.append("null");
        } else {
            sb.append("'");
            sb.append(Base64.encodeToString(bArr, 3));
            sb.append("'");
        }
        sb.append(", ");
        zza(sb, "GAIA", this.zzbAf);
        sb.append(", ");
        zza(sb, "PSEUDO", this.zzbAg);
        sb.append(", ");
        zza(sb, "ALWAYS", this.zzbAh);
        sb.append(", ");
        zza(sb, "OTHER", this.zzbAi);
        sb.append(", ");
        zza(sb, "weak", this.zzbAj);
        sb.append(", ");
        zza(sb, "directs", this.zzbAk);
        sb.append(")");
        return sb.toString();
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, this.zzbAd, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzbAe, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzbAf, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzbAg, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, this.zzbAh, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzbAi, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzbAj, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 9, this.zzbAk, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
