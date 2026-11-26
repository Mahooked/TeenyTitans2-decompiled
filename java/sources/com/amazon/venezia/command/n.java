package com.amazon.venezia.command;

import android.content.Intent;
import android.os.IInterface;
import android.os.RemoteException;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public interface n extends IInterface {
    String a() throws RemoteException;

    void a(y yVar) throws RemoteException;

    Intent b() throws RemoteException;

    Map c() throws RemoteException;
}
