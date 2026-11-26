.class final Lcom/adobe/mobile/TargetWorker;
.super Ljava/lang/Object;
.source "TargetWorker.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/TargetWorker$1;,
        Lcom/adobe/mobile/TargetWorker$TargetRunner;
    }
.end annotation


# static fields
.field protected static final COOKIE_EXPIRES_KEY_SUFFIX:Ljava/lang/String; = "_Expires"

.field protected static final COOKIE_NAME_PCID:Ljava/lang/String; = "mboxPC"

.field protected static final COOKIE_NAME_SESSION:Ljava/lang/String; = "mboxSession"

.field protected static final COOKIE_VALUE_KEY_SUFFIX:Ljava/lang/String; = "_Value"

.field protected static final PROFILE_PARAMETER_PREFIX:Ljava/lang/String; = "profile."

.field protected static final TARGET_API_A4T_ACTION_NAME:Ljava/lang/String; = "AnalyticsForTarget"

.field protected static final TARGET_API_CONTENT_TYPE:Ljava/lang/String; = "application/json"

.field protected static final TARGET_API_JSON_A4T_LOGGING_ENABLED:Ljava/lang/String; = "a4t"

.field protected static final TARGET_API_JSON_A4T_LOGGING_PAYLOAD:Ljava/lang/String; = "clientSideAnalyticsLoggingPayload"

.field protected static final TARGET_API_JSON_A4T_LOGGING_PAYLOAD_PARAMETERS:Ljava/lang/String; = "parameters"

.field protected static final TARGET_API_JSON_CONTENT:Ljava/lang/String; = "content"

.field protected static final TARGET_API_JSON_EDGE_HOST:Ljava/lang/String; = "edgeHost"

.field protected static final TARGET_API_JSON_ERROR_MESSAGE:Ljava/lang/String; = "message"

.field protected static final TARGET_API_JSON_LOGGING_ENABLED:Ljava/lang/String; = "clientSideAnalyticsLogging"

.field protected static final TARGET_API_JSON_MBOX:Ljava/lang/String; = "mbox"

.field protected static final TARGET_API_JSON_MBOX_PARAMETERS:Ljava/lang/String; = "mboxParameters"

.field protected static final TARGET_API_JSON_MC_VISITOR_ID:Ljava/lang/String; = "marketingCloudVisitorId"

.field protected static final TARGET_API_JSON_ORDER:Ljava/lang/String; = "order"

.field protected static final TARGET_API_JSON_ORDER_ID:Ljava/lang/String; = "id"

.field protected static final TARGET_API_JSON_ORDER_PRODUCTS:Ljava/lang/String; = "purchasedProductIds"

.field protected static final TARGET_API_JSON_ORDER_TOTAL:Ljava/lang/String; = "total"

.field protected static final TARGET_API_JSON_PROFILE_PARAMETERS:Ljava/lang/String; = "profileParameters"

.field protected static final TARGET_API_JSON_THIRD_PARTY_ID:Ljava/lang/String; = "thirdPartyId"

.field protected static final TARGET_API_JSON_TNT_ID:Ljava/lang/String; = "tntId"

.field protected static final TARGET_API_URL_BASE:Ljava/lang/String; = "https://%s/rest/v1/mbox/%s?client=%s"

.field protected static final TARGET_API_URL_HOST_BASE:Ljava/lang/String; = "%s.tt.omtrdc.net"

.field private static final TARGET_SESSION_TIMEOUT_LENGTH:J = 0x708L

.field private static final _checkOldCookiesMutex:Ljava/lang/Object;

.field private static _edgeHost:Ljava/lang/String;

.field private static final _edgeHostMutex:Ljava/lang/Object;

.field private static _oldCookiesHaveBeenChecked:Z

.field private static _persistentParameters:Ljava/util/HashMap;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation
.end field

.field private static final _persistentParametersMutex:Ljava/lang/Object;

.field private static _sessionId:Ljava/lang/String;

.field private static final _sessionIdMutex:Ljava/lang/Object;

.field private static _thirdPartyId:Ljava/lang/String;

.field private static final _thirdPartyIdMutex:Ljava/lang/Object;

.field private static _tntId:Ljava/lang/String;

.field private static final _tntIdMutex:Ljava/lang/Object;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .prologue
    const/4 v0, 0x0

    .line 100
    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    .line 101
    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    .line 102
    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    .line 103
    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;

    .line 104
    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    .line 109
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_sessionIdMutex:Ljava/lang/Object;

    .line 110
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_tntIdMutex:Ljava/lang/Object;

    .line 111
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_thirdPartyIdMutex:Ljava/lang/Object;

    .line 112
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_edgeHostMutex:Ljava/lang/Object;

    .line 113
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_persistentParametersMutex:Ljava/lang/Object;

    .line 565
    const/4 v0, 0x0

    sput-boolean v0, Lcom/adobe/mobile/TargetWorker;->_oldCookiesHaveBeenChecked:Z

    .line 566
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_checkOldCookiesMutex:Ljava/lang/Object;

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .prologue
    .line 41
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 349
    return-void
.end method

