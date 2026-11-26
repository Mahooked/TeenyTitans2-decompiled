.class public Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;
.super Landroid/bluetooth/BluetoothGattServerCallback;
.source "UnityBluetoothLE.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x1
    name = "UnityGattServices"
.end annotation


# instance fields
.field final synthetic this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;


# direct methods
.method public constructor <init>(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)V
    .locals 0
    .param p1, "this$0"    # Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    .prologue
    .line 958
    iput-object p1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-direct {p0}, Landroid/bluetooth/BluetoothGattServerCallback;-><init>()V

    return-void
.end method

.method private removeDeviceFromNotifyList(Ljava/util/ArrayList;Landroid/bluetooth/BluetoothDevice;)V
    .locals 3
    .param p2, "device"    # Landroid/bluetooth/BluetoothDevice;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/ArrayList",
            "<",
            "Landroid/bluetooth/BluetoothDevice;",
            ">;",
            "Landroid/bluetooth/BluetoothDevice;",
            ")V"
        }
    .end annotation

    .prologue
    .line 1035
    .local p1, "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    invoke-virtual {p1}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-ge v0, v1, :cond_0

    .line 1037
    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/bluetooth/BluetoothDevice;

    invoke-virtual {v1}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {p2}, Landroid/bluetooth/BluetoothDevice;->getAddress()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v1

    if-eqz v1, :cond_1

    .line 1039
    invoke-virtual {p1, v0}, Ljava/util/ArrayList;->remove(I)Ljava/lang/Object;

    .line 1043
    :cond_0
    return-void

    .line 1035
    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0
.end method


