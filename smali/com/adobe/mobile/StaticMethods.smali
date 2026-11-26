.class final Lcom/adobe/mobile/StaticMethods;
.super Ljava/lang/Object;
.source "StaticMethods.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/adobe/mobile/StaticMethods$NullActivityException;,
        Lcom/adobe/mobile/StaticMethods$NullContextException;
    }
.end annotation


# static fields
.field private static final ADID_DPID:Ljava/lang/String; = "20914"

.field private static final AID_JSON_ID_KEY:Ljava/lang/String; = "id"

.field private static final BYTE_TO_HEX:[C

.field private static final CARRIER_NAME_KEY:Ljava/lang/String; = "a.CarrierName"

.field private static final DEVICE_NAME_KEY:Ljava/lang/String; = "a.DeviceName"

.field protected static final LAST_KNOWN_TIMESTAMP_KEY:Ljava/lang/String; = "ADBLastKnownTimestampKey"

.field protected static final LIBRARY_VERSION:Ljava/lang/String; = "4.10.0-AN"

.field private static final MCPNS_DPID:Ljava/lang/String; = "20919"

.field private static final NO_ACTIVITY_MESSAGE:Ljava/lang/String; = "Message - No Current Activity (Messages must have a reference to the current activity to work properly)"

.field private static final NO_CONTEXT_MESSAGE:Ljava/lang/String; = "Config - No Application Context (Application context must be set prior to calling any library functions.)"

.field private static final NO_SHARED_PREFERENCES_EDITOR_MESSAGE:Ljava/lang/String; = "Config - Unable to create an instance of a SharedPreferences Editor"

.field private static final NO_SHARED_PREFERENCES_MESSAGE:Ljava/lang/String; = "Config - No SharedPreferences available"

.field private static final PUSH_ID_ENABLED_ACTION_NAME:Ljava/lang/String; = "Push"

.field private static final RESOLUTION_KEY:Ljava/lang/String; = "a.Resolution"

.field private static final RUN_MODE_APPLICATION:Ljava/lang/String; = "Application"

.field private static final RUN_MODE_EXTENSION:Ljava/lang/String; = "Extension"

.field private static final RUN_MODE_KEY:Ljava/lang/String; = "a.RunMode"

.field protected static final TIME_SINCE_LAUNCH_KEY:Ljava/lang/String; = "a.TimeSinceLaunch"

.field private static _activity:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference",
            "<",
            "Landroid/app/Activity;",
            ">;"
        }
    .end annotation
.end field

.field private static final _advertisingIdentifierMutex:Ljava/lang/Object;

.field private static _aidDone:Z

.field private static final _aidMutex:Ljava/lang/Object;

.field private static final _analyticsExecutorMutex:Ljava/lang/Object;

.field private static _appType:Lcom/adobe/mobile/Config$ApplicationType;

.field private static final _applicationIDMutex:Ljava/lang/Object;

.field private static final _applicationNameMutex:Ljava/lang/Object;

.field private static final _applicationVersionCodeMutex:Ljava/lang/Object;

.field private static final _applicationVersionMutex:Ljava/lang/Object;

.field private static final _audienceExecutorMutex:Ljava/lang/Object;

.field private static final _carrierMutex:Ljava/lang/Object;

.field static final _contextDataKeyWhiteList:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;"
        }
    .end annotation
.end field

.field static _contextDataKeyWhiteListCount:I

.field private static final _currentActivityMutex:Ljava/lang/Object;

.field private static _debugLogging:Z

.field private static final _defaultDataMutex:Ljava/lang/Object;

.field private static _isWearable:Z

.field private static final _mediaExecutorMutex:Ljava/lang/Object;

.field private static final _messageImageCachingExecutorMutex:Ljava/lang/Object;

.field private static final _messagesExecutorMutex:Ljava/lang/Object;

.field private static final _operatingSystemMutex:Ljava/lang/Object;

.field private static final _piiExecutorMutex:Ljava/lang/Object;

.field private static final _pushEnabledMutex:Ljava/lang/Object;

.field private static final _pushIdentifierMutex:Ljava/lang/Object;

.field private static final _resolutionMutex:Ljava/lang/Object;

.field private static final _sharedExecutorMutex:Ljava/lang/Object;

.field private static final _sharedPreferencesMutex:Ljava/lang/Object;

.field private static final _thirdPartyCallbacksExecutorMutex:Ljava/lang/Object;

.field private static final _timedActionsExecutorMutex:Ljava/lang/Object;

.field private static final _timestampMutex:Ljava/lang/Object;

.field private static final _userAgentMutex:Ljava/lang/Object;

.field private static final _userIdentifierMutex:Ljava/lang/Object;

.field private static final _visitorIDMutex:Ljava/lang/Object;

.field private static advertisingIdentifier:Ljava/lang/String;

.field private static aid:Ljava/lang/String;

.field private static analyticsExecutor:Ljava/util/concurrent/ExecutorService;

.field private static appID:Ljava/lang/String;

.field private static appName:Ljava/lang/String;

.field private static audienceExecutor:Ljava/util/concurrent/ExecutorService;

.field private static carrier:Ljava/lang/String;

