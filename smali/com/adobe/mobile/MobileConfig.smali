.class final Lcom/adobe/mobile/MobileConfig;
.super Ljava/lang/Object;
.source "MobileConfig.java"


# static fields
.field private static final CONFIG_FILE_NAME:Ljava/lang/String; = "ADBMobileConfig.json"

.field private static final CONFIG_PRIVACY_OPTED_IN:Ljava/lang/String; = "optedin"

.field private static final CONFIG_PRIVACY_OPTED_OUT:Ljava/lang/String; = "optedout"

.field private static final CONFIG_PRIVACY_UNKNOWN:Ljava/lang/String; = "optunknown"

.field private static final DEFAULT_AAM_ANALYTICS_FORWARD_ENABLED:Z = false

.field private static final DEFAULT_AAM_TIMEOUT:I = 0x2

.field private static final DEFAULT_BACKDATE_SESSION_INFO:Z = true

.field private static final DEFAULT_BATCH_LIMIT:I = 0x0

.field private static final DEFAULT_CHARSET:Ljava/lang/String; = "UTF-8"

.field private static final DEFAULT_LIFECYCLE_TIMEOUT:I = 0x12c

.field private static final DEFAULT_LOCATION_TIMEOUT:I = 0x2

.field private static final DEFAULT_OFFLINE_TRACKING:Z = false

.field private static final DEFAULT_PRIVACY_STATUS:Lcom/adobe/mobile/MobilePrivacyStatus;

.field private static final DEFAULT_REFERRER_TIMEOUT:I = 0x0

.field private static final DEFAULT_SSL:Z = false

.field private static final JSON_CONFIG_AAM_ANALYTICS_FORWARD_KEY:Ljava/lang/String; = "analyticsForwardingEnabled"

.field private static final JSON_CONFIG_AAM_KEY:Ljava/lang/String; = "audienceManager"

.field private static final JSON_CONFIG_ACQUISITION_KEY:Ljava/lang/String; = "acquisition"

.field private static final JSON_CONFIG_ANALYTICS_KEY:Ljava/lang/String; = "analytics"

.field private static final JSON_CONFIG_APP_IDENTIFIER_KEY:Ljava/lang/String; = "appid"

.field private static final JSON_CONFIG_BACKDATE_SESSION_INFO_KEY:Ljava/lang/String; = "backdateSessionInfo"

.field private static final JSON_CONFIG_BATCH_LIMIT_KEY:Ljava/lang/String; = "batchLimit"

.field private static final JSON_CONFIG_CHAR_SET_KEY:Ljava/lang/String; = "charset"

.field private static final JSON_CONFIG_CLIENT_CODE_KEY:Ljava/lang/String; = "clientCode"

.field private static final JSON_CONFIG_LIFECYCLE_TIMEOUT_KEY:Ljava/lang/String; = "lifecycleTimeout"

.field private static final JSON_CONFIG_MARKETINGCLOUD_KEY:Ljava/lang/String; = "marketingCloud"

.field private static final JSON_CONFIG_MESSAGES_KEY:Ljava/lang/String; = "messages"

.field private static final JSON_CONFIG_MESSAGES_URL_KEY:Ljava/lang/String; = "messages"

.field private static final JSON_CONFIG_OFFLINE_TRACKING_KEY:Ljava/lang/String; = "offlineEnabled"

.field private static final JSON_CONFIG_ORGID_KEY:Ljava/lang/String; = "org"

.field private static final JSON_CONFIG_POINTS_OF_INTEREST_KEY:Ljava/lang/String; = "poi"

.field private static final JSON_CONFIG_POI_REMOTES_KEY:Ljava/lang/String; = "analytics.poi"

.field private static final JSON_CONFIG_PRIVACY_DEFAULT_KEY:Ljava/lang/String; = "privacyDefault"

.field private static final JSON_CONFIG_REFERRER_TIMEOUT_KEY:Ljava/lang/String; = "referrerTimeout"

.field private static final JSON_CONFIG_REMOTES_KEY:Ljava/lang/String; = "remotes"

.field private static final JSON_CONFIG_REPORT_SUITES_KEY:Ljava/lang/String; = "rsids"

.field private static final JSON_CONFIG_SERVER_KEY:Ljava/lang/String; = "server"

.field private static final JSON_CONFIG_SSL_KEY:Ljava/lang/String; = "ssl"

.field private static final JSON_CONFIG_TARGET_KEY:Ljava/lang/String; = "target"

.field private static final JSON_CONFIG_TIMEOUT_KEY:Ljava/lang/String; = "timeout"

.field private static _instance:Lcom/adobe/mobile/MobileConfig;

.field private static final _instanceMutex:Ljava/lang/Object;

.field private static _userDefinedInputStream:Ljava/io/InputStream;

.field private static final _userDefinedInputStreamMutex:Ljava/lang/Object;

.field private static final _usingAnalyticsMutex:Ljava/lang/Object;

.field private static final _usingAudienceManagerMutex:Ljava/lang/Object;

.field private static final _usingGooglePlayServicesMutex:Ljava/lang/Object;

.field private static final _usingTargetMutex:Ljava/lang/Object;


# instance fields
.field private _aamAnalyticsForwardingEnabled:Z

.field private _aamServer:Ljava/lang/String;

.field private _aamTimeout:I

.field private _acquisitionAppIdentifier:Ljava/lang/String;

.field private _acquisitionServer:Ljava/lang/String;

.field private _adobeDataCallback:Lcom/adobe/mobile/Config$AdobeDataCallback;

.field private _backdateSessionInfoEnabled:Z

.field private _batchLimit:I

.field private _callbackTemplates:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList",
            "<",
            "Lcom/adobe/mobile/Message;",
            ">;"
        }
    .end annotation
.end field

.field private _characterSet:Ljava/lang/String;

.field private _clientCode:Ljava/lang/String;

.field private _defaultLocationTimeout:I

.field private _inAppMessages:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList",
            "<",
            "Lcom/adobe/mobile/Message;",
            ">;"
        }
    .end annotation
.end field

.field private _lifecycleTimeout:I

.field private _marketingCloudOrganizationId:Ljava/lang/String;

.field private _messagesURL:Ljava/lang/String;

.field private _networkConnectivity:Z

.field private _offlineTrackingEnabled:Z

.field private _piiRequests:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList",
            "<",
            "Lcom/adobe/mobile/Message;",
            ">;"
        }
    .end annotation
.end field

.field private _pointsOfInterest:Ljava/util/List;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/List",
            "<",
            "Ljava/util/List",
            "<",
            "Ljava/lang/Object;",
            ">;>;"
        }
    .end annotation
.end field

.field private _pointsOfInterestURL:Ljava/lang/String;

.field private _privacyStatus:Lcom/adobe/mobile/MobilePrivacyStatus;

.field private _referrerTimeout:I

.field private _reportSuiteIds:Ljava/lang/String;

.field private _ssl:Z

.field private _trackingServer:Ljava/lang/String;

.field private _usingAnalytics:Ljava/lang/Boolean;

.field private _usingAudienceManager:Ljava/lang/Boolean;

.field private _usingGooglePlayServices:Ljava/lang/Boolean;

.field private _usingTarget:Ljava/lang/Boolean;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    .prologue
    const/4 v1, 0x0

    .line 91
    sget-object v0, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_IN:Lcom/adobe/mobile/MobilePrivacyStatus;

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->DEFAULT_PRIVACY_STATUS:Lcom/adobe/mobile/MobilePrivacyStatus;

    .line 151
    sput-object v1, Lcom/adobe/mobile/MobileConfig;->_instance:Lcom/adobe/mobile/MobileConfig;

    .line 152
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->_instanceMutex:Ljava/lang/Object;

    .line 409
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->_usingAnalyticsMutex:Ljava/lang/Object;

    .line 428
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->_usingGooglePlayServicesMutex:Ljava/lang/Object;

    .line 439
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->_usingAudienceManagerMutex:Ljava/lang/Object;

    .line 460
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->_usingTargetMutex:Ljava/lang/Object;

    .line 962
    sput-object v1, Lcom/adobe/mobile/MobileConfig;->_userDefinedInputStream:Ljava/io/InputStream;

    .line 963
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->_userDefinedInputStreamMutex:Ljava/lang/Object;

    return-void
.end method

