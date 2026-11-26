package com.google.android.gms.common;

import android.os.RemoteException;
import android.util.Log;
import com.google.android.gms.common.internal.zzar;
import com.google.android.gms.common.internal.zzas;
import com.google.android.gms.common.internal.zzbo;
import com.google.android.gms.dynamic.IObjectWrapper;
import java.io.UnsupportedEncodingException;
import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
abstract class zzg extends zzas {
    private int zzaAg;

    protected zzg(byte[] bArr) {
        if (bArr.length != 25) {
            int length = bArr.length;
            String strValueOf = String.valueOf(com.google.android.gms.common.util.zzl.zza(bArr, 0, bArr.length, false));
            Log.wtf("GoogleCertificates", new StringBuilder(String.valueOf(strValueOf).length() + 51).append("Cert hash data has incorrect length (").append(length).append("):\n").append(strValueOf).toString(), new Exception());
            bArr = Arrays.copyOfRange(bArr, 0, 25);
            zzbo.zzb(bArr.length == 25, new StringBuilder(55).append("cert hash data has incorrect length. length=").append(bArr.length).toString());
        }
        this.zzaAg = Arrays.hashCode(bArr);
    }

    protected static byte[] zzcs(String str) {
        try {
            return str.getBytes("ISO-8859-1");
        } catch (UnsupportedEncodingException e) {
            throw new AssertionError(e);
        }
    }

    public boolean equals(Object obj) {
        IObjectWrapper iObjectWrapperZzoY;
        if (obj == null || !(obj instanceof zzar)) {
            return false;
        }
        try {
            zzar zzarVar = (zzar) obj;
            if (zzarVar.zzoZ() == hashCode() && (iObjectWrapperZzoY = zzarVar.zzoY()) != null) {
                return Arrays.equals(getBytes(), (byte[]) com.google.android.gms.dynamic.zzn.zzE(iObjectWrapperZzoY));
            }
            return false;
        } catch (RemoteException e) {
            Log.e("GoogleCertificates", "Failed to get Google certificates from remote", e);
            return false;
        }
    }

    abstract byte[] getBytes();

    public int hashCode() {
        return this.zzaAg;
    }

    @Override // com.google.android.gms.common.internal.zzar
    public final IObjectWrapper zzoY() {
        return com.google.android.gms.dynamic.zzn.zzw(getBytes());
    }

    @Override // com.google.android.gms.common.internal.zzar
    public final int zzoZ() {
        return hashCode();
    }
}
