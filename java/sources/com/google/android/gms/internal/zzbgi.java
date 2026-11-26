package com.google.android.gms.internal;

import com.google.android.gms.common.internal.zzbo;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzbgi {
    /* JADX WARN: Multi-variable type inference failed */
    protected static <O, I> I zza(zzbgj<I, O> zzbgjVar, Object obj) {
        return ((zzbgj) zzbgjVar).zzaIQ != null ? zzbgjVar.convertBack(obj) : obj;
    }

    private static void zza(StringBuilder sb, zzbgj zzbgjVar, Object obj) {
        if (zzbgjVar.zzaIH == 11) {
            sb.append(zzbgjVar.zzaIN.cast(obj).toString());
        } else {
            if (zzbgjVar.zzaIH != 7) {
                sb.append(obj);
                return;
            }
            sb.append("\"");
            sb.append(com.google.android.gms.common.util.zzo.zzcK((String) obj));
            sb.append("\"");
        }
    }

    private static void zza(StringBuilder sb, zzbgj zzbgjVar, ArrayList<Object> arrayList) {
        sb.append("[");
        int size = arrayList.size();
        for (int i = 0; i < size; i++) {
            if (i > 0) {
                sb.append(",");
            }
            Object obj = arrayList.get(i);
            if (obj != null) {
                zza(sb, zzbgjVar, obj);
            }
        }
        sb.append("]");
    }

    public String toString() {
        Map<String, zzbgj<?, ?>> mapZzrL = zzrL();
        StringBuilder sb = new StringBuilder(100);
        for (String str : mapZzrL.keySet()) {
            zzbgj<?, ?> zzbgjVar = mapZzrL.get(str);
            if (zza(zzbgjVar)) {
                Object objZza = zza(zzbgjVar, zzb(zzbgjVar));
                if (sb.length() == 0) {
                    sb.append("{");
                } else {
                    sb.append(",");
                }
                sb.append("\"").append(str).append("\":");
                if (objZza != null) {
                    switch (zzbgjVar.zzaIJ) {
                        case 8:
                            sb.append("\"").append(com.google.android.gms.common.util.zzc.encode((byte[]) objZza)).append("\"");
                            break;
                        case 9:
                            sb.append("\"").append(com.google.android.gms.common.util.zzc.zzg((byte[]) objZza)).append("\"");
                            break;
                        case 10:
                            com.google.android.gms.common.util.zzp.zza(sb, (HashMap) objZza);
                            break;
                        default:
                            if (zzbgjVar.zzaII) {
                                zza(sb, (zzbgj) zzbgjVar, (ArrayList<Object>) objZza);
                            } else {
                                zza(sb, zzbgjVar, objZza);
                            }
                            break;
                    }
                } else {
                    sb.append("null");
                }
            }
        }
        if (sb.length() > 0) {
            sb.append("}");
        } else {
            sb.append("{}");
        }
        return sb.toString();
    }

    protected boolean zza(zzbgj zzbgjVar) {
        if (zzbgjVar.zzaIJ != 11) {
            return zzcI(zzbgjVar.zzaIL);
        }
        if (zzbgjVar.zzaIK) {
            String str = zzbgjVar.zzaIL;
            throw new UnsupportedOperationException("Concrete type arrays not supported");
        }
        String str2 = zzbgjVar.zzaIL;
        throw new UnsupportedOperationException("Concrete types not supported");
    }

    protected Object zzb(zzbgj zzbgjVar) {
        String str = zzbgjVar.zzaIL;
        if (zzbgjVar.zzaIN == null) {
            return zzcH(zzbgjVar.zzaIL);
        }
        zzcH(zzbgjVar.zzaIL);
        zzbo.zza(true, "Concrete field shouldn't be value object: %s", zzbgjVar.zzaIL);
        boolean z = zzbgjVar.zzaIK;
        try {
            char upperCase = Character.toUpperCase(str.charAt(0));
            String strValueOf = String.valueOf(str.substring(1));
            return getClass().getMethod(new StringBuilder(String.valueOf(strValueOf).length() + 4).append("get").append(upperCase).append(strValueOf).toString(), new Class[0]).invoke(this, new Object[0]);
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    protected abstract Object zzcH(String str);

    protected abstract boolean zzcI(String str);

    public abstract Map<String, zzbgj<?, ?>> zzrL();
}
