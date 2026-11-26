package com.amazon.venezia.command;

import android.os.IInterface;
import android.os.RemoteException;

/* JADX INFO: loaded from: classes.dex */
public interface f extends IInterface {
    void a(FailureResult failureResult) throws RemoteException;

    void a(SuccessResult successResult) throws RemoteException;

    void a(j jVar) throws RemoteException;

    void a(r rVar) throws RemoteException;
}
