.class final Lcom/adobe/mobile/Lifecycle;
.super Ljava/lang/Object;
.source "Lifecycle.java"


# static fields
.field protected static final ADB_LIFECYCLE_PUSH_MESSAGE_ID_KEY:Ljava/lang/String; = "adb_m_id"

.field protected static final ADB_TRACK_INTERNAL_ADOBE_LINK:Ljava/lang/String; = "AdobeLink"

.field protected static final ADB_TRACK_INTERNAL_PUSH_CLICK_THROUGH:Ljava/lang/String; = "PushMessage"

.field private static final _contextDataMutex:Ljava/lang/Object;

.field private static final _lifecycleContextData:Ljava/util/HashMap;
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

.field private static final _lifecycleContextDataLowercase:Ljava/util/HashMap;
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

.field private static final _lowercaseContextDataMutex:Ljava/lang/Object;

.field private static final _previousSessionlifecycleContextData:Ljava/util/HashMap;
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

.field protected static volatile lifecycleHasRun:Z

.field protected static sessionStartTime:J


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .prologue
    .line 40
    const-wide/16 v0, 0x0

    sput-wide v0, Lcom/adobe/mobile/Lifecycle;->sessionStartTime:J

    .line 52
    const/4 v0, 0x0

    sput-boolean v0, Lcom/adobe/mobile/Lifecycle;->lifecycleHasRun:Z

    .line 53
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    .line 54
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    .line 55
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/adobe/mobile/Lifecycle;->_previousSessionlifecycleContextData:Ljava/util/HashMap;

    .line 232
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/Lifecycle;->_contextDataMutex:Ljava/lang/Object;

    .line 272
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/Lifecycle;->_lowercaseContextDataMutex:Ljava/lang/Object;

    return-void
.end method

