package com.adobe.mobile;

import android.content.SharedPreferences;
import java.math.BigDecimal;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;
import java.util.UUID;
import org.apache.http.client.methods.HttpPost;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

/* JADX INFO: loaded from: classes.dex */
final class TargetWorker {
    protected static final String COOKIE_EXPIRES_KEY_SUFFIX = "_Expires";
    protected static final String COOKIE_NAME_PCID = "mboxPC";
    protected static final String COOKIE_NAME_SESSION = "mboxSession";
    protected static final String COOKIE_VALUE_KEY_SUFFIX = "_Value";
    protected static final String PROFILE_PARAMETER_PREFIX = "profile.";
    protected static final String TARGET_API_A4T_ACTION_NAME = "AnalyticsForTarget";
    protected static final String TARGET_API_CONTENT_TYPE = "application/json";
    protected static final String TARGET_API_JSON_A4T_LOGGING_ENABLED = "a4t";
    protected static final String TARGET_API_JSON_A4T_LOGGING_PAYLOAD = "clientSideAnalyticsLoggingPayload";
    protected static final String TARGET_API_JSON_A4T_LOGGING_PAYLOAD_PARAMETERS = "parameters";
    protected static final String TARGET_API_JSON_CONTENT = "content";
    protected static final String TARGET_API_JSON_EDGE_HOST = "edgeHost";
    protected static final String TARGET_API_JSON_ERROR_MESSAGE = "message";
    protected static final String TARGET_API_JSON_LOGGING_ENABLED = "clientSideAnalyticsLogging";
    protected static final String TARGET_API_JSON_MBOX = "mbox";
    protected static final String TARGET_API_JSON_MBOX_PARAMETERS = "mboxParameters";
    protected static final String TARGET_API_JSON_MC_VISITOR_ID = "marketingCloudVisitorId";
    protected static final String TARGET_API_JSON_ORDER = "order";
    protected static final String TARGET_API_JSON_ORDER_ID = "id";
    protected static final String TARGET_API_JSON_ORDER_PRODUCTS = "purchasedProductIds";
    protected static final String TARGET_API_JSON_ORDER_TOTAL = "total";
    protected static final String TARGET_API_JSON_PROFILE_PARAMETERS = "profileParameters";
    protected static final String TARGET_API_JSON_THIRD_PARTY_ID = "thirdPartyId";
    protected static final String TARGET_API_JSON_TNT_ID = "tntId";
    protected static final String TARGET_API_URL_BASE = "https://%s/rest/v1/mbox/%s?client=%s";
    protected static final String TARGET_API_URL_HOST_BASE = "%s.tt.omtrdc.net";
    private static final long TARGET_SESSION_TIMEOUT_LENGTH = 1800;
    private static String _sessionId = null;
    private static String _tntId = null;
    private static String _thirdPartyId = null;
    private static String _edgeHost = null;
    private static HashMap<String, Object> _persistentParameters = null;
    private static final Object _sessionIdMutex = new Object();
    private static final Object _tntIdMutex = new Object();
    private static final Object _thirdPartyIdMutex = new Object();
    private static final Object _edgeHostMutex = new Object();
    private static final Object _persistentParametersMutex = new Object();
    private static boolean _oldCookiesHaveBeenChecked = false;
    private static final Object _checkOldCookiesMutex = new Object();

    TargetWorker() {
    }

    protected static void loadRequest(TargetLocationRequest request, Target.TargetCallback<String> callback) {
        if (request == null) {
            StaticMethods.logWarningFormat("Target - \"TargetLocationRequest\" object cannot be null", new Object[0]);
            if (callback != null) {
                callback.call(null);
                return;
            }
            return;
        }
        Map<String, Object> newMboxParameters = request.parameters;
        Map<String, Object> newProfileParameters = getProfileParametersFromMboxParameters(newMboxParameters);
        Map<String, Object> newOrderParameters = getOrderParametersFromMboxParameters(newMboxParameters);
        checkFor3rdPartyId(newMboxParameters);
        loadRequest(request.name, request.defaultContent, newProfileParameters, newOrderParameters, newMboxParameters, callback);
    }

