package com.amazon.android.framework.prompt;

import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class c implements Task {
    private /* synthetic */ Prompt a;

    c(Prompt prompt) {
        this.a = prompt;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        this.a.dismiss();
    }

    public final String toString() {
        return "DismissPromptTask: " + this.a.toString();
    }
}
