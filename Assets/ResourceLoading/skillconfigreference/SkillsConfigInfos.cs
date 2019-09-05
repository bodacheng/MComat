using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class SkillsConfigInfos
{
    private static SkillsConfigInfos instance;
    public static SkillsConfigInfos Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SkillsConfigInfos();
            }
            return instance;
        }
    }
    
    public static SkillConfigTable skillConfigTable = new SkillConfigTable();
    public static IDictionary<string, SkillConfig> SkillConfigDicForReference = new Dictionary<string, SkillConfig>();
    
    public IEnumerator loadAllSkillConfigs()
    {
        switch (ResourceLoadingSetting.Instance.ConfigFileLoadingMode)
        {
            case ResourceLoadMode.CachAB:
                break;
            case ResourceLoadMode.StreamingAssetAB:
                break;
            case ResourceLoadMode.Resource:
                SkillsConfigInfos.loadAllSkillConfigFromLocalConfigFile();
                break;
        }
        refreshSkillConfigDicForReference();
        yield break;
    }
    
    public static void refreshSkillConfigDicForReference()
    {
        SkillConfigDicForReference = skillConfigTable.getSkillConfigDic();
    }
    
    public static SkillConfig getSkillConfigByID(string ID)
    {
        if (ID != null)
        {
            SkillConfig skillConfig = null;
            SkillConfigDicForReference.TryGetValue(ID, out skillConfig);
            return skillConfig;
        }else{
            return null;
        }
    }
    
    public static List<SkillConfig> getSkillConfigsOfType(string type)
    {
        List<SkillConfig> SkillConfigsOfType = new List<SkillConfig>();
        foreach (KeyValuePair<string, SkillConfig> one in SkillsConfigInfos.SkillConfigDicForReference)
        {
            if (one.Value.type == type)
                SkillConfigsOfType.Add(one.Value);
        }
        return SkillConfigsOfType;
    }
    
    public static SkillIDsAndNames getSkillIDAndNameArray(string type, bool[] ranges, int rarelevel)// close, near, far.rarelevel = -1代表全部，0代表无星级技能
    {
        List<SkillConfig> list = getSkillConfigsOfType(type);
        List<string> IDs = new List<string>();
        List<string> KeyNames = new List<string>();

        foreach (SkillConfig one in list)
        {
            if (one.rangeLimit(ranges[0], ranges[1], ranges[2],ranges[3]) && (one.rarelevel == rarelevel|| rarelevel == -1))
            {
                IDs.Add(one.id);
                KeyNames.Add(one.keyName);
            }
        }
        IDs.Add("-1");
        KeyNames.Add("null");
        SkillIDsAndNames _SkillIDsAndNames = new SkillIDsAndNames(IDs.ToArray(), KeyNames.ToArray());
        return _SkillIDsAndNames;
    }
}

public class SkillIDsAndNames
{
    public string[] IDs;
    public string[] SkillNames;

    public SkillIDsAndNames(string[] IDs, string[] SkillNames)
    {
        this.IDs = IDs;
        this.SkillNames = SkillNames;
    }
}
