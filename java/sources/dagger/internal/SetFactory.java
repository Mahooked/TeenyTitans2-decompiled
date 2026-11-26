package dagger.internal;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashSet;
import java.util.List;
import java.util.Set;
import javax.inject.Provider;

/* JADX INFO: loaded from: classes.dex */
public final class SetFactory<T> implements Factory<Set<T>> {
    static final /* synthetic */ boolean $assertionsDisabled;
    private static final String ARGUMENTS_MUST_BE_NON_NULL = "SetFactory.create() requires its arguments to be non-null";
    private static final Factory<Set<Object>> EMPTY_FACTORY;
    private final List<Provider<Set<T>>> contributingProviders;

    static {
        $assertionsDisabled = !SetFactory.class.desiredAssertionStatus();
        EMPTY_FACTORY = new Factory<Set<Object>>() { // from class: dagger.internal.SetFactory.1
            @Override // javax.inject.Provider
            public Set<Object> get() {
                return java.util.Collections.emptySet();
            }
        };
    }

    public static <T> Factory<Set<T>> create() {
        return (Factory<Set<T>>) EMPTY_FACTORY;
    }

    public static <T> Factory<Set<T>> create(Factory<Set<T>> factory) {
        if ($assertionsDisabled || factory != null) {
            return factory;
        }
        throw new AssertionError(ARGUMENTS_MUST_BE_NON_NULL);
    }

    public static <T> Factory<Set<T>> create(Provider<Set<T>>... providers) {
        if (!$assertionsDisabled && providers == null) {
            throw new AssertionError(ARGUMENTS_MUST_BE_NON_NULL);
        }
        List<Provider<Set<T>>> contributingProviders = Arrays.asList(providers);
        if (!$assertionsDisabled && contributingProviders.contains(null)) {
            throw new AssertionError("Codegen error?  Null within provider list.");
        }
        if ($assertionsDisabled || !hasDuplicates(contributingProviders)) {
            return new SetFactory(contributingProviders);
        }
        throw new AssertionError("Codegen error?  Duplicates in the provider list");
    }

    private static boolean hasDuplicates(List<? extends Object> original) {
        Set<Object> asSet = new HashSet<>(original);
        return original.size() != asSet.size();
    }

    private SetFactory(List<Provider<Set<T>>> contributingProviders) {
        this.contributingProviders = contributingProviders;
    }

    @Override // javax.inject.Provider
    public Set<T> get() {
        int size = 0;
        List<Set<T>> providedSets = new ArrayList<>(this.contributingProviders.size());
        int c = this.contributingProviders.size();
        for (int i = 0; i < c; i++) {
            Provider<Set<T>> provider = this.contributingProviders.get(i);
            Set<T> providedSet = provider.get();
            if (providedSet == null) {
                String strValueOf = String.valueOf(provider);
                throw new NullPointerException(new StringBuilder(String.valueOf(strValueOf).length() + 14).append(strValueOf).append(" returned null").toString());
            }
            providedSets.add(providedSet);
            size += providedSet.size();
        }
        Set<T> result = Collections.newLinkedHashSetWithExpectedSize(size);
        int c2 = providedSets.size();
        for (int i2 = 0; i2 < c2; i2++) {
            for (T element : providedSets.get(i2)) {
                if (element == null) {
                    throw new NullPointerException("a null element was provided");
                }
                result.add(element);
            }
        }
        return java.util.Collections.unmodifiableSet(result);
    }
}
