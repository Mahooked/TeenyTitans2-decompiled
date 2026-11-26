package com.kayac.unitywebview;

import com.unity3d.player.UnityPlayer;

/* JADX INFO: compiled from: WebViewPlugin.java */
/* JADX INFO: loaded from: classes.dex */
class WebViewPluginInterface {
    private String mGameObject;

    public WebViewPluginInterface(String gameObject) {
        this.mGameObject = gameObject;
    }

    public void call(String message) {
        UnityPlayer.UnitySendMessage(this.mGameObject, "CallFromJS", message);
    }
}
