package com.google.android.gms.ads.internal;

import android.os.RemoteException;
import com.google.android.gms.internal.zzaet;
import com.google.android.gms.internal.zzafg;
import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzakm;
import com.google.android.gms.internal.zznb;
import com.google.android.gms.internal.zznc;
import com.google.android.gms.internal.zzxx;

/* JADX INFO: loaded from: classes.dex */
final class zzl implements Runnable {
    final /* synthetic */ zzafg zzsW;
    final /* synthetic */ zzi zztb;
    final /* synthetic */ zzaet zztc;
    private /* synthetic */ zznb zztd;

    zzl(zzi zziVar, zzafg zzafgVar, zzaet zzaetVar, zznb zznbVar) {
        this.zztb = zziVar;
        this.zzsW = zzafgVar;
        this.zztc = zzaetVar;
        this.zztd = zznbVar;
    }

    @Override // java.lang.Runnable
    public final void run() {
        if (this.zzsW.zzXY.zzTx && this.zztb.zzsP.zzwo != null) {
            String strZzaI = null;
            if (this.zzsW.zzXY.zzPi != null) {
                zzbs.zzbz();
                strZzaI = zzagz.zzaI(this.zzsW.zzXY.zzPi);
            }
            zznc zzncVar = new zznc(this.zztb, strZzaI, this.zzsW.zzXY.body);
            this.zztb.zzsP.zzwt = 1;
            try {
                this.zztb.zzsN = false;
                this.zztb.zzsP.zzwo.zza(zzncVar);
                return;
            } catch (RemoteException e) {
                zzafr.zzc("Could not call the onCustomRenderedAdLoadedListener.", e);
                this.zztb.zzsN = true;
            }
        }
        zzw zzwVar = new zzw(this.zztc);
        try {
            zzaka zzakaVarZza = this.zztb.zza(this.zzsW, zzwVar, this.zztc);
            zzakaVarZza.setOnTouchListener(new zzn(this, zzwVar));
            zzakaVarZza.setOnClickListener(new zzo(this, zzwVar));
            this.zztb.zzsP.zzwt = 0;
            zzbt zzbtVar = this.zztb.zzsP;
            zzbs.zzby();
            zzbtVar.zzvW = zzxx.zza(this.zztb.zzsP.zzqD, this.zztb, this.zzsW, this.zztb.zzsP.zzvS, zzakaVarZza, this.zztb.zzsX, this.zztb, this.zztd);
        } catch (zzakm e2) {
            zzafr.zzb("Could not obtain webview.", e2);
            zzagz.zzZr.post(new zzm(this));
        }
    }
}
