.class public final Lcom/amazon/android/licensing/i;
.super Ljava/lang/Object;


# static fields
.field public static final a:Lcom/amazon/android/framework/prompt/PromptContent;

.field public static final b:Lcom/amazon/android/framework/prompt/PromptContent;

.field public static final c:Lcom/amazon/android/framework/prompt/PromptContent;

.field public static final d:Lcom/amazon/android/framework/prompt/PromptContent;

.field public static final e:Lcom/amazon/android/framework/prompt/PromptContent;

.field public static final f:Lcom/amazon/android/framework/prompt/PromptContent;

.field public static final g:Lcom/amazon/android/framework/prompt/PromptContent;


# direct methods
.method static constructor <clinit>()V
    .locals 13

    const/4 v10, 0x2

    const/4 v9, 0x0

    const/4 v5, 0x1

    const-string v12, "Help"

    const-string v11, "Quit"

    new-instance v0, Lcom/amazon/android/framework/prompt/PromptContent;

    new-array v1, v5, [Ljava/lang/String;

    const-string v2, "Amazon Appstore required"

    aput-object v2, v1, v9

    const-string v2, "Amazon Appstore is not installed on your device. Please install Amazon Appstore and sign in to Amazon to use this app."

    new-array v3, v10, [Ljava/lang/String;

    const-string v4, "Quit"

    aput-object v11, v3, v9

    const-string v4, "Help"

    aput-object v12, v3, v5

    new-array v4, v10, [Lcom/amazon/android/framework/prompt/a;

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v9

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->b:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v5

    move v6, v5

    move v7, v5

    move v8, v5

    invoke-direct/range {v0 .. v8}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZII)V

    sput-object v0, Lcom/amazon/android/licensing/i;->a:Lcom/amazon/android/framework/prompt/PromptContent;

    new-instance v1, Lcom/amazon/android/framework/prompt/PromptContent;

    const-string v2, "Amazon Appstore connection failure"

    const-string v3, "An error occurred connecting to Amazon Appstore. Please try opening this app again"

    const-string v0, "Quit"

    move-object v4, v11

    move v6, v9

    invoke-direct/range {v1 .. v6}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZ)V

    sput-object v1, Lcom/amazon/android/licensing/i;->b:Lcom/amazon/android/framework/prompt/PromptContent;

    new-instance v0, Lcom/amazon/android/framework/prompt/PromptContent;

    new-array v1, v10, [Ljava/lang/String;

    const-string v2, "New Appstore version required"

    aput-object v2, v1, v9

    const-string v2, "Amazon Appstore Update Required"

    aput-object v2, v1, v5

    const-string v2, "Amazon Appstore is out of date.  Please visit the Amazon website to install the latest version of the Appstore. "

    new-array v3, v10, [Ljava/lang/String;

    const-string v4, "Quit"

    aput-object v11, v3, v9

    const-string v4, "Help"

    aput-object v12, v3, v5

    new-array v4, v10, [Lcom/amazon/android/framework/prompt/a;

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v9

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->b:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v5

    move v6, v5

    move v7, v5

    move v8, v10

    invoke-direct/range {v0 .. v8}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZII)V

    sput-object v0, Lcom/amazon/android/licensing/i;->c:Lcom/amazon/android/framework/prompt/PromptContent;

    new-instance v1, Lcom/amazon/android/framework/prompt/PromptContent;

    const-string v2, "Internet connection required"

    const-string v3, "An internet connection is required to open this app. Please connect to the internet and reopen this app."

    const-string v0, "Quit"

    move-object v4, v11

    move v6, v9

    invoke-direct/range {v1 .. v6}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZ)V

    sput-object v1, Lcom/amazon/android/licensing/i;->d:Lcom/amazon/android/framework/prompt/PromptContent;

    new-instance v0, Lcom/amazon/android/framework/prompt/PromptContent;

    new-array v1, v5, [Ljava/lang/String;

    const-string v2, "Connection error"

    aput-object v2, v1, v9

    const-string v2, "An unknown error occurred connecting to Amazon Appstore."

    new-array v3, v10, [Ljava/lang/String;

    const-string v4, "Quit"

    aput-object v11, v3, v9

    const-string v4, "Help"

    aput-object v12, v3, v5

    new-array v4, v10, [Lcom/amazon/android/framework/prompt/a;

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v9

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->b:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v5

    const/4 v8, 0x3

    move v6, v5

    move v7, v5

    invoke-direct/range {v0 .. v8}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZII)V

    sput-object v0, Lcom/amazon/android/licensing/i;->e:Lcom/amazon/android/framework/prompt/PromptContent;

    new-instance v1, Lcom/amazon/android/framework/prompt/PromptContent;

    const-string v2, "Internal error"

    const-string v3, "An internal error occurred, please try opening this app again"

    const-string v0, "Quit"

    move-object v4, v11

    move v6, v9

    invoke-direct/range {v1 .. v6}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZ)V

    sput-object v1, Lcom/amazon/android/licensing/i;->f:Lcom/amazon/android/framework/prompt/PromptContent;

    new-instance v0, Lcom/amazon/android/framework/prompt/PromptContent;

    const-string v1, "App update required"

    const-string v2, "Please update this app from the Amazon Appstore."

    new-array v3, v10, [Ljava/lang/String;

    const-string v4, "Quit"

    aput-object v11, v3, v9

    const-string v4, "Update"

    aput-object v4, v3, v5

    new-array v4, v10, [Lcom/amazon/android/framework/prompt/a;

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v9

    sget-object v6, Lcom/amazon/android/framework/prompt/a;->c:Lcom/amazon/android/framework/prompt/a;

    aput-object v6, v4, v5

    move v6, v9

    move v7, v9

    invoke-direct/range {v0 .. v7}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZI)V

    sput-object v0, Lcom/amazon/android/licensing/i;->g:Lcom/amazon/android/framework/prompt/PromptContent;

    return-void
.end method

.method private constructor <init>()V
    .locals 0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method
