package com.google.android.gms.ads.internal.js;

import android.content.Context;
import android.support.annotation.Nullable;
import com.google.android.gms.ads.internal.zzbs;
import com.google.android.gms.internal.zzagz;
import com.google.android.gms.internal.zzahz;
import com.google.android.gms.internal.zzaje;
import com.google.android.gms.internal.zzcu;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzl {
    private final Context mContext;
    private final Object mLock;
    private final String zzLa;
    private zzahz<zza> zzLb;
    private zzahz<zza> zzLc;

    @Nullable
    private zzac zzLd;
    private int zzLe;
    private final zzaje zztW;

    public zzl(Context context, zzaje zzajeVar, String str) {
        this.mLock = new Object();
        this.zzLe = 1;
        this.zzLa = str;
        this.mContext = context.getApplicationContext();
        this.zztW = zzajeVar;
        this.zzLb = new zzx();
        this.zzLc = new zzx();
    }

    public zzl(Context context, zzaje zzajeVar, String str, zzahz<zza> zzahzVar, zzahz<zza> zzahzVar2) {
        this(context, zzajeVar, str);
        this.zzLb = zzahzVar;
        this.zzLc = zzahzVar2;
    }

    protected final zzac zza(@Nullable zzcu zzcuVar) {
        zzac zzacVar = new zzac(this.zzLc);
        zzbs.zzbz();
        zzagz.runOnUiThread(new zzm(this, zzcuVar, zzacVar));
        zzacVar.zza(new zzu(this, zzacVar), new zzv(this, zzacVar));
        return zzacVar;
    }

    public final zzy zzb(@Nullable zzcu zzcuVar) {
        zzy zzyVarZzfa;
        synchronized (this.mLock) {
            if (this.zzLd == null || this.zzLd.getStatus() == -1) {
                this.zzLe = 2;
                this.zzLd = zza(zzcuVar);
                zzyVarZzfa = this.zzLd.zzfa();
            } else if (this.zzLe == 0) {
                zzyVarZzfa = this.zzLd.zzfa();
            } else if (this.zzLe == 1) {
                this.zzLe = 2;
                zza(zzcuVar);
                zzyVarZzfa = this.zzLd.zzfa();
            } else {
                zzyVarZzfa = this.zzLe == 2 ? this.zzLd.zzfa() : this.zzLd.zzfa();
            }
        }
        return zzyVarZzfa;
    }
}
