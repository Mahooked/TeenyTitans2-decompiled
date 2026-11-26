package com.amazon.android.e;

import android.app.Activity;

/* JADX INFO: loaded from: classes.dex */
final class e implements com.amazon.android.m.c {
    private /* synthetic */ c a;

    e(c cVar) {
        this.a = cVar;
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.f a() {
        return com.amazon.android.j.c.RESUME;
    }

    @Override // com.amazon.android.m.c
    public final /* bridge */ /* synthetic */ void a(com.amazon.android.m.d dVar) {
        com.amazon.android.j.b bVar = (com.amazon.android.j.b) dVar;
        a aVar = (a) this.a.f.get();
        if (aVar != null) {
            Activity activity = bVar.a;
            c.a.error("Context changed while awaiting result!");
            if (aVar.b != null) {
                c.a.error("Finishing activity from old context: " + aVar.b);
                aVar.b.finishActivity(aVar.a);
            }
            aVar.a(activity);
        }
    }

    @Override // com.amazon.android.m.c
    public final com.amazon.android.m.a b() {
        return com.amazon.android.m.a.MIDDLE;
    }
}
