package com.google.android.gms.internal;

/* JADX INFO: loaded from: classes.dex */
public final class adl implements Cloneable {
    private static final adm zzcsq = new adm();
    private int mSize;
    private boolean zzcsr;
    private int[] zzcss;
    private adm[] zzcst;

    adl() {
        this(10);
    }

    private adl(int i) {
        this.zzcsr = false;
        int iIdealIntArraySize = idealIntArraySize(i);
        this.zzcss = new int[iIdealIntArraySize];
        this.zzcst = new adm[iIdealIntArraySize];
        this.mSize = 0;
    }

    private static int idealIntArraySize(int i) {
        int i2 = i << 2;
        for (int i3 = 4; i3 < 32; i3++) {
            if (i2 <= (1 << i3) - 12) {
                i2 = (1 << i3) - 12;
                break;
            }
        }
        return i2 / 4;
    }

    private final int zzcz(int i) {
        int i2 = 0;
        int i3 = this.mSize - 1;
        while (i2 <= i3) {
            int i4 = (i2 + i3) >>> 1;
            int i5 = this.zzcss[i4];
            if (i5 < i) {
                i2 = i4 + 1;
            } else {
                if (i5 <= i) {
                    return i4;
                }
                i3 = i4 - 1;
            }
        }
        return i2 ^ (-1);
    }

    public final /* synthetic */ Object clone() throws CloneNotSupportedException {
        int i = this.mSize;
        adl adlVar = new adl(i);
        System.arraycopy(this.zzcss, 0, adlVar.zzcss, 0, i);
        for (int i2 = 0; i2 < i; i2++) {
            if (this.zzcst[i2] != null) {
                adlVar.zzcst[i2] = (adm) this.zzcst[i2].clone();
            }
        }
        adlVar.mSize = i;
        return adlVar;
    }

    public final boolean equals(Object obj) {
        boolean z;
        boolean z2;
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof adl)) {
            return false;
        }
        adl adlVar = (adl) obj;
        if (this.mSize != adlVar.mSize) {
            return false;
        }
        int[] iArr = this.zzcss;
        int[] iArr2 = adlVar.zzcss;
        int i = this.mSize;
        int i2 = 0;
        while (true) {
            if (i2 >= i) {
                z = true;
                break;
            }
            if (iArr[i2] != iArr2[i2]) {
                z = false;
                break;
            }
            i2++;
        }
        if (z) {
            adm[] admVarArr = this.zzcst;
            adm[] admVarArr2 = adlVar.zzcst;
            int i3 = this.mSize;
            int i4 = 0;
            while (true) {
                if (i4 >= i3) {
                    z2 = true;
                    break;
                }
                if (!admVarArr[i4].equals(admVarArr2[i4])) {
                    z2 = false;
                    break;
                }
                i4++;
            }
            if (z2) {
                return true;
            }
        }
        return false;
    }

    public final int hashCode() {
        int iHashCode = 17;
        for (int i = 0; i < this.mSize; i++) {
            iHashCode = (((iHashCode * 31) + this.zzcss[i]) * 31) + this.zzcst[i].hashCode();
        }
        return iHashCode;
    }

    public final boolean isEmpty() {
        return this.mSize == 0;
    }

    final int size() {
        return this.mSize;
    }

    final void zza(int i, adm admVar) {
        int iZzcz = zzcz(i);
        if (iZzcz >= 0) {
            this.zzcst[iZzcz] = admVar;
            return;
        }
        int i2 = iZzcz ^ (-1);
        if (i2 < this.mSize && this.zzcst[i2] == zzcsq) {
            this.zzcss[i2] = i;
            this.zzcst[i2] = admVar;
            return;
        }
        if (this.mSize >= this.zzcss.length) {
            int iIdealIntArraySize = idealIntArraySize(this.mSize + 1);
            int[] iArr = new int[iIdealIntArraySize];
            adm[] admVarArr = new adm[iIdealIntArraySize];
            System.arraycopy(this.zzcss, 0, iArr, 0, this.zzcss.length);
            System.arraycopy(this.zzcst, 0, admVarArr, 0, this.zzcst.length);
            this.zzcss = iArr;
            this.zzcst = admVarArr;
        }
        if (this.mSize - i2 != 0) {
            System.arraycopy(this.zzcss, i2, this.zzcss, i2 + 1, this.mSize - i2);
            System.arraycopy(this.zzcst, i2, this.zzcst, i2 + 1, this.mSize - i2);
        }
        this.zzcss[i2] = i;
        this.zzcst[i2] = admVar;
        this.mSize++;
    }

    final adm zzcx(int i) {
        int iZzcz = zzcz(i);
        if (iZzcz < 0 || this.zzcst[iZzcz] == zzcsq) {
            return null;
        }
        return this.zzcst[iZzcz];
    }

    final adm zzcy(int i) {
        return this.zzcst[i];
    }
}
