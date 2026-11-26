package com.google.android.gms.internal;

import java.util.Random;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzjj extends zzkl {
    private Object mLock = new Object();
    private final Random zzAO = new Random();
    private long zzAP;

    public zzjj() {
        zzdu();
    }

    @Override // com.google.android.gms.internal.zzkk
    public final long getValue() {
        return this.zzAP;
    }

    public final void zzdu() {
        synchronized (this.mLock) {
            int i = 3;
            long jNextInt = 0;
            while (true) {
                i--;
                if (i <= 0) {
                    break;
                }
                jNextInt = ((long) this.zzAO.nextInt()) + 2147483648L;
                if (jNextInt != this.zzAP && jNextInt != 0) {
                    break;
                }
            }
            this.zzAP = jNextInt;
        }
    }
}
