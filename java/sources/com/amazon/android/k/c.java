package com.amazon.android.k;

/* JADX INFO: loaded from: classes.dex */
public abstract class c implements a {
    private b workflow;

    protected final boolean isWorkflowChild() {
        return this.workflow != null;
    }

    protected final void quitParentWorkflow() {
        com.amazon.android.d.a.a(isWorkflowChild(), "task is no a workflow child");
        this.workflow.c();
    }

    @Override // com.amazon.android.k.a
    public final void setWorkflow(b bVar) {
        com.amazon.android.d.a.a(bVar, "workflow");
        com.amazon.android.d.a.a(this.workflow == null, "workflow instance can only be set once");
        this.workflow = bVar;
    }
}