.method constructor <init>()V
    .locals 0

    .prologue
    .line 34
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static addInstallData(Ljava/util/Map;J)V
    .locals 9
    .param p1, "date"    # J
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;J)V"
        }
    .end annotation

    .prologue
    .local p0, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v7, 0x0

    .line 402
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v4, "M/d/yyyy"

    sget-object v5, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v0, v4, v5}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 403
    .local v0, "dayMonthYearFormat":Ljava/text/DateFormat;
    const-string v4, "a.InstallDate"

    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v5

    invoke-virtual {v0, v5}, Ljava/text/DateFormat;->format(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    invoke-interface {p0, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 404
    const-string v4, "a.InstallEvent"

    const-string v5, "InstallEvent"

    invoke-interface {p0, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 405
    const-string v4, "a.DailyEngUserEvent"

    const-string v5, "DailyEngUserEvent"

    invoke-interface {p0, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 406
    const-string v4, "a.MonthlyEngUserEvent"

    const-string v5, "MonthlyEngUserEvent"

    invoke-interface {p0, v4, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 411
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v4

    const-string v5, "ADMS_Referrer_ContextData_Json_String"

    invoke-interface {v4, v5}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v4

    if-nez v4, :cond_0

    .line 412
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v4

    const-string v5, "utm_campaign"

    invoke-interface {v4, v5}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v4

    if-eqz v4, :cond_2

    .line 414
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->getReferrerDataFromSharedPreferences()Ljava/util/Map;

    move-result-object v3

    .line 415
    .local v3, "referrerData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz v3, :cond_1

    invoke-interface {v3}, Ljava/util/Map;->size()I

    move-result v4

    if-ltz v4, :cond_1

    .line 416
    invoke-interface {p0, v3}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 417
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v4

    sget-object v5, Lcom/adobe/mobile/Config$MobileDataEvent;->MOBILE_EVENT_ACQUISITION_INSTALL:Lcom/adobe/mobile/Config$MobileDataEvent;

    invoke-virtual {v4, v5, v3}, Lcom/adobe/mobile/MobileConfig;->invokeAdobeDataCallback(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V

    .line 426
    .end local v3    # "referrerData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_1
    :goto_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v2

    .line 428
    .local v2, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v4, "ADMS_InstallDate"

    invoke-interface {v2, v4, p1, p2}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 429
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 433
    .end local v2    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_1
    return-void

    .line 420
    :cond_2
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v4

    invoke-virtual {v4}, Lcom/adobe/mobile/MobileConfig;->mobileReferrerConfigured()Z

    move-result v4

    if-eqz v4, :cond_1

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v4

    invoke-virtual {v4}, Lcom/adobe/mobile/MobileConfig;->getReferrerTimeout()I

    move-result v4

    if-lez v4, :cond_1

    .line 422
    const/4 v4, 0x0

    invoke-static {v4}, Lcom/adobe/mobile/ReferrerHandler;->setReferrerProcessed(Z)V

    .line 423
    invoke-static {}, Lcom/adobe/mobile/Messages;->block3rdPartyCallbacksQueueForReferrer()V
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 430
    :catch_0
    move-exception v1

    .line 431
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Lifecycle - Error setting install data (%s)."

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method private static addLifecycleGenericData(Ljava/util/Map;J)V
    .locals 11
    .param p1, "date"    # J
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;J)V"
        }
    .end annotation

    .prologue
    .local p0, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v10, 0x0

    .line 462
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultData()Ljava/util/HashMap;

    move-result-object v7

    invoke-interface {p0, v7}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 463
    const-string v7, "a.LaunchEvent"

    const-string v8, "LaunchEvent"

    invoke-interface {p0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 464
    const-string v7, "a.OSVersion"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getOperatingSystem()Ljava/lang/String;

    move-result-object v8

    invoke-interface {p0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 466
    new-instance v5, Ljava/text/SimpleDateFormat;

    const-string v7, "H"

    sget-object v8, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct {v5, v7, v8}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 467
    .local v5, "hourOfDayDateFormat":Ljava/text/DateFormat;
    const-string v7, "a.HourOfDay"

    invoke-static {p1, p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v8

    invoke-virtual {v5, v8}, Ljava/text/DateFormat;->format(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v8

    invoke-interface {p0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 469
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v1

    .line 470
    .local v1, "cal":Ljava/util/Calendar;
    invoke-virtual {v1, p1, p2}, Ljava/util/Calendar;->setTimeInMillis(J)V

    .line 471
    const/4 v7, 0x7

    invoke-virtual {v1, v7}, Ljava/util/Calendar;->get(I)I

    move-result v2

    .line 472
    .local v2, "dow":I
    const-string v7, "a.DayOfWeek"

    invoke-static {v2}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v8

    invoke-interface {p0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 474
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAdvertisingIdentifier()Ljava/lang/String;

    move-result-object v0

    .line 475
    .local v0, "adid":Ljava/lang/String;
    if-eqz v0, :cond_0

    .line 476
    const-string v7, "a.adid"

    invoke-interface {p0, v7, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 480
    :cond_0
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v4

    .line 481
    .local v4, "editor":Landroid/content/SharedPreferences$Editor;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v7

    const-string v8, "ADMS_Launches"

    const/4 v9, 0x0

    invoke-interface {v7, v8, v9}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v7

    add-int/lit8 v6, v7, 0x1

    .line 482
    .local v6, "launches":I
    const-string v7, "a.Launches"

    invoke-static {v6}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v8

    invoke-interface {p0, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 484
    const-string v7, "ADMS_Launches"

    invoke-interface {v4, v7, v6}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    .line 485
    const-string v7, "ADMS_LastDateUsed"

    invoke-interface {v4, v7, p1, p2}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 486
    invoke-interface {v4}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    .line 490
    .end local v4    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v6    # "launches":I
    :goto_0
    return-void

    .line 487
    :catch_0
    move-exception v3

    .line 488
    .local v3, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v7, "Lifecycle - Error adding generic data (%s)."

    const/4 v8, 0x1

    new-array v8, v8, [Ljava/lang/Object;

    invoke-virtual {v3}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v10

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private static addNonInstallData(Ljava/util/Map;J)V
    .locals 27
    .param p1, "date"    # J
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;J)V"
        }
    .end annotation

    .prologue
    .line 496
    .local p0, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_0
    new-instance v7, Ljava/text/SimpleDateFormat;

    const-string v21, "yyyy/M/d"

    sget-object v22, Ljava/util/Locale;->US:Ljava/util/Locale;

    move-object/from16 v0, v21

    move-object/from16 v1, v22

    invoke-direct {v7, v0, v1}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 498
    .local v7, "dailyFormatter":Ljava/text/DateFormat;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v21

    const-string v22, "ADMS_LastDateUsed"

    const-wide/16 v24, 0x0

    move-object/from16 v0, v21

    move-object/from16 v1, v22

    move-wide/from16 v2, v24

    invoke-interface {v0, v1, v2, v3}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v16

    .line 499
    .local v16, "lastUseMillis":J
    new-instance v21, Ljava/util/Date;

    move-object/from16 v0, v21

    move-wide/from16 v1, v16

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    move-object/from16 v0, v21

    invoke-virtual {v7, v0}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v18

    .line 500
    .local v18, "lastUsedDateDaily":Ljava/lang/String;
    invoke-static/range {p1 .. p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v21

    move-object/from16 v0, v21

    invoke-virtual {v7, v0}, Ljava/text/DateFormat;->format(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    .line 502
    .local v5, "currentDateDaily":Ljava/lang/String;
    move-object/from16 v0, v18

    invoke-virtual {v5, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v21

    if-nez v21, :cond_0

    .line 503
    const-string v21, "a.DailyEngUserEvent"

    const-string v22, "DailyEngUserEvent"

    move-object/from16 v0, p0

    move-object/from16 v1, v21

    move-object/from16 v2, v22

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 507
    :cond_0
    new-instance v20, Ljava/text/SimpleDateFormat;

    const-string v21, "yyyy/M"

    sget-object v22, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-direct/range {v20 .. v22}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 508
    .local v20, "monthlyFormatter":Ljava/text/DateFormat;
    new-instance v21, Ljava/util/Date;

    move-object/from16 v0, v21

    move-wide/from16 v1, v16

    invoke-direct {v0, v1, v2}, Ljava/util/Date;-><init>(J)V

    invoke-virtual/range {v20 .. v21}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v19

    .line 509
    .local v19, "lastUsedDateMonthly":Ljava/lang/String;
    invoke-static/range {p1 .. p2}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v21

    invoke-virtual/range {v20 .. v21}, Ljava/text/DateFormat;->format(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v6

    .line 510
    .local v6, "currentDateMonthly":Ljava/lang/String;
    move-object/from16 v0, v19

    invoke-virtual {v6, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v21

    if-nez v21, :cond_1

    .line 511
    const-string v21, "a.MonthlyEngUserEvent"

    const-string v22, "MonthlyEngUserEvent"

    move-object/from16 v0, p0

    move-object/from16 v1, v21

    move-object/from16 v2, v22

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 515
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v21

    const-string v22, "ADMS_InstallDate"

    const-wide/16 v24, 0x0

    move-object/from16 v0, v21

    move-object/from16 v1, v22

    move-wide/from16 v2, v24

    invoke-interface {v0, v1, v2, v3}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v12

    .line 516
    .local v12, "installMillis":J
    const-string v21, "a.DaysSinceFirstUse"

    move-wide/from16 v0, p1

    invoke-static {v12, v13, v0, v1}, Lcom/adobe/mobile/Lifecycle;->calculateDaysSince(JJ)Ljava/lang/String;

    move-result-object v22

    move-object/from16 v0, p0

    move-object/from16 v1, v21

    move-object/from16 v2, v22

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 519
    const-string v21, "a.DaysSinceLastUse"

    move-wide/from16 v0, v16

    move-wide/from16 v2, p1

    invoke-static {v0, v1, v2, v3}, Lcom/adobe/mobile/Lifecycle;->calculateDaysSince(JJ)Ljava/lang/String;

    move-result-object v22

    move-object/from16 v0, p0

    move-object/from16 v1, v21

    move-object/from16 v2, v22

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 522
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v21

    const-string v22, "ADMS_SuccessfulClose"

    const/16 v23, 0x0

    invoke-interface/range {v21 .. v23}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v21

    if-nez v21, :cond_2

    .line 523
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v10

    .line 525
    .local v10, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v21, "ADMS_PauseDate"

    move-object/from16 v0, v21

    invoke-interface {v10, v0}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 526
    const-string v21, "ADMS_SessionStart"

    move-object/from16 v0, v21

    invoke-interface {v10, v0}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 527
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v22

    sput-wide v22, Lcom/adobe/mobile/Lifecycle;->sessionStartTime:J

    .line 528
    invoke-interface {v10}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 531
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v21

    const-string v22, "ADBLastKnownTimestampKey"

    const-wide/16 v24, 0x0

    move-object/from16 v0, v21

    move-object/from16 v1, v22

    move-wide/from16 v2, v24

    invoke-interface {v0, v1, v2, v3}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v14

    .line 533
    .local v14, "lastKnownTimestamp":J
    const-wide/16 v22, 0x0

    cmp-long v21, v14, v22

    if-lez v21, :cond_3

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v21

    invoke-virtual/range {v21 .. v21}, Lcom/adobe/mobile/MobileConfig;->mobileUsingAnalytics()Z

    move-result v21

    if-eqz v21, :cond_3

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v21

    invoke-virtual/range {v21 .. v21}, Lcom/adobe/mobile/MobileConfig;->getOfflineTrackingEnabled()Z

    move-result v21

    if-eqz v21, :cond_3

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v21

    invoke-virtual/range {v21 .. v21}, Lcom/adobe/mobile/MobileConfig;->getBackdateSessionInfoEnabled()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_1

    move-result v21

    if-eqz v21, :cond_3

    .line 535
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v8

    .line 536
    .local v8, "defaults":Landroid/content/SharedPreferences;
    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4}, Ljava/util/HashMap;-><init>()V

    .line 537
    .local v4, "crashCData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v21, "a.CrashEvent"

    const-string v22, "CrashEvent"

    move-object/from16 v0, v21

    move-object/from16 v1, v22

    invoke-virtual {v4, v0, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 538
    const-string v21, "a.OSVersion"

    const-string v22, "ADOBEMOBILE_STOREDDEFAULTS_OS"

    const-string v23, ""

    move-object/from16 v0, v22

    move-object/from16 v1, v23

    invoke-interface {v8, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v22

    move-object/from16 v0, v21

    move-object/from16 v1, v22

    invoke-virtual {v4, v0, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 539
    const-string v21, "a.AppID"

    const-string v22, "ADOBEMOBILE_STOREDDEFAULTS_APPID"

    const-string v23, ""

    move-object/from16 v0, v22

    move-object/from16 v1, v23

    invoke-interface {v8, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v22

    move-object/from16 v0, v21

    move-object/from16 v1, v22

    invoke-virtual {v4, v0, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 542
    const-string v21, "Crash"

    const-wide/16 v22, 0x1

    add-long v22, v22, v14

    move-object/from16 v0, v21

    move-wide/from16 v1, v22

    invoke-static {v0, v4, v1, v2}, Lcom/adobe/mobile/AnalyticsTrackInternal;->trackInternal(Ljava/lang/String;Ljava/util/Map;J)V

    .line 545
    sget-object v21, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    const-string v22, "a.CrashEvent"

    const-string v23, "CrashEvent"

    invoke-virtual/range {v21 .. v23}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0

    .line 555
    .end local v4    # "crashCData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v8    # "defaults":Landroid/content/SharedPreferences;
    :goto_0
    :try_start_2
    invoke-static {}, Lcom/adobe/mobile/AnalyticsTrackTimedAction;->sharedInstance()Lcom/adobe/mobile/AnalyticsTrackTimedAction;

    move-result-object v21

    invoke-virtual/range {v21 .. v21}, Lcom/adobe/mobile/AnalyticsTrackTimedAction;->trackTimedActionUpdateActionsClearAdjustedStartTime()V

    .line 560
    .end local v5    # "currentDateDaily":Ljava/lang/String;
    .end local v6    # "currentDateMonthly":Ljava/lang/String;
    .end local v7    # "dailyFormatter":Ljava/text/DateFormat;
    .end local v10    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v12    # "installMillis":J
    .end local v14    # "lastKnownTimestamp":J
    .end local v16    # "lastUseMillis":J
    .end local v18    # "lastUsedDateDaily":Ljava/lang/String;
    .end local v19    # "lastUsedDateMonthly":Ljava/lang/String;
    .end local v20    # "monthlyFormatter":Ljava/text/DateFormat;
    :cond_2
    :goto_1
    return-void

    .line 547
    .restart local v5    # "currentDateDaily":Ljava/lang/String;
    .restart local v6    # "currentDateMonthly":Ljava/lang/String;
    .restart local v7    # "dailyFormatter":Ljava/text/DateFormat;
    .restart local v10    # "editor":Landroid/content/SharedPreferences$Editor;
    .restart local v12    # "installMillis":J
    .restart local v14    # "lastKnownTimestamp":J
    .restart local v16    # "lastUseMillis":J
    .restart local v18    # "lastUsedDateDaily":Ljava/lang/String;
    .restart local v19    # "lastUsedDateMonthly":Ljava/lang/String;
    .restart local v20    # "monthlyFormatter":Ljava/text/DateFormat;
    :catch_0
    move-exception v11

    .line 548
    .local v11, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v21, "Config - Unable to get crash data for backdated hit (%s)"

    const/16 v22, 0x1

    move/from16 v0, v22

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v22, v0

    const/16 v23, 0x0

    invoke-virtual {v11}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v24

    aput-object v24, v22, v23

    invoke-static/range {v21 .. v22}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_2
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_2 .. :try_end_2} :catch_1

    goto :goto_0

    .line 557
    .end local v5    # "currentDateDaily":Ljava/lang/String;
    .end local v6    # "currentDateMonthly":Ljava/lang/String;
    .end local v7    # "dailyFormatter":Ljava/text/DateFormat;
    .end local v10    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v11    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .end local v12    # "installMillis":J
    .end local v14    # "lastKnownTimestamp":J
    .end local v16    # "lastUseMillis":J
    .end local v18    # "lastUsedDateDaily":Ljava/lang/String;
    .end local v19    # "lastUsedDateMonthly":Ljava/lang/String;
    .end local v20    # "monthlyFormatter":Ljava/text/DateFormat;
    :catch_1
    move-exception v9

    .line 558
    .local v9, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v21, "Lifecycle - Error setting non install data (%s)."

    const/16 v22, 0x1

    move/from16 v0, v22

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v22, v0

    const/16 v23, 0x0

    invoke-virtual {v9}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v24

    aput-object v24, v22, v23

    invoke-static/range {v21 .. v22}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 551
    .end local v9    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .restart local v5    # "currentDateDaily":Ljava/lang/String;
    .restart local v6    # "currentDateMonthly":Ljava/lang/String;
    .restart local v7    # "dailyFormatter":Ljava/text/DateFormat;
    .restart local v10    # "editor":Landroid/content/SharedPreferences$Editor;
    .restart local v12    # "installMillis":J
    .restart local v14    # "lastKnownTimestamp":J
    .restart local v16    # "lastUseMillis":J
    .restart local v18    # "lastUsedDateDaily":Ljava/lang/String;
    .restart local v19    # "lastUsedDateMonthly":Ljava/lang/String;
    .restart local v20    # "monthlyFormatter":Ljava/text/DateFormat;
    :cond_3
    :try_start_3
    const-string v21, "a.CrashEvent"

    const-string v22, "CrashEvent"

    move-object/from16 v0, p0

    move-object/from16 v1, v21

    move-object/from16 v2, v22

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_3
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_1

    goto :goto_0
.end method

.method private static addPersistedLifecycleToMap(Ljava/util/Map;)V
    .locals 9
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
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v8, 0x1

    const/4 v7, 0x0

    .line 301
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v4

    const-string v5, "ADMS_LifecycleData"

    const/4 v6, 0x0

    invoke-interface {v4, v5, v6}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 303
    .local v3, "lifecycleJSONString":Ljava/lang/String;
    if-eqz v3, :cond_0

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v4

    if-lez v4, :cond_0

    .line 304
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2, v3}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 305
    .local v2, "lifecycleData":Lorg/json/JSONObject;
    invoke-static {v2}, Lcom/adobe/mobile/StaticMethods;->mapFromJson(Lorg/json/JSONObject;)Ljava/util/HashMap;

    move-result-object v4

    invoke-interface {p0, v4}, Ljava/util/Map;->putAll(Ljava/util/Map;)V
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    .line 312
    .end local v2    # "lifecycleData":Lorg/json/JSONObject;
    .end local v3    # "lifecycleJSONString":Ljava/lang/String;
    :cond_0
    :goto_0
    return-void

    .line 307
    :catch_0
    move-exception v0

    .line 308
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Lifecycle - Issue loading persisted lifecycle data"

    new-array v5, v8, [Ljava/lang/Object;

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 309
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_1
    move-exception v1

    .line 310
    .local v1, "ex":Lorg/json/JSONException;
    const-string v4, "Lifecycle - Issue loading persisted lifecycle data (%s)"

    new-array v5, v8, [Ljava/lang/Object;

    invoke-virtual {v1}, Lorg/json/JSONException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private static addSessionLengthData(Ljava/util/Map;)V
    .locals 22
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
    .line 565
    .local p0, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v18

    const-string v19, "ADMS_PauseDate"

    const-wide/16 v20, 0x0

    invoke-interface/range {v18 .. v21}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v10

    .line 566
    .local v10, "pauseDate":J
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v18

    invoke-virtual/range {v18 .. v18}, Lcom/adobe/mobile/MobileConfig;->getLifecycleTimeout()I

    move-result v16

    .line 567
    .local v16, "sessionTimeout":I
    new-instance v18, Ljava/util/Date;

    invoke-direct/range {v18 .. v18}, Ljava/util/Date;-><init>()V

    invoke-virtual/range {v18 .. v18}, Ljava/util/Date;->getTime()J

    move-result-wide v18

    sub-long v18, v18, v10

    const-wide/16 v20, 0x3e8

    div-long v18, v18, v20

    move-wide/from16 v0, v18

    long-to-int v0, v0

    move/from16 v17, v0

    .line 570
    .local v17, "timeSincePause":I
    move/from16 v0, v17

    move/from16 v1, v16

    if-ge v0, v1, :cond_0

    .line 620
    .end local v10    # "pauseDate":J
    .end local v16    # "sessionTimeout":I
    .end local v17    # "timeSincePause":I
    :goto_0
    return-void

    .line 574
    .restart local v10    # "pauseDate":J
    .restart local v16    # "sessionTimeout":I
    .restart local v17    # "timeSincePause":I
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v18

    const-string v19, "ADMS_SessionStart"

    const-wide/16 v20, 0x0

    invoke-interface/range {v18 .. v21}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v14

    .line 575
    .local v14, "sessionStart":J
    sub-long v18, v10, v14

    const-wide/16 v20, 0x3e8

    div-long v18, v18, v20

    move-wide/from16 v0, v18

    long-to-int v13, v0

    .line 576
    .local v13, "sessionTime":I
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v18

    sput-wide v18, Lcom/adobe/mobile/Lifecycle;->sessionStartTime:J

    .line 579
    if-lez v13, :cond_2

    const v18, 0x93a80

    move/from16 v0, v18

    if-ge v13, v0, :cond_2

    .line 581
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v18

    const-string v19, "ADBLastKnownTimestampKey"

    const-wide/16 v20, 0x0

    invoke-interface/range {v18 .. v21}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v8

    .line 584
    .local v8, "lastKnownTimestamp":J
    const-wide/16 v18, 0x0

    cmp-long v18, v8, v18

    if-lez v18, :cond_1

    .line 585
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v18

    invoke-virtual/range {v18 .. v18}, Lcom/adobe/mobile/MobileConfig;->mobileUsingAnalytics()Z

    move-result v18

    if-eqz v18, :cond_1

    .line 586
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v18

    invoke-virtual/range {v18 .. v18}, Lcom/adobe/mobile/MobileConfig;->getOfflineTrackingEnabled()Z

    move-result v18

    if-eqz v18, :cond_1

    .line 587
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v18

    invoke-virtual/range {v18 .. v18}, Lcom/adobe/mobile/MobileConfig;->getBackdateSessionInfoEnabled()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result v18

    if-eqz v18, :cond_1

    .line 590
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v4

    .line 591
    .local v4, "defaults":Landroid/content/SharedPreferences;
    new-instance v12, Ljava/util/HashMap;

    invoke-direct {v12}, Ljava/util/HashMap;-><init>()V

    .line 592
    .local v12, "sessionCData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v18, "a.PrevSessionLength"

    invoke-static {v13}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v19

    move-object/from16 v0, v18

    move-object/from16 v1, v19

    invoke-virtual {v12, v0, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 593
    const-string v18, "a.OSVersion"

    const-string v19, "ADOBEMOBILE_STOREDDEFAULTS_OS"

    const-string v20, ""

    move-object/from16 v0, v19

    move-object/from16 v1, v20

    invoke-interface {v4, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v19

    move-object/from16 v0, v18

    move-object/from16 v1, v19

    invoke-virtual {v12, v0, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 594
    const-string v18, "a.AppID"

    const-string v19, "ADOBEMOBILE_STOREDDEFAULTS_APPID"

    const-string v20, ""

    move-object/from16 v0, v19

    move-object/from16 v1, v20

    invoke-interface {v4, v0, v1}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v19

    move-object/from16 v0, v18

    move-object/from16 v1, v19

    invoke-virtual {v12, v0, v1}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 597
    const-string v18, "SessionInfo"

    const-wide/16 v20, 0x1

    add-long v20, v20, v8

    move-object/from16 v0, v18

    move-wide/from16 v1, v20

    invoke-static {v0, v12, v1, v2}, Lcom/adobe/mobile/AnalyticsTrackInternal;->trackInternal(Ljava/lang/String;Ljava/util/Map;J)V

    .line 600
    sget-object v18, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    const-string v19, "a.PrevSessionLength"

    invoke-static {v13}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v20

    invoke-virtual/range {v18 .. v20}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_1

    .line 613
    .end local v4    # "defaults":Landroid/content/SharedPreferences;
    .end local v8    # "lastKnownTimestamp":J
    .end local v12    # "sessionCData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :goto_1
    :try_start_2
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v6

    .line 615
    .local v6, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v18, "ADMS_SessionStart"

    move-object/from16 v0, v18

    invoke-interface {v6, v0}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 616
    invoke-interface {v6}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_2
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_2 .. :try_end_2} :catch_0

    goto/16 :goto_0

    .line 617
    .end local v6    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v10    # "pauseDate":J
    .end local v13    # "sessionTime":I
    .end local v14    # "sessionStart":J
    .end local v16    # "sessionTimeout":I
    .end local v17    # "timeSincePause":I
    :catch_0
    move-exception v5

    .line 618
    .local v5, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v18, "Lifecycle - Error adding session length data (%s)."

    const/16 v19, 0x1

    move/from16 v0, v19

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v19, v0

    const/16 v20, 0x0

    invoke-virtual {v5}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v21

    aput-object v21, v19, v20

    invoke-static/range {v18 .. v19}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 602
    .end local v5    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .restart local v8    # "lastKnownTimestamp":J
    .restart local v10    # "pauseDate":J
    .restart local v13    # "sessionTime":I
    .restart local v14    # "sessionStart":J
    .restart local v16    # "sessionTimeout":I
    .restart local v17    # "timeSincePause":I
    :catch_1
    move-exception v7

    .line 603
    .local v7, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_3
    const-string v18, "Config - Unable to get session data for backdated hit (%s)"

    const/16 v19, 0x1

    move/from16 v0, v19

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v19, v0

    const/16 v20, 0x0

    invoke-virtual {v7}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v21

    aput-object v21, v19, v20

    invoke-static/range {v18 .. v19}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 606
    .end local v7    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :cond_1
    const-string v18, "a.PrevSessionLength"

    invoke-static {v13}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v19

    move-object/from16 v0, p0

    move-object/from16 v1, v18

    move-object/from16 v2, v19

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 610
    .end local v8    # "lastKnownTimestamp":J
    :cond_2
    const-string v18, "a.ignoredSessionLength"

    invoke-static {v13}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v19

    move-object/from16 v0, p0

    move-object/from16 v1, v18

    move-object/from16 v2, v19

    invoke-interface {v0, v1, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_3
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_0

    goto :goto_1
.end method

.method private static addUpgradeData(Ljava/util/Map;J)V
    .locals 11
    .param p1, "date"    # J
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;J)V"
        }
    .end annotation

    .prologue
    .line 437
    .local p0, "cdata":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 439
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v3

    const-string v6, "ADMS_UpgradeDate"

    const-wide/16 v8, 0x0

    invoke-interface {v3, v6, v8, v9}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v4

    .line 441
    .local v4, "upgradeDate":J
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationVersion()Ljava/lang/String;

    move-result-object v3

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v6

    const-string v7, "ADMS_LastVersion"

    const-string v8, ""

    invoke-interface {v6, v7, v8}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v3, v6}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v3

    if-nez v3, :cond_2

    .line 442
    const-string v3, "a.UpgradeEvent"

    const-string v6, "UpgradeEvent"

    invoke-interface {p0, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 443
    const-string v3, "ADMS_UpgradeDate"

    invoke-interface {v1, v3, p1, p2}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 444
    const-string v3, "ADMS_LaunchesAfterUpgrade"

    const/4 v6, 0x0

    invoke-interface {v1, v3, v6}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    .line 449
    :cond_0
    :goto_0
    const-wide/16 v6, 0x0

    cmp-long v3, v4, v6

    if-lez v3, :cond_1

    .line 450
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v3

    const-string v6, "ADMS_LaunchesAfterUpgrade"

    const/4 v7, 0x0

    invoke-interface {v3, v6, v7}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v3

    add-int/lit8 v2, v3, 0x1

    .line 451
    .local v2, "newLaunchesAfterUpgradeCount":I
    const-string v3, "a.LaunchesSinceUpgrade"

    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, ""

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    invoke-interface {p0, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 452
    const-string v3, "ADMS_LaunchesAfterUpgrade"

    invoke-interface {v1, v3, v2}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    .line 455
    .end local v2    # "newLaunchesAfterUpgradeCount":I
    :cond_1
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 459
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v4    # "upgradeDate":J
    :goto_1
    return-void

    .line 445
    .restart local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    .restart local v4    # "upgradeDate":J
    :cond_2
    const-wide/16 v6, 0x0

    cmp-long v3, v4, v6

    if-lez v3, :cond_0

    .line 446
    const-string v3, "a.DaysSinceLastUpgrade"

    invoke-static {v4, v5, p1, p2}, Lcom/adobe/mobile/Lifecycle;->calculateDaysSince(JJ)Ljava/lang/String;

    move-result-object v6

    invoke-interface {p0, v3, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 456
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    .end local v4    # "upgradeDate":J
    :catch_0
    move-exception v0

    .line 457
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v3, "Lifecycle - Error setting upgrade data (%s)."

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v3, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method private static calculateDaysSince(JJ)Ljava/lang/String;
    .locals 4
    .param p0, "since"    # J
    .param p2, "to"    # J

    .prologue
    .line 624
    sub-long v0, p2, p0

    const-wide/32 v2, 0x5265c00

    div-long/2addr v0, v2

    long-to-int v0, v0

    invoke-static {v0}, Ljava/lang/Integer;->toString(I)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method private static checkForAdobeClickThrough(Landroid/app/Activity;Z)V
    .locals 10
    .param p0, "activity"    # Landroid/app/Activity;
    .param p1, "isNewSession"    # Z

    .prologue
    .line 633
    if-nez p0, :cond_1

    .line 676
    :cond_0
    :goto_0
    return-void

    .line 636
    :cond_1
    invoke-virtual {p0}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v3

    .line 637
    .local v3, "intent":Landroid/content/Intent;
    if-eqz v3, :cond_0

    .line 640
    const/4 v0, 0x0

    .line 641
    .local v0, "action":Ljava/lang/String;
    const-string v7, "adb_m_id"

    invoke-virtual {v3, v7}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 642
    .local v5, "pushMessageID":Ljava/lang/String;
    const-string v7, "adb_m_l_id"

    invoke-virtual {v3, v7}, Landroid/content/Intent;->getStringExtra(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    .line 645
    .local v4, "messageID":Ljava/lang/String;
    invoke-virtual {v3}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v6

    .line 647
    .local v6, "uri":Landroid/net/Uri;
    invoke-static {v6}, Lcom/adobe/mobile/Lifecycle;->getAdobeDeepLinkQueryParameters(Landroid/net/Uri;)Ljava/util/Map;

    move-result-object v2

    .line 648
    .local v2, "deepLinkData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 651
    .local v1, "cData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    if-nez p1, :cond_2

    if-eqz v2, :cond_2

    .line 652
    const-string v0, "AdobeLink"

    .line 653
    invoke-virtual {v1, v2}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 654
    invoke-static {v1}, Lcom/adobe/mobile/Lifecycle;->updateContextData(Ljava/util/Map;)V

    .line 658
    :cond_2
    if-eqz v5, :cond_4

    invoke-virtual {v5}, Ljava/lang/String;->length()I

    move-result v7

    if-lez v7, :cond_4

    .line 659
    const-string v7, "a.push.payloadId"

    invoke-virtual {v1, v7, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 660
    const-string v0, "PushMessage"

    .line 661
    invoke-static {v1}, Lcom/adobe/mobile/Lifecycle;->updateContextData(Ljava/util/Map;)V

    .line 671
    :cond_3
    :goto_1
    if-eqz v0, :cond_0

    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v7

    invoke-virtual {v7}, Lcom/adobe/mobile/MobileConfig;->mobileUsingAnalytics()Z

    move-result v7

    if-eqz v7, :cond_0

    .line 674
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v8

    .line 672
    invoke-static {v0, v1, v8, v9}, Lcom/adobe/mobile/AnalyticsTrackInternal;->trackInternal(Ljava/lang/String;Ljava/util/Map;J)V

    goto :goto_0

    .line 662
    :cond_4
    if-eqz v4, :cond_3

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v7

    if-lez v7, :cond_3

    .line 663
    const-string v7, "a.message.id"

    invoke-virtual {v1, v7, v4}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 664
    const-string v7, "a.message.clicked"

    const/4 v8, 0x1

    invoke-static {v8}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v8

    invoke-virtual {v1, v7, v8}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 665
    const-string v0, "In-App Message"

    .line 666
    invoke-static {v1}, Lcom/adobe/mobile/Lifecycle;->updateContextData(Ljava/util/Map;)V

    goto :goto_1
.end method

.method private static checkForAdobeLinkData(Landroid/app/Activity;)Ljava/util/Map;
    .locals 3
    .param p0, "activity"    # Landroid/app/Activity;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            ")",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    const/4 v1, 0x0

    .line 680
    if-nez p0, :cond_1

    .line 692
    :cond_0
    :goto_0
    return-object v1

    .line 683
    :cond_1
    invoke-virtual {p0}, Landroid/app/Activity;->getIntent()Landroid/content/Intent;

    move-result-object v0

    .line 684
    .local v0, "intent":Landroid/content/Intent;
    if-eqz v0, :cond_0

    .line 687
    invoke-virtual {v0}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v2

    .line 688
    .local v2, "uri":Landroid/net/Uri;
    if-eqz v2, :cond_0

    .line 690
    invoke-static {v2}, Lcom/adobe/mobile/Lifecycle;->getAdobeDeepLinkQueryParameters(Landroid/net/Uri;)Ljava/util/Map;

    move-result-object v1

    .line 692
    .local v1, "queryParameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    goto :goto_0
.end method

.method private static checkReferrerDataForLaunch()V
    .locals 3

    .prologue
    .line 354
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->getReferrerDataFromSharedPreferences()Ljava/util/Map;

    move-result-object v0

    .line 355
    .local v0, "referrerData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz v0, :cond_0

    invoke-interface {v0}, Ljava/util/Map;->size()I

    move-result v1

    if-nez v1, :cond_1

    .line 360
    :cond_0
    :goto_0
    return-void

    .line 358
    :cond_1
    invoke-static {v0}, Lcom/adobe/mobile/Lifecycle;->updateContextData(Ljava/util/Map;)V

    .line 359
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v1

    sget-object v2, Lcom/adobe/mobile/Config$MobileDataEvent;->MOBILE_EVENT_ACQUISITION_LAUNCH:Lcom/adobe/mobile/Config$MobileDataEvent;

    invoke-virtual {v1, v2, v0}, Lcom/adobe/mobile/MobileConfig;->invokeAdobeDataCallback(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V

    goto :goto_0
.end method

.method private static clearContextDataLowercase()V
    .locals 2

    .prologue
    .line 290
    sget-object v1, Lcom/adobe/mobile/Lifecycle;->_lowercaseContextDataMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 291
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->clear()V

    .line 292
    monitor-exit v1

    .line 293
    return-void

    .line 292
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method private static generateLifecycleToBeSaved(Ljava/util/Map;)V
    .locals 6
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
    .line 315
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p0, :cond_0

    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1, p0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 318
    .local v1, "mutableData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :goto_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultData()Ljava/util/HashMap;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 319
    const-string v2, "a.locale"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultAcceptLanguage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 322
    const-string v2, "a.ltv.amount"

    invoke-static {}, Lcom/adobe/mobile/AnalyticsTrackLifetimeValueIncrease;->getLifetimeValue()Ljava/math/BigDecimal;

    move-result-object v3

    invoke-virtual {v1, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 324
    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-virtual {v2, v1}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 327
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->clearContextDataLowercase()V

    .line 328
    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-virtual {v2}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_1
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_1

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 329
    .local v0, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    sget-object v4, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v4, v2, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_1

    .line 315
    .end local v0    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v1    # "mutableData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_0
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    goto :goto_0

    .line 331
    .restart local v1    # "mutableData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_1
    return-void
.end method

.method private static getAdobeDeepLinkQueryParameters(Landroid/net/Uri;)Ljava/util/Map;
    .locals 10
    .param p0, "uri"    # Landroid/net/Uri;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/net/Uri;",
            ")",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    const/4 v9, -0x1

    const/4 v6, 0x0

    .line 696
    if-nez p0, :cond_1

    move-object v2, v6

    .line 723
    :cond_0
    :goto_0
    return-object v2

    .line 698
    :cond_1
    invoke-virtual {p0}, Landroid/net/Uri;->getQuery()Ljava/lang/String;

    move-result-object v3

    .line 699
    .local v3, "query":Ljava/lang/String;
    if-eqz v3, :cond_2

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v7

    if-gtz v7, :cond_3

    :cond_2
    move-object v2, v6

    .line 700
    goto :goto_0

    .line 703
    :cond_3
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 704
    .local v2, "parameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const/4 v5, 0x0

    .line 706
    .local v5, "start":I
    :cond_4
    const/16 v7, 0x26

    invoke-virtual {v3, v7, v5}, Ljava/lang/String;->indexOf(II)I

    move-result v1

    .line 707
    .local v1, "next":I
    if-ne v1, v9, :cond_7

    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v0

    .line 709
    .local v0, "end":I
    :goto_1
    const/16 v7, 0x3d

    invoke-virtual {v3, v7, v5}, Ljava/lang/String;->indexOf(II)I

    move-result v4

    .line 710
    .local v4, "separator":I
    if-gt v4, v0, :cond_5

    if-ne v4, v9, :cond_6

    .line 711
    :cond_5
    move v4, v0

    .line 714
    :cond_6
    invoke-virtual {v3, v5, v4}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v7

    add-int/lit8 v8, v4, 0x1

    invoke-virtual {v3, v8, v0}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v8

    invoke-interface {v2, v7, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 717
    add-int/lit8 v5, v0, 0x1

    .line 718
    invoke-virtual {v3}, Ljava/lang/String;->length()I

    move-result v7

    if-lt v5, v7, :cond_4

    .line 720
    const-string v7, "a.deeplink.id"

    invoke-interface {v2, v7}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v7

    if-nez v7, :cond_0

    move-object v2, v6

    .line 723
    goto :goto_0

    .end local v0    # "end":I
    .end local v4    # "separator":I
    :cond_7
    move v0, v1

    .line 707
    goto :goto_1
.end method

.method protected static getContextData()Ljava/util/HashMap;
    .locals 3
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
    .line 234
    sget-object v1, Lcom/adobe/mobile/Lifecycle;->_contextDataMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 236
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->size()I

    move-result v0

    if-lez v0, :cond_0

    .line 237
    new-instance v0, Ljava/util/HashMap;

    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-direct {v0, v2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    monitor-exit v1

    .line 246
    :goto_0
    return-object v0

    .line 240
    :cond_0
    sget-object v0, Lcom/adobe/mobile/Lifecycle;->_previousSessionlifecycleContextData:Ljava/util/HashMap;

    invoke-virtual {v0}, Ljava/util/HashMap;->size()I

    move-result v0

    if-lez v0, :cond_1

    .line 241
    new-instance v0, Ljava/util/HashMap;

    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_previousSessionlifecycleContextData:Ljava/util/HashMap;

    invoke-direct {v0, v2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    monitor-exit v1

    goto :goto_0

    .line 247
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0

    .line 244
    :cond_1
    :try_start_1
    sget-object v0, Lcom/adobe/mobile/Lifecycle;->_previousSessionlifecycleContextData:Ljava/util/HashMap;

    invoke-static {v0}, Lcom/adobe/mobile/Lifecycle;->addPersistedLifecycleToMap(Ljava/util/Map;)V

    .line 246
    new-instance v0, Ljava/util/HashMap;

    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_previousSessionlifecycleContextData:Ljava/util/HashMap;

    invoke-direct {v0, v2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    goto :goto_0
.end method

.method protected static getContextDataLowercase()Ljava/util/Map;
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
    .line 275
    sget-object v3, Lcom/adobe/mobile/Lifecycle;->_lowercaseContextDataMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 276
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    invoke-virtual {v2}, Ljava/util/HashMap;->size()I

    move-result v2

    if-gtz v2, :cond_0

    .line 278
    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    .line 279
    .local v1, "previousSessionlifecycleContextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-static {v1}, Lcom/adobe/mobile/Lifecycle;->addPersistedLifecycleToMap(Ljava/util/Map;)V

    .line 280
    invoke-virtual {v1}, Ljava/util/HashMap;->entrySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :goto_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 281
    .local v0, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    sget-object v5, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    invoke-virtual {v2}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v2

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v6

    invoke-virtual {v5, v2, v6}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 286
    .end local v0    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v2

    .line 285
    :cond_0
    :try_start_1
    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    monitor-exit v3
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    return-object v2
.end method

.method private static getReferrerDataFromSharedPreferences()Ljava/util/Map;
    .locals 14
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
    const/4 v9, 0x0

    .line 365
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "ADMS_Referrer_ContextData_Json_String"

    invoke-interface {v10, v11}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v10

    if-eqz v10, :cond_0

    .line 367
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "ADMS_Referrer_ContextData_Json_String"

    const/4 v12, 0x0

    invoke-interface {v10, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    .line 368
    .local v5, "responseString":Ljava/lang/String;
    invoke-static {v5}, Lcom/adobe/mobile/ReferrerHandler;->processV3ResponseAndReturnContextData(Ljava/lang/String;)Ljava/util/HashMap;

    move-result-object v4

    .line 397
    .end local v5    # "responseString":Ljava/lang/String;
    .local v6, "source":Ljava/lang/String;
    :goto_0
    return-object v4

    .line 370
    .end local v6    # "source":Ljava/lang/String;
    :cond_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "utm_campaign"

    invoke-interface {v10, v11}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v10

    if-eqz v10, :cond_1

    .line 372
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "utm_source"

    const/4 v12, 0x0

    invoke-interface {v10, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 373
    .restart local v6    # "source":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "utm_medium"

    const/4 v12, 0x0

    invoke-interface {v10, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 374
    .local v3, "medium":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "utm_term"

    const/4 v12, 0x0

    invoke-interface {v10, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 375
    .local v7, "term":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "utm_content"

    const/4 v12, 0x0

    invoke-interface {v10, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 376
    .local v1, "content":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "utm_campaign"

    const/4 v12, 0x0

    invoke-interface {v10, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 377
    .local v0, "campaign":Ljava/lang/String;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v10

    const-string v11, "trackingcode"

    const/4 v12, 0x0

    invoke-interface {v10, v11, v12}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    .line 379
    .local v8, "trackingcode":Ljava/lang/String;
    if-eqz v6, :cond_1

    if-eqz v0, :cond_1

    .line 380
    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4}, Ljava/util/HashMap;-><init>()V

    .line 382
    .local v4, "referrerContextData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v10, "a.referrer.campaign.source"

    invoke-interface {v4, v10, v6}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 383
    const-string v10, "a.referrer.campaign.medium"

    invoke-interface {v4, v10, v3}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 384
    const-string v10, "a.referrer.campaign.term"

    invoke-interface {v4, v10, v7}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 385
    const-string v10, "a.referrer.campaign.content"

    invoke-interface {v4, v10, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 386
    const-string v10, "a.referrer.campaign.name"

    invoke-interface {v4, v10, v0}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 387
    const-string v10, "a.referrer.campaign.trackingcode"

    invoke-interface {v4, v10, v8}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 393
    .end local v0    # "campaign":Ljava/lang/String;
    .end local v1    # "content":Ljava/lang/String;
    .end local v3    # "medium":Ljava/lang/String;
    .end local v4    # "referrerContextData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v7    # "term":Ljava/lang/String;
    .end local v8    # "trackingcode":Ljava/lang/String;
    :catch_0
    move-exception v2

    .line 394
    .local v2, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v10, "Lifecycle - Error pulling persisted Acquisition data (%s)"

    const/4 v11, 0x1

    new-array v11, v11, [Ljava/lang/Object;

    const/4 v12, 0x0

    invoke-virtual {v2}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v13

    aput-object v13, v11, v12

    invoke-static {v10, v11}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .end local v2    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :cond_1
    move-object v4, v9

    .line 397
    goto/16 :goto_0
.end method

.method private static persistLifecycleContextData()V
    .locals 7

    .prologue
    .line 222
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 223
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    new-instance v2, Lorg/json/JSONObject;

    sget-object v3, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-direct {v2, v3}, Lorg/json/JSONObject;-><init>(Ljava/util/Map;)V

    .line 224
    .local v2, "lifecycleJSON":Lorg/json/JSONObject;
    const-string v3, "ADMS_LifecycleData"

    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-interface {v1, v3, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 225
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    .line 230
    .end local v2    # "lifecycleJSON":Lorg/json/JSONObject;
    :goto_0
    return-void

    .line 227
    :catch_0
    move-exception v0

    .line 228
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v3, "Lifecycle - Error persisting lifecycle data (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static removeContextData(Ljava/lang/String;)V
    .locals 3
    .param p0, "key"    # Ljava/lang/String;

    .prologue
    .line 263
    sget-object v1, Lcom/adobe/mobile/Lifecycle;->_contextDataMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 264
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-virtual {v0, p0}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 265
    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 267
    sget-object v1, Lcom/adobe/mobile/Lifecycle;->_lowercaseContextDataMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 268
    :try_start_1
    sget-object v0, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    invoke-virtual {p0}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/util/HashMap;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    .line 269
    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_1

    .line 270
    return-void

    .line 265
    :catchall_0
    move-exception v0

    :try_start_2
    monitor-exit v1
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v0

    .line 269
    :catchall_1
    move-exception v0

    :try_start_3
    monitor-exit v1
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    throw v0
.end method

.method private static resetLifecycleFlags(J)V
    .locals 6
    .param p0, "date"    # J

    .prologue
    const/4 v5, 0x0

    .line 335
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 337
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v2

    const-string v3, "ADMS_SessionStart"

    invoke-interface {v2, v3}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 338
    const-string v2, "ADMS_SessionStart"

    invoke-interface {v1, v2, p0, p1}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 341
    const-wide/16 v2, 0x3e8

    div-long v2, p0, v2

    sput-wide v2, Lcom/adobe/mobile/Lifecycle;->sessionStartTime:J

    .line 344
    :cond_0
    const-string v2, "ADMS_LastVersion"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationVersion()Ljava/lang/String;

    move-result-object v3

    invoke-interface {v1, v2, v3}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 345
    const-string v2, "ADMS_SuccessfulClose"

    const/4 v3, 0x0

    invoke-interface {v1, v2, v3}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    .line 346
    const-string v2, "ADMS_PauseDate"

    invoke-interface {v1, v2}, Landroid/content/SharedPreferences$Editor;->remove(Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 347
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    .line 351
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_0
    return-void

    .line 348
    :catch_0
    move-exception v0

    .line 349
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Lifecycle - Error resetting lifecycle flags (%s)."

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v5

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static start(Landroid/app/Activity;Ljava/util/Map;)V
    .locals 26
    .param p0, "activity"    # Landroid/app/Activity;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/app/Activity;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 61
    .local p1, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    sget-boolean v20, Lcom/adobe/mobile/Lifecycle;->lifecycleHasRun:Z

    if-eqz v20, :cond_0

    .line 189
    :goto_0
    return-void

    .line 65
    :cond_0
    const/16 v20, 0x1

    sput-boolean v20, Lcom/adobe/mobile/Lifecycle;->lifecycleHasRun:Z

    .line 69
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v19

    .line 77
    .local v19, "userDefaults":Landroid/content/SharedPreferences;
    const/4 v6, 0x0

    .line 79
    .local v6, "currentActivity":Landroid/app/Activity;
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCurrentActivity()Landroid/app/Activity;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullActivityException; {:try_start_1 .. :try_end_1} :catch_2

    move-result-object v6

    .line 84
    :goto_1
    if-eqz v6, :cond_1

    if-eqz p0, :cond_1

    .line 85
    invoke-virtual {v6}, Landroid/app/Activity;->getComponentName()Landroid/content/ComponentName;

    move-result-object v20

    invoke-virtual/range {v20 .. v20}, Landroid/content/ComponentName;->toString()Ljava/lang/String;

    move-result-object v20

    invoke-virtual/range {p0 .. p0}, Landroid/app/Activity;->getComponentName()Landroid/content/ComponentName;

    move-result-object v21

    invoke-virtual/range {v21 .. v21}, Landroid/content/ComponentName;->toString()Ljava/lang/String;

    move-result-object v21

    invoke-virtual/range {v20 .. v21}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v20

    if-eqz v20, :cond_1

    .line 86
    const/16 v20, 0x0

    const/16 v21, 0x0

    const/16 v22, 0x0

    invoke-static/range {v20 .. v22}, Lcom/adobe/mobile/Messages;->checkForInAppMessage(Ljava/util/Map;Ljava/util/Map;Ljava/util/Map;)V

    .line 90
    :cond_1
    invoke-static/range {p0 .. p0}, Lcom/adobe/mobile/StaticMethods;->setCurrentActivity(Landroid/app/Activity;)V

    .line 92
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v14

    .line 93
    .local v14, "mobileConfig":Lcom/adobe/mobile/MobileConfig;
    const-string v20, "ADMS_PauseDate"

    const-wide/16 v22, 0x0

    move-object/from16 v0, v19

    move-object/from16 v1, v20

    move-wide/from16 v2, v22

    invoke-interface {v0, v1, v2, v3}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v12

    .line 94
    .local v12, "lastPauseDate":J
    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getLifecycleTimeout()I

    move-result v18

    .line 96
    .local v18, "sessionTimeout":I
    const/4 v11, 0x1

    .line 99
    .local v11, "isNewSession":Z
    const-wide/16 v20, 0x0

    cmp-long v20, v12, v20

    if-lez v20, :cond_2

    .line 100
    new-instance v20, Ljava/util/Date;

    invoke-direct/range {v20 .. v20}, Ljava/util/Date;-><init>()V

    invoke-virtual/range {v20 .. v20}, Ljava/util/Date;->getTime()J

    move-result-wide v20

    sub-long v20, v20, v12

    const-wide/16 v22, 0x3e8

    div-long v20, v20, v22

    move-wide/from16 v0, v20

    long-to-int v15, v0

    .line 101
    .local v15, "pausedTime":I
    const-string v20, "ADMS_SessionStart"

    const-wide/16 v22, 0x0

    move-object/from16 v0, v19

    move-object/from16 v1, v20

    move-wide/from16 v2, v22

    invoke-interface {v0, v1, v2, v3}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v16

    .line 104
    .local v16, "originalStartDate":J
    const-wide/16 v20, 0x3e8

    div-long v20, v16, v20

    sput-wide v20, Lcom/adobe/mobile/Lifecycle;->sessionStartTime:J

    .line 106
    invoke-static {}, Lcom/adobe/mobile/AnalyticsTrackTimedAction;->sharedInstance()Lcom/adobe/mobile/AnalyticsTrackTimedAction;

    move-result-object v20

    int-to-long v0, v15

    move-wide/from16 v22, v0

    move-object/from16 v0, v20

    move-wide/from16 v1, v22

    invoke-virtual {v0, v1, v2}, Lcom/adobe/mobile/AnalyticsTrackTimedAction;->trackTimedActionUpdateAdjustedStartTime(J)V

    .line 108
    move/from16 v0, v18

    if-ge v15, v0, :cond_2

    const-wide/16 v20, 0x0

    cmp-long v20, v16, v20

    if-lez v20, :cond_2

    .line 112
    :try_start_2
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v10

    .line 113
    .local v10, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v20, "ADMS_SessionStart"

    mul-int/lit16 v0, v15, 0x3e8

    move/from16 v21, v0

    move/from16 v0, v21

    int-to-long v0, v0

    move-wide/from16 v22, v0

    add-long v22, v22, v16

    move-object/from16 v0, v20

    move-wide/from16 v1, v22

    invoke-interface {v10, v0, v1, v2}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 114
    invoke-interface {v10}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_2
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_2 .. :try_end_2} :catch_1

    .line 120
    .end local v10    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_2
    const-string v20, "ADMS_SessionStart"

    const-wide/16 v22, 0x0

    move-object/from16 v0, v19

    move-object/from16 v1, v20

    move-wide/from16 v2, v22

    invoke-interface {v0, v1, v2, v3}, Landroid/content/SharedPreferences;->getLong(Ljava/lang/String;J)J

    move-result-wide v20

    const-wide/16 v22, 0x3e8

    div-long v20, v20, v22

    sput-wide v20, Lcom/adobe/mobile/Lifecycle;->sessionStartTime:J

    .line 122
    const/4 v11, 0x0

    .line 126
    .end local v15    # "pausedTime":I
    .end local v16    # "originalStartDate":J
    :cond_2
    new-instance v20, Ljava/util/Date;

    invoke-direct/range {v20 .. v20}, Ljava/util/Date;-><init>()V

    invoke-virtual/range {v20 .. v20}, Ljava/util/Date;->getTime()J

    move-result-wide v8

    .line 129
    .local v8, "date":J
    if-eqz v11, :cond_5

    .line 131
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v20

    const/16 v21, 0x0

    const/16 v22, 0x0

    const/16 v23, 0x0

    const/16 v24, 0x1

    invoke-virtual/range {v20 .. v24}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;Lcom/adobe/mobile/VisitorID$VisitorIDAuthenticationState;Z)V

    .line 134
    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->downloadRemoteConfigs()V

    .line 137
    sget-object v20, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-virtual/range {v20 .. v20}, Ljava/util/HashMap;->clear()V

    .line 139
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->clearContextDataLowercase()V

    .line 141
    if-eqz p1, :cond_6

    new-instance v5, Ljava/util/HashMap;

    move-object/from16 v0, p1

    invoke-direct {v5, v0}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    .line 144
    .local v5, "contextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :goto_3
    invoke-static/range {p0 .. p0}, Lcom/adobe/mobile/Lifecycle;->checkForAdobeLinkData(Landroid/app/Activity;)Ljava/util/Map;

    move-result-object v4

    .line 145
    .local v4, "clickThroughData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz v4, :cond_3

    .line 146
    invoke-virtual {v5, v4}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 150
    :cond_3
    const-string v20, "ADMS_InstallDate"

    invoke-interface/range {v19 .. v20}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v20

    if-nez v20, :cond_7

    .line 151
    invoke-static {v5, v8, v9}, Lcom/adobe/mobile/Lifecycle;->addInstallData(Ljava/util/Map;J)V

    .line 161
    :goto_4
    invoke-static {v5, v8, v9}, Lcom/adobe/mobile/Lifecycle;->addLifecycleGenericData(Ljava/util/Map;J)V

    .line 162
    invoke-static {v5}, Lcom/adobe/mobile/Lifecycle;->generateLifecycleToBeSaved(Ljava/util/Map;)V

    .line 169
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->persistLifecycleContextData()V

    .line 171
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v20

    sget-object v21, Lcom/adobe/mobile/Config$MobileDataEvent;->MOBILE_EVENT_LIFECYCLE:Lcom/adobe/mobile/Config$MobileDataEvent;

    move-object/from16 v0, v20

    move-object/from16 v1, v21

    invoke-virtual {v0, v1, v5}, Lcom/adobe/mobile/MobileConfig;->invokeAdobeDataCallback(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V

    .line 174
    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->mobileUsingAnalytics()Z

    move-result v20

    if-eqz v20, :cond_4

    .line 175
    const-string v20, "Lifecycle"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v22

    const-wide/16 v24, 0x1

    sub-long v22, v22, v24

    move-object/from16 v0, v20

    move-wide/from16 v1, v22

    invoke-static {v0, v5, v1, v2}, Lcom/adobe/mobile/AnalyticsTrackInternal;->trackInternal(Ljava/lang/String;Ljava/util/Map;J)V

    .line 179
    :cond_4
    invoke-virtual {v14}, Lcom/adobe/mobile/MobileConfig;->getAamAnalyticsForwardingEnabled()Z

    move-result v20

    if-nez v20, :cond_5

    .line 180
    sget-object v20, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    const/16 v21, 0x0

    invoke-static/range {v20 .. v21}, Lcom/adobe/mobile/AudienceManagerWorker;->SubmitSignal(Ljava/util/Map;Lcom/adobe/mobile/AudienceManager$AudienceManagerCallback;)V

    .line 186
    .end local v4    # "clickThroughData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v5    # "contextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_5
    move-object/from16 v0, p0

    invoke-static {v0, v11}, Lcom/adobe/mobile/Lifecycle;->checkForAdobeClickThrough(Landroid/app/Activity;Z)V

    .line 188
    invoke-static {v8, v9}, Lcom/adobe/mobile/Lifecycle;->resetLifecycleFlags(J)V

    goto/16 :goto_0

    .line 70
    .end local v6    # "currentActivity":Landroid/app/Activity;
    .end local v8    # "date":J
    .end local v11    # "isNewSession":Z
    .end local v12    # "lastPauseDate":J
    .end local v14    # "mobileConfig":Lcom/adobe/mobile/MobileConfig;
    .end local v18    # "sessionTimeout":I
    .end local v19    # "userDefaults":Landroid/content/SharedPreferences;
    :catch_0
    move-exception v7

    .line 71
    .local v7, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v20, "Lifecycle - Error starting lifecycle (%s)."

    const/16 v21, 0x1

    move/from16 v0, v21

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v21, v0

    const/16 v22, 0x0

    invoke-virtual {v7}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v23

    aput-object v23, v21, v22

    invoke-static/range {v20 .. v21}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 115
    .end local v7    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .restart local v6    # "currentActivity":Landroid/app/Activity;
    .restart local v11    # "isNewSession":Z
    .restart local v12    # "lastPauseDate":J
    .restart local v14    # "mobileConfig":Lcom/adobe/mobile/MobileConfig;
    .restart local v15    # "pausedTime":I
    .restart local v16    # "originalStartDate":J
    .restart local v18    # "sessionTimeout":I
    .restart local v19    # "userDefaults":Landroid/content/SharedPreferences;
    :catch_1
    move-exception v7

    .line 116
    .restart local v7    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v20, "Lifecycle - Error while updating start time (%s)."

    const/16 v21, 0x1

    move/from16 v0, v21

    new-array v0, v0, [Ljava/lang/Object;

    move-object/from16 v21, v0

    const/16 v22, 0x0

    invoke-virtual {v7}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v23

    aput-object v23, v21, v22

    invoke-static/range {v20 .. v21}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_2

    .line 141
    .end local v7    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .end local v15    # "pausedTime":I
    .end local v16    # "originalStartDate":J
    .restart local v8    # "date":J
    :cond_6
    new-instance v5, Ljava/util/HashMap;

    invoke-direct {v5}, Ljava/util/HashMap;-><init>()V

    goto/16 :goto_3

    .line 153
    .restart local v4    # "clickThroughData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .restart local v5    # "contextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :cond_7
    invoke-static {v5, v8, v9}, Lcom/adobe/mobile/Lifecycle;->addNonInstallData(Ljava/util/Map;J)V

    .line 154
    invoke-static {v5, v8, v9}, Lcom/adobe/mobile/Lifecycle;->addUpgradeData(Ljava/util/Map;J)V

    .line 155
    invoke-static {v5}, Lcom/adobe/mobile/Lifecycle;->addSessionLengthData(Ljava/util/Map;)V

    .line 157
    invoke-static {}, Lcom/adobe/mobile/Lifecycle;->checkReferrerDataForLaunch()V

    goto/16 :goto_4

    .line 81
    .end local v4    # "clickThroughData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v5    # "contextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v8    # "date":J
    .end local v11    # "isNewSession":Z
    .end local v12    # "lastPauseDate":J
    .end local v14    # "mobileConfig":Lcom/adobe/mobile/MobileConfig;
    .end local v18    # "sessionTimeout":I
    :catch_2
    move-exception v20

    goto/16 :goto_1
.end method

.method protected static stop()V
    .locals 10

    .prologue
    const/4 v9, 0x1

    const/4 v8, 0x0

    .line 193
    sput-boolean v8, Lcom/adobe/mobile/Lifecycle;->lifecycleHasRun:Z

    .line 195
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v4

    invoke-static {v4, v5}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v4

    invoke-static {v4}, Lcom/adobe/mobile/StaticMethods;->updateLastKnownTimestamp(Ljava/lang/Long;)V

    .line 198
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v2

    .line 199
    .local v2, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v4, "ADMS_SuccessfulClose"

    const/4 v5, 0x1

    invoke-interface {v2, v4, v5}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    .line 200
    const-string v4, "ADMS_PauseDate"

    new-instance v5, Ljava/util/Date;

    invoke-direct {v5}, Ljava/util/Date;-><init>()V

    invoke-virtual {v5}, Ljava/util/Date;->getTime()J

    move-result-wide v6

    invoke-interface {v2, v4, v6, v7}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 201
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    .line 209
    :goto_0
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCurrentActivity()Landroid/app/Activity;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullActivityException; {:try_start_1 .. :try_end_1} :catch_1

    move-result-object v0

    .line 215
    .local v0, "currentActivity":Landroid/app/Activity;
    invoke-virtual {v0}, Landroid/app/Activity;->isFinishing()Z

    move-result v4

    if-eqz v4, :cond_0

    .line 216
    invoke-static {}, Lcom/adobe/mobile/Messages;->resetAllInAppMessages()V

    .line 218
    .end local v0    # "currentActivity":Landroid/app/Activity;
    :cond_0
    :goto_1
    return-void

    .line 202
    :catch_0
    move-exception v1

    .line 203
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Lifecycle - Error updating lifecycle pause data (%s)"

    new-array v5, v9, [Ljava/lang/Object;

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v8

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 211
    .end local v1    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_1
    move-exception v3

    .line 212
    .local v3, "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    goto :goto_1
.end method

.method protected static updateContextData(Ljava/util/Map;)V
    .locals 6
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
    .line 251
    .local p0, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_contextDataMutex:Ljava/lang/Object;

    monitor-enter v2

    .line 252
    :try_start_0
    sget-object v1, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextData:Ljava/util/HashMap;

    invoke-virtual {v1, p0}, Ljava/util/HashMap;->putAll(Ljava/util/Map;)V

    .line 253
    monitor-exit v2
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    .line 255
    sget-object v2, Lcom/adobe/mobile/Lifecycle;->_lowercaseContextDataMutex:Ljava/lang/Object;

    monitor-enter v2

    .line 256
    :try_start_1
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v1

    invoke-interface {v1}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v1

    if-eqz v1, :cond_0

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 257
    .local v0, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    sget-object v4, Lcom/adobe/mobile/Lifecycle;->_lifecycleContextDataLowercase:Ljava/util/HashMap;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    invoke-virtual {v1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object v1

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v5

    invoke-virtual {v4, v1, v5}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0

    .line 259
    .end local v0    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    :catchall_0
    move-exception v1

    monitor-exit v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v1

    .line 253
    :catchall_1
    move-exception v1

    :try_start_2
    monitor-exit v2
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    throw v1

    .line 259
    :cond_0
    :try_start_3
    monitor-exit v2
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 260
    return-void
.end method
