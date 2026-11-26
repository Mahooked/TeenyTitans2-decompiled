.class public abstract Lcom/amazon/android/framework/prompt/SimplePrompt;
.super Lcom/amazon/android/framework/prompt/Prompt;


# static fields
.field private static final LEGACY_BUTTON_LABEL:Ljava/lang/String; = "OK"

.field private static final LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;


# instance fields
.field protected final content:Lcom/amazon/android/framework/prompt/PromptContent;

.field protected final mode:Lcom/amazon/android/framework/prompt/b;


# direct methods
.method static constructor <clinit>()V
    .locals 2

    new-instance v0, Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "SimplePrompt"

    invoke-direct {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;-><init>(Ljava/lang/String;)V

    sput-object v0, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    return-void
.end method

.method public constructor <init>(Lcom/amazon/android/framework/prompt/PromptContent;)V
    .locals 1

    sget-object v0, Lcom/amazon/android/framework/prompt/b;->a:Lcom/amazon/android/framework/prompt/b;

    invoke-direct {p0, p1, v0}, Lcom/amazon/android/framework/prompt/SimplePrompt;-><init>(Lcom/amazon/android/framework/prompt/PromptContent;Lcom/amazon/android/framework/prompt/b;)V

    return-void
.end method

.method public constructor <init>(Lcom/amazon/android/framework/prompt/PromptContent;Lcom/amazon/android/framework/prompt/b;)V
    .locals 3

    invoke-direct {p0}, Lcom/amazon/android/framework/prompt/Prompt;-><init>()V

    const-string v0, "content"

    invoke-static {p1, v0}, Lcom/amazon/android/d/a;->a(Ljava/lang/Object;Ljava/lang/String;)V

    iput-object p1, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    iput-object p2, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->mode:Lcom/amazon/android/framework/prompt/b;

    sget-object v0, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "created SimplePrompt with mode "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    return-void
.end method

.method private final createExtendedDialog(Landroid/app/Activity;)Landroid/app/Dialog;
    .locals 3

    sget-object v0, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "doCreate() called to create extended dialog"

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p1}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    iget-object v1, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v1}, Lcom/amazon/android/framework/prompt/PromptContent;->getExtendedTitle()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    iget-object v2, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v2}, Lcom/amazon/android/framework/prompt/PromptContent;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    invoke-direct {p0, v0}, Lcom/amazon/android/framework/prompt/SimplePrompt;->setUpButtons(Landroid/app/AlertDialog$Builder;)V

    sget-object v1, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v2, "doCreate() returning"

    invoke-virtual {v1, v2}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object v0

    return-object v0
.end method

.method private final createLegacyDialog(Landroid/app/Activity;)Landroid/app/Dialog;
    .locals 4

    sget-object v0, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "Legacy mode dialog, legacy title = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v2}, Lcom/amazon/android/framework/prompt/PromptContent;->getTitle()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", extended title = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v2}, Lcom/amazon/android/framework/prompt/PromptContent;->getExtendedTitle()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    new-instance v0, Landroid/app/AlertDialog$Builder;

    invoke-direct {v0, p1}, Landroid/app/AlertDialog$Builder;-><init>(Landroid/content/Context;)V

    iget-object v1, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v1}, Lcom/amazon/android/framework/prompt/PromptContent;->getExtendedTitle()Ljava/lang/String;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/app/AlertDialog$Builder;->setTitle(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    iget-object v2, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v2}, Lcom/amazon/android/framework/prompt/PromptContent;->getMessage()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setMessage(Ljava/lang/CharSequence;)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    const/4 v2, 0x0

    invoke-virtual {v1, v2}, Landroid/app/AlertDialog$Builder;->setCancelable(Z)Landroid/app/AlertDialog$Builder;

    move-result-object v1

    const-string v2, "OK"

    new-instance v3, Lcom/amazon/android/framework/prompt/l;

    invoke-direct {v3, p0}, Lcom/amazon/android/framework/prompt/l;-><init>(Lcom/amazon/android/framework/prompt/SimplePrompt;)V

    invoke-virtual {v1, v2, v3}, Landroid/app/AlertDialog$Builder;->setNeutralButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    invoke-virtual {v0}, Landroid/app/AlertDialog$Builder;->create()Landroid/app/AlertDialog;

    move-result-object v0

    return-object v0
.end method

