package rx.observers;

import java.util.Arrays;
import rx.Subscriber;
import rx.exceptions.CompositeException;
import rx.exceptions.Exceptions;
import rx.exceptions.OnErrorFailedException;
import rx.exceptions.OnErrorNotImplementedException;
import rx.internal.util.RxJavaPluginUtils;

/* JADX INFO: loaded from: classes.dex */
public class SafeSubscriber<T> extends Subscriber<T> {
    private final Subscriber<? super T> actual;
    boolean done;

    public SafeSubscriber(Subscriber<? super T> actual) {
        super(actual);
        this.done = false;
        this.actual = actual;
    }

    /* JADX WARN: Bottom block not found for handler: all -> 0x002f */
    @Override // rx.Observer
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    public void onCompleted() {
        /*
            r3 = this;
            boolean r1 = r3.done
            if (r1 != 0) goto Lf
            r1 = 1
            r3.done = r1
            rx.Subscriber<? super T> r1 = r3.actual     // Catch: java.lang.Throwable -> L1e java.lang.Throwable -> L2f
            r1.onCompleted()     // Catch: java.lang.Throwable -> L1e java.lang.Throwable -> L2f
            r3.unsubscribe()     // Catch: java.lang.Throwable -> L10
        Lf:
            return
        L10:
            r0 = move-exception
            rx.internal.util.RxJavaPluginUtils.handleException(r0)
            rx.exceptions.UnsubscribeFailedException r1 = new rx.exceptions.UnsubscribeFailedException
            java.lang.String r2 = r0.getMessage()
            r1.<init>(r2, r0)
            throw r1
        L1e:
            r0 = move-exception
            rx.exceptions.Exceptions.throwIfFatal(r0)     // Catch: java.lang.Throwable -> L2f
            rx.internal.util.RxJavaPluginUtils.handleException(r0)     // Catch: java.lang.Throwable -> L2f
            rx.exceptions.OnCompletedFailedException r1 = new rx.exceptions.OnCompletedFailedException     // Catch: java.lang.Throwable -> L2f
            java.lang.String r2 = r0.getMessage()     // Catch: java.lang.Throwable -> L2f
            r1.<init>(r2, r0)     // Catch: java.lang.Throwable -> L2f
            throw r1     // Catch: java.lang.Throwable -> L2f
        L2f:
            r1 = move-exception
            r3.unsubscribe()     // Catch: java.lang.Throwable -> L34
            throw r1
        L34:
            r0 = move-exception
            rx.internal.util.RxJavaPluginUtils.handleException(r0)
            rx.exceptions.UnsubscribeFailedException r1 = new rx.exceptions.UnsubscribeFailedException
            java.lang.String r2 = r0.getMessage()
            r1.<init>(r2, r0)
            throw r1
        */
        throw new UnsupportedOperationException("Method not decompiled: rx.observers.SafeSubscriber.onCompleted():void");
    }

    @Override // rx.Observer
    public void onError(Throwable e) throws Throwable {
        Exceptions.throwIfFatal(e);
        if (!this.done) {
            this.done = true;
            _onError(e);
        }
    }

    @Override // rx.Observer
    public void onNext(T args) {
        try {
            if (!this.done) {
                this.actual.onNext(args);
            }
        } catch (Throwable e) {
            Exceptions.throwOrReport(e, this);
        }
    }

    protected void _onError(Throwable e) throws Throwable {
        RxJavaPluginUtils.handleException(e);
        try {
            this.actual.onError(e);
            try {
                unsubscribe();
            } catch (RuntimeException unsubscribeException) {
                RxJavaPluginUtils.handleException(unsubscribeException);
                throw new OnErrorFailedException(unsubscribeException);
            }
        } catch (Throwable e2) {
            if (e2 instanceof OnErrorNotImplementedException) {
                try {
                    unsubscribe();
                    throw ((OnErrorNotImplementedException) e2);
                } catch (Throwable unsubscribeException2) {
                    RxJavaPluginUtils.handleException(unsubscribeException2);
                    throw new RuntimeException("Observer.onError not implemented and error while unsubscribing.", new CompositeException(Arrays.asList(e, unsubscribeException2)));
                }
            }
            RxJavaPluginUtils.handleException(e2);
            try {
                unsubscribe();
                throw new OnErrorFailedException("Error occurred when trying to propagate error to Observer.onError", new CompositeException(Arrays.asList(e, e2)));
            } catch (Throwable unsubscribeException3) {
                RxJavaPluginUtils.handleException(unsubscribeException3);
                throw new OnErrorFailedException("Error occurred when trying to propagate error to Observer.onError and during unsubscription.", new CompositeException(Arrays.asList(e, e2, unsubscribeException3)));
            }
        }
    }

    public Subscriber<? super T> getActual() {
        return this.actual;
    }
}
