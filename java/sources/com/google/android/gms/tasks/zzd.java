package com.google.android.gms.tasks;

/* JADX INFO: loaded from: classes.dex */
final class zzd implements Runnable {
    private /* synthetic */ Task zzbLT;
    private /* synthetic */ zzc zzbLV;

    zzd(zzc zzcVar, Task task) {
        this.zzbLV = zzcVar;
        this.zzbLT = task;
    }

    @Override // java.lang.Runnable
    public final void run() {
        try {
            Task task = (Task) this.zzbLV.zzbLR.then(this.zzbLT);
            if (task == null) {
                this.zzbLV.onFailure(new NullPointerException("Continuation returned null"));
            } else {
                task.addOnSuccessListener(TaskExecutors.zzbMf, this.zzbLV);
                task.addOnFailureListener(TaskExecutors.zzbMf, this.zzbLV);
            }
        } catch (RuntimeExecutionException e) {
            if (e.getCause() instanceof Exception) {
                this.zzbLV.zzbLS.setException((Exception) e.getCause());
            } else {
                this.zzbLV.zzbLS.setException(e);
            }
        } catch (Exception e2) {
            this.zzbLV.zzbLS.setException(e2);
        }
    }
}
