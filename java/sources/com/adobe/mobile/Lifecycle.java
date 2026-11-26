package com.adobe.mobile;

import android.app.Activity;
import android.content.Intent;
import android.content.SharedPreferences;
import android.net.Uri;
import com.comscore.measurement.MeasurementDispatcher;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Locale;
import java.util.Map;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class Lifecycle {
    protected static final String ADB_LIFECYCLE_PUSH_MESSAGE_ID_KEY = "adb_m_id";
    protected static final String ADB_TRACK_INTERNAL_ADOBE_LINK = "AdobeLink";
    protected static final String ADB_TRACK_INTERNAL_PUSH_CLICK_THROUGH = "PushMessage";
    protected static long sessionStartTime = 0;
    protected static volatile boolean lifecycleHasRun = false;
    private static final HashMap<String, Object> _lifecycleContextData = new HashMap<>();
    private static final HashMap<String, Object> _lifecycleContextDataLowercase = new HashMap<>();
    private static final HashMap<String, Object> _previousSessionlifecycleContextData = new HashMap<>();
    private static final Object _contextDataMutex = new Object();
    private static final Object _lowercaseContextDataMutex = new Object();

    Lifecycle() {
    }

    protected static void start(Activity activity, Map<String, Object> data) {
        if (!lifecycleHasRun) {
            lifecycleHasRun = true;
            try {
                SharedPreferences userDefaults = StaticMethods.getSharedPreferences();
                Activity currentActivity = null;
                try {
                    currentActivity = StaticMethods.getCurrentActivity();
                } catch (StaticMethods.NullActivityException e) {
                }
                if (currentActivity != null && activity != null && currentActivity.getComponentName().toString().equals(activity.getComponentName().toString())) {
                    Messages.checkForInAppMessage(null, null, null);
                }
                StaticMethods.setCurrentActivity(activity);
                MobileConfig mobileConfig = MobileConfig.getInstance();
                long lastPauseDate = userDefaults.getLong("ADMS_PauseDate", 0L);
                int sessionTimeout = mobileConfig.getLifecycleTimeout();
                boolean isNewSession = true;
                if (lastPauseDate > 0) {
                    int pausedTime = (int) ((new Date().getTime() - lastPauseDate) / 1000);
                    long originalStartDate = userDefaults.getLong("ADMS_SessionStart", 0L);
                    sessionStartTime = originalStartDate / 1000;
                    AnalyticsTrackTimedAction.sharedInstance().trackTimedActionUpdateAdjustedStartTime(pausedTime);
                    if (pausedTime < sessionTimeout && originalStartDate > 0) {
                        try {
                            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                            editor.putLong("ADMS_SessionStart", ((long) (pausedTime * 1000)) + originalStartDate);
                            editor.commit();
                        } catch (StaticMethods.NullContextException e2) {
                            StaticMethods.logErrorFormat("Lifecycle - Error while updating start time (%s).", e2.getMessage());
                        }
                        sessionStartTime = userDefaults.getLong("ADMS_SessionStart", 0L) / 1000;
                        isNewSession = false;
                    }
                }
                long date = new Date().getTime();
                if (isNewSession) {
                    VisitorIDService.sharedInstance().idSync(null, null, null, true);
                    mobileConfig.downloadRemoteConfigs();
                    _lifecycleContextData.clear();
                    clearContextDataLowercase();
                    HashMap<String, Object> contextData = data != null ? new HashMap<>(data) : new HashMap<>();
                    Map<String, Object> clickThroughData = checkForAdobeLinkData(activity);
                    if (clickThroughData != null) {
                        contextData.putAll(clickThroughData);
                    }
                    if (!userDefaults.contains("ADMS_InstallDate")) {
                        addInstallData(contextData, date);
                    } else {
                        addNonInstallData(contextData, date);
                        addUpgradeData(contextData, date);
                        addSessionLengthData(contextData);
                        checkReferrerDataForLaunch();
                    }
                    addLifecycleGenericData(contextData, date);
                    generateLifecycleToBeSaved(contextData);
                    persistLifecycleContextData();
                    MobileConfig.getInstance().invokeAdobeDataCallback(Config.MobileDataEvent.MOBILE_EVENT_LIFECYCLE, contextData);
                    if (mobileConfig.mobileUsingAnalytics()) {
                        AnalyticsTrackInternal.trackInternal("Lifecycle", contextData, StaticMethods.getTimeSince1970() - 1);
                    }
                    if (!mobileConfig.getAamAnalyticsForwardingEnabled()) {
                        AudienceManagerWorker.SubmitSignal(_lifecycleContextData, null);
                    }
                }
                checkForAdobeClickThrough(activity, isNewSession);
                resetLifecycleFlags(date);
            } catch (StaticMethods.NullContextException e3) {
                StaticMethods.logErrorFormat("Lifecycle - Error starting lifecycle (%s).", e3.getMessage());
            }
        }
    }

    protected static void stop() {
        lifecycleHasRun = false;
        StaticMethods.updateLastKnownTimestamp(Long.valueOf(StaticMethods.getTimeSince1970()));
        try {
            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
            editor.putBoolean("ADMS_SuccessfulClose", true);
            editor.putLong("ADMS_PauseDate", new Date().getTime());
            editor.commit();
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error updating lifecycle pause data (%s)", e.getMessage());
        }
        try {
            Activity currentActivity = StaticMethods.getCurrentActivity();
            if (currentActivity.isFinishing()) {
                Messages.resetAllInAppMessages();
            }
        } catch (StaticMethods.NullActivityException e2) {
        }
    }

    private static void persistLifecycleContextData() {
        try {
            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
            JSONObject lifecycleJSON = new JSONObject(_lifecycleContextData);
            editor.putString("ADMS_LifecycleData", lifecycleJSON.toString());
            editor.commit();
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logWarningFormat("Lifecycle - Error persisting lifecycle data (%s)", e.getMessage());
        }
    }

    protected static HashMap<String, Object> getContextData() {
        HashMap<String, Object> map;
        synchronized (_contextDataMutex) {
            if (_lifecycleContextData.size() > 0) {
                map = new HashMap<>(_lifecycleContextData);
            } else if (_previousSessionlifecycleContextData.size() > 0) {
                map = new HashMap<>(_previousSessionlifecycleContextData);
            } else {
                addPersistedLifecycleToMap(_previousSessionlifecycleContextData);
                map = new HashMap<>(_previousSessionlifecycleContextData);
            }
        }
        return map;
    }

    protected static void updateContextData(Map<String, Object> data) {
        synchronized (_contextDataMutex) {
            _lifecycleContextData.putAll(data);
        }
        synchronized (_lowercaseContextDataMutex) {
            for (Map.Entry<String, Object> entry : data.entrySet()) {
                _lifecycleContextDataLowercase.put(entry.getKey().toLowerCase(), entry.getValue());
            }
        }
    }

    protected static void removeContextData(String key) {
        synchronized (_contextDataMutex) {
            _lifecycleContextData.remove(key);
        }
        synchronized (_lowercaseContextDataMutex) {
            _lifecycleContextDataLowercase.remove(key.toLowerCase());
        }
    }

    protected static Map<String, Object> getContextDataLowercase() {
        HashMap<String, Object> map;
        synchronized (_lowercaseContextDataMutex) {
            if (_lifecycleContextDataLowercase.size() <= 0) {
                HashMap<String, Object> previousSessionlifecycleContextData = new HashMap<>();
                addPersistedLifecycleToMap(previousSessionlifecycleContextData);
                for (Map.Entry<String, Object> entry : previousSessionlifecycleContextData.entrySet()) {
                    _lifecycleContextDataLowercase.put(entry.getKey().toLowerCase(), entry.getValue());
                }
            }
            map = _lifecycleContextDataLowercase;
        }
        return map;
    }

    private static void clearContextDataLowercase() {
        synchronized (_lowercaseContextDataMutex) {
            _lifecycleContextDataLowercase.clear();
        }
    }

    private static void addPersistedLifecycleToMap(Map<String, Object> data) {
        try {
            String lifecycleJSONString = StaticMethods.getSharedPreferences().getString("ADMS_LifecycleData", null);
            if (lifecycleJSONString != null && lifecycleJSONString.length() > 0) {
                JSONObject lifecycleData = new JSONObject(lifecycleJSONString);
                data.putAll(StaticMethods.mapFromJson(lifecycleData));
            }
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Issue loading persisted lifecycle data", e.getMessage());
        } catch (JSONException ex) {
            StaticMethods.logWarningFormat("Lifecycle - Issue loading persisted lifecycle data (%s)", ex.getMessage());
        }
    }

    private static void generateLifecycleToBeSaved(Map<String, Object> data) {
        HashMap<String, Object> mutableData = data != null ? new HashMap<>(data) : new HashMap<>();
        mutableData.putAll(StaticMethods.getDefaultData());
        mutableData.put("a.locale", StaticMethods.getDefaultAcceptLanguage());
        mutableData.put("a.ltv.amount", AnalyticsTrackLifetimeValueIncrease.getLifetimeValue());
        _lifecycleContextData.putAll(mutableData);
        clearContextDataLowercase();
        for (Map.Entry<String, Object> entry : _lifecycleContextData.entrySet()) {
            _lifecycleContextDataLowercase.put(entry.getKey().toLowerCase(), entry.getValue());
        }
    }

    private static void resetLifecycleFlags(long date) {
        try {
            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
            if (!StaticMethods.getSharedPreferences().contains("ADMS_SessionStart")) {
                editor.putLong("ADMS_SessionStart", date);
                sessionStartTime = date / 1000;
            }
            editor.putString("ADMS_LastVersion", StaticMethods.getApplicationVersion());
            editor.putBoolean("ADMS_SuccessfulClose", false);
            editor.remove("ADMS_PauseDate");
            editor.commit();
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error resetting lifecycle flags (%s).", e.getMessage());
        }
    }

    private static void checkReferrerDataForLaunch() {
        Map<String, Object> referrerData = getReferrerDataFromSharedPreferences();
        if (referrerData != null && referrerData.size() != 0) {
            updateContextData(referrerData);
            MobileConfig.getInstance().invokeAdobeDataCallback(Config.MobileDataEvent.MOBILE_EVENT_ACQUISITION_LAUNCH, referrerData);
        }
    }

    /* JADX WARN: Code duplicated, block: B:16:0x00a3  */
    /* JADX WARN: Unsupported multi-entry loop pattern (BACK_EDGE: B:16:0x00a3 -> B:17:0x001c). Please report as a decompilation issue!!! */
    private static Map<String, Object> getReferrerDataFromSharedPreferences() {
        Map<String, Object> referrerContextData;
        try {
            if (StaticMethods.getSharedPreferences().contains("ADMS_Referrer_ContextData_Json_String")) {
                String responseString = StaticMethods.getSharedPreferences().getString("ADMS_Referrer_ContextData_Json_String", null);
                referrerContextData = ReferrerHandler.processV3ResponseAndReturnContextData(responseString);
            } else if (StaticMethods.getSharedPreferences().contains("utm_campaign")) {
                Object source = StaticMethods.getSharedPreferences().getString("utm_source", null);
                Object medium = StaticMethods.getSharedPreferences().getString("utm_medium", null);
                Object term = StaticMethods.getSharedPreferences().getString("utm_term", null);
                Object content = StaticMethods.getSharedPreferences().getString("utm_content", null);
                Object campaign = StaticMethods.getSharedPreferences().getString("utm_campaign", null);
                Object trackingcode = StaticMethods.getSharedPreferences().getString("trackingcode", null);
                if (source == null || campaign == null) {
                    referrerContextData = null;
                } else {
                    referrerContextData = new HashMap<>();
                    referrerContextData.put("a.referrer.campaign.source", source);
                    referrerContextData.put("a.referrer.campaign.medium", medium);
                    referrerContextData.put("a.referrer.campaign.term", term);
                    referrerContextData.put("a.referrer.campaign.content", content);
                    referrerContextData.put("a.referrer.campaign.name", campaign);
                    referrerContextData.put("a.referrer.campaign.trackingcode", trackingcode);
                }
            } else {
                referrerContextData = null;
            }
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error pulling persisted Acquisition data (%s)", e.getMessage());
        }
        return referrerContextData;
    }

    private static void addInstallData(Map<String, Object> cdata, long date) {
        DateFormat dayMonthYearFormat = new SimpleDateFormat("M/d/yyyy", Locale.US);
        cdata.put("a.InstallDate", dayMonthYearFormat.format(Long.valueOf(date)));
        cdata.put("a.InstallEvent", "InstallEvent");
        cdata.put("a.DailyEngUserEvent", "DailyEngUserEvent");
        cdata.put("a.MonthlyEngUserEvent", "MonthlyEngUserEvent");
        try {
            if (StaticMethods.getSharedPreferences().contains("ADMS_Referrer_ContextData_Json_String") || StaticMethods.getSharedPreferences().contains("utm_campaign")) {
                Map<String, Object> referrerData = getReferrerDataFromSharedPreferences();
                if (referrerData != null && referrerData.size() >= 0) {
                    cdata.putAll(referrerData);
                    MobileConfig.getInstance().invokeAdobeDataCallback(Config.MobileDataEvent.MOBILE_EVENT_ACQUISITION_INSTALL, referrerData);
                }
            } else if (MobileConfig.getInstance().mobileReferrerConfigured() && MobileConfig.getInstance().getReferrerTimeout() > 0) {
                ReferrerHandler.setReferrerProcessed(false);
                Messages.block3rdPartyCallbacksQueueForReferrer();
            }
            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
            editor.putLong("ADMS_InstallDate", date);
            editor.commit();
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error setting install data (%s).", e.getMessage());
        }
    }

    private static void addUpgradeData(Map<String, Object> cdata, long date) {
        try {
            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
            long upgradeDate = StaticMethods.getSharedPreferences().getLong("ADMS_UpgradeDate", 0L);
            if (!StaticMethods.getApplicationVersion().equalsIgnoreCase(StaticMethods.getSharedPreferences().getString("ADMS_LastVersion", ""))) {
                cdata.put("a.UpgradeEvent", "UpgradeEvent");
                editor.putLong("ADMS_UpgradeDate", date);
                editor.putInt("ADMS_LaunchesAfterUpgrade", 0);
            } else if (upgradeDate > 0) {
                cdata.put("a.DaysSinceLastUpgrade", calculateDaysSince(upgradeDate, date));
            }
            if (upgradeDate > 0) {
                int newLaunchesAfterUpgradeCount = StaticMethods.getSharedPreferences().getInt("ADMS_LaunchesAfterUpgrade", 0) + 1;
                cdata.put("a.LaunchesSinceUpgrade", "" + newLaunchesAfterUpgradeCount);
                editor.putInt("ADMS_LaunchesAfterUpgrade", newLaunchesAfterUpgradeCount);
            }
            editor.commit();
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error setting upgrade data (%s).", e.getMessage());
        }
    }

    private static void addLifecycleGenericData(Map<String, Object> cdata, long date) {
        cdata.putAll(StaticMethods.getDefaultData());
        cdata.put("a.LaunchEvent", "LaunchEvent");
        cdata.put("a.OSVersion", StaticMethods.getOperatingSystem());
        DateFormat hourOfDayDateFormat = new SimpleDateFormat("H", Locale.US);
        cdata.put("a.HourOfDay", hourOfDayDateFormat.format(Long.valueOf(date)));
        Calendar cal = Calendar.getInstance();
        cal.setTimeInMillis(date);
        int dow = cal.get(7);
        cdata.put("a.DayOfWeek", Integer.toString(dow));
        String adid = StaticMethods.getAdvertisingIdentifier();
        if (adid != null) {
            cdata.put("a.adid", adid);
        }
        try {
            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
            int launches = StaticMethods.getSharedPreferences().getInt("ADMS_Launches", 0) + 1;
            cdata.put("a.Launches", Integer.toString(launches));
            editor.putInt("ADMS_Launches", launches);
            editor.putLong("ADMS_LastDateUsed", date);
            editor.commit();
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error adding generic data (%s).", e.getMessage());
        }
    }

    private static void addNonInstallData(Map<String, Object> cdata, long date) {
        try {
            DateFormat dailyFormatter = new SimpleDateFormat("yyyy/M/d", Locale.US);
            long lastUseMillis = StaticMethods.getSharedPreferences().getLong("ADMS_LastDateUsed", 0L);
            String lastUsedDateDaily = dailyFormatter.format(new Date(lastUseMillis));
            String currentDateDaily = dailyFormatter.format(Long.valueOf(date));
            if (!currentDateDaily.equalsIgnoreCase(lastUsedDateDaily)) {
                cdata.put("a.DailyEngUserEvent", "DailyEngUserEvent");
            }
            DateFormat monthlyFormatter = new SimpleDateFormat("yyyy/M", Locale.US);
            String lastUsedDateMonthly = monthlyFormatter.format(new Date(lastUseMillis));
            String currentDateMonthly = monthlyFormatter.format(Long.valueOf(date));
            if (!currentDateMonthly.equalsIgnoreCase(lastUsedDateMonthly)) {
                cdata.put("a.MonthlyEngUserEvent", "MonthlyEngUserEvent");
            }
            long installMillis = StaticMethods.getSharedPreferences().getLong("ADMS_InstallDate", 0L);
            cdata.put("a.DaysSinceFirstUse", calculateDaysSince(installMillis, date));
            cdata.put("a.DaysSinceLastUse", calculateDaysSince(lastUseMillis, date));
            if (!StaticMethods.getSharedPreferences().getBoolean("ADMS_SuccessfulClose", false)) {
                SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                editor.remove("ADMS_PauseDate");
                editor.remove("ADMS_SessionStart");
                sessionStartTime = StaticMethods.getTimeSince1970();
                editor.commit();
                long lastKnownTimestamp = StaticMethods.getSharedPreferences().getLong("ADBLastKnownTimestampKey", 0L);
                if (lastKnownTimestamp > 0 && MobileConfig.getInstance().mobileUsingAnalytics() && MobileConfig.getInstance().getOfflineTrackingEnabled() && MobileConfig.getInstance().getBackdateSessionInfoEnabled()) {
                    try {
                        SharedPreferences defaults = StaticMethods.getSharedPreferences();
                        HashMap<String, Object> crashCData = new HashMap<>();
                        crashCData.put("a.CrashEvent", "CrashEvent");
                        crashCData.put("a.OSVersion", defaults.getString("ADOBEMOBILE_STOREDDEFAULTS_OS", ""));
                        crashCData.put("a.AppID", defaults.getString("ADOBEMOBILE_STOREDDEFAULTS_APPID", ""));
                        AnalyticsTrackInternal.trackInternal("Crash", crashCData, 1 + lastKnownTimestamp);
                        _lifecycleContextData.put("a.CrashEvent", "CrashEvent");
                    } catch (StaticMethods.NullContextException ex) {
                        StaticMethods.logWarningFormat("Config - Unable to get crash data for backdated hit (%s)", ex.getLocalizedMessage());
                    }
                } else {
                    cdata.put("a.CrashEvent", "CrashEvent");
                }
                AnalyticsTrackTimedAction.sharedInstance().trackTimedActionUpdateActionsClearAdjustedStartTime();
            }
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error setting non install data (%s).", e.getMessage());
        }
    }

    private static void addSessionLengthData(Map<String, Object> cdata) {
        try {
            long pauseDate = StaticMethods.getSharedPreferences().getLong("ADMS_PauseDate", 0L);
            int sessionTimeout = MobileConfig.getInstance().getLifecycleTimeout();
            int timeSincePause = (int) ((new Date().getTime() - pauseDate) / 1000);
            if (timeSincePause >= sessionTimeout) {
                long sessionStart = StaticMethods.getSharedPreferences().getLong("ADMS_SessionStart", 0L);
                int sessionTime = (int) ((pauseDate - sessionStart) / 1000);
                sessionStartTime = StaticMethods.getTimeSince1970();
                if (sessionTime > 0 && sessionTime < 604800) {
                    long lastKnownTimestamp = StaticMethods.getSharedPreferences().getLong("ADBLastKnownTimestampKey", 0L);
                    if (lastKnownTimestamp > 0 && MobileConfig.getInstance().mobileUsingAnalytics() && MobileConfig.getInstance().getOfflineTrackingEnabled() && MobileConfig.getInstance().getBackdateSessionInfoEnabled()) {
                        try {
                            SharedPreferences defaults = StaticMethods.getSharedPreferences();
                            HashMap<String, Object> sessionCData = new HashMap<>();
                            sessionCData.put("a.PrevSessionLength", String.valueOf(sessionTime));
                            sessionCData.put("a.OSVersion", defaults.getString("ADOBEMOBILE_STOREDDEFAULTS_OS", ""));
                            sessionCData.put("a.AppID", defaults.getString("ADOBEMOBILE_STOREDDEFAULTS_APPID", ""));
                            AnalyticsTrackInternal.trackInternal("SessionInfo", sessionCData, 1 + lastKnownTimestamp);
                            _lifecycleContextData.put("a.PrevSessionLength", String.valueOf(sessionTime));
                        } catch (StaticMethods.NullContextException ex) {
                            StaticMethods.logWarningFormat("Config - Unable to get session data for backdated hit (%s)", ex.getLocalizedMessage());
                        }
                    } else {
                        cdata.put("a.PrevSessionLength", Integer.toString(sessionTime));
                    }
                } else {
                    cdata.put("a.ignoredSessionLength", Integer.toString(sessionTime));
                }
                SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                editor.remove("ADMS_SessionStart");
                editor.commit();
            }
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Lifecycle - Error adding session length data (%s).", e.getMessage());
        }
    }

    private static String calculateDaysSince(long since, long to) {
        return Integer.toString((int) ((to - since) / MeasurementDispatcher.MILLIS_PER_DAY));
    }

    private static void checkForAdobeClickThrough(Activity activity, boolean isNewSession) {
        Intent intent;
        if (activity != null && (intent = activity.getIntent()) != null) {
            String action = null;
            String pushMessageID = intent.getStringExtra(ADB_LIFECYCLE_PUSH_MESSAGE_ID_KEY);
            String messageID = intent.getStringExtra("adb_m_l_id");
            Uri uri = intent.getData();
            Map<? extends String, ? extends Object> deepLinkData = getAdobeDeepLinkQueryParameters(uri);
            HashMap<String, Object> cData = new HashMap<>();
            if (!isNewSession && deepLinkData != null) {
                action = ADB_TRACK_INTERNAL_ADOBE_LINK;
                cData.putAll(deepLinkData);
                updateContextData(cData);
            }
            if (pushMessageID != null && pushMessageID.length() > 0) {
                cData.put("a.push.payloadId", pushMessageID);
                action = ADB_TRACK_INTERNAL_PUSH_CLICK_THROUGH;
                updateContextData(cData);
            } else if (messageID != null && messageID.length() > 0) {
                cData.put("a.message.id", messageID);
                cData.put("a.message.clicked", 1);
                action = "In-App Message";
                updateContextData(cData);
            }
            if (action != null && MobileConfig.getInstance().mobileUsingAnalytics()) {
                AnalyticsTrackInternal.trackInternal(action, cData, StaticMethods.getTimeSince1970());
            }
        }
    }

    private static Map<String, Object> checkForAdobeLinkData(Activity activity) {
        Intent intent;
        Uri uri;
        if (activity == null || (intent = activity.getIntent()) == null || (uri = intent.getData()) == null) {
            return null;
        }
        return getAdobeDeepLinkQueryParameters(uri);
    }

    private static Map<String, Object> getAdobeDeepLinkQueryParameters(Uri uri) {
        if (uri == null) {
            return null;
        }
        String query = uri.getQuery();
        if (query == null || query.length() <= 0) {
            return null;
        }
        Map<String, Object> parameters = new HashMap<>();
        int start = 0;
        do {
            int next = query.indexOf(38, start);
            int end = next == -1 ? query.length() : next;
            int separator = query.indexOf(61, start);
            if (separator > end || separator == -1) {
                separator = end;
            }
            parameters.put(query.substring(start, separator), query.substring(separator + 1, end));
            start = end + 1;
        } while (start < query.length());
        if (parameters.containsKey("a.deeplink.id")) {
            return parameters;
        }
        return null;
    }
}
