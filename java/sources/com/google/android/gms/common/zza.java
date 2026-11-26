package com.google.android.gms.common;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import com.comscore.streaming.Constants;
import com.google.android.gms.common.internal.zzbo;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;

/* JADX INFO: loaded from: classes.dex */
public final class zza implements ServiceConnection {
    private boolean zzazV = false;
    private final BlockingQueue<IBinder> zzazW = new LinkedBlockingQueue();

    @Override // android.content.ServiceConnection
    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        this.zzazW.add(iBinder);
    }

    @Override // android.content.ServiceConnection
    public final void onServiceDisconnected(ComponentName componentName) {
    }

    public final IBinder zza(long j, TimeUnit timeUnit) throws InterruptedException, TimeoutException {
        zzbo.zzcG("BlockingServiceConnection.getServiceWithTimeout() called on main thread");
        if (this.zzazV) {
            throw new IllegalStateException("Cannot call get on this connection more than once");
        }
        this.zzazV = true;
        IBinder iBinderPoll = this.zzazW.poll(Constants.HEARTBEAT_STAGE_ONE_INTERVAL, timeUnit);
        if (iBinderPoll == null) {
            throw new TimeoutException("Timed out waiting for the service connection");
        }
        return iBinderPoll;
    }

    public final IBinder zzoV() throws InterruptedException {
        zzbo.zzcG("BlockingServiceConnection.getService() called on main thread");
        if (this.zzazV) {
            throw new IllegalStateException("Cannot call get on this connection more than once");
        }
        this.zzazV = true;
        return this.zzazW.take();
    }
}
