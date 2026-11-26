package com.unity.purchasing.common;

import java.math.BigDecimal;

/* JADX INFO: loaded from: classes.dex */
public class ProductMetadata {
    public final String isoCurrencyCode;
    public final String localizedDescription;
    public final BigDecimal localizedPrice;
    public final String localizedPriceString;
    public final String localizedTitle;

    public ProductMetadata(String priceString, String title, String description, String currencyCode, BigDecimal localizedPrice) {
        this.localizedPriceString = priceString == null ? "" : priceString;
        this.localizedTitle = title;
        this.localizedDescription = description;
        this.isoCurrencyCode = currencyCode;
        this.localizedPrice = localizedPrice;
    }

    public String toString() {
        return "{ProductMetadata: localizedPriceString = " + this.localizedPriceString + ", localizedTitle = " + this.localizedTitle + ", localizedDescription = " + this.localizedDescription + ", isoCurrencyCode = " + this.isoCurrencyCode + ", localizedPrice = " + this.localizedPrice + ", }";
    }
}
