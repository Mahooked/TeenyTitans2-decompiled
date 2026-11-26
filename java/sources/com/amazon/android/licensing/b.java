package com.amazon.android.licensing;

import com.amazon.android.framework.prompt.PromptContent;
import com.amazon.android.framework.prompt.PromptManager;
import com.amazon.android.framework.resource.Resource;
import com.amazon.android.framework.util.KiwiLogger;

/* JADX INFO: loaded from: classes.dex */
public final class b extends com.amazon.android.k.c {
    private static final KiwiLogger a = new KiwiLogger("LicenseKillTask");

    @Resource
    private com.amazon.android.n.a b;

    @Resource
    private PromptManager c;

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        if (KiwiLogger.TRACE_ON) {
            a.trace("License Kill Task Executing!!!");
        }
        if (this.b.b("APPLICATION_LICENSE")) {
            a.test("license verification succeeded");
            return;
        }
        if (KiwiLogger.TRACE_ON) {
            a.trace("License Kill Task determined app is not licensed, killing app");
        }
        if (isWorkflowChild()) {
            quitParentWorkflow();
        }
        PromptContent promptContent = (PromptContent) this.b.a("LICENSE_FAILURE_CONTENT");
        if (promptContent != null) {
            if (KiwiLogger.TRACE_ON) {
                a.trace("Fetched failure content from store: " + promptContent);
            }
            this.b.a.c("LICENSE_FAILURE_CONTENT");
        } else {
            promptContent = i.e;
        }
        this.c.present(new com.amazon.android.framework.prompt.e(promptContent));
    }
}
