package com.amazon.android.framework.prompt;

import com.amazon.android.framework.task.Task;

/* JADX INFO: loaded from: classes.dex */
final class i implements Task {
    private /* synthetic */ Prompt a;
    private /* synthetic */ PromptManagerImpl b;

    i(PromptManagerImpl promptManagerImpl, Prompt prompt) {
        this.b = promptManagerImpl;
        this.a = prompt;
    }

    @Override // com.amazon.android.framework.task.Task
    public final void execute() {
        this.b.removeExpiredPrompt(this.a);
    }

    public final String toString() {
        return "PromptManager:removeExpiredPrompt: " + this.a;
    }
}
