package com.google.unity.ads;

import android.app.Activity;
import android.support.v4.view.PointerIconCompat;
import android.util.Log;
import android.view.ViewGroup;
import android.view.ViewParent;
import android.widget.PopupWindow;
import com.google.android.gms.ads.AdListener;
import com.google.android.gms.ads.AdRequest;
import com.google.android.gms.ads.AdSize;
import com.google.android.gms.ads.NativeExpressAdView;

/* JADX INFO: loaded from: classes.dex */
public class NativeExpressAd {
    private NativeExpressAdView mAdView;
    private boolean mHidden;
    private int mHorizontalOffset;
    private PopupWindow mPopupWindow;
    private int mPositionCode;
    private UnityAdListener mUnityListener;
    private Activity mUnityPlayerActivity;
    private int mVerticalOffset;

    public NativeExpressAd(Activity activity, UnityAdListener listener) {
        this.mUnityPlayerActivity = activity;
        this.mUnityListener = listener;
    }

    public void create(final String publisherId, final AdSize adSize, final int positionCode) {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.NativeExpressAd.1
            @Override // java.lang.Runnable
            public void run() {
                NativeExpressAd.this.createNativeExpressAdView(publisherId, adSize);
                NativeExpressAd.this.createPopupWindow();
                NativeExpressAd.this.mPositionCode = positionCode;
                NativeExpressAd.this.mHorizontalOffset = 0;
                NativeExpressAd.this.mVerticalOffset = 0;
                NativeExpressAd.this.mHidden = false;
            }
        });
    }

    public void create(final String publisherId, final AdSize adSize, final int positionX, final int positionY) {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.NativeExpressAd.2
            @Override // java.lang.Runnable
            public void run() {
                NativeExpressAd.this.createNativeExpressAdView(publisherId, adSize);
                NativeExpressAd.this.createPopupWindow();
                NativeExpressAd.this.mPositionCode = -1;
                NativeExpressAd.this.mHorizontalOffset = positionX;
                NativeExpressAd.this.mVerticalOffset = positionY;
                NativeExpressAd.this.mHidden = false;
            }
        });
    }

    public void createNativeExpressAdView(String publisherId, AdSize adSize) {
        this.mAdView = new NativeExpressAdView(this.mUnityPlayerActivity);
        this.mAdView.setBackgroundColor(0);
        this.mAdView.setAdUnitId(publisherId);
        this.mAdView.setAdSize(adSize);
        this.mAdView.setAdListener(new AdListener() { // from class: com.google.unity.ads.NativeExpressAd.3
            @Override // com.google.android.gms.ads.AdListener
            public void onAdLoaded() {
                if (NativeExpressAd.this.mUnityListener != null) {
                    if (!NativeExpressAd.this.mPopupWindow.isShowing() && !NativeExpressAd.this.mHidden) {
                        NativeExpressAd.this.showPopUpWindow();
                    }
                    NativeExpressAd.this.mUnityListener.onAdLoaded();
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdFailedToLoad(int errorCode) {
                if (NativeExpressAd.this.mUnityListener != null) {
                    NativeExpressAd.this.mUnityListener.onAdFailedToLoad(PluginUtils.getErrorReason(errorCode));
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdOpened() {
                if (NativeExpressAd.this.mUnityListener != null) {
                    NativeExpressAd.this.mUnityListener.onAdOpened();
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdClosed() {
                if (NativeExpressAd.this.mUnityListener != null) {
                    NativeExpressAd.this.mUnityListener.onAdClosed();
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdLeftApplication() {
                if (NativeExpressAd.this.mUnityListener != null) {
                    NativeExpressAd.this.mUnityListener.onAdLeftApplication();
                }
            }
        });
    }

    public void createPopupWindow() {
        int popUpWindowWidth = this.mAdView.getAdSize().getWidthInPixels(this.mUnityPlayerActivity);
        int popUpWindowHeight = this.mAdView.getAdSize().getHeightInPixels(this.mUnityPlayerActivity);
        this.mPopupWindow = new PopupWindow(this.mAdView, popUpWindowWidth, popUpWindowHeight);
        int visibilityFlags = this.mUnityPlayerActivity.getWindow().getAttributes().flags;
        this.mPopupWindow.getContentView().setSystemUiVisibility(visibilityFlags);
        PluginUtils.setPopUpWindowLayoutType(this.mPopupWindow, PointerIconCompat.TYPE_HAND);
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void showPopUpWindow() {
        if (this.mPositionCode == -1) {
            this.mPopupWindow.showAtLocation(this.mUnityPlayerActivity.getWindow().getDecorView().getRootView(), 0, (int) PluginUtils.convertDpToPixel(this.mHorizontalOffset), (int) PluginUtils.convertDpToPixel(this.mVerticalOffset));
        } else {
            this.mPopupWindow.showAtLocation(this.mUnityPlayerActivity.getWindow().getDecorView().getRootView(), PluginUtils.getLayoutGravityForPositionCode(this.mPositionCode), 0, 0);
        }
    }

    public void loadAd(final AdRequest request) {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.NativeExpressAd.4
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling loadAd() on NativeExpressAdView");
                NativeExpressAd.this.mAdView.loadAd(request);
            }
        });
    }

    public void setAdSize(final AdSize adSize) {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.NativeExpressAd.5
            @Override // java.lang.Runnable
            public void run() {
                NativeExpressAd.this.mAdView.setAdSize(adSize);
            }
        });
    }

    public void show() {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.NativeExpressAd.6
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling show() on NativeExpressAdView");
                NativeExpressAd.this.mHidden = false;
                NativeExpressAd.this.mAdView.setVisibility(0);
                if (!NativeExpressAd.this.mPopupWindow.isShowing()) {
                    NativeExpressAd.this.showPopUpWindow();
                }
                NativeExpressAd.this.mAdView.resume();
            }
        });
    }

    public void hide() {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.NativeExpressAd.7
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling hide() on NativeExpressAdView");
                NativeExpressAd.this.mHidden = true;
                NativeExpressAd.this.mAdView.setVisibility(8);
                NativeExpressAd.this.mPopupWindow.dismiss();
                NativeExpressAd.this.mAdView.pause();
            }
        });
    }

    public void destroy() {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.NativeExpressAd.8
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling destroy() on NativeExpressAdView");
                NativeExpressAd.this.mAdView.destroy();
                NativeExpressAd.this.mPopupWindow.dismiss();
                ViewParent parentView = NativeExpressAd.this.mAdView.getParent();
                if (parentView != null && (parentView instanceof ViewGroup)) {
                    ((ViewGroup) parentView).removeView(NativeExpressAd.this.mAdView);
                }
            }
        });
    }
}
