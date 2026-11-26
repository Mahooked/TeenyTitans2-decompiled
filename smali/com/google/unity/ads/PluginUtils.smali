.class public Lcom/google/unity/ads/PluginUtils;
.super Ljava/lang/Object;
.source "PluginUtils.java"


# static fields
.field public static final LOGTAG:Ljava/lang/String; = "AdsUnity"

.field private static final POSITION_BOTTOM:I = 0x1

.field private static final POSITION_BOTTOM_LEFT:I = 0x4

.field private static final POSITION_BOTTOM_RIGHT:I = 0x5

.field private static final POSITION_CENTER:I = 0x6

.field public static final POSITION_CUSTOM:I = -0x1

.field private static final POSITION_TOP:I = 0x0

.field private static final POSITION_TOP_LEFT:I = 0x2

.field private static final POSITION_TOP_RIGHT:I = 0x3


# direct methods
.method public constructor <init>()V
    .locals 0

    .prologue
    .line 32
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static convertDpToPixel(F)F
    .locals 3
    .param p0, "dp"    # F

    .prologue
    .line 145
    invoke-static {}, Landroid/content/res/Resources;->getSystem()Landroid/content/res/Resources;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v0

    .line 146
    .local v0, "metrics":Landroid/util/DisplayMetrics;
    iget v2, v0, Landroid/util/DisplayMetrics;->density:F

    mul-float v1, p0, v2

    .line 147
    .local v1, "px":F
    return v1
.end method

.method public static convertPixelsToDp(F)F
    .locals 3
    .param p0, "px"    # F

    .prologue
    .line 139
    invoke-static {}, Landroid/content/res/Resources;->getSystem()Landroid/content/res/Resources;

    move-result-object v2

    invoke-virtual {v2}, Landroid/content/res/Resources;->getDisplayMetrics()Landroid/util/DisplayMetrics;

    move-result-object v1

    .line 140
    .local v1, "metrics":Landroid/util/DisplayMetrics;
    iget v2, v1, Landroid/util/DisplayMetrics;->density:F

    div-float v0, p0, v2

    .line 141
    .local v0, "dp":F
    return v0
.end method

.method public static getErrorReason(I)Ljava/lang/String;
    .locals 5
    .param p0, "errorCode"    # I

    .prologue
    .line 86
    packed-switch p0, :pswitch_data_0

    .line 96
    const-string v0, "AdsUnity"

    const-string v1, "Unexpected error code: %s"

    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-static {p0}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-static {v1, v2}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    .line 97
    const-string v0, ""

    :goto_0
    return-object v0

    .line 88
    :pswitch_0
    const-string v0, "Internal error"

    goto :goto_0

    .line 90
    :pswitch_1
    const-string v0, "Invalid request"

    goto :goto_0

    .line 92
    :pswitch_2
    const-string v0, "Network Error"

    goto :goto_0

    .line 94
    :pswitch_3
    const-string v0, "No fill"

    goto :goto_0

    .line 86
    nop

    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_0
        :pswitch_1
        :pswitch_2
        :pswitch_3
    .end packed-switch
.end method

.method public static getLayoutGravityForPositionCode(I)I
    .locals 3
    .param p0, "positionCode"    # I

    .prologue
    .line 109
    packed-switch p0, :pswitch_data_0

    .line 132
    new-instance v1, Ljava/lang/IllegalArgumentException;

    const-string v2, "Attempted to position ad with invalid ad position."

    invoke-direct {v1, v2}, Ljava/lang/IllegalArgumentException;-><init>(Ljava/lang/String;)V

    throw v1

    .line 111
    :pswitch_0
    const/16 v0, 0x31

    .line 135
    .local v0, "gravity":I
    :goto_0
    return v0

    .line 114
    .end local v0    # "gravity":I
    :pswitch_1
    const/16 v0, 0x51

    .line 115
    .restart local v0    # "gravity":I
    goto :goto_0

    .line 117
    .end local v0    # "gravity":I
    :pswitch_2
    const/16 v0, 0x33

    .line 118
    .restart local v0    # "gravity":I
    goto :goto_0

    .line 120
    .end local v0    # "gravity":I
    :pswitch_3
    const/16 v0, 0x35

    .line 121
    .restart local v0    # "gravity":I
    goto :goto_0

    .line 123
    .end local v0    # "gravity":I
    :pswitch_4
    const/16 v0, 0x53

    .line 124
    .restart local v0    # "gravity":I
    goto :goto_0

    .line 126
    .end local v0    # "gravity":I
    :pswitch_5
    const/16 v0, 0x55

    .line 127
    .restart local v0    # "gravity":I
    goto :goto_0

    .line 129
    .end local v0    # "gravity":I
    :pswitch_6
    const/16 v0, 0x11

    .line 130
    .restart local v0    # "gravity":I
    goto :goto_0

    .line 109
    :pswitch_data_0
    .packed-switch 0x0
        :pswitch_0
        :pswitch_1
        :pswitch_2
        :pswitch_3
        :pswitch_4
        :pswitch_5
        :pswitch_6
    .end packed-switch
