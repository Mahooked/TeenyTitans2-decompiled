.class public final Lcom/amazon/android/l/b;
.super Ljava/lang/Object;


# static fields
.field private static final a:Lcom/amazon/android/framework/util/KiwiLogger;

.field private static final e:[[B

.field private static final f:[[B


# instance fields
.field private final b:Ljava/security/cert/PKIXParameters;

.field private final c:Ljava/security/cert/CertPathValidator;

.field private final d:Ljava/util/Set;


# direct methods
.method static constructor <clinit>()V
    .locals 8

    const/4 v7, 0x3

    const/4 v6, 0x2

    const/4 v5, 0x1

    const/4 v4, 0x0

    const/16 v3, 0x14

    new-instance v0, Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "CertVerifier"

    invoke-direct {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;-><init>(Ljava/lang/String;)V

    sput-object v0, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const/16 v0, 0xd

    new-array v0, v0, [[B

    new-array v1, v3, [B

    fill-array-data v1, :array_0

    aput-object v1, v0, v4

    new-array v1, v3, [B

    fill-array-data v1, :array_1

    aput-object v1, v0, v5

    new-array v1, v3, [B

    fill-array-data v1, :array_2

    aput-object v1, v0, v6

    new-array v1, v3, [B

    fill-array-data v1, :array_3

    aput-object v1, v0, v7

    const/4 v1, 0x4

    new-array v2, v3, [B

    fill-array-data v2, :array_4

    aput-object v2, v0, v1

    const/4 v1, 0x5

    new-array v2, v3, [B

    fill-array-data v2, :array_5

    aput-object v2, v0, v1

    const/4 v1, 0x6

    new-array v2, v3, [B

    fill-array-data v2, :array_6

    aput-object v2, v0, v1

    const/4 v1, 0x7

    new-array v2, v3, [B

    fill-array-data v2, :array_7

    aput-object v2, v0, v1

    const/16 v1, 0x8

    new-array v2, v3, [B

    fill-array-data v2, :array_8

    aput-object v2, v0, v1

    const/16 v1, 0x9

    new-array v2, v3, [B

    fill-array-data v2, :array_9

    aput-object v2, v0, v1

    const/16 v1, 0xa

    new-array v2, v3, [B

    fill-array-data v2, :array_a

    aput-object v2, v0, v1

    const/16 v1, 0xb

    new-array v2, v3, [B

    fill-array-data v2, :array_b

    aput-object v2, v0, v1

    const/16 v1, 0xc

    new-array v2, v3, [B

    fill-array-data v2, :array_c

    aput-object v2, v0, v1

    sput-object v0, Lcom/amazon/android/l/b;->e:[[B

    const/4 v0, 0x5

    new-array v0, v0, [[B

    new-array v1, v3, [B

    fill-array-data v1, :array_d

    aput-object v1, v0, v4

    new-array v1, v3, [B

    fill-array-data v1, :array_e

    aput-object v1, v0, v5

    new-array v1, v3, [B

    fill-array-data v1, :array_f

    aput-object v1, v0, v6

    new-array v1, v3, [B

    fill-array-data v1, :array_10

    aput-object v1, v0, v7

    const/4 v1, 0x4

    new-array v2, v3, [B

    fill-array-data v2, :array_11

    aput-object v2, v0, v1

    sput-object v0, Lcom/amazon/android/l/b;->f:[[B

    return-void

    :array_0
    .array-data 1
        -0x7bt
        0x37t
        0x1ct
        -0x5at
        -0x1bt
        0x50t
        0x14t
        0x3dt
        -0x32t
        0x28t
        0x3t
        0x47t
        0x1bt
        -0x22t
        0x3at
        0x9t
        -0x18t
        -0x8t
        0x77t
        0xft
    .end array-data

    :array_1
    .array-data 1
        -0x5ft
        -0x25t
        0x63t
        -0x6dt
        -0x6ft
        0x6ft
        0x17t
        -0x1ct
        0x18t
        0x55t
        0x9t
        0x40t
        0x4t
        0x15t
        -0x39t
        0x2t
        0x40t
        -0x50t
        -0x52t
        0x6bt
    .end array-data

    :array_2
    .array-data 1
        0x4et
        -0x4at
        -0x2bt
        0x78t
        0x49t
        -0x65t
        0x1ct
        -0x31t
        0x5ft
        0x58t
        0x1et
        -0x53t
        0x56t
        -0x42t
        0x3dt
        -0x65t
        0x67t
        0x44t
        -0x5bt
        -0x1bt
    .end array-data

    :array_3
    .array-data 1
        0x13t
        0x2dt
        0xdt
        0x45t
        0x53t
        0x4bt
        0x69t
        -0x69t
        -0x33t
        -0x4et
        -0x2bt
        -0x3dt
        0x39t
        -0x1et
        0x55t
        0x76t
        0x60t
        -0x65t
        0x5ct
        -0x3at
    .end array-data

    :array_4
    .array-data 1
        0x22t
        -0x2bt
        -0x28t
        -0x21t
        -0x71t
        0x2t
        0x31t
        -0x2ft
        -0x73t
        -0x9t
        -0x63t
        -0x49t
        -0x31t
        -0x76t
        0x2dt
        0x64t
        -0x37t
        0x3ft
        0x6ct
        0x3at
    .end array-data

    :array_5
    .array-data 1
        0x61t
        -0x11t
        0x43t
        -0x29t
        0x7ft
        -0x36t
        -0x2ct
        0x61t
        0x51t
        -0x44t
        -0x68t
        -0x20t
        -0x3dt
        0x59t
        0x12t
        -0x51t
        -0x61t
        -0x15t
        0x63t
        0x11t
    .end array-data

    :array_6
    .array-data 1
        -0x4dt
        -0x16t
        -0x3ct
        0x47t
        0x76t
        -0x37t
        -0x38t
        0x1ct
        -0x16t
        -0xet
        -0x63t
        -0x6bt
        -0x4at
        -0x34t
        -0x60t
        0x8t
        0x1bt
        0x67t
        -0x14t
        -0x63t
    .end array-data

    :array_7
    .array-data 1
        -0x70t
        -0x52t
        -0x5et
        0x69t
        -0x7bt
        -0x1t
        0x14t
        -0x80t
        0x4ct
        0x43t
        0x49t
        0x52t
        -0x14t
        -0x17t
        0x60t
        -0x7ct
        0x77t
        -0x51t
        0x55t
        0x6ft
    .end array-data

    :array_8
    .array-data 1
        0x20t
        0x42t
        -0x7bt
        -0x24t
        -0x9t
        -0x15t
        0x76t
        0x41t
        -0x6bt
        0x57t
        -0x72t
        0x13t
        0x6bt
        -0x2ct
        -0x49t
        -0x2ft
        -0x17t
        -0x72t
        0x46t
        -0x5bt
    .end array-data

    :array_9
    .array-data 1
        0x36t
        0x79t
        -0x36t
        0x35t
        0x66t
        -0x79t
        0x72t
        0x30t
        0x4dt
        0x30t
        -0x5bt
        -0x5t
        -0x79t
        0x3bt
        0xft
        -0x59t
        0x7bt
        -0x49t
        0xdt
        0x54t
    .end array-data

    :array_a
    .array-data 1
        -0x38t
        -0x14t
        -0x74t
        -0x79t
        -0x6et
        0x69t
        -0x35t
        0x4bt
        -0x55t
        0x39t
        -0x17t
        -0x73t
        0x7et
        0x57t
        0x67t
        -0xdt
        0x14t
        -0x6bt
        0x73t
        -0x63t
    .end array-data

    :array_b
    .array-data 1
        0x51t
        0x7ft
        0x61t
        0x1et
        0x29t
        -0x6ft
        0x6bt
        0x53t
        -0x7et
        -0x5t
        0x72t
        -0x19t
        0x44t
        -0x27t
        -0x73t
        -0x3dt
        -0x34t
        0x53t
        0x6dt
        0x64t
    .end array-data

    :array_c
    .array-data 1
        0x40t
        -0x4dt
        0x31t
        -0x60t
        -0x17t
        -0x41t
        -0x18t
        0x55t
        -0x44t
        0x39t
        -0x6dt
        -0x36t
        0x70t
        0x4ft
        0x4et
        -0x3et
        0x51t
        -0x2ct
        0x1dt
        -0x71t
    .end array-data

    :array_d
    .array-data 1
        -0x6ft
        -0x3at
        -0x2at
        -0x12t
        0x3et
        -0x76t
        -0x38t
        0x63t
        -0x7ct
        -0x1bt
        0x48t
        -0x3et
        -0x67t
        0x29t
        0x5ct
        0x75t
        0x6ct
        -0x7ft
        0x7bt
        -0x7ft
    .end array-data

    :array_e
    .array-data 1
        -0x20t
        -0x55t
        0x5t
        -0x6ct
        0x20t
        0x72t
        0x54t
        -0x6dt
        0x5t
        0x60t
        0x62t
        0x2t
        0x36t
        0x70t
        -0x9t
        -0x33t
        0x2et
        -0x4t
        0x66t
        0x66t
    .end array-data

    :array_f
    .array-data 1
        -0x61t
        -0x53t
        -0x6ft
        -0x5at
        -0x32t
        0x6at
        -0x3at
        -0x3bt
        0x0t
        0x47t
        -0x3ct
        0x4et
        -0x37t
        -0x2ct
        -0x5bt
        0xdt
        -0x6et
        -0x28t
        0x49t
        0x79t
    .end array-data

    :array_10
    .array-data 1
        -0x56t
        -0x25t
        -0x44t
        0x22t
        0x23t
        -0x71t
        -0x3ct
        0x1t
        -0x5ft
        0x27t
        -0x45t
        0x38t
        -0x23t
        -0xct
        0x1dt
        -0x25t
        0x8t
        -0x62t
        -0x10t
        0x12t
    .end array-data

    :array_11
    .array-data 1
        -0xft
        -0x75t
        0x53t
        -0x73t
        0x1bt
        -0x17t
        0x3t
        -0x4at
        -0x5at
        -0x10t
        0x56t
        0x43t
        0x5bt
        0x17t
        0x15t
        -0x77t
        -0x36t
        -0xdt
        0x6bt
        -0xet
    .end array-data
.end method

.method public constructor <init>()V
    .locals 13
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/security/GeneralSecurityException;
        }
    .end annotation

    const/4 v12, 0x0

    const/4 v11, 0x0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    invoke-static {}, Ljavax/net/ssl/TrustManagerFactory;->getDefaultAlgorithm()Ljava/lang/String;

    move-result-object v0

    invoke-static {v0}, Ljavax/net/ssl/TrustManagerFactory;->getInstance(Ljava/lang/String;)Ljavax/net/ssl/TrustManagerFactory;

    move-result-object v0

    invoke-virtual {v0, v12}, Ljavax/net/ssl/TrustManagerFactory;->init(Ljava/security/KeyStore;)V

    new-instance v1, Ljava/util/HashSet;

    invoke-direct {v1}, Ljava/util/HashSet;-><init>()V

    iput-object v1, p0, Lcom/amazon/android/l/b;->d:Ljava/util/Set;

    invoke-virtual {v0}, Ljavax/net/ssl/TrustManagerFactory;->getTrustManagers()[Ljavax/net/ssl/TrustManager;

    move-result-object v1

    array-length v2, v1

    move v3, v11

    :goto_0
    if-ge v3, v2, :cond_4

    aget-object v0, v1, v3

    instance-of v4, v0, Ljavax/net/ssl/X509TrustManager;

    if-eqz v4, :cond_3

    check-cast v0, Ljavax/net/ssl/X509TrustManager;

    invoke-interface {v0}, Ljavax/net/ssl/X509TrustManager;->getAcceptedIssuers()[Ljava/security/cert/X509Certificate;

    move-result-object v0

    if-eqz v0, :cond_3

    array-length v4, v0

    move v5, v11

    move v6, v11

    :goto_1
    if-ge v5, v4, :cond_2

    aget-object v7, v0, v5

    invoke-static {v7}, Lcom/amazon/android/l/b;->a(Ljava/security/cert/X509Certificate;)Z

    move-result v8

    if-eqz v8, :cond_1

    sget-boolean v8, Lcom/amazon/android/framework/util/KiwiLogger;->TRACE_ON:Z

    if-eqz v8, :cond_0

    sget-object v8, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v9, Ljava/lang/StringBuilder;

    invoke-direct {v9}, Ljava/lang/StringBuilder;-><init>()V

    const-string v10, "Trusted Cert: "

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v9

    invoke-virtual {v7}, Ljava/security/cert/X509Certificate;->getSubjectX500Principal()Ljavax/security/auth/x500/X500Principal;

    move-result-object v10

    invoke-virtual {v10}, Ljavax/security/auth/x500/X500Principal;->getName()Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v9, v10}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v9

    invoke-virtual {v9}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v8, v9}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    :cond_0
    new-instance v8, Ljava/security/cert/TrustAnchor;

    invoke-direct {v8, v7, v12}, Ljava/security/cert/TrustAnchor;-><init>(Ljava/security/cert/X509Certificate;[B)V

    iget-object v7, p0, Lcom/amazon/android/l/b;->d:Ljava/util/Set;

    invoke-interface {v7, v8}, Ljava/util/Set;->add(Ljava/lang/Object;)Z

    add-int/lit8 v6, v6, 0x1

    :cond_1
    add-int/lit8 v5, v5, 0x1

    goto :goto_1

    :cond_2
    sget-boolean v0, Lcom/amazon/android/framework/util/KiwiLogger;->TRACE_ON:Z

    if-eqz v0, :cond_3

    sget-object v0, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v4, "loaded %d certs\n"

    const/4 v5, 0x1

    new-array v5, v5, [Ljava/lang/Object;

    invoke-static {v6}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v6

    aput-object v6, v5, v11

    invoke-static {v4, v5}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v4}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    :cond_3
    add-int/lit8 v0, v3, 0x1

    move v3, v0

    goto :goto_0

    :cond_4
    iget-object v0, p0, Lcom/amazon/android/l/b;->d:Ljava/util/Set;

    invoke-interface {v0}, Ljava/util/Set;->isEmpty()Z

    move-result v0

    if-eqz v0, :cond_5

    sget-object v0, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "TrustManager did not return valid accepted issuers, likely 3P custom TrustManager implementation issue."

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;)V

    :cond_5
    new-instance v0, Ljava/security/cert/PKIXParameters;

    iget-object v1, p0, Lcom/amazon/android/l/b;->d:Ljava/util/Set;

    invoke-direct {v0, v1}, Ljava/security/cert/PKIXParameters;-><init>(Ljava/util/Set;)V

    iput-object v0, p0, Lcom/amazon/android/l/b;->b:Ljava/security/cert/PKIXParameters;

    iget-object v0, p0, Lcom/amazon/android/l/b;->b:Ljava/security/cert/PKIXParameters;

    invoke-virtual {v0, v11}, Ljava/security/cert/PKIXParameters;->setRevocationEnabled(Z)V

    const-string v0, "PKIX"

    invoke-static {v0}, Ljava/security/cert/CertPathValidator;->getInstance(Ljava/lang/String;)Ljava/security/cert/CertPathValidator;

    move-result-object v0

    iput-object v0, p0, Lcom/amazon/android/l/b;->c:Ljava/security/cert/CertPathValidator;

    return-void
.end method

.method private static a(Ljava/security/cert/X509Certificate;)Z
    .locals 4

    const/4 v3, 0x0

    :try_start_0
    const-string v0, "SHA1"

    invoke-static {v0}, Ljava/security/MessageDigest;->getInstance(Ljava/lang/String;)Ljava/security/MessageDigest;

    move-result-object v0

    invoke-virtual {p0}, Ljava/security/cert/X509Certificate;->getEncoded()[B

    move-result-object v1

    invoke-virtual {v0, v1}, Ljava/security/MessageDigest;->digest([B)[B

    move-result-object v0

    sget-object v1, Lcom/amazon/android/l/b;->e:[[B

    invoke-static {v1, v0}, Lcom/amazon/android/l/b;->a([[B[B)Z

    move-result v1

    if-nez v1, :cond_0

    sget-object v1, Lcom/amazon/android/l/b;->f:[[B

    invoke-static {v1, v0}, Lcom/amazon/android/l/b;->a([[B[B)Z
    :try_end_0
    .catch Ljava/security/NoSuchAlgorithmException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/security/cert/CertificateEncodingException; {:try_start_0 .. :try_end_0} :catch_1

    move-result v0

    if-eqz v0, :cond_1

    :cond_0
    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_1
    move v0, v3

    goto :goto_0

    :catch_0
    move-exception v0

    sget-object v1, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v2, "Signature algorithm unrecognized"

    invoke-virtual {v1, v2, v0}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;Ljava/lang/Throwable;)V

    move v0, v3

    goto :goto_0

    :catch_1
    move-exception v0

    sget-object v1, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v2, "Cant get fingerprint"

    invoke-virtual {v1, v2, v0}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;Ljava/lang/Throwable;)V

    move v0, v3

    goto :goto_0
.end method

.method private static a([[B[B)Z
    .locals 3

    const/4 v2, 0x0

    move v0, v2

    :goto_0
    array-length v1, p0

    if-ge v0, v1, :cond_1

    aget-object v1, p0, v0

    invoke-static {v1, p1}, Ljava/util/Arrays;->equals([B[B)Z

    move-result v1

    if-eqz v1, :cond_0

    const/4 v0, 0x1

    :goto_1
    return v0

    :cond_0
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    :cond_1
    move v0, v2

    goto :goto_1
.end method


# virtual methods
.method public final a(Ljava/security/cert/CertPath;)Z
    .locals 5

    const/4 v4, 0x0

    :try_start_0
    invoke-virtual {p1}, Ljava/security/cert/CertPath;->getCertificates()Ljava/util/List;

    move-result-object v0

    const/4 v1, 0x0

    invoke-interface {v0, v1}, Ljava/util/List;->get(I)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/security/cert/X509Certificate;

    invoke-virtual {v0}, Ljava/security/cert/X509Certificate;->getNotBefore()Ljava/util/Date;

    move-result-object v0

    sget-object v1, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Verifying CertPath with "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    iget-object v1, p0, Lcom/amazon/android/l/b;->b:Ljava/security/cert/PKIXParameters;

    invoke-virtual {v1, v0}, Ljava/security/cert/PKIXParameters;->setDate(Ljava/util/Date;)V

    iget-object v0, p0, Lcom/amazon/android/l/b;->c:Ljava/security/cert/CertPathValidator;

    iget-object v1, p0, Lcom/amazon/android/l/b;->b:Ljava/security/cert/PKIXParameters;

    invoke-virtual {v0, p1, v1}, Ljava/security/cert/CertPathValidator;->validate(Ljava/security/cert/CertPath;Ljava/security/cert/CertPathParameters;)Ljava/security/cert/CertPathValidatorResult;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :catch_0
    move-exception v0

    sget-boolean v1, Lcom/amazon/android/framework/util/KiwiLogger;->TRACE_ON:Z

    if-eqz v1, :cond_0

    sget-object v1, Lcom/amazon/android/l/b;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Failed to verify cert path: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2, v0}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;Ljava/lang/Throwable;)V

    :cond_0
    move v0, v4

    goto :goto_0
.end method
