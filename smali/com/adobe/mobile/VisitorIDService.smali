.class final Lcom/adobe/mobile/VisitorIDService;
.super Ljava/lang/Object;
.source "VisitorIDService.java"


# static fields
.field static final ANALYTICS_PARAMETER_KEY_BLOB:Ljava/lang/String; = "aamb"

.field static final ANALYTICS_PARAMETER_KEY_LOCATION_HINT:Ljava/lang/String; = "aamlh"

.field static final ANALYTICS_PARAMETER_KEY_MID:Ljava/lang/String; = "mid"

.field static final ANALYTICS_PARAMETER_KEY_ORG:Ljava/lang/String; = "mcorgid"

.field static final CID_DELIMITER:Ljava/lang/String; = "%01"

.field static final RESPONSE_KEY_BLOB:Ljava/lang/String; = "d_blob"

.field static final RESPONSE_KEY_ERROR:Ljava/lang/String; = "error_msg"

.field static final RESPONSE_KEY_HINT:Ljava/lang/String; = "dcs_region"

.field static final RESPONSE_KEY_MID:Ljava/lang/String; = "d_mid"

.field static final RESPONSE_KEY_OPT_OUT:Ljava/lang/String; = "d_optout"

.field static final RESPONSE_KEY_TTL:Ljava/lang/String; = "id_sync_ttl"

.field static SERVER:Ljava/lang/String; = null

.field static final TARGET_PARAMETER_KEY_AID:Ljava/lang/String; = "mboxMCAVID"

.field static final TARGET_PARAMETER_KEY_BLOB:Ljava/lang/String; = "mboxAAMB"

.field static final TARGET_PARAMETER_KEY_HINT:Ljava/lang/String; = "mboxMCGLH"

.field static final TARGET_PARAMETER_KEY_MID:Ljava/lang/String; = "mboxMCGVID"

.field static final TIMEOUT:I = 0x7d0

.field static final VISITOR_ID_PARAMETER_KEY_CUSTOMER:Ljava/lang/String; = "d_cid_ic"

.field private static _instance:Lcom/adobe/mobile/VisitorIDService;

.field private static final _instanceMutex:Ljava/lang/Object;


# instance fields
.field private _aamIdString:Ljava/lang/String;

.field private _analyticsIdString:Ljava/lang/String;

.field private _blob:Ljava/lang/String;

.field private _customerIds:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;"
        }
    .end annotation
.end field

.field private _lastSync:J

.field private _locationHint:Ljava/lang/String;

.field private _marketingCloudID:Ljava/lang/String;

.field private _ttl:J

.field private final _visitorIDExector:Ljava/util/concurrent/Executor;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .prologue
    .line 33
    const-string v0, "dpm.demdex.net"

    sput-object v0, Lcom/adobe/mobile/VisitorIDService;->SERVER:Ljava/lang/String;

    .line 67
    const/4 v0, 0x0

    sput-object v0, Lcom/adobe/mobile/VisitorIDService;->_instance:Lcom/adobe/mobile/VisitorIDService;

    .line 68
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/VisitorIDService;->_instanceMutex:Ljava/lang/Object;

    return-void
.end method

.method protected constructor <init>()V
    .locals 1

    .prologue
    .line 80
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 82
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    iput-object v0, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    .line 84
    invoke-virtual {p0}, Lcom/adobe/mobile/VisitorIDService;->resetVariablesFromSharedPreferences()V

    .line 86
    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;)V

    .line 87
    return-void
.end method

