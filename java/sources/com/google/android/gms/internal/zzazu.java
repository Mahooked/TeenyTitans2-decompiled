package com.google.android.gms.internal;

import android.os.Parcel;
import android.os.Parcelable;
import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
public final class zzazu extends com.google.android.gms.common.internal.safeparcel.zza {
    public static final Parcelable.Creator<zzazu> CREATOR = new zzazv();
    private boolean zzazA;
    public final aej zzazB;
    public zzbak zzazE;
    public byte[] zzazF;
    private int[] zzazG;
    private String[] zzazH;
    private int[] zzazI;
    private byte[][] zzazJ;
    private zzcqn[] zzazK;
    public final zzazr zzazL;
    public final zzazr zzazu;

    public zzazu(zzbak zzbakVar, aej aejVar, zzazr zzazrVar, zzazr zzazrVar2, int[] iArr, String[] strArr, int[] iArr2, byte[][] bArr, zzcqn[] zzcqnVarArr, boolean z) {
        this.zzazE = zzbakVar;
        this.zzazB = aejVar;
        this.zzazu = zzazrVar;
        this.zzazL = null;
        this.zzazG = iArr;
        this.zzazH = null;
        this.zzazI = iArr2;
        this.zzazJ = null;
        this.zzazK = null;
        this.zzazA = z;
    }

    zzazu(zzbak zzbakVar, byte[] bArr, int[] iArr, String[] strArr, int[] iArr2, byte[][] bArr2, boolean z, zzcqn[] zzcqnVarArr) {
        this.zzazE = zzbakVar;
        this.zzazF = bArr;
        this.zzazG = iArr;
        this.zzazH = strArr;
        this.zzazB = null;
        this.zzazu = null;
        this.zzazL = null;
        this.zzazI = iArr2;
        this.zzazJ = bArr2;
        this.zzazK = zzcqnVarArr;
        this.zzazA = z;
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof zzazu)) {
            return false;
        }
        zzazu zzazuVar = (zzazu) obj;
        return com.google.android.gms.common.internal.zzbe.equal(this.zzazE, zzazuVar.zzazE) && Arrays.equals(this.zzazF, zzazuVar.zzazF) && Arrays.equals(this.zzazG, zzazuVar.zzazG) && Arrays.equals(this.zzazH, zzazuVar.zzazH) && com.google.android.gms.common.internal.zzbe.equal(this.zzazB, zzazuVar.zzazB) && com.google.android.gms.common.internal.zzbe.equal(this.zzazu, zzazuVar.zzazu) && com.google.android.gms.common.internal.zzbe.equal(this.zzazL, zzazuVar.zzazL) && Arrays.equals(this.zzazI, zzazuVar.zzazI) && Arrays.deepEquals(this.zzazJ, zzazuVar.zzazJ) && Arrays.equals(this.zzazK, zzazuVar.zzazK) && this.zzazA == zzazuVar.zzazA;
    }

    public final int hashCode() {
        return Arrays.hashCode(new Object[]{this.zzazE, this.zzazF, this.zzazG, this.zzazH, this.zzazB, this.zzazu, this.zzazL, this.zzazI, this.zzazJ, this.zzazK, Boolean.valueOf(this.zzazA)});
    }

    public final String toString() {
        return "LogEventParcelable[" + this.zzazE + ", LogEventBytes: " + (this.zzazF == null ? null : new String(this.zzazF)) + ", TestCodes: " + Arrays.toString(this.zzazG) + ", MendelPackages: " + Arrays.toString(this.zzazH) + ", LogEvent: " + this.zzazB + ", ExtensionProducer: " + this.zzazu + ", VeProducer: " + this.zzazL + ", ExperimentIDs: " + Arrays.toString(this.zzazI) + ", ExperimentTokens: " + Arrays.toString(this.zzazJ) + ", ExperimentTokensParcelables: " + Arrays.toString(this.zzazK) + ", AddPhenotypeExperimentTokens: " + this.zzazA + "]";
    }

    @Override // android.os.Parcelable
    public final void writeToParcel(Parcel parcel, int i) {
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, (Parcelable) this.zzazE, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, this.zzazF, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 4, this.zzazG, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 5, this.zzazH, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 6, this.zzazI, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 7, this.zzazJ, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 8, this.zzazA);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 9, (Parcelable[]) this.zzazK, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }
}
