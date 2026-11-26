.class final Lcom/amazon/android/framework/prompt/p;
.super Landroid/os/AsyncTask;


# instance fields
.field private final a:Lcom/amazon/android/framework/context/ContextManager;


# direct methods
.method synthetic constructor <init>(Lcom/amazon/android/framework/context/ContextManager;)V
    .locals 1

    const/4 v0, 0x0

    invoke-direct {p0, p1, v0}, Lcom/amazon/android/framework/prompt/p;-><init>(Lcom/amazon/android/framework/context/ContextManager;B)V

    return-void
.end method

.method private constructor <init>(Lcom/amazon/android/framework/context/ContextManager;B)V
    .locals 0

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    iput-object p1, p0, Lcom/amazon/android/framework/prompt/p;->a:Lcom/amazon/android/framework/context/ContextManager;

    return-void
.end method


# virtual methods
.method public final bridge synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 1

    const/4 v0, 0x0

    return-object v0
.end method

.method public final bridge synthetic onPostExecute(Ljava/lang/Object;)V
    .locals 0

    invoke-static {}, Lcom/amazon/android/framework/prompt/e;->a()V

    return-void
.end method
