package com.amazon.android.framework.task.command;

import android.app.Application;
import android.content.Intent;
import android.content.ServiceConnection;
import android.content.pm.ResolveInfo;
import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.venezia.command.r;
import com.amazon.venezia.command.w;
import java.util.List;
import java.util.concurrent.BlockingQueue;
import java.util.concurrent.LinkedBlockingQueue;

/* JADX INFO: loaded from: classes.dex */
public final class c {
    private static final KiwiLogger a = new KiwiLogger("CommandServiceClient");
    private com.amazon.venezia.command.h b;
    private String c;

    @Resource
    private Application f;

    @Resource
    private f g;

    @Resource
    private com.amazon.android.n.a h;
    private final BlockingQueue d = new LinkedBlockingQueue();
    private final BlockingQueue e = new LinkedBlockingQueue();
    private final ServiceConnection i = new j(this);
    private final com.amazon.venezia.command.f j = new k(this);

    private static Intent a(String str) {
        Intent intent = new Intent();
        intent.setAction("com.amazon.venezia.CommandService");
        intent.setClassName(str, "com.amazon.venezia.service.command.CommandServiceImpl");
        return intent;
    }

    private Intent a(List list) {
        int i = -1;
        int i2 = 0;
        String str = null;
        while (i2 < list.size()) {
            try {
                ResolveInfo resolveInfo = (ResolveInfo) list.get(i2);
                String str2 = resolveInfo.serviceInfo.applicationInfo.packageName;
                a.trace("Examining package " + str2);
                a.trace("Priority is " + resolveInfo.filter.getPriority());
                a.trace("Checking signature of package " + str2);
                a.trace("isPackageSignatureTrusted " + str2);
                if (this.g.a(str2, l.a)) {
                    a.trace("Signature of package " + str2 + " is okay");
                    str = str2;
                    i = i2;
                    break;
                }
                a.trace("Signature of package " + str2 + " is bad");
                i2++;
                str = str2;
            } catch (Exception e) {
                a.trace("Caught exception " + e);
            }
        }
        if (i >= 0) {
            return a(str);
        }
        return null;
    }

    private static void a(KiwiException kiwiException, a aVar) {
        a.error("CommandServiceException happened, retriesLeft=" + aVar.a, kiwiException);
        if (aVar.a == 0) {
            aVar.b = false;
            return;
        }
        aVar.a--;
        aVar.b = true;
        try {
            a.trace("Sleeping for " + aVar.c + " ms");
            Thread.sleep(aVar.c);
        } catch (InterruptedException e) {
            aVar.b = false;
        }
        aVar.c *= 2;
    }

    private m d() throws RemoteException {
        try {
            a.trace("Blocking for result from service");
            m mVar = (m) this.d.take();
            a.trace("Received result from service");
            return mVar;
        } catch (InterruptedException e) {
            a.trace("TaskThread interrupted, returning null result");
            return null;
        }
    }

    public final m a(com.amazon.venezia.command.n nVar) throws RemoteException {
        nVar.a(null);
        return d();
    }

    public final m a(r rVar, b bVar) throws RemoteException {
        rVar.a(new i(this, bVar));
        return d();
    }

    public final m a(w wVar) throws RemoteException, com.amazon.android.b.f, com.amazon.android.b.g {
        Intent intentA;
        a aVar = new a();
        aVar.a = 6;
        aVar.c = 100L;
        while (true) {
            try {
                if (!(this.b != null)) {
                    long jCurrentTimeMillis = System.currentTimeMillis();
                    a.trace("Binding Service!!!");
                    Intent intent = new Intent();
                    intent.setAction("com.amazon.venezia.CommandService");
                    a.trace("Created intent with  action  com.amazon.venezia.CommandService");
                    if (!(this.f.getPackageManager().resolveService(intent, 64) != null)) {
                        throw new com.amazon.android.b.g();
                    }
                    a.trace("Found service on one or more packages");
                    if (this.c != null) {
                        a.trace("Using previously determined package " + this.c);
                        intentA = a(this.c);
                    } else {
                        a.trace("No previously determined package found, checking for suitable package.");
                        intentA = a(this.f.getPackageManager().queryIntentServices(intent, 64));
                        if (intentA == null) {
                            a.trace("No app with valid signature was providing our service.");
                            throw new com.amazon.android.b.g();
                        }
                    }
                    String packageName = intentA.getComponent().getPackageName();
                    a.trace("Attempting to bind to service on " + packageName);
                    if (!this.f.bindService(intentA, this.i, 1)) {
                        throw new com.amazon.android.b.f();
                    }
                    try {
                        a.trace("Blocking while service is being bound!!");
                        this.b = (com.amazon.venezia.command.h) this.e.take();
                        a.trace("service bound, returning!!");
                        if (KiwiLogger.TRACE_ON) {
                            a.trace("Kiwi.BindService Time: " + (System.currentTimeMillis() - jCurrentTimeMillis));
                        }
                        this.h.a("PACKAGE", packageName);
                        this.c = packageName;
                    } catch (InterruptedException e) {
                        Thread.currentThread().interrupt();
                        throw new com.amazon.android.b.f();
                    }
                }
                this.h.a("COMMAND", wVar.a());
                this.b.a(wVar, this.j);
                return d();
            } catch (com.amazon.android.b.f e2) {
                a(e2, aVar);
                if (!aVar.b) {
                    throw e2;
                }
            } catch (com.amazon.android.b.g e3) {
                a(e3, aVar);
                if (!aVar.b) {
                    throw e3;
                }
            }
        }
    }

    public final String a() {
        return this.c;
    }

    public final void b() {
        a.trace("Finishing CommandServiceClient, unbinding service: " + (this.b != null));
        if (this.b != null) {
            this.f.unbindService(this.i);
            this.b = null;
        }
    }
}
