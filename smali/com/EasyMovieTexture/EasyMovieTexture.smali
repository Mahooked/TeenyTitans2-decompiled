.class public Lcom/EasyMovieTexture/EasyMovieTexture;
.super Ljava/lang/Object;
.source "EasyMovieTexture.java"

# interfaces
.implements Landroid/media/MediaPlayer$OnPreparedListener;
.implements Landroid/media/MediaPlayer$OnBufferingUpdateListener;
.implements Landroid/media/MediaPlayer$OnCompletionListener;
.implements Landroid/media/MediaPlayer$OnErrorListener;
.implements Landroid/graphics/SurfaceTexture$OnFrameAvailableListener;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;
    }
.end annotation


# static fields
.field private static final GL_TEXTURE_EXTERNAL_OES:I = 0x8d65

.field public static m_objCtrl:Ljava/util/ArrayList;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/ArrayList",
            "<",
            "Lcom/EasyMovieTexture/EasyMovieTexture;",
            ">;"
        }
    .end annotation
.end field


# instance fields
.field private m_MediaPlayer:Landroid/media/MediaPlayer;

.field private m_Surface:Landroid/view/Surface;

.field private m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

.field private m_UnityActivity:Landroid/app/Activity;

.field private m_bRockchip:Z

.field private m_bSplitOBB:Z

.field public m_bUpdate:Z

.field private m_iCurrentSeekPercent:I

.field private m_iCurrentSeekPosition:I

.field m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

.field private m_iErrorCode:I

.field private m_iErrorCodeExtra:I

.field public m_iNativeMgrID:I

.field private m_iSurfaceTextureID:I

.field private m_iUnityTextureID:I

.field private m_strFileName:Ljava/lang/String;

.field private m_strOBBName:Ljava/lang/String;


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .prologue
    .line 56
    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    sput-object v0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    .line 92
    const-string v0, "BlueDoveMediaRender"

    invoke-static {v0}, Ljava/lang/System;->loadLibrary(Ljava/lang/String;)V

    .line 93
    return-void
.end method

.method public constructor <init>()V
    .locals 3

    .prologue
    const/4 v2, -0x1

    const/4 v0, 0x0

    const/4 v1, 0x0

    .line 37
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 38
    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_UnityActivity:Landroid/app/Activity;

    .line 39
    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    .line 41
    iput v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iUnityTextureID:I

    .line 42
    iput v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    .line 43
    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    .line 44
    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_Surface:Landroid/view/Surface;

    .line 45
    iput v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentSeekPercent:I

    .line 46
    iput v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentSeekPosition:I

    .line 51
    const/4 v0, 0x1

    iput-boolean v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bRockchip:Z

    .line 52
    iput-boolean v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bSplitOBB:Z

    .line 54
    iput-boolean v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bUpdate:Z

    .line 95
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->NOT_READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 37
    return-void
.end method

