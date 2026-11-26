package com.amazon.android.s;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class a extends AbstractCommandTask {
    private Map a;

    public a(Map map) {
        this.a = map;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final Map getCommandData() {
        return this.a;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandName() {
        return "lifeCycle_Events";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandVersion() {
        return "1.0";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final boolean isExecutionNeeded() {
        return true;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onFailure(FailureResult failureResult) throws RemoteException, KiwiException {
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onSuccess(SuccessResult successResult) throws RemoteException, KiwiException {
    }
}