.method private constructor <init>()V
    .locals 17

    .prologue
    .line 167
    invoke-direct/range {p0 .. p0}, Ljava/lang/Object;-><init>()V

    .line 99
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_networkConnectivity:Z

    .line 100
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_adobeDataCallback:Lcom/adobe/mobile/Config$AdobeDataCallback;

    .line 104
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_reportSuiteIds:Ljava/lang/String;

    .line 105
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_trackingServer:Ljava/lang/String;

    .line 106
    const-string v13, "UTF-8"

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_characterSet:Ljava/lang/String;

    .line 107
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_ssl:Z

    .line 108
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_offlineTrackingEnabled:Z

    .line 109
    const/4 v13, 0x1

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_backdateSessionInfoEnabled:Z

    .line 110
    const/16 v13, 0x12c

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_lifecycleTimeout:I

    .line 111
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_referrerTimeout:I

    .line 112
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_batchLimit:I

    .line 113
    sget-object v13, Lcom/adobe/mobile/MobileConfig;->DEFAULT_PRIVACY_STATUS:Lcom/adobe/mobile/MobilePrivacyStatus;

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_privacyStatus:Lcom/adobe/mobile/MobilePrivacyStatus;

    .line 114
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterest:Ljava/util/List;

    .line 115
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    .line 120
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_clientCode:Ljava/lang/String;

    .line 121
    const/4 v13, 0x2

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_defaultLocationTimeout:I

    .line 126
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamServer:Ljava/lang/String;

    .line 127
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamAnalyticsForwardingEnabled:Z

    .line 128
    const/4 v13, 0x2

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamTimeout:I

    .line 133
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_acquisitionServer:Ljava/lang/String;

    .line 134
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_acquisitionAppIdentifier:Ljava/lang/String;

    .line 139
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_messagesURL:Ljava/lang/String;

    .line 140
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_inAppMessages:Ljava/util/ArrayList;

    .line 141
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_callbackTemplates:Ljava/util/ArrayList;

    .line 142
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_piiRequests:Ljava/util/ArrayList;

    .line 146
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_marketingCloudOrganizationId:Ljava/lang/String;

    .line 408
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_usingAnalytics:Ljava/lang/Boolean;

    .line 427
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_usingGooglePlayServices:Ljava/lang/Boolean;

    .line 438
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_usingAudienceManager:Ljava/lang/Boolean;

    .line 459
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_usingTarget:Ljava/lang/Boolean;

    .line 169
    invoke-virtual/range {p0 .. p0}, Lcom/adobe/mobile/MobileConfig;->startNotifier()V

    .line 172
    invoke-direct/range {p0 .. p0}, Lcom/adobe/mobile/MobileConfig;->loadBundleConfig()Lorg/json/JSONObject;

    move-result-object v4

    .line 175
    .local v4, "configSettings":Lorg/json/JSONObject;
    if-nez v4, :cond_0

    .line 404
    :goto_0
    return-void

    .line 179
    :cond_0
    const/4 v3, 0x0

    .line 181
    .local v3, "analyticsSettings":Lorg/json/JSONObject;
    :try_start_0
    const-string v13, "analytics"

    invoke-virtual {v4, v13}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v3

    .line 187
    :goto_1
    if-eqz v3, :cond_1

    .line 189
    :try_start_1
    const-string v13, "server"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_trackingServer:Ljava/lang/String;

    .line 190
    const-string v13, "rsids"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_reportSuiteIds:Ljava/lang/String;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    .line 198
    :goto_2
    :try_start_2
    const-string v13, "charset"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_characterSet:Ljava/lang/String;
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_2

    .line 204
    :goto_3
    :try_start_3
    const-string v13, "ssl"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v13

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_ssl:Z
    :try_end_3
    .catch Lorg/json/JSONException; {:try_start_3 .. :try_end_3} :catch_3

    .line 210
    :goto_4
    :try_start_4
    const-string v13, "offlineEnabled"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v13

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_offlineTrackingEnabled:Z
    :try_end_4
    .catch Lorg/json/JSONException; {:try_start_4 .. :try_end_4} :catch_4

    .line 216
    :goto_5
    :try_start_5
    const-string v13, "backdateSessionInfo"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v13

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_backdateSessionInfoEnabled:Z
    :try_end_5
    .catch Lorg/json/JSONException; {:try_start_5 .. :try_end_5} :catch_5

    .line 222
    :goto_6
    :try_start_6
    const-string v13, "lifecycleTimeout"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v13

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_lifecycleTimeout:I
    :try_end_6
    .catch Lorg/json/JSONException; {:try_start_6 .. :try_end_6} :catch_6

    .line 228
    :goto_7
    :try_start_7
    const-string v13, "referrerTimeout"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v13

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_referrerTimeout:I
    :try_end_7
    .catch Lorg/json/JSONException; {:try_start_7 .. :try_end_7} :catch_7

    .line 234
    :goto_8
    :try_start_8
    const-string v13, "batchLimit"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v13

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_batchLimit:I
    :try_end_8
    .catch Lorg/json/JSONException; {:try_start_8 .. :try_end_8} :catch_8

    .line 241
    :goto_9
    :try_start_9
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v13

    const-string v14, "PrivacyStatus"

    invoke-interface {v13, v14}, Landroid/content/SharedPreferences;->contains(Ljava/lang/String;)Z

    move-result v13

    if-eqz v13, :cond_9

    .line 242
    invoke-static {}, Lcom/adobe/mobile/MobilePrivacyStatus;->values()[Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v13

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v14

    const-string v15, "PrivacyStatus"

    const/16 v16, 0x0

    invoke-interface/range {v14 .. v16}, Landroid/content/SharedPreferences;->getInt(Ljava/lang/String;I)I

    move-result v14

    aget-object v13, v13, v14

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_privacyStatus:Lcom/adobe/mobile/MobilePrivacyStatus;
    :try_end_9
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_9 .. :try_end_9} :catch_9

    .line 261
    :goto_a
    :try_start_a
    const-string v13, "poi"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v9

    .line 262
    .local v9, "poiArray":Lorg/json/JSONArray;
    move-object/from16 v0, p0

    invoke-direct {v0, v9}, Lcom/adobe/mobile/MobileConfig;->loadPoiFromJsonArray(Lorg/json/JSONArray;)V
    :try_end_a
    .catch Lorg/json/JSONException; {:try_start_a .. :try_end_a} :catch_b

    .line 270
    .end local v9    # "poiArray":Lorg/json/JSONArray;
    :cond_1
    :goto_b
    const/4 v12, 0x0

    .line 272
    .local v12, "targetSettings":Lorg/json/JSONObject;
    :try_start_b
    const-string v13, "target"

    invoke-virtual {v4, v13}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_b
    .catch Lorg/json/JSONException; {:try_start_b .. :try_end_b} :catch_c

    move-result-object v12

    .line 277
    :goto_c
    if-eqz v12, :cond_2

    .line 279
    :try_start_c
    const-string v13, "clientCode"

    invoke-virtual {v12, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_clientCode:Ljava/lang/String;
    :try_end_c
    .catch Lorg/json/JSONException; {:try_start_c .. :try_end_c} :catch_d

    .line 286
    :goto_d
    :try_start_d
    const-string v13, "timeout"

    invoke-virtual {v12, v13}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v13

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_defaultLocationTimeout:I
    :try_end_d
    .catch Lorg/json/JSONException; {:try_start_d .. :try_end_d} :catch_e

    .line 293
    :cond_2
    :goto_e
    const/4 v1, 0x0

    .line 296
    .local v1, "aamSettings":Lorg/json/JSONObject;
    :try_start_e
    const-string v13, "audienceManager"

    invoke-virtual {v4, v13}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_e
    .catch Lorg/json/JSONException; {:try_start_e .. :try_end_e} :catch_f

    move-result-object v1

    .line 301
    :goto_f
    if-eqz v1, :cond_4

    .line 303
    :try_start_f
    const-string v13, "server"

    invoke-virtual {v1, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamServer:Ljava/lang/String;
    :try_end_f
    .catch Lorg/json/JSONException; {:try_start_f .. :try_end_f} :catch_10

    .line 310
    :goto_10
    :try_start_10
    const-string v13, "analyticsForwardingEnabled"

    invoke-virtual {v1, v13}, Lorg/json/JSONObject;->getBoolean(Ljava/lang/String;)Z

    move-result v13

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamAnalyticsForwardingEnabled:Z
    :try_end_10
    .catch Lorg/json/JSONException; {:try_start_10 .. :try_end_10} :catch_11

    .line 315
    :goto_11
    move-object/from16 v0, p0

    iget-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamAnalyticsForwardingEnabled:Z

    if-eqz v13, :cond_3

    .line 316
    const-string v13, "Audience Manager - Analytics Server-Side Forwarding Is Enabled."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 320
    :cond_3
    :try_start_11
    const-string v13, "timeout"

    invoke-virtual {v1, v13}, Lorg/json/JSONObject;->getInt(Ljava/lang/String;)I

    move-result v13

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamTimeout:I
    :try_end_11
    .catch Lorg/json/JSONException; {:try_start_11 .. :try_end_11} :catch_12

    .line 327
    :cond_4
    :goto_12
    const/4 v2, 0x0

    .line 330
    .local v2, "acquisitionSettings":Lorg/json/JSONObject;
    :try_start_12
    const-string v13, "acquisition"

    invoke-virtual {v4, v13}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_12
    .catch Lorg/json/JSONException; {:try_start_12 .. :try_end_12} :catch_13

    move-result-object v2

    .line 335
    :goto_13
    if-eqz v2, :cond_5

    .line 337
    :try_start_13
    const-string v13, "appid"

    invoke-virtual {v2, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_acquisitionAppIdentifier:Ljava/lang/String;

    .line 338
    const-string v13, "server"

    invoke-virtual {v2, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_acquisitionServer:Ljava/lang/String;
    :try_end_13
    .catch Lorg/json/JSONException; {:try_start_13 .. :try_end_13} :catch_14

    .line 348
    :cond_5
    :goto_14
    const/4 v11, 0x0

    .line 350
    .local v11, "remoteURLs":Lorg/json/JSONObject;
    :try_start_14
    const-string v13, "remotes"

    invoke-virtual {v4, v13}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_14
    .catch Lorg/json/JSONException; {:try_start_14 .. :try_end_14} :catch_15

    move-result-object v11

    .line 356
    :goto_15
    if-eqz v11, :cond_6

    .line 358
    :try_start_15
    const-string v13, "messages"

    invoke-virtual {v11, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_messagesURL:Ljava/lang/String;
    :try_end_15
    .catch Lorg/json/JSONException; {:try_start_15 .. :try_end_15} :catch_16

    .line 364
    :goto_16
    :try_start_16
    const-string v13, "analytics.poi"

    invoke-virtual {v11, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;
    :try_end_16
    .catch Lorg/json/JSONException; {:try_start_16 .. :try_end_16} :catch_17

    .line 371
    :cond_6
    :goto_17
    const/4 v7, 0x0

    .line 373
    .local v7, "jsonMessages":Lorg/json/JSONArray;
    :try_start_17
    const-string v13, "messages"

    invoke-virtual {v4, v13}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;
    :try_end_17
    .catch Lorg/json/JSONException; {:try_start_17 .. :try_end_17} :catch_18

    move-result-object v7

    .line 378
    :goto_18
    if-eqz v7, :cond_7

    invoke-virtual {v7}, Lorg/json/JSONArray;->length()I

    move-result v13

    if-lez v13, :cond_7

    .line 379
    move-object/from16 v0, p0

    invoke-direct {v0, v7}, Lcom/adobe/mobile/MobileConfig;->loadMessagesFromJsonArray(Lorg/json/JSONArray;)V

    .line 383
    :cond_7
    const/4 v8, 0x0

    .line 385
    .local v8, "marketingCloudSettings":Lorg/json/JSONObject;
    :try_start_18
    const-string v13, "marketingCloud"

    invoke-virtual {v4, v13}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;
    :try_end_18
    .catch Lorg/json/JSONException; {:try_start_18 .. :try_end_18} :catch_19

    move-result-object v8

    .line 390
    :goto_19
    if-eqz v8, :cond_8

    .line 392
    :try_start_19
    const-string v13, "org"

    invoke-virtual {v8, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_marketingCloudOrganizationId:Ljava/lang/String;
    :try_end_19
    .catch Lorg/json/JSONException; {:try_start_19 .. :try_end_19} :catch_1a

    .line 400
    :cond_8
    :goto_1a
    invoke-virtual/range {p0 .. p0}, Lcom/adobe/mobile/MobileConfig;->loadCachedRemotes()V

    .line 403
    invoke-direct/range {p0 .. p0}, Lcom/adobe/mobile/MobileConfig;->updateBlacklist()V

    goto/16 :goto_0

    .line 182
    .end local v1    # "aamSettings":Lorg/json/JSONObject;
    .end local v2    # "acquisitionSettings":Lorg/json/JSONObject;
    .end local v7    # "jsonMessages":Lorg/json/JSONArray;
    .end local v8    # "marketingCloudSettings":Lorg/json/JSONObject;
    .end local v11    # "remoteURLs":Lorg/json/JSONObject;
    .end local v12    # "targetSettings":Lorg/json/JSONObject;
    :catch_0
    move-exception v6

    .line 183
    .local v6, "ex":Lorg/json/JSONException;
    const-string v13, "Analytics - Not configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_1

    .line 191
    .end local v6    # "ex":Lorg/json/JSONException;
    :catch_1
    move-exception v5

    .line 192
    .local v5, "e":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_trackingServer:Ljava/lang/String;

    .line 193
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_reportSuiteIds:Ljava/lang/String;

    .line 194
    const-string v13, "Analytics - Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_2

    .line 199
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_2
    move-exception v5

    .line 200
    .restart local v5    # "e":Lorg/json/JSONException;
    const-string v13, "UTF-8"

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_characterSet:Ljava/lang/String;

    goto/16 :goto_3

    .line 205
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_3
    move-exception v5

    .line 206
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_ssl:Z

    goto/16 :goto_4

    .line 211
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_4
    move-exception v5

    .line 212
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_offlineTrackingEnabled:Z

    goto/16 :goto_5

    .line 217
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_5
    move-exception v5

    .line 218
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x1

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_backdateSessionInfoEnabled:Z

    goto/16 :goto_6

    .line 223
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_6
    move-exception v5

    .line 224
    .restart local v5    # "e":Lorg/json/JSONException;
    const/16 v13, 0x12c

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_lifecycleTimeout:I

    goto/16 :goto_7

    .line 229
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_7
    move-exception v5

    .line 230
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_referrerTimeout:I

    goto/16 :goto_8

    .line 235
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_8
    move-exception v5

    .line 236
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_batchLimit:I

    goto/16 :goto_9

    .line 247
    .end local v5    # "e":Lorg/json/JSONException;
    :cond_9
    :try_start_1a
    const-string v13, "privacyDefault"

    invoke-virtual {v3, v13}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;
    :try_end_1a
    .catch Lorg/json/JSONException; {:try_start_1a .. :try_end_1a} :catch_a
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1a .. :try_end_1a} :catch_9

    move-result-object v10

    .line 252
    .local v10, "privacyString":Ljava/lang/String;
    :goto_1b
    if-eqz v10, :cond_a

    :try_start_1b
    move-object/from16 v0, p0

    invoke-direct {v0, v10}, Lcom/adobe/mobile/MobileConfig;->privacyStatusFromString(Ljava/lang/String;)Lcom/adobe/mobile/MobilePrivacyStatus;

    move-result-object v13

    :goto_1c
    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_privacyStatus:Lcom/adobe/mobile/MobilePrivacyStatus;
    :try_end_1b
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1b .. :try_end_1b} :catch_9

    goto/16 :goto_a

    .line 255
    .end local v10    # "privacyString":Ljava/lang/String;
    :catch_9
    move-exception v5

    .line 256
    .local v5, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v13, "Config - Error pulling privacy from shared preferences. (%s)"

    const/4 v14, 0x1

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    invoke-virtual {v5}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 248
    .end local v5    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_a
    move-exception v5

    .line 249
    .local v5, "e":Lorg/json/JSONException;
    const/4 v10, 0x0

    .restart local v10    # "privacyString":Ljava/lang/String;
    goto :goto_1b

    .line 252
    .end local v5    # "e":Lorg/json/JSONException;
    :cond_a
    :try_start_1c
    sget-object v13, Lcom/adobe/mobile/MobileConfig;->DEFAULT_PRIVACY_STATUS:Lcom/adobe/mobile/MobilePrivacyStatus;
    :try_end_1c
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1c .. :try_end_1c} :catch_9

    goto :goto_1c

    .line 263
    .end local v10    # "privacyString":Ljava/lang/String;
    :catch_b
    move-exception v5

    .line 264
    .restart local v5    # "e":Lorg/json/JSONException;
    const-string v13, "Analytics - Malformed POI List(%s)"

    const/4 v14, 0x1

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    invoke-virtual {v5}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_b

    .line 273
    .end local v5    # "e":Lorg/json/JSONException;
    .restart local v12    # "targetSettings":Lorg/json/JSONObject;
    :catch_c
    move-exception v6

    .line 274
    .restart local v6    # "ex":Lorg/json/JSONException;
    const-string v13, "Target - Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_c

    .line 280
    .end local v6    # "ex":Lorg/json/JSONException;
    :catch_d
    move-exception v5

    .line 281
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_clientCode:Ljava/lang/String;

    .line 282
    const-string v13, "Target - Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_d

    .line 287
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_e
    move-exception v5

    .line 288
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x2

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_defaultLocationTimeout:I

    goto/16 :goto_e

    .line 297
    .end local v5    # "e":Lorg/json/JSONException;
    .restart local v1    # "aamSettings":Lorg/json/JSONObject;
    :catch_f
    move-exception v5

    .line 298
    .restart local v5    # "e":Lorg/json/JSONException;
    const-string v13, "Audience Manager - Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_f

    .line 304
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_10
    move-exception v6

    .line 305
    .restart local v6    # "ex":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamServer:Ljava/lang/String;

    .line 306
    const-string v13, "Audience Manager - Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_10

    .line 311
    .end local v6    # "ex":Lorg/json/JSONException;
    :catch_11
    move-exception v6

    .line 312
    .restart local v6    # "ex":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-boolean v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamAnalyticsForwardingEnabled:Z

    goto/16 :goto_11

    .line 321
    .end local v6    # "ex":Lorg/json/JSONException;
    :catch_12
    move-exception v5

    .line 322
    .restart local v5    # "e":Lorg/json/JSONException;
    const/4 v13, 0x2

    move-object/from16 v0, p0

    iput v13, v0, Lcom/adobe/mobile/MobileConfig;->_aamTimeout:I

    goto/16 :goto_12

    .line 331
    .end local v5    # "e":Lorg/json/JSONException;
    .restart local v2    # "acquisitionSettings":Lorg/json/JSONObject;
    :catch_13
    move-exception v5

    .line 332
    .restart local v5    # "e":Lorg/json/JSONException;
    const-string v13, "Acquisition - Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_13

    .line 340
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_14
    move-exception v6

    .line 341
    .restart local v6    # "ex":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_acquisitionAppIdentifier:Ljava/lang/String;

    .line 342
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_acquisitionServer:Ljava/lang/String;

    .line 343
    const-string v13, "Acquisition - Not configured correctly (missing server or app identifier)."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_14

    .line 352
    .end local v6    # "ex":Lorg/json/JSONException;
    .restart local v11    # "remoteURLs":Lorg/json/JSONObject;
    :catch_15
    move-exception v5

    .line 353
    .restart local v5    # "e":Lorg/json/JSONException;
    const-string v13, "Remotes - Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_15

    .line 359
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_16
    move-exception v5

    .line 360
    .restart local v5    # "e":Lorg/json/JSONException;
    const-string v13, "Config - No in-app messages remote url loaded (%s)"

    const/4 v14, 0x1

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    invoke-virtual {v5}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_16

    .line 365
    .end local v5    # "e":Lorg/json/JSONException;
    :catch_17
    move-exception v5

    .line 366
    .restart local v5    # "e":Lorg/json/JSONException;
    const-string v13, "Config - No points of interest remote url loaded (%s)"

    const/4 v14, 0x1

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    invoke-virtual {v5}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_17

    .line 374
    .end local v5    # "e":Lorg/json/JSONException;
    .restart local v7    # "jsonMessages":Lorg/json/JSONArray;
    :catch_18
    move-exception v6

    .line 375
    .restart local v6    # "ex":Lorg/json/JSONException;
    const-string v13, "Messages - Not configured locally."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_18

    .line 386
    .end local v6    # "ex":Lorg/json/JSONException;
    .restart local v8    # "marketingCloudSettings":Lorg/json/JSONObject;
    :catch_19
    move-exception v6

    .line 387
    .restart local v6    # "ex":Lorg/json/JSONException;
    const-string v13, "Marketing Cloud - Not configured locally."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_19

    .line 393
    .end local v6    # "ex":Lorg/json/JSONException;
    :catch_1a
    move-exception v6

    .line 394
    .restart local v6    # "ex":Lorg/json/JSONException;
    const/4 v13, 0x0

    move-object/from16 v0, p0

    iput-object v13, v0, Lcom/adobe/mobile/MobileConfig;->_marketingCloudOrganizationId:Ljava/lang/String;

    .line 395
    const-string v13, "Visitor - ID Service Not Configured."

    const/4 v14, 0x0

    new-array v14, v14, [Ljava/lang/Object;

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_1a
.end method

.method static synthetic access$000(Lcom/adobe/mobile/MobileConfig;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/MobileConfig;

    .prologue
    .line 42
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_messagesURL:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$100(Lcom/adobe/mobile/MobileConfig;)V
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/MobileConfig;

    .prologue
    .line 42
    invoke-direct {p0}, Lcom/adobe/mobile/MobileConfig;->loadMessageImages()V

    return-void
.end method

.method static synthetic access$200(Lcom/adobe/mobile/MobileConfig;)V
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/MobileConfig;

    .prologue
    .line 42
    invoke-direct {p0}, Lcom/adobe/mobile/MobileConfig;->updateBlacklist()V

    return-void
.end method

.method static synthetic access$300(Lcom/adobe/mobile/MobileConfig;)Ljava/util/ArrayList;
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/MobileConfig;

    .prologue
    .line 42
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_inAppMessages:Ljava/util/ArrayList;

    return-object v0
.end method

.method static synthetic access$400(Lcom/adobe/mobile/MobileConfig;)Z
    .locals 1
    .param p0, "x0"    # Lcom/adobe/mobile/MobileConfig;

    .prologue
    .line 42
    iget-boolean v0, p0, Lcom/adobe/mobile/MobileConfig;->_networkConnectivity:Z

    return v0
.end method

.method static synthetic access$402(Lcom/adobe/mobile/MobileConfig;Z)Z
    .locals 0
    .param p0, "x0"    # Lcom/adobe/mobile/MobileConfig;
    .param p1, "x1"    # Z

    .prologue
    .line 42
    iput-boolean p1, p0, Lcom/adobe/mobile/MobileConfig;->_networkConnectivity:Z

    return p1
.end method

.method protected static getInstance()Lcom/adobe/mobile/MobileConfig;
    .locals 2

    .prologue
    .line 154
    sget-object v1, Lcom/adobe/mobile/MobileConfig;->_instanceMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 155
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/MobileConfig;->_instance:Lcom/adobe/mobile/MobileConfig;

    if-nez v0, :cond_0

    .line 156
    new-instance v0, Lcom/adobe/mobile/MobileConfig;

    invoke-direct {v0}, Lcom/adobe/mobile/MobileConfig;-><init>()V

    sput-object v0, Lcom/adobe/mobile/MobileConfig;->_instance:Lcom/adobe/mobile/MobileConfig;

    .line 159
    :cond_0
    sget-object v0, Lcom/adobe/mobile/MobileConfig;->_instance:Lcom/adobe/mobile/MobileConfig;

    monitor-exit v1

    return-object v0

    .line 160
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method private loadBundleConfig()Lorg/json/JSONObject;
    .locals 7

    .prologue
    const/4 v5, 0x1

    const/4 v6, 0x0

    .line 848
    const/4 v1, 0x0

    .line 852
    .local v1, "jsonData":Lorg/json/JSONObject;
    sget-object v4, Lcom/adobe/mobile/MobileConfig;->_userDefinedInputStreamMutex:Ljava/lang/Object;

    monitor-enter v4

    .line 853
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/MobileConfig;->_userDefinedInputStream:Ljava/io/InputStream;

    .line 854
    .local v2, "userPath":Ljava/io/InputStream;
    monitor-exit v4
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 858
    if-eqz v2, :cond_0

    .line 859
    :try_start_1
    const-string v3, "Config - Attempting to load config file from override stream"

    const/4 v4, 0x0

    new-array v4, v4, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 860
    invoke-direct {p0, v2}, Lcom/adobe/mobile/MobileConfig;->loadConfigFromStream(Ljava/io/InputStream;)Lorg/json/JSONObject;
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_1

    move-result-object v1

    .line 871
    :cond_0
    :goto_0
    if-nez v1, :cond_2

    .line 872
    if-eqz v2, :cond_1

    .line 873
    const-string v3, "Config - Failed attempting to load custom config, will fall back to standard config location."

    new-array v4, v6, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 876
    :cond_1
    const-string v3, "Config - Attempting to load config file from default location"

    new-array v4, v6, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 877
    const-string v3, "ADBMobileConfig.json"

    invoke-direct {p0, v3}, Lcom/adobe/mobile/MobileConfig;->loadConfigFile(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    .line 881
    if-nez v1, :cond_2

    .line 882
    const-string v3, "Config - Could not find config file at expected location.  Attempting to load from www folder"

    new-array v4, v6, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 883
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "www"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    sget-object v4, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    const-string v4, "ADBMobileConfig.json"

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {p0, v3}, Lcom/adobe/mobile/MobileConfig;->loadConfigFile(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v1

    .line 887
    :cond_2
    return-object v1

    .line 854
    .end local v2    # "userPath":Ljava/io/InputStream;
    :catchall_0
    move-exception v3

    :try_start_2
    monitor-exit v4
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v3

    .line 863
    .restart local v2    # "userPath":Ljava/io/InputStream;
    :catch_0
    move-exception v0

    .line 864
    .local v0, "e":Ljava/io/IOException;
    const-string v3, "Config - Error loading user defined config (%s)"

    new-array v4, v5, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 866
    .end local v0    # "e":Ljava/io/IOException;
    :catch_1
    move-exception v0

    .line 867
    .local v0, "e":Lorg/json/JSONException;
    const-string v3, "Config - Error parsing user defined config (%s)"

    new-array v4, v5, [Ljava/lang/Object;

    invoke-virtual {v0}, Lorg/json/JSONException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private loadConfigFile(Ljava/lang/String;)Lorg/json/JSONObject;
    .locals 8
    .param p1, "configFilePath"    # Ljava/lang/String;

    .prologue
    const/4 v4, 0x0

    const/4 v6, 0x1

    const/4 v7, 0x0

    .line 891
    const/4 v2, 0x0

    .line 893
    .local v2, "jsonData":Lorg/json/JSONObject;
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    .line 894
    .local v3, "resources":Landroid/content/res/Resources;
    if-nez v3, :cond_1

    .line 915
    .end local v3    # "resources":Landroid/content/res/Resources;
    :cond_0
    :goto_0
    return-object v4

    .line 898
    .restart local v3    # "resources":Landroid/content/res/Resources;
    :cond_1
    invoke-virtual {v3}, Landroid/content/res/Resources;->getAssets()Landroid/content/res/AssetManager;

    move-result-object v0

    .line 899
    .local v0, "assets":Landroid/content/res/AssetManager;
    if-eqz v0, :cond_0

    .line 903
    invoke-virtual {v0, p1}, Landroid/content/res/AssetManager;->open(Ljava/lang/String;)Ljava/io/InputStream;

    move-result-object v4

    invoke-direct {p0, v4}, Lcom/adobe/mobile/MobileConfig;->loadConfigFromStream(Ljava/io/InputStream;)Lorg/json/JSONObject;
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_2

    move-result-object v2

    .end local v0    # "assets":Landroid/content/res/AssetManager;
    .end local v3    # "resources":Landroid/content/res/Resources;
    :goto_1
    move-object v4, v2

    .line 915
    goto :goto_0

    .line 905
    :catch_0
    move-exception v1

    .line 906
    .local v1, "e":Ljava/io/IOException;
    const-string v4, "Config - Exception loading config file (%s)"

    new-array v5, v6, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 908
    .end local v1    # "e":Ljava/io/IOException;
    :catch_1
    move-exception v1

    .line 909
    .local v1, "e":Lorg/json/JSONException;
    const-string v4, "Config - Exception parsing config file (%s)"

    new-array v5, v6, [Ljava/lang/Object;

    invoke-virtual {v1}, Lorg/json/JSONException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 911
    .end local v1    # "e":Lorg/json/JSONException;
    :catch_2
    move-exception v1

    .line 912
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Config - Null context when attempting to read config file (%s)"

    new-array v5, v6, [Ljava/lang/Object;

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method private loadConfigFromStream(Ljava/io/InputStream;)Lorg/json/JSONObject;
    .locals 9
    .param p1, "stream"    # Ljava/io/InputStream;
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lorg/json/JSONException;,
            Ljava/io/IOException;
        }
    .end annotation

    .prologue
    const/4 v8, 0x1

    const/4 v7, 0x0

    .line 932
    if-nez p1, :cond_0

    .line 933
    const/4 v3, 0x0

    .line 959
    :goto_0
    return-object v3

    .line 938
    :cond_0
    :try_start_0
    invoke-virtual {p1}, Ljava/io/InputStream;->available()I

    move-result v3

    new-array v0, v3, [B

    .line 939
    .local v0, "data":[B
    invoke-virtual {p1, v0}, Ljava/io/InputStream;->read([B)I

    .line 942
    new-instance v2, Ljava/lang/String;

    const-string v3, "UTF-8"

    invoke-direct {v2, v0, v3}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    .line 943
    .local v2, "jsonString":Ljava/lang/String;
    new-instance v3, Lorg/json/JSONObject;

    invoke-direct {v3, v2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_3
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 953
    :try_start_1
    invoke-virtual {p1}, Ljava/io/InputStream;->close()V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_0

    .line 954
    :catch_0
    move-exception v1

    .line 955
    .local v1, "e":Ljava/io/IOException;
    const-string v4, "Config - Unable to close stream (%s)"

    new-array v5, v8, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 945
    .end local v0    # "data":[B
    .end local v1    # "e":Ljava/io/IOException;
    .end local v2    # "jsonString":Ljava/lang/String;
    :catch_1
    move-exception v1

    .line 946
    .restart local v1    # "e":Ljava/io/IOException;
    :try_start_2
    const-string v3, "Config - Exception when reading config (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 953
    :try_start_3
    invoke-virtual {p1}, Ljava/io/InputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_2

    .line 959
    .end local v1    # "e":Ljava/io/IOException;
    :goto_1
    new-instance v3, Lorg/json/JSONObject;

    invoke-direct {v3}, Lorg/json/JSONObject;-><init>()V

    goto :goto_0

    .line 954
    .restart local v1    # "e":Ljava/io/IOException;
    :catch_2
    move-exception v1

    .line 955
    const-string v3, "Config - Unable to close stream (%s)"

    new-array v4, v8, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v7

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 948
    .end local v1    # "e":Ljava/io/IOException;
    :catch_3
    move-exception v1

    .line 949
    .local v1, "e":Ljava/lang/NullPointerException;
    :try_start_4
    const-string v3, "Config - Stream closed when attempting to load config (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Ljava/lang/NullPointerException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    .line 953
    :try_start_5
    invoke-virtual {p1}, Ljava/io/InputStream;->close()V
    :try_end_5
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_4

    goto :goto_1

    .line 954
    :catch_4
    move-exception v1

    .line 955
    .local v1, "e":Ljava/io/IOException;
    const-string v3, "Config - Unable to close stream (%s)"

    new-array v4, v8, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v7

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 952
    .end local v1    # "e":Ljava/io/IOException;
    :catchall_0
    move-exception v3

    .line 953
    :try_start_6
    invoke-virtual {p1}, Ljava/io/InputStream;->close()V
    :try_end_6
    .catch Ljava/io/IOException; {:try_start_6 .. :try_end_6} :catch_5

    .line 956
    :goto_2
    throw v3

    .line 954
    :catch_5
    move-exception v1

    .line 955
    .restart local v1    # "e":Ljava/io/IOException;
    const-string v4, "Config - Unable to close stream (%s)"

    new-array v5, v8, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_2
.end method

.method private loadMessageImages()V
    .locals 2

    .prologue
    .line 1101
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getMessageImageCachingExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/MobileConfig$12;

    invoke-direct {v1, p0}, Lcom/adobe/mobile/MobileConfig$12;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 1142
    return-void
.end method

.method private loadMessagesDataFromRemote(Lorg/json/JSONObject;)V
    .locals 6
    .param p1, "jsonData"    # Lorg/json/JSONObject;

    .prologue
    const/4 v5, 0x0

    const/4 v4, 0x0

    .line 1019
    if-nez p1, :cond_0

    .line 1020
    const-string v2, "Messages - Remote messages config was null, falling back to bundled messages"

    new-array v3, v4, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1021
    const-string v2, "messageImages"

    invoke-static {v2}, Lcom/adobe/mobile/RemoteDownload;->deleteFilesInDirectory(Ljava/lang/String;)V

    .line 1046
    :goto_0
    return-void

    .line 1026
    :cond_0
    const/4 v1, 0x0

    .line 1028
    .local v1, "jsonMessages":Lorg/json/JSONArray;
    :try_start_0
    const-string v2, "messages"

    invoke-virtual {p1, v2}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v1

    .line 1034
    :goto_1
    const-string v2, "Messages - Using remote definition for messages"

    new-array v3, v4, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1037
    if-eqz v1, :cond_1

    invoke-virtual {v1}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-gtz v2, :cond_2

    .line 1038
    :cond_1
    const-string v2, "messageImages"

    invoke-static {v2}, Lcom/adobe/mobile/RemoteDownload;->deleteFilesInDirectory(Ljava/lang/String;)V

    .line 1039
    iput-object v5, p0, Lcom/adobe/mobile/MobileConfig;->_inAppMessages:Ljava/util/ArrayList;

    .line 1040
    iput-object v5, p0, Lcom/adobe/mobile/MobileConfig;->_callbackTemplates:Ljava/util/ArrayList;

    .line 1041
    iput-object v5, p0, Lcom/adobe/mobile/MobileConfig;->_piiRequests:Ljava/util/ArrayList;

    goto :goto_0

    .line 1030
    :catch_0
    move-exception v0

    .line 1031
    .local v0, "ex":Lorg/json/JSONException;
    const-string v2, "Messages - Remote messages not configured, falling back to bundled messages"

    new-array v3, v4, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 1045
    .end local v0    # "ex":Lorg/json/JSONException;
    :cond_2
    invoke-direct {p0, v1}, Lcom/adobe/mobile/MobileConfig;->loadMessagesFromJsonArray(Lorg/json/JSONArray;)V

    goto :goto_0
.end method

.method private loadMessagesFromJsonArray(Lorg/json/JSONArray;)V
    .locals 14
    .param p1, "messages"    # Lorg/json/JSONArray;

    .prologue
    const/4 v13, 0x1

    const/4 v12, 0x0

    .line 1051
    :try_start_0
    new-instance v6, Ljava/util/ArrayList;

    invoke-direct {v6}, Ljava/util/ArrayList;-><init>()V

    .line 1052
    .local v6, "tempInAppMessages":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    .line 1053
    .local v5, "tempCallbackTemplates":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    new-instance v7, Ljava/util/ArrayList;

    invoke-direct {v7}, Ljava/util/ArrayList;-><init>()V

    .line 1055
    .local v7, "tempPiiRequests":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v3

    .line 1057
    .local v3, "messageCount":I
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    if-ge v1, v3, :cond_3

    .line 1058
    invoke-virtual {p1, v1}, Lorg/json/JSONArray;->getJSONObject(I)Lorg/json/JSONObject;

    move-result-object v4

    .line 1060
    .local v4, "messageJson":Lorg/json/JSONObject;
    invoke-static {v4}, Lcom/adobe/mobile/Message;->messageWithJsonObject(Lorg/json/JSONObject;)Lcom/adobe/mobile/Message;

    move-result-object v2

    .line 1062
    .local v2, "message":Lcom/adobe/mobile/Message;
    if-eqz v2, :cond_0

    .line 1063
    const-string v8, "Messages - loaded message - %s"

    const/4 v9, 0x1

    new-array v9, v9, [Ljava/lang/Object;

    const/4 v10, 0x0

    invoke-virtual {v2}, Lcom/adobe/mobile/Message;->description()Ljava/lang/String;

    move-result-object v11

    aput-object v11, v9, v10

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1066
    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v8

    const-class v9, Lcom/adobe/mobile/MessageTemplatePii;

    if-ne v8, v9, :cond_1

    .line 1067
    invoke-virtual {v7, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1057
    :cond_0
    :goto_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1068
    :cond_1
    invoke-virtual {v2}, Ljava/lang/Object;->getClass()Ljava/lang/Class;

    move-result-object v8

    const-class v9, Lcom/adobe/mobile/MessageTemplateCallback;

    if-ne v8, v9, :cond_2

    .line 1069
    invoke-virtual {v5, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_1

    .line 1080
    .end local v1    # "i":I
    .end local v2    # "message":Lcom/adobe/mobile/Message;
    .end local v3    # "messageCount":I
    .end local v4    # "messageJson":Lorg/json/JSONObject;
    .end local v5    # "tempCallbackTemplates":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    .end local v6    # "tempInAppMessages":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    .end local v7    # "tempPiiRequests":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    :catch_0
    move-exception v0

    .line 1081
    .local v0, "e":Lorg/json/JSONException;
    const-string v8, "Messages - Unable to parse messages JSON (%s)"

    new-array v9, v13, [Ljava/lang/Object;

    invoke-virtual {v0}, Lorg/json/JSONException;->getMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v12

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1083
    .end local v0    # "e":Lorg/json/JSONException;
    :goto_2
    return-void

    .line 1071
    .restart local v1    # "i":I
    .restart local v2    # "message":Lcom/adobe/mobile/Message;
    .restart local v3    # "messageCount":I
    .restart local v4    # "messageJson":Lorg/json/JSONObject;
    .restart local v5    # "tempCallbackTemplates":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    .restart local v6    # "tempInAppMessages":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    .restart local v7    # "tempPiiRequests":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/adobe/mobile/Message;>;"
    :cond_2
    :try_start_1
    invoke-virtual {v6, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    goto :goto_1

    .line 1076
    .end local v2    # "message":Lcom/adobe/mobile/Message;
    .end local v4    # "messageJson":Lorg/json/JSONObject;
    :cond_3
    iput-object v6, p0, Lcom/adobe/mobile/MobileConfig;->_inAppMessages:Ljava/util/ArrayList;

    .line 1077
    iput-object v5, p0, Lcom/adobe/mobile/MobileConfig;->_callbackTemplates:Ljava/util/ArrayList;

    .line 1078
    iput-object v7, p0, Lcom/adobe/mobile/MobileConfig;->_piiRequests:Ljava/util/ArrayList;
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_2
.end method

.method private loadPoiFromJsonArray(Lorg/json/JSONArray;)V
    .locals 10
    .param p1, "poiArray"    # Lorg/json/JSONArray;

    .prologue
    const/4 v9, 0x1

    const/4 v8, 0x0

    .line 992
    if-nez p1, :cond_1

    .line 1012
    :cond_0
    :goto_0
    return-void

    .line 996
    :cond_1
    :try_start_0
    new-instance v5, Ljava/util/ArrayList;

    invoke-direct {v5}, Ljava/util/ArrayList;-><init>()V

    iput-object v5, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterest:Ljava/util/List;

    .line 997
    const/4 v2, 0x0

    .local v2, "i":I
    invoke-virtual {p1}, Lorg/json/JSONArray;->length()I

    move-result v0

    .local v0, "count":I
    :goto_1
    if-ge v2, v0, :cond_0

    .line 998
    invoke-virtual {p1, v2}, Lorg/json/JSONArray;->getJSONArray(I)Lorg/json/JSONArray;

    move-result-object v3

    .line 999
    .local v3, "singlePOI":Lorg/json/JSONArray;
    new-instance v4, Ljava/util/ArrayList;

    const/4 v5, 0x4

    invoke-direct {v4, v5}, Ljava/util/ArrayList;-><init>(I)V

    .line 1001
    .local v4, "singlePoint":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/Object;>;"
    const/4 v5, 0x0

    invoke-virtual {v3, v5}, Lorg/json/JSONArray;->getString(I)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1002
    const/4 v5, 0x1

    invoke-virtual {v3, v5}, Lorg/json/JSONArray;->getDouble(I)D

    move-result-wide v6

    invoke-static {v6, v7}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1003
    const/4 v5, 0x2

    invoke-virtual {v3, v5}, Lorg/json/JSONArray;->getDouble(I)D

    move-result-wide v6

    invoke-static {v6, v7}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1004
    const/4 v5, 0x3

    invoke-virtual {v3, v5}, Lorg/json/JSONArray;->getDouble(I)D

    move-result-wide v6

    invoke-static {v6, v7}, Ljava/lang/Double;->valueOf(D)Ljava/lang/Double;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1006
    iget-object v5, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterest:Ljava/util/List;

    invoke-interface {v5, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    .line 997
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    .line 1009
    .end local v0    # "count":I
    .end local v2    # "i":I
    .end local v3    # "singlePOI":Lorg/json/JSONArray;
    .end local v4    # "singlePoint":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/lang/Object;>;"
    :catch_0
    move-exception v1

    .line 1010
    .local v1, "ex":Lorg/json/JSONException;
    const-string v5, "Messages - Unable to parse remote points of interest JSON (%s)"

    new-array v6, v9, [Ljava/lang/Object;

    invoke-virtual {v1}, Lorg/json/JSONException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v8

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method private privacyStatusFromString(Ljava/lang/String;)Lcom/adobe/mobile/MobilePrivacyStatus;
    .locals 1
    .param p1, "string"    # Ljava/lang/String;

    .prologue
    .line 973
    if-eqz p1, :cond_2

    .line 974
    const-string v0, "optedin"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 975
    sget-object v0, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_IN:Lcom/adobe/mobile/MobilePrivacyStatus;

    .line 985
    :goto_0
    return-object v0

    .line 977
    :cond_0
    const-string v0, "optedout"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_1

    .line 978
    sget-object v0, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_OUT:Lcom/adobe/mobile/MobilePrivacyStatus;

    goto :goto_0

    .line 980
    :cond_1
    const-string v0, "optunknown"

    invoke-virtual {p1, v0}, Ljava/lang/String;->equalsIgnoreCase(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 981
    sget-object v0, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_UNKNOWN:Lcom/adobe/mobile/MobilePrivacyStatus;

    goto :goto_0

    .line 985
    :cond_2
    sget-object v0, Lcom/adobe/mobile/MobileConfig;->DEFAULT_PRIVACY_STATUS:Lcom/adobe/mobile/MobilePrivacyStatus;

    goto :goto_0
.end method

.method public static setUserDefinedConfigPath(Ljava/io/InputStream;)V
    .locals 2
    .param p0, "stream"    # Ljava/io/InputStream;

    .prologue
    .line 965
    sget-object v1, Lcom/adobe/mobile/MobileConfig;->_userDefinedInputStreamMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 966
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/MobileConfig;->_userDefinedInputStream:Ljava/io/InputStream;

    if-nez v0, :cond_0

    .line 967
    sput-object p0, Lcom/adobe/mobile/MobileConfig;->_userDefinedInputStream:Ljava/io/InputStream;

    .line 969
    :cond_0
    monitor-exit v1

    .line 970
    return-void

    .line 969
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method private updateBlacklist()V
    .locals 5

    .prologue
    .line 1086
    iget-object v2, p0, Lcom/adobe/mobile/MobileConfig;->_inAppMessages:Ljava/util/ArrayList;

    if-nez v2, :cond_1

    .line 1097
    :cond_0
    return-void

    .line 1090
    :cond_1
    iget-object v2, p0, Lcom/adobe/mobile/MobileConfig;->_inAppMessages:Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->iterator()Ljava/util/Iterator;

    move-result-object v3

    :cond_2
    :goto_0
    invoke-interface {v3}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    invoke-interface {v3}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/adobe/mobile/Message;

    .line 1091
    .local v1, "message":Lcom/adobe/mobile/Message;
    invoke-virtual {v1}, Lcom/adobe/mobile/Message;->loadBlacklist()Ljava/util/HashMap;

    move-result-object v0

    .line 1093
    .local v0, "blackList":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Integer;>;"
    invoke-virtual {v1}, Lcom/adobe/mobile/Message;->isBlacklisted()Z

    move-result v2

    if-eqz v2, :cond_2

    iget-object v2, v1, Lcom/adobe/mobile/Message;->showRule:Lcom/adobe/mobile/Messages$MessageShowRule;

    invoke-virtual {v2}, Lcom/adobe/mobile/Messages$MessageShowRule;->getValue()I

    move-result v4

    iget-object v2, v1, Lcom/adobe/mobile/Message;->messageId:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/util/HashMap;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/Integer;

    invoke-virtual {v2}, Ljava/lang/Integer;->intValue()I

    move-result v2

    if-eq v4, v2, :cond_2

    .line 1094
    invoke-virtual {v1}, Lcom/adobe/mobile/Message;->removeFromBlacklist()V

    goto :goto_0
.end method


# virtual methods
.method protected downloadRemoteConfigs()V
    .locals 2

    .prologue
    .line 675
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getMessagesExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/MobileConfig$8;

    invoke-direct {v1, p0}, Lcom/adobe/mobile/MobileConfig$8;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 695
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getThirdPartyCallbacksExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/MobileConfig$9;

    invoke-direct {v1, p0}, Lcom/adobe/mobile/MobileConfig$9;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 714
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getPIIExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    new-instance v1, Lcom/adobe/mobile/MobileConfig$10;

    invoke-direct {v1, p0}, Lcom/adobe/mobile/MobileConfig$10;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v0, v1}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 734
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    .line 735
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    new-instance v1, Lcom/adobe/mobile/MobileConfig$11;

    invoke-direct {v1, p0}, Lcom/adobe/mobile/MobileConfig$11;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-static {v0, v1}, Lcom/adobe/mobile/RemoteDownload;->remoteDownloadAsync(Ljava/lang/String;Lcom/adobe/mobile/RemoteDownload$RemoteDownloadBlock;)V

    .line 750
    :cond_0
    return-void
.end method

.method protected getAamAnalyticsForwardingEnabled()Z
    .locals 1

    .prologue
    .line 656
    iget-boolean v0, p0, Lcom/adobe/mobile/MobileConfig;->_aamAnalyticsForwardingEnabled:Z

    return v0
.end method

.method protected getAamServer()Ljava/lang/String;
    .locals 1

    .prologue
    .line 654
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_aamServer:Ljava/lang/String;

    return-object v0
.end method

.method protected getAamTimeout()I
    .locals 1

    .prologue
    .line 658
    iget v0, p0, Lcom/adobe/mobile/MobileConfig;->_aamTimeout:I

    return v0
.end method

.method protected getAcquisitionAppId()Ljava/lang/String;
    .locals 1

    .prologue
    .line 664
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_acquisitionAppIdentifier:Ljava/lang/String;

    return-object v0
.end method

.method protected getAcquisitionServer()Ljava/lang/String;
    .locals 1

    .prologue
    .line 667
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_acquisitionServer:Ljava/lang/String;

    return-object v0
.end method

.method protected getAnalyticsResponseType()I
    .locals 1

    .prologue
    .line 636
    iget-boolean v0, p0, Lcom/adobe/mobile/MobileConfig;->_aamAnalyticsForwardingEnabled:Z

    if-eqz v0, :cond_0

    const/16 v0, 0xa

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected getBackdateSessionInfoEnabled()Z
    .locals 1

    .prologue
    .line 528
    iget-boolean v0, p0, Lcom/adobe/mobile/MobileConfig;->_backdateSessionInfoEnabled:Z

    return v0
.end method

.method protected getBatchLimit()I
    .locals 1

    .prologue
    .line 535
    iget v0, p0, Lcom/adobe/mobile/MobileConfig;->_batchLimit:I

    return v0
.end method

.method protected getCallbackTemplates()Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList",
            "<",
            "Lcom/adobe/mobile/Message;",
            ">;"
        }
    .end annotation

    .prologue
    .line 829
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_callbackTemplates:Ljava/util/ArrayList;

    return-object v0
.end method

.method protected getCharacterSet()Ljava/lang/String;
    .locals 1

    .prologue
    .line 517
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_characterSet:Ljava/lang/String;

    return-object v0
.end method

.method protected getClientCode()Ljava/lang/String;
    .locals 1

    .prologue
    .line 643
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_clientCode:Ljava/lang/String;

    return-object v0
.end method

.method protected getDefaultLocationTimeout()I
    .locals 1

    .prologue
    .line 647
    iget v0, p0, Lcom/adobe/mobile/MobileConfig;->_defaultLocationTimeout:I

    return v0
.end method

.method protected getInAppMessageURL()Ljava/lang/String;
    .locals 1

    .prologue
    .line 826
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_messagesURL:Ljava/lang/String;

    return-object v0
.end method

.method protected getInAppMessages()Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList",
            "<",
            "Lcom/adobe/mobile/Message;",
            ">;"
        }
    .end annotation

    .prologue
    .line 822
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_inAppMessages:Ljava/util/ArrayList;

    return-object v0
.end method

.method protected getLifecycleTimeout()I
    .locals 1

    .prologue
    .line 531
    iget v0, p0, Lcom/adobe/mobile/MobileConfig;->_lifecycleTimeout:I

    return v0
.end method

.method protected getMarketingCloudOrganizationId()Ljava/lang/String;
    .locals 1

    .prologue
    .line 837
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_marketingCloudOrganizationId:Ljava/lang/String;

    return-object v0
.end method

.method protected getNetworkConnectivity(Landroid/content/Context;)Z
    .locals 8
    .param p1, "context"    # Landroid/content/Context;

    .prologue
    const/4 v4, 0x1

    const/4 v5, 0x0

    .line 1185
    const/4 v3, 0x1

    .line 1188
    .local v3, "tempNetworkConnectivity":Z
    if-eqz p1, :cond_0

    .line 1190
    :try_start_0
    const-string v6, "connectivity"

    invoke-virtual {p1, v6}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/net/ConnectivityManager;

    .line 1192
    .local v1, "connectivityManager":Landroid/net/ConnectivityManager;
    if-eqz v1, :cond_3

    .line 1194
    invoke-virtual {v1}, Landroid/net/ConnectivityManager;->getActiveNetworkInfo()Landroid/net/NetworkInfo;

    move-result-object v0

    .line 1196
    .local v0, "activeNetworkInfo":Landroid/net/NetworkInfo;
    if-eqz v0, :cond_2

    .line 1198
    invoke-virtual {v0}, Landroid/net/NetworkInfo;->isAvailable()Z

    move-result v6

    if-eqz v6, :cond_1

    invoke-virtual {v0}, Landroid/net/NetworkInfo;->isConnected()Z

    move-result v6

    if-eqz v6, :cond_1

    move v3, v4

    .line 1221
    .end local v0    # "activeNetworkInfo":Landroid/net/NetworkInfo;
    .end local v1    # "connectivityManager":Landroid/net/ConnectivityManager;
    :cond_0
    :goto_0
    return v3

    .restart local v0    # "activeNetworkInfo":Landroid/net/NetworkInfo;
    .restart local v1    # "connectivityManager":Landroid/net/ConnectivityManager;
    :cond_1
    move v3, v5

    .line 1198
    goto :goto_0

    .line 1202
    :cond_2
    const/4 v3, 0x0

    .line 1203
    const-string v6, "Analytics - Unable to determine connectivity status due to there being no default network currently active"

    const/4 v7, 0x0

    new-array v7, v7, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_0
    .catch Ljava/lang/NullPointerException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_2

    goto :goto_0

    .line 1211
    .end local v0    # "activeNetworkInfo":Landroid/net/NetworkInfo;
    .end local v1    # "connectivityManager":Landroid/net/ConnectivityManager;
    :catch_0
    move-exception v2

    .line 1212
    .local v2, "e":Ljava/lang/NullPointerException;
    const-string v6, "Analytics - Unable to determine connectivity status due to an unexpected error (%s)"

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v2}, Ljava/lang/NullPointerException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v4, v5

    invoke-static {v6, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 1207
    .end local v2    # "e":Ljava/lang/NullPointerException;
    .restart local v1    # "connectivityManager":Landroid/net/ConnectivityManager;
    :cond_3
    :try_start_1
    const-string v6, "Analytics - Unable to determine connectivity status due to the system service requested being unrecognized"

    const/4 v7, 0x0

    new-array v7, v7, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_1
    .catch Ljava/lang/NullPointerException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Ljava/lang/SecurityException; {:try_start_1 .. :try_end_1} :catch_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_2

    goto :goto_0

    .line 1214
    .end local v1    # "connectivityManager":Landroid/net/ConnectivityManager;
    :catch_1
    move-exception v2

    .line 1215
    .local v2, "e":Ljava/lang/SecurityException;
    const-string v6, "Analytics - Unable to access connectivity status due to a security error (%s)"

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v2}, Ljava/lang/SecurityException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v4, v5

    invoke-static {v6, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 1217
    .end local v2    # "e":Ljava/lang/SecurityException;
    :catch_2
    move-exception v2

    .line 1218
    .local v2, "e":Ljava/lang/Exception;
    const-string v6, "Analytics - Unable to access connectivity status due to an unexpected error (%s)"

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v2}, Ljava/lang/Exception;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v4, v5

    invoke-static {v6, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected getOfflineTrackingEnabled()Z
    .locals 1

    .prologue
    .line 525
    iget-boolean v0, p0, Lcom/adobe/mobile/MobileConfig;->_offlineTrackingEnabled:Z

    return v0
.end method

.method protected getPiiRequests()Ljava/util/ArrayList;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/ArrayList",
            "<",
            "Lcom/adobe/mobile/Message;",
            ">;"
        }
    .end annotation

    .prologue
    .line 831
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_piiRequests:Ljava/util/ArrayList;

    return-object v0
.end method

.method protected getPointsOfInterest()Ljava/util/List;
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()",
            "Ljava/util/List",
            "<",
            "Ljava/util/List",
            "<",
            "Ljava/lang/Object;",
            ">;>;"
        }
    .end annotation

    .prologue
    .line 627
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterest:Ljava/util/List;

    return-object v0
.end method

.method protected getPointsOfInterestURL()Ljava/lang/String;
    .locals 1

    .prologue
    .line 783
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    return-object v0
.end method

.method protected getPrivacyStatus()Lcom/adobe/mobile/MobilePrivacyStatus;
    .locals 1

    .prologue
    .line 623
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_privacyStatus:Lcom/adobe/mobile/MobilePrivacyStatus;

    return-object v0
.end method

.method protected getReferrerTimeout()I
    .locals 1

    .prologue
    .line 632
    iget v0, p0, Lcom/adobe/mobile/MobileConfig;->_referrerTimeout:I

    mul-int/lit16 v0, v0, 0x3e8

    return v0
.end method

.method protected getReportSuiteIds()Ljava/lang/String;
    .locals 1

    .prologue
    .line 509
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_reportSuiteIds:Ljava/lang/String;

    return-object v0
.end method

.method protected getSSL()Z
    .locals 1

    .prologue
    .line 521
    iget-boolean v0, p0, Lcom/adobe/mobile/MobileConfig;->_ssl:Z

    return v0
.end method

.method protected getTrackingServer()Ljava/lang/String;
    .locals 1

    .prologue
    .line 513
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_trackingServer:Ljava/lang/String;

    return-object v0
.end method

.method protected getVisitorIdServiceEnabled()Z
    .locals 1

    .prologue
    .line 841
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_marketingCloudOrganizationId:Ljava/lang/String;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_marketingCloudOrganizationId:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected invokeAdobeDataCallback(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V
    .locals 2
    .param p1, "event"    # Lcom/adobe/mobile/Config$MobileDataEvent;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Lcom/adobe/mobile/Config$MobileDataEvent;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 490
    .local p2, "data":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_adobeDataCallback:Lcom/adobe/mobile/Config$AdobeDataCallback;

    if-nez v0, :cond_0

    .line 491
    const-string v0, "Config - A callback has not been registered for Adobe events."

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Object;

    invoke-static {v0, v1}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 501
    :goto_0
    return-void

    .line 495
    :cond_0
    if-eqz p2, :cond_1

    .line 496
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_adobeDataCallback:Lcom/adobe/mobile/Config$AdobeDataCallback;

    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1, p2}, Ljava/util/HashMap;-><init>(Ljava/util/Map;)V

    invoke-interface {v0, p1, v1}, Lcom/adobe/mobile/Config$AdobeDataCallback;->call(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V

    goto :goto_0

    .line 498
    :cond_1
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_adobeDataCallback:Lcom/adobe/mobile/Config$AdobeDataCallback;

    const/4 v1, 0x0

    invoke-interface {v0, p1, v1}, Lcom/adobe/mobile/Config$AdobeDataCallback;->call(Lcom/adobe/mobile/Config$MobileDataEvent;Ljava/util/Map;)V

    goto :goto_0
.end method

.method protected loadCachedRemotes()V
    .locals 1

    .prologue
    .line 919
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_messagesURL:Ljava/lang/String;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_messagesURL:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    .line 920
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_messagesURL:Ljava/lang/String;

    invoke-static {v0}, Lcom/adobe/mobile/RemoteDownload;->getFileForCachedURL(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/adobe/mobile/MobileConfig;->updateMessagesData(Ljava/io/File;)V

    .line 923
    :cond_0
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    if-eqz v0, :cond_1

    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_1

    .line 924
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_pointsOfInterestURL:Ljava/lang/String;

    invoke-static {v0}, Lcom/adobe/mobile/RemoteDownload;->getFileForCachedURL(Ljava/lang/String;)Ljava/io/File;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/adobe/mobile/MobileConfig;->updatePOIData(Ljava/io/File;)V

    .line 926
    :cond_1
    return-void
.end method

.method protected mobileReferrerConfigured()Z
    .locals 1

    .prologue
    .line 481
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_acquisitionServer:Ljava/lang/String;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_acquisitionAppIdentifier:Ljava/lang/String;

    if-eqz v0, :cond_0

    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_acquisitionServer:Ljava/lang/String;

    .line 482
    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_acquisitionAppIdentifier:Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected mobileUsingAnalytics()Z
    .locals 3

    .prologue
    const/4 v0, 0x0

    .line 411
    sget-object v1, Lcom/adobe/mobile/MobileConfig;->_usingAnalyticsMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 412
    :try_start_0
    iget-object v2, p0, Lcom/adobe/mobile/MobileConfig;->_usingAnalytics:Ljava/lang/Boolean;

    if-nez v2, :cond_1

    .line 414
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getReportSuiteIds()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 415
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getReportSuiteIds()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_0

    .line 416
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getTrackingServer()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_0

    .line 417
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getTrackingServer()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_0

    const/4 v0, 0x1

    .line 414
    :cond_0
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingAnalytics:Ljava/lang/Boolean;

    .line 419
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingAnalytics:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-nez v0, :cond_1

    .line 420
    const-string v0, "Analytics - Your config file is not set up to use Analytics(missing report suite id(s) or tracking server information)"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v0, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 423
    :cond_1
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingAnalytics:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    monitor-exit v1

    return v0

    .line 424
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected mobileUsingAudienceManager()Z
    .locals 3

    .prologue
    const/4 v0, 0x0

    .line 441
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 455
    :goto_0
    return v0

    .line 444
    :cond_0
    sget-object v1, Lcom/adobe/mobile/MobileConfig;->_usingAudienceManagerMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 445
    :try_start_0
    iget-object v2, p0, Lcom/adobe/mobile/MobileConfig;->_usingAudienceManager:Ljava/lang/Boolean;

    if-nez v2, :cond_2

    .line 447
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getAamServer()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 448
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getAamServer()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_1

    const/4 v0, 0x1

    .line 447
    :cond_1
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingAudienceManager:Ljava/lang/Boolean;

    .line 450
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingAudienceManager:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-nez v0, :cond_2

    .line 451
    const-string v0, "Audience Manager - Your config file is not set up to use Audience Manager(missing audience manager server information)"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v0, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 455
    :cond_2
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingAudienceManager:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    monitor-exit v1

    goto :goto_0

    .line 456
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected mobileUsingGooglePlayServices()Z
    .locals 2

    .prologue
    .line 430
    sget-object v1, Lcom/adobe/mobile/MobileConfig;->_usingGooglePlayServicesMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 431
    :try_start_0
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingGooglePlayServices:Ljava/lang/Boolean;

    if-nez v0, :cond_0

    .line 432
    invoke-static {}, Lcom/adobe/mobile/WearableFunctionBridge;->isGooglePlayServicesEnabled()Z

    move-result v0

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingGooglePlayServices:Ljava/lang/Boolean;

    .line 434
    :cond_0
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingGooglePlayServices:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    monitor-exit v1

    return v0

    .line 435
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected mobileUsingTarget()Z
    .locals 3

    .prologue
    const/4 v0, 0x0

    .line 462
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 476
    :goto_0
    return v0

    .line 465
    :cond_0
    sget-object v1, Lcom/adobe/mobile/MobileConfig;->_usingTargetMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 466
    :try_start_0
    iget-object v2, p0, Lcom/adobe/mobile/MobileConfig;->_usingTarget:Ljava/lang/Boolean;

    if-nez v2, :cond_2

    .line 468
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getClientCode()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 469
    invoke-virtual {p0}, Lcom/adobe/mobile/MobileConfig;->getClientCode()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_1

    const/4 v0, 0x1

    .line 468
    :cond_1
    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v0

    iput-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingTarget:Ljava/lang/Boolean;

    .line 471
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingTarget:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    if-nez v0, :cond_2

    .line 472
    const-string v0, "Target Worker - Your config file is not set up to use Target(missing client code information)"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v0, v2}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 476
    :cond_2
    iget-object v0, p0, Lcom/adobe/mobile/MobileConfig;->_usingTarget:Ljava/lang/Boolean;

    invoke-virtual {v0}, Ljava/lang/Boolean;->booleanValue()Z

    move-result v0

    monitor-exit v1

    goto :goto_0

    .line 477
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected networkConnectivity()Z
    .locals 1

    .prologue
    .line 1150
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v0

    if-nez v0, :cond_0

    iget-boolean v0, p0, Lcom/adobe/mobile/MobileConfig;->_networkConnectivity:Z

    if-eqz v0, :cond_1

    :cond_0
    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_1
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected setAdobeDataCallback(Lcom/adobe/mobile/Config$AdobeDataCallback;)V
    .locals 0
    .param p1, "callback"    # Lcom/adobe/mobile/Config$AdobeDataCallback;

    .prologue
    .line 486
    iput-object p1, p0, Lcom/adobe/mobile/MobileConfig;->_adobeDataCallback:Lcom/adobe/mobile/Config$AdobeDataCallback;

    .line 487
    return-void
.end method

.method protected setPrivacyStatus(Lcom/adobe/mobile/MobilePrivacyStatus;)V
    .locals 6
    .param p1, "status"    # Lcom/adobe/mobile/MobilePrivacyStatus;

    .prologue
    const/4 v5, 0x0

    .line 540
    if-nez p1, :cond_0

    .line 620
    :goto_0
    return-void

    .line 545
    :cond_0
    sget-object v2, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_UNKNOWN:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-ne p1, v2, :cond_1

    iget-boolean v2, p0, Lcom/adobe/mobile/MobileConfig;->_offlineTrackingEnabled:Z

    if-nez v2, :cond_1

    .line 546
    const-string v2, "Analytics - Cannot set privacy status to unknown when offline tracking is disabled"

    new-array v3, v5, [Ljava/lang/Object;

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 551
    :cond_1
    sget-object v2, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_IN:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-ne p1, v2, :cond_2

    .line 552
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    new-instance v3, Lcom/adobe/mobile/MobileConfig$1;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$1;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 559
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getThirdPartyCallbacksExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    new-instance v3, Lcom/adobe/mobile/MobileConfig$2;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$2;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 566
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getPIIExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    new-instance v3, Lcom/adobe/mobile/MobileConfig$3;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$3;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 575
    :cond_2
    sget-object v2, Lcom/adobe/mobile/MobilePrivacyStatus;->MOBILE_PRIVACY_STATUS_OPT_OUT:Lcom/adobe/mobile/MobilePrivacyStatus;

    if-ne p1, v2, :cond_3

    .line 576
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    new-instance v3, Lcom/adobe/mobile/MobileConfig$4;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$4;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 587
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getThirdPartyCallbacksExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    new-instance v3, Lcom/adobe/mobile/MobileConfig$5;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$5;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 594
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getPIIExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    new-instance v3, Lcom/adobe/mobile/MobileConfig$6;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$6;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 601
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAudienceExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v2

    new-instance v3, Lcom/adobe/mobile/MobileConfig$7;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$7;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-interface {v2, v3}, Ljava/util/concurrent/ExecutorService;->execute(Ljava/lang/Runnable;)V

    .line 610
    :cond_3
    iput-object p1, p0, Lcom/adobe/mobile/MobileConfig;->_privacyStatus:Lcom/adobe/mobile/MobilePrivacyStatus;

    .line 612
    invoke-virtual {p1}, Lcom/adobe/mobile/MobilePrivacyStatus;->getValue()I

    move-result v2

    invoke-static {v2}, Lcom/adobe/mobile/WearableFunctionBridge;->syncPrivacyStatusToWearable(I)V

    .line 614
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 615
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v2, "PrivacyStatus"

    invoke-virtual {p1}, Lcom/adobe/mobile/MobilePrivacyStatus;->getValue()I

    move-result v3

    invoke-interface {v1, v2, v3}, Landroid/content/SharedPreferences$Editor;->putInt(Ljava/lang/String;I)Landroid/content/SharedPreferences$Editor;

    .line 616
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto/16 :goto_0

    .line 617
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v0

    .line 618
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Config - Error persisting privacy status (%s)."

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v3, v5

    invoke-static {v2, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0
.end method

.method protected startNotifier()V
    .locals 7

    .prologue
    .line 1156
    new-instance v2, Landroid/content/IntentFilter;

    const-string v3, "android.net.conn.CONNECTIVITY_CHANGE"

    invoke-direct {v2, v3}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    .line 1158
    .local v2, "filter":Landroid/content/IntentFilter;
    const/4 v0, 0x0

    .line 1160
    .local v0, "appCtx":Landroid/content/Context;
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v0

    .line 1165
    :goto_0
    if-nez v0, :cond_0

    .line 1181
    :goto_1
    return-void

    .line 1161
    :catch_0
    move-exception v1

    .line 1162
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v3, "Analytics - Error registering network receiver (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 1167
    .end local v1    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :cond_0
    new-instance v3, Lcom/adobe/mobile/MobileConfig$13;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/MobileConfig$13;-><init>(Lcom/adobe/mobile/MobileConfig;)V

    invoke-virtual {v0, v3, v2}, Landroid/content/Context;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    goto :goto_1
.end method

.method protected updateMessagesData(Ljava/io/File;)V
    .locals 10
    .param p1, "file"    # Ljava/io/File;

    .prologue
    const/4 v9, 0x1

    const/4 v8, 0x0

    .line 754
    const/4 v1, 0x0

    .line 757
    .local v1, "fis":Ljava/io/FileInputStream;
    if-nez p1, :cond_1

    .line 773
    if-eqz v1, :cond_0

    .line 774
    :try_start_0
    invoke-virtual {v1}, Ljava/io/FileInputStream;->close()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 781
    :cond_0
    :goto_0
    return-void

    .line 777
    :catch_0
    move-exception v0

    .line 778
    .local v0, "e":Ljava/io/IOException;
    const-string v4, "Messages - Unable to close file stream (%s)"

    new-array v5, v9, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v8

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 761
    .end local v0    # "e":Ljava/io/IOException;
    :cond_1
    :try_start_1
    new-instance v2, Ljava/io/FileInputStream;

    invoke-direct {v2, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_4
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 762
    .end local v1    # "fis":Ljava/io/FileInputStream;
    .local v2, "fis":Ljava/io/FileInputStream;
    :try_start_2
    invoke-direct {p0, v2}, Lcom/adobe/mobile/MobileConfig;->loadConfigFromStream(Ljava/io/InputStream;)Lorg/json/JSONObject;

    move-result-object v3

    .line 763
    .local v3, "jsonData":Lorg/json/JSONObject;
    invoke-direct {p0, v3}, Lcom/adobe/mobile/MobileConfig;->loadMessagesDataFromRemote(Lorg/json/JSONObject;)V
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_8
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_7
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 773
    if-eqz v2, :cond_2

    .line 774
    :try_start_3
    invoke-virtual {v2}, Ljava/io/FileInputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_1

    :cond_2
    move-object v1, v2

    .line 779
    .end local v2    # "fis":Ljava/io/FileInputStream;
    .restart local v1    # "fis":Ljava/io/FileInputStream;
    goto :goto_0

    .line 777
    .end local v1    # "fis":Ljava/io/FileInputStream;
    .restart local v2    # "fis":Ljava/io/FileInputStream;
    :catch_1
    move-exception v0

    .line 778
    .restart local v0    # "e":Ljava/io/IOException;
    const-string v4, "Messages - Unable to close file stream (%s)"

    new-array v5, v9, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v8

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v1, v2

    .line 780
    .end local v2    # "fis":Ljava/io/FileInputStream;
    .restart local v1    # "fis":Ljava/io/FileInputStream;
    goto :goto_0

    .line 765
    .end local v0    # "e":Ljava/io/IOException;
    .end local v3    # "jsonData":Lorg/json/JSONObject;
    :catch_2
    move-exception v0

    .line 766
    .local v0, "e":Lorg/json/JSONException;
    :goto_1
    :try_start_4
    const-string v4, "Messages - Unable to read messages remote config file, falling back to bundled messages (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    .line 773
    if-eqz v1, :cond_0

    .line 774
    :try_start_5
    invoke-virtual {v1}, Ljava/io/FileInputStream;->close()V
    :try_end_5
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_3

    goto :goto_0

    .line 777
    :catch_3
    move-exception v0

    .line 778
    .local v0, "e":Ljava/io/IOException;
    const-string v4, "Messages - Unable to close file stream (%s)"

    new-array v5, v9, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v8

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 768
    .end local v0    # "e":Ljava/io/IOException;
    :catch_4
    move-exception v0

    .line 769
    .restart local v0    # "e":Ljava/io/IOException;
    :goto_2
    :try_start_6
    const-string v4, "Messages - Unable to open messages config file, falling back to bundled messages (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    .line 773
    if-eqz v1, :cond_0

    .line 774
    :try_start_7
    invoke-virtual {v1}, Ljava/io/FileInputStream;->close()V
    :try_end_7
    .catch Ljava/io/IOException; {:try_start_7 .. :try_end_7} :catch_5

    goto :goto_0

    .line 777
    :catch_5
    move-exception v0

    .line 778
    const-string v4, "Messages - Unable to close file stream (%s)"

    new-array v5, v9, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v8

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 772
    .end local v0    # "e":Ljava/io/IOException;
    :catchall_0
    move-exception v4

    .line 773
    :goto_3
    if-eqz v1, :cond_3

    .line 774
    :try_start_8
    invoke-virtual {v1}, Ljava/io/FileInputStream;->close()V
    :try_end_8
    .catch Ljava/io/IOException; {:try_start_8 .. :try_end_8} :catch_6

    .line 779
    :cond_3
    :goto_4
    throw v4

    .line 777
    :catch_6
    move-exception v0

    .line 778
    .restart local v0    # "e":Ljava/io/IOException;
    const-string v5, "Messages - Unable to close file stream (%s)"

    new-array v6, v9, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v6, v8

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_4

    .line 772
    .end local v0    # "e":Ljava/io/IOException;
    .end local v1    # "fis":Ljava/io/FileInputStream;
    .restart local v2    # "fis":Ljava/io/FileInputStream;
    :catchall_1
    move-exception v4

    move-object v1, v2

    .end local v2    # "fis":Ljava/io/FileInputStream;
    .restart local v1    # "fis":Ljava/io/FileInputStream;
    goto :goto_3

    .line 768
    .end local v1    # "fis":Ljava/io/FileInputStream;
    .restart local v2    # "fis":Ljava/io/FileInputStream;
    :catch_7
    move-exception v0

    move-object v1, v2

    .end local v2    # "fis":Ljava/io/FileInputStream;
    .restart local v1    # "fis":Ljava/io/FileInputStream;
    goto :goto_2

    .line 765
    .end local v1    # "fis":Ljava/io/FileInputStream;
    .restart local v2    # "fis":Ljava/io/FileInputStream;
    :catch_8
    move-exception v0

    move-object v1, v2

    .end local v2    # "fis":Ljava/io/FileInputStream;
    .restart local v1    # "fis":Ljava/io/FileInputStream;
    goto :goto_1
.end method

.method protected updatePOIData(Ljava/io/File;)V
    .locals 13
    .param p1, "file"    # Ljava/io/File;

    .prologue
    const/4 v12, 0x1

    const/4 v11, 0x0

    .line 788
    const/4 v3, 0x0

    .line 791
    .local v3, "fis":Ljava/io/FileInputStream;
    if-nez p1, :cond_1

    .line 811
    if-eqz v3, :cond_0

    .line 812
    :try_start_0
    invoke-virtual {v3}, Ljava/io/FileInputStream;->close()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 819
    :cond_0
    :goto_0
    return-void

    .line 815
    :catch_0
    move-exception v1

    .line 816
    .local v1, "e":Ljava/io/IOException;
    const-string v7, "Config - Unable to close file stream (%s)"

    new-array v8, v12, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v11

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 795
    .end local v1    # "e":Ljava/io/IOException;
    :cond_1
    :try_start_1
    new-instance v4, Ljava/io/FileInputStream;

    invoke-direct {v4, p1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V
    :try_end_1
    .catch Lorg/json/JSONException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_4
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 796
    .end local v3    # "fis":Ljava/io/FileInputStream;
    .local v4, "fis":Ljava/io/FileInputStream;
    :try_start_2
    invoke-direct {p0, v4}, Lcom/adobe/mobile/MobileConfig;->loadConfigFromStream(Ljava/io/InputStream;)Lorg/json/JSONObject;

    move-result-object v5

    .line 797
    .local v5, "jsonData":Lorg/json/JSONObject;
    if-eqz v5, :cond_2

    .line 798
    const-string v7, "analytics"

    invoke-virtual {v5, v7}, Lorg/json/JSONObject;->getJSONObject(Ljava/lang/String;)Lorg/json/JSONObject;

    move-result-object v0

    .line 799
    .local v0, "analytics":Lorg/json/JSONObject;
    const-string v7, "poi"

    invoke-virtual {v0, v7}, Lorg/json/JSONObject;->getJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v6

    .line 800
    .local v6, "poiArray":Lorg/json/JSONArray;
    invoke-direct {p0, v6}, Lcom/adobe/mobile/MobileConfig;->loadPoiFromJsonArray(Lorg/json/JSONArray;)V
    :try_end_2
    .catch Lorg/json/JSONException; {:try_start_2 .. :try_end_2} :catch_8
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_7
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 811
    .end local v0    # "analytics":Lorg/json/JSONObject;
    .end local v6    # "poiArray":Lorg/json/JSONArray;
    :cond_2
    if-eqz v4, :cond_3

    .line 812
    :try_start_3
    invoke-virtual {v4}, Ljava/io/FileInputStream;->close()V
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_1

    :cond_3
    move-object v3, v4

    .line 817
    .end local v4    # "fis":Ljava/io/FileInputStream;
    .restart local v3    # "fis":Ljava/io/FileInputStream;
    goto :goto_0

    .line 815
    .end local v3    # "fis":Ljava/io/FileInputStream;
    .restart local v4    # "fis":Ljava/io/FileInputStream;
    :catch_1
    move-exception v1

    .line 816
    .restart local v1    # "e":Ljava/io/IOException;
    const-string v7, "Config - Unable to close file stream (%s)"

    new-array v8, v12, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v11

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object v3, v4

    .line 818
    .end local v4    # "fis":Ljava/io/FileInputStream;
    .restart local v3    # "fis":Ljava/io/FileInputStream;
    goto :goto_0

    .line 803
    .end local v1    # "e":Ljava/io/IOException;
    .end local v5    # "jsonData":Lorg/json/JSONObject;
    :catch_2
    move-exception v2

    .line 804
    .local v2, "ex":Lorg/json/JSONException;
    :goto_1
    :try_start_4
    const-string v7, "Config - Unable to read points of interest remote config file, falling back to bundled poi (%s)"

    const/4 v8, 0x1

    new-array v8, v8, [Ljava/lang/Object;

    const/4 v9, 0x0

    invoke-virtual {v2}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v8, v9

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    .line 811
    if-eqz v3, :cond_0

    .line 812
    :try_start_5
    invoke-virtual {v3}, Ljava/io/FileInputStream;->close()V
    :try_end_5
    .catch Ljava/io/IOException; {:try_start_5 .. :try_end_5} :catch_3

    goto :goto_0

    .line 815
    :catch_3
    move-exception v1

    .line 816
    .restart local v1    # "e":Ljava/io/IOException;
    const-string v7, "Config - Unable to close file stream (%s)"

    new-array v8, v12, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v11

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 806
    .end local v1    # "e":Ljava/io/IOException;
    .end local v2    # "ex":Lorg/json/JSONException;
    :catch_4
    move-exception v2

    .line 807
    .local v2, "ex":Ljava/io/IOException;
    :goto_2
    :try_start_6
    const-string v7, "Config - Unable to open points of interest config file, falling back to bundled poi (%s)"

    const/4 v8, 0x1

    new-array v8, v8, [Ljava/lang/Object;

    const/4 v9, 0x0

    invoke-virtual {v2}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v8, v9

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    .line 811
    if-eqz v3, :cond_0

    .line 812
    :try_start_7
    invoke-virtual {v3}, Ljava/io/FileInputStream;->close()V
    :try_end_7
    .catch Ljava/io/IOException; {:try_start_7 .. :try_end_7} :catch_5

    goto :goto_0

    .line 815
    :catch_5
    move-exception v1

    .line 816
    .restart local v1    # "e":Ljava/io/IOException;
    const-string v7, "Config - Unable to close file stream (%s)"

    new-array v8, v12, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v11

    invoke-static {v7, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto/16 :goto_0

    .line 810
    .end local v1    # "e":Ljava/io/IOException;
    .end local v2    # "ex":Ljava/io/IOException;
    :catchall_0
    move-exception v7

    .line 811
    :goto_3
    if-eqz v3, :cond_4

    .line 812
    :try_start_8
    invoke-virtual {v3}, Ljava/io/FileInputStream;->close()V
    :try_end_8
    .catch Ljava/io/IOException; {:try_start_8 .. :try_end_8} :catch_6

    .line 817
    :cond_4
    :goto_4
    throw v7

    .line 815
    :catch_6
    move-exception v1

    .line 816
    .restart local v1    # "e":Ljava/io/IOException;
    const-string v8, "Config - Unable to close file stream (%s)"

    new-array v9, v12, [Ljava/lang/Object;

    invoke-virtual {v1}, Ljava/io/IOException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v10

    aput-object v10, v9, v11

    invoke-static {v8, v9}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_4

    .line 810
    .end local v1    # "e":Ljava/io/IOException;
    .end local v3    # "fis":Ljava/io/FileInputStream;
    .restart local v4    # "fis":Ljava/io/FileInputStream;
    :catchall_1
    move-exception v7

    move-object v3, v4

    .end local v4    # "fis":Ljava/io/FileInputStream;
    .restart local v3    # "fis":Ljava/io/FileInputStream;
    goto :goto_3

    .line 806
    .end local v3    # "fis":Ljava/io/FileInputStream;
    .restart local v4    # "fis":Ljava/io/FileInputStream;
    :catch_7
    move-exception v2

    move-object v3, v4

    .end local v4    # "fis":Ljava/io/FileInputStream;
    .restart local v3    # "fis":Ljava/io/FileInputStream;
    goto :goto_2

    .line 803
    .end local v3    # "fis":Ljava/io/FileInputStream;
    .restart local v4    # "fis":Ljava/io/FileInputStream;
    :catch_8
    move-exception v2

    move-object v3, v4

    .end local v4    # "fis":Ljava/io/FileInputStream;
    .restart local v3    # "fis":Ljava/io/FileInputStream;
    goto :goto_1
.end method
