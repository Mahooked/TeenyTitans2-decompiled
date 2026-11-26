package com.google.android.gms.internal;

import java.util.Arrays;

/* JADX INFO: loaded from: classes.dex */
final class adr {
    final int tag;
    final byte[] zzbws;

    adr(int i, byte[] bArr) {
        this.tag = i;
        this.zzbws = bArr;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof adr)) {
            return false;
        }
        adr adrVar = (adr) obj;
        return this.tag == adrVar.tag && Arrays.equals(this.zzbws, adrVar.zzbws);
    }

    public final int hashCode() {
        return ((this.tag + 527) * 31) + Arrays.hashCode(this.zzbws);
    }
}
