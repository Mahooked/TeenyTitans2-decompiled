package com.amazon.android.licensing;

import com.amazon.android.framework.task.pipeline.TaskPipelineId;
import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
final class d extends com.amazon.android.n.b {
    private /* synthetic */ l b;

    /* JADX WARN: 'super' call moved to the top of the method (can break code semantics) */
    d(l lVar, Object obj, Date date) {
        super(obj, date);
        this.b = lVar;
    }

    @Override // com.amazon.android.h.b
    public final void doExpiration() {
        this.taskManager.enqueue(TaskPipelineId.COMMAND, new l());
    }
}
