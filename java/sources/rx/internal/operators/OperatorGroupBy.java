package rx.internal.operators;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;
import java.util.Queue;
import java.util.concurrent.ConcurrentHashMap;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicIntegerFieldUpdater;
import java.util.concurrent.atomic.AtomicLongFieldUpdater;
import java.util.concurrent.atomic.AtomicReferenceFieldUpdater;
import rx.Observable;
import rx.Producer;
import rx.Subscriber;
import rx.Subscription;
import rx.functions.Action0;
import rx.functions.Func1;
import rx.internal.producers.ProducerArbiter;
import rx.internal.util.RxRingBuffer;
import rx.internal.util.UtilityFunctions;
import rx.observables.GroupedObservable;
import rx.plugins.RxJavaPlugins;
import rx.subscriptions.Subscriptions;

/* JADX INFO: loaded from: classes.dex */
public final class OperatorGroupBy<T, K, V> implements Observable.Operator<GroupedObservable<K, V>, T> {
    final int bufferSize;
    final boolean delayError;
    final Func1<? super T, ? extends K> keySelector;
    final Func1<? super T, ? extends V> valueSelector;

    public OperatorGroupBy(Func1<? super T, ? extends K> keySelector) {
        this(keySelector, UtilityFunctions.identity(), RxRingBuffer.SIZE, false);
    }

    public OperatorGroupBy(Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector) {
        this(keySelector, valueSelector, RxRingBuffer.SIZE, false);
    }

    public OperatorGroupBy(Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector, int bufferSize, boolean delayError) {
        this.keySelector = keySelector;
        this.valueSelector = valueSelector;
        this.bufferSize = bufferSize;
        this.delayError = delayError;
    }

    @Override // rx.functions.Func1
    public Subscriber<? super T> call(Subscriber<? super GroupedObservable<K, V>> t) {
        final GroupBySubscriber<T, K, V> parent = new GroupBySubscriber<>(t, this.keySelector, this.valueSelector, this.bufferSize, this.delayError);
        t.add(Subscriptions.create(new Action0() { // from class: rx.internal.operators.OperatorGroupBy.1
            @Override // rx.functions.Action0
            public void call() {
                parent.cancel();
            }
        }));
        t.setProducer(parent.producer);
        return parent;
    }

    public static final class GroupByProducer implements Producer {
        final GroupBySubscriber<?, ?, ?> parent;

        public GroupByProducer(GroupBySubscriber<?, ?, ?> parent) {
            this.parent = parent;
        }

        @Override // rx.Producer
        public void request(long n) {
            this.parent.requestMore(n);
        }
    }

    public static final class GroupBySubscriber<T, K, V> extends Subscriber<T> {
        final Subscriber<? super GroupedObservable<K, V>> actual;
        final int bufferSize;
        volatile int cancelled;
        final boolean delayError;
        volatile boolean done;
        Throwable error;
        volatile int groupCount;
        final Func1<? super T, ? extends K> keySelector;
        final GroupByProducer producer;
        volatile long requested;
        final ProducerArbiter s;
        final Func1<? super T, ? extends V> valueSelector;
        volatile int wip;
        static final Object NULL_KEY = new Object();
        static final AtomicIntegerFieldUpdater<GroupBySubscriber> CANCELLED = AtomicIntegerFieldUpdater.newUpdater(GroupBySubscriber.class, "cancelled");
        static final AtomicLongFieldUpdater<GroupBySubscriber> REQUESTED = AtomicLongFieldUpdater.newUpdater(GroupBySubscriber.class, "requested");
        static final AtomicIntegerFieldUpdater<GroupBySubscriber> GROUP_COUNT = AtomicIntegerFieldUpdater.newUpdater(GroupBySubscriber.class, "groupCount");
        static final AtomicIntegerFieldUpdater<GroupBySubscriber> WIP = AtomicIntegerFieldUpdater.newUpdater(GroupBySubscriber.class, "wip");
        final Map<Object, GroupedUnicast<K, V>> groups = new ConcurrentHashMap();
        final Queue<GroupedObservable<K, V>> queue = new ConcurrentLinkedQueue();

        public GroupBySubscriber(Subscriber<? super GroupedObservable<K, V>> actual, Func1<? super T, ? extends K> keySelector, Func1<? super T, ? extends V> valueSelector, int bufferSize, boolean delayError) {
            this.actual = actual;
            this.keySelector = keySelector;
            this.valueSelector = valueSelector;
            this.bufferSize = bufferSize;
            this.delayError = delayError;
            GROUP_COUNT.lazySet(this, 1);
            this.s = new ProducerArbiter();
            this.s.request(bufferSize);
            this.producer = new GroupByProducer(this);
        }

        @Override // rx.Subscriber
        public void setProducer(Producer s) {
            this.s.setProducer(s);
        }

