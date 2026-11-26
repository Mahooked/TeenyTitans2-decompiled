package com.amazon.android;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;
import com.amazon.android.framework.prompt.PromptManager;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.task.command.AbstractCommandTask;
import com.amazon.android.framework.util.KiwiLogger;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public final class g extends AbstractCommandTask {
    private static final KiwiLogger a = new KiwiLogger("CheckIfAppisBlockedTask");

    @Resource
    private PromptManager b;

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final Map getCommandData() {
        return null;
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final String getCommandName() {
        return "check_blocked_status";
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
        if (isWorkflowChild()) {
            quitParentWorkflow();
        }
        a.test("app is blocked, killing");
        this.b.present(new com.amazon.android.framework.prompt.e(new PromptContent(failureResult.getDisplayableName(), failureResult.getDisplayableMessage(), failureResult.getButtonLabel(), failureResult.show())));
    }

    @Override // com.amazon.android.framework.task.command.AbstractCommandTask
    protected final void onSuccess(SuccessResult successResult) throws RemoteException, KiwiException {
        if (successResult.getData() == null || !successResult.getData().containsKey("verbose")) {
            return;
        }
        boolean zBooleanValue = ((Boolean) successResult.getData().get("verbose")).booleanValue();
        KiwiLogger.ERROR_ON = zBooleanValue;
        KiwiLogger.TRACE_ON = zBooleanValue;
    }
}