.end method

.method public static setPopUpWindowLayoutType(Landroid/widget/PopupWindow;I)V
    .locals 9
    .param p0, "popupWindow"    # Landroid/widget/PopupWindow;
    .param p1, "layoutType"    # I

    .prologue
    const/4 v8, 0x0

    const/4 v7, 0x1

    .line 152
    :try_start_0
    const-class v2, Landroid/widget/PopupWindow;

    const-string v3, "setWindowLayoutType"

    const/4 v4, 0x1

    new-array v4, v4, [Ljava/lang/Class;

    const/4 v5, 0x0

    sget-object v6, Ljava/lang/Integer;->TYPE:Ljava/lang/Class;

    aput-object v6, v4, v5

    invoke-virtual {v2, v3, v4}, Ljava/lang/Class;->getDeclaredMethod(Ljava/lang/String;[Ljava/lang/Class;)Ljava/lang/reflect/Method;

    move-result-object v1

    .line 153
    .local v1, "method":Ljava/lang/reflect/Method;
    const/4 v2, 0x1

    invoke-virtual {v1, v2}, Ljava/lang/reflect/Method;->setAccessible(Z)V

    .line 154
    const/4 v2, 0x1

    new-array v2, v2, [Ljava/lang/Object;

    const/4 v3, 0x0

    invoke-static {p1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    aput-object v4, v2, v3

    invoke-virtual {v1, p0, v2}, Ljava/lang/reflect/Method;->invoke(Ljava/lang/Object;[Ljava/lang/Object;)Ljava/lang/Object;
    :try_end_0
    .catch Ljava/lang/NoSuchMethodException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalAccessException; {:try_start_0 .. :try_end_0} :catch_1
    .catch Ljava/lang/reflect/InvocationTargetException; {:try_start_0 .. :try_end_0} :catch_2

    .line 165
    .end local v1    # "method":Ljava/lang/reflect/Method;
    :goto_0
    return-void

    .line 155
    :catch_0
    move-exception v0

    .line 156
    .local v0, "exception":Ljava/lang/NoSuchMethodException;
    const-string v2, "AdsUnity"

    const-string v3, "Unable to set popUpWindow window layout type: %s"

    new-array v4, v7, [Ljava/lang/Object;

    .line 157
    invoke-virtual {v0}, Ljava/lang/NoSuchMethodException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v8

    .line 156
    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    .line 158
    .end local v0    # "exception":Ljava/lang/NoSuchMethodException;
    :catch_1
    move-exception v0

    .line 159
    .local v0, "exception":Ljava/lang/IllegalAccessException;
    const-string v2, "AdsUnity"

    const-string v3, "Unable to set popUpWindow window layout type: %s"

    new-array v4, v7, [Ljava/lang/Object;

    .line 160
    invoke-virtual {v0}, Ljava/lang/IllegalAccessException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v8

    .line 159
    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0

    .line 161
    .end local v0    # "exception":Ljava/lang/IllegalAccessException;
    :catch_2
    move-exception v0

    .line 162
    .local v0, "exception":Ljava/lang/reflect/InvocationTargetException;
    const-string v2, "AdsUnity"

    const-string v3, "Unable to set popUpWindow window layout type: %s"

    new-array v4, v7, [Ljava/lang/Object;

    .line 163
    invoke-virtual {v0}, Ljava/lang/reflect/InvocationTargetException;->getLocalizedMessage()Ljava/lang/String;

    move-result-object v5

    aput-object v5, v4, v8

    .line 162
    invoke-static {v3, v4}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Landroid/util/Log;->d(Ljava/lang/String;Ljava/lang/String;)I

    goto :goto_0
.end method
