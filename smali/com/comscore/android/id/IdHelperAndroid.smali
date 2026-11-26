.class public Lcom/comscore/android/id/IdHelperAndroid;
.super Ljava/lang/Object;


# static fields
.field public static final INVALID_ID_VALUES:[Ljava/lang/String;

.field public static final NO_ID_AVAILABLE:Ljava/lang/String; = "none"

.field private static final a:Ljava/lang/String;

.field private static final b:Ljava/lang/String;

.field private static c:Z

.field private static final d:Z


# direct methods
.method static constructor <clinit>()V
    .locals 4

    const/4 v3, 0x0

    const/16 v0, 0x8

    new-array v0, v0, [Ljava/lang/String;

    const-string v1, "0123456789ABCDEF"

    aput-object v1, v0, v3

    const/4 v1, 0x1

    const-string v2, "0123456789abcdef"

    aput-object v2, v0, v1

    const/4 v1, 0x2

    const-string v2, "9774d56d682e549c"

    aput-object v2, v0, v1

    const/4 v1, 0x3

    const-string v2, "9774D56D682E549C"

    aput-object v2, v0, v1

    const/4 v1, 0x4

    const-string v2, "unknown"

    aput-object v2, v0, v1

    const/4 v1, 0x5

    const-string v2, "UNKNOWN"

    aput-object v2, v0, v1

    const/4 v1, 0x6

    const-string v2, "android_id"

    aput-object v2, v0, v1

    const/4 v1, 0x7

    const-string v2, "ANDROID_ID"

    aput-object v2, v0, v1

    sput-object v0, Lcom/comscore/android/id/IdHelperAndroid;->INVALID_ID_VALUES:[Ljava/lang/String;

    sput-boolean v3, Lcom/comscore/android/id/IdHelperAndroid;->c:Z

    const-string v0, "com.google.android.gms"

    sput-object v0, Lcom/comscore/android/id/IdHelperAndroid;->a:Ljava/lang/String;

    const-string v0, "com.google.android.gms.ads.identifier.service.START"

    sput-object v0, Lcom/comscore/android/id/IdHelperAndroid;->b:Ljava/lang/String;

    sput-boolean v3, Lcom/comscore/android/id/IdHelperAndroid;->d:Z

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method private static a(Landroid/content/Context;)Ljava/lang/String;
    .locals 4

    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    if-ne v0, v1, :cond_0

    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Cannot be called from the main thread"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_0
    const-string v0, ""

    new-instance v1, Lcom/comscore/android/id/b;

    const/4 v2, 0x0

    invoke-direct {v1, v2}, Lcom/comscore/android/id/b;-><init>(Lcom/comscore/android/id/a;)V

    new-instance v2, Landroid/content/Intent;

    sget-object v3, Lcom/comscore/android/id/IdHelperAndroid;->b:Ljava/lang/String;

    invoke-direct {v2, v3}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    sget-object v3, Lcom/comscore/android/id/IdHelperAndroid;->a:Ljava/lang/String;

    invoke-virtual {v2, v3}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    const/4 v3, 0x1

    invoke-virtual {p0, v2, v1, v3}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result v2

    if-eqz v2, :cond_1

    :try_start_0
    new-instance v2, Lcom/comscore/android/id/c;

    invoke-virtual {v1}, Lcom/comscore/android/id/b;->getBinder()Landroid/os/IBinder;

    move-result-object v3

    invoke-direct {v2, v3}, Lcom/comscore/android/id/c;-><init>(Landroid/os/IBinder;)V

    invoke-virtual {v2}, Lcom/comscore/android/id/c;->getId()Ljava/lang/String;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result-object v0

    invoke-virtual {p0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    :cond_1
    :goto_0
    return-object v0

    :catch_0
    move-exception v2

    invoke-virtual {p0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    goto :goto_0

    :catchall_0
    move-exception v0

    invoke-virtual {p0, v1}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    throw v0
.end method

.method private static b(Landroid/content/Context;)Z
    .locals 2

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v1, 0x4

    if-le v0, v1, :cond_1

    sget-boolean v0, Lcom/comscore/android/id/IdHelperAndroid;->d:Z

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    invoke-static {p0}, Lcom/comscore/android/id/API4;->isPackageInstalledFromGooglePlayStore(Landroid/content/Context;)Z

    move-result v0

    goto :goto_0

    :cond_1
    const/4 v0, 0x0

    goto :goto_0
.end method

.method public static checkAndroidId(Ljava/lang/String;)Z
    .locals 3

    const/4 v1, 0x0

    move v0, v1

    :goto_0
    sget-object v2, Lcom/comscore/android/id/IdHelperAndroid;->INVALID_ID_VALUES:[Ljava/lang/String;

    array-length v2, v2

    if-ge v0, v2, :cond_1

    sget-object v2, Lcom/comscore/android/id/IdHelperAndroid;->INVALID_ID_VALUES:[Ljava/lang/String;

    aget-object v2, v2, v0

    invoke-virtual {v2, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    :goto_1
    return v1

    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    const/4 v1, 0x1

    goto :goto_1
.end method

.method public static checkAndroidSerial(Ljava/lang/String;)Z
    .locals 4

    const/4 v3, 0x3

    const/4 v1, 0x0

    if-nez p0, :cond_1

    :cond_0
    :goto_0
    return v1

    :cond_1
    move v0, v1

    :goto_1
    sget-object v2, Lcom/comscore/android/id/IdHelperAndroid;->INVALID_ID_VALUES:[Ljava/lang/String;

    array-length v2, v2

    if-ge v0, v2, :cond_2

    sget-object v2, Lcom/comscore/android/id/IdHelperAndroid;->INVALID_ID_VALUES:[Ljava/lang/String;

    aget-object v2, v2, v0

    invoke-virtual {v2, p0}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_0

    add-int/lit8 v0, v0, 0x1

    goto :goto_1

    :cond_2
    invoke-virtual {p0}, Ljava/lang/String;->length()I

    move-result v0

    if-le v0, v3, :cond_0

    invoke-virtual {p0, v1, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v0

    const-string v2, "***"

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    invoke-virtual {p0, v1, v3}, Ljava/lang/String;->substring(II)Ljava/lang/String;

    move-result-object v0

    const-string v2, "000"

    invoke-virtual {v0, v2}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v0

    if-nez v0, :cond_0

    const/4 v1, 0x1

    goto :goto_0
.end method

.method public static final getAdvertisingDeviceId(Landroid/content/Context;)Lcom/comscore/android/id/DeviceId;
    .locals 2

    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    if-ne v0, v1, :cond_0

    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Cannot be called from the main thread"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_0
    invoke-static {p0}, Lcom/comscore/android/id/IdHelperAndroid;->b(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-static {p0}, Lcom/comscore/android/id/IdHelperAndroid;->getGooglePlayAdvertisingDeviceId(Landroid/content/Context;)Lcom/comscore/android/id/DeviceId;

    move-result-object v0

    :goto_0
    return-object v0

    :cond_1
    invoke-static {p0}, Lcom/comscore/android/id/IdHelperAndroid;->getDeviceId(Landroid/content/Context;)Lcom/comscore/android/id/DeviceId;

    move-result-object v0

    goto :goto_0
.end method

.method public static final getAndroidId(Landroid/content/Context;)Lcom/comscore/android/id/DeviceId;
    .locals 5

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    const/4 v1, 0x3

    if-lt v0, v1, :cond_0

    invoke-virtual {p0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v0

    const-string v1, "android_id"

    invoke-static {v0, v1}, Landroid/provider/Settings$Secure;->getString(Landroid/content/ContentResolver;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    if-eqz v1, :cond_0

    invoke-virtual {v1}, Ljava/lang/String;->length()I

    move-result v0

    if-lez v0, :cond_0

    const/4 v2, 0x7

    const/4 v3, 0x2

    new-instance v0, Lcom/comscore/android/id/DeviceId;

    const-string v4, "AndroidId"

    invoke-direct {v0, v4, v1, v2, v3}, Lcom/comscore/android/id/DeviceId;-><init>(Ljava/lang/String;Ljava/lang/String;II)V

    :goto_0
    return-object v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method public static final getAndroidSerial()Lcom/comscore/android/id/DeviceId;
    .locals 5

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    invoke-static {v0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/Integer;->intValue()I

    move-result v0

    const/16 v1, 0x9

    if-lt v0, v1, :cond_0

    invoke-static {}, Lcom/comscore/android/id/API9;->getAndroidSerial()Ljava/lang/String;

    move-result-object v1

    const/4 v2, 0x3

    const/4 v3, 0x1

    new-instance v0, Lcom/comscore/android/id/DeviceId;

    const-string v4, "AndroidSerial"

    invoke-direct {v0, v4, v1, v2, v3}, Lcom/comscore/android/id/DeviceId;-><init>(Ljava/lang/String;Ljava/lang/String;II)V

    :goto_0
    return-object v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method public static getDeviceId(Landroid/content/Context;)Lcom/comscore/android/id/DeviceId;
    .locals 2

    invoke-static {}, Lcom/comscore/android/id/IdHelperAndroid;->getAndroidSerial()Lcom/comscore/android/id/DeviceId;

    move-result-object v0

    invoke-virtual {v0}, Lcom/comscore/android/id/DeviceId;->getId()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/comscore/android/id/IdHelperAndroid;->checkAndroidSerial(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    invoke-static {p0}, Lcom/comscore/android/id/IdHelperAndroid;->getAndroidId(Landroid/content/Context;)Lcom/comscore/android/id/DeviceId;

    move-result-object v0

    invoke-virtual {v0}, Lcom/comscore/android/id/DeviceId;->getId()Ljava/lang/String;

    move-result-object v1

    invoke-static {v1}, Lcom/comscore/android/id/IdHelperAndroid;->checkAndroidId(Ljava/lang/String;)Z

    move-result v1

    if-nez v1, :cond_0

    const/4 v0, 0x0

    :cond_0
    return-object v0
.end method

.method public static final getGooglePlayAdvertisingDeviceId(Landroid/content/Context;)Lcom/comscore/android/id/DeviceId;
    .locals 2

    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v0

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v1

    if-ne v0, v1, :cond_0

    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Cannot be called from the main thread"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_0
    :try_start_0
    invoke-static {p0}, Lcom/comscore/android/id/IdHelperAndroid;->isGooglePlayServicesAvailable(Landroid/content/Context;)Z
    :try_end_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_0

    move-result v0

    if-eqz v0, :cond_2

    invoke-static {p0}, Lcom/comscore/android/id/IdHelperAndroid;->isAdvertisingIdEnabled(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_1

    invoke-static {p0}, Lcom/comscore/android/id/IdHelperAndroid;->a(Landroid/content/Context;)Ljava/lang/String;

    move-result-object v0

    :goto_0
    new-instance v1, Lcom/comscore/android/id/DeviceId;

    invoke-direct {v1, v0}, Lcom/comscore/android/id/DeviceId;-><init>(Ljava/lang/String;)V

    move-object v0, v1

    :goto_1
    return-object v0

    :catch_0
    move-exception v0

    throw v0

    :cond_1
    const-string v0, "none"

    goto :goto_0

    :cond_2
    const/4 v0, 0x0

    goto :goto_1
.end method

.method public static isAdvertisingIdEnabled(Landroid/content/Context;)Z
    .locals 5

    const/4 v0, 0x0

    const/4 v1, 0x1

    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v2

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v3

    if-ne v2, v3, :cond_0

    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Cannot be called from the main thread"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_0
    sget-boolean v2, Lcom/comscore/android/id/IdHelperAndroid;->c:Z

    if-eqz v2, :cond_2

    :cond_1
    :goto_0
    return v0

    :cond_2
    new-instance v2, Lcom/comscore/android/id/b;

    const/4 v3, 0x0

    invoke-direct {v2, v3}, Lcom/comscore/android/id/b;-><init>(Lcom/comscore/android/id/a;)V

    new-instance v3, Landroid/content/Intent;

    sget-object v4, Lcom/comscore/android/id/IdHelperAndroid;->b:Ljava/lang/String;

    invoke-direct {v3, v4}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    sget-object v4, Lcom/comscore/android/id/IdHelperAndroid;->a:Ljava/lang/String;

    invoke-virtual {v3, v4}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {p0, v3, v2, v1}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result v3

    if-eqz v3, :cond_4

    :try_start_0
    new-instance v3, Lcom/comscore/android/id/c;

    invoke-virtual {v2}, Lcom/comscore/android/id/b;->getBinder()Landroid/os/IBinder;

    move-result-object v4

    invoke-direct {v3, v4}, Lcom/comscore/android/id/c;-><init>(Landroid/os/IBinder;)V

    const/4 v4, 0x1

    invoke-virtual {v3, v4}, Lcom/comscore/android/id/c;->isLimitAdTrackingEnabled(Z)Z
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    move-result v3

    if-nez v3, :cond_3

    move v0, v1

    :cond_3
    invoke-virtual {p0, v2}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    :cond_4
    :goto_1
    if-nez v0, :cond_1

    sput-boolean v1, Lcom/comscore/android/id/IdHelperAndroid;->c:Z

    goto :goto_0

    :catch_0
    move-exception v3

    invoke-virtual {p0, v2}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    goto :goto_1

    :catchall_0
    move-exception v0

    invoke-virtual {p0, v2}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    throw v0
.end method

.method public static isGooglePlayServicesAvailable(Landroid/content/Context;)Z
    .locals 5

    const/4 v1, 0x1

    const/4 v0, 0x0

    invoke-static {}, Landroid/os/Looper;->myLooper()Landroid/os/Looper;

    move-result-object v2

    invoke-static {}, Landroid/os/Looper;->getMainLooper()Landroid/os/Looper;

    move-result-object v3

    if-ne v2, v3, :cond_0

    new-instance v0, Ljava/lang/IllegalStateException;

    const-string v1, "Cannot be called from the main thread"

    invoke-direct {v0, v1}, Ljava/lang/IllegalStateException;-><init>(Ljava/lang/String;)V

    throw v0

    :cond_0
    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v3, 0x8

    if-gt v2, v3, :cond_2

    :cond_1
    :goto_0
    return v0

    :cond_2
    :try_start_0
    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v2

    const-string v3, "com.android.vending"

    const/4 v4, 0x0

    invoke-virtual {v2, v3, v4}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    new-instance v2, Lcom/comscore/android/id/b;

    const/4 v3, 0x0

    invoke-direct {v2, v3}, Lcom/comscore/android/id/b;-><init>(Lcom/comscore/android/id/a;)V

    new-instance v3, Landroid/content/Intent;

    sget-object v4, Lcom/comscore/android/id/IdHelperAndroid;->b:Ljava/lang/String;

    invoke-direct {v3, v4}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    sget-object v4, Lcom/comscore/android/id/IdHelperAndroid;->a:Ljava/lang/String;

    invoke-virtual {v3, v4}, Landroid/content/Intent;->setPackage(Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {p0, v3, v2, v1}, Landroid/content/Context;->bindService(Landroid/content/Intent;Landroid/content/ServiceConnection;I)Z

    move-result v3

    if-eqz v3, :cond_1

    invoke-virtual {p0, v2}, Landroid/content/Context;->unbindService(Landroid/content/ServiceConnection;)V

    move v0, v1

    goto :goto_0

    :catch_0
    move-exception v1

    goto :goto_0
.end method

.method public static md5(Ljava/lang/String;)Ljava/lang/String;
    .locals 6

    :try_start_0
    const-string v0, "MD5"

    invoke-static {v0}, Ljava/security/MessageDigest;->getInstance(Ljava/lang/String;)Ljava/security/MessageDigest;

    move-result-object v0

    const-string v1, "UTF-8"

    invoke-virtual {p0, v1}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/security/MessageDigest;->digest([B)[B
    :try_end_0
    .catch Ljava/security/NoSuchAlgorithmException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_1

    move-result-object v1

    new-instance v2, Ljava/lang/StringBuilder;

    array-length v0, v1

    mul-int/lit8 v0, v0, 0x2

    invoke-direct {v2, v0}, Ljava/lang/StringBuilder;-><init>(I)V

    array-length v3, v1

    const/4 v0, 0x0

    :goto_0
    if-ge v0, v3, :cond_1

    aget-byte v4, v1, v0

    and-int/lit16 v4, v4, 0xff

    const/16 v5, 0x10

    if-ge v4, v5, :cond_0

    const/16 v5, 0x30

    invoke-virtual {v2, v5}, Ljava/lang/StringBuilder;->append(C)Ljava/lang/StringBuilder;

    :cond_0
    invoke-static {v4}, Ljava/lang/Integer;->toHexString(I)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :catch_0
    move-exception v0

    new-instance v1, Ljava/lang/RuntimeException;

    const-string v2, "Huh, MD5 should be supported?"

    invoke-direct {v1, v2, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    :catch_1
    move-exception v0

    new-instance v1, Ljava/lang/RuntimeException;

    const-string v2, "Huh, UTF-8 should be supported?"

    invoke-direct {v1, v2, v0}, Ljava/lang/RuntimeException;-><init>(Ljava/lang/String;Ljava/lang/Throwable;)V

    throw v1

    :cond_1
    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
