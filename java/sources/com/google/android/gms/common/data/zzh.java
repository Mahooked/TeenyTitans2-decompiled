package com.google.android.gms.common.data;

import java.util.NoSuchElementException;

/* JADX INFO: loaded from: classes.dex */
public final class zzh<T> extends zzb<T> {
    private T zzaFQ;

    public zzh(DataBuffer<T> dataBuffer) {
        super(dataBuffer);
    }

    @Override // com.google.android.gms.common.data.zzb, java.util.Iterator
    public final T next() {
        if (!hasNext()) {
            throw new NoSuchElementException(new StringBuilder(46).append("Cannot advance the iterator beyond ").append(this.zzaFv).toString());
        }
        this.zzaFv++;
        if (this.zzaFv == 0) {
            this.zzaFQ = this.zzaFu.get(0);
            if (!(this.zzaFQ instanceof zzc)) {
                String strValueOf = String.valueOf(this.zzaFQ.getClass());
                throw new IllegalStateException(new StringBuilder(String.valueOf(strValueOf).length() + 44).append("DataBuffer reference of type ").append(strValueOf).append(" is not movable").toString());
            }
        } else {
            ((zzc) this.zzaFQ).zzar(this.zzaFv);
        }
        return this.zzaFQ;
    }
}
