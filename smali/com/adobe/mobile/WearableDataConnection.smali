.class final Lcom/adobe/mobile/WearableDataConnection;
.super Ljava/lang/Object;
.source "WearableDataConnection.java"

# interfaces
.implements Lcom/google/android/gms/common/api/GoogleApiClient$OnConnectionFailedListener;
.implements Lcom/google/android/gms/wearable/DataApi$DataListener;


# instance fields
.field protected mDataMap:Lcom/google/android/gms/wearable/DataMap;

.field private final mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

.field private mInTimeCountDownLatch:Ljava/util/concurrent/CountDownLatch;

.field protected requestID:Ljava/lang/String;


# direct methods
.method protected constructor <init>(Landroid/content/Context;)V
    .locals 2
    .param p1, "context"    # Landroid/content/Context;

    .prologue
    .line 48
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 49
    new-instance v0, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    invoke-direct {v0, p1}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;-><init>(Landroid/content/Context;)V

    .line 50
    invoke-virtual {v0, p0}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->addOnConnectionFailedListener(Lcom/google/android/gms/common/api/GoogleApiClient$OnConnectionFailedListener;)Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    move-result-object v0

    sget-object v1, Lcom/google/android/gms/wearable/Wearable;->API:Lcom/google/android/gms/common/api/Api;

    .line 51
    invoke-virtual {v0, v1}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->addApi(Lcom/google/android/gms/common/api/Api;)Lcom/google/android/gms/common/api/GoogleApiClient$Builder;

    move-result-object v0

    .line 52
    invoke-virtual {v0}, Lcom/google/android/gms/common/api/GoogleApiClient$Builder;->build()Lcom/google/android/gms/common/api/GoogleApiClient;

    move-result-object v0

    iput-object v0, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    .line 53
    return-void
.end method

