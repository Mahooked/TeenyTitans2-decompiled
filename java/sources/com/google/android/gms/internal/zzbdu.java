package com.google.android.gms.internal;

import android.app.Activity;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import android.support.v4.util.ArrayMap;
import java.io.FileDescriptor;
import java.io.PrintWriter;
import java.lang.ref.WeakReference;
import java.util.Iterator;
import java.util.Map;
import java.util.WeakHashMap;

/* JADX INFO: loaded from: classes.dex */
public final class zzbdu extends Fragment implements zzbdt {
    private static WeakHashMap<Activity, WeakReference<zzbdu>> zzaEH = new WeakHashMap<>();
    private Bundle zzaEJ;
    private Map<String, zzbds> zzaEI = new ArrayMap();
    private int zzLe = 0;

    public static zzbdu zzo(Activity activity) {
        zzbdu zzbduVar;
        WeakReference<zzbdu> weakReference = zzaEH.get(activity);
        if (weakReference == null || (zzbduVar = weakReference.get()) == null) {
            try {
                zzbduVar = (zzbdu) activity.getFragmentManager().findFragmentByTag("LifecycleFragmentImpl");
                if (zzbduVar == null || zzbduVar.isRemoving()) {
                    zzbduVar = new zzbdu();
                    activity.getFragmentManager().beginTransaction().add(zzbduVar, "LifecycleFragmentImpl").commitAllowingStateLoss();
                }
                zzaEH.put(activity, new WeakReference<>(zzbduVar));
            } catch (ClassCastException e) {
                throw new IllegalStateException("Fragment with tag LifecycleFragmentImpl is not a LifecycleFragmentImpl", e);
            }
        }
        return zzbduVar;
    }

    @Override // android.app.Fragment
    public final void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
        super.dump(str, fileDescriptor, printWriter, strArr);
        Iterator<zzbds> it = this.zzaEI.values().iterator();
        while (it.hasNext()) {
            it.next().dump(str, fileDescriptor, printWriter, strArr);
        }
    }

    @Override // android.app.Fragment
    public void onActivityResult(int i, int i2, Intent intent) {
        super.onActivityResult(i, i2, intent);
        Iterator<zzbds> it = this.zzaEI.values().iterator();
        while (it.hasNext()) {
            it.next().onActivityResult(i, i2, intent);
        }
    }

    @Override // android.app.Fragment
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.zzLe = 1;
        this.zzaEJ = bundle;
        for (Map.Entry<String, zzbds> entry : this.zzaEI.entrySet()) {
            entry.getValue().onCreate(bundle != null ? bundle.getBundle(entry.getKey()) : null);
        }
    }

    @Override // android.app.Fragment
    public void onDestroy() {
        super.onDestroy();
        this.zzLe = 5;
        Iterator<zzbds> it = this.zzaEI.values().iterator();
        while (it.hasNext()) {
            it.next().onDestroy();
        }
    }

    @Override // android.app.Fragment
    public void onResume() {
        super.onResume();
        this.zzLe = 3;
        Iterator<zzbds> it = this.zzaEI.values().iterator();
        while (it.hasNext()) {
            it.next().onResume();
        }
    }

    @Override // android.app.Fragment
    public final void onSaveInstanceState(Bundle bundle) {
        super.onSaveInstanceState(bundle);
        if (bundle == null) {
            return;
        }
        for (Map.Entry<String, zzbds> entry : this.zzaEI.entrySet()) {
            Bundle bundle2 = new Bundle();
            entry.getValue().onSaveInstanceState(bundle2);
            bundle.putBundle(entry.getKey(), bundle2);
        }
    }

    @Override // android.app.Fragment
    public void onStart() {
        super.onStart();
        this.zzLe = 2;
        Iterator<zzbds> it = this.zzaEI.values().iterator();
        while (it.hasNext()) {
            it.next().onStart();
        }
    }

    @Override // android.app.Fragment
    public void onStop() {
        super.onStop();
        this.zzLe = 4;
        Iterator<zzbds> it = this.zzaEI.values().iterator();
        while (it.hasNext()) {
            it.next().onStop();
        }
    }

    @Override // com.google.android.gms.internal.zzbdt
    public final <T extends zzbds> T zza(String str, Class<T> cls) {
        return cls.cast(this.zzaEI.get(str));
    }

    @Override // com.google.android.gms.internal.zzbdt
    public final void zza(String str, @NonNull zzbds zzbdsVar) {
        if (this.zzaEI.containsKey(str)) {
            throw new IllegalArgumentException(new StringBuilder(String.valueOf(str).length() + 59).append("LifecycleCallback with tag ").append(str).append(" already added to this fragment.").toString());
        }
        this.zzaEI.put(str, zzbdsVar);
        if (this.zzLe > 0) {
            new Handler(Looper.getMainLooper()).post(new zzbdv(this, zzbdsVar, str));
        }
    }

    @Override // com.google.android.gms.internal.zzbdt
    public final Activity zzqF() {
        return getActivity();
    }
}
