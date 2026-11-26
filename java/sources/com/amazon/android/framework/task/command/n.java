package com.amazon.android.framework.task.command;

import android.content.Intent;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
final class n {
    final com.amazon.venezia.command.n a;
    final String b;
    final Intent c;

    public n(com.amazon.venezia.command.n nVar) throws RemoteException {
        this.a = nVar;
        this.b = nVar.a();
        this.c = nVar.b();
    }
}
