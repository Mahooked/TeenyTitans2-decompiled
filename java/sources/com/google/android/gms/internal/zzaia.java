package com.google.android.gms.internal;

import java.util.ArrayList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzaia {
    private final String[] zzZS;
    private final double[] zzZT;
    private final double[] zzZU;
    private final int[] zzZV;
    private int zzZW;

    private zzaia(zzaid zzaidVar) {
        int size = zzaidVar.zzaab.size();
        this.zzZS = (String[]) zzaidVar.zzaaa.toArray(new String[size]);
        this.zzZT = zzo(zzaidVar.zzaab);
        this.zzZU = zzo(zzaidVar.zzaac);
        this.zzZV = new int[size];
        this.zzZW = 0;
    }

    private static double[] zzo(List<Double> list) {
        double[] dArr = new double[list.size()];
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= dArr.length) {
                return dArr;
            }
            dArr[i2] = list.get(i2).doubleValue();
            i = i2 + 1;
        }
    }

    public final List<zzaic> getBuckets() {
        ArrayList arrayList = new ArrayList(this.zzZS.length);
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= this.zzZS.length) {
                return arrayList;
            }
            arrayList.add(new zzaic(this.zzZS[i2], this.zzZU[i2], this.zzZT[i2], ((double) this.zzZV[i2]) / ((double) this.zzZW), this.zzZV[i2]));
            i = i2 + 1;
        }
    }

    public final void zza(double d) {
        this.zzZW++;
        for (int i = 0; i < this.zzZU.length; i++) {
            if (this.zzZU[i] <= d && d < this.zzZT[i]) {
                int[] iArr = this.zzZV;
                iArr[i] = iArr[i] + 1;
            }
            if (d < this.zzZU[i]) {
                return;
            }
        }
    }
}
