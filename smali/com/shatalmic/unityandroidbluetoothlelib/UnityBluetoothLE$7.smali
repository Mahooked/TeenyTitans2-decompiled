.class Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$7;
.super Landroid/bluetooth/le/AdvertiseCallback;
.source "UnityBluetoothLE.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->androidStartAdvertising()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;


# direct methods
.method constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V
    .locals 0
    .param p1, "this$0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 1267
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$7;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Landroid/bluetooth/le/AdvertiseCallback;-><init>()V

    return-void
.end method


# virtual methods
.method public onStartFailure(I)V
    .locals 2
    .param p1, "errorCode"    # I

    .prologue
    .line 1277
    invoke-super {p0, p1}, Landroid/bluetooth/le/AdvertiseCallback;->onStartFailure(I)V

    .line 1278
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Error~Advertising onStartFailure: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 1279
    return-void
.end method

.method public onStartSuccess(Landroid/bluetooth/le/AdvertiseSettings;)V
    .locals 1
    .param p1, "settingsInEffect"    # Landroid/bluetooth/le/AdvertiseSettings;

    .prologue
    .line 1271
    invoke-super {p0, p1}, Landroid/bluetooth/le/AdvertiseCallback;->onStartSuccess(Landroid/bluetooth/le/AdvertiseSettings;)V

    .line 1272
    const-string v0, "StartedAdvertising"

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 1273
    return-void
.end method
