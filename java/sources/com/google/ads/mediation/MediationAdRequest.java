package com.google.ads.mediation;

import android.location.Location;
import com.google.ads.AdRequest;
import java.util.Calendar;
import java.util.Date;
import java.util.Set;

/* JADX INFO: loaded from: classes.dex */
@Deprecated
public class MediationAdRequest {
    private final Date zzda;
    private final AdRequest.Gender zzdb;
    private final Set<String> zzdc;
    private final boolean zzdd;
    private final Location zzde;

    public MediationAdRequest(Date date, AdRequest.Gender gender, Set<String> set, boolean z, Location location) {
        this.zzda = date;
        this.zzdb = gender;
        this.zzdc = set;
        this.zzdd = z;
        this.zzde = location;
    }

    public Integer getAgeInYears() {
        if (this.zzda == null) {
            return null;
        }
        Calendar calendar = Calendar.getInstance();
        Calendar calendar2 = Calendar.getInstance();
        calendar.setTime(this.zzda);
        Integer numValueOf = Integer.valueOf(calendar2.get(1) - calendar.get(1));
        return (calendar2.get(2) < calendar.get(2) || (calendar2.get(2) == calendar.get(2) && calendar2.get(5) < calendar.get(5))) ? Integer.valueOf(numValueOf.intValue() - 1) : numValueOf;
    }

    public Date getBirthday() {
        return this.zzda;
    }

    public AdRequest.Gender getGender() {
        return this.zzdb;
    }

    public Set<String> getKeywords() {
        return this.zzdc;
    }

    public Location getLocation() {
        return this.zzde;
    }

    public boolean isTesting() {
        return this.zzdd;
    }
}