.method static synthetic access$100()Ljava/lang/String;
    .locals 1

    .prologue
    .line 41
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->getRequestUrl()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$200(Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)Lorg/json/JSONObject;
    .locals 1
    .param p0, "x0"    # Ljava/lang/String;
    .param p1, "x1"    # Ljava/util/Map;
    .param p2, "x2"    # Ljava/util/Map;
    .param p3, "x3"    # Ljava/util/Map;

    .prologue
    .line 41
    invoke-static {p0, p1, p2, p3}, Lcom/adobe/mobile/TargetWorker;->getJsonObjectForParameters(Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$300(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lorg/json/JSONObject;

    .prologue
    .line 41
    invoke-static {p0}, Lcom/adobe/mobile/TargetWorker;->getErrorMessage(Lorg/json/JSONObject;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$400(Lorg/json/JSONObject;)V
    .locals 0
    .param p0, "x0"    # Lorg/json/JSONObject;

    .prologue
    .line 41
    invoke-static {p0}, Lcom/adobe/mobile/TargetWorker;->saveTargetResponseVariables(Lorg/json/JSONObject;)V

    return-void
.end method

.method static synthetic access$500(Lorg/json/JSONObject;)V
    .locals 0
    .param p0, "x0"    # Lorg/json/JSONObject;

    .prologue
    .line 41
    invoke-static {p0}, Lcom/adobe/mobile/TargetWorker;->forwardAnalyticsForTargetPayload(Lorg/json/JSONObject;)V

    return-void
.end method

.method protected static addPersistentParameter(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2
    .param p0, "key"    # Ljava/lang/String;
    .param p1, "value"    # Ljava/lang/String;

    .prologue
    .line 321
    if-eqz p0, :cond_0

    if-nez p1, :cond_1

    .line 332
    :cond_0
    :goto_0
    return-void

    .line 325
    :cond_1
    sget-object v1, Lcom/adobe/mobile/TargetWorker;->_persistentParametersMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 326
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    if-nez v0, :cond_2

    .line 327
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    .line 330
    :cond_2
    sget-object v0, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    invoke-virtual {v0, p0, p1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 331
    monitor-exit v1

    goto :goto_0

    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method private static checkFor3rdPartyId(Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 553
    .local p0, "profileParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p0, :cond_0

    const-string v1, "mbox3rdPartyId"

    invoke-interface {p0, v1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 554
    const-string v1, "mbox3rdPartyId"

    invoke-interface {p0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .line 555
    .local v0, "newThirdPartyId":Ljava/lang/Object;
    if-eqz v0, :cond_1

    .line 556
    invoke-virtual {v0}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/adobe/mobile/TargetWorker;->setThirdPartyId(Ljava/lang/String;)V

    .line 561
    :goto_0
    const-string v1, "mbox3rdPartyId"

    invoke-interface {p0, v1}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 563
    .end local v0    # "newThirdPartyId":Ljava/lang/Object;
    :cond_0
    return-void

    .line 559
    .restart local v0    # "newThirdPartyId":Ljava/lang/Object;
    :cond_1
    const/4 v1, 0x0

    invoke-static {v1}, Lcom/adobe/mobile/TargetWorker;->setThirdPartyId(Ljava/lang/String;)V

    goto :goto_0
.end method

.method private static checkForOldCookieValue(Ljava/lang/String;)Ljava/lang/String;
    .locals 10
    .param p0, "cookieName"    # Ljava/lang/String;

    .prologue
    .line 590
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v7

    if-nez v7, :cond_2

    .line 591
    :cond_0
    const/4 v6, 0x0

    .line 619
    :cond_1
    :goto_0
    return-object v6

    .line 594
    :cond_2
    const/4 v6, 0x0

    .line 597
    .local v6, "returnCookieValue":Ljava/lang/String;
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v4

    .line 598
    .local v4, "preferences":Landroid/content/SharedPreferences;
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v7, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    const-string v8, "_Expires"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v4, v7}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v7

    if-eqz v7, :cond_1

    .line 600
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v7, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    const-string v8, "_Expires"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    const-wide/16 v8, 0x0

    invoke-interface {v4, v7, v8, v9}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v0

    .line 601
    .local v0, "cookieExpiryTime":J
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v8

    cmp-long v7, v0, v8

    if-lez v7, :cond_3

    .line 602
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v7, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    const-string v8, "_Value"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    const-string v8, ""

    invoke-interface {v4, v7, v8}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 603
    .local v5, "prefsCookieValue":Ljava/lang/String;
    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v7

    if-lez v7, :cond_3

    .line 604
    move-object v6, v5

    .line 609
    .end local v5    # "prefsCookieValue":Ljava/lang/String;
    :cond_3
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v2

    .line 610
    .local v2, "editor":Landroid/content/SharedPreferences$Editor;
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v7, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    const-string v8, "_Value"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v2, v7}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 611
    new-instance v7, Ljava/lang/StringBuilder;

    invoke-direct {v7}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v7, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    const-string v8, "_Expires"

    invoke-virtual {v7, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v7

    invoke-virtual {v7}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v7

    invoke-interface {v2, v7}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 612
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto/16 :goto_0

    .line 615
    .end local v0    # "cookieExpiryTime":J
    .end local v2    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v4    # "preferences":Landroid/content/SharedPreferences;
    :catch_0
    move-exception v3

    .line 616
    .local v3, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v7, "Target - Error retrieving shared preferences - application context is null"

    const/4 v8, 0x0

    new-array v8, v8, [Ljava/lang/Object;

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0
.end method

.method private static forwardAnalyticsForTargetPayload(Lorg/json/JSONObject;)V
    .locals 10
    .param p0, "targetResponse"    # Lorg/json/JSONObject;

    .prologue
    .line 973
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Lorg/json/JSONObject;->length()I

    move-result v6

    if-gtz v6, :cond_1

    .line 1003
    :cond_0
    :goto_0
    return-void

    .line 978
    :cond_1
    :try_start_0
    const-string v6, "clientSideAnalyticsLoggingPayload"

    invoke-virtual {p0, v6}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    .line 980
    .local v0, "analyticsForTargetPayload":Lorg/json/JSONObject;
    if-eqz v0, :cond_0

    const-string v6, "a4t"

    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v6

    if-eqz v6, :cond_0

    .line 984
    const-string v6, "parameters"

    invoke-virtual {v0, v6}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v2

    .line 985
    .local v2, "analyticsForTargetPayloadParametersJson":Lorg/json/JSONObject;
    invoke-static {v2}, Lcom/adobe/mobile/StaticMethods;->mapFromJson(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object v3

    .line 988
    .local v3, "analyticsForTargetPayloadParametersTemp":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz v3, :cond_0

    invoke-virtual {v3}, Ljava/util/HashMap;->size()I

    move-result v6

    if-lez v6, :cond_0

    .line 993
    new-instance v1, Ljava/util/HashMap;

    invoke-virtual {v3}, Ljava/util/HashMap;->size()I

    move-result v6

    invoke-direct {v1, v6}, Ljava/util/HashMap;-><init>(I)V

    .line 994
    .local v1, "analyticsForTargetPayloadParameters":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-virtual {v3}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v6

    invoke-interface {v6}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v7

    :goto_1
    invoke-interface {v7}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_2

    invoke-interface {v7}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/Map$Entry;

    .line 995
    .local v4, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v8, "&&"

    invoke-virtual {v6, v8}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-interface {v4}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v6

    check-cast v6, Ljava/lang/String;

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-interface {v4}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v8

    invoke-virtual {v1, v6, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    .line 1000
    .end local v0    # "analyticsForTargetPayload":Lorg/json/JSONObject;
    .end local v1    # "analyticsForTargetPayloadParameters":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v2    # "analyticsForTargetPayloadParametersJson":Lorg/json/JSONObject;
    .end local v3    # "analyticsForTargetPayloadParametersTemp":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v4    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    :catch_0
    move-exception v5

    .line 1001
    .local v5, "ex":Lorg/json/JSONException;
    const-string v6, "Target - could not forward payload to Analytics (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    invoke-virtual {v5}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 998
    .end local v5    # "ex":Lorg/json/JSONException;
    .restart local v0    # "analyticsForTargetPayload":Lorg/json/JSONObject;
    .restart local v1    # "analyticsForTargetPayloadParameters":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    .restart local v2    # "analyticsForTargetPayloadParametersJson":Lorg/json/JSONObject;
    .restart local v3    # "analyticsForTargetPayloadParametersTemp":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_2
    :try_start_1
    const-string v6, "AnalyticsForTarget"

    invoke-static {v6, v1}, Lcom/adobe/mobile/AnalyticsTrackInternal;->trackInternal(Ljava/lang/String;Ljava/util/Map;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_0
.end method

.method private static getEdgeHost()Ljava/lang/String;
    .locals 6

    .prologue
    const/4 v2, 0x0

    .line 937
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_edgeHostMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 938
    :try_start_0
    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;

    if-eqz v4, :cond_0

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result v4

    if-eqz v4, :cond_1

    .line 940
    :cond_0
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    .line 941
    .local v1, "preferences":Landroid/content/SharedPreferences;
    const-string v4, "ADBMOBILE_TARGET_EDGE_HOST"

    const/4 v5, 0x0

    invoke-interface {v1, v4, v5}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    sput-object v4, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 949
    :cond_1
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;

    monitor-exit v3

    :goto_0
    return-object v2

    .line 943
    :catch_0
    move-exception v0

    .line 944
    .local v0, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Target - Error retrieving shared preferences - application context is null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 945
    monitor-exit v3

    goto :goto_0

    .line 950
    .end local v0    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method private static getErrorMessage(Lorg/json/JSONObject;)Ljava/lang/String;
    .locals 3
    .param p0, "jsonResponse"    # Lorg/json/JSONObject;

    .prologue
    const/4 v1, 0x0

    .line 485
    if-nez p0, :cond_0

    .line 494
    :goto_0
    return-object v1

    .line 490
    :cond_0
    :try_start_0
    const-string v2, "message"

    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v1

    goto :goto_0

    .line 492
    :catch_0
    move-exception v0

    .line 494
    .local v0, "ex":Lorg/json/JSONException;
    goto :goto_0
.end method

.method private static getInternalTargetParams()Ljava/util/HashMap;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 818
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 821
    .local v0, "internalParams":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->getContextData()Ljava/util/HashMap;

    move-result-object v1

    .line 822
    .local v1, "lifecycleData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz v1, :cond_0

    invoke-virtual {v1}, Ljava/util/HashMap;->size()I

    move-result v3

    if-lez v3, :cond_0

    .line 823
    invoke-virtual {v0, v1}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 827
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/AnalyticsTrackLifetimeValueIncrease;->getLifetimeValue()Ljava/math/BigDecimal;

    move-result-object v2

    .line 828
    .local v2, "ltv":Ljava/math/BigDecimal;
    if-eqz v2, :cond_1

    .line 829
    const-string v3, "a.ltv.amount"

    invoke-virtual {v2}, Ljava/math/BigDecimal;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v3, v4}, Lcom/adobe/mobile/TargetWorker;->addPersistentParameter(Ljava/lang/String;Ljava/lang/String;)V

    .line 833
    :cond_1
    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_persistentParametersMutex:Ljava/lang/Object;

    monitor-enter v4

    .line 834
    :try_start_0
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    if-eqz v3, :cond_2

    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    invoke-virtual {v3}, Ljava/util/HashMap;->size()I

    move-result v3

    if-lez v3, :cond_2

    .line 835
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    invoke-virtual {v0, v3}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 837
    :cond_2
    monitor-exit v4

    .line 839
    return-object v0

    .line 837
    :catchall_0
    move-exception v3

    monitor-exit v4
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v3
.end method

.method private static getJsonObjectForParameters(Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)Lorg/json/JSONObject;
    .locals 12
    .param p0, "mboxName"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lorg/json/JSONObject;"
        }
    .end annotation

    .prologue
    .local p1, "profileParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p2, "orderParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p3, "mboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v10, 0x1

    .line 503
    :try_start_0
    new-instance v4, Lorg/json/JSONObject;

    invoke-direct {v4}, Lorg/json/JSONObject;-><init>()V

    .line 506
    .local v4, "parametersJson":Lorg/json/JSONObject;
    const-string v8, "mbox"

    invoke-virtual {v4, v8, p0}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 507
    const-string v8, "clientSideAnalyticsLogging"

    const/4 v9, 0x1

    invoke-virtual {v4, v8, v9}, Lorg/json/JSONObject;->put(Ljava/lang/String;Z)Lorg/json/JSONObject;

    .line 510
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->getTntId()Ljava/lang/String;

    move-result-object v7

    .line 511
    .local v7, "tntId":Ljava/lang/String;
    if-eqz v7, :cond_0

    invoke-virtual {v7}, Ljava/lang/String;->isEmpty()Z

    move-result v8

    if-nez v8, :cond_0

    .line 512
    const-string v8, "tntId"

    invoke-virtual {v4, v8, v7}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 515
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->getThirdPartyId()Ljava/lang/String;

    move-result-object v6

    .line 516
    .local v6, "thirdPartyId":Ljava/lang/String;
    if-eqz v6, :cond_1

    invoke-virtual {v6}, Ljava/lang/String;->isEmpty()Z

    move-result v8

    if-nez v8, :cond_1

    .line 517
    const-string v8, "thirdPartyId"

    invoke-virtual {v4, v8, v6}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 520
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v8

    invoke-virtual {v8}, Lcom/adobe/mobile/VisitorIDService;->getMarketingCloudID()Ljava/lang/String;

    move-result-object v2

    .line 521
    .local v2, "mcVisId":Ljava/lang/String;
    if-eqz v2, :cond_2

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v8

    if-nez v8, :cond_2

    .line 522
    const-string v8, "marketingCloudVisitorId"

    invoke-virtual {v4, v8, v2}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 526
    :cond_2
    invoke-static {p1}, Lcom/adobe/mobile/TargetWorker;->getProfileParameters(Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object v5

    .line 527
    .local v5, "profileParametersJson":Lorg/json/JSONObject;
    if-eqz v5, :cond_3

    invoke-virtual {v5}, Lorg/json/JSONObject;->length()I

    move-result v8

    if-lez v8, :cond_3

    .line 528
    const-string v8, "profileParameters"

    invoke-virtual {v4, v8, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 532
    :cond_3
    invoke-static {p2}, Lcom/adobe/mobile/TargetWorker;->getOrderParameters(Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object v3

    .line 533
    .local v3, "orderDetailsJson":Lorg/json/JSONObject;
    if-eqz v3, :cond_4

    invoke-virtual {v3}, Lorg/json/JSONObject;->length()I

    move-result v8

    if-lez v8, :cond_4

    .line 534
    const-string v8, "order"

    invoke-virtual {v4, v8, v3}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 538
    :cond_4
    invoke-static {p3}, Lcom/adobe/mobile/TargetWorker;->getMboxParameters(Ljava/util/Map;)Lorg/json/JSONObject;

    move-result-object v1

    .line 539
    .local v1, "mboxParametersJson":Lorg/json/JSONObject;
    if-eqz v1, :cond_5

    invoke-virtual {v1}, Lorg/json/JSONObject;->length()I

    move-result v8

    if-lez v8, :cond_5

    .line 540
    const-string v8, "mboxParameters"

    invoke-virtual {v4, v8, v1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 547
    .end local v1    # "mboxParametersJson":Lorg/json/JSONObject;
    .end local v2    # "mcVisId":Ljava/lang/String;
    .end local v3    # "orderDetailsJson":Lorg/json/JSONObject;
    .end local v4    # "parametersJson":Lorg/json/JSONObject;
    .end local v5    # "profileParametersJson":Lorg/json/JSONObject;
    .end local v6    # "thirdPartyId":Ljava/lang/String;
    .end local v7    # "tntId":Ljava/lang/String;
    :cond_5
    :goto_0
    return-object v4

    .line 545
    :catch_0
    move-exception v0

    .line 546
    .local v0, "ex":Lorg/json/JSONException;
    const-string v8, "Target - JSONException while trying to get content (%s)"

    new-array v9, v10, [Ljava/lang/Object;

    const/4 v10, 0x0

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v11

    aput-object v11, v9, v10

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 547
    const/4 v4, 0x0

    goto :goto_0
.end method

.method private static getMboxParameters(Ljava/util/Map;)Lorg/json/JSONObject;
    .locals 14
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lorg/json/JSONObject;"
        }
    .end annotation

    .prologue
    .local p0, "mboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v13, 0x1

    const/4 v12, 0x0

    .line 730
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->getVisitorIdParameters()Lorg/json/JSONObject;

    move-result-object v7

    .line 732
    .local v7, "visitorIdParameters":Lorg/json/JSONObject;
    new-instance v4, Lorg/json/JSONObject;

    invoke-direct {v4}, Lorg/json/JSONObject;-><init>()V

    .line 735
    .local v4, "mboxParametersJson":Lorg/json/JSONObject;
    if-eqz v7, :cond_0

    invoke-virtual {v7}, Lorg/json/JSONObject;->length()I

    move-result v8

    if-lez v8, :cond_0

    .line 736
    invoke-virtual {v7}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v6

    .line 738
    .local v6, "visidKeys":Ljava/util/Iterator;
    :goto_0
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v8

    if-eqz v8, :cond_0

    .line 739
    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    .line 741
    .local v0, "currentKey":Ljava/lang/String;
    :try_start_0
    invoke-virtual {v7, v0}, Lorg/json/JSONObject;->get(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v8

    invoke-virtual {v4, v0, v8}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 743
    :catch_0
    move-exception v2

    .line 744
    .local v2, "ex":Lorg/json/JSONException;
    const-string v8, "Target - JSONException adding mbox parameter to target request (%s)"

    new-array v9, v13, [Ljava/lang/Object;

    invoke-virtual {v2}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v12

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 750
    .end local v0    # "currentKey":Ljava/lang/String;
    .end local v2    # "ex":Lorg/json/JSONException;
    .end local v6    # "visidKeys":Ljava/util/Iterator;
    :cond_0
    if-eqz p0, :cond_1

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v8

    if-lez v8, :cond_1

    .line 751
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v8

    invoke-interface {v8}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v9

    :goto_1
    invoke-interface {v9}, Ljava/util/Iterator;->hasNext()Z

    move-result v8

    if-eqz v8, :cond_1

    invoke-interface {v9}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 753
    .local v1, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_1
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/String;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v10

    invoke-virtual {v4, v8, v10}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_1

    .line 755
    :catch_1
    move-exception v2

    .line 756
    .restart local v2    # "ex":Lorg/json/JSONException;
    const-string v8, "Target - JSONException adding mbox parameter to target request (%s)"

    new-array v10, v13, [Ljava/lang/Object;

    invoke-virtual {v2}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v11

    aput-object v11, v10, v12

    invoke-static {v8, v10}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 762
    .end local v1    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v2    # "ex":Lorg/json/JSONException;
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->getInternalTargetParams()Ljava/util/HashMap;

    move-result-object v3

    .line 763
    .local v3, "internalParams":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz v3, :cond_2

    invoke-virtual {v3}, Ljava/util/HashMap;->size()I

    move-result v8

    if-lez v8, :cond_2

    .line 764
    invoke-virtual {v3}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v8

    invoke-interface {v8}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v9

    :goto_2
    invoke-interface {v9}, Ljava/util/Iterator;->hasNext()Z

    move-result v8

    if-eqz v8, :cond_2

    invoke-interface {v9}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/Map$Entry;

    .line 766
    .local v5, "param":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_2
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/String;

    invoke-interface {v5}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v10

    invoke-virtual {v4, v8, v10}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_2

    goto :goto_2

    .line 768
    :catch_2
    move-exception v2

    .line 769
    .restart local v2    # "ex":Lorg/json/JSONException;
    const-string v8, "Target - JSONException adding mbox parameter to target request (%s)"

    new-array v10, v13, [Ljava/lang/Object;

    invoke-virtual {v2}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v11

    aput-object v11, v10, v12

    invoke-static {v8, v10}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2

    .line 774
    .end local v2    # "ex":Lorg/json/JSONException;
    .end local v5    # "param":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_2
    return-object v4
.end method

.method private static getOrderParameters(Ljava/util/Map;)Lorg/json/JSONObject;
    .locals 14
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lorg/json/JSONObject;"
        }
    .end annotation

    .prologue
    .line 623
    .local p0, "orderParams":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p0, :cond_0

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v10

    if-gtz v10, :cond_2

    .line 624
    :cond_0
    const/4 v2, 0x0

    .line 662
    :cond_1
    :goto_0
    return-object v2

    .line 627
    :cond_2
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    .line 629
    .local v2, "orderJson":Lorg/json/JSONObject;
    const-string v10, "id"

    invoke-interface {p0, v10}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .line 630
    .local v1, "orderId":Ljava/lang/Object;
    const-string v10, "total"

    invoke-interface {p0, v10}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    .line 631
    .local v3, "orderTotal":Ljava/lang/Object;
    const-string v10, "purchasedProductIds"

    invoke-interface {p0, v10}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v7

    .line 634
    .local v7, "products":Ljava/lang/Object;
    if-eqz v1, :cond_3

    :try_start_0
    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v10}, Ljava/lang/String;->isEmpty()Z

    move-result v10

    if-nez v10, :cond_3

    .line 635
    const-string v10, "id"

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v11

    invoke-virtual {v2, v10, v11}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 638
    :cond_3
    if-eqz v3, :cond_4

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v10}, Ljava/lang/String;->isEmpty()Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-result v10

    if-nez v10, :cond_4

    .line 640
    :try_start_1
    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-static {v10}, Ljava/lang/Double;->parseDouble(Ljava/lang/String;)D

    move-result-wide v4

    .line 641
    .local v4, "orderTotalDouble":D
    const-string v10, "total"

    invoke-virtual {v2, v10, v4, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;D)Lorg/json/JSONObject;
    :try_end_1
    .catch Ljava/lang/NumberFormatException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    .line 648
    .end local v4    # "orderTotalDouble":D
    :cond_4
    :goto_1
    if-eqz v7, :cond_1

    :try_start_2
    invoke-virtual {v7}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v10}, Ljava/lang/String;->isEmpty()Z

    move-result v10

    if-nez v10, :cond_1

    .line 649
    invoke-virtual {v7}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v10

    const-string v11, ","

    invoke-virtual {v10, v11}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v9

    .line 650
    .local v9, "productsList":[Ljava/lang/String;
    new-instance v8, Lorg/json/JSONArray;

    invoke-direct {v8}, Lorg/json/JSONArray;-><init>()V

    .line 651
    .local v8, "productsJson":Lorg/json/JSONArray;
    array-length v11, v9

    const/4 v10, 0x0

    :goto_2
    if-ge v10, v11, :cond_5

    aget-object v6, v9, v10

    .line 652
    .local v6, "product":Ljava/lang/String;
    invoke-virtual {v6}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v8, v12}, Lorg/json/JSONArray;->put(Ljava/lang/Object;)Lorg/json/JSONArray;

    .line 651
    add-int/lit8 v10, v10, 0x1

    goto :goto_2

    .line 643
    .end local v6    # "product":Ljava/lang/String;
    .end local v8    # "productsJson":Lorg/json/JSONArray;
    .end local v9    # "productsList":[Ljava/lang/String;
    :catch_0
    move-exception v0

    .line 644
    .local v0, "ex":Ljava/lang/NumberFormatException;
    const-string v10, "Target - NumberFormatException while creating order details (%s)"

    const/4 v11, 0x1

    new-array v11, v11, [Ljava/lang/Object;

    const/4 v12, 0x0

    invoke-virtual {v0}, Ljava/lang/NumberFormatException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v13

    aput-object v13, v11, v12

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_1

    .line 658
    .end local v0    # "ex":Ljava/lang/NumberFormatException;
    :catch_1
    move-exception v0

    .line 659
    .local v0, "ex":Lorg/json/JSONException;
    const-string v10, "Target - JSONException while creating order details (%s)"

    const/4 v11, 0x1

    new-array v11, v11, [Ljava/lang/Object;

    const/4 v12, 0x0

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v13

    aput-object v13, v11, v12

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 662
    const/4 v2, 0x0

    goto/16 :goto_0

    .line 654
    .end local v0    # "ex":Lorg/json/JSONException;
    .restart local v8    # "productsJson":Lorg/json/JSONArray;
    .restart local v9    # "productsList":[Ljava/lang/String;
    :cond_5
    :try_start_3
    const-string v10, "purchasedProductIds"

    invoke-virtual {v2, v10, v8}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_3
    .catch Lorg/json/JSONException; {:try_start_3 .. :try_end_3} :catch_1

    goto/16 :goto_0
.end method

.method private static getOrderParametersFromMboxParameters(Ljava/util/Map;)Ljava/util/Map;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 700
    .local p0, "mboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p0, :cond_0

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v4

    if-gtz v4, :cond_2

    .line 701
    :cond_0
    const/4 v0, 0x0

    .line 726
    :cond_1
    :goto_0
    return-object v0

    .line 704
    :cond_2
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 706
    .local v0, "newOrderParams":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v4, "id"

    invoke-interface {p0, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    .line 707
    .local v1, "orderId":Ljava/lang/Object;
    const-string v4, "total"

    invoke-interface {p0, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    .line 708
    .local v2, "orderTotal":Ljava/lang/Object;
    const-string v4, "purchasedProductIds"

    invoke-interface {p0, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v3

    .line 711
    .local v3, "products":Ljava/lang/Object;
    if-eqz v1, :cond_3

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z

    move-result v4

    if-nez v4, :cond_3

    .line 712
    const-string v4, "id"

    invoke-virtual {v1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v4, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 713
    const-string v4, "id"

    invoke-interface {p0, v4}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 716
    :cond_3
    if-eqz v2, :cond_4

    invoke-virtual {v2}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z

    move-result v4

    if-nez v4, :cond_4

    .line 717
    const-string v4, "total"

    invoke-virtual {v0, v4, v2}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 718
    const-string v4, "total"

    invoke-interface {p0, v4}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 721
    :cond_4
    if-eqz v3, :cond_1

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z

    move-result v4

    if-nez v4, :cond_1

    .line 722
    const-string v4, "purchasedProductIds"

    invoke-virtual {v0, v4, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 723
    const-string v4, "purchasedProductIds"

    invoke-interface {p0, v4}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0
.end method

.method private static getProfileParameters(Ljava/util/Map;)Lorg/json/JSONObject;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lorg/json/JSONObject;"
        }
    .end annotation

    .prologue
    .line 801
    .local p0, "customerParams":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_0
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    .line 803
    .local v2, "profileParametersJson":Lorg/json/JSONObject;
    if-eqz p0, :cond_0

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v3

    if-lez v3, :cond_0

    .line 804
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 805
    .local v1, "param":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v2, v3, v5}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 811
    .end local v1    # "param":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v2    # "profileParametersJson":Lorg/json/JSONObject;
    :catch_0
    move-exception v0

    .line 812
    .local v0, "ex":Lorg/json/JSONException;
    const-string v3, "Target - Error adding profile parameters to target request (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 813
    const/4 v2, 0x0

    .end local v0    # "ex":Lorg/json/JSONException;
    :cond_0
    return-object v2
.end method

.method private static getProfileParametersFromMboxParameters(Ljava/util/Map;)Ljava/util/Map;
    .locals 10
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 667
    .local p0, "mboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p0, :cond_0

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v8

    if-gtz v8, :cond_2

    .line 668
    :cond_0
    const/4 v6, 0x0

    .line 695
    :cond_1
    return-object v6

    .line 671
    :cond_2
    new-instance v6, Ljava/util/HashMap;

    invoke-direct {v6}, Ljava/util/HashMap;-><init>()V

    .line 672
    .local v6, "newProfileParams":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4, p0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 676
    .local v4, "mboxParametersCopy":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-virtual {v4}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v8

    invoke-interface {v8}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v8

    :cond_3
    :goto_0
    invoke-interface {v8}, Ljava/util/Iterator;->hasNext()Z

    move-result v9

    if-eqz v9, :cond_1

    invoke-interface {v8}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 677
    .local v2, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 678
    .local v3, "key":Ljava/lang/String;
    if-eqz v3, :cond_3

    .line 681
    invoke-virtual {v3}, Ljava/lang/String;->trim()Ljava/lang/String;

    move-result-object v7

    .line 682
    .local v7, "trimmedKey":Ljava/lang/String;
    invoke-virtual {v7}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v0

    .line 683
    .local v0, "cleanedKey":Ljava/lang/String;
    const-string v9, "profile."

    invoke-virtual {v0, v9}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_3

    .line 685
    const-string v9, "."

    invoke-virtual {v0, v9}, Ljava/lang/String;->indexOf(Ljava/lang/String;)I

    move-result v1

    .line 686
    .local v1, "dotIndex":I
    add-int/lit8 v9, v1, 0x1

    invoke-virtual {v7, v9}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v5

    .line 688
    .local v5, "newKeyName":Ljava/lang/String;
    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v9

    invoke-virtual {v6, v5, v9}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 691
    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v9

    invoke-interface {p0, v9}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0
.end method

.method private static getRequestUrl()Ljava/lang/String;
    .locals 7

    .prologue
    const/4 v6, 0x1

    const/4 v5, 0x0

    .line 860
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getClientCode()Ljava/lang/String;

    move-result-object v2

    .line 866
    .local v2, "targetClientCode":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->getEdgeHost()Ljava/lang/String;

    move-result-object v0

    .line 867
    .local v0, "edgeHost":Ljava/lang/String;
    if-eqz v0, :cond_0

    invoke-virtual {v0}, Ljava/lang/String;->isEmpty()Z

    move-result v3

    if-nez v3, :cond_0

    .line 868
    move-object v1, v0

    .line 873
    .local v1, "requestHost":Ljava/lang/String;
    :goto_0
    const-string v3, "https://%s/rest/v1/mbox/%s?client=%s"

    const/4 v4, 0x3

    new-array v4, v4, [Ljava/lang/Object;

    aput-object v1, v4, v5

    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->getSessionId()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    const/4 v5, 0x2

    aput-object v2, v4, v5

    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    return-object v3

    .line 870
    .end local v1    # "requestHost":Ljava/lang/String;
    :cond_0
    const-string v3, "%s.tt.omtrdc.net"

    new-array v4, v6, [Ljava/lang/Object;

    aput-object v2, v4, v5

    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    .restart local v1    # "requestHost":Ljava/lang/String;
    goto :goto_0
.end method

.method protected static getSessionId()Ljava/lang/String;
    .locals 6

    .prologue
    const/4 v2, 0x0

    .line 298
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->setTntIdAndSessionFromOldCookieValues()V

    .line 300
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_sessionIdMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 301
    :try_start_0
    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    if-eqz v4, :cond_0

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z

    move-result v4

    if-nez v4, :cond_0

    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->sessionIdIsExpired()Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result v4

    if-eqz v4, :cond_2

    .line 303
    :cond_0
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    .line 304
    .local v1, "preferences":Landroid/content/SharedPreferences;
    const-string v4, "ADBMOBILE_TARGET_SESSION_ID"

    const/4 v5, 0x0

    invoke-interface {v1, v4, v5}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    sput-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    .line 305
    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    if-eqz v4, :cond_1

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z

    move-result v4

    if-nez v4, :cond_1

    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->sessionIdIsExpired()Z

    move-result v4

    if-eqz v4, :cond_2

    .line 306
    :cond_1
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v4

    invoke-virtual {v4}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v4

    const/4 v5, 0x0

    invoke-static {v4, v5}, Lcom/adobe/mobile/TargetWorker;->setSessionId(Ljava/lang/String;Z)V
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 315
    :cond_2
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    monitor-exit v3

    :goto_0
    return-object v2

    .line 309
    :catch_0
    move-exception v0

    .line 310
    .local v0, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Target - Error retrieving shared preferences - application context is null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 311
    monitor-exit v3

    goto :goto_0

    .line 316
    .end local v0    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method protected static getThirdPartyId()Ljava/lang/String;
    .locals 6

    .prologue
    const/4 v2, 0x0

    .line 176
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_thirdPartyIdMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 177
    :try_start_0
    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    if-eqz v4, :cond_0

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result v4

    if-eqz v4, :cond_1

    .line 179
    :cond_0
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    .line 180
    .local v1, "preferences":Landroid/content/SharedPreferences;
    const-string v4, "ADBMOBILE_TARGET_3RD_PARTY_ID"

    const/4 v5, 0x0

    invoke-interface {v1, v4, v5}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    sput-object v4, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 188
    :cond_1
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    monitor-exit v3

    :goto_0
    return-object v2

    .line 182
    :catch_0
    move-exception v0

    .line 183
    .local v0, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Target - Error retrieving shared preferences - application context is null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 184
    monitor-exit v3

    goto :goto_0

    .line 189
    .end local v0    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method protected static getTntId()Ljava/lang/String;
    .locals 6

    .prologue
    const/4 v2, 0x0

    .line 223
    invoke-static {}, Lcom/adobe/mobile/TargetWorker;->setTntIdAndSessionFromOldCookieValues()V

    .line 225
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_tntIdMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 227
    :try_start_0
    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    if-eqz v4, :cond_0

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result v4

    if-eqz v4, :cond_1

    .line 229
    :cond_0
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    .line 230
    .local v1, "preferences":Landroid/content/SharedPreferences;
    const-string v4, "ADBMOBILE_TARGET_TNT_ID"

    const/4 v5, 0x0

    invoke-interface {v1, v4, v5}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    sput-object v4, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 238
    :cond_1
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    monitor-exit v3

    :goto_0
    return-object v2

    .line 232
    :catch_0
    move-exception v0

    .line 233
    .local v0, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Target - Error retrieving shared preferences - application context is null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 234
    monitor-exit v3

    goto :goto_0

    .line 239
    .end local v0    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method private static getVisitorIdParameters()Lorg/json/JSONObject;
    .locals 8

    .prologue
    .line 779
    :try_start_0
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    .line 782
    .local v2, "visidParams":Lorg/json/JSONObject;
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v4

    invoke-virtual {v4}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v4

    if-eqz v4, :cond_0

    .line 783
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v4

    invoke-virtual {v4}, Lcom/adobe/mobile/VisitorIDService;->getTargetParameterMap()Ljava/util/HashMap;

    move-result-object v3

    .line 784
    .local v3, "visitorIdData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz v3, :cond_0

    invoke-virtual {v3}, Ljava/util/HashMap;->size()I

    move-result v4

    if-lez v4, :cond_0

    .line 785
    invoke-virtual {v3}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v5

    :goto_0
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 786
    .local v0, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v2, v4, v6}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 793
    .end local v0    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v3    # "visitorIdData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :catch_0
    move-exception v1

    .line 794
    .local v1, "ex":Lorg/json/JSONException;
    const-string v4, "Target - JSONException when adding Visitor ID data to Target request (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v1}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 795
    const/4 v2, 0x0

    .end local v1    # "ex":Lorg/json/JSONException;
    .end local v2    # "visidParams":Lorg/json/JSONObject;
    :cond_0
    return-object v2
.end method

.method protected static loadRequest(Lcom/adobe/mobile/TargetLocationRequest;Lcom/adobe/mobile/Target$TargetCallback;)V
    .locals 6
    .param p0, "request"    # Lcom/adobe/mobile/TargetLocationRequest;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/adobe/mobile/TargetLocationRequest;",
            "Lcom/adobe/mobile/Target$TargetCallback",
            "<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 120
    .local p1, "callback":Lcom/adobe/mobile/Target$TargetCallback;, "Lcom/adobe/mobile/Target$TargetCallback<Ljava/lang/String;>;"
    if-nez p0, :cond_1

    .line 121
    const-string v0, "Target - \"TargetLocationRequest\" object cannot be null"

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v0, v1}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 122
    if-eqz p1, :cond_0

    .line 123
    const/4 v0, 0x0

    invoke-interface {p1, v0}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    .line 145
    :cond_0
    :goto_0
    return-void

    .line 129
    :cond_1
    iget-object v4, p0, Lcom/adobe/mobile/TargetLocationRequest;->parameters:Ljava/util/HashMap;

    .line 134
    .local v4, "newMboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {v4}, Lcom/adobe/mobile/TargetWorker;->getProfileParametersFromMboxParameters(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v2

    .line 138
    .local v2, "newProfileParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {v4}, Lcom/adobe/mobile/TargetWorker;->getOrderParametersFromMboxParameters(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v3

    .line 142
    .local v3, "newOrderParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {v4}, Lcom/adobe/mobile/TargetWorker;->checkFor3rdPartyId(Ljava/util/Map;)V

    .line 144
    iget-object v0, p0, Lcom/adobe/mobile/TargetLocationRequest;->name:Ljava/lang/String;

    iget-object v1, p0, Lcom/adobe/mobile/TargetLocationRequest;->defaultContent:Ljava/lang/String;

    move-object v5, p1

    invoke-static/range {v0 .. v5}, Lcom/adobe/mobile/TargetWorker;->loadRequest(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;)V

    goto :goto_0
.end method

.method protected static loadRequest(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;)V
    .locals 8
    .param p0, "mboxName"    # Ljava/lang/String;
    .param p1, "defaultContent"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Lcom/adobe/mobile/Target$TargetCallback",
            "<",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 154
    .local p2, "profileParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p3, "orderParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p4, "mboxParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p5, "callback":Lcom/adobe/mobile/Target$TargetCallback;, "Lcom/adobe/mobile/Target$TargetCallback<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v1

    invoke-virtual {v1}, Lcom/adobe/mobile/MobileConfig;->mobileUsingTarget()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 155
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v1

    invoke-virtual {v1}, Lcom/adobe/mobile/MobileConfig;->getPrivacyStatus()Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v1

    sget-object v2, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_IN:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-eq v1, v2, :cond_2

    .line 156
    :cond_0
    if-eqz p5, :cond_1

    .line 157
    invoke-interface {p5, p1}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    .line 173
    :cond_1
    :goto_0
    return-void

    .line 163
    :cond_2
    if-eqz p0, :cond_3

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v1

    if-gtz v1, :cond_4

    .line 164
    :cond_3
    const-string v1, "Target - \"mboxName\" parameter is required for Target calls - returning default content"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 165
    if-eqz p5, :cond_1

    .line 166
    invoke-interface {p5, p1}, Lcom/adobe/mobile/Target$TargetCallback;->call(Ljava/lang/Object;)V

    goto :goto_0

    .line 171
    :cond_4
    new-instance v0, Lcom/adobe/mobile/TargetWorker$TargetRunner;

    const/4 v7, 0x0

    move-object v1, p0

    move-object v2, p1

    move-object v3, p2

    move-object v4, p3

    move-object v5, p4

    move-object v6, p5

    invoke-direct/range {v0 .. v7}, Lcom/adobe/mobile/TargetWorker$TargetRunner;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/Target$TargetCallback;Lcom/adobe/mobile/TargetWorker$1;)V

    .line 172
    .local v0, "targetRunner":Lcom/adobe/mobile/TargetWorker$TargetRunner;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v1

    invoke-interface {v1, v0}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    goto :goto_0
.end method

.method protected static removePersistentParameter(Ljava/lang/String;)V
    .locals 2
    .param p0, "key"    # Ljava/lang/String;

    .prologue
    .line 335
    if-nez p0, :cond_0

    .line 344
    :goto_0
    return-void

    .line 339
    :cond_0
    sget-object v1, Lcom/adobe/mobile/TargetWorker;->_persistentParametersMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 340
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    if-eqz v0, :cond_1

    .line 341
    sget-object v0, Lcom/adobe/mobile/TargetWorker;->_persistentParameters:Ljava/util/HashMap;

    invoke-virtual {v0, p0}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 343
    :cond_1
    monitor-exit v1

    goto :goto_0

    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method private static saveTargetResponseVariables(Lorg/json/JSONObject;)V
    .locals 3
    .param p0, "response"    # Lorg/json/JSONObject;

    .prologue
    const/4 v2, 0x0

    .line 845
    :try_start_0
    const-string v1, "tntId"

    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/adobe/mobile/TargetWorker;->setTntId(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 852
    :goto_0
    :try_start_1
    const-string v1, "edgeHost"

    invoke-virtual {p0, v1}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/adobe/mobile/TargetWorker;->setEdgeHost(Ljava/lang/String;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    .line 857
    :goto_1
    return-void

    .line 847
    :catch_0
    move-exception v0

    .line 848
    .local v0, "ex":Lorg/json/JSONException;
    invoke-static {v2}, Lcom/adobe/mobile/TargetWorker;->setTntId(Ljava/lang/String;)V

    goto :goto_0

    .line 854
    .end local v0    # "ex":Lorg/json/JSONException;
    :catch_1
    move-exception v0

    .line 855
    .restart local v0    # "ex":Lorg/json/JSONException;
    invoke-static {v2}, Lcom/adobe/mobile/TargetWorker;->setEdgeHost(Ljava/lang/String;)V

    goto :goto_1
.end method

.method private static sessionIdIsExpired()Z
    .locals 10

    .prologue
    const-wide/16 v8, 0x0

    const/4 v4, 0x0

    .line 923
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    .line 924
    .local v1, "preferences":Landroid/content/SharedPreferences;
    const-string v5, "ADBMOBILE_TARGET_LAST_TIMESTAMP"

    const-wide/16 v6, 0x0

    invoke-interface {v1, v5, v6, v7}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v2

    .line 927
    .local v2, "lastRequestTimestamp":J
    cmp-long v5, v2, v8

    if-eqz v5, :cond_0

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-wide v6

    sub-long/2addr v6, v2

    const-wide/16 v8, 0x708

    cmp-long v5, v6, v8

    if-ltz v5, :cond_0

    const/4 v4, 0x1

    .line 933
    .end local v2    # "lastRequestTimestamp":J
    :cond_0
    :goto_0
    return v4

    .line 929
    :catch_0
    move-exception v0

    .line 930
    .local v0, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v5, "Target - Error retrieving shared preferences - application context is null"

    new-array v6, v4, [Ljava/lang/Object;

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static setEdgeHost(Ljava/lang/String;)V
    .locals 5
    .param p0, "newEdgeHost"    # Ljava/lang/String;

    .prologue
    .line 954
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_edgeHostMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 955
    :try_start_0
    sput-object p0, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 958
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 959
    .local v0, "editor":Landroid/content/SharedPreferences$Editor;
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;

    if-eqz v2, :cond_0

    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 960
    :cond_0
    const-string v2, "ADBMOBILE_TARGET_EDGE_HOST"

    invoke-interface {v0, v2}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 964
    :goto_0
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 969
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_1
    :try_start_2
    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 970
    return-void

    .line 962
    .restart local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_1
    :try_start_3
    const-string v2, "ADBMOBILE_TARGET_EDGE_HOST"

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_edgeHost:Ljava/lang/String;

    invoke-interface {v0, v2, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;
    :try_end_3
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    .line 966
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v1

    .line 967
    .local v1, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_4
    const-string v2, "Target - Error retrieving shared preferences - application context is null"

    const/4 v4, 0x0

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 969
    .end local v1    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    throw v2
.end method

.method protected static setSessionId(Ljava/lang/String;)V
    .locals 1
    .param p0, "newSessionId"    # Ljava/lang/String;

    .prologue
    .line 877
    const/4 v0, 0x1

    invoke-static {p0, v0}, Lcom/adobe/mobile/TargetWorker;->setSessionId(Ljava/lang/String;Z)V

    .line 878
    return-void
.end method

.method private static setSessionId(Ljava/lang/String;Z)V
    .locals 2
    .param p0, "newSessionId"    # Ljava/lang/String;
    .param p1, "useMutex"    # Z

    .prologue
    .line 882
    if-eqz p1, :cond_0

    .line 883
    sget-object v1, Lcom/adobe/mobile/TargetWorker;->_sessionIdMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 884
    :try_start_0
    invoke-static {p0}, Lcom/adobe/mobile/TargetWorker;->setSessionIdInternal(Ljava/lang/String;)V

    .line 885
    monitor-exit v1

    .line 890
    :goto_0
    return-void

    .line 885
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0

    .line 888
    :cond_0
    invoke-static {p0}, Lcom/adobe/mobile/TargetWorker;->setSessionIdInternal(Ljava/lang/String;)V

    goto :goto_0
.end method

.method private static setSessionIdInternal(Ljava/lang/String;)V
    .locals 8
    .param p0, "newSessionId"    # Ljava/lang/String;

    .prologue
    .line 893
    sput-object p0, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    .line 896
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v3

    .line 897
    .local v3, "preferences":Landroid/content/SharedPreferences;
    const-string v4, "ADBMOBILE_TARGET_SESSION_ID"

    const/4 v5, 0x0

    invoke-interface {v3, v4, v5}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 901
    .local v2, "existingSessionId":Ljava/lang/String;
    if-eqz v2, :cond_0

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    invoke-virtual {v2, v4}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    if-nez v4, :cond_1

    .line 903
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 904
    .local v0, "editor":Landroid/content/SharedPreferences$Editor;
    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    if-eqz v4, :cond_2

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->isEmpty()Z

    move-result v4

    if-nez v4, :cond_2

    .line 905
    const-string v4, "ADBMOBILE_TARGET_SESSION_ID"

    sget-object v5, Lcom/adobe/mobile/TargetWorker;->_sessionId:Ljava/lang/String;

    invoke-interface {v0, v4, v5}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 906
    const-string v4, "ADBMOBILE_TARGET_LAST_TIMESTAMP"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v6

    invoke-interface {v0, v4, v6, v7}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 913
    :goto_0
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 919
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v2    # "existingSessionId":Ljava/lang/String;
    .end local v3    # "preferences":Landroid/content/SharedPreferences;
    :cond_1
    :goto_1
    return-void

    .line 909
    .restart local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    .restart local v2    # "existingSessionId":Ljava/lang/String;
    .restart local v3    # "preferences":Landroid/content/SharedPreferences;
    :cond_2
    const-string v4, "ADBMOBILE_TARGET_SESSION_ID"

    invoke-interface {v0, v4}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 910
    const-string v4, "ADBMOBILE_TARGET_LAST_TIMESTAMP"

    invoke-interface {v0, v4}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 916
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v2    # "existingSessionId":Ljava/lang/String;
    .end local v3    # "preferences":Landroid/content/SharedPreferences;
    :catch_0
    move-exception v1

    .line 917
    .local v1, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Target - Error retrieving shared preferences - application context is null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method protected static setThirdPartyId(Ljava/lang/String;)V
    .locals 5
    .param p0, "newThirdPartyId"    # Ljava/lang/String;

    .prologue
    .line 193
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_thirdPartyIdMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 194
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    if-eqz v2, :cond_0

    if-eqz p0, :cond_0

    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    invoke-virtual {v2, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 195
    monitor-exit v3

    .line 220
    :goto_0
    return-void

    .line 200
    :cond_0
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    if-eqz v2, :cond_1

    .line 201
    const/4 v2, 0x0

    invoke-static {v2}, Lcom/adobe/mobile/TargetWorker;->setSessionId(Ljava/lang/String;)V

    .line 204
    :cond_1
    sput-object p0, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 207
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 208
    .local v0, "editor":Landroid/content/SharedPreferences$Editor;
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    if-eqz v2, :cond_2

    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 209
    :cond_2
    const-string v2, "ADBMOBILE_TARGET_3RD_PARTY_ID"

    invoke-interface {v0, v2}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 214
    :goto_1
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 219
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_2
    :try_start_2
    monitor-exit v3

    goto :goto_0

    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2

    .line 212
    .restart local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_3
    :try_start_3
    const-string v2, "ADBMOBILE_TARGET_3RD_PARTY_ID"

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_thirdPartyId:Ljava/lang/String;

    invoke-interface {v0, v2, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;
    :try_end_3
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_1

    .line 216
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v1

    .line 217
    .local v1, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_4
    const-string v2, "Target - Error retrieving shared preferences - application context is null"

    const/4 v4, 0x0

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    goto :goto_2
.end method

.method protected static setTntId(Ljava/lang/String;)V
    .locals 5
    .param p0, "newTntId"    # Ljava/lang/String;

    .prologue
    .line 266
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_tntIdMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 268
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    invoke-static {v2, p0}, Lcom/adobe/mobile/TargetWorker;->tntIdValuesAreEqual(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 269
    monitor-exit v3

    .line 295
    :goto_0
    return-void

    .line 274
    :cond_0
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    if-eqz v2, :cond_1

    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_1

    .line 275
    const/4 v2, 0x0

    invoke-static {v2}, Lcom/adobe/mobile/TargetWorker;->setSessionId(Ljava/lang/String;)V

    .line 279
    :cond_1
    sput-object p0, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 282
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 283
    .local v0, "editor":Landroid/content/SharedPreferences$Editor;
    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    if-eqz v2, :cond_2

    sget-object v2, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->isEmpty()Z

    move-result v2

    if-eqz v2, :cond_3

    .line 284
    :cond_2
    const-string v2, "ADBMOBILE_TARGET_TNT_ID"

    invoke-interface {v0, v2}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 289
    :goto_1
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 294
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_2
    :try_start_2
    monitor-exit v3

    goto :goto_0

    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2

    .line 287
    .restart local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_3
    :try_start_3
    const-string v2, "ADBMOBILE_TARGET_TNT_ID"

    sget-object v4, Lcom/adobe/mobile/TargetWorker;->_tntId:Ljava/lang/String;

    invoke-interface {v0, v2, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;
    :try_end_3
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_1

    .line 291
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v1

    .line 292
    .local v1, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_4
    const-string v2, "Target - Error retrieving shared preferences - application context is null"

    const/4 v4, 0x0

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    goto :goto_2
.end method

.method protected static setTntIdAndSessionFromOldCookieValues()V
    .locals 4

    .prologue
    .line 568
    sget-object v3, Lcom/adobe/mobile/TargetWorker;->_checkOldCookiesMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 569
    :try_start_0
    sget-boolean v2, Lcom/adobe/mobile/TargetWorker;->_oldCookiesHaveBeenChecked:Z

    if-eqz v2, :cond_0

    .line 570
    monitor-exit v3

    .line 586
    .local v0, "oldPcid":Ljava/lang/String;
    .local v1, "oldSession":Ljava/lang/String;
    :goto_0
    return-void

    .line 574
    .end local v0    # "oldPcid":Ljava/lang/String;
    .end local v1    # "oldSession":Ljava/lang/String;
    :cond_0
    const-string v2, "mboxPC"

    invoke-static {v2}, Lcom/adobe/mobile/TargetWorker;->checkForOldCookieValue(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 575
    .restart local v0    # "oldPcid":Ljava/lang/String;
    if-eqz v0, :cond_1

    .line 576
    invoke-static {v0}, Lcom/adobe/mobile/TargetWorker;->setTntId(Ljava/lang/String;)V

    .line 579
    :cond_1
    const-string v2, "mboxSession"

    invoke-static {v2}, Lcom/adobe/mobile/TargetWorker;->checkForOldCookieValue(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 580
    .restart local v1    # "oldSession":Ljava/lang/String;
    if-eqz v1, :cond_2

    .line 581
    invoke-static {v1}, Lcom/adobe/mobile/TargetWorker;->setSessionId(Ljava/lang/String;)V

    .line 584
    :cond_2
    const/4 v2, 0x1

    sput-boolean v2, Lcom/adobe/mobile/TargetWorker;->_oldCookiesHaveBeenChecked:Z

    .line 585
    monitor-exit v3

    goto :goto_0

    .end local v1    # "oldSession":Ljava/lang/String;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v2
.end method

.method private static tntIdValuesAreEqual(Ljava/lang/String;Ljava/lang/String;)Z
    .locals 9
    .param p0, "oldId"    # Ljava/lang/String;
    .param p1, "newId"    # Ljava/lang/String;

    .prologue
    const/16 v8, 0x2e

    const/4 v4, 0x1

    const/4 v7, -0x1

    const/4 v5, 0x0

    .line 246
    if-nez p0, :cond_1

    if-nez p1, :cond_1

    .line 262
    :cond_0
    :goto_0
    return v4

    .line 249
    :cond_1
    if-eqz p0, :cond_2

    if-nez p1, :cond_3

    :cond_2
    move v4, v5

    .line 250
    goto :goto_0

    .line 252
    :cond_3
    invoke-virtual {p0, p1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-nez v6, :cond_0

    .line 256
    invoke-virtual {p0, v8}, Ljava/lang/String;->indexOf(I)I

    move-result v1

    .line 257
    .local v1, "dotIndexOfOldId":I
    if-ne v1, v7, :cond_4

    move-object v3, p0

    .line 259
    .local v3, "oldIdSubstring":Ljava/lang/String;
    :goto_1
    invoke-virtual {p1, v8}, Ljava/lang/String;->indexOf(I)I

    move-result v0

    .line 260
    .local v0, "dotIndexOfNewId":I
    if-ne v0, v7, :cond_5

    move-object v2, p1

    .line 262
    .local v2, "newIdSubstring":Ljava/lang/String;
    :goto_2
    invoke-virtual {v3, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v4

    goto :goto_0

    .line 257
    .end local v0    # "dotIndexOfNewId":I
    .end local v2    # "newIdSubstring":Ljava/lang/String;
    .end local v3    # "oldIdSubstring":Ljava/lang/String;
    :cond_4
    invoke-virtual {p0, v5, v1}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    goto :goto_1

    .line 260
    .restart local v0    # "dotIndexOfNewId":I
    .restart local v3    # "oldIdSubstring":Ljava/lang/String;
    :cond_5
    invoke-virtual {p1, v5, v0}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v2

    goto :goto_2
.end method
