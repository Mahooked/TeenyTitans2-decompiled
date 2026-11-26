.class final Lcom/adobe/mobile/GoogleApiClientWrapper;
.super Ljava/lang/Object;
.source "GoogleApiClientWrapper.java"


# direct methods
.method constructor <init>()V
    .locals 0

    .prologue
    .line 30
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method protected static await(Lcom/google/android/gms/common/api/PendingResult;)Lcom/google/android/gms/common/api/Result;
    .locals 7
    .param p0, "pendingResult"    # Lcom/google/android/gms/common/api/PendingResult;

    .prologue
    const/4 v6, 0x0

    .line 78
    :try_start_0
    const-class v3, Lcom/google/android/gms/common/api/PendingResult;

    const-string v4, "await"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Class;

    invoke-virtual {v3, v4, v5}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    .line 79
    .local v0, "awaitMethod":Ljava/lang/reflect/Method;
    const/4 v3, 0x0

    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {v0, p0, v3}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    .line 80
    .local v2, "result":Ljava/lang/Object;
    instance-of v3, v2, Lcom/google/android/gms/common/api/Result;

    if-eqz v3, :cond_0

    .line 81
    check-cast v2, Lcom/google/android/gms/common/api/Result;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 86
    .end local v0    # "awaitMethod":Ljava/lang/reflect/Method;
    .end local v2    # "result":Ljava/lang/Object;
    :goto_0
    return-object v2

    .line 83
    :catch_0
    move-exception v1

    .line 84
    .local v1, "e":Ljava/lang/Exception;
    const-string v3, "Wearable - Unable to call PendingResult.await() method (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 86
    .end local v1    # "e":Ljava/lang/Exception;
    :cond_0
    const/4 v2, 0x0

    goto :goto_0
.end method