# virtual methods
.method public onCharacteristicReadRequest(Landroid/bluetooth/BluetoothDevice;IILandroid/bluetooth/BluetoothGattCharacteristic;)V
    .locals 8
    .param p1, "device"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "requestId"    # I
    .param p3, "offset"    # I
    .param p4, "characteristic"    # Landroid/bluetooth/BluetoothGattCharacteristic;

    .prologue
    .line 992
    const-string v0, "UnityBluetoothLE"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "onCharacteristicReadRequest requestId="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, " offset="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 994
    invoke-virtual {p4}, Landroid/bluetooth/BluetoothGattCharacteristic;->getUuid()Ljava/util/UUID;

    move-result-object v0

    invoke-virtual {v0}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v7

    .line 995
    .local v7, "uuid":Ljava/lang/String;
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0, v7}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$800(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/lang/String;)Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v6

    .line 996
    .local v6, "myCharacteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    if-eqz v6, :cond_0

    .line 997
    invoke-virtual {v6}, Landroid/bluetooth/BluetoothGattCharacteristic;->getValue()[B

    move-result-object v0

    invoke-virtual {p4, v0}, Landroid/bluetooth/BluetoothGattCharacteristic;->setValue([B)Z

    .line 999
    :cond_0
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$900(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothGattServer;

    move-result-object v0

    const/4 v3, 0x0

    .line 1000
    invoke-virtual {p4}, Landroid/bluetooth/BluetoothGattCharacteristic;->getValue()[B

    move-result-object v5

    move-object v1, p1

    move v2, p2

    move v4, p3

    .line 999
    invoke-virtual/range {v0 .. v5}, Landroid/bluetooth/BluetoothGattServer;->sendResponse(Landroid/bluetooth/BluetoothDevice;III[B)Z

    .line 1001
    return-void
.end method

.method public onCharacteristicWriteRequest(Landroid/bluetooth/BluetoothDevice;ILandroid/bluetooth/BluetoothGattCharacteristic;ZZI[B)V
    .locals 10
    .param p1, "device"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "requestId"    # I
    .param p3, "characteristic"    # Landroid/bluetooth/BluetoothGattCharacteristic;
    .param p4, "preparedWrite"    # Z
    .param p5, "responseNeeded"    # Z
    .param p6, "offset"    # I
    .param p7, "value"    # [B

    .prologue
    .line 1007
    const-string v1, "UnityBluetoothLE"

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "onCharacteristicWriteRequest requestId="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, " preparedWrite="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    .line 1008
    invoke-static {p4}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, " responseNeeded="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    .line 1009
    invoke-static {p5}, Ljava/lang/Boolean;->toString(Z)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    const-string v3, " offset="

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    move/from16 v0, p6

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    .line 1007
    invoke-static {v1, v2}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 1011
    invoke-virtual {p3}, Landroid/bluetooth/BluetoothGattCharacteristic;->getUuid()Ljava/util/UUID;

    move-result-object v1

    invoke-virtual {v1}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v9

    .line 1012
    .local v9, "uuid":Ljava/lang/String;
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v1, v9}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$800(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/lang/String;)Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v8

    .line 1013
    .local v8, "myCharacteristic":Landroid/bluetooth/BluetoothGattCharacteristic;
    if-eqz v8, :cond_0

    .line 1014
    move-object/from16 v0, p7

    invoke-virtual {v8, v0}, Landroid/bluetooth/BluetoothGattCharacteristic;->setValue([B)Z

    .line 1016
    const/4 v1, 0x0

    move-object/from16 v0, p7

    array-length v2, v0

    move-object/from16 v0, p7

    invoke-static {v0, v1, v2}, Ljava/util/Arrays;->copyOfRange([BII)[B

    move-result-object v1

    const/4 v2, 0x0

    invoke-static {v1, v2}, Landroid/util/Base64;->encodeToString([BI)Ljava/lang/String;

    move-result-object v7

    .line 1017
    .local v7, "dataString":Ljava/lang/String;
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "PeripheralReceivedWriteData~"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, v9}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "~"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->UnitySend(Ljava/lang/String;)V

    .line 1020
    .end local v7    # "dataString":Ljava/lang/String;
    :cond_0
    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$900(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothGattServer;

    move-result-object v1

    const/4 v4, 0x0

    const/4 v6, 0x0

    move-object v2, p1

    move v3, p2

    move/from16 v5, p6

    invoke-virtual/range {v1 .. v6}, Landroid/bluetooth/BluetoothGattServer;->sendResponse(Landroid/bluetooth/BluetoothDevice;III[B)Z

    .line 1022
    return-void
.end method

.method public onConnectionStateChange(Landroid/bluetooth/BluetoothDevice;II)V
    .locals 3
    .param p1, "device"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "status"    # I
    .param p3, "newState"    # I

    .prologue
    .line 986
    const-string v0, "UnityBluetoothLE"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "onConnectionStateChange status="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "->"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 987
    return-void
.end method

.method public onDescriptorReadRequest(Landroid/bluetooth/BluetoothDevice;IILandroid/bluetooth/BluetoothGattDescriptor;)V
    .locals 6
    .param p1, "device"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "requestId"    # I
    .param p3, "offset"    # I
    .param p4, "descriptor"    # Landroid/bluetooth/BluetoothGattDescriptor;

    .prologue
    .line 1027
    invoke-super {p0, p1, p2, p3, p4}, Landroid/bluetooth/BluetoothGattServerCallback;->onDescriptorReadRequest(Landroid/bluetooth/BluetoothDevice;IILandroid/bluetooth/BluetoothGattDescriptor;)V

    .line 1030
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$900(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothGattServer;

    move-result-object v0

    const/4 v3, 0x0

    const/4 v5, 0x0

    move-object v1, p1

    move v2, p2

    move v4, p3

    invoke-virtual/range {v0 .. v5}, Landroid/bluetooth/BluetoothGattServer;->sendResponse(Landroid/bluetooth/BluetoothDevice;III[B)Z

    .line 1031
    return-void
.end method

.method public onDescriptorWriteRequest(Landroid/bluetooth/BluetoothDevice;ILandroid/bluetooth/BluetoothGattDescriptor;ZZI[B)V
    .locals 8
    .param p1, "device"    # Landroid/bluetooth/BluetoothDevice;
    .param p2, "requestId"    # I
    .param p3, "descriptor"    # Landroid/bluetooth/BluetoothGattDescriptor;
    .param p4, "preparedWrite"    # Z
    .param p5, "responseNeeded"    # Z
    .param p6, "offset"    # I
    .param p7, "value"    # [B

    .prologue
    .line 1048
    invoke-super/range {p0 .. p7}, Landroid/bluetooth/BluetoothGattServerCallback;->onDescriptorWriteRequest(Landroid/bluetooth/BluetoothDevice;ILandroid/bluetooth/BluetoothGattDescriptor;ZZI[B)V

    .line 1050
    invoke-virtual {p3}, Landroid/bluetooth/BluetoothGattDescriptor;->getUuid()Ljava/util/UUID;

    move-result-object v0

    iget-object v1, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$1000(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Ljava/util/UUID;->fromString(Ljava/lang/String;)Ljava/util/UUID;

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/util/UUID;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 1052
    invoke-virtual {p3}, Landroid/bluetooth/BluetoothGattDescriptor;->getCharacteristic()Landroid/bluetooth/BluetoothGattCharacteristic;

    move-result-object v0

    invoke-virtual {v0}, Landroid/bluetooth/BluetoothGattCharacteristic;->getUuid()Ljava/util/UUID;

    move-result-object v7

    .line 1054
    .local v7, "uuid":Ljava/util/UUID;
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$1100(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/Map;

    move-result-object v0

    if-nez v0, :cond_0

    .line 1055
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    new-instance v1, Ljava/util/HashMap;

    invoke-direct {v1}, Ljava/util/HashMap;-><init>()V

    invoke-static {v0, v1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$1102(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;Ljava/util/Map;)Ljava/util/Map;

    .line 1057
    :cond_0
    const/4 v6, 0x0

    .line 1058
    .local v6, "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$1100(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/Map;

    move-result-object v0

    invoke-interface {v0, v7}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_1

    .line 1059
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$1100(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/Map;

    move-result-object v0

    new-instance v1, Ljava/util/ArrayList;

    invoke-direct {v1}, Ljava/util/ArrayList;-><init>()V

    invoke-interface {v0, v7, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 1061
    :cond_1
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$1100(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Ljava/util/Map;

    move-result-object v0

    invoke-interface {v0, v7}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v6

    .end local v6    # "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    check-cast v6, Ljava/util/ArrayList;

    .line 1063
    .restart local v6    # "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    sget-object v0, Landroid/bluetooth/BluetoothGattDescriptor;->ENABLE_NOTIFICATION_VALUE:[B

    invoke-static {p7, v0}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 1065
    invoke-direct {p0, v6, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->removeDeviceFromNotifyList(Ljava/util/ArrayList;Landroid/bluetooth/BluetoothDevice;)V

    .line 1066
    invoke-virtual {v6, p1}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 1075
    .end local v6    # "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    .end local v7    # "uuid":Ljava/util/UUID;
    :cond_2
    :goto_0
    iget-object v0, p0, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->this$0:Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;

    invoke-static {v0}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;->access$900(Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE;)Landroid/bluetooth/BluetoothGattServer;

    move-result-object v0

    const/4 v3, 0x0

    move-object v1, p1

    move v2, p2

    move v4, p6

    move-object v5, p7

    invoke-virtual/range {v0 .. v5}, Landroid/bluetooth/BluetoothGattServer;->sendResponse(Landroid/bluetooth/BluetoothDevice;III[B)Z

    .line 1076
    return-void

    .line 1068
    .restart local v6    # "notifyDeviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Landroid/bluetooth/BluetoothDevice;>;"
    .restart local v7    # "uuid":Ljava/util/UUID;
    :cond_3
    sget-object v0, Landroid/bluetooth/BluetoothGattDescriptor;->DISABLE_NOTIFICATION_VALUE:[B

    invoke-static {p7, v0}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 1070
    invoke-direct {p0, v6, p1}, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$UnityGattServices;->removeDeviceFromNotifyList(Ljava/util/ArrayList;Landroid/bluetooth/BluetoothDevice;)V

    goto :goto_0
.end method

.method public onServiceAdded(ILandroid/bluetooth/BluetoothGattService;)V
    .locals 3
    .param p1, "status"    # I
    .param p2, "service"    # Landroid/bluetooth/BluetoothGattService;

    .prologue
    .line 975
    if-nez p1, :cond_0

    .line 976
    const-string v0, "UnityBluetoothLE"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "onServiceAdded status=GATT_SUCCESS service="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    .line 977
    invoke-virtual {p2}, Landroid/bluetooth/BluetoothGattService;->getUuid()Ljava/util/UUID;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    .line 976
    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 981
    :goto_0
    return-void

    .line 979
    :cond_0
    const-string v0, "UnityBluetoothLE"

    const-string v1, "onServiceAdded status!=GATT_SUCCESS"

    invoke-static {v0, v1}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0
.end method

.method public setupServices(Landroid/bluetooth/BluetoothGattServer;Ljava/util/ArrayList;)V
    .locals 2
    .param p1, "gattServer"    # Landroid/bluetooth/BluetoothGattServer;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/bluetooth/BluetoothGattServer;",
            "Ljava/util/ArrayList",
            "<",
            "Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;",
            ">;)V"
        }
    .end annotation

    .prologue
    .line 962
    .local p2, "serviceList":Ljava/util/ArrayList;, "Ljava/util/ArrayList<Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;>;"
    if-eqz p1, :cond_0

    if-eqz p2, :cond_0

    .line 964
    invoke-virtual {p1}, Landroid/bluetooth/BluetoothGattServer;->clearServices()V

    .line 966
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    invoke-virtual {p2}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-ge v0, v1, :cond_0

    .line 968
    invoke-virtual {p2, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;

    iget-object v1, v1, Lcom/shatalmic/unityandroidbluetoothlelib/UnityBluetoothLE$ServiceCharacteristicMap;->Service:Landroid/bluetooth/BluetoothGattService;

    invoke-virtual {p1, v1}, Landroid/bluetooth/BluetoothGattServer;->addService(Landroid/bluetooth/BluetoothGattService;)Z

    .line 966
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 971
    .end local v0    # "i":I
    :cond_0
    return-void
.end method
