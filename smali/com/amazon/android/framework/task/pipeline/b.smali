.class final Lcom/amazon/android/framework/task/pipeline/b;
.super Ljava/lang/Object;

# interfaces
.implements Lcom/amazon/android/m/c;


# instance fields
.field private synthetic a:Lcom/amazon/android/framework/task/pipeline/e;


# direct methods
.method constructor <init>(Lcom/amazon/android/framework/task/pipeline/e;)V
    .locals 0

    iput-object p1, p0, Lcom/amazon/android/framework/task/pipeline/b;->a:Lcom/amazon/android/framework/task/pipeline/e;

    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final a()Lcom/amazon/android/m/f;
    .locals 1

    sget-object v0, Lcom/amazon/android/j/c;->c:Lcom/amazon/android/j/c;

    return-object v0
.end method

.method public final bridge synthetic a(Lcom/amazon/android/m/d;)V
    .locals 1

    iget-object v0, p0, Lcom/amazon/android/framework/task/pipeline/b;->a:Lcom/amazon/android/framework/task/pipeline/e;

    invoke-static {v0}, Lcom/amazon/android/framework/task/pipeline/e;->a(Lcom/amazon/android/framework/task/pipeline/e;)V

    return-void
.end method

.method public final b()Lcom/amazon/android/m/a;
    .locals 1

    sget-object v0, Lcom/amazon/android/m/a;->b:Lcom/amazon/android/m/a;

    return-object v0
.end method

.method public final toString()Ljava/lang/String;
    .locals 1

    const-string v0, "ForegroundTaskPipeline:onResume listener"

    return-object v0
.end method
