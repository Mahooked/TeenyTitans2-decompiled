package com.google.android.gms.internal;

import android.support.v4.util.ArrayMap;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.GoogleApi;
import com.google.android.gms.tasks.Task;
import com.google.android.gms.tasks.TaskCompletionSource;
import java.util.Iterator;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
public final class zzbav {
    private int zzaBH;
    private final TaskCompletionSource<Void> zzaBG = new TaskCompletionSource<>();
    private boolean zzaBI = false;
    private final ArrayMap<zzbat<?>, ConnectionResult> zzaAB = new ArrayMap<>();

    /* JADX WARN: Type inference fix 'apply assigned field type' failed
    java.lang.UnsupportedOperationException: ArgType.getObject(), call class: class jadx.core.dex.instructions.args.ArgType$UnknownArg
    	at jadx.core.dex.instructions.args.ArgType.getObject(ArgType.java:596)
    	at jadx.core.dex.attributes.nodes.ClassTypeVarsAttr.getTypeVarsMapFor(ClassTypeVarsAttr.java:35)
    	at jadx.core.dex.nodes.utils.TypeUtils.replaceClassGenerics(TypeUtils.java:177)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.insertExplicitUseCast(FixTypesVisitor.java:397)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.tryFieldTypeWithNewCasts(FixTypesVisitor.java:359)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.applyFieldType(FixTypesVisitor.java:309)
    	at jadx.core.dex.visitors.typeinference.FixTypesVisitor.visit(FixTypesVisitor.java:94)
     */
    public zzbav(Iterable<? extends GoogleApi<?>> iterable) {
        Iterator<? extends GoogleApi<?>> it = iterable.iterator();
        while (it.hasNext()) {
            this.zzaAB.put(it.next().zzph(), null);
        }
        this.zzaBH = this.zzaAB.keySet().size();
    }

    public final Task<Void> getTask() {
        return this.zzaBG.getTask();
    }

    public final void zza(zzbat<?> zzbatVar, ConnectionResult connectionResult) {
        this.zzaAB.put(zzbatVar, connectionResult);
        this.zzaBH--;
        if (!connectionResult.isSuccess()) {
            this.zzaBI = true;
        }
        if (this.zzaBH == 0) {
            if (!this.zzaBI) {
                this.zzaBG.setResult(null);
            } else {
                this.zzaBG.setException(new com.google.android.gms.common.api.zza(this.zzaAB));
            }
        }
    }

    public final Set<zzbat<?>> zzpt() {
        return this.zzaAB.keySet();
    }

    public final void zzpu() {
        this.zzaBG.setResult(null);
    }
}
