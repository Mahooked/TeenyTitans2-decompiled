package com.amazon.android.b;

import android.os.DeadObjectException;
import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;

/* JADX INFO: loaded from: classes.dex */
public class a extends KiwiException {
    private static final long serialVersionUID = 1;

    public a(RemoteException remoteException) {
        super(remoteException instanceof DeadObjectException ? "COMMAND_SERVICE_DEAD_OBJECT_EXCEPTION" : "COMMAND_SERVICE_REMOTE_EXCEPTION");
    }
}
