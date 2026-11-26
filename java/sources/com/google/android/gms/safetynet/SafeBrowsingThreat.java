package com.google.android.gms.safetynet;

/* JADX INFO: loaded from: classes.dex */
public class SafeBrowsingThreat {
    public static final int TYPE_POTENTIALLY_HARMFUL_APPLICATION = 4;
    public static final int TYPE_SOCIAL_ENGINEERING = 5;
    private int zzbBK;

    public SafeBrowsingThreat(int i) {
        this.zzbBK = i;
    }

    public int getThreatType() {
        return this.zzbBK;
    }
}
