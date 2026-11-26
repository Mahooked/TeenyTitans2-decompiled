package com.google.android.gms.internal;

import android.content.ContentResolver;
import android.content.Context;
import android.util.Log;
import com.google.android.gms.common.internal.zzbo;
import java.nio.ByteBuffer;
import java.nio.charset.Charset;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public final class zzbah implements zzazq {
    private static final Charset UTF_8 = Charset.forName(HTTP.UTF_8);
    static Boolean zzazO = null;
    private zzbai zzazP;

    public zzbah(Context context) {
        this(new zzbai(context));
    }

    private zzbah(zzbai zzbaiVar) {
        this.zzazP = (zzbai) zzbo.zzu(zzbaiVar);
    }

    private static zzbaj zzcr(String str) {
        int i = 0;
        if (str == null) {
            return null;
        }
        String strSubstring = "";
        int iIndexOf = str.indexOf(44);
        if (iIndexOf >= 0) {
            strSubstring = str.substring(0, iIndexOf);
            i = iIndexOf + 1;
        }
        int iIndexOf2 = str.indexOf(47, i);
        if (iIndexOf2 <= 0) {
            String strValueOf = String.valueOf(str);
            Log.e("LogSamplerImpl", strValueOf.length() != 0 ? "Failed to parse the rule: ".concat(strValueOf) : new String("Failed to parse the rule: "));
            return null;
        }
        try {
            long j = Long.parseLong(str.substring(i, iIndexOf2));
            long j2 = Long.parseLong(str.substring(iIndexOf2 + 1));
            if (j >= 0 && j2 >= 0) {
                return new zzbaj(strSubstring, j, j2);
            }
            Log.e("LogSamplerImpl", new StringBuilder(72).append("negative values not supported: ").append(j).append("/").append(j2).toString());
            return null;
        } catch (NumberFormatException e) {
            String strValueOf2 = String.valueOf(str);
            Log.e("LogSamplerImpl", strValueOf2.length() != 0 ? "parseLong() failed while parsing: ".concat(strValueOf2) : new String("parseLong() failed while parsing: "), e);
            return null;
        }
    }

    @Override // com.google.android.gms.internal.zzazq
    public final boolean zzg(String str, int i) {
        String strZza;
        long jZzf;
        long j;
        if (str == null || str.isEmpty()) {
            str = i >= 0 ? String.valueOf(i) : null;
        }
        if (str == null) {
            return true;
        }
        zzbai zzbaiVar = this.zzazP;
        long j2 = zzbaiVar.zzazQ == null ? 0L : hi.getLong(zzbaiVar.zzazQ, "android_id", 0L);
        zzbai zzbaiVar2 = this.zzazP;
        if (zzbaiVar2.zzazQ == null) {
            strZza = null;
        } else {
            ContentResolver contentResolver = zzbaiVar2.zzazQ;
            String strValueOf = String.valueOf("gms:playlog:service:sampling_");
            String strValueOf2 = String.valueOf(str);
            strZza = hi.zza(contentResolver, strValueOf2.length() != 0 ? strValueOf.concat(strValueOf2) : new String(strValueOf), (String) null);
        }
        zzbaj zzbajVarZzcr = zzcr(strZza);
        if (zzbajVarZzcr == null) {
            return true;
        }
        String str2 = zzbajVarZzcr.zzazR;
        if (str2 == null || str2.isEmpty()) {
            jZzf = zzbac.zzf(ByteBuffer.allocate(8).putLong(j2).array());
        } else {
            byte[] bytes = str2.getBytes(UTF_8);
            ByteBuffer byteBufferAllocate = ByteBuffer.allocate(bytes.length + 8);
            byteBufferAllocate.put(bytes);
            byteBufferAllocate.putLong(j2);
            jZzf = zzbac.zzf(byteBufferAllocate.array());
        }
        long j3 = zzbajVarZzcr.zzazS;
        long j4 = zzbajVarZzcr.zzazT;
        if (j3 < 0 || j4 < 0) {
            throw new IllegalArgumentException(new StringBuilder(72).append("negative values not supported: ").append(j3).append("/").append(j4).toString());
        }
        if (j4 > 0) {
            if (jZzf >= 0) {
                j = jZzf % j4;
            } else {
                j = (((jZzf & Long.MAX_VALUE) % j4) + ((Long.MAX_VALUE % j4) + 1)) % j4;
            }
            if (j < j3) {
                return true;
            }
        }
        return false;
    }
}
