.class final Lcom/adobe/mobile/Messages;
.super Ljava/lang/Object;
.source "Messages.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/Messages$MessageShowRule;
    }
.end annotation


# static fields
.field protected static final MESSAGE_ACTION_NAME:Ljava/lang/String; = "In-App Message"

.field protected static final MESSAGE_BUTTON_ID:Ljava/lang/String; = "a.message.button.id"

.field protected static final MESSAGE_CANCEL_ID:Ljava/lang/String; = "ADBMessageCancelButton"

.field protected static final MESSAGE_CLICKED:Ljava/lang/String; = "a.message.clicked"

.field protected static final MESSAGE_CLICK_THROUGH_ID:Ljava/lang/String; = "ADBMessageClickThroughButton"

.field protected static final MESSAGE_ID:Ljava/lang/String; = "a.message.id"

.field protected static final MESSAGE_JSON_USER_INFO_KEY:Ljava/lang/String; = "userData"

.field protected static final MESSAGE_LOCAL_ADB_CODE:Ljava/lang/String; = "adbMessageCode"

.field protected static final MESSAGE_LOCAL_IDENTIFIER:Ljava/lang/Integer;

.field protected static final MESSAGE_LOCAL_PAYLOAD:Ljava/lang/String; = "alarm_message"

.field protected static final MESSAGE_LOCAL_REQUEST_CODE:Ljava/lang/String; = "requestCode"

.field protected static final MESSAGE_TOKEN_LIFETIME_VALUE:Ljava/lang/String; = "{lifetimeValue}"

.field protected static final MESSAGE_TOKEN_MESSAGE_ID:Ljava/lang/String; = "{messageId}"

.field protected static final MESSAGE_TOKEN_TRACKING_ID:Ljava/lang/String; = "{trackingId}"

.field protected static final MESSAGE_TOKEN_USER_ID:Ljava/lang/String; = "{userId}"

.field protected static final MESSAGE_TRIGGERED:Ljava/lang/String; = "a.message.triggered"

.field protected static final MESSAGE_VIEWED:Ljava/lang/String; = "a.message.viewed"

.field private static _currentMessage:Lcom/adobe/mobile/Message;

.field private static final _currentMessageMutex:Ljava/lang/Object;

.field private static _largeIconResourceId:I

.field private static _messageFullScreen:Lcom/adobe/mobile/MessageFullScreen;

.field private static final _messageFullScreenMutex:Ljava/lang/Object;

.field private static _smallIconResourceId:I


# direct methods
.method static constructor <clinit>()V
    .locals 3

    .prologue
    const/4 v2, 0x0

    const/4 v1, -0x1

    .line 54
    const v0, 0xb7267

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/Messages;->MESSAGE_LOCAL_IDENTIFIER:Ljava/lang/Integer;

    .line 211
    sput-object v2, Lcom/adobe/mobile/Messages;->_messageFullScreen:Lcom/adobe/mobile/MessageFullScreen;

    .line 212
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/Messages;->_messageFullScreenMutex:Ljava/lang/Object;

    .line 241
    sput v1, Lcom/adobe/mobile/Messages;->_smallIconResourceId:I

    .line 249
    sput v1, Lcom/adobe/mobile/Messages;->_largeIconResourceId:I

    .line 257
    sput-object v2, Lcom/adobe/mobile/Messages;->_currentMessage:Lcom/adobe/mobile/Message;

    .line 258
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/Messages;->_currentMessageMutex:Ljava/lang/Object;

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .prologue
    .line 27
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 61
    return-void
.end method

.method protected static block3rdPartyCallbacksQueueForReferrer()V
    .locals 2

    .prologue
    .line 93
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getThirdPartyCallbacksExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/Messages$1;

    invoke-direct {v1}, Lcom/adobe/mobile/Messages$1;-><init>()V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 106
    return-void
.end method

