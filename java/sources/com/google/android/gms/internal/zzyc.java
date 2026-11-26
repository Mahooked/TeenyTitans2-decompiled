package com.google.android.gms.internal;

import android.annotation.TargetApi;
import android.app.Activity;
import android.content.Context;
import android.view.View;
import android.view.ViewGroup;
import android.view.Window;
import android.widget.FrameLayout;
import android.widget.PopupWindow;

/* JADX INFO: loaded from: classes.dex */
@zzzn
@TargetApi(19)
public final class zzyc extends zzxz {
    private Object zzQZ;
    private PopupWindow zzRa;
    private boolean zzRb;

    zzyc(Context context, zzafg zzafgVar, zzaka zzakaVar, zzxy zzxyVar) {
        super(context, zzafgVar, zzakaVar, zzxyVar);
        this.zzQZ = new Object();
        this.zzRb = false;
    }

    private final void zzgr() {
        synchronized (this.zzQZ) {
            this.zzRb = true;
            if ((this.mContext instanceof Activity) && ((Activity) this.mContext).isDestroyed()) {
                this.zzRa = null;
            }
            if (this.zzRa != null) {
                if (this.zzRa.isShowing()) {
                    this.zzRa.dismiss();
                }
                this.zzRa = null;
            }
        }
    }

    @Override // com.google.android.gms.internal.zzxr, com.google.android.gms.internal.zzahp
    public final void cancel() {
        zzgr();
        super.cancel();
    }

    @Override // com.google.android.gms.internal.zzxz
    protected final void zzgq() {
        Window window = this.mContext instanceof Activity ? ((Activity) this.mContext).getWindow() : null;
        if (window == null || window.getDecorView() == null || ((Activity) this.mContext).isDestroyed()) {
            return;
        }
        FrameLayout frameLayout = new FrameLayout(this.mContext);
        frameLayout.setLayoutParams(new ViewGroup.LayoutParams(-1, -1));
        frameLayout.addView(this.zzJH.getView(), -1, -1);
        synchronized (this.zzQZ) {
            if (this.zzRb) {
                return;
            }
            this.zzRa = new PopupWindow((View) frameLayout, 1, 1, false);
            this.zzRa.setOutsideTouchable(true);
            this.zzRa.setClippingEnabled(false);
            zzafr.zzaC("Displaying the 1x1 popup off the screen.");
            try {
                this.zzRa.showAtLocation(window.getDecorView(), 0, -1, -1);
            } catch (Exception e) {
                this.zzRa = null;
            }
        }
    }

    @Override // com.google.android.gms.internal.zzxr
    protected final void zzr(int i) {
        zzgr();
        super.zzr(i);
    }
}
