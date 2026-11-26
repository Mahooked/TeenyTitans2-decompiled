package com.amazon.android.b;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.venezia.command.j;

/* JADX INFO: loaded from: classes.dex */
public class e extends KiwiException {
    private static final long serialVersionUID = 1;

    public e(j jVar) throws RemoteException {
        super("EXCEPTION_RESULT_FAILURE", jVar.a());
    }
}
