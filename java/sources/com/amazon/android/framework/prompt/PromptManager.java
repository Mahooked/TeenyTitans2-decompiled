package com.amazon.android.framework.prompt;

import android.app.Activity;
import android.app.Dialog;

/* JADX INFO: loaded from: classes.dex */
public interface PromptManager extends com.amazon.android.h.d {
    Dialog onCreateDialog(Activity activity, int i);

    void onWindowFocusChanged(Activity activity, boolean z);

    void present(Prompt prompt);
}
