package rx.internal.operators;

import java.util.ArrayList;
import java.util.List;
import java.util.Queue;
import java.util.concurrent.ConcurrentLinkedQueue;
import java.util.concurrent.atomic.AtomicInteger;
import java.util.concurrent.atomic.AtomicIntegerFieldUpdater;
import java.util.concurrent.atomic.AtomicReferenceFieldUpdater;
import rx.Completable;
import rx.Observable;
import rx.Subscriber;
import rx.Subscription;
import rx.exceptions.CompositeException;
import rx.plugins.RxJavaPlugins;
import rx.subscriptions.CompositeSubscription;

/* JADX INFO: loaded from: classes.dex */
public final class CompletableOnSubscribeMerge implements Completable.CompletableOnSubscribe {
    final boolean delayErrors;
    final int maxConcurrency;
    final Observable<Completable> source;

    /* JADX WARN: Multi-variable type inference failed */
    public CompletableOnSubscribeMerge(Observable<? extends Completable> observable, int maxConcurrency, boolean delayErrors) {
        this.source = observable;
        this.maxConcurrency = maxConcurrency;
        this.delayErrors = delayErrors;
    }

    @Override // rx.functions.Action1
    public void call(Completable.CompletableSubscriber s) {
        CompletableMergeSubscriber parent = new CompletableMergeSubscriber(s, this.maxConcurrency, this.delayErrors);
        s.onSubscribe(parent);
        this.source.subscribe((Subscriber<? super Completable>) parent);
    }

    static final class CompletableMergeSubscriber extends Subscriber<Completable> {
        static final AtomicReferenceFieldUpdater<CompletableMergeSubscriber, Queue> ERRORS = AtomicReferenceFieldUpdater.newUpdater(CompletableMergeSubscriber.class, Queue.class, "errors");
        static final AtomicIntegerFieldUpdater<CompletableMergeSubscriber> ONCE = AtomicIntegerFieldUpdater.newUpdater(CompletableMergeSubscriber.class, "once");
        final Completable.CompletableSubscriber actual;
        final boolean delayErrors;
        volatile boolean done;
        volatile Queue<Throwable> errors;
        final int maxConcurrency;
        volatile int once;
        final CompositeSubscription set = new CompositeSubscription();
        final AtomicInteger wip = new AtomicInteger(1);

        public CompletableMergeSubscriber(Completable.CompletableSubscriber actual, int maxConcurrency, boolean delayErrors) {
            this.actual = actual;
            this.maxConcurrency = maxConcurrency;
            this.delayErrors = delayErrors;
            if (maxConcurrency == Integer.MAX_VALUE) {
                request(Long.MAX_VALUE);
            } else {
                request(maxConcurrency);
            }
        }

        Queue<Throwable> getOrCreateErrors() {
            Queue<Throwable> q = this.errors;
            if (q != null) {
                return q;
            }
            Queue<Throwable> q2 = new ConcurrentLinkedQueue<>();
            return ERRORS.compareAndSet(this, null, q2) ? q2 : this.errors;
        }

        @Override // rx.Observer
        public void onNext(Completable t) {
            if (!this.done) {
                this.wip.getAndIncrement();
                t.subscribe(new Completable.CompletableSubscriber() { // from class: rx.internal.operators.CompletableOnSubscribeMerge.CompletableMergeSubscriber.1
                    Subscription d;
                    boolean innerDone;

                    @Override // rx.Completable.CompletableSubscriber
                    public void onSubscribe(Subscription d) {
                        this.d = d;
                        CompletableMergeSubscriber.this.set.add(d);
                    }

                    @Override // rx.Completable.CompletableSubscriber
                    public void onError(Throwable e) {
                        if (this.innerDone) {
                            RxJavaPlugins.getInstance().getErrorHandler().handleError(e);
                            return;
                        }
                        this.innerDone = true;
                        CompletableMergeSubscriber.this.set.remove(this.d);
                        CompletableMergeSubscriber.this.getOrCreateErrors().offer(e);
                        CompletableMergeSubscriber.this.terminate();
                        if (CompletableMergeSubscriber.this.delayErrors && !CompletableMergeSubscriber.this.done) {
                            CompletableMergeSubscriber.this.request(1L);
                        }
                    }

                    @Override // rx.Completable.CompletableSubscriber
                    public void onCompleted() {
                        if (!this.innerDone) {
                            this.innerDone = true;
                            CompletableMergeSubscriber.this.set.remove(this.d);
                            CompletableMergeSubscriber.this.terminate();
                            if (!CompletableMergeSubscriber.this.done) {
                                CompletableMergeSubscriber.this.request(1L);
                            }
                        }
                    }
                });
            }
        }

        @Override // rx.Observer
        public void onError(Throwable t) {
            if (this.done) {
                RxJavaPlugins.getInstance().getErrorHandler().handleError(t);
                return;
            }
            getOrCreateErrors().offer(t);
            this.done = true;
            terminate();
        }

        @Override // rx.Observer
        public void onCompleted() {
            if (!this.done) {
                this.done = true;
                terminate();
            }
        }

        void terminate() {
            Queue<Throwable> q;
            if (this.wip.decrementAndGet() == 0) {
                Queue<Throwable> q2 = this.errors;
                if (q2 == null || q2.isEmpty()) {
                    this.actual.onCompleted();
                    return;
                }
                Throwable e = CompletableOnSubscribeMerge.collectErrors(q2);
                if (ONCE.compareAndSet(this, 0, 1)) {
                    this.actual.onError(e);
                    return;
                } else {
                    RxJavaPlugins.getInstance().getErrorHandler().handleError(e);
                    return;
                }
            }
            if (!this.delayErrors && (q = this.errors) != null && !q.isEmpty()) {
                Throwable e2 = CompletableOnSubscribeMerge.collectErrors(q);
                if (ONCE.compareAndSet(this, 0, 1)) {
                    this.actual.onError(e2);
                } else {
                    RxJavaPlugins.getInstance().getErrorHandler().handleError(e2);
                }
            }
        }
    }

    public static Throwable collectErrors(Queue<Throwable> q) {
        List<Throwable> list = new ArrayList<>();
        while (true) {
            Throwable t = q.poll();
            if (t == null) {
                break;
            }
            list.add(t);
        }
        if (list.isEmpty()) {
            return null;
        }
        if (list.size() == 1) {
            return list.get(0);
        }
        return new CompositeException(list);
    }
}
