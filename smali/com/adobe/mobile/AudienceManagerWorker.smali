.class final Lcom/adobe/mobile/AudienceManagerWorker;
.super Ljava/lang/Object;
.source "AudienceManagerWorker.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;
    }
.end annotation


# static fields
.field private static final AUDIENCE_MANAGER_CUSTOMER_DATA_PREFIX:Ljava/lang/String; = "c_"

.field private static final AUDIENCE_MANAGER_DATA_PROVIDER_ID_KEY:Ljava/lang/String; = "d_dpid"

.field private static final AUDIENCE_MANAGER_DATA_PROVIDER_USER_ID_KEY:Ljava/lang/String; = "d_dpuuid"

.field private static final AUDIENCE_MANAGER_JSON_COOKIE_NAME_KEY:Ljava/lang/String; = "cn"

.field private static final AUDIENCE_MANAGER_JSON_COOKIE_VALUE_KEY:Ljava/lang/String; = "cv"

.field private static final AUDIENCE_MANAGER_JSON_DESTS_KEY:Ljava/lang/String; = "dests"

.field private static final AUDIENCE_MANAGER_JSON_STUFF_KEY:Ljava/lang/String; = "stuff"

.field private static final AUDIENCE_MANAGER_JSON_URL_KEY:Ljava/lang/String; = "c"

.field private static final AUDIENCE_MANAGER_JSON_USER_ID_KEY:Ljava/lang/String; = "uuid"

.field private static final AUDIENCE_MANAGER_SHARED_PREFS_PROFILE_KEY:Ljava/lang/String; = "AAMUserProfile"

.field private static final AUDIENCE_MANAGER_SHARED_PREFS_USER_ID_KEY:Ljava/lang/String; = "AAMUserId"

.field private static final AUDIENCE_MANAGER_URL_SUFFIX:Ljava/lang/String; = "&d_ptfm=android&d_dst=1&d_rtbd=json"

.field private static final AUDIENCE_MANAGER_USER_ID_KEY:Ljava/lang/String; = "d_uuid"

.field private static final TO_MILLI:I = 0x3e8

.field private static volatile UrlPrefix_pred:Z

.field private static volatile VisitorProfile_pred:Z

.field private static _dpid:Ljava/lang/String;

.field private static _dpuuid:Ljava/lang/String;

.field private static _urlPrefix:Ljava/lang/String;

.field private static _visitorProfile:Ljava/util/HashMap;
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


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .prologue
    const/4 v1, 0x1

    const/4 v0, 0x0

    .line 43
    sput-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_dpid:Ljava/lang/String;

    .line 44
    sput-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_dpuuid:Ljava/lang/String;

    .line 45
    sput-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_visitorProfile:Ljava/util/HashMap;

    .line 50
    sput-boolean v1, Lcom/adobe/mobile/AudienceManagerWorker;->VisitorProfile_pred:Z

    .line 429
    sput-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_urlPrefix:Ljava/lang/String;

    .line 430
    sput-boolean v1, Lcom/adobe/mobile/AudienceManagerWorker;->UrlPrefix_pred:Z

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .prologue
    .line 14
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 135
    return-void
.end method

