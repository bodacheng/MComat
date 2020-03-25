using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HitBoxLogger
{
    static HitBoxLogger instance;
    public static HitBoxLogger Instance
    {
        get
        {
            if (instance == null)
                instance = new HitBoxLogger();
            return instance;
        }
    }

    public List<KeyValuePair<string, HitBoxLifeEnding>> HitBoxersEndings = new List<KeyValuePair<string, HitBoxLifeEnding>>();
    public IDictionary<string, int> untouchedtimes = new Dictionary<string, int>();
    public IDictionary<string, int> touchedtimes = new Dictionary<string, int>();
    public IDictionary<string, int> successedtimes = new Dictionary<string, int>();
    
    public void AddLog(string stakeKey, HitBoxLifeEnding hitBoxLifeEnding)
    {
        HitBoxersEndings.Add(new KeyValuePair<string, HitBoxLifeEnding>(stakeKey, hitBoxLifeEnding));
    }
    
    public void Clear()
    {
        HitBoxersEndings.Clear();
        untouchedtimes.Clear();
        touchedtimes.Clear();
        successedtimes.Clear();
    }
    
    public string LoadCurrentToString()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/HitBoxLog.csv"))
        {
            string level = File.ReadAllText(Application.persistentDataPath + "/HitBoxLog.csv");
            return level;
        }
        Debug.Log("hitboxlog文件缺失");
        return null;
    }

    public void LogSummit()
    {
        for (int i = 0; i < HitBoxersEndings.Count; i++)
        {
            if (HitBoxersEndings[i].Key == null)
                continue;
            switch (HitBoxersEndings[i].Value)
            {
                case HitBoxLifeEnding.untouched:
                    if (untouchedtimes.ContainsKey(HitBoxersEndings[i].Key))
                        untouchedtimes[HitBoxersEndings[i].Key] += 1;
                    else
                        untouchedtimes.Add(HitBoxersEndings[i].Key,1);
                    break;
                case HitBoxLifeEnding.touched:
                    if (touchedtimes.ContainsKey(HitBoxersEndings[i].Key))
                        touchedtimes[HitBoxersEndings[i].Key] += 1;
                    else
                        touchedtimes.Add(HitBoxersEndings[i].Key,1);
                    break;
                case HitBoxLifeEnding.successed:
                    if (successedtimes.ContainsKey(HitBoxersEndings[i].Key))
                        successedtimes[HitBoxersEndings[i].Key] += 1;
                    else
                        successedtimes.Add(HitBoxersEndings[i].Key,1);
                    break;
            }
        }
    }
}