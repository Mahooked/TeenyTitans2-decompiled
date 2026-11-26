package com.amazon.android.c;

import java.io.File;
import java.io.FilenameFilter;

/* JADX INFO: loaded from: classes.dex */
final class c implements FilenameFilter {
    private /* synthetic */ a a;

    c(a aVar) {
        this.a = aVar;
    }

    @Override // java.io.FilenameFilter
    public final boolean accept(File file, String str) {
        return str.endsWith(".amzst") && !this.a.e.containsValue(str);
    }
}
