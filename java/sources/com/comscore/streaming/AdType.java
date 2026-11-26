package com.comscore.streaming;

/* JADX INFO: loaded from: classes.dex */
public enum AdType {
    LinearOnDemandPreRoll("a11"),
    LinearOnDemandMidRoll("a12"),
    LinearOnDemandPostRoll("a13"),
    LinearLive("a21"),
    Other("a00");

    private final String a;

    AdType(String str) {
        this.a = str;
    }

    protected String a() {
        return this.a;
    }
}
