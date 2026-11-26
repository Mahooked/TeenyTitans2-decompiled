.class final Lcom/amazon/android/framework/prompt/g;
.super Landroid/os/AsyncTask;


# instance fields
.field private final a:Landroid/content/Context;

.field private final b:Lcom/amazon/android/framework/context/ContextManager;


# direct methods
.method synthetic constructor <init>(Landroid/content/Context;Lcom/amazon/android/framework/context/ContextManager;)V
    .locals 1

    const/4 v0, 0x0

    invoke-direct {p0, p1, p2, v0}, Lcom/amazon/android/framework/prompt/g;-><init>(Landroid/content/Context;Lcom/amazon/android/framework/context/ContextManager;B)V

    return-void
.end method

.method private constructor <init>(Landroid/content/Context;Lcom/amazon/android/framework/context/ContextManager;B)V
    .locals 0

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    iput-object p1, p0, Lcom/amazon/android/framework/prompt/g;->a:Landroid/content/Context;

    iput-object p2, p0, Lcom/amazon/android/framework/prompt/g;->b:Lcom/amazon/android/framework/context/ContextManager;

    return-void
.end method


# virtual methods
.method public final bridge synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 2

    check-cast p1, [Landroid/content/Intent;

    iget-object v0, p0, Lcom/amazon/android/framework/prompt/g;->a:Landroid/content/Context;

    const/4 v1, 0x0

    aget-object v1, p1, v1

    invoke-virtual {v0, v1}, Landroid/content/Context;->startActivity(Landroid/content/Intent;)V

    const/4 v0, 0x0

    return-object v0
.end method

.method public final bridge synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    invoke-static {}, Lcom/amazon/android/framework/prompt/e;->a()V

    return-void
.end method