.method protected static checkFor3rdPartyCallbacks(Ljava/util/Map;Ljava/util/Map;)V
    .locals 2
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
            ">;)V"
        }
    .end annotation

    .prologue
    .line 109
    .local p0, "vars":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p1, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getThirdPartyCallbacksExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/Messages$2;

    invoke-direct {v1, p1, p0}, Lcom/adobe/mobile/Messages$2;-><init>(Ljava/util/Map;Ljava/util/Map;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 132
    return-void
.end method

.method protected static checkForInAppMessage(Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)V
    .locals 2
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
            ">;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 176
    .local p0, "vars":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p1, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .local p2, "lifecycleData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getMessagesExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/Messages$4;

    invoke-direct {v1, p0, p1, p2}, Lcom/adobe/mobile/Messages$4;-><init>(Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 209
    return-void
.end method

.method protected static checkForPIIRequests(Ljava/util/Map;)V
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
    .line 135
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getPIIExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/Messages$3;

    invoke-direct {v1, p0}, Lcom/adobe/mobile/Messages$3;-><init>(Ljava/util/Map;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 170
    return-void
.end method

.method protected static getCurrentFullscreenMessage()Lcom/adobe/mobile/MessageFullScreen;
    .locals 2

    .prologue
    .line 220
    sget-object v1, Lcom/adobe/mobile/Messages;->_messageFullScreenMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 221
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/Messages;->_messageFullScreen:Lcom/adobe/mobile/MessageFullScreen;

    monitor-exit v1

    return-object v0

    .line 222
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getCurrentMessage()Lcom/adobe/mobile/Message;
    .locals 2

    .prologue
    .line 260
    sget-object v1, Lcom/adobe/mobile/Messages;->_currentMessageMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 261
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/Messages;->_currentMessage:Lcom/adobe/mobile/Message;

    monitor-exit v1

    return-object v0

    .line 262
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getLargeIconResourceId()I
    .locals 1

    .prologue
    .line 254
    sget v0, Lcom/adobe/mobile/Messages;->_largeIconResourceId:I

    return v0
.end method

.method protected static getSmallIconResourceId()I
    .locals 1

    .prologue
    .line 246
    sget v0, Lcom/adobe/mobile/Messages;->_smallIconResourceId:I

    return v0
.end method

.method protected static lowercaseKeysForMap(Ljava/util/Map;)Ljava/util/HashMap;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/HashMap",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 78
    .local p0, "dataMap":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p0, :cond_0

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v2

    if-gtz v2, :cond_2

    .line 79
    :cond_0
    const/4 v1, 0x0

    .line 86
    :cond_1
    return-object v1

    .line 82
    :cond_2
    new-instance v1, Ljava/util/HashMap;

    invoke-interface {p0}, Ljava/util/Map;->size()I

    move-result v2

    invoke-direct {v1, v2}, Ljava/util/HashMap;-><init>(I)V

    .line 83
    .local v1, "lowercasedDataMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 84
    .local v0, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    invoke-virtual {v1, v2, v4}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0
.end method

.method protected static resetAllInAppMessages()V
    .locals 2

    .prologue
    .line 226
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getMessagesExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/Messages$5;

    invoke-direct {v1}, Lcom/adobe/mobile/Messages$5;-><init>()V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 239
    return-void
.end method

.method protected static setCurrentMessage(Lcom/adobe/mobile/Message;)V
    .locals 2
    .param p0, "message"    # Lcom/adobe/mobile/Message;

    .prologue
    .line 266
    sget-object v1, Lcom/adobe/mobile/Messages;->_currentMessageMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 267
    :try_start_0
    sput-object p0, Lcom/adobe/mobile/Messages;->_currentMessage:Lcom/adobe/mobile/Message;

    .line 268
    monitor-exit v1

    .line 269
    return-void

    .line 268
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static setCurrentMessageFullscreen(Lcom/adobe/mobile/MessageFullScreen;)V
    .locals 2
    .param p0, "message"    # Lcom/adobe/mobile/MessageFullScreen;

    .prologue
    .line 214
    sget-object v1, Lcom/adobe/mobile/Messages;->_messageFullScreenMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 215
    :try_start_0
    sput-object p0, Lcom/adobe/mobile/Messages;->_messageFullScreen:Lcom/adobe/mobile/MessageFullScreen;

    .line 216
    monitor-exit v1

    .line 217
    return-void

    .line 216
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static setLargeIconResourceId(I)V
    .locals 0
    .param p0, "resourceId"    # I

    .prologue
    .line 251
    sput p0, Lcom/adobe/mobile/Messages;->_largeIconResourceId:I

    .line 252
    return-void
.end method

.method protected static setSmallIconResourceId(I)V
    .locals 0
    .param p0, "resourceId"    # I

    .prologue
    .line 243
    sput p0, Lcom/adobe/mobile/Messages;->_smallIconResourceId:I

    .line 244
    return-void
.end method
