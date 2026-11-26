package com.google.android.gms.ads.internal.js;

import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzais;
import com.google.android.gms.internal.zzcu;

/* JADX INFO: loaded from: classes.dex */
final class zzm implements Runnable {
    final /* synthetic */ zzcu zzLf;
    final /* synthetic */ zzac zzLg;
    final /* synthetic */ zzl zzLh;

    zzm(zzl zzlVar, zzcu zzcuVar, zzac zzacVar) {
        this.zzLh = zzlVar;
        this.zzLf = zzcuVar;
        this.zzLg = zzacVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            zze zzeVar = new zze(this.zzLh.mContext, this.zzLh.zztW, this.zzLf, null);
            zzeVar.zza(new zzn(this, zzeVar));
            zzeVar.zza("/jsLoaded", new zzq(this, zzeVar));
            zzais zzaisVar = new zzais();
            zzr zzrVar = new zzr(this, zzeVar, zzaisVar);
            zzaisVar.set(zzrVar);
            zzeVar.zza("/requestReload", zzrVar);
            if (this.zzLh.zzLa.endsWith(".js")) {
                zzeVar.zzac(this.zzLh.zzLa);
            } else if (this.zzLh.zzLa.startsWith("<html>")) {
                zzeVar.zzae(this.zzLh.zzLa);
            } else {
                zzeVar.zzad(this.zzLh.zzLa);
            }
            zzagz.zzZr.postDelayed(new zzs(this, zzeVar), zzw.zzLp);
        } catch (Throwable th) {
            zzafr.zzb("Error creating webview.", th);
            zzbs.zzbD().zza(th, "SdkJavascriptFactory.loadJavascriptEngine");
            this.zzLg.reject();
        }
    }
}
