package com.amazon.android.licensing;

import com.amazon.android.framework.resource.Resource;

/* JADX INFO: loaded from: classes.dex */
public final class e extends com.amazon.android.k.b {

    @Resource
    private com.amazon.android.framework.task.command.c a;

    public e() {
        a(new l());
        a(new com.amazon.android.f.a());
        a(new b());
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
        return "LicenseEnforcementTaskWorkflow";
    }
}
