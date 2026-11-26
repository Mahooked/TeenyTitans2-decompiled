package com.amazon.android.h;

import com.amazon.android.framework.util.KiwiLogger;
import java.util.Date;

/* JADX INFO: loaded from: classes.dex */
public abstract class c extends b {
    private static final KiwiLogger LOGGER = new KiwiLogger("RelativeExpirable");
    private static final long MILLISECONDS_PER_SECOND = 1000;
    private final Date instantiation = new Date();

    @Override // com.amazon.android.h.b
    public final Date getExpiration() {
        LOGGER.trace("RelativeExpiration duration: " + getExpirationDurationInSeconds() + ", expirable: " + this);
        Date date = new Date(this.instantiation.getTime() + (1000 * getExpirationDurationInSeconds()));
        LOGGER.trace("Expiration should occur at time: " + date);
        return date;
    }

    protected abstract long getExpirationDurationInSeconds();
}
