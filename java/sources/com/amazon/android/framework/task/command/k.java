package com.amazon.android.framework.task.command;

import android.os.RemoteException;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import com.amazon.venezia.command.o;
import com.amazon.venezia.command.r;

/* JADX INFO: loaded from: classes.dex */
final class k extends o {
    private /* synthetic */ c a;

    k(c cVar) {
        this.a = cVar;
    }

    @Override // com.amazon.venezia.command.f
    public final void a(FailureResult failureResult) throws RemoteException {
        this.a.d.add(new m(failureResult));
    }

    @Override // com.amazon.venezia.command.f
    public final void a(SuccessResult successResult) throws RemoteException {
        this.a.d.add(new m(successResult));
    }

    @Override // com.amazon.venezia.command.f
    public final void a(com.amazon.venezia.command.j jVar) throws RemoteException {
        this.a.d.add(new m(jVar));
    }

    @Override // com.amazon.venezia.command.f
    public final void a(r rVar) throws RemoteException {
        this.a.d.add(new m(rVar));
    }
}
