package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzacq extends zzacw {
    private final String zzVB;
    private final int zzWp;

    public zzacq(String str, int i) {
        this.zzVB = str;
        this.zzWp = i;
    }

    public final boolean equals(Object obj) {
        if (obj == null || !(obj instanceof zzacq)) {
            return false;
        }
        zzacq zzacqVar = (zzacq) obj;
        return com.google.android.gms.common.internal.zzbe.equal(this.zzVB, zzacqVar.zzVB) && com.google.android.gms.common.internal.zzbe.equal(Integer.valueOf(this.zzWp), Integer.valueOf(zzacqVar.zzWp));
    }

    @Override // com.google.android.gms.internal.zzacv
    public final int getAmount() {
        return this.zzWp;
    }

    @Override // com.google.android.gms.internal.zzacv
    public final String getType() {
        return this.zzVB;
    }
}
