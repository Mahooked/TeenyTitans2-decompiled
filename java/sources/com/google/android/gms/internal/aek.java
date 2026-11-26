package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class aek extends adj<aek> implements Cloneable {
    private static volatile aek[] zzcuj;
    private String key = "";
    private String value = "";

    public aek() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    public static aek[] zzMe() {
        if (zzcuj == null) {
            synchronized (adn.zzcsw) {
                if (zzcuj == null) {
                    zzcuj = new aek[0];
                }
            }
        }
        return zzcuj;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzMf, reason: merged with bridge method [inline-methods] */
    public aek clone() {
        try {
            return (aek) super.clone();
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof aek)) {
            return false;
        }
        aek aekVar = (aek) obj;
        if (this.key == null) {
            if (aekVar.key != null) {
                return false;
            }
        } else if (!this.key.equals(aekVar.key)) {
            return false;
        }
        if (this.value == null) {
            if (aekVar.value != null) {
                return false;
            }
        } else if (!this.value.equals(aekVar.value)) {
            return false;
        }
        if (this.zzcso == null || this.zzcso.isEmpty()) {
            return aekVar.zzcso == null || aekVar.zzcso.isEmpty();
        }
        return this.zzcso.equals(aekVar.zzcso);
    }

    public final int hashCode() {
        int iHashCode = 0;
        int iHashCode2 = ((this.value == null ? 0 : this.value.hashCode()) + (((this.key == null ? 0 : this.key.hashCode()) + ((getClass().getName().hashCode() + 527) * 31)) * 31)) * 31;
        if (this.zzcso != null && !this.zzcso.isEmpty()) {
            iHashCode = this.zzcso.hashCode();
        }
        return iHashCode2 + iHashCode;
    }

    @Override // com.google.android.gms.internal.adj
    /* JADX INFO: renamed from: zzLN */
    public final /* synthetic */ adj clone() throws CloneNotSupportedException {
        return (aek) clone();
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLO */
    public final /* synthetic */ adp clone() throws CloneNotSupportedException {
        return (aek) clone();
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 10:
                    this.key = adgVar.readString();
                    break;
                case 18:
                    this.value = adgVar.readString();
                    break;
                default:
                    if (!super.zza(adgVar, iZzLA)) {
                    }
                    break;
            }
        }
        return this;
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    public final void zza(adh adhVar) throws IOException {
        if (this.key != null && !this.key.equals("")) {
            adhVar.zzl(1, this.key);
        }
        if (this.value != null && !this.value.equals("")) {
            adhVar.zzl(2, this.value);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.key != null && !this.key.equals("")) {
            iZzn += adh.zzm(1, this.key);
        }
        return (this.value == null || this.value.equals("")) ? iZzn : iZzn + adh.zzm(2, this.value);
    }
}
