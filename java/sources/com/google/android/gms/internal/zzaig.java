package com.google.android.gms.internal;

import org.apache.commons.io.IOUtils;

/* JADX INFO: loaded from: classes.dex */
final class zzaig implements zzu {
    private /* synthetic */ zzain zzaag;
    private /* synthetic */ String zzsD;

    zzaig(zzaie zzaieVar, String str, zzain zzainVar) {
        this.zzsD = str;
        this.zzaag = zzainVar;
    }

    @Override // com.google.android.gms.internal.zzu
    public final void zzd(zzaa zzaaVar) {
        String str = this.zzsD;
        String strValueOf = String.valueOf(zzaaVar.toString());
        zzafr.zzaT(new StringBuilder(String.valueOf(str).length() + 21 + String.valueOf(strValueOf).length()).append("Failed to load URL: ").append(str).append(IOUtils.LINE_SEPARATOR_UNIX).append(strValueOf).toString());
        this.zzaag.zzb((Object) null);
    }
}
