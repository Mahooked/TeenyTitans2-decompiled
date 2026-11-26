package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class aeh extends adj<aeh> implements Cloneable {
    private int zzbpb = 0;
    private String zzctL = "";
    private String version = "";

    public aeh() {
        this.zzcso = null;
        this.zzcsx = -1;
    }

    /* JADX INFO: Access modifiers changed from: private */
    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzMb, reason: merged with bridge method [inline-methods] */
    public aeh clone() {
        try {
            return (aeh) super.clone();
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof aeh)) {
            return false;
        }
        aeh aehVar = (aeh) obj;
        if (this.zzbpb != aehVar.zzbpb) {
            return false;
        }
        if (this.zzctL == null) {
            if (aehVar.zzctL != null) {
                return false;
            }
        } else if (!this.zzctL.equals(aehVar.zzctL)) {
            return false;
        }
        if (this.version == null) {
            if (aehVar.version != null) {
                return false;
            }
        } else if (!this.version.equals(aehVar.version)) {
            return false;
        }
        if (this.zzcso == null || this.zzcso.isEmpty()) {
            return aehVar.zzcso == null || aehVar.zzcso.isEmpty();
        }
        return this.zzcso.equals(aehVar.zzcso);
    }

    public final int hashCode() {
        int iHashCode = 0;
        int iHashCode2 = ((this.version == null ? 0 : this.version.hashCode()) + (((this.zzctL == null ? 0 : this.zzctL.hashCode()) + ((((getClass().getName().hashCode() + 527) * 31) + this.zzbpb) * 31)) * 31)) * 31;
        if (this.zzcso != null && !this.zzcso.isEmpty()) {
            iHashCode = this.zzcso.hashCode();
        }
        return iHashCode2 + iHashCode;
    }

    @Override // com.google.android.gms.internal.adj
    /* JADX INFO: renamed from: zzLN */
    public final /* synthetic */ adj clone() throws CloneNotSupportedException {
        return (aeh) clone();
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    /* JADX INFO: renamed from: zzLO */
    public final /* synthetic */ adp clone() throws CloneNotSupportedException {
        return (aeh) clone();
    }

    @Override // com.google.android.gms.internal.adp
    public final /* synthetic */ adp zza(adg adgVar) throws IOException {
        while (true) {
            int iZzLA = adgVar.zzLA();
            switch (iZzLA) {
                case 0:
                    break;
                case 8:
                    this.zzbpb = adgVar.zzLC();
                    break;
                case 18:
                    this.zzctL = adgVar.readString();
                    break;
                case 26:
                    this.version = adgVar.readString();
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
        if (this.zzbpb != 0) {
            adhVar.zzr(1, this.zzbpb);
        }
        if (this.zzctL != null && !this.zzctL.equals("")) {
            adhVar.zzl(2, this.zzctL);
        }
        if (this.version != null && !this.version.equals("")) {
            adhVar.zzl(3, this.version);
        }
        super.zza(adhVar);
    }

    @Override // com.google.android.gms.internal.adj, com.google.android.gms.internal.adp
    protected final int zzn() {
        int iZzn = super.zzn();
        if (this.zzbpb != 0) {
            iZzn += adh.zzs(1, this.zzbpb);
        }
        if (this.zzctL != null && !this.zzctL.equals("")) {
            iZzn += adh.zzm(2, this.zzctL);
        }
        return (this.version == null || this.version.equals("")) ? iZzn : iZzn + adh.zzm(3, this.version);
    }
}