.method private _generateAnalyticsCustomerIdString(Ljava/util/List;)Ljava/lang/String;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 636
    .local p1, "visitorIDs":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    if-nez p1, :cond_0

    .line 637
    const/4 v4, 0x0

    .line 653
    :goto_0
    return-object v4

    .line 640
    :cond_0
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    .line 641
    .local v3, "visitorIdMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_1
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/adobe/mobile/VisitorID;

    .line 642
    .local v2, "visitorID":Lcom/adobe/mobile/VisitorID;
    invoke-virtual {v2}, Lcom/adobe/mobile/VisitorID;->serializeIdentifierKeyForAnalyticsID()Ljava/lang/String;

    move-result-object v5

    iget-object v6, v2, Lcom/adobe/mobile/VisitorID;->id:Ljava/lang/String;

    invoke-virtual {v3, v5, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 643
    invoke-virtual {v2}, Lcom/adobe/mobile/VisitorID;->serializeAuthenticationKeyForAnalyticsID()Ljava/lang/String;

    move-result-object v5

    iget-object v6, v2, Lcom/adobe/mobile/VisitorID;->authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    invoke-virtual {v6}, Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;->getValue()I

    move-result v6

    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    invoke-virtual {v3, v5, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 646
    .end local v2    # "visitorID":Lcom/adobe/mobile/VisitorID;
    :cond_1
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 647
    .local v1, "translatedIds":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v4, "cid"

    invoke-static {v3}, Lcom/adobe/mobile/StaticMethods;->translateContextData(Ljava/util/Map;)Lcom/adobe/mobile/ContextData;

    move-result-object v5

    invoke-virtual {v1, v4, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 650
    new-instance v0, Ljava/lang/StringBuilder;

    const/16 v4, 0x800

    invoke-direct {v0, v4}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 651
    .local v0, "requestString":Ljava/lang/StringBuilder;
    invoke-static {v1, v0}, Lcom/adobe/mobile/StaticMethods;->serializeToQueryString(Ljava/util/Map;Ljava/lang/StringBuilder;)V

    .line 653
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    goto :goto_0
.end method

.method private _generateCustomerIdString(Ljava/util/List;)Ljava/lang/String;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 596
    .local p1, "newVisitorIDs":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    if-nez p1, :cond_0

    .line 597
    const/4 v3, 0x0

    .line 616
    :goto_0
    return-object v3

    .line 600
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 601
    .local v0, "customerIdString":Ljava/lang/StringBuilder;
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_2

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/adobe/mobile/VisitorID;

    .line 602
    .local v1, "newVisitorID":Lcom/adobe/mobile/VisitorID;
    const-string v4, "&"

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 603
    const-string v4, "d_cid_ic"

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 604
    const-string v4, "="

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 605
    iget-object v4, v1, Lcom/adobe/mobile/VisitorID;->idType:Ljava/lang/String;

    invoke-static {v4}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 606
    const-string v4, "%01"

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 608
    iget-object v4, v1, Lcom/adobe/mobile/VisitorID;->id:Ljava/lang/String;

    invoke-static {v4}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 609
    .local v2, "urlEncodedID":Ljava/lang/String;
    if-eqz v2, :cond_1

    .line 610
    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 613
    :cond_1
    const-string v4, "%01"

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 614
    iget-object v4, v1, Lcom/adobe/mobile/VisitorID;->authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    invoke-virtual {v4}, Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;->getValue()I

    move-result v4

    invoke-virtual {v0, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    goto :goto_1

    .line 616
    .end local v1    # "newVisitorID":Lcom/adobe/mobile/VisitorID;
    .end local v2    # "urlEncodedID":Ljava/lang/String;
    :cond_2
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    goto :goto_0
.end method

.method private _generateCustomerIds(Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)Ljava/util/List;
    .locals 9
    .param p2, "authenticationState"    # Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;",
            ")",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;"
        }
    .end annotation

    .prologue
    .line 657
    .local p1, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    if-nez p1, :cond_1

    .line 658
    const/4 v3, 0x0

    .line 672
    :cond_0
    return-object v3

    .line 661
    :cond_1
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 662
    .local v1, "identifiersCopy":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 664
    .local v3, "tempIds":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    invoke-virtual {v1}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_0
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/Map$Entry;

    .line 666
    .local v2, "newID":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    :try_start_0
    new-instance v7, Lcom/adobe/mobile/VisitorID;

    const-string v8, "d_cid_ic"

    invoke-interface {v2}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    invoke-interface {v2}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    invoke-direct {v7, v8, v4, v5, p2}, Lcom/adobe/mobile/VisitorID;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V

    invoke-interface {v3, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 667
    :catch_0
    move-exception v0

    .line 668
    .local v0, "ex":Ljava/lang/IllegalStateException;
    const-string v4, "ID Service - Unable to create ID after encoding:(%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-virtual {v0}, Ljava/lang/IllegalStateException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private _generateInternalIdString(Ljava/util/Map;)Ljava/lang/String;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 620
    .local p1, "dpids":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    if-nez p1, :cond_0

    .line 621
    const/4 v3, 0x0

    .line 632
    :goto_0
    return-object v3

    .line 624
    :cond_0
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 625
    .local v0, "dpidsCopy":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    .line 626
    .local v2, "internalIdString":Ljava/lang/StringBuilder;
    invoke-virtual {v0}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_1
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 627
    .local v1, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    const-string v3, "&d_cid="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 628
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-static {v3}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 629
    const-string v3, "%01"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 630
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-static {v3}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1

    .line 632
    .end local v1    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    :cond_1
    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    goto :goto_0
.end method

.method private _generateMID()Ljava/lang/String;
    .locals 12

    .prologue
    const-wide/16 v10, 0x0

    .line 562
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v4

    .line 564
    .local v4, "uuid":Ljava/util/UUID;
    invoke-virtual {v4}, Ljava/util/UUID;->getMostSignificantBits()J

    move-result-wide v2

    .line 565
    .local v2, "most":J
    invoke-virtual {v4}, Ljava/util/UUID;->getLeastSignificantBits()J

    move-result-wide v0

    .line 568
    .local v0, "least":J
    sget-object v5, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v6, "%019d%019d"

    const/4 v7, 0x2

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    cmp-long v9, v2, v10

    if-gez v9, :cond_0

    neg-long v2, v2

    .end local v2    # "most":J
    :cond_0
    invoke-static {v2, v3}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v9

    aput-object v9, v7, v8

    const/4 v8, 0x1

    cmp-long v9, v0, v10

    if-gez v9, :cond_1

    neg-long v0, v0

    .end local v0    # "least":J
    :cond_1
    invoke-static {v0, v1}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v5, v6, v7}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    return-object v5
.end method

.method private _generateStoredCustomerIdString(Ljava/util/List;)Ljava/lang/String;
    .locals 4
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 572
    .local p1, "visitorIDs":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    if-nez p1, :cond_0

    .line 573
    const/4 v2, 0x0

    .line 592
    :goto_0
    return-object v2

    .line 576
    :cond_0
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 577
    .local v0, "customerIdString":Ljava/lang/StringBuilder;
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_1
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_2

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/adobe/mobile/VisitorID;

    .line 578
    .local v1, "visitorID":Lcom/adobe/mobile/VisitorID;
    const-string v3, "&"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 579
    const-string v3, "d_cid_ic"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 580
    const-string v3, "="

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 581
    iget-object v3, v1, Lcom/adobe/mobile/VisitorID;->idType:Ljava/lang/String;

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 582
    const-string v3, "%01"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 584
    iget-object v3, v1, Lcom/adobe/mobile/VisitorID;->id:Ljava/lang/String;

    if-eqz v3, :cond_1

    .line 585
    iget-object v3, v1, Lcom/adobe/mobile/VisitorID;->id:Ljava/lang/String;

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 588
    :cond_1
    const-string v3, "%01"

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 589
    iget-object v3, v1, Lcom/adobe/mobile/VisitorID;->authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    invoke-virtual {v3}, Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;->getValue()I

    move-result v3

    invoke-virtual {v0, v3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    goto :goto_1

    .line 592
    .end local v1    # "visitorID":Lcom/adobe/mobile/VisitorID;
    :cond_2
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    goto :goto_0
.end method

.method private _mergeCustomerIds(Ljava/util/List;)Ljava/util/List;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;)",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;"
        }
    .end annotation

    .prologue
    .line 676
    .local p1, "newCustomerIds":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    if-nez p1, :cond_1

    .line 677
    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService;->_customerIds:Ljava/util/List;

    .line 699
    :cond_0
    return-object v2

    .line 680
    :cond_1
    iget-object v4, p0, Lcom/adobe/mobile/VisitorIDService;->_customerIds:Ljava/util/List;

    if-eqz v4, :cond_3

    new-instance v2, Ljava/util/ArrayList;

    iget-object v4, p0, Lcom/adobe/mobile/VisitorIDService;->_customerIds:Ljava/util/List;

    invoke-direct {v2, v4}, Ljava/util/ArrayList;-><init>(Ljava/util/Collection;)V

    .line 683
    .local v2, "tempIds":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    :goto_0
    invoke-interface {p1}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_1
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_0

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/adobe/mobile/VisitorID;

    .line 685
    .local v1, "newID":Lcom/adobe/mobile/VisitorID;
    invoke-interface {v2}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v5

    :cond_2
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v6

    if-eqz v6, :cond_4

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Lcom/adobe/mobile/VisitorID;

    .line 686
    .local v3, "visitorID":Lcom/adobe/mobile/VisitorID;
    iget-object v6, v1, Lcom/adobe/mobile/VisitorID;->idType:Ljava/lang/String;

    iget-object v7, v1, Lcom/adobe/mobile/VisitorID;->id:Ljava/lang/String;

    invoke-virtual {v3, v6, v7}, Lcom/adobe/mobile/VisitorID;->isVisitorID(Ljava/lang/String;Ljava/lang/String;)Z

    move-result v6

    if-eqz v6, :cond_2

    .line 687
    iget-object v5, v1, Lcom/adobe/mobile/VisitorID;->authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    iput-object v5, v3, Lcom/adobe/mobile/VisitorID;->authenticationState:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    goto :goto_1

    .line 680
    .end local v1    # "newID":Lcom/adobe/mobile/VisitorID;
    .end local v2    # "tempIds":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    .end local v3    # "visitorID":Lcom/adobe/mobile/VisitorID;
    :cond_3
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    goto :goto_0

    .line 693
    .restart local v1    # "newID":Lcom/adobe/mobile/VisitorID;
    .restart local v2    # "tempIds":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    :cond_4
    :try_start_0
    invoke-interface {v2, v1}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    .line 694
    :catch_0
    move-exception v0

    .line 695
    .local v0, "ex":Ljava/lang/IllegalStateException;
    const-string v5, "ID Service - Unable to create ID after encoding:(%s)"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-virtual {v0}, Ljava/lang/IllegalStateException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method private _parseIdString(Ljava/lang/String;)Ljava/util/List;
    .locals 13
    .param p1, "idString"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            ")",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;"
        }
    .end annotation

    .prologue
    .line 703
    if-nez p1, :cond_1

    .line 704
    const/4 v6, 0x0

    .line 731
    :cond_0
    return-object v6

    .line 707
    :cond_1
    const-string v7, "&"

    invoke-virtual {p1, v7}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v7

    invoke-static {v7}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    .line 708
    .local v0, "customerIdComponentsArray":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    new-instance v6, Ljava/util/ArrayList;

    invoke-direct {v6}, Ljava/util/ArrayList;-><init>()V

    .line 710
    .local v6, "visitorIDs":Ljava/util/List;, "Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;"
    invoke-interface {v0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v11

    :cond_2
    :goto_0
    invoke-interface {v11}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_0

    invoke-interface {v11}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 711
    .local v1, "customerIdString":Ljava/lang/String;
    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v7

    if-lez v7, :cond_2

    .line 712
    const-string v7, "="

    invoke-virtual {v1, v7}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v7

    invoke-static {v7}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v5

    .line 713
    .local v5, "internalId":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    const/4 v7, 0x1

    invoke-interface {v5, v7}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    const-string v8, "%01"

    invoke-virtual {v7, v8}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v7

    invoke-static {v7}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v4

    .line 714
    .local v4, "idinfo":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    invoke-interface {v5}, Ljava/util/List;->size()I

    move-result v7

    const/4 v8, 0x2

    if-ne v7, v8, :cond_0

    invoke-interface {v4}, Ljava/util/List;->size()I

    move-result v7

    const/4 v8, 0x3

    if-ne v7, v8, :cond_0

    .line 719
    :try_start_0
    new-instance v3, Lcom/adobe/mobile/VisitorID;

    const/4 v7, 0x0

    invoke-interface {v5, v7}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v7

    check-cast v7, Ljava/lang/String;

    const/4 v8, 0x0

    invoke-interface {v4, v8}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v8

    check-cast v8, Ljava/lang/String;

    const/4 v9, 0x1

    invoke-interface {v4, v9}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/lang/String;

    invoke-static {}, Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;->values()[Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    move-result-object v12

    const/4 v10, 0x2

    invoke-interface {v4, v10}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v10

    check-cast v10, Ljava/lang/String;

    invoke-static {v10}, Ljava/lang/Integer;->parseInt(Ljava/lang/String;)I

    move-result v10

    aget-object v10, v12, v10

    invoke-direct {v3, v7, v8, v9, v10}, Lcom/adobe/mobile/VisitorID;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V

    .line 720
    .local v3, "id":Lcom/adobe/mobile/VisitorID;
    invoke-interface {v6, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Ljava/lang/NumberFormatException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_1

    goto :goto_0

    .line 722
    .end local v3    # "id":Lcom/adobe/mobile/VisitorID;
    :catch_0
    move-exception v2

    .line 723
    .local v2, "ex":Ljava/lang/NumberFormatException;
    const-string v7, "ID Service - Unable to parse visitor ID: (%s) exception:(%s)"

    const/4 v8, 0x2

    new-array v8, v8, [Ljava/lang/Object;

    const/4 v9, 0x0

    aput-object p1, v8, v9

    const/4 v9, 0x1

    invoke-virtual {v2}, Ljava/lang/NumberFormatException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v8, v9

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 724
    .end local v2    # "ex":Ljava/lang/NumberFormatException;
    :catch_1
    move-exception v2

    .line 725
    .local v2, "ex":Ljava/lang/IllegalStateException;
    const-string v7, "ID Service - Unable to create ID after encoding:(%s)"

    const/4 v8, 0x1

    new-array v8, v8, [Ljava/lang/Object;

    const/4 v9, 0x0

    invoke-virtual {v2}, Ljava/lang/IllegalStateException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v8, v9

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0
.end method

.method static synthetic access$000(Lcom/adobe/mobile/VisitorIDService;)Ljava/util/List;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-object v0, p0, Lcom/adobe/mobile/VisitorIDService;->_customerIds:Ljava/util/List;

    return-object v0
.end method

.method static synthetic access$002(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/util/List;
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/util/List;

    .prologue
    .line 31
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService;->_customerIds:Ljava/util/List;

    return-object p1
.end method

.method static synthetic access$100(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/util/List;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 31
    invoke-direct {p0, p1}, Lcom/adobe/mobile/VisitorIDService;->_parseIdString(Ljava/lang/String;)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$1000(Lcom/adobe/mobile/VisitorIDService;)J
    .locals 2
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-wide v0, p0, Lcom/adobe/mobile/VisitorIDService;->_lastSync:J

    return-wide v0
.end method

.method static synthetic access$1002(Lcom/adobe/mobile/VisitorIDService;J)J
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # J

    .prologue
    .line 31
    iput-wide p1, p0, Lcom/adobe/mobile/VisitorIDService;->_lastSync:J

    return-wide p1
.end method

.method static synthetic access$1100(Lcom/adobe/mobile/VisitorIDService;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)Ljava/util/List;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/util/Map;
    .param p2, "x2"    # Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    .prologue
    .line 31
    invoke-direct {p0, p1, p2}, Lcom/adobe/mobile/VisitorIDService;->_generateCustomerIds(Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$1200(Lcom/adobe/mobile/VisitorIDService;Ljava/util/Map;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/util/Map;

    .prologue
    .line 31
    invoke-direct {p0, p1}, Lcom/adobe/mobile/VisitorIDService;->_generateInternalIdString(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$1300(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    invoke-direct {p0}, Lcom/adobe/mobile/VisitorIDService;->_generateMID()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$1400(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/util/List;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/util/List;

    .prologue
    .line 31
    invoke-direct {p0, p1}, Lcom/adobe/mobile/VisitorIDService;->_mergeCustomerIds(Ljava/util/List;)Ljava/util/List;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$1500(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/util/List;

    .prologue
    .line 31
    invoke-direct {p0, p1}, Lcom/adobe/mobile/VisitorIDService;->_generateStoredCustomerIdString(Ljava/util/List;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$200(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-object v0, p0, Lcom/adobe/mobile/VisitorIDService;->_analyticsIdString:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$202(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 31
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService;->_analyticsIdString:Ljava/lang/String;

    return-object p1
.end method

.method static synthetic access$300(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/util/List;

    .prologue
    .line 31
    invoke-direct {p0, p1}, Lcom/adobe/mobile/VisitorIDService;->_generateAnalyticsCustomerIdString(Ljava/util/List;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$400(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-object v0, p0, Lcom/adobe/mobile/VisitorIDService;->_aamIdString:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$402(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 31
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService;->_aamIdString:Ljava/lang/String;

    return-object p1
.end method

.method static synthetic access$500(Lcom/adobe/mobile/VisitorIDService;Ljava/util/List;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/util/List;

    .prologue
    .line 31
    invoke-direct {p0, p1}, Lcom/adobe/mobile/VisitorIDService;->_generateCustomerIdString(Ljava/util/List;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$600(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-object v0, p0, Lcom/adobe/mobile/VisitorIDService;->_marketingCloudID:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$602(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 31
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService;->_marketingCloudID:Ljava/lang/String;

    return-object p1
.end method

.method static synthetic access$700(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-object v0, p0, Lcom/adobe/mobile/VisitorIDService;->_locationHint:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$702(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 31
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService;->_locationHint:Ljava/lang/String;

    return-object p1
.end method

.method static synthetic access$800(Lcom/adobe/mobile/VisitorIDService;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-object v0, p0, Lcom/adobe/mobile/VisitorIDService;->_blob:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$802(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 31
    iput-object p1, p0, Lcom/adobe/mobile/VisitorIDService;->_blob:Ljava/lang/String;

    return-object p1
.end method

.method static synthetic access$900(Lcom/adobe/mobile/VisitorIDService;)J
    .locals 2
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;

    .prologue
    .line 31
    iget-wide v0, p0, Lcom/adobe/mobile/VisitorIDService;->_ttl:J

    return-wide v0
.end method

.method static synthetic access$902(Lcom/adobe/mobile/VisitorIDService;J)J
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/VisitorIDService;
    .param p1, "x1"    # J

    .prologue
    .line 31
    iput-wide p1, p0, Lcom/adobe/mobile/VisitorIDService;->_ttl:J

    return-wide p1
.end method

.method public static sharedInstance()Lcom/adobe/mobile/VisitorIDService;
    .locals 2

    .prologue
    .line 70
    sget-object v1, Lcom/adobe/mobile/VisitorIDService;->_instanceMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 71
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/VisitorIDService;->_instance:Lcom/adobe/mobile/VisitorIDService;

    if-nez v0, :cond_0

    .line 72
    new-instance v0, Lcom/adobe/mobile/VisitorIDService;

    invoke-direct {v0}, Lcom/adobe/mobile/VisitorIDService;-><init>()V

    sput-object v0, Lcom/adobe/mobile/VisitorIDService;->_instance:Lcom/adobe/mobile/VisitorIDService;

    .line 75
    :cond_0
    sget-object v0, Lcom/adobe/mobile/VisitorIDService;->_instance:Lcom/adobe/mobile/VisitorIDService;

    monitor-exit v1

    return-object v0

    .line 76
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method


# virtual methods
.method protected final getAAMParameterString()Ljava/lang/String;
    .locals 7

    .prologue
    .line 470
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    .line 471
    .local v2, "returnValue":Ljava/lang/StringBuilder;
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v3, Lcom/adobe/mobile/VisitorIDService$9;

    invoke-direct {v3, p0, v2}, Lcom/adobe/mobile/VisitorIDService$9;-><init>(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/StringBuilder;)V

    invoke-direct {v1, v3}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 505
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/Void;>;"
    iget-object v3, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v3, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 508
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 513
    :goto_0
    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    return-object v3

    .line 509
    :catch_0
    move-exception v0

    .line 510
    .local v0, "ex":Ljava/lang/Exception;
    const-string v3, "ID Service - Unable to retrieve audience manager parameters from queue(%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected final getAnalyticsIDRequestParameterString()Ljava/lang/String;
    .locals 7

    .prologue
    .line 348
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    .line 350
    .local v2, "returnValue":Ljava/lang/StringBuilder;
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v3, Lcom/adobe/mobile/VisitorIDService$5;

    invoke-direct {v3, p0, v2}, Lcom/adobe/mobile/VisitorIDService$5;-><init>(Lcom/adobe/mobile/VisitorIDService;Ljava/lang/StringBuilder;)V

    invoke-direct {v1, v3}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 369
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/Void;>;"
    iget-object v3, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v3, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 372
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 377
    :goto_0
    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    return-object v3

    .line 373
    :catch_0
    move-exception v0

    .line 374
    .local v0, "ex":Ljava/lang/Exception;
    const-string v3, "ID Service - Unable to retrieve analytics id parameters from queue(%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected final getAnalyticsIdString()Ljava/lang/String;
    .locals 6

    .prologue
    .line 417
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/VisitorIDService$7;

    invoke-direct {v2, p0}, Lcom/adobe/mobile/VisitorIDService$7;-><init>(Lcom/adobe/mobile/VisitorIDService;)V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 424
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v2, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 427
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 430
    :goto_0
    return-object v2

    .line 428
    :catch_0
    move-exception v0

    .line 429
    .local v0, "ex":Ljava/lang/Exception;
    const-string v2, "ID Service - Unable to retrieve analytics id string from queue(%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 430
    const-string v2, ""

    goto :goto_0
.end method

.method protected final getAnalyticsIdVisitorParameters()Ljava/util/Map;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 435
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    .line 436
    .local v0, "analyticsIdVisitorParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v2, Ljava/util/concurrent/FutureTask;

    new-instance v3, Lcom/adobe/mobile/VisitorIDService$8;

    invoke-direct {v3, p0, v0}, Lcom/adobe/mobile/VisitorIDService$8;-><init>(Lcom/adobe/mobile/VisitorIDService;Ljava/util/Map;)V

    invoke-direct {v2, v3}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 458
    .local v2, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/Void;>;"
    iget-object v3, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v3, v2}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 461
    :try_start_0
    invoke-virtual {v2}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 466
    :goto_0
    return-object v0

    .line 462
    :catch_0
    move-exception v1

    .line 463
    .local v1, "ex":Ljava/lang/Exception;
    const-string v3, "ID Service - Unable to retrieve analytics parameters from queue(%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected final getAnalyticsParameters()Ljava/util/Map;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation

    .prologue
    .line 381
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 382
    .local v2, "returnValue":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v3, Lcom/adobe/mobile/VisitorIDService$6;

    invoke-direct {v3, p0, v2}, Lcom/adobe/mobile/VisitorIDService$6;-><init>(Lcom/adobe/mobile/VisitorIDService;Ljava/util/Map;)V

    invoke-direct {v1, v3}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 404
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/Void;>;"
    iget-object v3, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v3, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 407
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 412
    :goto_0
    return-object v2

    .line 408
    :catch_0
    move-exception v0

    .line 409
    .local v0, "ex":Ljava/lang/Exception;
    const-string v3, "ID Service - Unable to retrieve analytics parameters from queue(%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected final getIdentifiers()Ljava/util/List;
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List",
            "<",
            "Lcom/adobe/mobile/VisitorID;",
            ">;"
        }
    .end annotation

    .prologue
    .line 330
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/VisitorIDService$4;

    invoke-direct {v2, p0}, Lcom/adobe/mobile/VisitorIDService$4;-><init>(Lcom/adobe/mobile/VisitorIDService;)V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 337
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/util/List<Lcom/adobe/mobile/VisitorID;>;>;"
    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v2, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 340
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/List;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 343
    :goto_0
    return-object v2

    .line 341
    :catch_0
    move-exception v0

    .line 342
    .local v0, "ex":Ljava/lang/Exception;
    const-string v2, "ID Service - Unable to retrieve marketing cloud identifiers from queue(%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 343
    const/4 v2, 0x0

    goto :goto_0
.end method

.method protected final getMarketingCloudID()Ljava/lang/String;
    .locals 6

    .prologue
    .line 312
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/VisitorIDService$3;

    invoke-direct {v2, p0}, Lcom/adobe/mobile/VisitorIDService$3;-><init>(Lcom/adobe/mobile/VisitorIDService;)V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 319
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v2, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 322
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 325
    :goto_0
    return-object v2

    .line 323
    :catch_0
    move-exception v0

    .line 324
    .local v0, "ex":Ljava/lang/Exception;
    const-string v2, "ID Service - Unable to retrieve marketing cloud id from queue(%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 325
    const/4 v2, 0x0

    goto :goto_0
.end method

.method protected final getTargetParameterMap()Ljava/util/HashMap;
    .locals 9
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
    .line 518
    const/4 v4, 0x0

    .line 520
    .local v4, "targetParameters":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v3, Ljava/util/concurrent/FutureTask;

    new-instance v5, Lcom/adobe/mobile/VisitorIDService$10;

    invoke-direct {v5, p0}, Lcom/adobe/mobile/VisitorIDService$10;-><init>(Lcom/adobe/mobile/VisitorIDService;)V

    invoke-direct {v3, v5}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 542
    .local v3, "targetParamTask":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;>;"
    iget-object v5, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v5, v3}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 545
    :try_start_0
    invoke-virtual {v3}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v5

    move-object v0, v5

    check-cast v0, Ljava/util/HashMap;

    move-object v4, v0
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 550
    :goto_0
    if-eqz v4, :cond_0

    .line 551
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAID()Ljava/lang/String;

    move-result-object v1

    .line 552
    .local v1, "aid":Ljava/lang/String;
    if-eqz v1, :cond_0

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v5

    if-lez v5, :cond_0

    .line 553
    const-string v5, "mboxMCAVID"

    invoke-virtual {v4, v5, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 557
    .end local v1    # "aid":Ljava/lang/String;
    :cond_0
    return-object v4

    .line 546
    :catch_0
    move-exception v2

    .line 547
    .local v2, "ex":Ljava/lang/Exception;
    const-string v5, "ID Service - Unable to retrieve target parameters from queue(%s)"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-virtual {v2}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected idSync(Ljava/util/Map;)V
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 134
    .local p1, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const/4 v0, 0x0

    sget-object v1, Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;->VISITOR_ID_AUTHENTICATION_STATE_UNKNOWN:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    const/4 v2, 0x0

    invoke-virtual {p0, p1, v0, v1, v2}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;Z)V

    .line 135
    return-void
.end method

.method protected idSync(Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V
    .locals 2
    .param p2, "authenticationState"    # Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;",
            ")V"
        }
    .end annotation

    .prologue
    .line 125
    .local p1, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const/4 v0, 0x0

    const/4 v1, 0x0

    invoke-virtual {p0, p1, v0, p2, v1}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;Z)V

    .line 126
    return-void
.end method

.method protected idSync(Ljava/util/Map;Ljava/util/Map;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 130
    .local p1, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    .local p2, "dpids":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    sget-object v0, Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;->VISITOR_ID_AUTHENTICATION_STATE_UNKNOWN:Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;

    const/4 v1, 0x0

    invoke-virtual {p0, p1, p2, v0, v1}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;Z)V

    .line 131
    return-void
.end method

.method protected idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V
    .locals 1
    .param p3, "authenticationState"    # Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;",
            ")V"
        }
    .end annotation

    .prologue
    .line 140
    .local p1, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    .local p2, "dpids":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const/4 v0, 0x0

    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;Z)V

    .line 141
    return-void
.end method

.method protected idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;Z)V
    .locals 7
    .param p3, "authenticationState"    # Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;
    .param p4, "forceResync"    # Z
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;",
            "Z)V"
        }
    .end annotation

    .prologue
    .local p1, "identifiers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    .local p2, "dpids":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const/4 v4, 0x0

    .line 147
    if-eqz p1, :cond_1

    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3, p1}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 148
    .local v3, "identifiersCopy":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    :goto_0
    if-eqz p2, :cond_0

    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 150
    .local v4, "dpidsCopy":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    :cond_0
    iget-object v6, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    new-instance v0, Lcom/adobe/mobile/VisitorIDService$2;

    move-object v1, p0

    move v2, p4

    move-object v5, p3

    invoke-direct/range {v0 .. v5}, Lcom/adobe/mobile/VisitorIDService$2;-><init>(Lcom/adobe/mobile/VisitorIDService;ZLjava/util/HashMap;Ljava/util/HashMap;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;)V

    invoke-interface {v6, v0}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 289
    return-void

    .end local v3    # "identifiersCopy":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    .end local v4    # "dpidsCopy":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    :cond_1
    move-object v3, v4

    .line 147
    goto :goto_0
.end method

.method protected final parseResponse([B)Lorg/json/JSONObject;
    .locals 7
    .param p1, "response"    # [B

    .prologue
    const/4 v5, 0x1

    const/4 v6, 0x0

    const/4 v2, 0x0

    .line 292
    if-nez p1, :cond_0

    move-object v1, v2

    .line 307
    :goto_0
    return-object v1

    .line 299
    :cond_0
    :try_start_0
    new-instance v1, Lorg/json/JSONObject;

    new-instance v3, Ljava/lang/String;

    const-string v4, "UTF-8"

    invoke-direct {v3, p1, v4}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    invoke-direct {v1, v3}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    .local v1, "responseObject":Lorg/json/JSONObject;
    goto :goto_0

    .line 300
    .end local v1    # "responseObject":Lorg/json/JSONObject;
    :catch_0
    move-exception v0

    .line 301
    .local v0, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v3, "ID Service - Unable to decode response(%s)"

    new-array v4, v5, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v1, v2

    .line 302
    goto :goto_0

    .line 303
    .end local v0    # "ex":Ljava/io/UnsupportedEncodingException;
    :catch_1
    move-exception v0

    .line 304
    .local v0, "ex":Lorg/json/JSONException;
    const-string v3, "ID Service - Unable to parse response(%s)"

    new-array v4, v5, [Ljava/lang/Object;

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v1, v2

    .line 305
    goto :goto_0
.end method

.method protected resetVariablesFromSharedPreferences()V
    .locals 6

    .prologue
    .line 90
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/VisitorIDService$1;

    invoke-direct {v2, p0}, Lcom/adobe/mobile/VisitorIDService$1;-><init>(Lcom/adobe/mobile/VisitorIDService;)V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 113
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/Void;>;"
    iget-object v2, p0, Lcom/adobe/mobile/VisitorIDService;->_visitorIDExector:Ljava/util/concurrent/Executor;

    invoke-interface {v2, v1}, Ljava/util/concurrent/Executor;->execute(Ljava/lang/Runnable;)V

    .line 116
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 121
    :goto_0
    return-void

    .line 117
    :catch_0
    move-exception v0

    .line 118
    .local v0, "ex":Ljava/lang/Exception;
    const-string v2, "ID Service - Unable to initialize visitor ID variables(%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method
