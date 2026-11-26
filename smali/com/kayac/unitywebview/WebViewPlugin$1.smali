.class Lcom/kayac/unitywebview/WebViewPlugin$1;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/kayac/unitywebview/WebViewPlugin;->Init(Ljava/lang/String;)V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/kayac/unitywebview/WebViewPlugin;

.field final synthetic val$a:Landroid/app/Activity;

.field final synthetic val$gameObject:Ljava/lang/String;


# direct methods
.method constructor <init>(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/app/Activity;Ljava/lang/String;)V
    .locals 0

    .prologue
    .line 94
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    iput-object p2, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->val$a:Landroid/app/Activity;

    iput-object p3, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->val$gameObject:Ljava/lang/String;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 3

    .prologue
    .line 96
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->val$a:Landroid/app/Activity;

    invoke-static {v0, v1}, Lcom/kayac/unitywebview/WebViewPlugin;->access$000(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/app/Activity;)V

    .line 97
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->val$a:Landroid/app/Activity;

    invoke-static {v0, v1}, Lcom/kayac/unitywebview/WebViewPlugin;->access$100(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/app/Activity;)V

    .line 98
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$200(Lcom/kayac/unitywebview/WebViewPlugin;)V

    .line 101
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    new-instance v1, Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

    iget-object v2, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-direct {v1, v2}, Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;-><init>(Lcom/kayac/unitywebview/WebViewPlugin;)V

    invoke-static {v0, v1}, Lcom/kayac/unitywebview/WebViewPlugin;->access$302(Lcom/kayac/unitywebview/WebViewPlugin;Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;)Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

    .line 102
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v1}, Lcom/kayac/unitywebview/WebViewPlugin;->access$300(Lcom/kayac/unitywebview/WebViewPlugin;)Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

    move-result-object v1

    const-string v2, "UnityInterface"

    invoke-virtual {v0, v1, v2}, Landroid/webkit/WebView;->addJavascriptInterface(Ljava/lang/Object;Ljava/lang/String;)V

    .line 105
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    new-instance v1, Landroid/webkit/WebChromeClient;

    invoke-direct {v1}, Landroid/webkit/WebChromeClient;-><init>()V

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setWebChromeClient(Landroid/webkit/WebChromeClient;)V

    .line 106
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$1;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    new-instance v1, Lcom/kayac/unitywebview/WebViewPlugin$1$1;

    invoke-direct {v1, p0}, Lcom/kayac/unitywebview/WebViewPlugin$1$1;-><init>(Lcom/kayac/unitywebview/WebViewPlugin$1;)V

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    .line 118
    return-void
.end method
