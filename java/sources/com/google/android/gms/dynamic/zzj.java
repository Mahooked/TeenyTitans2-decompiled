package com.google.android.gms.dynamic;

import android.annotation.SuppressLint;
import android.app.Fragment;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;

/* JADX INFO: loaded from: classes.dex */
@SuppressLint({"NewApi"})
public final class zzj extends zzl {
    private Fragment zzaSB;

    private zzj(Fragment fragment) {
        this.zzaSB = fragment;
    }

    public static zzj zza(Fragment fragment) {
        if (fragment != null) {
            return new zzj(fragment);
        }
        return null;
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final Bundle getArguments() {
        return this.zzaSB.getArguments();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final int getId() {
        return this.zzaSB.getId();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean getRetainInstance() {
        return this.zzaSB.getRetainInstance();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final String getTag() {
        return this.zzaSB.getTag();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final int getTargetRequestCode() {
        return this.zzaSB.getTargetRequestCode();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean getUserVisibleHint() {
        return this.zzaSB.getUserVisibleHint();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final IObjectWrapper getView() {
        return zzn.zzw(this.zzaSB.getView());
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean isAdded() {
        return this.zzaSB.isAdded();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean isDetached() {
        return this.zzaSB.isDetached();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean isHidden() {
        return this.zzaSB.isHidden();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean isInLayout() {
        return this.zzaSB.isInLayout();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean isRemoving() {
        return this.zzaSB.isRemoving();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean isResumed() {
        return this.zzaSB.isResumed();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final boolean isVisible() {
        return this.zzaSB.isVisible();
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void setHasOptionsMenu(boolean z) {
        this.zzaSB.setHasOptionsMenu(z);
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void setMenuVisibility(boolean z) {
        this.zzaSB.setMenuVisibility(z);
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void setRetainInstance(boolean z) {
        this.zzaSB.setRetainInstance(z);
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void setUserVisibleHint(boolean z) {
        this.zzaSB.setUserVisibleHint(z);
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void startActivity(Intent intent) {
        this.zzaSB.startActivity(intent);
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void startActivityForResult(Intent intent, int i) {
        this.zzaSB.startActivityForResult(intent, i);
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void zzC(IObjectWrapper iObjectWrapper) {
        this.zzaSB.registerForContextMenu((View) zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final void zzD(IObjectWrapper iObjectWrapper) {
        this.zzaSB.unregisterForContextMenu((View) zzn.zzE(iObjectWrapper));
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final IObjectWrapper zztA() {
        return zzn.zzw(this.zzaSB.getResources());
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final zzk zztB() {
        return zza(this.zzaSB.getTargetFragment());
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final IObjectWrapper zzty() {
        return zzn.zzw(this.zzaSB.getActivity());
    }

    @Override // com.google.android.gms.dynamic.zzk
    public final zzk zztz() {
        return zza(this.zzaSB.getParentFragment());
    }
}
