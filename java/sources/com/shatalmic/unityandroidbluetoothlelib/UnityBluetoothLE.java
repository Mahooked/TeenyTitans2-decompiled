package com.shatalmic.unityandroidbluetoothlelib;

import android.annotation.TargetApi;
import android.bluetooth.BluetoothAdapter;
import android.bluetooth.BluetoothDevice;
import android.bluetooth.BluetoothGatt;
import android.bluetooth.BluetoothGattCallback;
import android.bluetooth.BluetoothGattCharacteristic;
import android.bluetooth.BluetoothGattDescriptor;
import android.bluetooth.BluetoothGattServer;
import android.bluetooth.BluetoothGattServerCallback;
import android.bluetooth.BluetoothGattService;
import android.bluetooth.BluetoothManager;
import android.bluetooth.le.AdvertiseCallback;
import android.bluetooth.le.AdvertiseData;
import android.bluetooth.le.AdvertiseSettings;
import android.bluetooth.le.BluetoothLeAdvertiser;
import android.bluetooth.le.BluetoothLeScanner;
import android.bluetooth.le.ScanCallback;
import android.bluetooth.le.ScanFilter;
import android.bluetooth.le.ScanResult;
import android.bluetooth.le.ScanSettings;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.os.Build;
import android.os.ParcelUuid;
import android.os.Parcelable;
import android.support.v4.view.MotionEventCompat;
import android.util.Base64;
import android.util.Log;
import com.unity3d.player.UnityPlayer;
import java.io.UnsupportedEncodingException;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.UUID;
import java.util.concurrent.atomic.AtomicBoolean;
import org.apache.http.protocol.HTTP;

/* JADX INFO: loaded from: classes.dex */
public class UnityBluetoothLE {
    protected static final UUID CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID = UUID.fromString("00002902-0000-1000-8000-00805f9b34fb");
    public static final String TAG = "UnityBluetoothLE";
    public static UnityBluetoothLE _instance;
    private ScanCallback api21ScanCallback;
    private BluetoothAdapter mBluetoothAdapter;
    private BluetoothLeAdvertiser mBluetoothAdvertiser;
    private BluetoothLeScanner mBluetoothLeScanner;
    public AtomicBoolean mRunning = new AtomicBoolean();
    private ArrayList<UUID> uuidList = null;
    private Map<String, BluetoothDevice> deviceMap = null;
    private Map<String, BluetoothGatt> deviceGattMap = null;
    private Context mContext = null;
    private final BroadcastReceiver ActionFoundReceiver = new BroadcastReceiver() { // from class: com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE.1
        @Override // android.content.BroadcastReceiver
        public void onReceive(Context context, Intent intent) {
            String action = intent.getAction();
            if ("android.bluetooth.device.action.UUID".equals(action)) {
                UnityBluetoothLE.this.androidBluetoothLog("got action_uuid");
                if (UnityBluetoothLE.this.uuidList != null) {
                    BluetoothDevice device = (BluetoothDevice) intent.getParcelableExtra("android.bluetooth.device.extra.DEVICE");
                    Parcelable[] uuidExtra = intent.getParcelableArrayExtra("android.bluetooth.device.extra.UUID");
                    if (device != null && uuidExtra != null) {
                        for (Parcelable uuidParcel : uuidExtra) {
                            String uuid = uuidParcel.toString();
                            UnityBluetoothLE.this.androidBluetoothLog("checking uuid " + uuid);
                            if (UnityBluetoothLE.this.uuidList.contains(UnityBluetoothLE.this.getFullBluetoothLEUUID(uuid))) {
                                UnityBluetoothLE.this.sendDiscoveredDevice(device, 0, null);
                            }
                        }
                        return;
                    }
                    if (device != null) {
                        UnityBluetoothLE.this.androidBluetoothLog("device: " + device.getAddress());
                    } else {
                        UnityBluetoothLE.this.androidBluetoothLog("device is null");
                    }
                    if (uuidExtra != null) {
                        UnityBluetoothLE.this.androidBluetoothLog("uuid count: " + uuidExtra.length);
                    } else {
                        UnityBluetoothLE.this.androidBluetoothLog("uuidExtra is null");
                    }
                }
            }
        }
    };
    private boolean _noManufacturerData = false;
    private int _recordType = 255;
    private BluetoothAdapter.LeScanCallback mLeScanCallback = new BluetoothAdapter.LeScanCallback() { // from class: com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE.2
        @Override // android.bluetooth.BluetoothAdapter.LeScanCallback
        public void onLeScan(BluetoothDevice device, int rssi, byte[] scanRecord) {
            UnityBluetoothLE.this.sendDiscoveredDevice(device, rssi, scanRecord);
        }
    };
    private final BluetoothGattCallback mGattCallback = new BluetoothGattCallback() { // from class: com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE.5
        @Override // android.bluetooth.BluetoothGattCallback
        public void onConnectionStateChange(BluetoothGatt gatt, int status, int newState) {
            UnityBluetoothLE.this.androidBluetoothLog("onConnectionStateChange");
            String deviceAddress = gatt.getDevice().getAddress();
            if (newState == 2) {
                if (UnityBluetoothLE.this.deviceGattMap == null) {
                    UnityBluetoothLE.this.deviceGattMap = new HashMap();
                }
                UnityBluetoothLE.this.deviceGattMap.put(deviceAddress, gatt);
                UnityBluetoothLE.UnitySend("ConnectedPeripheral~" + deviceAddress);
                gatt.discoverServices();
                return;
            }
            if (newState == 0) {
                gatt.close();
                UnityBluetoothLE.UnitySend("DisconnectedPeripheral~" + deviceAddress);
            }
        }

        @Override // android.bluetooth.BluetoothGattCallback
        public void onServicesDiscovered(BluetoothGatt gatt, int status) {
            if (status == 0) {
                UnityBluetoothLE.this.androidBluetoothLog("Services Discovered");
                List<BluetoothGattService> services = gatt.getServices();
                if (services != null) {
                    String deviceAddress = gatt.getDevice().getAddress();
                    for (BluetoothGattService service : services) {
                        UnityBluetoothLE.UnitySend("DiscoveredService~" + deviceAddress + "~" + service.getUuid());
                        List<BluetoothGattCharacteristic> characteristics = service.getCharacteristics();
                        for (BluetoothGattCharacteristic characteristic : characteristics) {
                            UnityBluetoothLE.UnitySend("DiscoveredCharacteristic~" + deviceAddress + "~" + service.getUuid() + "~" + characteristic.getUuid());
                        }
                    }
                    return;
                }
                return;
            }
            UnityBluetoothLE.this.androidBluetoothLog("Error~Service Discovery " + status);
        }

        @Override // android.bluetooth.BluetoothGattCallback
        public void onCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, int status) {
            byte[] data;
            if (status == 0 && (data = characteristic.getValue()) != null) {
                String deviceAddress = gatt.getDevice().getAddress();
                String dataString = Base64.encodeToString(data, 0);
                UnityBluetoothLE.UnitySend("DidUpdateValueForCharacteristic~" + deviceAddress + "~" + characteristic.getUuid() + "~" + dataString);
            }
        }