.method private static BuildSchemaRequest(Ljava/util/Map;)Ljava/lang/String;
    .locals 3
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 271
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/AudienceManagerWorker;->GetUrlPrefix()Ljava/lang/String;

    move-result-object v1

    if-nez v1, :cond_0

    .line 272
    const/4 v1, 0x0

    .line 276
    :goto_0
    return-object v1

    .line 275
    :cond_0
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Lcom/adobe/mobile/AudienceManagerWorker;->GetUrlPrefix()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-static {p0}, Lcom/adobe/mobile/AudienceManagerWorker;->GetCustomUrlVariables(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-static {}, Lcom/adobe/mobile/AudienceManagerWorker;->GetDataProviderUrlVariables()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "&d_ptfm=android&d_dst=1&d_rtbd=json"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 276
    .local v0, "urlString":Ljava/lang/String;
    const-string v1, "?&"

    const-string v2, "?"

    invoke-virtual {v0, v1, v2}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v1

    goto :goto_0
.end method

.method private static GetCustomUrlVariables(Ljava/util/Map;)Ljava/lang/String;
    .locals 7
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 280
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p0, :cond_0

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v4

    if-gtz v4, :cond_1

    .line 281
    :cond_0
    const-string v4, ""

    .line 304
    :goto_0
    return-object v4

    .line 284
    :cond_1
    new-instance v2, Ljava/lang/StringBuilder;

    const/16 v4, 0x400

    invoke-direct {v2, v4}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 286
    .local v2, "urlVars":Ljava/lang/StringBuilder;
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_2
    :goto_1
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_3

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 287
    .local v0, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 288
    .local v1, "key":Ljava/lang/String;
    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    .line 291
    .local v3, "value":Ljava/lang/Object;
    if-eqz v1, :cond_2

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v5

    if-lez v5, :cond_2

    if-eqz v3, :cond_2

    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v5

    if-lez v5, :cond_2

    .line 295
    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v5}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v5

    const-class v6, Ljava/lang/String;

    if-ne v5, v6, :cond_2

    .line 296
    const-string v5, "&"

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    const-string v6, "c_"

    .line 297
    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    .line 298
    invoke-static {v1}, Lcom/adobe/mobile/AudienceManagerWorker;->SanitizeUrlVariableName(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    const-string v6, "="

    .line 299
    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v5

    .line 300
    invoke-virtual {v3}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_1

    .line 304
    .end local v0    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v1    # "key":Ljava/lang/String;
    .end local v3    # "value":Ljava/lang/Object;
    :cond_3
    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    goto :goto_0
.end method

.method private static GetDataProviderUrlVariables()Ljava/lang/String;
    .locals 3

    .prologue
    .line 314
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 317
    .local v0, "urlVars":Ljava/lang/StringBuilder;
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v1

    invoke-virtual {v1}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 318
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v1

    invoke-virtual {v1}, Lcom/adobe/mobile/VisitorIDService;->getAAMParameterString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 322
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/AudienceManagerWorker;->GetUUID()Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_1

    .line 323
    const-string v1, "&"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "d_uuid"

    .line 324
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "="

    .line 325
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    .line 326
    invoke-static {}, Lcom/adobe/mobile/AudienceManagerWorker;->GetUUID()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 330
    :cond_1
    sget-object v1, Lcom/adobe/mobile/AudienceManagerWorker;->_dpid:Ljava/lang/String;

    if-eqz v1, :cond_2

    sget-object v1, Lcom/adobe/mobile/AudienceManagerWorker;->_dpid:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    if-lez v1, :cond_2

    sget-object v1, Lcom/adobe/mobile/AudienceManagerWorker;->_dpuuid:Ljava/lang/String;

    if-eqz v1, :cond_2

    sget-object v1, Lcom/adobe/mobile/AudienceManagerWorker;->_dpuuid:Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v1

    if-lez v1, :cond_2

    .line 331
    const-string v1, "&"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "d_dpid"

    .line 332
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "="

    .line 333
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    sget-object v2, Lcom/adobe/mobile/AudienceManagerWorker;->_dpid:Ljava/lang/String;

    .line 334
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "&"

    .line 335
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "d_dpuuid"

    .line 336
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "="

    .line 337
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    sget-object v2, Lcom/adobe/mobile/AudienceManagerWorker;->_dpuuid:Ljava/lang/String;

    .line 338
    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 341
    :cond_2
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    return-object v1
.end method

.method public static GetDpid()Ljava/lang/String;
    .locals 6

    .prologue
    .line 108
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/AudienceManagerWorker$3;

    invoke-direct {v2}, Lcom/adobe/mobile/AudienceManagerWorker$3;-><init>()V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 115
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 118
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 121
    :goto_0
    return-object v2

    .line 119
    :catch_0
    move-exception v0

    .line 120
    .local v0, "e":Ljava/lang/Exception;
    const-string v2, "Audience Manager - Unable to get Dpid (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 121
    const/4 v2, 0x0

    goto :goto_0
.end method

.method public static GetDpuuid()Ljava/lang/String;
    .locals 6

    .prologue
    .line 90
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/AudienceManagerWorker$2;

    invoke-direct {v2}, Lcom/adobe/mobile/AudienceManagerWorker$2;-><init>()V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 97
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/lang/String;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 100
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 103
    :goto_0
    return-object v2

    .line 101
    :catch_0
    move-exception v0

    .line 102
    .local v0, "e":Ljava/lang/Exception;
    const-string v2, "Audience Manager - Unable to get Dpid (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 103
    const/4 v2, 0x0

    goto :goto_0
.end method

.method private static GetUUID()Ljava/lang/String;
    .locals 6

    .prologue
    const/4 v1, 0x0

    .line 361
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v3, "AAMUserId"

    const/4 v4, 0x0

    invoke-interface {v2, v3, v4}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v1

    .line 366
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :goto_0
    return-object v1

    .line 362
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_0
    move-exception v0

    .line 363
    .restart local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Audience Manager - Error getting uuid from shared preferences (%s)."

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private static GetUrlPrefix()Ljava/lang/String;
    .locals 4

    .prologue
    const/4 v3, 0x0

    .line 432
    sget-boolean v0, Lcom/adobe/mobile/AudienceManagerWorker;->UrlPrefix_pred:Z

    if-eqz v0, :cond_0

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v0

    invoke-virtual {v0}, Lcom/adobe/mobile/MobileConfig;->mobileUsingAudienceManager()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 433
    sput-boolean v3, Lcom/adobe/mobile/AudienceManagerWorker;->UrlPrefix_pred:Z

    .line 434
    const-string v1, "%s://%s/event?"

    const/4 v0, 0x2

    new-array v2, v0, [Ljava/lang/Object;

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v0

    invoke-virtual {v0}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, "https"

    :goto_0
    aput-object v0, v2, v3

    const/4 v0, 0x1

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getAamServer()Ljava/lang/String;

    move-result-object v3

    aput-object v3, v2, v0

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_urlPrefix:Ljava/lang/String;

    .line 437
    :cond_0
    sget-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_urlPrefix:Ljava/lang/String;

    return-object v0

    .line 434
    :cond_1
    const-string v0, "http"

    goto :goto_0
.end method

.method public static GetVisitorProfile()Ljava/util/HashMap;
    .locals 6
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
    .line 52
    new-instance v1, Ljava/util/concurrent/FutureTask;

    new-instance v2, Lcom/adobe/mobile/AudienceManagerWorker$1;

    invoke-direct {v2}, Lcom/adobe/mobile/AudienceManagerWorker$1;-><init>()V

    invoke-direct {v1, v2}, Ljava/util/concurrent/FutureTask;-><init>(Ljava/util/concurrent/Callable;)V

    .line 79
    .local v1, "f":Ljava/util/concurrent/FutureTask;, "Ljava/util/concurrent/FutureTask<Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    invoke-interface {v2, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 82
    :try_start_0
    invoke-virtual {v1}, Ljava/util/concurrent/FutureTask;->get()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/HashMap;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 85
    :goto_0
    return-object v2

    .line 83
    :catch_0
    move-exception v0

    .line 84
    .local v0, "e":Ljava/lang/Exception;
    const-string v2, "Audience Manager - Unable to retrieve Visitor Profile"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 85
    const/4 v2, 0x0

    goto :goto_0
.end method

.method private static ProcessDestsArray(Lorg/json/JSONObject;)V
    .locals 9
    .param p0, "jsonResponse"    # Lorg/json/JSONObject;

    .prologue
    .line 393
    :try_start_0
    const-string v5, "dests"

    invoke-virtual {p0, v5}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v1

    .line 394
    .local v1, "dests":Lorg/json/JSONArray;
    const/4 v3, 0x0

    .local v3, "i":I
    :goto_0
    invoke-virtual {v1}, Lorg/json/JSONArray;->length()I

    move-result v5

    if-ge v3, v5, :cond_1

    .line 395
    invoke-virtual {v1, v3}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v0

    .line 396
    .local v0, "dest":Lorg/json/JSONObject;
    const-string v5, "c"

    invoke-virtual {v0, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 397
    .local v4, "url":Ljava/lang/String;
    if-eqz v4, :cond_0

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v5

    if-lez v5, :cond_0

    .line 398
    const/4 v5, 0x0

    const/16 v6, 0x1388

    const-string v7, "Audience Manager"

    invoke-static {v4, v5, v6, v7}, Lcom/adobe/mobile/RequestHandler;->sendGenericRequest(Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 394
    :cond_0
    add-int/lit8 v3, v3, 0x1

    goto :goto_0

    .line 402
    .end local v0    # "dest":Lorg/json/JSONObject;
    .end local v1    # "dests":Lorg/json/JSONArray;
    .end local v3    # "i":I
    .end local v4    # "url":Ljava/lang/String;
    :catch_0
    move-exception v2

    .line 403
    .local v2, "ex":Lorg/json/JSONException;
    const-string v5, "Audience Manager - No destination in response (%s)"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-virtual {v2}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 405
    .end local v2    # "ex":Lorg/json/JSONException;
    :cond_1
    return-void
.end method

.method private static ProcessStuffArray(Lorg/json/JSONObject;)Ljava/util/HashMap;
    .locals 9
    .param p0, "jsonResponse"    # Lorg/json/JSONObject;
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
    .line 408
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 411
    .local v2, "returnedMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_0
    const-string v5, "stuff"

    invoke-virtual {p0, v5}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v4

    .line 414
    .local v4, "stuffArray":Lorg/json/JSONArray;
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    invoke-virtual {v4}, Lorg/json/JSONArray;->length()I

    move-result v5

    if-ge v1, v5, :cond_1

    .line 415
    invoke-virtual {v4, v1}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v3

    .line 416
    .local v3, "stuff":Lorg/json/JSONObject;
    if-eqz v3, :cond_0

    .line 417
    const-string v5, "cn"

    invoke-virtual {v3, v5}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, "cv"

    .line 418
    invoke-virtual {v3, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 417
    invoke-virtual {v2, v5, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 414
    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 422
    .end local v1    # "i":I
    .end local v3    # "stuff":Lorg/json/JSONObject;
    .end local v4    # "stuffArray":Lorg/json/JSONArray;
    :catch_0
    move-exception v0

    .line 423
    .local v0, "ex":Lorg/json/JSONException;
    const-string v5, "Audience Manager - No \'stuff\' array in response (%s)"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 426
    .end local v0    # "ex":Lorg/json/JSONException;
    :cond_1
    return-object v2
.end method

.method public static Reset()V
    .locals 2

    .prologue
    .line 226
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/AudienceManagerWorker$5;

    invoke-direct {v1}, Lcom/adobe/mobile/AudienceManagerWorker$5;-><init>()V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 233
    return-void
.end method

.method private static SanitizeUrlVariableName(Ljava/lang/String;)Ljava/lang/String;
    .locals 2
    .param p0, "name"    # Ljava/lang/String;

    .prologue
    .line 309
    const-string v0, "."

    const-string v1, "_"

    invoke-virtual {p0, v0, v1}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method public static SetDpidAndDpuuid(Ljava/lang/String;Ljava/lang/String;)V
    .locals 2
    .param p0, "newDpid"    # Ljava/lang/String;
    .param p1, "newDpuuid"    # Ljava/lang/String;

    .prologue
    .line 126
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/AudienceManagerWorker$4;

    invoke-direct {v1, p0, p1}, Lcom/adobe/mobile/AudienceManagerWorker$4;-><init>(Ljava/lang/String;Ljava/lang/String;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 133
    return-void
.end method

.method private static SetUUID(Ljava/lang/String;)V
    .locals 6
    .param p0, "newUuid"    # Ljava/lang/String;

    .prologue
    .line 346
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 347
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    if-nez p0, :cond_0

    .line 348
    const-string v2, "AAMUserId"

    invoke-interface {v1, v2}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 353
    :goto_0
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 357
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_1
    return-void

    .line 350
    .restart local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_0
    const-string v2, "AAMUserId"

    invoke-interface {v1, v2, p0}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 354
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v0

    .line 355
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Audience Manager - Error updating uuid in shared preferences (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method private static SetVisitorProfile(Ljava/util/Map;)V
    .locals 7
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
    .local p0, "profile":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v6, 0x0

    .line 371
    sput-boolean v6, Lcom/adobe/mobile/AudienceManagerWorker;->VisitorProfile_pred:Z

    .line 373
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 374
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    if-eqz p0, :cond_0

    .line 375
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, p0}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 376
    .local v2, "profileJSON":Lorg/json/JSONObject;
    const-string v3, "AAMUserProfile"

    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-interface {v1, v3, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 377
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3, p0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    sput-object v3, Lcom/adobe/mobile/AudienceManagerWorker;->_visitorProfile:Ljava/util/HashMap;

    .line 383
    .end local v2    # "profileJSON":Lorg/json/JSONObject;
    :goto_0
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 388
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_1
    return-void

    .line 379
    .restart local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_0
    const-string v3, "AAMUserProfile"

    invoke-interface {v1, v3}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 380
    const/4 v3, 0x0

    sput-object v3, Lcom/adobe/mobile/AudienceManagerWorker;->_visitorProfile:Ljava/util/HashMap;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 385
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v0

    .line 386
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v3, "Audience Manager - Error updating visitor profile (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method public static SubmitSignal(Ljava/util/Map;Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;)V
    .locals 2
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback",
            "<",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;>;)V"
        }
    .end annotation

    .prologue
    .line 212
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p1, "callback":Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;, "Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback<Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;>;"
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v0

    invoke-virtual {v0}, Lcom/adobe/mobile/MobileConfig;->getPrivacyStatus()Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v0

    sget-object v1, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_OUT:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-ne v0, v1, :cond_1

    .line 213
    const-string v0, "Audience Manager - Ignoring signal due to privacy status being opted out"

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v0, v1}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 215
    if-eqz p1, :cond_0

    .line 216
    const/4 v0, 0x0

    invoke-interface {p1, v0}, Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;->call(Ljava/lang/Object;)V

    .line 223
    :cond_0
    :goto_0
    return-void

    .line 222
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;

    invoke-direct {v1, p0, p1}, Lcom/adobe/mobile/AudienceManagerWorker$SubmitSignalTask;-><init>(Ljava/util/Map;Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    goto :goto_0
.end method

.method static synthetic access$000()Z
    .locals 1

    .prologue
    .line 14
    sget-boolean v0, Lcom/adobe/mobile/AudienceManagerWorker;->VisitorProfile_pred:Z

    return v0
.end method

.method static synthetic access$002(Z)Z
    .locals 0
    .param p0, "x0"    # Z

    .prologue
    .line 14
    sput-boolean p0, Lcom/adobe/mobile/AudienceManagerWorker;->VisitorProfile_pred:Z

    return p0
.end method

.method static synthetic access$100()Ljava/util/HashMap;
    .locals 1

    .prologue
    .line 14
    sget-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_visitorProfile:Ljava/util/HashMap;

    return-object v0
.end method

.method static synthetic access$102(Ljava/util/HashMap;)Ljava/util/HashMap;
    .locals 0
    .param p0, "x0"    # Ljava/util/HashMap;

    .prologue
    .line 14
    sput-object p0, Lcom/adobe/mobile/AudienceManagerWorker;->_visitorProfile:Ljava/util/HashMap;

    return-object p0
.end method

.method static synthetic access$200()Ljava/lang/String;
    .locals 1

    .prologue
    .line 14
    sget-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_dpuuid:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$202(Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p0, "x0"    # Ljava/lang/String;

    .prologue
    .line 14
    sput-object p0, Lcom/adobe/mobile/AudienceManagerWorker;->_dpuuid:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$300()Ljava/lang/String;
    .locals 1

    .prologue
    .line 14
    sget-object v0, Lcom/adobe/mobile/AudienceManagerWorker;->_dpid:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$302(Ljava/lang/String;)Ljava/lang/String;
    .locals 0
    .param p0, "x0"    # Ljava/lang/String;

    .prologue
    .line 14
    sput-object p0, Lcom/adobe/mobile/AudienceManagerWorker;->_dpid:Ljava/lang/String;

    return-object p0
.end method

.method static synthetic access$400(Ljava/util/Map;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Ljava/util/Map;

    .prologue
    .line 14
    invoke-static {p0}, Lcom/adobe/mobile/AudienceManagerWorker;->BuildSchemaRequest(Ljava/util/Map;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$500(Ljava/lang/String;)V
    .locals 0
    .param p0, "x0"    # Ljava/lang/String;

    .prologue
    .line 14
    invoke-static {p0}, Lcom/adobe/mobile/AudienceManagerWorker;->SetUUID(Ljava/lang/String;)V

    return-void
.end method

.method static synthetic access$600(Ljava/util/Map;)V
    .locals 0
    .param p0, "x0"    # Ljava/util/Map;

    .prologue
    .line 14
    invoke-static {p0}, Lcom/adobe/mobile/AudienceManagerWorker;->SetVisitorProfile(Ljava/util/Map;)V

    return-void
.end method

.method protected static processJsonResponse(Lorg/json/JSONObject;)Ljava/util/HashMap;
    .locals 7
    .param p0, "response"    # Lorg/json/JSONObject;
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
    const/4 v6, 0x1

    const/4 v5, 0x0

    .line 239
    invoke-static {p0}, Lcom/adobe/mobile/AudienceManagerWorker;->ProcessDestsArray(Lorg/json/JSONObject;)V

    .line 242
    :try_start_0
    const-string v2, "uuid"

    invoke-virtual {p0, v2}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Lcom/adobe/mobile/AudienceManagerWorker;->SetUUID(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 248
    :goto_0
    invoke-static {p0}, Lcom/adobe/mobile/AudienceManagerWorker;->ProcessStuffArray(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object v1

    .line 251
    .local v1, "returnedMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-virtual {v1}, Ljava/util/HashMap;->size()I

    move-result v2

    if-gtz v2, :cond_0

    .line 252
    const-string v2, "Audience Manager - response was empty"

    new-array v3, v5, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 253
    const/4 v1, 0x0

    .line 261
    .end local v1    # "returnedMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :goto_1
    return-object v1

    .line 243
    :catch_0
    move-exception v0

    .line 244
    .local v0, "e":Lorg/json/JSONException;
    const-string v2, "Audience Manager - Unable to parse JSON data (%s)"

    new-array v3, v6, [Ljava/lang/Object;

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v5

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 256
    .end local v0    # "e":Lorg/json/JSONException;
    .restart local v1    # "returnedMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_0
    const-string v2, "Audience Manager - response (%s)"

    new-array v3, v6, [Ljava/lang/Object;

    aput-object v1, v3, v5

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 259
    invoke-static {v1}, Lcom/adobe/mobile/AudienceManagerWorker;->SetVisitorProfile(Ljava/util/Map;)V

    goto :goto_1
.end method