    protected static void loadRequest(String mboxName, String defaultContent, Map<String, Object> profileParameters, Map<String, Object> orderParameters, Map<String, Object> mboxParameters, Target.TargetCallback<String> callback) {
        if (!MobileConfig.getInstance().mobileUsingTarget() || MobileConfig.getInstance().getPrivacyStatus() != MobilePrivacyStatus.MOBILE_PRIVACY_STATUS_OPT_IN) {
            if (callback != null) {
                callback.call(defaultContent);
            }
        } else {
            if (mboxName == null || mboxName.length() <= 0) {
                StaticMethods.logWarningFormat("Target - \"mboxName\" parameter is required for Target calls - returning default content", new Object[0]);
                if (callback != null) {
                    callback.call(defaultContent);
                    return;
                }
                return;
            }
            TargetRunner targetRunner = new TargetRunner(mboxName, defaultContent, profileParameters, orderParameters, mboxParameters, callback);
            StaticMethods.getAnalyticsExecutor().execute(targetRunner);
        }
    }

    protected static String getThirdPartyId() {
        String str = null;
        synchronized (_thirdPartyIdMutex) {
            if (_thirdPartyId == null || _thirdPartyId.isEmpty()) {
                try {
                    SharedPreferences preferences = StaticMethods.getSharedPreferences();
                    _thirdPartyId = preferences.getString("ADBMOBILE_TARGET_3RD_PARTY_ID", null);
                } catch (StaticMethods.NullContextException e) {
                    StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
                }
            }
            str = _thirdPartyId;
        }
        return str;
    }

    protected static void setThirdPartyId(String newThirdPartyId) {
        synchronized (_thirdPartyIdMutex) {
            if (_thirdPartyId == null || newThirdPartyId == null || !_thirdPartyId.equals(newThirdPartyId)) {
                if (_thirdPartyId != null) {
                    setSessionId(null);
                }
                _thirdPartyId = newThirdPartyId;
                try {
                    SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                    if (_thirdPartyId == null || _thirdPartyId.isEmpty()) {
                        editor.remove("ADBMOBILE_TARGET_3RD_PARTY_ID");
                    } else {
                        editor.putString("ADBMOBILE_TARGET_3RD_PARTY_ID", _thirdPartyId);
                    }
                    editor.commit();
                } catch (StaticMethods.NullContextException e) {
                    StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
                }
            }
        }
    }

    protected static String getTntId() {
        String str = null;
        setTntIdAndSessionFromOldCookieValues();
        synchronized (_tntIdMutex) {
            if (_tntId == null || _tntId.isEmpty()) {
                try {
                    SharedPreferences preferences = StaticMethods.getSharedPreferences();
                    _tntId = preferences.getString("ADBMOBILE_TARGET_TNT_ID", null);
                } catch (StaticMethods.NullContextException e) {
                    StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
                }
            }
            str = _tntId;
        }
        return str;
    }

    private static boolean tntIdValuesAreEqual(String oldId, String newId) {
        if (oldId == null && newId == null) {
            return true;
        }
        if (oldId == null || newId == null) {
            return false;
        }
        if (oldId.equals(newId)) {
            return true;
        }
        int dotIndexOfOldId = oldId.indexOf(46);
        String oldIdSubstring = dotIndexOfOldId == -1 ? oldId : oldId.substring(0, dotIndexOfOldId);
        int dotIndexOfNewId = newId.indexOf(46);
        String newIdSubstring = dotIndexOfNewId == -1 ? newId : newId.substring(0, dotIndexOfNewId);
        return oldIdSubstring.equals(newIdSubstring);
    }

