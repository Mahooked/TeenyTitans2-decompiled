package com.google.android.gms.internal;

import java.io.IOException;

/* JADX INFO: loaded from: classes.dex */
public final class adg {
    private final byte[] buffer;
    private int zzcse;
    private int zzcsf;
    private int zzcsg;
    private int zzcsh;
    private int zzcsi;
    private int zzcsk;
    private int zzcsj = Integer.MAX_VALUE;
    private int zzcsl = 64;
    private int zzcsm = 67108864;

    private adg(byte[] bArr, int i, int i2) {
        this.buffer = bArr;
        this.zzcse = i;
        this.zzcsf = i + i2;
        this.zzcsh = i;
    }

    public static adg zzH(byte[] bArr) {
        return zzb(bArr, 0, bArr.length);
    }

    private final void zzLJ() {
        this.zzcsf += this.zzcsg;
        int i = this.zzcsf;
        if (i <= this.zzcsj) {
            this.zzcsg = 0;
        } else {
            this.zzcsg = i - this.zzcsj;
            this.zzcsf -= this.zzcsg;
        }
    }

    private final byte zzLL() throws IOException {
        if (this.zzcsh == this.zzcsf) {
            throw ado.zzLQ();
        }
        byte[] bArr = this.buffer;
        int i = this.zzcsh;
        this.zzcsh = i + 1;
        return bArr[i];
    }

    public static adg zzb(byte[] bArr, int i, int i2) {
        return new adg(bArr, 0, i2);
    }

    private final void zzcq(int i) throws IOException {
        if (i < 0) {
            throw ado.zzLR();
        }
        if (this.zzcsh + i > this.zzcsj) {
            zzcq(this.zzcsj - this.zzcsh);
            throw ado.zzLQ();
        }
        if (i > this.zzcsf - this.zzcsh) {
            throw ado.zzLQ();
        }
        this.zzcsh += i;
    }

    public final int getPosition() {
        return this.zzcsh - this.zzcse;
    }

    public final byte[] readBytes() throws IOException {
        int iZzLF = zzLF();
        if (iZzLF < 0) {
            throw ado.zzLR();
        }
        if (iZzLF == 0) {
            return ads.zzcsI;
        }
        if (iZzLF > this.zzcsf - this.zzcsh) {
            throw ado.zzLQ();
        }
        byte[] bArr = new byte[iZzLF];
        System.arraycopy(this.buffer, this.zzcsh, bArr, 0, iZzLF);
        this.zzcsh = iZzLF + this.zzcsh;
        return bArr;
    }

    public final String readString() throws IOException {
        int iZzLF = zzLF();
        if (iZzLF < 0) {
            throw ado.zzLR();
        }
        if (iZzLF > this.zzcsf - this.zzcsh) {
            throw ado.zzLQ();
        }
        String str = new String(this.buffer, this.zzcsh, iZzLF, adn.UTF_8);
        this.zzcsh = iZzLF + this.zzcsh;
        return str;
    }

    public final int zzLA() throws IOException {
        if (this.zzcsh == this.zzcsf) {
            this.zzcsi = 0;
            return 0;
        }
        this.zzcsi = zzLF();
        if (this.zzcsi == 0) {
            throw new ado("Protocol message contained an invalid tag (zero).");
        }
        return this.zzcsi;
    }

    public final long zzLB() throws IOException {
        return zzLG();
    }

    public final int zzLC() throws IOException {
        return zzLF();
    }

    public final boolean zzLD() throws IOException {
        return zzLF() != 0;
    }

    public final long zzLE() throws IOException {
        long jZzLG = zzLG();
        return (-(jZzLG & 1)) ^ (jZzLG >>> 1);
    }

    public final int zzLF() throws IOException {
        byte bZzLL = zzLL();
        if (bZzLL >= 0) {
            return bZzLL;
        }
        int i = bZzLL & 127;
        byte bZzLL2 = zzLL();
        if (bZzLL2 >= 0) {
            return i | (bZzLL2 << 7);
        }
        int i2 = i | ((bZzLL2 & 127) << 7);
        byte bZzLL3 = zzLL();
        if (bZzLL3 >= 0) {
            return i2 | (bZzLL3 << 14);
        }
        int i3 = i2 | ((bZzLL3 & 127) << 14);
        byte bZzLL4 = zzLL();
        if (bZzLL4 >= 0) {
            return i3 | (bZzLL4 << 21);
        }
        int i4 = i3 | ((bZzLL4 & 127) << 21);
        byte bZzLL5 = zzLL();
        int i5 = i4 | (bZzLL5 << 28);
        if (bZzLL5 >= 0) {
            return i5;
        }
        for (int i6 = 0; i6 < 5; i6++) {
            if (zzLL() >= 0) {
                return i5;
            }
        }
        throw ado.zzLS();
    }

