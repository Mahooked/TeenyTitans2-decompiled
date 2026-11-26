.class final Lcom/adobe/mobile/RequestBuilder;
.super Ljava/lang/Object;
.source "RequestBuilder.java"


# static fields
.field private static final PRIVACY_MODE_KEY:Ljava/lang/String; = "a.privacy.mode"

.field private static final VAR_ESCAPE_PREFIX:Ljava/lang/String; = "&&"


# direct methods
.method constructor <init>()V
    .locals 0

    .prologue
    .line 25
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method protected static buildAndSendRequest(Ljava/util/Map;Ljava/util/Map;J)V
    .locals 16
    .param p2, "timeStamp"    # J
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;J)V"
        }
    .end annotation

    .prologue
    .line 31
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p1, "vars":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/WearableFunctionBridge;->shouldSendHit()Z

    move-result v9

    if-nez v9, :cond_1

    .line 122
    :cond_0
    :goto_0
    return-void

    .line 35
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v9

    invoke-virtual {v9}, Lcom/adobe/mobile/MobileConfig;->mobileUsingAnalytics()Z

    move-result v9

    if-eqz v9, :cond_0

    .line 39
    new-instance v6, Ljava/util/HashMap;

    invoke-direct {v6}, Ljava/util/HashMap;-><init>()V

    .line 41
    .local v6, "mutableData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultData()Ljava/util/HashMap;

    move-result-object v9

    invoke-virtual {v6, v9}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 42
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSinceLaunch()J

    move-result-wide v10

    .line 43
    .local v10, "t":J
    const-wide/16 v12, 0x0

    cmp-long v9, v10, v12

    if-lez v9, :cond_2

    .line 44
    const-string v9, "a.TimeSinceLaunch"

    invoke-static {v10, v11}, Ljava/lang/String;->valueOf(J)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v6, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 48
    :cond_2
    if-eqz p0, :cond_3

    .line 49
    move-object/from16 v0, p0

    invoke-virtual {v6, v0}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 52
    :cond_3
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v9

    invoke-virtual {v9}, Lcom/adobe/mobile/MobileConfig;->getPrivacyStatus()Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v9

    sget-object v12, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_UNKNOWN:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-ne v9, v12, :cond_4

    .line 53
    const-string v9, "a.privacy.mode"

    const-string v12, "unknown"

    invoke-virtual {v6, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 56
    :cond_4
    if-eqz p1, :cond_9

    new-instance v7, Ljava/util/HashMap;

    move-object/from16 v0, p1

    invoke-direct {v7, v0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 60
    .local v7, "mutableVars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :goto_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAID()Ljava/lang/String;

    move-result-object v2

    .line 61
    .local v2, "aid":Ljava/lang/String;
    if-eqz v2, :cond_5

    .line 62
    const-string v9, "aid"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAID()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v7, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 66
    :cond_5
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getVisitorID()Ljava/lang/String;

    move-result-object v9

    if-eqz v9, :cond_6

    .line 67
    const-string v9, "vid"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getVisitorID()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v7, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 70
    :cond_6
    const-string v9, "ce"

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v12

    invoke-virtual {v12}, Lcom/adobe/mobile/MobileConfig;->getCharacterSet()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v7, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 71
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v9

    invoke-virtual {v9}, Lcom/adobe/mobile/MobileConfig;->getOfflineTrackingEnabled()Z

    move-result v9

    if-eqz v9, :cond_7

    .line 72
    const-string v9, "ts"

    invoke-static/range {p2 .. p3}, Ljava/lang/Long;->toString(J)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v7, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 74
    :cond_7
    const-string v9, "t"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimestampString()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v7, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 77
    invoke-virtual {v6}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v9

    invoke-interface {v9}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    .line 78
    .local v3, "it":Ljava/util/Iterator;, "Ljava/util/Iterator<Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;>;"
    :cond_8
    :goto_2
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v9

    if-eqz v9, :cond_b

    .line 79
    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/util/Map$Entry;

    .line 80
    .local v5, "kvPair":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v5}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    .line 82
    .local v4, "key":Ljava/lang/String;
    if-nez v4, :cond_a

    .line 83
    invoke-interface {v3}, Ljava/util/Iterator;->remove()V

    goto :goto_2

    .line 56
    .end local v2    # "aid":Ljava/lang/String;
    .end local v3    # "it":Ljava/util/Iterator;, "Ljava/util/Iterator<Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;>;"
    .end local v4    # "key":Ljava/lang/String;
    .end local v5    # "kvPair":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v7    # "mutableVars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_9
    new-instance v7, Ljava/util/HashMap;

    invoke-direct {v7}, Ljava/util/HashMap;-><init>()V

    goto :goto_1

    .line 85
    .restart local v2    # "aid":Ljava/lang/String;
    .restart local v3    # "it":Ljava/util/Iterator;, "Ljava/util/Iterator<Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;>;"
    .restart local v4    # "key":Ljava/lang/String;
    .restart local v5    # "kvPair":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .restart local v7    # "mutableVars":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_a
    const-string v9, "&&"

    invoke-virtual {v4, v9}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v9

    if-eqz v9, :cond_8

    .line 86
    const-string v9, "&&"

    .line 87
    invoke-virtual {v9}, Ljava/lang/String;->length()I

    move-result v9

    invoke-virtual {v4, v9}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v9

    .line 88
    invoke-interface {v5}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v12

    .line 86
    invoke-virtual {v7, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 89
    invoke-interface {v3}, Ljava/util/Iterator;->remove()V

    goto :goto_2

    .line 93
    .end local v4    # "key":Ljava/lang/String;
    .end local v5    # "kvPair":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_b
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v9

    invoke-virtual {v9}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v9

    if-eqz v9, :cond_c

    .line 94
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v9

    invoke-virtual {v9}, Lcom/adobe/mobile/VisitorIDService;->getAnalyticsIdVisitorParameters()Ljava/util/Map;

    move-result-object v9

    invoke-virtual {v7, v9}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 98
    :cond_c
    new-instance v9, Ljava/util/HashMap;

    invoke-direct {v9, v7}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    new-instance v12, Ljava/util/HashMap;

    invoke-direct {v12, v6}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    new-instance v13, Ljava/util/HashMap;

    .line 100
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->getContextDataLowercase()Ljava/util/Map;

    move-result-object v14

    invoke-direct {v13, v14}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 98
    invoke-static {v9, v12, v13}, Lcom/adobe/mobile/Messages;->checkForInAppMessage(Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)V

    .line 103
    new-instance v9, Ljava/util/HashMap;

    invoke-direct {v9, v7}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    new-instance v12, Ljava/util/HashMap;

    invoke-direct {v12, v6}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    invoke-static {v9, v12}, Lcom/adobe/mobile/Messages;->checkFor3rdPartyCallbacks(Ljava/util/Map;Ljava/util/Map;)V

    .line 106
    const-string v9, "c"

    invoke-static {v6}, Lcom/adobe/mobile/StaticMethods;->translateContextData(Ljava/util/Map;)Lcom/adobe/mobile/ContextData;

    move-result-object v12

    invoke-virtual {v7, v9, v12}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 108
    new-instance v8, Ljava/lang/StringBuilder;

    const/16 v9, 0x800

    invoke-direct {v8, v9}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 110
    .local v8, "requestString":Ljava/lang/StringBuilder;
    const-string v9, "ndh=1"

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 111
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v9

    invoke-virtual {v9}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v9

    if-eqz v9, :cond_d

    .line 112
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v9

    invoke-virtual {v9}, Lcom/adobe/mobile/VisitorIDService;->getAnalyticsIdString()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 116
    :cond_d
    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->serializeToQueryString(Ljava/util/Map;Ljava/lang/StringBuilder;)V

    .line 118
    const-string v9, "Analytics - Request Queued (%s)"

    const/4 v12, 0x1

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    aput-object v8, v12, v13

    invoke-static {v9, v12}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 121
    invoke-static {}, Lcom/adobe/mobile/AnalyticsWorker;->sharedInstance()Lcom/adobe/mobile/AnalyticsWorker;

    move-result-object v9

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v12

    move-wide/from16 v0, p2

    invoke-virtual {v9, v12, v0, v1}, Lcom/adobe/mobile/AnalyticsWorker;->queue(Ljava/lang/String;J)V

    goto/16 :goto_0
.end method
