package com.google.android.gms.internal;

import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class zzaih extends zzar {
    private /* synthetic */ byte[] zzaah;
    private /* synthetic */ Map zzaai;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    zzaih(zzaie zzaieVar, int i, String str, zzv zzvVar, zzu zzuVar, byte[] bArr, Map map) {
        super(i, str, zzvVar, zzuVar);
        this.zzaah = bArr;
        this.zzaai = map;
    }

    @Override // com.google.android.gms.internal.zzp
    public final Map<String, String> getHeaders() throws zza {
        return this.zzaai == null ? super.getHeaders() : this.zzaai;
    }

    @Override // com.google.android.gms.internal.zzp
    public final byte[] zzg() throws zza {
        return this.zzaah == null ? super.zzg() : this.zzaah;
    }
}