    public final long zzLG() throws IOException {
        long j = 0;
        for (int i = 0; i < 64; i += 7) {
            byte bZzLL = zzLL();
            j |= ((long) (bZzLL & 127)) << i;
            if ((bZzLL & 128) == 0) {
                return j;
            }
        }
        throw ado.zzLS();
    }

    public final int zzLH() throws IOException {
        return (zzLL() & 255) | ((zzLL() & 255) << 8) | ((zzLL() & 255) << 16) | ((zzLL() & 255) << 24);
    }

    public final long zzLI() throws IOException {
        byte bZzLL = zzLL();
        return ((((long) zzLL()) & 255) << 8) | (((long) bZzLL) & 255) | ((((long) zzLL()) & 255) << 16) | ((((long) zzLL()) & 255) << 24) | ((((long) zzLL()) & 255) << 32) | ((((long) zzLL()) & 255) << 40) | ((((long) zzLL()) & 255) << 48) | ((((long) zzLL()) & 255) << 56);
    }

    public final int zzLK() {
        if (this.zzcsj == Integer.MAX_VALUE) {
            return -1;
        }
        return this.zzcsj - this.zzcsh;
    }

    public final void zza(adp adpVar) throws IOException {
        int iZzLF = zzLF();
        if (this.zzcsk >= this.zzcsl) {
            throw ado.zzLT();
        }
        int iZzcn = zzcn(iZzLF);
        this.zzcsk++;
        adpVar.zza(this);
        zzcl(0);
        this.zzcsk--;
        zzco(iZzcn);
    }

    public final void zza(adp adpVar, int i) throws IOException {
        if (this.zzcsk >= this.zzcsl) {
            throw ado.zzLT();
        }
        this.zzcsk++;
        adpVar.zza(this);
        zzcl((i << 3) | 4);
        this.zzcsk--;
    }

    public final void zzcl(int i) throws ado {
        if (this.zzcsi != i) {
            throw new ado("Protocol message end-group tag did not match expected tag.");
        }
    }

    public final boolean zzcm(int i) throws IOException {
        int iZzLA;
        switch (i & 7) {
            case 0:
                zzLF();
                return true;
            case 1:
                zzLI();
                return true;
            case 2:
                zzcq(zzLF());
                return true;
            case 3:
                break;
            case 4:
                return false;
            case 5:
                zzLH();
                return true;
            default:
                throw new ado("Protocol message tag had invalid wire type.");
        }
        do {
            iZzLA = zzLA();
            if (iZzLA != 0) {
            }
            zzcl(((i >>> 3) << 3) | 4);
            return true;
        } while (zzcm(iZzLA));
        zzcl(((i >>> 3) << 3) | 4);
        return true;
    }

    public final int zzcn(int i) throws ado {
        if (i < 0) {
            throw ado.zzLR();
        }
        int i2 = this.zzcsh + i;
        int i3 = this.zzcsj;
        if (i2 > i3) {
            throw ado.zzLQ();
        }
        this.zzcsj = i2;
        zzLJ();
        return i3;
    }

    public final void zzco(int i) {
        this.zzcsj = i;
        zzLJ();
    }

    public final void zzcp(int i) {
        zzq(i, this.zzcsi);
    }

    public final byte[] zzp(int i, int i2) {
        if (i2 == 0) {
            return ads.zzcsI;
        }
        byte[] bArr = new byte[i2];
        System.arraycopy(this.buffer, this.zzcse + i, bArr, 0, i2);
        return bArr;
    }

    final void zzq(int i, int i2) {
        if (i > this.zzcsh - this.zzcse) {
            throw new IllegalArgumentException(new StringBuilder(50).append("Position ").append(i).append(" is beyond current ").append(this.zzcsh - this.zzcse).toString());
        }
        if (i < 0) {
            throw new IllegalArgumentException(new StringBuilder(24).append("Bad position ").append(i).toString());
        }
        this.zzcsh = this.zzcse + i;
        this.zzcsi = i2;
    }
}
