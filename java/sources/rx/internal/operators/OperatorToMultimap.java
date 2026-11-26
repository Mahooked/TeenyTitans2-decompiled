package rx.internal.operators;

import java.util.ArrayList;
import java.util.Collection;
import java.util.HashMap;
import java.util.Map;
import rx.Observable;
import rx.Subscriber;
import rx.exceptions.Exceptions;
import rx.functions.Func0;
import rx.functions.Func1;
import rx.observers.Subscribers;

/* JADX INFO: loaded from: classes.dex */
public final class OperatorToMultimap<T, K, V> implements Observable.Operator<Map<K, Collection<V>>, T> {
    final Func1<? super K, ? extends Collection<V>> collectionFactory;
    final Func1<? super T, ? extends K> keySelector;
    private final Func0<? extends Map<K, Collection<V>>> mapFactory;
    final Func1<? super T, ? extends V> valueSelector;

    public static final class DefaultToMultimapFactory<K, V> implements Func0<Map<K, Collection<V>>> {
        @Override // rx.functions.Func0, java.util.concurrent.Callable
        public Map<K, Collection<V>> call() {
            return new HashMap();
        }
    }

    public static final class DefaultMultimapCollectionFactory<K, V> implements Func1<K, Collection<V>> {
        @Override // rx.functions.Func1
        public Collection<V> call(K t1) {
            return new ArrayList();
        }
    }

    public OperatorToMultimap(Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector) {
        this(keySelector, valueSelector, new DefaultToMultimapFactory(), new DefaultMultimapCollectionFactory());
    }

    public OperatorToMultimap(Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector, Func0<? extends Map<K, Collection<V>>> mapFactory) {
        this(keySelector, valueSelector, mapFactory, new DefaultMultimapCollectionFactory());
    }

    public OperatorToMultimap(Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector, Func0<? extends Map<K, Collection<V>>> mapFactory, Func1<? super K, ? extends Collection<V>> collectionFactory) {
        this.keySelector = keySelector;
        this.valueSelector = valueSelector;
        this.mapFactory = mapFactory;
        this.collectionFactory = collectionFactory;
    }

    @Override // rx.functions.Func1
    public Subscriber<? super T> call(final Subscriber<? super Map<K, Collection<V>>> subscriber) {
        try {
            final Map<K, Collection<V>> mapCall = this.mapFactory.call();
            return new Subscriber<T>(subscriber) { // from class: rx.internal.operators.OperatorToMultimap.1
                private Map<K, Collection<V>> map;

                {
                    this.map = mapCall;
                }

                @Override // rx.Subscriber
                public void onStart() {
                    request(Long.MAX_VALUE);
                }

                @Override // rx.Observer
                public void onNext(T v) {
                    try {
                        K key = OperatorToMultimap.this.keySelector.call(v);
                        V value = OperatorToMultimap.this.valueSelector.call(v);
                        Collection<V> collection = this.map.get(key);
                        if (collection == null) {
                            try {
                                collection = OperatorToMultimap.this.collectionFactory.call(key);
                                this.map.put(key, collection);
                            } catch (Throwable ex) {
                                Exceptions.throwOrReport(ex, subscriber);
                                return;
                            }
                        }
                        collection.add(value);
                    } catch (Throwable ex2) {
                        Exceptions.throwOrReport(ex2, subscriber);
                    }
                }

                @Override // rx.Observer
                public void onError(Throwable e) {
                    this.map = null;
                    subscriber.onError(e);
                }

                @Override // rx.Observer
                public void onCompleted() {
                    Map<K, Collection<V>> map0 = this.map;
                    this.map = null;
                    subscriber.onNext(map0);
                    subscriber.onCompleted();
                }
            };
        } catch (Throwable th) {
            Exceptions.throwIfFatal(th);
            subscriber.onError(th);
            Subscriber<? super T> subscriberEmpty = Subscribers.empty();
            subscriberEmpty.unsubscribe();
            return subscriberEmpty;
        }
    }
}
