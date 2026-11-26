package com.unity3d.plugin;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Build;
import android.os.Bundle;

/* JADX INFO: loaded from: classes.dex */
public class UnityAndroidPermissions {

    interface IPermissionRequestResult {
        void OnPermissionDenied(String str);

        void OnPermissionGranted(String str);
    }

    public boolean IsPermissionGranted(Activity activity, String permissionName) {
        if (Build.VERSION.SDK_INT < 23) {
            return true;
        }
        return activity != null && activity.checkSelfPermission(permissionName) == 0;
    }

    public boolean ShouldShowRequestPermissionRationale(Activity activity, String permissionName) {
        if (Build.VERSION.SDK_INT >= 23 && activity != null) {
            return activity.shouldShowRequestPermissionRationale(permissionName);
        }
        return false;
    }

    public void RequestPermissionAsync(Activity activity, String[] permissionNames, IPermissionRequestResult resultCallbacks) {
        if (Build.VERSION.SDK_INT >= 23 && activity != null && permissionNames != null && resultCallbacks != null) {
            Fragment request = new PermissionFragment(resultCallbacks);
            Bundle bundle = new Bundle();
            bundle.putStringArray(PermissionFragment.PERMISSION_NAMES, permissionNames);
            request.setArguments(bundle);
            FragmentTransaction fragmentTransaction = activity.getFragmentManager().beginTransaction();
            fragmentTransaction.add(0, request);
            fragmentTransaction.commit();
        }
    }
}
