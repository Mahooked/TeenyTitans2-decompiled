.class public final Lcom/amazon/android/f;
.super Lcom/amazon/android/k/b;


# direct methods
.method public constructor <init>()V
    .locals 1

    invoke-direct {p0}, Lcom/amazon/android/k/b;-><init>()V

    new-instance v0, Lcom/amazon/android/g;

    invoke-direct {v0}, Lcom/amazon/android/g;-><init>()V

    invoke-virtual {p0, v0}, Lcom/amazon/android/f;->a(Lcom/amazon/android/framework/task/Task;)V

    new-instance v0, Lcom/amazon/android/q/a;

    invoke-direct {v0}, Lcom/amazon/android/q/a;-><init>()V

    invoke-virtual {p0, v0}, Lcom/amazon/android/f;->a(Lcom/amazon/android/framework/task/Task;)V

    new-instance v0, Lcom/amazon/android/f/a;

    invoke-direct {v0}, Lcom/amazon/android/f/a;-><init>()V

    invoke-virtual {p0, v0}, Lcom/amazon/android/f;->a(Lcom/amazon/android/framework/task/Task;)V

    return-void
.end method


# virtual methods
.method protected final b()Ljava/lang/String;
    .locals 1

    const-string v0, "DrmFreeApplicationLaunchTaskWorkflow"

    return-object v0
.end method
