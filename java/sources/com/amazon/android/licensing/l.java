package com.amazon.android.licensing;

import android.app.Application;
import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.TaskManager;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.mas.kiwi.util.BC1;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import java.io.IOException;
import java.util.Date;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class l extends AbstractCommandTask {
    private static final KiwiLogger a = new KiwiLogger("VerifyApplicationEntitlmentTask");
    private LicenseFailurePromptContentMapper b = new LicenseFailurePromptContentMapper();

    @Resource
    private com.amazon.android.p.d c;

    @Resource
    private Application d;

    @Resource
    private TaskManager e;

    @Resource
    private com.amazon.android.n.a f;

    @Resource
    private com.amazon.android.l.c g;

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final Map getCommandData() {
        return null;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandName() {
        return "get_license";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandVersion() {
        return "1.0";
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final boolean isExecutionNeeded() {
        return this.f.a("APPLICATION_LICENSE") == null;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onException(KiwiException kiwiException) {
        PromptContent map = this.b.map(kiwiException);
        if (map == null && KiwiLogger.ERROR_ON) {
            a.error("No mapping specified for exception: " + kiwiException, kiwiException);
        }
        this.f.a("LICENSE_FAILURE_CONTENT", map);
        this.f.a("LICENSE_FAILURE_RAW_EXCEPTION", kiwiException.getClass().getName());
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onFailure(FailureResult failureResult) throws RemoteException, KiwiException {
        PromptContent promptContent = new PromptContent(failureResult.getDisplayableName(), failureResult.getDisplayableMessage(), failureResult.getButtonLabel(), failureResult.show());
        a.trace("onFailure: " + promptContent);
        this.f.a("LICENSE_FAILURE_CONTENT", promptContent);
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onSuccess(SuccessResult successResult) throws RemoteException, KiwiException {
        c cVar = new c(successResult.getData());
        f fVar = new f(new com.amazon.android.l.d(cVar.a, this.g.a()));
        com.amazon.android.i.a aVar = new com.amazon.android.i.a();
        aVar.a(cVar.b, fVar.b, k.CUSTOMER_ID);
        aVar.a(cVar.c, fVar.c, k.DEVICE_ID);
        aVar.a(fVar.e, this.d.getPackageName(), k.PACKAGE_NAME);
        Date date = fVar.d;
        Date date2 = new Date();
        k kVar = k.EXPIRATION;
        if (date.compareTo(date2) <= 0) {
            aVar.a.put(kVar, new com.amazon.android.i.b(kVar, "'" + date + "' <= '" + date2 + "'"));
        }
        try {
            aVar.a(fVar.a, BC1.getBC1ChecksumBase64(this.d.getPackageCodePath()), k.CHECKSUM);
        } catch (IOException e) {
            k kVar2 = k.CHECKSUM;
            aVar.a.put(kVar2, new com.amazon.android.i.b(kVar2, "Exception: " + e));
        }
        if (aVar.a()) {
            throw new com.amazon.android.r.a(aVar);
        }
        a.trace("License Verification succeeded!");
        this.f.a.a("APPLICATION_LICENSE", (com.amazon.android.n.b) new d(this, fVar, fVar.d));
        this.c.a(new com.amazon.android.o.a());
        this.e.enqueue(TaskPipelineId.BACKGROUND, new n());
    }
}
