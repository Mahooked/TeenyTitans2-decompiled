package com.amazon.android.framework.task.command;

import android.os.RemoteException;
import com.amazon.venezia.command.p;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class i extends p {
    private /* synthetic */ b a;
    private /* synthetic */ c b;

    i(c cVar, b bVar) {
        this.b = cVar;
        this.a = bVar;
    }

    @Override // com.amazon.venezia.command.s
    public final String a() throws RemoteException {
        return this.a.name();
    }

    @Override // com.amazon.venezia.command.s
    public final Map b() throws RemoteException {
        return null;
    }
}
