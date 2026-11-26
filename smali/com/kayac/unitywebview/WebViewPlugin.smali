.class public Lcom/kayac/unitywebview/WebViewPlugin;
.super Ljava/lang/Object;
.source "WebViewPlugin.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;
    }
.end annotation


# static fields
.field private static layout:Landroid/widget/FrameLayout;


# instance fields
.field private mJSInterface:Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

.field private mWebView:Landroid/webkit/WebView;

.field private name:Ljava/lang/String;

.field private res:Landroid/content/res/Resources;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .prologue
    .line 84
    const/4 v0, 0x0

    sput-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    return-void
.end method

.method public constructor <init>()V
    .locals 1

    .prologue
    const/4 v0, 0x0

    .line 62
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 87
    iput-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->res:Landroid/content/res/Resources;

    .line 88
    iput-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->name:Ljava/lang/String;

    return-void
.end method

.method private AddWebView(Landroid/app/Activity;)V
    .locals 5
    .param p1, "a"    # Landroid/app/Activity;

    .prologue
    const/4 v2, 0x1

    const/4 v4, -0x1

    .line 129
    sget-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    if-nez v0, :cond_0

    .line 130
    new-instance v0, Landroid/widget/FrameLayout;

    invoke-direct {v0, p1}, Landroid/widget/FrameLayout;-><init>(Landroid/content/Context;)V

    sput-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    .line 131
    sget-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    new-instance v1, Landroid/view/ViewGroup$LayoutParams;

    invoke-direct {v1, v4, v4}, Landroid/view/ViewGroup$LayoutParams;-><init>(II)V

    invoke-virtual {p1, v0, v1}, Landroid/app/Activity;->addContentView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    .line 133
    sget-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    invoke-virtual {v0, v2}, Landroid/widget/FrameLayout;->setFocusable(Z)V

    .line 134
    sget-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    invoke-virtual {v0, v2}, Landroid/widget/FrameLayout;->setFocusableInTouchMode(Z)V

    .line 137
    :cond_0
    sget-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    new-instance v2, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v3, 0x0

    invoke-direct {v2, v4, v4, v3}, Landroid/widget/FrameLayout$LayoutParams;-><init>(III)V

    invoke-virtual {v0, v1, v2}, Landroid/widget/FrameLayout;->addView(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V

    .line 140
    return-void
.end method

.method private CreateWebView(Landroid/app/Activity;)V
    .locals 3
    .param p1, "a"    # Landroid/app/Activity;

    .prologue
    const/4 v2, 0x1

    .line 122
    new-instance v0, Landroid/webkit/WebView;

    invoke-direct {v0, p1}, Landroid/webkit/WebView;-><init>(Landroid/content/Context;)V

    iput-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    .line 123
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    const/16 v1, 0x8

    invoke-virtual {v0, v1}, Landroid/webkit/WebView;->setVisibility(I)V

    .line 124
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    invoke-virtual {v0, v2}, Landroid/webkit/WebView;->setFocusable(Z)V

    .line 125
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    invoke-virtual {v0, v2}, Landroid/webkit/WebView;->setFocusableInTouchMode(Z)V

    .line 126
    return-void
.end method

.method private SetLayoutParams(Landroid/app/Activity;)V
    .locals 5
    .param p1, "a"    # Landroid/app/Activity;

    .prologue
    const/4 v4, 0x0

    .line 160
    new-instance v0, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v2, -0x1

    const/4 v3, 0x5

    invoke-direct {v0, v2, v3, v4}, Landroid/widget/FrameLayout$LayoutParams;-><init>(III)V

    .line 161
    .local v0, "params":Landroid/widget/FrameLayout$LayoutParams;
    iput v4, v0, Landroid/widget/FrameLayout$LayoutParams;->leftMargin:I

    .line 162
    iput v4, v0, Landroid/widget/FrameLayout$LayoutParams;->rightMargin:I

    .line 164
    const-string v2, "Bottom"

    invoke-virtual {v2}, Ljava/lang/String;->toString()Ljava/lang/String;

    move-result-object v1

    .line 165
    .local v1, "scompare":Ljava/lang/String;
    const-string v2, "Top"

    invoke-virtual {v2, v1}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v2

    if-eqz v2, :cond_0

    .line 166
    const-string v2, "WebView"

    const-string v3, "Top"

    invoke-static {v2, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 167
    iput v4, v0, Landroid/widget/FrameLayout$LayoutParams;->topMargin:I

    .line 168
    const/16 v2, 0x33

    iput v2, v0, Landroid/widget/FrameLayout$LayoutParams;->gravity:I

    .line 175
    :goto_0
    return-void

    .line 171
    :cond_0
    const-string v2, "WebView"

    const-string v3, "Bottom"

    invoke-static {v2, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    .line 172
    iput v4, v0, Landroid/widget/FrameLayout$LayoutParams;->bottomMargin:I

    .line 173
    const/16 v2, 0x53

    iput v2, v0, Landroid/widget/FrameLayout$LayoutParams;->gravity:I

    goto :goto_0
.end method

.method private SetWebView()V
    .locals 5

    .prologue
    const/4 v4, 0x0

    const/4 v3, 0x1

    .line 144
    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    new-instance v2, Landroid/webkit/WebViewClient;

    invoke-direct {v2}, Landroid/webkit/WebViewClient;-><init>()V

    invoke-virtual {v1, v2}, Landroid/webkit/WebView;->setWebViewClient(Landroid/webkit/WebViewClient;)V

    .line 147
    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    invoke-virtual {v1}, Landroid/webkit/WebView;->getSettings()Landroid/webkit/WebSettings;

    move-result-object v0

    .line 148
    .local v0, "webSettings":Landroid/webkit/WebSettings;
    invoke-virtual {v0, v4}, Landroid/webkit/WebSettings;->setSupportZoom(Z)V

    .line 149
    invoke-virtual {v0, v3}, Landroid/webkit/WebSettings;->setJavaScriptEnabled(Z)V

    .line 150
    invoke-virtual {v0, v3}, Landroid/webkit/WebSettings;->setPluginsEnabled(Z)V

    .line 151
    invoke-virtual {v0, v3}, Landroid/webkit/WebSettings;->setDatabaseEnabled(Z)V

    .line 152
    invoke-virtual {v0, v3}, Landroid/webkit/WebSettings;->setDomStorageEnabled(Z)V

    .line 153
    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    invoke-virtual {v1, v4}, Landroid/webkit/WebView;->setScrollBarStyle(I)V

    .line 154
    iget-object v1, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    invoke-virtual {v1, v3}, Landroid/webkit/WebView;->setVerticalScrollbarOverlay(Z)V

    .line 155
    return-void
.end method

.method static synthetic access$000(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/app/Activity;)V
    .locals 0
    .param p0, "x0"    # Lcom/kayac/unitywebview/WebViewPlugin;
    .param p1, "x1"    # Landroid/app/Activity;

    .prologue
    .line 62
    invoke-direct {p0, p1}, Lcom/kayac/unitywebview/WebViewPlugin;->CreateWebView(Landroid/app/Activity;)V

    return-void
.end method

.method static synthetic access$100(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/app/Activity;)V
    .locals 0
    .param p0, "x0"    # Lcom/kayac/unitywebview/WebViewPlugin;
    .param p1, "x1"    # Landroid/app/Activity;

    .prologue
    .line 62
    invoke-direct {p0, p1}, Lcom/kayac/unitywebview/WebViewPlugin;->AddWebView(Landroid/app/Activity;)V

    return-void
.end method

.method static synthetic access$200(Lcom/kayac/unitywebview/WebViewPlugin;)V
    .locals 0
    .param p0, "x0"    # Lcom/kayac/unitywebview/WebViewPlugin;

    .prologue
    .line 62
    invoke-direct {p0}, Lcom/kayac/unitywebview/WebViewPlugin;->SetWebView()V

    return-void
.end method

.method static synthetic access$300(Lcom/kayac/unitywebview/WebViewPlugin;)Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;
    .locals 1
    .param p0, "x0"    # Lcom/kayac/unitywebview/WebViewPlugin;

    .prologue
    .line 62
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mJSInterface:Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

    return-object v0
.end method

.method static synthetic access$302(Lcom/kayac/unitywebview/WebViewPlugin;Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;)Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;
    .locals 0
    .param p0, "x0"    # Lcom/kayac/unitywebview/WebViewPlugin;
    .param p1, "x1"    # Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

    .prologue
    .line 62
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mJSInterface:Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

    return-object p1
.end method

.method static synthetic access$400(Lcom/kayac/unitywebview/WebViewPlugin;)Landroid/webkit/WebView;
    .locals 1
    .param p0, "x0"    # Lcom/kayac/unitywebview/WebViewPlugin;

    .prologue
    .line 62
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    return-object v0
.end method

.method static synthetic access$402(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/webkit/WebView;)Landroid/webkit/WebView;
    .locals 0
    .param p0, "x0"    # Lcom/kayac/unitywebview/WebViewPlugin;
    .param p1, "x1"    # Landroid/webkit/WebView;

    .prologue
    .line 62
    iput-object p1, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mWebView:Landroid/webkit/WebView;

    return-object p1
.end method

.method static synthetic access$500()Landroid/widget/FrameLayout;
    .locals 1

    .prologue
    .line 62
    sget-object v0, Lcom/kayac/unitywebview/WebViewPlugin;->layout:Landroid/widget/FrameLayout;

    return-object v0
.end method


# virtual methods
.method public Destroy()V
    .locals 2

    .prologue
    .line 179
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 180
    .local v0, "a":Landroid/app/Activity;
    new-instance v1, Lcom/kayac/unitywebview/WebViewPlugin$2;

    invoke-direct {v1, p0}, Lcom/kayac/unitywebview/WebViewPlugin$2;-><init>(Lcom/kayac/unitywebview/WebViewPlugin;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 188
    return-void
.end method

.method public EvaluateJS(Ljava/lang/String;)V
    .locals 2
    .param p1, "js"    # Ljava/lang/String;

    .prologue
    .line 202
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 203
    .local v0, "a":Landroid/app/Activity;
    new-instance v1, Lcom/kayac/unitywebview/WebViewPlugin$4;

    invoke-direct {v1, p0, p1}, Lcom/kayac/unitywebview/WebViewPlugin$4;-><init>(Lcom/kayac/unitywebview/WebViewPlugin;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 208
    return-void
.end method

.method public Init(Ljava/lang/String;)V
    .locals 2
    .param p1, "gameObject"    # Ljava/lang/String;

    .prologue
    .line 92
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 94
    .local v0, "a":Landroid/app/Activity;
    new-instance v1, Lcom/kayac/unitywebview/WebViewPlugin$1;

    invoke-direct {v1, p0, v0, p1}, Lcom/kayac/unitywebview/WebViewPlugin$1;-><init>(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/app/Activity;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 119
    return-void
.end method

.method public LoadURL(Ljava/lang/String;)V
    .locals 2
    .param p1, "url"    # Ljava/lang/String;

    .prologue
    .line 192
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 193
    .local v0, "a":Landroid/app/Activity;
    new-instance v1, Lcom/kayac/unitywebview/WebViewPlugin$3;

    invoke-direct {v1, p0, p1}, Lcom/kayac/unitywebview/WebViewPlugin$3;-><init>(Lcom/kayac/unitywebview/WebViewPlugin;Ljava/lang/String;)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 198
    return-void
.end method

.method public SetMargins(IIII)V
    .locals 4
    .param p1, "left"    # I
    .param p2, "top"    # I
    .param p3, "right"    # I
    .param p4, "bottom"    # I

    .prologue
    const/4 v3, -0x1

    .line 212
    new-instance v1, Landroid/widget/FrameLayout$LayoutParams;

    const/4 v2, 0x0

    invoke-direct {v1, v3, v3, v2}, Landroid/widget/FrameLayout$LayoutParams;-><init>(III)V

    .line 215
    .local v1, "params":Landroid/widget/FrameLayout$LayoutParams;
    invoke-virtual {v1, p1, p2, p3, p4}, Landroid/widget/FrameLayout$LayoutParams;->setMargins(IIII)V

    .line 217
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 218
    .local v0, "a":Landroid/app/Activity;
    new-instance v2, Lcom/kayac/unitywebview/WebViewPlugin$5;

    invoke-direct {v2, p0, v1}, Lcom/kayac/unitywebview/WebViewPlugin$5;-><init>(Lcom/kayac/unitywebview/WebViewPlugin;Landroid/widget/FrameLayout$LayoutParams;)V

    invoke-virtual {v0, v2}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 223
    return-void
.end method

.method public SetVisibility(Z)V
    .locals 2
    .param p1, "visibility"    # Z

    .prologue
    .line 227
    sget-object v0, Lcom/unity3d/player/UnityPlayer;->currentActivity:Landroid/app/Activity;

    .line 228
    .local v0, "a":Landroid/app/Activity;
    new-instance v1, Lcom/kayac/unitywebview/WebViewPlugin$6;

    invoke-direct {v1, p0, p1}, Lcom/kayac/unitywebview/WebViewPlugin$6;-><init>(Lcom/kayac/unitywebview/WebViewPlugin;Z)V

    invoke-virtual {v0, v1}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V

    .line 239
    return-void
.end method

.method public pollWebViewMessage()Ljava/lang/String;
    .locals 1

    .prologue
    .line 242
    iget-object v0, p0, Lcom/kayac/unitywebview/WebViewPlugin;->mJSInterface:Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;

    iget-object v0, v0, Lcom/kayac/unitywebview/WebViewPlugin$JSInterface;->mMessageQueue:Ljava/util/concurrent/SynchronousQueue;

    invoke-virtual {v0}, Ljava/util/concurrent/SynchronousQueue;->poll()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    return-object v0
.end method
