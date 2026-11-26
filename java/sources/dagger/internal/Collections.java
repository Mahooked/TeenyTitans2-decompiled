package dagger.internal;

import java.util.LinkedHashMap;
import java.util.LinkedHashSet;

/* JADX INFO: loaded from: classes.dex */
final class Collections {
    private static final int MAX_POWER_OF_TWO = 1073741824;

    private Collections() {
    }

    static <E> LinkedHashSet<E> newLinkedHashSetWithExpectedSize(int expectedSize) {
        return new LinkedHashSet<>(calculateInitialCapacity(expectedSize));
    }

    static <K, V> LinkedHashMap<K, V> newLinkedHashMapWithExpectedSize(int expectedSize) {
        return new LinkedHashMap<>(calculateInitialCapacity(expectedSize));
    }

    private static int calculateInitialCapacity(int expectedSize) {
        if (expectedSize < 3) {
            return expectedSize + 1;
        }
        if (expectedSize < MAX_POWER_OF_TWO) {
            return (int) ((expectedSize / 0.75f) + 1.0f);
        }
        return Integer.MAX_VALUE;
    }
}