        @Override // rx.Observer
        public void onNext(T t) {
            if (!this.done) {
                Queue<GroupedObservable<K, V>> q = this.queue;
                Subscriber<? super GroupedObservable<K, V>> a = this.actual;
                try {
                    K key = this.keySelector.call(t);
                    boolean notNew = true;
                    Object mapKey = key != null ? key : NULL_KEY;
                    GroupedUnicast<K, V> group = this.groups.get(mapKey);
                    if (group == null) {
                        if (this.cancelled == 0) {
                            group = GroupedUnicast.createWith(key, this.bufferSize, this, this.delayError);
                            this.groups.put(mapKey, group);
                            GROUP_COUNT.getAndIncrement(this);
                            notNew = false;
                            q.offer(group);
                            drain();
                        } else {
                            return;
                        }
                    }
                    try {
                        V v = this.valueSelector.call(t);
                        group.onNext(v);
                        if (notNew) {
                            this.s.request(1L);
                        }
                    } catch (Throwable ex) {
                        unsubscribe();
                        errorAll(a, q, ex);
                    }
                } catch (Throwable ex2) {
                    unsubscribe();
                    errorAll(a, q, ex2);
                }
            }
        }

        @Override // rx.Observer
        public void onError(Throwable t) {
            if (this.done) {
                RxJavaPlugins.getInstance().getErrorHandler().handleError(t);
                return;
            }
            this.error = t;
            this.done = true;
            GROUP_COUNT.decrementAndGet(this);
            drain();
        }

        @Override // rx.Observer
        public void onCompleted() {
            if (!this.done) {
                for (GroupedUnicast<K, V> e : this.groups.values()) {
                    e.onComplete();
                }
                this.groups.clear();
                this.done = true;
                GROUP_COUNT.decrementAndGet(this);
                drain();
            }
        }

        public void requestMore(long n) {
            if (n < 0) {
                throw new IllegalArgumentException("n >= 0 required but it was " + n);
            }
            BackpressureUtils.getAndAddRequest(REQUESTED, this, n);
            drain();
        }

        public void cancel() {
            if (CANCELLED.compareAndSet(this, 0, 1) && GROUP_COUNT.decrementAndGet(this) == 0) {
                unsubscribe();
            }
        }

        public void cancel(K key) {
            Object mapKey = key != null ? key : NULL_KEY;
            if (this.groups.remove(mapKey) != null && GROUP_COUNT.decrementAndGet(this) == 0) {
                unsubscribe();
            }
        }

        void drain() {
            if (WIP.getAndIncrement(this) == 0) {
                int missed = 1;
                Queue<GroupedObservable<K, V>> q = this.queue;
                Subscriber<? super GroupedObservable<K, V>> a = this.actual;
                while (!checkTerminated(this.done, q.isEmpty(), a, q)) {
                    long r = this.requested;
                    boolean unbounded = r == Long.MAX_VALUE;
                    long e = 0;
                    while (r != 0) {
                        boolean d = this.done;
                        GroupedObservable<K, V> t = q.poll();
                        boolean empty = t == null;
                        if (!checkTerminated(d, empty, a, q)) {
                            if (empty) {
                                break;
                            }
                            a.onNext(t);
                            r--;
                            e--;
                        } else {
                            return;
                        }
                    }
                    if (e != 0) {
                        if (!unbounded) {
                            REQUESTED.addAndGet(this, e);
                        }
                        this.s.request(-e);
                    }
                    missed = WIP.addAndGet(this, -missed);
                    if (missed == 0) {
                        return;
                    }
                }
            }
        }

        void errorAll(Subscriber<? super GroupedObservable<K, V>> a, Queue<?> q, Throwable ex) {
            q.clear();
            List<GroupedUnicast<K, V>> list = new ArrayList<>(this.groups.values());
            this.groups.clear();
            for (GroupedUnicast<K, V> e : list) {
                e.onError(ex);
            }
            a.onError(ex);
        }

        boolean checkTerminated(boolean d, boolean empty, Subscriber<? super GroupedObservable<K, V>> a, Queue<?> q) {
            if (d) {
                Throwable err = this.error;
                if (err != null) {
                    errorAll(a, q, err);
                    return true;
                }
                if (empty) {
                    this.actual.onCompleted();
                    return true;
                }
            }
            return false;
        }
    }

    static final class GroupedUnicast<K, T> extends GroupedObservable<K, T> {
        final State<T, K> state;

        public static <T, K> GroupedUnicast<K, T> createWith(K key, int bufferSize, GroupBySubscriber<?, K, T> parent, boolean delayError) {
            State<T, K> state = new State<>(bufferSize, parent, key, delayError);
            return new GroupedUnicast<>(key, state);
        }

        protected GroupedUnicast(K key, State<T, K> state) {
            super(key, state);
            this.state = state;
        }

        public void onNext(T t) {
            this.state.onNext(t);
        }

        public void onError(Throwable e) {
            this.state.onError(e);
        }

