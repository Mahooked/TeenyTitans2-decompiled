package com.google.android.gms.internal;

import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.util.SparseArray;
import com.google.android.gms.common.internal.zzbo;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public class zzbgt extends zzbgl {
    public static final Parcelable.Creator<zzbgt> CREATOR = new zzbgu();
    private final String mClassName;
    private final zzbgo zzaIP;
    private final Parcel zzaIW;
    private final int zzaIX = 2;
    private int zzaIY;
    private int zzaIZ;
    private final int zzaku;

    zzbgt(int i, Parcel parcel, zzbgo zzbgoVar) {
        this.zzaku = i;
        this.zzaIW = (Parcel) zzbo.zzu(parcel);
        this.zzaIP = zzbgoVar;
        if (this.zzaIP == null) {
            this.mClassName = null;
        } else {
            this.mClassName = this.zzaIP.zzrR();
        }
        this.zzaIY = 2;
    }

    private static void zza(StringBuilder sb, int i, Object obj) {
        switch (i) {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
                sb.append(obj);
                return;
            case 7:
                sb.append("\"").append(com.google.android.gms.common.util.zzo.zzcK(obj.toString())).append("\"");
                return;
            case 8:
                sb.append("\"").append(com.google.android.gms.common.util.zzc.encode((byte[]) obj)).append("\"");
                return;
            case 9:
                sb.append("\"").append(com.google.android.gms.common.util.zzc.zzg((byte[]) obj));
                sb.append("\"");
                return;
            case 10:
                com.google.android.gms.common.util.zzp.zza(sb, (HashMap) obj);
                return;
            case 11:
                throw new IllegalArgumentException("Method does not accept concrete type.");
            default:
                throw new IllegalArgumentException(new StringBuilder(26).append("Unknown type = ").append(i).toString());
        }
    }

    /* JADX WARN: Multi-variable type inference failed */
    private final void zza(StringBuilder sb, zzbgj<?, ?> zzbgjVar, Parcel parcel, int i) {
        double[] dArrCreateDoubleArray = null;
        BigInteger[] bigIntegerArr = null;
        int i2 = 0;
        if (!zzbgjVar.zzaIK) {
            switch (zzbgjVar.zzaIJ) {
                case 0:
                    sb.append(com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i));
                    return;
                case 1:
                    sb.append(com.google.android.gms.common.internal.safeparcel.zzb.zzk(parcel, i));
                    return;
                case 2:
                    sb.append(com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i));
                    return;
                case 3:
                    sb.append(com.google.android.gms.common.internal.safeparcel.zzb.zzl(parcel, i));
                    return;
                case 4:
                    sb.append(com.google.android.gms.common.internal.safeparcel.zzb.zzn(parcel, i));
                    return;
                case 5:
                    sb.append(com.google.android.gms.common.internal.safeparcel.zzb.zzp(parcel, i));
                    return;
                case 6:
                    sb.append(com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i));
                    return;
                case 7:
                    sb.append("\"").append(com.google.android.gms.common.util.zzo.zzcK(com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i))).append("\"");
                    return;
                case 8:
                    sb.append("\"").append(com.google.android.gms.common.util.zzc.encode(com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, i))).append("\"");
                    return;
                case 9:
                    sb.append("\"").append(com.google.android.gms.common.util.zzc.zzg(com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, i)));
                    sb.append("\"");
                    return;
                case 10:
                    Bundle bundleZzs = com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i);
                    Set<String> setKeySet = bundleZzs.keySet();
                    setKeySet.size();
                    sb.append("{");
                    Object[] objArr = true;
                    for (String str : setKeySet) {
                        if (objArr == false) {
                            sb.append(",");
                        }
                        sb.append("\"").append(str).append("\"");
                        sb.append(":");
                        sb.append("\"").append(com.google.android.gms.common.util.zzo.zzcK(bundleZzs.getString(str))).append("\"");
                        objArr = false;
                    }
                    sb.append("}");
                    return;
                case 11:
                    Parcel parcelZzD = com.google.android.gms.common.internal.safeparcel.zzb.zzD(parcel, i);
                    parcelZzD.setDataPosition(0);
                    zza(sb, zzbgjVar.zzrP(), parcelZzD);
                    return;
                default:
                    throw new IllegalStateException("Unknown field type out");
            }
        }
        sb.append("[");
        switch (zzbgjVar.zzaIJ) {
            case 0:
                int[] iArrZzw = com.google.android.gms.common.internal.safeparcel.zzb.zzw(parcel, i);
                int length = iArrZzw.length;
                while (i2 < length) {
                    if (i2 != 0) {
                        sb.append(",");
                    }
                    sb.append(Integer.toString(iArrZzw[i2]));
                    i2++;
                }
                break;
            case 1:
                int iZza = com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i);
                int iDataPosition = parcel.dataPosition();
                if (iZza != 0) {
                    int i3 = parcel.readInt();
                    bigIntegerArr = new BigInteger[i3];
                    while (i2 < i3) {
                        bigIntegerArr[i2] = new BigInteger(parcel.createByteArray());
                        i2++;
                    }
                    parcel.setDataPosition(iZza + iDataPosition);
                }
                com.google.android.gms.common.util.zzb.zza(sb, bigIntegerArr);
                break;
            case 2:
                com.google.android.gms.common.util.zzb.zza(sb, com.google.android.gms.common.internal.safeparcel.zzb.zzx(parcel, i));
                break;
            case 3:
                com.google.android.gms.common.util.zzb.zza(sb, com.google.android.gms.common.internal.safeparcel.zzb.zzy(parcel, i));
                break;
            case 4:
                int iZza2 = com.google.android.gms.common.internal.safeparcel.zzb.zza(parcel, i);
                int iDataPosition2 = parcel.dataPosition();
                if (iZza2 != 0) {
                    dArrCreateDoubleArray = parcel.createDoubleArray();
                    parcel.setDataPosition(iZza2 + iDataPosition2);
                }
                com.google.android.gms.common.util.zzb.zza(sb, dArrCreateDoubleArray);
                break;
            case 5:
                com.google.android.gms.common.util.zzb.zza(sb, com.google.android.gms.common.internal.safeparcel.zzb.zzz(parcel, i));
                break;
            case 6:
                com.google.android.gms.common.util.zzb.zza(sb, com.google.android.gms.common.internal.safeparcel.zzb.zzv(parcel, i));
                break;
            case 7:
                com.google.android.gms.common.util.zzb.zza(sb, com.google.android.gms.common.internal.safeparcel.zzb.zzA(parcel, i));
                break;
            case 8:
            case 9:
            case 10:
                throw new UnsupportedOperationException("List of type BASE64, BASE64_URL_SAFE, or STRING_MAP is not supported");
            case 11:
                Parcel[] parcelArrZzE = com.google.android.gms.common.internal.safeparcel.zzb.zzE(parcel, i);
                int length2 = parcelArrZzE.length;
                for (int i4 = 0; i4 < length2; i4++) {
                    if (i4 > 0) {
                        sb.append(",");
                    }
                    parcelArrZzE[i4].setDataPosition(0);
                    zza(sb, zzbgjVar.zzrP(), parcelArrZzE[i4]);
                }
                break;
            default:
                throw new IllegalStateException("Unknown field type out.");
        }
        sb.append("]");
    }

    private final void zza(StringBuilder sb, Map<String, zzbgj<?, ?>> map, Parcel parcel) {
        SparseArray sparseArray = new SparseArray();
        for (Map.Entry<String, zzbgj<?, ?>> entry : map.entrySet()) {
            sparseArray.put(entry.getValue().zzaIM, entry);
        }
        sb.append('{');
        int iZzd = com.google.android.gms.common.internal.safeparcel.zzb.zzd(parcel);
        boolean z = false;
        while (parcel.dataPosition() < iZzd) {
            int i = parcel.readInt();
            Map.Entry entry2 = (Map.Entry) sparseArray.get(65535 & i);
            if (entry2 != null) {
                if (z) {
                    sb.append(",");
                }
                String str = (String) entry2.getKey();
                zzbgj<?, ?> zzbgjVar = (zzbgj) entry2.getValue();
                sb.append("\"").append(str).append("\":");
                if (zzbgjVar.zzrO()) {
                    switch (zzbgjVar.zzaIJ) {
                        case 0:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, Integer.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzg(parcel, i))));
                            break;
                        case 1:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, com.google.android.gms.common.internal.safeparcel.zzb.zzk(parcel, i)));
                            break;
                        case 2:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, Long.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzi(parcel, i))));
                            break;
                        case 3:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, Float.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzl(parcel, i))));
                            break;
                        case 4:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, Double.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzn(parcel, i))));
                            break;
                        case 5:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, com.google.android.gms.common.internal.safeparcel.zzb.zzp(parcel, i)));
                            break;
                        case 6:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, Boolean.valueOf(com.google.android.gms.common.internal.safeparcel.zzb.zzc(parcel, i))));
                            break;
                        case 7:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, com.google.android.gms.common.internal.safeparcel.zzb.zzq(parcel, i)));
                            break;
                        case 8:
                        case 9:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, com.google.android.gms.common.internal.safeparcel.zzb.zzt(parcel, i)));
                            break;
                        case 10:
                            zzb(sb, zzbgjVar, zza(zzbgjVar, zzo(com.google.android.gms.common.internal.safeparcel.zzb.zzs(parcel, i))));
                            break;
                        case 11:
                            throw new IllegalArgumentException("Method does not accept concrete type.");
                        default:
                            throw new IllegalArgumentException(new StringBuilder(36).append("Unknown field out type = ").append(zzbgjVar.zzaIJ).toString());
                    }
                } else {
                    zza(sb, zzbgjVar, parcel, i);
                }
                z = true;
            }
        }
        if (parcel.dataPosition() != iZzd) {
            throw new com.google.android.gms.common.internal.safeparcel.zzc(new StringBuilder(37).append("Overread allowed size end=").append(iZzd).toString(), parcel);
        }
        sb.append('}');
    }

    private final void zzb(StringBuilder sb, zzbgj<?, ?> zzbgjVar, Object obj) {
        if (!zzbgjVar.zzaII) {
            zza(sb, zzbgjVar.zzaIH, obj);
            return;
        }
        ArrayList arrayList = (ArrayList) obj;
        sb.append("[");
        int size = arrayList.size();
        for (int i = 0; i < size; i++) {
            if (i != 0) {
                sb.append(",");
            }
            zza(sb, zzbgjVar.zzaIH, arrayList.get(i));
        }
        sb.append("]");
    }

    private static HashMap<String, String> zzo(Bundle bundle) {
        HashMap<String, String> map = new HashMap<>();
        for (String str : bundle.keySet()) {
            map.put(str, bundle.getString(str));
        }
        return map;
    }

    private Parcel zzrT() {
        switch (this.zzaIY) {
            case 0:
                this.zzaIZ = com.google.android.gms.common.internal.safeparcel.zzd.zze(this.zzaIW);
            case 1:
                com.google.android.gms.common.internal.safeparcel.zzd.zzI(this.zzaIW, this.zzaIZ);
                this.zzaIY = 2;
                break;
        }
        return this.zzaIW;
    }

    @Override // com.google.android.gms.internal.zzbgi
    public String toString() {
        zzbo.zzb(this.zzaIP, "Cannot convert to JSON on client side.");
        Parcel parcelZzrT = zzrT();
        parcelZzrT.setDataPosition(0);
        StringBuilder sb = new StringBuilder(100);
        zza(sb, this.zzaIP.zzcJ(this.mClassName), parcelZzrT);
        return sb.toString();
    }

    @Override // android.os.Parcelable
    public void writeToParcel(Parcel parcel, int i) {
        zzbgo zzbgoVar;
        int iZze = com.google.android.gms.common.internal.safeparcel.zzd.zze(parcel);
        com.google.android.gms.common.internal.safeparcel.zzd.zzc(parcel, 1, this.zzaku);
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 2, zzrT(), false);
        switch (this.zzaIX) {
            case 0:
                zzbgoVar = null;
                break;
            case 1:
                zzbgoVar = this.zzaIP;
                break;
            case 2:
                zzbgoVar = this.zzaIP;
                break;
            default:
                throw new IllegalStateException(new StringBuilder(34).append("Invalid creation type: ").append(this.zzaIX).toString());
        }
        com.google.android.gms.common.internal.safeparcel.zzd.zza(parcel, 3, (Parcelable) zzbgoVar, i, false);
        com.google.android.gms.common.internal.safeparcel.zzd.zzI(parcel, iZze);
    }

    @Override // com.google.android.gms.internal.zzbgl, com.google.android.gms.internal.zzbgi
    public final Object zzcH(String str) {
        throw new UnsupportedOperationException("Converting to JSON does not require this method.");
    }

    @Override // com.google.android.gms.internal.zzbgl, com.google.android.gms.internal.zzbgi
    public final boolean zzcI(String str) {
        throw new UnsupportedOperationException("Converting to JSON does not require this method.");
    }

    @Override // com.google.android.gms.internal.zzbgi
    public final Map<String, zzbgj<?, ?>> zzrL() {
        if (this.zzaIP == null) {
            return null;
        }
        return this.zzaIP.zzcJ(this.mClassName);
    }
}
