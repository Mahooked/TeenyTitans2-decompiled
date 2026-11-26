.class public final Lcom/amazon/android/framework/prompt/e;
.super Lcom/amazon/android/framework/prompt/SimplePrompt;

# interfaces
.implements Lcom/amazon/android/framework/resource/b;


# static fields
.field private static final a:Lcom/amazon/android/framework/util/KiwiLogger;


# instance fields
.field private b:Lcom/amazon/android/framework/context/ContextManager;
    .annotation runtime Lcom/amazon/android/framework/resource/Resource;
    .end annotation
.end field

.field private c:Lcom/amazon/android/n/a;
    .annotation runtime Lcom/amazon/android/framework/resource/Resource;
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 2

    new-instance v0, Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "ShutdownPrompt"

    invoke-direct {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;-><init>(Ljava/lang/String;)V

    sput-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    return-void
.end method

.method public constructor <init>(Lcom/amazon/android/framework/prompt/PromptContent;)V
    .locals 2

    sget-object v0, Lcom/amazon/android/framework/prompt/b;->b:Lcom/amazon/android/framework/prompt/b;

    invoke-direct {p0, p1, v0}, Lcom/amazon/android/framework/prompt/SimplePrompt;-><init>(Lcom/amazon/android/framework/prompt/PromptContent;Lcom/amazon/android/framework/prompt/b;)V

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "new ShutdownPrompt called!"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    return-void
.end method

.method private final a(Landroid/content/Context;)Landroid/content/Intent;
    .locals 6

    const/4 v5, 0x0

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v1

    new-instance v2, Ljava/lang/StringBuilder;

    const-string v0, "https://www.amazon.com/appstore-error-help"

    invoke-direct {v2, v0}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    const-string v0, "?k="

    const-string v4, "1.1"

    invoke-static {v2, v0, v4}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "&m="

    iget-object v4, p0, Lcom/amazon/android/framework/prompt/e;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v4}, Lcom/amazon/android/framework/prompt/PromptContent;->getTitle()Ljava/lang/String;

    move-result-object v4

    invoke-static {v2, v0, v4}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "&ec="

    iget-object v4, p0, Lcom/amazon/android/framework/prompt/e;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v4}, Lcom/amazon/android/framework/prompt/PromptContent;->getErrorCode()I

    move-result v4

    invoke-static {v4}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v4

    invoke-static {v2, v0, v4}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/e;->c:Lcom/amazon/android/n/a;

    const-string v4, "PACKAGE"

    invoke-virtual {v0, v4}, Lcom/amazon/android/n/a;->a(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-eqz v0, :cond_0

    const-string v4, "&t="

    invoke-static {v2, v4, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v4, "&tv="

    invoke-static {v2, v4, v0, v3}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;Landroid/content/pm/PackageManager;)V

    :cond_0
    iget-object v0, p0, Lcom/amazon/android/framework/prompt/e;->c:Lcom/amazon/android/n/a;

    const-string v4, "COMMAND"

    invoke-virtual {v0, v4}, Lcom/amazon/android/n/a;->a(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    if-eqz v0, :cond_1

    const-string v4, "&c="

    invoke-static {v2, v4, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_1
    iget-object v0, p0, Lcom/amazon/android/framework/prompt/e;->c:Lcom/amazon/android/n/a;

    const-string v4, "LICENSE_FAILURE_RAW_EXCEPTION"

    invoke-virtual {v0, v4}, Lcom/amazon/android/n/a;->a(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Ljava/lang/String;

    if-eqz p0, :cond_2

    const-string v0, "&e="

    invoke-static {v2, v0, p0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_2
    sget-object v0, Landroid/os/Build;->PRODUCT:Ljava/lang/String;

    if-eqz v0, :cond_3

    const-string v4, "&d="

    invoke-static {v2, v4, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_3
    sget-object v0, Landroid/os/Build;->MANUFACTURER:Ljava/lang/String;

    if-eqz v0, :cond_4

    const-string v4, "&ma="

    invoke-static {v2, v4, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_4
    sget-object v0, Landroid/os/Build;->MODEL:Ljava/lang/String;

    if-eqz v0, :cond_5

    const-string v4, "&mo="

    invoke-static {v2, v4, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_5
    sget-object v0, Landroid/os/Build;->ID:Ljava/lang/String;

    if-eqz v0, :cond_6

    const-string v4, "&bn="

    invoke-static {v2, v4, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_6
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {v0}, Landroid/content/res/Resources;->getConfiguration()Landroid/content/res/Configuration;

    move-result-object v0

    iget v0, v0, Landroid/content/res/Configuration;->screenLayout:I

    and-int/lit8 v0, v0, 0xf

    const-string v4, "&sz="

    invoke-static {v0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v0

    invoke-static {v2, v4, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "&s="

    sget v4, Landroid/os/Build$VERSION;->SDK_INT:I

    invoke-static {v4}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v4

    invoke-static {v2, v0, v4}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "&p="

    invoke-static {v2, v0, v1}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "&pv="

    invoke-static {v2, v0, v1, v3}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;Landroid/content/pm/PackageManager;)V

    const-string v0, "&l="

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/Locale;->getLanguage()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "_"

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-static {}, Ljava/util/Locale;->getDefault()Ljava/util/Locale;

    move-result-object v3

    invoke-virtual {v3}, Ljava/util/Locale;->getCountry()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v1, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v2, v0, v1}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    const-string v0, "phone"

    invoke-virtual {p1, v0}, Landroid/content/Context;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p0

    check-cast p0, Landroid/telephony/TelephonyManager;

    if-nez p0, :cond_8

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "tm was null!"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    move-object v0, v5

    :goto_0
    invoke-static {v0}, Landroid/text/TextUtils;->isEmpty(Ljava/lang/CharSequence;)Z

    move-result v1

    if-nez v1, :cond_7

    const-string v1, "&mc="

    invoke-static {v2, v1, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V

    :cond_7
    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    sget-object v1, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Starting browser for uri "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    new-instance v1, Landroid/content/Intent;

    const-string v2, "android.intent.action.VIEW"

    invoke-static {v0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    invoke-direct {v1, v2, v0}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    return-object v1

    :cond_8
    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "tm was not null."

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    invoke-virtual {p0}, Landroid/telephony/TelephonyManager;->getSimState()I

    move-result v0

    const/4 v1, 0x5

    if-eq v0, v1, :cond_9

    sget-object v1, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "SIM not ready, returning null.  State was "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    move-object v0, v5

    goto :goto_0

    :cond_9
    invoke-virtual {p0}, Landroid/telephony/TelephonyManager;->getSimOperator()Ljava/lang/String;

    move-result-object v0

    goto :goto_0
.end method

.method private static a(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;
    .locals 3

    invoke-virtual {p0}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "amzn://apps/android?p="

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    const-string v1, "&ref="

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    new-instance v1, Landroid/content/Intent;

    const-string v2, "android.intent.action.VIEW"

    invoke-static {v0}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v0

    invoke-direct {v1, v2, v0}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    return-object v1
.end method

.method static synthetic a()V
    .locals 0

    invoke-static {}, Lcom/amazon/android/framework/prompt/e;->b()V

    return-void
.end method

.method private a(Landroid/content/Context;Landroid/content/Intent;)V
    .locals 3

    invoke-static {p1, p2}, Lcom/amazon/android/framework/prompt/e;->b(Landroid/content/Context;Landroid/content/Intent;)Z

    move-result v0

    if-nez v0, :cond_0

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "Could not start activity, nothing resolves for the given data."

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;)V

    invoke-static {}, Lcom/amazon/android/framework/prompt/e;->b()V

    :goto_0
    return-void

    :cond_0
    new-instance v0, Lcom/amazon/android/framework/prompt/g;

    iget-object v1, p0, Lcom/amazon/android/framework/prompt/e;->b:Lcom/amazon/android/framework/context/ContextManager;

    invoke-direct {v0, p1, v1}, Lcom/amazon/android/framework/prompt/g;-><init>(Landroid/content/Context;Lcom/amazon/android/framework/context/ContextManager;)V

    const/4 v1, 0x1

    new-array v1, v1, [Landroid/content/Intent;

    const/4 v2, 0x0

    aput-object p2, v1, v2

    invoke-virtual {v0, v1}, Landroid/os/AsyncTask;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    goto :goto_0
.end method

.method private static a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V
    .locals 3

    :try_start_0
    const-string v0, "UTF-8"

    invoke-static {p2, v0}, Ljava/net/URLEncoder;->encode(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {p0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p0, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;
    :try_end_0
    .catch Ljava/io/UnsupportedEncodingException; {:try_start_0 .. :try_end_0} :catch_0

    :goto_0
    return-void

    :catch_0
    move-exception v0

    sget-object v1, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v2, "Failed to URL-encode argument"

    invoke-virtual {v1, v2, v0}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;Ljava/lang/Throwable;)V

    goto :goto_0
.end method

.method private static a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;Landroid/content/pm/PackageManager;)V
    .locals 3

    const/4 v0, 0x0

    :try_start_0
    invoke-virtual {p3, p2, v0}, Landroid/content/pm/PackageManager;->getPackageInfo(Ljava/lang/String;I)Landroid/content/pm/PackageInfo;

    move-result-object v0

    iget v0, v0, Landroid/content/pm/PackageInfo;->versionCode:I

    invoke-static {v0}, Ljava/lang/String;->valueOf(I)Ljava/lang/String;

    move-result-object v0

    invoke-static {p0, p1, v0}, Lcom/amazon/android/framework/prompt/e;->a(Ljava/lang/StringBuilder;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Landroid/content/pm/PackageManager$NameNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    :goto_0
    return-void

    :catch_0
    move-exception v0

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Could not find information for package "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;)V

    goto :goto_0
.end method

.method private static b()V
    .locals 2

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "doShutdown()"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "Killing application"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->test(Ljava/lang/String;)V

    const/4 v0, 0x0

    invoke-static {v0}, Ljava/lang/System;->exit(I)V

    return-void
.end method

.method private static b(Landroid/content/Context;Landroid/content/Intent;)Z
    .locals 1

    const/high16 v0, 0x10000000

    invoke-virtual {p1, v0}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    invoke-virtual {p0}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object v0

    if-eqz v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method


# virtual methods
.method protected final canHandleAction(Lcom/amazon/android/framework/prompt/a;)Z
    .locals 3

    sget-object v0, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    if-ne p1, v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    iget-object v1, p0, Lcom/amazon/android/framework/prompt/e;->b:Lcom/amazon/android/framework/context/ContextManager;

    invoke-interface {v1}, Lcom/amazon/android/framework/context/ContextManager;->getRoot()Landroid/app/Activity;

    move-result-object v1

    sget-object v2, Lcom/amazon/android/framework/prompt/a;->c:Lcom/amazon/android/framework/prompt/a;

    if-ne p1, v2, :cond_2

    const-string v0, "badcert"

    invoke-static {v1, v0}, Lcom/amazon/android/framework/prompt/e;->a(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    :cond_1
    :goto_1
    if-nez v0, :cond_3

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Unexpected action "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", could not create intent."

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;)V

    const/4 v0, 0x0

    goto :goto_0

    :cond_2
    sget-object v2, Lcom/amazon/android/framework/prompt/a;->b:Lcom/amazon/android/framework/prompt/a;

    if-ne p1, v2, :cond_1

    invoke-direct {p0, v1}, Lcom/amazon/android/framework/prompt/e;->a(Landroid/content/Context;)Landroid/content/Intent;

    move-result-object v0

    goto :goto_1

    :cond_3
    invoke-static {v1, v0}, Lcom/amazon/android/framework/prompt/e;->b(Landroid/content/Context;Landroid/content/Intent;)Z

    move-result v0

    goto :goto_0
.end method

.method protected final doAction()V
    .locals 1

    const/4 v0, 0x0

    invoke-virtual {p0, v0}, Lcom/amazon/android/framework/prompt/e;->doAction(I)V

    return-void
.end method

.method protected final doAction(I)V
    .locals 3

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "doAction("

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ")"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/e;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v0}, Lcom/amazon/android/framework/prompt/PromptContent;->getButtonActions()[Lcom/amazon/android/framework/prompt/a;

    move-result-object v0

    sget-object v1, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    array-length v2, v0

    if-ge p1, v2, :cond_4

    aget-object v0, v0, p1

    :goto_0
    sget-object v1, Lcom/amazon/android/framework/prompt/a;->b:Lcom/amazon/android/framework/prompt/a;

    if-ne v0, v1, :cond_2

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "doAction() help"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Store contains "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Lcom/amazon/android/framework/prompt/e;->c:Lcom/amazon/android/n/a;

    invoke-virtual {v2}, Lcom/amazon/android/n/a;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/e;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v0}, Lcom/amazon/android/framework/prompt/PromptContent;->shouldShowFixup()Z

    move-result v0

    if-nez v0, :cond_1

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "Should not show fixup"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    new-instance v0, Lcom/amazon/android/framework/prompt/p;

    iget-object v1, p0, Lcom/amazon/android/framework/prompt/e;->b:Lcom/amazon/android/framework/context/ContextManager;

    invoke-direct {v0, v1}, Lcom/amazon/android/framework/prompt/p;-><init>(Lcom/amazon/android/framework/context/ContextManager;)V

    const/4 v1, 0x0

    new-array v1, v1, [Ljava/lang/Void;

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/prompt/p;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    :cond_0
    :goto_1
    return-void

    :cond_1
    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "Should show fixup"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/e;->b:Lcom/amazon/android/framework/context/ContextManager;

    invoke-interface {v0}, Lcom/amazon/android/framework/context/ContextManager;->getRoot()Landroid/app/Activity;

    move-result-object v0

    invoke-direct {p0, v0}, Lcom/amazon/android/framework/prompt/e;->a(Landroid/content/Context;)Landroid/content/Intent;

    move-result-object v1

    invoke-direct {p0, v0, v1}, Lcom/amazon/android/framework/prompt/e;->a(Landroid/content/Context;Landroid/content/Intent;)V

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "Attempted to start browser."

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    goto :goto_1

    :cond_2
    sget-object v1, Lcom/amazon/android/framework/prompt/a;->c:Lcom/amazon/android/framework/prompt/a;

    if-ne v0, v1, :cond_3

    sget-object v0, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "doAction() deeplink"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    const-string v0, "badcert"

    sget-object v1, Lcom/amazon/android/framework/prompt/e;->a:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v2, "Attempting to deep link to appstore."

    invoke-virtual {v1, v2}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    iget-object v1, p0, Lcom/amazon/android/framework/prompt/e;->b:Lcom/amazon/android/framework/context/ContextManager;

    invoke-interface {v1}, Lcom/amazon/android/framework/context/ContextManager;->getRoot()Landroid/app/Activity;

    move-result-object v1

    invoke-static {v1, v0}, Lcom/amazon/android/framework/prompt/e;->a(Landroid/content/Context;Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    invoke-direct {p0, v1, v0}, Lcom/amazon/android/framework/prompt/e;->a(Landroid/content/Context;Landroid/content/Intent;)V

    goto :goto_1

    :cond_3
    sget-object v1, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    if-ne v0, v1, :cond_0

    invoke-static {}, Lcom/amazon/android/framework/prompt/e;->b()V

    goto :goto_1

    :cond_4
    move-object v0, v1

    goto/16 :goto_0
.end method

.method protected final getExpirationDurationInSeconds()J
    .locals 2

    const-wide/32 v0, 0x1e13380

    return-wide v0
.end method

.method public final onResourcesPopulatedImpl()V
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/e;->b:Lcom/amazon/android/framework/context/ContextManager;

    invoke-interface {v0}, Lcom/amazon/android/framework/context/ContextManager;->stopServices()V

    return-void
.end method

.method public final toString()Ljava/lang/String;
    .locals 1

    const-string v0, "ShutdownPrompt"

    return-object v0
.end method
