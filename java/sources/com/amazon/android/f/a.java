package com.amazon.android.f;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.android.p.b;
import com.amazon.android.p.d;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class a extends AbstractCommandTask {
    private static final KiwiLogger a = new KiwiLogger("SubmitMetricsTask");

    @Resource
    private d b;
    private com.amazon.android.p.a c;

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final Map getCommandData() {
        HashMap map = new HashMap();
        ArrayList arrayList = new ArrayList(this.c.b());
        Iterator it = this.c.iterator();
        while (it.hasNext()) {
            arrayList.add(((b) it.next()).a);
        }
        map.put("metrics", arrayList);
        return map;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandName() {
        return "submit_metrics";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandVersion() {
        return "1.0";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final boolean isExecutionNeeded() {
        return !this.c.a();
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onFailure(FailureResult failureResult) throws RemoteException, KiwiException {
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onSuccess(SuccessResult successResult) throws RemoteException, KiwiException {
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void preExecution() throws KiwiException {
        this.c = this.b.a();
        if (KiwiLogger.TRACE_ON) {
            a.trace("--------------- SUBMIT METRICS -------------------");
            a.trace("Size: " + this.c.b());
            a.trace("--------------------------------------------------");
        }
    }
}
