package com.google.android.gms.internal;

import android.content.Context;
import android.graphics.Typeface;
import android.graphics.drawable.AnimationDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.ShapeDrawable;
import android.graphics.drawable.shapes.RoundRectShape;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import com.google.android.gms.common.internal.zzbo;
import java.util.Iterator;
import java.util.List;

/* JADX INFO: loaded from: classes.dex */
@zzzn
public final class zzno extends RelativeLayout {
    private static final float[] zzHx = {5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f, 5.0f};

    @Nullable
    private AnimationDrawable zzHy;

    public zzno(Context context, zznn zznnVar, RelativeLayout.LayoutParams layoutParams) {
        super(context);
        zzbo.zzu(zznnVar);
        ShapeDrawable shapeDrawable = new ShapeDrawable(new RoundRectShape(zzHx, null, null));
        shapeDrawable.getPaint().setColor(zznnVar.getBackgroundColor());
        setLayoutParams(layoutParams);
        com.google.android.gms.ads.internal.zzbs.zzbB().setBackground(this, shapeDrawable);
        RelativeLayout.LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-2, -2);
        if (!TextUtils.isEmpty(zznnVar.getText())) {
            RelativeLayout.LayoutParams layoutParams3 = new RelativeLayout.LayoutParams(-2, -2);
            TextView textView = new TextView(context);
            textView.setLayoutParams(layoutParams3);
            textView.setId(1195835393);
            textView.setTypeface(Typeface.DEFAULT);
            textView.setText(zznnVar.getText());
            textView.setTextColor(zznnVar.getTextColor());
            textView.setTextSize(zznnVar.getTextSize());
            zzji.zzds();
            int iZzc = zzaiy.zzc(context, 4);
            zzji.zzds();
            textView.setPadding(iZzc, 0, zzaiy.zzc(context, 4), 0);
            addView(textView);
            layoutParams2.addRule(1, textView.getId());
        }
        ImageView imageView = new ImageView(context);
        imageView.setLayoutParams(layoutParams2);
        imageView.setId(1195835394);
        List<Drawable> listZzec = zznnVar.zzec();
        if (listZzec.size() > 1) {
            this.zzHy = new AnimationDrawable();
            Iterator<Drawable> it = listZzec.iterator();
            while (it.hasNext()) {
                this.zzHy.addFrame(it.next(), zznnVar.zzed());
            }
            com.google.android.gms.ads.internal.zzbs.zzbB().setBackground(imageView, this.zzHy);
        } else if (listZzec.size() == 1) {
            imageView.setImageDrawable(listZzec.get(0));
        }
        addView(imageView);
    }

    @Override // android.view.ViewGroup, android.view.View
    public final void onAttachedToWindow() {
        if (this.zzHy != null) {
            this.zzHy.start();
        }
        super.onAttachedToWindow();
    }
}