        public void onComplete() {
            this.state.onComplete();
        }
    }

    static final class State<T, K> extends AtomicInteger implements Producer, Subscription, Observable.OnSubscribe<T> {
        private static final long serialVersionUID = -3852313036005250360L;
        volatile Subscriber<? super T> actual;
        volatile int cancelled;
        final boolean delayError;
        volatile boolean done;
        Throwable error;
        final K key;
        volatile int once;
        final GroupBySubscriber<?, K, T> parent;
        final Queue<Object> queue = new ConcurrentLinkedQueue();
        volatile long requested;
        static final AtomicLongFieldUpdater<State> REQUESTED = AtomicLongFieldUpdater.newUpdater(State.class, "requested");
        static final AtomicIntegerFieldUpdater<State> CANCELLED = AtomicIntegerFieldUpdater.newUpdater(State.class, "cancelled");
        static final AtomicReferenceFieldUpdater<State, Subscriber> ACTUAL = AtomicReferenceFieldUpdater.newUpdater(State.class, Subscriber.class, "actual");
        static final AtomicIntegerFieldUpdater<State> ONCE = AtomicIntegerFieldUpdater.newUpdater(State.class, "once");

        public State(int bufferSize, GroupBySubscriber<?, K, T> parent, K key, boolean delayError) {
            this.parent = parent;
            this.key = key;
            this.delayError = delayError;
        }

        @Override // rx.Producer
        public void request(long n) {
            if (n < 0) {
                throw new IllegalArgumentException("n >= required but it was " + n);
            }
            if (n != 0) {
                BackpressureUtils.getAndAddRequest(REQUESTED, this, n);
                drain();
            }
        }

        @Override // rx.Subscription
        public boolean isUnsubscribed() {
            return this.cancelled != 0;
        }

        @Override // rx.Subscription
        public void unsubscribe() {
            if (CANCELLED.compareAndSet(this, 0, 1) && getAndIncrement() == 0) {
                this.parent.cancel(this.key);
            }
        }

        @Override // rx.functions.Action1
        public void call(Subscriber<? super T> s) {
            if (ONCE.compareAndSet(this, 0, 1)) {
                s.add(this);
                s.setProducer(this);
                ACTUAL.lazySet(this, s);
                drain();
                return;
            }
            s.onError(new IllegalStateException("Only one Subscriber allowed!"));
        }

        public void onNext(T t) {
            if (t == null) {
                this.error = new NullPointerException();
                this.done = true;
            } else {
                this.queue.offer(NotificationLite.instance().next(t));
            }
            drain();
        }

        public void onError(Throwable e) {
            this.error = e;
            this.done = true;
            drain();
        }

        public void onComplete() {
            this.done = true;
            drain();
        }

        void drain() {
            if (getAndIncrement() == 0) {
                int iAddAndGet = 1;
                Queue<Object> queue = this.queue;
                boolean z = this.delayError;
                Subscriber<? super T> subscriber = this.actual;
                NotificationLite notificationLiteInstance = NotificationLite.instance();
                while (true) {
                    if (subscriber != null) {
                        if (!checkTerminated(this.done, queue.isEmpty(), subscriber, z)) {
                            long j = this.requested;
                            boolean z2 = j == Long.MAX_VALUE;
                            long j2 = 0;
                            while (j != 0) {
                                boolean z3 = this.done;
                                Object objPoll = queue.poll();
                                boolean z4 = objPoll == null;
                                if (!checkTerminated(z3, z4, subscriber, z)) {
                                    if (z4) {
                                        break;
                                    }
                                    subscriber.onNext((T) notificationLiteInstance.getValue(objPoll));
                                    j--;
                                    j2--;
                                } else {
                                    return;
                                }
                            }
                            if (j2 != 0) {
                                if (!z2) {
                                    REQUESTED.addAndGet(this, j2);
                                }
                                this.parent.s.request(-j2);
                            }
                        } else {
                            return;
                        }
                    }
                    iAddAndGet = addAndGet(-iAddAndGet);
                    if (iAddAndGet != 0) {
                        if (subscriber == null) {
                            subscriber = this.actual;
                        }
                    } else {
                        return;
                    }
                }
            }
        }

        boolean checkTerminated(boolean d, boolean empty, Subscriber<? super T> a, boolean delayError) {
            if (this.cancelled != 0) {
                this.queue.clear();
                this.parent.cancel(this.key);
                return true;
            }
            if (d) {
                if (delayError) {
                    if (empty) {
                        Throwable e = this.error;
                        if (e != null) {
                            a.onError(e);
                            return true;
                        }
                        a.onCompleted();
                        return true;
                    }
                } else {
                    Throwable e2 = this.error;
                    if (e2 != null) {
                        this.queue.clear();
                        a.onError(e2);
                        return true;
                    }
                    if (empty) {
                        a.onCompleted();
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
