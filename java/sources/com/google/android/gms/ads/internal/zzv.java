package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzaek;
import com.google.android.gms.internal.zzaeu;
import com.google.android.gms.internal.zzij;
import com.google.android.gms.internal.zzik;
import com.google.android.gms.internal.zzql;
import com.google.android.gms.internal.zzsg;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzv {
    public final zzsg zztm;
    public final com.google.android.gms.ads.internal.overlay.zzz zztn;
    public final zzaeu zzto;
    public final zzij zztp;

    private zzv(zzsg zzsgVar, com.google.android.gms.ads.internal.overlay.zzz zzzVar, zzaeu zzaeuVar, zzij zzijVar) {
        this.zztm = zzsgVar;
        this.zztn = zzzVar;
        this.zzto = zzaeuVar;
        this.zztp = zzijVar;
    }

    public static zzv zzaQ() {
        return new zzv(new zzql(), new com.google.android.gms.ads.internal.overlay.zzad(), new zzaek(), new zzik());
    }
}
