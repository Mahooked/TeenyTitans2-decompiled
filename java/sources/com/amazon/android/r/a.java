package com.amazon.android.r;

import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.i.b;
import java.util.Iterator;

/* JADX INFO: loaded from: classes.dex */
public class a extends KiwiException {
    private static final long serialVersionUID = 1;
    public final com.amazon.android.i.a a;

    public a(com.amazon.android.i.a aVar) {
        super("LICENSE_VERIFICATION_FAILURE", "VERIFICATION_ERRORS", a(aVar));
        com.amazon.android.d.a.a(aVar.a(), "Created a verification exception with a Verifier that has no errors");
        this.a = aVar;
    }

    private static String a(com.amazon.android.i.a aVar) {
        StringBuilder sb = new StringBuilder();
        Iterator it = aVar.iterator();
        while (it.hasNext()) {
            b bVar = (b) it.next();
            if (sb.length() != 0) {
                sb.append(",");
            }
            sb.append(bVar.a.a());
        }
        return sb.toString();
    }

    @Override // java.lang.Throwable
    public String toString() {
        return this.a.toString();
    }
}
