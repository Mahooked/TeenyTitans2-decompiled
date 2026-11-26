package com.amazon.android.p;

import java.io.Serializable;
import java.util.HashMap;
import java.util.Map;

/* JADX INFO: loaded from: classes.dex */
public class b implements Serializable {
    private static final long serialVersionUID = 1;
    public final Map a = new HashMap();

    public b(String str) {
        this.a.put("name", str);
        this.a.put("time", String.valueOf(System.currentTimeMillis()));
    }

    public final b a(String str, String str2) {
        this.a.put(str, str2);
        return this;
    }

    public String toString() {
        return "Metric: [" + this.a.toString() + "]";
    }
}
