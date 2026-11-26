package com.amazon.android.framework.prompt;

import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class n implements Task {
    private /* synthetic */ Prompt a;
    private /* synthetic */ PromptManagerImpl b;

    n(PromptManagerImpl promptManagerImpl, Prompt prompt) {
        this.b = promptManagerImpl;
        this.a = prompt;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        this.b.presentImpl(this.a);
    }

    public final String toString() {
        return "Prompt Presentation on Main Thread: " + this.a + ", " + this.a.getExpiration();
    }
}