    protected static void setTntId(String newTntId) {
        synchronized (_tntIdMutex) {
            if (!tntIdValuesAreEqual(_tntId, newTntId)) {
                if (_tntId != null && _tntId.length() > 0) {
                    setSessionId(null);
                }
                _tntId = newTntId;
                try {
                    SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                    if (_tntId == null || _tntId.isEmpty()) {
                        editor.remove("ADBMOBILE_TARGET_TNT_ID");
                    } else {
                        editor.putString("ADBMOBILE_TARGET_TNT_ID", _tntId);
                    }
                    editor.commit();
                } catch (StaticMethods.NullContextException e) {
                    StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
                }
            }
        }
    }

    protected static String getSessionId() {
        String str = null;
        setTntIdAndSessionFromOldCookieValues();
        synchronized (_sessionIdMutex) {
            if (_sessionId == null || _sessionId.isEmpty() || sessionIdIsExpired()) {
                try {
                    SharedPreferences preferences = StaticMethods.getSharedPreferences();
                    _sessionId = preferences.getString("ADBMOBILE_TARGET_SESSION_ID", null);
                    if (_sessionId == null || _sessionId.isEmpty() || sessionIdIsExpired()) {
                        setSessionId(UUID.randomUUID().toString(), false);
                    }
                } catch (StaticMethods.NullContextException e) {
                    StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
                }
            }
            str = _sessionId;
        }
        return str;
    }

    protected static void addPersistentParameter(String key, String value) {
        if (key != null && value != null) {
            synchronized (_persistentParametersMutex) {
                if (_persistentParameters == null) {
                    _persistentParameters = new HashMap<>();
                }
                _persistentParameters.put(key, value);
            }
        }
    }

    protected static void removePersistentParameter(String key) {
        if (key != null) {
            synchronized (_persistentParametersMutex) {
                if (_persistentParameters != null) {
                    _persistentParameters.remove(key);
                }
            }
        }
    }

    private static class TargetRunner implements Runnable {
        private Target.TargetCallback<String> callback;
        private String defaultContent;
        private String mboxName;
        private Map<String, Object> mboxParameters;
        private Map<String, Object> orderParameters;
        private Map<String, Object> profileParameters;

        private TargetRunner(String newMboxName, String newDefaultContent, Map<String, Object> newProfileParameters, Map<String, Object> newOrderParameters, Map<String, Object> newMboxParameters, Target.TargetCallback<String> newCallback) {
            this.mboxName = newMboxName;
            this.defaultContent = newDefaultContent;
            this.profileParameters = newProfileParameters;
            this.orderParameters = newOrderParameters;
            this.mboxParameters = newMboxParameters;
            this.callback = newCallback;
        }

