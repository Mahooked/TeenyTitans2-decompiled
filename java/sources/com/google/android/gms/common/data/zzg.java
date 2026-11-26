package com.google.android.gms.common.data;

import java.util.ArrayList;

/* JADX INFO: loaded from: classes.dex */
public abstract class zzg<T> extends AbstractDataBuffer<T> {
    private boolean zzaFO;
    private ArrayList<Integer> zzaFP;

    protected zzg(DataHolder dataHolder) {
        super(dataHolder);
        this.zzaFO = false;
    }

    private final int zzaw(int i) {
        if (i < 0 || i >= this.zzaFP.size()) {
            throw new IllegalArgumentException(new StringBuilder(53).append("Position ").append(i).append(" is out of bounds for this buffer").toString());
        }
        return this.zzaFP.get(i).intValue();
    }

    private final void zzqT() {
        synchronized (this) {
            if (!this.zzaFO) {
                int i = this.zzaCX.zzaFG;
                this.zzaFP = new ArrayList<>();
                if (i > 0) {
                    this.zzaFP.add(0);
                    String strZzqS = zzqS();
                    String strZzd = this.zzaCX.zzd(strZzqS, 0, this.zzaCX.zzat(0));
                    int i2 = 1;
                    while (i2 < i) {
                        int iZzat = this.zzaCX.zzat(i2);
                        String strZzd2 = this.zzaCX.zzd(strZzqS, i2, iZzat);
                        if (strZzd2 == null) {
                            throw new NullPointerException(new StringBuilder(String.valueOf(strZzqS).length() + 78).append("Missing value for markerColumn: ").append(strZzqS).append(", at row: ").append(i2).append(", for window: ").append(iZzat).toString());
                        }
                        if (strZzd2.equals(strZzd)) {
                            strZzd2 = strZzd;
                        } else {
                            this.zzaFP.add(Integer.valueOf(i2));
                        }
                        i2++;
                        strZzd = strZzd2;
                    }
                }
                this.zzaFO = true;
            }
        }
    }

    @Override // com.google.android.gms.common.data.AbstractDataBuffer, com.google.android.gms.common.data.DataBuffer
    public final T get(int i) {
        int iIntValue;
        zzqT();
        int iZzaw = zzaw(i);
        if (i < 0 || i == this.zzaFP.size()) {
            iIntValue = 0;
        } else {
            iIntValue = i == this.zzaFP.size() + (-1) ? this.zzaCX.zzaFG - this.zzaFP.get(i).intValue() : this.zzaFP.get(i + 1).intValue() - this.zzaFP.get(i).intValue();
            if (iIntValue == 1) {
                this.zzaCX.zzat(zzaw(i));
            }
        }
        return zzi(iZzaw, iIntValue);
    }

    @Override // com.google.android.gms.common.data.AbstractDataBuffer, com.google.android.gms.common.data.DataBuffer
    public int getCount() {
        zzqT();
        return this.zzaFP.size();
    }

    protected abstract T zzi(int i, int i2);

    protected abstract String zzqS();
}
