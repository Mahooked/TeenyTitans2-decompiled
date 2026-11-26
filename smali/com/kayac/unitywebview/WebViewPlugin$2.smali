.class Lcom/kayac/unitywebview/WebViewPlugin$2;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"

# interfaces
.implements Ljava/lang/Runnable;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/kayac/unitywebview/WebViewPlugin;->Destroy()V
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x0
    name = null
.end annotation


# instance fields
.field final synthetic this$0:Lcom/kayac/unitywebview/WebViewPlugin;


# direct methods
.method constructor <init>(Lcom/kayac/unitywebview/WebViewPlugin;)V
    .locals 0

    .prologue
    .line 180
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin$2;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public run()V
    .locals 2

    .prologue
    .line 182
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$2;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v0}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v0

    if-eqz v0, :cond_0

    .line 183
    invoke-static {}, Lcom/kayac/unitywebview/WebViewPlugin;->access$500()Landroid/widget/FrameLayout;

    move-result-object v0

    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin$2;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    invoke-static {v1}, Lcom/kayac/unitywebview/WebViewPlugin;->access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/widget/FrameLayout;->removeView(Landroid/view/View;)V

    .line 184
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin$2;->this$0:Lcom/kayac/unitywebview/WebViewPlugin;

    const/4 v1, 0x0

    invoke-static {v0, v1}, Lcom/kayac/unitywebview/WebViewPlugin;->access$402(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/webkit/WebView;)Landroid/webkit/WebView;

    .line 187
    :cond_0
    return-void
.end method
