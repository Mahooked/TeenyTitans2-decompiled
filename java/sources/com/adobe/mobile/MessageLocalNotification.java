package com.adobe.mobile;

import android.app.Activity;
import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Intent;
import java.security.SecureRandom;
import java.util.Calendar;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class MessageLocalNotification extends Message {
    private static final String JSON_CONFIG_CONTENT = "content";
    private static final String JSON_CONFIG_WAIT = "wait";
    protected String content;
    protected String deeplink;
    protected Integer localNotificationDelay;
    protected String userInfo;

    MessageLocalNotification() {
    }

    @Override // com.adobe.mobile.Message
    protected boolean initWithPayloadObject(JSONObject dictionary) {
        if (dictionary == null || dictionary.length() <= 0 || !super.initWithPayloadObject(dictionary)) {
            return false;
        }
        StaticMethods.logDebugFormat("Messages -  Building Local Notification message.", new Object[0]);
        try {
            JSONObject jsonPayload = dictionary.getJSONObject("payload");
            if (jsonPayload.length() <= 0) {
                StaticMethods.logDebugFormat("Messages - Unable to create local notification message \"%s\", payload is empty", this.messageId);
                return false;
            }
            try {
                this.content = jsonPayload.getString(JSON_CONFIG_CONTENT);
                if (this.content.length() <= 0) {
                    StaticMethods.logDebugFormat("Messages - Unable to create local notification message \"%s\", content is empty", this.messageId);
                    return false;
                }
                try {
                    this.localNotificationDelay = Integer.valueOf(jsonPayload.getInt(JSON_CONFIG_WAIT));
                    try {
                        this.deeplink = jsonPayload.getString(Config.ADB_MESSAGE_DEEPLINK_KEY);
                    } catch (JSONException e) {
                        StaticMethods.logDebugFormat("Messages - Tried to read deeplink for local notification message but found none.  This is not a required field", new Object[0]);
                    }
                    try {
                        this.userInfo = jsonPayload.getJSONObject("userData").toString();
                    } catch (NullPointerException e2) {
                        StaticMethods.logDebugFormat("Messages - Tried to read userData for local notification message but found none.  This is not a required field", new Object[0]);
                    } catch (JSONException e3) {
                        StaticMethods.logDebugFormat("Messages - Tried to read userData for local notification message but found none.  This is not a required field", new Object[0]);
                    }
                    StaticMethods.logDebugFormat("Message created with: \n MessageID: \"%s\" \n Content: \"%s\" \n Delay: \"%d\" \n Deeplink: \"%s\" \n User Data: \"%s\"", this.messageId, this.content, this.localNotificationDelay, this.deeplink, this.userInfo);
                    return true;
                } catch (JSONException e4) {
                    StaticMethods.logDebugFormat("Messages - Unable to create local notification message \"%s\", localNotificationDelay is required", this.messageId);
                    return false;
                }
            } catch (JSONException e5) {
                StaticMethods.logDebugFormat("Messages - Unable to create local notification message \"%s\", content is required", this.messageId);
                return false;
            }
        } catch (JSONException e6) {
            StaticMethods.logDebugFormat("Messages - Unable to create local notification message \"%s\", payload is required", this.messageId);
            return false;
        }
    }

    @Override // com.adobe.mobile.Message
    protected void show() {
        super.show();
        try {
            Activity currentActivity = StaticMethods.getCurrentActivity();
            int requestCode = new SecureRandom().nextInt();
            Calendar calendar = Calendar.getInstance();
            calendar.add(13, this.localNotificationDelay.intValue());
            Intent intent = new Intent("android.intent.action.VIEW");
            intent.setClass(currentActivity, MessageNotificationHandler.class);
            intent.putExtra("adbMessageCode", Messages.MESSAGE_LOCAL_IDENTIFIER);
            intent.putExtra("adb_m_l_id", this.messageId);
            intent.putExtra("requestCode", requestCode);
            intent.putExtra("userData", this.userInfo);
            intent.putExtra(Config.ADB_MESSAGE_DEEPLINK_KEY, this.deeplink);
            intent.putExtra("alarm_message", this.content);
            try {
                PendingIntent sender = PendingIntent.getBroadcast(StaticMethods.getSharedContext(), requestCode, intent, 134217728);
                AlarmManager alarmManager = (AlarmManager) StaticMethods.getSharedContext().getSystemService("alarm");
                alarmManager.set(0, calendar.getTimeInMillis(), sender);
            } catch (StaticMethods.NullContextException e) {
                StaticMethods.logErrorFormat("Messaging - Error scheduling local notification (%s)", e.getMessage());
            }
        } catch (StaticMethods.NullActivityException ex) {
            StaticMethods.logErrorFormat(ex.getMessage(), new Object[0]);
        }
    }
}
