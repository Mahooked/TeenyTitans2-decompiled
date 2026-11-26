package com.amazon.android.framework.task.command;

import android.os.Binder;
import android.os.RemoteException;
import com.amazon.venezia.command.FailureResult;
import com.amazon.venezia.command.SuccessResult;
import com.amazon.venezia.command.r;

/* JADX INFO: loaded from: classes.dex */
public final class m {
    String a;
    RemoteException b;
    SuccessResult c;
    FailureResult d;
    r e;
    com.amazon.venezia.command.j f;
    private int g = Binder.getCallingUid();

    public m(FailureResult failureResult) {
        this.d = failureResult;
        try {
            this.a = failureResult.getAuthToken();
        } catch (RemoteException e) {
            this.b = e;
        }
    }

    public m(SuccessResult successResult) {
        this.c = successResult;
        try {
            this.a = successResult.getAuthToken();
        } catch (RemoteException e) {
            this.b = e;
        }
    }

    public m(com.amazon.venezia.command.j jVar) {
        this.f = jVar;
    }

    public m(r rVar) {
        this.e = rVar;
        try {
            this.a = rVar.a();
        } catch (RemoteException e) {
            this.b = e;
        }
    }

    public final String toString() {
        StringBuilder sb = new StringBuilder("CommandResult: [");
        sb.append("CallingUid: ").append(this.g).append(", SuccessResult: ").append(this.c).append(", FailureResult: ").append(this.d).append(", DecisionResult: ").append(this.e).append(", ExceptionResult: ").append(this.f).append("]");
        return sb.toString();
    }
}