.method private filterActions([Lcom/amazon/android/framework/prompt/a;)[Z
    .locals 5

    if-nez p1, :cond_1

    const/4 v0, 0x0

    :cond_0
    return-object v0

    :cond_1
    array-length v0, p1

    new-array v0, v0, [Z

    const/4 v1, 0x0

    :goto_0
    array-length v2, p1

    if-ge v1, v2, :cond_0

    aget-object v2, p1, v1

    invoke-virtual {p0, v2}, Lcom/amazon/android/framework/prompt/SimplePrompt;->canHandleAction(Lcom/amazon/android/framework/prompt/a;)Z

    move-result v2

    aput-boolean v2, v0, v1

    aget-boolean v2, v0, v1

    if-nez v2, :cond_2

    sget-object v2, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "filterActions() filtering item "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    :cond_2
    add-int/lit8 v1, v1, 0x1

    goto :goto_0
.end method

.method private setNeutralButton(Landroid/app/AlertDialog$Builder;Ljava/lang/String;I)V
    .locals 1

    new-instance v0, Lcom/amazon/android/framework/prompt/j;

    invoke-direct {v0, p0, p3}, Lcom/amazon/android/framework/prompt/j;-><init>(Lcom/amazon/android/framework/prompt/SimplePrompt;I)V

    invoke-virtual {p1, p2, v0}, Landroid/app/AlertDialog$Builder;->setNeutralButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    return-void
.end method

.method private setUpButtons(Landroid/app/AlertDialog$Builder;)V
    .locals 8

    const/4 v7, 0x1

    const/4 v6, 0x0

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v0}, Lcom/amazon/android/framework/prompt/PromptContent;->getButtonLabels()[Ljava/lang/String;

    move-result-object v0

    array-length v1, v0

    if-lez v1, :cond_0

    array-length v1, v0

    const/4 v2, 0x2

    if-le v1, v2, :cond_1

    :cond_0
    sget-object v1, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Unexpected button count: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v2

    array-length v0, v0

    invoke-virtual {v2, v0}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v1, v0}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;)V

    :goto_0
    return-void

    :cond_1
    iget-object v1, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v1}, Lcom/amazon/android/framework/prompt/PromptContent;->getButtonActions()[Lcom/amazon/android/framework/prompt/a;

    move-result-object v1

    invoke-direct {p0, v1}, Lcom/amazon/android/framework/prompt/SimplePrompt;->filterActions([Lcom/amazon/android/framework/prompt/a;)[Z

    move-result-object v1

    const/4 v2, -0x1

    move v3, v6

    move v4, v2

    move v2, v6

    :goto_1
    array-length v5, v1

    if-ge v2, v5, :cond_3

    aget-boolean v5, v1, v2

    if-eqz v5, :cond_2

    add-int/lit8 v3, v3, 0x1

    move v4, v2

    :cond_2
    add-int/lit8 v2, v2, 0x1

    goto :goto_1

    :cond_3
    if-ne v3, v7, :cond_4

    sget-object v1, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v2, "single button dialog"

    invoke-virtual {v1, v2}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    aget-object v0, v0, v4

    invoke-direct {p0, p1, v0, v4}, Lcom/amazon/android/framework/prompt/SimplePrompt;->setNeutralButton(Landroid/app/AlertDialog$Builder;Ljava/lang/String;I)V

    goto :goto_0

    :cond_4
    sget-object v1, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v2, "two button dialog"

    invoke-virtual {v1, v2}, Lcom/amazon/android/framework/util/KiwiLogger;->trace(Ljava/lang/String;)V

    aget-object v1, v0, v6

    new-instance v2, Lcom/amazon/android/framework/prompt/m;

    invoke-direct {v2, p0}, Lcom/amazon/android/framework/prompt/m;-><init>(Lcom/amazon/android/framework/prompt/SimplePrompt;)V

    invoke-virtual {p1, v1, v2}, Landroid/app/AlertDialog$Builder;->setNegativeButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    aget-object v0, v0, v7

    new-instance v1, Lcom/amazon/android/framework/prompt/h;

    invoke-direct {v1, p0}, Lcom/amazon/android/framework/prompt/h;-><init>(Lcom/amazon/android/framework/prompt/SimplePrompt;)V

    invoke-virtual {p1, v0, v1}, Landroid/app/AlertDialog$Builder;->setPositiveButton(Ljava/lang/CharSequence;Landroid/content/DialogInterface$OnClickListener;)Landroid/app/AlertDialog$Builder;

    goto :goto_0
.end method


# virtual methods
.method protected canHandleAction(Lcom/amazon/android/framework/prompt/a;)Z
    .locals 1

    sget-object v0, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    if-ne p1, v0, :cond_0

    const/4 v0, 0x1

    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method protected abstract doAction()V
.end method

.method protected doAction(I)V
    .locals 2

    sget-object v0, Lcom/amazon/android/framework/prompt/SimplePrompt;->LOGGER:Lcom/amazon/android/framework/util/KiwiLogger;

    const-string v1, "extended doAction from base class called, this should never happen."

    invoke-virtual {v0, v1}, Lcom/amazon/android/framework/util/KiwiLogger;->error(Ljava/lang/String;)V

    invoke-virtual {p0}, Lcom/amazon/android/framework/prompt/SimplePrompt;->doAction()V

    return-void
.end method

.method protected doCompatibilityCheck(Landroid/app/Activity;)Z
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->content:Lcom/amazon/android/framework/prompt/PromptContent;

    invoke-virtual {v0}, Lcom/amazon/android/framework/prompt/PromptContent;->isVisible()Z

    move-result v0

    return v0
.end method

.method public final doCreate(Landroid/app/Activity;)Landroid/app/Dialog;
    .locals 2

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/SimplePrompt;->mode:Lcom/amazon/android/framework/prompt/b;

    sget-object v1, Lcom/amazon/android/framework/prompt/b;->a:Lcom/amazon/android/framework/prompt/b;

    if-ne v0, v1, :cond_0

    invoke-direct {p0, p1}, Lcom/amazon/android/framework/prompt/SimplePrompt;->createLegacyDialog(Landroid/app/Activity;)Landroid/app/Dialog;

    move-result-object v0

    :goto_0
    return-object v0

    :cond_0
    invoke-direct {p0, p1}, Lcom/amazon/android/framework/prompt/SimplePrompt;->createExtendedDialog(Landroid/app/Activity;)Landroid/app/Dialog;

    move-result-object v0

    goto :goto_0
.end method

.method protected doExpiration(Lcom/amazon/android/framework/prompt/f;)V
    .locals 0

    invoke-virtual {p0}, Lcom/amazon/android/framework/prompt/SimplePrompt;->doAction()V

    return-void
.end method
