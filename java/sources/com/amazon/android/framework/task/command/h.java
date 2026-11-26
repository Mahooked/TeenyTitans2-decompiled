package com.amazon.android.framework.task.command;

import android.os.RemoteException;
import com.amazon.venezia.command.z;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
final class h extends z {
    private /* synthetic */ AbstractCommandTask a;

    h(AbstractCommandTask abstractCommandTask) {
        this.a = abstractCommandTask;
    }

    @Override // com.amazon.venezia.command.w
    public final String a() throws RemoteException {
        return this.a.getCommandName();
    }

    @Override // com.amazon.venezia.command.w
    public final String b() throws RemoteException {
        return this.a.getCommandVersion();
    }

    @Override // com.amazon.venezia.command.w
    public final String c() throws RemoteException {
        return this.a.application.getPackageName();
    }

    @Override // com.amazon.venezia.command.w
    public final Map d() throws RemoteException {
        return this.a.getCommandData();
    }
}
