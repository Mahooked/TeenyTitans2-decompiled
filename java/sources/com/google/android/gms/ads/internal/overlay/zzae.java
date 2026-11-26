package com.google.android.gms.ads.internal.overlay;

import android.R;
import android.content.Context;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.ImageButton;
import com.google.android.gms.internal.zzaiy;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzzn;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzae extends FrameLayout implements View.OnClickListener {
    private final ImageButton zzPG;
    private final zzaj zzPH;

    public zzae(Context context, zzaf zzafVar, zzaj zzajVar) {
        super(context);
        this.zzPH = zzajVar;
        setOnClickListener(this);
        this.zzPG = new ImageButton(context);
        this.zzPG.setImageResource(R.drawable.btn_dialog);
        this.zzPG.setBackgroundColor(0);
        this.zzPG.setOnClickListener(this);
        ImageButton imageButton = this.zzPG;
        zzji.zzds();
        int iZzc = zzaiy.zzc(context, zzafVar.paddingLeft);
        zzji.zzds();
        int iZzc2 = zzaiy.zzc(context, 0);
        zzji.zzds();
        int iZzc3 = zzaiy.zzc(context, zzafVar.paddingRight);
        zzji.zzds();
        imageButton.setPadding(iZzc, iZzc2, iZzc3, zzaiy.zzc(context, zzafVar.paddingBottom));
        this.zzPG.setContentDescription("Interstitial close button");
        zzji.zzds();
        zzaiy.zzc(context, zzafVar.size);
        ImageButton imageButton2 = this.zzPG;
        zzji.zzds();
        int iZzc4 = zzaiy.zzc(context, zzafVar.size + zzafVar.paddingLeft + zzafVar.paddingRight);
        zzji.zzds();
        addView(imageButton2, new FrameLayout.LayoutParams(iZzc4, zzaiy.zzc(context, zzafVar.size + zzafVar.paddingBottom), 17));
    }

    @Override // android.view.View.OnClickListener
    public final void onClick(View view) {
        if (this.zzPH != null) {
            this.zzPH.zzfJ();
        }
    }

    public final void zza(boolean z, boolean z2) {
        if (!z2) {
            this.zzPG.setVisibility(0);
        } else if (z) {
            this.zzPG.setVisibility(4);
        } else {
            this.zzPG.setVisibility(8);
        }
    }
}
