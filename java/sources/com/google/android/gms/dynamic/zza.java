package com.google.android.gms.dynamic;

import android.R;
import android.app.Activity;
import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.FrameLayout;
import android.widget.LinearLayout;
import android.widget.TextView;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.common.internal.zzs;
import com.google.android.gms.dynamic.LifecycleDelegate;
import java.util.LinkedList;

/* JADX INFO: loaded from: classes.dex */
public abstract class zza<T extends LifecycleDelegate> {
    private T zzaSr;
    private Bundle zzaSs;
    private LinkedList<zzi> zzaSt;
    private final zzo<T> zzaSu = new zzb(this);

    static /* synthetic */ Bundle zza(zza zzaVar, Bundle bundle) {
        zzaVar.zzaSs = null;
        return null;
    }

    private final void zza(Bundle bundle, zzi zziVar) {
        if (this.zzaSr != null) {
            zziVar.zzb(this.zzaSr);
            return;
        }
        if (this.zzaSt == null) {
            this.zzaSt = new LinkedList<>();
        }
        this.zzaSt.add(zziVar);
        if (bundle != null) {
            if (this.zzaSs == null) {
                this.zzaSs = (Bundle) bundle.clone();
            } else {
                this.zzaSs.putAll(bundle);
            }
        }
        zza(this.zzaSu);
    }

    private final void zzaR(int i) {
        while (!this.zzaSt.isEmpty() && this.zzaSt.getLast().getState() >= i) {
            this.zzaSt.removeLast();
        }
    }

    public static void zzb(FrameLayout frameLayout) {
        GoogleApiAvailability googleApiAvailability = GoogleApiAvailability.getInstance();
        Context context = frameLayout.getContext();
        int iIsGooglePlayServicesAvailable = googleApiAvailability.isGooglePlayServicesAvailable(context);
        String strZzi = zzs.zzi(context, iIsGooglePlayServicesAvailable);
        String strZzk = zzs.zzk(context, iIsGooglePlayServicesAvailable);
        LinearLayout linearLayout = new LinearLayout(frameLayout.getContext());
        linearLayout.setOrientation(1);
        linearLayout.setLayoutParams(new FrameLayout.LayoutParams(-2, -2));
        frameLayout.addView(linearLayout);
        TextView textView = new TextView(frameLayout.getContext());
        textView.setLayoutParams(new FrameLayout.LayoutParams(-2, -2));
        textView.setText(strZzi);
        linearLayout.addView(textView);
        Intent intentZza = com.google.android.gms.common.zze.zza(context, iIsGooglePlayServicesAvailable, null);
        if (intentZza != null) {
            Button button = new Button(context);
            button.setId(R.id.button1);
            button.setLayoutParams(new FrameLayout.LayoutParams(-2, -2));
            button.setText(strZzk);
            linearLayout.addView(button);
            button.setOnClickListener(new zzf(context, intentZza));
        }
    }

    public void onCreate(Bundle bundle) {
        zza(bundle, new zzd(this, bundle));
    }

    public final View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        FrameLayout frameLayout = new FrameLayout(layoutInflater.getContext());
        zza(bundle, new zze(this, frameLayout, layoutInflater, viewGroup, bundle));
        if (this.zzaSr == null) {
            zza(frameLayout);
        }
        return frameLayout;
    }

    public void onDestroy() {
        if (this.zzaSr != null) {
            this.zzaSr.onDestroy();
        } else {
            zzaR(1);
        }
    }

    public final void onDestroyView() {
        if (this.zzaSr != null) {
            this.zzaSr.onDestroyView();
        } else {
            zzaR(2);
        }
    }

    public final void onInflate(Activity activity, Bundle bundle, Bundle bundle2) {
        zza(bundle2, new zzc(this, activity, bundle, bundle2));
    }

    public final void onLowMemory() {
        if (this.zzaSr != null) {
            this.zzaSr.onLowMemory();
        }
    }

    public void onPause() {
        if (this.zzaSr != null) {
            this.zzaSr.onPause();
        } else {
            zzaR(5);
        }
    }

    public void onResume() {
        zza((Bundle) null, new zzh(this));
    }

    public final void onSaveInstanceState(Bundle bundle) {
        if (this.zzaSr != null) {
            this.zzaSr.onSaveInstanceState(bundle);
        } else if (this.zzaSs != null) {
            bundle.putAll(this.zzaSs);
        }
    }

    public void onStart() {
        zza((Bundle) null, new zzg(this));
    }

    public void onStop() {
        if (this.zzaSr != null) {
            this.zzaSr.onStop();
        } else {
            zzaR(4);
        }
    }

    protected void zza(FrameLayout frameLayout) {
        GoogleApiAvailability googleApiAvailability = GoogleApiAvailability.getInstance();
        Context context = frameLayout.getContext();
        int iIsGooglePlayServicesAvailable = googleApiAvailability.isGooglePlayServicesAvailable(context);
        String strZzi = zzs.zzi(context, iIsGooglePlayServicesAvailable);
        String strZzk = zzs.zzk(context, iIsGooglePlayServicesAvailable);
        LinearLayout linearLayout = new LinearLayout(frameLayout.getContext());
        linearLayout.setOrientation(1);
        linearLayout.setLayoutParams(new FrameLayout.LayoutParams(-2, -2));
        frameLayout.addView(linearLayout);
        TextView textView = new TextView(frameLayout.getContext());
        textView.setLayoutParams(new FrameLayout.LayoutParams(-2, -2));
        textView.setText(strZzi);
        linearLayout.addView(textView);
        Intent intentZza = com.google.android.gms.common.zze.zza(context, iIsGooglePlayServicesAvailable, null);
        if (intentZza != null) {
            Button button = new Button(context);
            button.setId(R.id.button1);
            button.setLayoutParams(new FrameLayout.LayoutParams(-2, -2));
            button.setText(strZzk);
            linearLayout.addView(button);
            button.setOnClickListener(new zzf(context, intentZza));
        }
    }

    protected abstract void zza(zzo<T> zzoVar);

    public final T zztx() {
        return this.zzaSr;
    }
}
