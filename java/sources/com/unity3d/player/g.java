package com.unity3d.player;

import android.app.Fragment;
import android.app.FragmentTransaction;
import android.os.Bundle;
import com.unity3d.plugin.PermissionFragment;

/* JADX INFO: loaded from: classes.dex */
public final class g extends Fragment {
    private final Runnable a;

    public g(Runnable runnable) {
        this.a = runnable;
    }

    @Override // android.app.Fragment
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        requestPermissions(getArguments().getStringArray(PermissionFragment.PERMISSION_NAMES), 15881);
    }

    @Override // android.app.Fragment
    public final void onRequestPermissionsResult(int i, String[] strArr, int[] iArr) {
        if (i != 15881) {
            return;
        }
        for (int i2 = 0; i2 < strArr.length && i2 < iArr.length; i2++) {
            e.Log(4, strArr[i2] + (iArr[i2] == 0 ? " granted" : " denied"));
        }
        FragmentTransaction fragmentTransactionBeginTransaction = getActivity().getFragmentManager().beginTransaction();
        fragmentTransactionBeginTransaction.remove(this);
        fragmentTransactionBeginTransaction.commit();
        this.a.run();
    }
}
