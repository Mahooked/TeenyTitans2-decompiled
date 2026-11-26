package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public abstract class adp {
    protected volatile int zzcsx = -1;

    public static final <T extends adp> T zza(T t, byte[] bArr) throws ado {
        return (T) zza(t, bArr, 0, bArr.length);
    }

    private static <T extends adp> T zza(T t, byte[] bArr, int i, int i2) throws ado {
        try {
            adg adgVarZzb = adg.zzb(bArr, 0, i2);
            t.zza(adgVarZzb);
            adgVarZzb.zzcl(0);
            return t;
        } catch (ado e) {
            throw e;
        } catch (IOException e2) {
            throw new RuntimeException("Reading from a byte array threw an IOException (should never happen).");
        }
    }

    public static final byte[] zzc(adp adpVar) {
        byte[] bArr = new byte[adpVar.zzLV()];
        try {
            adh adhVarZzc = adh.zzc(bArr, 0, bArr.length);
            adpVar.zza(adhVarZzc);
            adhVarZzc.zzLM();
            return bArr;
        } catch (IOException e) {
            throw new RuntimeException("Serializing to a byte array threw an IOException (should never happen).", e);
        }
    }

    public String toString() {
        return adq.zzd(this);
    }

    @Override // 
    /* JADX INFO: renamed from: zzLO, reason: merged with bridge method [inline-methods] */
    public adp clone() throws CloneNotSupportedException {
        return (adp) super.clone();
    }

    public final int zzLU() {
        if (this.zzcsx < 0) {
            zzLV();
        }
        return this.zzcsx;
    }

    public final int zzLV() {
        int iZzn = zzn();
        this.zzcsx = iZzn;
        return iZzn;
    }

    public abstract adp zza(adg adgVar) throws IOException;

    public void zza(adh adhVar) throws IOException {
    }

    protected int zzn() {
        return 0;
    }
}
