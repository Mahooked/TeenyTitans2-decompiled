package com.google.android.gms.internal;

import com.google.android.gms.internal.adj;
import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public abstract class adj<M extends adj<M>> extends adp {
    protected adl zzcso;

    @Override // com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLN, reason: merged with bridge method [inline-methods] */
    public M clone() throws CloneNotSupportedException {
        M m = (M) super.clone();
        adn.zza(this, m);
        return m;
    }

    @Override // com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLO */
    public /* synthetic */ adp clone() throws CloneNotSupportedException {
        return (adj) clone();
    }

    public final <T> T zza(adk<M, T> adkVar) {
        adm admVarZzcx;
        if (this.zzcso == null || (admVarZzcx = this.zzcso.zzcx(adkVar.tag >>> 3)) == null) {
            return null;
        }
        return (T) admVarZzcx.zzb(adkVar);
    }

    @Override // com.google.android.gms.internal.adp
    public void zza(adh adhVar) throws IOException {
        if (this.zzcso == null) {
            return;
        }
        for (int i = 0; i < this.zzcso.size(); i++) {
            this.zzcso.zzcy(i).zza(adhVar);
        }
    }

    protected final boolean zza(adg adgVar, int i) throws IOException {
        int position = adgVar.getPosition();
        if (!adgVar.zzcm(i)) {
            return false;
        }
        int i2 = i >>> 3;
        adr adrVar = new adr(i, adgVar.zzp(position, adgVar.getPosition() - position));
        adm admVarZzcx = null;
        if (this.zzcso == null) {
            this.zzcso = new adl();
        } else {
            admVarZzcx = this.zzcso.zzcx(i2);
        }
        if (admVarZzcx == null) {
            admVarZzcx = new adm();
            this.zzcso.zza(i2, admVarZzcx);
        }
        admVarZzcx.zza(adrVar);
        return true;
    }

    @Override // com.google.android.gms.internal.adp
    protected int zzn() {
        if (this.zzcso == null) {
            return 0;
        }
        int iZzn = 0;
        for (int i = 0; i < this.zzcso.size(); i++) {
            iZzn += this.zzcso.zzcy(i).zzn();
        }
        return iZzn;
    }
}
