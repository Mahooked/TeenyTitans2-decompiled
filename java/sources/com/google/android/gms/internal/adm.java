package com.google.android.gms.internal;

import java.io.IOException;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Iterator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
final class adm implements Cloneable {
    private Object value;
    private adk<?, ?> zzcsu;
    private List<adr> zzcsv = new ArrayList();

    adm() {
    }

    private final byte[] toByteArray() throws IOException {
        byte[] bArr = new byte[zzn()];
        zza(adh.zzI(bArr));
        return bArr;
    }

    /* JADX INFO: Access modifiers changed from: private */
    /* JADX INFO: renamed from: zzLP, reason: merged with bridge method [inline-methods] */
    public adm clone() {
        int i = 0;
        adm admVar = new adm();
        try {
            admVar.zzcsu = this.zzcsu;
            if (this.zzcsv == null) {
                admVar.zzcsv = null;
            } else {
                admVar.zzcsv.addAll(this.zzcsv);
            }
            if (this.value != null) {
                if (this.value instanceof adp) {
                    admVar.value = (adp) ((adp) this.value).clone();
                } else if (this.value instanceof byte[]) {
                    admVar.value = ((byte[]) this.value).clone();
                } else if (this.value instanceof byte[][]) {
                    byte[][] bArr = (byte[][]) this.value;
                    byte[][] bArr2 = new byte[bArr.length][];
                    admVar.value = bArr2;
                    for (int i2 = 0; i2 < bArr.length; i2++) {
                        bArr2[i2] = (byte[]) bArr[i2].clone();
                    }
                } else if (this.value instanceof boolean[]) {
                    admVar.value = ((boolean[]) this.value).clone();
                } else if (this.value instanceof int[]) {
                    admVar.value = ((int[]) this.value).clone();
                } else if (this.value instanceof long[]) {
                    admVar.value = ((long[]) this.value).clone();
                } else if (this.value instanceof float[]) {
                    admVar.value = ((float[]) this.value).clone();
                } else if (this.value instanceof double[]) {
                    admVar.value = ((double[]) this.value).clone();
                } else if (this.value instanceof adp[]) {
                    adp[] adpVarArr = (adp[]) this.value;
                    adp[] adpVarArr2 = new adp[adpVarArr.length];
                    admVar.value = adpVarArr2;
                    while (true) {
                        int i3 = i;
                        if (i3 >= adpVarArr.length) {
                            break;
                        }
                        adpVarArr2[i3] = (adp) adpVarArr[i3].clone();
                        i = i3 + 1;
                    }
                }
            }
            return admVar;
        } catch (CloneNotSupportedException e) {
            throw new AssertionError(e);
        }
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof adm)) {
            return false;
        }
        adm admVar = (adm) obj;
        if (this.value == null || admVar.value == null) {
            if (this.zzcsv != null && admVar.zzcsv != null) {
                return this.zzcsv.equals(admVar.zzcsv);
            }
            try {
                return Arrays.equals(toByteArray(), admVar.toByteArray());
            } catch (IOException e) {
                throw new IllegalStateException(e);
            }
        }
        if (this.zzcsu != admVar.zzcsu) {
            return false;
        }
        if (!this.zzcsu.zzcjG.isArray()) {
            return this.value.equals(admVar.value);
        }
        if (this.value instanceof byte[]) {
            return Arrays.equals((byte[]) this.value, (byte[]) admVar.value);
        }
        if (this.value instanceof int[]) {
            return Arrays.equals((int[]) this.value, (int[]) admVar.value);
        }
        if (this.value instanceof long[]) {
            return Arrays.equals((long[]) this.value, (long[]) admVar.value);
        }
        if (this.value instanceof float[]) {
            return Arrays.equals((float[]) this.value, (float[]) admVar.value);
        }
        if (this.value instanceof double[]) {
            return Arrays.equals((double[]) this.value, (double[]) admVar.value);
        }
        return this.value instanceof boolean[] ? Arrays.equals((boolean[]) this.value, (boolean[]) admVar.value) : Arrays.deepEquals((Object[]) this.value, (Object[]) admVar.value);
    }

    public final int hashCode() {
        try {
            return Arrays.hashCode(toByteArray()) + 527;
        } catch (IOException e) {
            throw new IllegalStateException(e);
        }
    }

    final void zza(adh adhVar) throws IOException {
        if (this.value != null) {
            this.zzcsu.zza(this.value, adhVar);
            return;
        }
        for (adr adrVar : this.zzcsv) {
            adhVar.zzcu(adrVar.tag);
            adhVar.zzK(adrVar.zzbws);
        }
    }

    final void zza(adr adrVar) {
        this.zzcsv.add(adrVar);
    }

    /* JADX WARN: Multi-variable type inference failed */
    final <T> T zzb(adk<?, T> adkVar) {
        if (this.value == null) {
            this.zzcsu = adkVar;
            this.value = adkVar.zzX(this.zzcsv);
            this.zzcsv = null;
        } else if (!this.zzcsu.equals(adkVar)) {
            throw new IllegalStateException("Tried to getExtension with a different Extension.");
        }
        return (T) this.value;
    }

    final int zzn() {
        int length = 0;
        if (this.value != null) {
            return this.zzcsu.zzav(this.value);
        }
        Iterator<adr> it = this.zzcsv.iterator();
        while (true) {
            int i = length;
            if (!it.hasNext()) {
                return i;
            }
            adr next = it.next();
            length = next.zzbws.length + adh.zzcv(next.tag) + 0 + i;
        }
    }
}