.method public static GetObject(I)Lcom/EasyMovieTexture/EasyMovieTexture;
    .locals 2
    .param p0, "iID"    # I

    .prologue
    .line 60
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    invoke-virtual {v1}, Ljava/util/ArrayList;->size()I

    move-result v1

    if-lt v0, v1, :cond_0

    .line 68
    const/4 v1, 0x0

    :goto_1
    return-object v1

    .line 62
    :cond_0
    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    invoke-virtual {v1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/EasyMovieTexture/EasyMovieTexture;

    iget v1, v1, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    if-ne v1, p0, :cond_1

    .line 64
    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    invoke-virtual {v1, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Lcom/EasyMovieTexture/EasyMovieTexture;

    goto :goto_1

    .line 60
    :cond_1
    add-int/lit8 v0, v0, 0x1

    goto :goto_0
.end method


# virtual methods
.method public Destroy()V
    .locals 6

    .prologue
    const/4 v5, 0x1

    const/4 v4, 0x0

    const/4 v3, -0x1

    .line 100
    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    if-eq v2, v3, :cond_0

    .line 102
    new-array v1, v5, [I

    .line 103
    .local v1, "textures":[I
    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    aput v2, v1, v4

    .line 104
    invoke-static {v5, v1, v4}, Landroid/opengl/GLES20;->glDeleteTextures(I[II)V

    .line 105
    iput v3, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    .line 109
    .end local v1    # "textures":[I
    :cond_0
    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {p0, v2}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetManagerID(I)V

    .line 110
    invoke-virtual {p0}, Lcom/EasyMovieTexture/EasyMovieTexture;->QuitApplication()V

    .line 113
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    sget-object v2, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    invoke-virtual {v2}, Ljava/util/ArrayList;->size()I

    move-result v2

    if-lt v0, v2, :cond_1

    .line 127
    return-void

    .line 115
    :cond_1
    sget-object v2, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->get(I)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Lcom/EasyMovieTexture/EasyMovieTexture;

    iget v2, v2, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    iget v3, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    if-ne v2, v3, :cond_2

    .line 118
    const-string v2, "jni"

    new-instance v3, Ljava/lang/StringBuilder;

    const-string v4, " "

    invoke-direct {v3, v4}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget v4, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v3

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 120
    sget-object v2, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    invoke-virtual {v2, v0}, Ljava/util/ArrayList;->remove(I)Ljava/lang/Object;

    .line 113
    :cond_2
    add-int/lit8 v0, v0, 0x1

    goto :goto_0
.end method

.method public GetCurrentSeekPercent()I
    .locals 1

    .prologue
    .line 489
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentSeekPercent:I

    return v0
.end method

.method public GetDuration()I
    .locals 1

    .prologue
    .line 616
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 618
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->getDuration()I

    move-result v0

    .line 621
    :goto_0
    return v0

    :cond_0
    const/4 v0, -0x1

    goto :goto_0
.end method

.method public GetError()I
    .locals 1

    .prologue
    .line 729
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iErrorCode:I

    return v0
.end method

.method public GetErrorExtra()I
    .locals 1

    .prologue
    .line 734
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iErrorCodeExtra:I

    return v0
.end method

.method public native GetManagerID()I
.end method

.method public GetSeekPosition()I
    .locals 3

    .prologue
    .line 468
    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v1, :cond_1

    .line 470
    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v2, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v1, v2, :cond_0

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v2, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v1, v2, :cond_0

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v2, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PAUSED:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v1, v2, :cond_1

    .line 473
    :cond_0
    :try_start_0
    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v1}, Landroid/media/MediaPlayer;->getCurrentPosition()I

    move-result v1

    iput v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentSeekPosition:I
    :try_end_0
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_1

    .line 484
    :cond_1
    :goto_0
    iget v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentSeekPosition:I

    return v1

    .line 474
    :catch_0
    move-exception v0

    .line 476
    .local v0, "e":Ljava/lang/SecurityException;
    invoke-virtual {v0}, Ljava/lang/SecurityException;->printStackTrace()V

    goto :goto_0

    .line 477
    .end local v0    # "e":Ljava/lang/SecurityException;
    :catch_1
    move-exception v0

    .line 479
    .local v0, "e":Ljava/lang/IllegalStateException;
    invoke-virtual {v0}, Ljava/lang/IllegalStateException;->printStackTrace()V

    goto :goto_0
.end method

.method public GetSoundTrack()[I
    .locals 6

    .prologue
    const/4 v5, 0x2

    .line 636
    const/4 v1, 0x0

    .line 637
    .local v1, "iAudio":[I
    iget-object v4, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v4, :cond_0

    .line 639
    iget-object v4, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v4}, Landroid/media/MediaPlayer;->getTrackInfo()[Landroid/media/MediaPlayer$TrackInfo;

    move-result-object v3

    .line 641
    .local v3, "info":[Landroid/media/MediaPlayer$TrackInfo;
    const/4 v2, 0x0

    .line 642
    .local v2, "iCount":I
    const/4 v0, 0x0

    .local v0, "i":I
    :goto_0
    array-length v4, v3

    if-lt v0, v4, :cond_1

    .line 650
    if-eqz v2, :cond_0

    .line 652
    new-array v1, v2, [I

    .line 655
    const/4 v2, 0x0

    .line 656
    const/4 v0, 0x0

    :goto_1
    array-length v4, v3

    if-lt v0, v4, :cond_3

    .line 668
    .end local v0    # "i":I
    .end local v2    # "iCount":I
    .end local v3    # "info":[Landroid/media/MediaPlayer$TrackInfo;
    :cond_0
    return-object v1

    .line 644
    .restart local v0    # "i":I
    .restart local v2    # "iCount":I
    .restart local v3    # "info":[Landroid/media/MediaPlayer$TrackInfo;
    :cond_1
    aget-object v4, v3, v0

    invoke-virtual {v4}, Landroid/media/MediaPlayer$TrackInfo;->getTrackType()I

    move-result v4

    if-ne v4, v5, :cond_2

    .line 646
    add-int/lit8 v2, v2, 0x1

    .line 642
    :cond_2
    add-int/lit8 v0, v0, 0x1

    goto :goto_0

    .line 658
    :cond_3
    aget-object v4, v3, v0

    invoke-virtual {v4}, Landroid/media/MediaPlayer$TrackInfo;->getTrackType()I

    move-result v4

    if-ne v4, v5, :cond_4

    .line 660
    aput v0, v1, v2

    .line 661
    add-int/lit8 v2, v2, 0x1

    .line 656
    :cond_4
    add-int/lit8 v0, v0, 0x1

    goto :goto_1
.end method

.method public GetStatus()I
    .locals 1

    .prologue
    .line 710
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    invoke-virtual {v0}, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->GetValue()I

    move-result v0

    return v0
.end method

.method public GetVideoHeight()I
    .locals 1

    .prologue
    .line 574
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 576
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->getVideoHeight()I

    move-result v0

    .line 579
    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method public GetVideoWidth()I
    .locals 1

    .prologue
    .line 564
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 566
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->getVideoWidth()I

    move-result v0

    .line 569
    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method public native InitApplication()I
.end method

.method public native InitExtTexture()I
.end method

.method public InitJniManager()V
    .locals 1

    .prologue
    .line 701
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {p0, v0}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetManagerID(I)V

    .line 702
    invoke-virtual {p0}, Lcom/EasyMovieTexture/EasyMovieTexture;->InitApplication()I

    .line 703
    return-void
.end method

.method public native InitNDK(Ljava/lang/Object;)I
.end method

.method public InitNative(Lcom/EasyMovieTexture/EasyMovieTexture;)I
    .locals 1
    .param p1, "obj"    # Lcom/EasyMovieTexture/EasyMovieTexture;

    .prologue
    .line 677
    invoke-virtual {p0, p1}, Lcom/EasyMovieTexture/EasyMovieTexture;->InitNDK(Ljava/lang/Object;)I

    move-result v0

    iput v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    .line 678
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_objCtrl:Ljava/util/ArrayList;

    invoke-virtual {v0, p0}, Ljava/util/ArrayList;->add(Ljava/lang/Object;)Z

    .line 680
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    return v0
.end method

.method public IsUpdateFrame()Z
    .locals 1

    .prologue
    .line 584
    iget-boolean v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bUpdate:Z

    if-eqz v0, :cond_0

    .line 586
    const/4 v0, 0x1

    .line 590
    :goto_0
    return v0

    :cond_0
    const/4 v0, 0x0

    goto :goto_0
.end method

.method public Load()Z
    .locals 15
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/lang/SecurityException;,
            Ljava/lang/IllegalStateException;,
            Ljava/io/IOException;
        }
    .end annotation

    .prologue
    const/4 v14, 0x0

    .line 196
    invoke-virtual {p0}, Lcom/EasyMovieTexture/EasyMovieTexture;->UnLoad()V

    .line 199
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->NOT_READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 202
    new-instance v0, Landroid/media/MediaPlayer;

    invoke-direct {v0}, Landroid/media/MediaPlayer;-><init>()V

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    .line 203
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    const/4 v1, 0x3

    invoke-virtual {v0, v1}, Landroid/media/MediaPlayer;->setAudioStreamType(I)V

    .line 206
    iput-boolean v14, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bUpdate:Z

    .line 208
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    const-string v1, "file://"

    invoke-virtual {v0, v1}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_2

    .line 211
    :try_start_0
    new-instance v13, Ljava/io/File;

    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    const-string v1, "file://"

    const-string v2, ""

    invoke-virtual {v0, v1, v2}, Ljava/lang/String;->replace(Ljava/lang/CharSequence;Ljava/lang/CharSequence;)Ljava/lang/String;

    move-result-object v0

    invoke-direct {v13, v0}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    .line 213
    .local v13, "sourceFile":Ljava/io/File;
    invoke-virtual {v13}, Ljava/io/File;->exists()Z

    move-result v0

    if-eqz v0, :cond_0

    .line 215
    new-instance v10, Ljava/io/FileInputStream;

    invoke-direct {v10, v13}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    .line 216
    .local v10, "fs":Ljava/io/FileInputStream;
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v10}, Ljava/io/FileInputStream;->getFD()Ljava/io/FileDescriptor;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/media/MediaPlayer;->setDataSource(Ljava/io/FileDescriptor;)V

    .line 217
    invoke-virtual {v10}, Ljava/io/FileInputStream;->close()V
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    .line 332
    .end local v10    # "fs":Ljava/io/FileInputStream;
    .end local v13    # "sourceFile":Ljava/io/File;
    :cond_0
    :goto_0
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    const/4 v1, -0x1

    if-ne v0, v1, :cond_1

    .line 334
    invoke-virtual {p0}, Lcom/EasyMovieTexture/EasyMovieTexture;->InitExtTexture()I

    move-result v0

    iput v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    .line 338
    :cond_1
    new-instance v0, Landroid/graphics/SurfaceTexture;

    iget v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    invoke-direct {v0, v1}, Landroid/graphics/SurfaceTexture;-><init>(I)V

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    .line 339
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    invoke-virtual {v0, p0}, Landroid/graphics/SurfaceTexture;->setOnFrameAvailableListener(Landroid/graphics/SurfaceTexture$OnFrameAvailableListener;)V

    .line 340
    new-instance v0, Landroid/view/Surface;

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    invoke-direct {v0, v1}, Landroid/view/Surface;-><init>(Landroid/graphics/SurfaceTexture;)V

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_Surface:Landroid/view/Surface;

    .line 342
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_Surface:Landroid/view/Surface;

    invoke-virtual {v0, v1}, Landroid/media/MediaPlayer;->setSurface(Landroid/view/Surface;)V

    .line 343
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p0}, Landroid/media/MediaPlayer;->setOnPreparedListener(Landroid/media/MediaPlayer$OnPreparedListener;)V

    .line 344
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p0}, Landroid/media/MediaPlayer;->setOnCompletionListener(Landroid/media/MediaPlayer$OnCompletionListener;)V

    .line 345
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p0}, Landroid/media/MediaPlayer;->setOnErrorListener(Landroid/media/MediaPlayer$OnErrorListener;)V

    .line 348
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->prepareAsync()V

    .line 353
    const/4 v0, 0x1

    :goto_1
    return v0

    .line 219
    :catch_0
    move-exception v8

    .line 220
    .local v8, "e":Ljava/io/IOException;
    const-string v0, "Unity"

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Error m_MediaPlayer.setDataSource() : "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 221
    invoke-virtual {v8}, Ljava/io/IOException;->printStackTrace()V

    .line 223
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->ERROR:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    goto :goto_0

    .line 229
    .end local v8    # "e":Ljava/io/IOException;
    :cond_2
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    const-string v1, "://"

    invoke-virtual {v0, v1}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-eqz v0, :cond_3

    .line 232
    :try_start_1
    new-instance v11, Ljava/util/HashMap;

    invoke-direct {v11}, Ljava/util/HashMap;-><init>()V

    .line 233
    .local v11, "headers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    const-string v0, "rtsp_transport"

    const-string v1, "tcp"

    invoke-interface {v11, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 234
    const-string v0, "max_analyze_duration"

    const-string v1, "500"

    invoke-interface {v11, v0, v1}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    .line 263
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_UnityActivity:Landroid/app/Activity;

    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    invoke-static {v2}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object v2

    invoke-virtual {v0, v1, v2, v11}, Landroid/media/MediaPlayer;->setDataSource(Landroid/content/Context;Landroid/net/Uri;Ljava/util/Map;)V
    :try_end_1
    .catch Ljava/io/IOException; {:try_start_1 .. :try_end_1} :catch_1

    goto/16 :goto_0

    .line 271
    .end local v11    # "headers":Ljava/util/Map;, "Ljava/util/Map<Ljava/lang/String;Ljava/lang/String;>;"
    :catch_1
    move-exception v8

    .line 273
    .restart local v8    # "e":Ljava/io/IOException;
    const-string v0, "Unity"

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Error m_MediaPlayer.setDataSource() : "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 274
    invoke-virtual {v8}, Ljava/io/IOException;->printStackTrace()V

    .line 276
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->ERROR:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    move v0, v14

    .line 278
    goto :goto_1

    .line 284
    .end local v8    # "e":Ljava/io/IOException;
    :cond_3
    iget-boolean v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bSplitOBB:Z

    if-eqz v0, :cond_5

    .line 290
    :try_start_2
    new-instance v9, Lcom/android/vending/expansion/zipfile/ZipResourceFile;

    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strOBBName:Ljava/lang/String;

    invoke-direct {v9, v0}, Lcom/android/vending/expansion/zipfile/ZipResourceFile;-><init>(Ljava/lang/String;)V

    .line 292
    .local v9, "expansionFile":Lcom/android/vending/expansion/zipfile/ZipResourceFile;
    const-string v0, "unity"

    new-instance v1, Ljava/lang/StringBuilder;

    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strOBBName:Ljava/lang/String;

    invoke-static {v2}, Ljava/lang/String;->valueOf(Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    const-string v2, " "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 293
    new-instance v0, Ljava/lang/StringBuilder;

    const-string v1, "assets/"

    invoke-direct {v0, v1}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v0

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v9, v0}, Lcom/android/vending/expansion/zipfile/ZipResourceFile;->getAssetFileDescriptor(Ljava/lang/String;)Landroid/content/res/AssetFileDescriptor;

    move-result-object v6

    .line 295
    .local v6, "afd":Landroid/content/res/AssetFileDescriptor;
    invoke-virtual {v9}, Lcom/android/vending/expansion/zipfile/ZipResourceFile;->getAllEntries()[Lcom/android/vending/expansion/zipfile/ZipResourceFile$ZipEntryRO;

    move-result-object v7

    .line 297
    .local v7, "data":[Lcom/android/vending/expansion/zipfile/ZipResourceFile$ZipEntryRO;
    const/4 v12, 0x0

    .local v12, "i":I
    :goto_2
    array-length v0, v7

    if-lt v12, v0, :cond_4

    .line 302
    const-string v0, "unity"

    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v1, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    move-result-object v1

    const-string v2, " "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 303
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v6}, Landroid/content/res/AssetFileDescriptor;->getFileDescriptor()Ljava/io/FileDescriptor;

    move-result-object v1

    invoke-virtual {v6}, Landroid/content/res/AssetFileDescriptor;->getStartOffset()J

    move-result-wide v2

    invoke-virtual {v6}, Landroid/content/res/AssetFileDescriptor;->getLength()J

    move-result-wide v4

    invoke-virtual/range {v0 .. v5}, Landroid/media/MediaPlayer;->setDataSource(Ljava/io/FileDescriptor;JJ)V
    :try_end_2
    .catch Ljava/io/IOException; {:try_start_2 .. :try_end_2} :catch_2

    goto/16 :goto_0

    .line 306
    .end local v6    # "afd":Landroid/content/res/AssetFileDescriptor;
    .end local v7    # "data":[Lcom/android/vending/expansion/zipfile/ZipResourceFile$ZipEntryRO;
    .end local v9    # "expansionFile":Lcom/android/vending/expansion/zipfile/ZipResourceFile;
    .end local v12    # "i":I
    :catch_2
    move-exception v8

    .line 307
    .restart local v8    # "e":Ljava/io/IOException;
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->ERROR:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 308
    invoke-virtual {v8}, Ljava/io/IOException;->printStackTrace()V

    move v0, v14

    .line 309
    goto/16 :goto_1

    .line 299
    .end local v8    # "e":Ljava/io/IOException;
    .restart local v6    # "afd":Landroid/content/res/AssetFileDescriptor;
    .restart local v7    # "data":[Lcom/android/vending/expansion/zipfile/ZipResourceFile$ZipEntryRO;
    .restart local v9    # "expansionFile":Lcom/android/vending/expansion/zipfile/ZipResourceFile;
    .restart local v12    # "i":I
    :cond_4
    :try_start_3
    const-string v0, "unity"

    aget-object v1, v7, v12

    iget-object v1, v1, Lcom/android/vending/expansion/zipfile/ZipResourceFile$ZipEntryRO;->mFileName:Ljava/lang/String;

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I
    :try_end_3
    .catch Ljava/io/IOException; {:try_start_3 .. :try_end_3} :catch_2

    .line 297
    add-int/lit8 v12, v12, 0x1

    goto :goto_2

    .line 316
    .end local v6    # "afd":Landroid/content/res/AssetFileDescriptor;
    .end local v7    # "data":[Lcom/android/vending/expansion/zipfile/ZipResourceFile$ZipEntryRO;
    .end local v9    # "expansionFile":Lcom/android/vending/expansion/zipfile/ZipResourceFile;
    .end local v12    # "i":I
    :cond_5
    :try_start_4
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_UnityActivity:Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getAssets()Landroid/content/res/AssetManager;

    move-result-object v0

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    invoke-virtual {v0, v1}, Landroid/content/res/AssetManager;->openFd(Ljava/lang/String;)Landroid/content/res/AssetFileDescriptor;

    move-result-object v6

    .line 317
    .restart local v6    # "afd":Landroid/content/res/AssetFileDescriptor;
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v6}, Landroid/content/res/AssetFileDescriptor;->getFileDescriptor()Ljava/io/FileDescriptor;

    move-result-object v1

    invoke-virtual {v6}, Landroid/content/res/AssetFileDescriptor;->getStartOffset()J

    move-result-wide v2

    invoke-virtual {v6}, Landroid/content/res/AssetFileDescriptor;->getLength()J

    move-result-wide v4

    invoke-virtual/range {v0 .. v5}, Landroid/media/MediaPlayer;->setDataSource(Ljava/io/FileDescriptor;JJ)V

    .line 318
    invoke-virtual {v6}, Landroid/content/res/AssetFileDescriptor;->close()V
    :try_end_4
    .catch Ljava/io/IOException; {:try_start_4 .. :try_end_4} :catch_3

    goto/16 :goto_0

    .line 319
    .end local v6    # "afd":Landroid/content/res/AssetFileDescriptor;
    :catch_3
    move-exception v8

    .line 321
    .restart local v8    # "e":Ljava/io/IOException;
    const-string v0, "Unity"

    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Error m_MediaPlayer.setDataSource() : "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;)I

    .line 322
    invoke-virtual {v8}, Ljava/io/IOException;->printStackTrace()V

    .line 323
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->ERROR:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    move v0, v14

    .line 324
    goto/16 :goto_1
.end method

.method public NDK_SetFileName(Ljava/lang/String;)V
    .locals 0
    .param p1, "strFileName"    # Ljava/lang/String;

    .prologue
    .line 695
    iput-object p1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strFileName:Ljava/lang/String;

    .line 696
    return-void
.end method

.method public Pause()V
    .locals 2

    .prologue
    .line 552
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 554
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v0, v1, :cond_0

    .line 556
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->pause()V

    .line 557
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PAUSED:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 560
    :cond_0
    return-void
.end method

.method public Play(I)V
    .locals 2
    .param p1, "iSeek"    # I

    .prologue
    .line 495
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_1

    .line 497
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v0, v1, :cond_0

    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PAUSED:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v0, v1, :cond_0

    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->END:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v0, v1, :cond_1

    .line 501
    :cond_0
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->start()V

    .line 503
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 507
    :cond_1
    return-void
.end method

.method public native QuitApplication()V
.end method

.method public RePlay()V
    .locals 2

    .prologue
    .line 539
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 541
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PAUSED:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v0, v1, :cond_0

    .line 543
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->start()V

    .line 544
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 548
    :cond_0
    return-void
.end method

.method public native RenderScene([FII)V
.end method

.method public Reset()V
    .locals 2

    .prologue
    .line 511
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 513
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v0, v1, :cond_0

    .line 515
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->reset()V

    .line 520
    :cond_0
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->NOT_READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 521
    return-void
.end method

.method public SelectTrack(I)V
    .locals 1
    .param p1, "iIndex"    # I

    .prologue
    .line 626
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 628
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p1}, Landroid/media/MediaPlayer;->selectTrack(I)V

    .line 632
    :cond_0
    return-void
.end method

.method public native SetAssetManager(Landroid/content/res/AssetManager;)V
.end method

.method public SetLooping(Z)V
    .locals 1
    .param p1, "bLoop"    # Z

    .prologue
    .line 428
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 429
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p1}, Landroid/media/MediaPlayer;->setLooping(Z)V

    .line 430
    :cond_0
    return-void
.end method

.method public native SetManagerID(I)V
.end method

.method public SetNotReady()V
    .locals 1

    .prologue
    .line 715
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->NOT_READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 716
    return-void
.end method

.method public SetRockchip(Z)V
    .locals 0
    .param p1, "bValue"    # Z

    .prologue
    .line 422
    iput-boolean p1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bRockchip:Z

    .line 423
    return-void
.end method

.method public SetSeekPosition(I)V
    .locals 2
    .param p1, "iSeek"    # I

    .prologue
    .line 457
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_1

    .line 459
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v0, v1, :cond_0

    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v0, v1, :cond_0

    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PAUSED:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v0, v1, :cond_1

    .line 461
    :cond_0
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p1}, Landroid/media/MediaPlayer;->seekTo(I)V

    .line 464
    :cond_1
    return-void
.end method

.method public SetSpeed(F)V
    .locals 2
    .param p1, "fSpeed"    # F
    .annotation build Landroid/annotation/TargetApi;
        value = 0x17
    .end annotation

    .prologue
    .line 369
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v1}, Landroid/media/MediaPlayer;->getPlaybackParams()Landroid/media/PlaybackParams;

    move-result-object v1

    invoke-virtual {v1, p1}, Landroid/media/PlaybackParams;->setSpeed(F)Landroid/media/PlaybackParams;

    move-result-object v1

    invoke-virtual {v0, v1}, Landroid/media/MediaPlayer;->setPlaybackParams(Landroid/media/PlaybackParams;)V

    .line 370
    return-void
.end method

.method public SetSplitOBB(ZLjava/lang/String;)V
    .locals 0
    .param p1, "bValue"    # Z
    .param p2, "strOBBName"    # Ljava/lang/String;

    .prologue
    .line 610
    iput-boolean p1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bSplitOBB:Z

    .line 611
    iput-object p2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_strOBBName:Ljava/lang/String;

    .line 612
    return-void
.end method

.method public SetUnityActivity(Landroid/app/Activity;)V
    .locals 1
    .param p1, "unityActivity"    # Landroid/app/Activity;

    .prologue
    .line 687
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {p0, v0}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetManagerID(I)V

    .line 688
    iput-object p1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_UnityActivity:Landroid/app/Activity;

    .line 689
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_UnityActivity:Landroid/app/Activity;

    invoke-virtual {v0}, Landroid/app/Activity;->getAssets()Landroid/content/res/AssetManager;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetAssetManager(Landroid/content/res/AssetManager;)V

    .line 690
    return-void
.end method

.method public SetUnityTexture(I)V
    .locals 1
    .param p1, "iTextureID"    # I

    .prologue
    .line 597
    iput p1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iUnityTextureID:I

    .line 598
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {p0, v0}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetManagerID(I)V

    .line 599
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iUnityTextureID:I

    invoke-virtual {p0, v0}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetUnityTextureID(I)V

    .line 601
    return-void
.end method

.method public native SetUnityTextureID(I)V
.end method

.method public SetUnityTextureID(Ljava/lang/Object;)V
    .locals 0
    .param p1, "texturePtr"    # Ljava/lang/Object;

    .prologue
    .line 605
    return-void
.end method

.method public SetVolume(F)V
    .locals 1
    .param p1, "fVolume"    # F

    .prologue
    .line 435
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 437
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p1, p1}, Landroid/media/MediaPlayer;->setVolume(FF)V

    .line 441
    :cond_0
    return-void
