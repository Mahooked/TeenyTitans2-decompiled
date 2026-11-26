package com.amazon.android.framework.task.command;

import android.os.RemoteException;
import com.amazon.venezia.command.r;

/* JADX INFO: loaded from: classes.dex */
final class g {
    final String a;
    final String b;
    final long c;
    final n d;
    final n e;
    final n f;

    public g(r rVar) throws RemoteException {
        this.a = rVar.b();
        this.b = rVar.c();
        this.c = rVar.d();
        this.d = a(rVar.e());
        this.e = a(rVar.f());
        this.f = a(rVar.g());
    }

    private static n a(com.amazon.venezia.command.n nVar) throws RemoteException {
        if (nVar == null) {
            return null;
        }
        return new n(nVar);
    }
}
