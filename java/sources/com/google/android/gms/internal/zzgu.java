package com.google.android.gms.internal;

import android.support.annotation.Nullable;
import android.support.v4.widget.ExploreByTouchHelper;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzgu {
    private int zzyl;
    private final Object mLock = new Object();
    private List<zzgt> zzym = new LinkedList();

    public final boolean zza(zzgt zzgtVar) {
        boolean z;
        synchronized (this.mLock) {
            z = this.zzym.contains(zzgtVar);
        }
        return z;
    }

    public final boolean zzb(zzgt zzgtVar) {
        synchronized (this.mLock) {
            Iterator<zzgt> it = this.zzym.iterator();
            while (it.hasNext()) {
                zzgt next = it.next();
                if (!((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzCZ)).booleanValue() || com.google.android.gms.ads.internal.zzbs.zzbD().zzhn()) {
                    if (((Boolean) com.google.android.gms.ads.internal.zzbs.zzbL().zzd(zzmo.zzDb)).booleanValue() && !com.google.android.gms.ads.internal.zzbs.zzbD().zzho() && zzgtVar != next && next.zzcF().equals(zzgtVar.zzcF())) {
                        it.remove();
                        return true;
                    }
                } else if (zzgtVar != next && next.zzcD().equals(zzgtVar.zzcD())) {
                    it.remove();
                    return true;
                }
            }
            return false;
        }
    }

    public final void zzc(zzgt zzgtVar) {
        synchronized (this.mLock) {
            if (this.zzym.size() >= 10) {
                zzafr.zzaC(new StringBuilder(41).append("Queue is full, current size = ").append(this.zzym.size()).toString());
                this.zzym.remove(0);
            }
            int i = this.zzyl;
            this.zzyl = i + 1;
            zzgtVar.zzj(i);
            this.zzym.add(zzgtVar);
        }
    }

    @Nullable
    public final zzgt zzcL() {
        int i;
        zzgt zzgtVar;
        int i2;
        zzgt zzgtVar2 = null;
        int i3 = 0;
        synchronized (this.mLock) {
            if (this.zzym.size() == 0) {
                zzafr.zzaC("Queue empty");
                return null;
            }
            if (this.zzym.size() < 2) {
                zzgt zzgtVar3 = this.zzym.get(0);
                zzgtVar3.zzcG();
                return zzgtVar3;
            }
            int i4 = ExploreByTouchHelper.INVALID_ID;
            int i5 = 0;
            for (zzgt zzgtVar4 : this.zzym) {
                int score = zzgtVar4.getScore();
                if (score > i4) {
                    i2 = score;
                    zzgtVar = zzgtVar4;
                    i = i5;
                } else {
                    i = i3;
                    zzgtVar = zzgtVar2;
                    i2 = i4;
                }
                i5++;
                i4 = i2;
                zzgtVar2 = zzgtVar;
                i3 = i;
            }
            this.zzym.remove(i3);
            return zzgtVar2;
        }
    }
}