.end method

.method public SetVolume2(FF)V
    .locals 1
    .param p1, "fVolume1"    # F
    .param p2, "fVolume2"    # F

    .prologue
    .line 446
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 448
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p1, p2}, Landroid/media/MediaPlayer;->setVolume(FF)V

    .line 452
    :cond_0
    return-void
.end method

.method public SetWindowSize()V
    .locals 4

    .prologue
    .line 721
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {p0, v0}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetManagerID(I)V

    .line 722
    invoke-virtual {p0}, Lcom/EasyMovieTexture/EasyMovieTexture;->GetVideoWidth()I

    move-result v0

    invoke-virtual {p0}, Lcom/EasyMovieTexture/EasyMovieTexture;->GetVideoHeight()I

    move-result v1

    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iUnityTextureID:I

    iget-boolean v3, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bRockchip:Z

    invoke-virtual {p0, v0, v1, v2, v3}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetWindowSize(IIIZ)V

    .line 725
    return-void
.end method

.method public native SetWindowSize(IIIZ)V
.end method

.method public Stop()V
    .locals 2

    .prologue
    .line 525
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v0, :cond_0

    .line 527
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v0, v1, :cond_0

    .line 529
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0}, Landroid/media/MediaPlayer;->stop()V

    .line 534
    :cond_0
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->NOT_READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 535
    return-void
