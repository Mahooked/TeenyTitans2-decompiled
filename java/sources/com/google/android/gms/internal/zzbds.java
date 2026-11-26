package com.google.android.gms.internal;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.MainThread;
import java.io.FileDescriptor;
import java.io.PrintWriter;

/* JADX INFO: loaded from: classes.dex */
public class zzbds {
    protected final zzbdt zzaEG;

    protected zzbds(zzbdt zzbdtVar) {
        this.zzaEG = zzbdtVar;
    }

    protected static zzbdt zzb(zzbdr zzbdrVar) {
        return zzbdrVar.zzqC() ? zzbeo.zza(zzbdrVar.zzqE()) : zzbdu.zzo(zzbdrVar.zzqD());
    }

    public static zzbdt zzn(Activity activity) {
        return zzb(new zzbdr(activity));
    }

    @MainThread
    public void dump(String str, FileDescriptor fileDescriptor, PrintWriter printWriter, String[] strArr) {
    }

    public final Activity getActivity() {
        return this.zzaEG.zzqF();
    }

    @MainThread
    public void onActivityResult(int i, int i2, Intent intent) {
    }

    @MainThread
    public void onCreate(Bundle bundle) {
    }

    @MainThread
    public void onDestroy() {
    }

    @MainThread
    public void onResume() {
    }

    @MainThread
    public void onSaveInstanceState(Bundle bundle) {
    }

    @MainThread
    public void onStart() {
    }

    @MainThread
    public void onStop() {
    }
}
