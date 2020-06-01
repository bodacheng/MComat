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
        if (File.Exists(Application.persistentDataPath + "/HitBoxLog.csv"))
        {
            string level = File.ReadAllText(Application.persistentDataPath + "/HitBoxLog.csv");
            return level;
        }
        else
        {
            HitBoxLogTable.Instance.rowList = new List<HitBoxLogTable.Row>();
            for (int i = 0;i < SkillConfigTable.rowList.Count; i++)
            {
                if (string.IsNullOrEmpty(SkillConfigTable.rowList[i].RECORD_ID))
                    continue;
                HitBoxLogTable.Row row = new HitBoxLogTable.Row
                {
                    RECORD_ID = SkillConfigTable.rowList[i].RECORD_ID,
                    REAL_NAME = SkillConfigTable.rowList[i].REAL_NAME,
                    USEABLE_MONSTER_TYPE = SkillConfigTable.rowList[i].USEABLE_MONSTER_TYPE,
                    Untouched = "0",
                    Touched = "0",
                    Successed = "0",
                    TriggerdTimes = "0",
                    InteruptedTimes = "0"
                };
                HitBoxLogTable.Instance.rowList.Add(row);
            }
            Debug.Log("尝试新建hitboxlog");
            HitBoxLogTable.Instance.SaveByCurrentRows_HitBoxLog(Application.persistentDataPath + "/HitBoxLog.csv", null, null);
            string level = File.ReadAllText(Application.persistentDataPath + "/HitBoxLog.csv");
            return level;
        }
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