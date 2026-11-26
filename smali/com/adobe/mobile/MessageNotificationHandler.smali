.class public Lcom/adobe/mobile/MessageNotificationHandler;
.super Landroid/content/BroadcastReceiver;
.source "MessageNotificationHandler.java"


# direct methods
.method public constructor <init>()V
    .locals 0

    .prologue
    .line 45
    invoke-direct {p0}, Landroid/content/BroadcastReceiver;-><init>()V

    return-void
.end method

.method private getAppName()Ljava/lang/String;
    .locals 9

    .prologue
    const/4 v7, 0x1

    const/4 v8, 0x0

    .line 225
    const-string v0, ""

    .line 227
    .local v0, "appName":Ljava/lang/String;
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v4

    .line 228
    .local v4, "packageManager":Landroid/content/pm/PackageManager;
    if-eqz v4, :cond_0

    .line 229
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    invoke-virtual {v4, v5, v6}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v1

    .line 230
    .local v1, "applicationInfo":Landroid/content/pm/ApplicationInfo;
    if-eqz v1, :cond_0

    .line 231
    invoke-virtual {v4, v1}, Landroid/content/pm/PackageManager;->getApplicationLabel(Landroid/content/pm/ApplicationInfo;)Ljava/lang/CharSequence;

    move-result-object v5

    if-eqz v5, :cond_1

    .line 232
    invoke-virtual {v4, v1}, Landroid/content/pm/PackageManager;->getApplicationLabel(Landroid/content/pm/ApplicationInfo;)Ljava/lang/CharSequence;

    move-result-object v5

    check-cast v5, Ljava/lang/String;

    move-object v0, v5

    .line 243
    .end local v1    # "applicationInfo":Landroid/content/pm/ApplicationInfo;
    .end local v4    # "packageManager":Landroid/content/pm/PackageManager;
    :cond_0
    :goto_0
    return-object v0

    .line 232
    .restart local v1    # "applicationInfo":Landroid/content/pm/ApplicationInfo;
    .restart local v4    # "packageManager":Landroid/content/pm/PackageManager;
    :cond_1
    const-string v0, ""
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_1

    goto :goto_0

    .line 236
    .end local v1    # "applicationInfo":Landroid/content/pm/ApplicationInfo;
    .end local v4    # "packageManager":Landroid/content/pm/PackageManager;
    :catch_0
    move-exception v3

    .line 237
    .local v3, "ex":Landroid/content/pm/PackageManager$NameNotFoundException;
    const-string v5, "Messages - unable to retrieve app name for local notification (%s)"

    new-array v6, v7, [Ljava/lang/Object;

    invoke-virtual {v3}, Landroid/content/pm/PackageManager$NameNotFoundException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v8

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 238
    .end local v3    # "ex":Landroid/content/pm/PackageManager$NameNotFoundException;
    :catch_1
    move-exception v2

    .line 239
    .local v2, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v5, "Messages - unable to get app name (%s)"

    new-array v6, v7, [Ljava/lang/Object;

    invoke-virtual {v2}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v8

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private getLargeIcon()Landroid/graphics/Bitmap;
    .locals 14
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/ClassNotFoundException;,
            Ljava/lang/NoSuchMethodException;,
            Lcom/adobe/mobile/StaticMethods$NullContextException;,
            Ljava/lang/IllegalAccessException;,
            Ljava/lang/reflect/InvocationTargetException;
        }
    .end annotation

    .prologue
    const/4 v13, 0x2

    const/4 v12, 0x1

    const/4 v11, 0x0

    .line 251
    const/4 v4, 0x0

    .line 254
    .local v4, "iconDrawable":Landroid/graphics/drawable/Drawable;
    invoke-static {}, Lcom/adobe/mobile/Messages;->getLargeIconResourceId()I

    move-result v5

    .line 255
    .local v5, "largeIconResourceId":I
    const/4 v7, -0x1

    if-eq v5, v7, :cond_3

    .line 256
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v2

    .line 257
    .local v2, "ctx":Landroid/content/Context;
    sget v7, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v8, 0x14

    if-le v7, v8, :cond_2

    .line 258
    const-class v7, Landroid/content/res/Resources;

    const-string v8, "getDrawable"

    new-array v9, v13, [Ljava/lang/Class;

    sget-object v10, Ljava/lang/Integer;->TYPE:Ljava/lang/Class;

    aput-object v10, v9, v11

    const-class v10, Landroid/content/res/Resources$Theme;

    aput-object v10, v9, v12

    invoke-virtual {v7, v8, v9}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    .line 259
    .local v0, "__getDrawable":Ljava/lang/reflect/Method;
    invoke-virtual {v2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v7

    new-array v8, v13, [Ljava/lang/Object;

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v9

    aput-object v9, v8, v11

    invoke-virtual {v2}, Landroid/content/Context;->getTheme()Landroid/content/res/Resources$Theme;

    move-result-object v9

    aput-object v9, v8, v12

    invoke-virtual {v0, v7, v8}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    .end local v4    # "iconDrawable":Landroid/graphics/drawable/Drawable;
    check-cast v4, Landroid/graphics/drawable/Drawable;

    .line 276
    .end local v0    # "__getDrawable":Ljava/lang/reflect/Method;
    .end local v2    # "ctx":Landroid/content/Context;
    .restart local v4    # "iconDrawable":Landroid/graphics/drawable/Drawable;
    :cond_0
    :goto_0
    const/4 v3, 0x0

    .line 277
    .local v3, "icon":Landroid/graphics/Bitmap;
    if-eqz v4, :cond_1

    .line 278
    check-cast v4, Landroid/graphics/drawable/BitmapDrawable;

    .end local v4    # "iconDrawable":Landroid/graphics/drawable/Drawable;
    invoke-virtual {v4}, Landroid/graphics/drawable/BitmapDrawable;->getBitmap()Landroid/graphics/Bitmap;

    move-result-object v3

    .line 281
    :cond_1
    return-object v3

    .line 261
    .end local v3    # "icon":Landroid/graphics/Bitmap;
    .restart local v2    # "ctx":Landroid/content/Context;
    .restart local v4    # "iconDrawable":Landroid/graphics/drawable/Drawable;
    :cond_2
    const-class v7, Landroid/content/res/Resources;

    const-string v8, "getDrawable"

    new-array v9, v12, [Ljava/lang/Class;

    sget-object v10, Ljava/lang/Integer;->TYPE:Ljava/lang/Class;

    aput-object v10, v9, v11

    invoke-virtual {v7, v8, v9}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v0

    .line 262
    .restart local v0    # "__getDrawable":Ljava/lang/reflect/Method;
    invoke-virtual {v2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v7

    new-array v8, v12, [Ljava/lang/Object;

    invoke-static {v5}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v9

    aput-object v9, v8, v11

    invoke-virtual {v0, v7, v8}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    .end local v4    # "iconDrawable":Landroid/graphics/drawable/Drawable;
    check-cast v4, Landroid/graphics/drawable/Drawable;

    .restart local v4    # "iconDrawable":Landroid/graphics/drawable/Drawable;
    goto :goto_0

    .line 267
    .end local v0    # "__getDrawable":Ljava/lang/reflect/Method;
    .end local v2    # "ctx":Landroid/content/Context;
    :cond_3
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v7

    invoke-virtual {v7}, Landroid/content/Context;->getApplicationInfo()Landroid/content/pm/ApplicationInfo;

    move-result-object v1

    .line 268
    .local v1, "applicationInfo":Landroid/content/pm/ApplicationInfo;
    if-eqz v1, :cond_0

    .line 269
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v7

    invoke-virtual {v7}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v6

    .line 270
    .local v6, "packageManager":Landroid/content/pm/PackageManager;
    if-eqz v6, :cond_0

    .line 271
    invoke-virtual {v6, v1}, Landroid/content/pm/PackageManager;->getApplicationIcon(Landroid/content/pm/ApplicationInfo;)Landroid/graphics/drawable/Drawable;

    move-result-object v4

    goto :goto_0
.end method

.method private getSmallIcon()I
    .locals 2

    .prologue
    .line 247
    invoke-static {}, Lcom/adobe/mobile/Messages;->getSmallIconResourceId()I

    move-result v0

    const/4 v1, -0x1

    if-eq v0, v1, :cond_0

    invoke-static {}, Lcom/adobe/mobile/Messages;->getSmallIconResourceId()I

    move-result v0

    :goto_0
    return v0

    :cond_0
    const v0, 0x1080093

    goto :goto_0
.end method


# virtual methods
.method public onReceive(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 40
    .param p1, "context"    # Landroid/content/Context;
    .param p2, "intent"    # Landroid/content/Intent;

    .prologue
    .line 49
    invoke-virtual/range {p2 .. p2}, Landroid/content/Intent;->getExtras()Landroid/os/Bundle;

    move-result-object v8

    .line 51
    .local v8, "bundle":Landroid/os/Bundle;
    if-nez v8, :cond_1

    .line 52
    const-string v35, "Messages - unable to load extras from local notification intent"

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 222
    :cond_0
    :goto_0
    return-void

    .line 64
    :cond_1
    :try_start_0
    const-string v35, "alarm_message"

    move-object/from16 v0, v35

    invoke-virtual {v8, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    .line 65
    .local v14, "message":Ljava/lang/String;
    const-string v35, "adbMessageCode"

    move-object/from16 v0, v35

    invoke-virtual {v8, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v23

    .line 66
    .local v23, "requestCode":I
    const-string v35, "requestCode"

    move-object/from16 v0, v35

    invoke-virtual {v8, v0}, Landroid/os/Bundle;->getInt(Ljava/lang/String;)I

    move-result v22

    .line 67
    .local v22, "otherRequestCode":I
    const-string v35, "adb_m_l_id"

    move-object/from16 v0, v35

    invoke-virtual {v8, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v15

    .line 68
    .local v15, "messageID":Ljava/lang/String;
    const-string v35, "adb_deeplink"

    move-object/from16 v0, v35

    invoke-virtual {v8, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v11

    .line 69
    .local v11, "deeplink":Ljava/lang/String;
    const-string v35, "userData"

    move-object/from16 v0, v35

    invoke-virtual {v8, v0}, Landroid/os/Bundle;->getString(Ljava/lang/String;)Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v34

    .line 77
    .local v34, "userInfoStringJSON":Ljava/lang/String;
    sget-object v35, Lcom/adobe/mobile/Messages;->MESSAGE_LOCAL_IDENTIFIER:Ljava/lang/Integer;

    invoke-virtual/range {v35 .. v35}, Ljava/lang/Integer;->intValue()I

    move-result v35

    move/from16 v0, v23

    move/from16 v1, v35

    if-ne v0, v1, :cond_0

    .line 82
    if-nez v14, :cond_2

    .line 83
    const-string v35, "Messages - local notification message was empty "

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 71
    .end local v11    # "deeplink":Ljava/lang/String;
    .end local v14    # "message":Ljava/lang/String;
    .end local v15    # "messageID":Ljava/lang/String;
    .end local v22    # "otherRequestCode":I
    .end local v23    # "requestCode":I
    .end local v34    # "userInfoStringJSON":Ljava/lang/String;
    :catch_0
    move-exception v12

    .line 72
    .local v12, "ex":Ljava/lang/Exception;
    const-string v35, "Messages - unable to load message from local notification (%s)"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    invoke-virtual {v12}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v38

    aput-object v38, v36, v37

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 89
    .end local v12    # "ex":Ljava/lang/Exception;
    .restart local v11    # "deeplink":Ljava/lang/String;
    .restart local v14    # "message":Ljava/lang/String;
    .restart local v15    # "messageID":Ljava/lang/String;
    .restart local v22    # "otherRequestCode":I
    .restart local v23    # "requestCode":I
    .restart local v34    # "userInfoStringJSON":Ljava/lang/String;
    :cond_2
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCurrentActivity()Landroid/app/Activity;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullActivityException; {:try_start_1 .. :try_end_1} :catch_2

    move-result-object v10

    .line 98
    .local v10, "currentActivity":Landroid/app/Activity;
    :try_start_2
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;
    :try_end_2
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_2 .. :try_end_2} :catch_3

    move-result-object v32

    .line 105
    .local v32, "sharedContext":Landroid/content/Context;
    const/16 v17, 0x0

    .line 107
    .local v17, "notificationActivity":Landroid/app/Activity;
    :try_start_3
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCurrentActivity()Landroid/app/Activity;
    :try_end_3
    .catch Lcom/adobe/mobile/StaticMethods$NullActivityException; {:try_start_3 .. :try_end_3} :catch_4

    move-result-object v17

    .line 115
    :goto_1
    if-eqz v11, :cond_3

    invoke-virtual {v11}, Ljava/lang/String;->isEmpty()Z

    move-result v35

    if-nez v35, :cond_3

    .line 116
    new-instance v24, Landroid/content/Intent;

    const-string v35, "android.intent.action.VIEW"

    move-object/from16 v0, v24

    move-object/from16 v1, v35

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    .line 117
    .local v24, "resumeIntent":Landroid/content/Intent;
    invoke-static {v11}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v35

    move-object/from16 v0, v24

    move-object/from16 v1, v35

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setData(Landroid/net/Uri;)Landroid/content/Intent;

    .line 124
    :goto_2
    const/high16 v35, 0x24000000

    move-object/from16 v0, v24

    move/from16 v1, v35

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setFlags(I)Landroid/content/Intent;

    .line 125
    const-string v35, "adb_m_l_id"

    move-object/from16 v0, v24

    move-object/from16 v1, v35

    invoke-virtual {v0, v1, v15}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 126
    const-string v35, "userData"

    move-object/from16 v0, v24

    move-object/from16 v1, v35

    move-object/from16 v2, v34

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 131
    sget v7, Landroid/os/Build$VERSION;->SDK_INT:I

    .line 134
    .local v7, "buildVersion":I
    const/high16 v35, 0x8000000

    :try_start_4
    move-object/from16 v0, v32

    move/from16 v1, v22

    move-object/from16 v2, v24

    move/from16 v3, v35

    invoke-static {v0, v1, v2, v3}, Landroid/app/PendingIntent;->getActivity(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object v25

    .line 141
    .local v25, "sender":Landroid/app/PendingIntent;
    if-nez v25, :cond_5

    .line 142
    const-string v35, "Messages - could not retrieve sender from broadcast, unable to post notification"

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catch Ljava/lang/ClassNotFoundException; {:try_start_4 .. :try_end_4} :catch_1
    .catch Ljava/lang/NoSuchMethodException; {:try_start_4 .. :try_end_4} :catch_5
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_4 .. :try_end_4} :catch_6
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_7

    goto/16 :goto_0

    .line 210
    .end local v25    # "sender":Landroid/app/PendingIntent;
    :catch_1
    move-exception v12

    .line 211
    .local v12, "ex":Ljava/lang/ClassNotFoundException;
    const-string v35, "Messages - error posting notification, class not found (%s)"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    invoke-virtual {v12}, Ljava/lang/ClassNotFoundException;->getMessage()Ljava/lang/String;

    move-result-object v38

    aput-object v38, v36, v37

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 91
    .end local v7    # "buildVersion":I
    .end local v10    # "currentActivity":Landroid/app/Activity;
    .end local v12    # "ex":Ljava/lang/ClassNotFoundException;
    .end local v17    # "notificationActivity":Landroid/app/Activity;
    .end local v24    # "resumeIntent":Landroid/content/Intent;
    .end local v32    # "sharedContext":Landroid/content/Context;
    :catch_2
    move-exception v12

    .line 92
    .local v12, "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    invoke-virtual {v12}, Lcom/adobe/mobile/StaticMethods$NullActivityException;->getMessage()Ljava/lang/String;

    move-result-object v35

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 100
    .end local v12    # "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    .restart local v10    # "currentActivity":Landroid/app/Activity;
    :catch_3
    move-exception v12

    .line 101
    .local v12, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    invoke-virtual {v12}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v35

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 109
    .end local v12    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .restart local v17    # "notificationActivity":Landroid/app/Activity;
    .restart local v32    # "sharedContext":Landroid/content/Context;
    :catch_4
    move-exception v12

    .line 110
    .local v12, "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    const-string v35, "Messages - unable to find activity for your notification, using default"

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_1

    .line 118
    .end local v12    # "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    :cond_3
    if-eqz v17, :cond_4

    .line 119
    invoke-virtual/range {v17 .. v17}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v24

    .restart local v24    # "resumeIntent":Landroid/content/Intent;
    goto/16 :goto_2

    .line 121
    .end local v24    # "resumeIntent":Landroid/content/Intent;
    :cond_4
    move-object/from16 v24, p2

    .restart local v24    # "resumeIntent":Landroid/content/Intent;
    goto/16 :goto_2

    .line 147
    .restart local v7    # "buildVersion":I
    .restart local v25    # "sender":Landroid/app/PendingIntent;
    :cond_5
    const/16 v35, 0xb

    move/from16 v0, v35

    if-lt v7, v0, :cond_7

    .line 148
    :try_start_5
    const-class v35, Landroid/content/BroadcastReceiver;

    invoke-virtual/range {v35 .. v35}, Ljava/lang/Class;->getClassLoader()Ljava/lang/ClassLoader;

    move-result-object v9

    .line 149
    .local v9, "classLoader":Ljava/lang/ClassLoader;
    const-string v35, "android.app.Notification$Builder"

    move-object/from16 v0, v35

    invoke-virtual {v9, v0}, Ljava/lang/ClassLoader;->loadClass(Ljava/lang/String;)Ljava/lang/Class;

    move-result-object v19

    .line 151
    .local v19, "notificationBuilderClass":Ljava/lang/Class;, "Ljava/lang/Class<*>;"
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    const-class v37, Landroid/content/Context;

    aput-object v37, v35, v36

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    invoke-virtual {v0, v1}, Ljava/lang/Class;->getConstructor([Ljava/lang/Class;)Ljava/lang/reflect/Constructor;

    move-result-object v20

    .line 152
    .local v20, "notificationConstructor":Ljava/lang/reflect/Constructor;, "Ljava/lang/reflect/Constructor<*>;"
    const/16 v35, 0x1

    move-object/from16 v0, v20

    move/from16 v1, v35

    invoke-virtual {v0, v1}, Ljava/lang/reflect/Constructor;->setAccessible(Z)V

    .line 153
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v37

    aput-object v37, v35, v36

    move-object/from16 v0, v20

    move-object/from16 v1, v35

    invoke-virtual {v0, v1}, Ljava/lang/reflect/Constructor;->newInstance([Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v18

    .line 157
    .local v18, "notificationBuilder":Ljava/lang/Object;
    const-string v35, "setSmallIcon"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    sget-object v38, Ljava/lang/Integer;->TYPE:Ljava/lang/Class;

    aput-object v38, v36, v37

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v31

    .line 158
    .local v31, "setSmallIcon":Ljava/lang/reflect/Method;
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    invoke-direct/range {p0 .. p0}, Lcom/adobe/mobile/MessageNotificationHandler;->getSmallIcon()I

    move-result v37

    invoke-static/range {v37 .. v37}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v37

    aput-object v37, v35, v36

    move-object/from16 v0, v31

    move-object/from16 v1, v18

    move-object/from16 v2, v35

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 161
    const-string v35, "setLargeIcon"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    const-class v38, Landroid/graphics/Bitmap;

    aput-object v38, v36, v37

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v30

    .line 162
    .local v30, "setLargeIcon":Ljava/lang/reflect/Method;
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    invoke-direct/range {p0 .. p0}, Lcom/adobe/mobile/MessageNotificationHandler;->getLargeIcon()Landroid/graphics/Bitmap;

    move-result-object v37

    aput-object v37, v35, v36

    move-object/from16 v0, v30

    move-object/from16 v1, v18

    move-object/from16 v2, v35

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 165
    const-string v35, "setContentTitle"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    const-class v38, Ljava/lang/CharSequence;

    aput-object v38, v36, v37

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v29

    .line 166
    .local v29, "setContentTitle":Ljava/lang/reflect/Method;
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    invoke-direct/range {p0 .. p0}, Lcom/adobe/mobile/MessageNotificationHandler;->getAppName()Ljava/lang/String;

    move-result-object v37

    aput-object v37, v35, v36

    move-object/from16 v0, v29

    move-object/from16 v1, v18

    move-object/from16 v2, v35

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 169
    const-string v35, "setContentText"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    const-class v38, Ljava/lang/CharSequence;

    aput-object v38, v36, v37

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v28

    .line 170
    .local v28, "setContentText":Ljava/lang/reflect/Method;
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    aput-object v14, v35, v36

    move-object/from16 v0, v28

    move-object/from16 v1, v18

    move-object/from16 v2, v35

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 173
    const-string v35, "setContentIntent"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    const-class v38, Landroid/app/PendingIntent;

    aput-object v38, v36, v37

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v27

    .line 174
    .local v27, "setContentIntent":Ljava/lang/reflect/Method;
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    aput-object v25, v35, v36

    move-object/from16 v0, v27

    move-object/from16 v1, v18

    move-object/from16 v2, v35

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 178
    const-string v35, "setAutoCancel"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    sget-object v38, Ljava/lang/Boolean;->TYPE:Ljava/lang/Class;

    aput-object v38, v36, v37

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v26

    .line 179
    .local v26, "setAutoCancel":Ljava/lang/reflect/Method;
    const/16 v35, 0x1

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    const/16 v37, 0x1

    invoke-static/range {v37 .. v37}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v37

    aput-object v37, v35, v36

    move-object/from16 v0, v26

    move-object/from16 v1, v18

    move-object/from16 v2, v35

    invoke-virtual {v0, v1, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 182
    const/16 v35, 0x10

    move/from16 v0, v35

    if-lt v7, v0, :cond_6

    .line 183
    const-string v35, "build"

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v6

    .line 184
    .local v6, "build":Ljava/lang/reflect/Method;
    const/16 v35, 0x0

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    move-object/from16 v0, v18

    move-object/from16 v1, v35

    invoke-virtual {v6, v0, v1}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v16

    .line 191
    .end local v6    # "build":Ljava/lang/reflect/Method;
    .local v16, "notification":Ljava/lang/Object;
    :goto_3
    if-eqz v16, :cond_0

    .line 207
    .end local v9    # "classLoader":Ljava/lang/ClassLoader;
    .end local v16    # "notification":Ljava/lang/Object;
    .end local v18    # "notificationBuilder":Ljava/lang/Object;
    .end local v19    # "notificationBuilderClass":Ljava/lang/Class;, "Ljava/lang/Class<*>;"
    .end local v20    # "notificationConstructor":Ljava/lang/reflect/Constructor;, "Ljava/lang/reflect/Constructor<*>;"
    .end local v26    # "setAutoCancel":Ljava/lang/reflect/Method;
    .end local v27    # "setContentIntent":Ljava/lang/reflect/Method;
    .end local v28    # "setContentText":Ljava/lang/reflect/Method;
    .end local v29    # "setContentTitle":Ljava/lang/reflect/Method;
    .end local v30    # "setLargeIcon":Ljava/lang/reflect/Method;
    .end local v31    # "setSmallIcon":Ljava/lang/reflect/Method;
    :goto_4
    const-string v35, "notification"

    move-object/from16 v0, v35

    invoke-virtual {v10, v0}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v21

    check-cast v21, Landroid/app/NotificationManager;

    .line 208
    .local v21, "notificationManager":Landroid/app/NotificationManager;
    new-instance v35, Ljava/security/SecureRandom;

    invoke-direct/range {v35 .. v35}, Ljava/security/SecureRandom;-><init>()V

    invoke-virtual/range {v35 .. v35}, Ljava/security/SecureRandom;->nextInt()I

    move-result v35

    check-cast v16, Landroid/app/Notification;

    move-object/from16 v0, v21

    move/from16 v1, v35

    move-object/from16 v2, v16

    invoke-virtual {v0, v1, v2}, Landroid/app/NotificationManager;->notify(ILandroid/app/Notification;)V
    :try_end_5
    .catch Ljava/lang/ClassNotFoundException; {:try_start_5 .. :try_end_5} :catch_1
    .catch Ljava/lang/NoSuchMethodException; {:try_start_5 .. :try_end_5} :catch_5
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_5 .. :try_end_5} :catch_6
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_7

    goto/16 :goto_0

    .line 213
    .end local v21    # "notificationManager":Landroid/app/NotificationManager;
    .end local v25    # "sender":Landroid/app/PendingIntent;
    :catch_5
    move-exception v12

    .line 214
    .local v12, "ex":Ljava/lang/NoSuchMethodException;
    const-string v35, "Messages - error posting notification, method not found (%s)"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    invoke-virtual {v12}, Ljava/lang/NoSuchMethodException;->getMessage()Ljava/lang/String;

    move-result-object v38

    aput-object v38, v36, v37

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 187
    .end local v12    # "ex":Ljava/lang/NoSuchMethodException;
    .restart local v9    # "classLoader":Ljava/lang/ClassLoader;
    .restart local v18    # "notificationBuilder":Ljava/lang/Object;
    .restart local v19    # "notificationBuilderClass":Ljava/lang/Class;, "Ljava/lang/Class<*>;"
    .restart local v20    # "notificationConstructor":Ljava/lang/reflect/Constructor;, "Ljava/lang/reflect/Constructor<*>;"
    .restart local v25    # "sender":Landroid/app/PendingIntent;
    .restart local v26    # "setAutoCancel":Ljava/lang/reflect/Method;
    .restart local v27    # "setContentIntent":Ljava/lang/reflect/Method;
    .restart local v28    # "setContentText":Ljava/lang/reflect/Method;
    .restart local v29    # "setContentTitle":Ljava/lang/reflect/Method;
    .restart local v30    # "setLargeIcon":Ljava/lang/reflect/Method;
    .restart local v31    # "setSmallIcon":Ljava/lang/reflect/Method;
    :cond_6
    :try_start_6
    const-string v35, "getNotification"

    const/16 v36, 0x0

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v36, v0

    move-object/from16 v0, v19

    move-object/from16 v1, v35

    move-object/from16 v2, v36

    invoke-virtual {v0, v1, v2}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v13

    .line 188
    .local v13, "getNotification":Ljava/lang/reflect/Method;
    const/16 v35, 0x0

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    move-object/from16 v0, v18

    move-object/from16 v1, v35

    invoke-virtual {v13, v0, v1}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v16

    .restart local v16    # "notification":Ljava/lang/Object;
    goto :goto_3

    .line 196
    .end local v9    # "classLoader":Ljava/lang/ClassLoader;
    .end local v13    # "getNotification":Ljava/lang/reflect/Method;
    .end local v16    # "notification":Ljava/lang/Object;
    .end local v18    # "notificationBuilder":Ljava/lang/Object;
    .end local v19    # "notificationBuilderClass":Ljava/lang/Class;, "Ljava/lang/Class<*>;"
    .end local v20    # "notificationConstructor":Ljava/lang/reflect/Constructor;, "Ljava/lang/reflect/Constructor<*>;"
    .end local v26    # "setAutoCancel":Ljava/lang/reflect/Method;
    .end local v27    # "setContentIntent":Ljava/lang/reflect/Method;
    .end local v28    # "setContentText":Ljava/lang/reflect/Method;
    .end local v29    # "setContentTitle":Ljava/lang/reflect/Method;
    .end local v30    # "setLargeIcon":Ljava/lang/reflect/Method;
    .end local v31    # "setSmallIcon":Ljava/lang/reflect/Method;
    :cond_7
    new-instance v33, Landroid/app/Notification;

    invoke-direct/range {v33 .. v33}, Landroid/app/Notification;-><init>()V

    .line 197
    .local v33, "tempNotification":Landroid/app/Notification;
    const-class v35, Landroid/app/Notification;

    const-string v36, "setLatestEventInfo"

    const/16 v37, 0x4

    move/from16 v0, v37

    new-array v0, v0, [Ljava/lang/Class;

    move-object/from16 v37, v0

    const/16 v38, 0x0

    const-class v39, Landroid/content/Context;

    aput-object v39, v37, v38

    const/16 v38, 0x1

    const-class v39, Ljava/lang/String;

    aput-object v39, v37, v38

    const/16 v38, 0x2

    const-class v39, Ljava/lang/String;

    aput-object v39, v37, v38

    const/16 v38, 0x3

    const-class v39, Landroid/app/PendingIntent;

    aput-object v39, v37, v38

    invoke-virtual/range {v35 .. v37}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v5

    .line 198
    .local v5, "__setLatestEventInfo":Ljava/lang/reflect/Method;
    const/16 v35, 0x4

    move/from16 v0, v35

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v35, v0

    const/16 v36, 0x0

    aput-object v32, v35, v36

    const/16 v36, 0x1

    invoke-direct/range {p0 .. p0}, Lcom/adobe/mobile/MessageNotificationHandler;->getAppName()Ljava/lang/String;

    move-result-object v37

    aput-object v37, v35, v36

    const/16 v36, 0x2

    aput-object v14, v35, v36

    const/16 v36, 0x3

    aput-object v25, v35, v36

    move-object/from16 v0, v33

    move-object/from16 v1, v35

    invoke-virtual {v5, v0, v1}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;

    .line 200
    const-class v35, Landroid/app/Notification;

    const-string v36, "icon"

    invoke-virtual/range {v35 .. v36}, Ljava/lang/Class;->getField(Ljava/lang/String;)Ljava/lang/reflect/Field;

    move-result-object v4

    .line 201
    .local v4, "__icon":Ljava/lang/reflect/Field;
    invoke-direct/range {p0 .. p0}, Lcom/adobe/mobile/MessageNotificationHandler;->getSmallIcon()I

    move-result v35

    invoke-static/range {v35 .. v35}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v35

    move-object/from16 v0, v33

    move-object/from16 v1, v35

    invoke-virtual {v4, v0, v1}, Ljava/lang/reflect/Field;->set(Ljava/lang/Object;Ljava/lang/Object;)V

    .line 202
    const/16 v35, 0x10

    move/from16 v0, v35

    move-object/from16 v1, v33

    iput v0, v1, Landroid/app/Notification;->flags:I
    :try_end_6
    .catch Ljava/lang/ClassNotFoundException; {:try_start_6 .. :try_end_6} :catch_1
    .catch Ljava/lang/NoSuchMethodException; {:try_start_6 .. :try_end_6} :catch_5
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_6 .. :try_end_6} :catch_6
    .catch Ljava/lang/Exception; {:try_start_6 .. :try_end_6} :catch_7

    .line 204
    move-object/from16 v16, v33

    .local v16, "notification":Landroid/app/Notification;
    goto/16 :goto_4

    .line 216
    .end local v4    # "__icon":Ljava/lang/reflect/Field;
    .end local v5    # "__setLatestEventInfo":Ljava/lang/reflect/Method;
    .end local v16    # "notification":Landroid/app/Notification;
    .end local v25    # "sender":Landroid/app/PendingIntent;
    .end local v33    # "tempNotification":Landroid/app/Notification;
    :catch_6
    move-exception v12

    .line 217
    .local v12, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v35, "Messages - error posting notification (%s)"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    invoke-virtual {v12}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v38

    aput-object v38, v36, v37

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 219
    .end local v12    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_7
    move-exception v12

    .line 220
    .local v12, "ex":Ljava/lang/Exception;
    const-string v35, "Messages - unexpected error posting notification (%s)"

    const/16 v36, 0x1

    move/from16 v0, v36

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v36, v0

    const/16 v37, 0x0

    invoke-virtual {v12}, Ljava/lang/Exception;->getMessage()Ljava/lang/String;

    move-result-object v38

    aput-object v38, v36, v37

    invoke-static/range {v35 .. v36}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0
.end method
