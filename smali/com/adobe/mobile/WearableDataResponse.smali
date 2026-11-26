.class Lcom/adobe/mobile/WearableDataResponse;
.super Ljava/lang/Object;
.source "WearableDataResponse.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/WearableDataResponse$CacheResponse;,
        Lcom/adobe/mobile/WearableDataResponse$ShareConfigResponse;,
        Lcom/adobe/mobile/WearableDataResponse$ThirdPartyResponse;,
        Lcom/adobe/mobile/WearableDataResponse$PostResponse;,
        Lcom/adobe/mobile/WearableDataResponse$GetResponse;
    }
.end annotation


# instance fields
.field protected success:Z


# direct methods
.method constructor <init>()V
    .locals 1

    .prologue
    .line 38
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 40
    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/adobe/mobile/WearableDataResponse;->success:Z

    .line 87
    return-void
.end method

.method static synthetic access$000(Lcom/google/android/gms/wearable/Asset;Ljava/lang/String;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 0
    .param p0, "x0"    # Lcom/google/android/gms/wearable/Asset;
    .param p1, "x1"    # Ljava/lang/String;
    .param p2, "x2"    # Lcom/google/android/gms/common/api/GoogleApiClient;

    .prologue
    .line 38
    invoke-static {p0, p1, p2}, Lcom/adobe/mobile/WearableDataResponse;->saveFileFromAsset(Lcom/google/android/gms/wearable/Asset;Ljava/lang/String;Lcom/google/android/gms/common/api/GoogleApiClient;)V

    return-void
.end method

.method protected static createResponseFromDataMap(Lcom/google/android/gms/wearable/DataMap;Lcom/google/android/gms/common/api/GoogleApiClient;)Lcom/adobe/mobile/WearableDataResponse;
    .locals 3
    .param p0, "dataMap"    # Lcom/google/android/gms/wearable/DataMap;
    .param p1, "mGoogleApiClient"    # Lcom/google/android/gms/common/api/GoogleApiClient;

    .prologue
    const/4 v0, 0x0

    .line 163
    const-string v1, "Type"

    invoke-virtual {p0, v1}, Lcom/google/android/gms/wearable/DataMap;->containsKey(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_1

    .line 181
    :cond_0
    :goto_0
    return-object v0

    .line 166
    :cond_1
    const-string v1, "Type"

    invoke-virtual {p0, v1}, Lcom/google/android/gms/wearable/DataMap;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "POST"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_2

    .line 167
    new-instance v0, Lcom/adobe/mobile/WearableDataResponse$PostResponse;

    invoke-direct {v0, p0}, Lcom/adobe/mobile/WearableDataResponse$PostResponse;-><init>(Lcom/google/android/gms/wearable/DataMap;)V

    goto :goto_0

    .line 169
    :cond_2
    const-string v1, "Type"

    invoke-virtual {p0, v1}, Lcom/google/android/gms/wearable/DataMap;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "GET"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_3

    .line 170
    new-instance v0, Lcom/adobe/mobile/WearableDataResponse$GetResponse;

    invoke-direct {v0, p0}, Lcom/adobe/mobile/WearableDataResponse$GetResponse;-><init>(Lcom/google/android/gms/wearable/DataMap;)V

    goto :goto_0

    .line 172
    :cond_3
    const-string v1, "Type"

    invoke-virtual {p0, v1}, Lcom/google/android/gms/wearable/DataMap;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "Config"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_4

    .line 173
    new-instance v0, Lcom/adobe/mobile/WearableDataResponse$ShareConfigResponse;

    invoke-direct {v0, p0}, Lcom/adobe/mobile/WearableDataResponse$ShareConfigResponse;-><init>(Lcom/google/android/gms/wearable/DataMap;)V

    goto :goto_0

    .line 175
    :cond_4
    const-string v1, "Type"

    invoke-virtual {p0, v1}, Lcom/google/android/gms/wearable/DataMap;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "File"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_5

    .line 176
    new-instance v0, Lcom/adobe/mobile/WearableDataResponse$CacheResponse;

    invoke-direct {v0, p0, p1}, Lcom/adobe/mobile/WearableDataResponse$CacheResponse;-><init>(Lcom/google/android/gms/wearable/DataMap;Lcom/google/android/gms/common/api/GoogleApiClient;)V

    goto :goto_0

    .line 178
    :cond_5
    const-string v1, "Type"

    invoke-virtual {p0, v1}, Lcom/google/android/gms/wearable/DataMap;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    const-string v2, "ThirdParty"

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_0

    .line 179
    new-instance v0, Lcom/adobe/mobile/WearableDataResponse$ThirdPartyResponse;

    invoke-direct {v0, p0}, Lcom/adobe/mobile/WearableDataResponse$ThirdPartyResponse;-><init>(Lcom/google/android/gms/wearable/DataMap;)V

    goto :goto_0
.end method

.method private static saveFileFromAsset(Lcom/google/android/gms/wearable/Asset;Ljava/lang/String;Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 13
    .param p0, "asset"    # Lcom/google/android/gms/wearable/Asset;
    .param p1, "fileName"    # Ljava/lang/String;
    .param p2, "mGoogleApiClient"    # Lcom/google/android/gms/common/api/GoogleApiClient;

    .prologue
    .line 120
    if-eqz p0, :cond_0

    if-nez p2, :cond_1

    .line 160
    :cond_0
    :goto_0
    return-void

    .line 123
    :cond_1
    const-wide/16 v10, 0x3a98

    sget-object v12, Ljava/util/concurrent/TimeUnit;->MILLISECONDS:Ljava/util/concurrent/TimeUnit;

    invoke-static {p2, v10, v11, v12}, Lcom/adobe/mobile/GoogleApiClientWrapper;->blockingConnect(Lcom/google/android/gms/common/api/GoogleApiClient;JLjava/util/concurrent/TimeUnit;)Lcom/google/android/gms/common/ConnectionResult;

    move-result-object v8

    .line 125
    .local v8, "result":Lcom/google/android/gms/common/ConnectionResult;
    if-eqz v8, :cond_0

    invoke-virtual {v8}, Lcom/google/android/gms/common/ConnectionResult;->isSuccess()Z

    move-result v10

    if-eqz v10, :cond_0

    .line 129
    sget-object v10, Lcom/google/android/gms/wearable/Wearable;->DataApi:Lcom/google/android/gms/wearable/DataApi;

    invoke-interface {v10, p2, p0}, Lcom/google/android/gms/wearable/DataApi;->getFdForAsset(Lcom/google/android/gms/common/api/GoogleApiClient;Lcom/google/android/gms/wearable/Asset;)Lcom/google/android/gms/common/api/PendingResult;

    move-result-object v7

    .line 130
    .local v7, "pendingResult":Lcom/google/android/gms/common/api/PendingResult;, "Lcom/google/android/gms/common/api/PendingResult<Lcom/google/android/gms/wearable/DataApi$GetFdForAssetResult;>;"
    invoke-static {v7}, Lcom/adobe/mobile/GoogleApiClientWrapper;->await(Lcom/google/android/gms/common/api/PendingResult;)Lcom/google/android/gms/common/api/Result;

    move-result-object v1

    .line 131
    .local v1, "awaitResult":Lcom/google/android/gms/common/api/Result;
    instance-of v10, v1, Lcom/google/android/gms/wearable/DataApi$GetFdForAssetResult;

    if-eqz v10, :cond_2

    check-cast v1, Lcom/google/android/gms/wearable/DataApi$GetFdForAssetResult;

    .end local v1    # "awaitResult":Lcom/google/android/gms/common/api/Result;
    invoke-interface {v1}, Lcom/google/android/gms/wearable/DataApi$GetFdForAssetResult;->getInputStream()Ljava/io/InputStream;

    move-result-object v0

    .line 133
    .local v0, "assetInputStream":Ljava/io/InputStream;
    :goto_1
    invoke-static {p2}, Lcom/adobe/mobile/GoogleApiClientWrapper;->disconnect(Lcom/google/android/gms/common/api/GoogleApiClient;)V

    .line 136
    if-eqz v0, :cond_0

    .line 140
    new-instance v9, Ljava/io/File;

    invoke-direct {v9, p1}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 141
    .local v9, "targetFile":Ljava/io/File;
    const/4 v5, 0x0

    .line 143
    .local v5, "outStream":Ljava/io/OutputStream;
    :try_start_0
    new-instance v6, Ljava/io/FileOutputStream;

    invoke-direct {v6, v9}, Ljava/io/FileOutputStream;-><init>(Ljava/io/File;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_4
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 144
    .end local v5    # "outStream":Ljava/io/OutputStream;
    .local v6, "outStream":Ljava/io/OutputStream;
    const/16 v10, 0x2000

    :try_start_1
    new-array v2, v10, [B

    .line 146
    .local v2, "buffer":[B
    :goto_2
    invoke-virtual {v0, v2}, Ljava/io/InputStream;->read([B)I

    move-result v3

    .local v3, "bytesRead":I
    const/4 v10, -0x1

    if-eq v3, v10, :cond_3

    .line 147
    const/4 v10, 0x0

    invoke-virtual {v6, v2, v10, v3}, Ljava/io/OutputStream;->write([BII)V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    goto :goto_2

    .line 149
    .end local v2    # "buffer":[B
    .end local v3    # "bytesRead":I
    :catch_0
    move-exception v4

    move-object v5, v6

    .line 150
    .end local v6    # "outStream":Ljava/io/OutputStream;
    .local v4, "e":Ljava/io/IOException;
    .restart local v5    # "outStream":Ljava/io/OutputStream;
    :goto_3
    :try_start_2
    const-string v10, "Wearable - Failed to save cache file"

    const/4 v11, 0x0

    new-array v11, v11, [Ljava/lang/Object;

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 152
    if-eqz v5, :cond_0

    .line 154
    :try_start_3
    invoke-virtual {v5}, Ljava/io/OutputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_1

    goto :goto_0

    .line 155
    :catch_1
    move-exception v4

    .line 156
    const-string v10, "Wearable - Failed to close file output stream"

    const/4 v11, 0x0

    new-array v11, v11, [Ljava/lang/Object;

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 131
    .end local v0    # "assetInputStream":Ljava/io/InputStream;
    .end local v4    # "e":Ljava/io/IOException;
    .end local v5    # "outStream":Ljava/io/OutputStream;
    .end local v9    # "targetFile":Ljava/io/File;
    .restart local v1    # "awaitResult":Lcom/google/android/gms/common/api/Result;
    :cond_2
    const/4 v0, 0x0

    goto :goto_1

    .line 152
    .end local v1    # "awaitResult":Lcom/google/android/gms/common/api/Result;
    .restart local v0    # "assetInputStream":Ljava/io/InputStream;
    .restart local v2    # "buffer":[B
    .restart local v3    # "bytesRead":I
    .restart local v6    # "outStream":Ljava/io/OutputStream;
    .restart local v9    # "targetFile":Ljava/io/File;
    :cond_3
    if-eqz v6, :cond_5

    .line 154
    :try_start_4
    invoke-virtual {v6}, Ljava/io/OutputStream;->close()V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_2

    move-object v5, v6

    .line 157
    .end local v6    # "outStream":Ljava/io/OutputStream;
    .restart local v5    # "outStream":Ljava/io/OutputStream;
    goto :goto_0

    .line 155
    .end local v5    # "outStream":Ljava/io/OutputStream;
    .restart local v6    # "outStream":Ljava/io/OutputStream;
    :catch_2
    move-exception v4

    .line 156
    .restart local v4    # "e":Ljava/io/IOException;
    const-string v10, "Wearable - Failed to close file output stream"

    const/4 v11, 0x0

    new-array v11, v11, [Ljava/lang/Object;

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v5, v6

    .line 157
    .end local v6    # "outStream":Ljava/io/OutputStream;
    .restart local v5    # "outStream":Ljava/io/OutputStream;
    goto :goto_0

    .line 152
    .end local v2    # "buffer":[B
    .end local v3    # "bytesRead":I
    .end local v4    # "e":Ljava/io/IOException;
    :catchall_0
    move-exception v10

    :goto_4
    if-eqz v5, :cond_4

    .line 154
    :try_start_5
    invoke-virtual {v5}, Ljava/io/OutputStream;->close()V
    :try_end_5
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_3

    .line 157
    :cond_4
    :goto_5
    throw v10

    .line 155
    :catch_3
    move-exception v4

    .line 156
    .restart local v4    # "e":Ljava/io/IOException;
    const-string v11, "Wearable - Failed to close file output stream"

    const/4 v12, 0x0

    new-array v12, v12, [Ljava/lang/Object;

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_5

    .line 152
    .end local v4    # "e":Ljava/io/IOException;
    .end local v5    # "outStream":Ljava/io/OutputStream;
    .restart local v6    # "outStream":Ljava/io/OutputStream;
    :catchall_1
    move-exception v10

    move-object v5, v6

    .end local v6    # "outStream":Ljava/io/OutputStream;
    .restart local v5    # "outStream":Ljava/io/OutputStream;
    goto :goto_4

    .line 149
    :catch_4
    move-exception v4

    goto :goto_3

    .end local v5    # "outStream":Ljava/io/OutputStream;
    .restart local v2    # "buffer":[B
    .restart local v3    # "bytesRead":I
    .restart local v6    # "outStream":Ljava/io/OutputStream;
    :cond_5
    move-object v5, v6

    .end local v6    # "outStream":Ljava/io/OutputStream;
    .restart local v5    # "outStream":Ljava/io/OutputStream;
    goto/16 :goto_0
.end method


# virtual methods
.method protected isSuccess()Z
    .locals 1

    .prologue
    .line 42
    iget-boolean v0, p0, Lcom/adobe/mobile/WearableDataResponse;->success:Z

    return v0
.end method
