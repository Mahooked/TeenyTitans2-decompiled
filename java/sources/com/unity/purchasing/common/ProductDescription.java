package com.unity.purchasing.common;

/* JADX INFO: loaded from: classes.dex */
public class ProductDescription {
    public final ProductMetadata metadata;
    public final String receipt;
    public final String storeSpecificId;
    public final String transactionId;

    public ProductDescription(String id, ProductMetadata metadata, String receipt, String transactionId) {
        this.storeSpecificId = id;
        this.metadata = metadata;
        this.receipt = receipt;
        this.transactionId = transactionId;
    }

    public String toString() {
        return "{ProductDescription: storeSpecificId = " + this.storeSpecificId + ", metadata = " + this.metadata + ", receipt = " + this.receipt + ", transactionId = " + this.transactionId + ", }";
    }
}
