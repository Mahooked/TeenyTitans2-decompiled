package com.amazon.android.licensing;

import android.os.Build;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.android.framework.prompt.PromptContent;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public class LicenseFailurePromptContentMapper {
    private final Map mappings = new HashMap();

    public LicenseFailurePromptContentMapper() {
        if ("Amazon".equals(Build.MANUFACTURER)) {
            register(com.amazon.android.b.g.class, i.b);
        } else {
            register(com.amazon.android.b.g.class, i.a);
        }
        register(com.amazon.android.b.f.class, i.b);
        register(com.amazon.android.b.a.class, i.b);
        register(com.amazon.android.b.d.class, i.c);
        register(com.amazon.android.b.e.class, new m());
        register(com.amazon.android.b.h.class, i.e);
        register(com.amazon.android.b.b.class, i.e);
        register(com.amazon.android.r.a.class, new j());
        register(com.amazon.android.g.a.class, i.g);
        register(com.amazon.android.g.b.class, i.e);
        register(com.amazon.android.g.c.class, i.e);
    }

    private void register(Class cls, PromptContent promptContent) {
        register(cls, new g(this, promptContent));
    }

    private void register(Class cls, h hVar) {
        com.amazon.android.d.a.b(this.mappings.containsKey(cls), "mapping exists for type: " + cls);
        this.mappings.put(cls, hVar);
    }

    public PromptContent map(KiwiException kiwiException) {
        h hVar = (h) this.mappings.get(kiwiException.getClass());
        if (hVar == null) {
            return null;
        }
        return hVar.a(kiwiException);
    }
}
