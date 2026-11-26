package com.amazon.android.q;

import android.os.RemoteException;
import com.amazon.android.c.b;
import com.amazon.android.c.d;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class a extends AbstractCommandTask {
    private static final KiwiLogger a = new KiwiLogger("SubmitCrashReportsTask");

    @Resource
    private d b;
    private List c;

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final Map getCommandData() {
        HashMap map = new HashMap();
        ArrayList arrayList = new ArrayList();
        Iterator it = this.c.iterator();
        while (it.hasNext()) {
            arrayList.add(((b) it.next()).a());
        }
        map.put("reports", arrayList);
        return map;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandName() {
        return "submit_crash_reports";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandVersion() {
        return "1.0";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final boolean isExecutionNeeded() {
        return !this.c.isEmpty();
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onFailure(FailureResult failureResult) throws RemoteException, KiwiException {
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onSuccess(SuccessResult successResult) throws RemoteException, KiwiException {
        this.b.a(this.c);
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void preExecution() throws KiwiException {
        this.c = this.b.a();
        if (KiwiLogger.TRACE_ON) {
            a.trace("--------------- Crash Reports -------------------");
            a.trace("Size: " + this.c.size());
            a.trace("--------------------------------------------------");
        }
    }
}
