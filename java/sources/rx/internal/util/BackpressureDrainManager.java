package rx.internal.util;

import java.util.concurrent.atomic.AtomicLong;
import rx.Producer;
import rx.annotations.Experimental;

/* JADX INFO: loaded from: classes.dex */
@Experimental
public final class BackpressureDrainManager extends AtomicLong implements Producer {
    protected final BackpressureQueueCallback actual;
    protected boolean emitting;
    protected Throwable exception;
    protected volatile boolean terminated;

    public interface BackpressureQueueCallback {
        boolean accept(Object obj);

        void complete(Throwable th);

        Object peek();

        Object poll();
    }

    public BackpressureDrainManager(BackpressureQueueCallback actual) {
        this.actual = actual;
    }

    public final boolean isTerminated() {
        return this.terminated;
    }

    public final void terminate() {
        this.terminated = true;
    }

    public final void terminate(Throwable error) {
        if (!this.terminated) {
            this.exception = error;
            this.terminated = true;
        }
    }

    public final void terminateAndDrain() {
        this.terminated = true;
        drain();
    }

    public final void terminateAndDrain(Throwable error) {
        if (!this.terminated) {
            this.exception = error;
            this.terminated = true;
            drain();
        }
    }

    @Override // rx.Producer
    public final void request(long n) {
        long r;
        boolean mayDrain;
        long u;
        if (n != 0) {
            do {
                r = get();
                mayDrain = r == 0;
                if (r == Long.MAX_VALUE) {
                    break;
                }
                if (n == Long.MAX_VALUE) {
                    u = n;
                    mayDrain = true;
                } else if (r > Long.MAX_VALUE - n) {
                    u = Long.MAX_VALUE;
                } else {
                    u = r + n;
                }
            } while (!compareAndSet(r, u));
            if (mayDrain) {
                drain();
            }
        }
    }

