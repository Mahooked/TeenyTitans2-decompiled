.class final Ljp/ne/donuts/uniclipboard/Clipboard$1;
.super Ljava/lang/Object;
.source "Clipboard.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Ljp/ne/donuts/uniclipboard/Clipboard;->setText(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation


# instance fields
.field final synthetic val$activity:Landroid/app/Activity;

.field final synthetic val$text:Ljava/lang/String;


# direct methods
.method constructor <init>(Ljava/lang/String;Landroid/app/Activity;)V
    .locals 0

    .prologue
    .line 18
    iput-object p1, p0, Ljp/ne/donuts/uniclipboard/Clipboard$1;->val$text:Ljava/lang/String;

    iput-object p2, p0, Ljp/ne/donuts/uniclipboard/Clipboard$1;->val$activity:Landroid/app/Activity;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 6

    .prologue
    .line 21
    new-instance v2, Landroid/content/ClipData$Item;

    iget-object v4, p0, Ljp/ne/donuts/uniclipboard/Clipboard$1;->val$text:Ljava/lang/String;

    invoke-direct {v2, v4}, Landroid/content/ClipData$Item;-><init>(Ljava/lang/CharSequence;)V

    .line 22
    .local v2, "item":Landroid/content/ClipData$Item;
    const/4 v4, 0x1

    new-array v3, v4, [Ljava/lang/String;

    .line 23
    .local v3, "mimeType":[Ljava/lang/String;
    const/4 v4, 0x0

    const-string v5, "text/uri-list"

    aput-object v5, v3, v4

    .line 24
    new-instance v0, Landroid/content/ClipData;

    new-instance v4, Landroid/content/ClipDescription;

    const-string v5, "text_data"

    invoke-direct {v4, v5, v3}, Landroid/content/ClipDescription;-><init>(Ljava/lang/CharSequence;[Ljava/lang/String;)V

    invoke-direct {v0, v4, v2}, Landroid/content/ClipData;-><init>(Landroid/content/ClipDescription;Landroid/content/ClipData$Item;)V

    .line 25
    .local v0, "cd":Landroid/content/ClipData;
    iget-object v4, p0, Ljp/ne/donuts/uniclipboard/Clipboard$1;->val$activity:Landroid/app/Activity;

    const-string v5, "clipboard"

    invoke-virtual {v4, v5}, Landroid/app/Activity;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/content/ClipboardManager;

    .line 26
    .local v1, "cm":Landroid/content/ClipboardManager;
    invoke-virtual {v1, v0}, Landroid/content/ClipboardManager;->setPrimaryClip(Landroid/content/ClipData;)V

    .line 27
    return-void
.end method