.method private connect(I)Z
    .locals 3
    .param p1, "timeOut"    # I

    .prologue
    const/4 v0, 0x0

    .line 96
    iget-object v1, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v1}, Lcom/adobe/mobile/GoogleApiClientWrapper;->connect(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    .line 97
    invoke-direct {p0, p1}, Lcom/adobe/mobile/WearableDataConnection;->waitForConnect(I)Z

    move-result v1

    if-nez v1, :cond_0

    .line 98
    const-string v1, "Wearable - Timeout setup connection"

    new-array v2, v0, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 106
    :goto_0
    return v0

    .line 101
    :cond_0
    invoke-direct {p0}, Lcom/adobe/mobile/WearableDataConnection;->hasNodes()Z

    move-result v1

    if-nez v1, :cond_1

    .line 102
    const-string v1, "Wearable - No connected Node found"

    new-array v2, v0, [Ljava/lang/Object;

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 106
    :cond_1
    const/4 v0, 0x1

    goto :goto_0
.end method

.method private hasNodes()Z
    .locals 5

    .prologue
    .line 148
    sget-object v3, Lcom/google/android/gms/wearable/Wearable;->NodeApi:Lcom/google/android/gms/wearable/NodeApi;

    iget-object v4, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v3, v4}, Lcom/google/android/gms/wearable/NodeApi;->getConnectedNodes(Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/google/android/gms/common/api/PendingResult;

    move-result-object v2

    .line 149
    .local v2, "result":Lcom/google/android/gms/common/api/PendingResult;, "Lcom/google/android/gms/common/api/PendingResult<Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;>;"
    invoke-static {v2}, Lcom/adobe/mobile/GoogleApiClientWrapper;->await(Lcom/google/android/gms/common/api/PendingResult;)Lcom/google/android/gms/common/api/Result;

    move-result-object v0

    .line 150
    .local v0, "awaitResult":Lcom/google/android/gms/common/api/Result;
    instance-of v3, v0, Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;

    if-eqz v3, :cond_0

    check-cast v0, Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;

    .end local v0    # "awaitResult":Lcom/google/android/gms/common/api/Result;
    move-object v1, v0

    .line 151
    .local v1, "nodes":Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;
    :goto_0
    if-eqz v1, :cond_1

    invoke-interface {v1}, Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;->getNodes()Ljava/util/List;

    move-result-object v3

    if-eqz v3, :cond_1

    invoke-interface {v1}, Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;->getNodes()Ljava/util/List;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/List;->size()I

    move-result v3

    if-lez v3, :cond_1

    const/4 v3, 0x1

    :goto_1
    return v3

    .line 150
    .end local v1    # "nodes":Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;
    .restart local v0    # "awaitResult":Lcom/google/android/gms/common/api/Result;
    :cond_0
    const/4 v1, 0x0

    goto :goto_0

    .line 151
    .end local v0    # "awaitResult":Lcom/google/android/gms/common/api/Result;
    .restart local v1    # "nodes":Lcom/google/android/gms/wearable/NodeApi$GetConnectedNodesResult;
    :cond_1
    const/4 v3, 0x0

    goto :goto_1
.end method

.method private waitForConnect(I)Z
    .locals 6
    .param p1, "timeOut"    # I

    .prologue
    const/4 v1, 0x1

    .line 139
    iget-object v2, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v2}, Lcom/adobe/mobile/GoogleApiClientWrapper;->isConnected(Lcom/google/android/gms/common/api/GoogleApiClient;)Ljava/lang/Boolean;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v2

    if-eqz v2, :cond_1

    .line 144
    :cond_0
    :goto_0
    return v1

    .line 142
    :cond_1
    iget-object v2, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    int-to-long v4, p1

    sget-object v3, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {v2, v4, v5, v3}, Lcom/adobe/mobile/GoogleApiClientWrapper;->blockingConnect(Lcom/google/android/gms/common/api/GoogleApiClient;JLjava/util/concurrent/TimeUnit;)Lcom/google/android/gms/common/ConnectionResult;

    move-result-object v0

    .line 144
    .local v0, "connectionResult":Lcom/google/android/gms/common/ConnectionResult;
    if-eqz v0, :cond_2

    invoke-virtual {v0}, Lcom/google/android/gms/common/ConnectionResult;->isSuccess()Z

    move-result v2

    if-nez v2, :cond_0

    :cond_2
    const/4 v1, 0x0

    goto :goto_0
.end method


# virtual methods
.method protected getResponse()Lcom/adobe/mobile/WearableDataResponse;
    .locals 2

    .prologue
    .line 155
    iget-object v0, p0, Lcom/adobe/mobile/WearableDataConnection;->mDataMap:Lcom/google/android/gms/wearable/DataMap;

    if-nez v0, :cond_0

    .line 156
    const/4 v0, 0x0

    .line 158
    :goto_0
    return-object v0

    :cond_0
    iget-object v0, p0, Lcom/adobe/mobile/WearableDataConnection;->mDataMap:Lcom/google/android/gms/wearable/DataMap;

    iget-object v1, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v0, v1}, Lcom/adobe/mobile/WearableDataResponse;->createResponseFromDataMap(Lcom/google/android/gms/wearable/DataMap;Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/adobe/mobile/WearableDataResponse;

    move-result-object v0

    goto :goto_0
.end method

.method public onConnectionFailed(Lcom/google/android/gms/common/ConnectionResult;)V
    .locals 0
    .param p1, "result"    # Lcom/google/android/gms/common/ConnectionResult;

    .prologue
    .line 164
    return-void
.end method

.method public onDataChanged(Lcom/google/android/gms/wearable/DataEventBuffer;)V
    .locals 7
    .param p1, "dataEvents"    # Lcom/google/android/gms/wearable/DataEventBuffer;

    .prologue
    .line 112
    invoke-virtual {p1}, Lcom/google/android/gms/wearable/DataEventBuffer;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v5

    if-eqz v5, :cond_1

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Lcom/google/android/gms/wearable/DataEvent;

    .line 113
    .local v0, "event":Lcom/google/android/gms/wearable/DataEvent;
    invoke-interface {v0}, Lcom/google/android/gms/wearable/DataEvent;->getType()I

    move-result v5

    const/4 v6, 0x1

    if-eq v5, v6, :cond_2

    .line 136
    .end local v0    # "event":Lcom/google/android/gms/wearable/DataEvent;
    :cond_1
    :goto_0
    return-void

    .line 116
    .restart local v0    # "event":Lcom/google/android/gms/wearable/DataEvent;
    :cond_2
    invoke-interface {v0}, Lcom/google/android/gms/wearable/DataEvent;->getDataItem()Lcom/google/android/gms/wearable/DataItem;

    move-result-object v1

    .line 118
    .local v1, "item":Lcom/google/android/gms/wearable/DataItem;
    if-eqz v1, :cond_0

    .line 121
    invoke-interface {v1}, Lcom/google/android/gms/wearable/DataItem;->getUri()Landroid/net/Uri;

    move-result-object v3

    .line 122
    .local v3, "uri":Landroid/net/Uri;
    if-eqz v3, :cond_0

    invoke-virtual {v3}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object v5

    if-eqz v5, :cond_0

    invoke-virtual {v3}, Landroid/net/Uri;->getPath()Ljava/lang/String;

    move-result-object v5

    const-string v6, "/abdmobile/data/response"

    invoke-virtual {v5, v6}, Ljava/lang/String;->compareTo(Ljava/lang/String;)I

    move-result v5

    if-nez v5, :cond_0

    .line 126
    invoke-interface {v0}, Lcom/google/android/gms/wearable/DataEvent;->getDataItem()Lcom/google/android/gms/wearable/DataItem;

    move-result-object v5

    invoke-static {v5}, Lcom/google/android/gms/wearable/DataMapItem;->fromDataItem(Lcom/google/android/gms/wearable/DataItem;)Lcom/google/android/gms/wearable/DataMapItem;

    move-result-object v5

    invoke-virtual {v5}, Lcom/google/android/gms/wearable/DataMapItem;->getDataMap()Lcom/google/android/gms/wearable/DataMap;

    move-result-object v2

    .line 127
    .local v2, "returnedDataMap":Lcom/google/android/gms/wearable/DataMap;
    if-eqz v2, :cond_0

    .line 130
    const-string v5, "ID"

    invoke-virtual {v2, v5}, Lcom/google/android/gms/wearable/DataMap;->containsKey(Ljava/lang/String;)Z

    move-result v5

    if-eqz v5, :cond_0

    const-string v5, "ID"

    invoke-virtual {v2, v5}, Lcom/google/android/gms/wearable/DataMap;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    iget-object v6, p0, Lcom/adobe/mobile/WearableDataConnection;->requestID:Ljava/lang/String;

    invoke-virtual {v5, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_0

    .line 131
    iput-object v2, p0, Lcom/adobe/mobile/WearableDataConnection;->mDataMap:Lcom/google/android/gms/wearable/DataMap;

    .line 132
    iget-object v4, p0, Lcom/adobe/mobile/WearableDataConnection;->mInTimeCountDownLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {v4}, Ljava/util/concurrent/CountDownLatch;->countDown()V

    goto :goto_0
.end method

.method protected send(Lcom/adobe/mobile/WearableDataRequest;)Lcom/adobe/mobile/WearableDataResponse;
    .locals 9
    .param p1, "wearableDataRequest"    # Lcom/adobe/mobile/WearableDataRequest;

    .prologue
    const/4 v4, 0x0

    .line 56
    invoke-virtual {p1}, Lcom/adobe/mobile/WearableDataRequest;->getTimeOut()I

    move-result v5

    invoke-direct {p0, v5}, Lcom/adobe/mobile/WearableDataConnection;->connect(I)Z

    move-result v5

    if-nez v5, :cond_0

    .line 91
    :goto_0
    return-object v4

    .line 60
    :cond_0
    sget-object v5, Lcom/google/android/gms/wearable/Wearable;->DataApi:Lcom/google/android/gms/wearable/DataApi;

    iget-object v6, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v5, v6, p0}, Lcom/google/android/gms/wearable/DataApi;->addListener(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/wearable/DataApi$DataListener;)Lcom/google/android/gms/common/api/PendingResult;

    .line 62
    invoke-virtual {p1}, Lcom/adobe/mobile/WearableDataRequest;->getUUID()Ljava/lang/String;

    move-result-object v5

    iput-object v5, p0, Lcom/adobe/mobile/WearableDataConnection;->requestID:Ljava/lang/String;

    .line 64
    const-string v5, "/abdmobile/data/request"

    invoke-static {v5}, Lcom/google/android/gms/wearable/PutDataMapRequest;->create(Ljava/lang/String;)Lcom/google/android/gms/wearable/PutDataMapRequest;

    move-result-object v1

    .line 65
    .local v1, "dataMapRequest":Lcom/google/android/gms/wearable/PutDataMapRequest;
    invoke-virtual {v1}, Lcom/google/android/gms/wearable/PutDataMapRequest;->getDataMap()Lcom/google/android/gms/wearable/DataMap;

    move-result-object v0

    .line 67
    .local v0, "dataMap":Lcom/google/android/gms/wearable/DataMap;
    invoke-virtual {p1}, Lcom/adobe/mobile/WearableDataRequest;->getDataMap()Lcom/google/android/gms/wearable/DataMap;

    move-result-object v5

    invoke-virtual {v0, v5}, Lcom/google/android/gms/wearable/DataMap;->putAll(Lcom/google/android/gms/wearable/DataMap;)V

    .line 68
    invoke-virtual {v1}, Lcom/google/android/gms/wearable/PutDataMapRequest;->asPutDataRequest()Lcom/google/android/gms/wearable/PutDataRequest;

    move-result-object v3

    .line 71
    .local v3, "putDataRequest":Lcom/google/android/gms/wearable/PutDataRequest;
    new-instance v5, Ljava/util/concurrent/CountDownLatch;

    const/4 v6, 0x1

    invoke-direct {v5, v6}, Ljava/util/concurrent/CountDownLatch;-><init>(I)V

    iput-object v5, p0, Lcom/adobe/mobile/WearableDataConnection;->mInTimeCountDownLatch:Ljava/util/concurrent/CountDownLatch;

    .line 72
    sget-object v5, Lcom/google/android/gms/wearable/Wearable;->DataApi:Lcom/google/android/gms/wearable/DataApi;

    iget-object v6, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v5, v6, v3}, Lcom/google/android/gms/wearable/DataApi;->putDataItem(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/wearable/PutDataRequest;)Lcom/google/android/gms/common/api/PendingResult;

    .line 78
    :try_start_0
    iget-object v5, p0, Lcom/adobe/mobile/WearableDataConnection;->mInTimeCountDownLatch:Ljava/util/concurrent/CountDownLatch;

    invoke-virtual {p1}, Lcom/adobe/mobile/WearableDataRequest;->getTimeOut()I

    move-result v6

    int-to-long v6, v6

    sget-object v8, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-virtual {v5, v6, v7, v8}, Ljava/util/concurrent/CountDownLatch;->await(JLjava/util/concurrent/TimeUnit;)Z

    move-result v5

    if-nez v5, :cond_1

    .line 79
    const-string v5, "Wearable - Failed to get data from handheld app"

    const/4 v6, 0x0

    new-array v6, v6, [Ljava/lang/Object;

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/InterruptedException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 88
    sget-object v5, Lcom/google/android/gms/wearable/Wearable;->DataApi:Lcom/google/android/gms/wearable/DataApi;

    iget-object v6, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v5, v6, p0}, Lcom/google/android/gms/wearable/DataApi;->removeListener(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/wearable/DataApi$DataListener;)Lcom/google/android/gms/common/api/PendingResult;

    .line 89
    iget-object v5, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v5}, Lcom/adobe/mobile/GoogleApiClientWrapper;->disconnect(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    goto :goto_0

    .line 82
    :cond_1
    :try_start_1
    invoke-virtual {p0}, Lcom/adobe/mobile/WearableDataConnection;->getResponse()Lcom/adobe/mobile/WearableDataResponse;
    :try_end_1
    .catch Ljava/lang/InterruptedException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-result-object v4

    .line 88
    .local v4, "response":Lcom/adobe/mobile/WearableDataResponse;
    sget-object v5, Lcom/google/android/gms/wearable/Wearable;->DataApi:Lcom/google/android/gms/wearable/DataApi;

    iget-object v6, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v5, v6, p0}, Lcom/google/android/gms/wearable/DataApi;->removeListener(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/wearable/DataApi$DataListener;)Lcom/google/android/gms/common/api/PendingResult;

    .line 89
    iget-object v5, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v5}, Lcom/adobe/mobile/GoogleApiClientWrapper;->disconnect(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    goto :goto_0

    .line 84
    .end local v4    # "response":Lcom/adobe/mobile/WearableDataResponse;
    :catch_0
    move-exception v2

    .line 85
    .local v2, "e":Ljava/lang/InterruptedException;
    :try_start_2
    const-string v5, "Wearable - Failed to get data from handheld app"

    const/4 v6, 0x0

    new-array v6, v6, [Ljava/lang/Object;

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 86
    const/4 v4, 0x0

    .line 88
    .restart local v4    # "response":Lcom/adobe/mobile/WearableDataResponse;
    sget-object v5, Lcom/google/android/gms/wearable/Wearable;->DataApi:Lcom/google/android/gms/wearable/DataApi;

    iget-object v6, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v5, v6, p0}, Lcom/google/android/gms/wearable/DataApi;->removeListener(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/wearable/DataApi$DataListener;)Lcom/google/android/gms/common/api/PendingResult;

    .line 89
    iget-object v5, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v5}, Lcom/adobe/mobile/GoogleApiClientWrapper;->disconnect(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    goto :goto_0

    .line 88
    .end local v2    # "e":Ljava/lang/InterruptedException;
    .end local v4    # "response":Lcom/adobe/mobile/WearableDataResponse;
    :catchall_0
    move-exception v5

    sget-object v6, Lcom/google/android/gms/wearable/Wearable;->DataApi:Lcom/google/android/gms/wearable/DataApi;

    iget-object v7, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-interface {v6, v7, p0}, Lcom/google/android/gms/wearable/DataApi;->removeListener(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/wearable/DataApi$DataListener;)Lcom/google/android/gms/common/api/PendingResult;

    .line 89
    iget-object v6, p0, Lcom/adobe/mobile/WearableDataConnection;->mGoogleApiClient:Lcom/google/android/gms/common/api/GoogleApiClient;

    invoke-static {v6}, Lcom/adobe/mobile/GoogleApiClientWrapper;->disconnect(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    throw v5
.end method