.field private static final contextDataMask:[Z

.field private static defaultData:Ljava/util/HashMap;
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

.field private static final encodedChars:[Ljava/lang/String;

.field private static mediaExecutor:Ljava/util/concurrent/ExecutorService;

.field private static messageImageCachingExecutor:Ljava/util/concurrent/ExecutorService;

.field private static messagesExecutor:Ljava/util/concurrent/ExecutorService;

.field private static operatingSystem:Ljava/lang/String;

.field private static piiExecutor:Ljava/util/concurrent/ExecutorService;

.field private static prefs:Landroid/content/SharedPreferences;

.field private static pushEnabled:Z

.field private static pushIdentifier:Ljava/lang/String;

.field private static resolution:Ljava/lang/String;

.field private static sharedContext:Landroid/content/Context;

.field private static sharedExecutor:Ljava/util/concurrent/ExecutorService;

.field private static thirdPartyCallbacksExecutor:Ljava/util/concurrent/ExecutorService;

.field private static timedActionsExecutor:Ljava/util/concurrent/ExecutorService;

.field private static timestamp:Ljava/lang/String;

.field private static userAgent:Ljava/lang/String;

.field private static final utf8Mask:[Z

.field private static versionCode:I

.field private static versionName:Ljava/lang/String;

.field private static visitorID:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 6

    .prologue
    const/16 v5, 0x100

    const/4 v4, 0x0

    const/4 v3, 0x0

    .line 98
    new-array v0, v5, [Ljava/lang/String;

    const-string v1, "%00"

    aput-object v1, v0, v4

    const/4 v1, 0x1

    const-string v2, "%01"

    aput-object v2, v0, v1

    const/4 v1, 0x2

    const-string v2, "%02"

    aput-object v2, v0, v1

    const/4 v1, 0x3

    const-string v2, "%03"

    aput-object v2, v0, v1

    const/4 v1, 0x4

    const-string v2, "%04"

    aput-object v2, v0, v1

    const/4 v1, 0x5

    const-string v2, "%05"

    aput-object v2, v0, v1

    const/4 v1, 0x6

    const-string v2, "%06"

    aput-object v2, v0, v1

    const/4 v1, 0x7

    const-string v2, "%07"

    aput-object v2, v0, v1

    const/16 v1, 0x8

    const-string v2, "%08"

    aput-object v2, v0, v1

    const/16 v1, 0x9

    const-string v2, "%09"

    aput-object v2, v0, v1

    const/16 v1, 0xa

    const-string v2, "%0A"

    aput-object v2, v0, v1

    const/16 v1, 0xb

    const-string v2, "%0B"

    aput-object v2, v0, v1

    const/16 v1, 0xc

    const-string v2, "%0C"

    aput-object v2, v0, v1

    const/16 v1, 0xd

    const-string v2, "%0D"

    aput-object v2, v0, v1

    const/16 v1, 0xe

    const-string v2, "%0E"

    aput-object v2, v0, v1

    const/16 v1, 0xf

    const-string v2, "%0F"

    aput-object v2, v0, v1

    const/16 v1, 0x10

    const-string v2, "%10"

    aput-object v2, v0, v1

    const/16 v1, 0x11

    const-string v2, "%11"

    aput-object v2, v0, v1

    const/16 v1, 0x12

    const-string v2, "%12"

    aput-object v2, v0, v1

    const/16 v1, 0x13

    const-string v2, "%13"

    aput-object v2, v0, v1

    const/16 v1, 0x14

    const-string v2, "%14"

    aput-object v2, v0, v1

    const/16 v1, 0x15

    const-string v2, "%15"

    aput-object v2, v0, v1

    const/16 v1, 0x16

    const-string v2, "%16"

    aput-object v2, v0, v1

    const/16 v1, 0x17

    const-string v2, "%17"

    aput-object v2, v0, v1

    const/16 v1, 0x18

    const-string v2, "%18"

    aput-object v2, v0, v1

    const/16 v1, 0x19

    const-string v2, "%19"

    aput-object v2, v0, v1

    const/16 v1, 0x1a

    const-string v2, "%1A"

    aput-object v2, v0, v1

    const/16 v1, 0x1b

    const-string v2, "%1B"

    aput-object v2, v0, v1

    const/16 v1, 0x1c

    const-string v2, "%1C"

    aput-object v2, v0, v1

    const/16 v1, 0x1d

    const-string v2, "%1D"

    aput-object v2, v0, v1

    const/16 v1, 0x1e

    const-string v2, "%1E"

    aput-object v2, v0, v1

    const/16 v1, 0x1f

    const-string v2, "%1F"

    aput-object v2, v0, v1

    const/16 v1, 0x20

    const-string v2, "%20"

    aput-object v2, v0, v1

    const/16 v1, 0x21

    const-string v2, "%21"

    aput-object v2, v0, v1

    const/16 v1, 0x22

    const-string v2, "%22"

    aput-object v2, v0, v1

    const/16 v1, 0x23

    const-string v2, "%23"

    aput-object v2, v0, v1

    const/16 v1, 0x24

    const-string v2, "%24"

    aput-object v2, v0, v1

    const/16 v1, 0x25

    const-string v2, "%25"

    aput-object v2, v0, v1

    const/16 v1, 0x26

    const-string v2, "%26"

    aput-object v2, v0, v1

    const/16 v1, 0x27

    const-string v2, "%27"

    aput-object v2, v0, v1

    const/16 v1, 0x28

    const-string v2, "%28"

    aput-object v2, v0, v1

    const/16 v1, 0x29

    const-string v2, "%29"

    aput-object v2, v0, v1

    const/16 v1, 0x2a

    const-string v2, "*"

    aput-object v2, v0, v1

    const/16 v1, 0x2b

    const-string v2, "%2B"

    aput-object v2, v0, v1

    const/16 v1, 0x2c

    const-string v2, "%2C"

    aput-object v2, v0, v1

    const/16 v1, 0x2d

    const-string v2, "-"

    aput-object v2, v0, v1

    const/16 v1, 0x2e

    const-string v2, "."

    aput-object v2, v0, v1

    const/16 v1, 0x2f

    const-string v2, "%2F"

    aput-object v2, v0, v1

    const/16 v1, 0x30

    const-string v2, "0"

    aput-object v2, v0, v1

    const/16 v1, 0x31

    const-string v2, "1"

    aput-object v2, v0, v1

    const/16 v1, 0x32

    const-string v2, "2"

    aput-object v2, v0, v1

    const/16 v1, 0x33

    const-string v2, "3"

    aput-object v2, v0, v1

    const/16 v1, 0x34

    const-string v2, "4"

    aput-object v2, v0, v1

    const/16 v1, 0x35

    const-string v2, "5"

    aput-object v2, v0, v1

    const/16 v1, 0x36

    const-string v2, "6"

    aput-object v2, v0, v1

    const/16 v1, 0x37

    const-string v2, "7"

    aput-object v2, v0, v1

    const/16 v1, 0x38

    const-string v2, "8"

    aput-object v2, v0, v1

    const/16 v1, 0x39

    const-string v2, "9"

    aput-object v2, v0, v1

    const/16 v1, 0x3a

    const-string v2, "%3A"

    aput-object v2, v0, v1

    const/16 v1, 0x3b

    const-string v2, "%3B"

    aput-object v2, v0, v1

    const/16 v1, 0x3c

    const-string v2, "%3C"

    aput-object v2, v0, v1

    const/16 v1, 0x3d

    const-string v2, "%3D"

    aput-object v2, v0, v1

    const/16 v1, 0x3e

    const-string v2, "%3E"

    aput-object v2, v0, v1

    const/16 v1, 0x3f

    const-string v2, "%3F"

    aput-object v2, v0, v1

    const/16 v1, 0x40

    const-string v2, "%40"

    aput-object v2, v0, v1

    const/16 v1, 0x41

    const-string v2, "A"

    aput-object v2, v0, v1

    const/16 v1, 0x42

    const-string v2, "B"

    aput-object v2, v0, v1

    const/16 v1, 0x43

    const-string v2, "C"

    aput-object v2, v0, v1

    const/16 v1, 0x44

    const-string v2, "D"

    aput-object v2, v0, v1

    const/16 v1, 0x45

    const-string v2, "E"

    aput-object v2, v0, v1

    const/16 v1, 0x46

    const-string v2, "F"

    aput-object v2, v0, v1

    const/16 v1, 0x47

    const-string v2, "G"

    aput-object v2, v0, v1

    const/16 v1, 0x48

    const-string v2, "H"

    aput-object v2, v0, v1

    const/16 v1, 0x49

    const-string v2, "I"

    aput-object v2, v0, v1

    const/16 v1, 0x4a

    const-string v2, "J"

    aput-object v2, v0, v1

    const/16 v1, 0x4b

    const-string v2, "K"

    aput-object v2, v0, v1

    const/16 v1, 0x4c

    const-string v2, "L"

    aput-object v2, v0, v1

    const/16 v1, 0x4d

    const-string v2, "M"

    aput-object v2, v0, v1

    const/16 v1, 0x4e

    const-string v2, "N"

    aput-object v2, v0, v1

    const/16 v1, 0x4f

    const-string v2, "O"

    aput-object v2, v0, v1

    const/16 v1, 0x50

    const-string v2, "P"

    aput-object v2, v0, v1

    const/16 v1, 0x51

    const-string v2, "Q"

    aput-object v2, v0, v1

    const/16 v1, 0x52

    const-string v2, "R"

    aput-object v2, v0, v1

    const/16 v1, 0x53

    const-string v2, "S"

    aput-object v2, v0, v1

    const/16 v1, 0x54

    const-string v2, "T"

    aput-object v2, v0, v1

    const/16 v1, 0x55

    const-string v2, "U"

    aput-object v2, v0, v1

    const/16 v1, 0x56

    const-string v2, "V"

    aput-object v2, v0, v1

    const/16 v1, 0x57

    const-string v2, "W"

    aput-object v2, v0, v1

    const/16 v1, 0x58

    const-string v2, "X"

    aput-object v2, v0, v1

    const/16 v1, 0x59

    const-string v2, "Y"

    aput-object v2, v0, v1

    const/16 v1, 0x5a

    const-string v2, "Z"

    aput-object v2, v0, v1

    const/16 v1, 0x5b

    const-string v2, "%5B"

    aput-object v2, v0, v1

    const/16 v1, 0x5c

    const-string v2, "%5C"

    aput-object v2, v0, v1

    const/16 v1, 0x5d

    const-string v2, "%5D"

    aput-object v2, v0, v1

    const/16 v1, 0x5e

    const-string v2, "%5E"

    aput-object v2, v0, v1

    const/16 v1, 0x5f

    const-string v2, "_"

    aput-object v2, v0, v1

    const/16 v1, 0x60

    const-string v2, "%60"

    aput-object v2, v0, v1

    const/16 v1, 0x61

    const-string v2, "a"

    aput-object v2, v0, v1

    const/16 v1, 0x62

    const-string v2, "b"

    aput-object v2, v0, v1

    const/16 v1, 0x63

    const-string v2, "c"

    aput-object v2, v0, v1

    const/16 v1, 0x64

    const-string v2, "d"

    aput-object v2, v0, v1

    const/16 v1, 0x65

    const-string v2, "e"

    aput-object v2, v0, v1

    const/16 v1, 0x66

    const-string v2, "f"

    aput-object v2, v0, v1

    const/16 v1, 0x67

    const-string v2, "g"

    aput-object v2, v0, v1

    const/16 v1, 0x68

    const-string v2, "h"

    aput-object v2, v0, v1

    const/16 v1, 0x69

    const-string v2, "i"

    aput-object v2, v0, v1

    const/16 v1, 0x6a

    const-string v2, "j"

    aput-object v2, v0, v1

    const/16 v1, 0x6b

    const-string v2, "k"

    aput-object v2, v0, v1

    const/16 v1, 0x6c

    const-string v2, "l"

    aput-object v2, v0, v1

    const/16 v1, 0x6d

    const-string v2, "m"

    aput-object v2, v0, v1

    const/16 v1, 0x6e

    const-string v2, "n"

    aput-object v2, v0, v1

    const/16 v1, 0x6f

    const-string v2, "o"

    aput-object v2, v0, v1

    const/16 v1, 0x70

    const-string v2, "p"

    aput-object v2, v0, v1

    const/16 v1, 0x71

    const-string v2, "q"

    aput-object v2, v0, v1

    const/16 v1, 0x72

    const-string v2, "r"

    aput-object v2, v0, v1

    const/16 v1, 0x73

    const-string v2, "s"

    aput-object v2, v0, v1

    const/16 v1, 0x74

    const-string v2, "t"

    aput-object v2, v0, v1

    const/16 v1, 0x75

    const-string v2, "u"

    aput-object v2, v0, v1

    const/16 v1, 0x76

    const-string v2, "v"

    aput-object v2, v0, v1

    const/16 v1, 0x77

    const-string v2, "w"

    aput-object v2, v0, v1

    const/16 v1, 0x78

    const-string v2, "x"

    aput-object v2, v0, v1

    const/16 v1, 0x79

    const-string v2, "y"

    aput-object v2, v0, v1

    const/16 v1, 0x7a

    const-string v2, "z"

    aput-object v2, v0, v1

    const/16 v1, 0x7b

    const-string v2, "%7B"

    aput-object v2, v0, v1

    const/16 v1, 0x7c

    const-string v2, "%7C"

    aput-object v2, v0, v1

    const/16 v1, 0x7d

    const-string v2, "%7D"

    aput-object v2, v0, v1

    const/16 v1, 0x7e

    const-string v2, "%7E"

    aput-object v2, v0, v1

    const/16 v1, 0x7f

    const-string v2, "%7F"

    aput-object v2, v0, v1

    const/16 v1, 0x80

    const-string v2, "%80"

    aput-object v2, v0, v1

    const/16 v1, 0x81

    const-string v2, "%81"

    aput-object v2, v0, v1

    const/16 v1, 0x82

    const-string v2, "%82"

    aput-object v2, v0, v1

    const/16 v1, 0x83

    const-string v2, "%83"

    aput-object v2, v0, v1

    const/16 v1, 0x84

    const-string v2, "%84"

    aput-object v2, v0, v1

    const/16 v1, 0x85

    const-string v2, "%85"

    aput-object v2, v0, v1

    const/16 v1, 0x86

    const-string v2, "%86"

    aput-object v2, v0, v1

    const/16 v1, 0x87

    const-string v2, "%87"

    aput-object v2, v0, v1

    const/16 v1, 0x88

    const-string v2, "%88"

    aput-object v2, v0, v1

    const/16 v1, 0x89

    const-string v2, "%89"

    aput-object v2, v0, v1

    const/16 v1, 0x8a

    const-string v2, "%8A"

    aput-object v2, v0, v1

    const/16 v1, 0x8b

    const-string v2, "%8B"

    aput-object v2, v0, v1

    const/16 v1, 0x8c

    const-string v2, "%8C"

    aput-object v2, v0, v1

    const/16 v1, 0x8d

    const-string v2, "%8D"

    aput-object v2, v0, v1

    const/16 v1, 0x8e

    const-string v2, "%8E"

    aput-object v2, v0, v1

    const/16 v1, 0x8f

    const-string v2, "%8F"

    aput-object v2, v0, v1

    const/16 v1, 0x90

    const-string v2, "%90"

    aput-object v2, v0, v1

    const/16 v1, 0x91

    const-string v2, "%91"

    aput-object v2, v0, v1

    const/16 v1, 0x92

    const-string v2, "%92"

    aput-object v2, v0, v1

    const/16 v1, 0x93

    const-string v2, "%93"

    aput-object v2, v0, v1

    const/16 v1, 0x94

    const-string v2, "%94"

    aput-object v2, v0, v1

    const/16 v1, 0x95

    const-string v2, "%95"

    aput-object v2, v0, v1

    const/16 v1, 0x96

    const-string v2, "%96"

    aput-object v2, v0, v1

    const/16 v1, 0x97

    const-string v2, "%97"

    aput-object v2, v0, v1

    const/16 v1, 0x98

    const-string v2, "%98"

    aput-object v2, v0, v1

    const/16 v1, 0x99

    const-string v2, "%99"

    aput-object v2, v0, v1

    const/16 v1, 0x9a

    const-string v2, "%9A"

    aput-object v2, v0, v1

    const/16 v1, 0x9b

    const-string v2, "%9B"

    aput-object v2, v0, v1

    const/16 v1, 0x9c

    const-string v2, "%9C"

    aput-object v2, v0, v1

    const/16 v1, 0x9d

    const-string v2, "%9D"

    aput-object v2, v0, v1

    const/16 v1, 0x9e

    const-string v2, "%9E"

    aput-object v2, v0, v1

    const/16 v1, 0x9f

    const-string v2, "%9F"

    aput-object v2, v0, v1

    const/16 v1, 0xa0

    const-string v2, "%A0"

    aput-object v2, v0, v1

    const/16 v1, 0xa1

    const-string v2, "%A1"

    aput-object v2, v0, v1

    const/16 v1, 0xa2

    const-string v2, "%A2"

    aput-object v2, v0, v1

    const/16 v1, 0xa3

    const-string v2, "%A3"

    aput-object v2, v0, v1

    const/16 v1, 0xa4

    const-string v2, "%A4"

    aput-object v2, v0, v1

    const/16 v1, 0xa5

    const-string v2, "%A5"

    aput-object v2, v0, v1

    const/16 v1, 0xa6

    const-string v2, "%A6"

    aput-object v2, v0, v1

    const/16 v1, 0xa7

    const-string v2, "%A7"

    aput-object v2, v0, v1

    const/16 v1, 0xa8

    const-string v2, "%A8"

    aput-object v2, v0, v1

    const/16 v1, 0xa9

    const-string v2, "%A9"

    aput-object v2, v0, v1

    const/16 v1, 0xaa

    const-string v2, "%AA"

    aput-object v2, v0, v1

    const/16 v1, 0xab

    const-string v2, "%AB"

    aput-object v2, v0, v1

    const/16 v1, 0xac

    const-string v2, "%AC"

    aput-object v2, v0, v1

    const/16 v1, 0xad

    const-string v2, "%AD"

    aput-object v2, v0, v1

    const/16 v1, 0xae

    const-string v2, "%AE"

    aput-object v2, v0, v1

    const/16 v1, 0xaf

    const-string v2, "%AF"

    aput-object v2, v0, v1

    const/16 v1, 0xb0

    const-string v2, "%B0"

    aput-object v2, v0, v1

    const/16 v1, 0xb1

    const-string v2, "%B1"

    aput-object v2, v0, v1

    const/16 v1, 0xb2

    const-string v2, "%B2"

    aput-object v2, v0, v1

    const/16 v1, 0xb3

    const-string v2, "%B3"

    aput-object v2, v0, v1

    const/16 v1, 0xb4

    const-string v2, "%B4"

    aput-object v2, v0, v1

    const/16 v1, 0xb5

    const-string v2, "%B5"

    aput-object v2, v0, v1

    const/16 v1, 0xb6

    const-string v2, "%B6"

    aput-object v2, v0, v1

    const/16 v1, 0xb7

    const-string v2, "%B7"

    aput-object v2, v0, v1

    const/16 v1, 0xb8

    const-string v2, "%B8"

    aput-object v2, v0, v1

    const/16 v1, 0xb9

    const-string v2, "%B9"

    aput-object v2, v0, v1

    const/16 v1, 0xba

    const-string v2, "%BA"

    aput-object v2, v0, v1

    const/16 v1, 0xbb

    const-string v2, "%BB"

    aput-object v2, v0, v1

    const/16 v1, 0xbc

    const-string v2, "%BC"

    aput-object v2, v0, v1

    const/16 v1, 0xbd

    const-string v2, "%BD"

    aput-object v2, v0, v1

    const/16 v1, 0xbe

    const-string v2, "%BE"

    aput-object v2, v0, v1

    const/16 v1, 0xbf

    const-string v2, "%BF"

    aput-object v2, v0, v1

    const/16 v1, 0xc0

    const-string v2, "%C0"

    aput-object v2, v0, v1

    const/16 v1, 0xc1

    const-string v2, "%C1"

    aput-object v2, v0, v1

    const/16 v1, 0xc2

    const-string v2, "%C2"

    aput-object v2, v0, v1

    const/16 v1, 0xc3

    const-string v2, "%C3"

    aput-object v2, v0, v1

    const/16 v1, 0xc4

    const-string v2, "%C4"

    aput-object v2, v0, v1

    const/16 v1, 0xc5

    const-string v2, "%C5"

    aput-object v2, v0, v1

    const/16 v1, 0xc6

    const-string v2, "%C6"

    aput-object v2, v0, v1

    const/16 v1, 0xc7

    const-string v2, "%C7"

    aput-object v2, v0, v1

    const/16 v1, 0xc8

    const-string v2, "%C8"

    aput-object v2, v0, v1

    const/16 v1, 0xc9

    const-string v2, "%C9"

    aput-object v2, v0, v1

    const/16 v1, 0xca

    const-string v2, "%CA"

    aput-object v2, v0, v1

    const/16 v1, 0xcb

    const-string v2, "%CB"

    aput-object v2, v0, v1

    const/16 v1, 0xcc

    const-string v2, "%CC"

    aput-object v2, v0, v1

    const/16 v1, 0xcd

    const-string v2, "%CD"

    aput-object v2, v0, v1

    const/16 v1, 0xce

    const-string v2, "%CE"

    aput-object v2, v0, v1

    const/16 v1, 0xcf

    const-string v2, "%CF"

    aput-object v2, v0, v1

    const/16 v1, 0xd0

    const-string v2, "%D0"

    aput-object v2, v0, v1

    const/16 v1, 0xd1

    const-string v2, "%D1"

    aput-object v2, v0, v1

    const/16 v1, 0xd2

    const-string v2, "%D2"

    aput-object v2, v0, v1

    const/16 v1, 0xd3

    const-string v2, "%D3"

    aput-object v2, v0, v1

    const/16 v1, 0xd4

    const-string v2, "%D4"

    aput-object v2, v0, v1

    const/16 v1, 0xd5

    const-string v2, "%D5"

    aput-object v2, v0, v1

    const/16 v1, 0xd6

    const-string v2, "%D6"

    aput-object v2, v0, v1

    const/16 v1, 0xd7

    const-string v2, "%D7"

    aput-object v2, v0, v1

    const/16 v1, 0xd8

    const-string v2, "%D8"

    aput-object v2, v0, v1

    const/16 v1, 0xd9

    const-string v2, "%D9"

    aput-object v2, v0, v1

    const/16 v1, 0xda

    const-string v2, "%DA"

    aput-object v2, v0, v1

    const/16 v1, 0xdb

    const-string v2, "%DB"

    aput-object v2, v0, v1

    const/16 v1, 0xdc

    const-string v2, "%DC"

    aput-object v2, v0, v1

    const/16 v1, 0xdd

    const-string v2, "%DD"

    aput-object v2, v0, v1

    const/16 v1, 0xde

    const-string v2, "%DE"

    aput-object v2, v0, v1

    const/16 v1, 0xdf

    const-string v2, "%DF"

    aput-object v2, v0, v1

    const/16 v1, 0xe0

    const-string v2, "%E0"

    aput-object v2, v0, v1

    const/16 v1, 0xe1

    const-string v2, "%E1"

    aput-object v2, v0, v1

    const/16 v1, 0xe2

    const-string v2, "%E2"

    aput-object v2, v0, v1

    const/16 v1, 0xe3

    const-string v2, "%E3"

    aput-object v2, v0, v1

    const/16 v1, 0xe4

    const-string v2, "%E4"

    aput-object v2, v0, v1

    const/16 v1, 0xe5

    const-string v2, "%E5"

    aput-object v2, v0, v1

    const/16 v1, 0xe6

    const-string v2, "%E6"

    aput-object v2, v0, v1

    const/16 v1, 0xe7

    const-string v2, "%E7"

    aput-object v2, v0, v1

    const/16 v1, 0xe8

    const-string v2, "%E8"

    aput-object v2, v0, v1

    const/16 v1, 0xe9

    const-string v2, "%E9"

    aput-object v2, v0, v1

    const/16 v1, 0xea

    const-string v2, "%EA"

    aput-object v2, v0, v1

    const/16 v1, 0xeb

    const-string v2, "%EB"

    aput-object v2, v0, v1

    const/16 v1, 0xec

    const-string v2, "%EC"

    aput-object v2, v0, v1

    const/16 v1, 0xed

    const-string v2, "%ED"

    aput-object v2, v0, v1

    const/16 v1, 0xee

    const-string v2, "%EE"

    aput-object v2, v0, v1

    const/16 v1, 0xef

    const-string v2, "%EF"

    aput-object v2, v0, v1

    const/16 v1, 0xf0

    const-string v2, "%F0"

    aput-object v2, v0, v1

    const/16 v1, 0xf1

    const-string v2, "%F1"

    aput-object v2, v0, v1

    const/16 v1, 0xf2

    const-string v2, "%F2"

    aput-object v2, v0, v1

    const/16 v1, 0xf3

    const-string v2, "%F3"

    aput-object v2, v0, v1

    const/16 v1, 0xf4

    const-string v2, "%F4"

    aput-object v2, v0, v1

    const/16 v1, 0xf5

    const-string v2, "%F5"

    aput-object v2, v0, v1

    const/16 v1, 0xf6

    const-string v2, "%F6"

    aput-object v2, v0, v1

    const/16 v1, 0xf7

    const-string v2, "%F7"

    aput-object v2, v0, v1

    const/16 v1, 0xf8

    const-string v2, "%F8"

    aput-object v2, v0, v1

    const/16 v1, 0xf9

    const-string v2, "%F9"

    aput-object v2, v0, v1

    const/16 v1, 0xfa

    const-string v2, "%FA"

    aput-object v2, v0, v1

    const/16 v1, 0xfb

    const-string v2, "%FB"

    aput-object v2, v0, v1

    const/16 v1, 0xfc

    const-string v2, "%FC"

    aput-object v2, v0, v1

    const/16 v1, 0xfd

    const-string v2, "%FD"

    aput-object v2, v0, v1

    const/16 v1, 0xfe

    const-string v2, "%FE"

    aput-object v2, v0, v1

    const/16 v1, 0xff

    const-string v2, "%FF"

    aput-object v2, v0, v1

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->encodedChars:[Ljava/lang/String;

    .line 117
    new-array v0, v5, [Z

    fill-array-data v0, :array_0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->utf8Mask:[Z

    .line 136
    new-array v0, v5, [Z

    fill-array-data v0, :array_1

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->contextDataMask:[Z

    .line 157
    sget-object v0, Lcom/adobe/mobile/Config$ApplicationType;->APPLICATION_TYPE_HANDHELD:Lcom/adobe/mobile/Config$ApplicationType;

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_appType:Lcom/adobe/mobile/Config$ApplicationType;

    .line 158
    sput-boolean v4, Lcom/adobe/mobile/StaticMethods;->_isWearable:Z

    .line 180
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->prefs:Landroid/content/SharedPreferences;

    .line 181
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_sharedPreferencesMutex:Ljava/lang/Object;

    .line 199
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appID:Ljava/lang/String;

    .line 200
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_applicationIDMutex:Ljava/lang/Object;

    .line 221
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;

    .line 222
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_applicationNameMutex:Ljava/lang/Object;

    .line 256
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;

    .line 257
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_applicationVersionMutex:Ljava/lang/Object;

    .line 290
    const/4 v0, -0x1

    sput v0, Lcom/adobe/mobile/StaticMethods;->versionCode:I

    .line 291
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_applicationVersionCodeMutex:Ljava/lang/Object;

    .line 324
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->visitorID:Ljava/lang/String;

    .line 325
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_visitorIDMutex:Ljava/lang/Object;

    .line 340
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_userIdentifierMutex:Ljava/lang/Object;

    .line 355
    sput-boolean v4, Lcom/adobe/mobile/StaticMethods;->pushEnabled:Z

    .line 356
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_pushEnabledMutex:Ljava/lang/Object;

    .line 388
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->pushIdentifier:Ljava/lang/String;

    .line 389
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_pushIdentifierMutex:Ljava/lang/Object;

    .line 418
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->advertisingIdentifier:Ljava/lang/String;

    .line 419
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_advertisingIdentifierMutex:Ljava/lang/Object;

    .line 440
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->userAgent:Ljava/lang/String;

    .line 441
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_userAgentMutex:Ljava/lang/Object;

    .line 453
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    .line 454
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_defaultDataMutex:Ljava/lang/Object;

    .line 472
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->resolution:Ljava/lang/String;

    .line 473
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_resolutionMutex:Ljava/lang/Object;

    .line 489
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->carrier:Ljava/lang/String;

    .line 490
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_carrierMutex:Ljava/lang/Object;

    .line 506
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->operatingSystem:Ljava/lang/String;

    .line 507
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_operatingSystemMutex:Ljava/lang/Object;

    .line 531
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->timestamp:Ljava/lang/String;

    .line 532
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_timestampMutex:Ljava/lang/Object;

    .line 557
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->mediaExecutor:Ljava/util/concurrent/ExecutorService;

    .line 558
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_mediaExecutorMutex:Ljava/lang/Object;

    .line 569
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->timedActionsExecutor:Ljava/util/concurrent/ExecutorService;

    .line 570
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_timedActionsExecutorMutex:Ljava/lang/Object;

    .line 581
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->sharedExecutor:Ljava/util/concurrent/ExecutorService;

    .line 582
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_sharedExecutorMutex:Ljava/lang/Object;

    .line 593
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->analyticsExecutor:Ljava/util/concurrent/ExecutorService;

    .line 594
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_analyticsExecutorMutex:Ljava/lang/Object;

    .line 605
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->messagesExecutor:Ljava/util/concurrent/ExecutorService;

    .line 606
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_messagesExecutorMutex:Ljava/lang/Object;

    .line 617
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->thirdPartyCallbacksExecutor:Ljava/util/concurrent/ExecutorService;

    .line 618
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_thirdPartyCallbacksExecutorMutex:Ljava/lang/Object;

    .line 628
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->messageImageCachingExecutor:Ljava/util/concurrent/ExecutorService;

    .line 629
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_messageImageCachingExecutorMutex:Ljava/lang/Object;

    .line 640
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->audienceExecutor:Ljava/util/concurrent/ExecutorService;

    .line 641
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_audienceExecutorMutex:Ljava/lang/Object;

    .line 652
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->piiExecutor:Ljava/util/concurrent/ExecutorService;

    .line 653
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_piiExecutorMutex:Ljava/lang/Object;

    .line 664
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    .line 665
    sput-boolean v4, Lcom/adobe/mobile/StaticMethods;->_aidDone:Z

    .line 666
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_aidMutex:Ljava/lang/Object;

    .line 1032
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0, v5}, Ljava/util/HashMap;-><init>(I)V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteList:Ljava/util/Map;

    .line 1033
    sput v4, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteListCount:I

    .line 1204
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->sharedContext:Landroid/content/Context;

    .line 1251
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->_activity:Ljava/lang/ref/WeakReference;

    .line 1252
    new-instance v0, Ljava/lang/Object;

    invoke-direct {v0}, Ljava/lang/Object;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_currentActivityMutex:Ljava/lang/Object;

    .line 1292
    const-string v0, "000102030405060708090A0B0C0D0E0F101112131415161718191A1B1C1D1E1F202122232425262728292A2B2C2D2E2F303132333435363738393A3B3C3D3E3F404142434445464748494A4B4C4D4E4F505152535455565758595A5B5C5D5E5F606162636465666768696A6B6C6D6E6F707172737475767778797A7B7C7D7E7F808182838485868788898A8B8C8D8E8F909192939495969798999A9B9C9D9E9FA0A1A2A3A4A5A6A7A8A9AAABACADAEAFB0B1B2B3B4B5B6B7B8B9BABBBCBDBEBFC0C1C2C3C4C5C6C7C8C9CACBCCCDCECFD0D1D2D3D4D5D6D7D8D9DADBDCDDDEDFE0E1E2E3E4E5E6E7E8E9EAEBECEDEEEFF0F1F2F3F4F5F6F7F8F9FAFBFCFDFEFF"

    .line 1308
    invoke-virtual {v0}, Ljava/lang/String;->toCharArray()[C

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->BYTE_TO_HEX:[C

    .line 1292
    return-void

    .line 117
    nop

    :array_0
    .array-data 1
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x0t
        0x0t
        0x1t
        0x1t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
    .end array-data

    .line 136
    :array_1
    .array-data 1
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x1t
        0x0t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x1t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
        0x0t
    .end array-data
.end method

.method constructor <init>()V
    .locals 0

    .prologue
    .line 49
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 173
    return-void
.end method

.method protected static URLEncode(Ljava/lang/String;)Ljava/lang/String;
    .locals 10
    .param p0, "unencodedString"    # Ljava/lang/String;

    .prologue
    const/4 v5, 0x0

    const/4 v9, 0x0

    .line 905
    if-nez p0, :cond_1

    move-object p0, v5

    .line 943
    .end local p0    # "unencodedString":Ljava/lang/String;
    :cond_0
    :goto_0
    return-object p0

    .line 910
    .restart local p0    # "unencodedString":Ljava/lang/String;
    :cond_1
    :try_start_0
    const-string v6, "UTF-8"

    invoke-virtual {p0, v6}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v4

    .line 912
    .local v4, "stringBytes":[B
    array-length v3, v4

    .line 913
    .local v3, "len":I
    const/4 v0, 0x0

    .line 916
    .local v0, "curIndex":I
    :goto_1
    if-ge v0, v3, :cond_2

    sget-object v6, Lcom/adobe/mobile/StaticMethods;->utf8Mask:[Z

    aget-byte v7, v4, v0

    and-int/lit16 v7, v7, 0xff

    aget-boolean v6, v6, v7

    if-eqz v6, :cond_2

    .line 917
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 922
    :cond_2
    if-eq v0, v3, :cond_0

    .line 927
    new-instance v2, Ljava/lang/StringBuilder;

    array-length v6, v4

    shl-int/lit8 v6, v6, 0x1

    invoke-direct {v2, v6}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 930
    .local v2, "encodedString":Ljava/lang/StringBuilder;
    if-lez v0, :cond_3

    .line 931
    new-instance v6, Ljava/lang/String;

    const/4 v7, 0x0

    const-string v8, "UTF-8"

    invoke-direct {v6, v4, v7, v0, v8}, Ljava/lang/String;-><init>([BIILjava/lang/String;)V

    invoke-virtual {v2, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 935
    :cond_3
    :goto_2
    if-ge v0, v3, :cond_4

    .line 936
    sget-object v6, Lcom/adobe/mobile/StaticMethods;->encodedChars:[Ljava/lang/String;

    aget-byte v7, v4, v0

    and-int/lit16 v7, v7, 0xff

    aget-object v6, v6, v7

    invoke-virtual {v2, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 935
    add-int/lit8 v0, v0, 0x1

    goto :goto_2

    .line 940
    :cond_4
    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object p0

    goto :goto_0

    .line 941
    .end local v0    # "curIndex":I
    .end local v2    # "encodedString":Ljava/lang/StringBuilder;
    .end local v3    # "len":I
    .end local v4    # "stringBytes":[B
    :catch_0
    move-exception v1

    .line 942
    .local v1, "e":Ljava/io/UnsupportedEncodingException;
    new-instance v6, Ljava/lang/StringBuilder;

    invoke-direct {v6}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "UnsupportedEncodingException : "

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v1}, Ljava/io/UnsupportedEncodingException;->getMessage()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v6, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v6

    invoke-virtual {v6}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    new-array v7, v9, [Ljava/lang/Object;

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    move-object p0, v5

    .line 943
    goto :goto_0
.end method

.method private static addValueToHashMap(Ljava/lang/Object;Lcom/adobe/mobile/ContextData;Ljava/util/List;I)V
    .locals 4
    .param p0, "object"    # Ljava/lang/Object;
    .param p1, "table"    # Lcom/adobe/mobile/ContextData;
    .param p3, "index"    # I
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Object;",
            "Lcom/adobe/mobile/ContextData;",
            "Ljava/util/List",
            "<",
            "Ljava/lang/String;",
            ">;I)V"
        }
    .end annotation

    .prologue
    .line 1113
    .local p2, "subKeyArray":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    if-eqz p1, :cond_0

    if-nez p2, :cond_1

    .line 1137
    :cond_0
    :goto_0
    return-void

    .line 1117
    :cond_1
    invoke-interface {p2}, Ljava/util/List;->size()I

    move-result v0

    .line 1118
    .local v0, "arrayCount":I
    if-ge p3, v0, :cond_3

    invoke-interface {p2, p3}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    move-object v2, v3

    .line 1119
    .local v2, "keyName":Ljava/lang/String;
    :goto_1
    if-eqz v2, :cond_0

    .line 1123
    new-instance v1, Lcom/adobe/mobile/ContextData;

    invoke-direct {v1}, Lcom/adobe/mobile/ContextData;-><init>()V

    .line 1124
    .local v1, "data":Lcom/adobe/mobile/ContextData;
    invoke-virtual {p1, v2}, Lcom/adobe/mobile/ContextData;->containsKey(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_2

    .line 1125
    invoke-virtual {p1, v2}, Lcom/adobe/mobile/ContextData;->get(Ljava/lang/String;)Lcom/adobe/mobile/ContextData;

    move-result-object v1

    .line 1128
    :cond_2
    add-int/lit8 v3, v0, -0x1

    if-ne v3, p3, :cond_4

    .line 1130
    iput-object p0, v1, Lcom/adobe/mobile/ContextData;->value:Ljava/lang/Object;

    .line 1131
    invoke-virtual {p1, v2, v1}, Lcom/adobe/mobile/ContextData;->put(Ljava/lang/String;Lcom/adobe/mobile/ContextData;)V

    goto :goto_0

    .line 1118
    .end local v1    # "data":Lcom/adobe/mobile/ContextData;
    .end local v2    # "keyName":Ljava/lang/String;
    :cond_3
    const/4 v2, 0x0

    goto :goto_1

    .line 1134
    .restart local v1    # "data":Lcom/adobe/mobile/ContextData;
    .restart local v2    # "keyName":Ljava/lang/String;
    :cond_4
    invoke-virtual {p1, v2, v1}, Lcom/adobe/mobile/ContextData;->put(Ljava/lang/String;Lcom/adobe/mobile/ContextData;)V

    .line 1135
    add-int/lit8 p3, p3, 0x1

    invoke-static {p0, v1, p2, p3}, Lcom/adobe/mobile/StaticMethods;->addValueToHashMap(Ljava/lang/Object;Lcom/adobe/mobile/ContextData;Ljava/util/List;I)V

    goto :goto_0
.end method

.method protected static appendContextData(Ljava/util/Map;Ljava/lang/String;)Ljava/lang/String;
    .locals 17
    .param p1, "source"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 1360
    .local p0, "referrerData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-eqz p1, :cond_0

    if-eqz p0, :cond_0

    invoke-interface/range {p0 .. p0}, Ljava/util/Map;->size()I

    move-result v12

    if-nez v12, :cond_1

    .line 1404
    .end local p1    # "source":Ljava/lang/String;
    :cond_0
    :goto_0
    return-object p1

    .line 1363
    .restart local p1    # "source":Ljava/lang/String;
    :cond_1
    const-string v12, ".*(&c\\.(.*)&\\.c).*"

    invoke-static {v12}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v10

    .line 1364
    .local v10, "pattern":Ljava/util/regex/Pattern;
    move-object/from16 v0, p1

    invoke-virtual {v10, v0}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v8

    .line 1365
    .local v8, "matcher":Ljava/util/regex/Matcher;
    invoke-virtual {v8}, Ljava/util/regex/Matcher;->matches()Z

    move-result v12

    if-eqz v12, :cond_0

    .line 1368
    const/4 v12, 0x2

    invoke-virtual {v8, v12}, Ljava/util/regex/Matcher;->group(I)Ljava/lang/String;

    move-result-object v3

    .line 1369
    .local v3, "contextDataString":Ljava/lang/String;
    if-eqz v3, :cond_0

    .line 1373
    new-instance v1, Ljava/util/HashMap;

    const/16 v12, 0x40

    invoke-direct {v1, v12}, Ljava/util/HashMap;-><init>(I)V

    .line 1374
    .local v1, "contextData":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v6, Ljava/util/ArrayList;

    const/16 v12, 0x10

    invoke-direct {v6, v12}, Ljava/util/ArrayList;-><init>(I)V

    .line 1375
    .local v6, "keyPath":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    const-string v12, "&"

    invoke-virtual {v3, v12}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v13

    array-length v14, v13

    const/4 v12, 0x0

    :goto_1
    if-ge v12, v14, :cond_5

    aget-object v9, v13, v12

    .line 1376
    .local v9, "param":Ljava/lang/String;
    const-string v15, "."

    invoke-virtual {v9, v15}, Ljava/lang/String;->endsWith(Ljava/lang/String;)Z

    move-result v15

    if-eqz v15, :cond_3

    const-string v15, "="

    invoke-virtual {v9, v15}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v15

    if-nez v15, :cond_3

    .line 1377
    invoke-interface {v6, v9}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1375
    :cond_2
    :goto_2
    add-int/lit8 v12, v12, 0x1

    goto :goto_1

    .line 1379
    :cond_3
    const-string v15, "."

    invoke-virtual {v9, v15}, Ljava/lang/String;->startsWith(Ljava/lang/String;)Z

    move-result v15

    if-eqz v15, :cond_4

    .line 1380
    invoke-interface {v6}, Ljava/util/List;->size()I

    move-result v15

    if-lez v15, :cond_2

    .line 1381
    invoke-interface {v6}, Ljava/util/List;->size()I

    move-result v15

    add-int/lit8 v15, v15, -0x1

    invoke-interface {v6, v15}, Ljava/util/List;->remove(I)Ljava/lang/Object;

    goto :goto_2

    .line 1384
    :cond_4
    const-string v15, "="

    invoke-virtual {v9, v15}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v7

    .line 1385
    .local v7, "kvpair":[Ljava/lang/String;
    array-length v15, v7

    const/16 v16, 0x2

    move/from16 v0, v16

    if-ne v15, v0, :cond_2

    .line 1388
    const/4 v15, 0x0

    aget-object v15, v7, v15

    invoke-static {v6, v15}, Lcom/adobe/mobile/StaticMethods;->contextDataStringPath(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 1390
    .local v2, "contextDataKey":Ljava/lang/String;
    const/4 v15, 0x1

    :try_start_0
    aget-object v15, v7, v15

    const-string v16, "UTF-8"

    invoke-static/range {v15 .. v16}, Ljava/net/URLDecoder;->decode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v15

    invoke-interface {v1, v2, v15}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_2

    .line 1391
    :catch_0
    move-exception v5

    .line 1392
    .local v5, "e":Ljava/io/UnsupportedEncodingException;
    invoke-virtual {v5}, Ljava/io/UnsupportedEncodingException;->printStackTrace()V

    goto :goto_2

    .line 1397
    .end local v2    # "contextDataKey":Ljava/lang/String;
    .end local v5    # "e":Ljava/io/UnsupportedEncodingException;
    .end local v7    # "kvpair":[Ljava/lang/String;
    .end local v9    # "param":Ljava/lang/String;
    :cond_5
    move-object/from16 v0, p0

    invoke-interface {v1, v0}, Ljava/util/Map;->putAll(Ljava/util/Map;)V

    .line 1399
    new-instance v11, Ljava/lang/StringBuilder;

    const/4 v12, 0x0

    const/4 v13, 0x1

    invoke-virtual {v8, v13}, Ljava/util/regex/Matcher;->start(I)I

    move-result v13

    move-object/from16 v0, p1

    invoke-virtual {v0, v12, v13}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v12

    invoke-direct {v11, v12}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    .line 1400
    .local v11, "urlSb":Ljava/lang/StringBuilder;
    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4}, Ljava/util/HashMap;-><init>()V

    .line 1401
    .local v4, "contextMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    const-string v12, "c"

    invoke-static {v1}, Lcom/adobe/mobile/StaticMethods;->translateContextData(Ljava/util/Map;)Lcom/adobe/mobile/ContextData;

    move-result-object v13

    invoke-virtual {v4, v12, v13}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1402
    invoke-static {v4, v11}, Lcom/adobe/mobile/StaticMethods;->serializeToQueryString(Ljava/util/Map;Ljava/lang/StringBuilder;)V

    .line 1403
    const/4 v12, 0x1

    invoke-virtual {v8, v12}, Ljava/util/regex/Matcher;->end(I)I

    move-result v12

    move-object/from16 v0, p1

    invoke-virtual {v0, v12}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v11, v12}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1404
    invoke-virtual {v11}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    goto/16 :goto_0
.end method

.method protected static cleanContextDataDictionary(Ljava/util/Map;)Ljava/util/Map;
    .locals 5
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;"
        }
    .end annotation

    .prologue
    .line 1011
    .local p0, "dict":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-nez p0, :cond_1

    .line 1012
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 1024
    :cond_0
    return-object v2

    .line 1015
    :cond_1
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 1017
    .local v2, "tempContextData":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v4

    :cond_2
    :goto_0
    invoke-interface {v4}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v4}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 1018
    .local v1, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    invoke-static {v3}, Lcom/adobe/mobile/StaticMethods;->cleanContextDataKey(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    .line 1019
    .local v0, "cleanedKey":Ljava/lang/String;
    if-eqz v0, :cond_2

    .line 1020
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    invoke-virtual {v2, v0, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    goto :goto_0
.end method

.method protected static cleanContextDataKey(Ljava/lang/String;)Ljava/lang/String;
    .locals 17
    .param p0, "key"    # Ljava/lang/String;

    .prologue
    .line 1035
    if-nez p0, :cond_0

    .line 1036
    const/4 v9, 0x0

    .line 1109
    :goto_0
    return-object v9

    .line 1040
    :cond_0
    sget-object v14, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteList:Ljava/util/Map;

    monitor-enter v14

    .line 1041
    :try_start_0
    sget-object v13, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteList:Ljava/util/Map;

    move-object/from16 v0, p0

    invoke-interface {v13, v0}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v9

    check-cast v9, Ljava/lang/String;

    .line 1044
    .local v9, "preCleanedKey":Ljava/lang/String;
    if-eqz v9, :cond_1

    .line 1045
    monitor-exit v14

    goto :goto_0

    .line 1047
    .end local v9    # "preCleanedKey":Ljava/lang/String;
    :catchall_0
    move-exception v13

    monitor-exit v14
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v13

    .restart local v9    # "preCleanedKey":Ljava/lang/String;
    :cond_1
    :try_start_1
    monitor-exit v14
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 1054
    :try_start_2
    const-string v13, "UTF-8"

    move-object/from16 v0, p0

    invoke-virtual {v0, v13}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v12

    .line 1055
    .local v12, "utf8Key":[B
    array-length v13, v12

    new-array v8, v13, [B

    .line 1057
    .local v8, "outPut":[B
    const/4 v5, 0x0

    .line 1058
    .local v5, "lastByte":B
    const/4 v6, 0x0

    .line 1061
    .local v6, "outIndex":I
    array-length v14, v12

    const/4 v13, 0x0

    move v7, v6

    .end local v6    # "outIndex":I
    .local v7, "outIndex":I
    :goto_1
    if-ge v13, v14, :cond_3

    aget-byte v2, v12, v13

    .line 1063
    .local v2, "curByte":B
    const/16 v15, 0x2e

    if-ne v2, v15, :cond_2

    const/16 v15, 0x2e

    if-ne v5, v15, :cond_2

    move v6, v7

    .line 1061
    .end local v7    # "outIndex":I
    .restart local v6    # "outIndex":I
    :goto_2
    add-int/lit8 v13, v13, 0x1

    move v7, v6

    .end local v6    # "outIndex":I
    .restart local v7    # "outIndex":I
    goto :goto_1

    .line 1066
    :cond_2
    sget-object v15, Lcom/adobe/mobile/StaticMethods;->contextDataMask:[Z

    and-int/lit16 v0, v2, 0xff

    move/from16 v16, v0

    aget-boolean v15, v15, v16

    if-eqz v15, :cond_9

    .line 1068
    add-int/lit8 v6, v7, 0x1

    .end local v7    # "outIndex":I
    .restart local v6    # "outIndex":I
    aput-byte v2, v8, v7

    .line 1069
    move v5, v2

    goto :goto_2

    .line 1074
    .end local v2    # "curByte":B
    .end local v6    # "outIndex":I
    .restart local v7    # "outIndex":I
    :cond_3
    if-nez v7, :cond_4

    .line 1075
    const/4 v9, 0x0

    goto :goto_0

    .line 1079
    :cond_4
    const/4 v13, 0x0

    aget-byte v13, v8, v13

    const/16 v14, 0x2e

    if-ne v13, v14, :cond_5

    const/4 v10, 0x1

    .line 1080
    .local v10, "startIndex":I
    :goto_3
    add-int/lit8 v13, v7, -0x1

    aget-byte v13, v8, v13

    const/16 v14, 0x2e

    if-ne v13, v14, :cond_6

    const/4 v3, 0x1

    .line 1081
    .local v3, "endTrim":I
    :goto_4
    sub-int v13, v7, v3

    sub-int v11, v13, v10

    .line 1084
    .local v11, "totalLength":I
    if-gtz v11, :cond_7

    .line 1085
    const/4 v9, 0x0

    goto :goto_0

    .line 1079
    .end local v3    # "endTrim":I
    .end local v10    # "startIndex":I
    .end local v11    # "totalLength":I
    :cond_5
    const/4 v10, 0x0

    goto :goto_3

    .line 1080
    .restart local v10    # "startIndex":I
    :cond_6
    const/4 v3, 0x0

    goto :goto_4

    .line 1089
    .restart local v3    # "endTrim":I
    .restart local v11    # "totalLength":I
    :cond_7
    new-instance v1, Ljava/lang/String;

    const-string v13, "UTF-8"

    invoke-direct {v1, v8, v10, v11, v13}, Ljava/lang/String;-><init>([BIILjava/lang/String;)V
    :try_end_2
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_2 .. :try_end_2} :catch_0

    .line 1096
    .local v1, "cleanKey":Ljava/lang/String;
    sget-object v14, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteList:Ljava/util/Map;

    monitor-enter v14

    .line 1098
    :try_start_3
    sget v13, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteListCount:I

    const/16 v15, 0xfa

    if-le v13, v15, :cond_8

    .line 1100
    sget-object v13, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteList:Ljava/util/Map;

    invoke-interface {v13}, Ljava/util/Map;->clear()V

    .line 1101
    const/4 v13, 0x0

    sput v13, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteListCount:I

    .line 1105
    :cond_8
    sget-object v13, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteList:Ljava/util/Map;

    move-object/from16 v0, p0

    invoke-interface {v13, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1106
    sget v13, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteListCount:I

    add-int/lit8 v13, v13, 0x1

    sput v13, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteListCount:I

    .line 1107
    monitor-exit v14
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    move-object v9, v1

    .line 1109
    goto/16 :goto_0

    .line 1090
    .end local v1    # "cleanKey":Ljava/lang/String;
    .end local v3    # "endTrim":I
    .end local v5    # "lastByte":B
    .end local v7    # "outIndex":I
    .end local v8    # "outPut":[B
    .end local v10    # "startIndex":I
    .end local v11    # "totalLength":I
    .end local v12    # "utf8Key":[B
    :catch_0
    move-exception v4

    .line 1091
    .local v4, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v13, "Analytics - Unable to clean context data key (%s)"

    const/4 v14, 0x1

    new-array v14, v14, [Ljava/lang/Object;

    const/4 v15, 0x0

    invoke-virtual {v4}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v16

    aput-object v16, v14, v15

    invoke-static {v13, v14}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1092
    const/4 v9, 0x0

    goto/16 :goto_0

    .line 1107
    .end local v4    # "ex":Ljava/io/UnsupportedEncodingException;
    .restart local v1    # "cleanKey":Ljava/lang/String;
    .restart local v3    # "endTrim":I
    .restart local v5    # "lastByte":B
    .restart local v7    # "outIndex":I
    .restart local v8    # "outPut":[B
    .restart local v10    # "startIndex":I
    .restart local v11    # "totalLength":I
    .restart local v12    # "utf8Key":[B
    :catchall_1
    move-exception v13

    :try_start_4
    monitor-exit v14
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    throw v13

    .end local v1    # "cleanKey":Ljava/lang/String;
    .end local v3    # "endTrim":I
    .end local v10    # "startIndex":I
    .end local v11    # "totalLength":I
    .restart local v2    # "curByte":B
    :cond_9
    move v6, v7

    .end local v7    # "outIndex":I
    .restart local v6    # "outIndex":I
    goto :goto_2
.end method

.method protected static contextDataStringPath(Ljava/util/List;Ljava/lang/String;)Ljava/lang/String;
    .locals 4
    .param p1, "lastComponent"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Ljava/lang/String;",
            ">;",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 1409
    .local p0, "keyPath":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    .line 1410
    .local v1, "sb":Ljava/lang/StringBuilder;
    invoke-interface {p0}, Ljava/util/List;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 1411
    .local v0, "pathComponent":Ljava/lang/String;
    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 1413
    .end local v0    # "pathComponent":Ljava/lang/String;
    :cond_0
    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 1414
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    return-object v2
.end method

.method protected static expandTokens(Ljava/lang/String;Ljava/util/Map;)Ljava/lang/String;
    .locals 7
    .param p0, "inputString"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;)",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 1269
    .local p1, "tokens":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    move-object v2, p0

    .line 1272
    .local v2, "returnString":Ljava/lang/String;
    :try_start_0
    invoke-interface {p1}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v3

    invoke-interface {v3}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v5

    :goto_0
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_0

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/util/Map$Entry;

    .line 1273
    .local v0, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    invoke-interface {v0}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/CharSequence;

    invoke-interface {v0}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/CharSequence;

    invoke-virtual {v2, v3, v4}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v2

    .line 1274
    goto :goto_0

    .line 1276
    .end local v0    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/String;>;"
    :catch_0
    move-exception v1

    .line 1277
    .local v1, "ex":Ljava/lang/Exception;
    const-string v3, "Unable to expand tokens (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Ljava/lang/Exception;->toString()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1280
    .end local v1    # "ex":Ljava/lang/Exception;
    :cond_0
    return-object v2
.end method

.method private static generateAID()Ljava/lang/String;
    .locals 12

    .prologue
    const/16 v11, 0x10

    .line 782
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v8

    invoke-virtual {v8}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v8

    const-string v9, "-"

    const-string v10, ""

    invoke-virtual {v8, v9, v10}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v5

    .line 783
    .local v5, "uuid":Ljava/lang/String;
    sget-object v8, Ljava/util/Locale;->US:Ljava/util/Locale;

    invoke-virtual {v5, v8}, Ljava/lang/String;->toUpperCase(Ljava/util/Locale;)Ljava/lang/String;

    move-result-object v5

    .line 785
    const-string v8, "^[89A-F]"

    invoke-static {v8}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v1

    .line 786
    .local v1, "firstPattern":Ljava/util/regex/Pattern;
    const-string v8, "^[4-9A-F]"

    invoke-static {v8}, Ljava/util/regex/Pattern;->compile(Ljava/lang/String;)Ljava/util/regex/Pattern;

    move-result-object v4

    .line 787
    .local v4, "secondPattern":Ljava/util/regex/Pattern;
    const/4 v8, 0x0

    invoke-virtual {v5, v8, v11}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v1, v8}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v0

    .line 788
    .local v0, "firstMatcher":Ljava/util/regex/Matcher;
    const/16 v8, 0x20

    invoke-virtual {v5, v11, v8}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v4, v8}, Ljava/util/regex/Pattern;->matcher(Ljava/lang/CharSequence;)Ljava/util/regex/Matcher;

    move-result-object v3

    .line 790
    .local v3, "secondMatcher":Ljava/util/regex/Matcher;
    new-instance v2, Ljava/security/SecureRandom;

    invoke-direct {v2}, Ljava/security/SecureRandom;-><init>()V

    .line 791
    .local v2, "r":Ljava/security/SecureRandom;
    const/4 v8, 0x7

    invoke-virtual {v2, v8}, Ljava/security/SecureRandom;->nextInt(I)I

    move-result v8

    invoke-static {v8}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v0, v8}, Ljava/util/regex/Matcher;->replaceAll(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 792
    .local v6, "vi_hi":Ljava/lang/String;
    const/4 v8, 0x3

    invoke-virtual {v2, v8}, Ljava/security/SecureRandom;->nextInt(I)I

    move-result v8

    invoke-static {v8}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v3, v8}, Ljava/util/regex/Matcher;->replaceAll(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    .line 794
    .local v7, "vi_lo":Ljava/lang/String;
    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v8, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    const-string v9, "-"

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-virtual {v8, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v8

    return-object v8
.end method

.method protected static getAID()Ljava/lang/String;
    .locals 8

    .prologue
    .line 668
    sget-object v4, Lcom/adobe/mobile/StaticMethods;->_aidMutex:Ljava/lang/Object;

    monitor-enter v4

    .line 669
    :try_start_0
    sget-boolean v3, Lcom/adobe/mobile/StaticMethods;->_aidDone:Z

    if-nez v3, :cond_2

    .line 670
    const/4 v3, 0x1

    sput-boolean v3, Lcom/adobe/mobile/StaticMethods;->_aidDone:Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 672
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v3

    const-string v5, "ADOBEMOBILE_STOREDDEFAULTS_IGNORE_AID"

    const/4 v6, 0x0

    invoke-interface {v3, v5, v6}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v2

    .line 673
    .local v2, "ignoreAid":Z
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v3

    const-string v5, "ADOBEMOBILE_STOREDDEFAULTS_AID"

    const/4 v6, 0x0

    invoke-interface {v3, v5, v6}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    .line 678
    if-nez v2, :cond_0

    sget-object v3, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    if-eqz v3, :cond_1

    :cond_0
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v3

    invoke-virtual {v3}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v3

    if-nez v3, :cond_2

    if-eqz v2, :cond_2

    .line 679
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->retrieveAIDFromVisitorIDService()Ljava/lang/String;

    move-result-object v3

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    .line 680
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 682
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    if-eqz v3, :cond_3

    .line 684
    const-string v3, "ADOBEMOBILE_STOREDDEFAULTS_AID"

    sget-object v5, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    invoke-interface {v1, v3, v5}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 685
    const-string v3, "ADOBEMOBILE_STOREDDEFAULTS_IGNORE_AID"

    const/4 v5, 0x0

    invoke-interface {v1, v3, v5}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    .line 690
    :goto_0
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 692
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    invoke-static {v3}, Lcom/adobe/mobile/StaticMethods;->syncAIDIfNeeded(Ljava/lang/String;)V
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 698
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_2
    :goto_1
    :try_start_2
    monitor-exit v4
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    .line 700
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    return-object v3

    .line 687
    .restart local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_3
    :try_start_3
    const-string v3, "ADOBEMOBILE_STOREDDEFAULTS_IGNORE_AID"

    const/4 v5, 0x1

    invoke-interface {v1, v3, v5}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;
    :try_end_3
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_0
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    .line 694
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v0

    .line 695
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_4
    const-string v3, "Config - Error getting AID. (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 698
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v3

    monitor-exit v4
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    throw v3
.end method

.method protected static getAdvertisingIdentifier()Ljava/lang/String;
    .locals 2

    .prologue
    .line 421
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_advertisingIdentifierMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 422
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->advertisingIdentifier:Ljava/lang/String;

    monitor-exit v1

    return-object v0

    .line 423
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getAnalyticsExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 596
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_analyticsExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 597
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->analyticsExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 598
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->analyticsExecutor:Ljava/util/concurrent/ExecutorService;

    .line 601
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->analyticsExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 602
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method private static getAndroidVersion()Ljava/lang/String;
    .locals 1

    .prologue
    .line 1191
    sget-object v0, Landroid/os/Build$VERSION;->RELEASE:Ljava/lang/String;

    return-object v0
.end method

.method protected static getApplicationID()Ljava/lang/String;
    .locals 9

    .prologue
    .line 202
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->_applicationIDMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 203
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->appID:Ljava/lang/String;

    if-nez v2, :cond_0

    .line 204
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationName()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationName()Ljava/lang/String;

    move-result-object v2

    :goto_0
    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    .line 205
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationVersion()Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_2

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationVersion()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_2

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, " "

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationVersion()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v5, " "

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    :goto_1
    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    .line 206
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationVersionCode()I

    move-result v2

    if-eqz v2, :cond_3

    sget-object v2, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v5, "(%d)"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationVersionCode()I

    move-result v8

    invoke-static {v8}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v2, v5, v6}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    :goto_2
    invoke-virtual {v4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    sput-object v2, Lcom/adobe/mobile/StaticMethods;->appID:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 209
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 210
    .local v0, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v2, "ADOBEMOBILE_STOREDDEFAULTS_APPID"

    sget-object v4, Lcom/adobe/mobile/StaticMethods;->appID:Ljava/lang/String;

    invoke-interface {v0, v2, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 211
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 217
    :cond_0
    :goto_3
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->appID:Ljava/lang/String;

    monitor-exit v3

    return-object v2

    .line 204
    .end local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :cond_1
    const-string v2, ""

    goto :goto_0

    .line 205
    :cond_2
    const-string v2, ""

    goto :goto_1

    .line 206
    :cond_3
    const-string v2, ""

    goto :goto_2

    .line 213
    .restart local v0    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v1

    .line 214
    .local v1, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Config - Unable to set Application ID in preferences (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_3

    .line 218
    .end local v1    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method private static getApplicationName()Ljava/lang/String;
    .locals 8

    .prologue
    .line 224
    sget-object v4, Lcom/adobe/mobile/StaticMethods;->_applicationNameMutex:Ljava/lang/Object;

    monitor-enter v4

    .line 225
    :try_start_0
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;

    if-nez v3, :cond_0

    .line 226
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 228
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    .line 229
    .local v2, "packageManager":Landroid/content/pm/PackageManager;
    if-eqz v2, :cond_3

    .line 230
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    const/4 v5, 0x0

    invoke-virtual {v2, v3, v5}, Landroid/content/pm/PackageManager;->getApplicationInfo(Ljava/lang/String;I)Landroid/content/pm/ApplicationInfo;

    move-result-object v0

    .line 231
    .local v0, "applicationInfo":Landroid/content/pm/ApplicationInfo;
    if-eqz v0, :cond_2

    .line 232
    invoke-virtual {v2, v0}, Landroid/content/pm/PackageManager;->getApplicationLabel(Landroid/content/pm/ApplicationInfo;)Ljava/lang/CharSequence;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 233
    invoke-virtual {v2, v0}, Landroid/content/pm/PackageManager;->getApplicationLabel(Landroid/content/pm/ApplicationInfo;)Ljava/lang/CharSequence;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    :goto_0
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;
    :try_end_1
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 252
    .end local v0    # "applicationInfo":Landroid/content/pm/ApplicationInfo;
    :cond_0
    :goto_1
    :try_start_2
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;

    monitor-exit v4
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    return-object v3

    .line 233
    .restart local v0    # "applicationInfo":Landroid/content/pm/ApplicationInfo;
    :cond_1
    :try_start_3
    const-string v3, ""

    goto :goto_0

    .line 236
    :cond_2
    const-string v3, "Analytics - ApplicationInfo was null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 237
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;
    :try_end_3
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_3 .. :try_end_3} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_1
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_1

    .line 244
    .end local v0    # "applicationInfo":Landroid/content/pm/ApplicationInfo;
    :catch_0
    move-exception v1

    .line 245
    .local v1, "e":Landroid/content/pm/PackageManager$NameNotFoundException;
    :try_start_4
    const-string v3, "Analytics - PackageManager couldn\'t find application name (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v1}, Landroid/content/pm/PackageManager$NameNotFoundException;->toString()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 246
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;

    goto :goto_1

    .line 253
    .end local v1    # "e":Landroid/content/pm/PackageManager$NameNotFoundException;
    :catchall_0
    move-exception v3

    monitor-exit v4
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    throw v3

    .line 241
    :cond_3
    :try_start_5
    const-string v3, "Analytics - PackageManager was null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 242
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;
    :try_end_5
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_5 .. :try_end_5} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_5 .. :try_end_5} :catch_1
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    goto :goto_1

    .line 247
    :catch_1
    move-exception v1

    .line 248
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_6
    const-string v3, "Config - Unable to get package to pull application name. (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 249
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    goto :goto_1
.end method

.method protected static getApplicationType()Lcom/adobe/mobile/Config$ApplicationType;
    .locals 1

    .prologue
    .line 815
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->_appType:Lcom/adobe/mobile/Config$ApplicationType;

    return-object v0
.end method

.method protected static getApplicationVersion()Ljava/lang/String;
    .locals 8

    .prologue
    .line 259
    sget-object v4, Lcom/adobe/mobile/StaticMethods;->_applicationVersionMutex:Ljava/lang/Object;

    monitor-enter v4

    .line 260
    :try_start_0
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;

    if-nez v3, :cond_0

    .line 261
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 263
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    .line 264
    .local v2, "packageManager":Landroid/content/pm/PackageManager;
    if-eqz v2, :cond_3

    .line 265
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v3

    const/4 v5, 0x0

    invoke-virtual {v2, v3, v5}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v1

    .line 266
    .local v1, "packageInfo":Landroid/content/pm/PackageInfo;
    if-eqz v1, :cond_2

    .line 267
    iget-object v3, v1, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    if-eqz v3, :cond_1

    iget-object v3, v1, Landroid/content/pm/PackageInfo;->versionName:Ljava/lang/String;

    :goto_0
    sput-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;
    :try_end_1
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 286
    .end local v1    # "packageInfo":Landroid/content/pm/PackageInfo;
    :cond_0
    :goto_1
    :try_start_2
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;

    monitor-exit v4
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    return-object v3

    .line 267
    .restart local v1    # "packageInfo":Landroid/content/pm/PackageInfo;
    :cond_1
    :try_start_3
    const-string v3, ""

    goto :goto_0

    .line 270
    :cond_2
    const-string v3, "Analytics - PackageInfo was null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 271
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;
    :try_end_3
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_3 .. :try_end_3} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_1
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_1

    .line 278
    .end local v1    # "packageInfo":Landroid/content/pm/PackageInfo;
    :catch_0
    move-exception v0

    .line 279
    .local v0, "e":Landroid/content/pm/PackageManager$NameNotFoundException;
    :try_start_4
    const-string v3, "Analytics - PackageManager couldn\'t find application version (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Landroid/content/pm/PackageManager$NameNotFoundException;->toString()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 280
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;

    goto :goto_1

    .line 287
    .end local v0    # "e":Landroid/content/pm/PackageManager$NameNotFoundException;
    :catchall_0
    move-exception v3

    monitor-exit v4
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    throw v3

    .line 275
    :cond_3
    :try_start_5
    const-string v3, "Analytics - PackageManager was null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 276
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;
    :try_end_5
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_5 .. :try_end_5} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_5 .. :try_end_5} :catch_1
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    goto :goto_1

    .line 281
    :catch_1
    move-exception v0

    .line 282
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_6
    const-string v3, "Config - Unable to get package to pull application version. (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 283
    const-string v3, ""

    sput-object v3, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    goto :goto_1
.end method

.method protected static getApplicationVersionCode()I
    .locals 8

    .prologue
    const/4 v3, 0x0

    .line 293
    sget-object v4, Lcom/adobe/mobile/StaticMethods;->_applicationVersionCodeMutex:Ljava/lang/Object;

    monitor-enter v4

    .line 294
    :try_start_0
    sget v5, Lcom/adobe/mobile/StaticMethods;->versionCode:I
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    const/4 v6, -0x1

    if-ne v5, v6, :cond_1

    .line 296
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    .line 297
    .local v2, "packageManager":Landroid/content/pm/PackageManager;
    if-eqz v2, :cond_3

    .line 298
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    const/4 v6, 0x0

    invoke-virtual {v2, v5, v6}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v1

    .line 299
    .local v1, "packageInfo":Landroid/content/pm/PackageInfo;
    if-eqz v1, :cond_2

    .line 300
    iget v5, v1, Landroid/content/pm/PackageInfo;->versionCode:I

    if-lez v5, :cond_0

    iget v3, v1, Landroid/content/pm/PackageInfo;->versionCode:I

    :cond_0
    sput v3, Lcom/adobe/mobile/StaticMethods;->versionCode:I
    :try_end_1
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_1 .. :try_end_1} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 320
    .end local v1    # "packageInfo":Landroid/content/pm/PackageInfo;
    :cond_1
    :goto_0
    :try_start_2
    sget v3, Lcom/adobe/mobile/StaticMethods;->versionCode:I

    monitor-exit v4
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    return v3

    .line 303
    .restart local v1    # "packageInfo":Landroid/content/pm/PackageInfo;
    :cond_2
    :try_start_3
    const-string v3, "Analytics - PackageInfo was null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 304
    const/4 v3, 0x0

    sput v3, Lcom/adobe/mobile/StaticMethods;->versionCode:I
    :try_end_3
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_3 .. :try_end_3} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_3 .. :try_end_3} :catch_1
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    goto :goto_0

    .line 312
    .end local v1    # "packageInfo":Landroid/content/pm/PackageInfo;
    :catch_0
    move-exception v0

    .line 313
    .local v0, "e":Landroid/content/pm/PackageManager$NameNotFoundException;
    :try_start_4
    const-string v3, "Analytics - PackageManager couldn\'t find application version code (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Landroid/content/pm/PackageManager$NameNotFoundException;->toString()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 314
    const/4 v3, 0x0

    sput v3, Lcom/adobe/mobile/StaticMethods;->versionCode:I

    goto :goto_0

    .line 321
    .end local v0    # "e":Landroid/content/pm/PackageManager$NameNotFoundException;
    :catchall_0
    move-exception v3

    monitor-exit v4
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_0

    throw v3

    .line 308
    :cond_3
    :try_start_5
    const-string v3, "Analytics - PackageManager was null"

    const/4 v5, 0x0

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 309
    const/4 v3, 0x0

    sput v3, Lcom/adobe/mobile/StaticMethods;->versionCode:I
    :try_end_5
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_5 .. :try_end_5} :catch_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_5 .. :try_end_5} :catch_1
    .catchall {:try_start_5 .. :try_end_5} :catchall_0

    goto :goto_0

    .line 315
    :catch_1
    move-exception v0

    .line 316
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_6
    const-string v3, "Config - Unable to get package to pull application version code. (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v3, v5}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 317
    const/4 v3, 0x0

    sput v3, Lcom/adobe/mobile/StaticMethods;->versionCode:I
    :try_end_6
    .catchall {:try_start_6 .. :try_end_6} :catchall_0

    goto :goto_0
.end method

.method protected static getAudienceExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 643
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_audienceExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 644
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->audienceExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 645
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->audienceExecutor:Ljava/util/concurrent/ExecutorService;

    .line 648
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->audienceExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 649
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getCacheDirectory()Ljava/io/File;
    .locals 5

    .prologue
    .line 549
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/Context;->getCacheDir()Ljava/io/File;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v1

    .line 554
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :goto_0
    return-object v1

    .line 550
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_0
    move-exception v0

    .line 551
    .restart local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v1, "Config - Error getting cache directory. (%s)"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-static {v1, v2}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 554
    const/4 v1, 0x0

    goto :goto_0
.end method

.method protected static getCarrier()Ljava/lang/String;
    .locals 7

    .prologue
    .line 492
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->_carrierMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 493
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->carrier:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v2, :cond_0

    .line 495
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v2

    const-string v4, "phone"

    invoke-virtual {v2, v4}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/telephony/TelephonyManager;

    .line 496
    .local v1, "telephonyManager":Landroid/telephony/TelephonyManager;
    invoke-virtual {v1}, Landroid/telephony/TelephonyManager;->getNetworkOperatorName()Ljava/lang/String;

    move-result-object v2

    sput-object v2, Lcom/adobe/mobile/StaticMethods;->carrier:Ljava/lang/String;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 502
    :cond_0
    :goto_0
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->carrier:Ljava/lang/String;

    monitor-exit v3

    return-object v2

    .line 497
    :catch_0
    move-exception v0

    .line 498
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Config - Error getting device carrier. (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 503
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method protected static getCurrentActivity()Landroid/app/Activity;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/adobe/mobile/StaticMethods$NullActivityException;
        }
    .end annotation

    .prologue
    .line 1260
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_currentActivityMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 1261
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->_activity:Ljava/lang/ref/WeakReference;

    if-eqz v0, :cond_0

    sget-object v0, Lcom/adobe/mobile/StaticMethods;->_activity:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    if-nez v0, :cond_1

    .line 1262
    :cond_0
    new-instance v0, Lcom/adobe/mobile/StaticMethods$NullActivityException;

    const-string v2, "Message - No Current Activity (Messages must have a reference to the current activity to work properly)"

    invoke-direct {v0, v2}, Lcom/adobe/mobile/StaticMethods$NullActivityException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 1265
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0

    .line 1264
    :cond_1
    :try_start_1
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->_activity:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/app/Activity;

    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    return-object v0
.end method

.method protected static getCurrentOrientation()I
    .locals 2

    .prologue
    .line 1285
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCurrentActivity()Landroid/app/Activity;

    move-result-object v1

    invoke-virtual {v1}, Landroid/app/Activity;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    invoke-virtual {v1}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v1

    iget v1, v1, Landroid/content/res/Configuration;->orientation:I
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullActivityException; {:try_start_0 .. :try_end_0} :catch_0

    .line 1288
    .local v0, "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    :goto_0
    return v1

    .line 1287
    .end local v0    # "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    :catch_0
    move-exception v0

    .line 1288
    .restart local v0    # "ex":Lcom/adobe/mobile/StaticMethods$NullActivityException;
    const/4 v1, -0x1

    goto :goto_0
.end method

.method protected static getDebugLogging()Z
    .locals 1

    .prologue
    .line 805
    sget-boolean v0, Lcom/adobe/mobile/StaticMethods;->_debugLogging:Z

    return v0
.end method

.method protected static getDefaultAcceptLanguage()Ljava/lang/String;
    .locals 9

    .prologue
    const/4 v4, 0x0

    .line 1167
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v3

    .line 1173
    .local v3, "resources":Landroid/content/res/Resources;
    if-nez v3, :cond_1

    .line 1187
    .end local v3    # "resources":Landroid/content/res/Resources;
    :cond_0
    :goto_0
    return-object v4

    .line 1168
    :catch_0
    move-exception v1

    .line 1169
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v5, "Config - Error getting application resources for default accepted language. (%s)"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v5, v6}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 1177
    .end local v1    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .restart local v3    # "resources":Landroid/content/res/Resources;
    :cond_1
    invoke-virtual {v3}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v0

    .line 1178
    .local v0, "configuration":Landroid/content/res/Configuration;
    if-eqz v0, :cond_0

    .line 1182
    iget-object v2, v0, Landroid/content/res/Configuration;->locale:Ljava/util/Locale;

    .line 1183
    .local v2, "locale":Ljava/util/Locale;
    if-eqz v2, :cond_0

    .line 1187
    invoke-virtual {v2}, Ljava/util/Locale;->toString()Ljava/lang/String;

    move-result-object v4

    const/16 v5, 0x5f

    const/16 v6, 0x2d

    invoke-virtual {v4, v5, v6}, Ljava/lang/String;->replace(CC)Ljava/lang/String;

    move-result-object v4

    goto :goto_0
.end method

.method protected static getDefaultData()Ljava/util/HashMap;
    .locals 4
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
    .line 456
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_defaultDataMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 457
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    if-nez v0, :cond_0

    .line 458
    new-instance v0, Ljava/util/HashMap;

    invoke-direct {v0}, Ljava/util/HashMap;-><init>()V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    .line 459
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    const-string v2, "a.DeviceName"

    sget-object v3, Landroid/os/Build;->MODEL:Ljava/lang/String;

    invoke-virtual {v0, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 460
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    const-string v2, "a.Resolution"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getResolution()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 461
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    const-string v2, "a.OSVersion"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getOperatingSystem()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 462
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    const-string v2, "a.CarrierName"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getCarrier()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 463
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    const-string v2, "a.AppID"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getApplicationID()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v0, v2, v3}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 464
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    const-string v3, "a.RunMode"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isWearableApp()Z

    move-result v0

    if-eqz v0, :cond_1

    const-string v0, "Extension"

    :goto_0
    invoke-virtual {v2, v3, v0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 467
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    monitor-exit v1

    return-object v0

    .line 464
    :cond_1
    const-string v0, "Application"

    goto :goto_0

    .line 468
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getDefaultUserAgent()Ljava/lang/String;
    .locals 3

    .prologue
    .line 443
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_userAgentMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 444
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->userAgent:Ljava/lang/String;

    if-nez v0, :cond_0

    .line 445
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Mozilla/5.0 (Linux; U; Android "

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAndroidVersion()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v2, "; "

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    .line 446
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDefaultAcceptLanguage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v2, "; "

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    sget-object v2, Landroid/os/Build;->MODEL:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v2, " Build/"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    sget-object v2, Landroid/os/Build;->ID:Ljava/lang/String;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v2, ")"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->userAgent:Ljava/lang/String;

    .line 449
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->userAgent:Ljava/lang/String;

    monitor-exit v1

    return-object v0

    .line 450
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method private static getDeviceLocale()Ljava/util/Locale;
    .locals 7

    .prologue
    .line 1421
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v2

    .line 1427
    .local v2, "resources":Landroid/content/res/Resources;
    if-nez v2, :cond_0

    .line 1428
    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    .line 1436
    .end local v2    # "resources":Landroid/content/res/Resources;
    :goto_0
    return-object v3

    .line 1422
    :catch_0
    move-exception v1

    .line 1423
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v3, "Config - Error getting application resources for device locale. (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1424
    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    goto :goto_0

    .line 1431
    .end local v1    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .restart local v2    # "resources":Landroid/content/res/Resources;
    :cond_0
    invoke-virtual {v2}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v0

    .line 1432
    .local v0, "configuration":Landroid/content/res/Configuration;
    if-nez v0, :cond_1

    .line 1433
    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    goto :goto_0

    .line 1436
    :cond_1
    iget-object v3, v0, Landroid/content/res/Configuration;->locale:Ljava/util/Locale;

    if-eqz v3, :cond_2

    iget-object v3, v0, Landroid/content/res/Configuration;->locale:Ljava/util/Locale;

    goto :goto_0

    :cond_2
    sget-object v3, Ljava/util/Locale;->US:Ljava/util/Locale;

    goto :goto_0
.end method

.method protected static getHexString(Ljava/lang/String;)Ljava/lang/String;
    .locals 15
    .param p0, "originalString"    # Ljava/lang/String;

    .prologue
    const/4 v10, 0x0

    .line 1311
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v11

    if-gtz v11, :cond_1

    .line 1333
    :cond_0
    :goto_0
    return-object v10

    .line 1317
    :cond_1
    :try_start_0
    const-string v11, "UTF-8"

    invoke-virtual {p0, v11}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    move-result-object v0

    .line 1323
    .local v0, "bytes":[B
    array-length v1, v0

    .line 1324
    .local v1, "bytesLength":I
    shl-int/lit8 v10, v1, 0x1

    new-array v2, v10, [C

    .line 1326
    .local v2, "chars":[C
    const/4 v6, 0x0

    .line 1327
    .local v6, "index":I
    const/4 v8, 0x0

    .local v8, "offset":I
    move v9, v8

    .end local v8    # "offset":I
    .local v9, "offset":I
    move v7, v6

    .line 1328
    .end local v6    # "index":I
    .local v7, "index":I
    :goto_1
    if-ge v9, v1, :cond_2

    .line 1329
    add-int/lit8 v8, v9, 0x1

    .end local v9    # "offset":I
    .restart local v8    # "offset":I
    aget-byte v10, v0, v9

    and-int/lit16 v10, v10, 0xff

    shl-int/lit8 v4, v10, 0x1

    .line 1330
    .local v4, "hexIndex":I
    add-int/lit8 v6, v7, 0x1

    .end local v7    # "index":I
    .restart local v6    # "index":I
    sget-object v10, Lcom/adobe/mobile/StaticMethods;->BYTE_TO_HEX:[C

    add-int/lit8 v5, v4, 0x1

    .end local v4    # "hexIndex":I
    .local v5, "hexIndex":I
    aget-char v10, v10, v4

    aput-char v10, v2, v7

    .line 1331
    add-int/lit8 v7, v6, 0x1

    .end local v6    # "index":I
    .restart local v7    # "index":I
    sget-object v10, Lcom/adobe/mobile/StaticMethods;->BYTE_TO_HEX:[C

    aget-char v10, v10, v5

    aput-char v10, v2, v6

    move v9, v8

    .end local v8    # "offset":I
    .restart local v9    # "offset":I
    goto :goto_1

    .line 1319
    .end local v0    # "bytes":[B
    .end local v1    # "bytesLength":I
    .end local v2    # "chars":[C
    .end local v5    # "hexIndex":I
    .end local v7    # "index":I
    .end local v9    # "offset":I
    :catch_0
    move-exception v3

    .line 1320
    .local v3, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v11, "Failed to get hex from string (%s)"

    const/4 v12, 0x1

    new-array v12, v12, [Ljava/lang/Object;

    const/4 v13, 0x0

    invoke-virtual {v3}, Ljava/io/UnsupportedEncodingException;->getMessage()Ljava/lang/String;

    move-result-object v14

    aput-object v14, v12, v13

    invoke-static {v11, v12}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 1333
    .end local v3    # "ex":Ljava/io/UnsupportedEncodingException;
    .restart local v0    # "bytes":[B
    .restart local v1    # "bytesLength":I
    .restart local v2    # "chars":[C
    .restart local v7    # "index":I
    .restart local v9    # "offset":I
    :cond_2
    new-instance v10, Ljava/lang/String;

    invoke-direct {v10, v2}, Ljava/lang/String;-><init>([C)V

    goto :goto_0
.end method

.method protected static getIso8601Date()Ljava/lang/String;
    .locals 1

    .prologue
    .line 1440
    new-instance v0, Ljava/util/Date;

    invoke-direct {v0}, Ljava/util/Date;-><init>()V

    invoke-static {v0}, Lcom/adobe/mobile/StaticMethods;->getIso8601Date(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method

.method protected static getIso8601Date(Ljava/util/Date;)Ljava/lang/String;
    .locals 3
    .param p0, "date"    # Ljava/util/Date;

    .prologue
    .line 1444
    new-instance v0, Ljava/text/SimpleDateFormat;

    const-string v1, "yyyy-MM-dd\'T\'HH:mm:ssZZZ"

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDeviceLocale()Ljava/util/Locale;

    move-result-object v2

    invoke-direct {v0, v1, v2}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;Ljava/util/Locale;)V

    .line 1445
    .local v0, "iso8601Format":Ljava/text/DateFormat;
    if-eqz p0, :cond_0

    .end local p0    # "date":Ljava/util/Date;
    :goto_0
    invoke-virtual {v0, p0}, Ljava/text/DateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v1

    return-object v1

    .restart local p0    # "date":Ljava/util/Date;
    :cond_0
    new-instance p0, Ljava/util/Date;

    .end local p0    # "date":Ljava/util/Date;
    invoke-direct {p0}, Ljava/util/Date;-><init>()V

    goto :goto_0
.end method

.method protected static getMediaExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 560
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_mediaExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 561
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->mediaExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 562
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->mediaExecutor:Ljava/util/concurrent/ExecutorService;

    .line 565
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->mediaExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 566
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getMessageImageCachingExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 631
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_messageImageCachingExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 632
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->messageImageCachingExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 633
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->messageImageCachingExecutor:Ljava/util/concurrent/ExecutorService;

    .line 636
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->messageImageCachingExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 637
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getMessagesExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 608
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_messagesExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 609
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->messagesExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 610
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->messagesExecutor:Ljava/util/concurrent/ExecutorService;

    .line 613
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->messagesExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 614
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getOperatingSystem()Ljava/lang/String;
    .locals 7

    .prologue
    .line 509
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->_operatingSystemMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 510
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->operatingSystem:Ljava/lang/String;

    if-nez v2, :cond_0

    .line 511
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "Android "

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAndroidVersion()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    sput-object v2, Lcom/adobe/mobile/StaticMethods;->operatingSystem:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 514
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 515
    .local v0, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v2, "ADOBEMOBILE_STOREDDEFAULTS_OS"

    sget-object v4, Lcom/adobe/mobile/StaticMethods;->operatingSystem:Ljava/lang/String;

    invoke-interface {v0, v2, v4}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 516
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 523
    :cond_0
    :goto_0
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->operatingSystem:Ljava/lang/String;

    monitor-exit v3

    return-object v2

    .line 518
    :catch_0
    move-exception v1

    .line 519
    .local v1, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Config - Unable to set OS version in preferences (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 524
    .end local v1    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method protected static getPIIExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 655
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_piiExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 656
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->piiExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 657
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->piiExecutor:Ljava/util/concurrent/ExecutorService;

    .line 660
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->piiExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 661
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getPushIdentifier()Ljava/lang/String;
    .locals 2

    .prologue
    .line 391
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_pushIdentifierMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 392
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->pushIdentifier:Ljava/lang/String;

    monitor-exit v1

    return-object v0

    .line 393
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getResolution()Ljava/lang/String;
    .locals 7

    .prologue
    .line 475
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->_resolutionMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 476
    :try_start_0
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->resolution:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v2, :cond_0

    .line 478
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    .line 479
    .local v0, "displayMetrics":Landroid/util/DisplayMetrics;
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    iget v4, v0, Landroid/util/DisplayMetrics;->widthPixels:I

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v4, "x"

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    iget v4, v0, Landroid/util/DisplayMetrics;->heightPixels:I

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    sput-object v2, Lcom/adobe/mobile/StaticMethods;->resolution:Ljava/lang/String;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 485
    :cond_0
    :goto_0
    :try_start_2
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->resolution:Ljava/lang/String;

    monitor-exit v3

    return-object v2

    .line 480
    :catch_0
    move-exception v1

    .line 481
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Config - Error getting device resolution. (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 486
    .end local v1    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method protected static getSharedContext()Landroid/content/Context;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/adobe/mobile/StaticMethods$NullContextException;
        }
    .end annotation

    .prologue
    .line 1206
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->sharedContext:Landroid/content/Context;

    if-nez v0, :cond_0

    .line 1207
    new-instance v0, Lcom/adobe/mobile/StaticMethods$NullContextException;

    const-string v1, "Config - No Application Context (Application context must be set prior to calling any library functions.)"

    invoke-direct {v0, v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 1210
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->sharedContext:Landroid/content/Context;

    return-object v0
.end method

.method protected static getSharedExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 584
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_sharedExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 585
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->sharedExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 586
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->sharedExecutor:Ljava/util/concurrent/ExecutorService;

    .line 589
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->sharedExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 590
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getSharedPreferences()Landroid/content/SharedPreferences;
    .locals 4
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/adobe/mobile/StaticMethods$NullContextException;
        }
    .end annotation

    .prologue
    .line 183
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_sharedPreferencesMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 184
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->prefs:Landroid/content/SharedPreferences;

    if-nez v0, :cond_0

    .line 185
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedContext()Landroid/content/Context;

    move-result-object v0

    const-string v2, "APP_MEASUREMENT_CACHE"

    const/4 v3, 0x0

    invoke-virtual {v0, v2, v3}, Landroid/content/Context;->getSharedPreferences(Ljava/lang/String;I)Landroid/content/SharedPreferences;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->prefs:Landroid/content/SharedPreferences;

    .line 186
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->prefs:Landroid/content/SharedPreferences;

    if-nez v0, :cond_0

    .line 187
    const-string v0, "Config - No SharedPreferences available"

    const/4 v2, 0x0

    new-array v2, v2, [Ljava/lang/Object;

    invoke-static {v0, v2}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 191
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->prefs:Landroid/content/SharedPreferences;

    if-nez v0, :cond_1

    .line 192
    new-instance v0, Lcom/adobe/mobile/StaticMethods$NullContextException;

    const-string v2, "Config - No SharedPreferences available"

    invoke-direct {v0, v2}, Lcom/adobe/mobile/StaticMethods$NullContextException;-><init>(Ljava/lang/String;)V

    throw v0

    .line 196
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0

    .line 195
    :cond_1
    :try_start_1
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->prefs:Landroid/content/SharedPreferences;

    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    return-object v0
.end method

.method protected static getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;
    .locals 3
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Lcom/adobe/mobile/StaticMethods$NullContextException;
        }
    .end annotation

    .prologue
    .line 1195
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    invoke-interface {v1}, Landroid/content/SharedPreferences;->edit()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 1196
    .local v0, "editor":Landroid/content/SharedPreferences$Editor;
    if-nez v0, :cond_0

    .line 1197
    new-instance v1, Lcom/adobe/mobile/StaticMethods$NullContextException;

    const-string v2, "Config - Unable to create an instance of a SharedPreferences Editor"

    invoke-direct {v1, v2}, Lcom/adobe/mobile/StaticMethods$NullContextException;-><init>(Ljava/lang/String;)V

    throw v1

    .line 1200
    :cond_0
    return-object v0
.end method

.method protected static getThirdPartyCallbacksExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 620
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_thirdPartyCallbacksExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 621
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->thirdPartyCallbacksExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 622
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->thirdPartyCallbacksExecutor:Ljava/util/concurrent/ExecutorService;

    .line 624
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->thirdPartyCallbacksExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 625
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getTimeSince1970()J
    .locals 4

    .prologue
    .line 1140
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    const-wide/16 v2, 0x3e8

    div-long/2addr v0, v2

    return-wide v0
.end method

.method protected static getTimeSinceLaunch()J
    .locals 6

    .prologue
    .line 1224
    sget-wide v0, Lcom/adobe/mobile/Lifecycle;->sessionStartTime:J

    .line 1225
    .local v0, "originalStartDate":J
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v4

    sub-long v2, v4, v0

    .line 1228
    .local v2, "timeSinceLaunch":J
    const-wide/32 v4, 0x93a80

    cmp-long v4, v2, v4

    if-gez v4, :cond_0

    .end local v2    # "timeSinceLaunch":J
    :goto_0
    return-wide v2

    .restart local v2    # "timeSinceLaunch":J
    :cond_0
    const-wide/16 v2, 0x0

    goto :goto_0
.end method

.method public static getTimedActionsExecutor()Ljava/util/concurrent/ExecutorService;
    .locals 2

    .prologue
    .line 572
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_timedActionsExecutorMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 573
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->timedActionsExecutor:Ljava/util/concurrent/ExecutorService;

    if-nez v0, :cond_0

    .line 574
    invoke-static {}, Ljava/util/concurrent/Executors;->newSingleThreadExecutor()Ljava/util/concurrent/ExecutorService;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->timedActionsExecutor:Ljava/util/concurrent/ExecutorService;

    .line 577
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->timedActionsExecutor:Ljava/util/concurrent/ExecutorService;

    monitor-exit v1

    return-object v0

    .line 578
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getTimestampString()Ljava/lang/String;
    .locals 12

    .prologue
    .line 534
    sget-object v9, Lcom/adobe/mobile/StaticMethods;->_timestampMutex:Ljava/lang/Object;

    monitor-enter v9

    .line 535
    :try_start_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->timestamp:Ljava/lang/String;

    if-nez v0, :cond_0

    .line 536
    new-instance v7, Ljava/util/Date;

    invoke-direct {v7}, Ljava/util/Date;-><init>()V

    .line 537
    .local v7, "date":Ljava/util/Date;
    invoke-static {}, Ljava/util/Calendar;->getInstance()Ljava/util/Calendar;

    move-result-object v8

    .line 538
    .local v8, "tm":Ljava/util/Calendar;
    invoke-virtual {v8, v7}, Ljava/util/Calendar;->setTime(Ljava/util/Date;)V

    .line 539
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "00/00/0000 00:00:00 0 "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v10

    .line 540
    invoke-virtual {v8}, Ljava/util/Calendar;->getTimeZone()Ljava/util/TimeZone;

    move-result-object v0

    const/4 v1, 0x1

    const/4 v2, 0x1

    invoke-virtual {v8, v2}, Ljava/util/Calendar;->get(I)I

    move-result v2

    const/4 v3, 0x2

    invoke-virtual {v8, v3}, Ljava/util/Calendar;->get(I)I

    move-result v3

    const/4 v4, 0x5

    invoke-virtual {v8, v4}, Ljava/util/Calendar;->get(I)I

    move-result v4

    const/4 v5, 0x7

    invoke-virtual {v8, v5}, Ljava/util/Calendar;->get(I)I

    move-result v5

    const/16 v6, 0xb

    invoke-virtual {v8, v6}, Ljava/util/Calendar;->get(I)I

    move-result v6

    mul-int/lit8 v6, v6, 0x3c

    const/16 v11, 0xc

    invoke-virtual {v8, v11}, Ljava/util/Calendar;->get(I)I

    move-result v11

    add-int/2addr v6, v11

    mul-int/lit8 v6, v6, 0x3c

    const/16 v11, 0xd

    invoke-virtual {v8, v11}, Ljava/util/Calendar;->get(I)I

    move-result v11

    add-int/2addr v6, v11

    mul-int/lit16 v6, v6, 0x3e8

    const/16 v11, 0xe

    invoke-virtual {v8, v11}, Ljava/util/Calendar;->get(I)I

    move-result v11

    add-int/2addr v6, v11

    invoke-virtual/range {v0 .. v6}, Ljava/util/TimeZone;->getOffset(IIIIII)I

    move-result v0

    const v1, 0xea60

    div-int/2addr v0, v1

    mul-int/lit8 v0, v0, -0x1

    invoke-virtual {v10, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->timestamp:Ljava/lang/String;

    .line 543
    :cond_0
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->timestamp:Ljava/lang/String;

    monitor-exit v9

    return-object v0

    .line 544
    .end local v8    # "tm":Ljava/util/Calendar;
    :catchall_0
    move-exception v0

    monitor-exit v9
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static getVisitorID()Ljava/lang/String;
    .locals 6

    .prologue
    .line 327
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->_visitorIDMutex:Ljava/lang/Object;

    monitor-enter v2

    .line 328
    :try_start_0
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->visitorID:Ljava/lang/String;
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-nez v1, :cond_0

    .line 330
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    const-string v3, "APP_MEASUREMENT_VISITOR_ID"

    const/4 v4, 0x0

    invoke-interface {v1, v3, v4}, Landroid/content/SharedPreferences;->getString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    sput-object v1, Lcom/adobe/mobile/StaticMethods;->visitorID:Ljava/lang/String;
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 336
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :cond_0
    :goto_0
    :try_start_2
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->visitorID:Ljava/lang/String;

    monitor-exit v2

    return-object v1

    .line 331
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_0
    move-exception v0

    .line 332
    .restart local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v1, "Config - Unable to pull visitorID from shared preferences. (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v1, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 337
    :catchall_0
    move-exception v1

    monitor-exit v2
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v1
.end method

.method protected static hexToString(Ljava/lang/String;)Ljava/lang/String;
    .locals 10
    .param p0, "hexString"    # Ljava/lang/String;

    .prologue
    const/16 v9, 0x10

    .line 1337
    if-eqz p0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v6

    if-lez v6, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v6

    rem-int/lit8 v6, v6, 0x2

    if-eqz v6, :cond_1

    .line 1338
    :cond_0
    const/4 v1, 0x0

    .line 1356
    :goto_0
    return-object v1

    .line 1341
    :cond_1
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v5

    .line 1342
    .local v5, "length":I
    div-int/lit8 v6, v5, 0x2

    new-array v0, v6, [B

    .line 1343
    .local v0, "data":[B
    const/4 v4, 0x0

    .local v4, "i":I
    :goto_1
    if-ge v4, v5, :cond_2

    .line 1344
    div-int/lit8 v6, v4, 0x2

    invoke-virtual {p0, v4}, Ljava/lang/String;->charAt(I)C

    move-result v7

    invoke-static {v7, v9}, Ljava/lang/Character;->digit(CI)I

    move-result v7

    shl-int/lit8 v7, v7, 0x4

    add-int/lit8 v8, v4, 0x1

    .line 1345
    invoke-virtual {p0, v8}, Ljava/lang/String;->charAt(I)C

    move-result v8

    invoke-static {v8, v9}, Ljava/lang/Character;->digit(CI)I

    move-result v8

    add-int/2addr v7, v8

    int-to-byte v7, v7

    aput-byte v7, v0, v6

    .line 1343
    add-int/lit8 v4, v4, 0x2

    goto :goto_1

    .line 1348
    :cond_2
    const/4 v1, 0x0

    .line 1350
    .local v1, "decodedString":Ljava/lang/String;
    :try_start_0
    new-instance v2, Ljava/lang/String;

    const-string v6, "UTF-8"

    invoke-direct {v2, v0, v6}, Ljava/lang/String;-><init>([BLjava/lang/String;)V
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    .end local v1    # "decodedString":Ljava/lang/String;
    .local v2, "decodedString":Ljava/lang/String;
    move-object v1, v2

    .line 1354
    .end local v2    # "decodedString":Ljava/lang/String;
    .restart local v1    # "decodedString":Ljava/lang/String;
    goto :goto_0

    .line 1352
    :catch_0
    move-exception v3

    .line 1353
    .local v3, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v6, "Failed to get string from hex (%s)"

    const/4 v7, 0x1

    new-array v7, v7, [Ljava/lang/Object;

    const/4 v8, 0x0

    invoke-virtual {v3}, Ljava/io/UnsupportedEncodingException;->getMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v6, v7}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static isPushEnabled()Z
    .locals 6

    .prologue
    .line 358
    sget-object v2, Lcom/adobe/mobile/StaticMethods;->_pushEnabledMutex:Ljava/lang/Object;

    monitor-enter v2

    .line 360
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v1

    const-string v3, "ADBMOBILE_KEY_PUSH_ENABLED"

    const/4 v4, 0x0

    invoke-interface {v1, v3, v4}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z

    move-result v1

    sput-boolean v1, Lcom/adobe/mobile/StaticMethods;->pushEnabled:Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 364
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :goto_0
    :try_start_1
    monitor-exit v2
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 366
    sget-boolean v1, Lcom/adobe/mobile/StaticMethods;->pushEnabled:Z

    return v1

    .line 361
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catch_0
    move-exception v0

    .line 362
    .restart local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :try_start_2
    const-string v1, "Config - Unable to pull push status from shared preferences. (%s)"

    const/4 v3, 0x1

    new-array v3, v3, [Ljava/lang/Object;

    const/4 v4, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v3, v4

    invoke-static {v1, v3}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 364
    :catchall_0
    move-exception v1

    monitor-exit v2
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v1
.end method

.method protected static isWearableApp()Z
    .locals 1

    .prologue
    .line 819
    sget-boolean v0, Lcom/adobe/mobile/StaticMethods;->_isWearable:Z

    return v0
.end method

.method protected static join(Ljava/lang/Iterable;Ljava/lang/String;)Ljava/lang/String;
    .locals 3
    .param p1, "delimiter"    # Ljava/lang/String;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/Iterable",
            "<*>;",
            "Ljava/lang/String;",
            ")",
            "Ljava/lang/String;"
        }
    .end annotation

    .prologue
    .line 886
    .local p0, "elements":Ljava/lang/Iterable;, "Ljava/lang/Iterable<*>;"
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    .line 888
    .local v1, "sb":Ljava/lang/StringBuilder;
    invoke-interface {p0}, Ljava/lang/Iterable;->iterator()Ljava/util/Iterator;

    move-result-object v0

    .line 890
    .local v0, "iter":Ljava/util/Iterator;
    :goto_0
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-eqz v2, :cond_0

    .line 891
    invoke-interface {v0}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    .line 892
    invoke-interface {v0}, Ljava/util/Iterator;->hasNext()Z

    move-result v2

    if-nez v2, :cond_1

    .line 897
    :cond_0
    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    return-object v2

    .line 895
    :cond_1
    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0
.end method

.method protected static varargs logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    .locals 4
    .param p0, "format"    # Ljava/lang/String;
    .param p1, "args"    # [Ljava/lang/Object;

    .prologue
    .line 973
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDebugLogging()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 974
    if-eqz p1, :cond_1

    array-length v1, p1

    if-lez v1, :cond_1

    .line 975
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "ADBMobile Debug : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 976
    .local v0, "formattedString":Ljava/lang/String;
    const-string v1, "ADBMobile"

    invoke-static {v1, v0}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 982
    .end local v0    # "formattedString":Ljava/lang/String;
    :cond_0
    :goto_0
    return-void

    .line 979
    :cond_1
    const-string v1, "ADBMobile"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "ADBMobile Debug : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0
.end method

.method protected static varargs logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    .locals 4
    .param p0, "format"    # Ljava/lang/String;
    .param p1, "args"    # [Ljava/lang/Object;

    .prologue
    .line 951
    if-eqz p1, :cond_0

    array-length v1, p1

    if-lez v1, :cond_0

    .line 952
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "ADBMobile Error : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 953
    .local v0, "formattedString":Ljava/lang/String;
    const-string v1, "ADBMobile"

    invoke-static {v1, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 958
    .end local v0    # "formattedString":Ljava/lang/String;
    :goto_0
    return-void

    .line 956
    :cond_0
    const-string v1, "ADBMobile"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "ADBMobile Error : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0
.end method

.method protected static varargs logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V
    .locals 4
    .param p0, "format"    # Ljava/lang/String;
    .param p1, "args"    # [Ljava/lang/Object;

    .prologue
    .line 961
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getDebugLogging()Z

    move-result v1

    if-eqz v1, :cond_0

    .line 962
    if-eqz p1, :cond_1

    array-length v1, p1

    if-lez v1, :cond_1

    .line 963
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "ADBMobile Warning : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1, p1}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v0

    .line 964
    .local v0, "formattedString":Ljava/lang/String;
    const-string v1, "ADBMobile"

    invoke-static {v1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 970
    .end local v0    # "formattedString":Ljava/lang/String;
    :cond_0
    :goto_0
    return-void

    .line 967
    :cond_1
    const-string v1, "ADBMobile"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "ADBMobile Warning : "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v1, v2}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0
.end method

.method protected static mapFromJson(Lorg/json/JSONObject;)Ljava/util/HashMap;
    .locals 8
    .param p0, "jsonData"    # Lorg/json/JSONObject;
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
    .line 1233
    if-nez p0, :cond_1

    .line 1234
    const/4 v2, 0x0

    .line 1248
    :cond_0
    return-object v2

    .line 1238
    :cond_1
    invoke-virtual {p0}, Lorg/json/JSONObject;->keys()Ljava/util/Iterator;

    move-result-object v1

    .line 1239
    .local v1, "keyItr":Ljava/util/Iterator;, "Ljava/util/Iterator<Ljava/lang/String;>;"
    new-instance v2, Ljava/util/HashMap;

    invoke-direct {v2}, Ljava/util/HashMap;-><init>()V

    .line 1240
    .local v2, "map":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/Object;>;"
    :goto_0
    invoke-interface {v1}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    .line 1241
    invoke-interface {v1}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Ljava/lang/String;

    .line 1243
    .local v3, "name":Ljava/lang/String;
    :try_start_0
    invoke-virtual {p0, v3}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 1244
    :catch_0
    move-exception v0

    .line 1245
    .local v0, "ex":Lorg/json/JSONException;
    const-string v4, "Problem parsing json data (%s)"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    const/4 v6, 0x0

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v7

    aput-object v7, v5, v6

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method

.method protected static reset()V
    .locals 4

    .prologue
    const/4 v3, 0x0

    const/4 v2, 0x0

    .line 1450
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->appID:Ljava/lang/String;

    .line 1451
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->appName:Ljava/lang/String;

    .line 1452
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->visitorID:Ljava/lang/String;

    .line 1453
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->userAgent:Ljava/lang/String;

    .line 1454
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_defaultDataMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 1455
    const/4 v0, 0x0

    :try_start_0
    sput-object v0, Lcom/adobe/mobile/StaticMethods;->defaultData:Ljava/util/HashMap;

    .line 1456
    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 1457
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->resolution:Ljava/lang/String;

    .line 1458
    const/4 v0, -0x1

    sput v0, Lcom/adobe/mobile/StaticMethods;->versionCode:I

    .line 1459
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->versionName:Ljava/lang/String;

    .line 1460
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->carrier:Ljava/lang/String;

    .line 1461
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->operatingSystem:Ljava/lang/String;

    .line 1462
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->aid:Ljava/lang/String;

    .line 1463
    sput-boolean v3, Lcom/adobe/mobile/StaticMethods;->_aidDone:Z

    .line 1464
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->sharedContext:Landroid/content/Context;

    .line 1465
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->prefs:Landroid/content/SharedPreferences;

    .line 1466
    sput-object v2, Lcom/adobe/mobile/StaticMethods;->_activity:Ljava/lang/ref/WeakReference;

    .line 1467
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteList:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->clear()V

    .line 1468
    sput v3, Lcom/adobe/mobile/StaticMethods;->_contextDataKeyWhiteListCount:I

    .line 1469
    sget-object v0, Lcom/adobe/mobile/Config$ApplicationType;->APPLICATION_TYPE_HANDHELD:Lcom/adobe/mobile/Config$ApplicationType;

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_appType:Lcom/adobe/mobile/Config$ApplicationType;

    .line 1470
    sput-boolean v3, Lcom/adobe/mobile/StaticMethods;->_isWearable:Z

    .line 1471
    return-void

    .line 1456
    :catchall_0
    move-exception v0

    :try_start_1
    monitor-exit v1
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v0
.end method

.method private static retrieveAIDFromVisitorIDService()Ljava/lang/String;
    .locals 12

    .prologue
    const/4 v7, 0x0

    const/4 v11, 0x1

    const/4 v10, 0x0

    .line 740
    new-instance v4, Ljava/lang/StringBuilder;

    const/16 v6, 0x40

    invoke-direct {v4, v6}, Ljava/lang/StringBuilder;-><init>(I)V

    .line 741
    .local v4, "urlSb":Ljava/lang/StringBuilder;
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v6

    invoke-virtual {v6}, Lcom/adobe/mobile/MobileConfig;->getSSL()Z

    move-result v6

    if-eqz v6, :cond_3

    const-string v6, "https"

    :goto_0
    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 742
    const-string v6, "://"

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 743
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v6

    invoke-virtual {v6}, Lcom/adobe/mobile/MobileConfig;->getTrackingServer()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 744
    const-string v6, "/id"

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 746
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v6

    invoke-virtual {v6}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v5

    .line 748
    .local v5, "useVisidService":Z
    if-eqz v5, :cond_0

    .line 749
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v6

    invoke-virtual {v6}, Lcom/adobe/mobile/VisitorIDService;->getAnalyticsIDRequestParameterString()Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v4, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 752
    :cond_0
    const-string v6, "Analytics ID - Sending Analytics ID call(%s)"

    new-array v8, v11, [Ljava/lang/Object;

    aput-object v4, v8, v10

    invoke-static {v6, v8}, Lcom/adobe/mobile/StaticMethods;->logDebugFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 753
    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v6

    const/16 v8, 0x1f4

    const-string v9, "Analytics ID"

    invoke-static {v6, v7, v8, v9}, Lcom/adobe/mobile/RequestHandler;->retrieveData(Ljava/lang/String;Ljava/util/Map;ILjava/lang/String;)[B

    move-result-object v3

    .line 755
    .local v3, "serverResponse":[B
    const/4 v1, 0x0

    .line 756
    .local v1, "identifier":Ljava/lang/String;
    if-eqz v3, :cond_1

    .line 758
    :try_start_0
    new-instance v2, Lorg/json/JSONObject;

    new-instance v6, Ljava/lang/String;

    const-string v8, "UTF-8"

    invoke-direct {v6, v3, v8}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    invoke-direct {v2, v6}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    .line 759
    .local v2, "jsonResponse":Lorg/json/JSONObject;
    const-string v6, "id"

    invoke-virtual {v2, v6}, Lorg/json/JSONObject;->getString(Ljava/lang/String;)Ljava/lang/String;
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_1

    move-result-object v1

    .line 770
    .end local v2    # "jsonResponse":Lorg/json/JSONObject;
    :cond_1
    :goto_1
    if-eqz v1, :cond_2

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v6

    const/16 v8, 0x21

    if-eq v6, v8, :cond_5

    .line 771
    :cond_2
    if-eqz v5, :cond_4

    move-object v6, v7

    .line 778
    :goto_2
    return-object v6

    .line 741
    .end local v1    # "identifier":Ljava/lang/String;
    .end local v3    # "serverResponse":[B
    .end local v5    # "useVisidService":Z
    :cond_3
    const-string v6, "http"

    goto :goto_0

    .line 760
    .restart local v1    # "identifier":Ljava/lang/String;
    .restart local v3    # "serverResponse":[B
    .restart local v5    # "useVisidService":Z
    :catch_0
    move-exception v0

    .line 761
    .local v0, "ex":Ljava/io/UnsupportedEncodingException;
    const-string v6, "Analytics ID - Unable to decode /id response(%s)"

    new-array v8, v11, [Ljava/lang/Object;

    invoke-virtual {v0}, Ljava/io/UnsupportedEncodingException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v10

    invoke-static {v6, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 762
    .end local v0    # "ex":Ljava/io/UnsupportedEncodingException;
    :catch_1
    move-exception v0

    .line 763
    .local v0, "ex":Lorg/json/JSONException;
    if-nez v5, :cond_1

    .line 764
    const-string v6, "Analytics ID - Unable to parse /id response(%s)"

    new-array v8, v11, [Ljava/lang/Object;

    invoke-virtual {v0}, Lorg/json/JSONException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v9

    aput-object v9, v8, v10

    invoke-static {v6, v8}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1

    .line 775
    .end local v0    # "ex":Lorg/json/JSONException;
    :cond_4
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->generateAID()Ljava/lang/String;

    move-result-object v1

    :cond_5
    move-object v6, v1

    .line 778
    goto :goto_2
.end method

.method private static serializeKeyValuePair(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/StringBuilder;)V
    .locals 1
    .param p0, "key"    # Ljava/lang/String;
    .param p1, "value"    # Ljava/lang/Object;
    .param p2, "builder"    # Ljava/lang/StringBuilder;

    .prologue
    .line 866
    if-eqz p0, :cond_0

    if-eqz p1, :cond_0

    instance-of v0, p1, Lcom/adobe/mobile/ContextData;

    if-nez v0, :cond_0

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-gtz v0, :cond_1

    .line 883
    .end local p1    # "value":Ljava/lang/Object;
    :cond_0
    :goto_0
    return-void

    .line 870
    .restart local p1    # "value":Ljava/lang/Object;
    :cond_1
    instance-of v0, p1, Ljava/lang/String;

    if-eqz v0, :cond_2

    move-object v0, p1

    check-cast v0, Ljava/lang/String;

    invoke-virtual {v0}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    .line 874
    :cond_2
    const-string v0, "&"

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 875
    invoke-virtual {p2, p0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 876
    const-string v0, "="

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 878
    instance-of v0, p1, Ljava/util/List;

    if-eqz v0, :cond_3

    .line 879
    check-cast p1, Ljava/util/List;

    .end local p1    # "value":Ljava/lang/Object;
    const-string v0, ","

    invoke-static {p1, v0}, Lcom/adobe/mobile/StaticMethods;->join(Ljava/lang/Iterable;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 881
    .restart local p1    # "value":Ljava/lang/Object;
    :cond_3
    invoke-virtual {p1}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0
.end method

.method protected static serializeToQueryString(Ljava/util/Map;Ljava/lang/StringBuilder;)V
    .locals 6
    .param p1, "builder"    # Ljava/lang/StringBuilder;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;",
            "Ljava/lang/StringBuilder;",
            ")V"
        }
    .end annotation

    .prologue
    .line 827
    .local p0, "parameters":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    if-nez p0, :cond_1

    .line 862
    :cond_0
    return-void

    .line 831
    :cond_1
    invoke-interface {p0}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v4

    invoke-interface {v4}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v5

    :cond_2
    :goto_0
    invoke-interface {v5}, Ljava/util/Iterator;->hasNext()Z

    move-result v4

    if-eqz v4, :cond_0

    invoke-interface {v5}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 832
    .local v1, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/lang/String;

    invoke-static {v4}, Lcom/adobe/mobile/StaticMethods;->URLEncode(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    .line 835
    .local v2, "key":Ljava/lang/String;
    if-eqz v2, :cond_2

    .line 839
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v3

    .line 842
    .local v3, "obj":Ljava/lang/Object;
    instance-of v4, v3, Lcom/adobe/mobile/ContextData;

    if-eqz v4, :cond_4

    move-object v0, v3

    .line 843
    check-cast v0, Lcom/adobe/mobile/ContextData;

    .line 845
    .local v0, "data":Lcom/adobe/mobile/ContextData;
    iget-object v4, v0, Lcom/adobe/mobile/ContextData;->value:Ljava/lang/Object;

    if-eqz v4, :cond_3

    .line 846
    iget-object v4, v0, Lcom/adobe/mobile/ContextData;->value:Ljava/lang/Object;

    invoke-static {v2, v4, p1}, Lcom/adobe/mobile/StaticMethods;->serializeKeyValuePair(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/StringBuilder;)V

    .line 850
    :cond_3
    iget-object v4, v0, Lcom/adobe/mobile/ContextData;->contextData:Ljava/util/HashMap;

    if-eqz v4, :cond_2

    iget-object v4, v0, Lcom/adobe/mobile/ContextData;->contextData:Ljava/util/HashMap;

    invoke-virtual {v4}, Ljava/util/HashMap;->size()I

    move-result v4

    if-lez v4, :cond_2

    .line 851
    const-string v4, "&"

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 852
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 853
    const-string v4, "."

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 854
    iget-object v4, v0, Lcom/adobe/mobile/ContextData;->contextData:Ljava/util/HashMap;

    invoke-static {v4, p1}, Lcom/adobe/mobile/StaticMethods;->serializeToQueryString(Ljava/util/Map;Ljava/lang/StringBuilder;)V

    .line 855
    const-string v4, "&."

    invoke-virtual {p1, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 856
    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    goto :goto_0

    .line 859
    .end local v0    # "data":Lcom/adobe/mobile/ContextData;
    :cond_4
    invoke-static {v2, v3, p1}, Lcom/adobe/mobile/StaticMethods;->serializeKeyValuePair(Ljava/lang/String;Ljava/lang/Object;Ljava/lang/StringBuilder;)V

    goto :goto_0
.end method

.method protected static setAdvertisingIdentifier(Ljava/lang/String;)V
    .locals 4
    .param p0, "adid"    # Ljava/lang/String;

    .prologue
    .line 427
    if-eqz p0, :cond_0

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getAdvertisingIdentifier()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, v0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_0

    .line 438
    :goto_0
    return-void

    .line 431
    :cond_0
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_advertisingIdentifierMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 432
    :try_start_0
    sput-object p0, Lcom/adobe/mobile/StaticMethods;->advertisingIdentifier:Ljava/lang/String;

    .line 433
    invoke-static {p0}, Lcom/adobe/mobile/WearableFunctionBridge;->syncAdvertisingIdentifierToWearable(Ljava/lang/String;)V

    .line 434
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v0

    const/4 v2, 0x0

    new-instance v3, Lcom/adobe/mobile/StaticMethods$4;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/StaticMethods$4;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v2, v3}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;)V

    .line 437
    monitor-exit v1

    goto :goto_0

    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static setApplicationType(Lcom/adobe/mobile/Config$ApplicationType;)V
    .locals 2
    .param p0, "apptype"    # Lcom/adobe/mobile/Config$ApplicationType;

    .prologue
    .line 810
    sput-object p0, Lcom/adobe/mobile/StaticMethods;->_appType:Lcom/adobe/mobile/Config$ApplicationType;

    .line 811
    sget-object v0, Lcom/adobe/mobile/StaticMethods;->_appType:Lcom/adobe/mobile/Config$ApplicationType;

    sget-object v1, Lcom/adobe/mobile/Config$ApplicationType;->APPLICATION_TYPE_WEARABLE:Lcom/adobe/mobile/Config$ApplicationType;

    if-ne v0, v1, :cond_0

    const/4 v0, 0x1

    :goto_0
    sput-boolean v0, Lcom/adobe/mobile/StaticMethods;->_isWearable:Z

    .line 812
    return-void

    .line 811
    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected static setCurrentActivity(Landroid/app/Activity;)V
    .locals 2
    .param p0, "activity"    # Landroid/app/Activity;

    .prologue
    .line 1254
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_currentActivityMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 1255
    :try_start_0
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p0}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->_activity:Ljava/lang/ref/WeakReference;

    .line 1256
    monitor-exit v1

    .line 1257
    return-void

    .line 1256
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static setDebugLogging(Z)V
    .locals 0
    .param p0, "enabled"    # Z

    .prologue
    .line 801
    sput-boolean p0, Lcom/adobe/mobile/StaticMethods;->_debugLogging:Z

    .line 802
    return-void
.end method

.method protected static setPushEnabled(Z)V
    .locals 7
    .param p0, "enabled"    # Z

    .prologue
    .line 370
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->_pushEnabledMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 373
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 374
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v2, "ADBMOBILE_KEY_PUSH_ENABLED"

    invoke-interface {v1, v2, p0}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    .line 375
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z

    .line 378
    sput-boolean p0, Lcom/adobe/mobile/StaticMethods;->pushEnabled:Z

    .line 381
    invoke-static {p0}, Lcom/adobe/mobile/WearableFunctionBridge;->syncPushEnabledToWearable(Z)V
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 385
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_0
    :try_start_1
    monitor-exit v3

    .line 386
    return-void

    .line 382
    :catch_0
    move-exception v0

    .line 383
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Config - Unable to set pushEnabled shared preferences. (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 385
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    throw v2
.end method

.method protected static setPushIdentifier(Ljava/lang/String;)V
    .locals 6
    .param p0, "registrationID"    # Ljava/lang/String;

    .prologue
    .line 397
    sget-object v1, Lcom/adobe/mobile/StaticMethods;->_pushIdentifierMutex:Ljava/lang/Object;

    monitor-enter v1

    .line 398
    if-nez p0, :cond_0

    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isPushEnabled()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 399
    const/4 v0, 0x0

    invoke-static {v0}, Lcom/adobe/mobile/StaticMethods;->setPushEnabled(Z)V

    .line 400
    const-string v0, "Push"

    new-instance v2, Lcom/adobe/mobile/StaticMethods$1;

    invoke-direct {v2}, Lcom/adobe/mobile/StaticMethods$1;-><init>()V

    .line 401
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v4

    .line 400
    invoke-static {v0, v2, v4, v5}, Lcom/adobe/mobile/AnalyticsTrackInternal;->trackInternal(Ljava/lang/String;Ljava/util/Map;J)V

    .line 404
    :cond_0
    if-eqz p0, :cond_1

    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-ltz v0, :cond_1

    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->isPushEnabled()Z

    move-result v0

    if-nez v0, :cond_1

    .line 405
    const/4 v0, 0x1

    invoke-static {v0}, Lcom/adobe/mobile/StaticMethods;->setPushEnabled(Z)V

    .line 406
    const-string v0, "Push"

    new-instance v2, Lcom/adobe/mobile/StaticMethods$2;

    invoke-direct {v2}, Lcom/adobe/mobile/StaticMethods$2;-><init>()V

    .line 407
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getTimeSince1970()J

    move-result-wide v4

    .line 406
    invoke-static {v0, v2, v4, v5}, Lcom/adobe/mobile/AnalyticsTrackInternal;->trackInternal(Ljava/lang/String;Ljava/util/Map;J)V

    .line 410
    :cond_1
    sput-object p0, Lcom/adobe/mobile/StaticMethods;->pushIdentifier:Ljava/lang/String;

    .line 412
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v0

    const/4 v2, 0x0

    new-instance v3, Lcom/adobe/mobile/StaticMethods$3;

    invoke-direct {v3, p0}, Lcom/adobe/mobile/StaticMethods$3;-><init>(Ljava/lang/String;)V

    invoke-virtual {v0, v2, v3}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;Ljava/util/Map;)V

    .line 415
    monitor-exit v1

    .line 416
    return-void

    .line 415
    :catchall_0
    move-exception v0

    monitor-exit v1
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    throw v0
.end method

.method protected static setSharedContext(Landroid/content/Context;)V
    .locals 1
    .param p0, "context"    # Landroid/content/Context;

    .prologue
    .line 1214
    if-eqz p0, :cond_0

    .line 1215
    instance-of v0, p0, Landroid/app/Activity;

    if-eqz v0, :cond_1

    .line 1216
    invoke-virtual {p0}, Landroid/content/Context;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    sput-object v0, Lcom/adobe/mobile/StaticMethods;->sharedContext:Landroid/content/Context;

    .line 1221
    :cond_0
    :goto_0
    return-void

    .line 1218
    :cond_1
    sput-object p0, Lcom/adobe/mobile/StaticMethods;->sharedContext:Landroid/content/Context;

    goto :goto_0
.end method

.method protected static setVisitorID(Ljava/lang/String;)V
    .locals 7
    .param p0, "uid"    # Ljava/lang/String;

    .prologue
    .line 342
    sget-object v3, Lcom/adobe/mobile/StaticMethods;->_userIdentifierMutex:Ljava/lang/Object;

    monitor-enter v3

    .line 343
    :try_start_0
    sput-object p0, Lcom/adobe/mobile/StaticMethods;->visitorID:Ljava/lang/String;

    .line 344
    invoke-static {p0}, Lcom/adobe/mobile/WearableFunctionBridge;->syncVisitorIDToWearable(Ljava/lang/String;)V
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 346
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v1

    .line 347
    .local v1, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v2, "APP_MEASUREMENT_VISITOR_ID"

    invoke-interface {v1, v2, p0}, Landroid/content/SharedPreferences$Editor;->putString(Ljava/lang/String;Ljava/lang/String;)Landroid/content/SharedPreferences$Editor;

    .line 348
    invoke-interface {v1}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    .line 352
    .end local v1    # "editor":Landroid/content/SharedPreferences$Editor;
    :goto_0
    :try_start_2
    monitor-exit v3

    .line 353
    return-void

    .line 349
    :catch_0
    move-exception v0

    .line 350
    .local v0, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v2, "Config - Error updating visitorID. (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    const/4 v5, 0x0

    invoke-virtual {v0}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v4, v5

    invoke-static {v2, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 352
    .end local v0    # "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    :catchall_0
    move-exception v2

    monitor-exit v3
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    throw v2
.end method

.method private static syncAIDIfNeeded(Ljava/lang/String;)V
    .locals 9
    .param p0, "aid"    # Ljava/lang/String;

    .prologue
    const/4 v8, 0x1

    const/4 v7, 0x0

    .line 706
    if-nez p0, :cond_1

    .line 734
    :cond_0
    :goto_0
    return-void

    .line 711
    :cond_1
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v4

    invoke-virtual {v4}, Lcom/adobe/mobile/MobileConfig;->getVisitorIdServiceEnabled()Z

    move-result v4

    if-eqz v4, :cond_0

    .line 715
    const/4 v2, 0x0

    .line 717
    .local v2, "idSynced":Z
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferences()Landroid/content/SharedPreferences;

    move-result-object v4

    const-string v5, "ADOBEMOBILE_STOREDDEFAULTS_AID_SYNCED"

    const/4 v6, 0x0

    invoke-interface {v4, v5, v6}, Landroid/content/SharedPreferences;->getBoolean(Ljava/lang/String;Z)Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_1

    move-result v2

    .line 722
    :goto_1
    if-nez v2, :cond_0

    .line 723
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    .line 724
    .local v3, "integrationsMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    const-string v4, "AVID"

    invoke-virtual {v3, v4, p0}, Ljava/util/HashMap;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 725
    invoke-static {}, Lcom/adobe/mobile/VisitorIDService;->sharedInstance()Lcom/adobe/mobile/VisitorIDService;

    move-result-object v4

    invoke-virtual {v4, v3}, Lcom/adobe/mobile/VisitorIDService;->idSync(Ljava/util/Map;)V

    .line 727
    :try_start_1
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v0

    .line 728
    .local v0, "e":Landroid/content/SharedPreferences$Editor;
    const-string v4, "ADOBEMOBILE_STOREDDEFAULTS_AID_SYNCED"

    const/4 v5, 0x1

    invoke-interface {v0, v4, v5}, Landroid/content/SharedPreferences$Editor;->putBoolean(Ljava/lang/String;Z)Landroid/content/SharedPreferences$Editor;

    .line 729
    invoke-interface {v0}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_1
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_1 .. :try_end_1} :catch_0

    goto :goto_0

    .line 730
    .end local v0    # "e":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v1

    .line 731
    .local v1, "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Visitor ID - Null context when attempting to persist visitor ID sync status (%s)"

    new-array v5, v8, [Ljava/lang/Object;

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0

    .line 718
    .end local v1    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    .end local v3    # "integrationsMap":Ljava/util/HashMap;, "Ljava/util/HashMap<Ljava/lang/String;Ljava/lang/String;>;"
    :catch_1
    move-exception v1

    .line 719
    .restart local v1    # "ex":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v4, "Visitor ID - Null context when attempting to determine visitor ID sync status (%s)"

    new-array v5, v8, [Ljava/lang/Object;

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v6

    aput-object v6, v5, v7

    invoke-static {v4, v5}, Lcom/adobe/mobile/StaticMethods;->logWarningFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_1
.end method

.method protected static translateContextData(Ljava/util/Map;)Lcom/adobe/mobile/ContextData;
    .locals 9
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Ljava/lang/Object;",
            ">;)",
            "Lcom/adobe/mobile/ContextData;"
        }
    .end annotation

    .prologue
    .line 990
    .local p0, "dict":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/Object;>;"
    new-instance v5, Lcom/adobe/mobile/ContextData;

    invoke-direct {v5}, Lcom/adobe/mobile/ContextData;-><init>()V

    .line 992
    .local v5, "tempContextData":Lcom/adobe/mobile/ContextData;
    invoke-static {p0}, Lcom/adobe/mobile/StaticMethods;->cleanContextDataDictionary(Ljava/util/Map;)Ljava/util/Map;

    move-result-object v6

    invoke-interface {v6}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v6

    invoke-interface {v6}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v6

    :goto_0
    invoke-interface {v6}, Ljava/util/Iterator;->hasNext()Z

    move-result v7

    if-eqz v7, :cond_1

    invoke-interface {v6}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/util/Map$Entry;

    .line 993
    .local v1, "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    invoke-interface {v1}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/lang/String;

    .line 995
    .local v2, "key":Ljava/lang/String;
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    .line 996
    .local v3, "list":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    const/4 v4, 0x0

    .line 997
    .local v4, "pos":I
    :goto_1
    const/16 v7, 0x2e

    invoke-virtual {v2, v7, v4}, Ljava/lang/String;->indexOf(II)I

    move-result v0

    .local v0, "end":I
    if-ltz v0, :cond_0

    .line 998
    invoke-virtual {v2, v4, v0}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v3, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 999
    add-int/lit8 v4, v0, 0x1

    goto :goto_1

    .line 1001
    :cond_0
    invoke-virtual {v2}, Ljava/lang/String;->length()I

    move-result v7

    invoke-virtual {v2, v4, v7}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v7

    invoke-interface {v3, v7}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 1003
    invoke-interface {v1}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v7

    const/4 v8, 0x0

    invoke-static {v7, v5, v3, v8}, Lcom/adobe/mobile/StaticMethods;->addValueToHashMap(Ljava/lang/Object;Lcom/adobe/mobile/ContextData;Ljava/util/List;I)V

    goto :goto_0

    .line 1006
    .end local v0    # "end":I
    .end local v1    # "entry":Ljava/util/Map$Entry;, "Ljava/util/Map$Entry<Ljava/lang/String;Ljava/lang/Object;>;"
    .end local v2    # "key":Ljava/lang/String;
    .end local v3    # "list":Ljava/util/List;, "Ljava/util/List<Ljava/lang/String;>;"
    .end local v4    # "pos":I
    :cond_1
    return-object v5
.end method

.method protected static updateLastKnownTimestamp(Ljava/lang/Long;)V
    .locals 7
    .param p0, "timestamp"    # Ljava/lang/Long;

    .prologue
    const/4 v6, 0x0

    .line 1145
    invoke-static {}, Lcom/adobe/mobile/MobileConfig;->getInstance()Lcom/adobe/mobile/MobileConfig;

    move-result-object v0

    .line 1146
    .local v0, "config":Lcom/adobe/mobile/MobileConfig;
    if-nez v0, :cond_1

    .line 1147
    const-string v3, "Config - Lost config instance"

    new-array v4, v6, [Ljava/lang/Object;

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    .line 1162
    :cond_0
    :goto_0
    return-void

    .line 1151
    :cond_1
    invoke-virtual {v0}, Lcom/adobe/mobile/MobileConfig;->getOfflineTrackingEnabled()Z

    move-result v3

    if-eqz v3, :cond_0

    .line 1156
    :try_start_0
    invoke-static {}, Lcom/adobe/mobile/StaticMethods;->getSharedPreferencesEditor()Landroid/content/SharedPreferences$Editor;

    move-result-object v2

    .line 1157
    .local v2, "editor":Landroid/content/SharedPreferences$Editor;
    const-string v3, "ADBLastKnownTimestampKey"

    invoke-virtual {p0}, Ljava/lang/Long;->longValue()J

    move-result-wide v4

    invoke-interface {v2, v3, v4, v5}, Landroid/content/SharedPreferences$Editor;->putLong(Ljava/lang/String;J)Landroid/content/SharedPreferences$Editor;

    .line 1158
    invoke-interface {v2}, Landroid/content/SharedPreferences$Editor;->commit()Z
    :try_end_0
    .catch Lcom/adobe/mobile/StaticMethods$NullContextException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    .line 1159
    .end local v2    # "editor":Landroid/content/SharedPreferences$Editor;
    :catch_0
    move-exception v1

    .line 1160
    .local v1, "e":Lcom/adobe/mobile/StaticMethods$NullContextException;
    const-string v3, "Config - Error while updating last known timestamp. (%s)"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Object;

    invoke-virtual {v1}, Lcom/adobe/mobile/StaticMethods$NullContextException;->getMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v6

    invoke-static {v3, v4}, Lcom/adobe/mobile/StaticMethods;->logErrorFormat(Ljava/lang/String;[Ljava/lang/Object;)V

    goto :goto_0
.end method
