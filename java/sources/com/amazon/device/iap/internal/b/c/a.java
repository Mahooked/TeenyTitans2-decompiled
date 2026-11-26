package com.amazon.device.iap.internal.b.c;

import android.os.RemoteException;
import com.amazon.android.framework.exception.KiwiException;
import com.amazon.device.iap.internal.b.e;
import com.amazon.device.iap.internal.model.ProductBuilder;
import com.amazon.device.iap.internal.model.ProductDataResponseBuilder;
import com.amazon.device.iap.internal.util.MetricsHelper;
import com.amazon.device.iap.model.Product;
import com.amazon.device.iap.model.ProductDataResponse;
import com.amazon.device.iap.model.ProductType;
import com.amazon.venezia.command.SuccessResult;
import java.math.BigDecimal;
import java.util.Currency;
import java.util.HashMap;
import java.util.LinkedHashSet;
import java.util.Map;
import java.util.Set;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: compiled from: GetItemDataCommandV2.java */
/* JADX INFO: loaded from: classes.dex */
public final class a extends c {
    private static final String b = a.class.getSimpleName();

    public a(e eVar, Set<String> set) {
        super(eVar, "2.0", set);
    }

    @Override // com.amazon.device.iap.internal.b.i
    protected boolean a(SuccessResult successResult) throws RemoteException, KiwiException {
        Map data = successResult.getData();
        com.amazon.device.iap.internal.util.e.a(b, "data: " + data);
        LinkedHashSet linkedHashSet = new LinkedHashSet();
        HashMap map = new HashMap();
        for (String str : this.a) {
            if (!data.containsKey(str)) {
                linkedHashSet.add(str);
            } else {
                try {
                    map.put(str, a(str, data));
                } catch (IllegalArgumentException e) {
                    linkedHashSet.add(str);
                    MetricsHelper.submitJsonParsingExceptionMetrics(c(), (String) data.get(str), b + ".onResult()");
                    com.amazon.device.iap.internal.util.e.b(b, "Error parsing JSON for SKU " + str + ": " + e.getMessage());
                }
            }
        }
        e eVarB = b();
        eVarB.d().a(new ProductDataResponseBuilder().setRequestId(eVarB.c()).setRequestStatus(ProductDataResponse.RequestStatus.SUCCESSFUL).setUnavailableSkus(linkedHashSet).setProductData(map).build());
        return true;
    }

    private Product a(String str, Map map) throws IllegalArgumentException {
        JSONObject jSONObjectOptJSONObject;
        String str2 = (String) map.get(str);
        try {
            JSONObject jSONObject = new JSONObject(str2);
            ProductType productTypeValueOf = ProductType.valueOf(jSONObject.getString("itemType").toUpperCase());
            String string = jSONObject.getString("description");
            String strOptString = jSONObject.optString("price", null);
            if (com.amazon.device.iap.internal.util.d.a(strOptString) && (jSONObjectOptJSONObject = jSONObject.optJSONObject("priceJson")) != null) {
                strOptString = Currency.getInstance(jSONObjectOptJSONObject.getString("currency")).getSymbol() + new BigDecimal(jSONObjectOptJSONObject.getString("value"));
            }
            return new ProductBuilder().setSku(str).setProductType(productTypeValueOf).setDescription(string).setPrice(strOptString).setSmallIconUrl(jSONObject.getString("iconUrl")).setTitle(jSONObject.getString("title")).build();
        } catch (JSONException e) {
            throw new IllegalArgumentException("error in parsing json string" + str2);
        }
    }
}
