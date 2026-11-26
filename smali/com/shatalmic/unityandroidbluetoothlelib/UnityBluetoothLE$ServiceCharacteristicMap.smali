.class Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;
.super Ljava/lang/Object;
.source "UnityBluetoothLE.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "ServiceCharacteristicMap"
.end annotation


# instance fields
.field public Service:Landroid/bluetooth/BluetoothGattService;

.field public UUID:Ljava/lang/String;

.field public characteristicMap:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map",
            "<",
            "Ljava/lang/String;",
            "Landroid/bluetooth/BluetoothGattCharacteristic;",
            ">;"
        }
    .end annotation
.end field

.field final synthetic this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;


# direct methods
.method private constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V
    .locals 2

    .prologue
    const/4 v1, 0x0

    .line 1079
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 1080
    const-string v0, ""

    iput-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->UUID:Ljava/lang/String;

    .line 1081
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->Service:Landroid/bluetooth/BluetoothGattService;

    .line 1082
    iput-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->characteristicMap:Ljava/util/Map;

    return-void
.end method

.method synthetic constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;)V
    .locals 0
    .param p1, "x0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
    .param p2, "x1"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$1;

    .prologue
    .line 1079
    invoke-direct {p0, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;-><init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V

    return-void
.end method
