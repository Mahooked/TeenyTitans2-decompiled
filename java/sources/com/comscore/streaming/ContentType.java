package com.comscore.streaming;

/* JADX INFO: loaded from: classes.dex */
public enum ContentType {
    LongFormOnDemand("c11"),
    ShortFormOnDemand("c12"),
    Live("c13"),
    UserGeneratedLongFormOnDemand("c21"),
    UserGeneratedShortFormOnDemand("c22"),
    UserGeneratedLive("c23"),
    Bumper("c99"),
    Other("c00");

    private final String a;

    ContentType(String str) {
        this.a = str;
    }

    protected String a() {
        return this.a;
    }
}