    /* JADX WARN: Code duplicated, block: B:118:0x0076 A[SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:121:0x003f A[EDGE_INSN: B:121:0x003f->B:33:0x003f BREAK  A[LOOP:1: B:12:0x0015->B:66:0x0082], SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:126:? A[RETURN, SYNTHETIC] */
    /* JADX WARN: Code duplicated, block: B:52:0x006a A[Catch: all -> 0x0094, TRY_ENTER, TryCatch #3 {all -> 0x0094, blocks: (B:10:0x0012, B:33:0x003f, B:73:0x0093, B:16:0x001f, B:18:0x0025, B:52:0x006a, B:54:0x0070, B:34:0x0040, B:37:0x0049, B:41:0x005a, B:42:0x005e, B:69:0x008f, B:82:0x009f, B:87:0x00af, B:88:0x00b3), top: B:106:0x0012, inners: #1 }] */
    /* JADX WARN: Code duplicated, block: B:54:0x0070 A[Catch: all -> 0x0094, TRY_LEAVE, TryCatch #3 {all -> 0x0094, blocks: (B:10:0x0012, B:33:0x003f, B:73:0x0093, B:16:0x001f, B:18:0x0025, B:52:0x006a, B:54:0x0070, B:34:0x0040, B:37:0x0049, B:41:0x005a, B:42:0x005e, B:69:0x008f, B:82:0x009f, B:87:0x00af, B:88:0x00b3), top: B:106:0x0012, inners: #1 }] */
    /* JADX WARN: Code duplicated, block: B:58:0x0079  */
    /* JADX WARN: Code duplicated, block: B:59:0x007a  */
    /* JADX WARN: Code duplicated, block: B:66:0x0082 A[LOOP:1: B:12:0x0015->B:66:0x0082, LOOP_END] */
    /* JADX WARN: Code restructure failed: missing block: B:124:?, code lost:
    
        return;
     */
    /* JADX WARN: Code restructure failed: missing block: B:125:?, code lost:
    
        return;
     */
    /* JADX WARN: Code restructure failed: missing block: B:43:0x005f, code lost:
    
        if (1 != 0) goto L124;
     */
    /* JADX WARN: Code restructure failed: missing block: B:44:0x0061, code lost:
    
        monitor-enter(r14);
     */
    /* JADX WARN: Code restructure failed: missing block: B:46:0x0063, code lost:
    
        r14.emitting = false;
     */
    /* JADX WARN: Code restructure failed: missing block: B:47:0x0065, code lost:
    
        monitor-exit(r14);
     */
    /*
        Code decompiled incorrectly, please refer to instructions dump.
        To view partially-correct add '--show-bad-code' argument
    */
    public final void drain() {
        /*
            r14 = this;
            monitor-enter(r14)
            boolean r9 = r14.emitting     // Catch: java.lang.Throwable -> L36
            if (r9 == 0) goto L7
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L36
        L6:
            return
        L7:
            r9 = 1
            r14.emitting = r9     // Catch: java.lang.Throwable -> L36
            boolean r8 = r14.terminated     // Catch: java.lang.Throwable -> L36
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L36
            long r4 = r14.get()
            r7 = 0
            rx.internal.util.BackpressureDrainManager$BackpressureQueueCallback r0 = r14.actual     // Catch: java.lang.Throwable -> L94
        L14:
            r2 = 0
        L15:
            r10 = 0
            int r9 = (r4 > r10 ? 1 : (r4 == r10 ? 0 : -1))
            if (r9 > 0) goto L1d
            if (r8 == 0) goto L3f
        L1d:
            if (r8 == 0) goto L6a
            java.lang.Object r6 = r0.peek()     // Catch: java.lang.Throwable -> L94
            if (r6 != 0) goto L39
            r7 = 1
            java.lang.Throwable r1 = r14.exception     // Catch: java.lang.Throwable -> L94
            r0.complete(r1)     // Catch: java.lang.Throwable -> L94
            if (r7 != 0) goto L6
            monitor-enter(r14)
            r9 = 0
            r14.emitting = r9     // Catch: java.lang.Throwable -> L33
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L33
            goto L6
        L33:
            r9 = move-exception
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L33
            throw r9
        L36:
            r9 = move-exception
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L36
            throw r9
        L39:
            r10 = 0
            int r9 = (r4 > r10 ? 1 : (r4 == r10 ? 0 : -1))
            if (r9 != 0) goto L6a
        L3f:
            monitor-enter(r14)     // Catch: java.lang.Throwable -> L94
            boolean r8 = r14.terminated     // Catch: java.lang.Throwable -> L91
            java.lang.Object r9 = r0.peek()     // Catch: java.lang.Throwable -> L91
            if (r9 == 0) goto L88
            r3 = 1
        L49:
            long r10 = r14.get()     // Catch: java.lang.Throwable -> L91
            r12 = 9223372036854775807(0x7fffffffffffffff, double:NaN)
            int r9 = (r10 > r12 ? 1 : (r10 == r12 ? 0 : -1))
            if (r9 != 0) goto L9d
            if (r3 != 0) goto L8a
            if (r8 != 0) goto L8a
            r7 = 1
            r9 = 0
            r14.emitting = r9     // Catch: java.lang.Throwable -> L91
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L91
            if (r7 != 0) goto L6
            monitor-enter(r14)
            r9 = 0
            r14.emitting = r9     // Catch: java.lang.Throwable -> L67
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L67
            goto L6
        L67:
            r9 = move-exception
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L67
            throw r9
        L6a:
            java.lang.Object r6 = r0.poll()     // Catch: java.lang.Throwable -> L94
            if (r6 == 0) goto L3f
            boolean r9 = r0.accept(r6)     // Catch: java.lang.Throwable -> L94
            if (r9 == 0) goto L82
            r7 = 1
            if (r7 != 0) goto L6
            monitor-enter(r14)
            r9 = 0
            r14.emitting = r9     // Catch: java.lang.Throwable -> L7f
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L7f
            goto L6
        L7f:
            r9 = move-exception
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L7f
            throw r9
        L82:
            r10 = 1
            long r4 = r4 - r10
            int r2 = r2 + 1
            goto L15
        L88:
            r3 = 0
            goto L49
        L8a:
            r4 = 9223372036854775807(0x7fffffffffffffff, double:NaN)
        L8f:
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L91
            goto L14
        L91:
            r9 = move-exception
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L91
            throw r9     // Catch: java.lang.Throwable -> L94
        L94:
            r9 = move-exception
            if (r7 != 0) goto L9c
            monitor-enter(r14)
            r10 = 0
            r14.emitting = r10     // Catch: java.lang.Throwable -> Lc0
            monitor-exit(r14)     // Catch: java.lang.Throwable -> Lc0
        L9c:
            throw r9
        L9d:
            int r9 = -r2
            long r10 = (long) r9
            long r4 = r14.addAndGet(r10)     // Catch: java.lang.Throwable -> L91
            r10 = 0
            int r9 = (r4 > r10 ? 1 : (r4 == r10 ? 0 : -1))
            if (r9 == 0) goto Lab
            if (r3 != 0) goto L8f
        Lab:
            if (r8 == 0) goto Laf
            if (r3 == 0) goto L8f
        Laf:
            r7 = 1
            r9 = 0
            r14.emitting = r9     // Catch: java.lang.Throwable -> L91
            monitor-exit(r14)     // Catch: java.lang.Throwable -> L91
            if (r7 != 0) goto L6
            monitor-enter(r14)
            r9 = 0
            r14.emitting = r9     // Catch: java.lang.Throwable -> Lbd
            monitor-exit(r14)     // Catch: java.lang.Throwable -> Lbd
            goto L6
        Lbd:
            r9 = move-exception
            monitor-exit(r14)     // Catch: java.lang.Throwable -> Lbd
            throw r9
        Lc0:
            r9 = move-exception
            monitor-exit(r14)     // Catch: java.lang.Throwable -> Lc0
            throw r9
        */
        throw new UnsupportedOperationException("Method not decompiled: rx.internal.util.BackpressureDrainManager.drain():void");
    }
}
