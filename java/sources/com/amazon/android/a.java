package com.amazon.android;

import com.amazon.android.framework.resource.Resource;

/* JADX INFO: loaded from: classes.dex */
public final class a extends com.amazon.android.k.b {

    @Resource
    private com.amazon.android.framework.task.command.c a;

    public a() {
        a(new g());
        a(new com.amazon.android.q.a());
        a(new com.amazon.android.licensing.e());
    }

    @Override // com.amazon.android.k.b
    protected final void a() {
        if (isWorkflowChild()) {
            return;
        }
        this.a.b();
    }

    @Override // com.amazon.android.k.b
    protected final String b() {
        return "DrmFullApplicationLaunchTaskWorkflow";
    }
}
