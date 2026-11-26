.class final Lcom/adobe/mobile/MessageLocalNotification;
.super Lcom/adobe/mobile/Message;
.source "MessageLocalNotification.java"


# static fields
.field private static final JSON_CONFIG_CONTENT:Ljava/lang/String; = "content"

.field private static final JSON_CONFIG_WAIT:Ljava/lang/String; = "wait"


# instance fields
.field protected content:Ljava/lang/String;

.field protected deeplink:Ljava/lang/String;

.field protected localNotificationDelay:Ljava/lang/Integer;

.field protected userInfo:Ljava/lang/String;


# direct methods
.method constructor <init>()V
    .locals 0

    .prologue
    .line 34
    invoke-direct {p0}, Lcom/adobe/mobile/Message;-><init>()V

    return-void
.end method


# virtual methods
.method protected initWithPayloadObject(Lorg/json/JSONObject;)Z
    .locals 8
    .param p1, "dictionary"    # Lorg/json/JSONObject;

    .prologue
    const/4 v3, 0x1

    const/4 v2, 0x0

    .line 49
    if-eqz p1, :cond_0

    invoke-virtual {p1}, Lorg/json/JSONObject;->length()I

    move-result v4

    if-gtz v4, :cond_1

    .line 112
    :cond_0
    :goto_0
    return v2

    .line 54
    :cond_1
    invoke-super {p0, p1}, Lcom/adobe/mobile/Message;->initWithPayloadObject(Lorg/json/JSONObject;)Z

    move-result v4

    if-eqz v4, :cond_0

    .line 58
    const-string v4, "Messages -  Building Local Notification message."

    new-array v5, v2, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 62
    :try_start_0
    const-string v4, "payload"

    invoke-virtual {p1, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    .line 63
    .local v1, "jsonPayload":Lorg/json/JSONObject;
    invoke-virtual {v1}, Lorg/json/JSONObject;->length()I

    move-result v4

    if-gtz v4, :cond_2

    .line 64
    const-string v4, "Messages - Unable to create local notification message \"%s\", payload is empty"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    iget-object v7, p0, Lcom/adobe/mobile/MessageLocalNotification;->messageId:Ljava/lang/String;

    aput-object v7, v5, v6

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 68
    .end local v1    # "jsonPayload":Lorg/json/JSONObject;
    :catch_0
    move-exception v0

    .line 69
    .local v0, "ex":Lorg/json/JSONException;
    const-string v4, "Messages - Unable to create local notification message \"%s\", payload is required"

    new-array v3, v3, [Ljava/lang/Object;

    iget-object v5, p0, Lcom/adobe/mobile/MessageLocalNotification;->messageId:Ljava/lang/String;

    aput-object v5, v3, v2

    invoke-static {v4, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 75
    .end local v0    # "ex":Lorg/json/JSONException;
    .restart local v1    # "jsonPayload":Lorg/json/JSONObject;
    :cond_2
    :try_start_1
    const-string v4, "content"

    invoke-virtual {v1, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    iput-object v4, p0, Lcom/adobe/mobile/MessageLocalNotification;->content:Ljava/lang/String;

    .line 76
    iget-object v4, p0, Lcom/adobe/mobile/MessageLocalNotification;->content:Ljava/lang/String;

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    if-gtz v4, :cond_3

    .line 77
    const-string v4, "Messages - Unable to create local notification message \"%s\", content is empty"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    iget-object v7, p0, Lcom/adobe/mobile/MessageLocalNotification;->messageId:Ljava/lang/String;

    aput-object v7, v5, v6

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    goto :goto_0

    .line 81
    :catch_1
    move-exception v0

    .line 82
    .restart local v0    # "ex":Lorg/json/JSONException;
    const-string v4, "Messages - Unable to create local notification message \"%s\", content is required"

    new-array v3, v3, [Ljava/lang/Object;

    iget-object v5, p0, Lcom/adobe/mobile/MessageLocalNotification;->messageId:Ljava/lang/String;

    aput-object v5, v3, v2

    invoke-static {v4, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 87
    .end local v0    # "ex":Lorg/json/JSONException;
    :cond_3
    :try_start_2
    const-string v4, "wait"

    invoke-virtual {v1, v4}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v4

    invoke-static {v4}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    iput-object v4, p0, Lcom/adobe/mobile/MessageLocalNotification;->localNotificationDelay:Ljava/lang/Integer;
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_2

    .line 95
    :try_start_3
    const-string v4, "adb_deeplink"

    invoke-virtual {v1, v4}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    iput-object v4, p0, Lcom/adobe/mobile/MessageLocalNotification;->deeplink:Ljava/lang/String;
    :try_end_3
    .catch Lorg/json/JSONException; {:try_start_3 .. :try_end_3} :catch_3

    .line 102
    :goto_1
    :try_start_4
    const-string v4, "userData"

    invoke-virtual {v1, v4}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v4

    invoke-virtual {v4}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v4

    iput-object v4, p0, Lcom/adobe/mobile/MessageLocalNotification;->userInfo:Ljava/lang/String;
    :try_end_4
    .catch Lorg/json/JSONException; {:try_start_4 .. :try_end_4} :catch_4
    .catch Ljava/lang/NullPointerException; {:try_start_4 .. :try_end_4} :catch_5

    .line 110
    :goto_2
    const-string v4, "Message created with: \n MessageID: \"%s\" \n Content: \"%s\" \n Delay: \"%d\" \n Deeplink: \"%s\" \n User Data: \"%s\""

    const/4 v5, 0x5

    new-array v5, v5, [Ljava/lang/Object;

    iget-object v6, p0, Lcom/adobe/mobile/MessageLocalNotification;->messageId:Ljava/lang/String;

    aput-object v6, v5, v2

    iget-object v2, p0, Lcom/adobe/mobile/MessageLocalNotification;->content:Ljava/lang/String;

    aput-object v2, v5, v3

    const/4 v2, 0x2

    iget-object v6, p0, Lcom/adobe/mobile/MessageLocalNotification;->localNotificationDelay:Ljava/lang/Integer;

    aput-object v6, v5, v2

    const/4 v2, 0x3

    iget-object v6, p0, Lcom/adobe/mobile/MessageLocalNotification;->deeplink:Ljava/lang/String;

    aput-object v6, v5, v2

    const/4 v2, 0x4

    iget-object v6, p0, Lcom/adobe/mobile/MessageLocalNotification;->userInfo:Ljava/lang/String;

    aput-object v6, v5, v2

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move v2, v3

    .line 112
    goto/16 :goto_0

    .line 89
    :catch_2
    move-exception v0

    .line 90
    .restart local v0    # "ex":Lorg/json/JSONException;
    const-string v4, "Messages - Unable to create local notification message \"%s\", localNotificationDelay is required"

    new-array v3, v3, [Ljava/lang/Object;

    iget-object v5, p0, Lcom/adobe/mobile/MessageLocalNotification;->messageId:Ljava/lang/String;

    aput-object v5, v3, v2

    invoke-static {v4, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 97
    .end local v0    # "ex":Lorg/json/JSONException;
    :catch_3
    move-exception v0

    .line 98
    .restart local v0    # "ex":Lorg/json/JSONException;
    const-string v4, "Messages - Tried to read deeplink for local notification message but found none.  This is not a required field"

    new-array v5, v2, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 104
    .end local v0    # "ex":Lorg/json/JSONException;
    :catch_4
    move-exception v0

    .line 105
    .restart local v0    # "ex":Lorg/json/JSONException;
    const-string v4, "Messages - Tried to read userData for local notification message but found none.  This is not a required field"

    new-array v5, v2, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2

    .line 106
    .end local v0    # "ex":Lorg/json/JSONException;
    :catch_5
    move-exception v0

    .line 107
    .local v0, "ex":Ljava/lang/NullPointerException;
    const-string v4, "Messages - Tried to read userData for local notification message but found none.  This is not a required field"

    new-array v5, v2, [Ljava/lang/Object;

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2
.end method

.method protected show()V
    .locals 13

    .prologue
    const/4 v12, 0x0

    .line 117
    invoke-super {p0}, Lcom/adobe/mobile/Message;->show()V

    .line 121
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCurrentActivity()Landroid/app/Activity;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullActivityException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v2

    .line 128
    .local v2, "currentActivity":Landroid/app/Activity;
    new-instance v8, Ljava/security/SecureRandom;

    invoke-direct {v8}, Ljava/security/SecureRandom;-><init>()V

    invoke-virtual {v8}, Ljava/security/SecureRandom;->nextInt()I

    move-result v6

    .line 130
    .local v6, "requestCode":I
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v1

    .line 131
    .local v1, "calendar":Ljava/util/Calendar;
    const/16 v8, 0xd

    iget-object v9, p0, Lcom/adobe/mobile/MessageLocalNotification;->localNotificationDelay:Ljava/lang/Integer;

    invoke-virtual {v9}, Ljava/lang/Integer;->intValue()I

    move-result v9

    invoke-virtual {v1, v8, v9}, Ljava/util/Calendar;->add(II)V

    .line 132
    new-instance v5, Landroid/content/Intent;

    const-string v8, "android.intent.action.VIEW"

    invoke-direct {v5, v8}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 133
    .local v5, "intent":Landroid/content/Intent;
    const-class v8, Lcom/adobe/mobile/MessageNotificationHandler;

    invoke-virtual {v5, v2, v8}, Landroid/content/Intent;->setClass(Landroid/content/Context;Ljava/lang/Class;)Landroid/content/Intent;

    .line 134
    const-string v8, "adbMessageCode"

    sget-object v9, Lcom/adobe/mobile/Messages;->MESSAGE_LOCAL_IDENTIFIER:Ljava/lang/Integer;

    invoke-virtual {v5, v8, v9}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/io/Serializable;)Landroid/content/Intent;

    .line 135
    const-string v8, "adb_m_l_id"

    iget-object v9, p0, Lcom/adobe/mobile/MessageLocalNotification;->messageId:Ljava/lang/String;

    invoke-virtual {v5, v8, v9}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 136
    const-string v8, "requestCode"

    invoke-virtual {v5, v8, v6}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    .line 137
    const-string v8, "userData"

    iget-object v9, p0, Lcom/adobe/mobile/MessageLocalNotification;->userInfo:Ljava/lang/String;

    invoke-virtual {v5, v8, v9}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 138
    const-string v8, "adb_deeplink"

    iget-object v9, p0, Lcom/adobe/mobile/MessageLocalNotification;->deeplink:Ljava/lang/String;

    invoke-virtual {v5, v8, v9}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 139
    const-string v8, "alarm_message"

    iget-object v9, p0, Lcom/adobe/mobile/MessageLocalNotification;->content:Ljava/lang/String;

    invoke-virtual {v5, v8, v9}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 142
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v8

    const/high16 v9, 0x8000000

    invoke-static {v8, v6, v5, v9}, Landroid/app/PendingIntent;->getBroadcast(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v7

    .line 144
    .local v7, "sender":Landroid/app/PendingIntent;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v8

    const-string v9, "alarm"

    invoke-virtual {v8, v9}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/AlarmManager;

    .line 145
    .local v0, "alarmManager":Landroid/app/AlarmManager;
    const/4 v8, 0x0

    invoke-virtual {v1}, Ljava/util/Calendar;->getTimeInMillis()J

    move-result-wide v10

    invoke-virtual {v0, v8, v10, v11, v7}, Landroid/app/AlarmManager;->set(IJLandroid/app/PendingIntent;)V
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_1

    .line 149
    .end local v0    # "alarmManager":Landroid/app/AlarmManager;
    .end local v1    # "calendar":Ljava/util/Calendar;
    .end local v2    # "currentActivity":Landroid/app/Activity;
    .end local v5    # "intent":Landroid/content/Intent;
    .end local v6    # "requestCode":I
    .end local v7    # "sender":Landroid/app/PendingIntent;
    :goto_0
    return-void

    .line 123
    :catch_0
    move-exception v4

    .line 124
    .local v4, "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    invoke-virtual {v4}, Lcom/adobe/mobile/StaticMethods$NullActivityException;->getMessage()Ljava/lang/String;

    move-result-object v8

    new-array v9, v12, [Ljava/lang/Object;

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 146
    .end local v4    # "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    .restart local v1    # "calendar":Ljava/util/Calendar;
    .restart local v2    # "currentActivity":Landroid/app/Activity;
    .restart local v5    # "intent":Landroid/content/Intent;
    .restart local v6    # "requestCode":I
    :catch_1
    move-exception v3

    .line 147
    .local v3, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v8, "Messaging - Error scheduling local notification (%s)"

    const/4 v9, 0x1

    new-array v9, v9, [Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v12

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method