        @Override // android.bluetooth.BluetoothGattCallback
        public void onCharacteristicChanged(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic) {
            byte[] data = characteristic.getValue();
            if (data != null) {
                String deviceAddress = gatt.getDevice().getAddress();
                String dataString = Base64.encodeToString(data, 0);
                UnityBluetoothLE.UnitySend("DidUpdateValueForCharacteristic~" + deviceAddress + "~" + characteristic.getUuid() + "~" + dataString);
            }
        }

        @Override // android.bluetooth.BluetoothGattCallback
        public void onCharacteristicWrite(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, int status) {
            if (status == 0) {
                UnityBluetoothLE.UnitySend("DidWriteCharacteristic~" + characteristic.getUuid());
            } else {
                UnityBluetoothLE.UnitySend("Error~Response - failed to write characteristic: " + status);
            }
        }

        @Override // android.bluetooth.BluetoothGattCallback
        public void onDescriptorWrite(BluetoothGatt gatt, BluetoothGattDescriptor descriptor, int status) {
            if (status == 0) {
                UnityBluetoothLE.this.androidBluetoothLog("onDescriptorWrite Success");
                if (descriptor != null && descriptor.getCharacteristic() != null && descriptor.getCharacteristic().getUuid() != null) {
                    String deviceAddress = gatt.getDevice().getAddress();
                    UnityBluetoothLE.UnitySend("DidUpdateNotificationStateForCharacteristic~" + deviceAddress + "~" + descriptor.getCharacteristic().getUuid());
                    return;
                } else {
                    UnityBluetoothLE.UnitySend("Error~Descriptor Write Failed: characterstic or uuid blank");
                    return;
                }
            }
            UnityBluetoothLE.UnitySend("Error~Descriptor Write Failed: " + status);
        }
    };
    private AdvertiseCallback advertisingCallback = null;
    private String primaryServiceUUID = "";
    private ArrayList<BluetoothGattCharacteristic> characteristList = null;
    private ArrayList<ServiceCharacteristicMap> serviceList = null;
    private BluetoothGattServer mBluetoothGattServer = null;
    private UnityGattServices mUnityGattServices = null;
    private Map<UUID, ArrayList<BluetoothDevice>> notifyDeviceMap = null;
    private String notificationDescriptionUUID = "00002902-0000-1000-8000-00805f9b34fb";

    public static UnityBluetoothLE getInstance() {
        if (_instance == null) {
            _instance = new UnityBluetoothLE();
        }
        return _instance;
    }

    public static void UnitySend(String message) {
        UnityPlayer.UnitySendMessage("BluetoothLEReceiver", "OnBluetoothMessage", message);
    }

    public static void UnitySend(byte[] data, int length) {
        String message = Base64.encodeToString(Arrays.copyOfRange(data, 0, length), 0);
        UnityPlayer.UnitySendMessage("BluetoothLEReceiver", "OnBluetoothData", message);
    }

    public static void UnitySendPeripheral(byte[] data, int length) {
        String message = Base64.encodeToString(Arrays.copyOfRange(data, 0, length), 0);
        UnityPlayer.UnitySendMessage("BluetoothLEReceiver", "OnPeripheralData", message);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public UUID getFullBluetoothLEUUID(String uuidString) {
        androidBluetoothLog("getFullBluetoothLEUUID : " + uuidString);
        if (uuidString.length() == 4) {
            UUID uuid = UUID.fromString("0000" + uuidString + "-0000-1000-8000-00805F9B34FB");
            return uuid;
        }
        UUID uuid2 = UUID.fromString(uuidString);
        return uuid2;
    }

    private UUID getUUIDFromString(String uuidString) {
        String s2 = uuidString.replace("-", "");
        UUID uuid = new UUID(new BigInteger(s2.substring(0, 16), 16).longValue(), new BigInteger(s2.substring(16), 16).longValue());
        return uuid;
    }

    public void androidBluetoothInitialize(boolean asCentral, boolean asPeripheral) {
        Log.d(TAG, "androidBluetoothInitialize");
        this.mContext = UnityPlayer.currentActivity.getApplicationContext();
        if (!this.mContext.getPackageManager().hasSystemFeature("android.hardware.bluetooth_le")) {
            UnitySend("Error~Bluetooth Low Energy Not Available");
            return;
        }
        this.uuidList = null;
        this.deviceMap = null;
        this.deviceGattMap = null;
        IntentFilter filter = new IntentFilter("android.bluetooth.device.action.FOUND");
        filter.addAction("android.bluetooth.device.action.UUID");
        filter.addAction("android.bluetooth.adapter.action.DISCOVERY_STARTED");
        filter.addAction("android.bluetooth.adapter.action.DISCOVERY_FINISHED");
        this.mContext.registerReceiver(this.ActionFoundReceiver, filter);
        if (BluetoothAdapter.getDefaultAdapter() != null) {
            if ((asPeripheral && Build.VERSION.SDK_INT < 21) || (asPeripheral && Build.VERSION.SDK_INT >= 21 && BluetoothAdapter.getDefaultAdapter().getBluetoothLeAdvertiser() == null)) {
                UnitySend("Error~Peripheral mode is Not Available");
                return;
            }
            BluetoothManager bluetoothManager = (BluetoothManager) this.mContext.getSystemService("bluetooth");
            if (bluetoothManager != null) {
                this.mBluetoothAdapter = bluetoothManager.getAdapter();
                if (this.mBluetoothAdapter != null) {
                    if (this.mBluetoothAdapter.isEnabled()) {
                        if (Build.VERSION.SDK_INT >= 21) {
                            this.mBluetoothLeScanner = this.mBluetoothAdapter.getBluetoothLeScanner();
                        }
                        UnitySend("Initialized");
                        return;
                    }
                    UnitySend("Error~Bluetooth LE Not Enabled");
                    return;
                }
                onDestroy();
                return;
            }
            onDestroy();
            return;
        }
        UnitySend("Error~Bluetooth LE Not Available");
    }

    public void androidBluetoothLog(String message) {
        Log.i(TAG, message);
    }

    public void androidBluetoothDeInitialize() {
        Log.d(TAG, "androidBluetoothDeInitialize");
        onDestroy();
        UnitySend("DeInitialized");
    }

    public void androidBluetoothEnable(boolean enable) {
        androidBluetoothLog("androidBluetoothEnable");
        if (this.mBluetoothAdapter == null) {
            UnitySend("Error~Plugin Initialize not called.");
        } else if (enable) {
            this.mBluetoothAdapter.enable();
        } else {
            this.mBluetoothAdapter.disable();
        }
    }

    public void onDestroy() {
        if (this.mContext != null) {
            this.mContext.unregisterReceiver(this.ActionFoundReceiver);
            this.uuidList = null;
            this.deviceMap = null;
            this.deviceGattMap = null;
        }
    }

    public class AdRecord {
        public byte[] Data;
        public int Length;
        public int Type;

        public AdRecord(int length, int type, byte[] data) {
            this.Length = length;
            this.Type = type;
            this.Data = data;
        }

        public String decodeRecord() {
            try {
                String decodedRecord = new String(this.Data, HTTP.UTF_8);
                return decodedRecord;
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
                return "";
            }
        }
    }

    public class AdRecordList {
        List<AdRecord> Records = new ArrayList();

        public AdRecordList() {
        }

        public List<AdRecord> parseScanRecord(byte[] scanRecord) {
            int type;
            int index = 0;
            while (index < scanRecord.length) {
                int index2 = index + 1;
                int length = scanRecord[index];
                if (length == 0 || (type = scanRecord[index2]) == 0) {
                    break;
                    break;
                }
                byte[] data = Arrays.copyOfRange(scanRecord, index2 + 1, index2 + length);
                this.Records.add(UnityBluetoothLE.this.new AdRecord(length, type, data));
                index = index2 + length;
            }
            return this.Records;
        }

        public AdRecord getRecord(int type) {
            for (AdRecord record : this.Records) {
                if (record.Type == type) {
                    return record;
                }
            }
            return null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void sendDiscoveredDevice(BluetoothDevice device, int rssi, byte[] scanRecord) {
        String name = "No Name";
        if (device.getName() != null) {
            name = device.getName();
        }
        if (this.deviceMap == null) {
            this.deviceMap = new HashMap();
        }
        this.deviceMap.put(device.getAddress(), device);
        if (this._noManufacturerData) {
            byte[] manufacturerBytes = null;
            if (scanRecord != null) {
                AdRecordList recordList = new AdRecordList();
                recordList.parseScanRecord(scanRecord);
                AdRecord record = recordList.getRecord(this._recordType);
                if (record != null) {
                    manufacturerBytes = record.Data;
                }
            }
            if (manufacturerBytes != null || rssi != 0) {
                if (manufacturerBytes == null) {
                    manufacturerBytes = new byte[]{0};
                }
                UnitySend("DiscoveredPeripheral~" + device.getAddress() + "~" + name + "~" + Integer.valueOf(rssi) + "~" + Base64.encodeToString(manufacturerBytes, 0));
                return;
            }
            UnitySend("DiscoveredPeripheral~" + device.getAddress() + "~" + name);
            return;
        }
        if (scanRecord != null) {
            AdRecordList recordList2 = new AdRecordList();
            recordList2.parseScanRecord(scanRecord);
            AdRecord record2 = recordList2.getRecord(-1);
            if (record2 != null) {
                UnitySend("DiscoveredPeripheral~" + device.getAddress() + "~" + name + "~" + Integer.valueOf(rssi) + "~" + Base64.encodeToString(record2.Data, 0));
                return;
            } else {
                UnitySend("DiscoveredPeripheral~" + device.getAddress() + "~" + name);
                return;
            }
        }
        UnitySend("DiscoveredPeripheral~" + device.getAddress() + "~" + name);
    }

    public void androidBluetoothPause(boolean paused) {
    }

    @TargetApi(MotionEventCompat.AXIS_BRAKE)
    private void api21Scan(List<UUID> uuids) {
        List<ScanFilter> scanFilters = new ArrayList<>();
        if (uuids != null) {
            for (int i = 0; i < uuids.size(); i++) {
                scanFilters.add(new ScanFilter.Builder().setServiceUuid(ParcelUuid.fromString(uuids.get(i).toString())).build());
            }
        }
        ScanSettings settings = new ScanSettings.Builder().setCallbackType(1).build();
        ScanCallback scanCallback = new ScanCallback() { // from class: com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE.3
            @Override // android.bluetooth.le.ScanCallback
            public void onScanResult(int callbackType, ScanResult result) {
                UnityBluetoothLE.this.mLeScanCallback.onLeScan(result.getDevice(), result.getRssi(), result.getScanRecord().getBytes());
            }

            @Override // android.bluetooth.le.ScanCallback
            public void onBatchScanResults(List<ScanResult> results) {
                for (ScanResult sr : results) {
                    UnityBluetoothLE.this.mLeScanCallback.onLeScan(sr.getDevice(), sr.getRssi(), sr.getScanRecord().getBytes());
                }
            }
        };
        this.api21ScanCallback = scanCallback;
        if (uuids != null) {
            this.mBluetoothAdapter.getBluetoothLeScanner().startScan(scanFilters, settings, scanCallback);
        } else {
            this.mBluetoothAdapter.getBluetoothLeScanner().startScan((List<ScanFilter>) null, settings, scanCallback);
        }
    }

    public void androidBluetoothScanForPeripheralsWithServices(String serviceUUIDsString, boolean rssiOnly, int recordType) {
        this._noManufacturerData = rssiOnly;
        this._recordType = recordType;
        if (this.mBluetoothAdapter != null) {
            ArrayList<UUID> uuidList = new ArrayList<>();
            if (serviceUUIDsString != null) {
                if (serviceUUIDsString.contains("|")) {
                    androidBluetoothLog("serviceUUIDsString : " + serviceUUIDsString);
                    String[] serviceUUIDs = serviceUUIDsString.split("\\|");
                    if (serviceUUIDs != null && serviceUUIDs.length > 0) {
                        for (int i = 0; i < serviceUUIDs.length; i++) {
                            androidBluetoothLog("serviceUUID : " + serviceUUIDs[i]);
                            if (serviceUUIDs[i] != null && serviceUUIDs[i].length() >= 4) {
                                uuidList.add(getFullBluetoothLEUUID(serviceUUIDs[i]));
                            }
                        }
                    }
                } else if (serviceUUIDsString.length() > 0) {
                    uuidList.add(getFullBluetoothLEUUID(serviceUUIDsString));
                }
            }
            if (uuidList.size() > 0) {
                UUID[] uuids = (UUID[]) uuidList.toArray(new UUID[uuidList.size()]);
                for (UUID uuid : uuids) {
                    androidBluetoothLog("Scanning for: " + uuid.toString());
                }
                if (Build.VERSION.SDK_INT >= 21) {
                    androidBluetoothLog("Using api21scan");
                    api21Scan(uuidList);
                    return;
                } else {
                    androidBluetoothLog("Using legacy scan");
                    this.mBluetoothAdapter.startLeScan(uuids, this.mLeScanCallback);
                    return;
                }
            }
            if (Build.VERSION.SDK_INT >= 21) {
                androidBluetoothLog("Using api21scan");
                api21Scan(uuidList);
            } else {
                androidBluetoothLog("Using legacy scan");
                this.mBluetoothAdapter.startLeScan(this.mLeScanCallback);
            }
        }
    }

    @TargetApi(21)
    private void api21StopScan() {
        if (this.api21ScanCallback != null) {
            this.mBluetoothAdapter.getBluetoothLeScanner().stopScan(this.api21ScanCallback);
        }
    }

    public void androidBluetoothStopScan() {
        if (this.mBluetoothAdapter != null) {
            if (Build.VERSION.SDK_INT >= 21) {
                api21StopScan();
            } else {
                this.mBluetoothAdapter.stopLeScan(this.mLeScanCallback);
            }
        }
    }

    public void androidBluetoothRetrieveListOfPeripheralsWithServices(String serviceUUIDsString) {
        if (this.mBluetoothAdapter != null) {
            this.uuidList = new ArrayList<>();
            if (serviceUUIDsString.contains("|")) {
                String[] serviceUUIDs = serviceUUIDsString.split("|");
                if (serviceUUIDs != null && serviceUUIDs.length > 0) {
                    for (String str : serviceUUIDs) {
                        this.uuidList.add(getFullBluetoothLEUUID(str));
                    }
                }
            } else if (serviceUUIDsString.length() > 0) {
                this.uuidList.add(getFullBluetoothLEUUID(serviceUUIDsString));
            }
            androidBluetoothLog("getting bonded devices");
            UnityPlayer.currentActivity.runOnUiThread(new Runnable() { // from class: com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE.4
                @Override // java.lang.Runnable
                public void run() {
                    Set<BluetoothDevice> deviceSet = UnityBluetoothLE.this.mBluetoothAdapter.getBondedDevices();
                    if (deviceSet != null && deviceSet.size() > 0) {
                        for (BluetoothDevice device : deviceSet) {
                            if (device != null) {
                                UnityBluetoothLE.this.androidBluetoothLog("got device " + device.getAddress());
                                if (UnityBluetoothLE.this.uuidList.size() <= 0) {
                                    UnityBluetoothLE.this.sendDiscoveredDevice(device, 0, null);
                                } else {
                                    device.fetchUuidsWithSdp();
                                }
                            }
                        }
                    }
                }
            });
        }
    }

    public void androidBluetoothConnectToPeripheral(String name) {
        final BluetoothDevice device;
        if (this.mBluetoothAdapter != null && this.deviceMap != null && this.mContext != null && (device = this.deviceMap.get(name)) != null) {
            UnityPlayer.currentActivity.runOnUiThread(new Runnable() { // from class: com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE.6
                @Override // java.lang.Runnable
                public void run() {
                    device.connectGatt(UnityBluetoothLE.this.mContext, false, UnityBluetoothLE.this.mGattCallback);
                }
            });
        }
    }

    public void androidBluetoothDisconnectPeripheral(String name) {
        BluetoothGatt gatt;
        if (this.mBluetoothAdapter != null && this.deviceGattMap != null && this.mContext != null && (gatt = this.deviceGattMap.get(name)) != null) {
            gatt.disconnect();
        }
    }

    protected UUID getUUID(String uuidString) {
        if (uuidString.length() == 36) {
            UUID uuid = UUID.fromString(uuidString);
            return uuid;
        }
        if (uuidString.length() > 8) {
            return null;
        }
        StringBuilder newString = new StringBuilder();
        newString.append((CharSequence) "00000000", 0, 8 - uuidString.length());
        newString.append(uuidString);
        newString.append("-0000-1000-8000-00805f9b34fb");
        UUID uuid2 = UUID.fromString(newString.toString());
        return uuid2;
    }

    public void androidReadCharacteristic(String name, String serviceString, String characteristicString) {
        BluetoothGatt gatt;
        if (this.mBluetoothAdapter != null && this.deviceGattMap != null && this.mContext != null && (gatt = this.deviceGattMap.get(name)) != null) {
            UUID serviceUUID = getUUID(serviceString);
            if (serviceUUID != null) {
                BluetoothGattService service = gatt.getService(serviceUUID);
                if (service != null) {
                    UUID characteristicUUID = getUUID(characteristicString);
                    if (characteristicUUID != null) {
                        BluetoothGattCharacteristic characteristic = service.getCharacteristic(characteristicUUID);
                        if (characteristic != null) {
                            if (!gatt.readCharacteristic(characteristic)) {
                                UnitySend("Error~Failed to read characteristic");
                                return;
                            }
                            return;
                        }
                        UnitySend("Error~Characteristic not found for Read");
                        return;
                    }
                    UnitySend("Error~Not a Valid Characteristic UUID for Read");
                    return;
                }
                UnitySend("Error~Service not found for Read");
                return;
            }
            UnitySend("Error~Not a Valid Service UUID for Read");
        }
    }

    public void androidWriteCharacteristic(String name, String serviceString, String characteristicString, byte[] data, int length, boolean withResponse) {
        BluetoothGatt gatt;
        if (this.mBluetoothAdapter != null && this.deviceGattMap != null && this.mContext != null && (gatt = this.deviceGattMap.get(name)) != null) {
            UUID serviceUUID = getUUID(serviceString);
            if (serviceUUID != null) {
                BluetoothGattService service = gatt.getService(serviceUUID);
                if (service != null) {
                    UUID characteristicUUID = getUUID(characteristicString);
                    if (characteristicUUID != null) {
                        BluetoothGattCharacteristic characteristic = service.getCharacteristic(characteristicUUID);
                        if (characteristic != null) {
                            androidBluetoothLog("write characteristic");
                            characteristic.setValue(data);
                            characteristic.setWriteType(withResponse ? 2 : 1);
                            if (!gatt.writeCharacteristic(characteristic)) {
                                UnitySend("Error~Failed to write characteristic");
                                return;
                            }
                            return;
                        }
                        UnitySend("Error~Characteristic not found for Write");
                        return;
                    }
                    UnitySend("Error~Not a Valid Characteristic UUID for Write");
                    return;
                }
                UnitySend("Error~Service not found for Write");
                return;
            }
            UnitySend("Error~Not a Valid Service UUID for Write");
        }
    }

    protected void androidSetCharacteristicNotification(String name, String serviceString, String characteristicString, boolean enabled) {
        BluetoothGatt gatt;
        if (this.mBluetoothAdapter != null && this.deviceGattMap != null && this.mContext != null && (gatt = this.deviceGattMap.get(name)) != null) {
            UUID serviceUUID = getUUID(serviceString);
            if (serviceUUID != null) {
                BluetoothGattService service = gatt.getService(serviceUUID);
                if (service != null) {
                    UUID characteristicUUID = getUUID(characteristicString);
                    if (characteristicUUID != null) {
                        BluetoothGattCharacteristic characteristic = service.getCharacteristic(characteristicUUID);
                        if (characteristic != null) {
                            if (gatt.setCharacteristicNotification(characteristic, true)) {
                                BluetoothGattDescriptor descriptor = characteristic.getDescriptor(CHARACTERISTIC_UPDATE_NOTIFICATION_DESCRIPTOR_UUID);
                                if (descriptor != null) {
                                    int characteristicProperties = characteristic.getProperties();
                                    byte[] valueToSend = BluetoothGattDescriptor.DISABLE_NOTIFICATION_VALUE;
                                    if (enabled) {
                                        if ((characteristicProperties & 16) == 16) {
                                            valueToSend = BluetoothGattDescriptor.ENABLE_NOTIFICATION_VALUE;
                                        } else if ((characteristicProperties & 32) == 32) {
                                            valueToSend = BluetoothGattDescriptor.ENABLE_INDICATION_VALUE;
                                        }
                                    }
                                    if (valueToSend.length > 0) {
                                        androidBluetoothLog("Notification Description: " + ((int) valueToSend[0]));
                                        descriptor.setValue(valueToSend);
                                        if (!gatt.writeDescriptor(descriptor)) {
                                            UnitySend("Error~Failed to write characteristic descriptor");
                                            return;
                                        } else {
                                            androidBluetoothLog("Notification setup succeeded");
                                            return;
                                        }
                                    }
                                    UnitySend("Error~Failed to set notification type");
                                    return;
                                }
                                UnitySend("Error~Failed to get notification descriptor");
                                return;
                            }
                            UnitySend("Error~Failed to set characteristic notification");
                            return;
                        }
                        UnitySend("Error~Characteristic not found for Subscribe");
                        return;
                    }
                    UnitySend("Error~Not a Valid Characteristic UUID for Subscribe");
                    return;
                }
                UnitySend("Error~Service not found for Subscribe");
                return;
            }
            UnitySend("Error~Not a Valid Service UUID for Subscribe");
        }
    }

    public void androidSubscribeCharacteristic(String name, String serviceString, String characteristicString) {
        androidBluetoothLog("subscribe characteristic");
        androidSetCharacteristicNotification(name, serviceString, characteristicString, true);
    }

    public void androidUnsubscribeCharacteristic(String name, String serviceString, String characteristicString) {
        androidBluetoothLog("unsubscribe characteristic");
        androidSetCharacteristicNotification(name, serviceString, characteristicString, false);
    }

    public void androidBluetoothDisconnectAll() {
        if (this.mBluetoothAdapter != null && this.deviceGattMap != null && this.mContext != null) {
            for (String key : this.deviceGattMap.keySet()) {
                BluetoothGatt gatt = this.deviceGattMap.get(key);
                if (gatt != null) {
                    gatt.disconnect();
                }
            }
        }
    }

    public class UnityGattServices extends BluetoothGattServerCallback {
        public UnityGattServices() {
        }

        public void setupServices(BluetoothGattServer gattServer, ArrayList<ServiceCharacteristicMap> serviceList) {
            if (gattServer != null && serviceList != null) {
                gattServer.clearServices();
                for (int i = 0; i < serviceList.size(); i++) {
                    gattServer.addService(serviceList.get(i).Service);
                }
            }
        }

        @Override // android.bluetooth.BluetoothGattServerCallback
        public void onServiceAdded(int status, BluetoothGattService service) {
            if (status == 0) {
                Log.d(UnityBluetoothLE.TAG, "onServiceAdded status=GATT_SUCCESS service=" + service.getUuid().toString());
            } else {
                Log.d(UnityBluetoothLE.TAG, "onServiceAdded status!=GATT_SUCCESS");
            }
        }

        @Override // android.bluetooth.BluetoothGattServerCallback
        public void onConnectionStateChange(BluetoothDevice device, int status, int newState) {
            Log.d(UnityBluetoothLE.TAG, "onConnectionStateChange status=" + status + "->" + newState);
        }

        @Override // android.bluetooth.BluetoothGattServerCallback
        public void onCharacteristicReadRequest(BluetoothDevice device, int requestId, int offset, BluetoothGattCharacteristic characteristic) {
            Log.d(UnityBluetoothLE.TAG, "onCharacteristicReadRequest requestId=" + requestId + " offset=" + offset);
            String uuid = characteristic.getUuid().toString();
            BluetoothGattCharacteristic myCharacteristic = UnityBluetoothLE.this.findCharacteristic(uuid);
            if (myCharacteristic != null) {
                characteristic.setValue(myCharacteristic.getValue());
            }
            UnityBluetoothLE.this.mBluetoothGattServer.sendResponse(device, requestId, 0, offset, characteristic.getValue());
        }

        @Override // android.bluetooth.BluetoothGattServerCallback
        public void onCharacteristicWriteRequest(BluetoothDevice device, int requestId, BluetoothGattCharacteristic characteristic, boolean preparedWrite, boolean responseNeeded, int offset, byte[] value) {
            Log.d(UnityBluetoothLE.TAG, "onCharacteristicWriteRequest requestId=" + requestId + " preparedWrite=" + Boolean.toString(preparedWrite) + " responseNeeded=" + Boolean.toString(responseNeeded) + " offset=" + offset);
            String uuid = characteristic.getUuid().toString();
            BluetoothGattCharacteristic myCharacteristic = UnityBluetoothLE.this.findCharacteristic(uuid);
            if (myCharacteristic != null) {
                myCharacteristic.setValue(value);
                String dataString = Base64.encodeToString(Arrays.copyOfRange(value, 0, value.length), 0);
                UnityBluetoothLE.UnitySend("PeripheralReceivedWriteData~" + uuid + "~" + dataString);
            }
            UnityBluetoothLE.this.mBluetoothGattServer.sendResponse(device, requestId, 0, offset, null);
        }

        @Override // android.bluetooth.BluetoothGattServerCallback
        public void onDescriptorReadRequest(BluetoothDevice device, int requestId, int offset, BluetoothGattDescriptor descriptor) {
            super.onDescriptorReadRequest(device, requestId, offset, descriptor);
            UnityBluetoothLE.this.mBluetoothGattServer.sendResponse(device, requestId, 0, offset, null);
        }

        private void removeDeviceFromNotifyList(ArrayList<BluetoothDevice> notifyDeviceList, BluetoothDevice device) {
            for (int i = 0; i < notifyDeviceList.size(); i++) {
                if (notifyDeviceList.get(i).getAddress().equals(device.getAddress())) {
                    notifyDeviceList.remove(i);
                    return;
                }
            }
        }

        @Override // android.bluetooth.BluetoothGattServerCallback
        public void onDescriptorWriteRequest(BluetoothDevice device, int requestId, BluetoothGattDescriptor descriptor, boolean preparedWrite, boolean responseNeeded, int offset, byte[] value) {
            super.onDescriptorWriteRequest(device, requestId, descriptor, preparedWrite, responseNeeded, offset, value);
            if (descriptor.getUuid().equals(UUID.fromString(UnityBluetoothLE.this.notificationDescriptionUUID))) {
                UUID uuid = descriptor.getCharacteristic().getUuid();
                if (UnityBluetoothLE.this.notifyDeviceMap == null) {
                    UnityBluetoothLE.this.notifyDeviceMap = new HashMap();
                }
                if (!UnityBluetoothLE.this.notifyDeviceMap.containsKey(uuid)) {
                    UnityBluetoothLE.this.notifyDeviceMap.put(uuid, new ArrayList());
                }
                ArrayList<BluetoothDevice> notifyDeviceList = (ArrayList) UnityBluetoothLE.this.notifyDeviceMap.get(uuid);
                if (Arrays.equals(value, BluetoothGattDescriptor.ENABLE_NOTIFICATION_VALUE)) {
                    removeDeviceFromNotifyList(notifyDeviceList, device);
                    notifyDeviceList.add(device);
                } else if (Arrays.equals(value, BluetoothGattDescriptor.DISABLE_NOTIFICATION_VALUE)) {
                    removeDeviceFromNotifyList(notifyDeviceList, device);
                }
            }
            UnityBluetoothLE.this.mBluetoothGattServer.sendResponse(device, requestId, 0, offset, value);
        }
    }

    private class ServiceCharacteristicMap {
        public BluetoothGattService Service;
        public String UUID;
        public Map<String, BluetoothGattCharacteristic> characteristicMap;

        private ServiceCharacteristicMap() {
            this.UUID = "";
            this.Service = null;
            this.characteristicMap = null;
        }
    }

    public void androidPeripheralName(String newName) {
        this.mBluetoothAdapter.setName(newName);
    }

    public void androidCreateService(String uuid, boolean primary) {
        String uuid2 = uuid.toLowerCase();
        BluetoothGattService service = new BluetoothGattService(UUID.fromString(uuid2), primary ? 0 : 1);
        if (primary) {
            this.primaryServiceUUID = uuid2;
        }
        if (service != null) {
            if (this.serviceList == null) {
                this.serviceList = new ArrayList<>();
            }
            ServiceCharacteristicMap serviceCharacteristicMap = new ServiceCharacteristicMap();
            serviceCharacteristicMap.UUID = uuid2;
            serviceCharacteristicMap.Service = service;
            serviceCharacteristicMap.characteristicMap = new HashMap();
            for (int i = 0; i < this.characteristList.size(); i++) {
                serviceCharacteristicMap.characteristicMap.put(this.characteristList.get(i).getUuid().toString(), this.characteristList.get(i));
                serviceCharacteristicMap.Service.addCharacteristic(this.characteristList.get(i));
            }
            this.serviceList.add(serviceCharacteristicMap);
            this.characteristList.clear();
            return;
        }
        UnitySend("Error~Cannot create Gatt Service object while creating a service");
    }

    private ServiceCharacteristicMap findService(String uuid) {
        if (this.serviceList == null) {
            return null;
        }
        String uuid2 = uuid.toLowerCase();
        for (int i = 0; i < this.serviceList.size(); i++) {
            if (this.serviceList.get(i).UUID.compareTo(uuid2) == 0) {
                ServiceCharacteristicMap serviceCharacteristicMap = this.serviceList.get(i);
                return serviceCharacteristicMap;
            }
        }
        return null;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public BluetoothGattCharacteristic findCharacteristic(String uuid) {
        if (this.serviceList == null) {
            return null;
        }
        String uuid2 = uuid.toLowerCase();
        for (int i = 0; i < this.serviceList.size(); i++) {
            if (this.serviceList.get(i).characteristicMap != null && this.serviceList.get(i).characteristicMap.containsKey(uuid2)) {
                BluetoothGattCharacteristic characteristic = this.serviceList.get(i).characteristicMap.get(uuid2);
                return characteristic;
            }
        }
        return null;
    }

    public void androidRemoveService(String uuid) {
        ServiceCharacteristicMap serviceCharacteristicMap;
        if (this.serviceList != null && (serviceCharacteristicMap = findService(uuid)) != null) {
            this.serviceList.remove(serviceCharacteristicMap);
        }
    }

    public void androidRemoveServices() {
        this.serviceList = null;
    }

    public void androidCreateCharacteristic(String uuid, int properties, int permissions, byte[] data, int length) {
        String uuid2 = uuid.toLowerCase();
        if (this.characteristList == null) {
            this.characteristList = new ArrayList<>();
        }
        androidRemoveCharacteristic(uuid2);
        BluetoothGattCharacteristic gattCharacteristic = new BluetoothGattCharacteristic(UUID.fromString(uuid2), properties, permissions);
        if ((permissions & 16) == 16) {
            BluetoothGattDescriptor gattDescriptor = new BluetoothGattDescriptor(UUID.fromString(this.notificationDescriptionUUID), 17);
            gattCharacteristic.addDescriptor(gattDescriptor);
        }
        gattCharacteristic.setValue(data);
        this.characteristList.add(gattCharacteristic);
    }

    public void androidRemoveCharacteristic(String uuid) {
        if (this.characteristList != null) {
            String uuid2 = uuid.toLowerCase();
            for (int i = 0; i < this.characteristList.size(); i++) {
                if (this.characteristList.get(i).getUuid().compareTo(UUID.fromString(uuid2)) == 0) {
                    this.characteristList.remove(i);
                    return;
                }
            }
        }
    }

    public void androidRemoveCharacteristics() {
        this.characteristList = null;
    }

    public void androidStartAdvertising() {
        BluetoothManager bluetoothManager;
        if (Build.VERSION.SDK_INT >= 21) {
            if (this.mBluetoothAdvertiser == null) {
                this.mBluetoothAdvertiser = this.mBluetoothAdapter.getBluetoothLeAdvertiser();
            }
            if (this.mBluetoothGattServer == null && (bluetoothManager = (BluetoothManager) this.mContext.getSystemService("bluetooth")) != null) {
                this.mUnityGattServices = new UnityGattServices();
                this.mBluetoothGattServer = bluetoothManager.openGattServer(this.mContext, this.mUnityGattServices);
                this.mUnityGattServices.setupServices(this.mBluetoothGattServer, this.serviceList);
            }
            AdvertiseSettings settings = new AdvertiseSettings.Builder().setAdvertiseMode(1).setTxPowerLevel(2).setConnectable(true).build();
            new ParcelUuid(UUID.fromString(this.primaryServiceUUID));
            AdvertiseData data = new AdvertiseData.Builder().setIncludeDeviceName(true).build();
            this.advertisingCallback = new AdvertiseCallback() { // from class: com.shatalmic.unityandroidbluetoothlelib.UnityBluetoothLE.7
                @Override // android.bluetooth.le.AdvertiseCallback
                public void onStartSuccess(AdvertiseSettings settingsInEffect) {
                    super.onStartSuccess(settingsInEffect);
                    UnityBluetoothLE.UnitySend("StartedAdvertising");
                }

                @Override // android.bluetooth.le.AdvertiseCallback
                public void onStartFailure(int errorCode) {
                    super.onStartFailure(errorCode);
                    UnityBluetoothLE.UnitySend("Error~Advertising onStartFailure: " + errorCode);
                }
            };
            this.mBluetoothAdvertiser = this.mBluetoothAdapter.getBluetoothLeAdvertiser();
            this.mBluetoothAdvertiser.startAdvertising(settings, data, this.advertisingCallback);
            return;
        }
        UnitySend("Error~Failed to start advertising");
    }

    public void androidStopAdvertising() {
        if (Build.VERSION.SDK_INT >= 21 && this.mBluetoothAdvertiser != null && this.advertisingCallback != null) {
            if (this.mBluetoothGattServer != null) {
                this.mBluetoothGattServer.clearServices();
                this.mBluetoothGattServer.close();
                this.mBluetoothGattServer = null;
            }
            if (this.mBluetoothAdvertiser != null) {
                this.mBluetoothAdvertiser.stopAdvertising(this.advertisingCallback);
                this.mBluetoothAdvertiser = null;
            }
            UnitySend("StoppedAdvertising");
            return;
        }
        UnitySend("Error~Failed to stop advertising");
    }

    public void androidUpdateCharacteristicValue(String uuid, byte[] data, int length) {
        String uuid2 = uuid.toLowerCase();
        BluetoothGattCharacteristic gattCharacteristic = findCharacteristic(uuid2);
        if (gattCharacteristic != null) {
            gattCharacteristic.setValue(data);
            if (this.notifyDeviceMap != null && this.notifyDeviceMap.containsKey(UUID.fromString(uuid2))) {
                ArrayList<BluetoothDevice> notifyDeviceList = this.notifyDeviceMap.get(UUID.fromString(uuid2));
                for (int i = 0; i < notifyDeviceList.size(); i++) {
                    this.mBluetoothGattServer.notifyCharacteristicChanged(notifyDeviceList.get(i), gattCharacteristic, false);
                }
                return;
            }
            UnitySend("Error~Characteristic not found when updating value (2)");
            return;
        }
        UnitySend("Error~Characteristic not found when updating value (1)");
    }
}