.end method

.method public UnLoad()V
    .locals 8

    .prologue
    const/4 v7, 0x1

    const/4 v6, 0x0

    const/4 v5, -0x1

    const/4 v4, 0x0

    .line 133
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v2, :cond_2

    .line 135
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v3, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->NOT_READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v2, v3, :cond_3

    .line 138
    :try_start_0
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v2}, Landroid/media/MediaPlayer;->stop()V

    .line 139
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v2}, Landroid/media/MediaPlayer;->release()V
    :try_end_0
    .catch Ljava/lang/SecurityException; {:try_start_0 .. :try_end_0} :catch_0
    .catch Ljava/lang/IllegalStateException; {:try_start_0 .. :try_end_0} :catch_1

    .line 149
    :goto_0
    iput-object v4, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    .line 168
    :goto_1
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_Surface:Landroid/view/Surface;

    if-eqz v2, :cond_0

    .line 170
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_Surface:Landroid/view/Surface;

    invoke-virtual {v2}, Landroid/view/Surface;->release()V

    .line 171
    iput-object v4, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_Surface:Landroid/view/Surface;

    .line 174
    :cond_0
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    if-eqz v2, :cond_1

    .line 176
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    invoke-virtual {v2}, Landroid/graphics/SurfaceTexture;->release()V

    .line 177
    iput-object v4, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    .line 180
    :cond_1
    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    if-eq v2, v5, :cond_2

    .line 182
    new-array v1, v7, [I

    .line 183
    .local v1, "textures":[I
    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    aput v2, v1, v6

    .line 184
    invoke-static {v7, v1, v6}, Landroid/opengl/GLES20;->glDeleteTextures(I[II)V

    .line 185
    iput v5, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    .line 189
    .end local v1    # "textures":[I
    :cond_2
    sget-object v2, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->NOT_READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 190
    return-void

    .line 142
    :catch_0
    move-exception v0

    .line 144
    .local v0, "e":Ljava/lang/SecurityException;
    invoke-virtual {v0}, Ljava/lang/SecurityException;->printStackTrace()V

    goto :goto_0

    .line 145
    .end local v0    # "e":Ljava/lang/SecurityException;
    :catch_1
    move-exception v0

    .line 147
    .local v0, "e":Ljava/lang/IllegalStateException;
    invoke-virtual {v0}, Ljava/lang/IllegalStateException;->printStackTrace()V

    goto :goto_0

    .line 155
    .end local v0    # "e":Ljava/lang/IllegalStateException;
    :cond_3
    :try_start_1
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v2}, Landroid/media/MediaPlayer;->release()V
    :try_end_1
    .catch Ljava/lang/SecurityException; {:try_start_1 .. :try_end_1} :catch_2
    .catch Ljava/lang/IllegalStateException; {:try_start_1 .. :try_end_1} :catch_3

    .line 165
    :goto_2
    iput-object v4, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    goto :goto_1

    .line 158
    :catch_2
    move-exception v0

    .line 160
    .local v0, "e":Ljava/lang/SecurityException;
    invoke-virtual {v0}, Ljava/lang/SecurityException;->printStackTrace()V

    goto :goto_2

    .line 161
    .end local v0    # "e":Ljava/lang/SecurityException;
    :catch_3
    move-exception v0

    .line 163
    .local v0, "e":Ljava/lang/IllegalStateException;
    invoke-virtual {v0}, Ljava/lang/IllegalStateException;->printStackTrace()V

    goto :goto_2
.end method

.method public UpdateVideoTexture()V
    .locals 6

    .prologue
    const/4 v5, 0x0

    const/16 v4, 0xb71

    .line 376
    iget-boolean v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bUpdate:Z

    if-nez v2, :cond_1

    .line 417
    :cond_0
    :goto_0
    return-void

    .line 379
    :cond_1
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-eqz v2, :cond_0

    .line 381
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v3, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PLAYING:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-eq v2, v3, :cond_2

    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    sget-object v3, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->PAUSED:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    if-ne v2, v3, :cond_0

    .line 384
    :cond_2
    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {p0, v2}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetManagerID(I)V

    .line 387
    const/4 v2, 0x1

    new-array v0, v2, [Z

    .line 388
    .local v0, "abValue":[Z
    invoke-static {v4, v0, v5}, Landroid/opengl/GLES20;->glGetBooleanv(I[ZI)V

    .line 389
    invoke-static {v4}, Landroid/opengl/GLES20;->glDisable(I)V

    .line 390
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    invoke-virtual {v2}, Landroid/graphics/SurfaceTexture;->updateTexImage()V

    .line 396
    const/16 v2, 0x10

    new-array v1, v2, [F

    .line 399
    .local v1, "mMat":[F
    iget-object v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_SurfaceTexture:Landroid/graphics/SurfaceTexture;

    invoke-virtual {v2, v1}, Landroid/graphics/SurfaceTexture;->getTransformMatrix([F)V

    .line 401
    iget v2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iSurfaceTextureID:I

    iget v3, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iUnityTextureID:I

    invoke-virtual {p0, v1, v2, v3}, Lcom/EasyMovieTexture/EasyMovieTexture;->RenderScene([FII)V

    .line 404
    aget-boolean v2, v0, v5

    if-eqz v2, :cond_0

    .line 406
    invoke-static {v4}, Landroid/opengl/GLES20;->glEnable(I)V

    goto :goto_0
.end method

.method public onBufferingUpdate(Landroid/media/MediaPlayer;I)V
    .locals 1
    .param p1, "arg0"    # Landroid/media/MediaPlayer;
    .param p2, "arg1"    # I

    .prologue
    .line 793
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-ne p1, v0, :cond_0

    .line 794
    iput p2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentSeekPercent:I

    .line 799
    :cond_0
    return-void
.end method

.method public onCompletion(Landroid/media/MediaPlayer;)V
    .locals 1
    .param p1, "arg0"    # Landroid/media/MediaPlayer;

    .prologue
    .line 782
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-ne p1, v0, :cond_0

    .line 783
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->END:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 785
    :cond_0
    return-void
.end method

.method public onError(Landroid/media/MediaPlayer;II)Z
    .locals 3
    .param p1, "arg0"    # Landroid/media/MediaPlayer;
    .param p2, "arg1"    # I
    .param p3, "arg2"    # I

    .prologue
    .line 742
    iget-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-ne p1, v1, :cond_0

    .line 746
    sparse-switch p2, :sswitch_data_0

    .line 758
    new-instance v1, Ljava/lang/StringBuilder;

    const-string v2, "Unknown error "

    invoke-direct {v1, v2}, Ljava/lang/StringBuilder;-><init>(Ljava/lang/String;)V

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    move-result-object v1

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    .line 761
    .local v0, "strError":Ljava/lang/String;
    :goto_0
    iput p2, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iErrorCode:I

    .line 762
    iput p3, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iErrorCodeExtra:I

    .line 768
    sget-object v1, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->ERROR:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v1, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 770
    const/4 v1, 0x1

    .line 773
    .end local v0    # "strError":Ljava/lang/String;
    :goto_1
    return v1

    .line 749
    :sswitch_0
    const-string v0, "MEDIA_ERROR_NOT_VALID_FOR_PROGRESSIVE_PLAYBACK"

    .line 750
    .restart local v0    # "strError":Ljava/lang/String;
    goto :goto_0

    .line 752
    .end local v0    # "strError":Ljava/lang/String;
    :sswitch_1
    const-string v0, "MEDIA_ERROR_SERVER_DIED"

    .line 753
    .restart local v0    # "strError":Ljava/lang/String;
    goto :goto_0

    .line 755
    .end local v0    # "strError":Ljava/lang/String;
    :sswitch_2
    const-string v0, "MEDIA_ERROR_UNKNOWN"

    .line 756
    .restart local v0    # "strError":Ljava/lang/String;
    goto :goto_0

    .line 773
    .end local v0    # "strError":Ljava/lang/String;
    :cond_0
    const/4 v1, 0x0

    goto :goto_1

    .line 746
    nop

    :sswitch_data_0
    .sparse-switch
        0x1 -> :sswitch_2
        0x64 -> :sswitch_1
        0xc8 -> :sswitch_0
    .end sparse-switch
.end method

.method public declared-synchronized onFrameAvailable(Landroid/graphics/SurfaceTexture;)V
    .locals 1
    .param p1, "surface"    # Landroid/graphics/SurfaceTexture;

    .prologue
    .line 361
    monitor-enter p0

    const/4 v0, 0x1

    :try_start_0
    iput-boolean v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_bUpdate:Z
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    .line 362
    monitor-exit p0

    return-void

    .line 361
    :catchall_0
    move-exception v0

    monitor-exit p0

    throw v0
.end method

.method public onPrepared(Landroid/media/MediaPlayer;)V
    .locals 1
    .param p1, "arg0"    # Landroid/media/MediaPlayer;

    .prologue
    .line 804
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    if-ne p1, v0, :cond_0

    .line 806
    sget-object v0, Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;->READY:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    iput-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentState:Lcom/EasyMovieTexture/EasyMovieTexture$MEDIAPLAYER_STATE;

    .line 808
    iget v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iNativeMgrID:I

    invoke-virtual {p0, v0}, Lcom/EasyMovieTexture/EasyMovieTexture;->SetManagerID(I)V

    .line 809
    const/4 v0, 0x0

    iput v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_iCurrentSeekPercent:I

    .line 810
    iget-object v0, p0, Lcom/EasyMovieTexture/EasyMovieTexture;->m_MediaPlayer:Landroid/media/MediaPlayer;

    invoke-virtual {v0, p0}, Landroid/media/MediaPlayer;->setOnBufferingUpdateListener(Landroid/media/MediaPlayer$OnBufferingUpdateListener;)V

    .line 814
    :cond_0
    return-void
.end method
