.class public Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
.super Ljava/lang/Object;
.source "UnityBluetoothLE.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;,
        Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;,
        Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;,
        Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    }
.end annotation


# static fields
.field protected static final CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID:Ljava/util/UUID;

.field public static final TAG:Ljava/lang/String; = "UnityBluetoothLE"

.field public static _instance:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;


# instance fields
.field private final ActionFoundReceiver:Landroid/content/BroadcastReceiver;

.field private _noManufacturerData:Z

.field private _recordType:I

.field private advertisingCallback:Landroid/bluetooth/le/AdvertiseCallback;

.field private api21ScanCallback:Landroid/bluetooth/le/ScanCallback;

.field private characteristList:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList",
            "<",
            "Landroid/bluetooth/BluetoothGattCharacteristic;",
            ">;"
        }
    .end annotation
.end field

.field private deviceGattMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Landroid/bluetooth/BluetoothGatt;",
            ">;"
        }
    .end annotation
.end field

.field private deviceMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Landroid/bluetooth/BluetoothDevice;",
            ">;"
        }
    .end annotation
.end field

.field private mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

.field private mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

.field private mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

.field private mBluetoothLeScanner:Landroid/bluetooth/le/BluetoothLeScanner;

.field private mContext:Landroid/content/Context;

.field private final mGattCallback:Landroid/bluetooth/BluetoothGattCallback;

.field private mLeScanCallback:Landroid/bluetooth/BluetoothAdapter$LeScanCallback;

.field public mRunning:Ljava/util/concurrent/atomic/AtomicBoolean;

.field private mUnityGattServices:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;

.field private notificationDescriptionUUID:Ljava/lang/String;

.field private notifyDeviceMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/util/UUID;",
            "Ljava/util/ArrayList",
            "<",
            "Landroid/bluetooth/BluetoothDevice;",
            ">;>;"
        }
    .end annotation
.end field

.field private primaryServiceUUID:Ljava/lang/String;

.field private serviceList:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList",
            "<",
            "Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;",
            ">;"
        }
    .end annotation
.end field

.field private uuidList:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList",
            "<",
            "Ljava/util/UUID;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .prologue
    .line 866
    const-string v0, "00002902-0000-1000-8000-00805f9b34fb"

    invoke-static {v0}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v0

    sput-object v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID:Ljava/util/UUID;

    return-void
.end method

