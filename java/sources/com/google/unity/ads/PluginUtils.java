package com.google.unity.ads;

import android.content.res.Resources;
import android.util.DisplayMetrics;
import android.util.Log;
import android.widget.PopupWindow;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

/* JADX INFO: loaded from: classes.dex */
public class PluginUtils {
    public static final String LOGTAG = "AdsUnity";
    private static final int POSITION_BOTTOM = 1;
    private static final int POSITION_BOTTOM_LEFT = 4;
    private static final int POSITION_BOTTOM_RIGHT = 5;
    private static final int POSITION_CENTER = 6;
    public static final int POSITION_CUSTOM = -1;
    private static final int POSITION_TOP = 0;
    private static final int POSITION_TOP_LEFT = 2;
    private static final int POSITION_TOP_RIGHT = 3;

    public static String getErrorReason(int errorCode) {
        switch (errorCode) {
            case 0:
                return "Internal error";
            case 1:
                return "Invalid request";
            case 2:
                return "Network Error";
            case 3:
                return "No fill";
            default:
                Log.w(LOGTAG, String.format("Unexpected error code: %s", Integer.valueOf(errorCode)));
                return "";
        }
    }

    public static int getLayoutGravityForPositionCode(int positionCode) {
        switch (positionCode) {
            case 0:
                return 49;
            case 1:
                return 81;
            case 2:
                return 51;
            case 3:
                return 53;
            case 4:
                return 83;
            case 5:
                return 85;
            case 6:
                return 17;
            default:
                throw new IllegalArgumentException("Attempted to position ad with invalid ad position.");
        }
    }

    public static float convertPixelsToDp(float px) {
        DisplayMetrics metrics = Resources.getSystem().getDisplayMetrics();
        float dp = px / metrics.density;
        return dp;
    }

    public static float convertDpToPixel(float dp) {
        DisplayMetrics metrics = Resources.getSystem().getDisplayMetrics();
        float px = dp * metrics.density;
        return px;
    }

    public static void setPopUpWindowLayoutType(PopupWindow popupWindow, int layoutType) {
        try {
            Method method = PopupWindow.class.getDeclaredMethod("setWindowLayoutType", Integer.TYPE);
            method.setAccessible(true);
            method.invoke(popupWindow, Integer.valueOf(layoutType));
        } catch (IllegalAccessException exception) {
            Log.w(LOGTAG, String.format("Unable to set popUpWindow window layout type: %s", exception.getLocalizedMessage()));
        } catch (NoSuchMethodException exception2) {
            Log.w(LOGTAG, String.format("Unable to set popUpWindow window layout type: %s", exception2.getLocalizedMessage()));
        } catch (InvocationTargetException exception3) {
            Log.d(LOGTAG, String.format("Unable to set popUpWindow window layout type: %s", exception3.getLocalizedMessage()));
        }
    }
}
