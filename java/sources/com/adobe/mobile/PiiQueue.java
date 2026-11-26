package com.adobe.mobile;

/* JADX INFO: loaded from: classes.dex */
final class PiiQueue extends ThirdPartyQueue {
    private static final String PII_HIT_CACHE = "ADBMobilePIICache.sqlite";
    private static PiiQueue _instance = null;
    private static final Object _instanceMutex = new Object();

    protected PiiQueue() {
    }

    @Override // com.adobe.mobile.ThirdPartyQueue
    protected String threadSuffix() {
        return "pii";
    }

    @Override // com.adobe.mobile.ThirdPartyQueue
    protected String logPrefix() {
        return "PII";
    }

    @Override // com.adobe.mobile.ThirdPartyQueue
    protected String fileName() {
        return PII_HIT_CACHE;
    }

    protected static PiiQueue sharedInstance() {
        PiiQueue piiQueue;
        synchronized (_instanceMutex) {
            if (_instance == null) {
                _instance = new PiiQueue();
            }
            piiQueue = _instance;
        }
        return piiQueue;
    }

    @Override // com.adobe.mobile.ThirdPartyQueue
    protected ThirdPartyQueue getWorker() {
        return sharedInstance();
    }
}
