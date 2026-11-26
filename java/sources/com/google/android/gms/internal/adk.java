package com.google.android.gms.internal;

import com.google.android.gms.internal.adj;
import java.io.IOException;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
public final class adk<M extends adj<M>, T> {
    public final int tag;
    protected final Class<T> zzcjG;
    private int type = 11;
    protected final boolean zzcsp = false;

    private adk(int i, Class<T> cls, int i2, boolean z) {
        this.zzcjG = cls;
        this.tag = i2;
    }

    public static <M extends adj<M>, T extends adp> adk<M, T> zza(int i, Class<T> cls, long j) {
        return new adk<>(11, cls, (int) j, false);
    }

    private final Object zzb(adg adgVar) {
        Class<T> cls = this.zzcjG;
        try {
            switch (this.type) {
                case 10:
                    adp adpVar = (adp) cls.newInstance();
                    adgVar.zza(adpVar, this.tag >>> 3);
                    return adpVar;
                case 11:
                    adp adpVar2 = (adp) cls.newInstance();
                    adgVar.zza(adpVar2);
                    return adpVar2;
                default:
                    throw new IllegalArgumentException(new StringBuilder(24).append("Unknown type ").append(this.type).toString());
            }
        } catch (IOException e) {
            throw new IllegalArgumentException("Error reading extension field", e);
        } catch (IllegalAccessException e2) {
            String strValueOf = String.valueOf(cls);
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(strValueOf).length() + 33).append("Error creating instance of class ").append(strValueOf).toString(), e2);
        } catch (InstantiationException e3) {
            String strValueOf2 = String.valueOf(cls);
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(strValueOf2).length() + 33).append("Error creating instance of class ").append(strValueOf2).toString(), e3);
        }
    }

    public final boolean equals(Object obj) {
        if (this == obj) {
            return true;
        }
        if (!(obj instanceof adk)) {
            return false;
        }
        adk adkVar = (adk) obj;
        return this.type == adkVar.type && this.zzcjG == adkVar.zzcjG && this.tag == adkVar.tag;
    }

    public final int hashCode() {
        return (((((this.type + 1147) * 31) + this.zzcjG.hashCode()) * 31) + this.tag) * 31;
    }

    final T zzX(List<adr> list) {
        if (list == null || list.isEmpty()) {
            return null;
        }
        return this.zzcjG.cast(zzb(adg.zzH(list.get(list.size() - 1).zzbws)));
    }

    protected final void zza(Object obj, adh adhVar) {
        try {
            adhVar.zzcu(this.tag);
            switch (this.type) {
                case 10:
                    int i = this.tag >>> 3;
                    ((adp) obj).zza(adhVar);
                    adhVar.zzt(i, 4);
                    return;
                case 11:
                    adhVar.zzb((adp) obj);
                    return;
                default:
                    throw new IllegalArgumentException(new StringBuilder(24).append("Unknown type ").append(this.type).toString());
            }
        } catch (IOException e) {
            throw new IllegalStateException(e);
        }
    }

    protected final int zzav(Object obj) {
        int i = this.tag >>> 3;
        switch (this.type) {
            case 10:
                return (adh.zzct(i) << 1) + ((adp) obj).zzLV();
            case 11:
                return adh.zzb(i, (adp) obj);
            default:
                throw new IllegalArgumentException(new StringBuilder(24).append("Unknown type ").append(this.type).toString());
        }
    }
}
