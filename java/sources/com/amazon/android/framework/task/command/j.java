package com.amazon.android.framework.task.command;

import android.content.ComponentName;
import android.content.ServiceConnection;
import android.os.IBinder;
import android.os.IInterface;
import java.util.concurrent.BlockingQueue;

/* JADX INFO: loaded from: classes.dex */
final class j implements ServiceConnection {
    private /* synthetic */ c a;

    j(c cVar) {
        this.a = cVar;
    }

    @Override // android.content.ServiceConnection
    public final void onServiceConnected(ComponentName componentName, IBinder iBinder) {
        com.amazon.venezia.command.h lVar;
        c.a.trace("onServiceConnected");
        BlockingQueue blockingQueue = this.a.e;
        if (iBinder == null) {
            lVar = null;
        } else {
            IInterface iInterfaceQueryLocalInterface = iBinder.queryLocalInterface("com.amazon.venezia.command.CommandService");
            lVar = (iInterfaceQueryLocalInterface == null || !(iInterfaceQueryLocalInterface instanceof com.amazon.venezia.command.h)) ? new com.amazon.venezia.command.l(iBinder) : (com.amazon.venezia.command.h) iInterfaceQueryLocalInterface;
        }
        blockingQueue.add(lVar);
    }

    @Override // android.content.ServiceConnection
    public final void onServiceDisconnected(ComponentName componentName) {
        c.a.trace("onServiceDisconnected!!!");
    }
}
