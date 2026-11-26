package com.google.unity.ads;

import android.os.Bundle;
import java.util.HashMap;

/* JADX INFO: loaded from: classes.dex */
public interface AdNetworkExtras {
    Bundle buildExtras(HashMap<String, String> map);

    Class getAdapterClass();
}
