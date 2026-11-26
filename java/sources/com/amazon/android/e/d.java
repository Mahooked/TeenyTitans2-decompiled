package com.amazon.android.e;

import android.app.Activity;
import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class d implements Task {
    private /* synthetic */ a a;
    private /* synthetic */ c b;

    d(c cVar, a aVar) {
        this.b = cVar;
        this.a = aVar;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        Activity visible = this.b.d.getVisible();
        if (visible == null) {
            c.a.trace("No activity to call startActivityForResult on. startActivityForResult when an activity becomes visible");
        } else {
            this.a.a(visible);
        }
    }
}
