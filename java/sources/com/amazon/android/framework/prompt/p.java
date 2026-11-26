package com.amazon.android.framework.prompt;

import android.os.AsyncTask;
import com.amazon.android.framework.context.ContextManager;

/* JADX INFO: loaded from: classes.dex */
final class p extends AsyncTask {
    private final ContextManager a;

    /* synthetic */ p(ContextManager contextManager) {
        this(contextManager, (byte) 0);
    }

    private p(ContextManager contextManager, byte b) {
        this.a = contextManager;
    }

    @Override // android.os.AsyncTask
    public final /* bridge */ /* synthetic */ Object doInBackground(Object[] objArr) {
        return null;
    }

    @Override // android.os.AsyncTask
    public final /* bridge */ /* synthetic */ void onPostExecute(Object obj) {
        e.b();
    }
}