        @Override // java.lang.Runnable
        public void run() {
            TargetWorker.setTntIdAndSessionFromOldCookieValues();
            String requestUrl = TargetWorker.getRequestUrl();
            JSONObject parametersJson = TargetWorker.getJsonObjectForParameters(this.mboxName, this.profileParameters, this.orderParameters, this.mboxParameters);
            if (parametersJson == null) {
                if (this.callback != null) {
                    this.callback.call(this.defaultContent);
                    return;
                }
                return;
            }
            StaticMethods.logDebugFormat("Target - requesting content from url \"%s\" with parameters: %s", requestUrl, parametersJson.toString());
            NetworkObject networkObject = RequestHandler.retrieveNetworkObject(requestUrl, HttpPost.METHOD_NAME, parametersJson.toString(), MobileConfig.getInstance().getDefaultLocationTimeout(), TargetWorker.TARGET_API_CONTENT_TYPE, "Target");
            if (networkObject == null) {
                StaticMethods.logErrorFormat("Target - unable to open connection (%s)", requestUrl);
                if (this.callback != null) {
                    this.callback.call(this.defaultContent);
                    return;
                }
                return;
            }
            if (networkObject.response == null || networkObject.response.isEmpty()) {
                StaticMethods.logDebugFormat("Target - Response was empty", new Object[0]);
                if (this.callback != null) {
                    this.callback.call(this.defaultContent);
                    return;
                }
                return;
            }
            try {
                if (networkObject.responseCode == 200) {
                    JSONObject responseJson = new JSONObject(networkObject.response);
                    String errorMessage = TargetWorker.getErrorMessage(responseJson);
                    if (errorMessage != null && !errorMessage.isEmpty()) {
                        StaticMethods.logDebugFormat("Target - An error was reported by the server (%s)", new Object[0]);
                        if (this.callback != null) {
                            this.callback.call(this.defaultContent);
                        }
                    } else {
                        SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                        editor.putLong("ADBMOBILE_TARGET_LAST_TIMESTAMP", StaticMethods.getTimeSince1970());
                        editor.commit();
                        TargetWorker.saveTargetResponseVariables(responseJson);
                        TargetWorker.forwardAnalyticsForTargetPayload(responseJson);
                        String content = responseJson.getString(TargetWorker.TARGET_API_JSON_CONTENT);
                        if (content == null || content.isEmpty()) {
                            StaticMethods.logDebugFormat("Target - Content was empty", new Object[0]);
                            if (this.callback != null) {
                                this.callback.call(this.defaultContent);
                            }
                        } else {
                            StaticMethods.logDebugFormat("Target - Response received for location \"%s\" - \"%s\"", this.mboxName, content);
                            if (this.callback != null) {
                                this.callback.call(content);
                            }
                        }
                    }
                } else {
                    StaticMethods.logDebugFormat("Target - Response code from Target server (%d) was invalid, returning default content", Integer.valueOf(networkObject.responseCode));
                    if (this.callback != null) {
                        this.callback.call(this.defaultContent);
                    }
                }
            } catch (StaticMethods.NullContextException e) {
                StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
            } catch (NullPointerException ex) {
                StaticMethods.logErrorFormat("Target - NullPointerException while trying to get content (%s)", ex.getLocalizedMessage());
                if (this.callback != null) {
                    this.callback.call(this.defaultContent);
                }
            } catch (JSONException ex2) {
                StaticMethods.logErrorFormat("Target - JSONException while trying to get content (%s)", ex2.getLocalizedMessage());
                if (this.callback != null) {
                    this.callback.call(this.defaultContent);
                }
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static String getErrorMessage(JSONObject jsonResponse) {
        if (jsonResponse == null) {
            return null;
        }
        try {
            return jsonResponse.getString(TARGET_API_JSON_ERROR_MESSAGE);
        } catch (JSONException e) {
            return null;
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static JSONObject getJsonObjectForParameters(String mboxName, Map<String, Object> profileParameters, Map<String, Object> orderParameters, Map<String, Object> mboxParameters) {
        try {
            JSONObject parametersJson = new JSONObject();
            parametersJson.put(TARGET_API_JSON_MBOX, mboxName);
            parametersJson.put(TARGET_API_JSON_LOGGING_ENABLED, true);
            String tntId = getTntId();
            if (tntId != null && !tntId.isEmpty()) {
                parametersJson.put(TARGET_API_JSON_TNT_ID, tntId);
            }
            String thirdPartyId = getThirdPartyId();
            if (thirdPartyId != null && !thirdPartyId.isEmpty()) {
                parametersJson.put(TARGET_API_JSON_THIRD_PARTY_ID, thirdPartyId);
            }
            String mcVisId = VisitorIDService.sharedInstance().getMarketingCloudID();
            if (mcVisId != null && !mcVisId.isEmpty()) {
                parametersJson.put(TARGET_API_JSON_MC_VISITOR_ID, mcVisId);
            }
            JSONObject profileParametersJson = getProfileParameters(profileParameters);
            if (profileParametersJson != null && profileParametersJson.length() > 0) {
                parametersJson.put(TARGET_API_JSON_PROFILE_PARAMETERS, profileParametersJson);
            }
            JSONObject orderDetailsJson = getOrderParameters(orderParameters);
            if (orderDetailsJson != null && orderDetailsJson.length() > 0) {
                parametersJson.put(TARGET_API_JSON_ORDER, orderDetailsJson);
            }
            JSONObject mboxParametersJson = getMboxParameters(mboxParameters);
            if (mboxParametersJson != null && mboxParametersJson.length() > 0) {
                parametersJson.put(TARGET_API_JSON_MBOX_PARAMETERS, mboxParametersJson);
                return parametersJson;
            }
            return parametersJson;
        } catch (JSONException ex) {
            StaticMethods.logErrorFormat("Target - JSONException while trying to get content (%s)", ex.getLocalizedMessage());
            return null;
        }
    }

    private static void checkFor3rdPartyId(Map<String, Object> profileParameters) {
        if (profileParameters != null && profileParameters.containsKey(TargetLocationRequest.TARGET_PARAMETER_MBOX_3RDPARTY_ID)) {
            Object newThirdPartyId = profileParameters.get(TargetLocationRequest.TARGET_PARAMETER_MBOX_3RDPARTY_ID);
            if (newThirdPartyId != null) {
                setThirdPartyId(newThirdPartyId.toString());
            } else {
                setThirdPartyId(null);
            }
            profileParameters.remove(TargetLocationRequest.TARGET_PARAMETER_MBOX_3RDPARTY_ID);
        }
    }

    protected static void setTntIdAndSessionFromOldCookieValues() {
        synchronized (_checkOldCookiesMutex) {
            if (!_oldCookiesHaveBeenChecked) {
                String oldPcid = checkForOldCookieValue("mboxPC");
                if (oldPcid != null) {
                    setTntId(oldPcid);
                }
                String oldSession = checkForOldCookieValue("mboxSession");
                if (oldSession != null) {
                    setSessionId(oldSession);
                }
                _oldCookiesHaveBeenChecked = true;
            }
        }
    }

    private static String checkForOldCookieValue(String cookieName) {
        if (cookieName == null || cookieName.length() == 0) {
            return null;
        }
        String returnCookieValue = null;
        try {
            SharedPreferences preferences = StaticMethods.getSharedPreferences();
            if (!preferences.contains(cookieName + COOKIE_EXPIRES_KEY_SUFFIX)) {
                return null;
            }
            long cookieExpiryTime = preferences.getLong(cookieName + COOKIE_EXPIRES_KEY_SUFFIX, 0L);
            if (cookieExpiryTime > System.currentTimeMillis()) {
                String prefsCookieValue = preferences.getString(cookieName + COOKIE_VALUE_KEY_SUFFIX, "");
                if (prefsCookieValue.length() > 0) {
                    returnCookieValue = prefsCookieValue;
                }
            }
            SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
            editor.remove(cookieName + COOKIE_VALUE_KEY_SUFFIX);
            editor.remove(cookieName + COOKIE_EXPIRES_KEY_SUFFIX);
            editor.commit();
            return returnCookieValue;
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
            return returnCookieValue;
        }
    }

    private static JSONObject getOrderParameters(Map<String, Object> orderParams) {
        if (orderParams == null || orderParams.size() <= 0) {
            return null;
        }
        JSONObject orderJson = new JSONObject();
        Object orderId = orderParams.get("id");
        Object orderTotal = orderParams.get("total");
        Object products = orderParams.get("purchasedProductIds");
        if (orderId != null) {
            try {
                if (!orderId.toString().isEmpty()) {
                    orderJson.put("id", orderId.toString());
                }
            } catch (JSONException ex) {
                StaticMethods.logWarningFormat("Target - JSONException while creating order details (%s)", ex.getLocalizedMessage());
                return null;
            }
        }
        if (orderTotal != null && !orderTotal.toString().isEmpty()) {
            try {
                double orderTotalDouble = Double.parseDouble(orderTotal.toString());
                orderJson.put("total", orderTotalDouble);
            } catch (NumberFormatException ex2) {
                StaticMethods.logWarningFormat("Target - NumberFormatException while creating order details (%s)", ex2.getLocalizedMessage());
            }
        }
        if (products != null && !products.toString().isEmpty()) {
            String[] productsList = products.toString().split(",");
            JSONArray productsJson = new JSONArray();
            for (String product : productsList) {
                productsJson.put(product.trim());
            }
            orderJson.put("purchasedProductIds", productsJson);
            return orderJson;
        }
        return orderJson;
    }

    private static Map<String, Object> getProfileParametersFromMboxParameters(Map<String, Object> mboxParameters) {
        if (mboxParameters == null || mboxParameters.size() <= 0) {
            return null;
        }
        HashMap<String, Object> newProfileParams = new HashMap<>();
        HashMap<String, Object> mboxParametersCopy = new HashMap<>(mboxParameters);
        for (Map.Entry<String, Object> entry : mboxParametersCopy.entrySet()) {
            String key = entry.getKey();
            if (key != null) {
                String trimmedKey = key.trim();
                String cleanedKey = trimmedKey.toLowerCase();
                if (cleanedKey.startsWith(PROFILE_PARAMETER_PREFIX)) {
                    int dotIndex = cleanedKey.indexOf(".");
                    String newKeyName = trimmedKey.substring(dotIndex + 1);
                    newProfileParams.put(newKeyName, entry.getValue());
                    mboxParameters.remove(entry.getKey());
                }
            }
        }
        return newProfileParams;
    }

    private static Map<String, Object> getOrderParametersFromMboxParameters(Map<String, Object> mboxParameters) {
        if (mboxParameters == null || mboxParameters.size() <= 0) {
            return null;
        }
        HashMap<String, Object> newOrderParams = new HashMap<>();
        Object orderId = mboxParameters.get("id");
        Object orderTotal = mboxParameters.get("total");
        Object products = mboxParameters.get("purchasedProductIds");
        if (orderId != null && !orderId.toString().isEmpty()) {
            newOrderParams.put("id", orderId.toString());
            mboxParameters.remove("id");
        }
        if (orderTotal != null && !orderTotal.toString().isEmpty()) {
            newOrderParams.put("total", orderTotal);
            mboxParameters.remove("total");
        }
        if (products != null && !products.toString().isEmpty()) {
            newOrderParams.put("purchasedProductIds", products);
            mboxParameters.remove("purchasedProductIds");
            return newOrderParams;
        }
        return newOrderParams;
    }

    private static JSONObject getMboxParameters(Map<String, Object> mboxParameters) {
        JSONObject visitorIdParameters = getVisitorIdParameters();
        JSONObject mboxParametersJson = new JSONObject();
        if (visitorIdParameters != null && visitorIdParameters.length() > 0) {
            Iterator<String> itKeys = visitorIdParameters.keys();
            while (itKeys.hasNext()) {
                String currentKey = itKeys.next().toString();
                try {
                    mboxParametersJson.put(currentKey, visitorIdParameters.get(currentKey));
                } catch (JSONException ex) {
                    StaticMethods.logWarningFormat("Target - JSONException adding mbox parameter to target request (%s)", ex.getLocalizedMessage());
                }
            }
        }
        if (mboxParameters != null && mboxParameters.size() > 0) {
            for (Map.Entry<String, Object> entry : mboxParameters.entrySet()) {
                try {
                    mboxParametersJson.put(entry.getKey(), entry.getValue());
                } catch (JSONException ex2) {
                    StaticMethods.logWarningFormat("Target - JSONException adding mbox parameter to target request (%s)", ex2.getLocalizedMessage());
                }
            }
        }
        HashMap<String, Object> internalParams = getInternalTargetParams();
        if (internalParams != null && internalParams.size() > 0) {
            for (Map.Entry<String, Object> param : internalParams.entrySet()) {
                try {
                    mboxParametersJson.put(param.getKey(), param.getValue());
                } catch (JSONException ex3) {
                    StaticMethods.logWarningFormat("Target - JSONException adding mbox parameter to target request (%s)", ex3.getLocalizedMessage());
                }
            }
        }
        return mboxParametersJson;
    }

    private static JSONObject getVisitorIdParameters() {
        HashMap<String, Object> visitorIdData;
        try {
            JSONObject visidParams = new JSONObject();
            if (!MobileConfig.getInstance().getVisitorIdServiceEnabled() || (visitorIdData = VisitorIDService.sharedInstance().getTargetParameterMap()) == null || visitorIdData.size() <= 0) {
                return visidParams;
            }
            for (Map.Entry<String, Object> entry : visitorIdData.entrySet()) {
                visidParams.put(entry.getKey(), entry.getValue());
            }
            return visidParams;
        } catch (JSONException ex) {
            StaticMethods.logWarningFormat("Target - JSONException when adding Visitor ID data to Target request (%s)", ex.getLocalizedMessage());
            return null;
        }
    }

    private static JSONObject getProfileParameters(Map<String, Object> customerParams) {
        try {
            JSONObject profileParametersJson = new JSONObject();
            if (customerParams == null || customerParams.size() <= 0) {
                return profileParametersJson;
            }
            for (Map.Entry<String, Object> param : customerParams.entrySet()) {
                profileParametersJson.put(param.getKey(), param.getValue());
            }
            return profileParametersJson;
        } catch (JSONException ex) {
            StaticMethods.logWarningFormat("Target - Error adding profile parameters to target request (%s)", ex.getLocalizedMessage());
            return null;
        }
    }

    private static HashMap<String, Object> getInternalTargetParams() {
        HashMap<String, Object> internalParams = new HashMap<>();
        HashMap<String, Object> lifecycleData = Lifecycle.getContextData();
        if (lifecycleData != null && lifecycleData.size() > 0) {
            internalParams.putAll(lifecycleData);
        }
        BigDecimal ltv = AnalyticsTrackLifetimeValueIncrease.getLifetimeValue();
        if (ltv != null) {
            addPersistentParameter("a.ltv.amount", ltv.toString());
        }
        synchronized (_persistentParametersMutex) {
            if (_persistentParameters != null && _persistentParameters.size() > 0) {
                internalParams.putAll(_persistentParameters);
            }
        }
        return internalParams;
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static void saveTargetResponseVariables(JSONObject response) {
        try {
            setTntId(response.getString(TARGET_API_JSON_TNT_ID));
        } catch (JSONException e) {
            setTntId(null);
        }
        try {
            setEdgeHost(response.getString(TARGET_API_JSON_EDGE_HOST));
        } catch (JSONException e2) {
            setEdgeHost(null);
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static String getRequestUrl() {
        String requestHost;
        String targetClientCode = MobileConfig.getInstance().getClientCode();
        String edgeHost = getEdgeHost();
        if (edgeHost != null && !edgeHost.isEmpty()) {
            requestHost = edgeHost;
        } else {
            requestHost = String.format(TARGET_API_URL_HOST_BASE, targetClientCode);
        }
        return String.format(TARGET_API_URL_BASE, requestHost, getSessionId(), targetClientCode);
    }

    protected static void setSessionId(String newSessionId) {
        setSessionId(newSessionId, true);
    }

    private static void setSessionId(String newSessionId, boolean useMutex) {
        if (useMutex) {
            synchronized (_sessionIdMutex) {
                setSessionIdInternal(newSessionId);
            }
            return;
        }
        setSessionIdInternal(newSessionId);
    }

    private static void setSessionIdInternal(String newSessionId) {
        _sessionId = newSessionId;
        try {
            SharedPreferences preferences = StaticMethods.getSharedPreferences();
            String existingSessionId = preferences.getString("ADBMOBILE_TARGET_SESSION_ID", null);
            if (existingSessionId == null || !existingSessionId.equals(_sessionId)) {
                SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                if (_sessionId != null && !_sessionId.isEmpty()) {
                    editor.putString("ADBMOBILE_TARGET_SESSION_ID", _sessionId);
                    editor.putLong("ADBMOBILE_TARGET_LAST_TIMESTAMP", StaticMethods.getTimeSince1970());
                } else {
                    editor.remove("ADBMOBILE_TARGET_SESSION_ID");
                    editor.remove("ADBMOBILE_TARGET_LAST_TIMESTAMP");
                }
                editor.commit();
            }
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
        }
    }

    private static boolean sessionIdIsExpired() {
        try {
            SharedPreferences preferences = StaticMethods.getSharedPreferences();
            long lastRequestTimestamp = preferences.getLong("ADBMOBILE_TARGET_LAST_TIMESTAMP", 0L);
            return lastRequestTimestamp != 0 && StaticMethods.getTimeSince1970() - lastRequestTimestamp >= TARGET_SESSION_TIMEOUT_LENGTH;
        } catch (StaticMethods.NullContextException e) {
            StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
            return false;
        }
    }

    private static String getEdgeHost() {
        String str = null;
        synchronized (_edgeHostMutex) {
            if (_edgeHost == null || _edgeHost.isEmpty()) {
                try {
                    SharedPreferences preferences = StaticMethods.getSharedPreferences();
                    _edgeHost = preferences.getString("ADBMOBILE_TARGET_EDGE_HOST", null);
                } catch (StaticMethods.NullContextException e) {
                    StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
                }
            }
            str = _edgeHost;
        }
        return str;
    }

    protected static void setEdgeHost(String newEdgeHost) {
        synchronized (_edgeHostMutex) {
            _edgeHost = newEdgeHost;
            try {
                SharedPreferences.Editor editor = StaticMethods.getSharedPreferencesEditor();
                if (_edgeHost == null || _edgeHost.isEmpty()) {
                    editor.remove("ADBMOBILE_TARGET_EDGE_HOST");
                } else {
                    editor.putString("ADBMOBILE_TARGET_EDGE_HOST", _edgeHost);
                }
                editor.commit();
            } catch (StaticMethods.NullContextException e) {
                StaticMethods.logErrorFormat("Target - Error retrieving shared preferences - application context is null", new Object[0]);
            }
        }
    }

    /* JADX INFO: Access modifiers changed from: private */
    public static void forwardAnalyticsForTargetPayload(JSONObject targetResponse) {
        if (targetResponse != null && targetResponse.length() > 0) {
            try {
                JSONObject analyticsForTargetPayload = targetResponse.getJSONObject(TARGET_API_JSON_A4T_LOGGING_PAYLOAD);
                if (analyticsForTargetPayload != null && analyticsForTargetPayload.getBoolean(TARGET_API_JSON_A4T_LOGGING_ENABLED)) {
                    JSONObject analyticsForTargetPayloadParametersJson = analyticsForTargetPayload.getJSONObject(TARGET_API_JSON_A4T_LOGGING_PAYLOAD_PARAMETERS);
                    HashMap<String, Object> analyticsForTargetPayloadParametersTemp = StaticMethods.mapFromJson(analyticsForTargetPayloadParametersJson);
                    if (analyticsForTargetPayloadParametersTemp != null && analyticsForTargetPayloadParametersTemp.size() > 0) {
                        HashMap<String, Object> analyticsForTargetPayloadParameters = new HashMap<>(analyticsForTargetPayloadParametersTemp.size());
                        for (Map.Entry<String, Object> entry : analyticsForTargetPayloadParametersTemp.entrySet()) {
                            analyticsForTargetPayloadParameters.put("&&" + entry.getKey(), entry.getValue());
                        }
                        AnalyticsTrackInternal.trackInternal(TARGET_API_A4T_ACTION_NAME, analyticsForTargetPayloadParameters);
                    }
                }
            } catch (JSONException ex) {
                StaticMethods.logErrorFormat("Target - could not forward payload to Analytics (%s)", ex.getLocalizedMessage());
            }
        }
    }
}