.method public constructor <init>()V
    .locals 2

    .prologue
    const/4 v1, 0x0

    .line 50
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 56
    new-instance v0, Ljava/util/concurrent/atomic/AtomicBoolean;

    invoke-direct {v0}, Ljava/util/concurrent/atomic/AtomicBoolean;-><init>()V

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mRunning:Ljava/util/concurrent/atomic/AtomicBoolean;

    .line 61
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->uuidList:Ljava/util/ArrayList;

    .line 62
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    .line 63
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    .line 65
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    .line 115
    new-instance v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;

    invoke-direct {v0, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->ActionFoundReceiver:Landroid/content/BroadcastReceiver;

    .line 365
    const/4 v0, 0x0

    iput-boolean v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_noManufacturerData:Z

    .line 366
    const/16 v0, 0xff

    iput v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_recordType:I

    .line 423
    new-instance v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$2;

    invoke-direct {v0, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$2;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mLeScanCallback:Landroid/bluetooth/BluetoothAdapter$LeScanCallback;

    .line 626
    new-instance v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$5;

    invoke-direct {v0, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$5;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mGattCallback:Landroid/bluetooth/BluetoothGattCallback;

    .line 1086
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->advertisingCallback:Landroid/bluetooth/le/AdvertiseCallback;

    .line 1087
    const-string v0, ""

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->primaryServiceUUID:Ljava/lang/String;

    .line 1088
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    .line 1089
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    .line 1090
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    .line 1091
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mUnityGattServices:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;

    .line 1092
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notifyDeviceMap:Ljava/util/Map;

    .line 1093
    const-string v0, "00002902-0000-1000-8000-00805f9b34fb"

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notificationDescriptionUUID:Ljava/lang/String;

    return-void
.end method

.method public static UnitySend(Ljava/lang/String;)V
    .locals 2
    .param p0, "message"    # Ljava/lang/String;

    .prologue
    .line 77
    const-string v0, "BluetoothLEReceiver"

    const-string v1, "OnBluetoothMessage"

    invoke-static {v0, v1, p0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 78
    return-void
.end method

.method public static UnitySend([BI)V
    .locals 3
    .param p0, "data"    # [B
    .param p1, "length"    # I

    .prologue
    const/4 v2, 0x0

    .line 82
    invoke-static {p0, v2, p1}, Ljava/util/Arrays;->copyOfRange([BII)[B

    move-result-object v1

    invoke-static {v1, v2}, Landroid/util/Base64;->encodeToString([BI)Ljava/lang/String;

    move-result-object v0

    .line 83
    .local v0, "message":Ljava/lang/String;
    const-string v1, "BluetoothLEReceiver"

    const-string v2, "OnBluetoothData"

    invoke-static {v1, v2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 84
    return-void
.end method

.method public static UnitySendPeripheral([BI)V
    .locals 3
    .param p0, "data"    # [B
    .param p1, "length"    # I

    .prologue
    const/4 v2, 0x0

    .line 88
    invoke-static {p0, v2, p1}, Ljava/util/Arrays;->copyOfRange([BII)[B

    move-result-object v1

    invoke-static {v1, v2}, Landroid/util/Base64;->encodeToString([BI)Ljava/lang/String;

    move-result-object v0

    .line 89
    .local v0, "message":Ljava/lang/String;
    const-string v1, "BluetoothLEReceiver"

    const-string v2, "OnPeripheralData"

    invoke-static {v1, v2, v0}, Lcom/unity3d/player/UnityPlayer;->UnitySendMessage(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    .line 90
    return-void
.end method

.method static synthetic access$000(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/ArrayList;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->uuidList:Ljava/util/ArrayList;

    return-object v0
.end method

.method static synthetic access$100(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/lang/String;)Ljava/util/UUID;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 50
    invoke-direct {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getFullBluetoothLEUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$1000(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/lang/String;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notificationDescriptionUUID:Ljava/lang/String;

    return-object v0
.end method

.method static synthetic access$1100(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/Map;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notifyDeviceMap:Ljava/util/Map;

    return-object v0
.end method

.method static synthetic access$1102(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/util/Map;)Ljava/util/Map;
    .locals 0
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .param p1, "x1"    # Ljava/util/Map;

    .prologue
    .line 50
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notifyDeviceMap:Ljava/util/Map;

    return-object p1
.end method

.method static synthetic access$200(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Landroid/bluetooth/BluetoothDevice;I[B)V
    .locals 0
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .param p1, "x1"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "x2"    # I
    .param p3, "x3"    # [B

    .prologue
    .line 50
    invoke-direct {p0, p1, p2, p3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->sendDiscoveredDevice(Landroid/bluetooth/BluetoothDevice;I[B)V

    return-void
.end method

.method static synthetic access$300(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothAdapter$LeScanCallback;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mLeScanCallback:Landroid/bluetooth/BluetoothAdapter$LeScanCallback;

    return-object v0
.end method

.method static synthetic access$400(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothAdapter;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    return-object v0
.end method

.method static synthetic access$500(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/Map;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    return-object v0
.end method

.method static synthetic access$502(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/util/Map;)Ljava/util/Map;
    .locals 0
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .param p1, "x1"    # Ljava/util/Map;

    .prologue
    .line 50
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    return-object p1
.end method

.method static synthetic access$600(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/content/Context;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    return-object v0
.end method

.method static synthetic access$700(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothGattCallback;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mGattCallback:Landroid/bluetooth/BluetoothGattCallback;

    return-object v0
.end method

.method static synthetic access$800(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/lang/String;)Landroid/bluetooth/BluetoothGattCharacteristic;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .param p1, "x1"    # Ljava/lang/String;

    .prologue
    .line 50
    invoke-direct {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->findCharacteristic(Ljava/lang/String;)Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v0

    return-object v0
.end method

.method static synthetic access$900(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothGattServer;
    .locals 1
    .param p0, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 50
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    return-object v0
.end method

.method private api21Scan(Ljava/util/List;)V
    .locals 6
    .annotation build Landroid/annotation/TargetApi;
        value = 0x17
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List",
            "<",
            "Ljava/util/UUID;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 470
    .local p1, "uuids":Ljava/util/List;, "Ljava/util/List<Ljava/util/UUID;>;"
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 472
    .local v2, "scanFilters":Ljava/util/List;, "Ljava/util/List<Landroid/bluetooth/le/ScanFilter;>;"
    if-eqz p1, :cond_0

    .line 473
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    invoke-interface {p1}, Ljava/util/List;->size()I

    move-result v4

    if-ge v0, v4, :cond_0

    .line 474
    new-instance v5, Landroid/bluetooth/le/ScanFilter$Builder;

    invoke-direct {v5}, Landroid/bluetooth/le/ScanFilter$Builder;-><init>()V

    invoke-interface {p1, v0}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Ljava/util/UUID;

    invoke-virtual {v4}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Landroid/os/ParcelUuid;->fromString(Ljava/lang/String;)Landroid/os/ParcelUuid;

    move-result-object v4

    invoke-virtual {v5, v4}, Landroid/bluetooth/le/ScanFilter$Builder;->setServiceUuid(Landroid/os/ParcelUuid;)Landroid/bluetooth/le/ScanFilter$Builder;

    move-result-object v4

    invoke-virtual {v4}, Landroid/bluetooth/le/ScanFilter$Builder;->build()Landroid/bluetooth/le/ScanFilter;

    move-result-object v4

    invoke-interface {v2, v4}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    .line 473
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 477
    .end local v0    # "i":I
    :cond_0
    new-instance v4, Landroid/bluetooth/le/ScanSettings$Builder;

    invoke-direct {v4}, Landroid/bluetooth/le/ScanSettings$Builder;-><init>()V

    const/4 v5, 0x1

    invoke-virtual {v4, v5}, Landroid/bluetooth/le/ScanSettings$Builder;->setCallbackType(I)Landroid/bluetooth/le/ScanSettings$Builder;

    move-result-object v4

    invoke-virtual {v4}, Landroid/bluetooth/le/ScanSettings$Builder;->build()Landroid/bluetooth/le/ScanSettings;

    move-result-object v3

    .line 479
    .local v3, "settings":Landroid/bluetooth/le/ScanSettings;
    new-instance v1, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$3;

    invoke-direct {v1, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$3;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    .line 496
    .local v1, "scanCallback":Landroid/bluetooth/le/ScanCallback;
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->api21ScanCallback:Landroid/bluetooth/le/ScanCallback;

    .line 498
    if-eqz p1, :cond_1

    .line 499
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v4}, Landroid/bluetooth/BluetoothAdapter;->getBluetoothLeScanner()Landroid/bluetooth/le/BluetoothLeScanner;

    move-result-object v4

    invoke-virtual {v4, v2, v3, v1}, Landroid/bluetooth/le/BluetoothLeScanner;->startScan(Ljava/util/List;Landroid/bluetooth/le/ScanSettings;Landroid/bluetooth/le/ScanCallback;)V

    .line 502
    :goto_1
    return-void

    .line 501
    :cond_1
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v4}, Landroid/bluetooth/BluetoothAdapter;->getBluetoothLeScanner()Landroid/bluetooth/le/BluetoothLeScanner;

    move-result-object v4

    const/4 v5, 0x0

    invoke-virtual {v4, v5, v3, v1}, Landroid/bluetooth/le/BluetoothLeScanner;->startScan(Ljava/util/List;Landroid/bluetooth/le/ScanSettings;Landroid/bluetooth/le/ScanCallback;)V

    goto :goto_1
.end method

.method private api21StopScan()V
    .locals 2
    .annotation build Landroid/annotation/TargetApi;
        value = 0x15
    .end annotation

    .prologue
    .line 564
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->api21ScanCallback:Landroid/bluetooth/le/ScanCallback;

    if-eqz v0, :cond_0

    .line 565
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v0}, Landroid/bluetooth/BluetoothAdapter;->getBluetoothLeScanner()Landroid/bluetooth/le/BluetoothLeScanner;

    move-result-object v0

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->api21ScanCallback:Landroid/bluetooth/le/ScanCallback;

    invoke-virtual {v0, v1}, Landroid/bluetooth/le/BluetoothLeScanner;->stopScan(Landroid/bluetooth/le/ScanCallback;)V

    .line 566
    :cond_0
    return-void
.end method

.method private findCharacteristic(Ljava/lang/String;)Landroid/bluetooth/BluetoothGattCharacteristic;
    .locals 3
    .param p1, "uuid"    # Ljava/lang/String;

    .prologue
    .line 1156
    const/4 v0, 0x0

    .line 1158
    .local v0, "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    if-eqz v2, :cond_0

    .line 1160
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 1162
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-ge v1, v2, :cond_0

    .line 1163
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v2, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    iget-object v2, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->characteristicMap:Ljava/util/Map;

    if-eqz v2, :cond_1

    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v2, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    iget-object v2, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->characteristicMap:Ljava/util/Map;

    invoke-interface {v2, p1}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_1

    .line 1164
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v2, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    iget-object v2, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->characteristicMap:Ljava/util/Map;

    invoke-interface {v2, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    .end local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    check-cast v0, Landroid/bluetooth/BluetoothGattCharacteristic;

    .line 1170
    .end local v1    # "i":I
    .restart local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    :cond_0
    return-object v0

    .line 1162
    .restart local v1    # "i":I
    :cond_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0
.end method

.method private findService(Ljava/lang/String;)Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    .locals 3
    .param p1, "uuid"    # Ljava/lang/String;

    .prologue
    .line 1137
    const/4 v1, 0x0

    .line 1139
    .local v1, "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    if-eqz v2, :cond_0

    .line 1141
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 1143
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-ge v0, v2, :cond_0

    .line 1144
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    iget-object v2, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->UUID:Ljava/lang/String;

    invoke-virtual {v2, p1}, Ljava/lang/String;->compareTo(Ljava/lang/String;)I

    move-result v2

    if-nez v2, :cond_1

    .line 1145
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    .end local v1    # "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    check-cast v1, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    .line 1151
    .end local v0    # "i":I
    .restart local v1    # "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    :cond_0
    return-object v1

    .line 1143
    .restart local v0    # "i":I
    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0
.end method

.method private getFullBluetoothLEUUID(Ljava/lang/String;)Ljava/util/UUID;
    .locals 3
    .param p1, "uuidString"    # Ljava/lang/String;

    .prologue
    .line 94
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "getFullBluetoothLEUUID : "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p0, v1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 98
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v1

    const/4 v2, 0x4

    if-ne v1, v2, :cond_0

    .line 99
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "0000"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "-0000-1000-8000-00805F9B34FB"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v0

    .line 103
    .local v0, "uuid":Ljava/util/UUID;
    :goto_0
    return-object v0

    .line 101
    .end local v0    # "uuid":Ljava/util/UUID;
    :cond_0
    invoke-static {p1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v0

    .restart local v0    # "uuid":Ljava/util/UUID;
    goto :goto_0
.end method

.method public static getInstance()Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .locals 1

    .prologue
    .line 69
    sget-object v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_instance:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    if-nez v0, :cond_0

    .line 70
    new-instance v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;-><init>()V

    sput-object v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_instance:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .line 72
    :cond_0
    sget-object v0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_instance:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    return-object v0
.end method

.method private getUUIDFromString(Ljava/lang/String;)Ljava/util/UUID;
    .locals 7
    .param p1, "uuidString"    # Ljava/lang/String;

    .prologue
    const/16 v6, 0x10

    .line 107
    const-string v2, "-"

    const-string v3, ""

    invoke-virtual {p1, v2, v3}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v0

    .line 108
    .local v0, "s2":Ljava/lang/String;
    new-instance v1, Ljava/util/UUID;

    new-instance v2, Ljava/math/BigInteger;

    const/4 v3, 0x0

    .line 109
    invoke-virtual {v0, v3, v6}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3, v6}, Ljava/math/BigInteger;-><init>(Ljava/lang/String;I)V

    invoke-virtual {v2}, Ljava/math/BigInteger;->longValue()J

    move-result-wide v2

    new-instance v4, Ljava/math/BigInteger;

    .line 110
    invoke-virtual {v0, v6}, Ljava/lang/String;->substring(I)Ljava/lang/String;

    move-result-object v5

    invoke-direct {v4, v5, v6}, Ljava/math/BigInteger;-><init>(Ljava/lang/String;I)V

    invoke-virtual {v4}, Ljava/math/BigInteger;->longValue()J

    move-result-wide v4

    invoke-direct {v1, v2, v3, v4, v5}, Ljava/util/UUID;-><init>(JJ)V

    .line 112
    .local v1, "uuid":Ljava/util/UUID;
    return-object v1
.end method

.method private sendDiscoveredDevice(Landroid/bluetooth/BluetoothDevice;I[B)V
    .locals 7
    .param p1, "device"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "rssi"    # I
    .param p3, "scanRecord"    # [B

    .prologue
    const/4 v6, 0x0

    .line 370
    const-string v1, "No Name"

    .line 371
    .local v1, "name":Ljava/lang/String;
    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getName()Ljava/lang/String;

    move-result-object v4

    if-eqz v4, :cond_0

    .line 372
    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getName()Ljava/lang/String;

    move-result-object v1

    .line 374
    :cond_0
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    if-nez v4, :cond_1

    .line 375
    new-instance v4, Ljava/util/HashMap;

    invoke-direct {v4}, Ljava/util/HashMap;-><init>()V

    iput-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    .line 377
    :cond_1
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v5

    invoke-interface {v4, v5, p1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 379
    iget-boolean v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_noManufacturerData:Z

    if-eqz v4, :cond_6

    .line 380
    const/4 v0, 0x0

    .line 382
    .local v0, "manufacturerBytes":[B
    if-eqz p3, :cond_2

    .line 384
    new-instance v3, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;

    invoke-direct {v3, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    .line 385
    .local v3, "recordList":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;
    invoke-virtual {v3, p3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->parseScanRecord([B)Ljava/util/List;

    .line 386
    iget v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_recordType:I

    invoke-virtual {v3, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->getRecord(I)Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;

    move-result-object v2

    .line 388
    .local v2, "record":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    if-eqz v2, :cond_2

    .line 389
    iget-object v0, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->Data:[B

    .line 392
    .end local v2    # "record":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    .end local v3    # "recordList":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;
    :cond_2
    if-nez v0, :cond_3

    if-eqz p2, :cond_5

    .line 393
    :cond_3
    if-nez v0, :cond_4

    .line 394
    const/4 v4, 0x1

    new-array v0, v4, [B

    .end local v0    # "manufacturerBytes":[B
    aput-byte v6, v0, v6

    .line 396
    .restart local v0    # "manufacturerBytes":[B
    :cond_4
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "DiscoveredPeripheral~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-static {v0, v6}, Landroid/util/Base64;->encodeToString([BI)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 418
    .end local v0    # "manufacturerBytes":[B
    :goto_0
    return-void

    .line 399
    .restart local v0    # "manufacturerBytes":[B
    :cond_5
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "DiscoveredPeripheral~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 402
    .end local v0    # "manufacturerBytes":[B
    :cond_6
    if-eqz p3, :cond_8

    .line 404
    new-instance v3, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;

    invoke-direct {v3, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    .line 405
    .restart local v3    # "recordList":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;
    invoke-virtual {v3, p3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->parseScanRecord([B)Ljava/util/List;

    .line 406
    const/4 v4, -0x1

    invoke-virtual {v3, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;->getRecord(I)Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;

    move-result-object v2

    .line 408
    .restart local v2    # "record":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    if-eqz v2, :cond_7

    .line 409
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "DiscoveredPeripheral~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    iget-object v5, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;->Data:[B

    invoke-static {v5, v6}, Landroid/util/Base64;->encodeToString([BI)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 411
    :cond_7
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "DiscoveredPeripheral~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto/16 :goto_0

    .line 415
    .end local v2    # "record":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecord;
    .end local v3    # "recordList":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$AdRecordList;
    :cond_8
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "DiscoveredPeripheral~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {p1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    const-string v5, "~"

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-static {v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto/16 :goto_0
.end method


# virtual methods
.method public androidBluetoothConnectToPeripheral(Ljava/lang/String;)V
    .locals 3
    .param p1, "name"    # Ljava/lang/String;

    .prologue
    .line 741
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    if-eqz v1, :cond_0

    .line 743
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    invoke-interface {v1, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/bluetooth/BluetoothDevice;

    .line 744
    .local v0, "device":Landroid/bluetooth/BluetoothDevice;
    if-eqz v0, :cond_0

    .line 746
    sget-object v1, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;

    invoke-direct {v2, p0, v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$6;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Landroid/bluetooth/BluetoothDevice;)V

    invoke-virtual {v1, v2}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 756
    .end local v0    # "device":Landroid/bluetooth/BluetoothDevice;
    :cond_0
    return-void
.end method

.method public androidBluetoothDeInitialize()V
    .locals 2

    .prologue
    .line 263
    const-string v0, "UnityBluetoothLE"

    const-string v1, "androidBluetoothDeInitialize"

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 265
    invoke-virtual {p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->onDestroy()V

    .line 266
    const-string v0, "DeInitialized"

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 267
    return-void
.end method

.method public androidBluetoothDisconnectAll()V
    .locals 4

    .prologue
    .line 948
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v2, :cond_1

    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    if-eqz v2, :cond_1

    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    if-eqz v2, :cond_1

    .line 950
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    invoke-interface {v2}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v2

    invoke-interface {v2}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v2

    :cond_0
    :goto_0
    invoke-interface {v2}, Ljava/util/Iterator;->hasNext()Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-interface {v2}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 951
    .local v1, "key":Ljava/lang/String;
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    invoke-interface {v3, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/bluetooth/BluetoothGatt;

    .line 952
    .local v0, "gatt":Landroid/bluetooth/BluetoothGatt;
    if-eqz v0, :cond_0

    .line 953
    invoke-virtual {v0}, Landroid/bluetooth/BluetoothGatt;->disconnect()V

    goto :goto_0

    .line 956
    .end local v0    # "gatt":Landroid/bluetooth/BluetoothGatt;
    .end local v1    # "key":Ljava/lang/String;
    :cond_1
    return-void
.end method

.method public androidBluetoothDisconnectPeripheral(Ljava/lang/String;)V
    .locals 2
    .param p1, "name"    # Ljava/lang/String;

    .prologue
    .line 760
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    if-eqz v1, :cond_0

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    if-eqz v1, :cond_0

    .line 762
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    invoke-interface {v1, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/bluetooth/BluetoothGatt;

    .line 763
    .local v0, "gatt":Landroid/bluetooth/BluetoothGatt;
    if-eqz v0, :cond_0

    .line 764
    invoke-virtual {v0}, Landroid/bluetooth/BluetoothGatt;->disconnect()V

    .line 766
    .end local v0    # "gatt":Landroid/bluetooth/BluetoothGatt;
    :cond_0
    return-void
.end method

.method public androidBluetoothEnable(Z)V
    .locals 1
    .param p1, "enable"    # Z

    .prologue
    .line 271
    const-string v0, "androidBluetoothEnable"

    invoke-virtual {p0, v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 273
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-nez v0, :cond_0

    .line 275
    const-string v0, "Error~Plugin Initialize not called."

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 284
    :goto_0
    return-void

    .line 279
    :cond_0
    if-eqz p1, :cond_1

    .line 280
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v0}, Landroid/bluetooth/BluetoothAdapter;->enable()Z

    goto :goto_0

    .line 282
    :cond_1
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v0}, Landroid/bluetooth/BluetoothAdapter;->disable()Z

    goto :goto_0
.end method

.method public androidBluetoothInitialize(ZZ)V
    .locals 6
    .param p1, "asCentral"    # Z
    .param p2, "asPeripheral"    # Z

    .prologue
    const/4 v5, 0x0

    const/16 v4, 0x15

    .line 195
    const-string v2, "UnityBluetoothLE"

    const-string v3, "androidBluetoothInitialize"

    invoke-static {v2, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 198
    sget-object v2, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    invoke-virtual {v2}, Landroid/app/Activity;->getApplicationContext()Landroid/content/Context;

    move-result-object v2

    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    .line 202
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    invoke-virtual {v2}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    const-string v3, "android.hardware.bluetooth_le"

    invoke-virtual {v2, v3}, Landroid/content/pm/PackageManager;->hasSystemFeature(Ljava/lang/String;)Z

    move-result v2

    if-nez v2, :cond_0

    .line 203
    const-string v2, "Error~Bluetooth Low Energy Not Available"

    invoke-static {v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 254
    :goto_0
    return-void

    .line 207
    :cond_0
    iput-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->uuidList:Ljava/util/ArrayList;

    .line 208
    iput-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    .line 209
    iput-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    .line 212
    new-instance v1, Landroid/content/IntentFilter;

    const-string v2, "android.bluetooth.device.action.FOUND"

    invoke-direct {v1, v2}, Landroid/content/IntentFilter;-><init>(Ljava/lang/String;)V

    .line 213
    .local v1, "filter":Landroid/content/IntentFilter;
    const-string v2, "android.bluetooth.device.action.UUID"

    invoke-virtual {v1, v2}, Landroid/content/IntentFilter;->addAction(Ljava/lang/String;)V

    .line 214
    const-string v2, "android.bluetooth.adapter.action.DISCOVERY_STARTED"

    invoke-virtual {v1, v2}, Landroid/content/IntentFilter;->addAction(Ljava/lang/String;)V

    .line 215
    const-string v2, "android.bluetooth.adapter.action.DISCOVERY_FINISHED"

    invoke-virtual {v1, v2}, Landroid/content/IntentFilter;->addAction(Ljava/lang/String;)V

    .line 216
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->ActionFoundReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {v2, v3, v1}, Landroid/content/Context;->registerReceiver(Landroid/content/BroadcastReceiver;Landroid/content/IntentFilter;)Landroid/content/Intent;

    .line 218
    invoke-static {}, Landroid/bluetooth/BluetoothAdapter;->getDefaultAdapter()Landroid/bluetooth/BluetoothAdapter;

    move-result-object v2

    if-eqz v2, :cond_8

    .line 220
    if-eqz p2, :cond_1

    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v2, v4, :cond_2

    :cond_1
    if-eqz p2, :cond_3

    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v2, v4, :cond_3

    .line 221
    invoke-static {}, Landroid/bluetooth/BluetoothAdapter;->getDefaultAdapter()Landroid/bluetooth/BluetoothAdapter;

    move-result-object v2

    invoke-virtual {v2}, Landroid/bluetooth/BluetoothAdapter;->getBluetoothLeAdvertiser()Landroid/bluetooth/le/BluetoothLeAdvertiser;

    move-result-object v2

    if-nez v2, :cond_3

    .line 223
    :cond_2
    const-string v2, "Error~Peripheral mode is Not Available"

    invoke-static {v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 226
    :cond_3
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    const-string v3, "bluetooth"

    .line 227
    invoke-virtual {v2, v3}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/bluetooth/BluetoothManager;

    .line 229
    .local v0, "bluetoothManager":Landroid/bluetooth/BluetoothManager;
    if-eqz v0, :cond_7

    .line 231
    invoke-virtual {v0}, Landroid/bluetooth/BluetoothManager;->getAdapter()Landroid/bluetooth/BluetoothAdapter;

    move-result-object v2

    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    .line 233
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v2, :cond_6

    .line 235
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v2}, Landroid/bluetooth/BluetoothAdapter;->isEnabled()Z

    move-result v2

    if-eqz v2, :cond_5

    .line 237
    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v2, v4, :cond_4

    .line 239
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v2}, Landroid/bluetooth/BluetoothAdapter;->getBluetoothLeScanner()Landroid/bluetooth/le/BluetoothLeScanner;

    move-result-object v2

    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothLeScanner:Landroid/bluetooth/le/BluetoothLeScanner;

    .line 242
    :cond_4
    const-string v2, "Initialized"

    invoke-static {v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 244
    :cond_5
    const-string v2, "Error~Bluetooth LE Not Enabled"

    invoke-static {v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 246
    :cond_6
    invoke-virtual {p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->onDestroy()V

    goto :goto_0

    .line 248
    :cond_7
    invoke-virtual {p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->onDestroy()V

    goto/16 :goto_0

    .line 252
    .end local v0    # "bluetoothManager":Landroid/bluetooth/BluetoothManager;
    :cond_8
    const-string v2, "Error~Bluetooth LE Not Available"

    invoke-static {v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto/16 :goto_0
.end method

.method public androidBluetoothLog(Ljava/lang/String;)V
    .locals 1
    .param p1, "message"    # Ljava/lang/String;

    .prologue
    .line 258
    const-string v0, "UnityBluetoothLE"

    invoke-static {v0, p1}, Landroid/util/Log;->i(Ljava/lang/String;Ljava/lang/String;)I

    .line 259
    return-void
.end method

.method public androidBluetoothPause(Z)V
    .locals 0
    .param p1, "paused"    # Z

    .prologue
    .line 462
    return-void
.end method

.method public androidBluetoothRetrieveListOfPeripheralsWithServices(Ljava/lang/String;)V
    .locals 4
    .param p1, "serviceUUIDsString"    # Ljava/lang/String;

    .prologue
    .line 581
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v2, :cond_2

    .line 583
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->uuidList:Ljava/util/ArrayList;

    .line 585
    const-string v2, "|"

    invoke-virtual {p1, v2}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 587
    const-string v2, "|"

    invoke-virtual {p1, v2}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v1

    .line 588
    .local v1, "serviceUUIDs":[Ljava/lang/String;
    if-eqz v1, :cond_1

    array-length v2, v1

    if-lez v2, :cond_1

    .line 590
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    array-length v2, v1

    if-ge v0, v2, :cond_1

    .line 591
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->uuidList:Ljava/util/ArrayList;

    aget-object v3, v1, v0

    invoke-direct {p0, v3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getFullBluetoothLEUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 590
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 594
    .end local v0    # "i":I
    .end local v1    # "serviceUUIDs":[Ljava/lang/String;
    :cond_0
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v2

    if-lez v2, :cond_1

    .line 595
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->uuidList:Ljava/util/ArrayList;

    invoke-direct {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getFullBluetoothLEUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 597
    :cond_1
    const-string v2, "getting bonded devices"

    invoke-virtual {p0, v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 599
    sget-object v2, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    new-instance v3, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;

    invoke-direct {v3, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$4;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    invoke-virtual {v2, v3}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 623
    :cond_2
    return-void
.end method

.method public androidBluetoothScanForPeripheralsWithServices(Ljava/lang/String;ZI)V
    .locals 7
    .param p1, "serviceUUIDsString"    # Ljava/lang/String;
    .param p2, "rssiOnly"    # Z
    .param p3, "recordType"    # I

    .prologue
    const/16 v6, 0x15

    .line 506
    iput-boolean p2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_noManufacturerData:Z

    .line 507
    iput p3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->_recordType:I

    .line 509
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v4, :cond_4

    .line 511
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    .line 513
    .local v2, "uuidList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/util/UUID;>;"
    if-eqz p1, :cond_2

    .line 514
    const-string v4, "|"

    invoke-virtual {p1, v4}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v4

    if-eqz v4, :cond_1

    .line 515
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "serviceUUIDsString : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 517
    const-string v4, "\\|"

    invoke-virtual {p1, v4}, Ljava/lang/String;->split(Ljava/lang/String;)[Ljava/lang/String;

    move-result-object v1

    .line 518
    .local v1, "serviceUUIDs":[Ljava/lang/String;
    if-eqz v1, :cond_2

    array-length v4, v1

    if-lez v4, :cond_2

    .line 520
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    array-length v4, v1

    if-ge v0, v4, :cond_2

    .line 521
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "serviceUUID : "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    aget-object v5, v1, v0

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 522
    aget-object v4, v1, v0

    if-eqz v4, :cond_0

    aget-object v4, v1, v0

    invoke-virtual {v4}, Ljava/lang/String;->length()I

    move-result v4

    const/4 v5, 0x4

    if-lt v4, v5, :cond_0

    .line 523
    aget-object v4, v1, v0

    invoke-direct {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getFullBluetoothLEUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    invoke-virtual {v2, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 520
    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 527
    .end local v0    # "i":I
    .end local v1    # "serviceUUIDs":[Ljava/lang/String;
    :cond_1
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v4

    if-lez v4, :cond_2

    .line 528
    invoke-direct {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getFullBluetoothLEUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    invoke-virtual {v2, v4}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 531
    :cond_2
    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v4

    if-lez v4, :cond_6

    .line 533
    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v4

    new-array v3, v4, [Ljava/util/UUID;

    .line 534
    .local v3, "uuids":[Ljava/util/UUID;
    invoke-virtual {v2, v3}, Ljava/util/ArrayList;->toArray([Ljava/lang/Object;)[Ljava/lang/Object;

    move-result-object v3

    .end local v3    # "uuids":[Ljava/util/UUID;
    check-cast v3, [Ljava/util/UUID;

    .line 536
    .restart local v3    # "uuids":[Ljava/util/UUID;
    const/4 v0, 0x0

    .restart local v0    # "i":I
    :goto_1
    array-length v4, v3

    if-ge v0, v4, :cond_3

    .line 537
    new-instance v4, Ljava/lang/StringBuilder;

    invoke-direct {v4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v5, "Scanning for: "

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    aget-object v5, v3, v0

    invoke-virtual {v5}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v4, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v4

    invoke-virtual {v4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 536
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 539
    :cond_3
    sget v4, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v4, v6, :cond_5

    .line 540
    const-string v4, "Using api21scan"

    invoke-virtual {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 541
    invoke-direct {p0, v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->api21Scan(Ljava/util/List;)V

    .line 560
    .end local v0    # "i":I
    .end local v2    # "uuidList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/util/UUID;>;"
    .end local v3    # "uuids":[Ljava/util/UUID;
    :cond_4
    :goto_2
    return-void

    .line 544
    .restart local v0    # "i":I
    .restart local v2    # "uuidList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Ljava/util/UUID;>;"
    .restart local v3    # "uuids":[Ljava/util/UUID;
    :cond_5
    const-string v4, "Using legacy scan"

    invoke-virtual {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 545
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mLeScanCallback:Landroid/bluetooth/BluetoothAdapter$LeScanCallback;

    invoke-virtual {v4, v3, v5}, Landroid/bluetooth/BluetoothAdapter;->startLeScan([Ljava/util/UUID;Landroid/bluetooth/BluetoothAdapter$LeScanCallback;)Z

    goto :goto_2

    .line 550
    .end local v0    # "i":I
    .end local v3    # "uuids":[Ljava/util/UUID;
    :cond_6
    sget v4, Landroid/os/Build$VERSION;->SDK_INT:I

    if-lt v4, v6, :cond_7

    .line 551
    const-string v4, "Using api21scan"

    invoke-virtual {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 552
    invoke-direct {p0, v2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->api21Scan(Ljava/util/List;)V

    goto :goto_2

    .line 555
    :cond_7
    const-string v4, "Using legacy scan"

    invoke-virtual {p0, v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 556
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mLeScanCallback:Landroid/bluetooth/BluetoothAdapter$LeScanCallback;

    invoke-virtual {v4, v5}, Landroid/bluetooth/BluetoothAdapter;->startLeScan(Landroid/bluetooth/BluetoothAdapter$LeScanCallback;)Z

    goto :goto_2
.end method

.method public androidBluetoothStopScan()V
    .locals 2

    .prologue
    .line 570
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v0, :cond_0

    .line 572
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_1

    .line 573
    invoke-direct {p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->api21StopScan()V

    .line 577
    :cond_0
    :goto_0
    return-void

    .line 575
    :cond_1
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mLeScanCallback:Landroid/bluetooth/BluetoothAdapter$LeScanCallback;

    invoke-virtual {v0, v1}, Landroid/bluetooth/BluetoothAdapter;->stopLeScan(Landroid/bluetooth/BluetoothAdapter$LeScanCallback;)V

    goto :goto_0
.end method

.method public androidCreateCharacteristic(Ljava/lang/String;II[BI)V
    .locals 4
    .param p1, "uuid"    # Ljava/lang/String;
    .param p2, "properties"    # I
    .param p3, "permissions"    # I
    .param p4, "data"    # [B
    .param p5, "length"    # I

    .prologue
    .line 1190
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 1192
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    if-nez v2, :cond_0

    .line 1193
    new-instance v2, Ljava/util/ArrayList;

    invoke-direct {v2}, Ljava/util/ArrayList;-><init>()V

    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    .line 1197
    :cond_0
    invoke-virtual {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidRemoveCharacteristic(Ljava/lang/String;)V

    .line 1199
    new-instance v0, Landroid/bluetooth/BluetoothGattCharacteristic;

    .line 1200
    invoke-static {p1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v2

    invoke-direct {v0, v2, p2, p3}, Landroid/bluetooth/BluetoothGattCharacteristic;-><init>(Ljava/util/UUID;II)V

    .line 1204
    .local v0, "gattCharacteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    and-int/lit8 v2, p3, 0x10

    const/16 v3, 0x10

    if-ne v2, v3, :cond_1

    .line 1206
    new-instance v1, Landroid/bluetooth/BluetoothGattDescriptor;

    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notificationDescriptionUUID:Ljava/lang/String;

    invoke-static {v2}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v2

    const/16 v3, 0x11

    invoke-direct {v1, v2, v3}, Landroid/bluetooth/BluetoothGattDescriptor;-><init>(Ljava/util/UUID;I)V

    .line 1207
    .local v1, "gattDescriptor":Landroid/bluetooth/BluetoothGattDescriptor;
    invoke-virtual {v0, v1}, Landroid/bluetooth/BluetoothGattCharacteristic;->addDescriptor(Landroid/bluetooth/BluetoothGattDescriptor;)Z

    .line 1210
    .end local v1    # "gattDescriptor":Landroid/bluetooth/BluetoothGattDescriptor;
    :cond_1
    invoke-virtual {v0, p4}, Landroid/bluetooth/BluetoothGattCharacteristic;->setValue([B)Z

    .line 1212
    iget-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1213
    return-void
.end method

.method public androidCreateService(Ljava/lang/String;Z)V
    .locals 6
    .param p1, "uuid"    # Ljava/lang/String;
    .param p2, "primary"    # Z

    .prologue
    .line 1101
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 1103
    new-instance v1, Landroid/bluetooth/BluetoothGattService;

    .line 1104
    invoke-static {p1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    if-eqz p2, :cond_2

    const/4 v3, 0x0

    :goto_0
    invoke-direct {v1, v4, v3}, Landroid/bluetooth/BluetoothGattService;-><init>(Ljava/util/UUID;I)V

    .line 1107
    .local v1, "service":Landroid/bluetooth/BluetoothGattService;
    if-eqz p2, :cond_0

    .line 1108
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->primaryServiceUUID:Ljava/lang/String;

    .line 1110
    :cond_0
    if-eqz v1, :cond_4

    .line 1112
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    if-nez v3, :cond_1

    .line 1113
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    iput-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    .line 1115
    :cond_1
    new-instance v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    const/4 v3, 0x0

    invoke-direct {v2, p0, v3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;)V

    .line 1116
    .local v2, "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    iput-object p1, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->UUID:Ljava/lang/String;

    .line 1117
    iput-object v1, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->Service:Landroid/bluetooth/BluetoothGattService;

    .line 1119
    new-instance v3, Ljava/util/HashMap;

    invoke-direct {v3}, Ljava/util/HashMap;-><init>()V

    iput-object v3, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->characteristicMap:Ljava/util/Map;

    .line 1120
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_1
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v3}, Ljava/util/ArrayList;->size()I

    move-result v3

    if-ge v0, v3, :cond_3

    .line 1121
    iget-object v4, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->characteristicMap:Ljava/util/Map;

    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v3, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/bluetooth/BluetoothGattCharacteristic;

    invoke-virtual {v3}, Landroid/bluetooth/BluetoothGattCharacteristic;->getUuid()Ljava/util/UUID;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v3

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v5, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v5

    invoke-interface {v4, v3, v5}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1123
    iget-object v4, v2, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->Service:Landroid/bluetooth/BluetoothGattService;

    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v3, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/bluetooth/BluetoothGattCharacteristic;

    invoke-virtual {v4, v3}, Landroid/bluetooth/BluetoothGattService;->addCharacteristic(Landroid/bluetooth/BluetoothGattCharacteristic;)Z

    .line 1120
    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    .line 1104
    .end local v0    # "i":I
    .end local v1    # "service":Landroid/bluetooth/BluetoothGattService;
    .end local v2    # "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    :cond_2
    const/4 v3, 0x1

    goto :goto_0

    .line 1126
    .restart local v0    # "i":I
    .restart local v1    # "service":Landroid/bluetooth/BluetoothGattService;
    .restart local v2    # "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    :cond_3
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v3, v2}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1128
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v3}, Ljava/util/ArrayList;->clear()V

    .line 1133
    .end local v0    # "i":I
    .end local v2    # "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    :goto_2
    return-void

    .line 1131
    :cond_4
    const-string v3, "Error~Cannot create Gatt Service object while creating a service"

    invoke-static {v3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_2
.end method

.method public androidPeripheralName(Ljava/lang/String;)V
    .locals 1
    .param p1, "newName"    # Ljava/lang/String;

    .prologue
    .line 1096
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v0, p1}, Landroid/bluetooth/BluetoothAdapter;->setName(Ljava/lang/String;)Z

    .line 1097
    return-void
.end method

.method public androidReadCharacteristic(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 6
    .param p1, "name"    # Ljava/lang/String;
    .param p2, "serviceString"    # Ljava/lang/String;
    .param p3, "characteristicString"    # Ljava/lang/String;

    .prologue
    .line 791
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v5, :cond_0

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    if-eqz v5, :cond_0

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    if-eqz v5, :cond_0

    .line 793
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    invoke-interface {v5, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/bluetooth/BluetoothGatt;

    .line 794
    .local v2, "gatt":Landroid/bluetooth/BluetoothGatt;
    if-eqz v2, :cond_0

    .line 796
    invoke-virtual {p0, p2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    .line 797
    .local v4, "serviceUUID":Ljava/util/UUID;
    if-eqz v4, :cond_4

    .line 799
    invoke-virtual {v2, v4}, Landroid/bluetooth/BluetoothGatt;->getService(Ljava/util/UUID;)Landroid/bluetooth/BluetoothGattService;

    move-result-object v3

    .line 800
    .local v3, "service":Landroid/bluetooth/BluetoothGattService;
    if-eqz v3, :cond_3

    .line 802
    invoke-virtual {p0, p3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v1

    .line 803
    .local v1, "characteristicUUID":Ljava/util/UUID;
    if-eqz v1, :cond_2

    .line 805
    invoke-virtual {v3, v1}, Landroid/bluetooth/BluetoothGattService;->getCharacteristic(Ljava/util/UUID;)Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v0

    .line 806
    .local v0, "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    if-eqz v0, :cond_1

    .line 808
    invoke-virtual {v2, v0}, Landroid/bluetooth/BluetoothGatt;->readCharacteristic(Landroid/bluetooth/BluetoothGattCharacteristic;)Z

    move-result v5

    if-nez v5, :cond_0

    .line 809
    const-string v5, "Error~Failed to read characteristic"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 824
    .end local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    .end local v1    # "characteristicUUID":Ljava/util/UUID;
    .end local v2    # "gatt":Landroid/bluetooth/BluetoothGatt;
    .end local v3    # "service":Landroid/bluetooth/BluetoothGattService;
    .end local v4    # "serviceUUID":Ljava/util/UUID;
    :cond_0
    :goto_0
    return-void

    .line 812
    .restart local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    .restart local v1    # "characteristicUUID":Ljava/util/UUID;
    .restart local v2    # "gatt":Landroid/bluetooth/BluetoothGatt;
    .restart local v3    # "service":Landroid/bluetooth/BluetoothGattService;
    .restart local v4    # "serviceUUID":Ljava/util/UUID;
    :cond_1
    const-string v5, "Error~Characteristic not found for Read"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 815
    .end local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    :cond_2
    const-string v5, "Error~Not a Valid Characteristic UUID for Read"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 818
    .end local v1    # "characteristicUUID":Ljava/util/UUID;
    :cond_3
    const-string v5, "Error~Service not found for Read"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0

    .line 821
    .end local v3    # "service":Landroid/bluetooth/BluetoothGattService;
    :cond_4
    const-string v5, "Error~Not a Valid Service UUID for Read"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0
.end method

.method public androidRemoveCharacteristic(Ljava/lang/String;)V
    .locals 3
    .param p1, "uuid"    # Ljava/lang/String;

    .prologue
    .line 1217
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    if-eqz v1, :cond_0

    .line 1219
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 1221
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-ge v0, v1, :cond_0

    .line 1223
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/bluetooth/BluetoothGattCharacteristic;

    invoke-virtual {v1}, Landroid/bluetooth/BluetoothGattCharacteristic;->getUuid()Ljava/util/UUID;

    move-result-object v1

    invoke-static {p1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/util/UUID;->compareTo(Ljava/util/UUID;)I

    move-result v1

    if-nez v1, :cond_1

    .line 1224
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    invoke-virtual {v1, v0}, Ljava/util/ArrayList;->remove(I)Ljava/lang/Object;

    .line 1229
    .end local v0    # "i":I
    :cond_0
    return-void

    .line 1221
    .restart local v0    # "i":I
    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0
.end method

.method public androidRemoveCharacteristics()V
    .locals 1

    .prologue
    .line 1233
    const/4 v0, 0x0

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->characteristList:Ljava/util/ArrayList;

    .line 1234
    return-void
.end method

.method public androidRemoveService(Ljava/lang/String;)V
    .locals 2
    .param p1, "uuid"    # Ljava/lang/String;

    .prologue
    .line 1175
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    if-eqz v1, :cond_0

    .line 1177
    invoke-direct {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->findService(Ljava/lang/String;)Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    move-result-object v0

    .line 1178
    .local v0, "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    if-eqz v0, :cond_0

    .line 1179
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v1, v0}, Ljava/util/ArrayList;->remove(Ljava/lang/Object;)Z

    .line 1181
    .end local v0    # "serviceCharacteristicMap":Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
    :cond_0
    return-void
.end method

.method public androidRemoveServices()V
    .locals 1

    .prologue
    .line 1185
    const/4 v0, 0x0

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    .line 1186
    return-void
.end method

.method protected androidSetCharacteristicNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V
    .locals 10
    .param p1, "name"    # Ljava/lang/String;
    .param p2, "serviceString"    # Ljava/lang/String;
    .param p3, "characteristicString"    # Ljava/lang/String;
    .param p4, "enabled"    # Z

    .prologue
    .line 870
    iget-object v8, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v8, :cond_1

    iget-object v8, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    if-eqz v8, :cond_1

    iget-object v8, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    if-eqz v8, :cond_1

    .line 872
    iget-object v8, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    invoke-interface {v8, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v4

    check-cast v4, Landroid/bluetooth/BluetoothGatt;

    .line 873
    .local v4, "gatt":Landroid/bluetooth/BluetoothGatt;
    if-eqz v4, :cond_1

    .line 875
    invoke-virtual {p0, p2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v6

    .line 876
    .local v6, "serviceUUID":Ljava/util/UUID;
    if-eqz v6, :cond_a

    .line 878
    invoke-virtual {v4, v6}, Landroid/bluetooth/BluetoothGatt;->getService(Ljava/util/UUID;)Landroid/bluetooth/BluetoothGattService;

    move-result-object v5

    .line 879
    .local v5, "service":Landroid/bluetooth/BluetoothGattService;
    if-eqz v5, :cond_9

    .line 881
    invoke-virtual {p0, p3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v2

    .line 882
    .local v2, "characteristicUUID":Ljava/util/UUID;
    if-eqz v2, :cond_8

    .line 884
    invoke-virtual {v5, v2}, Landroid/bluetooth/BluetoothGattService;->getCharacteristic(Ljava/util/UUID;)Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v0

    .line 885
    .local v0, "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    if-eqz v0, :cond_7

    .line 887
    const/4 v8, 0x1

    invoke-virtual {v4, v0, v8}, Landroid/bluetooth/BluetoothGatt;->setCharacteristicNotification(Landroid/bluetooth/BluetoothGattCharacteristic;Z)Z

    move-result v8

    if-eqz v8, :cond_6

    .line 889
    sget-object v8, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID:Ljava/util/UUID;

    invoke-virtual {v0, v8}, Landroid/bluetooth/BluetoothGattCharacteristic;->getDescriptor(Ljava/util/UUID;)Landroid/bluetooth/BluetoothGattDescriptor;

    move-result-object v3

    .line 890
    .local v3, "descriptor":Landroid/bluetooth/BluetoothGattDescriptor;
    if-eqz v3, :cond_5

    .line 892
    invoke-virtual {v0}, Landroid/bluetooth/BluetoothGattCharacteristic;->getProperties()I

    move-result v1

    .line 893
    .local v1, "characteristicProperties":I
    sget-object v7, Landroid/bluetooth/BluetoothGattDescriptor;->DISABLE_NOTIFICATION_VALUE:[B

    .line 894
    .local v7, "valueToSend":[B
    if-eqz p4, :cond_0

    .line 896
    and-int/lit8 v8, v1, 0x10

    const/16 v9, 0x10

    if-ne v8, v9, :cond_2

    .line 897
    sget-object v7, Landroid/bluetooth/BluetoothGattDescriptor;->ENABLE_NOTIFICATION_VALUE:[B

    .line 902
    :cond_0
    :goto_0
    array-length v8, v7

    if-lez v8, :cond_4

    .line 903
    new-instance v8, Ljava/lang/StringBuilder;

    invoke-direct {v8}, Ljava/lang/StringBuilder;-><init>()V

    const-string v9, "Notification Description: "

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v8

    const/4 v9, 0x0

    aget-byte v9, v7, v9

    invoke-virtual {v8, v9}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v8

    invoke-virtual {v8}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v8

    invoke-virtual {p0, v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 904
    invoke-virtual {v3, v7}, Landroid/bluetooth/BluetoothGattDescriptor;->setValue([B)Z

    .line 905
    invoke-virtual {v4, v3}, Landroid/bluetooth/BluetoothGatt;->writeDescriptor(Landroid/bluetooth/BluetoothGattDescriptor;)Z

    move-result v8

    if-nez v8, :cond_3

    .line 906
    const-string v8, "Error~Failed to write characteristic descriptor"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 932
    .end local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    .end local v1    # "characteristicProperties":I
    .end local v2    # "characteristicUUID":Ljava/util/UUID;
    .end local v3    # "descriptor":Landroid/bluetooth/BluetoothGattDescriptor;
    .end local v4    # "gatt":Landroid/bluetooth/BluetoothGatt;
    .end local v5    # "service":Landroid/bluetooth/BluetoothGattService;
    .end local v6    # "serviceUUID":Ljava/util/UUID;
    .end local v7    # "valueToSend":[B
    :cond_1
    :goto_1
    return-void

    .line 898
    .restart local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    .restart local v1    # "characteristicProperties":I
    .restart local v2    # "characteristicUUID":Ljava/util/UUID;
    .restart local v3    # "descriptor":Landroid/bluetooth/BluetoothGattDescriptor;
    .restart local v4    # "gatt":Landroid/bluetooth/BluetoothGatt;
    .restart local v5    # "service":Landroid/bluetooth/BluetoothGattService;
    .restart local v6    # "serviceUUID":Ljava/util/UUID;
    .restart local v7    # "valueToSend":[B
    :cond_2
    and-int/lit8 v8, v1, 0x20

    const/16 v9, 0x20

    if-ne v8, v9, :cond_0

    .line 899
    sget-object v7, Landroid/bluetooth/BluetoothGattDescriptor;->ENABLE_INDICATION_VALUE:[B

    goto :goto_0

    .line 908
    :cond_3
    const-string v8, "Notification setup succeeded"

    invoke-virtual {p0, v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    goto :goto_1

    .line 911
    :cond_4
    const-string v8, "Error~Failed to set notification type"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 914
    .end local v1    # "characteristicProperties":I
    .end local v7    # "valueToSend":[B
    :cond_5
    const-string v8, "Error~Failed to get notification descriptor"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 917
    .end local v3    # "descriptor":Landroid/bluetooth/BluetoothGattDescriptor;
    :cond_6
    const-string v8, "Error~Failed to set characteristic notification"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 920
    :cond_7
    const-string v8, "Error~Characteristic not found for Subscribe"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 923
    .end local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    :cond_8
    const-string v8, "Error~Not a Valid Characteristic UUID for Subscribe"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 926
    .end local v2    # "characteristicUUID":Ljava/util/UUID;
    :cond_9
    const-string v8, "Error~Service not found for Subscribe"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 929
    .end local v5    # "service":Landroid/bluetooth/BluetoothGattService;
    :cond_a
    const-string v8, "Error~Not a Valid Service UUID for Subscribe"

    invoke-static {v8}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1
.end method

.method public androidStartAdvertising()V
    .locals 8

    .prologue
    const/4 v7, 0x1

    .line 1238
    sget v4, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v5, 0x15

    if-lt v4, v5, :cond_2

    .line 1240
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    if-nez v4, :cond_0

    .line 1241
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v4}, Landroid/bluetooth/BluetoothAdapter;->getBluetoothLeAdvertiser()Landroid/bluetooth/le/BluetoothLeAdvertiser;

    move-result-object v4

    iput-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    .line 1243
    :cond_0
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    if-nez v4, :cond_1

    .line 1244
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    const-string v5, "bluetooth"

    .line 1245
    invoke-virtual {v4, v5}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroid/bluetooth/BluetoothManager;

    .line 1247
    .local v0, "bluetoothManager":Landroid/bluetooth/BluetoothManager;
    if-eqz v0, :cond_1

    .line 1249
    new-instance v4, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;

    invoke-direct {v4, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    iput-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mUnityGattServices:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;

    .line 1250
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mUnityGattServices:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;

    invoke-virtual {v0, v4, v5}, Landroid/bluetooth/BluetoothManager;->openGattServer(Landroid/content/Context;Landroid/bluetooth/BluetoothGattServerCallback;)Landroid/bluetooth/BluetoothGattServer;

    move-result-object v4

    iput-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    .line 1251
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mUnityGattServices:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    iget-object v6, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->serviceList:Ljava/util/ArrayList;

    invoke-virtual {v4, v5, v6}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->setupServices(Landroid/bluetooth/BluetoothGattServer;Ljava/util/ArrayList;)V

    .line 1255
    .end local v0    # "bluetoothManager":Landroid/bluetooth/BluetoothManager;
    :cond_1
    new-instance v4, Landroid/bluetooth/le/AdvertiseSettings$Builder;

    invoke-direct {v4}, Landroid/bluetooth/le/AdvertiseSettings$Builder;-><init>()V

    .line 1256
    invoke-virtual {v4, v7}, Landroid/bluetooth/le/AdvertiseSettings$Builder;->setAdvertiseMode(I)Landroid/bluetooth/le/AdvertiseSettings$Builder;

    move-result-object v4

    const/4 v5, 0x2

    .line 1257
    invoke-virtual {v4, v5}, Landroid/bluetooth/le/AdvertiseSettings$Builder;->setTxPowerLevel(I)Landroid/bluetooth/le/AdvertiseSettings$Builder;

    move-result-object v4

    .line 1258
    invoke-virtual {v4, v7}, Landroid/bluetooth/le/AdvertiseSettings$Builder;->setConnectable(Z)Landroid/bluetooth/le/AdvertiseSettings$Builder;

    move-result-object v4

    .line 1259
    invoke-virtual {v4}, Landroid/bluetooth/le/AdvertiseSettings$Builder;->build()Landroid/bluetooth/le/AdvertiseSettings;

    move-result-object v3

    .line 1261
    .local v3, "settings":Landroid/bluetooth/le/AdvertiseSettings;
    new-instance v2, Landroid/os/ParcelUuid;

    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->primaryServiceUUID:Ljava/lang/String;

    invoke-static {v4}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    invoke-direct {v2, v4}, Landroid/os/ParcelUuid;-><init>(Ljava/util/UUID;)V

    .line 1263
    .local v2, "pUuid":Landroid/os/ParcelUuid;
    new-instance v4, Landroid/bluetooth/le/AdvertiseData$Builder;

    invoke-direct {v4}, Landroid/bluetooth/le/AdvertiseData$Builder;-><init>()V

    .line 1264
    invoke-virtual {v4, v7}, Landroid/bluetooth/le/AdvertiseData$Builder;->setIncludeDeviceName(Z)Landroid/bluetooth/le/AdvertiseData$Builder;

    move-result-object v4

    .line 1265
    invoke-virtual {v4}, Landroid/bluetooth/le/AdvertiseData$Builder;->build()Landroid/bluetooth/le/AdvertiseData;

    move-result-object v1

    .line 1267
    .local v1, "data":Landroid/bluetooth/le/AdvertiseData;
    new-instance v4, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$7;

    invoke-direct {v4, p0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$7;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    iput-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->advertisingCallback:Landroid/bluetooth/le/AdvertiseCallback;

    .line 1282
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    invoke-virtual {v4}, Landroid/bluetooth/BluetoothAdapter;->getBluetoothLeAdvertiser()Landroid/bluetooth/le/BluetoothLeAdvertiser;

    move-result-object v4

    iput-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    .line 1283
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->advertisingCallback:Landroid/bluetooth/le/AdvertiseCallback;

    invoke-virtual {v4, v3, v1, v5}, Landroid/bluetooth/le/BluetoothLeAdvertiser;->startAdvertising(Landroid/bluetooth/le/AdvertiseSettings;Landroid/bluetooth/le/AdvertiseData;Landroid/bluetooth/le/AdvertiseCallback;)V

    .line 1288
    .end local v1    # "data":Landroid/bluetooth/le/AdvertiseData;
    .end local v2    # "pUuid":Landroid/os/ParcelUuid;
    .end local v3    # "settings":Landroid/bluetooth/le/AdvertiseSettings;
    :goto_0
    return-void

    .line 1286
    :cond_2
    const-string v4, "Error~Failed to start advertising"

    invoke-static {v4}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0
.end method

.method public androidStopAdvertising()V
    .locals 3

    .prologue
    const/4 v2, 0x0

    .line 1292
    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x15

    if-lt v0, v1, :cond_2

    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    if-eqz v0, :cond_2

    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->advertisingCallback:Landroid/bluetooth/le/AdvertiseCallback;

    if-eqz v0, :cond_2

    .line 1294
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    if-eqz v0, :cond_0

    .line 1295
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    invoke-virtual {v0}, Landroid/bluetooth/BluetoothGattServer;->clearServices()V

    .line 1296
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    invoke-virtual {v0}, Landroid/bluetooth/BluetoothGattServer;->close()V

    .line 1297
    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    .line 1300
    :cond_0
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    if-eqz v0, :cond_1

    .line 1301
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->advertisingCallback:Landroid/bluetooth/le/AdvertiseCallback;

    invoke-virtual {v0, v1}, Landroid/bluetooth/le/BluetoothLeAdvertiser;->stopAdvertising(Landroid/bluetooth/le/AdvertiseCallback;)V

    .line 1302
    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdvertiser:Landroid/bluetooth/le/BluetoothLeAdvertiser;

    .line 1305
    :cond_1
    const-string v0, "StoppedAdvertising"

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 1310
    :goto_0
    return-void

    .line 1308
    :cond_2
    const-string v0, "Error~Failed to stop advertising"

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_0
.end method

.method public androidSubscribeCharacteristic(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .param p1, "name"    # Ljava/lang/String;
    .param p2, "serviceString"    # Ljava/lang/String;
    .param p3, "characteristicString"    # Ljava/lang/String;

    .prologue
    .line 936
    const-string v0, "subscribe characteristic"

    invoke-virtual {p0, v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 937
    const/4 v0, 0x1

    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidSetCharacteristicNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    .line 938
    return-void
.end method

.method public androidUnsubscribeCharacteristic(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 1
    .param p1, "name"    # Ljava/lang/String;
    .param p2, "serviceString"    # Ljava/lang/String;
    .param p3, "characteristicString"    # Ljava/lang/String;

    .prologue
    .line 942
    const-string v0, "unsubscribe characteristic"

    invoke-virtual {p0, v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 943
    const/4 v0, 0x0

    invoke-virtual {p0, p1, p2, p3, v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidSetCharacteristicNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V

    .line 944
    return-void
.end method

.method public androidUpdateCharacteristicValue(Ljava/lang/String;[BI)V
    .locals 6
    .param p1, "uuid"    # Ljava/lang/String;
    .param p2, "data"    # [B
    .param p3, "length"    # I

    .prologue
    .line 1314
    invoke-virtual {p1}, Ljava/lang/String;->toLowerCase()Ljava/lang/String;

    move-result-object p1

    .line 1316
    invoke-direct {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->findCharacteristic(Ljava/lang/String;)Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v0

    .line 1317
    .local v0, "gattCharacteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    if-eqz v0, :cond_2

    .line 1319
    invoke-virtual {v0, p2}, Landroid/bluetooth/BluetoothGattCharacteristic;->setValue([B)Z

    .line 1321
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notifyDeviceMap:Ljava/util/Map;

    if-eqz v3, :cond_0

    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notifyDeviceMap:Ljava/util/Map;

    invoke-static {p1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    invoke-interface {v3, v4}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 1323
    iget-object v3, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->notifyDeviceMap:Ljava/util/Map;

    invoke-static {p1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    invoke-interface {v3, v4}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Ljava/util/ArrayList;

    .line 1324
    .local v2, "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    const/4 v1, 0x0

    .local v1, "i":I
    :goto_0
    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v3

    if-ge v1, v3, :cond_1

    .line 1325
    iget-object v4, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothGattServer:Landroid/bluetooth/BluetoothGattServer;

    invoke-virtual {v2, v1}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v3

    check-cast v3, Landroid/bluetooth/BluetoothDevice;

    const/4 v5, 0x0

    invoke-virtual {v4, v3, v0, v5}, Landroid/bluetooth/BluetoothGattServer;->notifyCharacteristicChanged(Landroid/bluetooth/BluetoothDevice;Landroid/bluetooth/BluetoothGattCharacteristic;Z)Z

    .line 1324
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 1329
    .end local v1    # "i":I
    .end local v2    # "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    :cond_0
    const-string v3, "Error~Characteristic not found when updating value (2)"

    invoke-static {v3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 1335
    :cond_1
    :goto_1
    return-void

    .line 1333
    :cond_2
    const-string v3, "Error~Characteristic not found when updating value (1)"

    invoke-static {v3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1
.end method

.method public androidWriteCharacteristic(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;[BIZ)V
    .locals 6
    .param p1, "name"    # Ljava/lang/String;
    .param p2, "serviceString"    # Ljava/lang/String;
    .param p3, "characteristicString"    # Ljava/lang/String;
    .param p4, "data"    # [B
    .param p5, "length"    # I
    .param p6, "withResponse"    # Z

    .prologue
    .line 828
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mBluetoothAdapter:Landroid/bluetooth/BluetoothAdapter;

    if-eqz v5, :cond_0

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    if-eqz v5, :cond_0

    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    if-eqz v5, :cond_0

    .line 830
    iget-object v5, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    invoke-interface {v5, p1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/bluetooth/BluetoothGatt;

    .line 831
    .local v2, "gatt":Landroid/bluetooth/BluetoothGatt;
    if-eqz v2, :cond_0

    .line 833
    invoke-virtual {p0, p2}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v4

    .line 834
    .local v4, "serviceUUID":Ljava/util/UUID;
    if-eqz v4, :cond_5

    .line 836
    invoke-virtual {v2, v4}, Landroid/bluetooth/BluetoothGatt;->getService(Ljava/util/UUID;)Landroid/bluetooth/BluetoothGattService;

    move-result-object v3

    .line 837
    .local v3, "service":Landroid/bluetooth/BluetoothGattService;
    if-eqz v3, :cond_4

    .line 839
    invoke-virtual {p0, p3}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->getUUID(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v1

    .line 840
    .local v1, "characteristicUUID":Ljava/util/UUID;
    if-eqz v1, :cond_3

    .line 842
    invoke-virtual {v3, v1}, Landroid/bluetooth/BluetoothGattService;->getCharacteristic(Ljava/util/UUID;)Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v0

    .line 843
    .local v0, "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    if-eqz v0, :cond_2

    .line 845
    const-string v5, "write characteristic"

    invoke-virtual {p0, v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidBluetoothLog(Ljava/lang/String;)V

    .line 846
    invoke-virtual {v0, p4}, Landroid/bluetooth/BluetoothGattCharacteristic;->setValue([B)Z

    .line 847
    if-eqz p6, :cond_1

    const/4 v5, 0x2

    :goto_0
    invoke-virtual {v0, v5}, Landroid/bluetooth/BluetoothGattCharacteristic;->setWriteType(I)V

    .line 848
    invoke-virtual {v2, v0}, Landroid/bluetooth/BluetoothGatt;->writeCharacteristic(Landroid/bluetooth/BluetoothGattCharacteristic;)Z

    move-result v5

    if-nez v5, :cond_0

    .line 849
    const-string v5, "Error~Failed to write characteristic"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 864
    .end local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    .end local v1    # "characteristicUUID":Ljava/util/UUID;
    .end local v2    # "gatt":Landroid/bluetooth/BluetoothGatt;
    .end local v3    # "service":Landroid/bluetooth/BluetoothGattService;
    .end local v4    # "serviceUUID":Ljava/util/UUID;
    :cond_0
    :goto_1
    return-void

    .line 847
    .restart local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    .restart local v1    # "characteristicUUID":Ljava/util/UUID;
    .restart local v2    # "gatt":Landroid/bluetooth/BluetoothGatt;
    .restart local v3    # "service":Landroid/bluetooth/BluetoothGattService;
    .restart local v4    # "serviceUUID":Ljava/util/UUID;
    :cond_1
    const/4 v5, 0x1

    goto :goto_0

    .line 852
    :cond_2
    const-string v5, "Error~Characteristic not found for Write"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 855
    .end local v0    # "characteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    :cond_3
    const-string v5, "Error~Not a Valid Characteristic UUID for Write"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 858
    .end local v1    # "characteristicUUID":Ljava/util/UUID;
    :cond_4
    const-string v5, "Error~Service not found for Write"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1

    .line 861
    .end local v3    # "service":Landroid/bluetooth/BluetoothGattService;
    :cond_5
    const-string v5, "Error~Not a Valid Service UUID for Write"

    invoke-static {v5}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    goto :goto_1
.end method

.method protected getUUID(Ljava/lang/String;)Ljava/util/UUID;
    .locals 5
    .param p1, "uuidString"    # Ljava/lang/String;

    .prologue
    .line 770
    const/4 v1, 0x0

    .line 772
    .local v1, "uuid":Ljava/util/UUID;
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v2

    const/16 v3, 0x24

    if-ne v2, v3, :cond_1

    .line 774
    invoke-static {p1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v1

    .line 786
    :cond_0
    :goto_0
    return-object v1

    .line 776
    :cond_1
    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v2

    const/16 v3, 0x8

    if-gt v2, v3, :cond_0

    .line 778
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    .line 779
    .local v0, "newString":Ljava/lang/StringBuilder;
    const-string v2, "00000000"

    const/4 v3, 0x0

    invoke-virtual {p1}, Ljava/lang/String;->length()I

    move-result v4

    rsub-int/lit8 v4, v4, 0x8

    invoke-virtual {v0, v2, v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/CharSequence;II)Ljava/lang/StringBuilder;

    .line 780
    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 781
    const-string v2, "-0000-1000-8000-00805f9b34fb"

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    .line 783
    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v1

    goto :goto_0
.end method

.method public onDestroy()V
    .locals 3

    .prologue
    const/4 v2, 0x0

    .line 288
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    if-eqz v0, :cond_0

    .line 290
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->mContext:Landroid/content/Context;

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->ActionFoundReceiver:Landroid/content/BroadcastReceiver;

    invoke-virtual {v0, v1}, Landroid/content/Context;->unregisterReceiver(Landroid/content/BroadcastReceiver;)V

    .line 291
    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->uuidList:Ljava/util/ArrayList;

    .line 292
    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceMap:Ljava/util/Map;

    .line 293
    iput-object v2, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->deviceGattMap:Ljava/util/Map;

    .line 295
    :cond_0
    return-void
.end method
