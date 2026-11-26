package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.text.TextUtils;
import java.util.LinkedHashMap;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import org.apache.commons.io.FilenameUtils;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zznb {
    private boolean zzGI;
    private String zzHb;
    private zzmz zzHc;

    @Nullable
    private zznb zzHd;
    private final List<zzmz> zzGZ = new LinkedList();
    private final Map<String, String> zzHa = new LinkedHashMap();
    private final Object mLock = new Object();

    public zznb(boolean z, String str, String str2) {
        this.zzGI = z;
        this.zzHa.put("action", str);
        this.zzHa.put("ad_format", str2);
    }

    public final void zzO(String str) {
        if (this.zzGI) {
            synchronized (this.mLock) {
                this.zzHb = str;
            }
        }
    }

    public final boolean zza(zzmz zzmzVar, long j, String... strArr) {
        synchronized (this.mLock) {
            for (String str : strArr) {
                this.zzGZ.add(new zzmz(j, str, zzmzVar));
            }
        }
        return true;
    }

    public final boolean zza(@Nullable zzmz zzmzVar, String... strArr) {
        if (!this.zzGI || zzmzVar == null) {
            return false;
        }
        return zza(zzmzVar, com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime(), strArr);
    }

    @Nullable
    public final zzmz zzc(long j) {
        if (this.zzGI) {
            return new zzmz(j, null, null);
        }
        return null;
    }

    public final void zzc(@Nullable zznb zznbVar) {
        synchronized (this.mLock) {
            this.zzHd = zznbVar;
        }
    }

    public final zzmz zzdS() {
        return zzc(com.google.android.gms.ads.internal.zzbs.zzbF().elapsedRealtime());
    }

    public final void zzdT() {
        synchronized (this.mLock) {
            this.zzHc = zzdS();
        }
    }

    public final String zzdU() {
        String string;
        StringBuilder sb = new StringBuilder();
        synchronized (this.mLock) {
            for (zzmz zzmzVar : this.zzGZ) {
                long time = zzmzVar.getTime();
                String strZzdP = zzmzVar.zzdP();
                zzmz zzmzVarZzdQ = zzmzVar.zzdQ();
                if (zzmzVarZzdQ != null && time > 0) {
                    sb.append(strZzdP).append(FilenameUtils.EXTENSION_SEPARATOR).append(time - zzmzVarZzdQ.getTime()).append(',');
                }
            }
            this.zzGZ.clear();
            if (!TextUtils.isEmpty(this.zzHb)) {
                sb.append(this.zzHb);
            } else if (sb.length() > 0) {
                sb.setLength(sb.length() - 1);
            }
            string = sb.toString();
        }
        return string;
    }

    final Map<String, String> zzdV() {
        Map<String, String> mapZza;
        synchronized (this.mLock) {
            zzmr zzmrVarZzhr = com.google.android.gms.ads.internal.zzbs.zzbD().zzhr();
            mapZza = (zzmrVarZzhr == null || this.zzHd == null) ? this.zzHa : zzmrVarZzhr.zza(this.zzHa, this.zzHd.zzdV());
        }
        return mapZza;
    }

    public final zzmz zzdW() {
        zzmz zzmzVar;
        synchronized (this.mLock) {
            zzmzVar = this.zzHc;
        }
        return zzmzVar;
    }

    public final void zzh(String str, String str2) {
        zzmr zzmrVarZzhr;
        if (!this.zzGI || TextUtils.isEmpty(str2) || (zzmrVarZzhr = com.google.android.gms.ads.internal.zzbs.zzbD().zzhr()) == null) {
            return;
        }
        synchronized (this.mLock) {
            zzmv zzmvVarZzM = zzmrVarZzhr.zzM(str);
            Map<String, String> map = this.zzHa;
            map.put(str, zzmvVarZzM.zzg(map.get(str), str2));
        }
    }
}
