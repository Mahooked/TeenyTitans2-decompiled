package rx.internal.operators;

import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicIntegerFieldUpdater;
import rx.Completable;
import rx.Observable;
import rx.Subscriber;
import rx.Subscription;
import rx.exceptions.MissingBackpressureException;
import rx.internal.util.unsafe.SpscArrayQueue;
import rx.plugins.RxJavaPlugins;
import rx.subscriptions.SerialSubscription;

/* JADX INFO: loaded from: classes.dex */
public final class CompletableOnSubscribeConcat implements Completable.CompletableOnSubscribe {
    final int prefetch;
    final Observable<Completable> sources;

    /* JADX WARN: Multi-variable type inference failed */
    public CompletableOnSubscribeConcat(Observable<? extends Completable> observable, int prefetch) {
        this.sources = observable;
        this.prefetch = prefetch;
    }

    @Override // rx.functions.Action1
    public void call(Completable.CompletableSubscriber s) {
        CompletableConcatSubscriber parent = new CompletableConcatSubscriber(s, this.prefetch);
        s.onSubscribe(parent);
        this.sources.subscribe((Subscriber<? super Completable>) parent);
    }

    static final class CompletableConcatSubscriber extends Subscriber<Completable> {
        static final AtomicIntegerFieldUpdater<CompletableConcatSubscriber> ONCE = AtomicIntegerFieldUpdater.newUpdater(CompletableConcatSubscriber.class, "once");
        final Completable.CompletableSubscriber actual;
        volatile boolean done;
        volatile int once;
        final int prefetch;
        final SpscArrayQueue<Completable> queue;
        final SerialSubscription sr = new SerialSubscription();
        final ConcatInnerSubscriber inner = new ConcatInnerSubscriber();
        final AtomicInteger wip = new AtomicInteger();

        public CompletableConcatSubscriber(Completable.CompletableSubscriber actual, int prefetch) {
            this.actual = actual;
            this.prefetch = prefetch;
            this.queue = new SpscArrayQueue<>(prefetch);
            add(this.sr);
            request(prefetch);
        }

        @Override // rx.Observer
        public void onNext(Completable t) {
            if (!this.queue.offer(t)) {
                onError(new MissingBackpressureException());
            } else if (this.wip.getAndIncrement() == 0) {
                next();
            }
        }

        @Override // rx.Observer
        public void onError(Throwable t) {
            if (ONCE.compareAndSet(this, 0, 1)) {
                this.actual.onError(t);
            } else {
                RxJavaPlugins.getInstance().getErrorHandler().handleError(t);
            }
        }

        @Override // rx.Observer
        public void onCompleted() {
            if (!this.done) {
                this.done = true;
                if (this.wip.getAndIncrement() == 0) {
                    next();
                }
            }
        }

        void innerError(Throwable e) {
            unsubscribe();
            onError(e);
        }

        void innerComplete() {
            if (this.wip.decrementAndGet() != 0) {
                next();
            }
            if (!this.done) {
                request(1L);
            }
        }

        void next() {
            boolean d = this.done;
            Completable c = this.queue.poll();
            if (c == null) {
                if (d) {
                    if (ONCE.compareAndSet(this, 0, 1)) {
                        this.actual.onCompleted();
                        return;
                    }
                    return;
                }
                RxJavaPlugins.getInstance().getErrorHandler().handleError(new IllegalStateException("Queue is empty?!"));
                return;
            }
            c.subscribe(this.inner);
        }

        final class ConcatInnerSubscriber implements Completable.CompletableSubscriber {
            ConcatInnerSubscriber() {
            }

            @Override // rx.Completable.CompletableSubscriber
            public void onSubscribe(Subscription d) {
                CompletableConcatSubscriber.this.sr.set(d);
            }

            @Override // rx.Completable.CompletableSubscriber
            public void onError(Throwable e) {
                CompletableConcatSubscriber.this.innerError(e);
            }

            @Override // rx.Completable.CompletableSubscriber
            public void onCompleted() {
                CompletableConcatSubscriber.this.innerComplete();
            }
        }
    }
}
