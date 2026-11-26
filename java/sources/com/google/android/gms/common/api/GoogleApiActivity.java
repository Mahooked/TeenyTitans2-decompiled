package com.google.android.gms.common.api;

import android.app.Activity;
import android.app.Dialog;
import android.app.PendingIntent;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.IntentSender;
import android.os.Bundle;
import android.util.Log;
import com.amazon.android.Kiwi;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.GoogleApiAvailability;
import com.google.android.gms.internal.zzbdb;

/* JADX INFO: loaded from: classes.dex */
public class GoogleApiActivity extends Activity implements DialogInterface.OnCancelListener {
    private int zzaAR = 0;

    private void onActivityResultGoogleApiActivity(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        if (i == 1) {
            boolean booleanExtra = getIntent().getBooleanExtra("notify_manager", true);
            this.zzaAR = 0;
            setResult(i2, intent);
            if (booleanExtra) {
                zzbdb zzbdbVarZzay = zzbdb.zzay(this);
                switch (i2) {
                    case -1:
                        zzbdbVarZzay.zzps();
                        break;
                    case 0:
                        zzbdbVarZzay.zza(new ConnectionResult(13, null), getIntent().getIntExtra("failing_client_id", -1));
                        break;
                }
            }
        } else if (i == 2) {
            this.zzaAR = 0;
            setResult(i2, intent);
        }
        finish();
    }

    private void onCreateGoogleApiActivity(Bundle bundle) {
        super.onCreate(bundle);
        if (bundle != null) {
            this.zzaAR = bundle.getInt("resolution");
        }
        if (this.zzaAR != 1) {
            Bundle extras = getIntent().getExtras();
            if (extras == null) {
                Log.e("GoogleApiActivity", "Activity started without extras");
                finish();
                return;
            }
            PendingIntent pendingIntent = (PendingIntent) extras.get("pending_intent");
            Integer num = (Integer) extras.get("error_code");
            if (pendingIntent == null && num == null) {
                Log.e("GoogleApiActivity", "Activity started without resolution");
                finish();
            } else {
                if (pendingIntent == null) {
                    GoogleApiAvailability.getInstance().showErrorDialogFragment(this, num.intValue(), 2, this);
                    this.zzaAR = 1;
                    return;
                }
                try {
                    startIntentSenderForResult(pendingIntent.getIntentSender(), 1, null, 0, 0, 0);
                    this.zzaAR = 1;
                } catch (IntentSender.SendIntentException e) {
                    Log.e("GoogleApiActivity", "Failed to launch pendingIntent", e);
                    finish();
                }
            }
        }
    }

    public static PendingIntent zza(Context context, PendingIntent pendingIntent, int i) {
        return PendingIntent.getActivity(context, 0, zza(context, pendingIntent, i, true), 134217728);
    }

    public static Intent zza(Context context, PendingIntent pendingIntent, int i, boolean z) {
        Intent intent = new Intent(context, (Class<?>) GoogleApiActivity.class);
        intent.putExtra("pending_intent", pendingIntent);
        intent.putExtra("failing_client_id", i);
        intent.putExtra("notify_manager", z);
        return intent;
    }

    @Override // android.app.Activity
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        if (Kiwi.onActivityResult(this, requestCode, resultCode, data)) {
            return;
        }
        onActivityResultGoogleApiActivity(requestCode, resultCode, data);
    }

    @Override // android.content.DialogInterface.OnCancelListener
    public void onCancel(DialogInterface dialogInterface) {
        this.zzaAR = 0;
        setResult(0);
        finish();
    }

    @Override // android.app.Activity
    public void onCreate(Bundle bundle) {
        onCreateGoogleApiActivity(bundle);
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
        super.onDestroy();
        Kiwi.onDestroy(this);
    }

    @Override // android.app.Activity
    public void onPause() {
        super.onPause();
        Kiwi.onPause(this);
    }

    @Override // android.app.Activity
    public void onResume() {
        super.onResume();
        Kiwi.onResume(this);
    }

    @Override // android.app.Activity
    protected void onSaveInstanceState(Bundle bundle) {
        bundle.putInt("resolution", this.zzaAR);
        super.onSaveInstanceState(bundle);
    }

    @Override // android.app.Activity
    public void onStart() {
        super.onStart();
        Kiwi.onStart(this);
    }

    @Override // android.app.Activity
    public void onStop() {
        super.onStop();
        Kiwi.onStop(this);
    }
}
