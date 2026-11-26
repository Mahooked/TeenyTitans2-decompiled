package com.amazon.android.framework.prompt;

import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import com.amazon.android.framework.context.ContextManager;

/* JADX INFO: loaded from: classes.dex */
final class g extends AsyncTask {
    private final Context a;
    private final ContextManager b;

    /* synthetic */ g(Context context, ContextManager contextManager) {
        this(context, contextManager, (byte) 0);
    }

    private g(Context context, ContextManager contextManager, byte b) {
        this.a = context;
        this.b = contextManager;
    }

    @Override // android.os.AsyncTask
    public final /* bridge */ /* synthetic */ Object doInBackground(Object[] objArr) {
        this.a.startActivity(((Intent[]) objArr)[0]);
        return null;
    }

    @Override // android.os.AsyncTask
    public final /* bridge */ /* synthetic */ void onPostExecute(Object obj) {
        e.b();
    }
}
