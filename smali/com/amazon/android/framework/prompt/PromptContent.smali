.class public Lcom/amazon/android/framework/prompt/PromptContent;
.super Ljava/lang/Object;


# instance fields
.field private final buttonActions:[Lcom/amazon/android/framework/prompt/a;

.field private final buttonLabels:[Ljava/lang/String;

.field private final errorCode:I

.field private final legacyButton:I

.field private final legacyTitle:Ljava/lang/String;

.field private final message:Ljava/lang/String;

.field private final shouldShowFixup:Z

.field private final title:Ljava/lang/String;

.field private final visible:Z


# direct methods
.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Z)V
    .locals 6

    const/4 v5, 0x0

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move v4, p4

    invoke-direct/range {v0 .. v5}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZ)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ZZ)V
    .locals 8

    const/4 v0, 0x1

    const/4 v7, 0x0

    new-array v3, v0, [Ljava/lang/String;

    aput-object p3, v3, v7

    new-array v4, v0, [Lcom/amazon/android/framework/prompt/a;

    sget-object v0, Lcom/amazon/android/framework/prompt/a;->a:Lcom/amazon/android/framework/prompt/a;

    aput-object v0, v4, v7

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move v5, p4

    move v6, p5

    invoke-direct/range {v0 .. v7}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZI)V

    return-void
.end method

.method public constructor <init>(Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZI)V
    .locals 8

    const/4 v0, 0x1

    new-array v1, v0, [Ljava/lang/String;

    const/4 v0, 0x0

    aput-object p1, v1, v0

    move-object v0, p0

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    move v5, p5

    move v6, p6

    move v7, p7

    invoke-direct/range {v0 .. v7}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZI)V

    return-void
.end method

.method public constructor <init>([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZI)V
    .locals 9

    const/4 v8, -0x1

    move-object v0, p0

    move-object v1, p1

    move-object v2, p2

    move-object v3, p3

    move-object v4, p4

    move v5, p5

    move v6, p6

    move/from16 v7, p7

    invoke-direct/range {v0 .. v8}, Lcom/amazon/android/framework/prompt/PromptContent;-><init>([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZII)V

    return-void
.end method

.method public constructor <init>([Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;[Lcom/amazon/android/framework/prompt/a;ZZII)V
    .locals 3

    const/4 v2, 0x1

    const/4 v1, 0x0

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    aget-object v0, p1, v1

    iput-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->title:Ljava/lang/String;

    array-length v0, p1

    if-le v0, v2, :cond_0

    aget-object v0, p1, v2

    iput-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->legacyTitle:Ljava/lang/String;

    :goto_0
    iput-object p2, p0, Lcom/amazon/android/framework/prompt/PromptContent;->message:Ljava/lang/String;

    iput-object p3, p0, Lcom/amazon/android/framework/prompt/PromptContent;->buttonLabels:[Ljava/lang/String;

    iput-object p4, p0, Lcom/amazon/android/framework/prompt/PromptContent;->buttonActions:[Lcom/amazon/android/framework/prompt/a;

    iput-boolean p5, p0, Lcom/amazon/android/framework/prompt/PromptContent;->visible:Z

    iput-boolean p6, p0, Lcom/amazon/android/framework/prompt/PromptContent;->shouldShowFixup:Z

    iput p7, p0, Lcom/amazon/android/framework/prompt/PromptContent;->legacyButton:I

    iput p8, p0, Lcom/amazon/android/framework/prompt/PromptContent;->errorCode:I

    return-void

    :cond_0
    aget-object v0, p1, v1

    iput-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->legacyTitle:Ljava/lang/String;

    goto :goto_0
.end method


# virtual methods
.method public getButtonActions()[Lcom/amazon/android/framework/prompt/a;
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->buttonActions:[Lcom/amazon/android/framework/prompt/a;

    return-object v0
.end method

.method public getButtonLabel()Ljava/lang/String;
    .locals 2

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->buttonLabels:[Ljava/lang/String;

    iget v1, p0, Lcom/amazon/android/framework/prompt/PromptContent;->legacyButton:I

    aget-object v0, v0, v1

    return-object v0
.end method

.method public getButtonLabels()[Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->buttonLabels:[Ljava/lang/String;

    return-object v0
.end method

.method public getErrorCode()I
    .locals 1

    iget v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->errorCode:I

    return v0
.end method

.method public getExtendedTitle()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->title:Ljava/lang/String;

    return-object v0
.end method

.method public getMessage()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->message:Ljava/lang/String;

    return-object v0
.end method

.method public getTitle()Ljava/lang/String;
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->legacyTitle:Ljava/lang/String;

    return-object v0
.end method

.method public isVisible()Z
    .locals 1

    iget-boolean v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->visible:Z

    return v0
.end method

.method public shouldShowFixup()Z
    .locals 1

    iget-boolean v0, p0, Lcom/amazon/android/framework/prompt/PromptContent;->shouldShowFixup:Z

    return v0
.end method

.method public toString()Ljava/lang/String;
    .locals 4

    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "PromptContent: [ title:"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v1, p0, Lcom/amazon/android/framework/prompt/PromptContent;->title:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", message: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Lcom/amazon/android/framework/prompt/PromptContent;->message:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", label: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Lcom/amazon/android/framework/prompt/PromptContent;->buttonLabels:[Ljava/lang/String;

    const/4 v3, 0x0

    aget-object v2, v2, v3

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", visible: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-boolean v2, p0, Lcom/amazon/android/framework/prompt/PromptContent;->visible:Z

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, ", shouldShowFixup: "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-boolean v2, p0, Lcom/amazon/android/framework/prompt/PromptContent;->shouldShowFixup:Z

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Z)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, "]"

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    return-object v0
.end method
