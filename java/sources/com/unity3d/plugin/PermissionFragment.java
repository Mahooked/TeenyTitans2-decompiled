package com.unity3d.plugin;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
public class PermissionFragment extends Fragment {
    private static final int PERMISSIONS_REQUEST_CODE = 15887;
    public static final String PERMISSION_NAMES = "PermissionNames";
    private final UnityAndroidPermissions.IPermissionRequestResult m_ResultCallbacks;

    public PermissionFragment() {
        this.m_ResultCallbacks = null;
    }

    public PermissionFragment(UnityAndroidPermissions.IPermissionRequestResult resultCallbacks) {
        this.m_ResultCallbacks = resultCallbacks;
    }

    @Override // android.app.Fragment
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        if (this.m_ResultCallbacks == null) {
            getFragmentManager().beginTransaction().remove(this).commit();
        } else {
            String[] permissionNames = getArguments().getStringArray(PERMISSION_NAMES);
            requestPermissions(permissionNames, PERMISSIONS_REQUEST_CODE);
        }
    }

    @Override // android.app.Fragment
    public void onRequestPermissionsResult(int requestCode, String[] permissions, int[] grantResults) {
        if (requestCode == PERMISSIONS_REQUEST_CODE) {
            for (int i = 0; i < permissions.length && i < grantResults.length; i++) {
                if (grantResults[i] == 0) {
                    this.m_ResultCallbacks.OnPermissionGranted(permissions[i]);
                } else {
                    this.m_ResultCallbacks.OnPermissionDenied(permissions[i]);
                }
            }
            FragmentTransaction fragmentTransaction = getFragmentManager().beginTransaction();
            fragmentTransaction.remove(this);
            fragmentTransaction.commit();
        }
    }
}