.method protected static blockingConnect(Lcom/google/android/gms/common/api/GoogleApiClient;JLjava/util/concurrent/TimeUnit;)Lcom/google/android/gms/common/ConnectionResult;
    .locals 11
    .param p0, "client"    # Lcom/google/android/gms/common/api/GoogleApiClient;
    .param p1, "timeout"    # J
    .param p3, "timeunit"    # Ljava/util/concurrent/TimeUnit;

    .prologue
    const/4 v9, 0x1

    const/4 v8, 0x0

    .line 65
    :try_start_0
    const-class v3, Lcom/google/android/gms/common/api/GoogleApiClient;

    const-string v4, "blockingConnect"

    const/4 v5, 0x2

    new-array v5, v5, [Ljava/lang/Class;

    const/4 v6, 0x0

    sget-object v7, Ljava/lang/Long;->TYPE:Ljava/lang/Class;

    aput-object v7, v5, v6

    const/4 v6, 0x1

    const-class v7, Ljava/util/concurrent/TimeUnit;

    aput-object v7, v5, v6

    invoke-virtual {v3, v4, v5}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    .line 66
    .local v0, "blockingConnectMethod":Ljava/lang/reflect/Method;
    const/4 v3, 0x2

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v5

    aput-object v5, v3, v4

    const/4 v4, 0x1

    aput-object p3, v3, v4

    invoke-virtual {v0, p0, v3}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    .line 67
    .local v2, "result":Ljava/lang/Object;
    instance-of v3, v2, Lcom/google/android/gms/common/ConnectionResult;

    if-eqz v3, :cond_0

    .line 68
    check-cast v2, Lcom/google/android/gms/common/ConnectionResult;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 73
    .end local v0    # "blockingConnectMethod":Ljava/lang/reflect/Method;
    .end local v2    # "result":Ljava/lang/Object;
    :goto_0
    return-object v2

    .line 70
    :catch_0
    move-exception v1

    .line 71
    .local v1, "e":Ljava/lang/Exception;
    const-string v3, "Wearable - Unable to call GoogleApiClient.blockingConnect() method (%s)"

    new-array v4, v9, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v8

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 73
    .end local v1    # "e":Ljava/lang/Exception;
    :cond_0
    const/4 v2, 0x0

    goto :goto_0
.end method

.method protected static connect(Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 6
    .param p0, "client"    # Lcom/google/android/gms/common/api/GoogleApiClient;

    .prologue
    const/4 v5, 0x0

    .line 43
    :try_start_0
    const-class v2, Lcom/google/android/gms/common/api/GoogleApiClient;

    const-string v3, "connect"

    const/4 v4, 0x0

    new-array v4, v4, [Ljava/lang/Class;

    invoke-virtual {v2, v3, v4}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    .line 44
    .local v0, "connectMethod":Ljava/lang/reflect/Method;
    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-virtual {v0, p0, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 48
    .end local v0    # "connectMethod":Ljava/lang/reflect/Method;
    :goto_0
    return-void

    .line 45
    :catch_0
    move-exception v1

    .line 46
    .local v1, "e":Ljava/lang/Exception;
    const-string v2, "Wearable - Unable to call GoogleApiClient.connect() method (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v5

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static disconnect(Lcom/google/android/gms/common/api/GoogleApiClient;)V
    .locals 6
    .param p0, "client"    # Lcom/google/android/gms/common/api/GoogleApiClient;

    .prologue
    const/4 v5, 0x0

    .line 34
    :try_start_0
    const-class v2, Lcom/google/android/gms/common/api/GoogleApiClient;

    const-string v3, "disconnect"

    const/4 v4, 0x0

    new-array v4, v4, [Ljava/lang/Class;

    invoke-virtual {v2, v3, v4}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    .line 35
    .local v0, "disconnectMethod":Ljava/lang/reflect/Method;
    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-virtual {v0, p0, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    .line 39
    .end local v0    # "disconnectMethod":Ljava/lang/reflect/Method;
    :goto_0
    return-void

    .line 36
    :catch_0
    move-exception v1

    .line 37
    .local v1, "e":Ljava/lang/Exception;
    const-string v2, "Wearable - Unable to call GoogleApiClient.disconnect() method (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v5

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static isConnected(Lcom/google/android/gms/common/api/GoogleApiClient;)Ljava/lang/Boolean;
    .locals 7
    .param p0, "client"    # Lcom/google/android/gms/common/api/GoogleApiClient;

    .prologue
    const/4 v4, 0x0

    .line 53
    :try_start_0
    const-class v3, Lcom/google/android/gms/common/api/GoogleApiClient;

    const-string v5, "isConnected"

    const/4 v6, 0x0

    new-array v6, v6, [Ljava/lang/Class;

    invoke-virtual {v3, v5, v6}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    .line 54
    .local v0, "connectMethod":Ljava/lang/reflect/Method;
    const/4 v3, 0x0

    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {v0, p0, v3}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    .line 55
    .local v2, "result":Ljava/lang/Object;
    instance-of v3, v2, Ljava/lang/Boolean;

    if-eqz v3, :cond_0

    check-cast v2, Ljava/lang/Boolean;

    .end local v2    # "result":Ljava/lang/Object;
    invoke-virtual {v2}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v3

    :goto_0
    invoke-static {v3}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v3

    .line 59
    .end local v0    # "connectMethod":Ljava/lang/reflect/Method;
    :goto_1
    return-object v3

    .restart local v0    # "connectMethod":Ljava/lang/reflect/Method;
    .restart local v2    # "result":Ljava/lang/Object;
    :cond_0
    move v3, v4

    .line 55
    goto :goto_0

    .line 56
    .end local v0    # "connectMethod":Ljava/lang/reflect/Method;
    .end local v2    # "result":Ljava/lang/Object;
    :catch_0
    move-exception v1

    .line 57
    .local v1, "e":Ljava/lang/Exception;
    const-string v3, "Wearable - Unable to call GoogleApiClient.isConnected() method (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v4

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 59
    invoke-static {v4}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v3

    goto :goto_1
.end method
