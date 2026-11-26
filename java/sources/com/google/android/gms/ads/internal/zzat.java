package com.google.android.gms.ads.internal;

import com.google.android.gms.internal.zzafr;
import com.google.android.gms.internal.zzaka;
import com.google.android.gms.internal.zzakf;
import com.google.android.gms.internal.zzns;
import java.util.Iterator;
import java.util.List;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class zzat implements zzakf {
    private /* synthetic */ zzaka zzuA;
    private /* synthetic */ zzns zzuB;
    private /* synthetic */ String zzuz;

    zzat(zzns zznsVar, String str, zzaka zzakaVar) {
        this.zzuB = zznsVar;
        this.zzuz = str;
        this.zzuA = zzakaVar;
    }

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
    @Override // com.google.android.gms.internal.zzakf
    public final void zza(zzaka zzakaVar, boolean z) {
        try {
            JSONObject jSONObject = new JSONObject();
            jSONObject.put("headline", this.zzuB.getHeadline());
            jSONObject.put("body", this.zzuB.getBody());
            jSONObject.put("call_to_action", this.zzuB.getCallToAction());
            jSONObject.put("advertiser", this.zzuB.getAdvertiser());
            jSONObject.put("logo", zzar.zza(this.zzuB.zzem()));
            JSONArray jSONArray = new JSONArray();
            List images = this.zzuB.getImages();
            if (images != null) {
                Iterator it = images.iterator();
                while (it.hasNext()) {
                    jSONArray.put(zzar.zza(zzar.zzd(it.next())));
                }
            }
            jSONObject.put("images", jSONArray);
            jSONObject.put("extras", zzar.zza(this.zzuB.getExtras(), this.zzuz));
            JSONObject jSONObject2 = new JSONObject();
            jSONObject2.put("assets", jSONObject);
            jSONObject2.put("template_id", "1");
            this.zzuA.zza("google.afma.nativeExpressAds.loadAssets", jSONObject2);
        } catch (JSONException e) {
            zzafr.zzc("Exception occurred when loading assets", e);
        }
    }
}
