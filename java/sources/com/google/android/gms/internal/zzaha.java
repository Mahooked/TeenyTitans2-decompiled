package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Context;
import android.net.Uri;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
final class zzaha implements zznm {
    private /* synthetic */ List zzZw;
    private /* synthetic */ zznl zzZx;
    private /* synthetic */ Context zztF;

    zzaha(zzagz zzagzVar, List list, zznl zznlVar, Context context) {
        this.zzZw = list;
        this.zzZx = zznlVar;
        this.zztF = context;
    }

    @Override // com.google.android.gms.internal.zznm
    public final void zzea() {
        for (String str : this.zzZw) {
            String strValueOf = String.valueOf(str);
            zzafr.zzaS(strValueOf.length() != 0 ? "Pinging url: ".concat(strValueOf) : new String("Pinging url: "));
            this.zzZx.mayLaunchUrl(Uri.parse(str), null, null);
        }
        this.zzZx.zzc((Activity) this.zztF);
    }

    @Override // com.google.android.gms.internal.zznm
    public final void zzeb() {
    }
}
