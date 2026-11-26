package com.google.android.gms.internal;

import android.os.RemoteException;
import com.google.android.gms.ads.reward.RewardItem;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzadg implements RewardItem {
    private final zzacv zzWw;

    public zzadg(zzacv zzacvVar) {
        this.zzWw = zzacvVar;
    }

    @Override // com.google.android.gms.ads.reward.RewardItem
    public final int getAmount() {
        if (this.zzWw == null) {
            return 0;
        }
        try {
            return this.zzWw.getAmount();
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward getAmount to RewardItem", e);
            return 0;
        }
    }

    @Override // com.google.android.gms.ads.reward.RewardItem
    public final String getType() {
        if (this.zzWw == null) {
            return null;
        }
        try {
            return this.zzWw.getType();
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward getType to RewardItem", e);
            return null;
        }
    }
}
