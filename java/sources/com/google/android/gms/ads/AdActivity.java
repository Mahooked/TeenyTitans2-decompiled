package com.google.android.gms.ads;

import android.app.Activity;
import android.app.Dialog;
import android.content.Intent;
import android.content.res.Configuration;
import android.os.Bundle;
import android.os.RemoteException;
import android.view.View;
import android.view.ViewGroup;
import com.amazon.android.Kiwi;
import com.google.android.gms.common.annotation.KeepForSdkWithMembers;
import com.google.android.gms.dynamic.zzn;
import com.google.android.gms.internal.zzajc;
import com.google.android.gms.internal.zzji;
import com.google.android.gms.internal.zzwx;

/* JADX INFO: loaded from: classes.dex */
@KeepForSdkWithMembers
public class AdActivity extends Activity {
    public static final String CLASS_NAME = "com.google.android.gms.ads.AdActivity";
    public static final String SIMPLE_CLASS_NAME = "AdActivity";
    private zzwx zzrP;

    private void onActivityResultAdActivity(int i, int i2, Intent intent) {
        try {
            this.zzrP.onActivityResult(i, i2, intent);
        } catch (Exception e) {
            zzajc.zzc("Could not forward onActivityResult to ad overlay:", e);
        }
        super.onActivityResult(i, i2, intent);
    }

    private void onCreateAdActivity(Bundle bundle) {
        super.onCreate(bundle);
        this.zzrP = zzji.zzdt().zzb(this);
        if (this.zzrP == null) {
            zzajc.zzaT("Could not create ad overlay.");
            finish();
            return;
        }
        try {
            this.zzrP.onCreate(bundle);
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onCreate to ad overlay:", e);
            finish();
        }
    }

    private void onDestroyAdActivity() {
        try {
            if (this.zzrP != null) {
                this.zzrP.onDestroy();
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onDestroy to ad overlay:", e);
        }
        super.onDestroy();
    }

    private void onPauseAdActivity() {
        try {
            if (this.zzrP != null) {
                this.zzrP.onPause();
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onPause to ad overlay:", e);
            finish();
        }
        super.onPause();
    }

    private void onResumeAdActivity() {
        super.onResume();
        try {
            if (this.zzrP != null) {
                this.zzrP.onResume();
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onResume to ad overlay:", e);
            finish();
        }
    }

    private void onStartAdActivity() {
        super.onStart();
        try {
            if (this.zzrP != null) {
                this.zzrP.onStart();
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onStart to ad overlay:", e);
            finish();
        }
    }

    private void onStopAdActivity() {
        try {
            if (this.zzrP != null) {
                this.zzrP.onStop();
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onStop to ad overlay:", e);
            finish();
        }
        super.onStop();
    }

    private final void zzaa() {
        if (this.zzrP != null) {
            try {
                this.zzrP.zzaa();
            } catch (RemoteException e) {
                zzajc.zzc("Could not forward setContentViewSet to ad overlay:", e);
            }
        }
    }

    @Override // android.app.Activity
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (Kiwi.onActivityResult(this, requestCode, resultCode, data)) {
            return;
        }
        onActivityResultAdActivity(requestCode, resultCode, data);
    }

    @Override // android.app.Activity
    public void onBackPressed() {
        boolean zZzfK = true;
        try {
            if (this.zzrP != null) {
                zZzfK = this.zzrP.zzfK();
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onBackPressed to ad overlay:", e);
        }
        if (zZzfK) {
            super.onBackPressed();
        }
    }

    @Override // android.app.Activity, android.content.ComponentCallbacks
    public void onConfigurationChanged(Configuration configuration) {
        super.onConfigurationChanged(configuration);
        try {
            this.zzrP.zzo(zzn.zzw(configuration));
        } catch (RemoteException e) {
            zzajc.zzc("Failed to wrap configuration.", e);
        }
    }

    @Override // android.app.Activity
    public void onCreate(Bundle bundle) {
        onCreateAdActivity(bundle);
        Kiwi.onCreate((Activity) this, true);
    }

    @Override // android.app.Activity
    public Dialog onCreateDialog(int i) {
        Dialog dialog = Kiwi.onCreateDialog(this, i);
        return dialog != null ? dialog : super.onCreateDialog(i);
    }

    @Override // android.app.Activity
    public Dialog onCreateDialog(int i, Bundle bundle) {
        Dialog dialog = Kiwi.onCreateDialog(this, i);
        return dialog != null ? dialog : super.onCreateDialog(i, bundle);
    }

    @Override // android.app.Activity
    public void onDestroy() {
        onDestroyAdActivity();
        Kiwi.onDestroy(this);
    }

    @Override // android.app.Activity
    public void onPause() {
        onPauseAdActivity();
        Kiwi.onPause(this);
    }

    @Override // android.app.Activity
    protected void onRestart() {
        super.onRestart();
        try {
            if (this.zzrP != null) {
                this.zzrP.onRestart();
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onRestart to ad overlay:", e);
            finish();
        }
    }

    @Override // android.app.Activity
    public void onResume() {
        onResumeAdActivity();
        Kiwi.onResume(this);
    }

    @Override // android.app.Activity
    protected void onSaveInstanceState(Bundle bundle) {
        try {
            if (this.zzrP != null) {
                this.zzrP.onSaveInstanceState(bundle);
            }
        } catch (RemoteException e) {
            zzajc.zzc("Could not forward onSaveInstanceState to ad overlay:", e);
            finish();
        }
        super.onSaveInstanceState(bundle);
    }

    @Override // android.app.Activity
    public void onStart() {
        onStartAdActivity();
        Kiwi.onStart(this);
    }

    @Override // android.app.Activity
    public void onStop() {
        onStopAdActivity();
        Kiwi.onStop(this);
    }

    @Override // android.app.Activity
    public void setContentView(int i) {
        super.setContentView(i);
        zzaa();
    }

    @Override // android.app.Activity
    public void setContentView(View view) {
        super.setContentView(view);
        zzaa();
    }

    @Override // android.app.Activity
    public void setContentView(View view, ViewGroup.LayoutParams layoutParams) {
        super.setContentView(view, layoutParams);
        zzaa();
    }
}
