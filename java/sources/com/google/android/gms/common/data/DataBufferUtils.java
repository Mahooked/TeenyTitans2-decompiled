package com.google.android.gms.common.data;

import android.os.Bundle;
import java.util.ArrayList;
import java.util.Iterator;
import rx.internal.operators.OperatorReplay;

/* JADX INFO: loaded from: classes.dex */
public final class DataBufferUtils {
    private DataBufferUtils() {
    }

    public static <T, E extends Freezable<T>> ArrayList<T> freezeAndClose(DataBuffer<E> dataBuffer) {
        OperatorReplay.UnboundedReplayBuffer unboundedReplayBuffer = (ArrayList<T>) new ArrayList(dataBuffer.getCount());
        try {
            Iterator<E> it = dataBuffer.iterator();
            while (it.hasNext()) {
                unboundedReplayBuffer.add(it.next().freeze());
            }
            dataBuffer.close();
            return unboundedReplayBuffer;
        } catch (Throwable th) {
            dataBuffer.close();
            throw th;
        }
    }

    public static boolean hasData(DataBuffer<?> dataBuffer) {
        return dataBuffer != null && dataBuffer.getCount() > 0;
    }

    public static boolean hasNextPage(DataBuffer<?> dataBuffer) {
        Bundle bundleZzqN = dataBuffer.zzqN();
        return (bundleZzqN == null || bundleZzqN.getString("next_page_token") == null) ? false : true;
    }

    public static boolean hasPrevPage(DataBuffer<?> dataBuffer) {
        Bundle bundleZzqN = dataBuffer.zzqN();
        return (bundleZzqN == null || bundleZzqN.getString("prev_page_token") == null) ? false : true;
    }
}
