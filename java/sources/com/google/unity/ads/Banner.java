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
import com.google.android.gms.ads.AdView;

/* JADX INFO: loaded from: classes.dex */
public class Banner {
    private AdView mAdView;
    private boolean mHidden;
    private int mHorizontalOffset;
    private PopupWindow mPopupWindow;
    private int mPositionCode;
    private UnityAdListener mUnityListener;
    private Activity mUnityPlayerActivity;
    private int mVerticalOffset;

    public Banner(Activity activity, UnityAdListener listener) {
        this.mUnityPlayerActivity = activity;
        this.mUnityListener = listener;
    }

    public void create(final String publisherId, final AdSize adSize, final int positionCode) {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.Banner.1
            @Override // java.lang.Runnable
            public void run() {
                Banner.this.createAdView(publisherId, adSize);
                Banner.this.createPopupWindow();
                Banner.this.mHorizontalOffset = 0;
                Banner.this.mVerticalOffset = 0;
                Banner.this.mPositionCode = positionCode;
                Banner.this.mHidden = false;
            }
        });
    }

    public void create(final String publisherId, final AdSize adSize, final int positionX, final int positionY) {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.Banner.2
            @Override // java.lang.Runnable
            public void run() {
                Banner.this.createAdView(publisherId, adSize);
                Banner.this.createPopupWindow();
                Banner.this.mPositionCode = -1;
                Banner.this.mHorizontalOffset = positionX;
                Banner.this.mVerticalOffset = positionY;
                Banner.this.mHidden = false;
            }
        });
    }

    public void createAdView(String publisherId, AdSize adSize) {
        this.mAdView = new AdView(this.mUnityPlayerActivity);
        this.mAdView.setBackgroundColor(0);
        this.mAdView.setAdUnitId(publisherId);
        this.mAdView.setAdSize(adSize);
        this.mAdView.setAdListener(new AdListener() { // from class: com.google.unity.ads.Banner.3
            @Override // com.google.android.gms.ads.AdListener
            public void onAdLoaded() {
                if (Banner.this.mUnityListener != null) {
                    if (!Banner.this.mPopupWindow.isShowing() && !Banner.this.mHidden) {
                        Banner.this.showPopUpWindow();
                    }
                    Banner.this.mUnityListener.onAdLoaded();
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdFailedToLoad(int errorCode) {
                if (Banner.this.mUnityListener != null) {
                    Banner.this.mUnityListener.onAdFailedToLoad(PluginUtils.getErrorReason(errorCode));
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdOpened() {
                if (Banner.this.mUnityListener != null) {
                    Banner.this.mUnityListener.onAdOpened();
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdClosed() {
                if (Banner.this.mUnityListener != null) {
                    Banner.this.mUnityListener.onAdClosed();
                }
            }

            @Override // com.google.android.gms.ads.AdListener
            public void onAdLeftApplication() {
                if (Banner.this.mUnityListener != null) {
                    Banner.this.mUnityListener.onAdLeftApplication();
                }
            }
        });
    }

    /* JADX INFO: Access modifiers changed from: private */
    public void createPopupWindow() {
        int popUpWindowWidth = this.mAdView.getAdSize().equals(AdSize.SMART_BANNER) ? -1 : this.mAdView.getAdSize().getWidthInPixels(this.mUnityPlayerActivity);
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
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.Banner.4
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling loadAd() on Android");
                Banner.this.mAdView.loadAd(request);
            }
        });
    }

    public void show() {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.Banner.5
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling show() on Android");
                Banner.this.mHidden = false;
                Banner.this.mAdView.setVisibility(0);
                if (!Banner.this.mPopupWindow.isShowing()) {
                    Banner.this.showPopUpWindow();
                }
                Banner.this.mAdView.resume();
            }
        });
    }

    public void hide() {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.Banner.6
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling hide() on Android");
                Banner.this.mHidden = true;
                Banner.this.mAdView.setVisibility(8);
                Banner.this.mPopupWindow.dismiss();
                Banner.this.mAdView.pause();
            }
        });
    }

    public void destroy() {
        this.mUnityPlayerActivity.runOnUiThread(new Runnable() { // from class: com.google.unity.ads.Banner.7
            @Override // java.lang.Runnable
            public void run() {
                Log.d(PluginUtils.LOGTAG, "Calling destroy() on Android");
                Banner.this.mAdView.destroy();
                Banner.this.mPopupWindow.dismiss();
                ViewParent parentView = Banner.this.mAdView.getParent();
                if (parentView != null && (parentView instanceof ViewGroup)) {
                    ((ViewGroup) parentView).removeView(Banner.this.mAdView);
                }
            }
        });
    }
}
