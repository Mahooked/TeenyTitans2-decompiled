package rx.internal.operators;

import java.util.concurrent.atomic.AtomicBoolean;
import java.util.concurrent.atomic.AtomicInteger;
import rx.Completable;
import rx.Subscription;
import rx.plugins.RxJavaPlugins;
import rx.subscriptions.CompositeSubscription;

/* JADX INFO: loaded from: classes.dex */
public final class CompletableOnSubscribeMergeArray implements Completable.CompletableOnSubscribe {
    final Completable[] sources;

    public CompletableOnSubscribeMergeArray(Completable[] sources) {
        this.sources = sources;
    }

    @Override // rx.functions.Action1
    public void call(final Completable.CompletableSubscriber s) throws Throwable {
        final CompositeSubscription set = new CompositeSubscription();
        final AtomicInteger wip = new AtomicInteger(this.sources.length + 1);
        final AtomicBoolean once = new AtomicBoolean();
        s.onSubscribe(set);
        Completable[] arr$ = this.sources;
        for (Completable c : arr$) {
            if (!set.isUnsubscribed()) {
                if (c == null) {
                    set.unsubscribe();
                    NullPointerException npe = new NullPointerException("A completable source is null");
                    if (once.compareAndSet(false, true)) {
                        s.onError(npe);
                        return;
                    }
                    RxJavaPlugins.getInstance().getErrorHandler().handleError(npe);
                }
                c.subscribe(new Completable.CompletableSubscriber() { // from class: rx.internal.operators.CompletableOnSubscribeMergeArray.1
                    @Override // rx.Completable.CompletableSubscriber
                    public void onSubscribe(Subscription d) {
                        set.add(d);
                    }

                    @Override // rx.Completable.CompletableSubscriber
                    public void onError(Throwable e) throws Throwable {
                        set.unsubscribe();
                        if (once.compareAndSet(false, true)) {
                            s.onError(e);
                        } else {
                            RxJavaPlugins.getInstance().getErrorHandler().handleError(e);
                        }
                    }

                    @Override // rx.Completable.CompletableSubscriber
                    public void onCompleted() {
                        if (wip.decrementAndGet() == 0 && once.compareAndSet(false, true)) {
                            s.onCompleted();
                        }
                    }
                });
            } else {
                return;
            }
        }
        if (wip.decrementAndGet() == 0 && once.compareAndSet(false, true)) {
            s.onCompleted();
        }
    }
}
