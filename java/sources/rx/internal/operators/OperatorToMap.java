package rx.internal.operators;

import java.util.HashMap;
import java.util.Map;
import rx.Observable;
import rx.Subscriber;
import rx.exceptions.Exceptions;
import rx.functions.Func0;
import rx.functions.Func1;
import rx.observers.Subscribers;

/* JADX INFO: loaded from: classes.dex */
public final class OperatorToMap<T, K, V> implements Observable.Operator<Map<K, V>, T> {
    final Func1<? super T, ? extends K> keySelector;
    private final Func0<? extends Map<K, V>> mapFactory;
    final Func1<? super T, ? extends V> valueSelector;

    public static final class DefaultToMapFactory<K, V> implements Func0<Map<K, V>> {
        @Override // rx.functions.Func0, java.util.concurrent.Callable
        public Map<K, V> call() {
            return new HashMap();
        }
    }

    public OperatorToMap(Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector) {
        this(keySelector, valueSelector, new DefaultToMapFactory());
    }

    public OperatorToMap(Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector, Func0<? extends Map<K, V>> mapFactory) {
        this.keySelector = keySelector;
        this.valueSelector = valueSelector;
        this.mapFactory = mapFactory;
    }

    @Override // rx.functions.Func1
    public Subscriber<? super T> call(final Subscriber<? super Map<K, V>> subscriber) {
        try {
            final Map<K, V> mapCall = this.mapFactory.call();
            return new Subscriber<T>(subscriber) { // from class: rx.internal.operators.OperatorToMap.1
                private Map<K, V> map;

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
                        K key = OperatorToMap.this.keySelector.call(v);
                        V value = OperatorToMap.this.valueSelector.call(v);
                        this.map.put(key, value);
                    } catch (Throwable ex) {
                        Exceptions.throwOrReport(ex, subscriber);
                    }
                }

                @Override // rx.Observer
                public void onError(Throwable e) {
                    this.map = null;
                    subscriber.onError(e);
                }

                @Override // rx.Observer
                public void onCompleted() {
                    Map<K, V> map0 = this.map;
                    this.map = null;
                    subscriber.onNext(map0);
                    subscriber.onCompleted();
                }
            };
        } catch (Throwable th) {
            Exceptions.throwOrReport(th, subscriber);
            Subscriber<? super T> subscriberEmpty = Subscribers.empty();
            subscriberEmpty.unsubscribe();
            return subscriberEmpty;
        }
    }
}
