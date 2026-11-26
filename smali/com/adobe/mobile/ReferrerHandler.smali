.class final Lcom/adobe/mobile/ReferrerHandler;
.super Ljava/lang/Object;
.source "ReferrerHandler.java"


# static fields
.field static final ACQUISITION_V3_TOKEN:Ljava/lang/String; = "adb_acq_v3"

.field static final ADOBE_DATA_KEY:Ljava/lang/String; = "adobeData"

.field static final CONTEXT_DATA_KEY:Ljava/lang/String; = "contextData"

.field static final DEEP_LINK_KEY:Ljava/lang/String; = "link_deferred"

.field static final REFERRER_FIELDS:[Ljava/lang/String;

.field private static _referrerProcessed:Z


# direct methods
.method static constructor <clinit>()V
    .locals 4

    .prologue
    const/4 v3, 0x1

    .line 41
    sput-boolean v3, Lcom/adobe/mobile/ReferrerHandler;->_referrerProcessed:Z

    .line 51
    const/4 v0, 0x6

    new-array v0, v0, [Ljava/lang/String;

    const/4 v1, 0x0

    const-string v2, "utm_source"

    aput-object v2, v0, v1

    const-string v1, "utm_medium"

    aput-object v1, v0, v3

    const/4 v1, 0x2

    const-string v2, "utm_term"

    aput-object v2, v0, v1

    const/4 v1, 0x3

    const-string v2, "utm_content"

    aput-object v2, v0, v1

    const/4 v1, 0x4

    const-string v2, "utm_campaign"

    aput-object v2, v0, v1

    const/4 v1, 0x5

    const-string v2, "trackingcode"

    aput-object v2, v0, v1

    sput-object v0, Lcom/adobe/mobile/ReferrerHandler;->REFERRER_FIELDS:[Ljava/lang/String;

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .prologue
    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method protected static generateURLForV3(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 9
    .param p0, "ugid"    # Ljava/lang/String;
    .param p1, "adid"    # Ljava/lang/String;

    .prologue
    const/16 v8, 0x40

    const/4 v7, 0x1

    const/4 v6, 0x0

    .line 363
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2, v8}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 364
    .local v2, "urlSb":Ljava/lang/StringBuilder;
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v0

    .line 365
    .local v0, "mobileConfig":Lcom/adobe/mobile/MobileConfig;
    const-string v3, "https://%s/v3/%s/end"

    const/4 v4, 0x2

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v0}, Lcom/adobe/mobile/MobileConfig;->getAcquisitionServer()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-virtual {v0}, Lcom/adobe/mobile/MobileConfig;->getAcquisitionAppId()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v7

    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 367
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1, v8}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 368
    .local v1, "querySb":Ljava/lang/StringBuilder;
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v3

    if-lez v3, :cond_0

    .line 369
    const-string v3, "?a_ugid=%s"

    new-array v4, v7, [Ljava/lang/Object;

    invoke-static {p0}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 371
    :cond_0
    if-eqz p1, :cond_1

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v3

    if-lez v3, :cond_1

    .line 372
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->length()I

    move-result v3

    if-lez v3, :cond_2

    const-string v3, "&"

    :goto_0
    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 373
    const-string v3, "a_cid=%s"

    new-array v4, v7, [Ljava/lang/Object;

    invoke-static {p1}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 376
    :cond_1
    invoke-virtual {v2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/CharSequence;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    return-object v3

    .line 372
    :cond_2
    const-string v3, "?"

    goto :goto_0
.end method

.method protected static getContextDataFromJSON(Lorg/json/JSONObject;)Ljava/util/HashMap;
    .locals 8
    .param p0, "responseObject"    # Lorg/json/JSONObject;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lorg/json/JSONObject;",
            ")",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 286
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 288
    .local v1, "contextDataMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    if-nez p0, :cond_1

    .line 313
    :cond_0
    :goto_0
    return-object v1

    .line 294
    :cond_1
    :try_start_0
    const-string v5, "contextData"

    invoke-virtual {p0, v5}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-result-object v0

    .line 299
    .local v0, "contextData":Lorg/json/JSONObject;
    if-eqz v0, :cond_0

    .line 303
    invoke-virtual {v0}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v4

    .line 305
    .local v4, "keys":Ljava/util/Iterator;
    :goto_1
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_0

    .line 306
    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v3

    .line 308
    .local v3, "key":Ljava/lang/String;
    :try_start_1
    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v1, v3, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_1

    .line 309
    :catch_0
    move-exception v2

    .line 310
    .local v2, "ex":Lorg/json/JSONException;
    const-string v5, "Analytics - Unable to parse acquisition service response (the value for %s is not a string)"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    aput-object v3, v6, v7

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 295
    .end local v0    # "contextData":Lorg/json/JSONObject;
    .end local v2    # "ex":Lorg/json/JSONException;
    .end local v3    # "key":Ljava/lang/String;
    .end local v4    # "keys":Ljava/util/Iterator;
    :catch_1
    move-exception v2

    .line 296
    .restart local v2    # "ex":Lorg/json/JSONException;
    goto :goto_0
.end method

.method protected static getDeepLinkFromJSON(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 4
    .param p0, "responseObject"    # Lorg/json/JSONObject;

    .prologue
    const/4 v1, 0x0

    .line 317
    if-nez p0, :cond_1

    .line 337
    :cond_0
    :goto_0
    return-object v1

    .line 322
    :cond_1
    :try_start_0
    const-string v3, "adobeData"

    invoke-virtual {p0, v3}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v0

    .line 327
    .local v0, "adobeData":Lorg/json/JSONObject;
    if-eqz v0, :cond_0

    .line 332
    :try_start_1
    const-string v3, "link_deferred"

    invoke-virtual {v0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    move-result-object v1

    .local v1, "deepLink":Ljava/lang/String;
    goto :goto_0

    .line 323
    .end local v0    # "adobeData":Lorg/json/JSONObject;
    .end local v1    # "deepLink":Ljava/lang/String;
    :catch_0
    move-exception v2

    .line 324
    .local v2, "ex":Lorg/json/JSONException;
    goto :goto_0

    .line 333
    .end local v2    # "ex":Lorg/json/JSONException;
    .restart local v0    # "adobeData":Lorg/json/JSONObject;
    :catch_1
    move-exception v2

    .line 334
    .restart local v2    # "ex":Lorg/json/JSONException;
    goto :goto_0
.end method

.method protected static getReferrerDataFromV3Server(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    .locals 9
    .param p0, "ugid"    # Ljava/lang/String;
    .param p1, "adid"    # Ljava/lang/String;

    .prologue
    const/4 v7, 0x1

    const/4 v8, 0x0

    const/4 v4, 0x0

    .line 341
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v5

    invoke-virtual {v5}, Lcom/adobe/mobile/MobileConfig;->mobileReferrerConfigured()Z

    move-result v5

    if-nez v5, :cond_0

    move-object v2, v4

    .line 358
    :goto_0
    return-object v2

    .line 344
    :cond_0
    invoke-static {p0, p1}, Lcom/adobe/mobile/ReferrerHandler;->generateURLForV3(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 345
    .local v3, "url":Ljava/lang/String;
    const-string v5, "Analytics - Trying to fetch referrer data from (%s)"

    new-array v6, v7, [Ljava/lang/Object;

    aput-object v3, v6, v8

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 347
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v5

    invoke-virtual {v5}, Lcom/adobe/mobile/MobileConfig;->getReferrerTimeout()I

    move-result v5

    const-string v6, "Analytics"

    invoke-static {v3, v4, v5, v6}, Lcom/adobe/mobile/RequestHandler;->retrieveData(Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)[B

    move-result-object v1

    .line 348
    .local v1, "responseByteArray":[B
    if-nez v1, :cond_1

    move-object v2, v4

    .line 349
    goto :goto_0

    .line 353
    :cond_1
    :try_start_0
    new-instance v2, Ljava/lang/String;

    const-string v5, "UTF-8"

    invoke-direct {v2, v1, v5}, Ljava/lang/String;-><init>([BLjava/lang/String;)V
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    .local v2, "responseString":Ljava/lang/String;
    goto :goto_0

    .line 354
    .end local v2    # "responseString":Ljava/lang/String;
    :catch_0
    move-exception v0

    .line 355
    .local v0, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v5, "Analytics - Unable to decode response(%s)"

    new-array v6, v7, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v8

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v2, v4

    .line 356
    goto :goto_0
.end method

.method protected static getReferrerProcessed()Z
    .locals 1

    .prologue
    .line 48
    sget-boolean v0, Lcom/adobe/mobile/ReferrerHandler;->_referrerProcessed:Z

    return v0
.end method

.method protected static getReferrerURLFromIntent(Landroid/content/Intent;)Ljava/lang/String;
    .locals 7
    .param p0, "intent"    # Landroid/content/Intent;

    .prologue
    const/4 v6, 0x0

    const/4 v3, 0x0

    .line 82
    if-nez p0, :cond_1

    .line 83
    const-string v4, "Analytics - Unable to process referrer due to an invalid intent parameter"

    new-array v5, v6, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 109
    :cond_0
    :goto_0
    return-object v3

    .line 87
    :cond_1
    invoke-virtual {p0}, Landroid/content/Intent;->getAction()Ljava/lang/String;

    move-result-object v4

    const-string v5, "com.android.vending.INSTALL_REFERRER"

    invoke-virtual {v4, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_2

    .line 88
    const-string v4, "Analytics - Ignoring referrer due to the intent\'s action not being handled by analytics"

    new-array v5, v6, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 94
    :cond_2
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v1

    .line 96
    .local v1, "extras":Landroid/os/Bundle;
    if-eqz v1, :cond_3

    .line 97
    const/4 v4, 0x0

    invoke-virtual {v1, v4}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 105
    :cond_3
    const-string v4, "referrer"

    invoke-virtual {p0, v4}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 107
    .local v2, "referrerURL":Ljava/lang/String;
    if-eqz v2, :cond_0

    :try_start_1
    const-string v3, "UTF-8"

    invoke-static {v2, v3}, Ljava/net/URLDecoder;->decode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_1

    move-result-object v3

    goto :goto_0

    .line 100
    .end local v1    # "extras":Landroid/os/Bundle;
    .end local v2    # "referrerURL":Ljava/lang/String;
    :catch_0
    move-exception v0

    .line 101
    .local v0, "e":Ljava/lang/Exception;
    goto :goto_0

    .line 108
    .end local v0    # "e":Ljava/lang/Exception;
    .restart local v1    # "extras":Landroid/os/Bundle;
    .restart local v2    # "referrerURL":Ljava/lang/String;
    :catch_1
    move-exception v0

    .restart local v0    # "e":Ljava/lang/Exception;
    move-object v3, v2

    .line 109
    goto :goto_0
.end method

.method protected static handleV1Acquisition(Ljava/util/HashMap;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .prologue
    .local p0, "referrerFields":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v9, 0x1

    const/4 v5, 0x0

    .line 128
    sget-boolean v4, Lcom/adobe/mobile/Lifecycle;->lifecycleHasRun:Z

    if-eqz v4, :cond_0

    .line 129
    const-string v4, "utm_source"

    invoke-virtual {p0, v4}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_0

    const-string v4, "utm_campaign"

    .line 130
    invoke-virtual {p0, v4}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v4

    if-eqz v4, :cond_0

    .line 132
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    .line 133
    .local v3, "referrerLifecycleContextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v4, "a.referrer.campaign.source"

    const-string v6, "utm_source"

    invoke-virtual {p0, v6}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v3, v4, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 134
    const-string v4, "a.referrer.campaign.medium"

    const-string v6, "utm_medium"

    invoke-virtual {p0, v6}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v3, v4, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 135
    const-string v4, "a.referrer.campaign.term"

    const-string v6, "utm_term"

    invoke-virtual {p0, v6}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v3, v4, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 136
    const-string v4, "a.referrer.campaign.content"

    const-string v6, "utm_content"

    invoke-virtual {p0, v6}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v3, v4, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 137
    const-string v4, "a.referrer.campaign.name"

    const-string v6, "utm_campaign"

    invoke-virtual {p0, v6}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v3, v4, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 138
    const-string v4, "a.referrer.campaign.trackingcode"

    const-string v6, "trackingcode"

    invoke-virtual {p0, v6}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v3, v4, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 141
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v4

    new-instance v6, Lcom/adobe/mobile/ReferrerHandler$1;

    invoke-direct {v6, v3}, Lcom/adobe/mobile/ReferrerHandler$1;-><init>(Ljava/util/HashMap;)V

    invoke-interface {v4, v6}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 147
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v4

    sget-object v6, Lcom/adobe/mobile/Config$MobileDataEvent;->MOBILE_EVENT_ACQUISITION_INSTALL:Lcom/adobe/mobile/Config$MobileDataEvent;

    invoke-virtual {v4, v6, v3}, Lcom/adobe/mobile/MobileConfig;->invokeAdobeDataCallback(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V

    .line 151
    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->sharedInstance()Lcom/adobe/mobile/AnalyticsWorker;

    move-result-object v4

    invoke-virtual {v4, v3}, Lcom/adobe/mobile/AnalyticsWorker;->kickWithReferrerData(Ljava/util/Map;)V

    .line 157
    .end local v3    # "referrerLifecycleContextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_0
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 159
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    sget-object v6, Lcom/adobe/mobile/ReferrerHandler;->REFERRER_FIELDS:[Ljava/lang/String;

    array-length v7, v6

    move v4, v5

    :goto_0
    if-ge v4, v7, :cond_2

    aget-object v2, v6, v4

    .line 160
    .local v2, "field":Ljava/lang/String;
    invoke-virtual {p0, v2}, Ljava/util/HashMap;->containsKey(Ljava/lang/Object;)Z

    move-result v8

    if-eqz v8, :cond_1

    invoke-virtual {p0, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v8

    if-eqz v8, :cond_1

    .line 161
    invoke-virtual {p0, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-interface {v1, v2, v8}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 159
    :cond_1
    add-int/lit8 v4, v4, 0x1

    goto :goto_0

    .line 164
    .end local v2    # "field":Ljava/lang/String;
    :cond_2
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    .line 170
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_1
    sput-boolean v9, Lcom/adobe/mobile/ReferrerHandler;->_referrerProcessed:Z

    .line 171
    return-void

    .line 165
    :catch_0
    move-exception v0

    .line 166
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Analytics - Error persisting referrer data (%s)"

    new-array v6, v9, [Ljava/lang/Object;

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v5

    invoke-static {v4, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method protected static handleV3Acquisition(Ljava/util/HashMap;)V
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .prologue
    .local p0, "referrerFields":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v9, 0x1

    const/4 v8, 0x0

    .line 175
    const-string v5, "utm_content"

    invoke-virtual {p0, v5}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 176
    .local v4, "ugid":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAdvertisingIdentifier()Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Lcom/adobe/mobile/ReferrerHandler;->getReferrerDataFromV3Server(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 179
    .local v3, "jsonResponse":Ljava/lang/String;
    sget-boolean v5, Lcom/adobe/mobile/Lifecycle;->lifecycleHasRun:Z

    if-eqz v5, :cond_1

    .line 181
    sget-boolean v5, Lcom/adobe/mobile/ReferrerHandler;->_referrerProcessed:Z

    if-eqz v5, :cond_0

    .line 182
    const-string v5, "Analytics - Acquisition referrer timed out"

    new-array v6, v8, [Ljava/lang/Object;

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 212
    :goto_0
    return-void

    .line 185
    :cond_0
    invoke-static {v3}, Lcom/adobe/mobile/ReferrerHandler;->processV3ResponseAndReturnContextData(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v0

    .line 187
    .local v0, "contextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v5

    sget-object v6, Lcom/adobe/mobile/Config$MobileDataEvent;->MOBILE_EVENT_ACQUISITION_INSTALL:Lcom/adobe/mobile/Config$MobileDataEvent;

    invoke-virtual {v5, v6, v0}, Lcom/adobe/mobile/MobileConfig;->invokeAdobeDataCallback(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V

    .line 190
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v5

    new-instance v6, Lcom/adobe/mobile/ReferrerHandler$2;

    invoke-direct {v6, v0}, Lcom/adobe/mobile/ReferrerHandler$2;-><init>(Ljava/util/HashMap;)V

    invoke-interface {v5, v6}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 198
    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->sharedInstance()Lcom/adobe/mobile/AnalyticsWorker;

    move-result-object v5

    invoke-virtual {v5, v0}, Lcom/adobe/mobile/AnalyticsWorker;->kickWithReferrerData(Ljava/util/Map;)V

    .line 202
    .end local v0    # "contextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_1
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v2

    .line 204
    .local v2, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v5, "ADMS_Referrer_ContextData_Json_String"

    invoke-interface {v2, v5, v3}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 205
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    .line 211
    .end local v2    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_1
    sput-boolean v9, Lcom/adobe/mobile/ReferrerHandler;->_referrerProcessed:Z

    goto :goto_0

    .line 206
    :catch_0
    move-exception v1

    .line 207
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v5, "Analytics - Error persisting referrer data (%s)"

    new-array v6, v9, [Ljava/lang/Object;

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v8

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method protected static isV3Response(Ljava/util/HashMap;)Z
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)Z"
        }
    .end annotation

    .prologue
    .line 215
    .local p0, "referrerFields":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v0, "adb_acq_v3"

    const-string v1, "utm_source"

    invoke-virtual {p0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "adb_acq_v3"

    const-string v1, "utm_campaign"

    .line 216
    invoke-virtual {p0, v1}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected static parseReferrerURLToMap(Ljava/lang/String;)Ljava/util/HashMap;
    .locals 9
    .param p0, "referrerURL"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    const/4 v4, 0x0

    .line 114
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 116
    .local v1, "referrerFields":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v3, "&"

    invoke-virtual {p0, v3}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v5

    array-length v6, v5

    move v3, v4

    :goto_0
    if-ge v3, v6, :cond_1

    aget-object v0, v5, v3

    .line 117
    .local v0, "item":Ljava/lang/String;
    const-string v7, "="

    invoke-virtual {v0, v7}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v2

    .line 119
    .local v2, "tokens":[Ljava/lang/String;
    array-length v7, v2

    const/4 v8, 0x2

    if-ne v7, v8, :cond_0

    .line 120
    aget-object v7, v2, v4

    const/4 v8, 0x1

    aget-object v8, v2, v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 116
    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 123
    .end local v0    # "item":Ljava/lang/String;
    .end local v2    # "tokens":[Ljava/lang/String;
    :cond_1
    return-object v1
.end method

.method protected static parseV3ContextDataFromResponse(Ljava/lang/String;)Ljava/util/HashMap;
    .locals 2
    .param p0, "response"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 244
    invoke-static {p0}, Lcom/adobe/mobile/ReferrerHandler;->translateV3StringResponseToJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    .line 245
    .local v0, "responseJSON":Lorg/json/JSONObject;
    invoke-static {v0}, Lcom/adobe/mobile/ReferrerHandler;->getContextDataFromJSON(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object v1

    return-object v1
.end method

.method public static processIntent(Landroid/content/Intent;)V
    .locals 5
    .param p0, "intent"    # Landroid/content/Intent;

    .prologue
    const/4 v4, 0x0

    .line 63
    invoke-static {p0}, Lcom/adobe/mobile/ReferrerHandler;->getReferrerURLFromIntent(Landroid/content/Intent;)Ljava/lang/String;

    move-result-object v1

    .line 65
    .local v1, "referrerURL":Ljava/lang/String;
    if-eqz v1, :cond_0

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v2

    if-nez v2, :cond_1

    .line 66
    :cond_0
    const-string v2, "Analytics - Ignoring referrer due to the intent\'s referrer string being empty"

    new-array v3, v4, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 79
    :goto_0
    return-void

    .line 70
    :cond_1
    const-string v2, "Analytics - Received referrer information(%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    aput-object v1, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 72
    invoke-static {v1}, Lcom/adobe/mobile/ReferrerHandler;->parseReferrerURLToMap(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v0

    .line 74
    .local v0, "referrerFields":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {v0}, Lcom/adobe/mobile/ReferrerHandler;->isV3Response(Ljava/util/HashMap;)Z

    move-result v2

    if-eqz v2, :cond_2

    .line 75
    invoke-static {v0}, Lcom/adobe/mobile/ReferrerHandler;->handleV3Acquisition(Ljava/util/HashMap;)V

    goto :goto_0

    .line 77
    :cond_2
    invoke-static {v0}, Lcom/adobe/mobile/ReferrerHandler;->handleV1Acquisition(Ljava/util/HashMap;)V

    goto :goto_0
.end method

.method protected static processV3ResponseAndReturnContextData(Ljava/lang/String;)Ljava/util/HashMap;
    .locals 10
    .param p0, "response"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    const/4 v9, 0x0

    .line 221
    invoke-static {p0}, Lcom/adobe/mobile/ReferrerHandler;->translateV3StringResponseToJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v5

    .line 222
    .local v5, "responseJSON":Lorg/json/JSONObject;
    invoke-static {p0}, Lcom/adobe/mobile/ReferrerHandler;->parseV3ContextDataFromResponse(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v0

    .line 224
    .local v0, "contextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {v5}, Lcom/adobe/mobile/ReferrerHandler;->getDeepLinkFromJSON(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v2

    .line 225
    .local v2, "deepLink":Ljava/lang/String;
    if-nez v2, :cond_0

    .line 240
    :goto_0
    return-object v0

    .line 230
    :cond_0
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v1

    .line 231
    .local v1, "currentActivity":Landroid/app/Activity;
    new-instance v4, Landroid/content/Intent;

    const-string v6, "android.intent.action.VIEW"

    invoke-direct {v4, v6}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 232
    .local v4, "intent":Landroid/content/Intent;
    invoke-static {v2}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v6

    invoke-virtual {v4, v6}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 233
    invoke-virtual {v1, v4}, Landroid/app/Activity;->startActivity(Landroid/content/Intent;)V
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullActivityException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1

    goto :goto_0

    .line 234
    .end local v1    # "currentActivity":Landroid/app/Activity;
    .end local v4    # "intent":Landroid/content/Intent;
    :catch_0
    move-exception v3

    .line 235
    .local v3, "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    invoke-virtual {v3}, Lcom/adobe/mobile/StaticMethods$NullActivityException;->getMessage()Ljava/lang/String;

    move-result-object v6

    new-array v7, v9, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 236
    .end local v3    # "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    :catch_1
    move-exception v3

    .line 237
    .local v3, "ex":Ljava/lang/Exception;
    const-string v6, "Acquisition - Could not load deep link intent for Acquisition (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    invoke-virtual {v3}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v7, v9

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static setReferrerProcessed(Z)V
    .locals 0
    .param p0, "processed"    # Z

    .prologue
    .line 44
    sput-boolean p0, Lcom/adobe/mobile/ReferrerHandler;->_referrerProcessed:Z

    .line 45
    return-void
.end method

.method protected static translateV3StringResponseToJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    .locals 8
    .param p0, "response"    # Ljava/lang/String;

    .prologue
    const/4 v5, 0x1

    const/4 v3, 0x0

    const/4 v7, 0x0

    .line 249
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v4

    if-nez v4, :cond_1

    :cond_0
    move-object v2, v3

    .line 282
    :goto_0
    return-object v2

    .line 256
    :cond_1
    :try_start_0
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, p0}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 264
    .local v2, "responseObject":Lorg/json/JSONObject;
    :try_start_1
    const-string v4, "contextData"

    invoke-virtual {v2, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    move-result-object v0

    .line 270
    .local v0, "contextData":Lorg/json/JSONObject;
    if-nez v0, :cond_2

    .line 271
    const-string v4, "Analytics - Unable to parse acquisition service response (no contextData parameter in response)"

    new-array v5, v7, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v2, v3

    .line 272
    goto :goto_0

    .line 257
    .end local v0    # "contextData":Lorg/json/JSONObject;
    .end local v2    # "responseObject":Lorg/json/JSONObject;
    :catch_0
    move-exception v1

    .line 258
    .local v1, "ex":Lorg/json/JSONException;
    const-string v4, "Analytics - Unable to parse response(%s)"

    new-array v5, v5, [Ljava/lang/Object;

    invoke-virtual {v1}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v2, v3

    .line 259
    goto :goto_0

    .line 265
    .end local v1    # "ex":Lorg/json/JSONException;
    .restart local v2    # "responseObject":Lorg/json/JSONObject;
    :catch_1
    move-exception v1

    .line 266
    .restart local v1    # "ex":Lorg/json/JSONException;
    const-string v4, "Analytics - Unable to parse acquisition service response (no contextData parameter in response)"

    new-array v5, v7, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v2, v3

    .line 267
    goto :goto_0

    .line 276
    .end local v1    # "ex":Lorg/json/JSONException;
    .restart local v0    # "contextData":Lorg/json/JSONObject;
    :cond_2
    const-string v4, "a.referrer.campaign.name"

    invoke-virtual {v0, v4}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_3

    .line 277
    const-string v4, "Analytics - Acquisition referrer data was not complete (no a.referrer.campaign.name in context data), ignoring"

    new-array v5, v7, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v2, v3

    .line 278
    goto :goto_0

    .line 281
    :cond_3
    const-string v3, "Analytics - Received Referrer Data(%s)"

    new-array v4, v5, [Ljava/lang/Object;

    aput-object p0, v4, v7

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method
